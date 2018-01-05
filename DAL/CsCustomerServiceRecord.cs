using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace JMP.DAL
{
    //客服响应记录表
    public partial class CsCustomerServiceRecord
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CsCustomerServiceRecord");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(JMP.MDL.CsCustomerServiceRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CsCustomerServiceRecord(");
            strSql.Append("AskScreenshot,ResponseDate,ResponseScreenshot,HandleDetails,EvidenceScreenshot,CompletedDate,HandlerId,HandlerName,Status,AuditStatus,No,AuditByUserId,AuditByUserName,AuditDate,Grade,WatchId,HandelGrade,ParentId,NotifyWatcher,NotifyDate,MainCategory,SubCategory,CreatedOn,CreatedByUserId,DeveloperId,DeveloperEmail,AskDate");
            strSql.Append(") values (");
            strSql.Append("@AskScreenshot,@ResponseDate,@ResponseScreenshot,@HandleDetails,@EvidenceScreenshot,@CompletedDate,@HandlerId,@HandlerName,@Status,@AuditStatus,@No,@AuditByUserId,@AuditByUserName,@AuditDate,@Grade,@WatchId,@HandelGrade,@ParentId,@NotifyWatcher,@NotifyDate,@MainCategory,@SubCategory,@CreatedOn,@CreatedByUserId,@DeveloperId,@DeveloperEmail,@AskDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@AskScreenshot", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@ResponseDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ResponseScreenshot", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@HandleDetails", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@EvidenceScreenshot", SqlDbType.NVarChar,2000) ,
                        new SqlParameter("@CompletedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@HandlerId", SqlDbType.Int,4) ,
                        new SqlParameter("@HandlerName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@AuditStatus", SqlDbType.Bit,1) ,
                        new SqlParameter("@No", SqlDbType.NVarChar,12) ,
                        new SqlParameter("@AuditByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@AuditByUserName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,
                        new SqlParameter("@Grade", SqlDbType.Int,4) ,
                        new SqlParameter("@WatchId", SqlDbType.Int,4) ,
                        new SqlParameter("@HandelGrade", SqlDbType.Int,4) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@NotifyWatcher", SqlDbType.Bit,1) ,
                        new SqlParameter("@NotifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@MainCategory", SqlDbType.Int,4) ,
                        new SqlParameter("@SubCategory", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperEmail", SqlDbType.NVarChar,80) ,
                        new SqlParameter("@AskDate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.AskScreenshot;
            parameters[1].Value = model.ResponseDate;
            parameters[2].Value = model.ResponseScreenshot;
            parameters[3].Value = model.HandleDetails;
            parameters[4].Value = model.EvidenceScreenshot;
            parameters[5].Value = model.CompletedDate;
            parameters[6].Value = model.HandlerId;
            parameters[7].Value = model.HandlerName;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.AuditStatus;
            parameters[10].Value = model.No;
            parameters[11].Value = model.AuditByUserId;
            parameters[12].Value = model.AuditByUserName;
            parameters[13].Value = model.AuditDate;
            parameters[14].Value = model.Grade;
            parameters[15].Value = model.WatchId;
            parameters[16].Value = model.HandelGrade;
            parameters[17].Value = model.ParentId;
            parameters[18].Value = model.NotifyWatcher;
            parameters[19].Value = model.NotifyDate;
            parameters[20].Value = model.MainCategory;
            parameters[21].Value = model.SubCategory;
            parameters[22].Value = model.CreatedOn;
            parameters[23].Value = model.CreatedByUserId;
            parameters[24].Value = model.DeveloperId;
            parameters[25].Value = model.DeveloperEmail;
            parameters[26].Value = model.AskDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JMP.MDL.CsCustomerServiceRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CsCustomerServiceRecord set ");

            strSql.Append(" AskScreenshot = @AskScreenshot , ");
            strSql.Append(" ResponseDate = @ResponseDate , ");
            strSql.Append(" ResponseScreenshot = @ResponseScreenshot , ");
            strSql.Append(" HandleDetails = @HandleDetails , ");
            strSql.Append(" EvidenceScreenshot = @EvidenceScreenshot , ");
            strSql.Append(" CompletedDate = @CompletedDate , ");
            strSql.Append(" HandlerId = @HandlerId , ");
            strSql.Append(" HandlerName = @HandlerName , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" AuditStatus = @AuditStatus , ");
            strSql.Append(" No = @No , ");
            strSql.Append(" AuditByUserId = @AuditByUserId , ");
            strSql.Append(" AuditByUserName = @AuditByUserName , ");
            strSql.Append(" AuditDate = @AuditDate , ");
            strSql.Append(" Grade = @Grade , ");
            strSql.Append(" WatchId = @WatchId , ");
            strSql.Append(" HandelGrade = @HandelGrade , ");
            strSql.Append(" ParentId = @ParentId , ");
            strSql.Append(" NotifyWatcher = @NotifyWatcher , ");
            strSql.Append(" NotifyDate = @NotifyDate , ");
            strSql.Append(" MainCategory = @MainCategory , ");
            strSql.Append(" SubCategory = @SubCategory , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedByUserId = @CreatedByUserId , ");
            strSql.Append(" DeveloperId = @DeveloperId , ");
            strSql.Append(" DeveloperEmail = @DeveloperEmail , ");
            strSql.Append(" AskDate = @AskDate  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@AskScreenshot", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@ResponseDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ResponseScreenshot", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@HandleDetails", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@EvidenceScreenshot", SqlDbType.NVarChar,2000) ,
                        new SqlParameter("@CompletedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@HandlerId", SqlDbType.Int,4) ,
                        new SqlParameter("@HandlerName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@AuditStatus", SqlDbType.Bit,1) ,
                        new SqlParameter("@No", SqlDbType.NVarChar,12) ,
                        new SqlParameter("@AuditByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@AuditByUserName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,
                        new SqlParameter("@Grade", SqlDbType.Int,4) ,
                        new SqlParameter("@WatchId", SqlDbType.Int,4) ,
                        new SqlParameter("@HandelGrade", SqlDbType.Int,4) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@NotifyWatcher", SqlDbType.Bit,1) ,
                        new SqlParameter("@NotifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@MainCategory", SqlDbType.Int,4) ,
                        new SqlParameter("@SubCategory", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperEmail", SqlDbType.NVarChar,80) ,
                        new SqlParameter("@AskDate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.AskScreenshot;
            parameters[2].Value = model.ResponseDate;
            parameters[3].Value = model.ResponseScreenshot;
            parameters[4].Value = model.HandleDetails;
            parameters[5].Value = model.EvidenceScreenshot;
            parameters[6].Value = model.CompletedDate;
            parameters[7].Value = model.HandlerId;
            parameters[8].Value = model.HandlerName;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.AuditStatus;
            parameters[11].Value = model.No;
            parameters[12].Value = model.AuditByUserId;
            parameters[13].Value = model.AuditByUserName;
            parameters[14].Value = model.AuditDate;
            parameters[15].Value = model.Grade;
            parameters[16].Value = model.WatchId;
            parameters[17].Value = model.HandelGrade;
            parameters[18].Value = model.ParentId;
            parameters[19].Value = model.NotifyWatcher;
            parameters[20].Value = model.NotifyDate;
            parameters[21].Value = model.MainCategory;
            parameters[22].Value = model.SubCategory;
            parameters[23].Value = model.CreatedOn;
            parameters[24].Value = model.CreatedByUserId;
            parameters[25].Value = model.DeveloperId;
            parameters[26].Value = model.DeveloperEmail;
            parameters[27].Value = model.AskDate;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CsCustomerServiceRecord ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CsCustomerServiceRecord ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CsCustomerServiceRecord GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ResponseScreenshot, HandleDetails, EvidenceScreenshot, CompletedDate, HandlerId, HandlerName, Status, AuditStatus, AuditByUserId, AuditByUserName, No, AuditDate, Grade, WatchId, HandelGrade, ParentId, NotifyWatcher, NotifyDate, MainCategory, SubCategory, CreatedOn, CreatedByUserId, AskDate, AskScreenshot, ResponseDate,u_realname, DeveloperId, DeveloperEmail  ");
            strSql.Append("  from CsCustomerServiceRecord a left join jmp_locuser b on a.WatchId=b.u_id");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CsCustomerServiceRecord model = new JMP.MDL.CsCustomerServiceRecord();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.ResponseScreenshot = ds.Tables[0].Rows[0]["ResponseScreenshot"].ToString();
                model.HandleDetails = ds.Tables[0].Rows[0]["HandleDetails"].ToString();
                model.EvidenceScreenshot = ds.Tables[0].Rows[0]["EvidenceScreenshot"].ToString();
                if (ds.Tables[0].Rows[0]["CompletedDate"].ToString() != "")
                {
                    model.CompletedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CompletedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HandlerId"].ToString() != "")
                {
                    model.HandlerId = int.Parse(ds.Tables[0].Rows[0]["HandlerId"].ToString());
                }
                model.HandlerName = ds.Tables[0].Rows[0]["HandlerName"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditStatus"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AuditStatus"].ToString() == "1") || (ds.Tables[0].Rows[0]["AuditStatus"].ToString().ToLower() == "true"))
                    {
                        model.AuditStatus = true;
                    }
                    else
                    {
                        model.AuditStatus = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AuditByUserId"].ToString() != "")
                {
                    model.AuditByUserId = int.Parse(ds.Tables[0].Rows[0]["AuditByUserId"].ToString());
                }
                model.AuditByUserName = ds.Tables[0].Rows[0]["AuditByUserName"].ToString();
                model.No = ds.Tables[0].Rows[0]["No"].ToString();
                if (ds.Tables[0].Rows[0]["AuditDate"].ToString() != "")
                {
                    model.AuditDate = DateTime.Parse(ds.Tables[0].Rows[0]["AuditDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Grade"].ToString() != "")
                {
                    model.Grade = int.Parse(ds.Tables[0].Rows[0]["Grade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WatchId"].ToString() != "")
                {
                    model.WatchId = int.Parse(ds.Tables[0].Rows[0]["WatchId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HandelGrade"].ToString() != "")
                {
                    model.HandelGrade = int.Parse(ds.Tables[0].Rows[0]["HandelGrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NotifyWatcher"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["NotifyWatcher"].ToString() == "1") || (ds.Tables[0].Rows[0]["NotifyWatcher"].ToString().ToLower() == "true"))
                    {
                        model.NotifyWatcher = true;
                    }
                    else
                    {
                        model.NotifyWatcher = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["NotifyDate"].ToString() != "")
                {
                    model.NotifyDate = DateTime.Parse(ds.Tables[0].Rows[0]["NotifyDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MainCategory"].ToString() != "")
                {
                    model.MainCategory = int.Parse(ds.Tables[0].Rows[0]["MainCategory"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubCategory"].ToString() != "")
                {
                    model.SubCategory = int.Parse(ds.Tables[0].Rows[0]["SubCategory"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedByUserId"].ToString() != "")
                {
                    model.CreatedByUserId = int.Parse(ds.Tables[0].Rows[0]["CreatedByUserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AskDate"].ToString() != "")
                {
                    model.AskDate = DateTime.Parse(ds.Tables[0].Rows[0]["AskDate"].ToString());
                }
                model.AskScreenshot = ds.Tables[0].Rows[0]["AskScreenshot"].ToString();
                if (ds.Tables[0].Rows[0]["ResponseDate"].ToString() != "")
                {
                    model.ResponseDate = DateTime.Parse(ds.Tables[0].Rows[0]["ResponseDate"].ToString());
                }
                {
                    model.DeveloperId = int.Parse(ds.Tables[0].Rows[0]["DeveloperId"].ToString());
                }
                model.DeveloperEmail = ds.Tables[0].Rows[0]["DeveloperEmail"].ToString();


                model.u_realname = ds.Tables[0].Rows[0]["u_realname"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CsCustomerServiceRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM CsCustomerServiceRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

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
            string sql = string.Format("select a.*,b.u_realname from dbo.CsCustomerServiceRecord a left join jmp_locuser b on a.WatchId=b.u_id where 1=1");

            if (searchType != -1)
            {
                if (s_key != "")
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and b.u_realname like '%" + s_key + "%'";
                            break;
                        case 2:
                            sql += " and a.HandlerName like '%" + s_key + "%'";
                            break;
                        case 3:
                            sql += " and a.AuditByUserName like '%" + s_key + "%'";
                            break;
                        case 4:
                            sql += " and a.DeveloperId = " + s_key;
                            break;
                    }
                }

            }
            if (AuditStatus != -1)
            {
                sql += " and AuditStatus=" + AuditStatus;
            }
            if (Status != -1)
            {
                sql += " and Status=" + Status;
            }
            if (Grade != -1)
            {
                sql += " and Grade=" + Grade;
            }

            if (watchId > 0)
            {
                sql += " and WatchId=" + watchId;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                sql += " and CreatedOn>='" + fromDate + "'";
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                sql += " and CreatedOn<'" + Convert.ToDateTime(toDate).AddDays(1).ToString("yyyy-MM-dd") + "'";
            }

            string Order = " order by Id desc ";

            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.CsCustomerServiceRecord>(ds.Tables[0]);
        }
        public List<MDL.CsCustomerServiceRecordReprot> CsCustomerRecordReprotList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {

            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.CsCustomerServiceRecordReprot>(ds.Tables[0]);
        }
        public List<MDL.CsCustomerServiceSroceReprot> CsCustomerServiceReprotList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {

            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.CsCustomerServiceSroceReprot>(ds.Tables[0]);
        }

        public MDL.CsCustomerServiceRecord FindSingleByNo(string no)
        {
            return DbHelperSQL.ToList<MDL.CsCustomerServiceRecord>(GetList("No='" + no + "'").Tables[0]).FirstOrDefault();
        }
    }
}

