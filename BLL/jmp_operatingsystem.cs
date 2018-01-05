using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.MDL;
namespace JMP.BLL
{
    ///<summary>
    ///手机操作系统统计
    ///</summary>
    public partial class jmp_operatingsystem
    {

        private readonly JMP.DAL.jmp_operatingsystem dal = new JMP.DAL.jmp_operatingsystem();
        public jmp_operatingsystem()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_operatingsystem model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_operatingsystem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int o_id)
        {

            return dal.Delete(o_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string o_idlist)
        {
            return dal.DeleteList(o_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_operatingsystem GetModel(int o_id)
        {

            return dal.GetModel(o_id);
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
        public List<JMP.MDL.jmp_operatingsystem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_operatingsystem> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_operatingsystem> modelList = new List<JMP.MDL.jmp_operatingsystem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_operatingsystem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_operatingsystem();
                    if (dt.Rows[n]["o_id"].ToString() != "")
                    {
                        model.o_id = int.Parse(dt.Rows[n]["o_id"].ToString());
                    }
                    model.o_system = dt.Rows[n]["o_system"].ToString();
                    if (dt.Rows[n]["o_app_id"].ToString() != "")
                    {
                        model.o_app_id = int.Parse(dt.Rows[n]["o_app_id"].ToString());
                    }
                    if (dt.Rows[n]["o_count"].ToString() != "")
                    {
                        model.o_count = int.Parse(dt.Rows[n]["o_count"].ToString());
                    }
                    if (dt.Rows[n]["o_time"].ToString() != "")
                    {
                        model.o_time = DateTime.Parse(dt.Rows[n]["o_time"].ToString());
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
        /// 获得数据列表用于图标统计
        /// </summary>
        public List<JMP.MDL.jmp_operatingsystem> GetListTjCount(string stime, string etime,int searchType,string searchname)
        {
            return dal.GetListTjCount(stime, etime, searchType, searchname);
        }
         /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_operatingsystem ModelTjCount(string stime, string etime)
        {
            return dal.ModelTjCount( stime, etime);
        }
    }
}