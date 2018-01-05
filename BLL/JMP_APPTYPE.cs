using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //应用类型表
    public partial class jmp_apptype
    {

        private readonly JMP.DAL.jmp_apptype dal = new JMP.DAL.jmp_apptype();
        public jmp_apptype()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int t_id)
        {
            return dal.Exists(t_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_apptype model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_apptype model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int t_id)
        {

            return dal.Delete(t_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string t_idlist)
        {
            return dal.DeleteList(t_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_apptype GetModel(int t_id)
        {

            return dal.GetModel(t_id);
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
        public List<JMP.MDL.jmp_apptype> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_apptype> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_apptype> modelList = new List<JMP.MDL.jmp_apptype>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_apptype model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_apptype();
                    if (dt.Rows[n]["t_id"].ToString() != "")
                    {
                        model.t_id = int.Parse(dt.Rows[n]["t_id"].ToString());
                    }
                    model.t_name = dt.Rows[n]["t_name"].ToString();
                    model.t_sort = dt.Rows[n]["t_sort"].ToString();
                    if (dt.Rows[n]["t_topid"].ToString() != "")
                    {
                        model.t_topid = int.Parse(dt.Rows[n]["t_topid"].ToString());
                    }
                    if (dt.Rows[n]["t_state"].ToString() != "")
                    {
                        model.t_state = int.Parse(dt.Rows[n]["t_state"].ToString());
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
        /// 查询应用信息
        /// </summary>
        /// <param name="yylx">所属应用类型</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_apptype> SelectList(int yylx, string sea_name, string type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(yylx, sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            return dal.UpdateLocUserState(u_idlist, state);
        }
    }
}