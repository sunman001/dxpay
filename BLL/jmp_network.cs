using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///手机网络统计
    ///</summary>
    public partial class jmp_network
    {

        private readonly JMP.DAL.jmp_network dal = new JMP.DAL.jmp_network();
        public jmp_network()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_network model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_network model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int n_id)
        {

            return dal.Delete(n_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string n_idlist)
        {
            return dal.DeleteList(n_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_network GetModel(int n_id)
        {

            return dal.GetModel(n_id);
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
        public List<JMP.MDL.jmp_network> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_network> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_network> modelList = new List<JMP.MDL.jmp_network>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_network model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_network();
                    if (dt.Rows[n]["n_id"].ToString() != "")
                    {
                        model.n_id = int.Parse(dt.Rows[n]["n_id"].ToString());
                    }
                    model.n_network = dt.Rows[n]["n_network"].ToString();
                    if (dt.Rows[n]["n_app_id"].ToString() != "")
                    {
                        model.n_app_id = int.Parse(dt.Rows[n]["n_app_id"].ToString());
                    }
                    if (dt.Rows[n]["n_count"].ToString() != "")
                    {
                        model.n_count = int.Parse(dt.Rows[n]["n_count"].ToString());
                    }
                    if (dt.Rows[n]["n_time"].ToString() != "")
                    {
                        model.n_time = DateTime.Parse(dt.Rows[n]["n_time"].ToString());
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
        public List<JMP.MDL.jmp_network> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            return dal.GetListTjCount(stime, etime, searchType, searchname);
        }
        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_network modelTjCount(string stime, string etime)
        {
            return dal.modelTjCount(stime, etime);
        }
    }
}