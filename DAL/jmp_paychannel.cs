using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.MDL;
namespace JMP.DAL
{
    ///<summary>
    ///支付渠道汇总
    ///</summary>
    public partial class jmp_paychannel
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_paychannel");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_paychannel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_paychannel(");
            strSql.Append("payname,payid,money,datetimes,paytype,success,successratio,notpay");
            strSql.Append(") values (");
            strSql.Append("@payname,@payid,@money,@datetimes,@paytype");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@payname", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@payid", SqlDbType.Int,4) ,            
                        new SqlParameter("@money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@datetimes", SqlDbType.DateTime),
                        new SqlParameter("@paytype",SqlDbType.NVarChar,-1),
                        new SqlParameter("@success",model.success),
                        new SqlParameter("@successratio",model.successratio),
                        new SqlParameter("@notpay",model.notpay)
              
            };

            parameters[0].Value = model.payname;
            parameters[1].Value = model.payid;
            parameters[2].Value = model.money;
            parameters[3].Value = model.datetimes;
            parameters[4].Value = model.paytype;

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
        public bool Update(JMP.MDL.jmp_paychannel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_paychannel set ");

            strSql.Append(" payname = @payname , ");
            strSql.Append(" payid = @payid , ");
            strSql.Append(" money = @money , ");
            strSql.Append(" datetimes = @datetimes,  ");
            strSql.Append(" paytype = @paytype,  ");
            strSql.Append(" success=@success, successratio=@successratio, notpay=@notpay ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@payname", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@payid", SqlDbType.Int,4) ,            
                        new SqlParameter("@money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@datetimes", SqlDbType.DateTime), 
                        new SqlParameter("@paytype",SqlDbType.NVarChar,-1),
                        new SqlParameter("@success",model.success),
                        new SqlParameter("@successratio",model.successratio),
                        new SqlParameter("@notpay",model.notpay)
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.payname;
            parameters[2].Value = model.payid;
            parameters[3].Value = model.money;
            parameters[4].Value = model.datetimes;
            parameters[5].Value = model.paytype;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_paychannel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_paychannel ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_paychannel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, payname, payid, money, datetimes,paytype  ");
            strSql.Append("  from jmp_paychannel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            JMP.MDL.jmp_paychannel model = new JMP.MDL.jmp_paychannel();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.payname = ds.Tables[0].Rows[0]["payname"].ToString();
                model.paytype = ds.Tables[0].Rows[0]["paytype"].ToString();
                if (ds.Tables[0].Rows[0]["payid"].ToString() != "")
                {
                    model.payid = int.Parse(ds.Tables[0].Rows[0]["payid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["money"].ToString() != "")
                {
                    model.money = decimal.Parse(ds.Tables[0].Rows[0]["money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["datetimes"].ToString() != "")
                {
                    model.datetimes = DateTime.Parse(ds.Tables[0].Rows[0]["datetimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["success"].ToString() != "")
                {
                    model.success = int.Parse(ds.Tables[0].Rows[0]["success"].ToString());
                }
                if (ds.Tables[0].Rows[0]["successratio"].ToString() != "")
                {
                    model.successratio = decimal.Parse(ds.Tables[0].Rows[0]["successratio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["notpay"].ToString() != "")
                {
                    model.notpay = int.Parse(ds.Tables[0].Rows[0]["notpay"].ToString());
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
            strSql.Append(" FROM jmp_paychannel ");
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
            strSql.Append(" FROM jmp_paychannel ");
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
        public List<JMP.MDL.jmp_paychannel> GetLists(string sql, string OrderBy, int PageIndex, int PageSize, out int Count)
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
            DataTable dt = new DataTable();
            da.Fill(dt);
            Count = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_paychannel>(dt);
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
    }
}
