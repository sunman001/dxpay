using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
  public partial  class CoOperationLog
    {
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoOperationLog");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CoOperationLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoOperationLog(");
            strSql.Append("Summary,Message,CreatedOn,CreatedById,CreatedByName,IpAddress");
            strSql.Append(") values (");
            strSql.Append("@Summary,@Message,@CreatedOn,@CreatedById,@CreatedByName,@IpAddress");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@Summary", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IpAddress", SqlDbType.NVarChar,32)

            };

            parameters[0].Value = model.Summary;
            parameters[1].Value = model.Message;
            parameters[2].Value = model.CreatedOn;
            parameters[3].Value = model.CreatedById;
            parameters[4].Value = model.CreatedByName;
            parameters[5].Value = model.IpAddress;

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
        public bool Update(JMP.MDL.CoOperationLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoOperationLog set ");

            strSql.Append(" Summary = @Summary , ");
            strSql.Append(" Message = @Message , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedById = @CreatedById , ");
            strSql.Append(" CreatedByName = @CreatedByName , ");
            strSql.Append(" IpAddress = @IpAddress  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@Summary", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IpAddress", SqlDbType.NVarChar,32)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Summary;
            parameters[2].Value = model.Message;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedById;
            parameters[5].Value = model.CreatedByName;
            parameters[6].Value = model.IpAddress;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CoOperationLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CoOperationLog ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
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
        public JMP.MDL.CoOperationLog GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Summary, Message, CreatedOn, CreatedById, CreatedByName, IpAddress  ");
            strSql.Append("  from CoOperationLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoOperationLog model = new JMP.MDL.CoOperationLog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"].ToString();

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
            strSql.Append(" FROM CoOperationLog ");
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
            strSql.Append(" FROM CoOperationLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        DataTable dt = new DataTable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.CoOperationLog> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
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
            return DbHelperSQL.ToList<JMP.MDL.CoOperationLog>(ds.Tables[0]);
        }
    }
}
