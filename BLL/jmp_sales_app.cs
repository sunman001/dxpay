using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;

namespace JMP.BLL
{
    //销售排行（应用）
    public partial class jmp_sales_app
    {
        private readonly JMP.DAL.jmp_sales_app dal = new JMP.DAL.jmp_sales_app();
        public jmp_sales_app()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int r_id, int r_appid, decimal r_moneys, DateTime r_date)
        {
            return dal.Exists(r_id, r_appid, r_moneys, r_date);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_sales_app model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_sales_app model)
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            return dal.DeleteList(r_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_sales_app GetModel(int r_id)
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
        public List<JMP.MDL.jmp_sales_app> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_sales_app> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_sales_app> modelList = new List<JMP.MDL.jmp_sales_app>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_sales_app model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_sales_app();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_appid"].ToString() != "")
                    {
                        model.r_appid = int.Parse(dt.Rows[n]["r_appid"].ToString());
                    }
                    if (dt.Rows[n]["r_moneys"].ToString() != "")
                    {
                        model.r_moneys = decimal.Parse(dt.Rows[n]["r_moneys"].ToString());
                    }
                    if (dt.Rows[n]["r_date"].ToString() != "")
                    {
                        model.r_date = DateTime.Parse(dt.Rows[n]["r_date"].ToString());
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

        /// <summary>
        /// 获取数据列表
        /// </summary>        
        /// <param name="stime">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <param name="searchType">查询字段</param>
        /// <param name="searchKey">查询字段值</param>
        /// <param name="top">前几条</param> 
        /// <returns></returns>
        public DataTable GetLists(string stime, string etime, string searchType, string searchKey, int top = 0)
        {
            return dal.GetLists(stime, etime, searchType, searchKey, top);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="s_time">开始日期</param>
        /// <param name="e_time">结束日期</param>
        /// <param name="u_id">用户id</param>
        /// <param name="a_id">应用id</param>
        /// <param name="top">前几条</param>
        /// <returns></returns>
        public DataTable GetListsUser(string s_time, string e_time, string u_id, string a_id, int top = 0)
        {
            return dal.GetListsUser(s_time, e_time, u_id, a_id, top);
        }
        #endregion

    }
}