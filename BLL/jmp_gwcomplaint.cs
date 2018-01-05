using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;

namespace JMP.BLL
{
  public partial  class jmp_gwcomplaint
    {
        private readonly JMP.DAL.jmp_gwcomplaint dal = new JMP.DAL.jmp_gwcomplaint();
        public jmp_gwcomplaint()
        {
        }
        /// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_gwcomplaint model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_gwcomplaint model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_gwcomplaint GetModel(int id)
        {

            return dal.GetModel(id);
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
        public List<JMP.MDL.jmp_gwcomplaint> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_gwcomplaint> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_gwcomplaint> modelList = new List<JMP.MDL.jmp_gwcomplaint>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_gwcomplaint model;
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
        /// <summary>
        /// 根据应用id查询应用投诉信息
        /// </summary>
        /// <param name="c_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_gwcomplaint SelectId(int c_id)
        {
            return dal.SelectId(c_id);
        }
        public List<JMP.MDL.jmp_gwcomplaint> SelectList(string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(auditstate, sea_name, type, searchDesc, stime, endtime, pageIndexs, PageSize, out pageCount);
        }


        /// <summary>
        /// 处理应用投诉管理
        /// </summary>
        /// <param name="cid">选择的应用投诉ID</param>

        /// <returns></returns>
        public bool ComplaintLC(string cid, string remark, string r_auditor)
        {
            return dal.ComplaintLC(cid, remark, r_auditor);
        }


    }
}
