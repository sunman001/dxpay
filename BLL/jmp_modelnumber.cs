using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///手机型号统计
    ///</summary>
    public partial class jmp_modelnumber
    {

        private readonly JMP.DAL.jmp_modelnumber dal = new JMP.DAL.jmp_modelnumber();
        public jmp_modelnumber()
        { }
        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_modelnumber model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_modelnumber model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int m_id)
        {

            return dal.Delete(m_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string m_idlist)
        {
            return dal.DeleteList(m_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_modelnumber GetModel(int m_id)
        {

            return dal.GetModel(m_id);
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
        public List<JMP.MDL.jmp_modelnumber> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_modelnumber> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_modelnumber> modelList = new List<JMP.MDL.jmp_modelnumber>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_modelnumber model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_modelnumber();
                    if (dt.Rows[n]["m_id"].ToString() != "")
                    {
                        model.m_id = int.Parse(dt.Rows[n]["m_id"].ToString());
                    }
                    model.m_sdkver = dt.Rows[n]["m_sdkver"].ToString();
                    if (dt.Rows[n]["m_app_id"].ToString() != "")
                    {
                        model.m_app_id = int.Parse(dt.Rows[n]["m_app_id"].ToString());
                    }
                    if (dt.Rows[n]["m_count"].ToString() != "")
                    {
                        model.m_count = int.Parse(dt.Rows[n]["m_count"].ToString());
                    }
                    if (dt.Rows[n]["m_time"].ToString() != "")
                    {
                        model.m_time = DateTime.Parse(dt.Rows[n]["m_time"].ToString());
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
        public List<JMP.MDL.jmp_modelnumber> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            return dal.GetListTjCount(stime,etime,  searchType,  searchname);
        }
         /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_modelnumber modelTjCount(string stime, string etime)
        {
            return dal.modelTjCount(stime,etime);
        }
    }
}