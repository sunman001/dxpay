using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //代付通道
    public partial class PayChannel
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PayChannel");
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
        public int Add(JMP.MDL.PayChannel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PayChannel(");
            strSql.Append("ChannelName,ChannelIdentifier,Append,Appendtime");
            strSql.Append(") values (");
            strSql.Append("@ChannelName,@ChannelIdentifier,@Append,@Appendtime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@ChannelIdentifier", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@Append", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@Appendtime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.ChannelName;
            parameters[1].Value = model.ChannelIdentifier;
            parameters[2].Value = model.Append;
            parameters[3].Value = model.Appendtime;

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
        public bool Update(JMP.MDL.PayChannel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PayChannel set ");

            strSql.Append(" ChannelName = @ChannelName , ");
            strSql.Append(" ChannelIdentifier = @ChannelIdentifier , ");
            strSql.Append(" Append = @Append , ");
            strSql.Append(" Appendtime = @Appendtime  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@ChannelIdentifier", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@Append", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@Appendtime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.ChannelName;
            parameters[2].Value = model.ChannelIdentifier;
            parameters[3].Value = model.Append;
            parameters[4].Value = model.Appendtime;
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
            strSql.Append("delete from PayChannel ");
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
            strSql.Append("delete from PayChannel ");
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
        public JMP.MDL.PayChannel GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ChannelName, ChannelIdentifier, Append, Appendtime  ");
            strSql.Append("  from PayChannel ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.PayChannel model = new JMP.MDL.PayChannel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.ChannelName = ds.Tables[0].Rows[0]["ChannelName"].ToString();
                model.ChannelIdentifier = ds.Tables[0].Rows[0]["ChannelIdentifier"].ToString();
                model.Append = ds.Tables[0].Rows[0]["Append"].ToString();
                if (ds.Tables[0].Rows[0]["Appendtime"].ToString() != "")
                {
                    model.Appendtime = DateTime.Parse(ds.Tables[0].Rows[0]["Appendtime"].ToString());
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
            strSql.Append(" FROM PayChannel ");
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
            strSql.Append(" FROM PayChannel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 查询代付通道列表
        /// </summary>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.PayChannel> PayChannelList(int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from PayChannel");

            string Order = " order by Id desc ";

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
            return DbHelperSQL.ToList<JMP.MDL.PayChannel>(ds.Tables[0]);
        }
    }
}

