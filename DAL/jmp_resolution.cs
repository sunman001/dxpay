using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///手机分辨率统计
    ///</summary>
    public partial class jmp_resolution
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_resolution model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_resolution(");
            strSql.Append("r_screen,r_app_id,r_count,r_time");
            strSql.Append(") values (");
            strSql.Append("@r_screen,@r_app_id,@r_count,@r_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@r_screen", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.r_screen;
            parameters[1].Value = model.r_app_id;
            parameters[2].Value = model.r_count;
            parameters[3].Value = model.r_time;

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
        public bool Update(JMP.MDL.jmp_resolution model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_resolution set ");

            strSql.Append(" r_screen = @r_screen , ");
            strSql.Append(" r_app_id = @r_app_id , ");
            strSql.Append(" r_count = @r_count , ");
            strSql.Append(" r_time = @r_time  ");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@r_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_screen", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_screen;
            parameters[2].Value = model.r_app_id;
            parameters[3].Value = model.r_count;
            parameters[4].Value = model.r_time;
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
        public bool Delete(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_resolution ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


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
        public bool DeleteList(string r_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_resolution ");
            strSql.Append(" where ID in (" + r_idlist + ")  ");
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
        public JMP.MDL.jmp_resolution GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id, r_screen, r_app_id, r_count, r_time  ");
            strSql.Append("  from jmp_resolution ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


            JMP.MDL.jmp_resolution model = new JMP.MDL.jmp_resolution();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                model.r_screen = ds.Tables[0].Rows[0]["r_screen"].ToString();
                if (ds.Tables[0].Rows[0]["r_app_id"].ToString() != "")
                {
                    model.r_app_id = int.Parse(ds.Tables[0].Rows[0]["r_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_count"].ToString() != "")
                {
                    model.r_count = int.Parse(ds.Tables[0].Rows[0]["r_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_time"].ToString() != "")
                {
                    model.r_time = DateTime.Parse(ds.Tables[0].Rows[0]["r_time"].ToString());
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
            strSql.Append(" FROM jmp_resolution ");
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
            strSql.Append(" FROM jmp_resolution ");
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
        public List<JMP.MDL.jmp_resolution> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select top 10 a.r_screen,sum(a.r_count)as r_count from  jmp_resolution a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.r_app_id left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1   ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.r_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.r_time,120)<='" + etime + "' ";
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
            strSql += " group by a.r_screen  order by r_count desc ";
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_resolution>(dt);
        }

         /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_resolution modelTjCount(string stime, string etime)
        {
            string strSql = string.Format("select sum(r_count) as r_count from  jmp_resolution  where 1=1 ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),r_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),r_time,120)<='" + etime + "' ";
            }
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_resolution>(dt);
        }

    }
}

