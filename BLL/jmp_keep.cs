using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///留存用户统计
    ///</summary>
    public partial class jmp_keep
    {

        private readonly JMP.DAL.jmp_keep dal = new JMP.DAL.jmp_keep();
        public jmp_keep()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int k_id)
        {
            return dal.Exists(k_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_keep model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_keep model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int k_id)
        {

            return dal.Delete(k_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string k_idlist)
        {
            return dal.DeleteList(k_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_keep GetModel(int k_id)
        {

            return dal.GetModel(k_id);
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
        public List<JMP.MDL.jmp_keep> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_keep> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_keep> modelList = new List<JMP.MDL.jmp_keep>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_keep model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_keep();
                    if (dt.Rows[n]["k_id"].ToString() != "")
                    {
                        model.k_id = int.Parse(dt.Rows[n]["k_id"].ToString());
                    }
                    if (dt.Rows[n]["k_day6"].ToString() != "")
                    {
                        model.k_day6 = decimal.Parse(dt.Rows[n]["k_day6"].ToString());
                    }
                    if (dt.Rows[n]["k_day7"].ToString() != "")
                    {
                        model.k_day7 = decimal.Parse(dt.Rows[n]["k_day7"].ToString());
                    }
                    if (dt.Rows[n]["k_day14"].ToString() != "")
                    {
                        model.k_day14 = decimal.Parse(dt.Rows[n]["k_day14"].ToString());
                    }
                    if (dt.Rows[n]["k_day30"].ToString() != "")
                    {
                        model.k_day30 = decimal.Parse(dt.Rows[n]["k_day30"].ToString());
                    }
                    if (dt.Rows[n]["k_time"].ToString() != "")
                    {
                        model.k_time = DateTime.Parse(dt.Rows[n]["k_time"].ToString());
                    }
                    if (dt.Rows[n]["k_app_id"].ToString() != "")
                    {
                        model.k_app_id = int.Parse(dt.Rows[n]["k_app_id"].ToString());
                    }
                    if (dt.Rows[n]["k_type"].ToString() != "")
                    {
                        model.k_type = int.Parse(dt.Rows[n]["k_type"].ToString());
                    }
                    if (dt.Rows[n]["k_usercount"].ToString() != "")
                    {
                        model.k_usercount = int.Parse(dt.Rows[n]["k_usercount"].ToString());
                    }
                    if (dt.Rows[n]["k_day1"].ToString() != "")
                    {
                        model.k_day1 = decimal.Parse(dt.Rows[n]["k_day1"].ToString());
                    }
                    if (dt.Rows[n]["k_day2"].ToString() != "")
                    {
                        model.k_day2 = decimal.Parse(dt.Rows[n]["k_day2"].ToString());
                    }
                    if (dt.Rows[n]["k_day3"].ToString() != "")
                    {
                        model.k_day3 = decimal.Parse(dt.Rows[n]["k_day3"].ToString());
                    }
                    if (dt.Rows[n]["k_day4"].ToString() != "")
                    {
                        model.k_day4 = decimal.Parse(dt.Rows[n]["k_day4"].ToString());
                    }
                    if (dt.Rows[n]["k_day5"].ToString() != "")
                    {
                        model.k_day5 = decimal.Parse(dt.Rows[n]["k_day5"].ToString());
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
        /// 根据条件查询留存汇总信息
        /// </summary>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <param name="searchType">查询条件</param>
        /// <param name="searchname">查询内容</param>
        /// <returns></returns>
        public DataTable SelectList(string stime, string etime, int searchType, string searchname, int setype)
        {
            return dal.SelectList(stime, etime, searchType, searchname, setype);
        }
    }
}