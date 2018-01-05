using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DBA;
namespace JMP.DAL
{
    //通知短信分组信息表
    public partial class jmp_notificaiton_group
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_notificaiton_group");
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
        public int Add(JMP.MDL.jmp_notificaiton_group model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_notificaiton_group(");
            strSql.Append("IntervalValue,IntervalUnit,CreatedOn,CreatedBy,CreatedByUser,ModifiedOn,ModifiedBy,ModifiedByUser,Name,Code,Description,NotifyMobileList,MessageTemplate,IsDeleted,IsEnabled,IsAllowSendMessage,SendMode,AudioTelTempId,AudioTelTempContent");
            strSql.Append(") values (");
            strSql.Append("@IntervalValue,@IntervalUnit,@CreatedOn,@CreatedBy,@CreatedByUser,@ModifiedOn,@ModifiedBy,@ModifiedByUser,@Name,@Code,@Description,@NotifyMobileList,@MessageTemplate,@IsDeleted,@IsEnabled,@IsAllowSendMessage,@SendMode,@AudioTelTempId,@AudioTelTempContent");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@IntervalValue", SqlDbType.Int,4) ,
                        new SqlParameter("@IntervalUnit", SqlDbType.NVarChar,10) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedBy", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUser", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@ModifiedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifiedBy", SqlDbType.Int,4) ,
                        new SqlParameter("@ModifiedByUser", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@Code", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@NotifyMobileList", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@MessageTemplate", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@IsDeleted", SqlDbType.Bit,1) ,
                        new SqlParameter("@IsEnabled", SqlDbType.Bit,1) ,
                        new SqlParameter("@IsAllowSendMessage", SqlDbType.Bit,1),
                        new SqlParameter("@SendMode", SqlDbType.NVarChar,10),
                        new SqlParameter("@AudioTelTempId", SqlDbType.BigInt,12),
                        new SqlParameter("@AudioTelTempContent", SqlDbType.NVarChar,255)                  
            };

            parameters[0].Value = model.IntervalValue;
            parameters[1].Value = model.IntervalUnit;
            parameters[2].Value = model.CreatedOn;
            parameters[3].Value = model.CreatedBy;
            parameters[4].Value = model.CreatedByUser;
            parameters[5].Value = model.ModifiedOn;
            parameters[6].Value = model.ModifiedBy;
            parameters[7].Value = model.ModifiedByUser;
            parameters[8].Value = model.Name;
            parameters[9].Value = model.Code;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.NotifyMobileList;
            parameters[12].Value = model.MessageTemplate;
            parameters[13].Value = model.IsDeleted;
            parameters[14].Value = model.IsEnabled;
            parameters[15].Value = model.IsAllowSendMessage;
            parameters[16].Value = model.SendMode;
            parameters[17].Value = model.AudioTelTempId;
            parameters[18].Value = model.AudioTelTempContent;

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
        public bool Update(JMP.MDL.jmp_notificaiton_group model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_notificaiton_group set ");

            strSql.Append(" IntervalValue = @IntervalValue , ");
            strSql.Append(" IntervalUnit = @IntervalUnit , ");
            strSql.Append(" CreatedOn = @CreatedOn , ");
            strSql.Append(" CreatedBy = @CreatedBy , ");
            strSql.Append(" CreatedByUser = @CreatedByUser , ");
            strSql.Append(" ModifiedOn = @ModifiedOn , ");
            strSql.Append(" ModifiedBy = @ModifiedBy , ");
            strSql.Append(" ModifiedByUser = @ModifiedByUser , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Code = @Code , ");
            strSql.Append(" Description = @Description , ");
            strSql.Append(" NotifyMobileList = @NotifyMobileList , ");
            strSql.Append(" MessageTemplate = @MessageTemplate , ");
            strSql.Append(" IsDeleted = @IsDeleted , ");
            strSql.Append(" IsEnabled = @IsEnabled , ");
            strSql.Append(" IsAllowSendMessage = @IsAllowSendMessage,");
            strSql.Append(" SendMode = @SendMode,");
            strSql.Append(" AudioTelTempId=@AudioTelTempId,");
            strSql.Append(" AudioTelTempContent=@AudioTelTempContent");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@IntervalValue", SqlDbType.Int,4) ,
                        new SqlParameter("@IntervalUnit", SqlDbType.NVarChar,10) ,
                        new SqlParameter("@CreatedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@CreatedBy", SqlDbType.Int,4) ,
                        new SqlParameter("@CreatedByUser", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@ModifiedOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifiedBy", SqlDbType.Int,4) ,
                        new SqlParameter("@ModifiedByUser", SqlDbType.NVarChar,30) ,
                        new SqlParameter("@Name", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@Code", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@NotifyMobileList", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@MessageTemplate", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@IsDeleted", SqlDbType.Bit,1) ,
                        new SqlParameter("@IsEnabled", SqlDbType.Bit,1) ,
                        new SqlParameter("@IsAllowSendMessage", SqlDbType.Bit,1),
                        new SqlParameter("@SendMode",  SqlDbType.NVarChar,10),
                        new SqlParameter("@AudioTelTempId", SqlDbType.BigInt,12),
                        new SqlParameter("@AudioTelTempContent", SqlDbType.NVarChar,255)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.IntervalValue;
            parameters[2].Value = model.IntervalUnit;
            parameters[3].Value = model.CreatedOn;
            parameters[4].Value = model.CreatedBy;
            parameters[5].Value = model.CreatedByUser;
            parameters[6].Value = model.ModifiedOn;
            parameters[7].Value = model.ModifiedBy;
            parameters[8].Value = model.ModifiedByUser;
            parameters[9].Value = model.Name;
            parameters[10].Value = model.Code;
            parameters[11].Value = model.Description;
            parameters[12].Value = model.NotifyMobileList;
            parameters[13].Value = model.MessageTemplate;
            parameters[14].Value = model.IsDeleted;
            parameters[15].Value = model.IsEnabled;
            parameters[16].Value = model.IsAllowSendMessage;
            parameters[17].Value = model.SendMode;
            parameters[18].Value = model.AudioTelTempId;
            parameters[19].Value = model.AudioTelTempContent;
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
            strSql.Append("delete from jmp_notificaiton_group ");
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
            strSql.Append("delete from jmp_notificaiton_group ");
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
        public JMP.MDL.jmp_notificaiton_group GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, IntervalValue, IntervalUnit, CreatedOn, CreatedBy, CreatedByUser, ModifiedOn, ModifiedBy, ModifiedByUser, Name, Code, Description, NotifyMobileList, MessageTemplate, IsDeleted, IsEnabled, IsAllowSendMessage,SendMode,AudioTelTempId,AudioTelTempContent");
            strSql.Append("  from jmp_notificaiton_group ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;


            JMP.MDL.jmp_notificaiton_group model = new JMP.MDL.jmp_notificaiton_group();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IntervalValue"].ToString() != "")
                {
                    model.IntervalValue = int.Parse(ds.Tables[0].Rows[0]["IntervalValue"].ToString());
                }
                model.IntervalUnit = ds.Tables[0].Rows[0]["IntervalUnit"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedOn"].ToString() != "")
                {
                    model.CreatedOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedBy"].ToString() != "")
                {
                    model.CreatedBy = int.Parse(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                }
                model.CreatedByUser = ds.Tables[0].Rows[0]["CreatedByUser"].ToString();
                if (ds.Tables[0].Rows[0]["ModifiedOn"].ToString() != "")
                {
                    model.ModifiedOn = DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedBy"].ToString() != "")
                {
                    model.ModifiedBy = int.Parse(ds.Tables[0].Rows[0]["ModifiedBy"].ToString());
                }
                model.ModifiedByUser = ds.Tables[0].Rows[0]["ModifiedByUser"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.NotifyMobileList = ds.Tables[0].Rows[0]["NotifyMobileList"].ToString();
                model.MessageTemplate = ds.Tables[0].Rows[0]["MessageTemplate"].ToString();
                model.SendMode = ds.Tables[0].Rows[0]["SendMode"].ToString();
                model.AudioTelTempContent = ds.Tables[0].Rows[0]["AudioTelTempContent"].ToString();
                if (ds.Tables[0].Rows[0]["AudioTelTempId"].ToString() != "")
                {
                    model.AudioTelTempId = int.Parse(ds.Tables[0].Rows[0]["AudioTelTempId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDeleted"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDeleted"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDeleted"].ToString().ToLower() == "true"))
                    {
                        model.IsDeleted = true;
                    }
                    else
                    {
                        model.IsDeleted = false;
                    }
                }
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
                if (ds.Tables[0].Rows[0]["IsAllowSendMessage"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsAllowSendMessage"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsAllowSendMessage"].ToString().ToLower() == "true"))
                    {
                        model.IsAllowSendMessage = true;
                    }
                    else
                    {
                        model.IsAllowSendMessage = false;
                    }
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
            strSql.Append(" FROM jmp_notificaiton_group ");
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
            strSql.Append(" FROM jmp_notificaiton_group ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectState"></param>
        /// <param name="sea_name"></param>
        /// <param name="type"></param>
        /// <param name="searchDesc"></param>
        /// <param name="pageIndexs"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_notificaiton_group> SelectList(string SelectState, string IntervalUnit, string sea_name, int type, int searchDesc, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select * from jmp_notificaiton_group where IsDeleted=0");
            string Order = " order Id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and Name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and Code like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and Description like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and NotifyMobileList like  '%" + sea_name + "%' ";
                        break;
                    case 5:
                        sql += " and MessageTemplate like  '%" + sea_name + "%' ";
                        break;
                    case 6:
                        sql += " and IntervalValue like  '%" + sea_name + "%' ";
                        break;
                }

            }
            if (!string.IsNullOrEmpty(SelectState))
            {
                sql += " and IsEnabled='" + SelectState + "' ";
            }
            if (!string.IsNullOrEmpty(IntervalUnit))
            {
                sql += " and IntervalUnit='" + IntervalUnit + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by Id";
            }
            else
            {
                Order = " order by Id  desc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_notificaiton_group>(ds.Tables[0]);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            bool IsEnabled = state==1? true : false;
            strSql.Append(" update jmp_notificaiton_group set IsEnabled=" + state + "  ");
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

        public bool Delete(string u_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_notificaiton_group set IsDeleted=1  ");
            strSql.Append(" where Id in (" + u_idlist + ")");
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
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="c_id">id</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        public JMP.MDL.jmp_notificaiton_group SelectId(int c_id)
        {
            string sql = string.Format(" select * from jmp_notificaiton_group  where Id=@r_id ");
            SqlParameter par = new SqlParameter("@r_id", c_id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_notificaiton_group>(dt);
        }


    }
}

