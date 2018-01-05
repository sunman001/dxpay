using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //新增通道池配置表,一对多的关系
    public partial class jmp_channel_pool
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_channel_pool");
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
        public int Add(JMP.MDL.jmp_channel_pool model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_channel_pool(");
            strSql.Append("PoolName,IsEnabled,CreatedOn,CreatedByUserId,Description");
            strSql.Append(") values (");
            strSql.Append("@PoolName,@IsEnabled,@CreatedOn,@CreatedByUserId,@Description");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@PoolName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IsEnabled", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.PoolName;
            parameters[1].Value = model.IsEnabled;
            parameters[2].Value = model.CreatedOn;
            parameters[3].Value = model.CreatedByUserId;
            parameters[4].Value = model.Description;

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
        public bool Update(JMP.MDL.jmp_channel_pool model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_channel_pool set ");

            strSql.Append(" PoolName = @PoolName , ");
            strSql.Append(" IsEnabled = @IsEnabled , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedByUserId = @CreatedByUserId , ");
            strSql.Append(" Description = @Description  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@PoolName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IsEnabled", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.PoolName;
            parameters[2].Value = model.IsEnabled;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedByUserId;
            parameters[5].Value = model.Description;
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
            strSql.Append("delete from jmp_channel_pool ");
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
            strSql.Append("delete from jmp_channel_pool ");
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
        public JMP.MDL.jmp_channel_pool GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PoolName, IsEnabled, CreatedOn, CreatedByUserId, Description  ");
            strSql.Append("  from jmp_channel_pool ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.jmp_channel_pool model = new JMP.MDL.jmp_channel_pool();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.PoolName = ds.Tables[0].Rows[0]["PoolName"].ToString();
                if (ds.Tables[0].Rows[0]["IsEnabled"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsEnabled"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsEnabled"].ToString().ToLower() == "true"))
                    {
                        model.IsEnabled = true;
                    }
                    else
                    {
                        model.IsEnabled = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedByUserId"].ToString() != "")
                {
                    model.CreatedByUserId = int.Parse(ds.Tables[0].Rows[0]["CreatedByUserId"].ToString());
                }
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();

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
            strSql.Append(" FROM jmp_channel_pool ");
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
            strSql.Append(" FROM jmp_channel_pool ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 通道池列表弹窗
        /// </summary>
        /// <param name="pageIndexs">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<MDL.jmp_channel_pool> SelectListTc(int type, string sea_name, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = " select  * from   [dbo].[jmp_channel_pool] where  IsEnabled=1 ";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql = sql + " and  Id=" + sea_name;
                        break;
                    case 2:
                        sql = sql + " and PoolName like'%" + sea_name + "%'";
                        break;

                }
            }
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_channel_pool>(ds.Tables[0]);
        }

        /// <summary>
        /// 查询列
        /// </summary>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.jmp_channel_pool> poolList(int PoolName, string searchKey, int IsEnabled, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.*,b.u_realname from jmp_channel_pool a left join jmp_locuser b on a.CreatedByUserId=b.u_id where 1=1");

            if (PoolName > 0 && !string.IsNullOrEmpty(searchKey))
            {
                sql += " and PoolName like '%" + searchKey + "%'";
            }
            if (IsEnabled != -1)
            {
                sql += " and IsEnabled=" + IsEnabled;
            }

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
            return DbHelperSQL.ToList<JMP.MDL.jmp_channel_pool>(ds.Tables[0]);
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="u_idlist">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdatePoolState(int id, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_channel_pool set IsEnabled=" + state + "  ");
            strSql.Append(" where Id= " + id);
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

