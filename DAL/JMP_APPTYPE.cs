using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //应用类型表
    public partial class jmp_apptype
    {
        DataTable dt = new DataTable();
        public bool Exists(int t_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_apptype");
            strSql.Append(" where ");
            strSql.Append(" t_id = @t_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@t_id", SqlDbType.Int,4)
			};
            parameters[0].Value = t_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_apptype model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_apptype(");
            strSql.Append("t_name,t_sort,t_topid,t_state");
            strSql.Append(") values (");
            strSql.Append("@t_name,@t_sort,@t_topid,@t_state");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@t_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@t_sort", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@t_topid", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_state", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.t_name;
            parameters[1].Value = model.t_sort;
            parameters[2].Value = model.t_topid;
            parameters[3].Value = model.t_state;

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
        public bool Update(JMP.MDL.jmp_apptype model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_apptype set ");

            strSql.Append(" t_name = @t_name , ");
            strSql.Append(" t_sort = @t_sort , ");
            strSql.Append(" t_topid = @t_topid , ");
            strSql.Append(" t_state = @t_state  ");
            strSql.Append(" where t_id=@t_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@t_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@t_sort", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@t_topid", SqlDbType.Int,4) ,            
                        new SqlParameter("@t_state", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.t_id;
            parameters[1].Value = model.t_name;
            parameters[2].Value = model.t_sort;
            parameters[3].Value = model.t_topid;
            parameters[4].Value = model.t_state;
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
        public bool Delete(int t_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_apptype ");
            strSql.Append(" where t_id=@t_id");
            SqlParameter[] parameters = {
					new SqlParameter("@t_id", SqlDbType.Int,4)
			};
            parameters[0].Value = t_id;


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
        public bool DeleteList(string t_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_apptype ");
            strSql.Append(" where ID in (" + t_idlist + ")  ");
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
        public JMP.MDL.jmp_apptype GetModel(int t_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t_id, t_name, t_sort, t_topid, t_state  ");
            strSql.Append("  from jmp_apptype ");
            strSql.Append(" where t_id=@t_id");
            SqlParameter[] parameters = {
					new SqlParameter("@t_id", SqlDbType.Int,4)
			};
            parameters[0].Value = t_id;


            JMP.MDL.jmp_apptype model = new JMP.MDL.jmp_apptype();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["t_id"].ToString() != "")
                {
                    model.t_id = int.Parse(ds.Tables[0].Rows[0]["t_id"].ToString());
                }
                model.t_name = ds.Tables[0].Rows[0]["t_name"].ToString();
                model.t_sort = ds.Tables[0].Rows[0]["t_sort"].ToString();
                if (ds.Tables[0].Rows[0]["t_topid"].ToString() != "")
                {
                    model.t_topid = int.Parse(ds.Tables[0].Rows[0]["t_topid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["t_state"].ToString() != "")
                {
                    model.t_state = int.Parse(ds.Tables[0].Rows[0]["t_state"].ToString());
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
            strSql.Append(" FROM jmp_apptype ");
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
            strSql.Append(" FROM jmp_apptype ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 查询应用信息
        /// </summary>
        /// <param name="yylx">所属应用类型</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_apptype> SelectList(int yylx, string sea_name, string type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.*,isnull(b.t_name,'父级')as t_namecj from  JMP_APPTYPE  a  left join JMP_APPTYPE b on a.t_topid=b.t_id where 1=1 ");
            string Order = " order by t_sort desc ";
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "1":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and a.t_id like '%" + sea_name + "%' ";
                        }
                        break;
                    case "2":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and a.t_name like '%" + sea_name + "%' ";
                        }
                        break;
                }

            }
            if (SelectState > -1)
            {
                sql += " and a.t_state='" + SelectState + "' ";
            }
            if (yylx > -1)
            {
                sql += " and a.t_topid='" + yylx + "' ";
            }
            if (searchDesc == 0)
            {
                Order = "order by t_sort desc";
            }
            else
            {
                Order = "order by t_sort ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_apptype>(ds.Tables[0]);
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
            strSql.Append(" update JMP_APPTYPE set t_state=" + state + "  ");
            strSql.Append(" where t_id in (" + u_idlist + ")  ");
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

