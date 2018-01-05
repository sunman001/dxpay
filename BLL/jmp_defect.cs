using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    ///<summary>
    ///缺失用户报表统计
    ///</summary>
    public partial class jmp_defect
    {

        private readonly JMP.DAL.jmp_defect dal = new JMP.DAL.jmp_defect();
        public jmp_defect()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int d_id)
        {
            return dal.Exists(d_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_defect model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_defect model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int d_id)
        {

            return dal.Delete(d_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string d_idlist)
        {
            return dal.DeleteList(d_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_defect GetModel(int d_id)
        {

            return dal.GetModel(d_id);
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
        public List<JMP.MDL.jmp_defect> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_defect> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_defect> modelList = new List<JMP.MDL.jmp_defect>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_defect model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_defect();
                    if (dt.Rows[n]["d_id"].ToString() != "")
                    {
                        model.d_id = int.Parse(dt.Rows[n]["d_id"].ToString());
                    }
                    if (dt.Rows[n]["d_type"].ToString() != "")
                    {
                        model.d_type = int.Parse(dt.Rows[n]["d_type"].ToString());
                    }
                    if (dt.Rows[n]["d_aapid"].ToString() != "")
                    {
                        model.d_aapid = int.Parse(dt.Rows[n]["d_aapid"].ToString());
                    }
                    if (dt.Rows[n]["d_losscount"].ToString() != "")
                    {
                        model.d_losscount = int.Parse(dt.Rows[n]["d_losscount"].ToString());
                    }
                    if (dt.Rows[n]["d_lossproportion"].ToString() != "")
                    {
                        model.d_lossproportion = decimal.Parse(dt.Rows[n]["d_lossproportion"].ToString());
                    }
                    if (dt.Rows[n]["d_usercount"].ToString() != "")
                    {
                        model.d_usercount = int.Parse(dt.Rows[n]["d_usercount"].ToString());
                    }
                    if (dt.Rows[n]["d_datatype"].ToString() != "")
                    {
                        model.d_datatype = int.Parse(dt.Rows[n]["d_datatype"].ToString());
                    }
                    if (dt.Rows[n]["d_time"].ToString() != "")
                    {
                        model.d_time = DateTime.Parse(dt.Rows[n]["d_time"].ToString());
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
        /// 获取汇总信息
        /// </summary>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结算时间</param>
        /// <param name="searchType">查询条件</param>
        /// <param name="searchname">查询内容</param>
        /// <returns></returns>
        public DataTable selectDataTable(string stime, string etime, int searchType, string searchname,int stype,int sedatatype)
        {
            return dal.selectDataTable(stime, etime, searchType, searchname, stype, sedatatype);
        }
    }
}