using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    //营收概况（活跃用户）
    public partial class jmp_revenue_active
    {
        private readonly JMP.DAL.jmp_revenue_active dal = new JMP.DAL.jmp_revenue_active();
        public jmp_revenue_active()
        { }

        #region  Method
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
        public bool Add(JMP.MDL.jmp_revenue_active model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_revenue_active model)
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
        public JMP.MDL.jmp_revenue_active GetModel(int r_id)
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
        public List<JMP.MDL.jmp_revenue_active> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_revenue_active> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_revenue_active> modelList = new List<JMP.MDL.jmp_revenue_active>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_revenue_active model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_revenue_active();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_users"].ToString() != "")
                    {
                        model.r_users = int.Parse(dt.Rows[n]["r_users"].ToString());
                    }
                    if (dt.Rows[n]["r_moneys"].ToString() != "")
                    {
                        model.r_moneys = decimal.Parse(dt.Rows[n]["r_moneys"].ToString());
                    }
                    if (dt.Rows[n]["r_orders"].ToString() != "")
                    {
                        model.r_orders = int.Parse(dt.Rows[n]["r_orders"].ToString());
                    }
                    if (dt.Rows[n]["r_appid"].ToString() != "")
                    {
                        model.r_appid = int.Parse(dt.Rows[n]["r_appid"].ToString());
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
        /// 获得数据列表
        /// </summary>
        /// <param name="sdate">开始日期</param>
        /// <param name="eate">结束日期</param>
        /// <param name="sumTypes">汇总方式（应用名称、开发者邮箱）</param>
        /// <param name="sumkeys">汇总值</param>
        /// <returns></returns>
        public DataTable GetLists(string sdate, string eate, string sumTypes, string sumkeys)
        {
            return dal.GetLists(sdate, eate, sumTypes, sumkeys);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="s_time">开始日期</param>
        /// <param name="e_time">结束日期</param>
        /// <param name="u_id">用户id</param>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public DataTable GetListsUser(string s_time, string e_time, string u_id, string a_id)
        {
            return dal.GetListsUser(s_time, e_time, u_id, a_id);
        }
        #endregion

    }
}
