using System;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 新闻表
    /// </summary>
    public class newsrelease
    {
        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int n_id { get; set; }

        /// <summary>
        /// 新闻类别0、新闻列表1、行业状态
        /// </summary>
        [EntityTracker(Label = "新闻类别", Description = "新闻类别")]
        public int n_category { get; set; }

        /// <summary>
        /// 新闻标题
        /// </summary>
        [EntityTracker(Label = "新闻标题", Description = "新闻标题")]
        public string n_title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>		
        [EntityTracker(Label = "图片", Description = "图片")]
        public string n_picture { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>	
        [EntityTracker(Label = "新闻内容", Description = "新闻内容")]
        public string n_info { get; set; }

        /// <summary>
        ///发布人
        /// </summary>	
        [EntityTracker(Label = "发布人", Description = "发布人")]
        public string n_user { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>		
        [EntityTracker(Label = "发布时间", Description = "发布时间")]
        public DateTime n_time { get; set; }

        [EntityTracker(Label = "浏览次数", Description = "浏览次数")]
        public int n_count { get; set; }

        /// <summary>
        /// seo  keywords 关键字
        /// </summary>
        [EntityTracker(Label = "seo  keywords 关键字", Description = "seo  keywords 关键字")]
        public string keywords { get; set; }
        /// <summary>
        /// seo  description 关键字
        /// </summary>
        [EntityTracker(Label = "seo  description 关键字", Description = "seo  description 关键字")]
        public string description { get; set; }
    }
}
