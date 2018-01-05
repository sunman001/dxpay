using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///应用核查表:应用异常请求监控数据记录表
    ///</summary>
    public class jmp_app_request_audit
    {
        DataTable dt = new DataTable();
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_app_request_audit");
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
        public void Add(JMP.MDL.jmp_app_request_audit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_app_request_audit(");
            strSql.Append("app_id,app_name,message,created_on,is_processed,processed_time,processed_by,processed_result,is_send_message,message_send_time,type");
            strSql.Append(") values (");
            strSql.Append("@app_id,@app_name,@message,@created_on,@is_processed,@processed_time,@processed_by,@processed_result,@is_send_message,@message_send_time,@type");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@app_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@created_on", SqlDbType.DateTime) ,
                        new SqlParameter("@is_processed", SqlDbType.Int,4) ,
                        new SqlParameter("@processed_time", SqlDbType.DateTime) ,
                        new SqlParameter("@processed_by", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@processed_result", SqlDbType.NVarChar,-1),
                        new SqlParameter("@is_send_message", SqlDbType.Int,4) ,
                        new SqlParameter("@message_send_time", SqlDbType.DateTime),
                        new SqlParameter("@type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.app_id;
            parameters[1].Value = model.app_name;
            parameters[2].Value = model.message;
            parameters[3].Value = model.created_on;
            parameters[4].Value = model.is_processed;
            parameters[5].Value = model.processed_time;
            parameters[6].Value = model.processed_by;
            parameters[7].Value = model.processed_result;
            parameters[8].Value = model.is_send_message;
            parameters[9].Value = model.message_send_time;
            parameters[10].Value = model.type;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_app_request_audit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_app_request_audit set ");
            strSql.Append(" app_name = @app_name , ");
            strSql.Append(" message = @message , ");
            strSql.Append(" created_on = @created_on , ");
            strSql.Append(" is_processed = @is_processed , ");
            strSql.Append(" processed_time = @processed_time , ");
            strSql.Append(" processed_by = @processed_by , ");
            strSql.Append(" processed_result = @processed_result,");
            strSql.Append(" is_send_message = @is_send_message,");
            strSql.Append(" message_send_time = @message_send_time");
            strSql.Append(" type = @type");
            strSql.Append(" where id=@id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@app_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@message", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@created_on", SqlDbType.DateTime) ,
                        new SqlParameter("@is_processed", SqlDbType.Int,4) ,
                        new SqlParameter("@processed_time", SqlDbType.DateTime) ,
                        new SqlParameter("@processed_by", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@processed_result", SqlDbType.NVarChar,-1),
                        new SqlParameter("@is_send_message", SqlDbType.Int,4) ,
                        new SqlParameter("@message_send_time", SqlDbType.DateTime),
                        new SqlParameter("@type", SqlDbType.Int,4)
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.app_id;
            parameters[2].Value = model.app_name;
            parameters[3].Value = model.message;
            parameters[4].Value = model.created_on;
            parameters[5].Value = model.is_processed;
            parameters[6].Value = model.processed_time;
            parameters[7].Value = model.processed_by;
            parameters[8].Value = model.processed_result;
            parameters[9].Value = model.is_send_message;
            parameters[10].Value = model.message_send_time;
            parameters[11].Value = model.type;
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
            strSql.Append("delete from jmp_app_request_audit ");
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
        public JMP.MDL.jmp_app_request_audit GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, app_id, app_name, message, created_on, is_processed, processed_time, processed_by, processed_result,is_send_message,message_send_time,type");
            strSql.Append("  from jmp_app_request_audit ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
            parameters[0].Value = id;


            JMP.MDL.jmp_app_request_audit model = new JMP.MDL.jmp_app_request_audit();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["app_id"].ToString() != "")
                {
                    model.app_id = int.Parse(ds.Tables[0].Rows[0]["app_id"].ToString());
                }
                model.app_name = ds.Tables[0].Rows[0]["app_name"].ToString();
                model.message = ds.Tables[0].Rows[0]["message"].ToString();
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
                if (ds.Tables[0].Rows[0]["message_send_time"].ToString() != "")
                {
                    model.message_send_time = DateTime.Parse(ds.Tables[0].Rows[0]["message_send_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_send_message"].ToString() != "")
                {
                    model.is_send_message = int.Parse(ds.Tables[0].Rows[0]["is_send_message"].ToString());
                }
                if (ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
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
            strSql.Append(" FROM jmp_app_request_audit ");
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
            strSql.Append(" FROM jmp_app_request_audit ");
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
        public List<JMP.MDL.jmp_app_request_audit> SelectList(string sqls, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
            SqlParameter[] s = new[] {
                  new SqlParameter("@sqlstr",sql.ToString()),
                  new SqlParameter("@pageIndex",pageIndexs),
                  new SqlParameter("@pageSize",PageSize)
            };
            SqlDataReader reader = DbHelperSQL.RunProcedure("page", s);
            pageCount = 0;
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    pageCount = Convert.ToInt32(reader[0].ToString());
                }
                if (reader.NextResult())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            reader.Close();
            return DbHelperSQL.ToList<JMP.MDL.jmp_app_request_audit>(dt);
        }

        public List<JMP.MDL.jmp_app_request_audit> SelectList( string typelcass, string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from  jmp_app_request_audit where 1=1");
            string Order = " Order by id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and app_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += "  and processed_by like '%" + sea_name + "%' ";
                        break;
                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            {
                sql += " and created_on>='" + stime + " 00:00:00' and created_on<='" + endtime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(typelcass))
            {
                sql += " and type='" + typelcass + "' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and is_processed='" + auditstate + "' ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_app_request_audit>(ds.Tables[0]);
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
            var strSql = string.Format("UPDATE  jmp_app_request_audit SET processed_result='{1}',is_processed='1', processed_by='{2}', processed_time='{3}' WHERE id IN ({0})", uids, remark, r_auditor, processed_time);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }
        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app_request_audit> DcSelectList(string sql)
        {
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_app_request_audit>(dt);
        }


        public int GetCount(string strWhere)
        {
            var sql = "SELECT COUNT(*) FROM jmp_app_request_audit";
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
            var sql = "UPDATE jmp_app_request_audit SET is_send_message=1,message_send_time=GETDATE() WHERE is_send_message=0";

            var num = DbHelperSQL.ExecuteSql(sql);
            return num;
        }

        /// <summary>
        /// 将未发送标识的设置为已发送
        /// </summary>
        /// <param name="monitorType">监控类型[0:应用支付成功率,1:应用无订单,2:应用金额成功率,20:通道无订单]</param>
        /// <returns></returns>
        public int SetSentMessage(int monitorType)
        {
            var sql = string.Format("UPDATE jmp_app_request_audit SET is_send_message=1,message_send_time=GETDATE() WHERE type={0} AND is_send_message=0", monitorType);

            var num = DbHelperSQL.ExecuteSql(sql);
            return num;
        }
    }
}

