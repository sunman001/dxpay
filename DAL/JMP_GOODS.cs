using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //商品表
    public partial class jmp_goods
    {
        DataTable dt = new DataTable();
        public bool Exists(int g_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_goods");
            strSql.Append(" where ");
            strSql.Append(" g_id = @g_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@g_id", SqlDbType.Int,4)
			};
            parameters[0].Value = g_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_goods(");
            strSql.Append("g_app_id,g_name,g_saletype_id,g_price,g_state");
            strSql.Append(") values (");
            strSql.Append("@g_app_id,@g_name,@g_saletype_id,@g_price,@g_state");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@g_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@g_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@g_saletype_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@g_price", SqlDbType.Money,8) ,            
                        new SqlParameter("@g_state", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.g_app_id;
            parameters[1].Value = model.g_name;
            parameters[2].Value = model.g_saletype_id;
            parameters[3].Value = model.g_price;
            parameters[4].Value = model.g_state;

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
        public bool Update(JMP.MDL.jmp_goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_goods set ");

            strSql.Append(" g_app_id = @g_app_id , ");
            strSql.Append(" g_name = @g_name , ");
            strSql.Append(" g_saletype_id = @g_saletype_id , ");
            strSql.Append(" g_price = @g_price , ");
            strSql.Append(" g_state = @g_state  ");
            strSql.Append(" where g_id=@g_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@g_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@g_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@g_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@g_saletype_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@g_price", SqlDbType.Money,8) ,            
                        new SqlParameter("@g_state", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.g_id;
            parameters[1].Value = model.g_app_id;
            parameters[2].Value = model.g_name;
            parameters[3].Value = model.g_saletype_id;
            parameters[4].Value = model.g_price;
            parameters[5].Value = model.g_state;
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
        public bool Delete(int g_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_goods ");
            strSql.Append(" where g_id=@g_id");
            SqlParameter[] parameters = {
					new SqlParameter("@g_id", SqlDbType.Int,4)
			};
            parameters[0].Value = g_id;


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
        public bool DeleteList(string g_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_goods ");
            strSql.Append(" where ID in (" + g_idlist + ")  ");
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
        public JMP.MDL.jmp_goods GetModel(int g_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select g_id, g_app_id, g_name, g_saletype_id, g_price, g_state  ");
            strSql.Append("  from jmp_goods ");
            strSql.Append(" where g_id=@g_id");
            SqlParameter[] parameters = {
					new SqlParameter("@g_id", SqlDbType.Int,4)
			};
            parameters[0].Value = g_id;


            JMP.MDL.jmp_goods model = new JMP.MDL.jmp_goods();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["g_id"].ToString() != "")
                {
                    model.g_id = int.Parse(ds.Tables[0].Rows[0]["g_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["g_app_id"].ToString() != "")
                {
                    model.g_app_id = int.Parse(ds.Tables[0].Rows[0]["g_app_id"].ToString());
                }
                model.g_name = ds.Tables[0].Rows[0]["g_name"].ToString();
                if (ds.Tables[0].Rows[0]["g_saletype_id"].ToString() != "")
                {
                    model.g_saletype_id = int.Parse(ds.Tables[0].Rows[0]["g_saletype_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["g_price"].ToString() != "")
                {
                    model.g_price = decimal.Parse(ds.Tables[0].Rows[0]["g_price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["g_state"].ToString() != "")
                {
                    model.g_state = int.Parse(ds.Tables[0].Rows[0]["g_state"].ToString());
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
            strSql.Append(" FROM jmp_goods ");
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
            strSql.Append(" FROM jmp_goods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> SelectList(string sea_name, int type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.*,b.a_name,c.s_name from JMP_GOODS a left join JMP_APP b on a.g_app_id=b.a_id left join JMP_SALETYPE c on c.s_id=a.g_saletype_id  where 1=1");
            string Order = "order by g_id desc";
            if (type > 0)
            {
                switch (type)
                {
                    case 1:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and a.g_id like '%" + sea_name + "%' ";
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and a.g_name like '%" + sea_name + "%' ";
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and b.a_name like  '%" + sea_name + "%' ";
                        }
                        break;
                }

            }
            if (SelectState > -1)
            {
                sql += " and a.g_state='" + SelectState + "' ";
            }
            if (searchDesc == 0)
            {
                Order = " order by g_id desc ";
            }
            else
            {
                Order = " order by g_id   ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_goods>(ds.Tables[0]);
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> SelectListById(int id, string sea_name, int type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.*,b.a_name,c.s_name from JMP_GOODS a left join JMP_APP b on a.g_app_id=b.a_id left join JMP_SALETYPE c on c.s_id=a.g_saletype_id left join  jmp_user d on b.a_user_id=d.u_id where d.u_merchant_id='" + id + "'");
            string Order = "order by g_id desc";
            if (type > 0)
            {
                switch (type)
                {
                    case 1:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and a.g_id like '%" + sea_name + "%' ";
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and a.g_name like '%" + sea_name + "%' ";
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and b.a_name like  '%" + sea_name + "%' ";
                        }
                        break;
                }

            }
            if (SelectState > -1)
            {
                sql += " and a.g_state='" + SelectState + "' ";
            }
            if (searchDesc == 0)
            {
                Order = " order by g_id desc ";
            }
            else
            {
                Order = " order by g_id   ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_goods>(ds.Tables[0]);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_goods set g_state=" + state + "  ");
            strSql.Append(" where g_id in (" + u_idlist + ")  ");
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
        /// 局部试图根据应用id获取商品信息
        /// </summary>
        /// <param name="g_app_id">应用id</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> SelectListEj(int g_app_id)
        {
            string sql = string.Format(" select a.*,b.a_name,c.s_name from JMP_GOODS a left join JMP_APP b on a.g_app_id=b.a_id left join JMP_SALETYPE c on c.s_id=a.g_saletype_id  where 1=1 and a.g_app_id=@g_app_id ");
            SqlParameter par = new SqlParameter("@g_app_id", g_app_id);
            dt=DbHelperSQL.Query(sql,par).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_goods>(dt);
        }
        /// <summary>
        /// 用户根据应用id查询正常状态的商品
        /// </summary>
        /// <param name="g_app_id">应用id</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_goods> UserSelectList(int g_app_id)
        {
            string sql = string.Format(" select a.*,b.a_name,c.s_name from JMP_GOODS a left join JMP_APP b on a.g_app_id=b.a_id left join JMP_SALETYPE c on c.s_id=a.g_saletype_id  where 1=1 and a.g_app_id=@g_app_id and a.g_state='1' ");
            SqlParameter par = new SqlParameter("@g_app_id", g_app_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_goods>(dt);
        }
    }
}

