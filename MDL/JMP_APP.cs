using System;
using JMP.Model;
using DxPay.Dba.Attributes;

namespace JMP.MDL
{
    /// <summary>
    /// 应用表
    /// </summary>
    public class jmp_app
    {

        /// <summary>
        /// 应用ID
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        [DxColumn (PrimaryKey=true,AutoIncrement =true)]
        public int a_id { get; set; }
        /// <summary>
        /// 管理用户ID
        /// </summary>		
        [EntityTracker(Label = "管理用户ID", Description = "管理用户ID")]
        public int a_user_id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>		
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string a_name { get; set; }
        /// <summary>
        /// 关联平台ID
        /// </summary>		
        [EntityTracker(Label = "关联平台", Description = "关联平台")]
        public int a_platform_id { get; set; }
        /// <summary>
        /// 关联支付类型ID
        /// </summary>	
        [EntityTracker(Label = "关联支付类型ID", Description = "关联支付类型ID")]
        public string a_paymode_id { get; set; }
        /// <summary>
        /// 关联应用类型ID
        /// </summary>		
        [EntityTracker(Label = "关联应用类型ID", Description = "关联应用类型ID")]
        public int a_apptype_id { get; set; }
        /// <summary>
        /// 回掉地址
        /// </summary>	
        [EntityTracker(Label = "回掉地址", Description = "回掉地址")]
        public string a_notifyurl { get; set; }
        /// <summary>
        /// 应用密匙
        /// </summary>	
        [EntityTracker(Label = "应用密匙", Description = "应用密匙")]
        public string a_key { get; set; }
        /// <summary>
        /// 应用状态（0：冻结，1：正常，-1
        /// </summary>	
        [EntityTracker(Label = "应用状态", Description = "应用状态")]
        public int a_state { get; set; }
        /// <summary>
        /// 审核状态（0：未审核，1：审核通
        /// </summary>		
        [EntityTracker(Label = "审核状态", Description = "审核状态")]
        public int a_auditstate { get; set; }
        /// <summary>
        /// 应用秘钥
        /// </summary>	
        [EntityTracker(Label = "应用秘钥", Description = "应用秘钥")]
        public string a_secretkey { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [EntityTracker(Label = "审核人", Description = "审核人")]
        public string a_auditor { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>	
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime a_time { get; set; }
        /// <summary>
        /// 登录邮件地址
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间", Ignore = true), DxColumn(Ignore = true)]
        public string u_email { get; set; }
        /// <summary>
        /// 同步地址
        /// </summary>
        [EntityTracker(Label = "同步地址", Description = "同步地址")]
        public string a_showurl { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [EntityTracker(Label = "真实姓名", Description = "真实姓名", Ignore = true), DxColumn(Ignore = true)]
        public string u_realname { get; set; }
       
        /// <summary>
        /// 所属应用类型
        /// </summary>
        [EntityTracker(Label = "所属应用类型", Description = "所属应用类型", Ignore = true), DxColumn(Ignore = true)]
        public string t_name { get; set; }
        /// <summary>
        /// 所属平台
        /// </summary>
        [EntityTracker(Label = "所属平台", Description = "所属平台", Ignore = true), DxColumn(Ignore = true)]
        public string p_name { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [EntityTracker(Label = "用户id", Description = "用户id", Ignore = true), DxColumn(Ignore = true)]
        public int u_id { get; set; }
        /// <summary>
        /// 风险等级配置表id(添加时默认为0)
        /// </summary>
        [EntityTracker(Label = "风险等级配置表id", Description = "风险等级配置表id", Ignore = true)]
        public int a_rid { get; set; }
        /// <summary>
        /// 应用审核地址
        /// </summary>
        [EntityTracker(Label = "应用审核地址", Description = "应用审核地址", Ignore = true)]
        public string a_appurl { get; set; }

        /// <summary>
        /// 应用简介
        /// </summary>
        [EntityTracker(Label = "应用简介", Description = "应用简介", Ignore = true)]
        public string a_appsynopsis { get; set; }

        /// <summary>
        /// 风控高中低表ID
        /// </summary>
        [EntityTracker(Label = "风控高中低表ID", Description = "风控高中低表ID", Ignore = true), DxColumn(Ignore = true)]
        public int r_id { get; set; }

        /// <summary>
        /// 风控高中低
        /// </summary>
        [EntityTracker(Label = "风控高中低", Description = "风控高中低", Ignore = true), DxColumn(Ignore = true)]
        public string r_name { get; set; }

    }
}