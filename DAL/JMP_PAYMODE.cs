using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///jmp_paymode
    ///</summary>
    public partial class jmp_paymode
    {

        public bool Exists(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_paymode");
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
        public int Add(JMP.MDL.jmp_paymode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_paymode(");
            strSql.Append("p_name,p_rate,p_state,p_islocked");
            strSql.Append(") values (");
            strSql.Append("@p_name,@p_rate,@p_state,@p_islocked");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@p_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@p_rate", SqlDbType.Decimal,5) ,
                        new SqlParameter("@p_state", SqlDbType.Int,4) ,
                        new SqlParameter("@p_islocked", SqlDbType.Int,4)

            };

            parameters[0].Value = model.p_name;
            parameters[1].Value = model.p_rate;
            parameters[2].Value = model.p_state;
            parameters[3].Value = model.p_islocked;
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
        public bool Update(JMP.MDL.jmp_paymode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_paymode set ");

            strSql.Append(" p_name = @p_name , ");
            strSql.Append(" p_rate = @p_rate , ");
            strSql.Append(" p_state = @p_state , ");
            strSql.Append(" p_islocked = @p_islocked  ");
            strSql.Append(" where p_id=@p_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@p_id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@p_rate", SqlDbType.Decimal,5) ,
                        new SqlParameter("@p_state", SqlDbType.Int,4) ,
                        new SqlParameter("@p_islocked", SqlDbType.Int,4)

            };
            parameters[0].Value = model.p_id;
            parameters[1].Value = model.p_name;
            parameters[2].Value = model.p_rate;
            parameters[3].Value = model.p_state;
            parameters[4].Value = model.p_islocked;
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
        /// 添加接口费率
        /// </summary>
        public bool Update_rate(int id, string p_rate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_paymode set ");
            strSql.Append(" p_rate = '" + p_rate + "' ");
            strSql.Append(" where p_id='" + id + "'");

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_paymode ");
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
            strSql.Append("delete from jmp_paymode ");
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
        public JMP.MDL.jmp_paymode GetModel(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,p_rate,p_state,p_islocked  ");
            strSql.Append("  from jmp_paymode ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


            JMP.MDL.jmp_paymode model = new JMP.MDL.jmp_paymode();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
                }
                model.p_name = ds.Tables[0].Rows[0]["p_name"].ToString();
                if (ds.Tables[0].Rows[0]["p_rate"].ToString() != "")
                {
                    model.p_rate = decimal.Parse(ds.Tables[0].Rows[0]["p_rate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_state"].ToString() != "")
                {
                    model.p_state = int.Parse(ds.Tables[0].Rows[0]["p_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_islocked"].ToString() != "")
                {
                    model.p_islocked = int.Parse(ds.Tables[0].Rows[0]["p_islocked"].ToString());
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
            strSql.Append(" FROM jmp_paymode ");
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
            strSql.Append(" FROM jmp_paymode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        DataTable dt = new DataTable();
        /// <summary>
        /// 查询支付类型信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns>返回一个list集合</returns>
        public List<JMP.MDL.jmp_paymode> SelectList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_paymode>(ds.Tables[0]);
        }
        /// <summary>
        /// 批量更新锁定
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_paymode set p_islocked=" + state + "  ");
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

    }
}

