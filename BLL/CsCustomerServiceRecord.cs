using System;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //客服响应记录表
    public partial class CsCustomerServiceRecord
    {

        private readonly JMP.DAL.CsCustomerServiceRecord dal = new JMP.DAL.CsCustomerServiceRecord();
        public CsCustomerServiceRecord()
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
        public int Add(JMP.MDL.CsCustomerServiceRecord model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.CsCustomerServiceRecord model)
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
        public JMP.MDL.CsCustomerServiceRecord GetModel(int Id)
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
        public List<JMP.MDL.CsCustomerServiceRecord> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JMP.MDL.CsCustomerServiceRecord> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.CsCustomerServiceRecord> modelList = new List<JMP.MDL.CsCustomerServiceRecord>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.CsCustomerServiceRecord model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.CsCustomerServiceRecord();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.AskScreenshot = dt.Rows[n]["AskScreenshot"].ToString();
                    if (dt.Rows[n]["ResponseDate"].ToString() != "")
                    {
                        model.ResponseDate = DateTime.Parse(dt.Rows[n]["ResponseDate"].ToString());
                    }
                    model.ResponseScreenshot = dt.Rows[n]["ResponseScreenshot"].ToString();
                    model.HandleDetails = dt.Rows[n]["HandleDetails"].ToString();
                    model.EvidenceScreenshot = dt.Rows[n]["EvidenceScreenshot"].ToString();
                    if (dt.Rows[n]["CompletedDate"].ToString() != "")
                    {
                        model.CompletedDate = DateTime.Parse(dt.Rows[n]["CompletedDate"].ToString());
                    }
                    if (dt.Rows[n]["HandlerId"].ToString() != "")
                    {
                        model.HandlerId = int.Parse(dt.Rows[n]["HandlerId"].ToString());
                    }
                    model.HandlerName = dt.Rows[n]["HandlerName"].ToString();
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["AuditStatus"].ToString() != "")
                    {
                        if ((dt.Rows[n]["AuditStatus"].ToString() == "1") || (dt.Rows[n]["AuditStatus"].ToString().ToLower() == "true"))
                        {
                            model.AuditStatus = true;
                        }
                        else
                        {
                            model.AuditStatus = false;
                        }
                    }
                    model.No = dt.Rows[n]["No"].ToString();
                    if (dt.Rows[n]["AuditByUserId"].ToString() != "")
                    {
                        model.AuditByUserId = int.Parse(dt.Rows[n]["AuditByUserId"].ToString());
                    }
                    model.AuditByUserName = dt.Rows[n]["AuditByUserName"].ToString();
                    if (dt.Rows[n]["AuditDate"].ToString() != "")
                    {
                        model.AuditDate = DateTime.Parse(dt.Rows[n]["AuditDate"].ToString());
                    }
                    if (dt.Rows[n]["Grade"].ToString() != "")
                    {
                        model.Grade = int.Parse(dt.Rows[n]["Grade"].ToString());
                    }
                    if (dt.Rows[n]["WatchId"].ToString() != "")
                    {
                        model.WatchId = int.Parse(dt.Rows[n]["WatchId"].ToString());
                    }
                    if (dt.Rows[n]["HandelGrade"].ToString() != "")
                    {
                        model.HandelGrade = int.Parse(dt.Rows[n]["HandelGrade"].ToString());
                    }
                    if (dt.Rows[n]["ParentId"].ToString() != "")
                    {
                        model.ParentId = int.Parse(dt.Rows[n]["ParentId"].ToString());
                    }
                    if (dt.Rows[n]["NotifyWatcher"].ToString() != "")
                    {
                        if ((dt.Rows[n]["NotifyWatcher"].ToString() == "1") || (dt.Rows[n]["NotifyWatcher"].ToString().ToLower() == "true"))
                        {
                            model.NotifyWatcher = true;
                        }
                        else
                        {
                            model.NotifyWatcher = false;
                        }
                    }
                    if (dt.Rows[n]["NotifyDate"].ToString() != "")
                    {
                        model.NotifyDate = DateTime.Parse(dt.Rows[n]["NotifyDate"].ToString());
                    }
                    if (dt.Rows[n]["MainCategory"].ToString() != "")
                    {
                        model.MainCategory = int.Parse(dt.Rows[n]["MainCategory"].ToString());
                    }
                    if (dt.Rows[n]["SubCategory"].ToString() != "")
                    {
                        model.SubCategory = int.Parse(dt.Rows[n]["SubCategory"].ToString());
                    }
                    if (dt.Rows[n]["CreatedOn"].ToString() != "")
                    {
                        model.CreatedOn = DateTime.Parse(dt.Rows[n]["CreatedOn"].ToString());
                    }
                    if (dt.Rows[n]["CreatedByUserId"].ToString() != "")
                    {
                        model.CreatedByUserId = int.Parse(dt.Rows[n]["CreatedByUserId"].ToString());
                    }
                    if (dt.Rows[n]["DeveloperId"].ToString() != "")
                    {
                        model.DeveloperId = int.Parse(dt.Rows[n]["DeveloperId"].ToString());
                    }
                    model.DeveloperEmail = dt.Rows[n]["DeveloperEmail"].ToString();
                    if (dt.Rows[n]["AskDate"].ToString() != "")
                    {
                        model.AskDate = DateTime.Parse(dt.Rows[n]["AskDate"].ToString());
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
        /// 查询响应记录
        /// </summary>
        /// <param name="searchType">查询的类型</param>
        /// <param name="s_key">关键字</param>
        /// <param name="Status">处理状态</param>
        /// <param name="Grade">审核评级</param>
        /// <param name="AuditStatus">主管审核状态</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="watchId">值班人ID</param>
        /// <returns></returns>
        public List<MDL.CsCustomerServiceRecord> CsCustomerRecordList(int searchType, string s_key, int Status, int Grade, int AuditStatus, string fromDate, string toDate, int pageIndexs, int PageSize, out int pageCount, int watchId = 0)
        {
            return dal.CsCustomerRecordList(searchType, s_key, Status, Grade, AuditStatus,fromDate,toDate, pageIndexs, PageSize, out pageCount,watchId);
        }

        public List<MDL.CsCustomerServiceRecordReprot> CsCustomerRecordReprotList(string Order, string sql, int pageIndexs, int pageSize, out int pageCount)
        {
            return dal.CsCustomerRecordReprotList(sql, Order, pageIndexs, pageSize, out pageCount);

        }
        public List<MDL.CsCustomerServiceSroceReprot> CsCustomerServiceReprotList(string Order, string sql, int pageIndexs, int pageSize, out int pageCount)
        {
            return dal.CsCustomerServiceReprotList(sql, Order, pageIndexs, pageSize, out pageCount);

        }


        public MDL.CsCustomerServiceRecord FindSingleByNo(string no)
        {
            return dal.FindSingleByNo(no);
        }
    }
}