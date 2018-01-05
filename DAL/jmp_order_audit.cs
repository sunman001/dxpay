using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using JMP.DBA;
using System.Collections.Generic;

namespace JMP.DAL
{
    ///<summary>
    ///订单异常核查表:异常订单监控数据记录表
    ///</summary>
    public class jmp_order_audit
    {
        DataTable dt = new DataTable();

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_order_audit");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(JMP.MDL.jmp_order_audit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_order_audit(");
            strSql.Append("order_amount,created_on,is_processed,processed_time,processed_by,processed_result,order_code,order_table_name,app_id,message,trade_no,payment_time,payment_amount,payment_status,is_send_message,message_send_time");
            strSql.Append(") values (");
            strSql.Append("@order_amount,@created_on,@is_processed,@processed_time,@processed_by,@processed_result,@order_code,@order_table_name,@app_id,@message,@trade_no,@payment_time,@payment_amount,@payment_status,@is_send_message,@message_send_time");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@order_amount", SqlDbType.Decimal,5) ,
                        new SqlParameter("@created_on", SqlDbType.DateTime) ,
                        new SqlParameter("@is_processed", SqlDbType.Int,4) ,
                        new SqlParameter("@processed_time", SqlDbType.DateTime) ,
                        new SqlParameter("@processed_by", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@processed_result", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@order_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@order_table_name", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@trade_no", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@payment_time", SqlDbType.DateTime) ,
                        new SqlParameter("@payment_amount", SqlDbType.Decimal,5) ,
                        new SqlParameter("@payment_status", SqlDbType.NVarChar,255),
                        new SqlParameter("@is_send_message", SqlDbType.Int,4) ,
                        new SqlParameter("@message_send_time", SqlDbType.DateTime)
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.order_amount;
            parameters[2].Value = model.created_on;
            parameters[3].Value = model.is_processed;
            parameters[4].Value = model.processed_time;
            parameters[5].Value = model.processed_by;
            parameters[6].Value = model.processed_result;
            parameters[7].Value = model.order_code;
            parameters[8].Value = model.order_table_name;
            parameters[9].Value = model.app_id;
            parameters[10].Value = model.message;
            parameters[11].Value = model.trade_no;
            parameters[12].Value = model.payment_time;
            parameters[13].Value = model.payment_amount;
            parameters[14].Value = model.payment_status;
            parameters[15].Value = model.is_send_message;
            parameters[16].Value = model.message_send_time;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_order_audit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_order_audit set ");

