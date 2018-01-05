using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///解密出错记录表
    ///</summary>
    public partial class jmp_aes
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_aes");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQLDEVICE.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_aes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_aes(");
            strSql.Append("yw,mw,mwdx,bdx,time");
            strSql.Append(") values (");
            strSql.Append("@yw,@mw,@mwdx,@bdx,@time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@yw", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@mw", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@mwdx", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@bdx", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.yw;
            parameters[1].Value = model.mw;
            parameters[2].Value = model.mwdx;
            parameters[3].Value = model.bdx;
            parameters[4].Value = model.time;

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
        public bool Update(JMP.MDL.jmp_aes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_aes set ");

            strSql.Append(" yw = @yw , ");
            strSql.Append(" mw = @mw , ");
            strSql.Append(" mwdx = @mwdx , ");
            strSql.Append(" bdx = @bdx , ");
            strSql.Append(" time = @time  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@yw", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@mw", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@mwdx", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@bdx", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.yw;
            parameters[2].Value = model.mw;
            parameters[3].Value = model.mwdx;
            parameters[4].Value = model.bdx;
            parameters[5].Value = model.time;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_aes ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_aes ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_aes GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, yw, mw, mwdx, bdx, time  ");
            strSql.Append("  from jmp_aes ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            JMP.MDL.jmp_aes model = new JMP.MDL.jmp_aes();
            DataSet ds = DbHelperSQLDEVICE.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.yw = ds.Tables[0].Rows[0]["yw"].ToString();
                model.mw = ds.Tables[0].Rows[0]["mw"].ToString();
                model.mwdx = ds.Tables[0].Rows[0]["mwdx"].ToString();
                model.bdx = ds.Tables[0].Rows[0]["bdx"].ToString();
                model.time = DateTime.Parse(ds.Tables[0].Rows[0]["time"].ToString());

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
            strSql.Append(" FROM jmp_aes ");
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
            strSql.Append(" FROM jmp_aes ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLDEVICE.Query(strSql.ToString());
        }


    }
}

