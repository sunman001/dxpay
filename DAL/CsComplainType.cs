using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //投诉类型表
    public partial class CsComplainType
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CsComplainType");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.CsComplainType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CsComplainType(");
            strSql.Append("Name,Description,state,CreatedOn,CreatedByUserId");
            strSql.Append(") values (");
            strSql.Append("@Name,@Description,@state,@CreatedOn,@CreatedByUserId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@Name", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@state", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Name;
            parameters[1].Value = model.Description;
            parameters[2].Value = model.state;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedByUserId;

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
        public bool Update(JMP.MDL.CsComplainType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CsComplainType set ");

            strSql.Append(" Name = @Name , ");
            strSql.Append(" Description = @Description , ");
            strSql.Append(" state = @state , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedByUserId = @CreatedByUserId  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@state", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.state;
            parameters[4].Value = model.CreatedOn;
            parameters[5].Value = model.CreatedByUserId;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CsComplainType ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CsComplainType ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
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
        public JMP.MDL.CsComplainType GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Name, Description, state, CreatedOn, CreatedByUserId  ");
            strSql.Append("  from CsComplainType ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CsComplainType model = new JMP.MDL.CsComplainType();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                if (ds.Tables[0].Rows[0]["state"].ToString() != "")
                {
                    model.state = int.Parse(ds.Tables[0].Rows[0]["state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedByUserId"].ToString() != "")
                {
                    model.CreatedByUserId = int.Parse(ds.Tables[0].Rows[0]["CreatedByUserId"].ToString());
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
            strSql.Append(" FROM CsComplainType ");
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
            strSql.Append(" FROM CsComplainType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询投诉类型
        /// </summary>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="SelectState">状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CsComplainType> SelectList(string sea_name, string type, int SelectState, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.*,b.u_realname FROM CsComplainType a,jmp_locuser b where a.CreatedByUserId=b.u_id ");
            string Order = "";
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "1":
                        if (!string.IsNullOrEmpty(sea_name))
                        {
                            sql += "  and [Name] like '%" + sea_name + "%' ";
                        }
                        break;
                }

            }
            if (SelectState > -1)
            {
                sql += " and [state]='" + SelectState + "' ";
            }
            if (searchDesc == 0)
            {
                Order = "order by Id desc";
            }
            else
            {
                Order = "order by Id ";
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
            return DbHelperSQL.ToList<JMP.MDL.CsComplainType>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateCustomState(string idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update CsComplainType set [state]=" + state + "  ");
            strSql.Append(" where Id in (" + idlist + ")  ");
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

    }
}

