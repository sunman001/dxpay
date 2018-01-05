using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.DAL
{
    //银行打款对接表
    public partial class jmp_BankPlaymoney
    {

        public bool Exists(int b_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_BankPlaymoney");
            strSql.Append(" where ");
            strSql.Append(" b_id = @b_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@b_id", SqlDbType.Int,4)
            };
            parameters[0].Value = b_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_BankPlaymoney model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_BankPlaymoney(");
            strSql.Append("b_payfashion,b_remark,b_payforanotherId,b_batchnumber,b_number,b_tradeno,b_tradestate,b_date,b_bankid,b_money,b_paydate");
            strSql.Append(") values (");
            strSql.Append("@b_payfashion,@b_remark,@b_payforanotherId,@b_batchnumber,@b_number,@b_tradeno,@b_tradestate,@b_date,@b_bankid,@b_money,@b_paydate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@b_payfashion", SqlDbType.Int,4) ,
                        new SqlParameter("@b_remark", SqlDbType.NVarChar,1000) ,
                        new SqlParameter("@b_payforanotherId", SqlDbType.Int,4) ,
                        new SqlParameter("@b_batchnumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@b_number", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@b_tradeno", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@b_tradestate", SqlDbType.Int,4) ,
                        new SqlParameter("@b_date", SqlDbType.DateTime) ,
                        new SqlParameter("@b_bankid", SqlDbType.Int,4) ,
                        new SqlParameter("@b_money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@b_paydate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.b_payfashion;
            parameters[1].Value = model.b_remark;
            parameters[2].Value = model.b_payforanotherId;
            parameters[3].Value = model.b_batchnumber;
            parameters[4].Value = model.b_number;
            parameters[5].Value = model.b_tradeno;
            parameters[6].Value = model.b_tradestate;
            parameters[7].Value = model.b_date;
            parameters[8].Value = model.b_bankid;
            parameters[9].Value = model.b_money;
            parameters[10].Value = model.b_paydate;

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
        public bool Update(JMP.MDL.jmp_BankPlaymoney model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_BankPlaymoney set ");

            strSql.Append(" b_payfashion = @b_payfashion , ");
            strSql.Append(" b_remark = @b_remark , ");
            strSql.Append(" b_payforanotherId = @b_payforanotherId , ");
            strSql.Append(" b_batchnumber = @b_batchnumber , ");
            strSql.Append(" b_number = @b_number , ");
            strSql.Append(" b_tradeno = @b_tradeno , ");
            strSql.Append(" b_tradestate = @b_tradestate , ");
            strSql.Append(" b_date = @b_date , ");
            strSql.Append(" b_bankid = @b_bankid , ");
            strSql.Append(" b_money = @b_money , ");
            strSql.Append(" b_paydate = @b_paydate  ");
            strSql.Append(" where b_id=@b_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@b_id", SqlDbType.Int,4) ,
                        new SqlParameter("@b_payfashion", SqlDbType.Int,4) ,
                        new SqlParameter("@b_remark", SqlDbType.NVarChar,1000) ,
                        new SqlParameter("@b_payforanotherId", SqlDbType.Int,4) ,
                        new SqlParameter("@b_batchnumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@b_number", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@b_tradeno", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@b_tradestate", SqlDbType.Int,4) ,
                        new SqlParameter("@b_date", SqlDbType.DateTime) ,
                        new SqlParameter("@b_bankid", SqlDbType.Int,4) ,
                        new SqlParameter("@b_money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@b_paydate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.b_id;
            parameters[1].Value = model.b_payfashion;
            parameters[2].Value = model.b_remark;
            parameters[3].Value = model.b_payforanotherId;
            parameters[4].Value = model.b_batchnumber;
            parameters[5].Value = model.b_number;
            parameters[6].Value = model.b_tradeno;
            parameters[7].Value = model.b_tradestate;
            parameters[8].Value = model.b_date;
            parameters[9].Value = model.b_bankid;
            parameters[10].Value = model.b_money;
            parameters[11].Value = model.b_paydate;
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
        public bool Delete(int b_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_BankPlaymoney ");
            strSql.Append(" where b_id=@b_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@b_id", SqlDbType.Int,4)
            };
            parameters[0].Value = b_id;


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
        public bool DeleteList(string b_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_BankPlaymoney ");
            strSql.Append(" where ID in (" + b_idlist + ")  ");
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
        public JMP.MDL.jmp_BankPlaymoney GetModel(int b_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b_id, b_payfashion, b_remark, b_payforanotherId, b_batchnumber, b_number, b_tradeno, b_tradestate, b_date, b_bankid, b_money, b_paydate  ");
            strSql.Append("  from jmp_BankPlaymoney ");
            strSql.Append(" where b_id=@b_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@b_id", SqlDbType.Int,4)
            };
            parameters[0].Value = b_id;


            JMP.MDL.jmp_BankPlaymoney model = new JMP.MDL.jmp_BankPlaymoney();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["b_id"].ToString() != "")
                {
                    model.b_id = int.Parse(ds.Tables[0].Rows[0]["b_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b_payfashion"].ToString() != "")
                {
                    model.b_payfashion = int.Parse(ds.Tables[0].Rows[0]["b_payfashion"].ToString());
                }
                model.b_remark = ds.Tables[0].Rows[0]["b_remark"].ToString();
                if (ds.Tables[0].Rows[0]["b_payforanotherId"].ToString() != "")
                {
                    model.b_payforanotherId = int.Parse(ds.Tables[0].Rows[0]["b_payforanotherId"].ToString());
                }
                model.b_batchnumber = ds.Tables[0].Rows[0]["b_batchnumber"].ToString();
                model.b_number = ds.Tables[0].Rows[0]["b_number"].ToString();
                model.b_tradeno = ds.Tables[0].Rows[0]["b_tradeno"].ToString();
                if (ds.Tables[0].Rows[0]["b_tradestate"].ToString() != "")
                {
                    model.b_tradestate = int.Parse(ds.Tables[0].Rows[0]["b_tradestate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b_date"].ToString() != "")
                {
                    model.b_date = DateTime.Parse(ds.Tables[0].Rows[0]["b_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b_bankid"].ToString() != "")
                {
                    model.b_bankid = int.Parse(ds.Tables[0].Rows[0]["b_bankid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b_money"].ToString() != "")
                {
                    model.b_money = decimal.Parse(ds.Tables[0].Rows[0]["b_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b_paydate"].ToString() != "")
                {
                    model.b_paydate = DateTime.Parse(ds.Tables[0].Rows[0]["b_paydate"].ToString());
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
            strSql.Append(" FROM jmp_BankPlaymoney ");
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
            strSql.Append(" FROM jmp_BankPlaymoney ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_BankPlaymoney> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", Order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            Count = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            DataTable dt = ds.Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_BankPlaymoney>(dt);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <param name="state">审核状态</param>
        /// <param name="name">审核人</param>
        /// <param name="payId">代付上游通道ID</param>
        /// <returns></returns>
        public bool UpdateState(string batchnumber, int state, string name, int payId)
        {
            StringBuilder strSql = new StringBuilder();
            List<string> list = new List<string>();
            bool cg = false;
            if (batchnumber.Contains(","))
            {

                string[] strarr = batchnumber.Split(',');
                batchnumber = "";
                var count = 0;
                foreach (var i in strarr)
                {
                    count += count + 1;
                    if (count > 1)
                    {
                        batchnumber += ",";
                    }
                    batchnumber += "'" + i + "'";
                }
                strSql.Append("update jmp_pays set p_state=" + state + ",p_auditor='" + name + "',p_date=GETDATE()  where p_batchnumber in(" + batchnumber + ")");
                list.Add(strSql.ToString());
                if (state == -1)
                {
                    list.Add("update jmp_BankPlaymoney set b_tradestate=-1 where b_batchnumber in (" + batchnumber + ")");
                }
                else
                {
                    list.Add("update jmp_BankPlaymoney set b_payforanotherId=" + payId + "  where b_batchnumber in (" + batchnumber + ")");
                }

            }
            else
            {
                strSql.Append("update jmp_pays set p_state=" + state + ",p_auditor='" + name + "',p_date=GETDATE()  where p_batchnumber ='" + batchnumber + "'");
                list.Add(strSql.ToString());
                if (state == -1)
                {
                    list.Add("update jmp_BankPlaymoney set b_tradestate=-1 where b_batchnumber='" + batchnumber + "' ");
                }
                else
                {
                    list.Add("update jmp_BankPlaymoney set b_payforanotherId=" + payId + "  where b_batchnumber ='" + batchnumber + "'");
                }
            }

            int sucess = DbHelperSQL.ExecuteSqlTran(list);
            if (sucess > 0)
            {
                cg = true;
            }
            else
            {
                cg = false;
            }
            return cg;
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int SelectBankPayMoney(List<string> sqlstr)
        {
            object obj = DbHelperSQL.ExecuteSqlTran(sqlstr);
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
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectSQL(string sql)
        {
            return DbHelperSQL.Query(sql.ToString()).Tables[0];
        }


        /// <summary>
        /// 根据代付结果修改相对应的状态
        /// </summary>
        /// <param name="batchnumber">提款批次号</param>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <param name="tradeno">交易流水号</param>
        /// <param name="paydate">到账日期</param>
        /// <returns></returns>
        public bool UpdateBankPayHD(string batchnumber, int tradestate, string number, string tradeno, DateTime paydate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update jmp_BankPlaymoney set b_tradestate=" + tradestate + ",b_tradeno='" + tradeno + "',b_paydate='" + paydate + "'  where b_batchnumber='" + batchnumber + "' and b_number='" + number + "'");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 根据交易编号修改交易流水号
        /// </summary>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <param name="tradeno">交易流水号</param>
        /// <returns></returns>
        public bool UpdateBankPayTradeno(int tradestate, string number, string tradeno)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update jmp_BankPlaymoney set b_tradestate=" + tradestate + ",b_tradeno='" + tradeno + "' where b_number='" + number + "'");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 修改交易编号(必须是等待打款的数据)
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <param name="number">交易编号</param>
        /// <param name="payfashion">交易方式</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool UpdateBankPayNumber(string batchnumber, string number, int payfashion, string remark)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update jmp_BankPlaymoney set b_number='" + number + "',b_payfashion=" + payfashion + ",b_remark='" + remark + "' where b_batchnumber='" + batchnumber + "' and  b_tradestate=0");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 修改状态码
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <param name="number">状态码</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool UpdateBankPayTradestate(string batchnumber, int tradestate, string remark)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update jmp_BankPlaymoney set b_tradestate='" + tradestate + "',b_remark='" + remark + "' where b_batchnumber='" + batchnumber + "'");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 根据交易编号修改交易流水号
        /// </summary>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <returns></returns>
        public bool UpdateBankPayNumberTradestate(int tradestate, string number)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update jmp_BankPlaymoney set b_tradestate=" + tradestate + " where b_number='" + number + "'");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 手动打款
        /// </summary>
        /// <param name="batchnumber">提款批次号</param>
        /// <param name="tradestate">交易状态</param>
        /// <param name="number">交易编号</param>
        /// <param name="tradeno">交易流水号</param>
        /// <param name="paydate">到账日期</param>
        /// <param name="payfashion">交易类型</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool UpdateBankPayHandMovement(string batchnumber, int tradestate, string number, string tradeno, DateTime paydate, int payfashion, string remark)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update jmp_BankPlaymoney set b_tradestate=" + tradestate + ",b_number='" + number + "',b_tradeno='" + tradeno + "',b_paydate='" + paydate + "',b_payfashion=" + payfashion + ",b_remark='" + remark + "'  where b_batchnumber='" + batchnumber + "'");

            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="batchnumber">批次号</param>
        /// <returns></returns>
        public JMP.MDL.jmp_BankPlaymoney GetBankPlaymoneyModel(string batchnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b_tradestate  ");
            strSql.Append("  from jmp_BankPlaymoney ");
            strSql.Append(" where b_batchnumber=@batchnumber");
            SqlParameter[] parameters = {
                    new SqlParameter("@batchnumber", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = batchnumber;


            JMP.MDL.jmp_BankPlaymoney model = new JMP.MDL.jmp_BankPlaymoney();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["b_tradestate"].ToString() != "")
                {
                    model.b_tradestate = int.Parse(ds.Tables[0].Rows[0]["b_tradestate"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据sql语句查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable CountSect(string sql)
        {
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }
    }
}
