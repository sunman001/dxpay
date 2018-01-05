using System;

namespace JMP.Model.Query.Doc
{
    public class DocMenuQueryModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 父类ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态0 正常 1 锁定
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 文章总数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateOn { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateByID { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateByName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 类型（0帮助中心 1开发中心）
        /// </summary>
        public int Type { get; set; }

        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }
    }
}
