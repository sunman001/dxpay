using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///缺失用户报表统计
    ///</summary>
    public partial class jmp_defect
    {

        public bool Exists(int d_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_defect");
            strSql.Append(" where ");
            strSql.Append(" d_id = @d_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@d_id", SqlDbType.Int,4)
			};
            parameters[0].Value = d_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_defect model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_defect(");
            strSql.Append("d_type,d_aapid,d_losscount,d_lossproportion,d_usercount,d_datatype,d_time");
            strSql.Append(") values (");
            strSql.Append("@d_type,@d_aapid,@d_losscount,@d_lossproportion,@d_usercount,@d_datatype,@d_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@d_type", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_aapid", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_losscount", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_lossproportion", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@d_usercount", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_datatype", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.d_type;
            parameters[1].Value = model.d_aapid;
            parameters[2].Value = model.d_losscount;
            parameters[3].Value = model.d_lossproportion;
            parameters[4].Value = model.d_usercount;
            parameters[5].Value = model.d_datatype;
            parameters[6].Value = model.d_time;

            object obj = DbHelperSQLTotal.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(JMP.MDL.jmp_defect model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_defect set ");

            strSql.Append(" d_type = @d_type , ");
            strSql.Append(" d_aapid = @d_aapid , ");
            strSql.Append(" d_losscount = @d_losscount , ");
            strSql.Append(" d_lossproportion = @d_lossproportion , ");
            strSql.Append(" d_usercount = @d_usercount , ");
            strSql.Append(" d_datatype = @d_datatype , ");
            strSql.Append(" d_time = @d_time  ");
            strSql.Append(" where d_id=@d_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@d_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_type", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_aapid", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_losscount", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_lossproportion", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@d_usercount", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_datatype", SqlDbType.Int,4) ,            
                        new SqlParameter("@d_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.d_id;
            parameters[1].Value = model.d_type;
            parameters[2].Value = model.d_aapid;
            parameters[3].Value = model.d_losscount;
            parameters[4].Value = model.d_lossproportion;
            parameters[5].Value = model.d_usercount;
            parameters[6].Value = model.d_datatype;
            parameters[7].Value = model.d_time;
            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int d_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_defect ");
            strSql.Append(" where d_id=@d_id");
            SqlParameter[] parameters = {
					new SqlParameter("@d_id", SqlDbType.Int,4)
			};
            parameters[0].Value = d_id;


            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string d_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_defect ");
            strSql.Append(" where ID in (" + d_idlist + ")  ");
            int rows = DbHelperSQLTotal.ExecuteSql(strSql.ToString());
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
        public JMP.MDL.jmp_defect GetModel(int d_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select d_id, d_type, d_aapid, d_losscount, d_lossproportion, d_usercount, d_datatype, d_time  ");
            strSql.Append("  from jmp_defect ");
            strSql.Append(" where d_id=@d_id");
            SqlParameter[] parameters = {
					new SqlParameter("@d_id", SqlDbType.Int,4)
			};
            parameters[0].Value = d_id;


            JMP.MDL.jmp_defect model = new JMP.MDL.jmp_defect();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["d_id"].ToString() != "")
                {
                    model.d_id = int.Parse(ds.Tables[0].Rows[0]["d_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_type"].ToString() != "")
                {
                    model.d_type = int.Parse(ds.Tables[0].Rows[0]["d_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_aapid"].ToString() != "")
                {
                    model.d_aapid = int.Parse(ds.Tables[0].Rows[0]["d_aapid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_losscount"].ToString() != "")
                {
                    model.d_losscount = int.Parse(ds.Tables[0].Rows[0]["d_losscount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_lossproportion"].ToString() != "")
                {
                    model.d_lossproportion = decimal.Parse(ds.Tables[0].Rows[0]["d_lossproportion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_usercount"].ToString() != "")
                {
                    model.d_usercount = int.Parse(ds.Tables[0].Rows[0]["d_usercount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_datatype"].ToString() != "")
                {
                    model.d_datatype = int.Parse(ds.Tables[0].Rows[0]["d_datatype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d_time"].ToString() != "")
                {
                    model.d_time = DateTime.Parse(ds.Tables[0].Rows[0]["d_time"].ToString());
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
            strSql.Append(" FROM jmp_defect ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQLTotal.Query(strSql.ToString());
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
            strSql.Append(" FROM jmp_defect ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取汇总信息
        /// </summary>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结算时间</param>
        /// <param name="searchType">查询条件</param>
        /// <param name="searchname">查询内容</param>
        /// <returns></returns>
        public DataTable selectDataTable(string stime, string etime, int searchType, string searchname, int stype, int sedatatype)
        {
            string strSql = string.Format("  select SUM(d_losscount) as d_losscount,SUM(d_lossproportion) as d_lossproportion,SUM(d_usercount) as d_usercount, a.d_time,d_type from jmp_defect a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.d_aapid   left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id  where 1=1 ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.d_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.d_time,120)<='" + etime + "' ";
            }
            if (sedatatype >= 0 && sedatatype <= 2)
            {
                strSql += "  and a.d_datatype='" + sedatatype + "' ";
            }
            if (stype == 1)
            {
                strSql += "  and a.d_type='" + stype + "' ";
            }
            else if (stype == 0)
            {
                strSql += "  and a.d_type='" + stype + "' ";
            }
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        strSql += " and b.a_name ='" + searchname + "' ";
                        break;
                    case 2:
                        strSql += " and c.u_email='" + searchname + "' ";
                        break;
                }
            }
            strSql += "    group by a.d_time,d_type  order by a.d_time  ";
            return DbHelperSQLTotal.Query(strSql).Tables[0];
        }

    }
}

