using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using JMP.DBA;

namespace JMP.DAL
{
   public class JmpMonitorMinuteDetails
    {

        public bool Exists(int l_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_monitor_minute_details");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.JmpMonitorMinuteDetails model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into jmp_monitor_minute_details(");
            strSql.Append("AppId,MonitorType,WhichHour,Minutes,CreatedById,CreatedByName");
            strSql.Append(") values (");
            strSql.Append("@AppId,@MonitorType,@WhichHour,@Minutes,@CreatedById,@CreatedByName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                new SqlParameter("@AppId", SqlDbType.Int,4) ,
                new SqlParameter("@MonitorType", SqlDbType.Int,4) ,
                new SqlParameter("@WhichHour", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Minutes", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@CreatedById", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@CreatedByName", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.AppId;
            parameters[1].Value = model.MonitorType;
            parameters[2].Value = model.WhichHour;
            parameters[3].Value = model.Minutes;
            parameters[4].Value = model.CreatedById;
            parameters[5].Value = model.CreatedByName;

            var obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.JmpMonitorMinuteDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_monitor_minute_details set ");

            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" MonitorType = @MonitorType , ");
            strSql.Append(" WhichHour = @WhichHour , ");
            strSql.Append(" Minutes = @Minutes , ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int,4) ,
                new SqlParameter("@AppId", SqlDbType.Int,4) ,
                new SqlParameter("@MonitorType", SqlDbType.Int,4) ,
                new SqlParameter("@WhichHour", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Minutes", SqlDbType.NVarChar,-1)
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.AppId;
            parameters[2].Value = model.MonitorType;
            parameters[3].Value = model.WhichHour;
            parameters[4].Value = model.Minutes;
            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_monitor_minute_details ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from jmp_monitor_minute_details ");
            strSql.Append(" where ID in (" + idlist + ")  ");
            var rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0;
        }

        /// <summary>
        /// 删除指定监控类型和应用下所有监控时间
        /// <param name="appId">应用ID</param>
        /// <param name="monitorType">监控类型</param>
        /// </summary>
        public bool DeleteByMonitorType(int appId,int monitorType)
        {

            var strSql = new StringBuilder();
            strSql.Append("delete from jmp_monitor_minute_details ");
            strSql.Append(" where AppId=@AppId AND MonitorType=@MonitorType");
            SqlParameter[] parameters = {
                new SqlParameter("@AppId", SqlDbType.Int,4),
                new SqlParameter("@MonitorType", SqlDbType.Int,4)
            };
            parameters[0].Value = appId;
            parameters[1].Value = monitorType;

            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.JmpMonitorMinuteDetails GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,AppId,MonitorType,WhichHour,Minutes,CreatedOn,CreatedById,CreatedByName");
            strSql.Append("  from jmp_monitor_minute_details ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            var model = new Model.JmpMonitorMinuteDetails();
            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(ds.Tables[0].Rows[0]["AppId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                if (ds.Tables[0].Rows[0]["Minutes"].ToString() != "")
                {
                    model.Minutes = int.Parse(ds.Tables[0].Rows[0]["Minutes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MonitorType"].ToString() != "")
                {
                    model.MonitorType = int.Parse(ds.Tables[0].Rows[0]["MonitorType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WhichHour"].ToString() != "")
                {
                    model.WhichHour = int.Parse(ds.Tables[0].Rows[0]["WhichHour"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                
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
            strSql.Append(" FROM jmp_monitor_minute_details ");
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
            strSql.Append(" FROM jmp_monitor_minute_details ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public DataTable GetLists(string sqls, string order, int pageIndexs, int pageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return ds.Tables[0];
        }

    }
}
