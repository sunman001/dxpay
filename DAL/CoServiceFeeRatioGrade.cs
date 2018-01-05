using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;

namespace JMP.DAL
{
 public partial  class CoServiceFeeRatioGrade
    {
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoServiceFeeRatioGrade");
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
        public int Add(JMP.MDL.CoServiceFeeRatioGrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoServiceFeeRatioGrade(");
            strSql.Append("CreatedByName,Name,ServiceFeeRatio,CustomerWithoutAgentRatio,BusinessPersonnelAgentRatio,AgentPushMoneyRatio,Description,CreatedOn,CreatedById");
            strSql.Append(") values (");
            strSql.Append("@CreatedByName,@Name,@ServiceFeeRatio,@CustomerWithoutAgentRatio,@BusinessPersonnelAgentRatio,@AgentPushMoneyRatio,@Description,@CreatedOn,@CreatedById");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ServiceFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@CustomerWithoutAgentRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@BusinessPersonnelAgentRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@AgentPushMoneyRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedById", SqlDbType.Int,4)

            };

            parameters[0].Value = model.CreatedByName;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.ServiceFeeRatio;
            parameters[3].Value = model.CustomerWithoutAgentRatio;
            parameters[4].Value = model.BusinessPersonnelAgentRatio;
            parameters[5].Value = model.AgentPushMoneyRatio;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.CreatedOn;
            parameters[8].Value = model.CreatedById;

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
        public bool Update (JMP.MDL.CoServiceFeeRatioGrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoServiceFeeRatioGrade set ");

            strSql.Append(" CreatedByName = @CreatedByName , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" ServiceFeeRatio = @ServiceFeeRatio , ");
            strSql.Append(" CustomerWithoutAgentRatio = @CustomerWithoutAgentRatio , ");
            strSql.Append(" BusinessPersonnelAgentRatio = @BusinessPersonnelAgentRatio , ");
            strSql.Append(" AgentPushMoneyRatio = @AgentPushMoneyRatio , ");
            strSql.Append(" Description = @Description , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedById = @CreatedById  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ServiceFeeRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@CustomerWithoutAgentRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@BusinessPersonnelAgentRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@AgentPushMoneyRatio", SqlDbType.Decimal,5) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedById", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.CreatedByName;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.ServiceFeeRatio;
            parameters[4].Value = model.CustomerWithoutAgentRatio;
            parameters[5].Value = model.BusinessPersonnelAgentRatio;
            parameters[6].Value = model.AgentPushMoneyRatio;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.CreatedOn;
            parameters[9].Value = model.CreatedById;
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
            strSql.Append("delete from CoServiceFeeRatioGrade ");
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
            strSql.Append("delete from CoServiceFeeRatioGrade ");
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
        public JMP.MDL.CoServiceFeeRatioGrade GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CreatedByName, Name, ServiceFeeRatio, CustomerWithoutAgentRatio, BusinessPersonnelAgentRatio, AgentPushMoneyRatio, Description, CreatedOn, CreatedById  ");
            strSql.Append("  from CoServiceFeeRatioGrade ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoServiceFeeRatioGrade model = new JMP.MDL.CoServiceFeeRatioGrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString() != "")
                {
                    model.ServiceFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CustomerWithoutAgentRatio"].ToString() != "")
                {
                    model.CustomerWithoutAgentRatio = decimal.Parse(ds.Tables[0].Rows[0]["CustomerWithoutAgentRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BusinessPersonnelAgentRatio"].ToString() != "")
                {
                    model.BusinessPersonnelAgentRatio = decimal.Parse(ds.Tables[0].Rows[0]["BusinessPersonnelAgentRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgentPushMoneyRatio"].ToString() != "")
                {
                    model.AgentPushMoneyRatio = decimal.Parse(ds.Tables[0].Rows[0]["AgentPushMoneyRatio"].ToString());
                }
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
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
            strSql.Append(" FROM CoServiceFeeRatioGrade ");
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
            strSql.Append(" FROM CoServiceFeeRatioGrade ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询服务费等级信息
        /// </summary>
        ///  <param name="progress">进度</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CoServiceFeeRatioGrade> SelectList( string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from CoServiceFeeRatioGrade  where 1=1");
            string Order = " Order by  a.id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and Name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += "  and CreatedByName like '%" + sea_name + "%' ";
                        break;
                }

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
            return DbHelperSQL.ToList<JMP.MDL.CoServiceFeeRatioGrade>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据应用id服务费
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.CoServiceFeeRatioGrade SelectId(int c_id)
        {
            string sql = string.Format("select * from CoServiceFeeRatioGrade where id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.CoServiceFeeRatioGrade>(dt);
        }

        public JMP.MDL.CoServiceFeeRatioGrade GetModelByName(string Name)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  ");
            strSql.Append("from CoServiceFeeRatioGrade ");
            strSql.Append(" where ");
            strSql.Append(" Name = @Name");
            SqlParameter[] parameters = {
                new SqlParameter("@Name", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = Name;
            JMP.MDL.CoServiceFeeRatioGrade model = new JMP.MDL.CoServiceFeeRatioGrade();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString() != "")
                {
                    model.ServiceFeeRatio = decimal.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CustomerWithoutAgentRatio"].ToString() != "")
                {
                    model.CustomerWithoutAgentRatio = decimal.Parse(ds.Tables[0].Rows[0]["CustomerWithoutAgentRatio"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BusinessPersonnelAgentRatio"].ToString() != "")
                {
                    model.BusinessPersonnelAgentRatio = decimal.Parse(ds.Tables[0].Rows[0]["BusinessPersonnelAgentRatio"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 查询默认费率
        /// </summary>
        /// <returns></returns>
        public JMP.MDL.CoServiceFeeRatioGrade GetModelById()
        {

            string sql = string.Format("select Id  from  CoServiceFeeRatioGrade where AgentPushMoneyRatio in(select  max(AgentPushMoneyRatio)   from CoServiceFeeRatioGrade )");
            dt = DbHelperSQL.Query(sql).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.CoServiceFeeRatioGrade>(dt);
        }

    }
}
