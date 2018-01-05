using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //[结算]按通道和应用分组的开发者成本详情统计表
    public partial class CoSettlementChannelCost
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoSettlementChannelCost");
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
        public int Add(JMP.MDL.CoSettlementChannelCost model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoSettlementChannelCost(");
            strSql.Append("TotalAmount,CostRatio,CostFee,CreatedOn,SettlementDay,DeveloperId,DeveloperName,AppId,AppName,ChannelId,ChannelName,OrderCount");
            strSql.Append(") values (");
            strSql.Append("@TotalAmount,@CostRatio,@CostFee,@CreatedOn,@SettlementDay,@DeveloperId,@DeveloperName,@AppId,@AppName,@ChannelId,@ChannelName,@OrderCount");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@TotalAmount", SqlDbType.Money,8) ,
                        new SqlParameter("@CostRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@CostFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@SettlementDay", SqlDbType.VarChar,10) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperName", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppName", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@OrderCount", SqlDbType.Int,4)

            };

            parameters[0].Value = model.TotalAmount;
            parameters[1].Value = model.CostRatio;
            parameters[2].Value = model.CostFee;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.SettlementDay;
            parameters[5].Value = model.DeveloperId;
            parameters[6].Value = model.DeveloperName;
            parameters[7].Value = model.AppId;
            parameters[8].Value = model.AppName;
            parameters[9].Value = model.ChannelId;
            parameters[10].Value = model.ChannelName;
            parameters[11].Value = model.OrderCount;

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
        public bool Update(JMP.MDL.CoSettlementChannelCost model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoSettlementChannelCost set ");

            strSql.Append(" TotalAmount = @TotalAmount , ");
            strSql.Append(" CostRatio = @CostRatio , ");
            strSql.Append(" CostFee = @CostFee , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" SettlementDay = @SettlementDay , ");
            strSql.Append(" DeveloperId = @DeveloperId , ");
            strSql.Append(" DeveloperName = @DeveloperName , ");
            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" AppName = @AppName , ");
            strSql.Append(" ChannelId = @ChannelId , ");
            strSql.Append(" ChannelName = @ChannelName , ");
            strSql.Append(" OrderCount = @OrderCount  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@TotalAmount", SqlDbType.Money,8) ,
                        new SqlParameter("@CostRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@CostFee", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@SettlementDay", SqlDbType.VarChar,10) ,
                        new SqlParameter("@DeveloperId", SqlDbType.Int,4) ,
                        new SqlParameter("@DeveloperName", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppName", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@OrderCount", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.TotalAmount;
            parameters[2].Value = model.CostRatio;
            parameters[3].Value = model.CostFee;
            parameters[4].Value = model.CreatedOn;
            parameters[5].Value = model.SettlementDay;
            parameters[6].Value = model.DeveloperId;
            parameters[7].Value = model.DeveloperName;
            parameters[8].Value = model.AppId;
            parameters[9].Value = model.AppName;
            parameters[10].Value = model.ChannelId;
            parameters[11].Value = model.ChannelName;
            parameters[12].Value = model.OrderCount;
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
            strSql.Append("delete from CoSettlementChannelCost ");
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
            strSql.Append("delete from CoSettlementChannelCost ");
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
        public JMP.MDL.CoSettlementChannelCost GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, TotalAmount, CostRatio, CostFee, CreatedOn, SettlementDay, DeveloperId, DeveloperName, AppId, AppName, ChannelId, ChannelName, OrderCount  ");
            strSql.Append("  from CoSettlementChannelCost ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoSettlementChannelCost model = new JMP.MDL.CoSettlementChannelCost();
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
                if (ds.Tables[0].Rows[0]["CostRatio"].ToString() != "")
                {
                    model.CostRatio = decimal.Parse(ds.Tables[0].Rows[0]["CostRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostFee"].ToString() != "")
                {
                    model.CostFee = decimal.Parse(ds.Tables[0].Rows[0]["CostFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                model.SettlementDay =DateTime.Parse( ds.Tables[0].Rows[0]["SettlementDay"].ToString());
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
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                model.ChannelName = ds.Tables[0].Rows[0]["ChannelName"].ToString();
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
            strSql.Append(" FROM CoSettlementChannelCost ");
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
            strSql.Append(" FROM CoSettlementChannelCost ");
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



    }
}

