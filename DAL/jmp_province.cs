using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.DAL
{
    ///<summary>
    ///省份统计
    ///</summary>
    public partial class jmp_province
    {

        public bool Exists(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_province");
            strSql.Append(" where ");
            strSql.Append(" p_id = @p_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_province model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_province(");
            strSql.Append("p_province,p_appid,p_count,p_time");
            strSql.Append(") values (");
            strSql.Append("@p_province,@p_appid,@p_count,@p_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@p_province", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@p_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@p_count", SqlDbType.Int,4) ,
                        new SqlParameter("@p_time", SqlDbType.DateTime)

            };

            parameters[0].Value = model.p_province;
            parameters[1].Value = model.p_appid;
            parameters[2].Value = model.p_count;
            parameters[3].Value = model.p_time;

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
        public bool Update(JMP.MDL.jmp_province model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_province set ");

            strSql.Append(" p_province = @p_province , ");
            strSql.Append(" p_appid = @p_appid , ");
            strSql.Append(" p_count = @p_count , ");
            strSql.Append(" p_time = @p_time  ");
            strSql.Append(" where p_id=@p_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@p_id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_province", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@p_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@p_count", SqlDbType.Int,4) ,
                        new SqlParameter("@p_time", SqlDbType.DateTime)

            };

            parameters[0].Value = model.p_id;
            parameters[1].Value = model.p_province;
            parameters[2].Value = model.p_appid;
            parameters[3].Value = model.p_count;
            parameters[4].Value = model.p_time;
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
        public bool Delete(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_province ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


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
        public bool DeleteList(string p_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_province ");
            strSql.Append(" where ID in (" + p_idlist + ")  ");
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
        public JMP.MDL.jmp_province GetModel(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id, p_province, p_appid, p_count, p_time  ");
            strSql.Append("  from jmp_province ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


            JMP.MDL.jmp_province model = new JMP.MDL.jmp_province();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
                }
                model.p_province = ds.Tables[0].Rows[0]["p_province"].ToString();
                if (ds.Tables[0].Rows[0]["p_appid"].ToString() != "")
                {
                    model.p_appid = int.Parse(ds.Tables[0].Rows[0]["p_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_count"].ToString() != "")
                {
                    model.p_count = int.Parse(ds.Tables[0].Rows[0]["p_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_time"].ToString() != "")
                {
                    model.p_time = DateTime.Parse(ds.Tables[0].Rows[0]["p_time"].ToString());
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
            strSql.Append(" FROM jmp_province ");
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
            strSql.Append(" FROM jmp_province ");
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
        public List<JMP.MDL.jmp_province> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format("  select top 10 a.p_province,sum(a.p_count) as p_count from  jmp_province a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.p_appid  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1   ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.p_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.p_time,120)<='" + etime + "' ";
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
            strSql += "group by a.p_province  order by p_count desc ";
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_province>(dt);
        }
        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public JMP.MDL.jmp_province modelTjCount(string stime, string etime)
        {
            string strSql = string.Format("  select  sum(p_count)as p_count from  jmp_province  where 1=1   ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),p_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),p_time,120)<='" + etime + "' ";
            }
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_province>(dt);
        }


    }
}
