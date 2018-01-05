using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //投诉订单表
    public partial class CsComplainOrder
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CsComplainOrder");
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
        public int Add(JMP.MDL.CsComplainOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CsComplainOrder(");
            strSql.Append("Envidence,HandlerId,HandlerName,HandleDate,HandleResult,state,FounderId,FounderName,IsRefund,Price,OrderNumber,DownstreamStartTime,DownstreamEndTime,ChannelId,OrderTable,UserId,AppId,ComplainTypeId,ComplainTypeName,ComplainDate,CreatedOn");
            strSql.Append(") values (");
            strSql.Append("@Envidence,@HandlerId,@HandlerName,@HandleDate,@HandleResult,@state,@FounderId,@FounderName,@IsRefund,@Price,@OrderNumber,@DownstreamStartTime,@DownstreamEndTime,@ChannelId,@OrderTable,@UserId,@AppId,@ComplainTypeId,@ComplainTypeName,@ComplainDate,@CreatedOn");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@Envidence", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@HandlerId", SqlDbType.Int,4) ,
                        new SqlParameter("@HandlerName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@HandleDate", SqlDbType.DateTime) ,
                        new SqlParameter("@HandleResult", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@state", SqlDbType.Int,4) ,
                        new SqlParameter("@FounderId", SqlDbType.Int,4) ,
                        new SqlParameter("@FounderName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IsRefund", SqlDbType.Bit,1) ,
                        new SqlParameter("@Price", SqlDbType.Money,8) ,
                        new SqlParameter("@OrderNumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@DownstreamStartTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DownstreamEndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderTable", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@ComplainTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ComplainTypeName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ComplainDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Envidence;
            parameters[1].Value = model.HandlerId;
            parameters[2].Value = model.HandlerName;
            parameters[3].Value = model.HandleDate;
            parameters[4].Value = model.HandleResult;
            parameters[5].Value = model.state;
            parameters[6].Value = model.FounderId;
            parameters[7].Value = model.FounderName;
            parameters[8].Value = model.IsRefund;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.OrderNumber;
            parameters[11].Value = model.DownstreamStartTime;
            parameters[12].Value = model.DownstreamEndTime;
            parameters[13].Value = model.ChannelId;
            parameters[14].Value = model.OrderTable;
            parameters[15].Value = model.UserId;
            parameters[16].Value = model.AppId;
            parameters[17].Value = model.ComplainTypeId;
            parameters[18].Value = model.ComplainTypeName;
            parameters[19].Value = model.ComplainDate;
            parameters[20].Value = model.CreatedOn;

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
        public bool Update(JMP.MDL.CsComplainOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CsComplainOrder set ");

            strSql.Append(" Envidence = @Envidence , ");
            strSql.Append(" HandlerId = @HandlerId , ");
            strSql.Append(" HandlerName = @HandlerName , ");
            strSql.Append(" HandleDate = @HandleDate , ");
            strSql.Append(" HandleResult = @HandleResult , ");
            strSql.Append(" state = @state , ");
            strSql.Append(" FounderId = @FounderId , ");
            strSql.Append(" FounderName = @FounderName , ");
            strSql.Append(" IsRefund = @IsRefund , ");
            strSql.Append(" Price = @Price , ");
            strSql.Append(" OrderNumber = @OrderNumber , ");
            strSql.Append(" DownstreamStartTime = @DownstreamStartTime , ");
            strSql.Append(" DownstreamEndTime = @DownstreamEndTime , ");
            strSql.Append(" ChannelId = @ChannelId , ");
            strSql.Append(" OrderTable = @OrderTable , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" ComplainTypeId = @ComplainTypeId , ");
            strSql.Append(" ComplainTypeName = @ComplainTypeName , ");
            strSql.Append(" ComplainDate = @ComplainDate , ");
            strSql.Append(" CreatedOn = @CreatedOn  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@Envidence", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@HandlerId", SqlDbType.Int,4) ,
                        new SqlParameter("@HandlerName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@HandleDate", SqlDbType.DateTime) ,
                        new SqlParameter("@HandleResult", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@state", SqlDbType.Int,4) ,
                        new SqlParameter("@FounderId", SqlDbType.Int,4) ,
                        new SqlParameter("@FounderName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IsRefund", SqlDbType.Bit,1) ,
                        new SqlParameter("@Price", SqlDbType.Money,8) ,
                        new SqlParameter("@OrderNumber", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@DownstreamStartTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DownstreamEndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderTable", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@ComplainTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ComplainTypeName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ComplainDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Envidence;
            parameters[2].Value = model.HandlerId;
            parameters[3].Value = model.HandlerName;
            parameters[4].Value = model.HandleDate;
            parameters[5].Value = model.HandleResult;
            parameters[6].Value = model.state;
            parameters[7].Value = model.FounderId;
            parameters[8].Value = model.FounderName;
            parameters[9].Value = model.IsRefund;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.OrderNumber;
            parameters[12].Value = model.DownstreamStartTime;
            parameters[13].Value = model.DownstreamEndTime;
            parameters[14].Value = model.ChannelId;
            parameters[15].Value = model.OrderTable;
            parameters[16].Value = model.UserId;
            parameters[17].Value = model.AppId;
            parameters[18].Value = model.ComplainTypeId;
            parameters[19].Value = model.ComplainTypeName;
            parameters[20].Value = model.ComplainDate;
            parameters[21].Value = model.CreatedOn;
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
            strSql.Append("delete from CsComplainOrder ");
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
            strSql.Append("delete from CsComplainOrder ");
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
        public JMP.MDL.CsComplainOrder GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Envidence, HandlerId, HandlerName, HandleDate, HandleResult, state, FounderId, FounderName, IsRefund, Price, OrderNumber, DownstreamStartTime, DownstreamEndTime, ChannelId, OrderTable, UserId, AppId, ComplainTypeId, ComplainTypeName, ComplainDate, CreatedOn  ");
            strSql.Append("  from CsComplainOrder ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CsComplainOrder model = new JMP.MDL.CsComplainOrder();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Envidence = ds.Tables[0].Rows[0]["Envidence"].ToString();
                if (ds.Tables[0].Rows[0]["HandlerId"].ToString() != "")
                {
                    model.HandlerId = int.Parse(ds.Tables[0].Rows[0]["HandlerId"].ToString());
                }
                model.HandlerName = ds.Tables[0].Rows[0]["HandlerName"].ToString();
                if (ds.Tables[0].Rows[0]["HandleDate"].ToString() != "")
                {
                    model.HandleDate = DateTime.Parse(ds.Tables[0].Rows[0]["HandleDate"].ToString());
                }
                model.HandleResult = ds.Tables[0].Rows[0]["HandleResult"].ToString();
                if (ds.Tables[0].Rows[0]["state"].ToString() != "")
                {
                    model.state = int.Parse(ds.Tables[0].Rows[0]["state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FounderId"].ToString() != "")
                {
                    model.FounderId = int.Parse(ds.Tables[0].Rows[0]["FounderId"].ToString());
                }
                model.FounderName = ds.Tables[0].Rows[0]["FounderName"].ToString();
                if (ds.Tables[0].Rows[0]["IsRefund"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsRefund"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRefund"].ToString().ToLower() == "true"))
                    {
                        model.IsRefund = true;
                    }
                    else
                    {
                        model.IsRefund = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                model.OrderNumber = ds.Tables[0].Rows[0]["OrderNumber"].ToString();
                if (ds.Tables[0].Rows[0]["DownstreamStartTime"].ToString() != "")
                {
                    model.DownstreamStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["DownstreamStartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DownstreamEndTime"].ToString() != "")
                {
                    model.DownstreamEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["DownstreamEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                model.OrderTable = ds.Tables[0].Rows[0]["OrderTable"].ToString();
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(ds.Tables[0].Rows[0]["AppId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ComplainTypeId"].ToString() != "")
                {
                    model.ComplainTypeId = int.Parse(ds.Tables[0].Rows[0]["ComplainTypeId"].ToString());
                }
                model.ComplainTypeName = ds.Tables[0].Rows[0]["ComplainTypeName"].ToString();
                if (ds.Tables[0].Rows[0]["ComplainDate"].ToString() != "")
                {
                    model.ComplainDate = DateTime.Parse(ds.Tables[0].Rows[0]["ComplainDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
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
            strSql.Append(" FROM CsComplainOrder ");
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
            strSql.Append(" FROM CsComplainOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 查询投诉
        /// </summary>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CsComplainOrder> SelectList( string SeachDate, string stime,  string etime, string sea_name, string type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.*,b.a_name,c.u_realname FROM dx_base.dbo.CsComplainOrder a left join dx_base.dbo.jmp_app b on a.AppId = b.a_id left join dx_base.dbo.jmp_user c on a.UserId = c.u_id where 1=1");
            string Order = "";
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "0":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and a.[OrderNumber] like '%" + sea_name + "%' ";
                        }
                        break;

                    case "1":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and a.[ComplainTypeName] like '%" + sea_name + "%' ";
                        }
                        break;
                    case "2":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and b.[a_name] like '%" + sea_name + "%' ";
                        }
                        break;
                    case "3":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and c.[u_realname] like '%" + sea_name + "%' ";
                        }
                        break;
                }

            }
            if(!string.IsNullOrEmpty(SeachDate))
            {
                switch (SeachDate)
                {
                    case "0":
                        if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                        {
                            sql += " and a.ComplainDate>='" + stime + " 00:00:00' and a.ComplainDate<='" + etime + " 23:59:59' ";
                        }
                        break;

                    case "1":
                        if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                        {
                            sql += " and a.HandleDate>='" + stime + " 00:00:00' and a.HandleDate<='" + etime + " 23:59:59' ";
                        }
                        break;
                  
                }
            }
            if (SelectState > -1)
            {
                sql += " and a.[state]='" + SelectState + "' ";
            }
            if (searchDesc == 0)
            {
                Order = "order by Id desc";
            }
            else
            {
                Order = "order by Id ";
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
            return DbHelperSQL.ToList<JMP.MDL.CsComplainOrder>(ds.Tables[0]);
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateCustomState(string idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update CsComplainOrder set [state]=" + state + "  ");
            strSql.Append(" where Id in (" + idlist + ")  ");
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
        /// 处理投诉
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="HandlerId">处理者id</param>
        /// <param name="HandlerName">处理者姓名</param>
        /// <param name="HandleResult">处理结果</param>
        /// <param name="isRefund">是否退款</param>
        public bool UpdateCustomHandleResult(int id, int HandlerId, string HandlerName, string HandleResult, bool isRefund)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update CsComplainOrder set HandlerId='" + HandlerId + "',HandlerName='" + HandlerName + "',HandleDate=getdate(),HandleResult='" + HandleResult + "',IsRefund=" + (isRefund ? 1 : 0));
            strSql.Append(" where Id = " + id + "");
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
        /// 根据订单号查询投诉类型
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public JMP.MDL.CsComplainOrder SelectListOrder(string order)
        {
            string sql = string.Format("select b.Name from CsComplainOrder as a left join CsComplainType as b on a.ComplainTypeId=b.Id where a.OrderNumber=@order");
            SqlParameter par = new SqlParameter("@order", order);
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.CsComplainOrder>(dt);
        }
    }
}

