using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///手机网络统计
    ///</summary>
    public partial class jmp_network
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_network model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_network(");
            strSql.Append("n_network,n_app_id,n_count,n_time");
            strSql.Append(") values (");
            strSql.Append("@n_network,@n_app_id,@n_count,@n_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@n_network", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@n_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.n_network;
            parameters[1].Value = model.n_app_id;
            parameters[2].Value = model.n_count;
            parameters[3].Value = model.n_time;

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
        public bool Update(JMP.MDL.jmp_network model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_network set ");

            strSql.Append(" n_network = @n_network , ");
            strSql.Append(" n_app_id = @n_app_id , ");
            strSql.Append(" n_count = @n_count , ");
            strSql.Append(" n_time = @n_time  ");
            strSql.Append(" where n_id=@n_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@n_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_network", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@n_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@n_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.n_id;
            parameters[1].Value = model.n_network;
            parameters[2].Value = model.n_app_id;
            parameters[3].Value = model.n_count;
            parameters[4].Value = model.n_time;
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
        public bool Delete(int n_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_network ");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)
			};
            parameters[0].Value = n_id;


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
        public bool DeleteList(string n_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_network ");
            strSql.Append(" where ID in (" + n_idlist + ")  ");
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
        public JMP.MDL.jmp_network GetModel(int n_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select n_id, n_network, n_app_id, n_count, n_time  ");
            strSql.Append("  from jmp_network ");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
					new SqlParameter("@n_id", SqlDbType.Int,4)
			};
            parameters[0].Value = n_id;


            JMP.MDL.jmp_network model = new JMP.MDL.jmp_network();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["n_id"].ToString() != "")
                {
                    model.n_id = int.Parse(ds.Tables[0].Rows[0]["n_id"].ToString());
                }
                model.n_network = ds.Tables[0].Rows[0]["n_network"].ToString();
                if (ds.Tables[0].Rows[0]["n_app_id"].ToString() != "")
                {
                    model.n_app_id = int.Parse(ds.Tables[0].Rows[0]["n_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n_count"].ToString() != "")
                {
                    model.n_count = int.Parse(ds.Tables[0].Rows[0]["n_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n_time"].ToString() != "")
                {
                    model.n_time = DateTime.Parse(ds.Tables[0].Rows[0]["n_time"].ToString());
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
            strSql.Append(" FROM jmp_network ");
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
            strSql.Append(" FROM jmp_network ");
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
        public List<JMP.MDL.jmp_network> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select top 10 a.n_network,sum(a.n_count) as n_count from  jmp_network a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.n_app_id left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1   ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.n_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.n_time,120)<='" + etime + "' ";
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
            strSql += " group by a.n_network  order by n_count desc ";
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_network>(dt);
        }
        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_network modelTjCount(string stime, string etime)
        {
            string strSql = string.Format(" select sum(n_count)as n_count from  jmp_network where 1=1  ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),n_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),n_time,120)<='" + etime + "' ";
            }
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_network>(dt);
        }
    }
}

