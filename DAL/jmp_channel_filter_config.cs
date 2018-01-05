using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //通道过滤规则配置表
    public partial class jmp_channel_filter_config
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_channel_filter_config");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool Exists(int TypeId, int TargetId, int RelatedId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from (select TypeId, TargetId, RelatedId, CreatedByUserName, b.l_corporatename as RelatedName  from jmp_channel_filter_config a left join jmp_interface b on a.RelatedId = b.l_id  where TargetId = 1 group by TypeId, TargetId, RelatedId, CreatedByUserName, b.l_corporatename union all select TypeId, TargetId, RelatedId, CreatedByUserName, b.PoolName as RelatedName  from jmp_channel_filter_config a left join jmp_channel_pool b on a.RelatedId = b.Id where TargetId = 2 group by TypeId, TargetId, RelatedId, CreatedByUserName, b.PoolName  union all  select TypeId, TargetId, RelatedId, CreatedByUserName, '全局通道' as RelatedName  from jmp_channel_filter_config where  TargetId =0  group by TypeId, TargetId, RelatedId, CreatedByUserName) a");
            strSql.Append(" where ");
            strSql.Append(" TypeId=@TypeId and TargetId=@TargetId and RelatedId=@RelatedId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TypeId", SqlDbType.Int,4),
                     new SqlParameter("@TargetId", SqlDbType.Int,4),
                     new SqlParameter("@RelatedId", SqlDbType.Int,4)
            };
            parameters[0].Value = TypeId;
            parameters[1].Value = TargetId;
            parameters[2].Value = RelatedId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int  Add(JMP.MDL.jmp_channel_filter_config model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_channel_filter_config(");
            strSql.Append("CreatedByUserName,TypeId,TargetId,WhichHour,Threshold,RelatedId,IntervalOfRecover,CreatedOn,CreatedByUserId");
            strSql.Append(") values (");
            strSql.Append("@CreatedByUserName,@TypeId,@TargetId,@WhichHour,@Threshold,@RelatedId,@IntervalOfRecover,@CreatedOn,@CreatedByUserId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedByUserName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@TypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@TargetId", SqlDbType.Int,4) ,
                        new SqlParameter("@WhichHour", SqlDbType.Int,4) ,
                        new SqlParameter("@Threshold", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RelatedId", SqlDbType.Int,4) ,
                        new SqlParameter("@IntervalOfRecover", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4)

            };
            
            parameters[0].Value = model.CreatedByUserName;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.TargetId;
            parameters[3].Value = model.WhichHour;
            parameters[4].Value = model.Threshold;
            parameters[5].Value = model.RelatedId;
            parameters[6].Value = model.IntervalOfRecover;
            parameters[7].Value = model.CreatedOn;
            parameters[8].Value = model.CreatedByUserId;
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
        public bool Update(JMP.MDL.jmp_channel_filter_config model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_channel_filter_config set ");

            strSql.Append(" Id = @Id , ");
            strSql.Append(" CreatedByUserName = @CreatedByUserName , ");
            strSql.Append(" TypeId = @TypeId , ");
            strSql.Append(" TargetId = @TargetId , ");
            strSql.Append(" WhichHour = @WhichHour , ");
            strSql.Append(" Threshold = @Threshold , ");
            strSql.Append(" RelatedId = @RelatedId , ");
            strSql.Append(" IntervalOfRecover = @IntervalOfRecover , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedByUserId = @CreatedByUserId  ");
            strSql.Append(" where Id=@Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUserName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@TypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@TargetId", SqlDbType.Int,4) ,
                        new SqlParameter("@WhichHour", SqlDbType.Int,4) ,
                        new SqlParameter("@Threshold", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RelatedId", SqlDbType.Int,4) ,
                        new SqlParameter("@IntervalOfRecover", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedByUserId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.CreatedByUserName;
            parameters[2].Value = model.TypeId;
            parameters[3].Value = model.TargetId;
            parameters[4].Value = model.WhichHour;
            parameters[5].Value = model.Threshold;
            parameters[6].Value = model.RelatedId;
            parameters[7].Value = model.IntervalOfRecover;
            parameters[8].Value = model.CreatedOn;
            parameters[9].Value = model.CreatedByUserId;
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
            strSql.Append("delete from jmp_channel_filter_config ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)            };
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
        public bool DeleteByType(int TypeId, int TargetId, int RelatedId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_channel_filter_config ");
            strSql.Append(" where TypeId=@TypeId and TargetId=@TargetId and RelatedId=@RelatedId");
            SqlParameter[] parameters = {
                    new SqlParameter("@TypeId", SqlDbType.Int,4),
                     new SqlParameter("@TargetId", SqlDbType.Int,4),
                     new SqlParameter("@RelatedId", SqlDbType.Int,4)
            };
            parameters[0].Value = TypeId;
            parameters[1].Value = TargetId;
            parameters[2].Value = RelatedId;
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
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_channel_filter_config GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CreatedByUserName, TypeId, TargetId, WhichHour, Threshold, RelatedId, IntervalOfRecover, CreatedOn, CreatedByUserId  ");
            strSql.Append("  from jmp_channel_filter_config ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)            };
            parameters[0].Value = Id;


            JMP.MDL.jmp_channel_filter_config model = new JMP.MDL.jmp_channel_filter_config();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.CreatedByUserName = ds.Tables[0].Rows[0]["CreatedByUserName"].ToString();
                if (ds.Tables[0].Rows[0]["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TargetId"].ToString() != "")
                {
                    model.TargetId = int.Parse(ds.Tables[0].Rows[0]["TargetId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WhichHour"].ToString() != "")
                {
                    model.WhichHour = int.Parse(ds.Tables[0].Rows[0]["WhichHour"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Threshold"].ToString() != "")
                {
                    model.Threshold = decimal.Parse(ds.Tables[0].Rows[0]["Threshold"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RelatedId"].ToString() != "")
                {
                    model.RelatedId = int.Parse(ds.Tables[0].Rows[0]["RelatedId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IntervalOfRecover"].ToString() != "")
                {
                    model.IntervalOfRecover = int.Parse(ds.Tables[0].Rows[0]["IntervalOfRecover"].ToString());
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
            strSql.Append(" FROM jmp_channel_filter_config ");
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
            strSql.Append(" FROM jmp_channel_filter_config ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取数量列表
        /// </summary>
        /// <param name="Sname">值班人</param>
        /// <param name="startdate">值班开始时间</param>
        /// <param name="enddate">值班结束</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_channel_filter_config> SelectList( int type,string sea_name,int TypeId,int TargetId, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from (select TypeId, TargetId, RelatedId, CreatedByUserName, b.l_corporatename as RelatedName  from jmp_channel_filter_config a left join jmp_interface b on a.RelatedId = b.l_id  where TargetId = 1 group by TypeId, TargetId, RelatedId, CreatedByUserName, b.l_corporatename union all select TypeId, TargetId, RelatedId, CreatedByUserName, b.PoolName as RelatedName  from jmp_channel_filter_config a left join jmp_channel_pool b on a.RelatedId = b.Id where TargetId = 2 group by TypeId, TargetId, RelatedId, CreatedByUserName, b.PoolName  union all  select TypeId, TargetId, RelatedId, CreatedByUserName, '全局通道' as RelatedName  from jmp_channel_filter_config where  TargetId =0  group by TypeId, TargetId, RelatedId, CreatedByUserName) a where 1 = 1");
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and RelatedName like  '%" + sea_name + "%'";
                        break;
                }
            }

            if (TypeId>=0)
            {
                sql += " and TypeId='"+TypeId+"'";
            }
            if (TargetId >= 0)
            {
                sql += " and TypeId='" + TargetId + "'";
            }
         
            string Order = " order by TargetId asc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_channel_filter_config>(ds.Tables[0]);
        }
        
    }
}

