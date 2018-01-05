using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///商户表
    ///</summary>
    public partial class jmp_merchant
    {

        public bool Exists(int m_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_merchant");
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
        public int Add(JMP.MDL.jmp_merchant model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_merchant(");
            strSql.Append("m_loginname,m_pwd,m_realname,m_count,m_state");
            strSql.Append(") values (");
            strSql.Append("@m_loginname,@m_pwd,@m_realname,@m_count,@m_state");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@m_loginname", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_pwd", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_realname", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_state", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.m_loginname;
            parameters[1].Value = model.m_pwd;
            parameters[2].Value = model.m_realname;
            parameters[3].Value = model.m_count;
            parameters[4].Value = model.m_state;

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
        public bool Update(JMP.MDL.jmp_merchant model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_merchant set ");

            strSql.Append(" m_loginname = @m_loginname , ");
            strSql.Append(" m_pwd = @m_pwd , ");
            strSql.Append(" m_realname = @m_realname , ");
            strSql.Append(" m_count = @m_count , ");
            strSql.Append(" m_state = @m_state  ");
            strSql.Append(" where m_id=@m_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@m_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_loginname", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_pwd", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_realname", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_state", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.m_id;
            parameters[1].Value = model.m_loginname;
            parameters[2].Value = model.m_pwd;
            parameters[3].Value = model.m_realname;
            parameters[4].Value = model.m_count;
            parameters[5].Value = model.m_state;
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
            strSql.Append("delete from jmp_merchant ");
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
            strSql.Append("delete from jmp_merchant ");
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
        public JMP.MDL.jmp_merchant GetModel(int m_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select m_id, m_loginname, m_pwd, m_realname, m_count, m_state  ");
            strSql.Append("  from jmp_merchant ");
            strSql.Append(" where m_id=@m_id");
            SqlParameter[] parameters = {
					new SqlParameter("@m_id", SqlDbType.Int,4)
			};
            parameters[0].Value = m_id;


            JMP.MDL.jmp_merchant model = new JMP.MDL.jmp_merchant();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["m_id"].ToString() != "")
                {
                    model.m_id = int.Parse(ds.Tables[0].Rows[0]["m_id"].ToString());
                }
                model.m_loginname = ds.Tables[0].Rows[0]["m_loginname"].ToString();
                model.m_pwd = ds.Tables[0].Rows[0]["m_pwd"].ToString();
                model.m_realname = ds.Tables[0].Rows[0]["m_realname"].ToString();
                if (ds.Tables[0].Rows[0]["m_count"].ToString() != "")
                {
                    model.m_count = int.Parse(ds.Tables[0].Rows[0]["m_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_state"].ToString() != "")
                {
                    model.m_state = int.Parse(ds.Tables[0].Rows[0]["m_state"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_merchant GetModel(string m_loginname)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select m_id, m_loginname, m_pwd, m_realname, m_count, m_state  ");
            strSql.Append("  from jmp_merchant ");
            strSql.Append(" where m_loginname=@m_loginname");
            SqlParameter[] parameters = {
					new SqlParameter("@m_loginname", SqlDbType.NVarChar,-1)
			};
            parameters[0].Value = m_loginname;


            JMP.MDL.jmp_merchant model = new JMP.MDL.jmp_merchant();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["m_id"].ToString() != "")
                {
                    model.m_id = int.Parse(ds.Tables[0].Rows[0]["m_id"].ToString());
                }
                model.m_loginname = ds.Tables[0].Rows[0]["m_loginname"].ToString();
                model.m_pwd = ds.Tables[0].Rows[0]["m_pwd"].ToString();
                model.m_realname = ds.Tables[0].Rows[0]["m_realname"].ToString();
                if (ds.Tables[0].Rows[0]["m_count"].ToString() != "")
                {
                    model.m_count = int.Parse(ds.Tables[0].Rows[0]["m_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_state"].ToString() != "")
                {
                    model.m_state = int.Parse(ds.Tables[0].Rows[0]["m_state"].ToString());
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
            strSql.Append(" FROM jmp_merchant ");
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
            strSql.Append(" FROM jmp_merchant ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdateState(int state, string ids)
        {
            var strSql = string.Format("UPDATE jmp_merchant SET m_state={0} WHERE m_id IN ({1})", state, ids);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }
    }
}

