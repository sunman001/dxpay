using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///支付类型
    ///</summary>
    public partial class jmp_paymenttype
    {

        public bool Exists(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_paymenttype");
            strSql.Append(" where ");
            strSql.Append(" p_id = @p_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_paymenttype model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_paymenttype(");
            strSql.Append("p_name,p_type,p_extend,p_priority,p_forbidden,p_platform");
            strSql.Append(") values (");
            strSql.Append("@p_name,@p_type,@p_extend,@p_priority,@p_forbidden,@p_platform");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@p_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@p_type", SqlDbType.Int,4) ,
                        new SqlParameter("@p_extend", SqlDbType.NVarChar,-1),
                        new SqlParameter("@p_priority",SqlDbType.Int,4),
                        new SqlParameter("@p_forbidden",SqlDbType.Int,4),
                        new SqlParameter("@p_platform", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.p_name;
            parameters[1].Value = model.p_type;
            parameters[2].Value = model.p_extend;

            parameters[3].Value = model.p_priority;
            parameters[4].Value = model.p_forbidden;
            parameters[5].Value = model.p_platform;

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
        public bool Update(JMP.MDL.jmp_paymenttype model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_paymenttype set ");

            strSql.Append(" p_name = @p_name , ");
            strSql.Append(" p_type = @p_type , ");
            strSql.Append(" p_extend = @p_extend,  ");
            strSql.Append(" p_priority = @p_priority,  ");
            strSql.Append(" p_forbidden = @p_forbidden,  ");
            strSql.Append(" p_platform = @p_platform  ");
            strSql.Append(" where p_id=@p_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@p_id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@p_type", SqlDbType.Int,4) ,
                        new SqlParameter("@p_extend", SqlDbType.NVarChar,-1),
                         new SqlParameter("@p_priority",SqlDbType.Int,4),
                        new SqlParameter("@p_forbidden",SqlDbType.Int,4),
                        new SqlParameter("@p_platform", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.p_id;
            parameters[1].Value = model.p_name;
            parameters[2].Value = model.p_type;
            parameters[3].Value = model.p_extend;
            parameters[4].Value = model.p_priority;
            parameters[5].Value = model.p_forbidden;
            parameters[6].Value = model.p_platform;

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
        public bool Delete(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_paymenttype ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


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
        public bool DeleteList(string p_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_paymenttype ");
            strSql.Append(" where ID in (" + p_idlist + ")  ");
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
        public JMP.MDL.jmp_paymenttype GetModel(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id, p_name, p_type, p_extend,p_priority,p_forbidden,p_platform,CostRatio  ");
            strSql.Append("  from jmp_paymenttype ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


            JMP.MDL.jmp_paymenttype model = new JMP.MDL.jmp_paymenttype();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
                }
                model.p_name = ds.Tables[0].Rows[0]["p_name"].ToString();
                if (ds.Tables[0].Rows[0]["p_type"].ToString() != "")
                {
                    model.p_type = int.Parse(ds.Tables[0].Rows[0]["p_type"].ToString());
                }
                model.p_extend = ds.Tables[0].Rows[0]["p_extend"].ToString();

                if (ds.Tables[0].Rows[0]["p_priority"].ToString() != "")
                {
                    model.p_priority = int.Parse(ds.Tables[0].Rows[0]["p_priority"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_forbidden"].ToString() != "")
                {
                    model.p_forbidden = int.Parse(ds.Tables[0].Rows[0]["p_forbidden"].ToString());
                }
                model.p_platform = ds.Tables[0].Rows[0]["p_platform"].ToString();
                if (ds.Tables[0].Rows[0]["CostRatio"].ToString() != "")
                {
                    model.CostRatio = decimal.Parse(ds.Tables[0].Rows[0]["CostRatio"].ToString());
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
            strSql.Append(" FROM jmp_paymenttype ");
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
            strSql.Append(" FROM jmp_paymenttype ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询支付通道信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="pageIndexs">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_paymenttype> SelectListPage(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_paymenttype>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据风控配置或者应用id查询一条可用信息
        /// </summary>
        /// <param name="p_type">支付类型</param>
        /// <param name="glpt">关联平台</param>
        /// <param name="apptype">应用id或者风险配置id</param>
        ///  <param name="risk">风控类型（0：风险等级，1：应用配置）</param>
        /// <returns></returns>
        public DataTable SelectModesType(int p_type, int glpt, int apptype, int risk)
        {

            string sql = string.Format("  select  p_extend  from jmp_paymenttype t  LEFT join jmp_interface f on f.l_paymenttype_id = t.p_id where t.p_forbidden = 0 AND t.p_type = " + p_type + " AND ',' + t.p_platform + ',' like '%,' + cast(" + glpt + " as varchar(20)) + ',%' AND f.l_isenable = 1 AND ',' + f.l_apptypeid + ',' like '%,' + cast(" + apptype + " AS varchar(20)) + ',%' AND f.l_sort = (SELECT  top 1 l_sort FROM  jmp_interface WHERE   ',' + l_apptypeid + ',' like '%,' + cast(" + apptype + " as varchar(20)) + ',%' AND l_isenable = 1 AND l_paymenttype_id IN(SELECT p_id FROM jmp_paymenttype WHERE P_TYPE =" + p_type + ") ORDER BY l_sort) AND t.p_forbidden = 0 and f.l_risk =" + risk + " ");
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据应用id和支付类型查询通道池相关联的支付通道
        /// </summary>
        /// <param name="p_type">支付类型</param>
        /// <param name="glpt">关联平台</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public DataTable SelectInterface(int p_type, int glpt, int appid)
        {
            string sql = string.Format("WITH T1 AS( select  a.Id from[dbo].[jmp_channel_pool] as a inner join  jmp_channel_app_mapping as b  on a.Id = b.ChannelId where a.IsEnabled = 1 and b.AppId =" + appid + "), T2 AS( select * from jmp_paymenttype t left  join jmp_interface f on f.l_paymenttype_id = t.p_id where t.p_forbidden = 0 AND t.p_type =" + p_type + "  AND ',' + t.p_platform + ',' like '%,' + cast(" + glpt + " as varchar(20)) + ',%' AND f.l_isenable = 1 AND t.p_forbidden = 0 and f.l_risk = 2 AND l_paymenttype_id IN(SELECT p_id FROM jmp_paymenttype WHERE P_TYPE =" + p_type + " )) select p_extend from T2, T1  where ',' + T2.l_apptypeid + ',' like '%,' + cast(T1.id AS varchar(20)) + ',%' AND T2.l_sort = (SELECT  top 1 l_sort FROM  jmp_interface WHERE   ',' + l_apptypeid + ',' like '%,' + cast(T1.id as varchar(20)) + ',%' AND l_isenable = 1 and l_risk = 2 AND l_paymenttype_id IN(SELECT p_id FROM jmp_paymenttype WHERE P_TYPE = " + p_type + " ) ORDER BY l_sort )   group by p_extend  ");
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查询微信appid支付通道
        /// </summary>
        /// <param name="p_type">支付类型</param>
        /// <param name="glpt">关联平台</param>
        /// <param name="APPID">应用id或者风险配置id</param>
        /// <param name="risk">风控类型（0：风险等级，1：应用配置）</param>
        /// <returns></returns>
        public string SelectWXapp(int p_type, int glpt, int APPID, int risk)
        {
            string sql = string.Format(" select TOP 1 p_extend  from jmp_paymenttype t LEFT join jmp_interface f on f.l_paymenttype_id = t.p_id where t.p_forbidden = 0 AND t.p_type = " + p_type + " AND ',' + t.p_platform + ',' like '%,' + cast(" + glpt + " as varchar(20)) + ',%' AND ','+f.l_apptypeid+',' LIKE '%,'+CAST(" + APPID + " AS VARCHAR(20))+',%' AND f.l_isenable = 1 and f.l_risk=" + risk + "    ORDER BY f.l_sort  ");
            object fhstr = DbHelperSQL.GetSingle(sql);
            string str = fhstr == null ? null : fhstr.ToString();
            return str;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_paymenttype set p_forbidden=" + state + "  ");
            strSql.Append(" where p_id in (" + u_idlist + ")  ");
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
        /// 修改通道成本费率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="costratio"></param>
        /// <returns></returns>
        public bool UpdateCostRatio(int id, string costratio)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update jmp_paymenttype set CostRatio='" + costratio + "' ");
            sql.Append(" where p_id=" + id + "");

            int num = DbHelperSQL.ExecuteSql(sql.ToString());

            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

