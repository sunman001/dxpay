using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{
    ///<summary>
    ///省份统计
    ///</summary>
    public partial class jmp_province
    {

        private readonly JMP.DAL.jmp_province dal = new JMP.DAL.jmp_province();
        public jmp_province()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int p_id)
        {
            return dal.Exists(p_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_province model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_province model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_id)
        {

            return dal.Delete(p_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string p_idlist)
        {
            return dal.DeleteList(p_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_province GetModel(int p_id)
        {

            return dal.GetModel(p_id);
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
        public List<JMP.MDL.jmp_province> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_province> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_province> modelList = new List<JMP.MDL.jmp_province>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_province model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_province();
                    if (dt.Rows[n]["p_id"].ToString() != "")
                    {
                        model.p_id = int.Parse(dt.Rows[n]["p_id"].ToString());
                    }
                    model.p_province = dt.Rows[n]["p_province"].ToString();
                    if (dt.Rows[n]["p_appid"].ToString() != "")
                    {
                        model.p_appid = int.Parse(dt.Rows[n]["p_appid"].ToString());
                    }
                    if (dt.Rows[n]["p_count"].ToString() != "")
                    {
                        model.p_count = int.Parse(dt.Rows[n]["p_count"].ToString());
                    }
                    if (dt.Rows[n]["p_time"].ToString() != "")
                    {
                        model.p_time = DateTime.Parse(dt.Rows[n]["p_time"].ToString());
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
        /// 获得数据列表用于图标统计
        /// </summary>
        public List<JMP.MDL.jmp_province> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            return dal.GetListTjCount(stime, etime, searchType, searchname);
        }
        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_province modelTjCount(string stime, string etime)
        {
            return dal.modelTjCount(stime, etime);
        }
        #endregion

    }
}
