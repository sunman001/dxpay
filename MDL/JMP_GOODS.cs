using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //商品表
    public class jmp_goods
    {

        /// <summary>
        /// 商品ID
        /// </summary>		
        private int _g_id;
        [EntityTracker(Label = "商品ID", Description = "商品ID")]
        public int g_id
        {
            get { return _g_id; }
            set { _g_id = value; }
        }
        /// <summary>
        /// 应用ID
        /// </summary>		
        private int _g_app_id;
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int g_app_id
        {
            get { return _g_app_id; }
            set { _g_app_id = value; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>		
        private string _g_name;
        [EntityTracker(Label = "商品名称", Description = "商品名称")]
        public string g_name
        {
            get { return _g_name; }
            set { _g_name = value; }
        }
        /// <summary>
        /// 销售类型 
        /// </summary>		
        private int _g_saletype_id;
        [EntityTracker(Label = "销售类型", Description = "销售类型")]
        public int g_saletype_id
        {
            get { return _g_saletype_id; }
            set { _g_saletype_id = value; }
        }
        /// <summary>
        /// 平台定义价格：不为空，自定义价格：可为空
        /// </summary>		
        private decimal _g_price;
        [EntityTracker(Label = "平台定义价格", Description = "平台定义价格")]
        public decimal g_price
        {
            get { return _g_price; }
            set { _g_price = value; }
        }
        /// <summary>
        /// 状态：0 禁用 1 正常
        /// </summary>		
        private int _g_state;
        [EntityTracker(Label = "状态", Description = "状态")]
        public int g_state
        {
            get { return _g_state; }
            set { _g_state = value; }
        }
        /// <summary>
        /// 销售方式
        /// </summary>
        [EntityTracker(Label = "销售方式", Description = "销售方式")]
        public string s_name { get; set; }
        /// <summary>
        ///所属应用 
        /// </summary>
        [EntityTracker(Label = "所属应用", Description = "所属应用")]
        public string a_name { get; set; }
    }
}