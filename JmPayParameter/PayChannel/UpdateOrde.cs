using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter.PayChannel
{
    /// <summary>
    ///修改订单接口
    /// </summary>
    public static class UpdateOrde
    {
        /// <summary>
        /// 根据id修改支付通道(非appid方式)
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="payid">支付通道id</param>
        /// <returns></returns>
        public static bool OrdeUpdateInfo(int oid, int payid)
        {
            bool isSuccess = false;
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, payid))
            {
                //AddLocLog.AddLog(1, 4, "", "修改支付通道报错", "修改支付渠道失败！订单id：" + oid.ToString() + ",通道id：" + payid);//写入报错日志
                PayApiGlobalErrorLogger.Log("修改支付渠道失败！订单id：" + oid + ",通道id：" + payid, summary: "修改支付通道报错");
            }
            else
            {
                isSuccess = true;
            }
            return isSuccess;
        }
    }

    public class UpdateOrdes
    {
        /// <summary>
        /// 根据id修改支付通道(非appid方式)
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="payid">支付通道id</param>
        /// <returns></returns>
        public bool OrdeUpdateInfo(int oid, int payid, string code)
        {
            bool isSuccess = false;
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, payid))
            {
                PayApiGlobalErrorLogger.Log("修改支付渠道失败！订单id：" + oid + ",通道id：" + payid + ",订单编号：" + code, summary: "修改支付通道报错");
            }
            else
            {
                isSuccess = true;
            }
            return isSuccess;
        }
    }
}
