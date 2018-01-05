using JMP.Model;
using System;

namespace WEBDEV.Models
{
    /// <summary>
    /// 提款银行卡表(用于批量导入银行卡)
    /// </summary>
    public class ImportUserCardModel
    {
        /// <summary>
		/// 主键
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int u_id { get; set; }

        /// <summary>
        /// 用户id（开发者id）
        /// </summary>
        [EntityTracker(Label = "用户id（开发者id）", Description = "用户id（开发者id）")]
        public int u_userid { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [EntityTracker(Label = "银行卡号", Description = "银行卡号")]
        public string u_banknumber { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        [EntityTracker(Label = "银行名称", Description = "银行名称")]
        public string u_bankname { get; set; }

        /// <summary>
        /// 开户行名称
        /// </summary>
        [EntityTracker(Label = "开户行名称", Description = "开户行名称")]
        public string u_openbankname { get; set; }

        /// <summary>
        /// 持卡人姓名
        /// </summary>
        [EntityTracker(Label = "持卡人姓名", Description = "持卡人姓名")]
        public string u_name { get; set; }

        /// <summary>
        /// 审核状态（0：等待审核，1：审核通过，-1审核失败）
        /// </summary>
        [EntityTracker(Label = "审核状态", Description = "审核状态")]
        public int u_state { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityTracker(Label = "备注", Description = "备注")]
        public string u_remarks { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [EntityTracker(Label = "审核人", Description = "审核人")]
        public string u_auditor { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [EntityTracker(Label = "审核时间", Description = "审核时间")]
        public DateTime? u_date { get; set; }

        /// <summary>
        /// 冻结状态（0：正常，1：冻结）
        /// </summary>
        [EntityTracker(Label = "冻结状态", Description = "冻结状态")]
        public int u_freeze { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [EntityTracker(Label = "省份", Description = "省份")]
        public string u_province { get; set; }

        /// <summary>
        /// 地区（城市）
        /// </summary>
        [EntityTracker(Label = "地区（城市）", Description = "地区（城市）")]
        public string u_area { get; set; }

        /// <summary>
        /// 对公对私标记00:对私 01对公
        /// </summary>
        [EntityTracker(Label ="付款标识",Description ="付款标识")]
        public string u_flag { get; set; }

    }
}
