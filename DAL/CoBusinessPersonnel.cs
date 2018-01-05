using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    //商务信息表
    public class CoBusinessPersonnel
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoBusinessPersonnel");
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
        public int Add(JMP.MDL.CoBusinessPersonnel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoBusinessPersonnel(");
            strSql.Append("CreatedById,CreatedByName,LoginCount,State,RoleId,LoginName,Password,DisplayName,EmailAddress,MobilePhone,QQ,Website,CreatedOn");
            strSql.Append(") values (");
            strSql.Append("@CreatedById,@CreatedByName,@LoginCount,@State,@RoleId,@LoginName,@Password,@DisplayName,@EmailAddress,@MobilePhone,@QQ,@Website,@CreatedOn");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@LoginCount", SqlDbType.Int,4) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@RoleId", SqlDbType.Int,4) ,
                        new SqlParameter("@LoginName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Password", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@DisplayName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@MobilePhone", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@QQ", SqlDbType.NVarChar,15) ,
                        new SqlParameter("@Website", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.CreatedById;
            parameters[1].Value = model.CreatedByName;
            parameters[2].Value = model.LoginCount;
            parameters[3].Value = model.State;
            parameters[4].Value = model.RoleId;
            parameters[5].Value = model.LoginName;
            parameters[6].Value = model.Password;
            parameters[7].Value = model.DisplayName;
            parameters[8].Value = model.EmailAddress;
            parameters[9].Value = model.MobilePhone;
            parameters[10].Value = model.QQ;
            parameters[11].Value = model.Website;
            parameters[12].Value = model.CreatedOn;

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
        public bool Update(JMP.MDL.CoBusinessPersonnel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoBusinessPersonnel set ");

            strSql.Append(" CreatedById = @CreatedById , ");
            strSql.Append(" CreatedByName = @CreatedByName , ");
            strSql.Append(" LoginCount = @LoginCount , ");
            strSql.Append(" State = @State , ");
            strSql.Append(" RoleId = @RoleId , ");
            strSql.Append(" LoginName = @LoginName , ");
            strSql.Append(" Password = @Password , ");
            strSql.Append(" DisplayName = @DisplayName , ");
            strSql.Append(" EmailAddress = @EmailAddress , ");
            strSql.Append(" MobilePhone = @MobilePhone , ");
            strSql.Append(" QQ = @QQ , ");
            strSql.Append(" Website = @Website , ");
            strSql.Append(" CreatedOn = @CreatedOn,  ");
            strSql.Append(" LogintTime=@LogintTime");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@LoginCount", SqlDbType.Int,4) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@RoleId", SqlDbType.Int,4) ,
                        new SqlParameter("@LoginName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Password", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@DisplayName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@MobilePhone", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@QQ", SqlDbType.NVarChar,15) ,
                        new SqlParameter("@Website", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime),
                        new SqlParameter("@LogintTime",SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.CreatedById;
            parameters[2].Value = model.CreatedByName;
            parameters[3].Value = model.LoginCount;
            parameters[4].Value = model.State;
            parameters[5].Value = model.RoleId;
            parameters[6].Value = model.LoginName;
            parameters[7].Value = model.Password;
            parameters[8].Value = model.DisplayName;
            parameters[9].Value = model.EmailAddress;
            parameters[10].Value = model.MobilePhone;
            parameters[11].Value = model.QQ;
            parameters[12].Value = model.Website;
            parameters[13].Value = model.CreatedOn;
            parameters[14].Value = model.LogintTime;
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
            strSql.Append("delete from CoBusinessPersonnel ");
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
            strSql.Append("delete from CoBusinessPersonnel ");
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
        /// 批量更新商务状态
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateState(string coid, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoBusinessPersonnel set [State]=" + state + " where Id in(" + coid + ")");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoBusinessPersonnel GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CreatedById, CreatedByName, LoginCount, State, RoleId, LoginName, Password, DisplayName, EmailAddress, MobilePhone, QQ, Website, CreatedOn,LogintTime  ");
            strSql.Append("  from CoBusinessPersonnel ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoBusinessPersonnel model = new JMP.MDL.CoBusinessPersonnel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                if (ds.Tables[0].Rows[0]["LoginCount"].ToString() != "")
                {
                    model.LoginCount = int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }
                model.LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                model.EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.Website = ds.Tables[0].Rows[0]["Website"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }

                if (ds.Tables[0].Rows[0]["LogintTime"].ToString() != "")
                {
                    model.LogintTime = DateTime.Parse(ds.Tables[0].Rows[0]["LogintTime"].ToString());
                }


                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 是否存在该登录账号
        /// </summary>
        /// <param name="lname">登录账号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsLogName(string lname, string uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from CoBusinessPersonnel where LoginName='{0}'", lname);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and Id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CoBusinessPersonnel ");
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
            strSql.Append(" FROM CoBusinessPersonnel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询所有商务信息
        public List<JMP.MDL.CoBusinessPersonnel> SelectList(string s_type, string s_keys, string s_state, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select Id, CreatedById, CreatedByName, LoginCount, [State], RoleId, LoginName, Password, DisplayName, EmailAddress, MobilePhone, QQ, Website, CreatedOn from CoBusinessPersonnel where 1 = 1");

            if (!string.IsNullOrEmpty(s_keys))
            {
                if (s_type == "0")
                {
                    sql += " and EmailAddress='" + s_keys + "'";
                }
                else
                {
                    sql += " and DisplayName like '%" + s_keys + "%'";
                }

            }
            if (!string.IsNullOrEmpty(s_state))
            {
                sql += " and [State]=" + s_state;
            }


            string Order = " order by Id desc ";

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
            return DbHelperSQL.ToList<JMP.MDL.CoBusinessPersonnel>(ds.Tables[0]);
        }


        /// <summary>
        /// 随机获取一个当天登录过的商务姓名
        /// </summary>
        /// <returns></returns>
        public JMP.MDL.CoBusinessPersonnel getModelLoginTime()
        {
            string sql = string.Format("select top 1 DisplayName from [dbo].[CoBusinessPersonnel] where CONVERT(varchar(10),LogintTime,110)=CONVERT(varchar(10),GETDATE(),110) order by newid()");
            JMP.MDL.CoBusinessPersonnel model = new JMP.MDL.CoBusinessPersonnel();
            DataSet ds = DbHelperSQL.Query(sql);
            return DbHelperSQL.ToModel<JMP.MDL.CoBusinessPersonnel>(ds.Tables[0]);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoBusinessPersonnel GetModel(string userName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("  from CoBusinessPersonnel ");
            strSql.Append(" where LoginName=@LoginName");
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginName", SqlDbType.NVarChar,-1)
            };
            parameters[0].Value = userName;


            JMP.MDL.CoBusinessPersonnel model = new JMP.MDL.CoBusinessPersonnel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                if (ds.Tables[0].Rows[0]["LoginCount"].ToString() != "")
                {
                    model.LoginCount = int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }
                model.LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                model.EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.Website = ds.Tables[0].Rows[0]["Website"].ToString();
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

    }
}
