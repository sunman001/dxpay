using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///手机型号统计
    ///</summary>
    public partial class jmp_modelnumber
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_modelnumber model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_modelnumber(");
            strSql.Append("m_sdkver,m_app_id,m_count,m_time");
            strSql.Append(") values (");
            strSql.Append("@m_sdkver,@m_app_id,@m_count,@m_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@m_sdkver", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.m_sdkver;
            parameters[1].Value = model.m_app_id;
            parameters[2].Value = model.m_count;
            parameters[3].Value = model.m_time;

            object obj = DbHelperSQLTotal.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(JMP.MDL.jmp_modelnumber model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_modelnumber set ");

            strSql.Append(" m_sdkver = @m_sdkver , ");
            strSql.Append(" m_app_id = @m_app_id , ");
            strSql.Append(" m_count = @m_count , ");
            strSql.Append(" m_time = @m_time  ");
            strSql.Append(" where m_id=@m_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@m_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_sdkver", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@m_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@m_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.m_id;
            parameters[1].Value = model.m_sdkver;
            parameters[2].Value = model.m_app_id;
            parameters[3].Value = model.m_count;
            parameters[4].Value = model.m_time;
            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_modelnumber ");
            strSql.Append(" where m_id=@m_id");
            SqlParameter[] parameters = {
					new SqlParameter("@m_id", SqlDbType.Int,4)
			};
            parameters[0].Value = m_id;


            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_modelnumber ");
            strSql.Append(" where ID in (" + m_idlist + ")  ");
            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString());
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
        public JMP.MDL.jmp_modelnumber GetModel(int m_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select m_id, m_sdkver, m_app_id, m_count, m_time  ");
            strSql.Append("  from jmp_modelnumber ");
            strSql.Append(" where m_id=@m_id");
            SqlParameter[] parameters = {
					new SqlParameter("@m_id", SqlDbType.Int,4)
			};
            parameters[0].Value = m_id;


            JMP.MDL.jmp_modelnumber model = new JMP.MDL.jmp_modelnumber();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["m_id"].ToString() != "")
                {
                    model.m_id = int.Parse(ds.Tables[0].Rows[0]["m_id"].ToString());
                }
                model.m_sdkver = ds.Tables[0].Rows[0]["m_sdkver"].ToString();
                if (ds.Tables[0].Rows[0]["m_app_id"].ToString() != "")
                {
                    model.m_app_id = int.Parse(ds.Tables[0].Rows[0]["m_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_count"].ToString() != "")
                {
                    model.m_count = int.Parse(ds.Tables[0].Rows[0]["m_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m_time"].ToString() != "")
                {
                    model.m_time = DateTime.Parse(ds.Tables[0].Rows[0]["m_time"].ToString());
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
            strSql.Append(" FROM jmp_modelnumber ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQLTotal.Query(strSql.ToString());
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
            strSql.Append(" FROM jmp_modelnumber ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public List<JMP.MDL.jmp_modelnumber> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select top 10 a.m_sdkver,sum(a.m_count)as m_count from  jmp_modelnumber  a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.m_app_id left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1    ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.m_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.m_time,120)<='" + etime + "' ";
            }
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        strSql += " and b.a_name ='" + searchname + "' ";
                        break;
                    case 2:
                        strSql += " and c.u_email='" + searchname + "' ";
                        break;
                }
            }
            strSql += " group by a.m_sdkver  order by m_count desc ";
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_modelnumber>(dt);
        }

        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_modelnumber modelTjCount(string stime, string etime)
        {
            string strSql = string.Format("select sum(m_count) as m_count from  jmp_modelnumber  where 1=1");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),m_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),m_time,120)<='" + etime + "' ";
            }
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_modelnumber>(dt);
        }

    }
}

