using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //jmp_app_monitor_time_range
    public class jmp_app_monitor_time_range
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_app_monitor_time_range");
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
        public int Add(JMP.MDL.jmp_app_monitor_time_range model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_app_monitor_time_range(");
            strSql.Append("AppMonitorId,WhichHour,Minutes,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy");
            strSql.Append(") values (");
            strSql.Append("@AppMonitorId,@WhichHour,@Minutes,@CreatedOn,@CreatedBy,@ModifiedOn,@ModifiedBy");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@AppMonitorId", SqlDbType.Int,4) ,
                        new SqlParameter("@WhichHour", SqlDbType.Int,4) ,
                        new SqlParameter("@Minutes", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedBy", SqlDbType.Int,4) ,
                        new SqlParameter("@ModifiedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifiedBy", SqlDbType.Int,4)

            };

            parameters[0].Value = model.AppMonitorId;
            parameters[1].Value = model.WhichHour;
            parameters[2].Value = model.Minutes;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedBy;
            parameters[5].Value = model.ModifiedOn;
            parameters[6].Value = model.ModifiedBy;

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
        public bool Update(JMP.MDL.jmp_app_monitor_time_range model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_app_monitor_time_range set ");

            strSql.Append(" AppMonitorId = @AppMonitorId , ");
            strSql.Append(" WhichHour = @WhichHour , ");
            strSql.Append(" Minutes = @Minutes , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedBy = @CreatedBy , ");
            strSql.Append(" ModifiedOn = @ModifiedOn , ");
            strSql.Append(" ModifiedBy = @ModifiedBy  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@AppMonitorId", SqlDbType.Int,4) ,
                        new SqlParameter("@WhichHour", SqlDbType.Int,4) ,
                        new SqlParameter("@Minutes", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedBy", SqlDbType.Int,4) ,
                        new SqlParameter("@ModifiedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifiedBy", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.AppMonitorId;
            parameters[2].Value = model.WhichHour;
            parameters[3].Value = model.Minutes;
            parameters[4].Value = model.CreatedOn;
            parameters[5].Value = model.CreatedBy;
            parameters[6].Value = model.ModifiedOn;
            parameters[7].Value = model.ModifiedBy;
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
            strSql.Append("delete from jmp_app_monitor_time_range ");
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
            strSql.Append("delete from jmp_app_monitor_time_range ");
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
        public JMP.MDL.jmp_app_monitor_time_range GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, AppMonitorId, WhichHour, Minutes, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy  ");
            strSql.Append("  from jmp_app_monitor_time_range ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.jmp_app_monitor_time_range model = new JMP.MDL.jmp_app_monitor_time_range();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AppMonitorId"].ToString() != "")
                {
                    model.AppMonitorId = int.Parse(ds.Tables[0].Rows[0]["AppMonitorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WhichHour"].ToString() != "")
                {
                    model.WhichHour = int.Parse(ds.Tables[0].Rows[0]["WhichHour"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Minutes"].ToString() != "")
                {
                    model.Minutes = int.Parse(ds.Tables[0].Rows[0]["Minutes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedBy"].ToString() != "")
                {
                    model.CreatedBy = int.Parse(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedOn"].ToString() != "")
                {
                    model.ModifiedOn = DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedBy"].ToString() != "")
                {
                    model.ModifiedBy = int.Parse(ds.Tables[0].Rows[0]["ModifiedBy"].ToString());
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
            strSql.Append(" FROM jmp_app_monitor_time_range ");
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
            strSql.Append(" FROM jmp_app_monitor_time_range ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


    }
}

