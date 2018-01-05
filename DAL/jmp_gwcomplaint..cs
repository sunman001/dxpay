using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
  public   class jmp_gwcomplaint
    {
      

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "jmp_gwcomplaint");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_gwcomplaint");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_gwcomplaint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_gwcomplaint(");
            strSql.Append("name,telephone,reason,state,cltime,tjtime,cluser,result,remarks)");
            strSql.Append(" values (");
            strSql.Append("@name,@telephone,@reason,@state,@cltime,@tjtime,@cluser,@cljg ,@remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@telephone", SqlDbType.NVarChar,50),
                    new SqlParameter("@reason", SqlDbType.NVarChar,-1),
                    new SqlParameter("@state", SqlDbType.Int,4),
                    new SqlParameter("@cltime", SqlDbType.DateTime),
                    new SqlParameter("@tjtime", SqlDbType.DateTime),
                    new SqlParameter("@cluser", SqlDbType.NVarChar,50),
                    new SqlParameter("@cljg", SqlDbType.NVarChar,-1),
                    new SqlParameter("@remarks", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.telephone;
            parameters[2].Value = model.reason;
            parameters[3].Value = model.state;
            parameters[4].Value = model.cltime;
            parameters[5].Value = model.tjtime;
            parameters[6].Value = model.cluser;
            parameters[7].Value = model.result;
            parameters[8].Value = model.remarks;
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
        public bool Update(JMP.MDL.jmp_gwcomplaint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_gwcomplaint set ");
            strSql.Append("name=@name,");
            strSql.Append("telephone=@telephone,");
            strSql.Append("reason=@reason,");
            strSql.Append("state=@state,");
            strSql.Append("cltime=@cltime,");
            strSql.Append("tjtime=@tjtime,");
            strSql.Append("cluser=@cluser,");
            strSql.Append("reason=@cljg");
            strSql.Append("remarks=@remarks");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@telephone", SqlDbType.NVarChar,50),
                    new SqlParameter("@reason", SqlDbType.NVarChar,-1),
                    new SqlParameter("@state", SqlDbType.Int,4),
                    new SqlParameter("@cltime", SqlDbType.DateTime),
                    new SqlParameter("@tjtime", SqlDbType.DateTime),
                    new SqlParameter("@cluser", SqlDbType.NVarChar,50),
                    new SqlParameter("@cljg", SqlDbType.NVarChar,-1),
                    new SqlParameter("@remarks", SqlDbType.NVarChar,-1),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.telephone;
            parameters[2].Value = model.reason;
            parameters[3].Value = model.state;
            parameters[4].Value = model.cltime;
            parameters[5].Value = model.tjtime;
            parameters[6].Value = model.cluser;
            parameters[7].Value = model.result;
            parameters[8].Value = model.remarks;
            parameters[9].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_gwcomplaint ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_gwcomplaint ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_gwcomplaint GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,telephone,reason,state,cltime,tjtime,cluser,cljg from jmp_gwcomplaint ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            JMP.MDL.jmp_gwcomplaint model = new JMP.MDL.jmp_gwcomplaint();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
        public JMP.MDL.jmp_gwcomplaint DataRowToModel(DataRow row)
        {
            JMP.MDL.jmp_gwcomplaint model = new JMP.MDL.jmp_gwcomplaint();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["telephone"] != null)
                {
                    model.telephone = row["telephone"].ToString();
                }
                if (row["reason"] != null)
                {
                    model.reason = row["reason"].ToString();
                }
                if (row["state"] != null && row["state"].ToString() != "")
                {
                    model.state = int.Parse(row["state"].ToString());
                }
                if (row["cltime"] != null && row["cltime"].ToString() != "")
                {
                    model.cltime = DateTime.Parse(row["cltime"].ToString());
                }
                if (row["tjtime"] != null && row["tjtime"].ToString() != "")
                {
                    model.tjtime = DateTime.Parse(row["tjtime"].ToString());
                }
                if (row["cluser"] != null)
                {
                    model.cluser = row["cluser"].ToString();
                }
                if (row["cljg"] != null)
                {
                    model.result = row["result"].ToString();
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
            strSql.Append("select id,name,telephone,reason,state,cltime,tjtime,cluser,cljg ");
            strSql.Append(" FROM jmp_gwcomplaint ");
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
            strSql.Append(" id,name,telephone,reason,state,cltime,tjtime,cluser,cljg ");
            strSql.Append(" FROM jmp_gwcomplaint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM jmp_gwcomplaint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from jmp_gwcomplaint T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据应id查询信息
        /// </summary>
        /// <param name="c_id">应用投诉id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.jmp_gwcomplaint SelectId(int c_id)
        {
            string sql = string.Format(" select * from  jmp_gwcomplaint where id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_gwcomplaint>(dt);
        }
        /// <summary>
        /// 查询官网投诉管理
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
        public List<JMP.MDL.jmp_gwcomplaint> SelectList(string auditstate, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select * from  jmp_gwcomplaint where 1=1");
            string Order = " Order by id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and telephone like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and reason like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and cluser ='" + sea_name + "' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            {
                sql += " and tjtime>='" + stime + " 00:00:00' and tjtime<='" + endtime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and state='" + auditstate + "' ";
            }

            if (searchDesc == 1)
            {
                Order = " order by id  ";
            }
            else
            {
                Order = " order by id desc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_gwcomplaint>(ds.Tables[0]);
        }


        /// <summary>
        /// 官网投诉管理
        /// </summary>
        /// <param name="uids">选择投诉的ID</param>
        /// <param name="remark">处理结果</param>
        /// <returns></returns>
        public bool ComplaintLC(string uids, string remark, string r_auditor)
        {

            DateTime time = DateTime.Now;
            var strSql = string.Format("UPDATE jmp_gwcomplaint SET result='{1}',state='1', cluser='{2}' ,cltime='{3}' WHERE id IN ({0})", uids, remark, r_auditor, time);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }



    }
}
