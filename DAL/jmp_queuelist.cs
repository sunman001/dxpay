using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///通知队列表
    ///</summary>
    public partial class jmp_queuelist
    {

        public bool Exists(int q_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_queuelist");
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
        public int Add(JMP.MDL.jmp_queuelist model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_queuelist(");
            strSql.Append("trade_time,trade_price,trade_paycode,trade_code,trade_no,q_privateinfo,q_uersid,q_address,q_sign,q_noticestate,q_times,q_noticetimes,q_tablename,q_o_id,trade_type");
            strSql.Append(") values (");
            strSql.Append("@trade_time,@trade_price,@trade_paycode,@trade_code,@trade_no,@q_privateinfo,@q_uersid,@q_address,@q_sign,@q_noticestate,@q_times,@q_noticetimes,@q_tablename,@q_o_id,@trade_type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@trade_time", SqlDbType.DateTime) ,
                        new SqlParameter("@trade_price", SqlDbType.Money,8) ,
                        new SqlParameter("@trade_paycode", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@trade_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@trade_no", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_privateinfo", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_uersid", SqlDbType.Int,4) ,
                        new SqlParameter("@q_address", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_sign", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_noticestate", SqlDbType.Int,4) ,
                        new SqlParameter("@q_times", SqlDbType.Int,4) ,
                        new SqlParameter("@q_noticetimes", SqlDbType.DateTime) ,
                        new SqlParameter("@q_tablename", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_o_id", SqlDbType.Int,4) ,
                        new SqlParameter("@trade_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.trade_time;
            parameters[1].Value = model.trade_price;
            parameters[2].Value = model.trade_paycode;
            parameters[3].Value = model.trade_code;
            parameters[4].Value = model.trade_no;
            parameters[5].Value = model.q_privateinfo;
            parameters[6].Value = model.q_uersid;
            parameters[7].Value = model.q_address;
            parameters[8].Value = model.q_sign;
            parameters[9].Value = model.q_noticestate;
            parameters[10].Value = model.q_times;
            parameters[11].Value = model.q_noticetimes;
            parameters[12].Value = model.q_tablename;
            parameters[13].Value = model.q_o_id;
            parameters[14].Value = model.trade_type;

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
        public bool Update(JMP.MDL.jmp_queuelist model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_queuelist set ");

            strSql.Append(" trade_time = @trade_time , ");
            strSql.Append(" trade_price = @trade_price , ");
            strSql.Append(" trade_paycode = @trade_paycode , ");
            strSql.Append(" trade_code = @trade_code , ");
            strSql.Append(" trade_no = @trade_no , ");
            strSql.Append(" q_privateinfo = @q_privateinfo , ");
            strSql.Append(" q_uersid = @q_uersid , ");
            strSql.Append(" q_address = @q_address , ");
            strSql.Append(" q_sign = @q_sign , ");
            strSql.Append(" q_noticestate = @q_noticestate , ");
            strSql.Append(" q_times = @q_times , ");
            strSql.Append(" q_noticetimes = @q_noticetimes , ");
            strSql.Append(" q_tablename = @q_tablename , ");
            strSql.Append(" q_o_id = @q_o_id , ");
            strSql.Append(" trade_type = @trade_type  ");
            strSql.Append(" where q_id=@q_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@q_id", SqlDbType.Int,4) ,
                        new SqlParameter("@trade_time", SqlDbType.DateTime) ,
                        new SqlParameter("@trade_price", SqlDbType.Money,8) ,
                        new SqlParameter("@trade_paycode", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@trade_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@trade_no", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_privateinfo", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_uersid", SqlDbType.Int,4) ,
                        new SqlParameter("@q_address", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_sign", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_noticestate", SqlDbType.Int,4) ,
                        new SqlParameter("@q_times", SqlDbType.Int,4) ,
                        new SqlParameter("@q_noticetimes", SqlDbType.DateTime) ,
                        new SqlParameter("@q_tablename", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@q_o_id", SqlDbType.Int,4) ,
                        new SqlParameter("@trade_type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.q_id;
            parameters[1].Value = model.trade_time;
            parameters[2].Value = model.trade_price;
            parameters[3].Value = model.trade_paycode;
            parameters[4].Value = model.trade_code;
            parameters[5].Value = model.trade_no;
            parameters[6].Value = model.q_privateinfo;
            parameters[7].Value = model.q_uersid;
            parameters[8].Value = model.q_address;
            parameters[9].Value = model.q_sign;
            parameters[10].Value = model.q_noticestate;
            parameters[11].Value = model.q_times;
            parameters[12].Value = model.q_noticetimes;
            parameters[13].Value = model.q_tablename;
            parameters[14].Value = model.q_o_id;
            parameters[15].Value = model.trade_type;
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
            strSql.Append("delete from jmp_queuelist ");
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
            strSql.Append("delete from jmp_queuelist ");
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
        public JMP.MDL.jmp_queuelist GetModel(int q_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select q_id, trade_time, trade_price, trade_paycode, trade_code, trade_no, q_privateinfo, q_uersid, q_address, q_sign, q_noticestate, q_times, q_noticetimes, q_tablename, q_o_id, trade_type  ");
            strSql.Append("  from jmp_queuelist ");
            strSql.Append(" where q_id=@q_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@q_id", SqlDbType.Int,4)
            };
            parameters[0].Value = q_id;


            JMP.MDL.jmp_queuelist model = new JMP.MDL.jmp_queuelist();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["q_id"].ToString() != "")
                {
                    model.q_id = int.Parse(ds.Tables[0].Rows[0]["q_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["trade_time"].ToString() != "")
                {
                    model.trade_time = DateTime.Parse(ds.Tables[0].Rows[0]["trade_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["trade_price"].ToString() != "")
                {
                    model.trade_price = decimal.Parse(ds.Tables[0].Rows[0]["trade_price"].ToString());
                }
                model.trade_paycode = ds.Tables[0].Rows[0]["trade_paycode"].ToString();
                model.trade_code = ds.Tables[0].Rows[0]["trade_code"].ToString();
                model.trade_no = ds.Tables[0].Rows[0]["trade_no"].ToString();
                model.q_privateinfo = ds.Tables[0].Rows[0]["q_privateinfo"].ToString();
                if (ds.Tables[0].Rows[0]["q_uersid"].ToString() != "")
                {
                    model.q_uersid = int.Parse(ds.Tables[0].Rows[0]["q_uersid"].ToString());
                }
                model.q_address = ds.Tables[0].Rows[0]["q_address"].ToString();
                model.q_sign = ds.Tables[0].Rows[0]["q_sign"].ToString();
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
                if (ds.Tables[0].Rows[0]["q_o_id"].ToString() != "")
                {
                    model.q_o_id = int.Parse(ds.Tables[0].Rows[0]["q_o_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["trade_type"].ToString() != "")
                {
                    model.trade_type = int.Parse(ds.Tables[0].Rows[0]["trade_type"].ToString());
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
            strSql.Append(" FROM jmp_queuelist ");
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
            strSql.Append(" FROM jmp_queuelist ");
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
        /// <param name="code"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_queuelist SelectGetModel(string code)
        {

            string sql = string.Format(" select q_id, trade_time, trade_price, trade_paycode, trade_code, trade_no, q_privateinfo, q_uersid, q_address, q_sign, q_noticestate, q_times, q_noticetimes, q_tablename, q_o_id, trade_type from jmp_queuelist where trade_code=@code ");
            SqlParameter parameters = new SqlParameter("@code", code);
            DataTable dt = DbHelperSQL.Query(sql, parameters).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_queuelist>(dt);
        }
        /// <summary>
        /// 跟订单号修改通知次数
        /// </summary>
        /// <param name="code">订单号</param>
        /// <param name="q_times">通知次数</param>
        /// <returns></returns>
        public int UpdateOrder(string code, int q_times)
        {

            string sql = string.Format(" UPDATE dbo.jmp_queuelist SET q_times=@q_times WHERE trade_code=@code ");
            SqlParameter[] parameters ={
                 new SqlParameter("@code",code),
                 new SqlParameter("@q_times",q_times)
            };
            int rows = 0;
            rows = DbHelperSQL.ExecuteSql(sql, parameters);

            return rows;
        }

    }
}

