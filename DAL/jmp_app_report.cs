using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
    /// <summary>
    /// 数据访问类:jmp_app_report
    /// </summary>
    public partial class jmp_app_report
    {
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_app_report");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            return DbHelperSQLTotal.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_app_report model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_app_report(");
            strSql.Append("r_times,r_starttime,r_username,r_uid,r_app_key,r_app_name,r_equipment,r_succeed,r_notpay,r_alipay)");
            strSql.Append(" values (");
            strSql.Append("@r_times,@r_starttime,@r_username,@r_uid,@r_app_key,@r_app_name,@r_equipment,@r_succeed,@r_notpay,@r_alipay)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@r_times", SqlDbType.DateTime),
					new SqlParameter("@r_starttime", SqlDbType.DateTime),
					new SqlParameter("@r_username", SqlDbType.NVarChar,-1),
					new SqlParameter("@r_uid", SqlDbType.Int,4),
					new SqlParameter("@r_app_key", SqlDbType.NVarChar,-1),
					new SqlParameter("@r_app_name", SqlDbType.NVarChar,-1),
					new SqlParameter("@r_equipment", SqlDbType.Int,4),
					new SqlParameter("@r_succeed", SqlDbType.Int,4),
					new SqlParameter("@r_notpay", SqlDbType.Int,4),
					new SqlParameter("@r_alipay", SqlDbType.Money,8)};
            parameters[0].Value = model.r_times;
            parameters[1].Value = model.r_starttime;
            parameters[2].Value = model.r_username;
            parameters[3].Value = model.r_uid;
            parameters[4].Value = model.r_app_key;
            parameters[5].Value = model.r_app_name;
            parameters[6].Value = model.r_equipment;
            parameters[7].Value = model.r_succeed;
            parameters[8].Value = model.r_notpay;
            parameters[9].Value = model.r_alipay;

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
        public bool Update(JMP.MDL.jmp_app_report model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_app_report set ");
            strSql.Append("r_times=@r_times,");
            strSql.Append("r_starttime=@r_starttime,");
            strSql.Append("r_username=@r_username,");
            strSql.Append("r_uid=@r_uid,");
            strSql.Append("r_app_key=@r_app_key,");
            strSql.Append("r_app_name=@r_app_name,");
            strSql.Append("r_equipment=@r_equipment,");
            strSql.Append("r_succeed=@r_succeed,");
            strSql.Append("r_notpay=@r_notpay,");
            strSql.Append("r_alipay=@r_alipay");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_times", SqlDbType.DateTime),
					new SqlParameter("@r_starttime", SqlDbType.DateTime),
					new SqlParameter("@r_username", SqlDbType.NVarChar,-1),
					new SqlParameter("@r_uid", SqlDbType.Int,4),
					new SqlParameter("@r_app_key", SqlDbType.NVarChar,-1),
					new SqlParameter("@r_app_name", SqlDbType.NVarChar,-1),
					new SqlParameter("@r_equipment", SqlDbType.Int,4),
					new SqlParameter("@r_succeed", SqlDbType.Int,4),
					new SqlParameter("@r_notpay", SqlDbType.Int,4),
					new SqlParameter("@r_alipay", SqlDbType.Money,8),
					new SqlParameter("@r_id", SqlDbType.Int,4)};
            parameters[0].Value = model.r_times;
            parameters[1].Value = model.r_starttime;
            parameters[2].Value = model.r_username;
            parameters[3].Value = model.r_uid;
            parameters[4].Value = model.r_app_key;
            parameters[5].Value = model.r_app_name;
            parameters[6].Value = model.r_equipment;
            parameters[7].Value = model.r_succeed;
            parameters[8].Value = model.r_notpay;
            parameters[9].Value = model.r_alipay;
            parameters[10].Value = model.r_id;

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
        public bool Delete(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_app_report ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string r_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_app_report ");
            strSql.Append(" where r_id in (" + r_idlist + ")  ");
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
        public JMP.MDL.jmp_app_report GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 r_id,r_times,r_starttime,r_username,r_uid,r_app_key,r_app_name,r_equipment,r_succeed,r_notpay,r_alipay from jmp_app_report ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            JMP.MDL.jmp_app_report model = new JMP.MDL.jmp_app_report();
            DataSet ds = DbHelperSQLTotal.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_app_report DataRowToModel(DataRow row)
        {
            JMP.MDL.jmp_app_report model = new JMP.MDL.jmp_app_report();
            if (row != null)
            {
                if (row["r_id"] != null && row["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(row["r_id"].ToString());
                }
                if (row["r_times"] != null && row["r_times"].ToString() != "")
                {
                    model.r_times = DateTime.Parse(row["r_times"].ToString());
                }
                if (row["r_starttime"] != null && row["r_starttime"].ToString() != "")
                {
                    model.r_starttime = DateTime.Parse(row["r_starttime"].ToString());
                }
                if (row["r_username"] != null)
                {
                    model.r_username = row["r_username"].ToString();
                }
                if (row["r_uid"] != null && row["r_uid"].ToString() != "")
                {
                    model.r_uid = int.Parse(row["r_uid"].ToString());
                }
                if (row["r_app_key"] != null)
                {
                    model.r_app_key = row["r_app_key"].ToString();
                }
                if (row["r_app_name"] != null)
                {
                    model.r_app_name = row["r_app_name"].ToString();
                }
                if (row["r_equipment"] != null && row["r_equipment"].ToString() != "")
                {
                    model.r_equipment = int.Parse(row["r_equipment"].ToString());
                }
                if (row["r_succeed"] != null && row["r_succeed"].ToString() != "")
                {
                    model.r_succeed = int.Parse(row["r_succeed"].ToString());
                }
                if (row["r_notpay"] != null && row["r_notpay"].ToString() != "")
                {
                    model.r_notpay = int.Parse(row["r_notpay"].ToString());
                }
                if (row["r_alipay"] != null && row["r_alipay"].ToString() != "")
                {
                    model.r_alipay = decimal.Parse(row["r_alipay"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id,r_times,r_starttime,r_username,r_uid,r_app_key,r_app_name,r_equipment,r_succeed,r_notpay,r_alipay ");
            strSql.Append(" FROM jmp_app_report ");
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
            strSql.Append(" r_id,r_times,r_starttime,r_username,r_uid,r_app_key,r_app_name,r_equipment,r_succeed,r_notpay,r_alipay ");
            strSql.Append(" FROM jmp_app_report ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM jmp_app_report ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQLTotal.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.r_id desc");
            }
            strSql.Append(")AS Row, T.*  from jmp_app_report T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQLTotal.Query(strSql.ToString());
        }

       

        #endregion  BasicMethod

        #region 用户报表-陶涛-2016-03-23
        DataTable dt = new DataTable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_app_report> SelectList(string sqls, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
            SqlParameter[] s = new[] { 
                  new SqlParameter("@sqlstr",sql.ToString()),
                  new SqlParameter("@pageIndex",pageIndexs),
                  new SqlParameter("@pageSize",PageSize)
            };
            SqlDataReader reader = DbHelperSQLTotal.RunProcedure("page", s);
            pageCount = 0;
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    pageCount = Convert.ToInt32(reader[0].ToString());
                }
                if (reader.NextResult())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            reader.Close();
            return DbHelperSQLTotal.ToList<JMP.MDL.jmp_app_report>(dt);
        }
        #endregion
    }
}
