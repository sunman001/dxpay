using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //代付信息PayForAnotherInfo
    public partial class PayForAnotherInfo
    {
        public bool Exists(int p_Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PayForAnotherInfo");
            strSql.Append(" where ");
            strSql.Append(" p_Id = @p_Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_Id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.PayForAnotherInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PayForAnotherInfo(");
            strSql.Append("p_auditortime,p_append,p_appendtime,p_InterfaceName,p_InterfaceType,p_MerchantNumber,p_KeyType,p_PrivateKey,p_PublicKey,IsEnabled,p_auditor");
            strSql.Append(") values (");
            strSql.Append("@p_auditortime,@p_append,@p_appendtime,@p_InterfaceName,@p_InterfaceType,@p_MerchantNumber,@p_KeyType,@p_PrivateKey,@p_PublicKey,@IsEnabled,@p_auditor");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@p_auditortime", SqlDbType.DateTime) ,
                        new SqlParameter("@p_append", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_appendtime", SqlDbType.DateTime) ,
                        new SqlParameter("@p_InterfaceName", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_InterfaceType", SqlDbType.Int,4) ,
                        new SqlParameter("@p_MerchantNumber", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_KeyType", SqlDbType.Int,4) ,
                        new SqlParameter("@p_PrivateKey", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@p_PublicKey", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@IsEnabled", SqlDbType.Bit,1) ,
                        new SqlParameter("@p_auditor", SqlDbType.NVarChar,200)

            };

            parameters[0].Value = model.p_auditortime;
            parameters[1].Value = model.p_append;
            parameters[2].Value = model.p_appendtime;
            parameters[3].Value = model.p_InterfaceName;
            parameters[4].Value = model.p_InterfaceType;
            parameters[5].Value = model.p_MerchantNumber;
            parameters[6].Value = model.p_KeyType;
            parameters[7].Value = model.p_PrivateKey;
            parameters[8].Value = model.p_PublicKey;
            parameters[9].Value = model.IsEnabled;
            parameters[10].Value = model.p_auditor;

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
        public bool Update(JMP.MDL.PayForAnotherInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PayForAnotherInfo set ");

            strSql.Append(" p_auditortime = @p_auditortime , ");
            strSql.Append(" p_append = @p_append , ");
            strSql.Append(" p_appendtime = @p_appendtime , ");
            strSql.Append(" p_InterfaceName = @p_InterfaceName , ");
            strSql.Append(" p_InterfaceType = @p_InterfaceType , ");
            strSql.Append(" p_MerchantNumber = @p_MerchantNumber , ");
            strSql.Append(" p_KeyType = @p_KeyType , ");
            strSql.Append(" p_PrivateKey = @p_PrivateKey , ");
            strSql.Append(" p_PublicKey = @p_PublicKey , ");
            strSql.Append(" IsEnabled = @IsEnabled , ");
            strSql.Append(" p_auditor = @p_auditor  ");
            strSql.Append(" where p_Id=@p_Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@p_Id", SqlDbType.Int,4) ,
                        new SqlParameter("@p_auditortime", SqlDbType.DateTime) ,
                        new SqlParameter("@p_append", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_appendtime", SqlDbType.DateTime) ,
                        new SqlParameter("@p_InterfaceName", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_InterfaceType", SqlDbType.Int,4) ,
                        new SqlParameter("@p_MerchantNumber", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@p_KeyType", SqlDbType.Int,4) ,
                        new SqlParameter("@p_PrivateKey", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@p_PublicKey", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@IsEnabled", SqlDbType.Bit,1) ,
                        new SqlParameter("@p_auditor", SqlDbType.NVarChar,200)

            };

            parameters[0].Value = model.p_Id;
            parameters[1].Value = model.p_auditortime;
            parameters[2].Value = model.p_append;
            parameters[3].Value = model.p_appendtime;
            parameters[4].Value = model.p_InterfaceName;
            parameters[5].Value = model.p_InterfaceType;
            parameters[6].Value = model.p_MerchantNumber;
            parameters[7].Value = model.p_KeyType;
            parameters[8].Value = model.p_PrivateKey;
            parameters[9].Value = model.p_PublicKey;
            parameters[10].Value = model.IsEnabled;
            parameters[11].Value = model.p_auditor;
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
        public bool Delete(int p_Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PayForAnotherInfo ");
            strSql.Append(" where p_Id=@p_Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_Id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_Id;


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
        public bool DeleteList(string p_Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PayForAnotherInfo ");
            strSql.Append(" where ID in (" + p_Idlist + ")  ");
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
        public JMP.MDL.PayForAnotherInfo GetModel(int p_Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_Id, p_auditortime, p_append, p_appendtime, p_InterfaceName, p_InterfaceType, p_MerchantNumber, p_KeyType, p_PrivateKey, p_PublicKey, IsEnabled, p_auditor  ");
            strSql.Append("  from PayForAnotherInfo ");
            strSql.Append(" where p_Id=@p_Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_Id", SqlDbType.Int,4)
            };
            parameters[0].Value = p_Id;


            JMP.MDL.PayForAnotherInfo model = new JMP.MDL.PayForAnotherInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["p_Id"].ToString() != "")
                {
                    model.p_Id = int.Parse(ds.Tables[0].Rows[0]["p_Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p_auditortime"].ToString() != "")
                {
                    model.p_auditortime = DateTime.Parse(ds.Tables[0].Rows[0]["p_auditortime"].ToString());
                }
                model.p_append = ds.Tables[0].Rows[0]["p_append"].ToString();
                if (ds.Tables[0].Rows[0]["p_appendtime"].ToString() != "")
                {
                    model.p_appendtime = DateTime.Parse(ds.Tables[0].Rows[0]["p_appendtime"].ToString());
                }
                model.p_InterfaceName = ds.Tables[0].Rows[0]["p_InterfaceName"].ToString();
                if (ds.Tables[0].Rows[0]["p_InterfaceType"].ToString() != "")
                {
                    model.p_InterfaceType = int.Parse(ds.Tables[0].Rows[0]["p_InterfaceType"].ToString());
                }
                model.p_MerchantNumber = ds.Tables[0].Rows[0]["p_MerchantNumber"].ToString();
                if (ds.Tables[0].Rows[0]["p_KeyType"].ToString() != "")
                {
                    model.p_KeyType = int.Parse(ds.Tables[0].Rows[0]["p_KeyType"].ToString());
                }
                model.p_PrivateKey = ds.Tables[0].Rows[0]["p_PrivateKey"].ToString();
                model.p_PublicKey = ds.Tables[0].Rows[0]["p_PublicKey"].ToString();
                if (ds.Tables[0].Rows[0]["IsEnabled"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsEnabled"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsEnabled"].ToString().ToLower() == "true"))
                    {
                        model.IsEnabled = true;
                    }
                    else
                    {
                        model.IsEnabled = false;
                    }
                }
                model.p_auditor = ds.Tables[0].Rows[0]["p_auditor"].ToString();

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
            strSql.Append(" FROM PayForAnotherInfo ");
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
            strSql.Append(" FROM PayForAnotherInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 查询代付通道列
        /// </summary>
        /// <param name="PayType">查询条件类型</param>
        /// <param name="searchKey">查询值</param>
        /// <param name="IsEnabled">启用禁用状态</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.PayForAnotherInfo> PayForAnotherInfoList(int PayType, string searchKey, int IsEnabled, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from PayForAnotherInfo a inner join PayChannel b on a.p_InterfaceType=b.Id where 1=1");

            if (PayType > 0 && !string.IsNullOrEmpty(searchKey))
            {
                sql += " and p_InterfaceName like '%" + searchKey + "%'";
            }
            if (IsEnabled != -1)
            {
                sql += " and IsEnabled=" + IsEnabled;
            }

            string Order = " order by p_Id desc ";

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
            return DbHelperSQL.ToList<JMP.MDL.PayForAnotherInfo>(ds.Tables[0]);
        }


        /// <summary>
        /// 查询代付通道列用于财务选择
        /// </summary>
        /// <param name="PayType">查询条件类型</param>
        /// <param name="searchKey">查询值</param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MDL.PayForAnotherInfo> PayForAnotherIsEnabledList(int PayType, string searchKey, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select * from PayForAnotherInfo a inner join PayChannel b on a.p_InterfaceType=b.Id where IsEnabled=1");

            if (PayType > 0 && !string.IsNullOrEmpty(searchKey))
            {
                sql += " and p_InterfaceName like '%" + searchKey + "%'";
            }

            string Order = " order by p_Id desc ";

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
            return DbHelperSQL.ToList<JMP.MDL.PayForAnotherInfo>(ds.Tables[0]);
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdatePayForAnotherState(int id, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update PayForAnotherInfo set IsEnabled=" + state + "  ");
            strSql.Append(" where p_Id= " + id);
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
        DataTable dt = new DataTable();

        /// <summary>
        /// 通过代付接口ID查询信息
        /// </summary>
        /// <param name="Id">代付接口ID</param>
        /// <returns></returns>
        public JMP.MDL.PayForAnotherInfo GetPayInfoId(int Id)
        {
            string sql = string.Format(" SELECT a.*,b.ChannelIdentifier FROM PayForAnotherInfo a left join  PayChannel b on a.p_InterfaceType=b.Id where p_Id=@id ");
            SqlParameter par = new SqlParameter("@id", Id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.PayForAnotherInfo>(dt);
        }

    }
}

