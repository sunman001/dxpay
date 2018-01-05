using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 支付通道配置表
    /// </summary>
    public class jmp_payment_type_config
    {
        /// <summary>
        /// ID自增列
        /// </summary>
        [EntityTracker(Label = "ID自增列", Description = "ID自增列")]
        public int Id { get; set; }

        /// <summary>
        /// 支付通道ID
        /// </summary>
        [EntityTracker(Label = "支付通道ID", Description = "支付通道ID")]
        public int PaymentTypeId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [EntityTracker(Label = "标签名称", Description = "标签名称")]
        public string Label { get; set; }

        /// <summary>
        /// 字段名字
        /// </summary>
        [EntityTracker(Label = "字段名字", Description = "字段名字")]
        public string FieldName { get; set; }

        /// <summary>
        /// 表单元素类型[text/textarea]
        /// </summary>
        [EntityTracker(Label = "表单元素类型", Description = "表单元素类型")]
        public string InputType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [EntityTracker(Label = "描述", Description = "描述")]
        public string Description { get; set; }

        /// <summary>
        /// 0:正常,1:冻结
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [EntityTracker(Label = "创建者", Description = "创建者")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 支付通道
        /// </summary>
        [EntityTracker(Label = "支付通道", Ignore = true)]
        public string paymenttypeName { get; set; }

        /// <summary>
        /// 支付类型名称
        /// </summary>
        [EntityTracker(Label = "支付类型名称", Ignore = true)]
        public string paymodeName { get; set; }

        /// <summary>
        /// 支付类型ID
        /// </summary>
        [EntityTracker(Label = "支付类型ID", Ignore = true)]
        public int paymodeId { get; set; }
    }
}
