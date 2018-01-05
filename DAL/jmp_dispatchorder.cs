using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///调单设置
    ///</summary>
    public partial class jmp_dispatchorder
    {
        DataTable dt = new DataTable();
        public bool Exists(int d_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_dispatchorder");
            strSql.Append(" where ");
            strSql.Append(" d_id = @d_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@d_id", SqlDbType.Int,4)
            };
            parameters[0].Value = d_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_dispatchorder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_dispatchorder(");
            strSql.Append("d_apptyeid,d_ratio,d_state,d_datatime");
            strSql.Append(") values (");
            strSql.Append("@d_apptyeid,@d_ratio,@d_state,@d_datatime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@d_apptyeid", SqlDbType.Int,4) ,
                        new SqlParameter("@d_ratio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@d_state", SqlDbType.Int,4) ,
                        new SqlParameter("@d_datatime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.d_apptyeid;
            parameters[1].Value = model.d_ratio;
            parameters[2].Value = model.d_state;
            parameters[3].Value = model.d_datatime;

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
        public bool Update(JMP.MDL.jmp_dispatchorder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_dispatchorder set ");

            strSql.Append(" d_apptyeid = @d_apptyeid , ");
            strSql.Append(" d_ratio = @d_ratio , ");
            strSql.Append(" d_state = @d_state , ");
            strSql.Append(" d_datatime = @d_datatime  ");
            strSql.Append(" where d_id=@d_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@d_id", SqlDbType.Int,4) ,
                        new SqlParameter("@d_apptyeid", SqlDbType.Int,4) ,
                        new SqlParameter("@d_ratio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@d_state", SqlDbType.Int,4) ,
                        new SqlParameter("@d_datatime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.d_id;
            parameters[1].Value = model.d_apptyeid;
            parameters[2].Value = model.d_ratio;
            parameters[3].Value = model.d_state;
            parameters[4].Value = model.d_datatime;
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
        public bool Delete(int d_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_dispatchorder ");
            strSql.Append(" where d_id=@d_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@d_id", SqlDbType.Int,4)
            };
            parameters[0].Value = d_id;


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
        public bool DeleteList(string d_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_dispatchorder ");
            strSql.Append(" where ID in (" + d_idlist + ")  ");
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
        public JMP.MDL.jmp_dispatchorder GetModel(int d_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select d_id, d_apptyeid, d_ratio, d_state, d_datatime  ");
            strSql.Append("  from jmp_dispatchorder ");
            strSql.Append(" where d_id=@d_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@d_id", SqlDbType.Int,4)
            };
            parameters[0].Value = d_id;


            JMP.MDL.jmp_dispatchorder model = new JMP.MDL.jmp_dispatchorder();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["d_id"].ToString() != "")
                {
                    model.d_id = int.Parse(ds.Tables[0].Rows[0]["d_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_apptyeid"].ToString() != "")
                {
                    model.d_apptyeid = int.Parse(ds.Tables[0].Rows[0]["d_apptyeid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_ratio"].ToString() != "")
                {
                    model.d_ratio = decimal.Parse(ds.Tables[0].Rows[0]["d_ratio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_state"].ToString() != "")
                {
                    model.d_state = int.Parse(ds.Tables[0].Rows[0]["d_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_datatime"].ToString() != "")
                {
                    model.d_datatime = DateTime.Parse(ds.Tables[0].Rows[0]["d_datatime"].ToString());
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
            strSql.Append(" FROM jmp_dispatchorder ");
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
            strSql.Append(" FROM jmp_dispatchorder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序类型（0：升序，1：降序）</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_dispatchorder> SelectPager(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_dispatchorder>(ds.Tables[0]);
        }
        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_dispatchorder set d_state=" + state + "  ");
            strSql.Append(" where d_id in (" + u_idlist + ")  ");
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
        /// 查询调单比列
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public object Selectddbl(int appid)
        {
            string sql = string.Format("select d_ratio from jmp_dispatchorder where [d_state]=0 and d_apptyeid=(select [t_topid] from jmp_apptype where [t_id]=(SELECT a_apptype_id FROM jmp_app WHERE a_id=@appid )) ");
            SqlParameter[] parameters = {
                    new SqlParameter("@appid", SqlDbType.Int,4)
            };
            parameters[0].Value = appid;
            object obj = DbHelperSQL.GetSingle(sql.ToString(), parameters);
            return obj;
        }
    }
}

