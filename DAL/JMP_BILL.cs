using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{

    ///<summary>
    ///账单表
    ///</summary>
    public partial class jmp_bill
    {

        public bool Exists(int b_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_bill");
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
        public int Add(JMP.MDL.jmp_bill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_bill(");
            strSql.Append("SdWxOfficalAccountPay,SdWxAppPay,SdWxQrCodePay,SdAliQrCodePay,BlTotalAmount,BlAliPay,BlWxPay,BlUnionPay,BlWxOfficalAccountPay,BlWxAppPay,OrderDate,BlWxQrCodePay,BlAliQrCodePay,UserId,UserName,CreatedOn,SdTotalAmount,SdAliPay,SdWxPay,SdUnionPay");
            strSql.Append(") values (");
            strSql.Append("@SdWxOfficalAccountPay,@SdWxAppPay,@SdWxQrCodePay,@SdAliQrCodePay,@BlTotalAmount,@BlAliPay,@BlWxPay,@BlUnionPay,@BlWxOfficalAccountPay,@BlWxAppPay,@OrderDate,@BlWxQrCodePay,@BlAliQrCodePay,@UserId,@UserName,@CreatedOn,@SdTotalAmount,@SdAliPay,@SdWxPay,@SdUnionPay");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@SdWxOfficalAccountPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdWxAppPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdWxQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdAliQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlTotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlAliPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlWxPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlUnionPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlWxOfficalAccountPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlWxAppPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderDate", SqlDbType.Date,3) ,
                        new SqlParameter("@BlWxQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlAliQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@SdTotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdAliPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdWxPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdUnionPay", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.SdWxOfficalAccountPay;
            parameters[1].Value = model.SdWxAppPay;
            parameters[2].Value = model.SdWxQrCodePay;
            parameters[3].Value = model.SdAliQrCodePay;
            parameters[4].Value = model.BlTotalAmount;
            parameters[5].Value = model.BlAliPay;
            parameters[6].Value = model.BlWxPay;
            parameters[7].Value = model.BlUnionPay;
            parameters[8].Value = model.BlWxOfficalAccountPay;
            parameters[9].Value = model.BlWxAppPay;
            parameters[10].Value = model.OrderDate;
            parameters[11].Value = model.BlWxQrCodePay;
            parameters[12].Value = model.BlAliQrCodePay;
            parameters[13].Value = model.UserId;
            parameters[14].Value = model.UserName;
            parameters[15].Value = model.CreatedOn;
            parameters[16].Value = model.SdTotalAmount;
            parameters[17].Value = model.SdAliPay;
            parameters[18].Value = model.SdWxPay;
            parameters[19].Value = model.SdUnionPay;

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
        public bool Update(JMP.MDL.jmp_bill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_bill set ");

            strSql.Append(" SdWxOfficalAccountPay = @SdWxOfficalAccountPay , ");
            strSql.Append(" SdWxAppPay = @SdWxAppPay , ");
            strSql.Append(" SdWxQrCodePay = @SdWxQrCodePay , ");
            strSql.Append(" SdAliQrCodePay = @SdAliQrCodePay , ");
            strSql.Append(" BlTotalAmount = @BlTotalAmount , ");
            strSql.Append(" BlAliPay = @BlAliPay , ");
            strSql.Append(" BlWxPay = @BlWxPay , ");
            strSql.Append(" BlUnionPay = @BlUnionPay , ");
            strSql.Append(" BlWxOfficalAccountPay = @BlWxOfficalAccountPay , ");
            strSql.Append(" BlWxAppPay = @BlWxAppPay , ");
            strSql.Append(" OrderDate = @OrderDate , ");
            strSql.Append(" BlWxQrCodePay = @BlWxQrCodePay , ");
            strSql.Append(" BlAliQrCodePay = @BlAliQrCodePay , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" SdTotalAmount = @SdTotalAmount , ");
            strSql.Append(" SdAliPay = @SdAliPay , ");
            strSql.Append(" SdWxPay = @SdWxPay , ");
            strSql.Append(" SdUnionPay = @SdUnionPay  ");
            strSql.Append(" where b_id=@b_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@b_id", SqlDbType.Int,4) ,
                        new SqlParameter("@SdWxOfficalAccountPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdWxAppPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdWxQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdAliQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlTotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlAliPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlWxPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlUnionPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlWxOfficalAccountPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlWxAppPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderDate", SqlDbType.Date,3) ,
                        new SqlParameter("@BlWxQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BlAliQrCodePay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@SdTotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdAliPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdWxPay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SdUnionPay", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.b_id;
            parameters[1].Value = model.SdWxOfficalAccountPay;
            parameters[2].Value = model.SdWxAppPay;
            parameters[3].Value = model.SdWxQrCodePay;
            parameters[4].Value = model.SdAliQrCodePay;
            parameters[5].Value = model.BlTotalAmount;
            parameters[6].Value = model.BlAliPay;
            parameters[7].Value = model.BlWxPay;
            parameters[8].Value = model.BlUnionPay;
            parameters[9].Value = model.BlWxOfficalAccountPay;
            parameters[10].Value = model.BlWxAppPay;
            parameters[11].Value = model.OrderDate;
            parameters[12].Value = model.BlWxQrCodePay;
            parameters[13].Value = model.BlAliQrCodePay;
            parameters[14].Value = model.UserId;
            parameters[15].Value = model.UserName;
            parameters[16].Value = model.CreatedOn;
            parameters[17].Value = model.SdTotalAmount;
            parameters[18].Value = model.SdAliPay;
            parameters[19].Value = model.SdWxPay;
            parameters[20].Value = model.SdUnionPay;
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
            strSql.Append("delete from jmp_bill ");
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
            strSql.Append("delete from jmp_bill ");
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
        public JMP.MDL.jmp_bill GetModel(int b_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b_id, SdWxOfficalAccountPay, SdWxAppPay, SdWxQrCodePay, SdAliQrCodePay, BlTotalAmount, BlAliPay, BlWxPay, BlUnionPay, BlWxOfficalAccountPay, BlWxAppPay, OrderDate, BlWxQrCodePay, BlAliQrCodePay, UserId, UserName, CreatedOn, SdTotalAmount, SdAliPay, SdWxPay, SdUnionPay  ");
            strSql.Append("  from jmp_bill ");
            strSql.Append(" where b_id=@b_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@b_id", SqlDbType.Int,4)
            };
            parameters[0].Value = b_id;


            JMP.MDL.jmp_bill model = new JMP.MDL.jmp_bill();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["b_id"].ToString() != "")
                {
                    model.b_id = int.Parse(ds.Tables[0].Rows[0]["b_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdWxOfficalAccountPay"].ToString() != "")
                {
                    model.SdWxOfficalAccountPay = decimal.Parse(ds.Tables[0].Rows[0]["SdWxOfficalAccountPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdWxAppPay"].ToString() != "")
                {
                    model.SdWxAppPay = decimal.Parse(ds.Tables[0].Rows[0]["SdWxAppPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdWxQrCodePay"].ToString() != "")
                {
                    model.SdWxQrCodePay = decimal.Parse(ds.Tables[0].Rows[0]["SdWxQrCodePay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdAliQrCodePay"].ToString() != "")
                {
                    model.SdAliQrCodePay = decimal.Parse(ds.Tables[0].Rows[0]["SdAliQrCodePay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlTotalAmount"].ToString() != "")
                {
                    model.BlTotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["BlTotalAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlAliPay"].ToString() != "")
                {
                    model.BlAliPay = decimal.Parse(ds.Tables[0].Rows[0]["BlAliPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlWxPay"].ToString() != "")
                {
                    model.BlWxPay = decimal.Parse(ds.Tables[0].Rows[0]["BlWxPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlUnionPay"].ToString() != "")
                {
                    model.BlUnionPay = decimal.Parse(ds.Tables[0].Rows[0]["BlUnionPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlWxOfficalAccountPay"].ToString() != "")
                {
                    model.BlWxOfficalAccountPay = decimal.Parse(ds.Tables[0].Rows[0]["BlWxOfficalAccountPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlWxAppPay"].ToString() != "")
                {
                    model.BlWxAppPay = decimal.Parse(ds.Tables[0].Rows[0]["BlWxAppPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderDate"].ToString() != "")
                {
                    model.OrderDate = DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlWxQrCodePay"].ToString() != "")
                {
                    model.BlWxQrCodePay = decimal.Parse(ds.Tables[0].Rows[0]["BlWxQrCodePay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlAliQrCodePay"].ToString() != "")
                {
                    model.BlAliQrCodePay = decimal.Parse(ds.Tables[0].Rows[0]["BlAliQrCodePay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdTotalAmount"].ToString() != "")
                {
                    model.SdTotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["SdTotalAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdAliPay"].ToString() != "")
                {
                    model.SdAliPay = decimal.Parse(ds.Tables[0].Rows[0]["SdAliPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdWxPay"].ToString() != "")
                {
                    model.SdWxPay = decimal.Parse(ds.Tables[0].Rows[0]["SdWxPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SdUnionPay"].ToString() != "")
                {
                    model.SdUnionPay = decimal.Parse(ds.Tables[0].Rows[0]["SdUnionPay"].ToString());
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
            strSql.Append(" FROM jmp_bill ");
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
            strSql.Append(" FROM jmp_bill ");
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
        public List<JMP.MDL.jmp_bill> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_bill>(dt);
        }



        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectSum(string sql)
        {
            return DbHelperSQL.Query(sql.ToString()).Tables[0];
        }
        /// <summary>
        /// 根据id字符串查询提款总金额
        /// </summary>
        /// <param name="bid">id字符串</param>
        /// <returns></returns>
        public JMP.MDL.jmp_bill GetselectSum(string bid)
        {
            string sql = string.Format(" select SUM(b_money) as b_money  from  jmp_bill where  b_id in(" + bid + ") and b_state='0' ");
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_bill>(dt);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_bill set b_state='1'  ");
            strSql.Append(" where b_id in (" + u_idlist + ")  ");
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

