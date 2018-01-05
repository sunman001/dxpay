using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //销售排行（商品）
    public class jmp_sales_goods
    {

        /// <summary>
        /// 编码
        /// </summary>
        [EntityTracker(Label = "编码", Description = "编码")]
        public int r_id { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [EntityTracker(Label = "商品编码", Description = "商品编码")]
        public int r_goodid { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [EntityTracker(Label = "交易金额", Description = "交易金额")]
        public decimal r_moneys { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        [EntityTracker(Label = "交易日期", Description = "交易日期")]
        public DateTime r_date { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int r_appid { get; set; }
    }
}