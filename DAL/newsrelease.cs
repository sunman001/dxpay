using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///应用官方新闻发布
    ///</summary>
    public partial class newsrelease
    {

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int n_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from newsrelease");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@n_id", SqlDbType.Int,4)
            };
            parameters[0].Value = n_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.newsrelease model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into newsrelease(");
            strSql.Append("n_category,n_title,n_picture,n_info,n_user,n_time,n_count,description,keywords");
            strSql.Append(") values (");
            strSql.Append("@n_category,@n_title,@n_picture,@n_info,@n_user,@n_time,@n_count,@description,@keywords");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@n_category", SqlDbType.Int,4) ,
                        new SqlParameter("@n_title", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@n_picture", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@n_info", SqlDbType.Text) ,
                        new SqlParameter("@n_user", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@n_time", SqlDbType.DateTime) ,
                        new SqlParameter("@n_count", SqlDbType.Int,4),
                         new SqlParameter("@description",model.description),
                        new SqlParameter("@keywords",model.keywords)

            };

            parameters[0].Value = model.n_category;
            parameters[1].Value = model.n_title;
            parameters[2].Value = model.n_picture;
            parameters[3].Value = model.n_info;
            parameters[4].Value = model.n_user;
            parameters[5].Value = model.n_time;
            parameters[6].Value = model.n_count;
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
        public bool Update(JMP.MDL.newsrelease model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update newsrelease set ");

            strSql.Append(" n_category = @n_category , ");
            strSql.Append(" n_title = @n_title , ");
            strSql.Append(" n_picture = @n_picture , ");
            strSql.Append(" n_info = @n_info , ");
            strSql.Append(" n_user = @n_user , ");
            strSql.Append(" n_time = @n_time , ");
            strSql.Append(" n_count = @n_count,  ");
            strSql.Append(" description = @description,  ");
            strSql.Append(" keywords = @keywords  ");
            strSql.Append(" where n_id=@n_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@n_id", SqlDbType.Int,4) ,
                        new SqlParameter("@n_category", SqlDbType.Int,4) ,
                        new SqlParameter("@n_title", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@n_picture", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@n_info", SqlDbType.Text) ,
                        new SqlParameter("@n_user", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@n_time", SqlDbType.DateTime) ,
                        new SqlParameter("@n_count", SqlDbType.Int,4),
                        new SqlParameter("@description",model.description),
                        new SqlParameter("@keywords",model.keywords)

            };

            parameters[0].Value = model.n_id;
            parameters[1].Value = model.n_category;
            parameters[2].Value = model.n_title;
            parameters[3].Value = model.n_picture;
            parameters[4].Value = model.n_info;
            parameters[5].Value = model.n_user;
            parameters[6].Value = model.n_time;
            parameters[7].Value = model.n_count;
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
        public bool Delete(int n_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from newsrelease ");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@n_id", SqlDbType.Int,4)
            };
            parameters[0].Value = n_id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string n_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from newsrelease ");
            strSql.Append(" where n_id in (" + n_idlist + ")  ");
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
        public JMP.MDL.newsrelease GetModel(int n_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from newsrelease ");
            strSql.Append(" where n_id=@n_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@n_id", SqlDbType.Int,4)
            };
            parameters[0].Value = n_id;

            JMP.MDL.newsrelease model = new JMP.MDL.newsrelease();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.newsrelease DataRowToModel(DataRow row)
        {
            JMP.MDL.newsrelease model = new JMP.MDL.newsrelease();
            if (row != null)
            {
                if (row["n_id"] != null && row["n_id"].ToString() != "")
                {
                    model.n_id = int.Parse(row["n_id"].ToString());
                }
                if (row["n_category"] != null && row["n_category"].ToString() != "")
                {
                    model.n_category = int.Parse(row["n_category"].ToString());
                }
                if (row["n_title"] != null)
                {
                    model.n_title = row["n_title"].ToString();
                }
                if (row["n_picture"] != null)
                {
                    model.n_picture = row["n_picture"].ToString();
                }
                if (row["n_info"] != null)
                {
                    model.n_info = row["n_info"].ToString();
                }
                if (row["n_user"] != null)
                {
                    model.n_user = row["n_user"].ToString();
                }
                if (row["n_count"] != null)
                {
                    model.n_count = int.Parse(row["n_count"].ToString());
                }
                if (row["n_time"] != null && row["n_time"].ToString() != "")
                {
                    model.n_time = DateTime.Parse(row["n_time"].ToString());
                }
                if (row["description"] != null)
                {
                    model.description = row["description"].ToString();
                }
                if (row["keywords"] != null)
                {
                    model.keywords = row["keywords"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select n_id,n_category,n_title,n_picture,n_info,n_user,,n_count ");
            strSql.Append(" FROM newsrelease ");
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
            strSql.Append(" n_id,n_category,n_title,n_picture,n_info,n_user,n_time ");
            strSql.Append(" FROM newsrelease ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM newsrelease ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.n_id desc");
            }
            strSql.Append(")AS Row, T.*  from newsrelease T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="c_id">id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.newsrelease SelectId(int c_id)
        {
            string sql = string.Format(" select * from newsrelease  where n_id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.newsrelease>(dt);
        }
        /// <summary>
        /// 根据新闻当前id查询上一条和下一条
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <param name="type">所属类型</param>
        /// <returns></returns>
        public List<JMP.MDL.newsrelease> SelectUpDw(int id, int type)
        {
            string sql = string.Format(" select top 1 * from newsrelease  where n_id >@id  and n_category =@type union all select top 1 * from newsrelease  where n_id <@id  and n_category =@type ");
            SqlParameter[] par = {
                  new SqlParameter("@id", id),
                  new SqlParameter("@type",type)

            };
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.newsrelease>(dt);
        }

        public List<JMP.MDL.newsrelease> SelectList(string category, string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from newsrelease where 1=1");
            string Order = " Order by n_id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and n_title like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and n_info like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and n_user like  '%" + sea_name + "%' ";
                        break;
                }
            }
            //if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            //{
            //    sql += " and n_time>='" + stime + " 00:00:00' and n_time<='" + endtime + " 23:59:59' ";
            //}
            if (!string.IsNullOrEmpty(category))
            {
                sql += " and n_category='" + category + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by n_id  ";
            }
            else
            {
                Order = " order by n_id desc ";
            }
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
            return DbHelperSQL.ToList<JMP.MDL.newsrelease>(ds.Tables[0]);
        }

        public List<JMP.MDL.newsrelease> GetListsByType(int type, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from newsrelease where 1=1 and n_category='" + type + "' ");
            string Order = " Order by n_id desc";
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
            return DbHelperSQL.ToList<JMP.MDL.newsrelease>(ds.Tables[0]);
        }


        public List<JMP.MDL.newsrelease> GetLists(int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from newsrelease where 1=1 ");
            string Order = " Order by n_id desc";
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
            return DbHelperSQL.ToList<JMP.MDL.newsrelease>(ds.Tables[0]);
        }
        /// <summary>
        /// 查询最新10条行业新闻
        /// </summary>
        /// <returns></returns>
        public List<JMP.MDL.newsrelease> SelectListxw()
        {
            string sql = string.Format(" select top 20 * from newsrelease where 1=1 and n_category=2 Order by n_id desc ");
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.newsrelease>(dt);
        }

        public bool UpdateCount(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update newsrelease set n_count=n_count+1 ");
            strSql.Append(" where n_id = " + id + "  ");
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

