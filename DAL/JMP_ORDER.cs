using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace JMP.DAL
{
    //订单表
    public partial class jmp_order
    {
        DataTable dt = new DataTable();
        public bool Exists(int o_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_order");
            strSql.Append(" where ");
            strSql.Append(" o_id = @o_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@o_id", SqlDbType.Int,4)
            };
            parameters[0].Value = o_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_order model, string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TableName + "(");
            strSql.Append("o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress");
            strSql.Append(") values (");
            strSql.Append("@o_code,@o_bizcode,@o_tradeno,@o_paymode_id,@o_app_id,@o_goodsname,@o_term_key,@o_price,@o_payuser,@o_ctime,@o_ptime,@o_state,@o_times,@o_address,@o_noticestate,@o_noticetimes,@o_privateinfo,@o_interface_id,@o_showaddress");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@o_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_bizcode", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_tradeno", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_paymode_id", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@o_goodsname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_term_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_price", SqlDbType.Money,8) ,
                        new SqlParameter("@o_payuser", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_ctime", SqlDbType.DateTime) ,
                        new SqlParameter("@o_ptime", SqlDbType.DateTime) ,
                        new SqlParameter("@o_state", SqlDbType.Int,4) ,
                         new SqlParameter("@o_times", model.o_times) ,
                         new SqlParameter("@o_address", model.o_address) ,
                         new SqlParameter("@o_noticestate", model.o_noticestate),
                         new SqlParameter("@o_noticetimes",model.o_noticetimes),
                         new SqlParameter("@o_privateinfo",model.o_privateinfo),
                         new SqlParameter("@o_interface_id",model.o_interface_id),
                         new SqlParameter("@o_showaddress",model.o_showaddress)
            };
            parameters[0].Value = model.o_code;
            parameters[1].Value = model.o_bizcode;
            parameters[2].Value = model.o_tradeno;
            parameters[3].Value = model.o_paymode_id;
            parameters[4].Value = model.o_app_id;
            parameters[5].Value = model.o_goodsname;
            parameters[6].Value = model.o_term_key;
            parameters[7].Value = model.o_price;
            parameters[8].Value = model.o_payuser;
            parameters[9].Value = model.o_ctime;
            parameters[10].Value = model.o_ptime;
            parameters[11].Value = model.o_state;

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
        /// 添加订单表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOrder(JMP.MDL.jmp_order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_order(");
            strSql.Append("o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress");
            strSql.Append(") values (");
            strSql.Append("@o_code,@o_bizcode,@o_tradeno,@o_paymode_id,@o_app_id,@o_goodsname,@o_term_key,@o_price,@o_payuser,@o_ctime,@o_ptime,@o_state,@o_times,@o_address,@o_noticestate,@o_noticetimes,@o_privateinfo,@o_interface_id,@o_showaddress");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@o_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_bizcode", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_tradeno", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_paymode_id", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@o_goodsname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_term_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_price", SqlDbType.Money,8) ,
                        new SqlParameter("@o_payuser", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_ctime", SqlDbType.DateTime) ,
                        new SqlParameter("@o_ptime", SqlDbType.DateTime) ,
                        new SqlParameter("@o_state", SqlDbType.Int,4) ,
                        new SqlParameter("@o_times", model.o_times) ,
                        new SqlParameter("@o_address", model.o_address) ,
                        new SqlParameter("@o_noticestate", model.o_noticestate),
                        new SqlParameter("@o_noticetimes",model.o_noticetimes),
                        new SqlParameter("@o_privateinfo",model.o_privateinfo),
                        new SqlParameter("@o_interface_id",model.o_interface_id),
                        new SqlParameter("@o_showaddress",model.o_showaddress)
            };
            parameters[0].Value = model.o_code;
            parameters[1].Value = model.o_bizcode;
            parameters[2].Value = model.o_tradeno;
            parameters[3].Value = model.o_paymode_id;
            parameters[4].Value = model.o_app_id;
            parameters[5].Value = model.o_goodsname;
            parameters[6].Value = model.o_term_key;
            parameters[7].Value = model.o_price;
            parameters[8].Value = model.o_payuser;
            parameters[9].Value = model.o_ctime;
            parameters[10].Value = model.o_ptime;
            parameters[11].Value = model.o_state;

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
        public bool Update(JMP.MDL.jmp_order model, string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + TableName + " set ");

            strSql.Append(" o_code = @o_code , ");
            strSql.Append(" o_bizcode = @o_bizcode , ");
            strSql.Append(" o_tradeno = @o_tradeno , ");
            strSql.Append(" o_paymode_id = @o_paymode_id , ");
            strSql.Append(" o_app_id = @o_app_id , ");
            strSql.Append(" o_goodsname = @o_goodsname , ");
            strSql.Append(" o_term_key = @o_term_key , ");
            strSql.Append(" o_price = @o_price , ");
            strSql.Append(" o_payuser = @o_payuser , ");
            strSql.Append(" o_ctime = @o_ctime , ");
            strSql.Append(" o_ptime = @o_ptime , ");
            strSql.Append(" o_state = @o_state,  ");
            strSql.Append(" o_times = @o_times , ");
            strSql.Append(" o_address = @o_address , ");
            strSql.Append(" o_noticestate = @o_noticestate,  ");
            strSql.Append(" o_noticetimes = @o_noticetimes,  ");
            strSql.Append(" o_privateinfo = @o_privateinfo,  ");
            strSql.Append(" o_interface_id = @o_interface_id,  ");
            strSql.Append(" o_showaddress = @o_showaddress  ");
            strSql.Append(" where o_id=@o_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@o_id", SqlDbType.Int,4) ,
                        new SqlParameter("@o_code", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_bizcode", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_tradeno", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_paymode_id", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_app_id", SqlDbType.Int,4) ,
                        new SqlParameter("@o_goodsname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_term_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_price", SqlDbType.Money,8) ,
                        new SqlParameter("@o_payuser", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@o_ctime", SqlDbType.DateTime) ,
                        new SqlParameter("@o_ptime", SqlDbType.DateTime) ,
                        new SqlParameter("@o_state", SqlDbType.Int,4),
                          new SqlParameter("@o_times", model.o_times) ,
                         new SqlParameter("@o_address", model.o_address) ,
                         new SqlParameter("@o_noticestate", model.o_noticestate),
                         new SqlParameter("@o_noticetimes",model.o_noticetimes),
                         new SqlParameter("@o_privateinfo",model.o_privateinfo),
                          new SqlParameter("@o_interface_id",model.o_interface_id),
                         new SqlParameter("@o_showaddress",model.o_showaddress)

            };

            parameters[0].Value = model.o_id;
            parameters[1].Value = model.o_code;
            parameters[2].Value = model.o_bizcode;
            parameters[3].Value = model.o_tradeno;
            parameters[4].Value = model.o_paymode_id;
            parameters[5].Value = model.o_app_id;
            parameters[6].Value = model.o_goodsname;
            parameters[7].Value = model.o_term_key;
            parameters[8].Value = model.o_price;
            parameters[9].Value = model.o_payuser;
            parameters[10].Value = model.o_ctime;
            parameters[11].Value = model.o_ptime;
            parameters[12].Value = model.o_state;
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
        /// 根据订单编码唯一得到实体类
        /// </summary>
        public JMP.MDL.jmp_order GetModelCode(string o_code, string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime, o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress  ");
            strSql.Append("  from  " + TableName);
            strSql.Append(" where o_code=@o_code");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@o_code", @o_code)
            //};
            SqlParameter parameters = new SqlParameter("@o_code", @o_code);
            JMP.MDL.jmp_order model = new JMP.MDL.jmp_order();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return DbHelperSQL.ToModel<JMP.MDL.jmp_order>(ds.Tables[0]);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    if (ds.Tables[0].Rows[0]["o_id"].ToString() != "")
            //    {
            //        model.o_id = int.Parse(ds.Tables[0].Rows[0]["o_id"].ToString());
            //    }
            //    model.o_code = ds.Tables[0].Rows[0]["o_code"].ToString();
            //    model.o_bizcode = ds.Tables[0].Rows[0]["o_bizcode"].ToString();
            //    model.o_tradeno = ds.Tables[0].Rows[0]["o_tradeno"].ToString();
            //    model.o_paymode_id = ds.Tables[0].Rows[0]["o_paymode_id"].ToString();
            //    model.o_privateinfo = ds.Tables[0].Rows[0]["o_privateinfo"].ToString();
            //    if (ds.Tables[0].Rows[0]["o_app_id"].ToString() != "")
            //    {
            //        model.o_app_id = int.Parse(ds.Tables[0].Rows[0]["o_app_id"].ToString());
            //    }

            //    model.o_goodsname = ds.Tables[0].Rows[0]["o_goodsname"].ToString();

            //    model.o_term_key = ds.Tables[0].Rows[0]["o_term_key"].ToString();
            //    if (ds.Tables[0].Rows[0]["o_price"].ToString() != "")
            //    {
            //        model.o_price = decimal.Parse(ds.Tables[0].Rows[0]["o_price"].ToString());
            //    }
            //    model.o_payuser = ds.Tables[0].Rows[0]["o_payuser"].ToString();
            //    if (ds.Tables[0].Rows[0]["o_ctime"].ToString() != "")
            //    {
            //        model.o_ctime = DateTime.Parse(ds.Tables[0].Rows[0]["o_ctime"].ToString());
            //    }
            //    if (ds.Tables[0].Rows[0]["o_ptime"].ToString() != "")
            //    {
            //        model.o_ptime = DateTime.Parse(ds.Tables[0].Rows[0]["o_ptime"].ToString());
            //    }
            //    if (ds.Tables[0].Rows[0]["o_state"].ToString() != "")
            //    {
            //        model.o_state = int.Parse(ds.Tables[0].Rows[0]["o_state"].ToString());
            //    }

            //    if (ds.Tables[0].Rows[0]["o_times"].ToString() != "")
            //    {
            //        model.o_times = int.Parse(ds.Tables[0].Rows[0]["o_times"].ToString());
            //    }
            //    model.o_address = ds.Tables[0].Rows[0]["o_address"].ToString();

            //    if (ds.Tables[0].Rows[0]["o_noticestate"].ToString() != "")
            //    {
            //        model.o_noticestate = int.Parse(ds.Tables[0].Rows[0]["o_noticestate"].ToString());
            //    }
            //    if (ds.Tables[0].Rows[0]["o_noticetimes"].ToString() != "")
            //    {
            //        model.o_noticetimes = DateTime.Parse(ds.Tables[0].Rows[0]["o_noticetimes"].ToString());
            //    }
            //    if (ds.Tables[0].Rows[0]["o_interface_id"].ToString() != "")
            //    {
            //        model.o_interface_id = Int32.Parse(ds.Tables[0].Rows[0]["o_interface_id"].ToString());
            //    }
            //    model.o_showaddress = ds.Tables[0].Rows[0]["o_showaddress"].ToString();
            //    return model;
            //}
            //else
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 根据订单编号查询订单信息（支付接口专用，H5或收银台模式第二次请求时调用）
        /// </summary>
        /// <param name="o_code"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectCode(string o_code, string TableName) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime, o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress  ");
            strSql.Append("  from  " + TableName);
            strSql.Append(" where o_code=@o_code and o_interface_id=0 ");
            SqlParameter parameters = new SqlParameter("@o_code", @o_code);
            JMP.MDL.jmp_order model = new JMP.MDL.jmp_order();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return DbHelperSQL.ToModel<JMP.MDL.jmp_order>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据商户订单号和应用id查询订单信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="bizcode">商户订单编号</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectOrderbizcode(int appid, string code, string bizcode, string TableName)
        {
            StringBuilder strSqlcode = new StringBuilder();
            StringBuilder strSqbizcode = new StringBuilder();
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(code))
            {
                strSqlcode.Append("select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime, o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress  ");
                strSqlcode.Append("  from  " + TableName);
                strSqlcode.Append(" where   o_app_id=@appid  and  o_code=@o_code and o_ptime>='" + DateTime.Now.AddHours(-1) + "' and  o_ptime<='" + DateTime.Now + "'  ");
            }
            if (!string.IsNullOrEmpty(bizcode))
            {
                strSqbizcode.Append("select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime, o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress  ");
                strSqbizcode.Append("  from  " + TableName);
                strSqbizcode.Append(" where   o_app_id=@appid  and  o_bizcode=@bizcode and o_ptime>='" + DateTime.Now.AddHours(-1) + "' and  o_ptime<='" + DateTime.Now + "' ");
            }

            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(bizcode))
            {
                str.Append(strSqlcode);
                str.Append(" union ");
                str.Append(strSqbizcode);
            }
            else
            {
                str.Append(strSqlcode);
                str.Append(strSqbizcode);
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@bizcode", bizcode),
                    new SqlParameter("@appid",appid),
                    new SqlParameter("@o_code",code)
            };
            JMP.MDL.jmp_order model = new JMP.MDL.jmp_order();
            DataSet ds = DbHelperSQL.Query(str.ToString(), parameters);
            return DbHelperSQL.ToModel<JMP.MDL.jmp_order>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据订单ID唯一得到实体类
        /// </summary>
        public JMP.MDL.jmp_order GetModelid(int id, string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime, o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress  ");
            strSql.Append("  from  " + TableName);
            strSql.Append(" where o_id=@o_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@o_id", @id)
            };
            JMP.MDL.jmp_order model = new JMP.MDL.jmp_order();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["o_id"].ToString() != "")
                {
                    model.o_id = int.Parse(ds.Tables[0].Rows[0]["o_id"].ToString());
                }
                model.o_code = ds.Tables[0].Rows[0]["o_code"].ToString();
                model.o_bizcode = ds.Tables[0].Rows[0]["o_bizcode"].ToString();
                model.o_tradeno = ds.Tables[0].Rows[0]["o_tradeno"].ToString();
                model.o_paymode_id = ds.Tables[0].Rows[0]["o_paymode_id"].ToString();
                model.o_privateinfo = ds.Tables[0].Rows[0]["o_privateinfo"].ToString();
                if (ds.Tables[0].Rows[0]["o_app_id"].ToString() != "")
                {
                    model.o_app_id = int.Parse(ds.Tables[0].Rows[0]["o_app_id"].ToString());
                }

                model.o_goodsname = ds.Tables[0].Rows[0]["o_goodsname"].ToString();

                model.o_term_key = ds.Tables[0].Rows[0]["o_term_key"].ToString();
                if (ds.Tables[0].Rows[0]["o_price"].ToString() != "")
                {
                    model.o_price = decimal.Parse(ds.Tables[0].Rows[0]["o_price"].ToString());
                }
                model.o_payuser = ds.Tables[0].Rows[0]["o_payuser"].ToString();
                if (ds.Tables[0].Rows[0]["o_ctime"].ToString() != "")
                {
                    model.o_ctime = DateTime.Parse(ds.Tables[0].Rows[0]["o_ctime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["o_ptime"].ToString() != "")
                {
                    model.o_ptime = DateTime.Parse(ds.Tables[0].Rows[0]["o_ptime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["o_state"].ToString() != "")
                {
                    model.o_state = int.Parse(ds.Tables[0].Rows[0]["o_state"].ToString());
                }

                if (ds.Tables[0].Rows[0]["o_times"].ToString() != "")
                {
                    model.o_times = int.Parse(ds.Tables[0].Rows[0]["o_times"].ToString());
                }
                model.o_address = ds.Tables[0].Rows[0]["o_address"].ToString();

                if (ds.Tables[0].Rows[0]["o_noticestate"].ToString() != "")
                {
                    model.o_noticestate = int.Parse(ds.Tables[0].Rows[0]["o_noticestate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["o_noticetimes"].ToString() != "")
                {
                    model.o_noticetimes = DateTime.Parse(ds.Tables[0].Rows[0]["o_noticetimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["o_interface_id"].ToString() != "")
                {
                    model.o_interface_id = Int32.Parse(ds.Tables[0].Rows[0]["o_interface_id"].ToString());
                }
                model.o_showaddress = ds.Tables[0].Rows[0]["o_showaddress"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到新增订单数量
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public object getordernum(DateTime dt, string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT SUM(a) FROM ( ");
            strSql.Append("select count(1) AS a ");
            strSql.Append(" FROM " + TableName + " where o_ctime >'" + dt.AddMinutes(-5).ToString() + "'");
            strSql.Append(" union all  select count(1) AS a FROM dbo.jmp_order where  o_ctime >'" + dt.AddMinutes(-5).ToString() + "'  ");
            strSql.Append("  ) as a ");
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
        /// <summary>
        /// 得到成功订单数量
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public object SelectCG(DateTime dt, string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT SUM(a) FROM ( ");
            strSql.Append("select count(1)  AS a ");
            strSql.Append(" FROM " + TableName + " where o_ctime >'" + dt.AddMinutes(-5).ToString() + "' and o_state='1' ");
            strSql.Append(" union all  select count(1) AS a FROM dbo.jmp_order where  o_ctime >'" + dt.AddMinutes(-5).ToString() + "' and o_state='1'  ");
            strSql.Append("  ) as a ");
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
        /// <summary>
        /// 得到支付成功订单最后一条的时间
        /// </summary>
        /// <returns></returns>
        public object SelectCgTimes(string TableName)
        {
            string sql = string.Format(" select top 1 o_noticetimes from  " + TableName + " where o_state='1' order by o_noticetimes desc ");
            return DbHelperSQL.GetSingle(sql);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM jmp_order ");
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
            strSql.Append(" FROM jmp_order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据生成的订单编号查询是否存在重复记录
        /// </summary>
        /// <param name="o_code">订单编号</param>
        /// <returns></returns>
        public bool Existss(string o_code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_order");
            strSql.Append(" where ");
            strSql.Append(" o_code = @o_code  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@o_code", o_code)
            };
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改支付渠道id
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="o_id">订单id</param>
        /// <param name="o_interface_id">渠道id</param>
        /// <returns></returns>
        public bool UpdatePay(int o_id, int o_interface_id)
        {
            string sql = string.Format(" update jmp_order set  o_interface_id=@o_interface_id where o_id=@o_id  and o_interface_id=0 ");
            SqlParameter[] par ={
                                //new SqlParameter("@TableName",SqlDbType.NVarChar,-1),
                                new SqlParameter("@o_interface_id",SqlDbType.Int,4),
                                new SqlParameter("@o_id",SqlDbType.Int,4)
                               };
            //par[0].Value = TableName;
            par[0].Value = o_interface_id;
            par[1].Value = o_id;
            int cg = DbHelperSQL.ExecuteSql(sql, par);
            if (cg > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 修改支付类型
        /// </summary>
        /// <param name="o_id">订单表id</param>
        /// <param name="o_paymode_id">支付类型编号</param>
        /// <returns></returns>
        public bool UpdatePayMode(int o_id, int o_paymode_id)
        {
            string sql = string.Format(" update jmp_order set  o_paymode_id=@o_paymode_id where o_id=@o_id and o_state=0 ");
            SqlParameter[] par ={
                                new SqlParameter("@o_paymode_id",SqlDbType.Int,4),
                                new SqlParameter("@o_id",SqlDbType.Int,4)
                               };
            par[0].Value = o_paymode_id;
            par[1].Value = o_id;
            int cg = DbHelperSQL.ExecuteSql(sql, par);
            if (cg > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 报表模块--陶涛20160322
        /// <summary>
        /// 查询成功订单的交易量
        /// </summary>
        /// <param name="tablename">订单表名</param>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetListReportOrderSuccess(string tablename, string t_time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select convert(nvarchar(13),o_ptime,120) times,count(1) counts from {0}  
            where convert(nvarchar(10),o_ptime,120)='{1}' and o_state=1
            group by convert(nvarchar(13),o_ptime,120)", tablename, t_time);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询所有订单的交易量
        /// </summary>
        /// <param name="tablename">订单表名</param>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetListReportOrder(string tablename, string t_time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select convert(nvarchar(13),o_ctime,120) times,count(1) counts from {0}  
            where convert(nvarchar(10),o_ctime,120)='{1}'
            group by convert(nvarchar(13),o_ctime,120)", tablename, t_time);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        /// <summary>
        /// 实时监控
        /// </summary>
        /// <param name="tabname">表名</param>
        /// <param name="r_date">日期（2016-05-05）</param>
        /// <param name="u_id">用户id</param>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public DataTable GetListRealTime(string tabname, string r_date, string u_id, string a_id = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select r_cout,r_money,o_app_id,r_time from(");
            strSql.AppendLine("    select count(distinct o_term_key) as r_cout,isnull(sum(o_price),0) r_money,");
            strSql.AppendLine("    o_app_id,convert(nvarchar(13),o_ptime,120) as r_time from " + tabname);
            strSql.AppendLine("    where o_state=1 and convert(nvarchar(10),o_ptime,120)='" + r_date + "'");
            strSql.AppendLine("    and convert(nvarchar(7),o_ptime,120)='" + DateTime.Parse(r_date).ToString("yyyy-MM") + "'");
            strSql.AppendLine("    group by o_app_id,convert(nvarchar(13),o_ptime,120)");
            strSql.AppendLine(") temp");
            strSql.AppendLine("left join jmp_app t_app on temp.o_app_id=t_app.a_id");
            strSql.AppendLine("left join jmp_user t_user on t_app.a_user_id=t_user.u_id");
            strSql.AppendLine("where t_user.u_id=" + u_id);
            if (!string.IsNullOrEmpty(a_id))
            {
                strSql.Append(a_id == "0" ? "" : "and temp.o_app_id=" + a_id);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> SelectList(string sqls, int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_order>(dt);
        }
        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序类型（0：升序，1：降序）</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> SelectPager(string where, string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("OrderList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@where", where));
            da.SelectCommand.Parameters.Add(new SqlParameter("@sql1", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            Count = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_order>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> DcSelectList(string sql)
        {
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_order>(dt);
        }

        /// <summary>
        /// 查询商务对应的成功订单的交易量
        /// </summary>
        /// <param name="tablename">订单表名</param>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetMerchantListReportOrderSuccess(string tablename, string t_time, int merchantId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"SELECT  CONVERT(NVARCHAR(13),o_ptime,120) times,COUNT(1) counts
FROM    {0} AS O
LEFT JOIN jmp_app AS JA
        ON JA.a_id=O.o_app_id
WHERE   O.o_state=1
        AND CONVERT(NVARCHAR(10),o_ptime,120)='{1}'
        AND EXISTS ( SELECT 1
                     FROM   jmp_user AS JU
                     WHERE  JU.u_merchant_id={2}
                            AND JU.u_id=JA.a_user_id )
GROUP BY CONVERT(NVARCHAR(13),o_ptime,120);", tablename, t_time, merchantId);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 查询商务对应的指定日期内所有订单的交易量
        /// </summary>
        /// <param name="tablename">订单表名</param>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetMerchantListReportOrderAll(string tablename, string t_time, int merchantId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"SELECT  CONVERT(NVARCHAR(13),o_ptime,120) times,COUNT(1) counts
FROM    {0} AS O
LEFT JOIN jmp_app AS JA
        ON JA.a_id=O.o_app_id
WHERE 
        CONVERT(NVARCHAR(10),o_ptime,120)='{1}'
        AND EXISTS ( SELECT 1
                     FROM   jmp_user AS JU
                     WHERE  JU.u_merchant_id={2}
                            AND JU.u_id=JA.a_user_id )
GROUP BY CONVERT(NVARCHAR(13),o_ptime,120);", tablename, t_time, merchantId);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据订单id查询订单信息和商品名称
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="tname">订单表表名</param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectOrderGoodsName(int oid, string tname)
        {
            string sql = string.Format(" select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime,  o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress from " + tname + "  where  o_id=@oid ");
            SqlParameter par = new SqlParameter("@oid", oid);
            DataTable dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_order>(dt);
        }


        /// <summary>
        /// 将订单状态标识修改为异常,值为:2
        /// <param name="tableName">订单表名</param>
        /// <param name="oCode">订单编码</param>
        /// </summary>
        public bool ChangeStateToAbnormal(string tableName, string oCode)
        {
            if (string.IsNullOrEmpty(oCode))
            {
                return false;
            }
            var strSql = new StringBuilder();
            strSql.Append("update " + tableName + " set o_state=2");
            strSql.Append(" where o_code=@o_code ");
            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), new SqlParameter("@o_code", oCode));
            return rows > 0;
        }

        /// <summary>
        /// 根据订单号获取需要手动通知的信息
        /// </summary>
        /// <param name="o_code">订单号</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectOrder(string o_code, string TableName)
        {
            string sql = string.Format("select o_id, o_code, o_bizcode, o_tradeno, o_paymode_id, o_app_id, o_goodsname, o_term_key, o_price, o_payuser, o_ctime, o_ptime,o_state, o_times, o_address, o_noticestate, o_noticetimes, o_privateinfo, o_interface_id, o_showaddress FROM " + TableName + "  WHERE o_state = 1 AND o_noticestate = -1 AND o_code=@o_code  AND o_times>7 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@o_code", @o_code)
            };
            JMP.MDL.jmp_order model = new JMP.MDL.jmp_order();
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql.ToString(), parameters).Tables[0];
            model = DbHelperSQL.ToModel<JMP.MDL.jmp_order>(dt);
            return model;
        }



        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序类型（0：升序，1：降序）</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> SelectPagerYunY(string where, string BpWhere, string AgentWhere, string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("BpOrderList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@where", where));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Bpwhere", BpWhere));
            da.SelectCommand.Parameters.Add(new SqlParameter("@AgentWhere", AgentWhere));
            da.SelectCommand.Parameters.Add(new SqlParameter("@sql1", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            Count = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_order>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据订单表名和订单号获取指定的订单实体
        /// </summary>
        /// <param name="tableName">订单表名</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public MDL.jmp_order FindOrderByTableNameAndOrderNo(string tableName, string orderNo)
        {
            var sql = string.Format("SELECT * FROM " + tableName + " WHERE o_code=@o_code");
            SqlParameter[] parameters = {
                new SqlParameter("@o_code", orderNo)
            };
            var d = DbHelperSQL.Query(sql, parameters).Tables[0];
            var model = DbHelperSQL.ToModel<MDL.jmp_order>(d);
            return model;
        }


        /// <summary>
        /// 根据订单表名和订单号获取指定的订单实体(包含实时订单表数据)
        /// </summary>
        /// <param name="tableName">归档订单表名</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public MDL.jmp_order FindOrderByTableNameAndOrderNoIncludeRealtime(string tableName, string orderNo)
        {
            var sql = string.Format("SELECT o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress FROM jmp_order WHERE o_code=@o_code UNION SELECT o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress FROM " + tableName + " WHERE o_code=@o_code");
            SqlParameter[] parameters = {
                new SqlParameter("@o_code", orderNo)
            };
            var d = DbHelperSQL.Query(sql, parameters).Tables[0];
            var model = DbHelperSQL.ToModel<MDL.jmp_order>(d);
            return model;
        }
    }
}

