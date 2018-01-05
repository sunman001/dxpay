using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //风险等级表
    public partial class jmp_risklevelallocation
    {

        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_risklevelallocation");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)
            };
            parameters[0].Value = r_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_risklevelallocation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_risklevelallocation(");
            strSql.Append("r_apptypeid,r_risklevel,r_state");
            strSql.Append(") values (");
            strSql.Append("@r_apptypeid,@r_risklevel,@r_state");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@r_apptypeid", SqlDbType.Int,4) ,
                        new SqlParameter("@r_risklevel", SqlDbType.Int,4) ,
                        new SqlParameter("@r_state", SqlDbType.Int,4)

            };

            parameters[0].Value = model.r_apptypeid;
            parameters[1].Value = model.r_risklevel;
            parameters[2].Value = model.r_state;
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
        public bool Update(JMP.MDL.jmp_risklevelallocation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_risklevelallocation set ");

            strSql.Append(" r_apptypeid = @r_apptypeid , ");
            strSql.Append(" r_risklevel = @r_risklevel , ");
            strSql.Append(" r_state = @r_state  ");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@r_id", SqlDbType.Int,4) ,
                        new SqlParameter("@r_apptypeid", SqlDbType.Int,4) ,
                        new SqlParameter("@r_risklevel", SqlDbType.Int,4) ,
                        new SqlParameter("@r_state", SqlDbType.Int,4)

            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_apptypeid;
            parameters[2].Value = model.r_risklevel;
            parameters[3].Value = model.r_state;
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
        public bool Delete(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_risklevelallocation ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)
            };
            parameters[0].Value = r_id;


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
        public bool DeleteList(string r_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_risklevelallocation ");
            strSql.Append(" where ID in (" + r_idlist + ")  ");
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
        public JMP.MDL.jmp_risklevelallocation GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id, r_apptypeid,r_risklevel,r_state  ");
            strSql.Append("  from jmp_risklevelallocation ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)
            };
            parameters[0].Value = r_id;


            JMP.MDL.jmp_risklevelallocation model = new JMP.MDL.jmp_risklevelallocation();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_apptypeid"].ToString() != "")
                {
                    model.r_apptypeid = int.Parse(ds.Tables[0].Rows[0]["r_apptypeid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_risklevel"].ToString() != "")
                {
                    model.r_risklevel = int.Parse(ds.Tables[0].Rows[0]["r_risklevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_state"].ToString() != "")
                {
                    model.r_state = int.Parse(ds.Tables[0].Rows[0]["r_state"].ToString());
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
            strSql.Append(" FROM jmp_risklevelallocation ");
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
            strSql.Append(" FROM jmp_risklevelallocation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 列表分页查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndexs">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_risklevelallocation> SelectPage(string sql, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_risklevelallocation>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_risklevelallocation set r_state=" + state + "  ");
            strSql.Append(" where r_id in (" + u_idlist + ")  ");
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
        /// 根据应用子类型查询对应的应用的风险等级
        /// </summary>
        /// <param name="apptypeid">应用类型子id</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_risklevelallocation> SelectAppType(int apptypeid)
        {
            string sql = string.Format("  select a.r_id,a.r_apptypeid,a.r_risklevel,a.r_state,b.t_name,c.r_name from jmp_risklevelallocation a  left join  jmp_apptype b on a.r_apptypeid = b.t_id left join  jmp_risklevel c on a.r_risklevel = c.r_id where b.t_id=(select t_topid from jmp_apptype  where t_id ="+ apptypeid + " )   order by  r_id desc ");
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_risklevelallocation>(dt);
        }
        /// <summary>
        /// 根据rid字符串查询相关风控配置信息
        /// </summary>
        /// <param name="rid">rid字符串（1,2）</param>
        /// <returns></returns>
        public DataTable SelectRid(string rid) {
            string sql = "select a.r_id,a.r_apptypeid,a.r_risklevel,a.r_state,b.t_name,c.r_name from jmp_risklevelallocation a left join  jmp_apptype b on a.r_apptypeid = b.t_id left join  jmp_risklevel c on a.r_risklevel = c.r_id   where  a.r_state=0 and  a.r_id in(" + rid + ") order by a.r_id desc ";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }
    }
}

