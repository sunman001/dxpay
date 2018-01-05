using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //活跃终端表
    public partial class jmp_liveteral
    {

        public bool Exists(int l_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_liveteral");
            strSql.Append(" where ");
            strSql.Append(" l_id = @l_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;

            return DbHelperSQLDEVICE.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_liveteral model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_liveteral(");
            strSql.Append("l_teral_key,l_time,l_appid");
            strSql.Append(") values (");
            strSql.Append("@l_teral_key,@l_time,@l_appid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@l_teral_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_time", SqlDbType.DateTime) ,
                        new SqlParameter("@l_appid",SqlDbType.Int,4)

            };

            parameters[0].Value = model.l_teral_key;
            parameters[1].Value = model.l_time;
            parameters[2].Value = model.l_appid;

            object obj = DbHelperSQLDEVICE.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(JMP.MDL.jmp_liveteral model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_liveteral set ");

            strSql.Append(" l_teral_key = @l_teral_key , ");
            strSql.Append(" l_time = @l_time,l_appid=@l_appid  ");
            strSql.Append(" where l_id=@l_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@l_id", SqlDbType.Int,4) ,
                        new SqlParameter("@l_teral_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_time", SqlDbType.DateTime),
                        new SqlParameter("@l_appid",SqlDbType.Int,4)

            };

            parameters[0].Value = model.l_id;
            parameters[1].Value = model.l_teral_key;
            parameters[2].Value = model.l_time;
            parameters[3].Value = model.l_appid;
            int rows = DbHelperSQLDEVICE.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_liveteral ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


            int rows = DbHelperSQLDEVICE.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from jmp_liveteral ");
            strSql.Append(" where ID in (" + l_idlist + ")  ");
            int rows = DbHelperSQLDEVICE.ExecuteSql(strSql.ToString());
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
        public JMP.MDL.jmp_liveteral GetModel(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select l_id, l_teral_key, l_time,l_appid  ");
            strSql.Append("  from jmp_liveteral ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


            JMP.MDL.jmp_liveteral model = new JMP.MDL.jmp_liveteral();
            DataSet ds = DbHelperSQLDEVICE.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["l_id"].ToString() != "")
                {
                    model.l_id = int.Parse(ds.Tables[0].Rows[0]["l_id"].ToString());
                }
                model.l_teral_key = ds.Tables[0].Rows[0]["l_teral_key"].ToString();
                if (ds.Tables[0].Rows[0]["l_time"].ToString() != "")
                {
                    model.l_time = DateTime.Parse(ds.Tables[0].Rows[0]["l_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_appid"].ToString() != "")
                {
                    model.l_appid = int.Parse(ds.Tables[0].Rows[0]["l_appid"].ToString());
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
            strSql.Append(" FROM jmp_liveteral ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQLDEVICE.Query(strSql.ToString());
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
            strSql.Append(" FROM jmp_liveteral ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLDEVICE.Query(strSql.ToString());
        }


    }
}

