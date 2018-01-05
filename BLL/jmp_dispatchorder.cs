using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///调单设置
    ///</summary>
    public partial class jmp_dispatchorder
    {

        private readonly JMP.DAL.jmp_dispatchorder dal = new JMP.DAL.jmp_dispatchorder();
        public jmp_dispatchorder()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int d_id)
        {
            return dal.Exists(d_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_dispatchorder model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_dispatchorder model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int d_id)
        {

            return dal.Delete(d_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string d_idlist)
        {
            return dal.DeleteList(d_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_dispatchorder GetModel(int d_id)
        {

            return dal.GetModel(d_id);
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
        public List<JMP.MDL.jmp_dispatchorder> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_dispatchorder> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_dispatchorder> modelList = new List<JMP.MDL.jmp_dispatchorder>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_dispatchorder model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_dispatchorder();
                    if (dt.Rows[n]["d_id"].ToString() != "")
                    {
                        model.d_id = int.Parse(dt.Rows[n]["d_id"].ToString());
                    }
                    if (dt.Rows[n]["d_apptyeid"].ToString() != "")
                    {
                        model.d_apptyeid = int.Parse(dt.Rows[n]["d_apptyeid"].ToString());
                    }
                    if (dt.Rows[n]["d_ratio"].ToString() != "")
                    {
                        model.d_ratio = decimal.Parse(dt.Rows[n]["d_ratio"].ToString());
                    }
                    if (dt.Rows[n]["d_state"].ToString() != "")
                    {
                        model.d_state = int.Parse(dt.Rows[n]["d_state"].ToString());
                    }
                    if (dt.Rows[n]["d_datatime"].ToString() != "")
                    {
                        model.d_datatime = DateTime.Parse(dt.Rows[n]["d_datatime"].ToString());
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
        /// 分页查询信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序类型（0：升序，1：降序）</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_dispatchorder> SelectPager(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.SelectPager(sql, Order, PageIndex, PageSize, out Count);
        }
        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            return dal.UpdateState(u_idlist, state);
        }
        /// <summary>
        /// 查询调单比列
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public object Selectddbl(int appid)
        {
            return dal.Selectddbl(appid);
        }
    }
}