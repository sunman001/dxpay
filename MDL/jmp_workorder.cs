using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 工单表 
    /// </summary>
   public class jmp_workorder
    {
        /// <summary>
        /// id
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 分类目录(0:紧急问题,1:功能需求)
        /// </summary>
        [EntityTracker(Label = "分类目录", Description = "分类目录")]
        public int catalog { get; set; }

        /// <summary>
        /// 工单标题
        /// </summary>
        [EntityTracker(Label = "工单标题", Description = "工单标题")]
        public string title { get; set; }

        /// <summary>
        /// 工单内容
        /// </summary>
        [EntityTracker(Label = "工单内容", Description = "工单内容")]
        public string content { get; set; }

        /// <summary>
        /// 工单状态(0:正常,-1:关闭，-2申诉)
        /// </summary>
        [EntityTracker(Label = "工单状态", Description = "工单状态")]
        public int status { get; set; }

        /// <summary>
        /// 进度(0:未响应,1:处理中,2:已处理,3:已评分，4：已完成)
        /// </summary>
        [EntityTracker(Label = "进度", Description = "进度")]
        public int progress { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime createdon { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int createdbyid { get; set; }

        /// <summary>
        /// 当天的值班者ID,多个以逗号分隔
        /// </summary>
        [EntityTracker(Label = "当天的值班者ID", Description = "当天的值班者ID")]
        public string watchidsoftheday { get; set; }

        /// <summary>
        /// 查看次数(0:未查看,>0已查看,每打开一次累加1)
        /// </summary>
        [EntityTracker(Label = "查看次数", Description = "查看次数")]
        public int viewcount { get; set; }

        /// <summary>
        /// 最后一次查看时间
        /// </summary>
        [EntityTracker(Label = "最后一次查看时间", Description = "最后一次查看时间")]
        public DateTime? latestviewdate { get; set; }

        /// <summary>
        /// 评分(1-5分)
        /// </summary>
        [EntityTracker(Label = "评分", Description = "评分")]
        public int score { get; set; }

        /// <summary>
        /// 评分内容
        /// </summary>
        [EntityTracker(Label = "评分内容", Description = "评分内容")]
        public string scorereason { get; set; }

        /// <summary>
        /// 是否已推送提醒
        /// </summary>
        [EntityTracker(Label = "是否已推送提醒", Description = "是否已推送提醒")]
        public bool pushedremind { get; set; }

        /// <summary>
        /// 推送提醒时间
        /// </summary>
        [EntityTracker(Label = "推送提醒时间", Description = "推送提醒时间")]
        public DateTime? pushreminddate { get; set; }

        /// <summary>
        /// 工单被关闭的原因(关闭时必填)
        /// </summary>
        [EntityTracker(Label = "工单被关闭的原因", Description = "工单被关闭的原因")]
        public string closereason { get; set; }

        /// <summary>
        /// 发起者申诉原因
        /// </summary>
        [EntityTracker(Label = "发起者申诉原因", Description = "发起者申诉原因")]
        public string initiatorreason { get; set; }

        /// <summary>
        /// 处理者申请原因
        /// </summary>
        [EntityTracker(Label = "处理者申请原因", Description = "处理者申请原因")]
        public string handlerreason { get; set; }

        [EntityTracker(Label = "响应时间", Description = "响应时间")]
        public DateTime ? resultDate { get; set; }

        /// <summary>
        /// 响应结果
        /// </summary>
        [EntityTracker(Label = "响应结果", Description = "响应结果")]
        public string result { get; set; }

        //值班人姓名
        [EntityTracker(Label = "值班人姓名", Description = "值班人姓名",Ignore =true)]
        public string name { get; set; }

        //发起人
        [EntityTracker(Label = "发起人", Description = "发起人", Ignore = true)]
        public string createdbyname { get; set; }
    }
}
