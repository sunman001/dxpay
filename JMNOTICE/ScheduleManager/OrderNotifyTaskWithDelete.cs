using JMNOTICE.Models;
using JMNOTICE.Util;
using JMP.DBA;
using System.Collections.Generic;

namespace JMNOTICE.ScheduleManager
{
    public class OrderNotifyTaskWithDelete : OrderNotifyTask
    {
        /// <summary>
        /// 通用的订单通知任务(本次通知完成后会删除数据库通知队列的数据)
        /// </summary>
        /// <param name="dataLoader">数据加载器接口实例</param>
        /// <param name="taskCron">订单通知的配置选项</param>
        public OrderNotifyTaskWithDelete(IDataLoader<OrderNotifyQueueListModel> dataLoader, TaskCron taskCron) : base(dataLoader, taskCron)
        {
        }
        public override void AfterSendNotify(OrderNotifyQueueListModel notify, string success)
        {
            var notifySuccess = success.Trim().ToLower() == "success" ? 1 : -1;

            var _success = DbHelperSQL.ExecuteSql("update jmp_order set o_noticestate=" + notifySuccess + ",o_noticetimes=GETDATE(),o_times=" + (notify.q_times + 1) + " where o_id=" + notify.q_o_id);
            //更新队列表
            if (_success <= 0)
            {
                DbHelperSQL.ExecuteSql("update " + notify.q_tablename + " set o_noticestate=" + notifySuccess + ",o_noticetimes=GETDATE(),o_times=" + (notify.q_times + 1) + " where o_code='" + notify.trade_code + "'");
            }

            //var updateOrder = "update " + notify.q_tablename + " set o_noticestate=" + notifySuccess +",o_noticetimes='" + DateTime.Now.ToString(CultureInfo.InvariantCulture) +"',o_times=" + (notify.q_times + 1) + " where o_id=" + notify.q_o_id;


            //不管通知成功与否,都从订单通知队列表中删除此条记录
            var deleteFromQueueList = "delete from jmp_queuelist where q_id=" + notify.q_id;
            JMP.TOOL.AddLocLog.AddUserLog(0, 5, "", string.Format("永久移除订单通知队列,订单号:{0},通知次数:{1},通知结果:{2},归档表:{3}", notify.trade_code, notify.q_times + 1, success, notify.q_tablename), "通知程序队列移除操作");//写入报错日志
            DbHelperSQL.ExecuteSqlTran(new List<string>
            {
                deleteFromQueueList
            });
        }

    }
}
