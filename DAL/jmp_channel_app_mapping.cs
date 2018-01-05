using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //[通道池]通道池-应用映射表
    public partial class jmp_channel_app_mapping
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_channel_app_mapping");
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
        public int Add(JMP.MDL.jmp_channel_app_mapping model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_channel_app_mapping(");
            strSql.Append("ChannelId,AppId,CreatedOn,CreatedByUerId,CreatedByUserName");
            strSql.Append(") values (");
            strSql.Append("@ChannelId,@AppId,@CreatedOn,@CreatedByUerId,@CreatedByUserName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUerId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUserName", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.ChannelId;
            parameters[1].Value = model.AppId;
            parameters[2].Value = model.CreatedOn;
            parameters[3].Value = model.CreatedByUerId;
            parameters[4].Value = model.CreatedByUserName;

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
        public bool Update(JMP.MDL.jmp_channel_app_mapping model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_channel_app_mapping set ");

            strSql.Append(" ChannelId = @ChannelId , ");
            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedByUerId = @CreatedByUerId , ");
            strSql.Append(" CreatedByUserName = @CreatedByUserName  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@ChannelId", SqlDbType.Int,4) ,
                        new SqlParameter("@AppId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUerId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUserName", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.ChannelId;
            parameters[2].Value = model.AppId;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedByUerId;
            parameters[5].Value = model.CreatedByUserName;
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
            strSql.Append("delete from jmp_channel_app_mapping ");
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
            strSql.Append("delete from jmp_channel_app_mapping ");
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
        public JMP.MDL.jmp_channel_app_mapping GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ChannelId, AppId, CreatedOn, CreatedByUerId, CreatedByUserName  ");
            strSql.Append("  from jmp_channel_app_mapping ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.jmp_channel_app_mapping model = new JMP.MDL.jmp_channel_app_mapping();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChannelId"].ToString() != "")
                {
                    model.ChannelId = int.Parse(ds.Tables[0].Rows[0]["ChannelId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppId"].ToString() != "")
                {
                    model.AppId = int.Parse(ds.Tables[0].Rows[0]["AppId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedByUerId"].ToString() != "")
                {
                    model.CreatedByUerId = int.Parse(ds.Tables[0].Rows[0]["CreatedByUerId"].ToString());
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
            strSql.Append(" FROM jmp_channel_app_mapping ");
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
            strSql.Append(" FROM jmp_channel_app_mapping ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        public DataSet GetModelChannelId(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ChannelId, AppId, CreatedOn, CreatedByUerId, CreatedByUserName  ");
            strSql.Append("  from jmp_channel_app_mapping ");
            strSql.Append(" where ChannelId=" + Id);

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 执行SQL（实现事务）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int AddAppMapping(string[] list)
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

