using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///公告表
    ///</summary>
    public partial class jmp_notice
    {

        public bool Exists(int n_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_notice");
            strSql.Append(" where ");
            strSql.Append(" n_id = @n_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)
			};
            parameters[0].Value = n_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_notice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_notice(");
            strSql.Append("n_title,n_content,n_time,n_top,n_state,n_locuserid");
            strSql.Append(") values (");
            strSql.Append("@n_title,@n_content,@n_time,@n_top,@n_state,@n_locuserid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@n_title", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@n_content", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@n_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@n_top", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_state", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_locuserid", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.n_title;
            parameters[1].Value = model.n_content;
            parameters[2].Value = model.n_time;
            parameters[3].Value = model.n_top;
            parameters[4].Value = model.n_state;
            parameters[5].Value = model.n_locuserid;

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
        public bool Update(JMP.MDL.jmp_notice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_notice set ");

            strSql.Append(" n_title = @n_title , ");
            strSql.Append(" n_content = @n_content , ");
            strSql.Append(" n_time = @n_time , ");
            strSql.Append(" n_top = @n_top , ");
            strSql.Append(" n_state = @n_state , ");
            strSql.Append(" n_locuserid = @n_locuserid  ");
            strSql.Append(" where n_id=@n_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@n_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_title", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@n_content", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@n_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@n_top", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_state", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_locuserid", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.n_id;
            parameters[1].Value = model.n_title;
            parameters[2].Value = model.n_content;
            parameters[3].Value = model.n_time;
            parameters[4].Value = model.n_top;
            parameters[5].Value = model.n_state;
            parameters[6].Value = model.n_locuserid;
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
        public bool Delete(int n_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_notice ");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)
			};
            parameters[0].Value = n_id;


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
        public bool DeleteList(string n_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_notice ");
            strSql.Append(" where ID in (" + n_idlist + ")  ");
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
        public JMP.MDL.jmp_notice GetModel(int n_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select n_id, n_title, n_content, n_time, n_top, n_state, n_locuserid  ");
            strSql.Append("  from jmp_notice ");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)
			};
            parameters[0].Value = n_id;


            JMP.MDL.jmp_notice model = new JMP.MDL.jmp_notice();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["n_id"].ToString() != "")
                {
                    model.n_id = int.Parse(ds.Tables[0].Rows[0]["n_id"].ToString());
                }
                model.n_title = ds.Tables[0].Rows[0]["n_title"].ToString();
                model.n_content = ds.Tables[0].Rows[0]["n_content"].ToString();
                if (ds.Tables[0].Rows[0]["n_time"].ToString() != "")
                {
                    model.n_time = DateTime.Parse(ds.Tables[0].Rows[0]["n_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n_top"].ToString() != "")
                {
                    model.n_top = int.Parse(ds.Tables[0].Rows[0]["n_top"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n_state"].ToString() != "")
                {
                    model.n_state = int.Parse(ds.Tables[0].Rows[0]["n_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n_locuserid"].ToString() != "")
                {
                    model.n_locuserid = int.Parse(ds.Tables[0].Rows[0]["n_locuserid"].ToString());
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
            strSql.Append(" FROM jmp_notice ");
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
            strSql.Append(" FROM jmp_notice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询公告信息
        /// </summary>
        /// <param name="StrSql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_notice> SelectList(string StrSql, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", StrSql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_notice>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_notice set n_state=" + state + "  ");
            strSql.Append(" where n_id in (" + u_idlist + ")  ");
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
    }
}

