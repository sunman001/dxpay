using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 商务信息表
    /// </summary>
    public class CoBusinessPersonnel
    {
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "Id", Description = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        [EntityTracker(Label = "登录名称", Description = "登录名称")]
        public string LoginName { get; set; }

        /// <summary>
        /// 加密的密码字符串
        /// </summary>
        [EntityTracker(Label = "加密的密码字符串", Description = "加密的密码字符串")]
        public string Password { get; set; }

        /// <summary>
        /// 显示姓名
        /// </summary>
        [EntityTracker(Label = "显示姓名", Description = "显示姓名")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [EntityTracker(Label = "邮箱地址", Description = "邮箱地址")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [EntityTracker(Label = "手机号码", Description = "手机号码")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        [EntityTracker(Label = "QQ号码", Description = "QQ号码")]
        public string QQ { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        [EntityTracker(Label = "网址", Description = "网址")]
        public string Website { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedById { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        [EntityTracker(Label = "创建者姓名", Description = "创建者姓名")]
        public string CreatedByName { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [EntityTracker(Label = "登录次数", Description = "登录次数")]
        public int LoginCount { get; set; }


        /// <summary>
        /// 最后登录时间
        /// </summary>
        [EntityTracker(Label = "最后登录时间", Description = "最后登录时间")]
        public DateTime? LogintTime { get; set; }


        /// <summary>
        /// 状态[0:正常,1:冻结]
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int State { get; set; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        [EntityTracker(Label = "用户角色ID", Description = "用户角色ID")]
        public int RoleId { get; set; }

    }
}
