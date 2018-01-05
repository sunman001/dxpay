using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///应用监控表
    ///</summary>
    public partial class appmonitor
    {

        public bool Exists(int a_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from appmonitor");
            strSql.Append(" where ");
            strSql.Append(" a_id = @a_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.appmonitor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into appmonitor(");
            strSql.Append("a_appid,a_request,a_minute,a_state,a_datetime,a_time_range,a_type");
            strSql.Append(") values (");
            strSql.Append("@a_appid,@a_request,@a_minute,@a_state,@a_datetime,@a_time_range,@a_type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@a_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_request", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_minute", SqlDbType.Int,4) ,
                        new SqlParameter("@a_state", SqlDbType.Int,4) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime),
                       new SqlParameter("@a_time_range", SqlDbType.NVarChar,-1),
                new SqlParameter("@a_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.a_appid;
            parameters[1].Value = model.a_request;
            parameters[2].Value = model.a_minute;
            parameters[3].Value = model.a_state;
            parameters[4].Value = model.a_datetime;
            parameters[5].Value = model.a_time_range;
            parameters[6].Value = model.a_type;
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
        public bool Update(JMP.MDL.appmonitor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update appmonitor set ");

            strSql.Append(" a_appid = @a_appid , ");
            strSql.Append(" a_request = @a_request , ");
            strSql.Append(" a_minute = @a_minute , ");
            strSql.Append(" a_state = @a_state , ");
            strSql.Append(" a_datetime = @a_datetime , ");
            strSql.Append(" a_time_range = @a_time_range,");
            strSql.Append(" a_type = @a_type");
            strSql.Append(" where a_id=@a_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@a_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_request", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_minute", SqlDbType.Int,4) ,
                        new SqlParameter("@a_state", SqlDbType.Int,4) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime),
                         new SqlParameter("@a_time_range", SqlDbType.NVarChar,-1),
                new SqlParameter("@a_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.a_id;
            parameters[1].Value = model.a_appid;
            parameters[2].Value = model.a_request;
            parameters[3].Value = model.a_minute;
            parameters[4].Value = model.a_state;
            parameters[5].Value = model.a_datetime;
            parameters[6].Value = model.a_time_range;
            parameters[7].Value = model.a_type;
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
        public bool Delete(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from appmonitor ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


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
        public bool DeleteList(string a_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from appmonitor ");
            strSql.Append(" where ID in (" + a_idlist + ")  ");
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
        public JMP.MDL.appmonitor GetModel(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a_id, a_appid, a_request, a_minute, a_state, a_datetime,a_type,a_time_range  ");
            strSql.Append("  from appmonitor ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


            JMP.MDL.appmonitor model = new JMP.MDL.appmonitor();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["a_id"].ToString() != "")
                {
                    model.a_id = int.Parse(ds.Tables[0].Rows[0]["a_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_appid"].ToString() != "")
                {
                    model.a_appid = int.Parse(ds.Tables[0].Rows[0]["a_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_request"].ToString() != "")
                {
                    model.a_request = decimal.Parse(ds.Tables[0].Rows[0]["a_request"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_minute"].ToString() != "")
                {
                    model.a_minute = int.Parse(ds.Tables[0].Rows[0]["a_minute"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_state"].ToString() != "")
                {
                    model.a_state = int.Parse(ds.Tables[0].Rows[0]["a_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_datetime"].ToString() != "")
                {
                    model.a_datetime = DateTime.Parse(ds.Tables[0].Rows[0]["a_datetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_type"].ToString() != "")
                {
                    model.a_type = int.Parse(ds.Tables[0].Rows[0]["a_type"].ToString());
                }
                model.a_time_range = ds.Tables[0].Rows[0]["a_time_range"].ToString();
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
            strSql.Append(" FROM appmonitor ");
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
            strSql.Append(" FROM appmonitor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="c_id">应用投诉id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.appmonitor SelectId(int c_id)
        {
            string sql = string.Format(" select a.*,b.a_name from appmonitor a left join jmp_app b on a.a_appid=b.a_id where a.a_id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.appmonitor>(dt);
        }


        /// <summary>
        /// 查询应该投诉管理
        /// </summary>
        /// <param name="SelectState"></param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="aType"></param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.appmonitor> SelectList(string SelectState, string sea_name, int type, int searchDesc,int aType, int pageIndexs, int PageSize, out int pageCount)
        {
            var sql = " select a.*,b.a_name from appmonitor a left join jmp_app b on a.a_appid=b.a_id where 1=1";
            string order;
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and b.a_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and a.a_request like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and a.a_minute like  '%" + sea_name + "%' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(SelectState))
            {
                sql += " and a.a_state='" + SelectState + "' ";
            }
            if (aType > -1)
            {
                sql += " AND a.a_type="+aType;
            }
            //if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            //{
            //    sql += " and a.a_datetime>='" + stime + " 00:00:00' and a.a_datetime<='" + endtime + " 23:59:59' ";
            //}
            if (searchDesc == 1)
            {
                order = " order by a_id";
            }
            else
            {
                order = " order by a_id  desc ";
            }
            
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.appmonitor>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update appmonitor set a_state=" + state + "  ");
            strSql.Append(" where a_id in (" + u_idlist + ")  ");
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
        /// 查询指定时间内无订单的应用(关联表:appmonitor)
        /// </summary>
        /// <returns></returns>
        public DataSet GetNoOrderApp()
        {
            return DbHelperSQL.Query("PROC_GET_NO_ORDERS_APP_FROM_TIMESPAN");
        }

        /// <summary>
        /// 获得状态为可用的数据列表(a_state=1)
        /// </summary>
        public DataSet GetEnabledList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM appmonitor ");
            strSql.Append(" where a_state=1 and a_type=0");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询应用监控是否存在
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="monitorType">监控类型</param>
        /// <returns></returns>
        public bool Exists(int appId,int monitorType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from appmonitor");
            strSql.Append(" where ");
            strSql.Append("a_state=1 AND a_appid = @a_appid");
            strSql.Append(" AND a_type=@a_type");
            SqlParameter[] parameters = {
                new SqlParameter("@a_appid", SqlDbType.Int,4),
                new SqlParameter("@a_type", SqlDbType.Int,4)
            };
            parameters[0].Value = appId;
            parameters[1].Value = monitorType;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}

