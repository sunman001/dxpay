using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///公告表
    ///</summary>
    public partial class jmp_notice
    {

        private readonly JMP.DAL.jmp_notice dal = new JMP.DAL.jmp_notice();
        public jmp_notice()
        { }

        #region  Method
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
        public int Add(JMP.MDL.jmp_notice model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_notice model)
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string n_idlist)
        {
            return dal.DeleteList(n_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_notice GetModel(int n_id)
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_notice> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_notice> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_notice> modelList = new List<JMP.MDL.jmp_notice>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_notice model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_notice();
                    if (dt.Rows[n]["n_id"].ToString() != "")
                    {
                        model.n_id = int.Parse(dt.Rows[n]["n_id"].ToString());
                    }
                    model.n_title = dt.Rows[n]["n_title"].ToString();
                    model.n_content = dt.Rows[n]["n_content"].ToString();
                    if (dt.Rows[n]["n_time"].ToString() != "")
                    {
                        model.n_time = DateTime.Parse(dt.Rows[n]["n_time"].ToString());
                    }
                    if (dt.Rows[n]["n_top"].ToString() != "")
                    {
                        model.n_top = int.Parse(dt.Rows[n]["n_top"].ToString());
                    }
                    if (dt.Rows[n]["n_state"].ToString() != "")
                    {
                        model.n_state = int.Parse(dt.Rows[n]["n_state"].ToString());
                    }
                    if (dt.Rows[n]["n_locuserid"].ToString() != "")
                    {
                        model.n_locuserid = int.Parse(dt.Rows[n]["n_locuserid"].ToString());
                    }


                    modelList.Add(model);
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
        #endregion
        /// <summary>
        /// 查询公告信息
        /// </summary>
        /// <param name="StrSql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_notice> SelectList(string StrSql, string order,  int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(StrSql,order, pageIndexs, PageSize, out pageCount);
        }
          /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            return dal.UpdateLocUserState(u_idlist,state);
        }
    }
}