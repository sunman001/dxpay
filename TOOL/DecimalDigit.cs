using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOOL
{
    /// <summary>
    /// 转换小数位数公用方法
    /// </summary>
    public class DecimalDigit
    {
        /// <summary>
        /// 保留两位小数不四舍五入
        /// </summary>
        /// <param name="m1">需要转换的decimal数据类型</param>
        /// <returns>返回decimal的数据类型</returns>
        public static decimal DecimalPlaces(decimal m1)
        {
            decimal m3 = 0;
            if (m1 > 0)
            {
                m3 = Math.Truncate(m1 * 100) / 100;
            }
            return m3;
        }
        /// <summary>
        /// 保留两位小数不四舍五入
        /// </summary>
        /// <param name="m1">需要转换的decimal数据类型</param>
        /// <returns>返回一个string类型，按银行显示金额格式显示（如：10,000.00）</returns>
        public static string DecimalPlacesToString(decimal m1)
        {
            decimal m3 = 0;
            if (m1 > 0)
            {
                m3 = Math.Truncate(m1 * 100) / 100;
            }
            string m2 = String.Format("{0:N2}", m3);
            return m2;
        }
        /// <summary>
        /// 保留两位小数不四舍五入
        /// </summary>
        /// <param name="M1"></param>
        /// <returns></returns>
        public static string DecimalToString(string M1)
        {
            string M3 = "";
            if (string.IsNullOrEmpty(M1))
            {
                int b = M1.ToString().IndexOf('.') + 3;
                M3 = M1.ToString().Substring(0, b);
            }
            return M3;
        }
    }
}
