using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //管理员用户表
    public partial class jmp_locuser
    {

        public bool Exists(int u_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_locuser");
            strSql.Append(" where ");
            strSql.Append(" u_id = @u_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_locuser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_locuser(");
            strSql.Append("u_role_id,u_loginname,u_pwd,u_realname,u_department,u_position,u_count,u_state,u_mobilenumber,u_emailaddress,u_qq");
            strSql.Append(") values (");
            strSql.Append("@u_role_id,@u_loginname,@u_pwd,@u_realname,@u_department,@u_position,@u_count,@u_state,@u_mobilenumber,@u_emailaddress,@u_qq");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@u_role_id", SqlDbType.Int,4) ,
                        new SqlParameter("@u_loginname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_pwd", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_realname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_department", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_position", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_count", SqlDbType.Int,4) ,
                        new SqlParameter("@u_state", SqlDbType.Int,4) ,
                        new SqlParameter("@u_mobilenumber",SqlDbType.NVarChar,20),
                        new SqlParameter("@u_emailaddress",SqlDbType.NVarChar,120),
                        new SqlParameter("@u_qq",SqlDbType.NVarChar,20)

            };

            parameters[0].Value = model.u_role_id;
            parameters[1].Value = model.u_loginname;
            parameters[2].Value = model.u_pwd;
            parameters[3].Value = model.u_realname;
            parameters[4].Value = model.u_department;
            parameters[5].Value = model.u_position;
            parameters[6].Value = model.u_count;
            parameters[7].Value = model.u_state;
            parameters[8].Value = model.u_mobilenumber;
            parameters[9].Value = model.u_emailaddress;
            parameters[10].Value = model.u_qq;

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
        public bool Update(JMP.MDL.jmp_locuser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_LOCUSER set ");
            strSql.Append("u_role_id=@u_role_id,");
            strSql.Append("u_pwd=@u_pwd,");
            strSql.Append("u_realname=@u_realname,");
            strSql.Append("u_department=@u_department,");
            strSql.Append("u_position=@u_position,");
            strSql.Append("u_count=@u_count,");
            strSql.Append("u_state=@u_state,");
            strSql.Append("u_loginname=@u_loginname,");
            strSql.Append("u_mobilenumber=@u_mobilenumber,");
            strSql.Append("u_emailaddress=@u_emailaddress,");
            strSql.Append("u_qq=@u_qq");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                new SqlParameter("@u_role_id", SqlDbType.Int,4),
                new SqlParameter("@u_pwd", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_realname", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_department", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_position", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_count", SqlDbType.Int,4),
                new SqlParameter("@u_state", SqlDbType.Int,4),
                new SqlParameter("@u_loginname", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_id", SqlDbType.Int,4),
                new SqlParameter("@u_mobilenumber",SqlDbType.NVarChar,20),
                new SqlParameter("@u_emailaddress",SqlDbType.NVarChar,120),
                new SqlParameter("@u_qq",SqlDbType.NVarChar,20)
            };
            parameters[0].Value = model.u_role_id;
            parameters[1].Value = model.u_pwd;
            parameters[2].Value = model.u_realname;
            parameters[3].Value = model.u_department;
            parameters[4].Value = model.u_position;
            parameters[5].Value = model.u_count;
            parameters[6].Value = model.u_state;
            parameters[7].Value = model.u_loginname;
            parameters[8].Value = model.u_id;
            parameters[9].Value = model.u_mobilenumber;
            parameters[10].Value = model.u_emailaddress;
            parameters[11].Value = model.u_qq;

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
        public bool Delete(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_locuser ");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;


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
        public bool DeleteList(string u_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_locuser ");
            strSql.Append(" where ID in (" + u_idlist + ")  ");
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
        public JMP.MDL.jmp_locuser GetModel(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id, u_role_id, u_loginname, u_pwd, u_realname, u_department, u_position, u_count, u_state,u_mobilenumber,u_emailaddress,u_qq  ");
            strSql.Append("  from jmp_locuser ");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;


            JMP.MDL.jmp_locuser model = new JMP.MDL.jmp_locuser();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_role_id"].ToString() != "")
                {
                    model.u_role_id = int.Parse(ds.Tables[0].Rows[0]["u_role_id"].ToString());
                }
                model.u_loginname = ds.Tables[0].Rows[0]["u_loginname"].ToString();
                model.u_pwd = ds.Tables[0].Rows[0]["u_pwd"].ToString();
                model.u_realname = ds.Tables[0].Rows[0]["u_realname"].ToString();
                model.u_department = ds.Tables[0].Rows[0]["u_department"].ToString();
                model.u_position = ds.Tables[0].Rows[0]["u_position"].ToString();
                if (ds.Tables[0].Rows[0]["u_count"].ToString() != "")
                {
                    model.u_count = int.Parse(ds.Tables[0].Rows[0]["u_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                model.u_mobilenumber = ds.Tables[0].Rows[0]["u_mobilenumber"].ToString();
                model.u_emailaddress = ds.Tables[0].Rows[0]["u_emailaddress"].ToString();
                model.u_qq = ds.Tables[0].Rows[0]["u_qq"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_locuser GetModel(string userName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id, u_role_id, u_loginname, u_pwd, u_realname, u_department, u_position, u_count, u_state,u_mobilenumber,u_emailaddress,u_qq  ");
            strSql.Append("  from jmp_locuser ");
            strSql.Append(" where u_loginname=@u_loginname");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_loginname", SqlDbType.NVarChar,-1)
            };
            parameters[0].Value = userName;


            JMP.MDL.jmp_locuser model = new JMP.MDL.jmp_locuser();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_role_id"].ToString() != "")
                {
                    model.u_role_id = int.Parse(ds.Tables[0].Rows[0]["u_role_id"].ToString());
                }
                model.u_loginname = ds.Tables[0].Rows[0]["u_loginname"].ToString();
                model.u_pwd = ds.Tables[0].Rows[0]["u_pwd"].ToString();
                model.u_realname = ds.Tables[0].Rows[0]["u_realname"].ToString();
                model.u_department = ds.Tables[0].Rows[0]["u_department"].ToString();
                model.u_position = ds.Tables[0].Rows[0]["u_position"].ToString();
                if (ds.Tables[0].Rows[0]["u_count"].ToString() != "")
                {
                    model.u_count = int.Parse(ds.Tables[0].Rows[0]["u_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                model.u_mobilenumber = ds.Tables[0].Rows[0]["u_mobilenumber"].ToString();
                model.u_emailaddress = ds.Tables[0].Rows[0]["u_emailaddress"].ToString();
                model.u_qq = ds.Tables[0].Rows[0]["u_qq"].ToString();
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
            strSql.Append(" FROM jmp_locuser ");
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
            strSql.Append(" FROM jmp_locuser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        DataTable dt = new DataTable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_locuser> SelectList(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_locuser>(ds.Tables[0]);
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update JMP_LOCUSER set u_state=" + state + "  ");
            strSql.Append(" where u_id in (" + u_idlist + ")  ");
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
        /// 根据用户名判断是否存在该记录
        /// </summary>
        public bool ExistsName(string u_loginname, string u_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JMP_LOCUSER");
            strSql.Append(" where u_loginname=@u_loginname");
            if (!string.IsNullOrEmpty(u_id))
            {
                strSql.Append(" and u_id<>@u_id");
                SqlParameter[] parameters = {
                    new SqlParameter("@u_loginname", SqlDbType.NVarChar,-1),
                    new SqlParameter("@u_id", SqlDbType.Int,4)
                };

                parameters[0].Value = u_loginname;
                parameters[1].Value = int.Parse(u_id);
                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }
            else
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@u_loginname", SqlDbType.NVarChar,-1)
                };

                parameters[0].Value = u_loginname;
                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }
        }

    }
}

