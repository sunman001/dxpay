using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JmPay.PayChannelMonitor.Models;
using JmPay.PayChannelMonitor.Scheduler;
using JmPay.PayChannelMonitor.Util;
using JmPay.PayChannelMonitor.Util.RichTextBoxExtensions;
using JMP.MDL;

namespace JmPay.PayChannelMonitor
{
    public partial class FrmMain : Form
    {
        private readonly ScheduleDictionary _scheduleDictionary = new ScheduleDictionary();

        private List<ScheduleGroupViewModel> _scheduleGroupViewModelList;

        #region 配置选项

        private void InitConfig()
        {
            BindConfig();
            ApplyConfig();
        }

        private void BindConfig()
        {
            txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Text = Properties.Settings.Default.TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.ToString();
        }

        private void ApplyConfig()
        {
            GlobalConfig.TIMESPAN_NO_ORDER_APP_SEND_MESSAGE = Convert.ToInt32(txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Text);
        }

        private void SaveConfig()
        {
            Properties.Settings.Default.TIMESPAN_NO_ORDER_APP_SEND_MESSAGE = Convert.ToInt32(txt_TIMESPAN_NO_ORDER_APP_SEND_MESSAGE.Text);
            Properties.Settings.Default.Save();
        }

        private void SaveAndApply()
        {
            SaveConfig();
            ApplyConfig();
        }
        #endregion

        private void BindingScheduleGroupToDataGrid()
        {
            _scheduleGroupViewModelList = GlobalConfig.SchedulerGroup.Where(x => !x.IsDeleted && x.IsEnabled).Select(x => new ScheduleGroupViewModel
            {
                Name = x.Name,
                Code = x.Code,
                IntervalValue = x.IntervalValue,
                IntervalUnit = x.IntervalUnit,
                MessageTemplate = x.MessageTemplate,
                IsDeleted = x.IsDeleted,
                IsEnabled = x.IsEnabled,
                Status = ScheduleStatus.Pending
            }).ToList();
            dgvSchedulers.DataSource = _scheduleGroupViewModelList;
        }

        private void Init()
        {
            //初始化全局变量
            GlobalConfig.InitGlobalConfig(_scheduleDictionary);
            BindingScheduleGroupToDataGrid();
        }

        #region 日志
        private void WriteTo(Level level, string message)
        {
            if (rtxtLog.Lines.Length > 2000)
            {
                rtxtLog.Clear();
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
            rtxtLog.AppendColorfulText(string.Format("[{0}]{1}==>>{2}", level.ToString().ToUpper(), DateTime.Now, message.Replace("\r\n", "")), color);
        }

        private void UpdateUi(string message, Level level = Level.Default)
        {
            try
            {
                rtxtLog.Invoke(new Action(() =>
                {
                    if (rtxtLog.Lines.Length > 2000)
                    {
                        rtxtLog.Clear();
                    }
                    //rtxtLog.AppendColorfulText(string.Format("{0}==>>{1}", DateTime.Now, message), color);
                    WriteTo(level, message);
                }));
            }
            catch { }
        }
        #endregion

        #region 控件
        public FrmMain()
        {
            InitializeComponent();
            try
            {
                var frmTitle = ConfigurationManager.AppSettings["FrmTitle"];
                if (!string.IsNullOrEmpty(frmTitle))
                {
                    this.Text = frmTitle;
                }
                InitConfig();
            }
            catch (Exception ex)
            {
                UpdateUi(string.Format("程序启动时出错,原因:{0}", ex), Level.Error);
            }
            //var intervalDefaultValue = ConfigReader.GetSettingValueByKey("INTERVAL_DEFAULT_VALUE");
            //txt_INTERVAL_APP_PAY_SUCCESS_ABNORMAL.Text = intervalDefaultValue;
            //txt_INTERVAL_ORDER_PAY_ABNORMAL.Text = intervalDefaultValue;
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            try
            {
                _scheduleDictionary.OnDoingJob += _scheduleDictionary_OnDoingJob;
                _scheduleDictionary.InitDict();
                Init();
                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    AutoRunSchedule();
                });
                //tbtnStart_Click(null, null);

                //tbtnStartAuditorSendMessage_Click(null, null);

                //btnStartNoOrderApp_Click(null, null);

            }
            catch (Exception ex)
            {
                UpdateUi(string.Format("程序启动时出错,原因:{0}", ex), Level.Error);
            }
        }

        private void _scheduleDictionary_OnDoingJob(string obj, Level? level)
        {
            if (level == null)
            {
                UpdateUi(obj);
            }
            else
            {
                UpdateUi(obj, (Level)level);
            }

        }

        private void Jobber_OnScheduleAdding(Level level, string message)
        {
            UpdateUi(message, level);
        }

        private void Jobber_OnScheduleAddedCompleted(object sender, CustomEvent.JobberEvent e)
        {
            statusLeft.Text = string.Format("任务初始化完成,队列任务总数:[{0}]个", e.ScheduleCount);
            //btnStartAllSchedules.Enabled = false;
            //btnStopAllSchedules.Enabled = true;
        }

        private void Jobber_OnAddingScheduleError(string message)
        {
            UpdateUi(message, Level.Error);
        }

