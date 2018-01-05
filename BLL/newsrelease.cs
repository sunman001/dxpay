using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///新闻发布
    ///</summary>
    public partial class newsrelease
    {

        private readonly JMP.DAL.newsrelease dal = new JMP.DAL.newsrelease();
        public newsrelease()
        { }
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int n_id)
        {
            return dal.Exists(n_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.newsrelease model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.newsrelease model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int n_id)
        {

            return dal.Delete(n_id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string n_idlist)
        {
            return dal.DeleteList(n_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.newsrelease GetModel(int n_id)
        {

            return dal.GetModel(n_id);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 根据id查询新闻信息
        /// </summary>
        /// <param name="c_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.newsrelease SelectId(int c_id)
        {
            return dal.SelectId(c_id);
        }
        /// <summary>
        /// 根据新闻当前id查询上一条和下一条
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <param name="type">所属类型</param>
        /// <returns></returns>
        public List<JMP.MDL.newsrelease> SelectUpDw(int id, int type)
        {
            return dal.SelectUpDw(id, type);
        }
        public List<JMP.MDL.newsrelease> SelectList(string category, string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(category, sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount);
        }
        public List<JMP.MDL.newsrelease> GetListsByType(int type, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.GetListsByType(type, pageIndexs, PageSize, out pageCount);
        }
        public List<JMP.MDL.newsrelease> GetLists(int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.GetLists(pageIndexs, PageSize, out pageCount);
        }
        public bool UpdateCount(int id)
        {
            return dal.UpdateCount(id);
        }
        /// <summary>
        /// 查询最新10条行业新闻
        /// </summary>
        /// <returns></returns>
        public List<JMP.MDL.newsrelease> SelectListxw()
        {
            return dal.SelectListxw();
        }
    }
}