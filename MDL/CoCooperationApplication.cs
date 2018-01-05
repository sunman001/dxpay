using System;
using DxPay.Dba.Attributes;

namespace JMP.MDL
{
    /// <summary>
    /// 合作申请表
    /// </summary>
    public class CoCooperationApplication
    {
        /// <summary>
        /// Id
        /// </summary>
        [DxColumn(AutoIncrement = true, PrimaryKey = true)]
        public int Id { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 网址[可选]
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// 合作描述[可选]
        /// </summary>
        public string RequestContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 被查看次数
        /// </summary>
        public int ReadCount { get; set; }
        /// <summary>
        /// 最近一次被查看时间
        /// </summary>
        public DateTime?LatestReadTime { get; set; }
        /// <summary>
        /// 抢单者时间
        /// </summary>
        public DateTime?GrabbedDate { get; set; }
        /// <summary>
        /// 抢单者ID
        /// </summary>
        public int GrabbedById { get; set; }
        /// <summary>
        /// 抢单者姓名
        /// </summary>
        public string GrabbedByName { get; set; }
        /// <summary>
        /// 合作表单状态[-1:已关闭,0:新建,1:已抢,2:已分配]
        /// </summary>
        public int State { get; set; }
    }
}
