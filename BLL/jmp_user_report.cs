using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    //用户报表
    public partial class jmp_user_report
    {

        private readonly JMP.DAL.jmp_user_report dal = new JMP.DAL.jmp_user_report();
        public jmp_user_report()
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
        public int Add(JMP.MDL.jmp_user_report model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_user_report model)
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
        public JMP.MDL.jmp_user_report GetModel(int r_id)
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
        public List<JMP.MDL.jmp_user_report> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_user_report> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_user_report> modelList = new List<JMP.MDL.jmp_user_report>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_user_report model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_user_report();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    if (dt.Rows[n]["r_notpay"].ToString() != "")
                    {
                        model.a_notpay = decimal.Parse(dt.Rows[n]["r_notpay"].ToString());
                    }
                    if (dt.Rows[n]["r_alipay"].ToString() != "")
                    {
                        model.a_alipay = decimal.Parse(dt.Rows[n]["r_alipay"].ToString());
                    }
                    if (dt.Rows[n]["r_wechat"].ToString() != "")
                    {
                        model.a_wechat = decimal.Parse(dt.Rows[n]["r_wechat"].ToString());
                    }
                    model.r_app_key = dt.Rows[n]["r_app_key"].ToString();
                    model.r_app_name = dt.Rows[n]["r_app_name"].ToString();
                    if (dt.Rows[n]["r_user_id"].ToString() != "")
                    {
                        model.r_user_id = int.Parse(dt.Rows[n]["r_user_id"].ToString());
                    }
                    model.r_user_name = dt.Rows[n]["r_user_name"].ToString();
                    if (dt.Rows[n]["r_date"].ToString() != "")
                    {
                        model.r_date = DateTime.Parse(dt.Rows[n]["r_date"].ToString());
                    }
                    if (dt.Rows[n]["r_create"].ToString() != "")
                    {
                        model.r_create = DateTime.Parse(dt.Rows[n]["r_create"].ToString());
                    }
                    if (dt.Rows[n]["r_equipment"].ToString() != "")
                    {
                        model.r_equipment = decimal.Parse(dt.Rows[n]["r_equipment"].ToString());
                    }
                    if (dt.Rows[n]["r_succeed"].ToString() != "")
                    {
                        model.a_succeed = decimal.Parse(dt.Rows[n]["r_succeed"].ToString());
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
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public DataTable GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, Order, PageIndex, PageSize, out Count);
        }

        /// <summary>
        /// 获取当天的报表（应用和用户）
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        /// <returns></returns>
        public DataTable GetTodayReport(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetTodayList(sql, Order, PageIndex, PageSize, out Count);
        }
        #endregion
        /// <summary>
        /// 根据sql语句查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable CountSect(string sql)
        {
            return dal.CountSect(sql);
        }
        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_user_report> DcSelectList(string sql)
        {
            return dal.DcSelectList(sql);
        }

    }
}
