using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JMP.DBA;
using JMP.Model.Query;

namespace JMP.DAL
{
    ///<summary>
    ///支付接口配置表
    ///</summary>
    public partial class jmp_interface
    {
        DataTable dt = new DataTable();

        public bool Exists(int l_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_interface");
            strSql.Append(" where ");
            strSql.Append(" l_id = @l_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_interface model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_interface(");
            strSql.Append("l_risk,l_daymoney,l_time,l_minimum,l_maximum,l_CostRatio,l_str,l_sort,l_isenable,l_paymenttype_id,l_apptypeid,l_corporatename,l_priority,l_jsonstr");
            strSql.Append(") values (");
            strSql.Append("@l_risk,@l_daymoney,@l_time,@l_minimum,@l_maximum,@l_CostRatio,@l_str,@l_sort,@l_isenable,@l_paymenttype_id,@l_apptypeid,@l_corporatename,@l_priority,@l_jsonstr");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@l_risk", SqlDbType.Int,4) ,
                        new SqlParameter("@l_daymoney", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_time", SqlDbType.DateTime) ,
                        new SqlParameter("@l_minimum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_maximum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_CostRatio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_str", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_sort", SqlDbType.Int,4) ,
                        new SqlParameter("@l_isenable", SqlDbType.Int,4) ,
                        new SqlParameter("@l_paymenttype_id", SqlDbType.Int,4) ,
                        new SqlParameter("@l_apptypeid", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_corporatename", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_priority", SqlDbType.Int,4) ,
                        new SqlParameter("@l_jsonstr", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.l_risk;
            parameters[1].Value = model.l_daymoney;
            parameters[2].Value = model.l_time;
            parameters[3].Value = model.l_minimum;
            parameters[4].Value = model.l_maximum;
            parameters[5].Value = model.l_CostRatio;
            parameters[6].Value = model.l_str;
            parameters[7].Value = model.l_sort;
            parameters[8].Value = model.l_isenable;
            parameters[9].Value = model.l_paymenttype_id;
            parameters[10].Value = model.l_apptypeid;
            parameters[11].Value = model.l_corporatename;
            parameters[12].Value = model.l_priority;
            parameters[13].Value = model.l_jsonstr;

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
        public bool Update(JMP.MDL.jmp_interface model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_interface set ");

            strSql.Append(" l_risk = @l_risk , ");
            strSql.Append(" l_daymoney = @l_daymoney , ");
            strSql.Append(" l_time = @l_time , ");
            strSql.Append(" l_minimum = @l_minimum , ");
            strSql.Append(" l_maximum = @l_maximum , ");
            strSql.Append(" l_CostRatio = @l_CostRatio , ");
            strSql.Append(" l_str = @l_str , ");
            strSql.Append(" l_sort = @l_sort , ");
            strSql.Append(" l_isenable = @l_isenable , ");
            strSql.Append(" l_paymenttype_id = @l_paymenttype_id , ");
            strSql.Append(" l_apptypeid = @l_apptypeid , ");
            strSql.Append(" l_corporatename = @l_corporatename , ");
            strSql.Append(" l_priority = @l_priority , ");
            strSql.Append(" l_jsonstr = @l_jsonstr  ");
            strSql.Append(" where l_id=@l_id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@l_id", SqlDbType.Int,4) ,
                        new SqlParameter("@l_risk", SqlDbType.Int,4) ,
                        new SqlParameter("@l_daymoney", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_time", SqlDbType.DateTime) ,
                        new SqlParameter("@l_minimum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_maximum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_CostRatio", SqlDbType.Decimal,9) ,
                        new SqlParameter("@l_str", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_sort", SqlDbType.Int,4) ,
                        new SqlParameter("@l_isenable", SqlDbType.Int,4) ,
                        new SqlParameter("@l_paymenttype_id", SqlDbType.Int,4) ,
                        new SqlParameter("@l_apptypeid", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_corporatename", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@l_priority", SqlDbType.Int,4) ,
                        new SqlParameter("@l_jsonstr", SqlDbType.NVarChar,-1)

            };

            parameters[0].Value = model.l_id;
            parameters[1].Value = model.l_risk;
            parameters[2].Value = model.l_daymoney;
            parameters[3].Value = model.l_time;
            parameters[4].Value = model.l_minimum;
            parameters[5].Value = model.l_maximum;
            parameters[6].Value = model.l_CostRatio;
            parameters[7].Value = model.l_str;
            parameters[8].Value = model.l_sort;
            parameters[9].Value = model.l_isenable;
            parameters[10].Value = model.l_paymenttype_id;
            parameters[11].Value = model.l_apptypeid;
            parameters[12].Value = model.l_corporatename;
            parameters[13].Value = model.l_priority;
            parameters[14].Value = model.l_jsonstr;
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
        public bool Delete(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_interface ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


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
        public bool DeleteList(string l_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from jmp_interface ");
            strSql.Append(" where ID in (" + l_idlist + ")  ");
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
        public JMP.MDL.jmp_interface GetModel(int l_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select l_id, l_risk, l_daymoney, l_time, l_minimum, l_maximum, l_CostRatio, l_str, l_sort, l_isenable, l_paymenttype_id, l_apptypeid, l_corporatename, l_priority, l_jsonstr  ");
            strSql.Append("  from jmp_interface ");
            strSql.Append(" where l_id=@l_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@l_id", SqlDbType.Int,4)
            };
            parameters[0].Value = l_id;


            JMP.MDL.jmp_interface model = new JMP.MDL.jmp_interface();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["l_id"].ToString() != "")
                {
                    model.l_id = int.Parse(ds.Tables[0].Rows[0]["l_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_risk"].ToString() != "")
                {
                    model.l_risk = int.Parse(ds.Tables[0].Rows[0]["l_risk"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_daymoney"].ToString() != "")
                {
                    model.l_daymoney = decimal.Parse(ds.Tables[0].Rows[0]["l_daymoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_time"].ToString() != "")
                {
                    model.l_time = DateTime.Parse(ds.Tables[0].Rows[0]["l_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_minimum"].ToString() != "")
                {
                    model.l_minimum = decimal.Parse(ds.Tables[0].Rows[0]["l_minimum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_maximum"].ToString() != "")
                {
                    model.l_maximum = decimal.Parse(ds.Tables[0].Rows[0]["l_maximum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_CostRatio"].ToString() != "")
                {
                    model.l_CostRatio = decimal.Parse(ds.Tables[0].Rows[0]["l_CostRatio"].ToString());
                }
                model.l_str = ds.Tables[0].Rows[0]["l_str"].ToString();
                if (ds.Tables[0].Rows[0]["l_sort"].ToString() != "")
                {
                    model.l_sort = int.Parse(ds.Tables[0].Rows[0]["l_sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_isenable"].ToString() != "")
                {
                    model.l_isenable = int.Parse(ds.Tables[0].Rows[0]["l_isenable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["l_paymenttype_id"].ToString() != "")
                {
                    model.l_paymenttype_id = int.Parse(ds.Tables[0].Rows[0]["l_paymenttype_id"].ToString());
                }
                model.l_apptypeid = ds.Tables[0].Rows[0]["l_apptypeid"].ToString();
                model.l_corporatename = ds.Tables[0].Rows[0]["l_corporatename"].ToString();
                if (ds.Tables[0].Rows[0]["l_priority"].ToString() != "")
                {
                    model.l_priority = int.Parse(ds.Tables[0].Rows[0]["l_priority"].ToString());
                }
                model.l_jsonstr = ds.Tables[0].Rows[0]["l_jsonstr"].ToString();

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
            strSql.Append(" FROM jmp_interface ");
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
            strSql.Append(" FROM jmp_interface ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 查询支付配置信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">分页数量</param>
        /// <param name="pageCount">总数量</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_interface> SelectList(string sql, string Order, int pageIndexs, int PageSize, out int pageCount)
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
            return DbHelperSQL.ToList<JMP.MDL.jmp_interface>(ds.Tables[0]);
        }
        /// <summary>
        /// 根据id查询相关信息
        /// </summary>
        public JMP.MDL.jmp_interface GetModels(int l_id)
        {
            string sql = string.Format("select a .l_id, a .l_str, a .l_sort,a.l_corporatename, a .l_isenable, a .l_paymenttype_id,a.l_risk,b.p_name,b.p_type,b.p_extend,a.l_apptypeid,a.l_jsonstr ,a.l_daymoney,a.l_minimum,a.l_maximum  from  jmp_interface a  left join jmp_paymenttype b on b.p_id=a.l_paymenttype_id  where 1=1 and a .l_id=@l_id ");
            SqlParameter par = new SqlParameter("@l_id", l_id);

            dt = DbHelperSQL.Query(sql.ToString(), par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_interface>(dt);
        }
        /// <summary>
        /// 根据支付类型获取第一条支付配置信息
        /// </summary>
        /// <param name="type">支付类型</param>
        /// <returns></returns>
        public JMP.MDL.jmp_interface strzf(string type, int tid)
        {
            string sql = string.Format("select top 1 a.l_id, a.l_str, a.l_sort, a.l_isenable,a.l_corporatename, a.l_paymenttype_id,b.p_name,b.p_type,a.l_apptypeid,a.l_jsonstr  ,a.l_daymoney,a.l_minimum,a.l_maximum from  jmp_interface a  left join  jmp_paymenttype b on b.p_id=a.l_paymenttype_id  where 1=1 and b.p_extend=@type and l_isenable='1'  and  ','+l_apptypeid+',' like '%,'+cast((select t_topid from jmp_apptype where t_id=@tid) as varchar(20))+',%' order by l_sort");
            SqlParameter[] par ={ new SqlParameter("@type", type),
                                  new SqlParameter("@tid",tid)
                                };
            dt = DbHelperSQL.Query(sql.ToString(), par).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_interface>(dt);
        }

        /// <summary>
        /// 根据支付类型获取第一条支付配置信息
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        public string strzf_monitor(int interfaceId)
        {
            var sql = "SELECT JI.l_str FROM jmp_interface AS JI WHERE JI.l_id= @tid";
            SqlParameter[] par ={ new SqlParameter("@tid",interfaceId)
                                };
            var result = DbHelperSQL.GetSingle(sql, par).ToString();
            return result;
        }

        /// <summary>
        /// 根据支付类型获取支付通道配置信息
        /// </summary>
        /// <param name="type">支付类型</param>
        /// <param name="tid">风控配置表id</param>
        /// <param name="appid">应用表id</param>
        /// <returns></returns>
        public DataTable SelectPay(string type, int tid, int appid)
        {
            string sql = "";
            DataTable ddt = new DataTable();
            sql = string.Format(" WITH T1 AS( select  a.Id from[dbo].[jmp_channel_pool] as a inner join  jmp_channel_app_mapping as b  on a.Id = b.ChannelId where a.IsEnabled = 1 and b.AppId = " + appid + ") select a.l_id, a.l_str,ISNULL(l_maximum, 0) l_maximum,ISNULL(l_minimum, 0) l_minimum from  jmp_interface a left join  jmp_paymenttype b on b.p_id = a.l_paymenttype_id  left join T1 ON   ',' + l_apptypeid + ',' like '%,' + cast(T1.id as varchar(20)) + ',%' where a.l_risk =2 and b.p_extend = '" + type + "' and l_isenable = 1   and  ',' + l_apptypeid + ',' like '%,' + cast(T1.id as varchar(20)) + ',%' and a.l_sort =( SELECT  top 1 l_sort FROM  jmp_interface left join jmp_paymenttype  on jmp_paymenttype.p_id = l_paymenttype_id WHERE   ',' + l_apptypeid + ',' like '%,' + cast(T1.id as varchar(20)) + ',%' AND l_isenable = 1 and jmp_paymenttype.p_extend = '" + type + "'  ORDER BY l_sort)  group by a.l_id, a.l_str,a.l_maximum,a.l_minimum ");//根据通道池
            ddt = DbHelperSQL.Query(sql.ToString()).Tables[0];
            if (ddt.Rows.Count == 0)
            {
                sql = string.Format("  select  a.l_id, a.l_str,ISNULL(l_maximum,0) l_maximum,ISNULL(l_minimum,0) l_minimum  from  jmp_interface a   left join  jmp_paymenttype b on b.p_id = a.l_paymenttype_id   where a.l_risk=1 and b.p_extend =@type and l_isenable = 1   and  ',' + l_apptypeid + ',' like '%,' + cast(@tid as varchar(20)) + ',%' and a.l_sort = (SELECT  top 1 l_sort FROM  jmp_interface  left join  jmp_paymenttype  on jmp_paymenttype.p_id = l_paymenttype_id WHERE   ',' + l_apptypeid + ',' like '%,' + cast(@tid as varchar(20)) + ',%'  AND l_isenable = 1  and jmp_paymenttype.p_extend =@type  ORDER BY l_sort) ");//根据应用id
                SqlParameter[] par ={ new SqlParameter("@type", type),
                                      new SqlParameter("@tid",appid)
                                    };
                ddt = DbHelperSQL.Query(sql.ToString(), par).Tables[0];
                if (ddt.Rows.Count == 0)
                {
                    sql = string.Format("  select  a.l_id, a.l_str,ISNULL(l_maximum,0) l_maximum,ISNULL(l_minimum,0) l_minimum from  jmp_interface a   left join  jmp_paymenttype b on b.p_id = a.l_paymenttype_id   where a.l_risk=0 and b.p_extend =@type and l_isenable = 1   and  ',' + l_apptypeid + ',' like '%,' + cast(@tid as varchar(20)) + ',%' and a.l_sort = (SELECT  top 1 l_sort FROM  jmp_interface  left join  jmp_paymenttype  on jmp_paymenttype.p_id = l_paymenttype_id WHERE   ',' + l_apptypeid + ',' like '%,' + cast(@tid as varchar(20)) + ',%'  AND l_isenable = 1  and jmp_paymenttype.p_extend =@type  ORDER BY l_sort) ");//根据风控类型
                    SqlParameter[] pars ={ new SqlParameter("@type", type),
                                  new SqlParameter("@tid",tid)
                                };
                    ddt = DbHelperSQL.Query(sql.ToString(), pars).Tables[0];
                }
            }
            return ddt;
        }
        /// <summary>
        /// 根据应用id查询支付通道信息
        /// </summary>
        /// <param name="type">支付类型</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public DataTable selectAppid(string type, int appid)
        {
            string sql = string.Format(" select top 1  a.l_id, a.l_str from  jmp_interface a   left join  jmp_paymenttype b on b.p_id=a.l_paymenttype_id    where 1=1 and b.p_extend=@type   and l_isenable=1    and  ','+l_apptypeid+',' like '%,'+cast(@appid as varchar(20))+',%'  order by  l_sort ");
            SqlParameter[] par ={ new SqlParameter("@type", type),
                                  new SqlParameter("@appid",appid)
                                };
            dt = DbHelperSQL.Query(sql.ToString(), par).Tables[0];
            return dt;
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="u_idlist">多个(1,2,3,4,5)</param>
        /// <param name="state">更新状态</param>
        /// <returns></returns>
        public bool UpdateLocUserState(string u_idlist, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update jmp_interface set l_isenable=" + state + "  ");
            strSql.Append(" where l_id in (" + u_idlist + ")  ");
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
        /// 根据id字符传查询除此之外的数据用于冻结判断
        /// </summary>
        /// <param name="str">id字符串</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public DataTable SelectDataTable(string str, int type)
        {
            string sql = string.Format(" select a.* from  jmp_interface a left join jmp_paymenttype b on a.l_paymenttype_id=b.p_id where l_id not in (" + str + ") and b.p_type='" + type + "' and l_isenable='1' ");
            return DbHelperSQL.Query(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取今日已使用过的支付通道实体列表
        /// </summary>
        /// <param name="tableName">当前时间对应的订单表名</param>
        /// <returns></returns>
        public List<OrderedInterface> GetTodayOrderedInterfaces(string tableName)
        {
            var sql = string.Format(@"WITH T AS(
SELECT O.o_interface_id,COUNT(1) AS OrderTotal FROM {0} AS O 
WHERE O.o_paymode_id=1 AND O.o_state=1 AND O.o_ctime BETWEEN '{1} 00:00:00' AND '{1} 23:59:59'
GROUP BY O.o_interface_id
)
SELECT T.o_interface_id AS InterfaceId, JI.l_corporatename AS InterfaceName,T.OrderTotal,JI.l_isenable AS IsEnabled FROM T 
LEFT JOIN jmp_interface AS JI ON JI.l_id=t.o_interface_id", tableName, DateTime.Now.ToString("yyyy-MM-dd"));
            var dataTable = DbHelperSQL.Query(sql).Tables[0];
            var lst = new List<OrderedInterface>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                lst = dataTable.AsEnumerable().Select(x => new OrderedInterface
                {
                    InterfaceId = x.Field<int>("InterfaceId"),
                    InterfaceName = x.Field<string>("InterfaceName"),
                    OrderTotal = x.Field<int>("OrderTotal"),
                    IsEnabled = x.Field<int>("IsEnabled") == 1
                }).ToList();
            }
            return lst;
        }


        /// <summary>
        /// 根据支付ID获取今日订单数量
        /// </summary>
        /// <param name="tableName">当前时间对应的订单表名</param>
        /// <returns></returns>
        public int GetTodayOrderedInterfaces_byid(string tableName)
        {
            var sql = string.Format(@"SELECT COUNT(1) FROM {0} WHERE o_paymode_id=1 AND o_state=1 AND o_ptime>'{1}'", tableName, DateTime.Now.AddMinutes(-15).ToString("yyyy-MM-dd"));
            var sl = DbHelperSQL.GetSingle(sql);
            if (sl == null) sl = 0;
            return Convert.ToInt32(sl);
        }

        /// <summary>
        /// 修改通道成本费率
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="costratio">费率</param>
        /// <returns></returns>
        public bool UpdateInterfaceCostRatio(int id, string costratio)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" update jmp_interface set l_CostRatio='" + costratio + "' ");
            sql.Append(" where l_id=" + id + "");

            int num = DbHelperSQL.ExecuteSql(sql.ToString());

            if (num > 0)
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

