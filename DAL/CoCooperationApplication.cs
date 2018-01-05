using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
  public partial  class CoCooperationApplication
    {
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoCooperationApplication");
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
        public int Add(JMP.MDL.CoCooperationApplication model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoCooperationApplication(");
            strSql.Append("LatestReadTime,GrabbedDate,GrabbedById,GrabbedByName,State,Name,EmailAddress,MobilePhone,QQ,Website,RequestContent,CreatedOn,ReadCount");
            strSql.Append(") values (");
            strSql.Append("@LatestReadTime,@GrabbedDate,@GrabbedById,@GrabbedByName,@State,@Name,@EmailAddress,@MobilePhone,@QQ,@Website,@RequestContent,@CreatedOn,@ReadCount");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@LatestReadTime", SqlDbType.DateTime) ,
                        new SqlParameter("@GrabbedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@GrabbedById", SqlDbType.Int,4) ,
                        new SqlParameter("@GrabbedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@MobilePhone", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@QQ", SqlDbType.NVarChar,15) ,
                        new SqlParameter("@Website", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@RequestContent", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ReadCount", SqlDbType.Int,4)

            };

            parameters[0].Value = model.LatestReadTime;
            parameters[1].Value = model.GrabbedDate;
            parameters[2].Value = model.GrabbedById;
            parameters[3].Value = model.GrabbedByName;
            parameters[4].Value = model.State;
            parameters[5].Value = model.Name;
            parameters[6].Value = model.EmailAddress;
            parameters[7].Value = model.MobilePhone;
            parameters[8].Value = model.QQ;
            parameters[9].Value = model.Website;
            parameters[10].Value = model.RequestContent;
            parameters[11].Value = model.CreatedOn;
            parameters[12].Value = model.ReadCount;

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
        public bool Update(JMP.MDL.CoCooperationApplication model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoCooperationApplication set ");

            strSql.Append(" LatestReadTime = @LatestReadTime , ");
            strSql.Append(" GrabbedDate = @GrabbedDate , ");
            strSql.Append(" GrabbedById = @GrabbedById , ");
            strSql.Append(" GrabbedByName = @GrabbedByName , ");
            strSql.Append(" State = @State , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" EmailAddress = @EmailAddress , ");
            strSql.Append(" MobilePhone = @MobilePhone , ");
            strSql.Append(" QQ = @QQ , ");
            strSql.Append(" Website = @Website , ");
            strSql.Append(" RequestContent = @RequestContent , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" ReadCount = @ReadCount  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@LatestReadTime", SqlDbType.DateTime) ,
                        new SqlParameter("@GrabbedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@GrabbedById", SqlDbType.Int,4) ,
                        new SqlParameter("@GrabbedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@MobilePhone", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@QQ", SqlDbType.NVarChar,15) ,
                        new SqlParameter("@Website", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@RequestContent", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ReadCount", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.LatestReadTime;
            parameters[2].Value = model.GrabbedDate;
            parameters[3].Value = model.GrabbedById;
            parameters[4].Value = model.GrabbedByName;
            parameters[5].Value = model.State;
            parameters[6].Value = model.Name;
            parameters[7].Value = model.EmailAddress;
            parameters[8].Value = model.MobilePhone;
            parameters[9].Value = model.QQ;
            parameters[10].Value = model.Website;
            parameters[11].Value = model.RequestContent;
            parameters[12].Value = model.CreatedOn;
            parameters[13].Value = model.ReadCount;
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
            strSql.Append("delete from CoCooperationApplication ");
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
            strSql.Append("delete from CoCooperationApplication ");
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
        public  JMP.MDL.CoCooperationApplication GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, LatestReadTime, GrabbedDate, GrabbedById, GrabbedByName, State, Name, EmailAddress, MobilePhone, QQ, Website, RequestContent, CreatedOn, ReadCount  ");
            strSql.Append("  from CoCooperationApplication ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoCooperationApplication model = new JMP.MDL.CoCooperationApplication();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LatestReadTime"].ToString() != "")
                {
                    model.LatestReadTime = DateTime.Parse(ds.Tables[0].Rows[0]["LatestReadTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GrabbedDate"].ToString() != "")
                {
                    model.GrabbedDate = DateTime.Parse(ds.Tables[0].Rows[0]["GrabbedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GrabbedById"].ToString() != "")
                {
                    model.GrabbedById = int.Parse(ds.Tables[0].Rows[0]["GrabbedById"].ToString());
                }
                model.GrabbedByName = ds.Tables[0].Rows[0]["GrabbedByName"].ToString();
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.Website = ds.Tables[0].Rows[0]["Website"].ToString();
                model.RequestContent = ds.Tables[0].Rows[0]["RequestContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReadCount"].ToString() != "")
                {
                    model.ReadCount = int.Parse(ds.Tables[0].Rows[0]["ReadCount"].ToString());
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
            strSql.Append(" FROM CoCooperationApplication ");
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
            strSql.Append(" FROM CoCooperationApplication ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询未抢单列表
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
        public List<JMP.MDL.CoCooperationApplication> SelectList(string status,  string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select Id,[Name],EmailAddress,MobilePhone, QQ,Website,RequestContent,CreatedOn,[State] from CoCooperationApplication WHERE [State]=0");
            string Order = " Order by  ID desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and Name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and EmailAddress like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        sql += " and MobilePhone like  '%" + sea_name + "%' ";
                        break;
                }

            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += " and [State]='" + status + "' ";
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
            return DbHelperSQL.ToList<JMP.MDL.CoCooperationApplication>(ds.Tables[0]);
        }

        /// <summary>
        /// 查询我的抢单代理商合作信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="sea_name"></param>
        /// <param name="type"></param>
        /// <param name="searchDesc"></param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.CoCooperationApplication> SelectListById(string status, string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount,int userid)
        {
            string sql = string.Format("select Id,[Name],EmailAddress,MobilePhone, QQ,Website,RequestContent,CreatedOn,[State] from CoCooperationApplication WHERE  GrabbedById='"+userid+"' ");

            string Order = " Order by  ID desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "and Name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += "and EmailAddress like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        sql += "and MobilePhone like  '%" + sea_name + "%' ";
                        break;
                }
            }
            if(!string.IsNullOrEmpty(status))
            {
                sql += " and State='"+status+"'";
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
            return DbHelperSQL.ToList<JMP.MDL.CoCooperationApplication>(ds.Tables[0]);
        }


        /// <summary>
        /// 修改抢单状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(int id, string state, DateTime GrabbedDate, int GrabbedById, string GrabbedByName)
        {
            StringBuilder strSql = new StringBuilder();
            if(state=="1")
            {
                strSql.Append(" update CoCooperationApplication set [State]='" + state +" ', GrabbedDate='"+ GrabbedDate + "',GrabbedByName='"+ GrabbedByName + "',GrabbedById='"+ GrabbedById + "' ");
            }
            else
            {
              strSql.Append(" update CoCooperationApplication set [State]=" + state + " ");
            }
            
            strSql.Append(" where id = " + id + "  ");
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
