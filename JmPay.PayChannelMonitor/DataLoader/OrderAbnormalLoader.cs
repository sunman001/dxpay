namespace JmPay.PayChannelMonitor.DataLoader
{
    public class OrderAbnormalLoader :IDataLoader
    {
        public object Load(string procName)
        {
            var bll=new JMP.BLL.jmp_order_audit();
            var dt = bll.GetList(5, "is_send_message=0", "id ASC").Tables[0];
            var items = bll.DataTableToList(dt);
            return items;
        }
    }
}
