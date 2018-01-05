using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //用户日志表
    public partial class jmp_agentuserlog
    {

        public bool Exists(int l_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_agentuserlog");
            strSql.Append(" where ");
            strSql.Append(" l_id = @l_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@l_id", SqlDbType.Int,4)
			};
            parameters[0].Value = l_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_agentuserlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_agentuserlog(");
            strSql.Append("l_user_id,l_logtype_id,l_ip,l_location,l_info,l_sms,l_time");
            strSql.Append(") values (");
            strSql.Append("@l_user_id,@l_logtype_id,@l_ip,@l_location,@l_info,@l_sms,@l_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@l_user_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@l_logtype_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@l_ip", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_location", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_info", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_sms", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.l_user_id;
            parameters[1].Value = model.l_logtype_id;
            parameters[2].Value = model.l_ip;
            parameters[3].Value = model.l_location;
            parameters[4].Value = model.l_info;
            parameters[5].Value = model.l_sms;
            parameters[6].Value = model.l_time;

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
        public bool Update(JMP.MDL.jmp_agentuserlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_agentuserlog set ");

            strSql.Append(" l_user_id = @l_user_id , ");
            strSql.Append(" l_logtype_id = @l_logtype_id , ");
            strSql.Append(" l_ip = @l_ip , ");
            strSql.Append(" l_location = @l_location , ");
            strSql.Append(" l_info = @l_info , ");
            strSql.Append(" l_sms = @l_sms , ");
            strSql.Append(" l_time = @l_time  ");
            strSql.Append(" where l_id=@l_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@l_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@l_user_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@l_logtype_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@l_ip", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_location", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_info", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_sms", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.l_id;
            parameters[1].Value = model.l_user_id;
            parameters[2].Value = model.l_logtype_id;
            parameters[3].Value = model.l_ip;
            parameters[4].Value = model.l_location;
            parameters[5].Value = model.l_info;
            parameters[6].Value = model.l_sms;
            parameters[7].Value = model.l_time;
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
        public bool Delete(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_agentuserlog ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
					new SqlParameter("@l_id", SqlDbType.Int,4)
			};
            parameters[0].Value = l_id;


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
        public bool DeleteList(string l_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_agentuserlog ");
            strSql.Append(" where ID in (" + l_idlist + ")  ");
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
        public JMP.MDL.jmp_agentuserlog GetModel(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select l_id, l_user_id, l_logtype_id, l_ip, l_location, l_info, l_sms, l_time  ");
            strSql.Append("  from jmp_agentuserlog ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
					new SqlParameter("@l_id", SqlDbType.Int,4)
			};
            parameters[0].Value = l_id;


            JMP.MDL.jmp_agentuserlog model = new JMP.MDL.jmp_agentuserlog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["l_id"].ToString() != "")
                {
                    model.l_id = int.Parse(ds.Tables[0].Rows[0]["l_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_user_id"].ToString() != "")
                {
                    model.l_user_id = int.Parse(ds.Tables[0].Rows[0]["l_user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_logtype_id"].ToString() != "")
                {
                    model.l_logtype_id = int.Parse(ds.Tables[0].Rows[0]["l_logtype_id"].ToString());
                }
                model.l_ip = ds.Tables[0].Rows[0]["l_ip"].ToString();
                model.l_location = ds.Tables[0].Rows[0]["l_location"].ToString();
                model.l_info = ds.Tables[0].Rows[0]["l_info"].ToString();
                model.l_sms = ds.Tables[0].Rows[0]["l_sms"].ToString();
                if (ds.Tables[0].Rows[0]["l_time"].ToString() != "")
                {
                    model.l_time = DateTime.Parse(ds.Tables[0].Rows[0]["l_time"].ToString());
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
            strSql.Append(" FROM jmp_agentuserlog ");
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
            strSql.Append(" FROM jmp_agentuserlog ");
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
        /// <param name="Order">排序字段</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public DataTable GetLists(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
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
            return ds.Tables[0];
        }
        DataTable dt = new DataTable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_agentuserlog> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_agentuserlog>(ds.Tables[0]);
        }

    }
}

