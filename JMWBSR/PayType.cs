using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JMWBSR
{
    /// <summary>
    /// 支付接口调用公共方法
    /// </summary>
    public class PayType
    {
        /// <summary>
        /// 支付接口公共方法
        /// </summary>
        /// <param name="paymodeid">支付类型</param>
        /// <param name="appid">应用id</param>
        /// <param name="tid">应用类型id</param>
        /// <param name="paytype">关联平台（1:安卓，2:苹果，3:H5）</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">金额</param>
        /// <param name="codeid">订单表id</param>
        /// <param name="privateinfo">商户私有信息</param>
        /// <returns></returns>
        public static string PaySelect(string paymodeid, int appid, int tid, int paytype,string code,string goodsname,decimal price,int codeid,string privateinfo)
        {
            price = decimal.Parse(price.ToString("f2"));
            string sHtmlText = "{\"message\":\"支付通有误\",\"result\":107}";
            string zftd = "";//支付通道
            DataTable zftddt = new DataTable();
            JMP.BLL.jmp_paymenttype paybll = new JMP.BLL.jmp_paymenttype();
            switch (paymodeid)
            {
                case "1":
                    #region 选择支付宝支付
                    string ZFBzftd = "ZFBzftd" + appid;
                    if (JMP.TOOL.CacheHelper.IsCache(ZFBzftd))//判读是否存在缓存
                    {
                        zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(ZFBzftd);//获取缓存
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                        }
                        else
                        {
                            zftddt = paybll.SelectModesType(1, paytype, tid);
                            if (zftddt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, zftddt.Rows.Count);
                                zftd = zftddt.Rows[row]["p_extend"].ToString();
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, ZFBzftd, 5);//存入缓存
                            }
                        }
                    }
                    else
                    {
                        zftddt = paybll.SelectModesType(1, paytype, tid);
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, ZFBzftd, 5);//存入缓存
                        }
                    }
                    switch (zftd)
                    {
                        case "ZFB":
                            #region 支付宝通道
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.PayZfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.PayZfbIos(tid,code, goodsname,price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.PayZfbH5(tid,code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "SYZFB":
                            #region 首游支付宝
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.SyZfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.SyZfbIOS(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.SyZfbH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "PYZFB":
                            #region 鹏缘支付宝
                            switch (paytype)
                            {


                                case 1://安卓调用
                                    sHtmlText = Pay.PyzfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.Pyzfbios(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.PyZfbH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "SDWBZFB":
                            #region 山东微保支付宝
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.SdWbZfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.SdWbZfbIos(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.SdWbZfbH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "whdzfb":
                            #region 微赢互动支付宝
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.wyhdzfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.wyhdzfbios(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.wyhdzfbh5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "skwxzfb":
                            #region 思科无限支付宝
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.skwxzfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.skwxzfbIos(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.skwxzfbh5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "wftzfb":
                            #region 威富通支付宝
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.WftZfbAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.WftZfbIos(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.WftZfbH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;

                    }
                    #endregion
                    break;
                case "2":
                    #region 选择微信支付
                    string wxzftd = "wxzftd" + appid;
                    if (JMP.TOOL.CacheHelper.IsCache(wxzftd))//判读是否存在缓存
                    {
                        zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wxzftd);//获取缓存
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                        }
                        else
                        {
                            zftddt = paybll.SelectModesType(2, paytype, tid);
                            if (zftddt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, zftddt.Rows.Count);
                                zftd = zftddt.Rows[row]["p_extend"].ToString();
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, wxzftd, 5);//存入缓存
                            }
                        }

                    }
                    else
                    {
                        zftddt = paybll.SelectModesType(2, paytype, tid);
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, wxzftd, 5);//存入缓存
                        }
                    }
                    switch (zftd)
                    {
                        case "WFT":
                            #region 威富通
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.PayWftAz(tid, code, goodsname, price, privateinfo, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.PayWftIos(tid, code, goodsname, price, privateinfo, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.PayWftAzH5(tid, code, goodsname, price, privateinfo, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "SYWAP":
                            #region 首游微信wap支付
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.SyWxWapAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.SyWxWapIOS(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.SyWxWapH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "HYWX":
                            #region 汇元wap支付
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.HyWxWaPAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.HyWxWaPIOS(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.HyWxWaPH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;
                    }
                    #endregion
                    break;
                case "3":
                    #region 选择银联支付
                    string ylzftd = "ylzftd" + appid;
                    if (JMP.TOOL.CacheHelper.IsCache(ylzftd))//判读是否存在缓存
                    {
                        zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(ylzftd);//获取缓存
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                        }
                        else
                        {
                            zftddt = paybll.SelectModesType(3, paytype, tid);
                            if (zftddt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, zftddt.Rows.Count);
                                zftd = zftddt.Rows[row]["p_extend"].ToString();
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, ylzftd, 5);//存入缓存
                            }
                        }
                    }
                    else
                    {
                        zftddt = paybll.SelectModesType(3, paytype, tid);
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, ylzftd, 5);//存入缓存
                        }
                    }
                    switch (zftd)
                    {
                        case "ZF":
                            #region 银联智付
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.PayZfAz(tid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.PayZfIos(tid, code, goodsname, price, codeid);
                                    break;
                                case 3://H5调用
                                    sHtmlText = Pay.PayZfH5(tid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "HYYL":
                            #region 汇元银联
                            if (paytype == 3)//H5调用
                            {
                                sHtmlText = Pay.HyYlPayH5(tid, code, goodsname, price, codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;
                    }
                    #endregion
                    break;
                case "4":
                    #region 选择微信公众号支付
                    string wxgzhzftd = "wxgzhzftd" + appid;
                    if (JMP.TOOL.CacheHelper.IsCache(wxgzhzftd))//判读是否存在缓存
                    {
                        zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wxgzhzftd);//获取缓存
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                        }
                        else
                        {
                            zftddt = paybll.SelectModesType(4, paytype, tid);
                            if (zftddt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, zftddt.Rows.Count);
                                zftd = zftddt.Rows[row]["p_extend"].ToString();
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, wxgzhzftd, 5);//存入缓存
                            }
                        }
                    }
                    else
                    {
                        zftddt = paybll.SelectModesType(4, paytype, tid);
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, wxgzhzftd, 5);//存入缓存
                        }
                    }
                    switch (zftd)
                    {
                        case "WFTGZH":
                            #region 威富通公众号支付方式
                            if (paytype == 3)
                            {
                                sHtmlText = Pay.PayWftGzhH5(codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        case "NYGZH":
                            #region 南粤公众号支付方式
                            if (paytype == 3)
                            {
                                sHtmlText = Pay.NyGzhH5(tid, code, price, codeid, goodsname);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        case "whdgzh":
                            #region 微互动公众号
                            if (paytype == 3)
                            {
                                sHtmlText = Pay.whdgzhH5(tid, code, goodsname, price, codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        case "sywxgzh":
                            #region 首游微信公众号
                            if (paytype == 3)
                            {
                                sHtmlText = Pay.SyGzhH5(tid, code, price, codeid, goodsname);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;
                    }
                    #endregion
                    break;
                case "5":
                    #region 选择微信app支付
                    string wxappzftd = "wxappzftd" + appid;
                    //SelectWXapp
                    if (JMP.TOOL.CacheHelper.IsCache(wxappzftd))//判读是否存在缓存
                    {
                        zftd = JMP.TOOL.CacheHelper.GetCaChe<string>(wxappzftd);//获取缓存
                        if (string.IsNullOrEmpty(zftd))
                        {
                            zftd = paybll.SelectWXapp(5, paytype, appid);
                            if (!string.IsNullOrEmpty(zftd))
                            {
                                JMP.TOOL.CacheHelper.CacheObjectLocak<string>(zftd, wxappzftd, 5);//存入缓存
                            }
                        }
                    }
                    else
                    {
                        zftd = paybll.SelectWXapp(5, paytype, appid);
                        if (!string.IsNullOrEmpty(zftd))
                        {
                            JMP.TOOL.CacheHelper.CacheObjectLocak<string>(zftd, wxappzftd, 5);//存入缓存
                        }
                    }
                    switch (zftd)
                    {
                        case "WX":
                            #region 微信
                            switch (paytype)
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.PayWxAz(appid, code, goodsname, price, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.PayWxIos(appid, code, goodsname, price, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "WFTAPP":
                            #region 威富通app支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.PayWftAppAz(appid, code, goodsname, price, privateinfo, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.PayWftAppIos(appid, code, goodsname, price, privateinfo, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "NYAPP":
                            #region 南粤app支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.NyAppidAz(appid, code, price, codeid, goodsname);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.NyAppidIos(appid, code, price, codeid, goodsname);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "SYAPPID":
                            #region 首游app支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.SyAppidAz(appid, code, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.SyAppidIos(appid, code, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "ZYXAPP":
                            #region 众易鑫app支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.ZyxAppidAz(appid, code, price, codeid, goodsname);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.ZyxAppidIos(appid, code, price, codeid, goodsname);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "FFL":
                            #region 发发啦app支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.FflAppidAz(appid, code, codeid);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.FflAppidIos(appid, code, codeid);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "skwxappid":
                            #region 思科无限appid
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.skwxappidAz(appid, code, price, codeid, goodsname);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.skwxappidIos(appid, code, price, codeid, goodsname);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "xyyhappid":
                            #region 兴业银行appid支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.xyyhappidAz(appid, code, price, codeid, goodsname);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.xyyhappidIos(appid, code, price, codeid, goodsname);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        case "wyhdappid":
                            #region 微赢互动appid支付
                            switch (paytype)//判断支付平台通道
                            {
                                case 1://安卓调用
                                    sHtmlText = Pay.WyhdAppidAz(appid, code, price, codeid, goodsname);
                                    break;
                                case 2://苹果调用
                                    sHtmlText = Pay.WyhdAppidIos(appid, code, price, codeid, goodsname);
                                    break;
                                default:
                                    sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                                    break;
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;

                    }
                    #endregion
                    break;
                case "6":
                    #region 选择微信扫码支付
                    #region 获取支付通道
                    string wxsmzftd = "wxsmzftd" + appid;
                    if (JMP.TOOL.CacheHelper.IsCache(wxsmzftd))//判读是否存在缓存
                    {
                        zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wxsmzftd);//获取缓存
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                        }
                        else
                        {
                            zftddt = paybll.SelectModesType(6, paytype, tid);
                            if (zftddt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, zftddt.Rows.Count);
                                zftd = zftddt.Rows[row]["p_extend"].ToString();
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, wxsmzftd, 5);//存入缓存
                            }
                        }
                    }
                    else
                    {
                        zftddt = paybll.SelectModesType(6, paytype, tid);
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, wxsmzftd, 5);//存入缓存
                        }
                    }
                    #endregion
                    switch (zftd)
                    {
                        case "WFTSM":
                            #region 选择威富通微信扫码支付
                            if (paytype == 3)// 验证支付平台
                            {
                                sHtmlText = Pay.WftWxSm(tid, code, goodsname, price, privateinfo, codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        case "sfwxsm":
                            #region 舒付微信扫码支付
                            if (paytype == 3)// 验证支付平台
                            {
                                sHtmlText = Pay.sfwxsm(tid, code, goodsname, price, codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        case "wxhdwxsm":
                            #region 微赢互动微信扫码支付
                            if (paytype == 3)// 验证支付平台
                            {
                                sHtmlText = Pay.wyhdwxsm(tid, code, goodsname, price, codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        case "nywxsm":
                            #region 南粤微信扫码支付
                            if (paytype == 3)// 验证支付平台
                            {
                                sHtmlText = Pay.NywxsmH5(tid, code, price, codeid, goodsname);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;
                    }
                    #endregion
                    break;
                case "7":
                    #region 选择支付宝扫码支付
                    #region 获取支付通道
                    string ZfbSMzftd = "ZfbSMzftd" + appid;
                    if (JMP.TOOL.CacheHelper.IsCache(ZfbSMzftd))//判读是否存在缓存
                    {
                        zftddt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(ZfbSMzftd);//获取缓存
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                        }
                        else
                        {
                            zftddt = paybll.SelectModesType(7, paytype, tid);
                            if (zftddt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, zftddt.Rows.Count);
                                zftd = zftddt.Rows[row]["p_extend"].ToString();
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, ZfbSMzftd, 5);//存入缓存
                            }
                        }
                    }
                    else
                    {
                        zftddt = paybll.SelectModesType(7, paytype, tid);
                        if (zftddt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, zftddt.Rows.Count);
                            zftd = zftddt.Rows[row]["p_extend"].ToString();
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(zftddt, ZfbSMzftd, 5);//存入缓存
                        }
                    }
                    #endregion
                    switch (zftd)
                    {
                        case "WFTZFBSM":
                            #region 威富通支付宝扫码
                            if (paytype == 3)// 验证支付平台
                            {
                                sHtmlText = Pay.WftZfbSm(tid, code, goodsname, price, privateinfo, codeid);
                            }
                            else
                            {
                                sHtmlText = "{\"message\":\"支付平台有误\",\"result\":9988}";
                            }
                            #endregion
                            break;
                        default:
                            sHtmlText = "{\"message\":\"支付通道未配置\",\"result\":106}";
                            break;
                    }
                    #endregion
                    break;
                default:
                    sHtmlText = "{\"message\":\"支付通有误\",\"result\":107}";
                    break;
            }
            return sHtmlText;
        }
    }
}