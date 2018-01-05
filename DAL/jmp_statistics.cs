using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.DAL
{
    ///<summary>
    ///手机品牌统计
    ///</summary>
    public partial class jmp_statistics
    {

        public bool Exists(int s_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_statistics");
            strSql.Append(" where ");
            strSql.Append(" s_id = @s_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
			};
            parameters[0].Value = s_id;
            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_statistics model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_statistics(");
            strSql.Append("s_brand,s_app_id,s_count,s_time");
            strSql.Append(") values (");
            strSql.Append("@s_brand,@s_app_id,@s_count,@s_time");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@s_brand", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@s_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.s_brand;
            parameters[1].Value = model.s_app_id;
            parameters[2].Value = model.s_count;
            parameters[3].Value = model.s_time;

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
        public bool Update(JMP.MDL.jmp_statistics model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_statistics set ");

            strSql.Append(" s_brand = @s_brand , ");
            strSql.Append(" s_app_id = @s_app_id , ");
            strSql.Append(" s_count = @s_count , ");
            strSql.Append(" s_time = @s_time  ");
            strSql.Append(" where s_id=@s_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@s_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_brand", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@s_app_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_count", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_time", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.s_id;
            parameters[1].Value = model.s_brand;
            parameters[2].Value = model.s_app_id;
            parameters[3].Value = model.s_count;
            parameters[4].Value = model.s_time;
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
        public bool Delete(int s_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_statistics ");
            strSql.Append(" where s_id=@s_id");
            SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
			};
            parameters[0].Value = s_id;


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
        public bool DeleteList(string s_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_statistics ");
            strSql.Append(" where ID in (" + s_idlist + ")  ");
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
        public JMP.MDL.jmp_statistics GetModel(int s_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select s_id, s_brand, s_app_id, s_count, s_time  ");
            strSql.Append("  from jmp_statistics ");
            strSql.Append(" where s_id=@s_id");
            SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
			};
            parameters[0].Value = s_id;


            JMP.MDL.jmp_statistics model = new JMP.MDL.jmp_statistics();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["s_id"].ToString() != "")
                {
                    model.s_id = int.Parse(ds.Tables[0].Rows[0]["s_id"].ToString());
                }
                model.s_brand = ds.Tables[0].Rows[0]["s_brand"].ToString();
                if (ds.Tables[0].Rows[0]["s_app_id"].ToString() != "")
                {
                    model.s_app_id = int.Parse(ds.Tables[0].Rows[0]["s_app_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["s_count"].ToString() != "")
                {
                    model.s_count = int.Parse(ds.Tables[0].Rows[0]["s_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["s_time"].ToString() != "")
                {
                    model.s_time = DateTime.Parse(ds.Tables[0].Rows[0]["s_time"].ToString());
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
            strSql.Append(" FROM jmp_statistics ");
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
            strSql.Append(" FROM jmp_statistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表用于图标统计
        /// </summary>
        public List<JMP.MDL.jmp_statistics> GetListTjCount(string stime, string etime, int searchType, string searchname)
        {
            string strSql = string.Format(" select top 10 a.s_brand,sum(a.s_count)as s_count from  jmp_statistics  a left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_app b on b.a_id=a.s_app_id left join  " + JMP.DbName.PubDbName.dbbase + ".dbo.jmp_user c on c.u_id=b.a_user_id where 1=1   ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),a.s_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),a.s_time,120)<='" + etime + "' ";
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
            strSql += " group by a.s_brand  order by s_count desc ";
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_statistics>(dt);
        }
        /// <summary>
        /// 查询总数用户报表统计
        /// </summary>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="searchType"></param>
        /// <param name="searchname"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_statistics modelCoutn(string stime, string etime)
        {
            string strSql = string.Format(" select sum(s_count) as s_count  from   jmp_statistics  where 1=1 ");
            if (!string.IsNullOrEmpty(stime))
            {
                strSql += "  and convert(varchar(10),s_time,120)>='" + stime + "' ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                strSql += "  and convert(varchar(10),s_time,120)<='" + etime + "' ";
            }
            DataTable dt = DbHelperSQLTotal.Query(strSql.ToString()).Tables[0];
            return DbHelperSQLTotal.ToModel<JMP.MDL.jmp_statistics>(dt);

        }
    }
}
