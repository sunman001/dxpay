using JMP.Model.Query.WorkOrder;
using System;
using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    //jmp_scheduling
    public partial class jmp_scheduling
    {

        private readonly JMP.DAL.jmp_scheduling dal = new JMP.DAL.jmp_scheduling();
        public jmp_scheduling()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_scheduling model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_scheduling model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_scheduling GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_scheduling GetModel(string startdate)
        {

            return dal.GetModel(startdate);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_scheduling GetModel(string startdate,int Type)
        {

            return dal.GetModel(startdate,Type);
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
        public List<JMP.MDL.jmp_scheduling> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_scheduling> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_scheduling> modelList = new List<JMP.MDL.jmp_scheduling>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_scheduling model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_scheduling();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["watchid"].ToString() != "")
                    {
                        model.watchid = int.Parse(dt.Rows[n]["watchid"].ToString());
                    }
                    if (dt.Rows[n]["watchstartdate"].ToString() != "")
                    {
                        model.watchstartdate = DateTime.Parse(dt.Rows[n]["watchstartdate"].ToString());
                    }
                    if (dt.Rows[n]["createdon"].ToString() != "")
                    {
                        model.createdon = DateTime.Parse(dt.Rows[n]["createdon"].ToString());
                    }
                    if (dt.Rows[n]["createdbyid"].ToString() != "")
                    {
                        model.createdbyid = int.Parse(dt.Rows[n]["createdbyid"].ToString());
                    }
                    if (dt.Rows[n]["watchenddate"].ToString() != "")
                    {
                        model.watchenddate = DateTime.Parse(dt.Rows[n]["watchenddate"].ToString());
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
        /// 获取数量列表
        /// </summary>
        /// <param name="Sname">值班人</param>
        /// <param name="startdate">值班开始时间</param>
        /// <param name="enddate">值班结束</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_scheduling> SelectList(bool isType, string Sname,int type,  bool isSelect,int userid, DateTime startdate, DateTime enddate, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(isType, Sname, type,isSelect ,userid , startdate, enddate, pageIndexs, PageSize, out pageCount);
        }

        #endregion

        #region 自定义方法
        /// <summary>
        /// 查询所有的当天值班人员集合
        /// </summary>
        /// <returns></returns>
        public List<WatcherQuerier> FindAllWatcherOfTheDay()
        {
            return dal.FindAllWatcherOfTheDay();
        }

        public bool ScheduleExists(string scheStartTime,int deptType)
        {
            var mo = GetModel(scheStartTime, deptType);
            //判断是否已存在相同数据
            return mo != null;
        }
        #endregion

    }
}