using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///账单表
    ///</summary>
    public class jmp_bill
    {

        /// <summary>
        /// 账单id
        /// </summary>		
        [EntityTracker(Label = "账单id", Description = "账单id")]
        public int b_id { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>	
        [EntityTracker(Label = "订单日期", Description = "订单日期")]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>	
        [EntityTracker(Label = "用户ID", Description = "用户ID")]
        public int UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>	
        [EntityTracker(Label = "用户名", Description = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 账单创建时间
        /// </summary>	
        [EntityTracker(Label = "账单创建时间", Description = "账单创建时间")]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 总流水
        /// </summary>	
        [EntityTracker(Label = "总流水", Description = "总流水")]
        public decimal SdTotalAmount { get; set; }
        /// <summary>
        /// 流水支付宝
        /// </summary>	
        [EntityTracker(Label = "流水支付宝", Description = "流水支付宝")]
        public decimal SdAliPay { get; set; }
        /// <summary>
        /// 流水微信
        /// </summary>	
        [EntityTracker(Label = "流水微信", Description = "流水微信")]
        public decimal SdWxPay { get; set; }
        /// <summary>
        /// 流水银联
        /// </summary>	
        [EntityTracker(Label = "流水银联", Description = "流水银联")]
        public decimal SdUnionPay { get; set; }
        /// <summary>
        /// 流水微信公众号
        /// </summary>	
        [EntityTracker(Label = "流水微信公众号", Description = "流水微信公众号")]
        public decimal SdWxOfficalAccountPay { get; set; }
        /// <summary>
        /// 流水微信APP
        /// </summary>	
        [EntityTracker(Label = "流水微信APP", Description = "流水微信APP")]
        public decimal SdWxAppPay { get; set; }
        /// <summary>
        /// 流水微信扫码
        /// </summary>	
        [EntityTracker(Label = "流水微信扫码", Description = "流水微信扫码")]
        public decimal SdWxQrCodePay { get; set; }
        /// <summary>
        /// 流水支付宝扫码
        /// </summary>	
        [EntityTracker(Label = "流水支付宝扫码", Description = "流水支付宝扫码")]
        public decimal SdAliQrCodePay { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>	
        [EntityTracker(Label = "结算金额", Description = "结算金额")]
        public decimal BlTotalAmount { get; set; }
        /// <summary>
        /// 结算支付宝
        /// </summary>	
        [EntityTracker(Label = "结算支付宝", Description = "结算支付宝")]
        public decimal BlAliPay { get; set; }
        /// <summary>
        /// 结算微信
        /// </summary>
        [EntityTracker(Label = "结算微信", Description = "结算微信")]
        public decimal BlWxPay { get; set; }
        /// <summary>
        /// 结算银联
        /// </summary>
        [EntityTracker(Label = "结算银联", Description = "结算银联")]
        public decimal BlUnionPay { get; set; }
        /// <summary>
        /// 结算微信公众号
        /// </summary>		
        [EntityTracker(Label = "结算微信公众号", Description = "结算微信公众号")]
        public decimal BlWxOfficalAccountPay { get; set; }
        /// <summary>
        /// 结算微信APP
        /// </summary>	
        [EntityTracker(Label = "结算微信APP", Description = "结算微信APP")]
        public decimal BlWxAppPay { get; set; }
        /// <summary>
        /// 结算微信扫码
        /// </summary>	
        [EntityTracker(Label = "结算微信扫码", Description = "结算微信扫码")]
        public decimal BlWxQrCodePay { get; set; }
        /// <summary>
        /// 结算支付宝扫码
        /// </summary>	
        [EntityTracker(Label = "结算支付宝扫码", Description = "结算支付宝扫码")]
        public decimal BlAliQrCodePay { get; set; }

        #region 增加用户表字段
        /// <summary>
        /// 开户账号
        /// </summary>
        [EntityTracker(Label = "结算支付宝扫码", Description = "结算支付宝扫码", Ignore = true)]
        public string u_account { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [EntityTracker(Label = "结算支付宝扫码", Description = "结算支付宝扫码", Ignore = true)]
        public string u_realname { get; set; }
        #endregion

    }
}