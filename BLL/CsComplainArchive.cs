using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //投诉每日汇总表
    public partial class CsComplainArchive
    {

        private readonly JMP.DAL.CsComplainArchive dal = new JMP.DAL.CsComplainArchive();
        public CsComplainArchive()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CsComplainArchive model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CsComplainArchive model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CsComplainArchive GetModel(int Id)
        {

            return dal.GetModel(Id);
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
        public List<JMP.MDL.CsComplainArchive> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.CsComplainArchive> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CsComplainArchive> modelList = new List<JMP.MDL.CsComplainArchive>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CsComplainArchive model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CsComplainArchive();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["AppId"].ToString() != "")
                    {
                        model.AppId = int.Parse(dt.Rows[n]["AppId"].ToString());
                    }
                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }
                    if (dt.Rows[n]["ComplainTypeId"].ToString() != "")
                    {
                        model.ComplainTypeId = int.Parse(dt.Rows[n]["ComplainTypeId"].ToString());
                    }
                    if (dt.Rows[n]["Amount"].ToString() != "")
                    {
                        model.Amount = int.Parse(dt.Rows[n]["Amount"].ToString());
                    }
                    if (dt.Rows[n]["ArchiveDay"].ToString() != "")
                    {
                        model.ArchiveDay = DateTime.Parse(dt.Rows[n]["ArchiveDay"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
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
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CsComplainArchive> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetLists(sql, Order, PageIndex, PageSize, out Count);
        }

        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectArchive(string sql)
        {
            return dal.SelectArchive(sql);
        }

        #endregion

    }
}