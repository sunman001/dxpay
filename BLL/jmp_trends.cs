using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///流量趋势汇总
    ///</summary>
    public partial class jmp_trends
    {

        private readonly JMP.DAL.jmp_trends dal = new JMP.DAL.jmp_trends();
        public jmp_trends()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int t_id)
        {
            return dal.Exists(t_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_trends model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_trends model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int t_id)
        {

            return dal.Delete(t_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string t_idlist)
        {
            return dal.DeleteList(t_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_trends GetModel(int t_id)
        {

            return dal.GetModel(t_id);
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
        public List<JMP.MDL.jmp_trends> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_trends> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_trends> modelList = new List<JMP.MDL.jmp_trends>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_trends model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_trends();
                    if (dt.Rows[n]["t_id"].ToString() != "")
                    {
                        model.t_id = int.Parse(dt.Rows[n]["t_id"].ToString());
                    }
                    if (dt.Rows[n]["t_app_id"].ToString() != "")
                    {
                        model.t_app_id = int.Parse(dt.Rows[n]["t_app_id"].ToString());
                    }
                    if (dt.Rows[n]["t_newcount"].ToString() != "")
                    {
                        model.t_newcount = int.Parse(dt.Rows[n]["t_newcount"].ToString());
                    }
                    if (dt.Rows[n]["t_activecount"].ToString() != "")
                    {
                        model.t_activecount = int.Parse(dt.Rows[n]["t_activecount"].ToString());
                    }
                    if (dt.Rows[n]["t_time"].ToString() != "")
                    {
                        model.t_time = DateTime.Parse(dt.Rows[n]["t_time"].ToString());
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
        /// 获得数据列表
        /// </summary>
        public DataTable GetListDataTable(string stime, string etime, int searchType, string searchname)
        {
            return dal.GetListDataTable(stime, etime, searchType, searchname);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListSet(string stime, string etime, int searchType, string searchname)
        {
            return dal.GetListSet(stime, etime, searchType, searchname);
        }
    }
}