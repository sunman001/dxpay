using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using JMP.DBA;

namespace JMP.DAL
{
    //结算日志详情记录表
    public class LogCoSettlement
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LogCoSettlement");
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
        public int Add(JMP.MDL.LogCoSettlement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LogCoSettlement(");
            strSql.Append("UserId,TypeId,IpAddress,Location,Summary,Message,CreatedOn");
            strSql.Append(") values (");
            strSql.Append("@UserId,@TypeId,@IpAddress,@Location,@Summary,@Message,@CreatedOn");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", SqlDbType.Int,4) ,
                new SqlParameter("@TypeId", SqlDbType.Int,4) ,
                new SqlParameter("@IpAddress", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Location", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Summary", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Message", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.IpAddress;
            parameters[3].Value = model.Location;
            parameters[4].Value = model.Summary;
            parameters[5].Value = model.Message;
            parameters[6].Value = model.CreatedOn;

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
        public bool Update(JMP.MDL.LogCoSettlement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LogCoSettlement set ");

            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" TypeId = @TypeId , ");
            strSql.Append(" IpAddress = @IpAddress , ");
            strSql.Append(" Location = @Location , ");
            strSql.Append(" Summary = @Summary , ");
            strSql.Append(" Message = @Message , ");
            strSql.Append(" CreatedOn = @CreatedOn  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                new SqlParameter("@Id", SqlDbType.Int,4) ,
                new SqlParameter("@UserId", SqlDbType.Int,4) ,
                new SqlParameter("@TypeId", SqlDbType.Int,4) ,
                new SqlParameter("@IpAddress", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Location", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Summary", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@Message", SqlDbType.NVarChar,-1) ,
                new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.TypeId;
            parameters[3].Value = model.IpAddress;
            parameters[4].Value = model.Location;
            parameters[5].Value = model.Summary;
            parameters[6].Value = model.Message;
            parameters[7].Value = model.CreatedOn;
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
            strSql.Append("delete from LogCoSettlement ");
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
            strSql.Append("delete from LogCoSettlement ");
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
        public JMP.MDL.LogCoSettlement GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, UserId, TypeId, IpAddress, Location, Summary, Message, CreatedOn  ");
            strSql.Append("  from LogCoSettlement ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.LogCoSettlement model = new JMP.MDL.LogCoSettlement();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
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
            strSql.Append(" FROM LogCoSettlement ");
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
            strSql.Append(" FROM LogCoSettlement ");
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
        public List<MDL.LogCoSettlement> SelectList(string where, string order, int pageIndexs, int pageSize, out int pageCount)
        {
            var sql = string.Format("select a.* from LogCoSettlement as a {0}", where);
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
            return DbHelperSQL.ToList<MDL.LogCoSettlement>(ds.Tables[0]);
        }
    }
}

