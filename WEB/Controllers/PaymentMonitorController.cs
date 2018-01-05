using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using JMP.MDL;
using JMP.TOOL;
using TOOL;
using TOOL.Message;
using TOOL.Message.TextMessage.ChuangLan;
using WEB.Extensions.PanChannelMonitor;
using System.Linq;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    /// <summary>
    /// 支付通道状态检测控制器
    /// </summary>
    public class PaymentMonitorController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        /// <summary>
        /// 支付通道检测接口
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public ContentResult Monitor()
        {
            var tmpId = Request.QueryString["tid"] ?? "0";
            var auto = Request.QueryString["auto"] ?? "0";
            var tid = Convert.ToInt32(tmpId);
            if (tid == 0)
            {
                return new ContentResult
                {
                    Content = "TID参数错误"
                };
            }
            //根据通道ID获取通道的KEY的签名
            var channel = new JMP.BLL.jmp_interface().GetModel(tid);
            //获取通道的支付类型
            var paymentTypeModel = new JMP.BLL.jmp_paymenttype().GetModel(channel.l_paymenttype_id);
            if (paymentTypeModel.p_type > 2)
            {
                return new ContentResult
                {
                    Content = "暂时不支持【支付宝，微信】以外的通道检测！"
                };
            }
            //需要排除通道检测的通道编码集合
            var excludes = new List<string> { "MPAPI" };
            if (excludes.Contains(paymentTypeModel.p_extend))
            {
                return new ContentResult
                {
                    Content = "被排除检测的支付通道！"
                };
            }
            //创建通道接口实例
            var payChannelMonitor = PayChannelFactory.Creator(paymentTypeModel.p_type);
            payChannelMonitor.AllowCheck = true;
            if (auto == "1")
                payChannelMonitor.AllowAutoCheck = true;
            else
                payChannelMonitor.AllowAutoCheck = false;
            payChannelMonitor.Tid = tid;
            //当检测为自动模式并存在于今天的通道中
            if (payChannelMonitor.AllowAutoCheck == true && paymentTypeModel.p_type == 1 && OrderMonitor(tid))
            {
                return new ContentResult
                {
                    Content = "跳过未使用支付宝通道的自动检测"
                };
            }
            //检测支付通道是否正常
            var success = CheckPayChannel(payChannelMonitor);

            if (success)
            {
                var content = string.Format("通道[{0}(编号:{1})]检测完成,状态:{2}", channel.l_corporatename, channel.l_id, success ? "正常" : "异常");
                var monitor = payChannelMonitor as ZhiFuBaoPayChannelMonitor;
                if (monitor != null && payChannelMonitor.AllowAutoCheck == false)
                {
                    content = monitor.AliPayForm;
                }
                return new ContentResult
                {
                    Content = content
                };
            }
            else
            {
                //支付通道异常,并自动检测时发送提示短信
                if (payChannelMonitor.AllowAutoCheck == true)
                {
                    SendMessage(channel);
                }
            }
            return new ContentResult
            {
                Content = string.Format("通道[{0}(编号:{1})]检测完成,状态:{2}", channel.l_corporatename, channel.l_id, success ? "正常" : "异常")
            };
        }
        /// <summary>
        /// 判断当前支付宝通道是否在今日列表中
        /// </summary>
        /// <returns></returns>
        public bool OrderMonitor(int tid)
        {

            string hcmc = "dqsyzfb";
            System.Collections.Generic.List<JMP.Model.Query.OrderedInterface> lst;
            if (JMP.TOOL.CacheHelper.IsCache(hcmc))
            {
                lst = JMP.TOOL.CacheHelper.GetCaChe<System.Collections.Generic.List<JMP.Model.Query.OrderedInterface>>(hcmc);
            }
            else
            {
                var interBll = new JMP.BLL.jmp_interface();
                lst = interBll.GetTodayOrderedInterfaces(WeekDateTime.GetCurrentOrderTableName);
                JMP.TOOL.CacheHelper.CacheObjectLocak<System.Collections.Generic.List<JMP.Model.Query.OrderedInterface>>(lst, hcmc, 10);
            }

            int sl = lst.Where(x => (x.InterfaceId == tid)).Count();

            if (sl > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据监控器接口实现检测通道状态
        /// </summary>
        /// <param name="payChannelMonitor">通道监控器接口实现</param>
        /// <returns></returns>
        private bool CheckPayChannel(IPayChannelMonitor payChannelMonitor)
        {
            //检测2次
            var maxRetries = 2;
            bool success;
            do
            {
                try
                {
                    if (payChannelMonitor.AllowAutoCheck == true && payChannelMonitor is ZhiFuBaoPayChannelMonitor)
                    {
                        success = payChannelMonitor.checkorder();
                    }
                    else
                    {
                        success = payChannelMonitor.Check();
                    }
                    if (success)
                    {
                        break;
                    }
                    maxRetries--;
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    success = false;
                    AddLocLog.AddLog(1, 4, Request.UserHostAddress, "通道状态检测", ex.ToString());
                    break;
                }
            } while (maxRetries > 0);
            return success;
        }

        /// <summary>
        /// 发送通知短信
        /// </summary>
        /// <param name="channel"></param>
        private void SendMessage(jmp_interface channel)
        {
            //TODO:发送短信提示
            var request = new ChuangLanRequest
            {
                Mobile = ConfigReader.GetSettingValueByKey("CHUANGLAN.MOBILE.MONITOR"),
                Content = string.Format(ConfigReader.GetSettingValueByKey("CHUANGLAN.CONTENT.MONITOR"), channel.l_corporatename, channel.l_id)
            };
            try
            {
                IMessageSender messageSender = new ChuangLanMessageSender(request);
                messageSender.Send();
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, Request.UserHostAddress, "通道状态检测", ex.ToString());
            }
        }

    }
}
