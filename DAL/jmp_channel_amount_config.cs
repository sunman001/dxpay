using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //根据时间段设置通道池每次调用支付通道的数量
    public partial class jmp_channel_amount_config
    {


        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_channel_amount_config");
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
        public int Add(JMP.MDL.jmp_channel_amount_config model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_channel_amount_config(");
            strSql.Append("ChannelPoolId,WhichHour,Amount,CreatedOn,CreatedByUserId,CreatedByUserName");
            strSql.Append(") values (");
            strSql.Append("@ChannelPoolId,@WhichHour,@Amount,@CreatedOn,@CreatedByUserId,@CreatedByUserName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ChannelPoolId", SqlDbType.Int,4) ,
                        new SqlParameter("@WhichHour", SqlDbType.Int,4) ,
                        new SqlParameter("@Amount", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUserName", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.ChannelPoolId;
            parameters[1].Value = model.WhichHour;
            parameters[2].Value = model.Amount;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedByUserId;
            parameters[5].Value = model.CreatedByUserName;

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
        public bool Update(JMP.MDL.jmp_channel_amount_config model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_channel_amount_config set ");

            strSql.Append(" ChannelPoolId = @ChannelPoolId , ");
            strSql.Append(" WhichHour = @WhichHour , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedByUserId = @CreatedByUserId , ");
            strSql.Append(" CreatedByUserName = @CreatedByUserName  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelPoolId", SqlDbType.Int,4) ,
                        new SqlParameter("@WhichHour", SqlDbType.Int,4) ,
                        new SqlParameter("@Amount", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUserName", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.ChannelPoolId;
            parameters[2].Value = model.WhichHour;
            parameters[3].Value = model.Amount;
            parameters[4].Value = model.CreatedOn;
            parameters[5].Value = model.CreatedByUserId;
            parameters[6].Value = model.CreatedByUserName;
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
            strSql.Append("delete from jmp_channel_amount_config ");
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
            strSql.Append("delete from jmp_channel_amount_config ");
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
        public JMP.MDL.jmp_channel_amount_config GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ChannelPoolId, WhichHour, Amount, CreatedOn, CreatedByUserId, CreatedByUserName  ");
            strSql.Append("  from jmp_channel_amount_config ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.jmp_channel_amount_config model = new JMP.MDL.jmp_channel_amount_config();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelPoolId"].ToString() != "")
                {
                    model.ChannelPoolId = int.Parse(ds.Tables[0].Rows[0]["ChannelPoolId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WhichHour"].ToString() != "")
                {
                    model.WhichHour = int.Parse(ds.Tables[0].Rows[0]["WhichHour"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = int.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedByUserId"].ToString() != "")
                {
                    model.CreatedByUserId = int.Parse(ds.Tables[0].Rows[0]["CreatedByUserId"].ToString());
                }
                model.CreatedByUserName = ds.Tables[0].Rows[0]["CreatedByUserName"].ToString();

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
            strSql.Append(" FROM jmp_channel_amount_config ");
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
            strSql.Append(" FROM jmp_channel_amount_config ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取数据列
        /// </summary>
        public DataSet GetModelChannelPoolId(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ChannelPoolId, WhichHour, Amount, CreatedOn, CreatedByUserId, CreatedByUserName");
            strSql.Append("  from jmp_channel_amount_config ");
            strSql.Append(" where ChannelPoolId=" + Id);

            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 设置通道池配置数量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddAmount(string[] list)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();

            for (int i = 0; i < list.Length; i++)
            {
                CommandInfo cm = new CommandInfo();
                cm.CommandText = list[i].ToString();
                cmdList.Add(cm);
            }
            int num = DbHelperSQL.ExecuteSqlTran(cmdList);
            return num;
        }

    }
}

