using DxPay.LogManager.LogFactory.ApiLog;
using System;

namespace JMALI.notice
{
    /// <summary>
    /// 通知接口公共方法
    /// </summary>
    public class notice
    {
        /// <summary>
        /// 根据通道id查询通道信息
        /// </summary>
        /// <param name="pid">通道id</param>
        /// <returns>返回通道key值</returns>
        public string SelectKey(int pid)
        {
            string key = "";
            try
            {
                var zf = new JMP.BLL.jmp_interface();
                string str = zf.strzf_monitor(pid);//获取通道key值
                if (!string.IsNullOrEmpty(str))
                {
                    key = str.Split(',')[1];
                }
                return key;
            }
            catch (Exception ex)
            {

                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("通道id：" + pid + "，错误信息：" + ex.ToString(), summary: "通知接口查询通道信息错误", channelId: pid);
                return key;
            }
        }
        /// <summary>
        /// 根据通道id查询融梦通道信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string SelecRmHdKey(int pid)
        {
            string key = "";
            try
            {
                var zf = new JMP.BLL.jmp_interface();
                string str = zf.strzf_monitor(pid);//获取通道key值
                if (!string.IsNullOrEmpty(str))
                {
                    key = str.Split(',')[3];
                }
                return key;
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "通知接口查询通道信息错误", "通道id：" + pid + "，错误信息：" + ex.ToString());//写入报错日志
                PayNotifyGlobalErrorLogger.Log("通道id：" + pid + "，错误信息：" + ex.ToString(), summary: "通知接口查询通道信息错误");
                return key;
            }
        }
        /// <summary> 
        /// 获取通道账号
        /// </summary>
        /// <param name="pid">通道id</param>
        /// <returns></returns>
        public string SelectOrder(int pid)
        {
            string Order = "";
            try
            {
                var zf = new JMP.BLL.jmp_interface();
                string str = zf.strzf_monitor(pid);//获取通道账号值
                if (!string.IsNullOrEmpty(str))
                {
                    Order = str.Split(',')[0];
                }
                return Order;
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "通知接口查询通道信息错误", "通道id：" + pid + "，错误信息：" + ex.ToString());//写入报错日志
                PayNotifyGlobalErrorLogger.Log("通道id：" + pid + "，错误信息：" + ex.ToString(), summary: "通知接口查询通道信息错误");
                return Order;
            }
        }
        /// <summary>
        /// 通知接口外部调用公共方式（主入口）
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额（单位：元）</param>
        /// <param name="gmt_payment">支付时间</param>
        /// <param name="trade_no">第三方流水号</param>
        /// <param name="payuser">付款账号</param>
        /// <param name="channelId">通道id</param>
        /// <param name="tname">接口名称</param>
        /// <param name="noticestr">传入参数字符串</param>
        /// <returns></returns>
        public string PubNotice(string code, decimal price, DateTime gmt_payment, string trade_no, string payuser, int channelId, string tname, string noticestr)
        {
            string message = "";
            try
            {
                JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                string orderTableName = JMP.TOOL.WeekDateTime.GetOrderTableName(gmt_payment.ToString());//获取订单表名
                morder = SelectOrder(code, noticestr);//获取订单信息
                if (morder != null)
                {
                    if (price == morder.o_price)
                    {
                        int payid = !string.IsNullOrEmpty(morder.o_paymode_id) ? Int32.Parse(morder.o_paymode_id) : 0;
                        if (JMP.TOOL.OrderProportion.ddbl(morder.o_app_id, price, payid))//调单设置
                        {
                            message = "ok";
                        }
                        else
                        {
                            if (morder.o_interface_id != channelId)
                            {
                                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("订单号：" + code + ",我们记录的通道id：" + morder.o_interface_id + ",上游获取回来的id：" + channelId + ",订单表id：" + morder.o_id, summary: tname + "通知接口错误，串单了", channelId: channelId);
                            }
                            if (morder.o_state != 1)
                            {
                                if (UpdateOrder(morder, trade_no, gmt_payment, price, payuser, noticestr, channelId))
                                {
                                    message = addqueuelist(morder, orderTableName, tname, noticestr);
                                }
                            }
                            else
                            {
                                message = "ok";
                            }
                        }
                    }
                    else
                    {
                        message = AddAuditor(morder, orderTableName, trade_no, gmt_payment, price, noticestr);
                    }
                }
                else
                {

                    PayApiDetailErrorLogger.UpstreamNotifyErrorLog("订单号：" + code + ",未查询到相关信息！,传入参数：" + noticestr, summary: tname + "通知接口错误", channelId: channelId);
                    message = "error";
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("通知接口主入口出错，订单号：" + code + ",传入参数：" + noticestr + ",错误信息：" + ex.ToString(), summary: tname + "通知接口错误", channelId: channelId);
                message = "error";
            }
            return message;
        }
        /// <summary>
        /// 通知接口外部调用公共方法（主入口，不判断金额以上游通知的金额为准）
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额（单位：元）</param>
        /// <param name="gmt_payment">支付时间</param>
        /// <param name="trade_no">第三方流水号</param>
        /// <param name="payuser">付款账号</param>
        /// <param name="tname">接口名称</param>
        /// <param name="noticestr">传入参数字符串</param>
        /// <param name="channelId">通道id</param>
        /// <returns></returns>
        public string PubNoticeMoney(string code, decimal price, DateTime gmt_payment, string trade_no, string payuser, string tname, string noticestr, int channelId)
        {
            string message = "";
            try
            {
                JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                string orderTableName = JMP.TOOL.WeekDateTime.GetOrderTableName(gmt_payment.ToString());//获取订单表名
                morder = SelectOrder(code, noticestr);//获取订单信息
                if (morder != null)
                {
                    if (price < (morder.o_price - 2) || price > (morder.o_price + 2) || price <= 0)
                    {
                        return message = AddAuditor(morder, orderTableName, trade_no, gmt_payment, price, noticestr);
                    }
                    else
                    {
                        morder.o_price = price;//以上游通知的金额为准
                        int payid = !string.IsNullOrEmpty(morder.o_paymode_id) ? Int32.Parse(morder.o_paymode_id) : 0;
                        if (JMP.TOOL.OrderProportion.ddbl(morder.o_app_id, price, payid))//调单设置
                        {
                            message = "ok";
                        }
                        else
                        {
                            if (morder.o_state != 1)
                            {
                                if (UpdateOrder(morder, trade_no, gmt_payment, price, payuser, noticestr, channelId))
                                {
                                    message = addqueuelist(morder, orderTableName, tname, noticestr);
                                }
                            }
                            else
                            {
                                message = "ok";
                            }
                        }
                    }
                }
                else
                {
                    // AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, tname + "通知接口错误", "订单号：" + code + ",未查询到相关信息！,传入参数：" + noticestr);//写入报错日志
                    PayNotifyGlobalErrorLogger.Log("订单号：" + code + ",未查询到相关信息！,传入参数：" + noticestr, summary: tname + "通知接口错误");
                    message = "error";
                }
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, tname + "通知接口错误", "通知接口主入口出错，订单号：" + code + ",传入参数：" + noticestr + ",错误信息：" + ex.ToString());//写入报错日志
                PayNotifyGlobalErrorLogger.Log("通知接口主入口出错，订单号：" + code + ",传入参数：" + noticestr + ",错误信息：" + ex.ToString(), summary: tname + "通知接口错误");
                message = "error";
            }
            return message;
        }
        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <param name="noticestr">传入的字符串</param>
        /// <returns>返回一个实体类型</returns>
        private JMP.MDL.jmp_order SelectOrder(string code, string noticestr)
        {
            try
            {
                //订单支付完成执行
                JMP.BLL.jmp_order order = new JMP.BLL.jmp_order();
                JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                string TableName = "jmp_order";
                morder = order.GetModelbycode(code, TableName);
                return morder;
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "通知接口错误", "订单号：" + code + "根据订单号查询出错,传入参数：" + noticestr + "!错误信息：" + ex.ToString());//写入报错日志
                PayNotifyGlobalErrorLogger.Log("订单号：" + code + "根据订单号查询出错,传入参数：" + noticestr + "!错误信息：" + ex.ToString(), summary: "通知接口错误");
                return null;
            }
        }
        /// <summary>
        /// 当回传金额和支付金额不一致时调用此方法(把信息传入订单异常表)
        /// </summary>
        /// <param name="morder">订单实体类型</param>
        /// <param name="orderTableName">表名</param>
        /// <param name="trade_no">第三方流水号</param>
        /// <param name="gmt_payment">支付时间</param>
        /// <param name="price">支付金额</param>
        /// <param name="noticestr">传入的字符串</param>
        /// <returns></returns>
        private string AddAuditor(JMP.MDL.jmp_order morder, string orderTableName, string trade_no, DateTime gmt_payment, decimal price, string noticestr)
        {
            string message = "";
            try
            {
                string TableName = "jmp_order";
                if (JMP.TOOL.UpdateOrder.OrderState.UpdateOrderState(morder.o_code, TableName))
                {
                    string ddsm = "订单号：" + morder.o_code + ",支付信息异常请核实！";//短信说明
                    JMP.TOOL.Auditor.IAuditor audit = new JMP.TOOL.Auditor.OrderAbnormalAuditor(morder.o_code, orderTableName, morder.o_app_id, ddsm, trade_no, gmt_payment, price, "成功", morder.o_price);
                    audit.Add();
                    message = "ok";
                }
                else
                {
                    message = "error";
                }
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "通知接口错误", "订单号：" + morder.o_code + "添加金额异常表出错,传入参数：" + noticestr + "!错误信息：" + ex.ToString());//写入报错日志
                PayNotifyGlobalErrorLogger.Log("订单号：" + morder.o_code + "添加金额异常表出错,传入参数：" + noticestr + "!错误信息：" + ex.ToString(), summary: "通知接口错误");
                message = "error";
            }
            return message;
        }
        /// <summary>
        /// 添加到队列通知表
        /// </summary>
        /// <param name="morder">根据订单查询后的moder实体类型</param>
        /// <param name="orderTableName">订单归档表表名</param>
        /// <param name="noticestr">传入的字符串</param>
        /// <returns></returns>
        private string addqueuelist(JMP.MDL.jmp_order morder, string orderTableName, string tname, string noticestr)
        {
            string message = "";
            try
            {
                int appid = selectUserID(morder.o_app_id, tname, noticestr);
                if (appid > 0)
                {
                    JMP.MDL.jmp_queuelist quli = new JMP.MDL.jmp_queuelist();
                    JMP.BLL.jmp_queuelist bllq = new JMP.BLL.jmp_queuelist();
                    quli.q_address = morder.o_address;
                    quli.q_sign = new JMP.BLL.jmp_app().GetModel(morder.o_app_id).a_secretkey;
                    quli.q_noticestate = 0;
                    quli.q_times = 0;
                    quli.q_noticetimes = DateTime.Now;
                    quli.q_tablename = orderTableName;
                    quli.q_o_id = morder.o_id;
                    quli.trade_type = Int32.Parse(morder.o_paymode_id);
                    quli.trade_time = morder.o_ptime;
                    quli.trade_price = morder.o_price;
                    quli.trade_paycode = morder.o_tradeno;
                    quli.trade_code = morder.o_code;
                    quli.trade_no = morder.o_bizcode;
                    quli.q_privateinfo = morder.o_privateinfo;
                    quli.q_uersid = appid;
                    int cg = bllq.Add(quli);
                    if (cg > 0)
                    {
                        message = "ok";
                    }
                    else
                    {
                        //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, tname + "通知接口错误", "订单号：" + morder.o_code + "添加到通知队列失败,传入参数：" + noticestr);//写入报错日志
                        PayNotifyGlobalErrorLogger.Log("订单号：" + morder.o_code + "添加到通知队列失败,传入参数：" + noticestr, summary: tname + "通知接口错误");
                        message = "error";
                    }
                }
                else
                {
                    message = "error";
                }
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, tname + "通知接口错误", "订单号：" + morder.o_code + "添加到通知队列失败,错误信息：" + ex.ToString() + ",传入参数：" + noticestr);//写入报错日志
                PayNotifyGlobalErrorLogger.Log("订单号：" + morder.o_code + "添加到通知队列失败,错误信息：" + ex.ToString() + ",传入参数：" + noticestr, summary: tname + "通知接口错误");
                message = "error";
                throw;
            }
            return message;
        }
        /// <summary>
        /// 修改时时订单表数据
        /// </summary>
        /// <param name="morder">根据订单查询后的mode实体类型</param>
        /// <param name="trade_no">第三方流水号</param>
        /// <param name="gmt_payment">支付时间</param>
        /// <param name="price">支付金额</param>
        /// <param name="payuser">付款账号</param>
        /// <param name="noticestr">传入的字符串</param>
        /// <param name="channelId">支付通道id</param>
        /// <returns></returns>
        private bool UpdateOrder(JMP.MDL.jmp_order morder, string trade_no, DateTime gmt_payment, decimal price, string payuser, string noticestr, int channelId)
        {
            try
            {
                JMP.BLL.jmp_order order = new JMP.BLL.jmp_order();
                string TableName = "jmp_order";
                morder.o_tradeno = trade_no;
                morder.o_ptime = gmt_payment;
                morder.o_payuser = payuser;
                morder.o_state = 1;
                morder.o_noticestate = 0;
                morder.o_price = price;
                if (morder.o_interface_id != channelId)
                {
                    morder.o_interface_id = channelId;
                }
                if (order.Update(morder, TableName))
                {
                    return true;
                }
                else
                {
                    //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "通知接口错误", "修改订单失败！订单号：" + morder.o_code + ",传入参数：" + noticestr);//写入报错日志
                    PayNotifyGlobalErrorLogger.Log("修改订单失败！订单号：" + morder.o_code + ",传入参数：" + noticestr, summary: "通知接口错误");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "通知接口错误", "修改订单失败！订单号：" + morder.o_code + ",错误信息：" + ex.ToString() + ",传入参数：" + noticestr);//写入报错日志
                PayNotifyGlobalErrorLogger.Log("修改订单失败！订单号：" + morder.o_code + ",错误信息：" + ex.ToString() + ",传入参数：" + noticestr, summary: "通知接口错误");
                return false;
            }
        }
        /// <summary>
        /// 根据应用id查询所属用户
        /// </summary>
        /// <param name="APPID">应用id</param>
        /// <param name="tname">接口名称</param>
        /// <param name="noticestr">传入的字符串</param>
        /// <returns></returns>
        private int selectUserID(int APPID, string tname, string noticestr)
        {
            int aid = 0;
            try
            {
                JMP.MDL.jmp_app app = new JMP.MDL.jmp_app();
                JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();
                string notfrurl = "notfrurl" + APPID;//缓存名称
                //判读是否存在缓存
                if (JMP.TOOL.CacheHelper.IsCache(notfrurl))
                {
                    app = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_app>(notfrurl);//获取缓存
                    if (app != null)
                    {
                        aid = app.u_id;
                    }
                }
                else
                {
                    app = appbll.SelectId(APPID);
                    if (app != null)
                    {
                        aid = app.u_id;
                        JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_app>(app, notfrurl, 5);//存入缓存
                    }
                    else
                    {
                        //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, tname + "通知接口错误", "应用id：" + APPID + ",获取用户id失败！获取的参数：" + noticestr);//写入报错日志
                        PayNotifyGlobalErrorLogger.Log("应用id：" + APPID + ",获取用户id失败！获取的参数：" + noticestr, summary: tname + "通知接口错误");
                    }
                }
            }
            catch (Exception ex)
            {
                // AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, tname + "通知接口错误", "查询用户id失败，报错信息：" + ex.ToString() + ",获取的参数:" + noticestr);//写入报错日志
                PayNotifyGlobalErrorLogger.Log("查询用户id失败，报错信息：" + ex.ToString() + ",获取的参数:" + noticestr, summary: tname + "通知接口错误");
            }
            return aid;
        }
    }
}