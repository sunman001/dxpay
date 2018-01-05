using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //提款表
    public partial class jmp_pay
    {
        public bool Exists(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_pay");
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
        public int Add(JMP.MDL.jmp_pay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_pay(");
            strSql.Append("p_tradeno,p_applytime,p_dealno,p_paytime,p_state,p_money,p_bill_id");
            strSql.Append(") values (");
            strSql.Append("@p_tradeno,@p_applytime,@p_dealno,@p_paytime,@p_state,@p_money,@p_bill_id");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			    new SqlParameter("@p_tradeno", SqlDbType.NVarChar,-1),            
                new SqlParameter("@p_applytime", SqlDbType.DateTime),            
                new SqlParameter("@p_dealno", SqlDbType.NVarChar,-1),            
                new SqlParameter("@p_paytime", SqlDbType.DateTime),            
                new SqlParameter("@p_state", SqlDbType.Int,4),            
                new SqlParameter("@p_money", SqlDbType.Money,8),            
                new SqlParameter("@p_bill_id", SqlDbType.NVarChar,-1)
            };

            parameters[0].Value = model.p_tradeno;
            parameters[1].Value = model.p_applytime;
            parameters[2].Value = model.p_dealno;
            parameters[3].Value = model.p_paytime;
            parameters[4].Value = model.p_state;
            parameters[5].Value = model.p_money;
            parameters[6].Value = model.p_bill_id;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
                return 0;
            else
            {
                return Convert.ToInt32(obj);
            }

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_pay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_pay set ");

            strSql.Append("p_tradeno=@p_tradeno,");
            strSql.Append("p_applytime=@p_applytime,");
            strSql.Append("p_dealno=@p_dealno,");
            strSql.Append("p_paytime=@p_paytime,");
            strSql.Append("p_state=@p_state,");
            strSql.Append("p_money=@p_money,");
            strSql.Append("p_bill_id=@p_bill_id");
            strSql.Append(" where p_id=@p_id ");

            SqlParameter[] parameters = {
			    new SqlParameter("@p_id", SqlDbType.Int,4),            
                new SqlParameter("@p_tradeno", SqlDbType.NVarChar,-1),            
                new SqlParameter("@p_applytime", SqlDbType.DateTime),            
                new SqlParameter("@p_dealno", SqlDbType.NVarChar,-1),            
                new SqlParameter("@p_paytime", SqlDbType.DateTime),            
                new SqlParameter("@p_state", SqlDbType.Int,4),            
                new SqlParameter("@p_money", SqlDbType.Money,8),            
                new SqlParameter("@p_bill_id", SqlDbType.NVarChar,-1)
            };

            parameters[0].Value = model.p_id;
            parameters[1].Value = model.p_tradeno;
            parameters[2].Value = model.p_applytime;
            parameters[3].Value = model.p_dealno;
            parameters[4].Value = model.p_paytime;
            parameters[5].Value = model.p_state;
            parameters[6].Value = model.p_money;
            parameters[7].Value = model.p_bill_id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新交易号
        /// </summary>
        /// <param name="payid">提款id</param>
        /// <param name="dealno">交易号</param>
        /// <returns></returns>
        public bool Update(int payid, string dealno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_pay set p_dealno=@p_dealno,p_paytime=@p_paytime,p_state=@p_state where p_id=@p_id");
            SqlParameter[] parameters = {        
                new SqlParameter("@p_dealno", SqlDbType.NVarChar,-1),
                new SqlParameter("@p_paytime", SqlDbType.DateTime),
                new SqlParameter("@p_state",  SqlDbType.Int,4),
			    new SqlParameter("@p_id", SqlDbType.Int,4)
            };

            parameters[0].Value = dealno;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = 1;
            parameters[3].Value = payid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_pay ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
				new SqlParameter("@p_id", SqlDbType.Int,4)
			};
            parameters[0].Value = p_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string p_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_pay ");
            strSql.Append(" where ID in (" + p_idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_pay GetModel(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_tradeno,p_applytime,p_dealno,p_paytime,p_state,p_money,p_bill_id");
            strSql.Append("  from jmp_pay ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
				new SqlParameter("@p_id", SqlDbType.Int,4)
			};
            parameters[0].Value = p_id;

            JMP.MDL.jmp_pay model = new JMP.MDL.jmp_pay();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
                }
                model.p_tradeno = ds.Tables[0].Rows[0]["p_tradeno"].ToString();
                if (ds.Tables[0].Rows[0]["p_applytime"].ToString() != "")
                {
                    model.p_applytime = DateTime.Parse(ds.Tables[0].Rows[0]["p_applytime"].ToString());
                }
                model.p_dealno = ds.Tables[0].Rows[0]["p_dealno"].ToString();
                if (ds.Tables[0].Rows[0]["p_paytime"].ToString() != "")
                {
                    model.p_paytime = DateTime.Parse(ds.Tables[0].Rows[0]["p_paytime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_state"].ToString() != "")
                {
                    model.p_state = int.Parse(ds.Tables[0].Rows[0]["p_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_money"].ToString() != "")
                {
                    model.p_money = decimal.Parse(ds.Tables[0].Rows[0]["p_money"].ToString());
                }
                model.p_bill_id = ds.Tables[0].Rows[0]["p_bill_id"].ToString();

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
            strSql.Append(" FROM jmp_pay ");

            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append(" where " + strWhere);

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
                strSql.Append(" top " + Top.ToString());

            strSql.Append(" * ");
            strSql.Append(" FROM jmp_pay ");
            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append(" where " + strWhere);

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public DataTable GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
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
        /// 查询编号
        /// </summary>
        /// <returns></returns>
        public string SelectBh()
        {
            string sql = string.Format(" SELECT  RIGHT(1000001+ISNULL(RIGHT(MAX(p_tradeno),7),0),7) FROM jmp_pay  where CONVERT(varchar(10),p_applytime,120)='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");

            string bh = "SD" + DateTime.Now.ToString("yyyyMMdd") + DbHelperSQL.GetSingle(sql);
            return bh;
        }

    }
}