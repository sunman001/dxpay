using System;
using JMP.Model;

namespace JMP.MDL
{
    public class CoServiceFeeRatioGrade
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 服务费等级名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 开发者的服务费比例
        /// </summary>
        public decimal ServiceFeeRatio { get; set; }
        /// <summary>
        /// 直客提成比例
        /// </summary>
        public decimal CustomerWithoutAgentRatio { get; set; }
        /// <summary>
        /// 商务对代理商的提成比例
        /// </summary>
        public decimal BusinessPersonnelAgentRatio { get; set; }
        /// <summary>
        /// 代理商提成比例[小数]
        /// </summary>
        public decimal AgentPushMoneyRatio { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int CreatedById { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatedByName { get; set; }

    }
}
