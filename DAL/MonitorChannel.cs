using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///通道监控表
    ///</summary>
    public class MonitorChannel
    {

        public bool Exists(int a_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorChannel");
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
        public int Add(Model.MonitorChannel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MonitorChannel(");
            strSql.Append("ChannelId,Threshold,a_minute,a_state,a_datetime,a_time_range,a_type");
            strSql.Append(") values (");
            strSql.Append("@ChannelId,@Threshold,@a_minute,@a_state,@a_datetime,@a_time_range,@a_type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@Threshold", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_minute", SqlDbType.Int,4) ,
                        new SqlParameter("@a_state", SqlDbType.Int,4) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime),
                       new SqlParameter("@a_time_range", SqlDbType.NVarChar,-1),
                new SqlParameter("@a_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.ChannelId;
            parameters[1].Value = model.Threshold;
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
        public bool Update(Model.MonitorChannel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MonitorChannel set ");

            strSql.Append(" ChannelId = @ChannelId , ");
            strSql.Append(" Threshold = @Threshold , ");
            strSql.Append(" a_minute = @a_minute , ");
            strSql.Append(" a_state = @a_state , ");
            strSql.Append(" a_datetime = @a_datetime , ");
            strSql.Append(" a_time_range = @a_time_range,");
            strSql.Append(" a_type = @a_type");
            strSql.Append(" where a_id=@a_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@a_id", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@Threshold", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_minute", SqlDbType.Int,4) ,
                        new SqlParameter("@a_state", SqlDbType.Int,4) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime),
                         new SqlParameter("@a_time_range", SqlDbType.NVarChar,-1),
                new SqlParameter("@a_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.a_id;
            parameters[1].Value = model.ChannelId;
            parameters[2].Value = model.Threshold;
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
            strSql.Append("delete from MonitorChannel ");
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
            strSql.Append("delete from MonitorChannel ");
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
        public Model.MonitorChannel GetModel(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a_id, ChannelId, Threshold, a_minute, a_state, a_datetime,a_type  ");
            strSql.Append("  from MonitorChannel ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


            Model.MonitorChannel model = new Model.MonitorChannel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["a_id"].ToString() != "")
                {
                    model.a_id = int.Parse(ds.Tables[0].Rows[0]["a_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Threshold"].ToString() != "")
                {
                    model.Threshold = decimal.Parse(ds.Tables[0].Rows[0]["Threshold"].ToString());
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
            strSql.Append(" FROM MonitorChannel ");
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
            strSql.Append(" FROM MonitorChannel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据通道id查询信息
        /// </summary>
        /// <param name="c_id">通道投诉id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public Model.MonitorChannel SelectId(int c_id)
        {
            string sql = string.Format(" select a.*,b.a_name from MonitorChannel a left join jmp_app b on a.ChannelId=b.a_id where a.a_id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<Model.MonitorChannel>(dt);
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
        public List<Model.MonitorChannel> SelectList(string SelectState, string sea_name, int type, int searchDesc, int aType, int pageIndexs, int PageSize, out int pageCount)
        {
            var sql = " select a.*,b.[l_corporatename] as a_name from MonitorChannel a left join [dbo].[jmp_interface] AS b on a.ChannelId=b.[l_id]";
            string order;
            var whereList=new List<string>();
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        whereList.Add("b.l_corporatename like '%" + sea_name + "%'");
                        break;
                }
            }
            if (!string.IsNullOrEmpty(SelectState))
            {
                whereList.Add("a.a_state='" + SelectState + "'");
            }
            if (aType > -1)
            {
                whereList.Add("a.a_type=" + aType);
            }
            if (whereList.Count > 0)
            {
                sql = string.Format("{0} WHERE {1}", sql, string.Join(" AND ", whereList));
            }
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
            return DbHelperSQL.ToList<Model.MonitorChannel>(ds.Tables[0]);
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
            strSql.Append(" update MonitorChannel set a_state=" + state + "  ");
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
        /// 查询指定时间内无订单的通道(关联表:MonitorChannel)
        /// </summary>
        /// <returns></returns>
        public DataSet GetNoOrderApp()
        {
            return DbHelperSQL.Query("PROC_GET_NO_ORDERS_APP_FROM_TIMESPAN");
        }

        /// <summary>
        /// 查询通道监控是否存在
        /// </summary>
        /// <param name="appId">通道ID</param>
        /// <param name="monitorType">监控类型</param>
        /// <returns></returns>
        public bool Exists(int appId, int monitorType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorChannel");
            strSql.Append(" where ");
            strSql.Append(" ChannelId = @ChannelId");
            strSql.Append(" AND a_type=@a_type");
            SqlParameter[] parameters = {
                new SqlParameter("@ChannelId", SqlDbType.Int,4),
                new SqlParameter("@a_type", SqlDbType.Int,4)
            };
            parameters[0].Value = appId;
            parameters[1].Value = monitorType;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public Model.MonitorChannel GetModelByTD(int appId, int monitorType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a_id, ChannelId, Threshold, a_minute, a_state, a_datetime,a_type  ");
            strSql.Append("  from MonitorChannel ");
            strSql.Append(" where ");
            strSql.Append(" ChannelId = @ChannelId");
            strSql.Append(" AND a_type=@a_type");
            SqlParameter[] parameters = {
                new SqlParameter("@ChannelId", SqlDbType.Int,4),
                new SqlParameter("@a_type", SqlDbType.Int,4)
            };
            parameters[0].Value = appId;
            parameters[1].Value = monitorType;



            Model.MonitorChannel model = new Model.MonitorChannel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["a_id"].ToString() != "")
                {
                    model.a_id = int.Parse(ds.Tables[0].Rows[0]["a_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Threshold"].ToString() != "")
                {
                    model.Threshold = decimal.Parse(ds.Tables[0].Rows[0]["Threshold"].ToString());
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
                return model;
            }
            else
            {
                return null;
            }
        }


    }
}

