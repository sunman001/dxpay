using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    ///<summary>
    ///通道监控表
    ///</summary>
    public class MonitorChannel
    {

        private readonly DAL.MonitorChannel _dal = new DAL.MonitorChannel();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int a_id)
        {
            return _dal.Exists(a_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.MonitorChannel model)
        {
            return _dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.MonitorChannel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int a_id)
        {

            return _dal.Delete(a_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string a_idlist)
        {
            return _dal.DeleteList(a_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MonitorChannel GetModel(int a_id)
        {

            return _dal.GetModel(a_id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return _dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.MonitorChannel> GetModelList(string strWhere)
        {
            DataSet ds = _dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.MonitorChannel> DataTableToList(DataTable dt)
        {
            var modelList = new List<Model.MonitorChannel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.MonitorChannel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.MonitorChannel();
                    if (dt.Rows[n]["a_id"].ToString() != "")
                    {
                        model.a_id = int.Parse(dt.Rows[n]["a_id"].ToString());
                    }
                    if (dt.Rows[n]["ChannelId"].ToString() != "")
                    {
                        model.ChannelId = int.Parse(dt.Rows[n]["ChannelId"].ToString());
                    }
                    if (dt.Rows[n]["Threshold"].ToString() != "")
                    {
                        model.Threshold = decimal.Parse(dt.Rows[n]["Threshold"].ToString());
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
        /// 根据通道id查询通道投诉信息
        /// </summary>
        /// <param name="c_id">通道id</param>
        /// <returns></returns>
        public Model.MonitorChannel SelectId(int c_id)
        {
            return _dal.SelectId(c_id);
        }

        /// <summary>
        /// 查询通道投诉管理
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
        public List<Model.MonitorChannel> SelectList(  string SelectState,string sea_name, int type, int searchDesc, int aType, int pageIndexs, int PageSize, out int pageCount)
        {
            return _dal.SelectList(SelectState,sea_name, type, searchDesc, aType, pageIndexs, PageSize, out pageCount);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            return _dal.UpdateState(u_idlist, state);
        }

        #endregion

        /// <summary>
        /// 查询指定时间内无订单的通道(关联表:MonitorChannel)
        /// </summary>
        /// <returns></returns>
        public DataSet GetNoOrderApp()
        {
            return _dal.GetNoOrderApp();
        }

        /// <summary>
        /// 查询通道监控是否存在
        /// </summary>
        /// <param name="appId">通道ID</param>
        /// <param name="monitorType">监控类型</param>
        /// <returns></returns>
        public bool Exists(int appId, int monitorType)
        {
            return _dal.Exists(appId,monitorType);
        }

        public Model.MonitorChannel GetModelByTD(int appId, int monitorType)
        {

            return _dal.GetModelByTD(appId,monitorType);
        }
    }
}