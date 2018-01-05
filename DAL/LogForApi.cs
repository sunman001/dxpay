using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    /// <summary>
    /// 支付项目接口相关全局错误日志记录表
    /// </summary>
    public class LogForApi
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LogForApi");
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
        public int Add(JMP.MDL.LogForApi model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LogForApi(");
            strSql.Append("PlatformId,RelatedId,ErrorTypeId,ClientId,ClientName,TypeValue,IpAddress,Location,Summary,Message,CreatedOn");
            strSql.Append(") values (");
            strSql.Append("@PlatformId,@RelatedId,@ErrorTypeId,@ClientId,@ClientName,@TypeValue,@IpAddress,@Location,@Summary,@Message,@CreatedOn");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@PlatformId", SqlDbType.Int,4) ,
                        new SqlParameter("@RelatedId", SqlDbType.Int,4) ,
                        new SqlParameter("@ErrorTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ClientId", SqlDbType.Int,4) ,
                        new SqlParameter("@ClientName", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@TypeValue", SqlDbType.Int,4) ,
                        new SqlParameter("@IpAddress", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@Location", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Summary", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@Message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.PlatformId;
            parameters[1].Value = model.RelatedId;
            parameters[2].Value = model.ErrorTypeId;
            parameters[3].Value = model.ClientId;
            parameters[4].Value = model.ClientName;
            parameters[5].Value = model.TypeValue;
            parameters[6].Value = model.IpAddress;
            parameters[7].Value = model.Location;
            parameters[8].Value = model.Summary;
            parameters[9].Value = model.Message;
            parameters[10].Value = model.CreatedOn;

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
        public bool Update(JMP.MDL.LogForApi model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LogForApi set ");

            strSql.Append(" PlatformId = @PlatformId , ");
            strSql.Append(" RelatedId = @RelatedId , ");
            strSql.Append(" ErrorTypeId = @ErrorTypeId , ");
            strSql.Append(" ClientId = @ClientId , ");
            strSql.Append(" ClientName = @ClientName , ");
            strSql.Append(" TypeValue = @TypeValue , ");
            strSql.Append(" IpAddress = @IpAddress , ");
            strSql.Append(" Location = @Location , ");
            strSql.Append(" Summary = @Summary , ");
            strSql.Append(" Message = @Message , ");
            strSql.Append(" CreatedOn = @CreatedOn  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@PlatformId", SqlDbType.Int,4) ,
                        new SqlParameter("@RelatedId", SqlDbType.Int,4) ,
                        new SqlParameter("@ErrorTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ClientId", SqlDbType.Int,4) ,
                        new SqlParameter("@ClientName", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@TypeValue", SqlDbType.Int,4) ,
                        new SqlParameter("@IpAddress", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@Location", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Summary", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@Message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.PlatformId;
            parameters[2].Value = model.RelatedId;
            parameters[3].Value = model.ErrorTypeId;
            parameters[4].Value = model.ClientId;
            parameters[5].Value = model.ClientName;
            parameters[6].Value = model.TypeValue;
            parameters[7].Value = model.IpAddress;
            parameters[8].Value = model.Location;
            parameters[9].Value = model.Summary;
            parameters[10].Value = model.Message;
            parameters[11].Value = model.CreatedOn;
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
            strSql.Append("delete from LogForApi ");
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
            strSql.Append("delete from LogForApi ");
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
        public JMP.MDL.LogForApi GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PlatformId, RelatedId, ErrorTypeId, ClientId, ClientName, TypeValue, IpAddress, Location, Summary, Message, CreatedOn  ");
            strSql.Append("  from LogForApi ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.LogForApi model = new JMP.MDL.LogForApi();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PlatformId"].ToString() != "")
                {
                    model.PlatformId = int.Parse(ds.Tables[0].Rows[0]["PlatformId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RelatedId"].ToString() != "")
                {
                    model.RelatedId = int.Parse(ds.Tables[0].Rows[0]["RelatedId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ErrorTypeId"].ToString() != "")
                {
                    model.ErrorTypeId = int.Parse(ds.Tables[0].Rows[0]["ErrorTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClientId"].ToString() != "")
                {
                    model.ClientId = int.Parse(ds.Tables[0].Rows[0]["ClientId"].ToString());
                }
                model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                if (ds.Tables[0].Rows[0]["TypeValue"].ToString() != "")
                {
                    model.TypeValue = int.Parse(ds.Tables[0].Rows[0]["TypeValue"].ToString());
                }
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"].ToString();
                model.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                model.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
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
            strSql.Append(" FROM LogForApi ");
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
            strSql.Append(" FROM LogForApi ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">WHERE查询条件,请带上WHERE关键字</param>
        /// <param name="order"></param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<MDL.LogForApi> SelectList(string where, string order, int pageIndexs, int pageSize, out int pageCount)
        {
            var sql = string.Format("select a.* from LogForApi as a {0}", where);
            var con = new SqlConnection(DbHelperSQL.connectionString);
            var da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            var ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<MDL.LogForApi>(ds.Tables[0]);
        }

    }
}

