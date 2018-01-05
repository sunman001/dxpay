using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///每日应用汇总按天
    ///</summary>
    public partial class jmp_appreport
    {

        public bool Exists(int a_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_appreport");
            strSql.Append(" where ");
            strSql.Append(" a_id = @a_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_appreport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_appreport(");
            strSql.Append("a_successratio,a_alipay,a_wechat,a_curr,a_arpur,a_datetime,a_time,a_appname,a_appid,a_uerid,a_equipment,a_count,a_success,a_notpay,a_request,a_money,a_qqwallet");
            strSql.Append(") values (");
            strSql.Append("@a_successratio,@a_alipay,@a_wechat,@a_curr,@a_arpur,@a_datetime,@a_time,@a_appname,@a_appid,@a_uerid,@a_equipment,@a_count,@a_success,@a_notpay,@a_request,@a_money,@a_qqwallet");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@a_successratio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_alipay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_wechat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_curr", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_arpur", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime) ,
                        new SqlParameter("@a_time", SqlDbType.DateTime) ,
                        new SqlParameter("@a_appname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_uerid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_equipment", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_count", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_success", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_notpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_request", SqlDbType.Decimal,9),
                        new SqlParameter("@a_money",SqlDbType.Decimal,9),
                        new SqlParameter("@a_qqwallet",SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.a_successratio;
            parameters[1].Value = model.a_alipay;
            parameters[2].Value = model.a_wechat;
            parameters[3].Value = model.a_curr;
            parameters[4].Value = model.a_arpur;
            parameters[5].Value = model.a_datetime;
            parameters[6].Value = model.a_time;
            parameters[7].Value = model.a_appname;
            parameters[8].Value = model.a_appid;
            parameters[9].Value = model.a_uerid;
            parameters[10].Value = model.a_equipment;
            parameters[11].Value = model.a_count;
            parameters[12].Value = model.a_success;
            parameters[13].Value = model.a_notpay;
            parameters[14].Value = model.a_request;
            parameters[15].Value = model.a_money;
            parameters[16].Value = model.a_qqwallet;

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
        public bool Update(JMP.MDL.jmp_appreport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_appreport set ");

            strSql.Append(" a_successratio = @a_successratio , ");
            strSql.Append(" a_alipay = @a_alipay , ");
            strSql.Append(" a_wechat = @a_wechat , ");
            strSql.Append(" a_curr = @a_curr , ");
            strSql.Append(" a_arpur = @a_arpur , ");
            strSql.Append(" a_datetime = @a_datetime , ");
            strSql.Append(" a_time = @a_time , ");
            strSql.Append(" a_appname = @a_appname , ");
            strSql.Append(" a_appid = @a_appid , ");
            strSql.Append(" a_uerid = @a_uerid , ");
            strSql.Append(" a_equipment = @a_equipment , ");
            strSql.Append(" a_count = @a_count , ");
            strSql.Append(" a_success = @a_success , ");
            strSql.Append(" a_notpay = @a_notpay , ");
            strSql.Append(" a_request = @a_request,  ");
            strSql.Append(" a_money=@a_money  ");
            strSql.Append(" a_qqwallet=@a_qqwallet  ");
            strSql.Append(" where a_id=@a_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@a_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_successratio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_alipay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_wechat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_curr", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_arpur", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime) ,
                        new SqlParameter("@a_time", SqlDbType.DateTime) ,
                        new SqlParameter("@a_appname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_uerid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_equipment", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_count", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_success", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_notpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_request", SqlDbType.Decimal,9),
                        new SqlParameter("@a_money",SqlDbType.Decimal,9),
                        new SqlParameter("@a_qqwallet",SqlDbType.Decimal,9)
            };

            parameters[0].Value = model.a_id;
            parameters[1].Value = model.a_successratio;
            parameters[2].Value = model.a_alipay;
            parameters[3].Value = model.a_wechat;
            parameters[4].Value = model.a_curr;
            parameters[5].Value = model.a_arpur;
            parameters[6].Value = model.a_datetime;
            parameters[7].Value = model.a_time;
            parameters[8].Value = model.a_appname;
            parameters[9].Value = model.a_appid;
            parameters[10].Value = model.a_uerid;
            parameters[11].Value = model.a_equipment;
            parameters[12].Value = model.a_count;
            parameters[13].Value = model.a_success;
            parameters[14].Value = model.a_notpay;
            parameters[15].Value = model.a_request;
            parameters[16].Value = model.a_money;
            parameters[17].Value = model.a_qqwallet;

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
        public bool Delete(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_appreport ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


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
        public bool DeleteList(string a_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_appreport ");
            strSql.Append(" where ID in (" + a_idlist + ")  ");
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
        public JMP.MDL.jmp_appreport GetModel(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a_id, a_successratio, a_alipay, a_wechat, a_curr, a_arpur, a_datetime, a_time, a_appname, a_appid, a_uerid, a_equipment, a_count, a_success, a_notpay, a_request,a_money,a_qqwallet  ");
            strSql.Append("  from jmp_appreport ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


            JMP.MDL.jmp_appreport model = new JMP.MDL.jmp_appreport();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["a_id"].ToString() != "")
                {
                    model.a_id = int.Parse(ds.Tables[0].Rows[0]["a_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_successratio"].ToString() != "")
                {
                    model.a_successratio = decimal.Parse(ds.Tables[0].Rows[0]["a_successratio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_alipay"].ToString() != "")
                {
                    model.a_alipay = decimal.Parse(ds.Tables[0].Rows[0]["a_alipay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_wechat"].ToString() != "")
                {
                    model.a_wechat = decimal.Parse(ds.Tables[0].Rows[0]["a_wechat"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_curr"].ToString() != "")
                {
                    model.a_curr = decimal.Parse(ds.Tables[0].Rows[0]["a_curr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_arpur"].ToString() != "")
                {
                    model.a_arpur = decimal.Parse(ds.Tables[0].Rows[0]["a_arpur"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_datetime"].ToString() != "")
                {
                    model.a_datetime = DateTime.Parse(ds.Tables[0].Rows[0]["a_datetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_time"].ToString() != "")
                {
                    model.a_time = DateTime.Parse(ds.Tables[0].Rows[0]["a_time"].ToString());
                }
                model.a_appname = ds.Tables[0].Rows[0]["a_appname"].ToString();
                if (ds.Tables[0].Rows[0]["a_appid"].ToString() != "")
                {
                    model.a_appid = int.Parse(ds.Tables[0].Rows[0]["a_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_uerid"].ToString() != "")
                {
                    model.a_uerid = int.Parse(ds.Tables[0].Rows[0]["a_uerid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_equipment"].ToString() != "")
                {
                    model.a_equipment = decimal.Parse(ds.Tables[0].Rows[0]["a_equipment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_count"].ToString() != "")
                {
                    model.a_count = decimal.Parse(ds.Tables[0].Rows[0]["a_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_success"].ToString() != "")
                {
                    model.a_success = decimal.Parse(ds.Tables[0].Rows[0]["a_success"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_notpay"].ToString() != "")
                {
                    model.a_notpay = decimal.Parse(ds.Tables[0].Rows[0]["a_notpay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_request"].ToString() != "")
                {
                    model.a_request = decimal.Parse(ds.Tables[0].Rows[0]["a_request"].ToString());
                }

                if (ds.Tables[0].Rows[0]["a_money"].ToString() != "")
                {
                    model.a_money = decimal.Parse(ds.Tables[0].Rows[0]["a_money"].ToString());
                }

                if (ds.Tables[0].Rows[0]["a_qqwallet"].ToString() != "")
                {
                    model.a_qqwallet = decimal.Parse(ds.Tables[0].Rows[0]["a_qqwallet"].ToString());
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
            strSql.Append(" FROM jmp_appreport ");
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
            strSql.Append(" FROM jmp_appreport ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public DataTable GetLists(string sql, string OrderBy, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQLTotal.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", OrderBy));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            Count = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return ds.Tables[0];
        }


        /// <summary>
        /// 获取列表(营收概况)
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="sType">汇总方式（应用名称、开发者邮箱）</param>
        /// <param name="sKeys">汇总值</param>
        /// <returns></returns>
        public DataTable GetListys(string start, string end, string sType, string sKeys)
        {
            string DbBase = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select isnull( sum(temp.a_number),0) a_number, ISNULL( SUM(temp.a_curr),0 )a_curr ,ISNULL(SUM(temp.a_success),0)a_success,ISNULL(SUM(temp.a_notpay),0)a_notpay,temp.a_time  from  jmp_appreport temp ");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_app] t_app on t_app.a_id=temp.a_appid");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_user] t_user on t_user.u_id=t_app.a_user_id");
            strSql.Append("where 1=1");
            strSql.AppendLine("    and convert(nvarchar(10), temp.a_time,120)>='" + start + "'");
            strSql.AppendLine("    and convert(nvarchar(10), temp.a_time,120)<='" + end + "'");
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
            strSql.AppendLine("   group by  temp.a_time ");
            return DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
        }



        /// <summary>
        /// 获取列表（营业概况开发者）
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
            strSql.AppendLine("select isnull( sum(temp.a_number),0) a_number, ISNULL( SUM(temp.a_curr),0 )a_curr ,ISNULL(SUM(temp.a_success),0)a_success, ISNULL(SUM(temp.a_notpay),0) a_notpay,temp.a_time  from  jmp_appreport temp ");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_app] t_app on t_app.a_id=temp.a_appid");
            strSql.AppendLine("left join [" + DbBase + "].[dbo].[jmp_user] t_user on t_user.u_id=t_app.a_user_id");
            strSql.Append("where 1=1");
            strSql.AppendLine("    and convert(nvarchar(10), temp.a_time,120)>='" + start + "'");
            strSql.AppendLine("    and convert(nvarchar(10), temp.a_time,120)<='" + end + "'");
            strSql.Append("and t_user.u_id=" + uid);
            if (!string.IsNullOrEmpty(aid))
            {
                strSql.Append(aid == "0" ? "" : " and temp.a_appid=" + aid);
            }
            strSql.AppendLine("group by temp.a_time ");
            return DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
        }

    }
}