        private void btnApplyConfig_Click(object sender, EventArgs e)
        {
            ApplyConfig();
            MessageBox.Show("配置选项应用成功");
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            SaveAndApply();
            MessageBox.Show("配置选项保存成功");
        }
        #endregion

        private void btnStartAllSchedules_Click(object sender, EventArgs e)
        {
            Init();
            var jobber = new Jobber();
            jobber.OnAddingScheduleError += Jobber_OnAddingScheduleError;
            jobber.OnScheduleAddedCompleted += Jobber_OnScheduleAddedCompleted;
            jobber.OnScheduleAdding += Jobber_OnScheduleAdding;
            _scheduleGroupViewModelList.ForEach(x => ToggleStatus(x));
            GlobalConfig.RUNNIGN_SCHEDULE_GROUP_LIST.AddRange(_scheduleGroupViewModelList.Select(x => x.Code));
            jobber.AddSchedules(GlobalConfig.SchedulerGroup);
            dgvSchedulers.Refresh();
        }

        private void btnStopAllSchedules_Click(object sender, EventArgs e)
        {
            var jobber = new Jobber();
            jobber.OnAddingScheduleError += Jobber_OnAddingScheduleError;
            jobber.OnScheduleAddedCompleted += Jobber_OnScheduleAddedCompleted;
            jobber.OnScheduleAdding += Jobber_OnScheduleAdding;
            jobber.RemoveAllSchedules();
            _scheduleGroupViewModelList.ForEach(x => ToggleStatus(x));
            dgvSchedulers.Refresh();
            btnStartAllSchedules.Enabled = true;
            btnStopAllSchedules.Enabled = false;
        }

        private void dgvSchedulers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSchedulers.SelectedRows.Count <= 0)
            {
                return;
            }
            var rowIndex = dgvSchedulers.SelectedCells[0].RowIndex;
            var selectedRow = dgvSchedulers.Rows[rowIndex];
            var code = selectedRow.Cells["Code"].Value.ToString();
            //MessageBox.Show(code);
        }

        private void dgvSchedulers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var colName = dgvSchedulers.Columns[e.ColumnIndex].Name;
            if (colName != "Status")
            {
                return;
            }
            var row = dgvSchedulers.Rows[e.RowIndex];
            var code = dgvSchedulers.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            var sg = _scheduleGroupViewModelList.Find(x => x.Code == code);
            SwitchStatus(sg, row);
        }

        private void SwitchStatus(ScheduleGroupViewModel sg, DataGridViewRow row)
        {
            sg = ToggleStatus(sg);
            switch (sg.Status)
            {
                case ScheduleStatus.Pending:
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    break;
                case ScheduleStatus.Running:
                    row.DefaultCellStyle.ForeColor = Color.Green;
                    break;
            }
            dgvSchedulers.Refresh();
        }

        private ScheduleGroupViewModel ToggleStatus(ScheduleGroupViewModel scheduleGroup)
        {
            var jobber = new Jobber();
            jobber.OnAddingScheduleError += Jobber_OnAddingScheduleError;
            jobber.OnScheduleAddedCompleted += Jobber_OnScheduleAddedCompleted;
            jobber.OnScheduleAdding += Jobber_OnScheduleAdding;
            switch (scheduleGroup.Status)
            {
                case ScheduleStatus.Pending:
                    scheduleGroup.Status = ScheduleStatus.Running;
                    GlobalConfig.RUNNIGN_SCHEDULE_GROUP_LIST.Add(scheduleGroup.Code);

                    jobber.AddSchedules(new List<jmp_notificaiton_group> {new jmp_notificaiton_group
                    {
                        Code=scheduleGroup.Code,
                        IntervalUnit=scheduleGroup.IntervalUnit,
                        IntervalValue=scheduleGroup.IntervalValue,
                        IsDeleted=scheduleGroup.IsDeleted,
                        IsEnabled=scheduleGroup.IsEnabled,
                        Name=scheduleGroup.Name
                    } });
                    break;
                case ScheduleStatus.Running:
                    scheduleGroup.Status = ScheduleStatus.Pending;
                    GlobalConfig.RUNNIGN_SCHEDULE_GROUP_LIST.Remove(scheduleGroup.Code);
                    jobber.RemoveSchedule(scheduleGroup.Code);
                    break;
            }
            return scheduleGroup;
        }

        private void tbtnReloadScheduleGroupData_Click(object sender, EventArgs e)
        {
            var jobber = new Jobber();
            jobber.OnAddingScheduleError += Jobber_OnAddingScheduleError;
            jobber.OnScheduleAddedCompleted += Jobber_OnScheduleAddedCompleted;
            jobber.OnScheduleAdding += Jobber_OnScheduleAdding;
            jobber.RemoveAllSchedules();
            btnClearLog_Click(null, null);
            Init();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtxtLog.Clear();
        }

        private void AutoRunSchedule()
        {
            Invoke(new Action(() =>
            {
                var lst = _scheduleGroupViewModelList.ToList();
                for (var i = 0; i < lst.Count; i++)
                {
                    var x = lst[i];
                    var row = dgvSchedulers.Rows[i];
                    SwitchStatus(x, row);
                    Thread.Sleep(500);
                }
            }));
        }
    }
}
