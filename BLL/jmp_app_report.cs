using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    /// <summary>
    /// 应用报表
    /// </summary>
    public partial class jmp_app_report
    {
        private readonly DAL.jmp_app_report dal = new DAL.jmp_app_report();
        public jmp_app_report()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int r_id)
        {
            return dal.Exists(r_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MDL.jmp_app_report model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MDL.jmp_app_report model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int r_id)
        {

            return dal.Delete(r_id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            return dal.DeleteList(r_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MDL.jmp_app_report GetModel(int r_id)
        {

            return dal.GetModel(r_id);
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDL.jmp_app_report> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDL.jmp_app_report> DataTableToList(DataTable dt)
        {
            List<MDL.jmp_app_report> modelList = new List<MDL.jmp_app_report>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDL.jmp_app_report model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
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

        #endregion  BasicMethod
        #region  用户报表-陶涛-2016-03-23
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app_report> SelectList(string sqls, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, pageIndexs, PageSize, out pageCount);
        }
        #endregion
    }
}
