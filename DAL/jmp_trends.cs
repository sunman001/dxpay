using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///流量趋势汇总
    ///</summary>
    public partial class jmp_trends
    {

        public bool Exists(int t_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_trends");
            strSql.Append(" where ");
            strSql.Append(" t_id = @t_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@t_id", SqlDbType.Int,4)
			};
            parameters[0].Value = t_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_trends model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_trends(");
            strSql.Append("t_app_id,t_newcount,t_activecount,t_time");
            strSql.Append(") values (");
            strSql.Append("@t_app_id,@t_newcount,@t_activecount,@t_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@t_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_newcount", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_activecount", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.t_app_id;
            parameters[1].Value = model.t_newcount;
            parameters[2].Value = model.t_activecount;
            parameters[3].Value = model.t_time;

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
        public bool Update(JMP.MDL.jmp_trends model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_trends set ");

            strSql.Append(" t_app_id = @t_app_id , ");
            strSql.Append(" t_newcount = @t_newcount , ");
            strSql.Append(" t_activecount = @t_activecount , ");
            strSql.Append(" t_time = @t_time  ");
            strSql.Append(" where t_id=@t_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@t_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_newcount", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_activecount", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.t_id;
            parameters[1].Value = model.t_app_id;
            parameters[2].Value = model.t_newcount;
            parameters[3].Value = model.t_activecount;
            parameters[4].Value = model.t_time;
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
        public bool Delete(int t_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_trends ");
            strSql.Append(" where t_id=@t_id");
            SqlParameter[] parameters = {
					new SqlParameter("@t_id", SqlDbType.Int,4)
			};
            parameters[0].Value = t_id;


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
        public bool DeleteList(string t_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_trends ");
            strSql.Append(" where ID in (" + t_idlist + ")  ");
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
        public JMP.MDL.jmp_trends GetModel(int t_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t_id, t_app_id, t_newcount, t_activecount, t_time  ");
            strSql.Append("  from jmp_trends ");
            strSql.Append(" where t_id=@t_id");
            SqlParameter[] parameters = {
					new SqlParameter("@t_id", SqlDbType.Int,4)
			};
            parameters[0].Value = t_id;


            JMP.MDL.jmp_trends model = new JMP.MDL.jmp_trends();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["t_id"].ToString() != "")
                {
                    model.t_id = int.Parse(ds.Tables[0].Rows[0]["t_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["t_app_id"].ToString() != "")
                {
                    model.t_app_id = int.Parse(ds.Tables[0].Rows[0]["t_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["t_newcount"].ToString() != "")
                {
                    model.t_newcount = int.Parse(ds.Tables[0].Rows[0]["t_newcount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["t_activecount"].ToString() != "")
                {
                    model.t_activecount = int.Parse(ds.Tables[0].Rows[0]["t_activecount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["t_time"].ToString() != "")
                {
                    model.t_time = DateTime.Parse(ds.Tables[0].Rows[0]["t_time"].ToString());
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
            strSql.Append("select t_app_id,t_newcount,t_activecount,t_time ");
            strSql.Append(" FROM jmp_trends ");
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
            strSql.Append(" FROM jmp_trends ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetListDataTable(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select SUM(a.t_newcount)as t_newcount,SUM(a.t_activecount) as t_activecount,a.t_time from  jmp_trends a  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.t_app_id  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id  where 1=1 ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.t_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.t_time,120)<='" + etime + "' ";
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
            strSql += "  group by a.t_time  order by a.t_time  ";
            return DbHelperSQLTotal.Query(strSql).Tables[0];
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListSet(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select SUM(a.t_newcount)as t_newcount,SUM(a.t_activecount) as t_activecount, CONVERT(varchar(10),a.t_time,120) as t_time from  jmp_trends a  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.t_app_id  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id  where 1=1 ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.t_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.t_time,120)<='" + etime + "' ";
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
            strSql += "  group by a.t_time  order by a.t_time  ";
            return DbHelperSQLTotal.Query(strSql);
        }

    }
}

