using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.DAL;
using JMP.MDL;
using JMP.Model.Query;

namespace JMP.BLL
{
    ///<summary>
    ///每日应用汇总
    ///</summary>
    public partial class jmp_appcount
    {

        private readonly JMP.DAL.jmp_appcount dal = new JMP.DAL.jmp_appcount();
        public jmp_appcount()
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
        public int Add(JMP.MDL.jmp_appcount model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_appcount model)
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
        public JMP.MDL.jmp_appcount GetModel(int a_id)
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
        public List<JMP.MDL.jmp_appcount> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_appcount> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_appcount> modelList = new List<JMP.MDL.jmp_appcount>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_appcount model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_appcount();
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

        /// <summary>
        /// 查询订单每天走势图（设备量、成功量、请求量）
        /// </summary>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetListReportOrderSuccess(string t_time, string dept, int userid, int RoleID)
        {
            return dal.GetListReportOrderSuccess(t_time, dept, userid, RoleID);
        }


        /// <summary>
        /// 查询订单每天走势图（设备量、成功量、请求量）
        /// </summary>
        /// <param name="t_time">查询日期</param>
        /// <param name="u_merchant_id">商务ID</param>
        /// <returns></returns>

        public DataSet GetListReportOrderSuccessById(string t_time, int u_merchant_id)
        {
            return dal.GetListReportOrderSuccessByID(t_time, u_merchant_id);
        }
        /// <summary>
        /// 查询今天截止目前的交易金额合计
        /// </summary>
        /// <returns></returns>
        public string SelectSumDay(string dept, int userid)
        {
            return dal.SelectSumDay(dept, userid);
        }
        /// <summary>
        /// 查询前三天平均交易量
        /// </summary>
        /// <param name="kst_time">开始日期（2016-03-18）</param>
        /// <param name="jst_time">结束日期（2016-03-18）</param>
        /// <param name="userid">用户id（用于开发者平台查询使用）</param>
        /// <returns></returns>
        public DataSet GetListAverage(string kst_time, string jst_time, string dept, int userid, int RoleID)
        {
            return dal.GetListAverage(kst_time, jst_time, dept, userid, RoleID);
        }
        /// <summary>
        /// 根据商务id查询前三天平均交易量
        /// </summary>
        /// <param name="kst_time">开始日期（2016-03-18）</param>
        /// <param name="jst_time">结束日期（2016-03-18）</param>
        /// <param name="swid">商务id</param>
        /// <returns></returns>
        public DataSet GetListAverageSw(string kst_time, string jst_time, int swid)
        {
            return dal.GetListAverageSw(kst_time, jst_time, swid);
        }
        /// <summary>
        /// 根据用户查询交易数据
        /// </summary>
        /// <param name="t_time">查询时间</param>
        /// <param name="u_id">用户id</param>
        /// <returns></returns>
        public DataTable GetListDay(string t_time, int u_id)
        {
            return dal.GetListDay(t_time, u_id);
        }

        /// <summary>
        /// 根据商户今日业绩
        /// </summary>
        /// <param name="merchantId">商务ID</param>
        /// <returns></returns>
        public string GetTodayResults(int merchantId, string kstime, string endtime)
        {
            return dal.TodayResults(merchantId, kstime, endtime);
        }
        /// <summary>
        /// 查询今日成功金额
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public object dayCj(int appid)
        {
            return dal.dayCj(appid);
        }
        /// <summary>
        /// 查询当日有订单的所有应用的支付衰减详情(与前三天数据比较)
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<AppPaySuccessAttenuation> GetTodayAppPaySuccessAttenuation(DateTime today)
        {
            return dal.GetTodayAppPaySuccessAttenuation(today);
        }


        /// <summary>
        /// 根据开发者ID查询统计图像报表
        /// </summary>
        /// <param name="uid">开发者ID</param>
        /// <param name="startTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="startTimeAdy">三日数据开始日期</param>
        /// <param name="endTimeAdy">三日数据结束日期</param>
        /// <returns></returns>
        public DataSet DataStatisticsAdy(int uid, string startTime, string endTime, string startTimeAdy, string endTimeAdy)
        {
            return dal.DataStatisticsAdy(uid, startTime, endTime, startTimeAdy, endTimeAdy);
        }
        /// <summary>
        /// 根据用户ID查询交易金额和交易笔数
        /// </summary>
        /// <param name="t_time">日期</param>
        /// <param name="u_id">用户ID</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public JMP.MDL.jmp_appcount DataAppcountAdy(string t_time, int u_id, int start)
        {
            return dal.DataAppcountAdy(t_time, u_id, start);
        }
    }
}