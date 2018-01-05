using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///手机分辨率统计
    ///</summary>
    public partial class jmp_resolution
    {

        private readonly JMP.DAL.jmp_resolution dal = new JMP.DAL.jmp_resolution();
        public jmp_resolution()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_resolution model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_resolution model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int r_id)
        {

            return dal.Delete(r_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            return dal.DeleteList(r_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_resolution GetModel(int r_id)
        {

            return dal.GetModel(r_id);
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
        public List<JMP.MDL.jmp_resolution> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_resolution> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_resolution> modelList = new List<JMP.MDL.jmp_resolution>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_resolution model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_resolution();
                    if (dt.Rows[n]["r_id"].ToString() != "")
                    {
                        model.r_id = int.Parse(dt.Rows[n]["r_id"].ToString());
                    }
                    model.r_screen = dt.Rows[n]["r_screen"].ToString();
                    if (dt.Rows[n]["r_app_id"].ToString() != "")
                    {
                        model.r_app_id = int.Parse(dt.Rows[n]["r_app_id"].ToString());
                    }
                    if (dt.Rows[n]["r_count"].ToString() != "")
                    {
                        model.r_count = int.Parse(dt.Rows[n]["r_count"].ToString());
                    }
                    if (dt.Rows[n]["r_time"].ToString() != "")
                    {
                        model.r_time = DateTime.Parse(dt.Rows[n]["r_time"].ToString());
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
        public List<JMP.MDL.jmp_resolution> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            return dal.GetListTjCount(stime, etime,  searchType,  searchname);
        }
         /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_resolution modelTjCount(string stime, string etime)
        {
            return dal.modelTjCount(stime, etime);
        }
    }
}