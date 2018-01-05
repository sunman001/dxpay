using JMP.DBA;
using JMP.Model.Query.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JMP.DAL
{
    public class jmp_Help_Content
    {
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_Help_Content");
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
        public int Add(JMP.MDL.jmp_Help_Content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_Help_Content(");
            strSql.Append("ClassId,Title,Content,Count,CreateById,CreateByName,CreateOn,ISOverhead,UpdateById,UpdateByName,UpdateOn,PrentID,State,Type");
            strSql.Append(") values (");
            strSql.Append("@ClassId,@Title,@Content,@Count,@CreateById,@CreateByName,@CreateOn,@ISOverhead,@UpdateById,@UpdateByName,@UpdateOn,@PrentID,@State,@Type");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@Title", SqlDbType.NVarChar,300) ,
                        new SqlParameter("@Content", SqlDbType.Text) ,
                        new SqlParameter("@Count", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateByName", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@CreateOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ISOverhead", SqlDbType.Bit,1),
                        new SqlParameter("@UpdateById", SqlDbType.Int,4) ,
                        new SqlParameter("@UpdateByName", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@UpdateOn", SqlDbType.DateTime),
                        new SqlParameter("@PrentID", SqlDbType.Int,4),
                        new SqlParameter("@State", SqlDbType.Int,4),
                        new SqlParameter("@Type", SqlDbType.Int,4)

            };

            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Content;
            parameters[3].Value = model.Count;
            parameters[4].Value = model.CreateById;
            parameters[5].Value = model.CreateByName;
            parameters[6].Value = model.CreateOn;
            parameters[7].Value = model.ISOverhead;
            parameters[8].Value = model.UpdateById;
            parameters[9].Value = model.UpdateByName;
            parameters[10].Value = model.UpdateOn;
            parameters[11].Value = model.PrentID;
            parameters[12].Value = model.State;
            parameters[13].Value = model.Type;
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
        public bool Update(JMP.MDL.jmp_Help_Content model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_Help_Content set ");
            strSql.Append(" UpdateById = @UpdateById , ");
            strSql.Append(" UpdateByName = @UpdateByName , ");
            strSql.Append(" UpdateOn = @UpdateOn , ");
            strSql.Append(" ISOverhead = @ISOverhead , ");
            strSql.Append(" State = @State , ");
            strSql.Append(" PrentID = @PrentID , ");
            strSql.Append(" ClassId = @ClassId , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" Content = @Content ,");
            strSql.Append(" Count = @Count ,");
            strSql.Append(" CreateById = @CreateById ,");
            strSql.Append(" CreateByName = @CreateByName ,");
            strSql.Append(" CreateOn = @CreateOn, ");
            strSql.Append(" Type = @Type  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4) ,
                        new SqlParameter("@UpdateById", SqlDbType.Int,4) ,
                        new SqlParameter("@UpdateByName", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@UpdateOn", SqlDbType.DateTime) ,
                        new SqlParameter("@ISOverhead", SqlDbType.Bit,1) ,
                        new SqlParameter("@State", SqlDbType.Int,4) ,
                        new SqlParameter("@PrentID", SqlDbType.Int,4) ,
                        new SqlParameter("@ClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@Title", SqlDbType.NVarChar,300) ,
                        new SqlParameter("@Content", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@Count", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateById", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateByName", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@CreateOn", SqlDbType.DateTime),
                        new SqlParameter("@Type", SqlDbType.Int,4) 

            };

            parameters[0].Value = model.ID;
            parameters[1].Value = model.UpdateById;
            parameters[2].Value = model.UpdateByName;
            parameters[3].Value = model.UpdateOn;
            parameters[4].Value = model.ISOverhead;
            parameters[5].Value = model.State;
            parameters[6].Value = model.PrentID;
            parameters[7].Value = model.ClassId;
            parameters[8].Value = model.Title;
            parameters[9].Value = model.Content;
            parameters[10].Value = model.Count;
            parameters[11].Value = model.CreateById;
            parameters[12].Value = model.CreateByName;
            parameters[13].Value = model.CreateOn;
            parameters[14].Value = model.Type;
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
            strSql.Append("delete from jmp_Help_Content ");
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
            strSql.Append("delete from jmp_Help_Content ");
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
        public JMP.MDL.jmp_Help_Content GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ClassId,PrentID, Title, Content, Count, CreateById, CreateByName, CreateOn, ISOverhead ,PrentID,Type,State ");
            strSql.Append("  from jmp_Help_Content ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


            JMP.MDL.jmp_Help_Content model = new JMP.MDL.jmp_Help_Content();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PrentID"].ToString() != "")
                {
                    model.PrentID = int.Parse(ds.Tables[0].Rows[0]["PrentID"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                if (ds.Tables[0].Rows[0]["Count"].ToString() != "")
                {
                    model.Count = int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateById"].ToString() != "")
                {
                    model.CreateById = int.Parse(ds.Tables[0].Rows[0]["CreateById"].ToString());
                }
                model.CreateByName = ds.Tables[0].Rows[0]["CreateByName"].ToString();
                if (ds.Tables[0].Rows[0]["CreateOn"].ToString() != "")
                {
                    model.CreateOn = DateTime.Parse(ds.Tables[0].Rows[0]["CreateOn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ISOverhead"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ISOverhead"].ToString() == "1") || (ds.Tables[0].Rows[0]["ISOverhead"].ToString().ToLower() == "true"))
                    {
                        model.ISOverhead = true;
                    }
                    else
                    {
                        model.ISOverhead = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["PrentID"].ToString() != "")
                {
                    model.PrentID = int.Parse(ds.Tables[0].Rows[0]["PrentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
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
            strSql.Append(" FROM jmp_Help_Content ");
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
            strSql.Append(" FROM jmp_Help_Content ");
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
        public List<JMP.MDL.jmp_Help_Content> SelectList( int sType,string Type, string sea_name, int State, int PrentID, int ClassId, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format(" select a.*,b.ClassName ,c.ClassName as PrentClassName  from jmp_Help_Content a  left join jmp_Help_Classification b on a.ClassId=b.ID  left join jmp_Help_Classification c on a.PrentID=c.ID where 1=1");


            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(sea_name))
            {
                switch (Type)
                {
                    case "1":
                        sql += " and a.ID="+sea_name+"";
                        break;
                    case "2":
                        sql += "  and a.Title like  '%" + sea_name + "%'";
                        break;
                    case "3":
                        sql += " and  a.CreateByName = '" + sea_name + "'";
                        break;
                }

            }
            if (State >= 0)
            {
                sql += " and a.State='" + State + "'";
            }
            if (PrentID > 0)
            {
                sql += " and a.PrentID='" + PrentID + "'";
            }
            if(sType>0)
            {
                sql += " and a.Type='" + sType + "'";
            }
            if (ClassId > 0)
            {
                sql += " and a.ClassId='" + ClassId + "'";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_Help_Content>(ds.Tables[0]);
        }
        public bool UpdateState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_Help_Content set State=" + state + "  ");
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
        public bool UpdateClassCount(int PrentId,int type)
        {
            StringBuilder strSql = new StringBuilder();
            if(type==0)
            {
               strSql.Append(" update jmp_Help_Classification set Count=(select count(1) from jmp_Help_Content  where PrentID="+PrentId+" ) where ID="+PrentId+" ");  
            }
            else
            {
                strSql.Append(" update jmp_Help_Classification set Count=(select count(1) from jmp_Help_Content  where ClassId=" + PrentId + " ) where ID=" + PrentId + " ");
            }
                   
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

        public bool UpdateViewCount(int articleId, int viewCount)
        {
            var sql = string.Format("UPDATE jmp_Help_Content SET [Count]={0} WHERE ID={1}", viewCount, articleId);
            return DbHelperSQL.ExecuteSql(sql) > 0;
        }

        /// <summary>
        /// 返回主分类下按子分类分组的前5篇文章
        /// </summary>
        /// <param name="mainCatalogId">主分类ID</param>
        /// <returns></returns>
        public List<ArticleWithTotalCountQueryModel> FindTopArticleListGroupBySubCatalog(int mainCatalogId)
        {
            var sql = @";with t as (
	                select *,row_number() over (partition by jhc.ClassId order by ID desc) as Rn from dbo.jmp_Help_Content as jhc where jhc.PrentID={0} 
                ),t1 as (
	                select t.ClassId,count(1) as SubCatalogArticleCount from t group by t.ClassId
                ),t2 as(
	                select t.*,jhc.ClassName from t left join dbo.jmp_Help_Classification as jhc on t.ClassId=jhc.ID
                )
                select t2.ID,t2.PrentID as MainCatalogId,t2.ClassId as SubCatalogId,t2.ClassName as SubCatalogName,t2.Title,t1.SubCatalogArticleCount from t2,t1 where t2.ClassId=t1.ClassId and t2.Rn<=5";
            var ds = DbHelperSQL.Query(string.Format(sql, mainCatalogId));
            var list = DbHelperSQL.ConvertToList<ArticleWithTotalCountQueryModel>(ds.Tables[0]).ToList();
            return list;
        }

        public List<MDL.jmp_Help_Content> Search(string[] keywords)
        {
            var sb = new List<string>();
            foreach (var kw in keywords)
            {
                var q = kw.Replace("'", "");
                sb.Add(string.Format("SELECT * FROM jmp_Help_Content WHERE Title LIKE '%{0}%'", q));
                sb.Add(string.Format("SELECT * FROM jmp_Help_Content WHERE Content LIKE '%{0}%'", q));
            }
            var ds = DbHelperSQL.Query(string.Join(" UNION ", sb));
            var list = DbHelperSQL.ConvertToList<MDL.jmp_Help_Content>(ds.Tables[0]).ToList();
            return list;
        }
    }
}

