using JMP.DBA;
using JMP.Model.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JMP.DAL
{
    //用户表
    public partial class jmp_user
    {
        public bool Exists(int u_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_user");
            strSql.Append(" where ");
            strSql.Append(" u_id = @u_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
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
            strSql.AppendFormat("select count(1) from jmp_user where u_email='{0}'", umail);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and u_id<>{0}", uid);

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
            strSql.AppendFormat("select count(1) from jmp_user where u_idnumber='{0}'", idno);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and u_id<>{0}", uid);

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
            strSql.AppendFormat("select count(1) from jmp_user where u_blicensenumber='{0}'", yyzz);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and u_id<>{0}", uid);

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
            strSql.AppendFormat("select count(1) from jmp_user where u_account='{0}'", yyzz);

            if (!string.IsNullOrEmpty(uid))
                strSql.AppendFormat(" and u_id<>{0}", uid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该身份证号
        /// </summary>
        /// <param name="idno">身份证号</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public bool ExistsIdnos(string idno, string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from jmp_user where u_idnumber='{0}' and u_email<>'{1}'", idno, email);
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该营业执照
        /// </summary>
        /// <param name="yyzz">营业执照</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public bool ExistsYyzzs(string yyzz, string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from jmp_user where u_blicensenumber='{0}' and u_email<>'{1}'", yyzz, email);
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该银行卡号
        /// </summary>
        /// <param name="yyzz">银行卡号</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public bool ExistsBankNos(string yyzz, string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from jmp_user where u_account='{0}' and u_email<>'{1}'", yyzz, email);
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_user(");
            strSql.Append("u_category,u_idnumber,u_photo,u_blicense,u_blicensenumber,u_count,u_state,u_auditstate,u_topid,u_address,u_email,u_role_id,u_drawing,u_merchant_id,relation_type,relation_person_id,ServiceFeeRatioGradeId,IsSpecialApproval,SpecialApproval,BusinessEntity,RegisteredAddress,u_password,u_time,u_auditor,u_realname,u_phone,u_qq,u_bankname,u_name,u_account,u_photof,u_licence");
            strSql.Append(") values (");
            strSql.Append("@u_category,@u_idnumber,@u_photo,@u_blicense,@u_blicensenumber,@u_count,@u_state,@u_auditstate,@u_topid,@u_address,@u_email,@u_role_id,@u_drawing,@u_merchant_id,@relation_type,@relation_person_id,@ServiceFeeRatioGradeId,@IsSpecialApproval,@SpecialApproval,@BusinessEntity,@RegisteredAddress,@u_password,@u_time,@u_auditor,@u_realname,@u_phone,@u_qq,@u_bankname,@u_name,@u_account,@u_photof,@u_licence");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@u_category", SqlDbType.Int,4) ,
                        new SqlParameter("@u_idnumber", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_photo", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_blicense", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_blicensenumber", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_count", SqlDbType.Int,4) ,
                        new SqlParameter("@u_state", SqlDbType.Int,4) ,
                        new SqlParameter("@u_auditstate", SqlDbType.Int,4) ,
                        new SqlParameter("@u_topid", SqlDbType.Int,4) ,
                        new SqlParameter("@u_address", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_email", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_role_id", SqlDbType.Int,4) ,
                        new SqlParameter("@u_drawing", SqlDbType.Int,4) ,
                        new SqlParameter("@u_merchant_id", SqlDbType.Int,4) ,
                        new SqlParameter("@relation_type", SqlDbType.Int,4) ,
                        new SqlParameter("@relation_person_id", SqlDbType.Int,4) ,
                        new SqlParameter("@ServiceFeeRatioGradeId", SqlDbType.Int,4) ,
                        new SqlParameter("@IsSpecialApproval", SqlDbType.Bit,1) ,
                        new SqlParameter("@SpecialApproval", SqlDbType.Decimal,5) ,
                        new SqlParameter("@BusinessEntity", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@RegisteredAddress", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@u_password", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_time", SqlDbType.DateTime) ,
                        new SqlParameter("@u_auditor", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@u_realname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_phone", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_qq", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_bankname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_account", SqlDbType.NVarChar,-1),
                        new SqlParameter("@u_photof", SqlDbType.NVarChar,-1),
                        new SqlParameter("@u_licence", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.u_category;
            parameters[1].Value = model.u_idnumber;
            parameters[2].Value = model.u_photo;
            parameters[3].Value = model.u_blicense;
            parameters[4].Value = model.u_blicensenumber;
            parameters[5].Value = model.u_count;
            parameters[6].Value = model.u_state;
            parameters[7].Value = model.u_auditstate;
            parameters[8].Value = model.u_topid;
            parameters[9].Value = model.u_address;
            parameters[10].Value = model.u_email;
            parameters[11].Value = model.u_role_id;
            parameters[12].Value = model.u_drawing;
            parameters[13].Value = model.u_merchant_id;
            parameters[14].Value = model.relation_type;
            parameters[15].Value = model.relation_person_id;
            parameters[16].Value = model.ServiceFeeRatioGradeId;
            parameters[17].Value = model.IsSpecialApproval;
            parameters[18].Value = model.SpecialApproval;
            parameters[19].Value = model.BusinessEntity;
            parameters[20].Value = model.RegisteredAddress;
            parameters[21].Value = model.u_password;
            parameters[22].Value = model.u_time;
            parameters[23].Value = model.u_auditor;
            parameters[24].Value = model.u_realname;
            parameters[25].Value = model.u_phone;
            parameters[26].Value = model.u_qq;
            parameters[27].Value = model.u_bankname;
            parameters[28].Value = model.u_name;
            parameters[29].Value = model.u_account;
            parameters[30].Value = model.u_photof;
            parameters[31].Value = model.u_licence;
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
        public bool Update(JMP.MDL.jmp_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user set ");

            strSql.Append(" u_category = @u_category , ");
            strSql.Append(" u_idnumber = @u_idnumber , ");
            strSql.Append(" u_photo = @u_photo , ");
            strSql.Append(" u_blicense = @u_blicense , ");
            strSql.Append(" u_blicensenumber = @u_blicensenumber , ");
            strSql.Append(" u_count = @u_count , ");
            strSql.Append(" u_state = @u_state , ");
            strSql.Append(" u_auditstate = @u_auditstate , ");
            strSql.Append(" u_topid = @u_topid , ");
            strSql.Append(" u_address = @u_address , ");
            strSql.Append(" u_email = @u_email , ");
            strSql.Append(" u_role_id = @u_role_id , ");
            strSql.Append(" u_drawing = @u_drawing , ");
            strSql.Append(" u_merchant_id = @u_merchant_id , ");
            strSql.Append(" relation_type = @relation_type , ");
            strSql.Append(" relation_person_id = @relation_person_id , ");
            strSql.Append(" ServiceFeeRatioGradeId = @ServiceFeeRatioGradeId , ");
            strSql.Append(" IsSpecialApproval = @IsSpecialApproval , ");
            strSql.Append(" BusinessEntity = @BusinessEntity , ");
            strSql.Append(" u_password = @u_password , ");
            strSql.Append(" RegisteredAddress = @RegisteredAddress , ");
            strSql.Append(" u_realname = @u_realname , ");
            strSql.Append(" u_phone = @u_phone , ");
            strSql.Append(" u_qq = @u_qq , ");
            strSql.Append(" u_bankname = @u_bankname ,");
            strSql.Append(" u_name = @u_name , ");
            strSql.Append(" u_account = @u_account,");
            strSql.Append(" u_photof = @u_photof,");
            strSql.Append(" u_licence = @u_licence");
            strSql.Append(" where u_id=@u_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@u_id", SqlDbType.Int,4) ,
                        new SqlParameter("@u_category", SqlDbType.Int,4) ,
                        new SqlParameter("@u_idnumber", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_photo", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_blicense", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_blicensenumber", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_count", SqlDbType.Int,4) ,
                        new SqlParameter("@u_state", SqlDbType.Int,4) ,
                        new SqlParameter("@u_auditstate", SqlDbType.Int,4) ,
                        new SqlParameter("@u_topid", SqlDbType.Int,4) ,
                        new SqlParameter("@u_address", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_email", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_role_id", SqlDbType.Int,4) ,
                        new SqlParameter("@u_drawing", SqlDbType.Int,4) ,
                        new SqlParameter("@u_merchant_id", SqlDbType.Int,4) ,
                        new SqlParameter("@relation_type", SqlDbType.Int,4) ,
                        new SqlParameter("@relation_person_id", SqlDbType.Int,4) ,
                        new SqlParameter("@ServiceFeeRatioGradeId", SqlDbType.Int,4) ,
                        new SqlParameter("@IsSpecialApproval", SqlDbType.Bit,1) ,
                        new SqlParameter("@BusinessEntity", SqlDbType.VarChar,50) ,
                        new SqlParameter("@u_password", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@RegisteredAddress", SqlDbType.VarChar,50) ,
                        new SqlParameter("@u_realname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_phone", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_qq", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_bankname", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_name", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@u_account", SqlDbType.NVarChar,-1),
                         new SqlParameter("@u_photof", SqlDbType.NVarChar,-1),
                        new SqlParameter("@u_licence", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.u_id;
            parameters[1].Value = model.u_category;
            parameters[2].Value = model.u_idnumber;
            parameters[3].Value = model.u_photo;
            parameters[4].Value = model.u_blicense;
            parameters[5].Value = model.u_blicensenumber;
            parameters[6].Value = model.u_count;
            parameters[7].Value = model.u_state;
            parameters[8].Value = model.u_auditstate;
            parameters[9].Value = model.u_topid;
            parameters[10].Value = model.u_address;
            parameters[11].Value = model.u_email;
            parameters[12].Value = model.u_role_id;
            parameters[13].Value = model.u_drawing;
            parameters[14].Value = model.u_merchant_id;
            parameters[15].Value = model.relation_type;
            parameters[16].Value = model.relation_person_id;
            parameters[17].Value = model.ServiceFeeRatioGradeId;
            parameters[18].Value = model.IsSpecialApproval;
            parameters[19].Value = model.BusinessEntity;
            parameters[20].Value = model.u_password;
            parameters[21].Value = model.RegisteredAddress;
            parameters[22].Value = model.u_realname;
            parameters[23].Value = model.u_phone;
            parameters[24].Value = model.u_qq;
            parameters[25].Value = model.u_bankname;
            parameters[26].Value = model.u_name;
            parameters[27].Value = model.u_account;
            parameters[28].Value = model.u_photof;
            parameters[29].Value = model.u_licence;
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateByEmail(JMP.MDL.jmp_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user set ");
            strSql.Append("u_idnumber=@u_idnumber,");
            strSql.Append("u_photo=@u_photo,");
            strSql.Append("u_blicense=@u_blicense,");
            strSql.Append("u_blicensenumber=@u_blicensenumber,");
            strSql.Append("u_address=@u_address,");
            strSql.Append("u_realname=@u_realname,");
            strSql.Append("u_qq=@u_qq,");
            strSql.Append("u_bankname=@u_bankname,");
            strSql.Append("u_name=@u_name,");
            strSql.Append("u_account=@u_account,");
            strSql.Append("BusinessEntity=@BusinessEntity,");
            strSql.Append("RegisteredAddress=@RegisteredAddress,");
            strSql.Append("u_photof=@u_photof,");
            strSql.Append("u_licence=@u_licence");
            strSql.Append(" where u_email=@u_email ");



            SqlParameter[] parameters = {
                new SqlParameter("@u_idnumber", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_photo", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_blicense", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_blicensenumber", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_address", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_realname", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_qq", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_bankname", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_name", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_account", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_email", SqlDbType.NVarChar,-1),
                new SqlParameter("@BusinessEntity",SqlDbType.NVarChar,-1),
                new SqlParameter("@RegisteredAddress",SqlDbType.NVarChar,-1),
                new SqlParameter("@u_photof",SqlDbType.NVarChar,-1),
                new SqlParameter("@u_licence",SqlDbType.NVarChar,-1)
            };


            parameters[0].Value = model.u_idnumber;
            parameters[1].Value = model.u_photo;
            parameters[2].Value = model.u_blicense;
            parameters[3].Value = model.u_blicensenumber;
            parameters[4].Value = model.u_address;
            parameters[5].Value = model.u_realname;
            parameters[6].Value = model.u_qq;
            parameters[7].Value = model.u_bankname;
            parameters[8].Value = model.u_name;
            parameters[9].Value = model.u_account;
            parameters[10].Value = model.u_email;
            parameters[11].Value = model.BusinessEntity;
            parameters[12].Value = model.RegisteredAddress;
            parameters[13].Value = model.u_photof;
            parameters[14].Value = model.u_licence;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateInfo(JMP.MDL.jmp_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user set ");
            strSql.Append("u_phone=@u_phone,");
            strSql.Append("u_qq=@u_qq,");
            strSql.Append("u_address=@u_address,");
            strSql.Append("u_password=@u_password ");
            strSql.Append("where u_email=@u_email");

            SqlParameter[] parameters = {
                new SqlParameter("@u_phone", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_qq", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_address", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_password", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_email", SqlDbType.NVarChar,-1)
            };

            parameters[0].Value = model.u_phone;
            parameters[1].Value = model.u_qq;
            parameters[2].Value = model.u_address;
            parameters[3].Value = model.u_password;
            parameters[4].Value = model.u_email;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="uname">登录邮箱</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        public bool UpdatePwd(string uname, string pass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user set ");
            strSql.Append("u_password=@u_password ");
            strSql.Append("where u_email=@u_email");

            SqlParameter[] parameters = {
                new SqlParameter("@u_password", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_email", SqlDbType.NVarChar,-1)
            };

            parameters[0].Value = pass;
            parameters[1].Value = uname;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 通过手机号码更改用户密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool UpdatePwdByPhone(string phone, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user set ");
            strSql.Append("u_password=@u_password ");
            strSql.Append("where u_phone=@u_phone");

            SqlParameter[] parameters = {
                new SqlParameter("@u_password", SqlDbType.NVarChar,-1),
                new SqlParameter("@u_phone", SqlDbType.NVarChar,-1)
            };

            parameters[0].Value = pwd;
            parameters[1].Value = phone;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 修改开发者费率
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="ServiceFeeRatioGradeId">费率表ID</param>
        /// <returns></returns>
        public bool UpdateServiceFeeRatioGradeId(int id, int ServiceFeeRatioGradeId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update jmp_user set ");
            sql.Append("ServiceFeeRatioGradeId='" + ServiceFeeRatioGradeId + "' ");
            sql.Append("where u_id='" + id + "'");

            return DbHelperSQL.ExecuteSql(sql.ToString()) > 0;
        }


        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="ids">用户id列表</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public bool UpdateState(string ids, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_USER set u_state=" + state + " where u_id in(" + ids + ")");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int u_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_user ");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string u_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_user ");
            strSql.Append(" where ID in (" + u_idlist + ")  ");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_user GetModel(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id, u_category, u_idnumber, u_photo, u_blicense, u_blicensenumber, u_count, u_state, u_auditstate, u_topid, u_address, u_email, u_role_id, u_drawing, u_merchant_id, relation_type, relation_person_id, ServiceFeeRatioGradeId, IsSpecialApproval, SpecialApproval, BusinessEntity, RegisteredAddress, u_password, u_time, u_auditor, u_realname, u_phone, u_qq, u_bankname, u_name, u_account,u_paypwd,u_photof,u_licence,FrozenMoney,IsSignContract,IsRecord ");
            strSql.Append("  from jmp_user ");
            strSql.Append(" where u_id=@u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;


            JMP.MDL.jmp_user model = new JMP.MDL.jmp_user();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_category"].ToString() != "")
                {
                    model.u_category = int.Parse(ds.Tables[0].Rows[0]["u_category"].ToString());
                }
                model.u_idnumber = ds.Tables[0].Rows[0]["u_idnumber"].ToString();
                model.u_photo = ds.Tables[0].Rows[0]["u_photo"].ToString();
                model.u_blicense = ds.Tables[0].Rows[0]["u_blicense"].ToString();
                model.u_blicensenumber = ds.Tables[0].Rows[0]["u_blicensenumber"].ToString();
                if (ds.Tables[0].Rows[0]["u_count"].ToString() != "")
                {
                    model.u_count = int.Parse(ds.Tables[0].Rows[0]["u_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_auditstate"].ToString() != "")
                {
                    model.u_auditstate = int.Parse(ds.Tables[0].Rows[0]["u_auditstate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_topid"].ToString() != "")
                {
                    model.u_topid = int.Parse(ds.Tables[0].Rows[0]["u_topid"].ToString());
                }
                model.u_address = ds.Tables[0].Rows[0]["u_address"].ToString();
                model.u_email = ds.Tables[0].Rows[0]["u_email"].ToString();
                if (ds.Tables[0].Rows[0]["u_role_id"].ToString() != "")
                {
                    model.u_role_id = int.Parse(ds.Tables[0].Rows[0]["u_role_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_drawing"].ToString() != "")
                {
                    model.u_drawing = int.Parse(ds.Tables[0].Rows[0]["u_drawing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_merchant_id"].ToString() != "")
                {
                    model.u_merchant_id = int.Parse(ds.Tables[0].Rows[0]["u_merchant_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["relation_type"].ToString() != "")
                {
                    model.relation_type = int.Parse(ds.Tables[0].Rows[0]["relation_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["relation_person_id"].ToString() != "")
                {
                    model.relation_person_id = int.Parse(ds.Tables[0].Rows[0]["relation_person_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ServiceFeeRatioGradeId"].ToString() != "")
                {
                    model.ServiceFeeRatioGradeId = int.Parse(ds.Tables[0].Rows[0]["ServiceFeeRatioGradeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSpecialApproval"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsSpecialApproval"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsSpecialApproval"].ToString().ToLower() == "true"))
                    {
                        model.IsSpecialApproval = true;
                    }
                    else
                    {
                        model.IsSpecialApproval = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["IsSignContract"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsSignContract"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsSignContract"].ToString().ToLower() == "true"))
                    {
                        model.IsSignContract = true;
                    }
                    else
                    {
                        model.IsSignContract = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["IsRecord"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsRecord"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecord"].ToString().ToLower() == "true"))
                    {
                        model.IsRecord = true;
                    }
                    else
                    {
                        model.IsRecord = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["SpecialApproval"].ToString() != "")
                {
                    model.SpecialApproval = decimal.Parse(ds.Tables[0].Rows[0]["SpecialApproval"].ToString());
                }
                model.BusinessEntity = ds.Tables[0].Rows[0]["BusinessEntity"].ToString();
                model.RegisteredAddress = ds.Tables[0].Rows[0]["RegisteredAddress"].ToString();
                model.u_password = ds.Tables[0].Rows[0]["u_password"].ToString();
                if (ds.Tables[0].Rows[0]["u_time"].ToString() != "")
                {
                    model.u_time = DateTime.Parse(ds.Tables[0].Rows[0]["u_time"].ToString());
                }
                model.u_auditor = ds.Tables[0].Rows[0]["u_auditor"].ToString();
                model.u_realname = ds.Tables[0].Rows[0]["u_realname"].ToString();
                model.u_phone = ds.Tables[0].Rows[0]["u_phone"].ToString();
                model.u_qq = ds.Tables[0].Rows[0]["u_qq"].ToString();
                model.u_bankname = ds.Tables[0].Rows[0]["u_bankname"].ToString();
                model.u_name = ds.Tables[0].Rows[0]["u_name"].ToString();
                model.u_account = ds.Tables[0].Rows[0]["u_account"].ToString();
                model.u_paypwd = ds.Tables[0].Rows[0]["u_paypwd"].ToString();
                model.u_photof = ds.Tables[0].Rows[0]["u_photof"].ToString();
                model.u_licence= ds.Tables[0].Rows[0]["u_licence"].ToString();
                if (ds.Tables[0].Rows[0]["FrozenMoney"].ToString() != "")
                {
                    model.FrozenMoney = decimal.Parse(ds.Tables[0].Rows[0]["FrozenMoney"].ToString());
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
        public JMP.MDL.jmp_user GetModel(string mail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id,u_category,u_idnumber,u_photo,u_blicense,u_blicensenumber,u_count,u_state,u_auditstate,u_topid,u_address,u_email,u_role_id,u_password,u_realname,u_phone,u_qq,u_bankname,u_name,u_account,u_merchant_id,u_drawing,u_time,u_auditor,u_paypwd,u_photof,u_licence ");
            strSql.Append(" from jmp_user");
            strSql.Append(" where u_email=@u_email or u_phone=@u_email");
            SqlParameter[] parameters = {
                new SqlParameter("@u_email", SqlDbType.NVarChar,-1)
            };
            parameters[0].Value = mail;

            JMP.MDL.jmp_user model = new JMP.MDL.jmp_user();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_category"].ToString() != "")
                {
                    model.u_category = int.Parse(ds.Tables[0].Rows[0]["u_category"].ToString());
                }
                model.u_idnumber = ds.Tables[0].Rows[0]["u_idnumber"].ToString();
                model.u_photo = ds.Tables[0].Rows[0]["u_photo"].ToString();
                model.u_blicense = ds.Tables[0].Rows[0]["u_blicense"].ToString();
                model.u_blicensenumber = ds.Tables[0].Rows[0]["u_blicensenumber"].ToString();
                if (ds.Tables[0].Rows[0]["u_count"].ToString() != "")
                {
                    model.u_count = int.Parse(ds.Tables[0].Rows[0]["u_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_auditstate"].ToString() != "")
                {
                    model.u_auditstate = int.Parse(ds.Tables[0].Rows[0]["u_auditstate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_topid"].ToString() != "")
                {
                    model.u_topid = int.Parse(ds.Tables[0].Rows[0]["u_topid"].ToString());
                }
                model.u_address = ds.Tables[0].Rows[0]["u_address"].ToString();
                model.u_email = ds.Tables[0].Rows[0]["u_email"].ToString();
                //if (ds.Tables[0].Rows[0]["u_poundage"].ToString() != "")
                //{
                //    model.u_poundage = decimal.Parse(ds.Tables[0].Rows[0]["u_poundage"].ToString());
                //}
                if (ds.Tables[0].Rows[0]["u_role_id"].ToString() != "")
                {
                    model.u_role_id = int.Parse(ds.Tables[0].Rows[0]["u_role_id"].ToString());
                }
                model.u_password = ds.Tables[0].Rows[0]["u_password"].ToString();
                model.u_realname = ds.Tables[0].Rows[0]["u_realname"].ToString();
                model.u_phone = ds.Tables[0].Rows[0]["u_phone"].ToString();
                model.u_qq = ds.Tables[0].Rows[0]["u_qq"].ToString();
                model.u_bankname = ds.Tables[0].Rows[0]["u_bankname"].ToString();
                model.u_name = ds.Tables[0].Rows[0]["u_name"].ToString();
                model.u_account = ds.Tables[0].Rows[0]["u_account"].ToString();
                if (ds.Tables[0].Rows[0]["u_drawing"].ToString() != "")
                {
                    model.u_drawing = int.Parse(ds.Tables[0].Rows[0]["u_drawing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_merchant_id"].ToString() != "")
                {
                    model.u_merchant_id = int.Parse(ds.Tables[0].Rows[0]["u_merchant_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_time"].ToString() != "")
                {
                    model.u_time = DateTime.Parse(ds.Tables[0].Rows[0]["u_time"].ToString());
                }
                model.u_auditor = ds.Tables[0].Rows[0]["u_auditor"].ToString();
                model.u_paypwd = ds.Tables[0].Rows[0]["u_paypwd"].ToString();
                model.u_photof = ds.Tables[0].Rows[0]["u_photof"].ToString();
                model.u_licence = ds.Tables[0].Rows[0]["u_licence"].ToString();


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
        public JMP.MDL.jmp_user GetModelByTel(string tel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id,u_category,u_idnumber,u_photo,u_blicense,u_blicensenumber,u_count,u_state,u_auditstate,u_topid,u_address,u_email,u_role_id,u_password,u_realname,u_phone,u_qq,u_bankname,u_name,u_account,u_merchant_id,u_drawing,u_time,u_auditor,u_photof,u_licence");
            strSql.Append(" from jmp_user");
            strSql.Append(" where u_phone=@u_phone");
            SqlParameter[] parameters = {
                new SqlParameter("@u_phone", SqlDbType.NVarChar,-1)
            };
            parameters[0].Value = tel;

            JMP.MDL.jmp_user model = new JMP.MDL.jmp_user();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(ds.Tables[0].Rows[0]["u_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_category"].ToString() != "")
                {
                    model.u_category = int.Parse(ds.Tables[0].Rows[0]["u_category"].ToString());
                }
                model.u_idnumber = ds.Tables[0].Rows[0]["u_idnumber"].ToString();
                model.u_photo = ds.Tables[0].Rows[0]["u_photo"].ToString();
                model.u_blicense = ds.Tables[0].Rows[0]["u_blicense"].ToString();
                model.u_blicensenumber = ds.Tables[0].Rows[0]["u_blicensenumber"].ToString();
                if (ds.Tables[0].Rows[0]["u_count"].ToString() != "")
                {
                    model.u_count = int.Parse(ds.Tables[0].Rows[0]["u_count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_state"].ToString() != "")
                {
                    model.u_state = int.Parse(ds.Tables[0].Rows[0]["u_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_auditstate"].ToString() != "")
                {
                    model.u_auditstate = int.Parse(ds.Tables[0].Rows[0]["u_auditstate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_topid"].ToString() != "")
                {
                    model.u_topid = int.Parse(ds.Tables[0].Rows[0]["u_topid"].ToString());
                }
                model.u_address = ds.Tables[0].Rows[0]["u_address"].ToString();
                model.u_email = ds.Tables[0].Rows[0]["u_email"].ToString();
                //if (ds.Tables[0].Rows[0]["u_poundage"].ToString() != "")
                //{
                //    model.u_poundage = decimal.Parse(ds.Tables[0].Rows[0]["u_poundage"].ToString());
                //}
                if (ds.Tables[0].Rows[0]["u_role_id"].ToString() != "")
                {
                    model.u_role_id = int.Parse(ds.Tables[0].Rows[0]["u_role_id"].ToString());
                }
                model.u_password = ds.Tables[0].Rows[0]["u_password"].ToString();
                model.u_realname = ds.Tables[0].Rows[0]["u_realname"].ToString();
                model.u_phone = ds.Tables[0].Rows[0]["u_phone"].ToString();
                model.u_qq = ds.Tables[0].Rows[0]["u_qq"].ToString();
                model.u_bankname = ds.Tables[0].Rows[0]["u_bankname"].ToString();
                model.u_name = ds.Tables[0].Rows[0]["u_name"].ToString();
                model.u_account = ds.Tables[0].Rows[0]["u_account"].ToString();
                if (ds.Tables[0].Rows[0]["u_drawing"].ToString() != "")
                {
                    model.u_drawing = int.Parse(ds.Tables[0].Rows[0]["u_drawing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_merchant_id"].ToString() != "")
                {
                    model.u_merchant_id = int.Parse(ds.Tables[0].Rows[0]["u_merchant_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["u_time"].ToString() != "")
                {
                    model.u_time = DateTime.Parse(ds.Tables[0].Rows[0]["u_time"].ToString());
                }
                model.u_auditor = ds.Tables[0].Rows[0]["u_auditor"].ToString();
                model.u_photof = ds.Tables[0].Rows[0]["u_photof"].ToString();
                model.u_licence = ds.Tables[0].Rows[0]["u_licence"].ToString();

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
            strSql.Append("select *");
            strSql.Append(" from jmp_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("where " + strWhere);
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
            strSql.Append("  u_id,u_category,u_idnumber,u_photo,u_blicense,u_blicensenumber,u_count,u_state,u_auditstate,u_topid,u_address,u_email,u_poundage,u_role_id,u_password,u_realname,u_phone,u_qq,u_bankname,u_name,u_account,u_merchant_id,u_drawing,u_time,u_auditor ");
            strSql.Append(" FROM jmp_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        DataTable dt = new DataTable();

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_user> GetLists(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_user>(ds.Tables[0]);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<AppUserMerchant> GetAppUserMerchantLists(string sqls, string Order, int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<AppUserMerchant>(ds.Tables[0]);
        }

        /// <summary>
        /// 获取未读消息数
        /// </summary>
        /// <param name="uid">用户</param>
        /// <returns></returns>
        public int GetUserMsgCount(int uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from jmp_message where m_state=0 and m_receiver='" + uid + "' ");
            object obj = DbHelperSQL.GetSingle(builder.ToString());
            return obj != null ? Convert.ToInt32(obj) : 0;
        }


        /// <summary>
        /// 指定到商务
        /// </summary>
        /// <param name="uids"></param>
        /// <param name="mid">商务ID</param>
        /// <returns></returns>
        public bool AssignToMerchant(string uids, int mid)
        {
            var strSql = string.Format("UPDATE jmp_user SET u_merchant_id={0} WHERE u_id IN ({1})", mid, uids);
            var i = DbHelperSQL.ExecuteSql(strSql);
            return i > 0;
        }


        /// <summary>
        /// 获取商务所属的用户总数
        /// </summary>
        /// <param name="merchantId">商务ID</param>
        /// <returns></returns>
        public int GetAppUserCount(int merchantId, string u_state)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(u_state))
            {
                builder.Append(string.Format("select count(1) from jmp_user where u_merchant_id={0} and u_state={1}", merchantId, u_state));
            }
            else
            {
                builder.Append(string.Format("select count(1) from jmp_user where u_merchant_id={0}", merchantId));

            }
            var obj = DbHelperSQL.GetSingle(builder.ToString());
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 获取商务所属的活跃用户总数
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public int GetActiveAppUserCount(int merchantId, string kstime, string endtime)
        {
            var builder = new StringBuilder();

            builder.Append(string.Format("select COUNT(1) from (select a_uerid from " + JMP.DbName.PubDbName.dbtotal + ".dbo.jmp_appcount where a_uerid in (select u_id from jmp_user where u_merchant_id={0}) and a_datetime>='{1}'  and a_datetime<= '{2}' group by a_uerid) as a", merchantId, kstime, endtime));
            var obj = DbHelperSQL.GetSingle(builder.ToString());
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <param name="state">状态值</param>
        ///  <param name="name">审核人</param>
        /// <returns></returns>
        public bool UpdateAuditState(int ids, int state,string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_USER set u_auditstate=" + state + ",u_auditor='"+name+"' where u_id =" + ids + "");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }
        public bool UpdateRiskSH(int id, int IsSignContract, int IsRecord)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_USER set IsSignContract=" + IsSignContract + ",IsRecord='" + IsRecord + "' where u_id =" + id + "");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 是否存在该手机号码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="userid">用户id（可不传）</param>
        /// <returns></returns>
        public bool ExistsPhone(string phone, string userid = "")
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from jmp_user where u_phone='{0}'", phone);

            if (!string.IsNullOrEmpty(userid))
                strSql.AppendFormat(" and u_id<>{0}", userid);

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 修改开发者特批状态
        /// </summary>
        /// <param name="uids">用户ID</param>
        /// <param name="state">状态</param>
        /// <param name="fl">特批时需要减掉的服务费率</param>
        /// <returns></returns>
        public bool UpdateIsSpecialApproval(int ids, int state, string fl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JMP_USER set IsSpecialApproval=" + state + ",SpecialApproval='" + fl + "' where u_id =" + ids + "");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 设置支付密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="Pwd">支付密码</param>
        /// <returns></returns>
        public bool UpdateUserPayPwd(int id, string Pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_user set u_paypwd='"+Pwd+"' where u_id='"+id+"'");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        public MDL.jmp_user FindByRealName(string realName)
        {
            var sql = "SELECT * FROM jmp_user WHERE u_realname=@RealName";
            SqlParameter[] parameters = {
                new SqlParameter("@RealName", SqlDbType.NVarChar)
            };
            parameters[0].Value = realName;
            var ds = DbHelperSQL.Query(sql,parameters);
            var entity = DbHelperSQL.ToModel<MDL.jmp_user>(ds.Tables[0]);
            return entity;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="ServiceFeeRatioGradeId">费率表ID</param>
        /// <returns></returns>
        public bool UpdateFrozenMoney(int id, decimal FrozenMoney)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update jmp_user set ");
            sql.Append("FrozenMoney='" + FrozenMoney + "' ");
            sql.Append("where u_id='" + id + "'");
            return DbHelperSQL.ExecuteSql(sql.ToString()) > 0;
        }
    }
}

