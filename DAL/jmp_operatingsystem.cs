using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///手机操作系统统计
    ///</summary>
    public partial class jmp_operatingsystem
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_operatingsystem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_operatingsystem(");
            strSql.Append("o_system,o_app_id,o_count,o_time");
            strSql.Append(") values (");
            strSql.Append("@o_system,@o_app_id,@o_count,@o_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@o_system", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@o_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@o_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@o_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.o_system;
            parameters[1].Value = model.o_app_id;
            parameters[2].Value = model.o_count;
            parameters[3].Value = model.o_time;

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
        public bool Update(JMP.MDL.jmp_operatingsystem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_operatingsystem set ");

            strSql.Append(" o_system = @o_system , ");
            strSql.Append(" o_app_id = @o_app_id , ");
            strSql.Append(" o_count = @o_count , ");
            strSql.Append(" o_time = @o_time  ");
            strSql.Append(" where o_id=@o_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@o_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@o_system", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@o_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@o_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@o_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.o_id;
            parameters[1].Value = model.o_system;
            parameters[2].Value = model.o_app_id;
            parameters[3].Value = model.o_count;
            parameters[4].Value = model.o_time;
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
        public bool Delete(int o_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_operatingsystem ");
            strSql.Append(" where o_id=@o_id");
            SqlParameter[] parameters = {
					new SqlParameter("@o_id", SqlDbType.Int,4)
			};
            parameters[0].Value = o_id;


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
        public bool DeleteList(string o_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_operatingsystem ");
            strSql.Append(" where ID in (" + o_idlist + ")  ");
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
        public JMP.MDL.jmp_operatingsystem GetModel(int o_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o_id, o_system, o_app_id, o_count, o_time  ");
            strSql.Append("  from jmp_operatingsystem ");
            strSql.Append(" where o_id=@o_id");
            SqlParameter[] parameters = {
					new SqlParameter("@o_id", SqlDbType.Int,4)
			};
            parameters[0].Value = o_id;


            JMP.MDL.jmp_operatingsystem model = new JMP.MDL.jmp_operatingsystem();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["o_id"].ToString() != "")
                {
                    model.o_id = int.Parse(ds.Tables[0].Rows[0]["o_id"].ToString());
                }
                model.o_system = ds.Tables[0].Rows[0]["o_system"].ToString();
                if (ds.Tables[0].Rows[0]["o_app_id"].ToString() != "")
                {
                    model.o_app_id = int.Parse(ds.Tables[0].Rows[0]["o_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["o_count"].ToString() != "")
                {
                    model.o_count = int.Parse(ds.Tables[0].Rows[0]["o_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["o_time"].ToString() != "")
                {
                    model.o_time = DateTime.Parse(ds.Tables[0].Rows[0]["o_time"].ToString());
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
            strSql.Append(" FROM jmp_operatingsystem ");
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
            strSql.Append(" FROM jmp_operatingsystem ");
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
        public List<JMP.MDL.jmp_operatingsystem> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select top 10 a.o_system,sum(a.o_count)as o_count from  jmp_operatingsystem a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.o_app_id left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1   ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.o_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.o_time,120)<='" + etime + "' ";
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
            strSql += " group by a.o_system order by o_count desc ";
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_operatingsystem>(dt);
        }
        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_operatingsystem ModelTjCount(string stime, string etime)
        {
            string strSql = string.Format(" select sum(o_count)as o_count  from  jmp_operatingsystem  where 1=1  ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),o_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),o_time,120)<='" + etime + "' ";
            }
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_operatingsystem>(dt);
        }
    }
}

