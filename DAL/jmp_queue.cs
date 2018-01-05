using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///订单通知队列表
    ///</summary>
    public partial class jmp_queue
    {

        public bool Exists(int q_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_queue");
            strSql.Append(" where ");
            strSql.Append(" q_id = @q_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@q_id", SqlDbType.Int,4)
			};
            parameters[0].Value = q_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_queue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_queue(");
            strSql.Append("q_order_code,q_bizcode,q_address,q_noticestate,q_times,q_noticetimes,q_tablename,q_sign,q_privateinfo");
            strSql.Append(") values (");
            strSql.Append("@q_order_code,@q_bizcode,@q_address,@q_noticestate,@q_times,@q_noticetimes,@q_tablename,@q_sign,@q_privateinfo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@q_order_code", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@q_bizcode", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@q_address", SqlDbType.NVarChar,-1) ,  
                        new SqlParameter("@q_noticestate", SqlDbType.Int,4) ,            
                        new SqlParameter("@q_times", SqlDbType.Int,4) ,            
                        new SqlParameter("@q_noticetimes", SqlDbType.DateTime) ,            
                        new SqlParameter("@q_tablename", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_sign", SqlDbType.NVarChar,-1),
                        new SqlParameter("@q_privateinfo",SqlDbType.NVarChar,-1)
              
            };

            parameters[0].Value = model.q_order_code;
            parameters[1].Value = model.q_bizcode;
            parameters[2].Value = model.q_address;
            parameters[3].Value = model.q_noticestate;
            parameters[4].Value = model.q_times;
            parameters[5].Value = model.q_noticetimes;
            parameters[6].Value = model.q_tablename;
            parameters[7].Value = model.q_sign;
            parameters[8].Value = model.q_privateinfo;

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
        /// 删除一条数据
        /// </summary>
        public bool Deleteall()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_queue ");
            strSql.Append(" where q_noticestate != 0 ");
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
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_queue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_queue set ");

            strSql.Append(" q_order_code = @q_order_code , ");
            strSql.Append(" q_bizcode = @q_bizcode , ");
            strSql.Append(" q_address = @q_address , ");
            strSql.Append(" q_noticestate = @q_noticestate , ");
            strSql.Append(" q_times = @q_times , ");
            strSql.Append(" q_noticetimes = @q_noticetimes , ");
            strSql.Append(" q_sign = @q_sign , ");
            strSql.Append(" q_tablename = @q_tablename,  ");
            strSql.Append(" q_privateinfo = @q_privateinfo  ");
            strSql.Append(" where q_id=@q_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@q_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@q_order_code", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@q_bizcode", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@q_address", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@q_noticestate", SqlDbType.Int,4) ,            
                        new SqlParameter("@q_times", SqlDbType.Int,4) ,            
                        new SqlParameter("@q_noticetimes", SqlDbType.DateTime) ,            
                        new SqlParameter("@q_tablename", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_sign", SqlDbType.NVarChar,-1),
                        new SqlParameter("@q_privateinfo",SqlDbType.NVarChar,-1)
              
            };

            parameters[0].Value = model.q_id;
            parameters[1].Value = model.q_order_code;
            parameters[2].Value = model.q_bizcode;
            parameters[3].Value = model.q_address;
            parameters[4].Value = model.q_noticestate;
            parameters[5].Value = model.q_times;
            parameters[6].Value = model.q_noticetimes;
            parameters[7].Value = model.q_tablename;
            parameters[8].Value = model.q_sign;
            parameters[9].Value = model.q_privateinfo;
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
            strSql.Append("delete from jmp_queue ");
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
            strSql.Append("delete from jmp_queue ");
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
        public JMP.MDL.jmp_queue GetModel(int q_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select q_id, q_order_code, q_bizcode, q_address, q_noticestate, q_times, q_noticetimes, q_tablename ,q_sign,q_privateinfo ");
            strSql.Append("  from jmp_queue ");
            strSql.Append(" where q_id=@q_id");
            SqlParameter[] parameters = {
					new SqlParameter("@q_id", SqlDbType.Int,4)
			};
            parameters[0].Value = q_id;


            JMP.MDL.jmp_queue model = new JMP.MDL.jmp_queue();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["q_id"].ToString() != "")
                {
                    model.q_id = int.Parse(ds.Tables[0].Rows[0]["q_id"].ToString());
                }
                model.q_order_code = ds.Tables[0].Rows[0]["q_order_code"].ToString();
                model.q_bizcode = ds.Tables[0].Rows[0]["q_bizcode"].ToString();
                model.q_address = ds.Tables[0].Rows[0]["q_address"].ToString();
                if (ds.Tables[0].Rows[0]["q_noticestate"].ToString() != "")
                {
                    model.q_noticestate = int.Parse(ds.Tables[0].Rows[0]["q_noticestate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["q_times"].ToString() != "")
                {
                    model.q_times = int.Parse(ds.Tables[0].Rows[0]["q_times"].ToString());
                }
                if (ds.Tables[0].Rows[0]["q_noticetimes"].ToString() != "")
                {
                    model.q_noticetimes = DateTime.Parse(ds.Tables[0].Rows[0]["q_noticetimes"].ToString());
                }
                model.q_tablename = ds.Tables[0].Rows[0]["q_tablename"].ToString();
                model.q_sign = ds.Tables[0].Rows[0]["q_sign"].ToString();
                model.q_privateinfo = ds.Tables[0].Rows[0]["q_privateinfo"].ToString();
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
            strSql.Append("select [q_id] ,[q_order_code],[q_bizcode],[q_address] ,[q_sign] ,[q_noticestate],[q_times],[q_noticetimes],[q_tablename],[q_privateinfo] ");
            strSql.Append(" FROM jmp_queue ");
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
            strSql.Append(" FROM jmp_queue ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

