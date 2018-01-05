using System;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //jmp_app_monitor_time_range
    public class jmp_app_monitor_time_range
    {

        private readonly JMP.DAL.jmp_app_monitor_time_range dal = new JMP.DAL.jmp_app_monitor_time_range();
        public jmp_app_monitor_time_range()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_app_monitor_time_range model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_app_monitor_time_range model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_app_monitor_time_range GetModel(int Id)
        {

            return dal.GetModel(Id);
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
        public List<JMP.MDL.jmp_app_monitor_time_range> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_app_monitor_time_range> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_app_monitor_time_range> modelList = new List<JMP.MDL.jmp_app_monitor_time_range>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_app_monitor_time_range model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_app_monitor_time_range();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["AppMonitorId"].ToString() != "")
                    {
                        model.AppMonitorId = int.Parse(dt.Rows[n]["AppMonitorId"].ToString());
                    }
                    if (dt.Rows[n]["WhichHour"].ToString() != "")
                    {
                        model.WhichHour = int.Parse(dt.Rows[n]["WhichHour"].ToString());
                    }
                    if (dt.Rows[n]["Minutes"].ToString() != "")
                    {
                        model.Minutes = int.Parse(dt.Rows[n]["Minutes"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedBy"].ToString() != "")
                    {
                        model.CreatedBy = int.Parse(dt.Rows[n]["CreatedBy"].ToString());
                    }
                    if (dt.Rows[n]["ModifiedOn"].ToString() != "")
                    {
                        model.ModifiedOn = DateTime.Parse(dt.Rows[n]["ModifiedOn"].ToString());
                    }
                    if (dt.Rows[n]["ModifiedBy"].ToString() != "")
                    {
                        model.ModifiedBy = int.Parse(dt.Rows[n]["ModifiedBy"].ToString());
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

    }
}