using System;
using JMP.MDL;

namespace JMP.TOOL.Auditor
{
    /// <summary>
    /// 订单异常拦截核查器
    /// </summary>
    public class OrderAbnormalAuditor : IAuditor
    {
        private readonly jmp_order_audit _orderAuditor;
        /// <summary>
        /// 订单异常拦截核查器构造器
        /// </summary>
        /// <param name="orderCode">盾行的订单编码唯一</param>
        /// <param name="orderTableName">盾行的订单编码对应的订单表</param>
        /// <param name="appId">应用ID</param>
        /// <param name="message">错误消息</param>
        /// <param name="tradeNo">交易流水号(支付回调)</param>
        /// <param name="paymentTime">支付时间(支付回调)</param>
        /// <param name="paymentAmount">实际支付金额(支付回调)</param>
        /// <param name="paymentStatus">支付状态(支付回调)</param>
        /// <param name="orderAmount">订单金额(在盾行生成订单时的金额)</param>
        public OrderAbnormalAuditor(string orderCode, string orderTableName, int appId, string message, string tradeNo, DateTime paymentTime, decimal paymentAmount, string paymentStatus, decimal orderAmount)
        {
            try
            {
                _orderAuditor = new jmp_order_audit
                {
                    app_id = appId,
                    created_on = DateTime.Now,
                    message = message,
                    order_amount = orderAmount,
                    order_code = orderCode,
                    order_table_name = orderTableName,
                    payment_amount = paymentAmount,
                    payment_status = paymentStatus,
                    payment_time = paymentTime,
                    trade_no = tradeNo
                };
            }
            catch { }
        }
        public bool Add()
        {
            try
            {
                if (_orderAuditor == null)
                {
                    return false;
                }
                var bll = new JMP.BLL.jmp_order_audit();
                bll.Add(_orderAuditor);
                return true;
            }
            catch(Exception ex)
            {
                AddLocLog.AddLog(1, 3, "", "添加应用异常请求监控器时出错", ex.ToString());
                return false;
            }
        }
    }
}
