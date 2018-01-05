using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    //阶梯手续费表
    public partial class jmp_poundage
    {

        private readonly JMP.DAL.jmp_poundage dal = new JMP.DAL.jmp_poundage();
        public jmp_poundage()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int p_id)
        {
            return dal.Exists(p_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_poundage model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_poundage model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_id)
        {

            return dal.Delete(p_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string p_idlist)
        {
            return dal.DeleteList(p_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_poundage GetModel(int p_id)
        {

            return dal.GetModel(p_id);
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
        public List<JMP.MDL.jmp_poundage> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_poundage> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_poundage> modelList = new List<JMP.MDL.jmp_poundage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_poundage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_poundage();
                    if (dt.Rows[n]["p_id"].ToString() != "")
                    {
                        model.p_id = int.Parse(dt.Rows[n]["p_id"].ToString());
                    }
                    if (dt.Rows[n]["p_sprice"].ToString() != "")
                    {
                        model.p_sprice = int.Parse(dt.Rows[n]["p_sprice"].ToString());
                    }
                    if (dt.Rows[n]["p_eprice"].ToString() != "")
                    {
                        model.p_eprice = int.Parse(dt.Rows[n]["p_eprice"].ToString());
                    }
                    if (dt.Rows[n]["p_rate"].ToString() != "")
                    {
                        model.p_rate = decimal.Parse(dt.Rows[n]["p_rate"].ToString());
                    }
                    if (dt.Rows[n]["p_state"].ToString() != "")
                    {
                        model.p_state = int.Parse(dt.Rows[n]["p_state"].ToString());
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
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_poundage> SelectList(string sqls,string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, Order, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 批量更新状态
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