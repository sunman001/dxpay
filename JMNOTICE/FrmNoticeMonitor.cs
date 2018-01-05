using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentScheduler;
using JMNOTICE.CustomEvent;
using JMNOTICE.ScheduleManager;
using JMNOTICE.Util;
using JMNOTICE.Util.ListBoxExt;
using JMNOTICE.Util.LogManager;
using JMNOTICE.Util.RichTextBoxExtensions;
using TOOL;

namespace JMNOTICE
{
    /// <summary>
    /// 订单通知程序窗体
    /// </summary>
    public partial class FrmNoticeMonitor : Form
    {
        #region 私有字段

        /// <summary>
        /// 作业调度器
        /// </summary>
        private Jobber _jobber;
        /// <summary>
        /// 配置文件中的所有任务配置选项,形如:0.1.0;1.2.0;2.3.0;3.4.0;4.6.0;5.10.0;6.11.0;7.12.0;8.12.1;10.2.1;11.2.1;12.2.1
        /// 解释:通知次数.间隔时间(秒).是否需要删除通知数据;通知次数.间隔时间(秒).是否需要删除通知数据
        /// </summary>
        //private string _notifyTasksCron = "";
        /// <summary>
        /// 订单通知配置集合实体
        /// </summary>
        private TaskCronConfig _taskCronConfig;

        /// <summary>
        /// 日志接口类
        /// </summary>
        private ILogger _logger;
        #endregion

        #region 全局配置选项

        /// <summary>
        /// 初始化全局配置选项
        /// </summary>
        private void InitGlobalConfig()
        {
            BindGlobalConfig();
            ApplyGlobalConfig();
            CheckAllowReadDataFromDatabase();
        }
        /// <summary>
        /// 应用全局配置选项
        /// </summary>
        private void ApplyGlobalConfig()
        {
            GlobalConfig.MAX_THREAD_COUNT = Convert.ToInt32(txt_MAX_THREAD_COUNT.Text);
            GlobalConfig.EACH_TIME_SELECT_TOP_COUNT = Convert.ToInt32(txt_EACH_TIME_SELECT_TOP_COUNT.Text);
            GlobalConfig.LOOP_LOAD_DATA_THRESHOLD = Convert.ToInt32(txt_LOOP_LOAD_DATA_THRESHOLD.Text);
            GlobalConfig.TASK_CRONS_CONFIG_STRING = txt_TASK_CRONS_CONFIG_STRING.Text.Trim();
            GlobalConfig.CLEAR_LOG_THRESHOLD = Convert.ToInt32(txt_CLEAR_LOG_THRESHOLD.Text);
            GlobalConfig.ENCRYPT_IP_ADDRESS = txt_ENCRYPT_IP_ADDRESS.Text.Trim();
            GlobalConfig.LOG_DIRECTORY = Path.Combine(Application.StartupPath, "log");
            GlobalConfig.CLIENT_ID = txt_CLIENT_ID.Text.Trim();
            //CreateLogDirectoryIfNotExists();
        }

        /// <summary>
        /// 检查是否允许从数据库加载数据
        /// </summary>
        private void CheckAllowReadDataFromDatabase()
        {
            GlobalConfig.AllowReadDataFromDatabase = true;
            if (!Properties.Settings.Default.NEED_CHECK_FROM_DATABASE)
            {
                return;
            }
            if (string.IsNullOrEmpty(GlobalConfig.CLIENT_ID))
            {
                GlobalConfig.AllowReadDataFromDatabase = false;
                UpdateUi(Level.Error, "此程序的客户端标识未配置");
                return;
            }
            var bll=new JMP.BLL.jmp_system();
            var item = bll.GetModelList(string.Format("s_state=1 AND s_name='{0}'", GlobalConfig.CLIENT_ID)).FirstOrDefault();
            if (item != null)
            {
                GlobalConfig.AllowReadDataFromDatabase = item.s_value == "1";
            }
            if (!GlobalConfig.AllowReadDataFromDatabase)
            {
                UpdateUi(Level.Error, string.Format("客户端[{0}]的通知功能已被禁用", GlobalConfig.CLIENT_ID));
            }
        }

