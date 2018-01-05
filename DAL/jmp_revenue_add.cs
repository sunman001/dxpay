using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //营收概况（新增用户）
    public partial class jmp_revenue_add
    {
        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_revenue_add");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id  ");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_revenue_add model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_revenue_add(");
            strSql.Append("r_users,r_moneys,r_orders,r_date,r_appid");
            strSql.Append(") values (");
            strSql.Append("@r_users,@r_moneys,@r_orders,@r_date,@r_appid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			        new SqlParameter("@r_users", SqlDbType.Int,4),            
                    new SqlParameter("@r_moneys", SqlDbType.Decimal,17),            
                    new SqlParameter("@r_orders", SqlDbType.Int,4),            
                    new SqlParameter("@r_date", SqlDbType.DateTime),            
                    new SqlParameter("@r_appid", SqlDbType.Int,4)
            };

            parameters[0].Value = model.r_users;
            parameters[1].Value = model.r_moneys;
            parameters[2].Value = model.r_orders;
            parameters[3].Value = model.r_date;
            parameters[4].Value = model.r_appid;

            object obj = DbHelperSQLTotal.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_revenue_add model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_revenue_add set ");

            strSql.Append("r_users=@r_users,");
            strSql.Append("r_moneys=@r_moneys,");
            strSql.Append("r_orders=@r_orders,");
            strSql.Append("r_date=@r_date,");
            strSql.Append("r_appid=@r_appid");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
			    new SqlParameter("@r_id", SqlDbType.Int,4),            
                new SqlParameter("@r_users", SqlDbType.Int,4),            
                new SqlParameter("@r_moneys", SqlDbType.Decimal,17),            
                new SqlParameter("@r_orders", SqlDbType.Int,4),            
                new SqlParameter("@r_date", SqlDbType.DateTime),
                new SqlParameter("@r_appid", SqlDbType.Int,4)
            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_users;
            parameters[2].Value = model.r_moneys;
            parameters[3].Value = model.r_orders;
            parameters[4].Value = model.r_date;
            parameters[5].Value = model.r_appid;
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
            strSql.Append("delete from jmp_revenue_add ");
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
            strSql.Append("delete from jmp_revenue_add ");
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
        public JMP.MDL.jmp_revenue_add GetModel(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id,r_users,r_moneys,r_orders,r_date,r_appid");
            strSql.Append("  from jmp_revenue_add ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            JMP.MDL.jmp_revenue_add model = new JMP.MDL.jmp_revenue_add();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_users"].ToString() != "")
                {
                    model.r_users = int.Parse(ds.Tables[0].Rows[0]["r_users"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_moneys"].ToString() != "")
                {
                    model.r_moneys = decimal.Parse(ds.Tables[0].Rows[0]["r_moneys"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_orders"].ToString() != "")
                {
                    model.r_orders = int.Parse(ds.Tables[0].Rows[0]["r_orders"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_appid"].ToString() != "")
                {
                    model.r_appid = int.Parse(ds.Tables[0].Rows[0]["r_appid"].ToString());
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
            strSql.Append("select r_id,r_users,r_moneys,r_orders,r_date,r_appid ");
            strSql.Append(" FROM jmp_revenue_add ");

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

            strSql.Append(" r_id,r_users,r_moneys,r_orders,r_date,r_appid ");
            strSql.Append(" FROM jmp_revenue_add ");
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
        /// <param name="sType">汇总方式（应用名称、开发者邮箱）</param>
        /// <param name="sKeys">汇总值</param>
        /// <returns></returns>
        public DataTable GetLists(string start, string end, string sType, string sKeys)
        {
            string DbBase = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select isnull(sum(r_users),0) r_users,isnull(sum(r_moneys),0) r_moneys,isnull(sum(r_orders),0) r_orders,r_date from jmp_revenue_add temp ");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_app] t_app on t_app.a_id=temp.r_appid");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_user] t_user on t_user.u_id=t_app.a_user_id");
            strSql.Append("where 1=1");
            strSql.AppendLine("    and convert(nvarchar(10),r_date,120)>='" + start + "'");
            strSql.AppendLine("    and convert(nvarchar(10),r_date,120)<='" + end + "'");
            if (!string.IsNullOrEmpty(sType) && !string.IsNullOrEmpty(sKeys))
            {
                switch (sType)
                {
                    case "0":
                        strSql.AppendFormat(" and t_app.a_name='{0}'", sKeys);
                        break;
                    case "1":
                        strSql.AppendFormat(" and t_user.u_email='{0}'", sKeys);
                        break;
                }
            }
            strSql.AppendLine("   group by r_date ");
            return DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="uid">用户id</param>
        /// <param name="aid">应用id</param>
        /// <returns></returns>
        public DataTable GetListsUser(string start, string end, string uid, string aid = "")
        {
            string DbBase = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select isnull(sum(r_users),0) r_users,isnull(sum(r_moneys),0) r_moneys,isnull(sum(r_orders),0) r_orders,r_date from jmp_revenue_add temp ");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_app] t_app on t_app.a_id=temp.r_appid");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_user] t_user on t_user.u_id=t_app.a_user_id");
            strSql.Append("where 1=1");
            strSql.AppendLine("    and convert(nvarchar(10),r_date,120)>='" + start + "'");
            strSql.AppendLine("    and convert(nvarchar(10),r_date,120)<='" + end + "'");
            strSql.Append("and t_user.u_id=" + uid);
            if (!string.IsNullOrEmpty(aid))
            {
                strSql.Append(aid == "0" ? "" : " and temp.r_appid=" + aid);
            }
            strSql.AppendLine("group by r_date ");
            return DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
        }

    }
}
