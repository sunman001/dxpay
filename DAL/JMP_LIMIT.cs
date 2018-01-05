using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///jmp_limit
    ///</summary>
    public partial class jmp_limit
    {

        public bool Exists(int l_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_limit");
            strSql.Append(" where ");
            strSql.Append(" l_id = @l_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_limit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_limit(");
            strSql.Append("l_name,l_topid,l_url,l_sort,l_state,l_icon,l_type");
            strSql.Append(") values (");
            strSql.Append("@l_name,@l_topid,@l_url,@l_sort,@l_state,@l_icon,@l_type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@l_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_topid", SqlDbType.Int,4) ,
                        new SqlParameter("@l_url", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_sort", SqlDbType.Int,4) ,
                        new SqlParameter("@l_state", SqlDbType.Int,4) ,
                        new SqlParameter("@l_icon", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.l_name;
            parameters[1].Value = model.l_topid;
            parameters[2].Value = model.l_url;
            parameters[3].Value = model.l_sort;
            parameters[4].Value = model.l_state;
            parameters[5].Value = model.l_icon;
            parameters[6].Value = model.l_type;

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
        public bool Update(JMP.MDL.jmp_limit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_limit set ");

            strSql.Append(" l_name = @l_name , ");
            strSql.Append(" l_topid = @l_topid , ");
            strSql.Append(" l_url = @l_url , ");
            strSql.Append(" l_sort = @l_sort , ");
            strSql.Append(" l_state = @l_state , ");
            strSql.Append(" l_icon = @l_icon , ");
            strSql.Append(" l_type = @l_type  ");
            strSql.Append(" where l_id=@l_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@l_id", SqlDbType.Int,4) ,
                        new SqlParameter("@l_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_topid", SqlDbType.Int,4) ,
                        new SqlParameter("@l_url", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_sort", SqlDbType.Int,4) ,
                        new SqlParameter("@l_state", SqlDbType.Int,4) ,
                        new SqlParameter("@l_icon", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.l_id;
            parameters[1].Value = model.l_name;
            parameters[2].Value = model.l_topid;
            parameters[3].Value = model.l_url;
            parameters[4].Value = model.l_sort;
            parameters[5].Value = model.l_state;
            parameters[6].Value = model.l_icon;
            parameters[7].Value = model.l_type;
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
        public bool Delete(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_limit ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


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
        public bool DeleteList(string l_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_limit ");
            strSql.Append(" where ID in (" + l_idlist + ")  ");
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
        public JMP.MDL.jmp_limit GetModel(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select l_id, l_name, l_topid, l_url, l_sort, l_state, l_icon, l_type  ");
            strSql.Append("  from jmp_limit ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


            JMP.MDL.jmp_limit model = new JMP.MDL.jmp_limit();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["l_id"].ToString() != "")
                {
                    model.l_id = int.Parse(ds.Tables[0].Rows[0]["l_id"].ToString());
                }
                model.l_name = ds.Tables[0].Rows[0]["l_name"].ToString();
                if (ds.Tables[0].Rows[0]["l_topid"].ToString() != "")
                {
                    model.l_topid = int.Parse(ds.Tables[0].Rows[0]["l_topid"].ToString());
                }
                model.l_url = ds.Tables[0].Rows[0]["l_url"].ToString();
                if (ds.Tables[0].Rows[0]["l_sort"].ToString() != "")
                {
                    model.l_sort = int.Parse(ds.Tables[0].Rows[0]["l_sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_state"].ToString() != "")
                {
                    model.l_state = int.Parse(ds.Tables[0].Rows[0]["l_state"].ToString());
                }
                model.l_icon = ds.Tables[0].Rows[0]["l_icon"].ToString();
                if (ds.Tables[0].Rows[0]["l_type"].ToString() != "")
                {
                    model.l_type = int.Parse(ds.Tables[0].Rows[0]["l_type"].ToString());
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
            strSql.Append("select l_id, l_name, l_topid, l_url, l_sort, l_state, l_icon, l_type ");
            strSql.Append(" FROM jmp_limit ");
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
            strSql.Append(" l_id, l_name, l_topid, l_url, l_sort, l_state, l_icon, l_type ");
            strSql.Append(" FROM jmp_limit ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询方法是否有权限
        /// </summary>
        /// <param name="voids">方法名（AddDialog()</param>
        /// <param name="locUserId">用户id</param>
        /// <param name="roleId">权限id</param>
        /// <returns></returns>
        public bool GetLocUserLimitVoids(string voids, string locUserId, int roleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select l_id,l_name,l_topid,l_url,l_sort,l_state,l_icon,l_type from JMP_LIMIT where l_id in (select * from dbo.f_split((select r_value from JMP_LOCUSER as a,JMP_ROLE as b  ");
            strSql.Append(" where a.u_id=" + locUserId + " and b.r_id=" + roleId + " and a.u_role_id=b.r_id and a.u_state=1 ),',')) and l_url like '%" + voids + "%' ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            int rows = ds.Tables[0].Rows.Count;
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public List<JMP.MDL.jmp_limit> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_limit>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新状态
        /// <param name="l_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// </summary>
        public bool UpdateLimitState(string l_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_LIMIT set l_state='" + state + "' ");
            strSql.Append(" where l_id in (" + l_idlist + ")  ");
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

        public string getrolelimit(int rid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_value from  jmp_role where r_id=" + rid);
            return DbHelperSQL.GetSingle(strSql.ToString()).ToString();

        }

        /// <summary>
        /// 查询单个值
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <returns></returns>
        public object GetSingle(string query)
        {
            return DbHelperSQL.GetSingle(query);
        }


        /// <summary>
        /// 根据URL路径查询对应的权限数据集
        /// </summary>
        /// <param name="path">URL路径</param>
        /// <returns></returns>
        public DataSet GetByUrlPath(string path)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM jmp_limit ");
            strSql.Append(" where l_url='" + path + "'");

            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

