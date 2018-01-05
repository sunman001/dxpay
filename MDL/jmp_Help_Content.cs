using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
   public  class jmp_Help_Content
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 子类ID
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 父类ID
        /// </summary>
        public int PrentID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///内容
        /// </summary>
        public string  Content { get; set; }
        /// <summary>
        /// 查看次数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateById { get; set; }
        /// <summary>
        ///创建人姓名
        /// </summary>
        public string CreateByName { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateOn { get; set; }
        /// <summary>
        /// 是否前置
        /// </summary>
        public bool ISOverhead { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public int UpdateById { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdateByName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        /// 状态 0 正常 1锁定
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 子类名称
        /// </summary>
        [EntityTracker(Label = "子类名称", Description = "子类名称",Ignore =true)]
        public string ClassName { get; set; }
        [EntityTracker(Label = "父类名称", Description = "父类名称", Ignore = true)]
        public string PrentClassName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}
