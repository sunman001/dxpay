using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //投诉每日汇总表
    public partial class CsComplainArchive
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CsComplainArchive");
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
        public int Add(JMP.MDL.CsComplainArchive model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CsComplainArchive(");
            strSql.Append("AppId,UserId,ComplainTypeId,Amount,ArchiveDay,CreatedOn");
            strSql.Append(") values (");
            strSql.Append("@AppId,@UserId,@ComplainTypeId,@Amount,@ArchiveDay,@CreatedOn");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@ComplainTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Amount", SqlDbType.Int,4) ,
                        new SqlParameter("@ArchiveDay", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.AppId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ComplainTypeId;
            parameters[3].Value = model.Amount;
            parameters[4].Value = model.ArchiveDay;
            parameters[5].Value = model.CreatedOn;

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
        public bool Update(JMP.MDL.CsComplainArchive model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CsComplainArchive set ");

            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" ComplainTypeId = @ComplainTypeId , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" ArchiveDay = @ArchiveDay , ");
            strSql.Append(" CreatedOn = @CreatedOn  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@ComplainTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Amount", SqlDbType.Int,4) ,
                        new SqlParameter("@ArchiveDay", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.AppId;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.ComplainTypeId;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.ArchiveDay;
            parameters[6].Value = model.CreatedOn;
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
            strSql.Append("delete from CsComplainArchive ");
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
            strSql.Append("delete from CsComplainArchive ");
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
        public JMP.MDL.CsComplainArchive GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, AppId, UserId, ComplainTypeId, Amount, ArchiveDay, CreatedOn  ");
            strSql.Append("  from CsComplainArchive ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CsComplainArchive model = new JMP.MDL.CsComplainArchive();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(ds.Tables[0].Rows[0]["AppId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ComplainTypeId"].ToString() != "")
                {
                    model.ComplainTypeId = int.Parse(ds.Tables[0].Rows[0]["ComplainTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = int.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ArchiveDay"].ToString() != "")
                {
                    model.ArchiveDay = DateTime.Parse(ds.Tables[0].Rows[0]["ArchiveDay"].ToString());
                }
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
            strSql.Append(" FROM CsComplainArchive ");
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
            strSql.Append(" FROM CsComplainArchive ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
        public List<JMP.MDL.CsComplainArchive> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQLTotal.connectionString);
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
            DataTable dt = ds.Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.CsComplainArchive>(dt);
        }


        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectArchive(string sql)
        {
            return DbHelperSQLTotal.Query(sql.ToString()).Tables[0];
        }

    }
}

