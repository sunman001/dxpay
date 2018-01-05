using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //订单表
    public class jmp_order
    {

        /// <summary>
        /// 订单ID
        /// </summary>		
        private int _o_id;
        [EntityTracker(Label = "订单ID", Description = "订单ID")]
        public int o_id
        {
            get { return _o_id; }
            set { _o_id = value; }
        }

        /// <summary>
        /// 订单编码唯一
        /// </summary>		
        private string _o_code;
        [EntityTracker(Label = "订单编码唯一", Description = "订单编码唯一")]
        public string o_code
        {
            get { return _o_code; }
            set { _o_code = value; }
        }

        /// <summary>
        /// 商户订单号
        /// </summary>		
        private string _o_bizcode;
        [EntityTracker(Label = "商户订单号", Description = "商户订单号")]
        public string o_bizcode
        {
            get { return _o_bizcode; }
            set { _o_bizcode = value; }
        }

        /// <summary>
        /// 流水号
        /// </summary>		
        private string _o_tradeno;
        [EntityTracker(Label = "流水号", Description = "流水号")]
        public string o_tradeno
        {
            get { return _o_tradeno; }
            set { _o_tradeno = value; }
        }

        /// <summary>
        /// 支付类型
        /// </summary>		
        private string _o_paymode_id;
        [EntityTracker(Label = "支付类型", Description = "支付类型")]
        public string o_paymode_id
        {
            get { return _o_paymode_id; }
            set { _o_paymode_id = value; }
        }

        /// <summary>
        /// 应用ID
        /// </summary>		
        private int _o_app_id;
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int o_app_id
        {
            get { return _o_app_id; }
            set { _o_app_id = value; }
        }

        ///// <summary>
        ///// 商品id
        ///// </summary>		
        //private int _o_goods_id;
        //[EntityTracker(Label = "商品id", Description = "商品id")]
        //public int o_goods_id
        //{
        //    get { return _o_goods_id; }
        //    set { _o_goods_id = value; }
        //}


        /// <summary>
        /// 商品名称
        /// </summary>		
        [EntityTracker(Label = "商品名称", Description = "商品名称")]
        public string o_goodsname { get; set; }

        /// <summary>
        /// 关联终端唯一KEY
        /// </summary>		
        private string _o_term_key;
        [EntityTracker(Label = "关联终端唯一KEY", Description = "关联终端唯一KEY")]
        public string o_term_key
        {
            get { return _o_term_key; }
            set { _o_term_key = value; }
        }

        /// <summary>
        /// 订单金额
        /// </summary>		
        private decimal _o_price;
        [EntityTracker(Label = "订单金额", Description = "订单金额")]
        public decimal o_price
        {
            get { return _o_price; }
            set { _o_price = value; }
        }

        /// <summary>
        /// 付款账户
        /// </summary>		
        private string _o_payuser;
        [EntityTracker(Label = "付款账户", Description = "付款账户")]
        public string o_payuser
        {
            get { return _o_payuser; }
            set { _o_payuser = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _o_ctime;
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime o_ctime
        {
            get { return _o_ctime; }
            set { _o_ctime = value; }
        }

        /// <summary>
        /// 支付时间
        /// </summary>		
        private DateTime _o_ptime;
        [EntityTracker(Label = "支付时间", Description = "支付时间")]
        public DateTime o_ptime
        {
            get { return _o_ptime; }
            set { _o_ptime = value; }
        }

        /// <summary>
        /// 订单状态：0 创建 1支付成功 -1 支付失败
        /// </summary>		
        private int _o_state;
        [EntityTracker(Label = "订单状态", Description = "订单状态")]
        public int o_state
        {
            get { return _o_state; }
            set { _o_state = value; }
        }

        /// <summary>
        /// 支付配置id
        /// </summary>
        [EntityTracker(Label = "支付配置id", Description = "支付配置id")]
        public int o_interface_id { get; set; }

        /// <summary>
        /// 同步地址
        /// </summary>
        [EntityTracker(Label = "同步地址", Description = "同步地址")]
        public string o_showaddress { get; set; }

        /// <summary>
        /// 商户私有信息
        /// </summary>
        [EntityTracker(Label = "商户私有信息", Description = "商户私有信息")]
        public string o_privateinfo { get; set; }

        /// <summary>
        /// 通知次数
        /// </summary>
        [EntityTracker(Label = "通知次数", Description = "通知次数")]
        public int o_times { get; set; }

        /// <summary>
        /// 通知地址
        /// </summary>
        [EntityTracker(Label = "通知地址", Description = "通知地址")]
        public string o_address { get; set; }

        /// <summary>
        /// 通知状态(0:创建，1:通知成功，-1通知失败)
        /// </summary>
        [EntityTracker(Label = "通知状态", Description = "通知状态")]
        public int o_noticestate { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        [EntityTracker(Label = "通知时间", Description = "通知时间")]
        public DateTime o_noticetimes { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        [EntityTracker(Label = "应用key", Ignore = true)]
        public string a_key { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Ignore = true)]
        public string a_name { get; set; }

        /// <summary>
        /// 支付类型名称
        /// </summary>
        [EntityTracker(Label = "支付类型名称", Ignore = true)]
        public string p_name { get; set; }

        ///// <summary>
        ///// 商品名称
        ///// </summary>
        //[EntityTracker(Label = "商品名称", Ignore = true)]
        //public string g_name { get; set; }

        /// <summary>
        /// 移动端mark地址
        /// </summary>		
        [EntityTracker(Label = "移动端mark地址", Ignore = true)]
        public string t_mark { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>	
        [EntityTracker(Label = "ip地址", Ignore = true)]
        public string t_ip { get; set; }

        /// <summary>
        /// 省份地址
        /// </summary>		
        [EntityTracker(Label = "省份地址", Ignore = true)]
        public string t_province { get; set; }

        /// <summary>
        /// 手机运营商
        /// </summary>		
        [EntityTracker(Label = "手机运营商", Ignore = true)]
        public string t_nettype { get; set; }

        /// <summary>
        /// 手机品牌
        /// </summary>	
        [EntityTracker(Label = "手机品牌", Ignore = true)]
        public string t_brand { get; set; }

        /// <summary>
        /// 手机系统
        /// </summary>		
        [EntityTracker(Label = "手机系统", Ignore = true)]
        public string t_system { get; set; }

        /// <summary>
        /// 硬件版本
        /// </summary>		
        [EntityTracker(Label = "硬件版本", Ignore = true)]
        public string t_hardware { get; set; }

        /// <summary>
        /// 屏幕分辨率
        /// </summary>	
        [EntityTracker(Label = "屏幕分辨率", Ignore = true)]
        public string t_screen { get; set; }

        /// <summary>
        /// 手机网络
        /// </summary>	
        [EntityTracker(Label = "手机网络", Ignore = true)]
        public string t_network { get; set; }

        /// <summary>
        /// 开发者名称
        /// </summary>
        [EntityTracker(Label = "开发者名称", Ignore = true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 关联平台
        /// </summary>
        [EntityTracker(Label = "关联平台", Ignore = true)]
        public int a_platform_id { get; set; }

        /// <summary>
        /// 支付壳子名称
        /// </summary>
        [EntityTracker(Label = "支付壳子名称", Ignore = true)]
        public string l_corporatename { get; set; }

        /// <summary>
        /// 关联关系（0:未指定,1:商务,2:代理商）
        /// </summary>
        [EntityTracker(Label = "关联关系", Ignore = true)]
        public int relation_type { get; set; }
        /// <summary>
        /// 代理商名称
        /// </summary>
        [EntityTracker(Label = "代理商名称", Ignore = true)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 商务名称
        /// </summary>
        [EntityTracker(Label = "代理商名称", Ignore = true)]
        public string bpname { get; set; }


        /// <summary>
        /// 开发者ID
        /// </summary>
        [EntityTracker(Label = "开发者ID", Ignore = true)]
        public int u_id { get; set; }
    }
}