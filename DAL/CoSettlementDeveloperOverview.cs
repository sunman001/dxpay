using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //开发者每日结算详情表
    public partial class CoSettlementDeveloperOverview
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoSettlementDeveloperOverview");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CoSettlementDeveloperOverview model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoSettlementDeveloperOverview(");
            strSql.Append("BpPushMoneyRatio,AgentPushMoney,AgentPushMoneyRatio,PortFee,CostFee,DeveloperId,DeveloperName,SettlementDay,CreatedOn,TotalAmount,ServiceFee,ServiceFeeRatio,BpPushMoney");
            strSql.Append(") values (");
            strSql.Append("@BpPushMoneyRatio,@AgentPushMoney,@AgentPushMoneyRatio,@PortFee,@CostFee,@DeveloperId,@DeveloperName,@SettlementDay,@CreatedOn,@TotalAmount,@ServiceFee,@ServiceFeeRatio,@BpPushMoney");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@BpPushMoneyRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@AgentPushMoney", SqlDbType.Decimal,9) ,
                        new SqlParameter("@AgentPushMoneyRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@PortFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CostFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@SettlementDay", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@TotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@BpPushMoney", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.BpPushMoneyRatio;
            parameters[1].Value = model.AgentPushMoney;
            parameters[2].Value = model.AgentPushMoneyRatio;
            parameters[3].Value = model.PortFee;
            parameters[4].Value = model.CostFee;
            parameters[5].Value = model.DeveloperId;
            parameters[6].Value = model.DeveloperName;
            parameters[7].Value = model.SettlementDay;
            parameters[8].Value = model.CreatedOn;
            parameters[9].Value = model.TotalAmount;
            parameters[10].Value = model.ServiceFee;
            parameters[11].Value = model.ServiceFeeRatio;
            parameters[12].Value = model.BpPushMoney;

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
        public bool Update(JMP.MDL.CoSettlementDeveloperOverview model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoSettlementDeveloperOverview set ");

            strSql.Append(" BpPushMoneyRatio = @BpPushMoneyRatio , ");
            strSql.Append(" AgentPushMoney = @AgentPushMoney , ");
            strSql.Append(" AgentPushMoneyRatio = @AgentPushMoneyRatio , ");
            strSql.Append(" PortFee = @PortFee , ");
            strSql.Append(" CostFee = @CostFee , ");
            strSql.Append(" DeveloperId = @DeveloperId , ");
            strSql.Append(" DeveloperName = @DeveloperName , ");
            strSql.Append(" SettlementDay = @SettlementDay , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" TotalAmount = @TotalAmount , ");
            strSql.Append(" ServiceFee = @ServiceFee , ");
            strSql.Append(" ServiceFeeRatio = @ServiceFeeRatio , ");
            strSql.Append(" BpPushMoney = @BpPushMoney  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@BpPushMoneyRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@AgentPushMoney", SqlDbType.Decimal,9) ,
                        new SqlParameter("@AgentPushMoneyRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@PortFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CostFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@SettlementDay", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@TotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@BpPushMoney", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.BpPushMoneyRatio;
            parameters[2].Value = model.AgentPushMoney;
            parameters[3].Value = model.AgentPushMoneyRatio;
            parameters[4].Value = model.PortFee;
            parameters[5].Value = model.CostFee;
            parameters[6].Value = model.DeveloperId;
            parameters[7].Value = model.DeveloperName;
            parameters[8].Value = model.SettlementDay;
            parameters[9].Value = model.CreatedOn;
            parameters[10].Value = model.TotalAmount;
            parameters[11].Value = model.ServiceFee;
            parameters[12].Value = model.ServiceFeeRatio;
            parameters[13].Value = model.BpPushMoney;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CoSettlementDeveloperOverview ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CoSettlementDeveloperOverview ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
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
        public JMP.MDL.CoSettlementDeveloperOverview GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, BpPushMoneyRatio, AgentPushMoney, AgentPushMoneyRatio, PortFee, CostFee, DeveloperId, DeveloperName, SettlementDay, CreatedOn, TotalAmount, ServiceFee, ServiceFeeRatio, BpPushMoney  ");
            strSql.Append("  from CoSettlementDeveloperOverview ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoSettlementDeveloperOverview model = new JMP.MDL.CoSettlementDeveloperOverview();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BpPushMoneyRatio"].ToString() != "")
                {
                    model.BpPushMoneyRatio = decimal.Parse(ds.Tables[0].Rows[0]["BpPushMoneyRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgentPushMoney"].ToString() != "")
                {
                    model.AgentPushMoney = decimal.Parse(ds.Tables[0].Rows[0]["AgentPushMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgentPushMoneyRatio"].ToString() != "")
                {
                    model.AgentPushMoneyRatio = decimal.Parse(ds.Tables[0].Rows[0]["AgentPushMoneyRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PortFee"].ToString() != "")
                {
                    model.PortFee = decimal.Parse(ds.Tables[0].Rows[0]["PortFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostFee"].ToString() != "")
                {
                    model.CostFee = decimal.Parse(ds.Tables[0].Rows[0]["CostFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeveloperId"].ToString() != "")
                {
                    model.DeveloperId = int.Parse(ds.Tables[0].Rows[0]["DeveloperId"].ToString());
                }
                model.DeveloperName = ds.Tables[0].Rows[0]["DeveloperName"].ToString();
                if (ds.Tables[0].Rows[0]["SettlementDay"].ToString() != "")
                {
                    model.SettlementDay = DateTime.Parse(ds.Tables[0].Rows[0]["SettlementDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalAmount"].ToString() != "")
                {
                    model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["TotalAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFee"].ToString() != "")
                {
                    model.ServiceFee = decimal.Parse(ds.Tables[0].Rows[0]["ServiceFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString() != "")
                {
                    model.ServiceFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BpPushMoney"].ToString() != "")
                {
                    model.BpPushMoney = decimal.Parse(ds.Tables[0].Rows[0]["BpPushMoney"].ToString());
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
            strSql.Append(" FROM CoSettlementDeveloperOverview ");
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
            strSql.Append(" FROM CoSettlementDeveloperOverview ");
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
        public List<JMP.MDL.CoSettlementDeveloperOverview> GetLists(string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            SqlConnection con = new SqlConnection(DbHelperSQLTotal.connectionString);
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
            return DbHelperSQLTotal.ToList<JMP.MDL.CoSettlementDeveloperOverview>(dt);
        }


        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable SelectSum(string sql)
        {
            return DbHelperSQLTotal.Query(sql.ToString()).Tables[0];
        }

    }
}

