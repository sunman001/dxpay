
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///查询记录表
    ///</summary>
    public partial class jmp_query
    {

        public bool Exists(int q_id, string q_code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_query");
            strSql.Append(" where ");
            strSql.Append(" q_id = @q_id and  ");
            strSql.Append(" q_code = @q_code  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@q_id", SqlDbType.Int,4),
                    new SqlParameter("@q_code", SqlDbType.NVarChar,50)          };
            parameters[0].Value = q_id;
            parameters[1].Value = q_code;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_query model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_query(");
            strSql.Append("q_code,q_time");
            strSql.Append(") values (");
            strSql.Append("@q_code,@q_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@q_code", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@q_time", SqlDbType.Int,4)

            };

            parameters[0].Value = model.q_code;
            parameters[1].Value = model.q_time;

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
        public bool Update(JMP.MDL.jmp_query model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_query set ");

            strSql.Append(" q_code = @q_code , ");
            strSql.Append(" q_time = @q_time  ");
            strSql.Append(" where q_id=@q_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@q_id", SqlDbType.Int,4) ,
                        new SqlParameter("@q_code", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@q_time", SqlDbType.Int,4)

            };

            parameters[0].Value = model.q_id;
            parameters[1].Value = model.q_code;
            parameters[2].Value = model.q_time;
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
        public bool Delete(int q_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_query ");
            strSql.Append(" where q_id=@q_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@q_id", SqlDbType.Int,4)
            };
            parameters[0].Value = q_id;


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
        public bool DeleteList(string q_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_query ");
            strSql.Append(" where ID in (" + q_idlist + ")  ");
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
        public JMP.MDL.jmp_query GetModel(int q_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select q_id, q_code, q_time  ");
            strSql.Append("  from jmp_query ");
            strSql.Append(" where q_id=@q_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@q_id", SqlDbType.Int,4)
            };
            parameters[0].Value = q_id;


            JMP.MDL.jmp_query model = new JMP.MDL.jmp_query();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["q_id"].ToString() != "")
                {
                    model.q_id = int.Parse(ds.Tables[0].Rows[0]["q_id"].ToString());
                }
                model.q_code = ds.Tables[0].Rows[0]["q_code"].ToString();
                if (ds.Tables[0].Rows[0]["q_time"].ToString() != "")
                {
                    model.q_time = int.Parse(ds.Tables[0].Rows[0]["q_time"].ToString());
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
            strSql.Append(" FROM jmp_query ");
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
            strSql.Append(" FROM jmp_query ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
       
        /// <summary>
        /// 根据订单编号查询信息
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <returns></returns>
        public JMP.MDL.jmp_query SelectCode(string code)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT * FROM  jmp_query WHERE q_code=@code ";
            SqlParameter[] par ={ new SqlParameter("@code",@code)
                                };

            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_query>(dt);
        }

    }
}
