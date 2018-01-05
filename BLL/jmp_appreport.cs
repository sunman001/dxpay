using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
using JMP.DBA;

namespace JMP.BLL
{
    ///<summary>
    ///每日应用汇总按天
    ///</summary>
    public partial class jmp_appreport
    {

        private readonly JMP.DAL.jmp_appreport dal = new JMP.DAL.jmp_appreport();
        public jmp_appreport()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int a_id)
        {
            return dal.Exists(a_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_appreport model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_appreport model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int a_id)
        {

            return dal.Delete(a_id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string a_idlist)
        {
            return dal.DeleteList(a_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_appreport GetModel(int a_id)
        {

            return dal.GetModel(a_id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_appreport> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_appreport> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_appreport> modelList = new List<JMP.MDL.jmp_appreport>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_appreport model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_appreport();
                    if (dt.Rows[n]["a_id"].ToString() != "")
                    {
                        model.a_id = int.Parse(dt.Rows[n]["a_id"].ToString());
                    }
                    if (dt.Rows[n]["a_successratio"].ToString() != "")
                    {
                        model.a_successratio = decimal.Parse(dt.Rows[n]["a_successratio"].ToString());
                    }
                    if (dt.Rows[n]["a_alipay"].ToString() != "")
                    {
                        model.a_alipay = decimal.Parse(dt.Rows[n]["a_alipay"].ToString());
                    }
                    if (dt.Rows[n]["a_wechat"].ToString() != "")
                    {
                        model.a_wechat = decimal.Parse(dt.Rows[n]["a_wechat"].ToString());
                    }
                    if (dt.Rows[n]["a_curr"].ToString() != "")
                    {
                        model.a_curr = decimal.Parse(dt.Rows[n]["a_curr"].ToString());
                    }
                    if (dt.Rows[n]["a_arpur"].ToString() != "")
                    {
                        model.a_arpur = decimal.Parse(dt.Rows[n]["a_arpur"].ToString());
                    }
                    if (dt.Rows[n]["a_datetime"].ToString() != "")
                    {
                        model.a_datetime = DateTime.Parse(dt.Rows[n]["a_datetime"].ToString());
                    }
                    if (dt.Rows[n]["a_time"].ToString() != "")
                    {
                        model.a_time = DateTime.Parse(dt.Rows[n]["a_time"].ToString());
                    }
                    model.a_appname = dt.Rows[n]["a_appname"].ToString();
                    if (dt.Rows[n]["a_appid"].ToString() != "")
                    {
                        model.a_appid = int.Parse(dt.Rows[n]["a_appid"].ToString());
                    }
                    if (dt.Rows[n]["a_uerid"].ToString() != "")
                    {
                        model.a_uerid = int.Parse(dt.Rows[n]["a_uerid"].ToString());
                    }
                    if (dt.Rows[n]["a_equipment"].ToString() != "")
                    {
                        model.a_equipment = decimal.Parse(dt.Rows[n]["a_equipment"].ToString());
                    }
                    if (dt.Rows[n]["a_count"].ToString() != "")
                    {
                        model.a_count = decimal.Parse(dt.Rows[n]["a_count"].ToString());
                    }
                    if (dt.Rows[n]["a_success"].ToString() != "")
                    {
                        model.a_success = decimal.Parse(dt.Rows[n]["a_success"].ToString());
                    }
                    if (dt.Rows[n]["a_notpay"].ToString() != "")
                    {
                        model.a_notpay = decimal.Parse(dt.Rows[n]["a_notpay"].ToString());
                    }
                    if (dt.Rows[n]["a_request"].ToString() != "")
                    {
                        model.a_request = decimal.Parse(dt.Rows[n]["a_request"].ToString());
                    }
                    if (dt.Rows[n]["a_money"].ToString() != "") {
                        model.a_money = decimal.Parse(dt.Rows[n]["a_money"].ToString());
                    }
                    if (dt.Rows[n]["a_qqwallet"].ToString() != "")
                    {
                        model.a_qqwallet = decimal.Parse(dt.Rows[n]["a_qqwallet"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表（营业概况）
        /// </summary>
        /// <param name="sdate">开始日期</param>
        /// <param name="eate">结束日期</param>
        /// <param name="sumTypes">汇总方式（应用名称、开发者邮箱）</param>
        /// <param name="sumkeys">汇总值</param>
        /// <returns></returns>
        public DataTable GetListys(string sdate, string eate, string sumTypes, string sumkeys)
        {
            return dal.GetListys(sdate, eate, sumTypes, sumkeys);
        }

        /// <summary>
        /// 获取列表(营业概况开发者)
        /// </summary>
        /// <param name="s_time">开始日期</param>
        /// <param name="e_time">结束日期</param>
        /// <param name="u_id">用户id</param>
        /// <param name="a_id">应用id</param>
        /// <returns></returns>
        public DataTable GetListsUser(string s_time, string e_time, string u_id, string a_id)
        {
            return dal.GetListsUser(s_time, e_time, u_id, a_id);
        }

        #endregion

    }
}