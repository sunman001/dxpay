using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //用户报表
    public partial class jmp_user_report
    {
        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_user_report");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id  ");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_user_report model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_user_report(");
            strSql.Append("r_notpay,r_alipay,r_wechat,r_app_key,r_app_name,r_user_id,r_user_name,r_date,r_create,r_equipment,r_succeed");
            strSql.Append(") values (");
            strSql.Append("@r_notpay,@r_alipay,@r_wechat,@r_app_key,@r_app_name,@r_user_id,@r_user_name,@r_date,@r_create,@r_equipment,@r_succeed");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			    new SqlParameter("@r_notpay", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_alipay", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_wechat", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_app_key", SqlDbType.NVarChar,-1) ,            
                new SqlParameter("@r_app_name", SqlDbType.NVarChar,-1) ,            
                new SqlParameter("@r_user_id", SqlDbType.Int,4) ,            
                new SqlParameter("@r_user_name", SqlDbType.NVarChar,-1) ,            
                new SqlParameter("@r_date", SqlDbType.Date,3) ,            
                new SqlParameter("@r_create", SqlDbType.DateTime) ,            
                new SqlParameter("@r_equipment", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_succeed", SqlDbType.Decimal,17)  
            };

            parameters[0].Value = model.a_notpay;
            parameters[1].Value = model.a_alipay;
            parameters[2].Value = model.a_wechat;
            parameters[3].Value = model.r_app_key;
            parameters[4].Value = model.r_app_name;
            parameters[5].Value = model.r_user_id;
            parameters[6].Value = model.r_user_name;
            parameters[7].Value = model.r_date;
            parameters[8].Value = model.r_create;
            parameters[9].Value = model.r_equipment;
            parameters[10].Value = model.a_succeed;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_user_report model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user_report set ");

            strSql.Append("r_notpay=@r_notpay,");
            strSql.Append("r_alipay=@r_alipay,");
            strSql.Append("r_wechat=@r_wechat,");
            strSql.Append("r_app_key=@r_app_key,");
            strSql.Append("r_app_name=@r_app_name,");
            strSql.Append("r_user_id=@r_user_id,");
            strSql.Append("r_user_name=@r_user_name,");
            strSql.Append("r_date=@r_date,");
            strSql.Append("r_create=@r_create,");
            strSql.Append("r_equipment=@r_equipment,");
            strSql.Append("r_succeed=@r_succeed");
            strSql.Append(" where r_id=@r_id");

            SqlParameter[] parameters = {
			    new SqlParameter("@r_id", SqlDbType.Int,4) ,            
                new SqlParameter("@r_notpay", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_alipay", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_wechat", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_app_key", SqlDbType.NVarChar,-1) ,            
                new SqlParameter("@r_app_name", SqlDbType.NVarChar,-1) ,            
                new SqlParameter("@r_user_id", SqlDbType.Int,4) ,            
                new SqlParameter("@r_user_name", SqlDbType.NVarChar,-1) ,            
                new SqlParameter("@r_date", SqlDbType.Date,3) ,            
                new SqlParameter("@r_create", SqlDbType.DateTime) ,            
                new SqlParameter("@r_equipment", SqlDbType.Decimal,17) ,            
                new SqlParameter("@r_succeed", SqlDbType.Decimal,17) 
            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.a_notpay;
            parameters[2].Value = model.a_alipay;
            parameters[3].Value = model.a_wechat;
            parameters[4].Value = model.r_app_key;
            parameters[5].Value = model.r_app_name;
            parameters[6].Value = model.r_user_id;
            parameters[7].Value = model.r_user_name;
            parameters[8].Value = model.r_date;
            parameters[9].Value = model.r_create;
            parameters[10].Value = model.r_equipment;
            parameters[11].Value = model.a_succeed;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_user_report");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
			    new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_user_report");
            strSql.Append(" where r_id in (" + r_idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_user_report GetModel(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from jmp_user_report ");
            strSql.Append("where r_id=@r_id");
            SqlParameter[] parameters = {
				new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            JMP.MDL.jmp_user_report model = new JMP.MDL.jmp_user_report();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_notpay"].ToString() != "")
                {
                    model.a_notpay = decimal.Parse(ds.Tables[0].Rows[0]["r_notpay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_alipay"].ToString() != "")
                {
                    model.a_alipay = decimal.Parse(ds.Tables[0].Rows[0]["r_alipay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_wechat"].ToString() != "")
                {
                    model.a_wechat = decimal.Parse(ds.Tables[0].Rows[0]["r_wechat"].ToString());
                }
                model.r_app_key = ds.Tables[0].Rows[0]["r_app_key"].ToString();
                model.r_app_name = ds.Tables[0].Rows[0]["r_app_name"].ToString();
                if (ds.Tables[0].Rows[0]["r_user_id"].ToString() != "")
                {
                    model.r_user_id = int.Parse(ds.Tables[0].Rows[0]["r_user_id"].ToString());
                }
                model.r_user_name = ds.Tables[0].Rows[0]["r_user_name"].ToString();
                if (ds.Tables[0].Rows[0]["r_date"].ToString() != "")
                {
                    model.r_date = DateTime.Parse(ds.Tables[0].Rows[0]["r_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_create"].ToString() != "")
                {
                    model.r_create = DateTime.Parse(ds.Tables[0].Rows[0]["r_create"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_equipment"].ToString() != "")
                {
                    model.r_equipment = decimal.Parse(ds.Tables[0].Rows[0]["r_equipment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_succeed"].ToString() != "")
                {
                    model.a_succeed = decimal.Parse(ds.Tables[0].Rows[0]["r_succeed"].ToString());
                }
                return model;
            }
            else
                return null;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM jmp_user_report ");

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

            strSql.Append(" * ");
            strSql.Append(" FROM jmp_user_report ");
            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append(" where " + strWhere);

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
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_user_report> DcSelectList(string sql)
        {
            DataTable dt = DbHelperSQLTotal.Query(sql).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_user_report>(dt);
        }

    }
}