        /// <summary>
        /// 从配置文件加载全局配置选项并绑定到控件
        /// </summary>
        private void BindGlobalConfig()
        {
            txt_MAX_THREAD_COUNT.Text = Properties.Settings.Default.MAX_THREAD_COUNT.ToString();
            txt_EACH_TIME_SELECT_TOP_COUNT.Text = Properties.Settings.Default.EACH_TIME_SELECT_TOP_COUNT.ToString();
            txt_LOOP_LOAD_DATA_THRESHOLD.Text = Properties.Settings.Default.LOOP_LOAD_DATA_THRESHOLD.ToString();
            txt_TASK_CRONS_CONFIG_STRING.Text = Properties.Settings.Default.TASK_CRONS_CONFIG_STRING.Trim();
            txt_CLEAR_LOG_THRESHOLD.Text = Properties.Settings.Default.CLEAR_LOG_THRESHOLD.ToString();
            txt_ENCRYPT_IP_ADDRESS.Text = Properties.Settings.Default.ENCRYPT_IP_ADDRESS.Trim();
            txt_CLIENT_ID.Text = Properties.Settings.Default.CLIENT_ID.Trim();
            chkNEED_CHECK_FROM_DATABASE.Checked = Properties.Settings.Default.NEED_CHECK_FROM_DATABASE;
        }

