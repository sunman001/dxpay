using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///应用监控表
    ///</summary>
    public partial class appmonitor
    {

        private readonly JMP.DAL.appmonitor dal = new JMP.DAL.appmonitor();
        public appmonitor()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int a_id)
        {
            return dal.Exists(a_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.appmonitor model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.appmonitor model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int a_id)
        {

            return dal.Delete(a_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string a_idlist)
        {
            return dal.DeleteList(a_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.appmonitor GetModel(int a_id)
        {

            return dal.GetModel(a_id);
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
        public List<JMP.MDL.appmonitor> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JMP.MDL.appmonitor> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.appmonitor> modelList = new List<JMP.MDL.appmonitor>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.appmonitor model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.appmonitor();
                    if (dt.Rows[n]["a_id"].ToString() != "")
                    {
                        model.a_id = int.Parse(dt.Rows[n]["a_id"].ToString());
                    }
                    if (dt.Rows[n]["a_appid"].ToString() != "")
                    {
                        model.a_appid = int.Parse(dt.Rows[n]["a_appid"].ToString());
                    }
                    if (dt.Rows[n]["a_request"].ToString() != "")
                    {
                        model.a_request = decimal.Parse(dt.Rows[n]["a_request"].ToString());
                    }
                    if (dt.Rows[n]["a_minute"].ToString() != "")
                    {
                        model.a_minute = int.Parse(dt.Rows[n]["a_minute"].ToString());
                    }
                    if (dt.Rows[n]["a_state"].ToString() != "")
                    {
                        model.a_state = int.Parse(dt.Rows[n]["a_state"].ToString());
                    }
                    if (dt.Rows[n]["a_datetime"].ToString() != "")
                    {
                        model.a_datetime = DateTime.Parse(dt.Rows[n]["a_datetime"].ToString());
                    }
                    //a_type
                    if (dt.Rows[n]["a_type"].ToString() != "")
                    {
                        model.a_type = int.Parse(dt.Rows[n]["a_type"].ToString());
                    }
                    model.a_time_range = dt.Rows[n]["a_time_range"].ToString();


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
        /// 根据应用id查询应用投诉信息
        /// </summary>
        /// <param name="c_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.appmonitor SelectId(int c_id)
        {
            return dal.SelectId(c_id);
        }

        /// <summary>
        /// 查询应用投诉管理
        /// </summary>
        /// <param name="SelectState">状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="aType"></param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="userid">用户id（后台默认传0，开发者平台默认传用户id）</param>
        /// <param name="auditstate">处理状态</param>
        /// <returns></returns>
        public List<JMP.MDL.appmonitor> SelectList(  string SelectState,string sea_name, int type, int searchDesc, int aType, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(SelectState,sea_name, type, searchDesc, aType, pageIndexs, PageSize, out pageCount);
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

        #endregion

        /// <summary>
        /// 查询指定时间内无订单的应用(关联表:appmonitor)
        /// </summary>
        /// <returns></returns>
        public DataSet GetNoOrderApp()
        {
            return dal.GetNoOrderApp();
        }

        /// <summary>
        /// 获得状态为可用的数据列表(a_state=1)
        /// </summary>
        public List<MDL.appmonitor> GetEnabledList()
        {
            var ds = dal.GetEnabledList();
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 查询应用监控是否存在
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="monitorType">监控类型</param>
        /// <returns></returns>
        public bool Exists(int appId, int monitorType)
        {
            return dal.Exists(appId,monitorType);
        }
    }
}