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
    //提款表
    public class jmp_pays
    {
        public bool Exists(int p_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_pays");
            strSql.Append(" where ");
            strSql.Append(" p_id = @p_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_pays model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_pays(");
            strSql.Append("p_remarks,p_applytime,p_money,p_bill_id,p_userid,p_state,p_auditor,p_batchnumber,p_date");
            strSql.Append(") values (");
            strSql.Append("@p_remarks,@p_applytime,@p_money,@p_bill_id,@p_userid,@p_state,@p_auditor,@p_batchnumber,@p_date");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@p_remarks", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_applytime", SqlDbType.DateTime) ,
                        new SqlParameter("@p_money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@p_bill_id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_userid", SqlDbType.Int,4) ,
                        new SqlParameter("@p_state", SqlDbType.Int,4) ,
                        new SqlParameter("@p_auditor", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@p_batchnumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@p_date", SqlDbType.DateTime)

            };

            parameters[0].Value = model.p_remarks;
            parameters[1].Value = model.p_applytime;
            parameters[2].Value = model.p_money;
            parameters[3].Value = model.p_bill_id;
            parameters[4].Value = model.p_userid;
            parameters[5].Value = model.p_state;
            parameters[6].Value = model.p_auditor;
            parameters[7].Value = model.p_batchnumber;
            parameters[8].Value = model.p_date;

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
        public bool Update(JMP.MDL.jmp_pays model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_pays set ");

            strSql.Append(" p_remarks = @p_remarks , ");
            strSql.Append(" p_applytime = @p_applytime , ");
            strSql.Append(" p_money = @p_money , ");
            strSql.Append(" p_bill_id = @p_bill_id , ");
            strSql.Append(" p_userid = @p_userid , ");
            strSql.Append(" p_state = @p_state , ");
            strSql.Append(" p_auditor = @p_auditor , ");
            strSql.Append(" p_batchnumber = @p_batchnumber , ");
            strSql.Append(" p_date = @p_date  ");
            strSql.Append(" where p_id=@p_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@p_id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_remarks", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_applytime", SqlDbType.DateTime) ,
                        new SqlParameter("@p_money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@p_bill_id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_userid", SqlDbType.Int,4) ,
                        new SqlParameter("@p_state", SqlDbType.Int,4) ,
                        new SqlParameter("@p_auditor", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@p_batchnumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@p_date", SqlDbType.DateTime)

            };

            parameters[0].Value = model.p_id;
            parameters[1].Value = model.p_remarks;
            parameters[2].Value = model.p_applytime;
            parameters[3].Value = model.p_money;
            parameters[4].Value = model.p_bill_id;
            parameters[5].Value = model.p_userid;
            parameters[6].Value = model.p_state;
            parameters[7].Value = model.p_auditor;
            parameters[8].Value = model.p_batchnumber;
            parameters[9].Value = model.p_date;
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
        public bool Delete(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_pays ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


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
        public bool DeleteList(string p_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_pays ");
            strSql.Append(" where ID in (" + p_idlist + ")  ");
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
        public JMP.MDL.jmp_pays GetModel(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id, p_remarks, p_applytime, p_money, p_bill_id, p_userid, p_state, p_auditor, p_batchnumber, p_date  ");
            strSql.Append("  from jmp_pays ");
            strSql.Append(" where p_id=@p_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_id;


            JMP.MDL.jmp_pays model = new JMP.MDL.jmp_pays();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
                }
                model.p_remarks = ds.Tables[0].Rows[0]["p_remarks"].ToString();
                if (ds.Tables[0].Rows[0]["p_applytime"].ToString() != "")
                {
                    model.p_applytime = DateTime.Parse(ds.Tables[0].Rows[0]["p_applytime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_money"].ToString() != "")
                {
                    model.p_money = decimal.Parse(ds.Tables[0].Rows[0]["p_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_bill_id"].ToString() != "")
                {
                    model.p_bill_id = int.Parse(ds.Tables[0].Rows[0]["p_bill_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_userid"].ToString() != "")
                {
                    model.p_userid = int.Parse(ds.Tables[0].Rows[0]["p_userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_state"].ToString() != "")
                {
                    model.p_state = int.Parse(ds.Tables[0].Rows[0]["p_state"].ToString());
                }
                model.p_auditor = ds.Tables[0].Rows[0]["p_auditor"].ToString();
                model.p_batchnumber = ds.Tables[0].Rows[0]["p_batchnumber"].ToString();
                if (ds.Tables[0].Rows[0]["p_date"].ToString() != "")
                {
                    model.p_date = DateTime.Parse(ds.Tables[0].Rows[0]["p_date"].ToString());
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
            strSql.Append(" FROM jmp_pays ");
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
            strSql.Append(" FROM jmp_pays ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectList(string sql)
        {
            return DbHelperSQL.Query(sql.ToString()).Tables[0];
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_pays GetPaysModel(string batchnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 *  ");
            strSql.Append("  from jmp_pays ");
            strSql.Append(" where p_batchnumber=@batchnumber");
            SqlParameter[] parameters = {
                    new SqlParameter("@batchnumber", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = batchnumber;


            JMP.MDL.jmp_pays model = new JMP.MDL.jmp_pays();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
                }
                model.p_remarks = ds.Tables[0].Rows[0]["p_remarks"].ToString();
                if (ds.Tables[0].Rows[0]["p_applytime"].ToString() != "")
                {
                    model.p_applytime = DateTime.Parse(ds.Tables[0].Rows[0]["p_applytime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_money"].ToString() != "")
                {
                    model.p_money = decimal.Parse(ds.Tables[0].Rows[0]["p_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_bill_id"].ToString() != "")
                {
                    model.p_bill_id = int.Parse(ds.Tables[0].Rows[0]["p_bill_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_userid"].ToString() != "")
                {
                    model.p_userid = int.Parse(ds.Tables[0].Rows[0]["p_userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_state"].ToString() != "")
                {
                    model.p_state = int.Parse(ds.Tables[0].Rows[0]["p_state"].ToString());
                }
                model.p_auditor = ds.Tables[0].Rows[0]["p_auditor"].ToString();
                model.p_batchnumber = ds.Tables[0].Rows[0]["p_batchnumber"].ToString();
                if (ds.Tables[0].Rows[0]["p_date"].ToString() != "")
                {
                    model.p_date = DateTime.Parse(ds.Tables[0].Rows[0]["p_date"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

    }
}
