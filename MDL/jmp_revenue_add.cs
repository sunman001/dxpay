using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    //营收概况（新增用户）
    public class jmp_revenue_add
    {

        /// <summary>
        /// 编码
        /// </summary>
        [EntityTracker(Label = "编码", Description = "编码")]
        public int r_id { get; set; }

        /// <summary>
        /// 交易用户数
        /// </summary>
        [EntityTracker(Label = "交易用户数", Description = "交易用户数")]
        public int r_users { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [EntityTracker(Label = "交易金额", Description = "交易金额")]
        public decimal r_moneys { get; set; }

        /// <summary>
        /// 交易笔数
        /// </summary>
        [EntityTracker(Label = "交易笔数", Description = "交易笔数")]
        public int r_orders { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [EntityTracker(Label = "日期", Description = "日期")]
        public DateTime r_date { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int r_appid { get; set; }
    }
}
