using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL.UpdateOrder
{
    /// <summary>
    /// 修改订单状态公共方法
    /// </summary>
    public class OrderState
    {
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <param name="tname">表名</param>
        /// <returns></returns>
        public static bool UpdateOrderState(string code, string tname)
        {
            JMP.BLL.jmp_order order = new JMP.BLL.jmp_order();
            bool re = false;
            if (order.ChangeStateToAbnormal(tname, code))
            {
                re = true;
            }
            return re;
        }
    }
}
