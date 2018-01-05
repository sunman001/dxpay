using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //用户日志表
    public class JmpMonitorMinuteDetails
    {

        private readonly DAL.JmpMonitorMinuteDetails _dal = new DAL.JmpMonitorMinuteDetails();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int lId)
        {
            return _dal.Exists(lId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.JmpMonitorMinuteDetails model)
        {
            return _dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.JmpMonitorMinuteDetails model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int lId)
        {

            return _dal.Delete(lId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string lIdlist)
        {
            return _dal.DeleteList(lIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.JmpMonitorMinuteDetails GetModel(int lId)
        {

            return _dal.GetModel(lId);
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
        public DataSet GetList(int top, string strWhere, string filedOrder)
        {
            return _dal.GetList(top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.JmpMonitorMinuteDetails> GetModelList(string strWhere)
        {
            var ds = _dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.JmpMonitorMinuteDetails> DataTableToList(DataTable dt)
        {
            var modelList = new List<Model.JmpMonitorMinuteDetails>();
            var rowsCount = dt.Rows.Count;
            if (rowsCount <= 0) return modelList;
            for (var n = 0; n < rowsCount; n++)
            {
                var model = new Model.JmpMonitorMinuteDetails();
                if (dt.Rows[0]["id"].ToString() != "")
                {
                    model.Id = int.Parse(dt.Rows[0]["id"].ToString());
                }
                if (dt.Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(dt.Rows[0]["AppId"].ToString());
                }
                if (dt.Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(dt.Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = dt.Rows[0]["CreatedByName"].ToString();
                if (dt.Rows[0]["Minutes"].ToString() != "")
                {
                    model.Minutes = int.Parse(dt.Rows[0]["Minutes"].ToString());
                }
                if (dt.Rows[0]["MonitorType"].ToString() != "")
                {
                    model.MonitorType = int.Parse(dt.Rows[0]["MonitorType"].ToString());
                }
                if (dt.Rows[0]["WhichHour"].ToString() != "")
                {
                    model.WhichHour = int.Parse(dt.Rows[0]["WhichHour"].ToString());
                }
                if (dt.Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(dt.Rows[0]["CreatedOn"].ToString());
                }
                    
                modelList.Add(model);
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
        /// <param name="sql">查询语句</param>
        /// <param name="order">排序字段</param>
        /// <param name="currPage">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public DataTable GetLists(string sql, string order, int currPage, int pageSize, out int pageCount)
        {
            return _dal.GetLists(sql, order, currPage, pageSize, out pageCount);
        }
        #endregion

        /// <summary>
        /// 删除指定监控类型和应用下所有监控时间
        /// <param name="appId">应用ID</param>
        /// <param name="monitorType">监控类型</param>
        /// </summary>
        public bool DeleteByMonitorType(int appId, int monitorType)
        {
            return _dal.DeleteByMonitorType(appId, monitorType);
        }
    }
}