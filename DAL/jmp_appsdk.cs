using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    ///<summary>
    ///应用上传
    ///</summary>
    public partial class jmp_appsdk
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_appsdk");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_appsdk model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_appsdk(");
            strSql.Append("appid,appurl,uptimes");
            strSql.Append(") values (");
            strSql.Append("@appid,@appurl,@uptimes");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@appid", SqlDbType.Int,4) ,            
                        new SqlParameter("@appurl", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@uptimes", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.appid;
            parameters[1].Value = model.appurl;
            parameters[2].Value = model.uptimes;

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
        public bool Update(JMP.MDL.jmp_appsdk model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_appsdk set ");

            strSql.Append(" appid = @appid , ");
            strSql.Append(" appurl = @appurl , ");
            strSql.Append(" uptimes = @uptimes  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@appid", SqlDbType.Int,4) ,            
                        new SqlParameter("@appurl", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@uptimes", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.appid;
            parameters[2].Value = model.appurl;
            parameters[3].Value = model.uptimes;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_appsdk ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_appsdk ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_appsdk GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, appid, appurl, uptimes  ");
            strSql.Append("  from jmp_appsdk ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            JMP.MDL.jmp_appsdk model = new JMP.MDL.jmp_appsdk();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["appid"].ToString() != "")
                {
                    model.appid = int.Parse(ds.Tables[0].Rows[0]["appid"].ToString());
                }
                model.appurl = ds.Tables[0].Rows[0]["appurl"].ToString();
                if (ds.Tables[0].Rows[0]["uptimes"].ToString() != "")
                {
                    model.uptimes = DateTime.Parse(ds.Tables[0].Rows[0]["uptimes"].ToString());
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
            strSql.Append(" FROM jmp_appsdk ");
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
            strSql.Append(" FROM jmp_appsdk ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据应用id查询上传信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_appsdk SelectModel(int appid)
        {
            string sql = string.Format("  select a.*,b.a_name,b.a_auditstate,b.a_platform_id from jmp_appsdk a left join jmp_app b  on a.appid=b.a_id where 1=1 and a.appid=@appid ");
            SqlParameter par = new SqlParameter("@appid", appid);
            DataTable dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_appsdk>(dt);

        }

    }
}
