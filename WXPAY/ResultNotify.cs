using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;

namespace WxPayAPI
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        public ResultNotify(Page page)
            : base(page)
        {
        }

        public override void ProcessNotify(int tid)
        {

            WxPayData notifyData = GetNotifyData(tid);

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            string ordertime = notifyData.GetValue("time_end").ToString();

            if (ordertime == "null")
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {

                //商户订单号

                string out_trade_no = notifyData.GetValue("out_trade_no").ToString();

                //交易号

                string trade_no = notifyData.GetValue("transaction_id").ToString();

                //交易状态
                string trade_status = notifyData.GetValue("result_code").ToString();

                //买家账号
                string buyer_email = notifyData.GetValue("transaction_id").ToString();

                //买家付款时间
                string gmt_payment = ordertime;
                //交易金额（单位：分转换成元）
                decimal o_price = decimal.Parse((decimal.Parse(notifyData.GetValue("total_fee").ToString()) / 100).ToString("f2"));

                if (trade_status == "SUCCESS")
                {
                    try
                    {
                        JMP.BLL.jmp_order order = new JMP.BLL.jmp_order();
                        JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                        string TableName = "jmp_order";
                        string orderTableName = JMP.TOOL.WeekDateTime.GetOrderTableName(DateTime.ParseExact(gmt_payment, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd"));//获取订单表名
                        morder = order.GetModelbycode(out_trade_no, TableName);
                        if (morder != null)
                        {
                            if (morder.o_price == o_price)
                            {
                                int payid = !string.IsNullOrEmpty(morder.o_paymode_id) ? Int32.Parse(morder.o_paymode_id) : 0;
                                if (JMP.TOOL.OrderProportion.ddbl(morder.o_app_id, o_price, payid))
                                {
                                    WxPayData res = new WxPayData();
                                    res.SetValue("return_code", "SUCCESS");
                                    res.SetValue("return_msg", "OK");
                                    page.Response.Write(res.ToXml());
                                    page.Response.End();
                                }
                                else
                                {
                                    if (morder.o_state != 1)
                                    {
                                        morder.o_tradeno = trade_no;
                                        morder.o_ptime = DateTime.ParseExact(gmt_payment, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                        morder.o_payuser = buyer_email;
                                        morder.o_state = 1;
                                        morder.o_noticestate = 0;
                                        morder.o_price = o_price;
                                        order.Update(morder, TableName);

                                        JMP.MDL.jmp_app app = new JMP.MDL.jmp_app();
                                        JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();
                                        app = appbll.SelectId(morder.o_app_id);
                                        if (app != null)
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
                                            quli.q_uersid = app.u_id;
                                            int cg = bllq.Add(quli);
                                            if (cg > 0)
                                            {
                                                WxPayData res = new WxPayData();
                                                res.SetValue("return_code", "SUCCESS");
                                                res.SetValue("return_msg", "OK");
                                                page.Response.Write(res.ToXml());
                                                page.Response.End();
                                            }
                                            else
                                            {
                                                //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微信官网通知错误", "订单号：" + morder.o_code + "添加到通知队列失败");//写入报错日志
                                                PayApiGlobalErrorLogger.Log("订单号：" + morder.o_code + "添加到通知队列失败",summary: "微信官网通知错误");
                                                WxPayData res = new WxPayData();
                                                res.SetValue("return_code", "FAIL");
                                                res.SetValue("return_msg", "FAIL");
                                                page.Response.Write(res.ToXml());
                                                page.Response.End();
                                            }
                                        }
                                        else
                                        {
                                            //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微信官网通知错误", "应用id：" + morder.o_app_id + ",获取用户id失败！");//写入报错日志
                                            PayApiGlobalErrorLogger.Log("应用id：" + morder.o_app_id + ",获取用户id失败！",summary: "微信官网通知错误");
                                            WxPayData res = new WxPayData();
                                            res.SetValue("return_code", "FAIL");
                                            res.SetValue("return_msg", "FAIL");
                                            page.Response.Write(res.ToXml());
                                            page.Response.End();
                                        }

                                    }
                                    else
                                    {
                                        //AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微信官网通知接口错误", "订单号：" + out_trade_no + ",未查询到相关信息！");//写入报错日志
                                        PayApiGlobalErrorLogger.Log("订单号：" + out_trade_no + ",未查询到相关信息！",summary: "微信官网通知接口错误");
                                        WxPayData res = new WxPayData();
                                        res.SetValue("return_code", "SUCCESS");
                                        res.SetValue("return_msg", "OK");
                                        page.Response.Write(res.ToXml());
                                        page.Response.End();
                                    }
                                }
                            }
                            else
                            {
                                if (JMP.TOOL.UpdateOrder.OrderState.UpdateOrderState(morder.o_code, TableName))
                                {
                                    string ddsm = "订单号：" + morder.o_code + ",支付信息异常请核实！";//短信说明
                                    JMP.TOOL.Auditor.IAuditor audit = new JMP.TOOL.Auditor.OrderAbnormalAuditor(morder.o_code, orderTableName, morder.o_app_id, ddsm, trade_no, DateTime.ParseExact(gmt_payment, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), o_price, "成功", morder.o_price);
                                    audit.Add();
                                    WxPayData res = new WxPayData();
                                    res.SetValue("return_code", "SUCCESS");
                                    res.SetValue("return_msg", "OK");
                                    page.Response.Write(res.ToXml());
                                    page.Response.End();
                                }
                                else
                                {
                                    WxPayData res = new WxPayData();
                                    res.SetValue("return_code", "FAIL");
                                    res.SetValue("return_msg", "FAIL");
                                    page.Response.Write(res.ToXml());
                                    page.Response.End();
                                }

                            }

                        }
                        else
                        {
                            WxPayData res = new WxPayData();
                            res.SetValue("return_code", "FAIL");
                            res.SetValue("return_msg", "FAIL");
                            page.Response.Write(res.ToXml());
                            page.Response.End();
                        }

                    }
                    catch
                    {
                        WxPayData res = new WxPayData();
                        res.SetValue("return_code", "FAIL");
                        res.SetValue("return_msg", "FAIL");
                        page.Response.Write(res.ToXml());
                        page.Response.End();
                    }
                }


            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id, int tid)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req, tid);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //查询订单
        private string QueryOrdertime(string transaction_id, int tid)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req, tid);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return res.GetValue("time_end").ToString();
            }
            else
            {
                return "null";
            }
        }

    }
}