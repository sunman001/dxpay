using JMP.DBA;
using JMP.Model.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JMP.DAL
{
    ///<summary>
    ///角色表
    ///</summary>
    public partial class jmp_role
    {

        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_role");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_role(");
            strSql.Append("r_name,r_value,r_state,r_type");
            strSql.Append(") values (");
            strSql.Append("@r_name,@r_value,@r_state,@r_type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@r_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_value", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_state", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_type", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.r_name;
            parameters[1].Value = model.r_value;
            parameters[2].Value = model.r_state;
            parameters[3].Value = model.r_type;

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
        public bool Update(JMP.MDL.jmp_role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_role set ");

            strSql.Append(" r_name = @r_name , ");
            strSql.Append(" r_value = @r_value , ");
            strSql.Append(" r_state = @r_state , ");
            strSql.Append(" r_type = @r_type  ");
            strSql.Append(" where r_id=@r_id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@r_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_value", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@r_state", SqlDbType.Int,4) ,            
                        new SqlParameter("@r_type", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_name;
            parameters[2].Value = model.r_value;
            parameters[3].Value = model.r_state;
            parameters[4].Value = model.r_type;
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
        public bool Delete(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_role ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


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
        public bool DeleteList(string r_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_role ");
            strSql.Append(" where ID in (" + r_idlist + ")  ");
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
        public JMP.MDL.jmp_role GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id, r_name, r_value, r_state, r_type  ");
            strSql.Append("  from jmp_role ");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_id", SqlDbType.Int,4)
			};
            parameters[0].Value = r_id;


            JMP.MDL.jmp_role model = new JMP.MDL.jmp_role();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                model.r_name = ds.Tables[0].Rows[0]["r_name"].ToString();
                model.r_value = ds.Tables[0].Rows[0]["r_value"].ToString();
                if (ds.Tables[0].Rows[0]["r_state"].ToString() != "")
                {
                    model.r_state = int.Parse(ds.Tables[0].Rows[0]["r_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_type"].ToString() != "")
                {
                    model.r_type = int.Parse(ds.Tables[0].Rows[0]["r_type"].ToString());
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
            strSql.Append("select r_id, r_name, r_value, r_state, r_type ");
            strSql.Append(" FROM jmp_role ");
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
            strSql.Append(" r_id, r_name, r_value, r_state, r_type ");
            strSql.Append(" FROM jmp_role ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        DataTable dt = new DataTable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_role> SelectList(string sqls,string Order, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(sqls);
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_role>(ds.Tables[0]);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        public bool UpdateValue(JMP.MDL.jmp_role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_ROLE set ");
            strSql.Append("r_value=@r_value");
            strSql.Append(" where r_id=@r_id");
            SqlParameter[] parameters = {
					new SqlParameter("@r_value", SqlDbType.NChar,-1),
					new SqlParameter("@r_id", SqlDbType.Int,4)};
            parameters[0].Value = model.r_value;
            parameters[1].Value = model.r_id;

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
        /// 查询运营平台的角色-权限映射关系集合
        /// </summary>
        /// <returns></returns>
        public List<RolePermissionMappingQueryModel> FindAdminRolePermissionMappingList()
        {
            var sql = @";with T as ( SELECT A.r_id,  
     Split.a.value('.', 'INT') AS PermissionId  
 FROM  (SELECT jr.r_id,  
         CAST ('<M>' + REPLACE(jr.r_value, ',', '</M><M>') + '</M>' AS XML) AS PermissionId  
     FROM  dbo.jmp_role as jr where jr.r_state=1 and jr.r_type=0) AS A CROSS APPLY PermissionId.nodes ('/M') AS Split(a) 
)
select t.r_id as RoleId,t.PermissionId,jl.l_url as PermissionUrl from T 
left join dbo.jmp_limit as jl on jl.l_id=T.PermissionId";
            var ds = DbHelperSQL.Query(sql);
            var list = DbHelperSQL.ConvertToList<RolePermissionMappingQueryModel>(ds.Tables[0]).ToList();
            return list;
        }

    }
}

