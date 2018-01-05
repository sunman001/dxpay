using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
   public  class jmp_AppChannelReport
    {
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_AppChannelReport");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_AppChannelReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_AppChannelReport(");
            strSql.Append("CreatedOn,CreatedDate,ChannelId,ChannelName,PayTypeName,AppId,Money,Notpay,Success,Successratio");
            strSql.Append(") values (");
            strSql.Append("@CreatedOn,@CreatedDate,@ChannelId,@ChannelName,@PayTypeName,@AppId,@Money,@Notpay,@Success,@Successratio");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@PayTypeName", SqlDbType.NChar,10) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@Money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Notpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Success", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Successratio", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.CreatedOn;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.ChannelId;
            parameters[3].Value = model.ChannelName;
            parameters[4].Value = model.PayTypeName;
            parameters[5].Value = model.AppId;
            parameters[6].Value = model.Money;
            parameters[7].Value = model.Notpay;
            parameters[8].Value = model.Success;
            parameters[9].Value = model.Successratio;

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
        public bool Update(JMP.MDL.jmp_AppChannelReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_AppChannelReport set ");

            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedDate = @CreatedDate , ");
            strSql.Append(" ChannelId = @ChannelId , ");
            strSql.Append(" ChannelName = @ChannelName , ");
            strSql.Append(" PayTypeName = @PayTypeName , ");
            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" Money = @Money , ");
            strSql.Append(" Notpay = @Notpay , ");
            strSql.Append(" Success = @Success , ");
            strSql.Append(" Successratio = @Successratio  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelName", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@PayTypeName", SqlDbType.NChar,10) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@Money", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Notpay", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Success", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Successratio", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.ID;
            parameters[1].Value = model.CreatedOn;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.ChannelId;
            parameters[4].Value = model.ChannelName;
            parameters[5].Value = model.PayTypeName;
            parameters[6].Value = model.AppId;
            parameters[7].Value = model.Money;
            parameters[8].Value = model.Notpay;
            parameters[9].Value = model.Success;
            parameters[10].Value = model.Successratio;
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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_AppChannelReport ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_AppChannelReport ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public JMP.MDL.jmp_AppChannelReport GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, CreatedOn, CreatedDate, ChannelId, ChannelName, PayTypeName, AppId, Money, Notpay, Success, Successratio  ");
            strSql.Append("  from jmp_AppChannelReport ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


            JMP.MDL.jmp_AppChannelReport model = new JMP.MDL.jmp_AppChannelReport();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                model.ChannelName = ds.Tables[0].Rows[0]["ChannelName"].ToString();
                model.PayTypeName = ds.Tables[0].Rows[0]["PayTypeName"].ToString();
                if (ds.Tables[0].Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(ds.Tables[0].Rows[0]["AppId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Money"].ToString() != "")
                {
                    model.Money = decimal.Parse(ds.Tables[0].Rows[0]["Money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Notpay"].ToString() != "")
                {
                    model.Notpay = decimal.Parse(ds.Tables[0].Rows[0]["Notpay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Success"].ToString() != "")
                {
                    model.Success = decimal.Parse(ds.Tables[0].Rows[0]["Success"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Successratio"].ToString() != "")
                {
                    model.Successratio = decimal.Parse(ds.Tables[0].Rows[0]["Successratio"].ToString());
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
            strSql.Append(" FROM jmp_AppChannelReport ");
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
            strSql.Append(" FROM jmp_AppChannelReport ");
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
