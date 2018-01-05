using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///应用投诉表
    ///</summary>
    public partial class jmp_complaint
    {

        public bool Exists(int c_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_complaint");
            strSql.Append(" where ");
            strSql.Append(" c_id = @c_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@c_id", SqlDbType.Int,4)
			};
            parameters[0].Value = c_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_complaint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_complaint(");
            strSql.Append("c_tjtimes,c_tjname,c_clname,c_cltimes,c_result,c_reason,c_state,c_appid,c_userid,c_payid,c_tradeno,c_code,c_money,c_times,c_datimes");
            strSql.Append(") values (");
            strSql.Append("@c_tjtimes,@c_tjname,@c_clname,@c_cltimes,@c_result,@c_reason,@c_state,@c_appid,@c_userid,@c_payid,@c_tradeno,@c_code,@c_money,@c_times,@c_datimes");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@c_tjtimes", SqlDbType.DateTime) ,            
                        new SqlParameter("@c_tjname", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@c_clname", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@c_cltimes", SqlDbType.DateTime) ,            
                        new SqlParameter("@c_result", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_reason", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_state", SqlDbType.Char,10) ,            
                        new SqlParameter("@c_appid", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_userid", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_payid", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_tradeno", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_code", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@c_times", SqlDbType.DateTime) ,            
                        new SqlParameter("@c_datimes", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.c_tjtimes;
            parameters[1].Value = model.c_tjname;
            parameters[2].Value = model.c_clname;
            parameters[3].Value = model.c_cltimes;
            parameters[4].Value = model.c_result;
            parameters[5].Value = model.c_reason;
            parameters[6].Value = model.c_state;
            parameters[7].Value = model.c_appid;
            parameters[8].Value = model.c_userid;
            parameters[9].Value = model.c_payid;
            parameters[10].Value = model.c_tradeno;
            parameters[11].Value = model.c_code;
            parameters[12].Value = model.c_money;
            parameters[13].Value = model.c_times;
            parameters[14].Value = model.c_datimes;

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
        public bool Update(JMP.MDL.jmp_complaint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_complaint set ");

            strSql.Append(" c_tjtimes = @c_tjtimes , ");
            strSql.Append(" c_tjname = @c_tjname , ");
            strSql.Append(" c_clname = @c_clname , ");
            strSql.Append(" c_cltimes = @c_cltimes , ");
            strSql.Append(" c_result = @c_result , ");
            strSql.Append(" c_reason = @c_reason , ");
            strSql.Append(" c_state = @c_state , ");
            strSql.Append(" c_appid = @c_appid , ");
            strSql.Append(" c_userid = @c_userid , ");
            strSql.Append(" c_payid = @c_payid , ");
            strSql.Append(" c_tradeno = @c_tradeno , ");
            strSql.Append(" c_code = @c_code , ");
            strSql.Append(" c_money = @c_money , ");
            strSql.Append(" c_times = @c_times , ");
            strSql.Append(" c_datimes = @c_datimes  ");
            strSql.Append(" where c_id=@c_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@c_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_tjtimes", SqlDbType.DateTime) ,            
                        new SqlParameter("@c_tjname", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@c_clname", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@c_cltimes", SqlDbType.DateTime) ,            
                        new SqlParameter("@c_result", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_reason", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_state", SqlDbType.Char,10) ,            
                        new SqlParameter("@c_appid", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_userid", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_payid", SqlDbType.Int,4) ,            
                        new SqlParameter("@c_tradeno", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_code", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@c_money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@c_times", SqlDbType.DateTime) ,            
                        new SqlParameter("@c_datimes", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.c_id;
            parameters[1].Value = model.c_tjtimes;
            parameters[2].Value = model.c_tjname;
            parameters[3].Value = model.c_clname;
            parameters[4].Value = model.c_cltimes;
            parameters[5].Value = model.c_result;
            parameters[6].Value = model.c_reason;
            parameters[7].Value = model.c_state;
            parameters[8].Value = model.c_appid;
            parameters[9].Value = model.c_userid;
            parameters[10].Value = model.c_payid;
            parameters[11].Value = model.c_tradeno;
            parameters[12].Value = model.c_code;
            parameters[13].Value = model.c_money;
            parameters[14].Value = model.c_times;
            parameters[15].Value = model.c_datimes;
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
        public bool Delete(int c_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_complaint ");
            strSql.Append(" where c_id=@c_id");
            SqlParameter[] parameters = {
					new SqlParameter("@c_id", SqlDbType.Int,4)
			};
            parameters[0].Value = c_id;


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
        public bool DeleteList(string c_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_complaint ");
            strSql.Append(" where ID in (" + c_idlist + ")  ");
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
        public JMP.MDL.jmp_complaint GetModel(int c_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c_id, c_tjtimes, c_tjname, c_clname, c_cltimes, c_result, c_reason, c_state, c_appid, c_userid, c_payid, c_tradeno, c_code, c_money, c_times, c_datimes  ");
            strSql.Append("  from jmp_complaint ");
            strSql.Append(" where c_id=@c_id");
            SqlParameter[] parameters = {
					new SqlParameter("@c_id", SqlDbType.Int,4)
			};
            parameters[0].Value = c_id;


            JMP.MDL.jmp_complaint model = new JMP.MDL.jmp_complaint();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["c_id"].ToString() != "")
                {
                    model.c_id = int.Parse(ds.Tables[0].Rows[0]["c_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["c_tjtimes"].ToString() != "")
                {
                    model.c_tjtimes = DateTime.Parse(ds.Tables[0].Rows[0]["c_tjtimes"].ToString());
                }
                model.c_tjname = ds.Tables[0].Rows[0]["c_tjname"].ToString();
                model.c_clname = ds.Tables[0].Rows[0]["c_clname"].ToString();
                if (ds.Tables[0].Rows[0]["c_cltimes"].ToString() != "")
                {
                    model.c_cltimes = DateTime.Parse(ds.Tables[0].Rows[0]["c_cltimes"].ToString());
                }
                model.c_result = ds.Tables[0].Rows[0]["c_result"].ToString();
                model.c_reason = ds.Tables[0].Rows[0]["c_reason"].ToString();
                model.c_state = ds.Tables[0].Rows[0]["c_state"].ToString();
                if (ds.Tables[0].Rows[0]["c_appid"].ToString() != "")
                {
                    model.c_appid = int.Parse(ds.Tables[0].Rows[0]["c_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["c_userid"].ToString() != "")
                {
                    model.c_userid = int.Parse(ds.Tables[0].Rows[0]["c_userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["c_payid"].ToString() != "")
                {
                    model.c_payid = int.Parse(ds.Tables[0].Rows[0]["c_payid"].ToString());
                }
                model.c_tradeno = ds.Tables[0].Rows[0]["c_tradeno"].ToString();
                model.c_code = ds.Tables[0].Rows[0]["c_code"].ToString();
                if (ds.Tables[0].Rows[0]["c_money"].ToString() != "")
                {
                    model.c_money = decimal.Parse(ds.Tables[0].Rows[0]["c_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["c_times"].ToString() != "")
                {
                    model.c_times = DateTime.Parse(ds.Tables[0].Rows[0]["c_times"].ToString());
                }
                if (ds.Tables[0].Rows[0]["c_datimes"].ToString() != "")
                {
                    model.c_datimes = DateTime.Parse(ds.Tables[0].Rows[0]["c_datimes"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="c_id">应用投诉id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.jmp_complaint SelectId(int c_id)
        {
            string sql = string.Format(" select a.c_id,a.c_appid,a.c_userid,a.c_payid,a.c_tradeno,a.c_code,a.c_money,a.c_times,a.c_datimes,a.c_tjtimes,a.c_tjname,a.c_clname,a.c_cltimes,a.c_reason,a.c_result, a.c_state, d.l_corporatename, c.a_name, f.u_realname from jmp_complaint a left join jmp_interface d on a.c_payid = d.l_id left join jmp_app c  on a.c_appid = c.a_id left join jmp_user as f on a.c_userid = f.u_id where a.c_id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_complaint>(dt);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM jmp_complaint ");
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
            strSql.Append(" FROM jmp_complaint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 查询应该投诉管理
        /// </summary>
        /// <param name="userid">用户id（后台默认传0，开发者平台默认传用户id）</param>
        /// <param name="auditstate">审核状态</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_complaint> SelectList(int UserDept, string UserId, string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount,int dept)
        {
            string sql = string.Format("select a.c_id,a.c_appid,a.c_userid,a.c_payid,a.c_tradeno,a.c_code,a.c_money,a.c_times,a.c_datimes,a.c_tjtimes,a.c_tjname,a.c_clname,a.c_cltimes,a.c_reason,a.c_result,a.c_state, d.l_corporatename, c.a_name, f.u_realname from jmp_complaint a left join jmp_interface d on a.c_payid = d.l_id left join jmp_app c  on a.c_appid = c.a_id left join jmp_user as f on a.c_userid = f.u_id where 1=1");
            string Order = " Order by c_id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and c.a_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and f.u_realname like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and d.l_corporatename like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.c_tradeno ='" + sea_name + "' ";
                        break;
                    case 5:
                        sql += " and a.c_tjname =  '" + sea_name + "' ";
                        break;
                    case 6:
                        sql += " and a.c_clname =  '" + sea_name + "' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            {
                sql += " and a.c_tjtimes>='" + stime + " 00:00:00' and a.c_tjtimes<='" + endtime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and a.c_state='" + auditstate + "' ";
            }

           
            if(UserDept==dept)
            {
                sql += " and  f.u_merchant_id="+int.Parse(UserId);
            }
            if (searchDesc == 1)
            {
                Order = " order by c_id  ";
            }
            else
            {
                Order = " order by c_id desc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_complaint>(ds.Tables[0]);
        }


      /// <summary>
      /// 应用投诉管理
      /// </summary>
      /// <param name="uids">选择投诉的ID</param>
      /// <param name="remark">处理结果</param>
      /// <returns></returns>
        public bool ComplaintLC(string uids, string remark,string r_auditor)
        {
         
            var strSql = string.Format("UPDATE jmp_complaint SET c_result='{1}',c_state='1', c_clname='{2}' WHERE c_id IN ({0})", uids,remark, r_auditor);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }

        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_complaint> DcSelectList(string sql)
        {
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_complaint>(dt);
        }



    }
}

