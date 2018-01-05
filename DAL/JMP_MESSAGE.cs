using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///消息表
    ///</summary>
    public partial class jmp_message
    {

        public bool Exists(int m_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_message");
            strSql.Append(" where ");
            strSql.Append(" m_id = @m_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@m_id", SqlDbType.Int,4)
			};
            parameters[0].Value = m_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_message(");
            strSql.Append("m_sender,m_receiver,m_type,m_time,m_state,m_content,m_topid");
            strSql.Append(") values (");
            strSql.Append("@m_sender,@m_receiver,@m_type,@m_time,@m_state,@m_content,@m_topid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@m_sender", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_receiver", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_type", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@m_state", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_content", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_topid", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.m_sender;
            parameters[1].Value = model.m_receiver;
            parameters[2].Value = model.m_type;
            parameters[3].Value = model.m_time;
            parameters[4].Value = model.m_state;
            parameters[5].Value = model.m_content;
            parameters[6].Value = model.m_topid;

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
        public bool Update(JMP.MDL.jmp_message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_message set ");

            strSql.Append(" m_sender = @m_sender , ");
            strSql.Append(" m_receiver = @m_receiver , ");
            strSql.Append(" m_type = @m_type , ");
            strSql.Append(" m_time = @m_time , ");
            strSql.Append(" m_state = @m_state , ");
            strSql.Append(" m_content = @m_content , ");
            strSql.Append(" m_topid = @m_topid  ");
            strSql.Append(" where m_id=@m_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@m_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_sender", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_receiver", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_type", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@m_state", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_content", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_topid", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.m_id;
            parameters[1].Value = model.m_sender;
            parameters[2].Value = model.m_receiver;
            parameters[3].Value = model.m_type;
            parameters[4].Value = model.m_time;
            parameters[5].Value = model.m_state;
            parameters[6].Value = model.m_content;
            parameters[7].Value = model.m_topid;
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
        public bool Delete(int m_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_message ");
            strSql.Append(" where m_id=@m_id");
            SqlParameter[] parameters = {
					new SqlParameter("@m_id", SqlDbType.Int,4)
			};
            parameters[0].Value = m_id;


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
        public bool DeleteList(string m_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_message ");
            strSql.Append(" where ID in (" + m_idlist + ")  ");
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
        public JMP.MDL.jmp_message GetModel(int m_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select m_id, m_sender, m_receiver, m_type, m_time, m_state, m_content, m_topid  ");
            strSql.Append("  from jmp_message ");
            strSql.Append(" where m_id=@m_id");
            SqlParameter[] parameters = {
					new SqlParameter("@m_id", SqlDbType.Int,4)
			};
            parameters[0].Value = m_id;


            JMP.MDL.jmp_message model = new JMP.MDL.jmp_message();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["m_id"].ToString() != "")
                {
                    model.m_id = int.Parse(ds.Tables[0].Rows[0]["m_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_sender"].ToString() != "")
                {
                    model.m_sender = int.Parse(ds.Tables[0].Rows[0]["m_sender"].ToString());
                }
                model.m_receiver = ds.Tables[0].Rows[0]["m_receiver"].ToString();
                if (ds.Tables[0].Rows[0]["m_type"].ToString() != "")
                {
                    model.m_type = int.Parse(ds.Tables[0].Rows[0]["m_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_time"].ToString() != "")
                {
                    model.m_time = DateTime.Parse(ds.Tables[0].Rows[0]["m_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_state"].ToString() != "")
                {
                    model.m_state = int.Parse(ds.Tables[0].Rows[0]["m_state"].ToString());
                }
                model.m_content = ds.Tables[0].Rows[0]["m_content"].ToString();
                if (ds.Tables[0].Rows[0]["m_topid"].ToString() != "")
                {
                    model.m_topid = int.Parse(ds.Tables[0].Rows[0]["m_topid"].ToString());
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
            strSql.Append("select m_id, m_sender, m_receiver, m_type, m_time, m_state, m_content, m_topid ");
            strSql.Append(" FROM jmp_message ");
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
            strSql.Append(" m_id, m_sender, m_receiver, m_type, m_time, m_state, m_content, m_topid ");
            strSql.Append(" FROM jmp_message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 查询消息信息
        /// </summary>
        /// <param name="StrSql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_message> SelectList(string StrSql,string order,int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_message>(ds.Tables[0]);
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
            strSql.Append(" update jmp_message set m_state=" + state + "  ");
            strSql.Append(" where m_id in (" + u_idlist + ")  ");
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
        /// 用户删除消息操作
        /// </summary>
        /// <param name="mid">消息id</param>
        /// <param name="m_userdelete">用户id</param>
        /// <returns></returns>
        public int UserDeleteMagess(int mid, string m_userdelete)
        {
            string sql = string.Format(" update  jmp_message set m_state=@m_userdelete where m_id=@mid ");
            SqlParameter[] par ={
                                new SqlParameter("@mid",mid),
                                new SqlParameter("@m_userdelete",m_userdelete)
                               };
            return DbHelperSQL.ExecuteSql(sql, par);
        }
        /// <summary>
        /// 用事物执行批量添加
        /// </summary>
        public int AdminAdd(StringBuilder strSql)
        {
            List<CommandInfo> cmd_list = new List<CommandInfo>();
            cmd_list.Add(new CommandInfo(strSql.ToString(), null));
            int rows = DbHelperSQL.ExecuteSqlTran(cmd_list);
            return rows;
        }
        /// <summary>
        /// 查询回复消息
        /// </summary>
        /// <param name="topid"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_message> ReplySelect(int topid) {
            string sql = string.Format(" select m_id, m_sender, m_receiver, m_type, m_time, m_state, m_content, m_topid from  jmp_message   where m_topid=@topid or m_id=@topid order by m_time ");
            SqlParameter par = new SqlParameter("@topid", topid);
            DataTable dt = DbHelperSQL.Query(sql,par).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_message>(dt);
        }

    }
}

