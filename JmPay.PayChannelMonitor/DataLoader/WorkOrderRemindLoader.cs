namespace JmPay.PayChannelMonitor.DataLoader
{
    /// <summary>
    /// 新工单提醒任务数据源加载器
    /// </summary>
    public class WorkOrderRemindLoader : IDataLoader
    {
        public object Load(string procName)
        {
            var bll=new JMP.BLL.jmp_workorder();
            return bll.GetUnRemindWorkOrders();
        }
    }
}
