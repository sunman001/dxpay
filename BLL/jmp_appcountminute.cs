using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.BLL
{   ///<summary>
    ///每日应用汇总按10分钟统计
    ///</summary>
    public partial class jmp_appcountminute
    {

        private readonly JMP.DAL.jmp_appcountminute dal = new JMP.DAL.jmp_appcountminute();
        public jmp_appcountminute()
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
        public int Add(JMP.MDL.jmp_appcountminute model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_appcountminute model)
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
        public JMP.MDL.jmp_appcountminute GetModel(int a_id)
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
        public List<JMP.MDL.jmp_appcountminute> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_appcountminute> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_appcountminute> modelList = new List<JMP.MDL.jmp_appcountminute>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_appcountminute model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_appcountminute();
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
                    if (dt.Rows[n]["a_unionpay"].ToString() != "")
                    {
                        model.a_unionpay = decimal.Parse(dt.Rows[n]["a_unionpay"].ToString());
                    }
                    if (dt.Rows[n]["a_money"].ToString() != "")
                    {
                        model.a_money = decimal.Parse(dt.Rows[n]["a_money"].ToString());
                    }
                    if (dt.Rows[n]["a_qqwallet"].ToString() != "")
                    {
                        model.a_qqwallet = decimal.Parse(dt.Rows[n]["a_qqwallet"].ToString());
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
        #endregion


        /// <summary>
        /// 获取当天的报表（应用和用户）
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public DataTable GetTodayList(string sql, string OrderBy, int PageIndex, int PageSize, out int Count)
        {
            return dal.GetTodayList(sql, OrderBy, PageIndex, PageSize, out Count);
        }

        /// <summary>
        /// 根据sql语句查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable CountSect(string sql)
        {
            return dal.CountSect(sql);
        }
    }
}
