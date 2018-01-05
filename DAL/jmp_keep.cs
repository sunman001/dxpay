using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    ///<summary>
    ///留存用户统计
    ///</summary>
    public partial class jmp_keep
    {

        public bool Exists(int k_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_keep");
            strSql.Append(" where ");
            strSql.Append(" k_id = @k_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@k_id", SqlDbType.Int,4)
			};
            parameters[0].Value = k_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_keep model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_keep(");
            strSql.Append("k_day6,k_day7,k_day14,k_day30,k_time,k_app_id,k_type,k_usercount,k_day1,k_day2,k_day3,k_day4,k_day5");
            strSql.Append(") values (");
            strSql.Append("@k_day6,@k_day7,@k_day14,@k_day30,@k_time,@k_app_id,@k_type,@k_usercount,@k_day1,@k_day2,@k_day3,@k_day4,@k_day5");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@k_day6", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day7", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day14", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day30", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@k_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_type", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_usercount", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_day1", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day2", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day3", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day4", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day5", SqlDbType.Decimal,5)             
              
            };

            parameters[0].Value = model.k_day6;
            parameters[1].Value = model.k_day7;
            parameters[2].Value = model.k_day14;
            parameters[3].Value = model.k_day30;
            parameters[4].Value = model.k_time;
            parameters[5].Value = model.k_app_id;
            parameters[6].Value = model.k_type;
            parameters[7].Value = model.k_usercount;
            parameters[8].Value = model.k_day1;
            parameters[9].Value = model.k_day2;
            parameters[10].Value = model.k_day3;
            parameters[11].Value = model.k_day4;
            parameters[12].Value = model.k_day5;

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
        public bool Update(JMP.MDL.jmp_keep model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_keep set ");

            strSql.Append(" k_day6 = @k_day6 , ");
            strSql.Append(" k_day7 = @k_day7 , ");
            strSql.Append(" k_day14 = @k_day14 , ");
            strSql.Append(" k_day30 = @k_day30 , ");
            strSql.Append(" k_time = @k_time , ");
            strSql.Append(" k_app_id = @k_app_id , ");
            strSql.Append(" k_type = @k_type , ");
            strSql.Append(" k_usercount = @k_usercount , ");
            strSql.Append(" k_day1 = @k_day1 , ");
            strSql.Append(" k_day2 = @k_day2 , ");
            strSql.Append(" k_day3 = @k_day3 , ");
            strSql.Append(" k_day4 = @k_day4 , ");
            strSql.Append(" k_day5 = @k_day5  ");
            strSql.Append(" where k_id=@k_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@k_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_day6", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day7", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day14", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day30", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_time", SqlDbType.DateTime) ,            
                        new SqlParameter("@k_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_type", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_usercount", SqlDbType.Int,4) ,            
                        new SqlParameter("@k_day1", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day2", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day3", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day4", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@k_day5", SqlDbType.Decimal,5)             
              
            };

            parameters[0].Value = model.k_id;
            parameters[1].Value = model.k_day6;
            parameters[2].Value = model.k_day7;
            parameters[3].Value = model.k_day14;
            parameters[4].Value = model.k_day30;
            parameters[5].Value = model.k_time;
            parameters[6].Value = model.k_app_id;
            parameters[7].Value = model.k_type;
            parameters[8].Value = model.k_usercount;
            parameters[9].Value = model.k_day1;
            parameters[10].Value = model.k_day2;
            parameters[11].Value = model.k_day3;
            parameters[12].Value = model.k_day4;
            parameters[13].Value = model.k_day5;
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
        public bool Delete(int k_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_keep ");
            strSql.Append(" where k_id=@k_id");
            SqlParameter[] parameters = {
					new SqlParameter("@k_id", SqlDbType.Int,4)
			};
            parameters[0].Value = k_id;


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
        public bool DeleteList(string k_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_keep ");
            strSql.Append(" where ID in (" + k_idlist + ")  ");
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
        public JMP.MDL.jmp_keep GetModel(int k_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select k_id, k_day6, k_day7, k_day14, k_day30, k_time, k_app_id, k_type, k_usercount, k_day1, k_day2, k_day3, k_day4, k_day5  ");
            strSql.Append("  from jmp_keep ");
            strSql.Append(" where k_id=@k_id");
            SqlParameter[] parameters = {
					new SqlParameter("@k_id", SqlDbType.Int,4)
			};
            parameters[0].Value = k_id;


            JMP.MDL.jmp_keep model = new JMP.MDL.jmp_keep();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["k_id"].ToString() != "")
                {
                    model.k_id = int.Parse(ds.Tables[0].Rows[0]["k_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day6"].ToString() != "")
                {
                    model.k_day6 = decimal.Parse(ds.Tables[0].Rows[0]["k_day6"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day7"].ToString() != "")
                {
                    model.k_day7 = decimal.Parse(ds.Tables[0].Rows[0]["k_day7"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day14"].ToString() != "")
                {
                    model.k_day14 = decimal.Parse(ds.Tables[0].Rows[0]["k_day14"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day30"].ToString() != "")
                {
                    model.k_day30 = decimal.Parse(ds.Tables[0].Rows[0]["k_day30"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_time"].ToString() != "")
                {
                    model.k_time = DateTime.Parse(ds.Tables[0].Rows[0]["k_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_app_id"].ToString() != "")
                {
                    model.k_app_id = int.Parse(ds.Tables[0].Rows[0]["k_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_type"].ToString() != "")
                {
                    model.k_type = int.Parse(ds.Tables[0].Rows[0]["k_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_usercount"].ToString() != "")
                {
                    model.k_usercount = int.Parse(ds.Tables[0].Rows[0]["k_usercount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day1"].ToString() != "")
                {
                    model.k_day1 = decimal.Parse(ds.Tables[0].Rows[0]["k_day1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day2"].ToString() != "")
                {
                    model.k_day2 = decimal.Parse(ds.Tables[0].Rows[0]["k_day2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day3"].ToString() != "")
                {
                    model.k_day3 = decimal.Parse(ds.Tables[0].Rows[0]["k_day3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day4"].ToString() != "")
                {
                    model.k_day4 = decimal.Parse(ds.Tables[0].Rows[0]["k_day4"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k_day5"].ToString() != "")
                {
                    model.k_day5 = decimal.Parse(ds.Tables[0].Rows[0]["k_day5"].ToString());
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
            strSql.Append(" FROM jmp_keep ");
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
            strSql.Append(" FROM jmp_keep ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据条件查询留存汇总信息
        /// </summary>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <param name="searchType">查询条件</param>
        /// <param name="searchname">查询内容</param>
        /// <returns></returns>
        public DataTable SelectList(string stime, string etime, int searchType, string searchname, int setype)
        {
            string strsql = string.Format("   select sum(k_day6)as k_day6,sum(k_day7) as k_day7,sum(k_day14) as k_day14,sum(k_day30) as k_day30,k_time,k_type,sum(k_usercount) as k_usercount,sum(k_day1) as k_day1,sum(k_day2) as k_day2,sum(k_day3) as k_day3,sum(k_day4) as k_day4,sum(k_day5) as k_day5 from  jmp_keep a  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.k_app_id  left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1  ");
            if (setype == 1)
            {
                strsql += " and a.k_type='1' ";
            }
            else
            {
                strsql += " and a.k_type='0' ";
            }
            if (!string.IsNullOrEmpty(stime))
            {
                strsql += "  and convert(varchar(10),a.k_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strsql += "  and convert(varchar(10),a.k_time,120)<='" + etime + "' ";
            }
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        strsql += " and b.a_name ='" + searchname + "' ";
                        break;
                    case 2:
                        strsql += " and c.u_email='" + searchname + "' ";
                        break;
                }
            }
            strsql += "  group by a.k_time,k_type  order by a.k_time ";
            return DbHelperSQLTotal.Query(strsql).Tables[0];
        }
    }
}