        /// <summary>
        /// 持久化全局配置选项到配置文件
        /// </summary>
        private void SaveGlobalConfig()
        {
            ApplyGlobalConfig();
            Properties.Settings.Default.MAX_THREAD_COUNT = Convert.ToInt32(txt_MAX_THREAD_COUNT.Text);
            Properties.Settings.Default.EACH_TIME_SELECT_TOP_COUNT = Convert.ToInt32(txt_EACH_TIME_SELECT_TOP_COUNT.Text);
            Properties.Settings.Default.LOOP_LOAD_DATA_THRESHOLD = Convert.ToInt32(txt_LOOP_LOAD_DATA_THRESHOLD.Text);
            Properties.Settings.Default.TASK_CRONS_CONFIG_STRING = txt_TASK_CRONS_CONFIG_STRING.Text.Trim();
            Properties.Settings.Default.CLEAR_LOG_THRESHOLD = Convert.ToInt32(txt_CLEAR_LOG_THRESHOLD.Text);
            Properties.Settings.Default.ENCRYPT_IP_ADDRESS = txt_ENCRYPT_IP_ADDRESS.Text.Trim();
            Properties.Settings.Default.CLIENT_ID = txt_CLIENT_ID.Text.Trim();
            Properties.Settings.Default.NEED_CHECK_FROM_DATABASE = chkNEED_CHECK_FROM_DATABASE.Checked ;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 从配置文件加载订单通知任务
        /// </summary>
        private void LoadNotifyTasksCron()
        {
            try
            {
                UpdateUi(Level.Info, "正在从配置文件加载订单通知任务...");
                var settingsProperty = GlobalConfig.TASK_CRONS_CONFIG_STRING; //Properties.Settings.Default.NotifyTasksCron;
                if (settingsProperty == null)
                {
                    UpdateUi(Level.Warning, "订单通知任务配置加载失败,请检查配置!!!");
                    return;
                }
                //_notifyTasksCron = settingsProperty;
                _taskCronConfig = new TaskCronConfig(GlobalConfig.TASK_CRONS_CONFIG_STRING);
                _taskCronConfig.OnConvertError += TaskConvertError;
                _taskCronConfig.ConvertTaskCron();
                UpdateUi(Level.Info, "订单通知任务配置加载完成.");
            }
            catch (Exception ex)
            {
                UpdateUi(Level.Error, ex.ToString());
            }
        }

        private void TaskConvertError(string message)
        {
            UpdateUi(Level.Error, message);
        }

        /// <summary>
        /// 如果日志目录不存在,则创建
        /// </summary>
        private void CreateLogDirectoryIfNotExists()
        {
            if (!Directory.Exists(GlobalConfig.LOG_DIRECTORY))
            {
                Directory.CreateDirectory(GlobalConfig.LOG_DIRECTORY);
            }
        }
        #endregion

        #region 订单通知作业
        /// <summary>
        /// 开始作业
        /// </summary>
        private void StartJob()
        {
            //通知次数.间隔时间(秒).是否需要删除通知数据;通知次数.间隔时间(秒).是否需要删除通知数据
            /* 0.1.0;1.5.0;2.10.0;3.30.0;4.60.0;5.300.0;6.600.0;7.1800.0;8.3600.1;10.1.1;11.1.1;12.1.1
            */
            
            if (string.IsNullOrEmpty(GlobalConfig.TASK_CRONS_CONFIG_STRING))
            {
                return;
            }
            LoadNotifyTasksCron();
            UpdateUi(Level.Info, string.Format("正在初始化订单通知任务,共{0}个任务...", _taskCronConfig.TotalTask));
            try
            {
                _jobber.AddSchedules(_taskCronConfig);
            }
            catch (Exception ex)
            {
                UpdateUi(Level.Error,string.Format("创建任务时出错,原因:{0}",ex));
            }
            UpdateUi(Level.Info, string.Format("订单通知任务初始化完成,共{0}个任务.", _taskCronConfig.TotalTask));
        }

        /// <summary>
        /// 停止所有作业
        /// </summary>
        private void StopJob()
        {
            _jobber.RemoveAllSchedules();
        }
        /// <summary>
        /// 添加作业时触发事件的方法
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private void OnScheduleAdding(Level level, string message)
        {
            UpdateUi(level, message);
        }
        private void AddingScheduleError(string message)
        {
            UpdateUi(Level.Error, message);
        }

        private void OnScheduleAddedCompleted(object sender, JobberEvent e)
        {
            statusLeft.Text = string.Format("所有任务创建成功,队列总任务{0}个.", e.ScheduleCount);
        }

        #endregion

        #region 日志
        /// <summary>
        /// 清空日志
        /// </summary>
        private void ClearLog()
        {
            try
            {
                lstLog.Invoke(new Action(() =>
                {
                    lstLog.Text = "";
                }));
            }
            catch
            {
            }
        }

        private void WriteTo(Level level, string message)
        {
            try
            {
                if (lstLog.Lines.Length > GlobalConfig.CLEAR_LOG_THRESHOLD)
                {
                    ClearLog();
                    SaveLogToDatabase();
                }
                Color color;
                switch (level)
                {
                    case Level.Critical:
                        color = Color.Orange;
                        break;
                    case Level.Error:
                        color = Color.Red;
                        break;
                    case Level.Warning:
                        color = Color.Goldenrod;
                        break;
                    case Level.Info:
                        color = Color.Green;
                        break;
                    case Level.Verbose:
                        color = Color.Blue;
                        break;
                    default:
                        color = Color.Black;
                        break;
                }
                lstLog.AppendColorfulText(
                    string.Format("[{0}]{1}==>>{2}", level.ToString().ToUpper(), DateTime.Now, message), color);
            }
            catch
            {
            }
        }

        private List<string> ReadErrorLogLines()
        {
            var lst = lstLog.Lines.Where(x => !x.StartsWith("[INFO]") && x.Trim().Length > 0).ToList();
            return lst;
        }

        private void SaveLogToDatabase()
        {
            try
            {
                var logs = ReadErrorLogLines();
                Task.Run(() =>
                {
                    _logger.Write(logs);
                }).ContinueWith(t =>
                {
                    ClearLog();
                });
            }
            catch { }
        }

        /// <summary>
        /// 更新日志UI
        /// </summary>
        /// <param name="level">日志消息级别</param>
        /// <param name="message">日志消息</param>
        private void UpdateUi(Level level, string message)
        {
            try
            {
                if (message.Trim().Length > 0)
                {
                    message = message.Replace("\r\n","");
                }
                lstLog.Invoke(new Action(() =>
                {
                    WriteTo(level, message);
                }));
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        
        /// <summary>
        /// 记录程序启动时间
        /// </summary>
        private void RecordApplicationRunTime()
        {
            Task.Run(() =>
            {
                LogWritterFactory.Write("订单通知程序启动", string.Format("订单通知程序启动时间:{0}", GlobalPara.ApplicationStartTime));
            });
        }

        private void JobManager_JobException(JobExceptionInfo obj)
        {
            LogWritterFactory.Write("订单通知程序作业异常",string.Format("订单通知程序作业[{0}]异常:原因:{1}", obj.Name, obj.Exception));
        }

        private void DoTask()
        {
            try
            {
                JobManager.AddJob(() =>
                    {
                        try
                        {
                            CheckAllowReadDataFromDatabase();
                        }
                        catch (Exception ex)
                        {
                            UpdateUi(Level.Error, string.Format("检测是否允许从数据库加载数据时出错[内部],原因:{0}", ex));
                        }
                    },
                    t => t.WithName("TASK_CHECK_ALLOW_READ_DATA_FROM_DB").NonReentrant().ToRunEvery(5).Minutes());
            }
            catch (Exception ex)
            {
                UpdateUi(Level.Error, string.Format("检测是否允许从数据库加载数据时出错,原因:{0}",ex));
            }
        }

        public FrmNoticeMonitor()
        {
            InitializeComponent();
        }

        private void FrmNoticeMonitor_Load(object sender, EventArgs e)
        {
            try
            {
                JobManager.JobException += JobManager_JobException;
                //_appStartDateTime = DateTime.Now;
                GlobalPara.ApplicationStartTime = DateTime.Now;
                statusRight.Text = "程序启动时间:刚刚";
                //1.初始化全局配置选项
                InitGlobalConfig();

                //2.实例化日志收集器
                _logger = new DbLogger();

                //3.从配置文件加载订单通知任务
                //LoadNotifyTasksCron();

                //4.实体化订单通知调度器
                _jobber = new Jobber();
                _jobber.OnScheduleAdding += OnScheduleAdding;
                _jobber.OnScheduleAddedCompleted += OnScheduleAddedCompleted;
                _jobber.OnAddingScheduleError += AddingScheduleError;
                //5.启用订单通知作业
                btnStartOrderNotifyJob_Click(null, null);

                //记录程序启动时间
                RecordApplicationRunTime();
                DoTask();
            }
            catch (Exception ex)
            {
                UpdateUi(Level.Error, "订单通知程序初始化出错,原因:" + ex);
            }
        }

        private void btnStartOrderNotifyJob_Click(object sender, EventArgs e)
        {
            StartJob();
            btnStartOrderNotifyJob.Enabled = false;
            btnStopOrderNotifyJob.Enabled = true;
        }

        private void btnStopOrderNotifyJob_Click(object sender, EventArgs e)
        {
            StopJob();
            btnStartOrderNotifyJob.Enabled = true;
            btnStopOrderNotifyJob.Enabled = false;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            SaveLogToDatabase();
            //ClearLog();
        }

        private void btnApplyGlobalConfig_Click(object sender, EventArgs e)
        {
            ApplyGlobalConfig();
        }

        private void btnSaveGlobalConfig_Click(object sender, EventArgs e)
        {
            SaveGlobalConfig();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            SaveLogToDatabase();
        }

        private void FrmNoticeMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLogToDatabase();
            Application.Exit();
        }

        private void timerAppStartTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                statusRight.Text = string.Format("程序启动时间:{0},已运行{1}", GlobalPara.ApplicationStartTime,
                    GlobalPara.ApplicationStartTime.TimeAgo(""));
                lbl_GlobalInfo.Text = GlobalPara.Info;
            }
            catch (Exception ex)
            {
                UpdateUi(Level.Error,string.Format("更新UI时出错,原因:{0}",ex));
            }
        }
    }
}
