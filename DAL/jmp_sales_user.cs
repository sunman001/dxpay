using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //销售排行（商户）
    public partial class jmp_sales_user
    {
        public bool Exists(int r_id, int r_userid, decimal r_moneys, DateTime r_date)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_sales_user");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id and  ");
            strSql.Append(" r_userid = @r_userid and  ");
            strSql.Append(" r_moneys = @r_moneys and  ");
            strSql.Append(" r_date = @r_date  ");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4),
				new SqlParameter("@r_userid", SqlDbType.Int,4),
				new SqlParameter("@r_moneys", SqlDbType.Money,8),
				new SqlParameter("@r_date", SqlDbType.DateTime)
            };
            parameters[0].Value = r_id;
            parameters[1].Value = r_userid;
            parameters[2].Value = r_moneys;
            parameters[3].Value = r_date;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_sales_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_sales_user(");
            strSql.Append("r_moneys,r_date,r_appid");
            strSql.Append(") values (");
            strSql.Append("@r_moneys,@r_date,@r_appid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			    new SqlParameter("@r_moneys", SqlDbType.Money,8),            
                new SqlParameter("@r_date", SqlDbType.DateTime),            
                new SqlParameter("@r_appid", SqlDbType.Int,4) 
            };

            parameters[0].Value = model.r_moneys;
            parameters[1].Value = model.r_date;
            parameters[2].Value = model.r_appid;

            object obj = DbHelperSQLTotal.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_sales_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_sales_user set ");

            strSql.Append("r_appid=@r_appid,");
            strSql.Append("r_moneys=@r_moneys,");
            strSql.Append("r_date=@r_date");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
			    new SqlParameter("@r_id", SqlDbType.Int,4),            
                new SqlParameter("@r_appid", SqlDbType.Int,4),            
                new SqlParameter("@r_moneys", SqlDbType.Money,8),            
                new SqlParameter("@r_date", SqlDbType.DateTime) 
            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_appid;
            parameters[2].Value = model.r_moneys;
            parameters[3].Value = model.r_date;
            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_sales_user ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_sales_user ");
            strSql.Append(" where ID in (" + r_idlist + ")  ");
            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString());
            if (rows > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_sales_user GetModel(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id,r_appid,r_moneys,r_date");
            strSql.Append("  from jmp_sales_user ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


            JMP.MDL.jmp_sales_user model = new JMP.MDL.jmp_sales_user();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_appid"].ToString() != "")
                {
                    model.r_appid = int.Parse(ds.Tables[0].Rows[0]["r_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_moneys"].ToString() != "")
                {
                    model.r_moneys = decimal.Parse(ds.Tables[0].Rows[0]["r_moneys"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_date"].ToString() != "")
                {
                    model.r_date = DateTime.Parse(ds.Tables[0].Rows[0]["r_date"].ToString());
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
            strSql.Append("select r_id,r_appid,r_moneys,r_date ");
            strSql.Append(" FROM jmp_sales_user ");

            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append(" where " + strWhere);

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
                strSql.Append(" top " + Top.ToString());

            strSql.Append(" r_id,r_appid,r_moneys,r_date ");
            strSql.Append(" FROM jmp_sales_user ");
            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append(" where " + strWhere);

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="sType">查询字段</param>
        /// <param name="sKey">查询字段值</param>
        /// <param name="tops">前几条</param>
        /// <returns></returns>
        public DataTable GetLists(string start, string end, string sType, string sKey, int tops)
        {
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select " + (tops > 0 ? "top " + tops : "") + " b_user.u_realname as tname,sum(r_moneys) r_moneys from (");
            strSql.AppendLine("    select r_appid,sum(r_moneys) r_moneys from jmp_sales_user");
            strSql.AppendLine("    where convert(nvarchar(10),r_date,120)>='" + start + "'");
            strSql.AppendLine("    and convert(nvarchar(10),r_date,120)<='" + end + "'");
            strSql.AppendLine("    group by r_appid");
            strSql.AppendLine(") temp");
            strSql.AppendLine("left join [" + BsaeDb + "].[dbo].[jmp_app] b_app on temp.r_appid=b_app.a_id");
            strSql.AppendLine("left join [" + BsaeDb + "].[dbo].[jmp_user] b_user on b_app.a_user_id=b_user.u_id");
            if (!string.IsNullOrEmpty(sType) && !string.IsNullOrEmpty(sKey))
            {
                strSql.Append("where ");
                switch (sType)
                {
                    case "0":
                        strSql.AppendFormat("b_app.a_name='{0}'", sKey);
                        break;
                    case "1":
                        strSql.AppendFormat("b_user.u_email='{0}'", sKey);
                        break;
                }
            }
            strSql.AppendLine("group by b_user.u_realname");
            strSql.AppendLine("order by r_moneys desc");
            return DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="uid">用户id</param>
        /// <param name="aid">应用id</param>
        /// <param name="tops">前几条</param>
        /// <returns></returns>
        public DataTable GetListsUser(string start, string end, string uid, string aid, int tops)
        {
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select " + (tops > 0 ? "top " + tops : "") + " b_user.u_realname as tname,sum(temp.r_moneys) r_moneys from (");
            strSql.AppendLine("    select r_appid,sum(r_moneys) r_moneys from jmp_sales_user");
            strSql.AppendLine("    where convert(nvarchar(10),r_date,120)>='" + start + "'");
            strSql.AppendLine("    and convert(nvarchar(10),r_date,120)<='" + end + "'");
            strSql.AppendLine("    group by r_appid");
            strSql.AppendLine(") temp");
            strSql.AppendLine("left join [" + BsaeDb + "].[dbo].[jmp_app] b_app on temp.r_appid=b_app.a_id");
            strSql.AppendLine("left join [" + BsaeDb + "].[dbo].[jmp_user] b_user on b_app.a_user_id=b_user.u_id");
            strSql.AppendLine("where b_user.u_id=" + uid);
            if (!string.IsNullOrEmpty(aid))
            {
                strSql.Append(aid == "0" ? "" : " and temp.r_appid=" + aid + "\r\n");
            }
            strSql.AppendLine("group by b_user.u_realname");
            return DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
        }

    }
}

