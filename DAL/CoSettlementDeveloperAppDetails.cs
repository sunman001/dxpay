using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //[结算]按应用分组的开发者每日结算详情数据表
    public partial class CoSettlementDeveloperAppDetails
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoSettlementDeveloperAppDetails");
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
        public int Add(JMP.MDL.CoSettlementDeveloperAppDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoSettlementDeveloperAppDetails(");
            strSql.Append("TotalAmount,OriginServiceFee,ServiceFee,ServiceFeeRatio,IsSpecialApproval,SpecialApprovalServiceFee,SpecialApprovalFeeRatio,PortFee,PortFeeRatio,DefaultPortFeeRatio,SettlementDay,CreatedOn,ChannelId,ChannelName,ChannelCostFee,ChannelCostRatio,ChannelRefundAmount,DeveloperId,DeveloperName,AppId,AppName,PayModeId,PayModeName,OrderCount");
            strSql.Append(") values (");
            strSql.Append("@TotalAmount,@OriginServiceFee,@ServiceFee,@ServiceFeeRatio,@IsSpecialApproval,@SpecialApprovalServiceFee,@SpecialApprovalFeeRatio,@PortFee,@PortFeeRatio,@DefaultPortFeeRatio,@SettlementDay,@CreatedOn,@ChannelId,@ChannelName,@ChannelCostFee,@ChannelCostRatio,@ChannelRefundAmount,@DeveloperId,@DeveloperName,@AppId,@AppName,@PayModeId,@PayModeName,@OrderCount");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@TotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OriginServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@IsSpecialApproval", SqlDbType.Bit,1) ,
                        new SqlParameter("@SpecialApprovalServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SpecialApprovalFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@PortFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PortFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@DefaultPortFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@SettlementDay", SqlDbType.Date,3) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@ChannelCostFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ChannelCostRatio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ChannelRefundAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@PayModeId", SqlDbType.Int,4) ,
                        new SqlParameter("@PayModeName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@OrderCount", SqlDbType.Int,4)

            };

            parameters[0].Value = model.TotalAmount;
            parameters[1].Value = model.OriginServiceFee;
            parameters[2].Value = model.ServiceFee;
            parameters[3].Value = model.ServiceFeeRatio;
            parameters[4].Value = model.IsSpecialApproval;
            parameters[5].Value = model.SpecialApprovalServiceFee;
            parameters[6].Value = model.SpecialApprovalFeeRatio;
            parameters[7].Value = model.PortFee;
            parameters[8].Value = model.PortFeeRatio;
            parameters[9].Value = model.DefaultPortFeeRatio;
            parameters[10].Value = model.SettlementDay;
            parameters[11].Value = model.CreatedOn;
            parameters[12].Value = model.ChannelId;
            parameters[13].Value = model.ChannelName;
            parameters[14].Value = model.ChannelCostFee;
            parameters[15].Value = model.ChannelCostRatio;
            parameters[16].Value = model.ChannelRefundAmount;
            parameters[17].Value = model.DeveloperId;
            parameters[18].Value = model.DeveloperName;
            parameters[19].Value = model.AppId;
            parameters[20].Value = model.AppName;
            parameters[21].Value = model.PayModeId;
            parameters[22].Value = model.PayModeName;
            parameters[23].Value = model.OrderCount;

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
        public bool Update(JMP.MDL.CoSettlementDeveloperAppDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoSettlementDeveloperAppDetails set ");

            strSql.Append(" TotalAmount = @TotalAmount , ");
            strSql.Append(" OriginServiceFee = @OriginServiceFee , ");
            strSql.Append(" ServiceFee = @ServiceFee , ");
            strSql.Append(" ServiceFeeRatio = @ServiceFeeRatio , ");
            strSql.Append(" IsSpecialApproval = @IsSpecialApproval , ");
            strSql.Append(" SpecialApprovalServiceFee = @SpecialApprovalServiceFee , ");
            strSql.Append(" SpecialApprovalFeeRatio = @SpecialApprovalFeeRatio , ");
            strSql.Append(" PortFee = @PortFee , ");
            strSql.Append(" PortFeeRatio = @PortFeeRatio , ");
            strSql.Append(" DefaultPortFeeRatio = @DefaultPortFeeRatio , ");
            strSql.Append(" SettlementDay = @SettlementDay , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" ChannelId = @ChannelId , ");
            strSql.Append(" ChannelName = @ChannelName , ");
            strSql.Append(" ChannelCostFee = @ChannelCostFee , ");
            strSql.Append(" ChannelCostRatio = @ChannelCostRatio , ");
            strSql.Append(" ChannelRefundAmount = @ChannelRefundAmount , ");
            strSql.Append(" DeveloperId = @DeveloperId , ");
            strSql.Append(" DeveloperName = @DeveloperName , ");
            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" AppName = @AppName , ");
            strSql.Append(" PayModeId = @PayModeId , ");
            strSql.Append(" PayModeName = @PayModeName , ");
            strSql.Append(" OrderCount = @OrderCount  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@TotalAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OriginServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ServiceFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@IsSpecialApproval", SqlDbType.Bit,1) ,
                        new SqlParameter("@SpecialApprovalServiceFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SpecialApprovalFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@PortFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PortFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@DefaultPortFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@SettlementDay", SqlDbType.Date,3) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@ChannelCostFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ChannelCostRatio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ChannelRefundAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@PayModeId", SqlDbType.Int,4) ,
                        new SqlParameter("@PayModeName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@OrderCount", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.TotalAmount;
            parameters[2].Value = model.OriginServiceFee;
            parameters[3].Value = model.ServiceFee;
            parameters[4].Value = model.ServiceFeeRatio;
            parameters[5].Value = model.IsSpecialApproval;
            parameters[6].Value = model.SpecialApprovalServiceFee;
            parameters[7].Value = model.SpecialApprovalFeeRatio;
            parameters[8].Value = model.PortFee;
            parameters[9].Value = model.PortFeeRatio;
            parameters[10].Value = model.DefaultPortFeeRatio;
            parameters[11].Value = model.SettlementDay;
            parameters[12].Value = model.CreatedOn;
            parameters[13].Value = model.ChannelId;
            parameters[14].Value = model.ChannelName;
            parameters[15].Value = model.ChannelCostFee;
            parameters[16].Value = model.ChannelCostRatio;
            parameters[17].Value = model.ChannelRefundAmount;
            parameters[18].Value = model.DeveloperId;
            parameters[19].Value = model.DeveloperName;
            parameters[20].Value = model.AppId;
            parameters[21].Value = model.AppName;
            parameters[22].Value = model.PayModeId;
            parameters[23].Value = model.PayModeName;
            parameters[24].Value = model.OrderCount;
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
            strSql.Append("delete from CoSettlementDeveloperAppDetails ");
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
            strSql.Append("delete from CoSettlementDeveloperAppDetails ");
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
        public JMP.MDL.CoSettlementDeveloperAppDetails GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, TotalAmount, OriginServiceFee, ServiceFee, ServiceFeeRatio, IsSpecialApproval, SpecialApprovalServiceFee, SpecialApprovalFeeRatio, PortFee, PortFeeRatio, DefaultPortFeeRatio, SettlementDay, CreatedOn, ChannelId, ChannelName, ChannelCostFee, ChannelCostRatio, ChannelRefundAmount, DeveloperId, DeveloperName, AppId, AppName, PayModeId, PayModeName, OrderCount  ");
            strSql.Append("  from CoSettlementDeveloperAppDetails ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoSettlementDeveloperAppDetails model = new JMP.MDL.CoSettlementDeveloperAppDetails();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalAmount"].ToString() != "")
                {
                    model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["TotalAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OriginServiceFee"].ToString() != "")
                {
                    model.OriginServiceFee = decimal.Parse(ds.Tables[0].Rows[0]["OriginServiceFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFee"].ToString() != "")
                {
                    model.ServiceFee = decimal.Parse(ds.Tables[0].Rows[0]["ServiceFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString() != "")
                {
                    model.ServiceFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSpecialApproval"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsSpecialApproval"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsSpecialApproval"].ToString().ToLower() == "true"))
                    {
                        model.IsSpecialApproval = true;
                    }
                    else
                    {
                        model.IsSpecialApproval = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["SpecialApprovalServiceFee"].ToString() != "")
                {
                    model.SpecialApprovalServiceFee = decimal.Parse(ds.Tables[0].Rows[0]["SpecialApprovalServiceFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SpecialApprovalFeeRatio"].ToString() != "")
                {
                    model.SpecialApprovalFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["SpecialApprovalFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PortFee"].ToString() != "")
                {
                    model.PortFee = decimal.Parse(ds.Tables[0].Rows[0]["PortFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PortFeeRatio"].ToString() != "")
                {
                    model.PortFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["PortFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DefaultPortFeeRatio"].ToString() != "")
                {
                    model.DefaultPortFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["DefaultPortFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SettlementDay"].ToString() != "")
                {
                    model.SettlementDay = DateTime.Parse(ds.Tables[0].Rows[0]["SettlementDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                model.ChannelName = ds.Tables[0].Rows[0]["ChannelName"].ToString();
                if (ds.Tables[0].Rows[0]["ChannelCostFee"].ToString() != "")
                {
                    model.ChannelCostFee = decimal.Parse(ds.Tables[0].Rows[0]["ChannelCostFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelCostRatio"].ToString() != "")
                {
                    model.ChannelCostRatio = decimal.Parse(ds.Tables[0].Rows[0]["ChannelCostRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelRefundAmount"].ToString() != "")
                {
                    model.ChannelRefundAmount = decimal.Parse(ds.Tables[0].Rows[0]["ChannelRefundAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeveloperId"].ToString() != "")
                {
                    model.DeveloperId = int.Parse(ds.Tables[0].Rows[0]["DeveloperId"].ToString());
                }
                model.DeveloperName = ds.Tables[0].Rows[0]["DeveloperName"].ToString();
                if (ds.Tables[0].Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(ds.Tables[0].Rows[0]["AppId"].ToString());
                }
                model.AppName = ds.Tables[0].Rows[0]["AppName"].ToString();
                if (ds.Tables[0].Rows[0]["PayModeId"].ToString() != "")
                {
                    model.PayModeId = int.Parse(ds.Tables[0].Rows[0]["PayModeId"].ToString());
                }
                model.PayModeName = ds.Tables[0].Rows[0]["PayModeName"].ToString();
                if (ds.Tables[0].Rows[0]["OrderCount"].ToString() != "")
                {
                    model.OrderCount = int.Parse(ds.Tables[0].Rows[0]["OrderCount"].ToString());
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
            strSql.Append(" FROM CoSettlementDeveloperAppDetails ");
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
            strSql.Append(" FROM CoSettlementDeveloperAppDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询明细根据支付方式分组
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataSet GetModelKFZ(int id, string date)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select PayModeName,PortFeeRatio,ServiceFeeRatio,IsSpecialApproval,SpecialApprovalFeeRatio,(isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0)) as TotalAmount, isnull(SUM(PortFee), 0) as PortFee,ISNULL(SUM(ChannelRefundAmount),0) as ChannelRefundAmount,isnull(SUM(OrderCount), 0) as OrderCount, isnull(SUM(ServiceFee), 0) as ServiceFee,(isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0) - isnull(SUM(PortFee), 0) - isnull(SUM(ServiceFee), 0)) as KFZIncome ");
            strsql.Append(" from [dbo].[CoSettlementDeveloperAppDetails] where DeveloperId='" + id + "'");
            strsql.Append(" and SettlementDay='" + date + "'");
            strsql.Append(" group by PayModeName,PortFeeRatio,ServiceFeeRatio,IsSpecialApproval,SpecialApprovalFeeRatio");

            return DbHelperSQLTotal.Query(strsql.ToString());
        }

        /// <summary>
        /// 查询明细根据应用分组
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelAppName(int id, string date)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select AppId,SettlementDay,AppName,a_platform_id,(isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0)) as TotalAmount,isnull(SUM(PortFee), 0) as PortFee,ISNULL(SUM(ChannelRefundAmount),0) as ChannelRefundAmount,(isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0) - isnull(SUM(PortFee), 0) - isnull(SUM(ServiceFee), 0)) as KFZIncome");
            strsql.Append(" from CoSettlementDeveloperAppDetails a inner join dx_base.dbo.jmp_app b on a.AppId = b.a_id");
            strsql.Append(" where DeveloperId='" + id + "' and SettlementDay='" + date + "' ");
            strsql.Append(" group by AppId,SettlementDay,AppName,a_platform_id");

            return DbHelperSQLTotal.Query(strsql.ToString());
        }

        /// <summary>
        /// 查询明细根据支付方式分组
        /// </summary>
        /// <param name="id">APPID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataSet GetModelPayType(int id, string date)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select PayModeName,PortFeeRatio,ServiceFeeRatio,IsSpecialApproval,SpecialApprovalFeeRatio,(isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0)) as TotalAmount, isnull(SUM(PortFee),0) as PortFee,ISNULL(SUM(ChannelRefundAmount),0) as ChannelRefundAmount, isnull(SUM(OrderCount),0) as OrderCount,isnull(SUM(ServiceFee), 0) as ServiceFee, (isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0) - isnull(SUM(PortFee), 0) - isnull(SUM(ServiceFee), 0)) as KFZIncome ");
            strsql.Append(" from [dbo].[CoSettlementDeveloperAppDetails] where AppId='" + id + "'");
            strsql.Append(" and SettlementDay='" + date + "'");
            strsql.Append(" group by PayModeName,PortFeeRatio,ServiceFeeRatio,IsSpecialApproval,SpecialApprovalFeeRatio");

            return DbHelperSQLTotal.Query(strsql.ToString());
        }

        /// <summary>
        /// 开发者首页根据开发者查询数据（昨天，本月，上月）
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="date">日期</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public JMP.MDL.CoSettlementDeveloperAppDetails GetModelKFZ_total(int id, string date, int start)
        {
            StringBuilder strsql = new StringBuilder();

            strsql.Append("select ISNULL(SUM(OrderCount),0) as OrderCount,(isnull(SUM(TotalAmount), 0) - isnull(SUM(ChannelRefundAmount), 0)) as TotalAmount,(ISNULL(SUM(TotalAmount),0)-ISNULL(SUM(ServiceFee),0)-ISNULL(SUM(PortFee),0)-ISNULL(SUM(ChannelRefundAmount),0)) as KFZIncome");
            strsql.Append(" from dx_total.[dbo].[CoSettlementDeveloperAppDetails] a, dx_base.dbo.jmp_user b ");
            strsql.Append(" where a.DeveloperId=b.u_id and b.u_id='" + id + "'");
            if (start == 0)
            {
                strsql.Append(" and a.SettlementDay='" + date + "'");
            }
            else
            {
                strsql.Append(" and CONVERT(varchar(7),SettlementDay,120)='" + date + "'");
            }

            DataTable dt = DbHelperSQLTotal.Query(strsql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.CoSettlementDeveloperAppDetails>(dt);

        }
    }
}