            strSql.Append(" id = @id , ");
            strSql.Append(" order_amount = @order_amount , ");
            strSql.Append(" created_on = @created_on , ");
            strSql.Append(" is_processed = @is_processed , ");
            strSql.Append(" processed_time = @processed_time , ");
            strSql.Append(" processed_by = @processed_by , ");
            strSql.Append(" processed_result = @processed_result , ");
            strSql.Append(" order_code = @order_code , ");
            strSql.Append(" order_table_name = @order_table_name , ");
            strSql.Append(" app_id = @app_id , ");
            strSql.Append(" message = @message , ");
            strSql.Append(" trade_no = @trade_no , ");
            strSql.Append(" payment_time = @payment_time , ");
            strSql.Append(" payment_amount = @payment_amount , ");
            strSql.Append(" payment_status = @payment_status , ");
            strSql.Append(" is_send_message = @is_send_message , ");
            strSql.Append(" message_send_time = @message_send_time  ");
            strSql.Append(" where id=@id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@order_amount", SqlDbType.Decimal,5) ,
                        new SqlParameter("@created_on", SqlDbType.DateTime) ,
                        new SqlParameter("@is_processed", SqlDbType.Int,4) ,
                        new SqlParameter("@processed_time", SqlDbType.DateTime) ,
                        new SqlParameter("@processed_by", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@processed_result", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@order_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@order_table_name", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@trade_no", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@payment_time", SqlDbType.DateTime) ,
                        new SqlParameter("@payment_amount", SqlDbType.Decimal,5) ,
                        new SqlParameter("@payment_status", SqlDbType.NVarChar,255),
                        new SqlParameter("@is_send_message", SqlDbType.Int,4) ,
                        new SqlParameter("@message_send_time", SqlDbType.DateTime)
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.order_amount;
            parameters[2].Value = model.created_on;
            parameters[3].Value = model.is_processed;
            parameters[4].Value = model.processed_time;
            parameters[5].Value = model.processed_by;
            parameters[6].Value = model.processed_result;
            parameters[7].Value = model.order_code;
            parameters[8].Value = model.order_table_name;
            parameters[9].Value = model.app_id;
            parameters[10].Value = model.message;
            parameters[11].Value = model.trade_no;
            parameters[12].Value = model.payment_time;
            parameters[13].Value = model.payment_amount;
            parameters[14].Value = model.payment_status;
            parameters[15].Value = model.is_send_message;
            parameters[16].Value = model.message_send_time;
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
            strSql.Append("delete from jmp_order_audit ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
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
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_order_audit GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, order_amount, created_on, is_processed, processed_time, processed_by, processed_result, order_code, order_table_name, app_id, message, trade_no, payment_time, payment_amount, payment_status,is_send_message,message_send_time");
            strSql.Append("  from jmp_order_audit ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
            parameters[0].Value = id;


            JMP.MDL.jmp_order_audit model = new JMP.MDL.jmp_order_audit();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_amount"].ToString() != "")
                {
                    model.order_amount = decimal.Parse(ds.Tables[0].Rows[0]["order_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["created_on"].ToString() != "")
                {
                    model.created_on = DateTime.Parse(ds.Tables[0].Rows[0]["created_on"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_processed"].ToString() != "")
                {
                    model.is_processed = int.Parse(ds.Tables[0].Rows[0]["is_processed"].ToString());
                }
                if (ds.Tables[0].Rows[0]["processed_time"].ToString() != "")
                {
                    model.processed_time = DateTime.Parse(ds.Tables[0].Rows[0]["processed_time"].ToString());
                }
                model.processed_by = ds.Tables[0].Rows[0]["processed_by"].ToString();
                model.processed_result = ds.Tables[0].Rows[0]["processed_result"].ToString();
                model.order_code = ds.Tables[0].Rows[0]["order_code"].ToString();
                model.order_table_name = ds.Tables[0].Rows[0]["order_table_name"].ToString();
                if (ds.Tables[0].Rows[0]["app_id"].ToString() != "")
                {
                    model.app_id = int.Parse(ds.Tables[0].Rows[0]["app_id"].ToString());
                }
                model.message = ds.Tables[0].Rows[0]["message"].ToString();
                model.trade_no = ds.Tables[0].Rows[0]["trade_no"].ToString();
                if (ds.Tables[0].Rows[0]["payment_time"].ToString() != "")
                {
                    model.payment_time = DateTime.Parse(ds.Tables[0].Rows[0]["payment_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_amount"].ToString() != "")
                {
                    model.payment_amount = decimal.Parse(ds.Tables[0].Rows[0]["payment_amount"].ToString());
                }
                model.payment_status = ds.Tables[0].Rows[0]["payment_status"].ToString();
                if (ds.Tables[0].Rows[0]["message_send_time"].ToString() != "")
                {
                    model.message_send_time = DateTime.Parse(ds.Tables[0].Rows[0]["message_send_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_send_message"].ToString() != "")
                {
                    model.is_send_message = int.Parse(ds.Tables[0].Rows[0]["is_send_message"].ToString());
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
            strSql.Append(" FROM jmp_order_audit ");
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
            strSql.Append(" FROM jmp_order_audit ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        /// <summary>
        /// 查询应该投诉管理
        /// </summary>
        /// <param name="userid">用户id（后台默认传0，开发者平台默认传用户id）</param>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order_audit> SelectList( string paymentstatus,string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.* ,b.a_name from  jmp_order_audit a  left join jmp_app b on a.app_id=b.a_id where 1=1");
            string Order = " Order by id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and b.a_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += "  and a.order_code like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += "  and a.order_table_name like '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += "  and a.trade_no like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        sql += "  and a.processed_by like '%" + sea_name + "%' ";
                        break;
                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            {
                sql += " and a.created_on>='" + stime + " 00:00:00' and a.created_on<='" + endtime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and a.is_processed='" + auditstate + "' ";
            }
            if (!string.IsNullOrEmpty(paymentstatus))
            {
                sql += " and a.payment_status='" + paymentstatus + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by id  ";
            }
            else
            {
                Order = " order by id desc ";
            }
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_order_audit>(ds.Tables[0]);
        }

        /// <summary>
        /// 处理订单异常
        /// </summary>
        /// <param name="uids">选择投诉的ID</param>
        /// <param name="remark">处理结果</param>
        /// <returns></returns>
        public bool OrderAuditLC(string uids, string remark, string r_auditor)
        {
            string processed_time = DateTime.Now.ToString();
            var strSql = string.Format("UPDATE  jmp_order_audit SET processed_result='{1}',is_processed='1', processed_by='{2}', processed_time='{3}' WHERE id IN ({0})", uids, remark, r_auditor,processed_time);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }

        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order_audit> DcSelectList(string sql)
        {
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_order_audit>(dt);
        }

        public int GetCount(string strWhere)
        {
            var sql = "SELECT COUNT(*) FROM jmp_order_audit";
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql = string.Format("{0} WHERE {1}", sql, strWhere);
            }
            var obj = DbHelperSQL.GetSingle(sql);
            var num = 0;
            try
            {
                num = int.Parse(obj.ToString());
            }
            catch { }
            return num;
        }
        /// <summary>
        /// 将未发送标识的设置为已发送
        /// </summary>
        /// <returns></returns>
        public int SetSentMessage()
        {
            var sql = "UPDATE jmp_order_audit SET is_send_message=1,message_send_time=getdate() WHERE is_send_message=0";

            var num = DbHelperSQL.ExecuteSql(sql);
            return num;
        }
    }
}

