using JMP.DBA;
using JMP.Model.Query.Doc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JMP.DAL
{
    public  class jmp_Help_Classification
    {
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_Help_Classification");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_Help_Classification model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_Help_Classification(");
            strSql.Append("ClassName,ParentID,Sort,State,Count,CreateById,CreateByName,CreateOn,Icon,Description,Type");
            strSql.Append(") values (");
            strSql.Append("@ClassName,@ParentID,@Sort,@State,@Count,@CreateById,@CreateByName,@CreateOn,@Icon,@Description,@Type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ClassName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ParentID", SqlDbType.Int,4),
                        new SqlParameter("@Sort", SqlDbType.Int,4),
                        new SqlParameter("@State", SqlDbType.Int,4),
                        new SqlParameter("@Count", SqlDbType.Int,4),
                        new SqlParameter("@CreateById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateByName", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@CreateOn", SqlDbType.DateTime),
                        new SqlParameter("@Icon", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@Type", SqlDbType.Int,4)


            };

            parameters[0].Value = model.ClassName;
            parameters[1].Value = model.ParentID;
            parameters[2].Value = model.Sort;
            parameters[3].Value = model.State;
            parameters[4].Value = model.Count;
            parameters[5].Value = model.CreateByID;
            parameters[6].Value = model.CreateByName;
            parameters[7].Value = model.CreateOn;
            parameters[8].Value = model.Icon;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.Type;
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
        public bool Update(JMP.MDL.jmp_Help_Classification model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_Help_Classification set ");

            strSql.Append(" ClassName = @ClassName , ");
            strSql.Append(" ParentID = @ParentID, ");
            strSql.Append(" Sort = @Sort,");
            strSql.Append(" State = @State,");
            strSql.Append(" Count = @Count,");
            strSql.Append(" CreateByID = @CreateByID,");
            strSql.Append(" CreateByName = @CreateByName,");
            strSql.Append(" CreateOn = @CreateOn,");
            strSql.Append(" Icon = @Icon,");
            strSql.Append(" Description = @Description,");
            strSql.Append(" Type = @Type");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4) ,
                        new SqlParameter("@ClassName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ParentID", SqlDbType.Int,4),
                        new SqlParameter("@Sort", SqlDbType.Int,4),
                        new SqlParameter("@State", SqlDbType.Int,4),
                        new SqlParameter("@Count", SqlDbType.Int,4),
                        new SqlParameter("@CreateByID", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateByName", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@CreateOn", SqlDbType.DateTime),
                        new SqlParameter("@Icon", SqlDbType.NVarChar,255),
                        new SqlParameter("@Description", SqlDbType.NVarChar,500),
                        new SqlParameter("@Type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.ID;
            parameters[1].Value = model.ClassName;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.State;
            parameters[5].Value = model.Count;
            parameters[6].Value = model.CreateByID;
            parameters[7].Value = model.CreateByName;
            parameters[8].Value = model.CreateOn;
            parameters[9].Value = model.Icon;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.Type;
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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_Help_Classification ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_Help_Classification ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public JMP.MDL.jmp_Help_Classification GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ClassName, ParentID , Sort,State,Count,CreateOn,CreateByID,CreateByName,Icon,Description,Type");
            strSql.Append(" from jmp_Help_Classification ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


            JMP.MDL.jmp_Help_Classification model = new JMP.MDL.jmp_Help_Classification();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.ClassName = ds.Tables[0].Rows[0]["ClassName"].ToString();
                if (ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Count"].ToString() != "")
                {
                    model.Count = int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateByID"].ToString() != "")
                {
                    model.CreateByID = int.Parse(ds.Tables[0].Rows[0]["CreateByID"].ToString());
                }
                model.CreateByName = ds.Tables[0].Rows[0]["CreateByName"].ToString();
                if (ds.Tables[0].Rows[0]["CreateOn"].ToString() != "")
                {
                    model.CreateOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreateOn"].ToString());
                }
                model.Icon = ds.Tables[0].Rows[0]["Icon"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
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
            strSql.Append(" FROM jmp_Help_Classification ");
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
            strSql.Append(" FROM jmp_Help_Classification ");
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
        public List<JMP.MDL.jmp_Help_Classification> SelectList(int sType, int ParentID,  string Sname, int type, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select * from jmp_Help_Classification where 1=1");

         
            if (!string.IsNullOrEmpty(Sname))
            {
                sql += " and ClassName like '%" + Sname + "%'";
            }
           if(type>=0)
            {
                sql += " and State='"+type+"'";    
            }
            if(sType>=0)
            {
                sql += " and Type='" + sType + "'";
            }
            if (ParentID > 0)
            {
                sql += " and ParentID='" + ParentID + "'";
            }
            string Order = " order by ID desc ";

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
            return DbHelperSQL.ToList<JMP.MDL.jmp_Help_Classification>(ds.Tables[0]);
        }
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_Help_Classification set State=" + state + "  ");
            strSql.Append(" where ID in (" + u_idlist + ")  ");
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
        /// 读取所有可用的开发文档集合
        /// </summary>
        /// <returns></returns>
        public List<DocMenuQueryModel> FindAllEnabledDocList()
        {
            var sql = @"select jhc.ID as ArticleId,jhc.Title,jhc.ClassId,jhc2.* from dbo.jmp_Help_Content as jhc left join dbo.jmp_Help_Classification as jhc2 on jhc.ClassId=jhc2.ID where jhc2.Type=1 and jhc2.State=0 and jhc.State=0
union select 0 as ArticleId,jhc.ClassName as Title,0 as ClassId,* from dbo.jmp_Help_Classification as jhc where jhc.State=0 and jhc.Type=1";
            var ds = DbHelperSQL.Query(sql);
            var list = DbHelperSQL.ConvertToList<DocMenuQueryModel>(ds.Tables[0]).ToList();
            return list;
        }
    }
}

