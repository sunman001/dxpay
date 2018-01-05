using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    /// <summary>
    /// 工单交流表
    /// </summary>
    public partial class jmp_exchange
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_exchange");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_exchange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_exchange(");
            strSql.Append("workorderid,handlerid,handledate,handleresultdescription");
            strSql.Append(") values (");
            strSql.Append("@workorderid,@handlerid,@handledate,@handleresultdescription");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@workorderid", SqlDbType.Int,4) ,
                        new SqlParameter("@handlerid", SqlDbType.Int,4) ,
                        new SqlParameter("@handledate", SqlDbType.DateTime) ,
                        new SqlParameter("@handleresultdescription", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.workorderid;
            parameters[1].Value = model.handlerid;
            parameters[2].Value = model.handledate;
            parameters[3].Value = model.handleresultdescription;

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
        public bool Update(JMP.MDL.jmp_exchange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_exchange set ");

            strSql.Append(" workorderid = @workorderid , ");
            strSql.Append(" handlerid = @handlerid , ");
            strSql.Append(" handledate = @handledate , ");
            strSql.Append(" handleresultdescription = @handleresultdescription  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@workorderid", SqlDbType.Int,4) ,
                        new SqlParameter("@handlerid", SqlDbType.Int,4) ,
                        new SqlParameter("@handledate", SqlDbType.DateTime) ,
                        new SqlParameter("@handleresultdescription", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.workorderid;
            parameters[2].Value = model.handlerid;
            parameters[3].Value = model.handledate;
            parameters[4].Value = model.handleresultdescription;
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
            strSql.Append("delete from jmp_exchange ");
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_exchange ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_exchange GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, workorderid, handlerid, handledate, handleresultdescription  ");
            strSql.Append("  from jmp_exchange ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            JMP.MDL.jmp_exchange model = new JMP.MDL.jmp_exchange();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["workorderid"].ToString() != "")
                {
                    model.workorderid = int.Parse(ds.Tables[0].Rows[0]["workorderid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["handlerid"].ToString() != "")
                {
                    model.handlerid = int.Parse(ds.Tables[0].Rows[0]["handlerid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["handledate"].ToString() != "")
                {
                    model.handledate = DateTime.Parse(ds.Tables[0].Rows[0]["handledate"].ToString());
                }
                model.handleresultdescription = ds.Tables[0].Rows[0]["handleresultdescription"].ToString();

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
            strSql.Append(" FROM jmp_exchange ");
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
            strSql.Append(" FROM jmp_exchange ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询工单交流
        /// </summary>
        /// <param name="status">审核状态</param>
        ///  <param name="progress">进度</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_exchange> SelectListByworkorderid(int workorderid, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.* ,b.[catalog],b.content,b.title,c.u_realname as name,d.u_realname as creartbyname from jmp_exchange a left join jmp_workorder b on a.workorderid=b.id left join jmp_locuser c on b.watchidsoftheday=c.u_id left join jmp_locuser d on a.handlerid=d.u_id   where workorderid="+workorderid+"");
            string Order = " Order by  id desc";
           
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_exchange>(ds.Tables[0]);
        }


    }
}

