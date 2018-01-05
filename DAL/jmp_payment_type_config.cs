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
    /// <summary>
    /// 支付通道配置表
    /// </summary>
    public class jmp_payment_type_config
    {
        DataTable dt = new DataTable();
        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(JMP.MDL.jmp_payment_type_config model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_payment_type_config(");
            strSql.Append("PaymentTypeId,Label,FieldName,InputType,Description,Status,CreatedBy");
            strSql.Append(") values (");
            strSql.Append("@PaymentTypeId,@Label,@FieldName,@InputType,@Description,@Status,@CreatedBy");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@PaymentTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Label", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@FieldName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@InputType", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,-1),
                        new SqlParameter("@Status",SqlDbType.Int,4),
                        new SqlParameter("@CreatedBy",SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.PaymentTypeId;
            parameters[1].Value = model.Label;
            parameters[2].Value = model.FieldName;
            parameters[3].Value = model.InputType;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.CreatedBy;
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
        /// 修改方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(JMP.MDL.jmp_payment_type_config model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_payment_type_config set ");
            strSql.Append(" PaymentTypeId=@PaymentTypeId,Label=@Label,FieldName=@FieldName,InputType=@InputType,Description=@Description,Status=@Status,CreatedBy=@CreatedBy ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@PaymentTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Label", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@FieldName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@InputType", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,-1),
                        new SqlParameter("@Status",SqlDbType.Int,4),
                        new SqlParameter("@CreatedBy",SqlDbType.NVarChar,50),
                        new SqlParameter("@Id",SqlDbType.Int,4)

            };
            parameters[0].Value = model.PaymentTypeId;
            parameters[1].Value = model.Label;
            parameters[2].Value = model.FieldName;
            parameters[3].Value = model.InputType;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.CreatedBy;
            parameters[7].Value = model.Id;
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
        /// 分页查询
        /// </summary>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_payment_type_config> ListPage(int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("");
            string Order = "order by Id desc";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_payment_type_config>(ds.Tables[0]);
        }
        /// <summary>
        /// 根据支付类型查询支付参数信息
        /// </summary>
        /// <param name="PaymentTypeId"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_payment_type_config> SelectPaymentTypeId(int PaymentTypeId)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("  select * from jmp_payment_type_config  where Status = 0 and PaymentTypeId=@PaymentTypeId ");
            SqlParameter par = new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4);
            par.Value = PaymentTypeId;
            dt = DbHelperSQL.Query(sql,par).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_payment_type_config>(dt);
        }

        /// <summary>
        /// 查询支付配置信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">分页数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_payment_type_config> SelectList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
        {
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_payment_type_config>(ds.Tables[0]);
        }

        /// <summary>
        /// 根据id查询相关信息
        /// </summary>
        public JMP.MDL.jmp_payment_type_config GetModels(int id)
        {
            string sql = string.Format("select a.*,b.p_name as  paymenttypeName, c.p_id as paymodeId, c.p_name as paymodeName   from  [dbo].jmp_payment_type_config a left join [dbo].[jmp_paymenttype] b  on a.PaymentTypeId=b.p_id left join [dbo].[jmp_paymode] c on b.p_type=c.p_id where 1=1 and a.Id=@l_id ");
            SqlParameter par = new SqlParameter("@l_id", id);

            dt = DbHelperSQL.Query(sql.ToString(), par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_payment_type_config>(dt);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_payment_type_config GetModel(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PaymentTypeId,Label,FieldName,InputType,[Description],[Status]");
            strSql.Append("  from jmp_payment_type_config  ");
            strSql.Append(" where Id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


            JMP.MDL.jmp_payment_type_config model = new JMP.MDL.jmp_payment_type_config();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if(ds.Tables[0].Rows[0]["PaymentTypeId"].ToString() != "")
                {
                     model.PaymentTypeId = int.Parse( ds.Tables[0].Rows[0]["PaymentTypeId"].ToString());
                }
              
                if (ds.Tables[0].Rows[0]["Label"].ToString() != "")
                {
                    model.Label = ds.Tables[0].Rows[0]["Label"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FieldName"].ToString() != "")
                {
                    model.FieldName = ds.Tables[0].Rows[0]["FieldName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InputType"].ToString() != "")
                {
                    model.InputType = ds.Tables[0].Rows[0]["InputType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                   model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                    
                model.Status = int.Parse( ds.Tables[0].Rows[0]["Status"].ToString());
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_payment_type_config set Status=" + state + "  ");
            strSql.Append(" where Id in (" + u_idlist + ")  ");
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
