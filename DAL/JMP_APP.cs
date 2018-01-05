using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //应用表
    public partial class jmp_app
    {

        public bool Exists(int a_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_app");
            strSql.Append(" where ");
            strSql.Append(" a_id = @a_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据生成的key查询是否存在重复记录
        /// </summary>
        /// <param name="a_key">key值</param>
        /// <returns></returns>
        public bool Existss(string a_key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JMP_APP");
            strSql.Append(" where ");
            strSql.Append(" a_key = @a_key  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_key", a_key)
            };
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_app model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_app(");
            strSql.Append("a_auditstate,a_secretkey,a_time,a_showurl,a_auditor,a_rid,a_appurl,a_appsynopsis,a_user_id,a_name,a_platform_id,a_paymode_id,a_apptype_id,a_notifyurl,a_key,a_state");
            strSql.Append(") values (");
            strSql.Append("@a_auditstate,@a_secretkey,@a_time,@a_showurl,@a_auditor,@a_rid,@a_appurl,@a_appsynopsis,@a_user_id,@a_name,@a_platform_id,@a_paymode_id,@a_apptype_id,@a_notifyurl,@a_key,@a_state");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@a_auditstate", SqlDbType.Int,4) ,
                        new SqlParameter("@a_secretkey", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_time", SqlDbType.DateTime) ,
                        new SqlParameter("@a_showurl", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_auditor", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@a_rid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_appurl", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@a_appsynopsis", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_user_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_platform_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_paymode_id", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_apptype_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_notifyurl", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_state", SqlDbType.Int,4)

            };
            parameters[0].Value = model.a_auditstate;
            parameters[1].Value = model.a_secretkey;
            parameters[2].Value = model.a_time;
            parameters[3].Value = model.a_showurl;
            parameters[4].Value = model.a_auditor;
            parameters[5].Value = model.a_rid;
            parameters[6].Value = model.a_appurl;
            parameters[7].Value = model.a_appsynopsis;
            parameters[8].Value = model.a_user_id;
            parameters[9].Value = model.a_name;
            parameters[10].Value = model.a_platform_id;
            parameters[11].Value = model.a_paymode_id;
            parameters[12].Value = model.a_apptype_id;
            parameters[13].Value = model.a_notifyurl;
            parameters[14].Value = model.a_key;
            parameters[15].Value = model.a_state;

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
        public bool Update(JMP.MDL.jmp_app model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_app set ");
            strSql.Append(" a_auditstate = @a_auditstate , ");
            strSql.Append(" a_secretkey = @a_secretkey , ");
            strSql.Append(" a_time = @a_time , ");
            strSql.Append(" a_showurl = @a_showurl , ");
            strSql.Append(" a_auditor = @a_auditor , ");
            strSql.Append(" a_rid = @a_rid , ");
            strSql.Append(" a_appurl = @a_appurl , ");
            strSql.Append(" a_appsynopsis = @a_appsynopsis , ");
            strSql.Append(" a_user_id = @a_user_id , ");
            strSql.Append(" a_name = @a_name , ");
            strSql.Append(" a_platform_id = @a_platform_id , ");
            strSql.Append(" a_paymode_id = @a_paymode_id , ");
            strSql.Append(" a_apptype_id = @a_apptype_id , ");
            strSql.Append(" a_notifyurl = @a_notifyurl , ");
            strSql.Append(" a_key = @a_key , ");
            strSql.Append(" a_state = @a_state  ");
            strSql.Append(" where a_id=@a_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@a_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_auditstate", SqlDbType.Int,4) ,
                        new SqlParameter("@a_secretkey", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_time", SqlDbType.DateTime) ,
                        new SqlParameter("@a_showurl", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_auditor", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@a_rid", SqlDbType.Int,4) ,
                        new SqlParameter("@a_appurl", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@a_appsynopsis", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_user_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_platform_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_paymode_id", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_apptype_id", SqlDbType.Int,4) ,
                        new SqlParameter("@a_notifyurl", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_key", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@a_state", SqlDbType.Int,4)

            };
            parameters[0].Value = model.a_id;
            parameters[1].Value = model.a_auditstate;
            parameters[2].Value = model.a_secretkey;
            parameters[3].Value = model.a_time;
            parameters[4].Value = model.a_showurl;
            parameters[5].Value = model.a_auditor;
            parameters[6].Value = model.a_rid;
            parameters[7].Value = model.a_appurl;
            parameters[8].Value = model.a_appsynopsis;
            parameters[9].Value = model.a_user_id;
            parameters[10].Value = model.a_name;
            parameters[11].Value = model.a_platform_id;
            parameters[12].Value = model.a_paymode_id;
            parameters[13].Value = model.a_apptype_id;
            parameters[14].Value = model.a_notifyurl;
            parameters[15].Value = model.a_key;
            parameters[16].Value = model.a_state;
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
        public bool Delete(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_app ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;


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
        public bool DeleteList(string a_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_app ");
            strSql.Append(" where ID in (" + a_idlist + ")  ");
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
        public JMP.MDL.jmp_app GetModel(int a_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a_id, a_auditstate, a_secretkey, a_time, a_showurl, a_auditor, a_rid, a_appurl, a_appsynopsis, a_user_id, a_name, a_platform_id, a_paymode_id, a_apptype_id, a_notifyurl, a_key, a_state  ");
            strSql.Append("  from jmp_app ");
            strSql.Append(" where a_id=@a_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@a_id", SqlDbType.Int,4)
            };
            parameters[0].Value = a_id;

            JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["a_id"].ToString() != "")
                {
                    model.a_id = int.Parse(ds.Tables[0].Rows[0]["a_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["a_auditstate"].ToString() != "")
                {
                    model.a_auditstate = int.Parse(ds.Tables[0].Rows[0]["a_auditstate"].ToString());
                }
                model.a_secretkey = ds.Tables[0].Rows[0]["a_secretkey"].ToString();
                if (ds.Tables[0].Rows[0]["a_time"].ToString() != "")
                {
                    model.a_time = DateTime.Parse(ds.Tables[0].Rows[0]["a_time"].ToString());
                }
                model.a_showurl = ds.Tables[0].Rows[0]["a_showurl"].ToString();
                model.a_auditor = ds.Tables[0].Rows[0]["a_auditor"].ToString();
                if (ds.Tables[0].Rows[0]["a_rid"].ToString() != "")
                {
                    model.a_rid = int.Parse(ds.Tables[0].Rows[0]["a_rid"].ToString());
                }
                model.a_appurl = ds.Tables[0].Rows[0]["a_appurl"].ToString();
                model.a_appsynopsis = ds.Tables[0].Rows[0]["a_appsynopsis"].ToString();
                if (ds.Tables[0].Rows[0]["a_user_id"].ToString() != "")
                {
                    model.a_user_id = int.Parse(ds.Tables[0].Rows[0]["a_user_id"].ToString());
                }
                model.a_name = ds.Tables[0].Rows[0]["a_name"].ToString();
                if (ds.Tables[0].Rows[0]["a_platform_id"].ToString() != "")
                {
                    model.a_platform_id = int.Parse(ds.Tables[0].Rows[0]["a_platform_id"].ToString());
                }
                model.a_paymode_id = ds.Tables[0].Rows[0]["a_paymode_id"].ToString();
                if (ds.Tables[0].Rows[0]["a_apptype_id"].ToString() != "")
                {
                    model.a_apptype_id = int.Parse(ds.Tables[0].Rows[0]["a_apptype_id"].ToString());
                }
                model.a_notifyurl = ds.Tables[0].Rows[0]["a_notifyurl"].ToString();
                model.a_key = ds.Tables[0].Rows[0]["a_key"].ToString();
                if (ds.Tables[0].Rows[0]["a_state"].ToString() != "")
                {
                    model.a_state = int.Parse(ds.Tables[0].Rows[0]["a_state"].ToString());
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
            strSql.Append("select a_id, a_auditstate, a_secretkey, a_time, a_user_id, a_name, a_platform_id, a_paymode_id, a_apptype_id, a_notifyurl, a_key, a_state,a_showurl,a_auditor,a_rid ");
            strSql.Append(" FROM jmp_app ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable selectsql(string sql)
        {
            return DbHelperSQL.Query(sql.ToString()).Tables[0];
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
            strSql.Append(" FROM jmp_app ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        DataTable dt = new DataTable();
        /// <summary>
        /// 查询应用信息
        /// </summary>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectList( int paytype,  int r_id,int platformid, int auditstate, string sea_name, int type, int SelectState, int appType, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            //string sql = string.Format("select a.a_id, a.a_auditstate, a.a_secretkey, a.a_time, a.a_user_id, a.a_name, a.a_platform_id,a.a_paymode_id, a.a_apptype_id, a.a_notifyurl, a.a_showurl, a.a_auditor, a.a_key, a.a_state, b.u_email, b.u_realname, typeapp.t_name from JMP_APP a left join JMP_USER b on a.a_user_id = b.u_id inner join jmp_apptype apptype on apptype.t_id = a.a_apptype_id inner join jmp_apptype typeapp on typeapp.t_id = apptype.t_topid where 1=1");
            string sql = string.Format(" select a.a_id, a.a_auditstate, a.a_secretkey, a.a_time, a.a_user_id, a.a_name, a.a_platform_id,a.a_paymode_id, a.a_apptype_id, a.a_notifyurl, a.a_showurl, a.a_auditor, a.a_key, a.a_state, b.u_email, b.u_realname, typeapp.t_name,d.r_id ,d.r_name from JMP_APP a  left join JMP_USER b on a.a_user_id = b.u_id inner join jmp_apptype apptype on apptype.t_id = a.a_apptype_id inner join jmp_apptype typeapp on typeapp.t_id = apptype.t_topid left join jmp_risklevelallocation c on c.r_id = a.a_rid left join jmp_risklevel d on d.r_id = c.r_risklevel  where 1 = 1 ");
            string Order = " Order by a_id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and a.a_id =' " + sea_name + "'";
                        break;
                    case 2:
                        sql += " and a.a_name like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and b.u_realname like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.a_key =  '" + sea_name + "' ";
                        break;
                }

            }
            if(paytype>0)
            {
                sql += "  and  a.a_paymode_id like '%"+paytype+"%' ";
            }
            if (auditstate > -1)
            {
                sql += " and a.a_auditstate='" + auditstate + "' ";
            }
            if (SelectState > -1)
            {
                sql += " and a.a_state='" + SelectState + "' ";
            }
            if (platformid > 0)
            {
                sql += " and a.a_platform_id=" + platformid + "  ";
            }
            if(r_id>0)
            {
                sql += " and d.r_id="+r_id+"";
            }
            if (searchDesc == 1)
            {
                Order = " order by a_id  ";
            }
            else
            {
                Order = " order by a_id desc ";
            }
            if (appType > 0)
            {
                sql += " and typeapp.t_id='" + appType + "'";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_app>(ds.Tables[0]);
        }
        /// <summary>
        /// 查询应用信息根据商务信息过滤
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectListById(int id, int auditstate, string sea_name, int type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.a_id, a.a_auditstate, a.a_secretkey, a.a_time, a.a_user_id, a.a_name, a.a_platform_id, a.a_paymode_id, a.a_apptype_id,a.a_auditor ,a.a_notifyurl,a.a_showurl, a.a_key, a.a_state,b.u_email,b.u_realname,(select count(1) from jmp_goods where g_app_id=a.a_id )as goddscount from JMP_APP a  left join JMP_USER b on  a.a_user_id=b.u_id  where b.u_merchant_id='" + id + "' ");
            string Order = " Order by a_id desc";
            if (type > 0)
            {
                switch (type)
                {
                    case 1:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and a.a_id like '%" + sea_name + "%' ";
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and a.a_name like '%" + sea_name + "%' ";
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += " and b.u_realname like  '%" + sea_name + "%' ";
                        }
                        break;
                }

            }
            if (auditstate > -1)
            {
                sql += " and a.a_auditstate='" + auditstate + "' ";
            }
            if (SelectState > -1)
            {
                sql += " and a.a_state='" + SelectState + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by a_id  ";
            }
            else
            {
                Order = " order by a_id desc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_app>(ds.Tables[0]);
        }
        /// <summary>
        /// 根据用户查询所属应用
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectUserList(string userid, string searchname, int terrace, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.a_id, a.a_auditstate, a.a_secretkey, a.a_time, a.a_user_id, a.a_name, a.a_platform_id, a.a_paymode_id, a.a_apptype_id,a.a_auditor, a.a_notifyurl,a.a_showurl, a.a_key, a.a_state,b.u_email,b.u_realname,(select t_name from jmp_apptype where t_id=(select t_topid from jmp_apptype where t_id=a.a_apptype_id )) as t_name,c.p_name from JMP_APP a  left join JMP_USER b on  a.a_user_id=b.u_id  left join jmp_platform c on a.a_platform_id=c.p_id where 1=1 and a_state>-1   and a.a_user_id='" + userid + "'   ");
            if (searchname != "")
            {
                sql += " and a.a_name='" + searchname + "'";
            }
            if (terrace > 0)
            {
                sql += " and a.a_platform_id='" + terrace + "'";
            }
            string order = " order by a_id desc ";
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_app>(ds.Tables[0]);
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
            strSql.Append(" update JMP_APP set a_state=" + state + "  ");
            strSql.Append(" where a_id in (" + u_idlist + ")  ");
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

        //UpdateAppAuditState
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateAppAuditState(string u_idlist, int auditState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update JMP_APP set a_auditstate=" + auditState + "  ");
            strSql.Append(" where a_id in (" + u_idlist + ")  ");
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
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_app SelectId(int a_id)
        {
            string sql = string.Format(" select a.*,b.u_email,b.u_id from jmp_app a left join jmp_user b on b.u_id=a.a_user_id  where 1=1  and a.a_id=@a_id ");
            SqlParameter par = new SqlParameter("@a_id", a_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_app>(dt);
        }


        /// <summary>
        /// 接口根据传入应用key查询应用信息和商品信息
        /// </summary>
        /// <param name="key">应用key</param>
        /// <returns></returns>
        public DataSet GetListjK(string key)
        {
            string strSql = string.Format(" select a.*,b.g_price,b.g_id,b.g_name from jmp_app a,jmp_goods b where a.a_id=b.g_app_id and b.g_state='1' and a.a_auditstate='1' and a.a_state='1' and a.a_key=@key ");
            SqlParameter par = new SqlParameter("@key", key);
            return DbHelperSQL.Query(strSql, par);
        }
        /// <summary>
        /// 根据应用id查询正常能使用的应用（非冻结和非未审核的）
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_app SelectAppIdStat(int a_id)
        {
            string sql = string.Format(" select a.* from jmp_app a where  a.a_auditstate='1' and a.a_state='1' and a.a_id=@a_id ");
            SqlParameter par = new SqlParameter("@a_id", a_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_app>(dt);
        }
        /// <summary>
        /// 根据应用id查询所属平台信息
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_app SelectAppId(int a_id)
        {
            string sql = string.Format(" select a.*,b.p_name from  jmp_app a left join  jmp_platform b on a.a_platform_id=b.p_id where 1=1 and a_id=@a_id ");
            SqlParameter par = new SqlParameter("@a_id", a_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_app>(dt);
        }
        /// <summary>
        /// 应用弹窗
        /// </summary>
        /// <param name="platformid">关联平台（1：安卓，2：苹果，3：H5）</param>
        /// <param name="orders">排序（0：降序，1：升序）</param>
        /// <param name="types">查询条件（1：应用编号，2：应用名称，3：用户名称）</param>
        /// <param name="typesname">查询内容</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectListTc(int platformid, int orders, int types, string typesname, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.a_id,a.a_name,b.u_realname,b.u_id,a.a_user_id,a.a_auditstate,a.a_state,a.a_platform_id from jmp_app a left join jmp_user b on a.a_user_id=b.u_id where a.a_state=1 and a.a_auditstate=1 and b.u_auditstate=1 ");
            if (types > 0 && !string.IsNullOrEmpty(typesname))
            {
                switch (types)
                {
                    case 1:
                        sql += " and a.a_id=" + typesname;//应用编号
                        break;
                    case 2:
                        sql += " and a.a_name like '%" + typesname + "%' ";//应用名称
                        break;
                    case 3:
                        sql += " and b.u_realname like '%" + typesname + "%' ";//用户名称
                        break;
                }
            }
            if (platformid > 0)
            {
                sql += " and a.a_platform_id=" + platformid;
            }
            string order = orders == 0 ? " order by a_id desc " : " order by a_id ";
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_app>(ds.Tables[0]);
        }
        /// <summary>
        /// 应用弹窗用于支付通道
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="order">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app> SelectTClist(string sql, string order, int pageIndexs, int PageSize, out int pageCount)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<JMP.MDL.jmp_app>(ds.Tables[0]);
        }


        /// <summary>
        /// 审核应用
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <param name="start">审核状态</param>
        /// <param name="rid">风控等级</param>
        /// <param name="name">审核人</param>
        /// <returns></returns>
        public bool Update_auditstate(int id, int start, int rid, string name)
        {
            string sql = string.Format("update jmp_app set a_auditstate=" + start + ",a_rid=" + rid + ",a_auditor='" + name + "' where a_id=" + id + "");

            int rows = DbHelperSQL.ExecuteSql(sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

