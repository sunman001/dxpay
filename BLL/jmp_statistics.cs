using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///手机品牌统计
    ///</summary>
    public partial class jmp_statistics
    {

        private readonly JMP.DAL.jmp_statistics dal = new JMP.DAL.jmp_statistics();
        public jmp_statistics()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int s_id)
        {
            return dal.Exists(s_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_statistics model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_statistics model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int s_id)
        {

            return dal.Delete(s_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string s_idlist)
        {
            return dal.DeleteList(s_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_statistics GetModel(int s_id)
        {

            return dal.GetModel(s_id);
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
        public List<JMP.MDL.jmp_statistics> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_statistics> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_statistics> modelList = new List<JMP.MDL.jmp_statistics>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_statistics model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_statistics();
                    if (dt.Rows[n]["s_id"].ToString() != "")
                    {
                        model.s_id = int.Parse(dt.Rows[n]["s_id"].ToString());
                    }
                    model.s_brand = dt.Rows[n]["s_brand"].ToString();
                    if (dt.Rows[n]["s_app_id"].ToString() != "")
                    {
                        model.s_app_id = int.Parse(dt.Rows[n]["s_app_id"].ToString());
                    }
                    if (dt.Rows[n]["s_count"].ToString() != "")
                    {
                        model.s_count = int.Parse(dt.Rows[n]["s_count"].ToString());
                    }
                    if (dt.Rows[n]["s_time"].ToString() != "")
                    {
                        model.s_time = DateTime.Parse(dt.Rows[n]["s_time"].ToString());
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
        public List<JMP.MDL.jmp_statistics> GetListTjCount(string stime, string etime,int searchType,string searchname)
        {
            return dal.GetListTjCount(stime, etime, searchType, searchname);
        }
        /// <summary>
        /// 查询总数用户报表统计
        /// </summary>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="searchType"></param>
        /// <param name="searchname"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_statistics modelCoutn(string stime, string etime)
        {
            return dal.modelCoutn(stime,etime);
        }
    }
}