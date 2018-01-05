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
    public partial class CoAgent
    {
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CoAgent");
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
        public int Add(JMP.MDL.CoAgent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CoAgent(");
            strSql.Append("CreatedById,CreatedByName,OwnerId,OwnerName,LoginCount,State,BankFullName,BankAccountName,BankAccount,Classify,LoginName,IDCardNumber,PersonalPhotoPath,BusinessLicensePhotoPath,BusinessLicenseNumber,ContactAddress,AuditState,RoleId,ServiceFeeRatioGradeId,Password,DisplayName,EmailAddress,MobilePhone,QQ,Website,CreatedOn");
            strSql.Append(") values (");
            strSql.Append("@CreatedById,@CreatedByName,@OwnerId,@OwnerName,@LoginCount,@State,@BankFullName,@BankAccountName,@BankAccount,@Classify,@LoginName,@IDCardNumber,@PersonalPhotoPath,@BusinessLicensePhotoPath,@BusinessLicenseNumber,@ContactAddress,@AuditState,@RoleId,@ServiceFeeRatioGradeId,@Password,@DisplayName,@EmailAddress,@MobilePhone,@QQ,@Website,@CreatedOn");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@OwnerId", SqlDbType.Int,4) ,
                        new SqlParameter("@OwnerName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@LoginCount", SqlDbType.Int,4) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@BankFullName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BankAccountName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BankAccount", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Classify", SqlDbType.Int,4) ,
                        new SqlParameter("@LoginName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IDCardNumber", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@PersonalPhotoPath", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BusinessLicensePhotoPath", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BusinessLicenseNumber", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@ContactAddress", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@AuditState", SqlDbType.Int,4) ,
                        new SqlParameter("@RoleId", SqlDbType.Int,4) ,
                        new SqlParameter("@ServiceFeeRatioGradeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Password", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@DisplayName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@MobilePhone", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@QQ", SqlDbType.NVarChar,15) ,
                        new SqlParameter("@Website", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.CreatedById;
            parameters[1].Value = model.CreatedByName;
            parameters[2].Value = model.OwnerId;
            parameters[3].Value = model.OwnerName;
            parameters[4].Value = model.LoginCount;
            parameters[5].Value = model.State;
            parameters[6].Value = model.BankFullName;
            parameters[7].Value = model.BankAccountName;
            parameters[8].Value = model.BankAccount;
            parameters[9].Value = model.Classify;
            parameters[10].Value = model.LoginName;
            parameters[11].Value = model.IDCardNumber;
            parameters[12].Value = model.PersonalPhotoPath;
            parameters[13].Value = model.BusinessLicensePhotoPath;
            parameters[14].Value = model.BusinessLicenseNumber;
            parameters[15].Value = model.ContactAddress;
            parameters[16].Value = model.AuditState;
            parameters[17].Value = model.RoleId;
            parameters[18].Value = model.ServiceFeeRatioGradeId;
            parameters[19].Value = model.Password;
            parameters[20].Value = model.DisplayName;
            parameters[21].Value = model.EmailAddress;
            parameters[22].Value = model.MobilePhone;
            parameters[23].Value = model.QQ;
            parameters[24].Value = model.Website;
            parameters[25].Value = model.CreatedOn;

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
        public bool Update(JMP.MDL.CoAgent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoAgent set ");

            strSql.Append(" CreatedById = @CreatedById , ");
            strSql.Append(" CreatedByName = @CreatedByName , ");
            strSql.Append(" OwnerId = @OwnerId , ");
            strSql.Append(" OwnerName = @OwnerName , ");
            strSql.Append(" LoginCount = @LoginCount , ");
            strSql.Append(" State = @State , ");
            strSql.Append(" BankFullName = @BankFullName , ");
            strSql.Append(" BankAccountName = @BankAccountName , ");
            strSql.Append(" BankAccount = @BankAccount , ");
            strSql.Append(" Classify = @Classify , ");
            strSql.Append(" LoginName = @LoginName , ");
            strSql.Append(" IDCardNumber = @IDCardNumber , ");
            strSql.Append(" PersonalPhotoPath = @PersonalPhotoPath , ");
            strSql.Append(" BusinessLicensePhotoPath = @BusinessLicensePhotoPath , ");
            strSql.Append(" BusinessLicenseNumber = @BusinessLicenseNumber , ");
            strSql.Append(" ContactAddress = @ContactAddress , ");
            strSql.Append(" AuditState = @AuditState , ");
            strSql.Append(" RoleId = @RoleId , ");
            strSql.Append(" ServiceFeeRatioGradeId = @ServiceFeeRatioGradeId , ");
            strSql.Append(" Password = @Password , ");
            strSql.Append(" DisplayName = @DisplayName , ");
            strSql.Append(" EmailAddress = @EmailAddress , ");
            strSql.Append(" MobilePhone = @MobilePhone , ");
            strSql.Append(" QQ = @QQ , ");
            strSql.Append(" Website = @Website , ");
            strSql.Append(" CreatedOn = @CreatedOn  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@OwnerId", SqlDbType.Int,4) ,
                        new SqlParameter("@OwnerName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@LoginCount", SqlDbType.Int,4) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@BankFullName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BankAccountName", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BankAccount", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Classify", SqlDbType.Int,4) ,
                        new SqlParameter("@LoginName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@IDCardNumber", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@PersonalPhotoPath", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BusinessLicensePhotoPath", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@BusinessLicenseNumber", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@ContactAddress", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@AuditState", SqlDbType.Int,4) ,
                        new SqlParameter("@RoleId", SqlDbType.Int,4) ,
                        new SqlParameter("@ServiceFeeRatioGradeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Password", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@DisplayName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@MobilePhone", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@QQ", SqlDbType.NVarChar,15) ,
                        new SqlParameter("@Website", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.CreatedById;
            parameters[2].Value = model.CreatedByName;
            parameters[3].Value = model.OwnerId;
            parameters[4].Value = model.OwnerName;
            parameters[5].Value = model.LoginCount;
            parameters[6].Value = model.State;
            parameters[7].Value = model.BankFullName;
            parameters[8].Value = model.BankAccountName;
            parameters[9].Value = model.BankAccount;
            parameters[10].Value = model.Classify;
            parameters[11].Value = model.LoginName;
            parameters[12].Value = model.IDCardNumber;
            parameters[13].Value = model.PersonalPhotoPath;
            parameters[14].Value = model.BusinessLicensePhotoPath;
            parameters[15].Value = model.BusinessLicenseNumber;
            parameters[16].Value = model.ContactAddress;
            parameters[17].Value = model.AuditState;
            parameters[18].Value = model.RoleId;
            parameters[19].Value = model.ServiceFeeRatioGradeId;
            parameters[20].Value = model.Password;
            parameters[21].Value = model.DisplayName;
            parameters[22].Value = model.EmailAddress;
            parameters[23].Value = model.MobilePhone;
            parameters[24].Value = model.QQ;
            parameters[25].Value = model.Website;
            parameters[26].Value = model.CreatedOn;
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
            strSql.Append("delete from CoAgent ");
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
            strSql.Append("delete from CoAgent ");
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
        public JMP.MDL.CoAgent GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CreatedById, CreatedByName, OwnerId, OwnerName, LoginCount, State, BankFullName, BankAccountName, BankAccount, Classify, LoginName, IDCardNumber, PersonalPhotoPath, BusinessLicensePhotoPath, BusinessLicenseNumber, ContactAddress, AuditState, RoleId, ServiceFeeRatioGradeId, Password, DisplayName, EmailAddress, MobilePhone, QQ, Website, CreatedOn  ");
            strSql.Append("  from CoAgent ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.CoAgent model = new JMP.MDL.CoAgent();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                if (ds.Tables[0].Rows[0]["OwnerId"].ToString() != "")
                {
                    model.OwnerId = int.Parse(ds.Tables[0].Rows[0]["OwnerId"].ToString());
                }
                model.OwnerName = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                if (ds.Tables[0].Rows[0]["LoginCount"].ToString() != "")
                {
                    model.LoginCount = int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                model.BankFullName = ds.Tables[0].Rows[0]["BankFullName"].ToString();
                model.BankAccountName = ds.Tables[0].Rows[0]["BankAccountName"].ToString();
                model.BankAccount = ds.Tables[0].Rows[0]["BankAccount"].ToString();
                if (ds.Tables[0].Rows[0]["Classify"].ToString() != "")
                {
                    model.Classify = int.Parse(ds.Tables[0].Rows[0]["Classify"].ToString());
                }
                model.LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
                model.IDCardNumber = ds.Tables[0].Rows[0]["IDCardNumber"].ToString();
                model.PersonalPhotoPath = ds.Tables[0].Rows[0]["PersonalPhotoPath"].ToString();
                model.BusinessLicensePhotoPath = ds.Tables[0].Rows[0]["BusinessLicensePhotoPath"].ToString();
                model.BusinessLicenseNumber = ds.Tables[0].Rows[0]["BusinessLicenseNumber"].ToString();
                model.ContactAddress = ds.Tables[0].Rows[0]["ContactAddress"].ToString();
                if (ds.Tables[0].Rows[0]["AuditState"].ToString() != "")
                {
                    model.AuditState = int.Parse(ds.Tables[0].Rows[0]["AuditState"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFeeRatioGradeId"].ToString() != "")
                {
                    model.ServiceFeeRatioGradeId = int.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatioGradeId"].ToString());
                }
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                model.EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.Website = ds.Tables[0].Rows[0]["Website"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.CoAgent GetModel(string  username)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, CreatedById, CreatedByName, OwnerId, OwnerName, LoginCount, State, BankFullName, BankAccountName, BankAccount, Classify, LoginName, IDCardNumber, PersonalPhotoPath, BusinessLicensePhotoPath, BusinessLicenseNumber, ContactAddress, AuditState, RoleId, ServiceFeeRatioGradeId, Password, DisplayName, EmailAddress, MobilePhone, QQ, Website, CreatedOn  ");
            strSql.Append("  from CoAgent ");
            strSql.Append(" where LoginName=@LoginName");
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginName", SqlDbType.NVarChar,-1)
            };
            parameters[0].Value = username;


            JMP.MDL.CoAgent model = new JMP.MDL.CoAgent();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedById"].ToString() != "")
                {
                    model.CreatedById = int.Parse(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                }
                model.CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"].ToString();
                if (ds.Tables[0].Rows[0]["OwnerId"].ToString() != "")
                {
                    model.OwnerId = int.Parse(ds.Tables[0].Rows[0]["OwnerId"].ToString());
                }
                model.OwnerName = ds.Tables[0].Rows[0]["OwnerName"].ToString();
                if (ds.Tables[0].Rows[0]["LoginCount"].ToString() != "")
                {
                    model.LoginCount = int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                model.BankFullName = ds.Tables[0].Rows[0]["BankFullName"].ToString();
                model.BankAccountName = ds.Tables[0].Rows[0]["BankAccountName"].ToString();
                model.BankAccount = ds.Tables[0].Rows[0]["BankAccount"].ToString();
                if (ds.Tables[0].Rows[0]["Classify"].ToString() != "")
                {
                    model.Classify = int.Parse(ds.Tables[0].Rows[0]["Classify"].ToString());
                }
                model.LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
                model.IDCardNumber = ds.Tables[0].Rows[0]["IDCardNumber"].ToString();
                model.PersonalPhotoPath = ds.Tables[0].Rows[0]["PersonalPhotoPath"].ToString();
                model.BusinessLicensePhotoPath = ds.Tables[0].Rows[0]["BusinessLicensePhotoPath"].ToString();
                model.BusinessLicenseNumber = ds.Tables[0].Rows[0]["BusinessLicenseNumber"].ToString();
                model.ContactAddress = ds.Tables[0].Rows[0]["ContactAddress"].ToString();
                if (ds.Tables[0].Rows[0]["AuditState"].ToString() != "")
                {
                    model.AuditState = int.Parse(ds.Tables[0].Rows[0]["AuditState"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleId"].ToString() != "")
                {
                    model.RoleId = int.Parse(ds.Tables[0].Rows[0]["RoleId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFeeRatioGradeId"].ToString() != "")
                {
                    model.ServiceFeeRatioGradeId = int.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatioGradeId"].ToString());
                }
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                model.EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.Website = ds.Tables[0].Rows[0]["Website"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
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
            strSql.Append(" FROM CoAgent ");
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
            strSql.Append(" FROM CoAgent ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 查询代理商管理
        /// </summary>
        /// <param name="s_type">查询类型</param>
        /// <param name="s_keys">查询内容</param>
        /// <param name="status">账号状态</param>
        /// <param name="AuditState">审核状态</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.CoAgent> SelectList(int s_type,  string s_keys, string status, string AuditState,int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select Id,LoginName,Password,DisplayName,EmailAddress,MobilePhone,QQ,Website,CreatedOn,CreatedByName,OwnerName,[State], BankFullName,BankAccountName,BankAccount, Classify ,IDCardNumber,BusinessLicenseNumber, ContactAddress, AuditState from CoAgent  where 1=1");
            string Order = " Order by  id desc";
            if (s_type > 0 && !string.IsNullOrEmpty(s_keys))
            {
                switch (s_type)
                {
                    case 1:
                        sql += "  and LoginName like '%" + s_keys + "%' ";
                        break;
                    case 2:
                        sql += " and DisplayName like '%" + s_keys + "%' ";
                        break;
                    case 3:
                        sql += " and MobilePhone like  '%" + s_keys + "%' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(status))
            {
                sql += " and [State]='"+ status + "'";
            }

            if (!string.IsNullOrEmpty(AuditState))
            {
                sql += " and AuditState='" + AuditState + "' ";
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
            return DbHelperSQL.ToList<JMP.MDL.CoAgent>(ds.Tables[0]);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(int id, int state)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" update CoAgent set [AuditState]=" + state + " ");
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


        /// <summary>
        /// 批量更新代理商状态
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateAgentState(string coid, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CoAgent set [State]=" + state + " where Id in(" + coid + ")");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        //////////////////////////////////////////////////验证方法//////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 是否存在该登录账号
        /// </summary>
        /// <param name="lname">登录账号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsLogName(string lname, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from CoAgent where LoginName='{0}'", lname);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and Id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该邮箱账号
        /// </summary>
        /// <param name="umail">邮箱账号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsEmail(string umail, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from CoAgent where EmailAddress='{0}'", umail);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and Id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该身份证号
        /// </summary>
        /// <param name="idno">身份证号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsIdno(string idno, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from CoAgent where IDCardNumber='{0}'", idno);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and Id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该营业执照
        /// </summary>
        /// <param name="yyzz">营业执照</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsYyzz(string yyzz, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from CoAgent where BusinessLicenseNumber='{0}'", yyzz);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and Id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该银行卡号
        /// </summary>
        /// <param name="yyzz">银行卡号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public bool ExistsBankNo(string yyzz, string uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from CoAgent where BankAccount='{0}'", yyzz);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and Id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }
    }
}
