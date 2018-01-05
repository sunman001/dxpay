using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    /// <summary>
    /// 终端属性分析表
    /// </summary>
    public partial class jmp_terminal
    {
        DataTable dt = new DataTable();
        public bool Exists(int t_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_terminal");
            strSql.Append(" where ");
            strSql.Append(" t_id = @t_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@t_id", SqlDbType.Int,4)
            };
            parameters[0].Value = t_id;

            return DbHelperSQLDEVICE.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_terminal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_terminal(");
            strSql.Append("t_system,t_hardware,t_sdkver,t_time,t_screen,t_network,t_appid,t_key,t_mark,t_ip,t_province,t_imsi,t_nettype,t_brand");
            strSql.Append(") values (");
            strSql.Append("@t_system,@t_hardware,@t_sdkver,@t_time,@t_screen,@t_network,@t_appid,@t_key,@t_mark,@t_ip,@t_province,@t_imsi,@t_nettype,@t_brand");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@t_system", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_hardware", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_sdkver", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_time", SqlDbType.DateTime) ,
                        new SqlParameter("@t_screen", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_network", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@t_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_mark", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_ip", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_province", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_imsi", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_nettype", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_brand", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.t_system;
            parameters[1].Value = model.t_hardware;
            parameters[2].Value = model.t_sdkver;
            parameters[3].Value = model.t_time;
            parameters[4].Value = model.t_screen;
            parameters[5].Value = model.t_network;
            parameters[6].Value = model.t_appid;
            parameters[7].Value = model.t_key;
            parameters[8].Value = model.t_mark;
            parameters[9].Value = model.t_ip;
            parameters[10].Value = model.t_province;
            parameters[11].Value = model.t_imsi;
            parameters[12].Value = model.t_nettype;
            parameters[13].Value = model.t_brand;
            object obj = DbHelperSQLDEVICE.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(JMP.MDL.jmp_terminal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_terminal set ");

            strSql.Append(" t_system = @t_system , ");
            strSql.Append(" t_hardware = @t_hardware , ");
            strSql.Append(" t_sdkver = @t_sdkver , ");
            strSql.Append(" t_time = @t_time , ");
            strSql.Append(" t_screen = @t_screen , ");
            strSql.Append(" t_network = @t_network , ");
            strSql.Append(" t_appid = @t_appid , ");
            strSql.Append(" t_key = @t_key , ");
            strSql.Append(" t_mark = @t_mark , ");
            strSql.Append(" t_ip = @t_ip , ");
            strSql.Append(" t_province = @t_province , ");
            strSql.Append(" t_imsi = @t_imsi , ");
            strSql.Append(" t_nettype = @t_nettype , ");
            strSql.Append(" t_brand = @t_brand  ");
            strSql.Append(" where t_id=@t_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@t_id", SqlDbType.Int,4) ,
                        new SqlParameter("@t_system", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_hardware", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_sdkver", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_time", SqlDbType.DateTime) ,
                        new SqlParameter("@t_screen", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_network", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@t_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_mark", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_ip", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_province", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_imsi", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_nettype", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@t_brand", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.t_id;
            parameters[1].Value = model.t_system;
            parameters[2].Value = model.t_hardware;
            parameters[3].Value = model.t_sdkver;
            parameters[4].Value = model.t_time;
            parameters[5].Value = model.t_screen;
            parameters[6].Value = model.t_network;
            parameters[7].Value = model.t_appid;
            parameters[8].Value = model.t_key;
            parameters[9].Value = model.t_mark;
            parameters[10].Value = model.t_ip;
            parameters[11].Value = model.t_province;
            parameters[12].Value = model.t_imsi;
            parameters[13].Value = model.t_nettype;
            parameters[14].Value = model.t_brand;
            int rows = DbHelperSQLDEVICE.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_terminal ");
            strSql.Append(" where t_id=@t_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@t_id", SqlDbType.Int,4)
            };
            parameters[0].Value = t_id;


            int rows = DbHelperSQLDEVICE.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_terminal ");
            strSql.Append(" where ID in (" + t_idlist + ")  ");
            int rows = DbHelperSQLDEVICE.ExecuteSql(strSql.ToString());
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
        public JMP.MDL.jmp_terminal GetModel(int t_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t_id, t_system, t_hardware, t_sdkver, t_time, t_screen, t_network, t_appid, t_key, t_mark, t_ip, t_province, t_imsi, t_nettype, t_brand  ");
            strSql.Append("  from jmp_terminal ");
            strSql.Append(" where t_id=@t_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@t_id", SqlDbType.Int,4)
            };
            parameters[0].Value = t_id;


            JMP.MDL.jmp_terminal model = new JMP.MDL.jmp_terminal();
            DataSet ds = DbHelperSQLDEVICE.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["t_id"].ToString() != "")
                {
                    model.t_id = int.Parse(ds.Tables[0].Rows[0]["t_id"].ToString());
                }
                model.t_system = ds.Tables[0].Rows[0]["t_system"].ToString();
                model.t_hardware = ds.Tables[0].Rows[0]["t_hardware"].ToString();
                model.t_sdkver = ds.Tables[0].Rows[0]["t_sdkver"].ToString();
                if (ds.Tables[0].Rows[0]["t_time"].ToString() != "")
                {
                    model.t_time = DateTime.Parse(ds.Tables[0].Rows[0]["t_time"].ToString());
                }
                model.t_screen = ds.Tables[0].Rows[0]["t_screen"].ToString();
                model.t_network = ds.Tables[0].Rows[0]["t_network"].ToString();
                if (ds.Tables[0].Rows[0]["t_appid"].ToString() != "")
                {
                    model.t_appid = int.Parse(ds.Tables[0].Rows[0]["t_appid"].ToString());
                }
                model.t_key = ds.Tables[0].Rows[0]["t_key"].ToString();
                model.t_mark = ds.Tables[0].Rows[0]["t_mark"].ToString();
                model.t_ip = ds.Tables[0].Rows[0]["t_ip"].ToString();
                model.t_province = ds.Tables[0].Rows[0]["t_province"].ToString();
                model.t_imsi = ds.Tables[0].Rows[0]["t_imsi"].ToString();
                model.t_nettype = ds.Tables[0].Rows[0]["t_nettype"].ToString();
                model.t_brand = ds.Tables[0].Rows[0]["t_brand"].ToString();

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
            strSql.Append(" FROM jmp_terminal ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQLDEVICE.Query(strSql.ToString());
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
            strSql.Append(" FROM jmp_terminal ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLDEVICE.Query(strSql.ToString());
        }




        #region 报表模块--陶涛20160322
        /// <summary>
        /// 查询新增用户（每小时）
        /// </summary>
        /// <param name="t_time">查询日期（2016-03-31）</param>
        /// <returns></returns>
        public DataSet GetListReportAddUser(string t_time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select convert(nvarchar(13),t_time,120) times,count(1) counts from jmp_terminal
            where convert(nvarchar(10),t_time,120)='{0}' 
            group by convert(nvarchar(13),t_time,120)", t_time);
            return DbHelperSQLDEVICE.Query(strSql.ToString());
        }
        #endregion
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_terminal> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
            SqlConnection con = new SqlConnection(DbHelperSQLDEVICE.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQLDEVICE.ToList<JMP.MDL.jmp_terminal>(ds.Tables[0]);
        }

        /// <summary>
        /// 查询新增用户（每小时）
        /// </summary>
        /// <param name="baseDbName">设备数据库名称</param>
        /// <param name="tTime">查询日期（2016-03-31）</param>
        /// <param name="merchantId">商务ID</param>
        /// <returns></returns>
        public DataSet GetMerchantListReportAddUser(string baseDbName, string tTime, int merchantId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"SELECT  CONVERT(NVARCHAR(13),t_time,120) times,COUNT(1) counts
FROM    jmp_terminal AS JT
LEFT JOIN {0}.dbo.jmp_app AS JA
        ON JA.a_id=JT.t_appid
WHERE   CONVERT(NVARCHAR(10),t_time,120)='{1}'
        AND EXISTS ( SELECT 1
                     FROM   {0}.dbo.jmp_user AS JU
                     WHERE  JU.u_merchant_id={2}
                            AND JU.u_id=JA.a_user_id )
GROUP BY CONVERT(NVARCHAR(13),t_time,120);", baseDbName, tTime, merchantId);
            return DbHelperSQLDEVICE.Query(strSql.ToString());
        }

    }
}

