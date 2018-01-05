using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
using JMP.Model.Query;

namespace JMP.DAL
{
    ///<summary>
    ///每日应用汇总
    ///</summary>
    public partial class jmp_appcount
    {
        DataTable dt = new DataTable();

        public bool Exists(int a_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_appcount");
            strSql.Append(" where ");
            strSql.Append(" a_id = @a_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_appcount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_appcount(");
            strSql.Append("a_successratio,a_alipay,a_wechat,a_curr,a_arpur,a_datetime,a_unionpay,a_money,a_qqwallet,a_appname,a_appid,a_uerid,a_equipment,a_count,a_success,a_notpay,a_request");
            strSql.Append(") values (");
            strSql.Append("@a_successratio,@a_alipay,@a_wechat,@a_curr,@a_arpur,@a_datetime,@a_unionpay,@a_money,@a_qqwallet,@a_appname,@a_appid,@a_uerid,@a_equipment,@a_count,@a_success,@a_notpay,@a_request");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@a_successratio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_alipay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_wechat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_curr", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_arpur", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime) ,
                        new SqlParameter("@a_unionpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_qqwallet", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_appname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_uerid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_equipment", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_count", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_success", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_notpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_request", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.a_successratio;
            parameters[1].Value = model.a_alipay;
            parameters[2].Value = model.a_wechat;
            parameters[3].Value = model.a_curr;
            parameters[4].Value = model.a_arpur;
            parameters[5].Value = model.a_datetime;
            parameters[6].Value = model.a_unionpay;
            parameters[7].Value = model.a_money;
            parameters[8].Value = model.a_qqwallet;
            parameters[9].Value = model.a_appname;
            parameters[10].Value = model.a_appid;
            parameters[11].Value = model.a_uerid;
            parameters[12].Value = model.a_equipment;
            parameters[13].Value = model.a_count;
            parameters[14].Value = model.a_success;
            parameters[15].Value = model.a_notpay;
            parameters[16].Value = model.a_request;

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
        public bool Update(JMP.MDL.jmp_appcount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_appcount set ");

            strSql.Append(" a_successratio = @a_successratio , ");
            strSql.Append(" a_alipay = @a_alipay , ");
            strSql.Append(" a_wechat = @a_wechat , ");
            strSql.Append(" a_curr = @a_curr , ");
            strSql.Append(" a_arpur = @a_arpur , ");
            strSql.Append(" a_datetime = @a_datetime , ");
            strSql.Append(" a_unionpay = @a_unionpay , ");
            strSql.Append(" a_money = @a_money , ");
            strSql.Append(" a_qqwallet = @a_qqwallet , ");
            strSql.Append(" a_appname = @a_appname , ");
            strSql.Append(" a_appid = @a_appid , ");
            strSql.Append(" a_uerid = @a_uerid , ");
            strSql.Append(" a_equipment = @a_equipment , ");
            strSql.Append(" a_count = @a_count , ");
            strSql.Append(" a_success = @a_success , ");
            strSql.Append(" a_notpay = @a_notpay , ");
            strSql.Append(" a_request = @a_request  ");
            strSql.Append(" where a_id=@a_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@a_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_successratio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_alipay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_wechat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_curr", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_arpur", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_datetime", SqlDbType.DateTime) ,
                        new SqlParameter("@a_unionpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_qqwallet", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_appname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_uerid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_equipment", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_count", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_success", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_notpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@a_request", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.a_id;
            parameters[1].Value = model.a_successratio;
            parameters[2].Value = model.a_alipay;
            parameters[3].Value = model.a_wechat;
            parameters[4].Value = model.a_curr;
            parameters[5].Value = model.a_arpur;
            parameters[6].Value = model.a_datetime;
            parameters[7].Value = model.a_unionpay;
            parameters[8].Value = model.a_money;
            parameters[9].Value = model.a_qqwallet;
            parameters[10].Value = model.a_appname;
            parameters[11].Value = model.a_appid;
            parameters[12].Value = model.a_uerid;
            parameters[13].Value = model.a_equipment;
            parameters[14].Value = model.a_count;
            parameters[15].Value = model.a_success;
            parameters[16].Value = model.a_notpay;
            parameters[17].Value = model.a_request;
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
        public bool Delete(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_appcount ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


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
        public bool DeleteList(string a_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_appcount ");
            strSql.Append(" where ID in (" + a_idlist + ")  ");
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
        public JMP.MDL.jmp_appcount GetModel(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a_id, a_successratio, a_alipay, a_wechat, a_curr, a_arpur, a_datetime, a_unionpay, a_money, a_qqwallet, a_appname, a_appid, a_uerid, a_equipment, a_count, a_success, a_notpay, a_request  ");
            strSql.Append("  from jmp_appcount ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


            JMP.MDL.jmp_appcount model = new JMP.MDL.jmp_appcount();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

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
                if (ds.Tables[0].Rows[0]["a_unionpay"].ToString() != "")
                {
                    model.a_unionpay = decimal.Parse(ds.Tables[0].Rows[0]["a_unionpay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_money"].ToString() != "")
                {
                    model.a_money = decimal.Parse(ds.Tables[0].Rows[0]["a_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_qqwallet"].ToString() != "")
                {
                    model.a_qqwallet = decimal.Parse(ds.Tables[0].Rows[0]["a_qqwallet"].ToString());
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
            strSql.Append(" FROM jmp_appcount ");
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
            strSql.Append(" FROM jmp_appcount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取当天的报表（应用和用户）
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public DataTable GetTodayList(string sql, string OrderBy, int PageIndex, int PageSize, out int Count)
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
        /// 根据sql语句查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable CountSect(string sql)
        {
            DataTable dt = new DataTable();
            dt = DbHelperSQLTotal.Query(sql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 查询订单每天走势图（设备量、成功量、请求量）
        /// </summary>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetListReportOrderSuccess(string t_time, string dept, int userid, int RoleID)
        { 
            string sql = " select  a.a_datetime,SUM( a.a_equipment)as a_equipment,SUM( a.a_success) as a_success,SUM(a.a_count) as a_count from dbo.jmp_appcount a left join " + JMP.DbName.PubDbName.dbbase + ".[dbo].[jmp_user] b on a.a_uerid=b.u_id     where a_datetime>=@kstime and a_datetime<=@endtime ";
            string kstime = t_time + " 00:00:00";
            string endtime = t_time + " 23:59:59";
            //string depts = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            SqlParameter[] par = {
                                new SqlParameter("@kstime",kstime),
                                new SqlParameter("@endtime",endtime)
                               };

            if (int.Parse(dept) == RoleID)
            {
                sql += " and b.u_merchant_id=" + userid;
            }
            sql += "group by a.a_datetime";
            return DbHelperSQLTotal.Query(sql.ToString(), par);
        }


        /// <summary>
        /// 查询前三天平均交易量
        /// </summary>
        /// <param name="kst_time">开始日期（2016-03-18）</param>
        /// <param name="jst_time">结束日期（2016-03-18）</param>
        /// <param name="userid">用户id（用于开发者平台查询使用）</param>
        /// <returns></returns>
        public DataSet GetListAverage(string kst_time, string jst_time, string dept, int userid, int RoleID)
        {
            string sql = string.Format("  select (SUM( a.a_success)/3) as a_success ,CONVERT(nvarchar(2), a.a_datetime,108) as a_datetime from  jmp_appcount a left join " + JMP.DbName.PubDbName.dbbase + ".[dbo].[jmp_user] b on a.a_uerid=b.u_id    ");
            string kstime = kst_time + " 00:00:00";
            string endtime = jst_time + " 23:59:59";
            sql += " where a_datetime>='" + kstime + "' and  a_datetime<='" + endtime + "' ";
            if (int.Parse(dept) == RoleID)
            {
                sql += " and b.u_merchant_id=" + userid;
            }
            if (userid > 0)
            {
                sql += " and a_uerid=" + userid;
            }
            sql += "  group by CONVERT(nvarchar(2),a_datetime,108) order by CONVERT(nvarchar(2),a_datetime,108) desc ";
            return DbHelperSQLTotal.Query(sql.ToString());
        }




        /// <summary>
        /// 根据商务id查询前三天平均交易量
        /// </summary>
        /// <param name="kst_time">开始日期（2016-03-18）</param>
        /// <param name="jst_time">结束日期（2016-03-18）</param>
        /// <param name="swid">商务id</param>
        /// <returns></returns>
        public DataSet GetListAverageSw(string kst_time, string jst_time, int swid)
        {
            string sql = string.Format("select (SUM(a_success)/3) as a_success ,CONVERT(nvarchar(2),a_datetime,108) as a_datetime from  jmp_appcount a  left join " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user b on a.a_uerid=b.u_id    ");
            string kstime = kst_time + " 00:00:00";
            string endtime = jst_time + " 23:59:59";
            sql += " where b.u_merchant_id=" + swid + " and  a_datetime>='" + kstime + "' and  a_datetime<='" + endtime + "' ";
            sql += "  group by CONVERT(nvarchar(2),a_datetime,108) order by CONVERT(nvarchar(2),a_datetime,108) desc ";
            return DbHelperSQLTotal.Query(sql.ToString());
        }

        /// <summary>
        /// 查询订单每天走势图（设备量、成功量、请求量）
        /// </summary>
        /// <param name="t_time">查询日期</param>
        /// <param name="u_merchant_id">商务ID</param>
        /// <returns></returns>
        public DataSet GetListReportOrderSuccessByID(string t_time, int u_merchant_id)
        {
            string sql = string.Format("select a.a_datetime  ,SUM(a. a_equipment)as a_equipment,SUM( a.a_success) as a_success,SUM( a.a_count) as a_count from dbo.jmp_appcount as a left join " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user b on a.a_uerid=b.u_id where a.a_datetime>=@kstime and a.a_datetime<=@endtime  and b.u_merchant_id=@u_merchant_id group by a.a_datetime ");
            string kstime = t_time + " 00:00:00";
            string endtime = t_time + " 23:59:59";
            SqlParameter[] par = {
                                new SqlParameter("@kstime",kstime),
                                new SqlParameter("@endtime",endtime),
                                new SqlParameter ("@u_merchant_id",u_merchant_id)
                               };
            return DbHelperSQLTotal.Query(sql.ToString(), par);

        }

        /// <summary>
        /// 根据用户查询交易数据
        /// </summary>
        /// <param name="t_time">查询时间</param>
        /// <param name="u_id">用户id</param>
        /// <returns></returns>
        public DataTable GetListDay(string t_time, int u_id)
        {
            string sql = string.Format("select a_datetime,SUM(a_curr)as a_curr,SUM(a_success) as a_success,SUM(a_count) as a_count from dbo.jmp_appcount where a_datetime>=@kstime and a_datetime<=@endtime and a_uerid=@u_id group by a_datetime ");
            string kstime = t_time + " 00:00:00";
            string endtime = t_time + " 23:59:59";
            SqlParameter[] par = {
                                new SqlParameter("@kstime",kstime),
                                new SqlParameter("@endtime",endtime),
                                new SqlParameter("@u_id",u_id)
                               };
            return DbHelperSQLTotal.Query(sql.ToString(), par).Tables[0];
        }

        /// <summary>
        /// 查询今天截止目前的交易金额合计
        /// </summary>
        /// <returns></returns>
        public string SelectSumDay(string dept, int userid)
        {
            string kstime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            string depts = System.Configuration.ConfigurationManager.AppSettings["RoleID"];

            string sqlstr = "select isnull(SUM(a_curr),0) a_curr from  dbo.jmp_appcount  a left join " + JMP.DbName.PubDbName.dbbase + ". [dbo].[jmp_user] b on a.a_uerid=b.u_id where a.a_datetime>=@kstime and a.a_datetime<=@endtime";
            dept = dept.Replace("  ", "").ToString();
            if (dept == depts)
            {
                sqlstr += " and  b.u_merchant_id=" + userid;
            }
            SqlParameter[] par = {
                                  new SqlParameter("@kstime",kstime),
                                  new SqlParameter("@endtime",endtime)
                                 };
            return DbHelperSQLTotal.GetSingle(sqlstr.ToString(), par).ToString();
        }
        /// <summary>
        /// 商务业绩
        /// </summary>
        /// <param name="merchantId"> 商务ID</param>
        /// <returns></returns>
        public string TodayResults(int merchantId, string kstime, string endtime)
        {

            string sqlstr = string.Format(" select isnull(SUM(a_curr),0) a_curr from  dbo.jmp_appcount as a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user as b on a.a_uerid=b.u_id  where b.u_merchant_id='" + merchantId + "' and a.a_datetime>=@kstime and a.a_datetime<=@endtime  ");
            SqlParameter[] par = {
                                    new SqlParameter("@kstime",kstime),
                                    new SqlParameter("@endtime",endtime)
                                 };
            return DbHelperSQLTotal.GetSingle(sqlstr.ToString(), par).ToString();
        }
        /// <summary>
        /// 查询今日成功金额
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public object dayCj(int appid)
        {
            string sql = string.Format("SELECT SUM(a_alipay)+ SUM(a_wechat)+SUM(a_qqwallet) AS moeny FROM " + JMP.DbName.PubDbName.dbtotal + ".dbo.jmp_appcount WHERE a_appid=@appid AND a_datetime>'" + System.DateTime.Now.AddDays(-1).ToShortDateString() + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@appid", SqlDbType.Int,4)
            };
            parameters[0].Value = appid;
            object obj = DbHelperSQLTotal.GetSingle(sql.ToString(), parameters);
            return obj;
        }

        /// <summary>
        /// 查询当日有订单的所有应用的支付衰减详情(与前三天数据比较)
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<AppPaySuccessAttenuation> GetTodayAppPaySuccessAttenuation(DateTime today)
        {
            var sql = string.Format(@"WITH T1 AS(
SELECT a_appid,a_appname,SUM(a_count) AS TodayTotalRequest,SUM(a_success) AS TodayPaySuccess,CAST(SUM(a_success)/SUM(a_count)*100 AS DECIMAL(8,2)) AS TodaySuccessRatio FROM jmp_appcount WHERE a_datetime >='{0} 00:00:00'
GROUP BY a_appid,a_appname ),
T2 AS( 
SELECT a_appid,SUM(a_count) AS FirstThreeDaysTotalRequest,SUM(a_success) AS FirstThreeDaysPaySuccess,CAST(SUM(a_success)/SUM(a_count)*100 AS DECIMAL(8,2)) AS FirstThreeDaysSuccessRatio FROM jmp_appreport WHERE a_datetime BETWEEN '{1} 00:00:00' AND '{2} 23:59:59'
GROUP BY a_appid )
SELECT T1.a_appid AS AppId,T1.a_appname AS AppName,T1.TodayTotalRequest,T1.TodayPaySuccess,T1.TodaySuccessRatio,T2.FirstThreeDaysTotalRequest,T2.FirstThreeDaysPaySuccess,T2.FirstThreeDaysSuccessRatio,(T1.TodaySuccessRatio-T2.FirstThreeDaysSuccessRatio) AS SuccessAttenuation  FROM T1 
LEFT JOIN T2 ON T2.a_appid = T1.a_appid ", today.ToString("yyyy-MM-dd"), today.AddDays(-3).ToString("yyyy-MM-dd"), today.ToString("yyyy-MM-dd"));
            var dt = DbHelperSQLTotal.Query(sql).Tables[0];
            var list = DbHelperSQL.ConvertToList<AppPaySuccessAttenuation>(dt);
            return list;
        }


        /// <summary>
        /// 根据开发者ID查询统计图像报表
        /// </summary>
        /// <param name="uid">开发者ID</param>
        /// <param name="startTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="startTimeAdy">三日数据开始日期</param>
        /// <param name="endTimeAdy">三日数据结束日期</param>
        /// <returns></returns>
        public DataSet DataStatisticsAdy(int uid, string startTime, string endTime, string startTimeAdy, string endTimeAdy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.[Hours],isnull(sum(a_success),0) as a_success,isnull(sum(a_curr),0) as a_curr,isnull(sum(b_success),0) as b_success from ");
            sql.Append(" (select convert(int, DATENAME(HOUR, dateadd(hour, number, CONVERT(varchar(12), getdate(), 111)))) as [Hours] from master.dbo.spt_values  where type = 'P' and number < DATENAME(HOUR, '" + endTime + "') + 1) as a ");
            sql.Append(" left join");
            sql.Append(" (");
            sql.Append(" select SUM( a.a_success) as a_success,SUM(a.a_curr) as a_curr,0 as b_success,DATENAME(HOUR,a_datetime) as [HOUR] from dbo.jmp_appcount a left join dx_base.[dbo].[jmp_user] b on a.a_uerid = b.u_id where a_datetime >= '" + startTime + "' and a_datetime <= '" + endTime + "' and b.u_id =" + uid + " group by a.a_datetime ");
            sql.Append(" union all");
            sql.Append(" select 0 as  a_success,0 as a_curr,convert(decimal(18,0),(SUM(a.a_success)/3)) as b_success ,DATENAME(HOUR,a_datetime) as [HOUR] from jmp_appcount a left join dx_base.[dbo].[jmp_user] b on a.a_uerid = b.u_id where a_datetime >= '" + startTimeAdy + "' and  a_datetime <= '" + endTimeAdy + "' and b.u_id=" + uid + " group by a.a_datetime");
            sql.Append(" ) as b");
            sql.Append(" on a.[Hours]= b.[HOUR] group by a.[Hours] order by a.[Hours] asc");

            return DbHelperSQLTotal.Query(sql.ToString());
        }

        /// <summary>
        /// 根据用户ID查询交易金额和交易笔数(开发者首页)
        /// </summary>
        /// <param name="t_time">日期</param>
        /// <param name="u_id">用户ID</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public JMP.MDL.jmp_appcount DataAppcountAdy(string t_time, int u_id, int start)
        {
            string sql = string.Format("select isnull(SUM(a_curr),0) as a_curr,FLOOR(isnull(SUM(a_success),0)) as a_success from dbo.jmp_appcount");
            sql += "  where a_uerid= " + u_id + "";

            switch (start)
            {
                case 0:
                    sql += " and a_datetime>='" + t_time + " 00:00:00' and a_datetime<='" + t_time + " 23:59:59'";
                    break;
                case 1:
                    sql += " and CONVERT(varchar(7),a_datetime,120)='" + t_time + "'";
                    break;
            }

            DataTable dt = DbHelperSQLTotal.Query(sql).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_appcount>(dt);

        }

    }
}
