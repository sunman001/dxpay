using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 判断单笔支付金额
    /// </summary>
    public class JudgeMoney
    {
        /// <summary>
        /// 验证每笔订单最小支付金额
        /// </summary>
        /// <param name="price">支付金额（元）</param>
        /// <param name="minmun">最小支付金额（元）</param>
        /// <returns></returns>
        public static bool JudgeMinimum(decimal price, decimal minmun)
        {
            bool Success = true;
            if (minmun > 0)
            {
                if (price < minmun)
                {
                    Success = false;
                }
            }
            return Success;
        }
        /// <summary>
        /// 验证每笔订单最大支付金额
        /// </summary>
        /// <param name="price">支付金额（元）</param>
        /// <param name="maximum">最大支付金额（元）</param>
        /// <returns></returns>
        public static bool JudgeMaximum(decimal price, decimal maximum)
        {
            bool Success = true;
            if (maximum > 0)
            {
                if (price > maximum)
                {
                    Success = false;
                }
            }
            return Success;
        }
    }
}
