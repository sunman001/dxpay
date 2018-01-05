using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace JmPayParameter.PayChannel
{
    /// <summary>
    /// 查询支付通道
    /// </summary>
    public class SelectPayChannel
    {
        private static JMP.BLL.jmp_paymenttype paybll = new JMP.BLL.jmp_paymenttype();
        /// <summary>
        /// 查询支付通达
        /// </summary>
        /// <param name="paytrpe">支付类型(1:支付宝，2：微信，3：银联，4：微信公众号，5：微信appid，6微信扫码，7：支付宝扫码)</param>
        /// <param name="paymode">关联平台（1：安卓，2：ios，3:H5）</param>
        /// <param name="apptype">应用id或者风险配置id</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public static string SelectPass(int paytrpe, int paymode, int apptype, int CacheTime, int appid)
        {
            string paramentername = "";
            DataTable zftddt = new DataTable();
            string SelectPass = "SelectPass" + paytrpe + appid;

            if (JMP.TOOL.CacheHelper.IsCache(SelectPass))//判读是否存在缓存
            {
                zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(SelectPass);//获取缓存
                if (zftddt.Rows.Count > 0)
                {
                    int row = new Random().Next(0, zftddt.Rows.Count);
                    paramentername = zftddt.Rows[row]["p_extend"].ToString();
                }
                else
                {
                    zftddt = SelectPaymenttype(paytrpe, paymode, appid, apptype);
                    if (zftddt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, zftddt.Rows.Count);
                        paramentername = zftddt.Rows[row]["p_extend"].ToString();
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, SelectPass, CacheTime);//存入缓存
                    }
                }
            }
            else
            {
                zftddt = SelectPaymenttype(paytrpe, paymode, appid, apptype);
                if (zftddt.Rows.Count > 0)
                {
                    int row = new Random().Next(0, zftddt.Rows.Count);
                    paramentername = zftddt.Rows[row]["p_extend"].ToString();
                    JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, SelectPass, CacheTime);//存入缓存
                }
            }
            return paramentername;
        }
        /// <summary>
        /// 在数据库查询支付通道
        /// </summary>
        /// <param name="paytrpe">支付类型(1:支付宝，2：微信，3：银联，4：微信公众号，5：微信appid，6微信扫码，7：支付宝扫码)</param>
        /// <param name="paymode">关联平台（1：安卓，2：ios，3:H5）</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控等级配置</param>
        /// <returns></returns>
        private static DataTable SelectPaymenttype(int paytrpe, int paymode, int appid, int apptype)
        {
            //优先查询应用id如果应用id没有数据在用风控等级查询
            DataTable dt = new DataTable();
            dt = paybll.SelectInterface(paytrpe, paymode, appid);
            if (dt.Rows.Count == 0)
            {
                dt = paybll.SelectModesType(paytrpe, paymode, appid, 1);
                if (dt.Rows.Count == 0)
                {
                    dt = paybll.SelectModesType(paytrpe, paymode, apptype, 0);
                }
            }
            return dt;
        }

    }
}
