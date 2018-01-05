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
    //提款银行卡表
    public class jmp_userbank
    {
        public bool Exists(int u_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_userbank");
            strSql.Append(" where ");
            strSql.Append(" u_id = @u_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_userbank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_userbank(");
            strSql.Append("u_date,u_freeze,u_province,u_area,u_flag,u_userid,u_banknumber,u_bankname,u_openbankname,u_name,u_state,u_remarks,u_auditor");
            strSql.Append(") values (");
            strSql.Append("@u_date,@u_freeze,@u_province,@u_area,@u_flag,@u_userid,@u_banknumber,@u_bankname,@u_openbankname,@u_name,@u_state,@u_remarks,@u_auditor");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@u_date", SqlDbType.DateTime) ,
                        new SqlParameter("@u_freeze", SqlDbType.Int,4) ,
                        new SqlParameter("@u_province", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@u_area", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@u_flag", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@u_userid", SqlDbType.Int,4) ,
                        new SqlParameter("@u_banknumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_bankname", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_openbankname", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@u_name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_state", SqlDbType.Int,4) ,
                        new SqlParameter("@u_remarks", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@u_auditor", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.u_date;
            parameters[1].Value = model.u_freeze;
            parameters[2].Value = model.u_province;
            parameters[3].Value = model.u_area;
            parameters[4].Value = model.u_flag;
            parameters[5].Value = model.u_userid;
            parameters[6].Value = model.u_banknumber;
            parameters[7].Value = model.u_bankname;
            parameters[8].Value = model.u_openbankname;
            parameters[9].Value = model.u_name;
            parameters[10].Value = model.u_state;
            parameters[11].Value = model.u_remarks;
            parameters[12].Value = model.u_auditor;

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
        public bool Update(JMP.MDL.jmp_userbank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_userbank set ");

            strSql.Append(" u_date = @u_date , ");
            strSql.Append(" u_freeze = @u_freeze , ");
            strSql.Append(" u_province = @u_province , ");
            strSql.Append(" u_area = @u_area , ");
            strSql.Append(" u_flag = @u_flag , ");
            strSql.Append(" u_userid = @u_userid , ");
            strSql.Append(" u_banknumber = @u_banknumber , ");
            strSql.Append(" u_bankname = @u_bankname , ");
            strSql.Append(" u_openbankname = @u_openbankname , ");
            strSql.Append(" u_name = @u_name , ");
            strSql.Append(" u_state = @u_state , ");
            strSql.Append(" u_remarks = @u_remarks , ");
            strSql.Append(" u_auditor = @u_auditor  ");
            strSql.Append(" where u_id=@u_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@u_id", SqlDbType.Int,4) ,
                        new SqlParameter("@u_date", SqlDbType.DateTime) ,
                        new SqlParameter("@u_freeze", SqlDbType.Int,4) ,
                        new SqlParameter("@u_province", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@u_area", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@u_flag", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@u_userid", SqlDbType.Int,4) ,
                        new SqlParameter("@u_banknumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_bankname", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_openbankname", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@u_name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_state", SqlDbType.Int,4) ,
                        new SqlParameter("@u_remarks", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@u_auditor", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.u_id;
            parameters[1].Value = model.u_date;
            parameters[2].Value = model.u_freeze;
            parameters[3].Value = model.u_province;
            parameters[4].Value = model.u_area;
            parameters[5].Value = model.u_flag;
            parameters[6].Value = model.u_userid;
            parameters[7].Value = model.u_banknumber;
            parameters[8].Value = model.u_bankname;
            parameters[9].Value = model.u_openbankname;
            parameters[10].Value = model.u_name;
            parameters[11].Value = model.u_state;
            parameters[12].Value = model.u_remarks;
            parameters[13].Value = model.u_auditor;
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
        public bool Delete(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_userbank ");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;


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
        public bool DeleteList(string u_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_userbank ");
            strSql.Append(" where ID in (" + u_idlist + ")  ");
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
        public JMP.MDL.jmp_userbank GetModel(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id, u_date, u_freeze, u_province, u_area, u_flag, u_userid, u_banknumber, u_bankname, u_openbankname, u_name, u_state, u_remarks, u_auditor  ");
            strSql.Append("  from jmp_userbank ");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;


            JMP.MDL.jmp_userbank model = new JMP.MDL.jmp_userbank();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_date"].ToString() != "")
                {
                    model.u_date = DateTime.Parse(ds.Tables[0].Rows[0]["u_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_freeze"].ToString() != "")
                {
                    model.u_freeze = int.Parse(ds.Tables[0].Rows[0]["u_freeze"].ToString());
                }
                model.u_province = ds.Tables[0].Rows[0]["u_province"].ToString();
                model.u_area = ds.Tables[0].Rows[0]["u_area"].ToString();
                model.u_flag = ds.Tables[0].Rows[0]["u_flag"].ToString();
                if (ds.Tables[0].Rows[0]["u_userid"].ToString() != "")
                {
                    model.u_userid = int.Parse(ds.Tables[0].Rows[0]["u_userid"].ToString());
                }
                model.u_banknumber = ds.Tables[0].Rows[0]["u_banknumber"].ToString();
                model.u_bankname = ds.Tables[0].Rows[0]["u_bankname"].ToString();
                model.u_openbankname = ds.Tables[0].Rows[0]["u_openbankname"].ToString();
                model.u_name = ds.Tables[0].Rows[0]["u_name"].ToString();
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                model.u_remarks = ds.Tables[0].Rows[0]["u_remarks"].ToString();
                model.u_auditor = ds.Tables[0].Rows[0]["u_auditor"].ToString();

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
            strSql.Append(" FROM jmp_userbank ");
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
            strSql.Append(" FROM jmp_userbank ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        ///  查询开发者绑定的银行卡信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="searchType">搜索条件</param>
        /// <param name="banknumber">搜索信息</param>
        /// <param name="flag">付款标识</param>
        /// <param name="state">审核状态</param>
        /// <param name="freeze">冻结状态</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_userbank> SelectUserBankList(int id, string searchType, string banknumber, string flag, string state, string freeze, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from [dbo].[jmp_userbank] where u_userid='" + id + "'");

            if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(banknumber))
            {
                switch (searchType)
                {
                    case "1":
                        sql += " and u_banknumber='" + banknumber + "'";
                        break;
                    case "2":
                        sql += " and u_bankname like '%" + banknumber + "%'";
                        break;
                    case "3":
                        sql += " and u_name like '%" + banknumber + "%'";
                        break;
                }
            }
            if (!string.IsNullOrEmpty(flag))
            {
                sql += " and u_flag='" + flag + "'";
            }
            if (!string.IsNullOrEmpty(state))
            {
                sql += " and u_state='" + state + "'";
            }
            if (!string.IsNullOrEmpty(freeze))
            {
                sql += " and u_freeze='" + freeze + "'";
            }

            string order = " order by u_id desc ";
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_userbank>(ds.Tables[0]);
        }


        /// <summary>
        ///  查询开发者绑定的银行卡信息(审核通过，状态正常的卡)
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="searchType">搜索条件</param>
        /// <param name="banknumber">搜索信息</param>
        /// <param name="flag">付款标识</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_userbank> SelectUserBankListStart(int id, string searchType, string banknumber, string flag, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from [dbo].[jmp_userbank] where u_userid='" + id + "' and u_state=1 and u_freeze=0");

            if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(banknumber))
            {
                switch (searchType)
                {
                    case "1":
                        sql += " and u_banknumber='" + banknumber + "'";
                        break;
                    case "2":
                        sql += " and u_bankname like '%" + banknumber + "%'";
                        break;
                    case "3":
                        sql += " and u_name like '%" + banknumber + "%'";
                        break;
                }
            }
            if (!string.IsNullOrEmpty(flag))
            {
                sql += " and u_flag='" + flag + "'";
            }

            string order = " order by u_date desc";
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_userbank>(ds.Tables[0]);
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_userbank> GetAppUserBankLists(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_userbank>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新银行卡状态
        /// </summary>
        /// <param name="ids">主键id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateState(string ids, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_userbank set u_freeze=" + state + " where u_id in(" + ids + ")");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ids">主键id</param>
        /// <param name="state">状态值</param>
        ///  <param name="name">审核人</param>
        /// <returns></returns>
        public bool UpdateAuditState(int ids, int state, string name, string u_remarks)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_userbank set u_state=" + state + ",u_auditor='" + name + "' ,u_date= GETDATE(),u_remarks='" + u_remarks + "' where u_id =" + ids + "");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }



        /// <summary>
        /// 是否存在该银行卡号
        /// </summary>
        /// <param name="yyzz">银行卡号</param>
        /// <param name="uid">银行卡表主键id</param>
        /// <returns></returns>
        public bool ExistsBankNo(string yyzz, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from jmp_userbank where u_banknumber='{0}' and u_state!='-1' ", yyzz);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and u_id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        public bool ExitstBankNOBYAll(string yyzz, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from jmp_userbank where u_banknumber='{0}' and u_state!='-1' ", yyzz);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and u_id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 根据交易批次号查询数据
        /// </summary>
        /// <param name="batchnumber">交易批次号</param>
        /// <returns></returns>
        public JMP.MDL.jmp_userbank SelectUserBank_paymoney(string batchnumber)
        {
            string sql = string.Format("select * from jmp_userbank a,jmp_BankPlaymoney b where a.u_id=b.b_bankid and b.b_batchnumber='" + batchnumber + "'");
            DataSet ds = DbHelperSQL.Query(sql);
            return DbHelperSQL.ToModel<JMP.MDL.jmp_userbank>(ds.Tables[0]);
        }

        public JMP.MDL.jmp_userbank GetUserBankByBankNo(string u_banknumber, int   u_userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id, u_date, u_freeze, u_province, u_area, u_flag, u_userid, u_banknumber, u_bankname, u_openbankname, u_name, u_state, u_remarks, u_auditor  ");
            strSql.Append("  from jmp_userbank ");
            strSql.Append(" where  u_freeze=1 and  u_banknumber=@u_banknumber and u_state!='-1' ");
            strSql.Append(" and u_userid=@u_userid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@u_banknumber", SqlDbType.NVarChar, 100),
                new SqlParameter("@u_userid", SqlDbType.Int, 4)
            };
            parameters[0].Value = u_banknumber;
            parameters[1].Value = u_userid;
            JMP.MDL.jmp_userbank model = new JMP.MDL.jmp_userbank();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_date"].ToString() != "")
                {
                    model.u_date = DateTime.Parse(ds.Tables[0].Rows[0]["u_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_freeze"].ToString() != "")
                {
                    model.u_freeze = int.Parse(ds.Tables[0].Rows[0]["u_freeze"].ToString());
                }
                model.u_province = ds.Tables[0].Rows[0]["u_province"].ToString();
                model.u_area = ds.Tables[0].Rows[0]["u_area"].ToString();
                model.u_flag = ds.Tables[0].Rows[0]["u_flag"].ToString();
                if (ds.Tables[0].Rows[0]["u_userid"].ToString() != "")
                {
                    model.u_userid = int.Parse(ds.Tables[0].Rows[0]["u_userid"].ToString());
                }
                model.u_banknumber = ds.Tables[0].Rows[0]["u_banknumber"].ToString();
                model.u_bankname = ds.Tables[0].Rows[0]["u_bankname"].ToString();
                model.u_openbankname = ds.Tables[0].Rows[0]["u_openbankname"].ToString();
                model.u_name = ds.Tables[0].Rows[0]["u_name"].ToString();
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                model.u_remarks = ds.Tables[0].Rows[0]["u_remarks"].ToString();
                model.u_auditor = ds.Tables[0].Rows[0]["u_auditor"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
