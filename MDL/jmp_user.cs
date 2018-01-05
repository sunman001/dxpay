using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;
using DxPay.Dba.Attributes;

namespace JMP.MDL
{
    //jmp_user
    public class jmp_user
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [EntityTracker(Label = "用户ID", Description = "用户ID")]
        [DxColumn(PrimaryKey = true, AutoIncrement = true)]
        public int u_id { get; set; }

        /// <summary>
        /// 登录邮件地址
        /// </summary>
        [EntityTracker(Label = "登录邮件地址", Description = "登录邮件地址")]
        public string u_email { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [EntityTracker(Label = "登录密码", Description = "登录密码")]
        public string u_password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [EntityTracker(Label = "真实姓名", Description = "真实姓名")]
        public string u_realname { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [EntityTracker(Label = "联系电话", Description = "联系电话")]
        public string u_phone { get; set; }

        /// <summary>
        /// 联系qq
        /// </summary>
        [EntityTracker(Label = "联系qq", Description = "联系qq")]
        public string u_qq { get; set; }

        /// <summary>
        /// 开户银行全称
        /// </summary>
        [EntityTracker(Label = "开户银行全称", Description = "开户银行全称")]
        public string u_bankname { get; set; }

        /// <summary>
        /// 开户名
        /// </summary>
        [EntityTracker(Label = "开户名", Description = "开户名")]
        public string u_name { get; set; }

        /// <summary>
        /// 开户账号
        /// </summary>
        [EntityTracker(Label = "开户账号", Description = "开户账号")]
        public string u_account { get; set; }

        /// <summary>
        /// 类别：0 个人 1 企业
        /// </summary>
        [EntityTracker(Label = "类别", Description = "类别")]
        public int u_category { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [EntityTracker(Label = "身份证号码", Description = "身份证号码")]
        public string u_idnumber { get; set; }

        /// <summary>
        /// 持证照片地址
        /// </summary>
        [EntityTracker(Label = "持证照片正面地址", Description = "持证照片正面地址")]
        public string u_photo { get; set; }

        /// <summary>
        /// 身份证反面照片
        /// </summary>
        [EntityTracker(Label = "持证照片反面地址", Description = "持证照片反面地址")]
        public string u_photof { get; set; }

        /// <summary>
        /// 开户许可证
        /// </summary>
        [EntityTracker(Label = "开户许可证照片地址", Description = "开户许可证照片地址")]
        public string u_licence { get; set; }
        /// <summary>
        /// 营业执照照片
        /// </summary>
        [EntityTracker(Label = "营业执照照片", Description = "营业执照照片")]
        public string u_blicense { get; set; }

        /// <summary>
        /// 营业执照编号
        /// </summary>
        [EntityTracker(Label = "营业执照编号", Description = "营业执照编号")]
        public string u_blicensenumber { get; set; }

        /// <summary>
        /// 用户登录次数
        /// </summary>
        [EntityTracker(Label = "用户登录次数", Description = "用户登录次数")]
        public int u_count { get; set; }

        /// <summary>
        /// 账户状态：0 冻结 1 正常
        /// </summary>
        [EntityTracker(Label = "账户状态", Description = "账户状态")]
        public int u_state { get; set; }

        /// <summary>
        /// 手动提款标示（0：自动，1：手动）
        /// </summary>
        [EntityTracker(Label = "手动提款标示", Description = "手动提款标示")]
        public int u_drawing { get; set; }

        /// <summary>
        /// 商务id
        /// </summary>
        [EntityTracker(Label = "商务id", Description = "商务id")]
        public int u_merchant_id { get; set; }

        /// <summary>
        /// 审核状态：-1 未通过 0 等待审核
        /// </summary>
        [EntityTracker(Label = "审核状态", Description = "审核状态")]
        public int u_auditstate { get; set; }

        /// <summary>
        /// 父级用户ID(0:父级)
        /// </summary>
        [EntityTracker(Label = "父级用户ID", Description = "父级用户ID")]
        public int u_topid { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [EntityTracker(Label = "联系地址", Description = "联系地址")]
        public string u_address { get; set; }

        ///// <summary>
        ///// 手续费(默认为0)
        ///// </summary>
        //[EntityTracker(Label = "手续费", Description = "手续费")]
        //public decimal u_poundage { get; set; }

        /// <summary>
        /// u_role_id
        /// </summary>
        [EntityTracker(Label = "u_role_id", Description = "u_role_id")]
        public int u_role_id { get; set; }

        /// <summary>
        /// 关联关系[0:未指定,1:商务,2:代理商]
        /// </summary>
        [EntityTracker(Label = "关联关系", Description = "关联关系")]
        public int relation_type { get; set; }

        /// <summary>
        /// 关联的关系人ID
        /// </summary>
        [EntityTracker(Label = "关联的关系人ID", Description = "关联的关系人ID")]
        public int relation_person_id { get; set; }

        /// <summary>
        /// 开发者服务费关联表ID(商务或者代理商设置此项,根据relation_type字段)
        /// </summary>
        [EntityTracker(Label = "开发者服务费关联表ID", Description = "开发者服务费关联表ID")]
        public int ServiceFeeRatioGradeId { get; set; }

        /// <summary>
        /// 是否特批(如果是特批,读取系统表[jmp_system]中的CO_SPECIAL_SERVICE_FEE_RATIO选项的值)
        /// </summary>
        [EntityTracker(Label = "是否特批", Description = "是否特批")]
        public bool IsSpecialApproval { get; set; }

        /// <summary>
        /// 开发者名称
        /// </summary>
        [EntityTracker(Label = "开发者名称", Description = "开发者名称", Ignore = true)]
        [DxColumn(Ignore = true)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 费率等级名称
        /// </summary>
        [EntityTracker(Label = "费率等级名称", Description = "费率等级名称", Ignore = true)]
        [DxColumn(Ignore = true)]
        public string Name { get; set; }

        /// <summary>
        /// 企业法人
        /// </summary>
        [EntityTracker(Label = "企业法人", Description = "企业法人")]
        public string BusinessEntity { get; set; }

        /// <summary>
        /// 企业地址
        /// </summary>
        [EntityTracker(Label = "企业地址", Description = "企业地址")]
        public string RegisteredAddress { get; set; }

        [EntityTracker(Label = "开发者的服务费比例", Description = "开发者的服务费比例", Ignore = true), DxColumn(Ignore = true)]
        public decimal ServiceFeeRatio { get; set; }

        [EntityTracker(Label = "直客提成比例", Description = "直客提成比例", Ignore = true), DxColumn(Ignore = true)]
        public decimal CustomerWithoutAgentRatio { get; set; }

        /// <summary>
        /// 特批时需要减掉的服务费率(小数)
        /// </summary>
        [EntityTracker(Label = "特批时需要减掉的服务费率", Description = "特批时需要减掉的服务费率")]
        public decimal SpecialApproval { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [EntityTracker(Label = "注册时间", Description = "注册时间")]
        public DateTime? u_time { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [EntityTracker(Label = "审核人", Description = "审核人")]
        public string u_auditor { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        [EntityTracker(Label = "支付密码", Description = "支付密码")]
        public string u_paypwd { get; set; }

        /// <summary>
        /// 冻结金额
        /// </summary>
        [EntityTracker(Label = "冻结金额", Description = "冻结金额")]
        public decimal FrozenMoney { get; set; }

        [EntityTracker(Label = "是否签订合同", Description = "是否签订合同")]
        public bool IsSignContract { get; set; }

        [EntityTracker(Label = "是否产品备案", Description = "是否产品备案")]
        public bool IsRecord { get; set; }

    }
}