using System;
using System.Collections.Generic;
using System.Data;
namespace JMP.BLL
{
    //订单表
    public partial class jmp_order
    {

        private readonly JMP.DAL.jmp_order dal = new JMP.DAL.jmp_order();
        public jmp_order()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int o_id)
        {
            return dal.Exists(o_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(JMP.MDL.jmp_order model, string TableName)
        {
            return dal.Add(model, TableName);

        }
        /// <summary>
        /// 添加订单表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOrder(JMP.MDL.jmp_order model)
        {
            return dal.AddOrder(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(JMP.MDL.jmp_order model, string TableName)
        {
            return dal.Update(model, TableName);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_order GetModelbycode(string o_code, string TableName)
        {

            return dal.GetModelCode(o_code, TableName);
        }
        /// <summary>
        /// 根据订单编号查询订单信息（支付接口专用，H5或收银台模式第二次请求时调用）
        /// </summary>
        /// <param name="o_code"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectCode(string o_code, string TableName)
        {
            return dal.SelectCode(o_code, TableName);
        }
        /// <summary>
        /// 根据商户订单号和应用id查询订单信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="bizcode">商户订单编号</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectOrderbizcode(int appid, string code, string bizcode, string TableName)
        {
            return dal.SelectOrderbizcode(appid, code, bizcode, TableName);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JMP.MDL.jmp_order GetModelbyid(int id, string TableName)
        {

            return dal.GetModelid(id, TableName);
        }


        /// <summary>
        /// 根据时间得到新增订单数据条数
        /// </summary>
        public object GetOrderNum(DateTime dt, string TableName)
        {

            return dal.getordernum(dt, TableName);
        }
        /// <summary>
        /// 得到成功订单数量
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public object SelectCG(DateTime dt, string TableName)
        {
            return dal.SelectCG(dt, TableName);
        }
        /// <summary>
        /// 得到支付成功订单最后一条的时间
        /// </summary>
        /// <returns></returns>
        public object SelectCgTimes(string TableName)
        {
            return dal.SelectCgTimes(TableName);
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
        public List<JMP.MDL.jmp_order> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<JMP.MDL.jmp_order> DataTableToList(DataTable dt)
        {
            List<JMP.MDL.jmp_order> modelList = new List<JMP.MDL.jmp_order>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JMP.MDL.jmp_order model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new JMP.MDL.jmp_order();
                    if (dt.Rows[n]["o_id"].ToString() != "")
                    {
                        model.o_id = int.Parse(dt.Rows[n]["o_id"].ToString());
                    }
                    model.o_code = dt.Rows[n]["o_code"].ToString();
                    model.o_bizcode = dt.Rows[n]["o_bizcode"].ToString();
                    model.o_tradeno = dt.Rows[n]["o_tradeno"].ToString();
                    model.o_paymode_id = dt.Rows[n]["o_paymode_id"].ToString();
                    if (dt.Rows[n]["o_app_id"].ToString() != "")
                    {
                        model.o_app_id = int.Parse(dt.Rows[n]["o_app_id"].ToString());
                    }

                    model.o_goodsname = dt.Rows[n]["o_goodsname"].ToString();

                    model.o_term_key = dt.Rows[n]["o_term_key"].ToString();
                    if (dt.Rows[n]["o_price"].ToString() != "")
                    {
                        model.o_price = decimal.Parse(dt.Rows[n]["o_price"].ToString());
                    }
                    model.o_payuser = dt.Rows[n]["o_payuser"].ToString();
                    if (dt.Rows[n]["o_ctime"].ToString() != "")
                    {
                        model.o_ctime = DateTime.Parse(dt.Rows[n]["o_ctime"].ToString());
                    }
                    if (dt.Rows[n]["o_ptime"].ToString() != "")
                    {
                        model.o_ptime = DateTime.Parse(dt.Rows[n]["o_ptime"].ToString());
                    }
                    if (dt.Rows[n]["o_state"].ToString() != "")
                    {
                        model.o_state = int.Parse(dt.Rows[n]["o_state"].ToString());
                    }

                    if (dt.Rows[n]["o_times"].ToString() != "")
                    {
                        model.o_times = int.Parse(dt.Rows[n]["o_times"].ToString());
                    }
                    model.o_address = dt.Rows[n]["o_address"].ToString();

                    if (dt.Rows[n]["o_noticestate"].ToString() != "")
                    {
                        model.o_noticestate = int.Parse(dt.Rows[n]["o_noticestate"].ToString());
                    }
                    if (dt.Rows[n]["o_noticetimes"].ToString() != "")
                    {
                        model.o_noticetimes = DateTime.Parse(dt.Rows[n]["o_noticetimes"].ToString());
                    }
                    model.o_privateinfo = dt.Rows[n]["o_privateinfo"].ToString();

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
        /// 根据生成的订单编号查询是否存在重复记录
        /// </summary>
        /// <param name="o_code">订单编号</param>
        /// <returns></returns>
        public bool Existss(string o_code)
        {
            return dal.Existss(o_code);
        }

        #endregion

        #region 报表模块--陶涛20160322
        /// <summary>
        /// 查询成功订单的交易量
        /// </summary>
        // <param name="tname">订单表名</param>
        /// <param name="times">查询日期（2016-03-18）</param>
        /// <returns></returns>
        public DataSet GetListReportOrderSuccess(string tname, string times)
        {
            return dal.GetListReportOrderSuccess(tname, times);
        }

        /// <summary>
        /// 查询所有订单的交易量
        /// </summary>
        /// <param name="tname">订单表名</param>
        /// <param name="times">查询日期（2016-03-18）</param>
        /// <returns></returns>
        public DataSet GetListReportOrder(string tname, string times)
        {
            return dal.GetListReportOrder(tname, times);
        }
        #endregion

        /// <summary>
        /// 实时监控
        /// </summary>
        /// <param name="tname">表名</param>
        /// <param name="rdate">查询日期（2016-05-05）</param>
        /// <param name="uid">用户id</param>
        /// <param name="aid">应用id</param>
        /// <returns></returns>
        public DataTable GetListRealTime(string tname, string rdate, string uid, string aid)
        {
            return dal.GetListRealTime(tname, rdate, uid, aid);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqls">SQL语句</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> SelectList(string sqls, int pageIndexs, int PageSize, out int pageCount)
        {
            return dal.SelectList(sqls, pageIndexs, PageSize, out pageCount);
        }
        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// 
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序类型（0：升序，1：降序）</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> SelectPager(string sql, string where, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.SelectPager(sql, where, Order, PageIndex, PageSize, out Count);
        }
        /// <summary>
        /// 根据sql查询信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> DcSelectList(string sql)
        {
            return dal.DcSelectList(sql);
        }
        /// <summary>
        /// 修改支付渠道id
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="o_id">订单id</param>
        /// <param name="o_interface_id">渠道id</param>
        /// <returns></returns>
        public bool UpdatePay(int o_id, int o_interface_id)
        {
            return dal.UpdatePay(o_id, o_interface_id);
        }

        /// <summary>
        /// 查询商务对应的成功订单的交易量
        /// </summary>
        /// <param name="tablename">订单表名</param>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetMerchantListReportOrderSuccess(string tablename, string t_time, int merchantId)
        {
            return dal.GetMerchantListReportOrderSuccess(tablename, t_time, merchantId);
        }

        ///// <summary>
        /// 查询商务对应的指定日期内所有订单的交易量
        /// </summary>
        /// <param name="tablename">订单表名</param>
        /// <param name="t_time">查询日期(2016-03-18)</param>
        /// <returns></returns>
        public DataSet GetMerchantListReportOrderAll(string tablename, string t_time, int merchantId)
        {
            return dal.GetMerchantListReportOrderAll(tablename, t_time, merchantId);
        }

        /// <summary>
        /// 根据订单id查询订单信息和商品名称
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="tname">订单表表名</param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectOrderGoodsName(int oid, string tname)
        {
            return dal.SelectOrderGoodsName(oid, tname);
        }

        /// <summary>
        /// 将订单状态标识修改为异常,值为:2
        /// <param name="tableName">订单表名</param>
        /// <param name="oCode">订单编码</param>
        /// </summary>
        public bool ChangeStateToAbnormal(string tableName, string oCode)
        {
            return dal.ChangeStateToAbnormal(tableName, oCode);
        }
        /// <summary>
        /// 根据订单号获取需要手动通知的信息
        /// </summary>
        /// <param name="o_code">订单号</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public JMP.MDL.jmp_order SelectOrder(string o_code, string TableName)
        {
            return dal.SelectOrder(o_code, TableName);
        }
        /// <summary>
        /// 修改支付类型
        /// </summary>
        /// <param name="o_id">订单表id</param>
        /// <param name="o_paymode_id">支付类型编号</param>
        /// <returns></returns>
        public bool UpdatePayMode(int o_id, int o_paymode_id)
        {
            return dal.UpdatePayMode(o_id, o_paymode_id);
        }
        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="Order">排序字段</param>
        /// <param name="OrderType">排序类型（0：升序，1：降序）</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页显示数量</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public List<JMP.MDL.jmp_order> SelectPagerYunY(string where, string BpWhere, string AgentWhere, string sql, string Order, int PageIndex, int PageSize, out int Count)
        {
            return dal.SelectPagerYunY(where, BpWhere, AgentWhere, sql, Order, PageIndex, PageSize, out Count);
        }

        /// <summary>
        /// 根据订单表名和订单号获取指定的订单实体
        /// </summary>
        /// <param name="tableName">订单表名</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public MDL.jmp_order FindOrderByTableNameAndOrderNo(string tableName, string orderNo)
        {
            return dal.FindOrderByTableNameAndOrderNo(tableName, orderNo);
        }

        /// <summary>
        /// 根据订单表名和订单号获取指定的订单实体(包含实时订单表数据)
        /// </summary>
        /// <param name="tableName">归档订单表名</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public MDL.jmp_order FindOrderByTableNameAndOrderNoIncludeRealtime(string tableName, string orderNo)
        {
            return dal.FindOrderByTableNameAndOrderNoIncludeRealtime(tableName, orderNo);
        }
    }
}