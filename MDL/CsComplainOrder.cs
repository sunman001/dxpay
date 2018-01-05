using System;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 投诉订单表
    /// </summary>
    public class CsComplainOrder
    {
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [EntityTracker(Label = "订单编号", Description = "订单编号")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 归档订单表名
        /// </summary>
        [EntityTracker(Label = "归档订单表名", Description = "归档订单表名")]
        public string OrderTable { get; set; }

        /// <summary>
        /// 投诉类型ID
        /// </summary>
        [EntityTracker(Label = "投诉类型ID", Description = "投诉类型ID")]
        public int ComplainTypeId { get; set; }

        /// <summary>
        /// 投诉类型名称
        /// </summary>
        [EntityTracker(Label = "投诉类型名称", Description = "投诉类型名称")]
        public string ComplainTypeName { get; set; }

        /// <summary>
        /// 投诉时间
        /// </summary>
        [EntityTracker(Label = "投诉时间", Description = "投诉时间")]
        public DateTime ComplainDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 投诉证据[可能为图片路径,多个图片路径以逗号隔开]
        /// </summary>
        [EntityTracker(Label = "投诉证据", Description = "投诉证据")]
        public string Envidence { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        [EntityTracker(Label = "处理人ID", Description = "处理人ID")]
        public int HandlerId { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        [EntityTracker(Label = "处理人姓名", Description = "处理人姓名")]
        public string HandlerName { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [EntityTracker(Label = "处理时间", Description = "处理时间")]
        public DateTime? HandleDate { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [EntityTracker(Label = "处理结果", Description = "处理结果")]
        public string HandleResult { get; set; }

        /// <summary>
        /// 状态（0：正常，1：冻结）
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int state { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [EntityTracker(Label = "创建人ID", Description = "创建人ID")]
        public int FounderId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [EntityTracker(Label = "创建人姓名", Description = "创建人姓名")]
        public string FounderName { get; set; }


        /// <summary>
        /// 开发者ID
        /// </summary>
        [EntityTracker(Label = "开发者ID", Description = "开发者ID")]
        public int UserId { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int AppId { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        [EntityTracker(Label = "订单金额", Description = "订单金额")]
        public decimal Price { get; set; }

        /// <summary>
        /// 已退款
        /// </summary>
        [EntityTracker(Label = "已退款", Description = "是否已退款[0:否,1:是],默认:0")]
        public bool IsRefund { get; set; }

        [EntityTracker(Label = "下游接手时间", Description = "下游接手时间")]
        public DateTime? DownstreamStartTime { get; set; }

        [EntityTracker(Label = "下游处理完成时间",Description = "下游处理完成时间")]
        public DateTime? DownstreamEndTime { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称",Ignore =true)]
        public string a_name { get; set; }

        /// <summary>
        /// 开发者名称
        /// </summary>
        [EntityTracker(Label = "开发者名称", Description = "开发者名称", Ignore = true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 投诉类型名称
        /// </summary>
        [EntityTracker(Label = "投诉类型名称", Description = "投诉类型名称", Ignore = true)]
        public string Name { get; set; }

        /// <summary>
        /// 支付渠道ID
        /// </summary>
        [EntityTracker(Label = "支付渠道ID", Description = "支付渠道ID")]
        public int ChannelId { get; set; }


    }
}