using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //风险等级类型表
    public partial class jmp_risklevel
    {

        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_risklevel");
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
        public int Add(JMP.MDL.jmp_risklevel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_risklevel(");
            strSql.Append("r_name");
            strSql.Append(") values (");
            strSql.Append("@r_name");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@r_name", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.r_name;

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
        public bool Update(JMP.MDL.jmp_risklevel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_risklevel set ");

            strSql.Append(" r_name = @r_name  ");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@r_id", SqlDbType.Int,4) ,
                        new SqlParameter("@r_name", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_name;
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
            strSql.Append("delete from jmp_risklevel ");
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
            strSql.Append("delete from jmp_risklevel ");
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
        public JMP.MDL.jmp_risklevel GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id, r_name  ");
            strSql.Append("  from jmp_risklevel ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)
            };
            parameters[0].Value = r_id;


            JMP.MDL.jmp_risklevel model = new JMP.MDL.jmp_risklevel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                model.r_name = ds.Tables[0].Rows[0]["r_name"].ToString();

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
            strSql.Append(" FROM jmp_risklevel ");
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
            strSql.Append(" FROM jmp_risklevel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


    }
}

