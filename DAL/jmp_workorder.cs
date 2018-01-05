using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JMP.DBA;
namespace JMP.DAL
{
    /// <summary>
    /// 工单
    /// </summary>
    public partial class jmp_workorder
    {
        DataTable dt = new DataTable();

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_workorder");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_workorder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_workorder(");
            strSql.Append("viewcount,latestviewdate,score,pushedremind,pushreminddate,closereason,initiatorreason,handlerreason,result,catalog,title,content,status,progress,createdon,createdbyid,watchidsoftheday,resultDate");
            strSql.Append(") values (");
            strSql.Append("@viewcount,@latestviewdate,@score,@pushedremind,@pushreminddate,@closereason,@initiatorreason,@handlerreason,@result,@catalog,@title,@content,@status,@progress,@createdon,@createdbyid,@watchidsoftheday,@resultDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@viewcount", SqlDbType.Int,4) ,
                        new SqlParameter("@latestviewdate", SqlDbType.DateTime) ,
                        new SqlParameter("@score", SqlDbType.Int,4) ,
                        new SqlParameter("@pushedremind", SqlDbType.Bit,1) ,
                        new SqlParameter("@pushreminddate", SqlDbType.DateTime) ,
                        new SqlParameter("@closereason", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@initiatorreason", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@handlerreason", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@result", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@catalog", SqlDbType.Int,4) ,
                        new SqlParameter("@title", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@content", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@status", SqlDbType.Int,4) ,
                        new SqlParameter("@progress", SqlDbType.Int,4) ,
                        new SqlParameter("@createdon", SqlDbType.DateTime) ,
                        new SqlParameter("@createdbyid", SqlDbType.Int,4) ,
                        new SqlParameter("@watchidsoftheday", SqlDbType.NVarChar,255),
                        new SqlParameter("@resultDate", SqlDbType.DateTime)

            };

            parameters[0].Value = model.viewcount;
            parameters[1].Value = model.latestviewdate;
            parameters[2].Value = model.score;
            parameters[3].Value = model.pushedremind;
            parameters[4].Value = model.pushreminddate;
            parameters[5].Value = model.closereason;
            parameters[6].Value = model.initiatorreason;
            parameters[7].Value = model.handlerreason;
            parameters[8].Value = model.result;
            parameters[9].Value = model.catalog;
            parameters[10].Value = model.title;
            parameters[11].Value = model.content;
            parameters[12].Value = model.status;
            parameters[13].Value = model.progress;
            parameters[14].Value = model.createdon;
            parameters[15].Value = model.createdbyid;
            parameters[16].Value = model.watchidsoftheday;
            parameters[17].Value = model.resultDate;
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
        public bool Update(JMP.MDL.jmp_workorder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_workorder set ");

            strSql.Append(" viewcount = @viewcount , ");
            strSql.Append(" latestviewdate = @latestviewdate , ");
            strSql.Append(" score = @score , ");
            strSql.Append(" pushedremind = @pushedremind , ");
            strSql.Append(" pushreminddate = @pushreminddate , ");
            strSql.Append(" closereason = @closereason , ");
            strSql.Append(" initiatorreason = @initiatorreason , ");
            strSql.Append(" handlerreason = @handlerreason , ");
            strSql.Append(" result = @result , ");
            strSql.Append(" catalog = @catalog , ");
            strSql.Append(" title = @title , ");
            strSql.Append(" content = @content , ");
            strSql.Append(" status = @status , ");
            strSql.Append(" progress = @progress , ");
            strSql.Append(" createdon = @createdon , ");
            strSql.Append(" createdbyid = @createdbyid , ");
            strSql.Append(" watchidsoftheday = @watchidsoftheday  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@viewcount", SqlDbType.Int,4) ,
                        new SqlParameter("@latestviewdate", SqlDbType.DateTime) ,
                        new SqlParameter("@score", SqlDbType.Int,4) ,
                        new SqlParameter("@pushedremind", SqlDbType.Bit,1) ,
                        new SqlParameter("@pushreminddate", SqlDbType.DateTime) ,
                        new SqlParameter("@closereason", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@initiatorreason", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@handlerreason", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@result", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@catalog", SqlDbType.Int,4) ,
                        new SqlParameter("@title", SqlDbType.NVarChar,255) ,
                        new SqlParameter("@content", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@status", SqlDbType.Int,4) ,
                        new SqlParameter("@progress", SqlDbType.Int,4) ,
                        new SqlParameter("@createdon", SqlDbType.DateTime) ,
                        new SqlParameter("@createdbyid", SqlDbType.Int,4) ,
                        new SqlParameter("@watchidsoftheday", SqlDbType.NVarChar,255)

            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.viewcount;
            parameters[2].Value = model.latestviewdate;
            parameters[3].Value = model.score;
            parameters[4].Value = model.pushedremind;
            parameters[5].Value = model.pushreminddate;
            parameters[6].Value = model.closereason;
            parameters[7].Value = model.initiatorreason;
            parameters[8].Value = model.handlerreason;
            parameters[9].Value = model.result;
            parameters[10].Value = model.catalog;
            parameters[11].Value = model.title;
            parameters[12].Value = model.content;
            parameters[13].Value = model.status;
            parameters[14].Value = model.progress;
            parameters[15].Value = model.createdon;
            parameters[16].Value = model.createdbyid;
            parameters[17].Value = model.watchidsoftheday;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_workorder ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_workorder ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public JMP.MDL.jmp_workorder GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, viewcount, latestviewdate, score, pushedremind, pushreminddate, closereason, initiatorreason, handlerreason, result, catalog, title, content, status, progress, createdon, createdbyid, watchidsoftheday  ");
            strSql.Append("  from jmp_workorder ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            JMP.MDL.jmp_workorder model = new JMP.MDL.jmp_workorder();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["viewcount"].ToString() != "")
                {
                    model.viewcount = int.Parse(ds.Tables[0].Rows[0]["viewcount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["latestviewdate"].ToString() != "")
                {
                    model.latestviewdate = DateTime.Parse(ds.Tables[0].Rows[0]["latestviewdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["score"].ToString() != "")
                {
                    model.score = int.Parse(ds.Tables[0].Rows[0]["score"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pushedremind"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["pushedremind"].ToString() == "1") || (ds.Tables[0].Rows[0]["pushedremind"].ToString().ToLower() == "true"))
                    {
                        model.pushedremind = true;
                    }
                    else
                    {
                        model.pushedremind = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["pushreminddate"].ToString() != "")
                {
                    model.pushreminddate = DateTime.Parse(ds.Tables[0].Rows[0]["pushreminddate"].ToString());
                }
                model.closereason = ds.Tables[0].Rows[0]["closereason"].ToString();
                model.initiatorreason = ds.Tables[0].Rows[0]["initiatorreason"].ToString();
                model.handlerreason = ds.Tables[0].Rows[0]["handlerreason"].ToString();
                model.result = ds.Tables[0].Rows[0]["result"].ToString();
                if (ds.Tables[0].Rows[0]["catalog"].ToString() != "")
                {
                    model.catalog = int.Parse(ds.Tables[0].Rows[0]["catalog"].ToString());
                }
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.content = ds.Tables[0].Rows[0]["content"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["progress"].ToString() != "")
                {
                    model.progress = int.Parse(ds.Tables[0].Rows[0]["progress"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdon"].ToString() != "")
                {
                    model.createdon = DateTime.Parse(ds.Tables[0].Rows[0]["createdon"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createdbyid"].ToString() != "")
                {
                    model.createdbyid = int.Parse(ds.Tables[0].Rows[0]["createdbyid"].ToString());
                }
                model.watchidsoftheday = ds.Tables[0].Rows[0]["watchidsoftheday"].ToString();

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
            strSql.Append(" FROM jmp_workorder ");
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
            strSql.Append(" FROM jmp_workorder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据当前时间获取当前排班人
        /// </summary>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_scheduling SelectByDate(DateTime datatime)
        {
            string sql = string.Format(" select a.*,b.u_realname,b.u_mobilenumber,b.u_emailaddress,b.u_qq from jmp_scheduling  a left join jmp_locuser b on a.watchid=b.u_id  where 1=1 and a.watchstartdate<=@datatime and a.watchenddate>=@datatime");
            SqlParameter par = new SqlParameter("@datatime", datatime);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_scheduling>(dt);
        }

        /// <summary>
        /// 查询工单管理
        /// </summary>
        /// <param name="status">审核状态</param>
        ///  <param name="progress">进度</param>
        /// <param name="sea_name">查询内容</param>
        /// <param name="type">查询条件选择</param>
        /// <param name="searchDesc">排序</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_workorder> SelectList(string status, string progress, string sea_name, int type, int searchDesc, string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
            string sql = string.Format("select a.*, b.u_realname as name ,c.u_realname as createdbyname from jmp_workorder a left join jmp_locuser b on a.watchidsoftheday=b.u_id left join jmp_locuser c on a.createdbyid=c.u_id  where 1=1");
            string Order = " Order by  a.id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and  b.u_realname like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and a.title like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        sql += " and c.u_realname like  '%" + sea_name + "%' ";
                        break;
                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(endtime))
            {
                sql += " and a.createdon>='" + stime + " 00:00:00' and a.createdon<='" + endtime + " 23:59:59' ";
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += " and a.status='" + status + "' ";
            }

            if (!string.IsNullOrEmpty(progress))
            {
                sql += " and a.progress='" + progress + "' ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_workorder>(ds.Tables[0]);
        }

        /// <summary>
        /// 关闭工单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateState(int id, string state, string reason)
        {
            StringBuilder strSql = new StringBuilder();
            if (state == "-1")
            {
                strSql.Append(" update jmp_workorder set [status]=" + state + ", closereason='" + reason + "' ");
            }
            if (state == "-2")
            {
                strSql.Append(" update jmp_workorder set [status]=" + state + ", initiatorReason='" + reason + "' ");
            }
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
        /// 关闭工单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateHandState(int id, string state, string reason)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_workorder set [status]=" + state + ", handlerReason='" + reason + "' ");
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
        /// 根据应用id查询信息
        /// </summary>
        /// <param name="c_id">应用投诉id</param>
        /// <returns></returns>
        public JMP.MDL.jmp_workorder SelectId(int id)
        {
            string sql = string.Format(" select a.*, b.u_realname as name ,c.u_realname as createdbyname from jmp_workorder a left join jmp_locuser b on a.watchidsoftheday=b.u_id left join jmp_locuser c on a.createdbyid=c.u_id  where a.id=@id ");
            SqlParameter par = new SqlParameter("@id", id);
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_workorder>(dt);
        }

        /// <summary>
        /// 改变工单进度
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="state">进度</param>
        /// <returns></returns>
        public bool UpdateProgress(int id, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_workorder set [progress]=" + state + "  ");
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

        public bool UpdateResult( int id ,string result)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_workorder set result='" + result + "',resultDate='"+DateTime.Now+"'  ");
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

        public bool UpdateView(int id, DateTime date)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_workorder set viewcount=viewcount+1,latestviewdate='"+ date + "'  ");
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
        /// 进行评分
        /// </summary>
        /// <param name="id"></param>
        /// <param name="score"></param>
        /// <param name="scorereason"></param>
        /// <returns></returns>
        public bool UpdateScore(JMP.MDL.jmp_workorder mode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_workorder set [score]=@score,[scorereason]=@scorereason  ");
            strSql.Append(" where id =@id  ");

            SqlParameter[] s = {
                    new SqlParameter("@score",mode.score),
                    new SqlParameter("@scorereason",mode.scorereason),
                    new SqlParameter("@id",mode.id)
            };

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), s);
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
        /// 获取列表(值班人工单统计)
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="sType">汇总方式（应用名称、开发者邮箱）</param>
     
        /// <returns></returns>
        public DataTable GetListys(string start, string end)
        {
          
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select sum (countworkorder) as countworkorder   , sum(sucessworkorder) as sucessworkorder ,sum(branch) as branch,sum(socre) as socre, createdon  from ( ");
            //总工单量
            strSql.AppendLine("select count(1) as countworkorder,0 sucessworkorder, 0 branch ,0 socre , CONVERT(varchar(100), createdon, 23) as createdon from jmp_workorder where CONVERT(varchar(100), createdon, 23)>='" + start+"' and CONVERT(varchar(100), createdon, 23)<='"+end+ "' group by  CONVERT(varchar(100), createdon, 23)  union all ");
            //成功处理量
            strSql.AppendLine(" select   0 countworkorder ,count(1) as sucessworkorder,0 branch ,0 socre ,CONVERT(varchar(100), createdon, 23) as createdon from jmp_workorder where CONVERT(varchar(100), createdon, 23)>='" + start + "' and CONVERT(varchar(100), createdon, 23)<='" + end + "' and  status=0 and progress>= 2 group by  CONVERT(varchar(100), createdon, 23) union all ");
            //平均响应时间
            strSql.AppendLine(" select   0 countworkorder ,0 sucessworkorder, avg (DATEDIFF(n,createdon,resultDate))branch ,0 socre ,CONVERT(varchar(100), createdon, 23) as createdon from jmp_workorder where CONVERT(varchar(100), createdon, 23)>='" + start + "' and CONVERT(varchar(100), createdon, 23)<='" + end + "'    group by CONVERT(varchar(100), createdon, 23)  union all  ");
            //平均评分
            strSql.AppendLine("select   0 countworkorder ,0 sucessworkorder,0 branch ,avg(score) socre,CONVERT(varchar(100), createdon, 23) as createdon from jmp_workorder where CONVERT(varchar(100), createdon, 23)>='" + start + "' and CONVERT(varchar(100), createdon, 23)<='" + end + "' and score!=0  group by CONVERT(varchar(100), createdon, 23)) as workorder");
            strSql.Append("where 1=1");
            strSql.AppendLine(" group by  createdon");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }


        /// <summary>
        /// 读取未提醒给值班人员的工单[读取后会将标识设为已提醒]
        /// </summary>
        public DataSet GetUnRemindWorkOrders()
        {
            var ds= GetList(100, "pushedremind=0 and status=0 and progress=0", "id ASC");
            var ids = DbHelperSQL.ToList<MDL.jmp_workorder>(ds.Tables[0]).Select(x=>x.id);
            var enumerable = ids as int[] ?? ids.ToArray();
            if (!enumerable.Any()) return ds;
            var update =
                string.Format("UPDATE jmp_workorder SET pushedremind=1,pushreminddate=GETDATE() WHERE id IN({0})",
                    string.Join(",", enumerable));
            DbHelperSQL.ExecuteSql(update);
            return ds;
        }
        public List<JMP.MDL.jmp_workorderReport> Getlist( string sea_name, int searchDesc,  string stime, string endtime, int pageIndexs, int PageSize, out int pageCount)
        {
           string sql = string.Format(" select sum (countworkorder) as countworkorder   , sum(sucessworkorder) as sucessworkorder ,sum(branch) as branch,sum(socre) as socre, u_realname  from ( select count(1) as countworkorder, 0 sucessworkorder, 0 branch, 0 socre, b.u_realname  from jmp_workorder a left join jmp_locuser b on a.watchidsoftheday = b.u_id where CONVERT(varchar(100), createdon, 23) >= '"+stime+ "' and CONVERT(varchar(100), createdon, 23) <=  '" + endtime + "' group by  b.u_realname  union all select   0 countworkorder, count(1) as sucessworkorder, 0 branch, 0 socre, b.u_realname from jmp_workorder a  left join jmp_locuser b on a.watchidsoftheday = b.u_id where CONVERT(varchar(100), createdon, 23) >=  '" + stime + "' and CONVERT(varchar(100), createdon, 23) <=  '" + endtime + "' and  status = 0 and progress>= 2  group by  b.u_realname union all select   0 countworkorder, 0 sucessworkorder, avg(DATEDIFF(n, createdon, resultDate))branch, 0 socre, b.u_realname from jmp_workorder a left join jmp_locuser b on a.watchidsoftheday = b.u_id where CONVERT(varchar(100), createdon, 23) >=  '" + stime + "' and CONVERT(varchar(100), createdon, 23) <=  '" + endtime + "'   group by b.u_realname  union all select   0 countworkorder, 0 sucessworkorder, 0 branch, avg(score) socre, b.u_realname from jmp_workorder a left join jmp_locuser b on a.watchidsoftheday = b.u_id where CONVERT(varchar(100), createdon, 23) >=  '" + stime + "' and CONVERT(varchar(100), createdon, 23) <= '" + endtime + "' and score!=0 group by b.u_realname) as workorder  where 1 = 1");
            if(!string.IsNullOrEmpty(sea_name))
            {
                sql += " and  u_realname like '%" + sea_name + "%' "; 
            }
            sql += "group by  u_realname";

            string Order = "  order by u_realname ";
            if (searchDesc == 1)
            {
                Order = " order by u_realname  ";
            }
            else
            {
                Order = " order by u_realname desc ";
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_workorderReport>(ds.Tables[0]);
        }

    }
}

