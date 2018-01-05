using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///退款申请表
    ///</summary>
    public partial class jmp_refund
    {

        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_refund");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_refund model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_refund(");
            strSql.Append("r_money,r_date,r_time,r_static,r_auditor,r_auditortime,r_remark,r_name,r_tel,r_userid,r_appid,r_payid,r_tradeno,r_code,r_price");
            strSql.Append(") values (");
            strSql.Append("@r_money,@r_date,@r_time,@r_static,@r_auditor,@r_auditortime,@r_remark,@r_name,@r_tel,@r_userid,@r_appid,@r_payid,@r_tradeno,@r_code,@r_price");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@r_money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@r_date", SqlDbType.DateTime) ,            
                        new SqlParameter("@r_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@r_static", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_auditor", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@r_auditortime", SqlDbType.DateTime) ,            
                        new SqlParameter("@r_remark", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_name", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@r_tel", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@r_userid", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_appid", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_payid", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_tradeno", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_code", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_price", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.r_money;
            parameters[1].Value = model.r_date;
            parameters[2].Value = model.r_time;
            parameters[3].Value = model.r_static;
            parameters[4].Value = model.r_auditor;
            parameters[5].Value = model.r_auditortime;
            parameters[6].Value = model.r_remark;
            parameters[7].Value = model.r_name;
            parameters[8].Value = model.r_tel;
            parameters[9].Value = model.r_userid;
            parameters[10].Value = model.r_appid;
            parameters[11].Value = model.r_payid;
            parameters[12].Value = model.r_tradeno;
            parameters[13].Value = model.r_code;
            parameters[14].Value = model.r_price;

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
        public bool Update(JMP.MDL.jmp_refund model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_refund set ");

            strSql.Append(" r_money = @r_money , ");
            strSql.Append(" r_date = @r_date , ");
            strSql.Append(" r_time = @r_time , ");
            strSql.Append(" r_static = @r_static , ");
            strSql.Append(" r_auditor = @r_auditor , ");
            strSql.Append(" r_auditortime = @r_auditortime , ");
            strSql.Append(" r_remark = @r_remark , ");
            strSql.Append(" r_name = @r_name , ");
            strSql.Append(" r_tel = @r_tel , ");
            strSql.Append(" r_userid = @r_userid , ");
            strSql.Append(" r_appid = @r_appid , ");
            strSql.Append(" r_payid = @r_payid , ");
            strSql.Append(" r_tradeno = @r_tradeno , ");
            strSql.Append(" r_code = @r_code , ");
            strSql.Append(" r_price = @r_price  ");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@r_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@r_date", SqlDbType.DateTime) ,            
                        new SqlParameter("@r_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@r_static", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_auditor", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@r_auditortime", SqlDbType.DateTime) ,            
                        new SqlParameter("@r_remark", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_name", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@r_tel", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@r_userid", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_appid", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_payid", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_tradeno", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_code", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_price", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_money;
            parameters[2].Value = model.r_date;
            parameters[3].Value = model.r_time;
            parameters[4].Value = model.r_static;
            parameters[5].Value = model.r_auditor;
            parameters[6].Value = model.r_auditortime;
            parameters[7].Value = model.r_remark;
            parameters[8].Value = model.r_name;
            parameters[9].Value = model.r_tel;
            parameters[10].Value = model.r_userid;
            parameters[11].Value = model.r_appid;
            parameters[12].Value = model.r_payid;
            parameters[13].Value = model.r_tradeno;
            parameters[14].Value = model.r_code;
            parameters[15].Value = model.r_price;
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
        public bool Delete(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_refund ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


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
        public bool DeleteList(string r_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_refund ");
            strSql.Append(" where ID in (" + r_idlist + ")  ");
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
        public JMP.MDL.jmp_refund GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id, r_money, r_date, r_time, r_static, r_auditor, r_auditortime, r_remark, r_name, r_tel, r_userid, r_appid, r_payid, r_tradeno, r_code, r_price  ");
            strSql.Append("  from jmp_refund ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


            JMP.MDL.jmp_refund model = new JMP.MDL.jmp_refund();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_money"].ToString() != "")
                {
                    model.r_money = decimal.Parse(ds.Tables[0].Rows[0]["r_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_date"].ToString() != "")
                {
                    model.r_date = DateTime.Parse(ds.Tables[0].Rows[0]["r_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_time"].ToString() != "")
                {
                    model.r_time = DateTime.Parse(ds.Tables[0].Rows[0]["r_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_static"].ToString() != "")
                {
                    model.r_static = int.Parse(ds.Tables[0].Rows[0]["r_static"].ToString());
                }
                model.r_auditor = ds.Tables[0].Rows[0]["r_auditor"].ToString();
                if (ds.Tables[0].Rows[0]["r_auditortime"].ToString() != "")
                {
                    model.r_auditortime = DateTime.Parse(ds.Tables[0].Rows[0]["r_auditortime"].ToString());
                }
                model.r_remark = ds.Tables[0].Rows[0]["r_remark"].ToString();
                model.r_name = ds.Tables[0].Rows[0]["r_name"].ToString();
                model.r_tel = ds.Tables[0].Rows[0]["r_tel"].ToString();
                if (ds.Tables[0].Rows[0]["r_userid"].ToString() != "")
                {
                    model.r_userid = int.Parse(ds.Tables[0].Rows[0]["r_userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_appid"].ToString() != "")
                {
                    model.r_appid = int.Parse(ds.Tables[0].Rows[0]["r_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_payid"].ToString() != "")
                {
                    model.r_payid = int.Parse(ds.Tables[0].Rows[0]["r_payid"].ToString());
                }
                model.r_tradeno = ds.Tables[0].Rows[0]["r_tradeno"].ToString();
                model.r_code = ds.Tables[0].Rows[0]["r_code"].ToString();
                if (ds.Tables[0].Rows[0]["r_price"].ToString() != "")
                {
                    model.r_price = decimal.Parse(ds.Tables[0].Rows[0]["r_price"].ToString());
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
            strSql.Append(" FROM jmp_refund ");
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
            strSql.Append(" FROM jmp_refund ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询退款信息
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
        public List<JMP.MDL.jmp_refund> SelectList( int UserRoleId, string UserId, string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount,int RoleID)
        {
            string sql = string.Format("select a.r_id, a.r_name, a.r_tel,a.r_appid,a.r_payid , a.r_userid, a.r_tradeno, a.r_code, a.r_price, a.r_money, a.r_date, a.r_time, a.r_auditor, a.r_auditortime, a.r_remark, a.r_static, b.u_realname ,c.a_name,d.l_corporatename from jmp_refund as a left join jmp_app as c on a.r_appid = c.a_id left join jmp_user as b on a.r_userid = b.u_id left join jmp_interface as d on a.r_payid = d.l_id  where 1 = 1");
            string Order = " Order by a.r_id desc";
         
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and a.r_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and c.a_name like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and b.u_realname like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.r_tradeno ='" + sea_name + "' ";
                        break;
                    case 5:
                        sql += " and a.r_code =  '" + sea_name + "' ";
                        break;
                    case 6:
                        sql += " and d.l_corporatename =  '" + sea_name + "' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            {
                sql += " and r_time>='" + stime + " 00:00:00' and r_time<='" + endtime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and a.r_static='" + auditstate + "' ";
            }
            if(UserRoleId== RoleID)
            {
                sql += " and b.u_merchant_id= "+int.Parse(UserId);
            }
            if (searchDesc == 1)
            {
                Order = " order by r_id  ";
            }
            else
            {
                Order = " order by r_id desc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_refund>(ds.Tables[0]);
        }

        /// <summary>
        /// 审核退款信息
        /// </summary>
        /// <param name="uids"></param>

        /// <returns></returns>
        public bool AuditorToRefund(string uids, string rstatic, string rauditor, string remark,string r_payid)
        {
            var r_auditortime = DateTime.Now;
            var strSql = string.Format("UPDATE jmp_refund SET r_static={1},r_auditor='{2}',r_auditortime='{4}',r_remark='{3}',r_payid='{5}' WHERE r_id IN ({0})", uids, rstatic, rauditor, remark, r_auditortime,r_payid);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_refund> SelectDc(string sql)
        {
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_refund>(dt);
        }

        /// <summary>
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="a_id">退款id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.jmp_refund SelectId(int r_id)
        {
            string sql = string.Format(" select a.r_id, a.r_name, a.r_tel,a.r_appid, a.r_userid, a.r_tradeno, a.r_code, a.r_price, a.r_money, a.r_date, a.r_time, a.r_auditor, a.r_auditortime, a.r_remark, a.r_static, b.u_realname ,c.a_name from jmp_refund as a  left join jmp_app as c on a.r_appid = c.a_id left join jmp_user as b on a.r_userid = b.u_id  where a.r_id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", r_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_refund>(dt);
        }
    }
}

