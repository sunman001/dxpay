using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
using JMP.Model.Query.WorkOrder;

namespace JMP.DAL
{
    //jmp_scheduling
    public partial class jmp_scheduling
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_scheduling");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_scheduling model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_scheduling(");
            strSql.Append("watchid,watchstartdate,createdon,createdbyid,watchenddate,Type");
            strSql.Append(") values (");
            strSql.Append("@watchid,@watchstartdate,@createdon,@createdbyid,@watchenddate,@Type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@watchid", SqlDbType.Int,4) ,
                        new SqlParameter("@watchstartdate", SqlDbType.DateTime) ,
                        new SqlParameter("@createdon", SqlDbType.DateTime) ,
                        new SqlParameter("@createdbyid", SqlDbType.Int,4) ,
                        new SqlParameter("@watchenddate", SqlDbType.DateTime),
                        new SqlParameter("@Type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.watchid;
            parameters[1].Value = model.watchstartdate;
            parameters[2].Value = model.createdon;
            parameters[3].Value = model.createdbyid;
            parameters[4].Value = model.watchenddate;
            parameters[5].Value = model.Type;
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
        public bool Update(JMP.MDL.jmp_scheduling model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_scheduling set ");

            strSql.Append(" watchid = @watchid , ");
            strSql.Append(" watchstartdate = @watchstartdate , ");
            strSql.Append(" createdon = @createdon , ");
            strSql.Append(" createdbyid = @createdbyid , ");
            strSql.Append(" watchenddate = @watchenddate, ");
            strSql.Append(" Type = @Type");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@watchid", SqlDbType.Int,4) ,
                        new SqlParameter("@watchstartdate", SqlDbType.DateTime) ,
                        new SqlParameter("@createdon", SqlDbType.DateTime) ,
                        new SqlParameter("@createdbyid", SqlDbType.Int,4) ,
                        new SqlParameter("@watchenddate", SqlDbType.DateTime),
                         new SqlParameter("@Type", SqlDbType.Int,4) 

            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.watchid;
            parameters[2].Value = model.watchstartdate;
            parameters[3].Value = model.createdon;
            parameters[4].Value = model.createdbyid;
            parameters[5].Value = model.watchenddate;
            parameters[6].Value = model.Type;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_scheduling ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_scheduling ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_scheduling GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, watchid, watchstartdate, createdon, createdbyid, watchenddate ,Type ");
            strSql.Append("  from jmp_scheduling ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            JMP.MDL.jmp_scheduling model = new JMP.MDL.jmp_scheduling();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchid"].ToString() != "")
                {
                    model.watchid = int.Parse(ds.Tables[0].Rows[0]["watchid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchstartdate"].ToString() != "")
                {
                    model.watchstartdate = DateTime.Parse(ds.Tables[0].Rows[0]["watchstartdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdon"].ToString() != "")
                {
                    model.createdon = DateTime.Parse(ds.Tables[0].Rows[0]["createdon"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdbyid"].ToString() != "")
                {
                    model.createdbyid = int.Parse(ds.Tables[0].Rows[0]["createdbyid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchenddate"].ToString() != "")
                {
                    model.watchenddate = DateTime.Parse(ds.Tables[0].Rows[0]["watchenddate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type =int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体(根据值班日期查询)
        /// </summary>
        public JMP.MDL.jmp_scheduling GetModel(string startDate,int Type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, watchid, watchstartdate, createdon, createdbyid, watchenddate  ,Type");
            strSql.Append("  from jmp_scheduling ");
            strSql.Append(" where watchstartdate=@watchstartdate and Type=@Type");
            SqlParameter[] parameters = {
                    new SqlParameter("@watchstartdate", SqlDbType.NVarChar,50),
                    new SqlParameter("@Type", SqlDbType.Int)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = Type;
            JMP.MDL.jmp_scheduling model = new JMP.MDL.jmp_scheduling();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchid"].ToString() != "")
                {
                    model.watchid = int.Parse(ds.Tables[0].Rows[0]["watchid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchstartdate"].ToString() != "")
                {
                    model.watchstartdate = DateTime.Parse(ds.Tables[0].Rows[0]["watchstartdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdon"].ToString() != "")
                {
                    model.createdon = DateTime.Parse(ds.Tables[0].Rows[0]["createdon"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdbyid"].ToString() != "")
                {
                    model.createdbyid = int.Parse(ds.Tables[0].Rows[0]["createdbyid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchenddate"].ToString() != "")
                {
                    model.watchenddate = DateTime.Parse(ds.Tables[0].Rows[0]["watchenddate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 得到一个对象实体(根据值班日期查询)
        /// </summary>
        public JMP.MDL.jmp_scheduling GetModel(string startDate)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, watchid, watchstartdate, createdon, createdbyid, watchenddate  ,Type");
            strSql.Append("  from jmp_scheduling ");
            strSql.Append(" where watchstartdate=@watchstartdate");
            SqlParameter[] parameters = {
                    new SqlParameter("@watchstartdate", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = startDate;


            JMP.MDL.jmp_scheduling model = new JMP.MDL.jmp_scheduling();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchid"].ToString() != "")
                {
                    model.watchid = int.Parse(ds.Tables[0].Rows[0]["watchid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchstartdate"].ToString() != "")
                {
                    model.watchstartdate = DateTime.Parse(ds.Tables[0].Rows[0]["watchstartdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdon"].ToString() != "")
                {
                    model.createdon = DateTime.Parse(ds.Tables[0].Rows[0]["createdon"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdbyid"].ToString() != "")
                {
                    model.createdbyid = int.Parse(ds.Tables[0].Rows[0]["createdbyid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["watchenddate"].ToString() != "")
                {
                    model.watchenddate = DateTime.Parse(ds.Tables[0].Rows[0]["watchenddate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
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
            strSql.Append(" FROM jmp_scheduling ");
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
            strSql.Append(" FROM jmp_scheduling ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取数量列表
        /// </summary>
        /// <param name="Sname">值班人</param>
        /// <param name="startdate">值班开始时间</param>
        /// <param name="enddate">值班结束</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_scheduling> SelectList( bool isType, string Sname, int type, bool isSelect,int userId, DateTime startdate, DateTime enddate, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.id,a.watchstartdate,a.watchenddate,c.u_realname as u_realname,a.createdon,b.u_realname as createdby from jmp_scheduling as a left join jmp_locuser as b on a.createdbyid = b.u_id left join jmp_locuser as c on a.watchid = c.u_id where 1=1");

            if(isType)
            {
                sql += " and Type='" + type + "'";
            }
            if (!string.IsNullOrEmpty(Sname))
            {
                sql += " and c.u_realname like '%" + Sname + "%'";
            }
            if (startdate != null)
            {
                sql += " and a.watchstartdate>='" + startdate + "'  ";

                // sql += " and a.watchenddate>='" + startdate + "' and  a.watchenddate<='" + enddate + "' ";
            }

            if (enddate != null)
            {

                sql += " and  a.watchstartdate<='" + enddate + "' ";
            }
            if(isSelect==true)
            {
                sql += " and  a.watchid ='" + userId + "' ";
            }
            
            string Order = " order by watchstartdate asc ";
         
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_scheduling>(ds.Tables[0]);
        }


        #region 自定义方法
        /// <summary>
        /// 查询所有的当天值班人员集合
        /// </summary>
        /// <returns></returns>
        public List<WatcherQuerier> FindAllWatcherOfTheDay()
        {
            var ds = DbHelperSQL.Query(
                @"SELECT DISTINCT U.u_id AS Id, U.u_loginname AS LoginName,U.u_realname AS RealName,U.u_mobilenumber AS MobileNumber FROM jmp_scheduling AS S 
LEFT JOIN jmp_locuser AS U ON U.u_id=s.watchid
WHERE S.watchstartdate<GETDATE() AND S.watchenddate>GETDATE()");
            var lst = DbHelperSQL.ToList<WatcherQuerier>(ds.Tables[0]);
            return lst;
        }
        #endregion
    }
}

