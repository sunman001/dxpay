<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="JMWBSR.cashdesk.h5.checkout" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" style="font-size: 42.6667px;">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="must-revalidate" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="cache-control" content="no-store" />
    <meta http-equiv="expires" content="0" />
    <script src="./js/jquery-2.2.4.js"></script>
    <noscript>
        &lt;h1 style="color: red"&gt;您的浏览器不支持JavaScript，请更换浏览器或开启JavaScript设置!&lt;/h1&gt;
    </noscript>
    <script>
        var payApiBaseUrl = "<%=H5ViewModel.PayApiBaseUrl%>";
        function confirmPay() {
            var payType = $("li span.secbtn.current").closest("font").data("paytype");
            var pay = JSON.parse(document.getElementById('payParameter').value);
            pay.paymode = payType;
            var payParameter = JSON.stringify(pay);
            window.location.href = payApiBaseUrl + '?Pay=' + payParameter;
        }
    </script>
    <script src="./js/fastclick-1.0.5.js"></script>
    <script src="./js/iscroll-4.2.5.js"></script>
    <script src="./js/global.js"></script>
    <script src="./js/ipay-common.js"></script>
    <script src="./js/home.js"></script>
    
    <link rel="stylesheet" href="./css/style.css" />
    <title>收银台</title>
</head>
<!-- svn 38572 -->

<body class="homeBody lBody" style="padding-bottom: 0;" data-billprice="<%=H5ViewModel.Price %>">
<header class="clearfix">
    <p>聚合收银台</p>
</header>
<div class="bill">
    <div class="billn">
        <h2>¥ <span><%=H5ViewModel.Price %></span>
        </h2>
        <p class="billName current" style="color: #767676;">
            <%=H5ViewModel.goodsname %>
        </p>
        <p style="color: #b9b9b9">
            <%=H5ViewModel.code %>
        </p>
        <div class="footer">
            <!--底部信息-->
            24小时客服电话：0755-86720380
        </div>
    </div>
</div>
<div class="borderTop"></div>
<!-- 支付方式列表 -->
<div id="wrapper-w">
    <div id="wrapper" style="overflow: hidden; height: 343px;">
        <div id="scroller" style="transition-property: transform; transform-origin: 0px 0px 0px; transform: translate(0px, 0px) scale(1) translateZ(0px);">
            <input type="hidden" value='<%=H5ViewModel.Parameter %>' id="payParameter" />
            <ul class="paymentList marginT15">
                <% foreach (var payment in H5ViewModel.PaymentModes)
                   {%>
                    <% if (!payment.Enabled)
                       {%>
                    <li class="disabledPay">
                    <%}
                       else
                       {%>
                        <li>
                            <%}%>
                            <font data-realfee="<%=payment.Price %>" class="<%= payment.ClsName %> topay" data-paytype="<%= payment.PayType %>"> <span class="<%=string.IsNullOrEmpty(payment.Description)?"":"addDb" %>"> <%= payment.Name %></span>
                                <p class="desc"><%=payment.Description %></p>
                                <span class="secbtn"></span>

                            </font>
                        </li>
                        <%} %>
                <p class="ac_togglePayment morePay">
                    <span class="icon_down_n">其他支付方式</span>
                </p>
                <div class="footer" style="display: block;">
                    24小时客服电话：0755-86720380
                </div>
            </ul>
        </div>
    </div>
</div>
<div class="btnPayBg" style="display: block;"></div>
<div class="btnPay down" onclick="confirmPay()">
    <p>
        确认支付&nbsp;<span class="secbtn">¥<%=H5ViewModel.Price %></span>
    </p>
</div>
</body>

</html>