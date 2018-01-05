using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.MDL;

namespace JMP.DAL
{
    public partial class jmp_apprate
    {
        public bool Exists(int r_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from jmp_apprate");
            strSql.Append(" where ");
            strSql.Append(" r_id = @r_id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)          };
            parameters[0].Value = r_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(JMP.MDL.jmp_apprate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jmp_apprate(");
            strSql.Append("r_id,r_appid,r_paymodeid,r_proportion,r_state,r_time,r_name");
            strSql.Append(") values (");
            strSql.Append("@r_id,@r_appid,@r_paymodeid,@r_proportion,@r_state,@r_time,@r_name");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@r_id", SqlDbType.Int,4) ,
                        new SqlParameter("@r_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@r_paymodeid", SqlDbType.Int,4) ,
                        new SqlParameter("@r_proportion", SqlDbType.Decimal,9) ,
                        new SqlParameter("@r_state", SqlDbType.Int,4) ,
                        new SqlParameter("@r_time", SqlDbType.DateTime) ,
                        new SqlParameter("@r_name", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_appid;
            parameters[2].Value = model.r_paymodeid;
            parameters[3].Value = model.r_proportion;
            parameters[4].Value = model.r_state;
            parameters[5].Value = model.r_time;
            parameters[6].Value = model.r_name;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_apprate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update jmp_apprate set ");

            strSql.Append(" r_id = @r_id , ");
            strSql.Append(" r_appid = @r_appid , ");
            strSql.Append(" r_paymodeid = @r_paymodeid , ");
            strSql.Append(" r_proportion = @r_proportion , ");
            strSql.Append(" r_state = @r_state , ");
            strSql.Append(" r_time = @r_time , ");
            strSql.Append(" r_name = @r_name  ");
            strSql.Append(" where r_id=@r_id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@r_id", SqlDbType.Int,4) ,
                        new SqlParameter("@r_appid", SqlDbType.Int,4) ,
                        new SqlParameter("@r_paymodeid", SqlDbType.Int,4) ,
                        new SqlParameter("@r_proportion", SqlDbType.Decimal,9) ,
                        new SqlParameter("@r_state", SqlDbType.Int,4) ,
                        new SqlParameter("@r_time", SqlDbType.DateTime) ,
                        new SqlParameter("@r_name", SqlDbType.NVarChar,50)

            };

            parameters[0].Value = model.r_id;
            parameters[1].Value = model.r_appid;
            parameters[2].Value = model.r_paymodeid;
            parameters[3].Value = model.r_proportion;
            parameters[4].Value = model.r_state;
            parameters[5].Value = model.r_time;
            parameters[6].Value = model.r_name;
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
            strSql.Append("delete from jmp_apprate ");
            strSql.Append(" where r_id=@r_id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)          };
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
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_apprate GetModel(int r_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select r_id, r_appid, r_paymodeid, r_proportion, r_state, r_time, r_name  ");
            strSql.Append("  from jmp_apprate ");
            strSql.Append(" where r_id=@r_id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@r_id", SqlDbType.Int,4)          };
            parameters[0].Value = r_id;


            JMP.MDL.jmp_apprate model = new JMP.MDL.jmp_apprate();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["r_id"].ToString() != "")
                {
                    model.r_id = int.Parse(ds.Tables[0].Rows[0]["r_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_appid"].ToString() != "")
                {
                    model.r_appid = int.Parse(ds.Tables[0].Rows[0]["r_appid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_paymodeid"].ToString() != "")
                {
                    model.r_paymodeid = int.Parse(ds.Tables[0].Rows[0]["r_paymodeid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_proportion"].ToString() != "")
                {
                    model.r_proportion = decimal.Parse(ds.Tables[0].Rows[0]["r_proportion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_state"].ToString() != "")
                {
                    model.r_state = int.Parse(ds.Tables[0].Rows[0]["r_state"].ToString());
                }
                if (ds.Tables[0].Rows[0]["r_time"].ToString() != "")
                {
                    model.r_time = DateTime.Parse(ds.Tables[0].Rows[0]["r_time"].ToString());
                }
                model.r_name = ds.Tables[0].Rows[0]["r_name"].ToString();

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
            strSql.Append(" FROM jmp_apprate ");
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
            strSql.Append(" FROM jmp_apprate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据用户id查询相关信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_apprate> SelectListAppid(int userid)
        {
            string sql = string.Format(" WITH t AS (SELECT * FROM jmp_apprate AS r WHERE r.r_appid =@userid )SELECT a.p_id,a.p_name,ISNULL(t.r_proportion, 0) AS r_proportion, ISNULL(t.r_paymodeid, 0) AS r_paymodeid FROM jmp_paymode as a LEFT JOIN t ON a.p_id = T.r_paymodeid ");
            SqlParameter par = new SqlParameter("@userid", SqlDbType.Int, 4);
            par.Value = userid;
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql, par).Tables[0];
            return DbHelperSQL.ToList<JMP.MDL.jmp_apprate>(dt);
        }

        /// <summary>
        /// 手续费设置
        /// </summary>
        /// <param name="cmdList">sql语句集合</param>
        /// <returns></returns>
        public int InserSxF(string[] list)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();

            for (int i = 0; i < list.Length; i++)
            {
                CommandInfo cm = new CommandInfo();
                cm.CommandText = list[i].ToString();
                cmdList.Add(cm);
            }
            int cg = DbHelperSQL.ExecuteSqlTran(cmdList);
            return cg;
        }
        /// <summary>
        /// 根据应用id和支付类型判断是否对该支付类型设置汇率
        /// </summary>
        /// <param name="Appid">应用id</param>
        /// <param name="PayType">支付类型</param>
        /// <returns></returns>
        public JMP.MDL.jmp_apprate SelectAppidState(int Appid, int PayType)
        {
            string sql = string.Format(" select   *  from  [dbo].[jmp_apprate]  where r_appid=@r_appid  and  r_state=0  and r_paymodeid=@r_paymodeid and r_proportion>0 ");
            SqlParameter[] sqlParameter = {
                               new SqlParameter("r_appid",SqlDbType.Int,4),
                               new SqlParameter("r_paymodeid",SqlDbType.Int,4)
            };
            sqlParameter[0].Value = Appid;
            sqlParameter[1].Value = PayType;
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sql, sqlParameter).Tables[0];
            return DbHelperSQL.ToModel<JMP.MDL.jmp_apprate>(dt);
        }
    }
}
