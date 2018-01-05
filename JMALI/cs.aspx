<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cs.aspx.cs" Inherits="JMALI.cs" %>

<html>
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>接口测试</title>
    <style type="">
        .data-table table {
            width: 100%;
            border: 1px solid #cdcccc;
            -webkit-border-radius: 5px 5px 0px 0px;
            -moz-border-radius: 5px 5px 0px 0px;
            border-radius: 5px 5px 0px 0px;
            behavior: url(../other/PIE.htc);
        }

            .data-table table th {
                height: 37px;
                color: #676767;
                font-size: 15px;
                vertical-align: middle;
                border-left: 1px solid #dddddd;
                border-top: 1px solid #dddddd;
            }

            .data-table table td {
                padding: 10px;
                text-align: center;
                border-left: 1px solid #dddddd;
                border-top: 1px solid #dddddd;
                font-size: 12px;
                color: #999999;
            }

            .data-table table tr:first-child td {
                padding: 10px;
                text-align: center;
                border-top: none;
            }

            .data-table table tr td:first-child {
                border-left: none;
            }

            .data-table table tr:hover {
                background: #f1f1f1;
            }

            .data-table table td a {
                margin: 0px 5px;
                color: #21779a;
            }

                .data-table table td a:hover {
                    color: #21779a;
                    text-decoration: underline;
                }
    </style>
    <script>
        function yz() {

            document.getElementById("but").disabled = true;

            var sel = document.getElementById("sel").value;
            var o_price = document.getElementById("price").value;
            var Pattern = document.getElementById("Pattern").value;
            var appkey = document.getElementById("appkey").value;
            var appid = document.getElementById("appid").value;

            if (eval(Pattern) < 0 || eval(Pattern) > 3) {
                alert("请选择测试模式");
                return false;
            }
            if (eval(Pattern) == 3) {
                if (appkey == "" || appid == "") {
                    alert("请输入应用key和应用编号");
                    document.getElementById("but").disabled = false;
                    return false;
                }
            }
            if (sel == "0") {
                alert("选择支付方式");
                document.getElementById("but").disabled = false;
                return false;
            } else {
                var tst = /^\d{1,}(\.\d{1,2})?$/;
                if (!tst.test(o_price)) {
                    alert("支付金额为整数或者保留两位小数！");
                    document.getElementById("but").disabled = false;
                    return false;
                } else {
                    if (eval(o_price) > 0) {
                        window.location.href = "/cs.aspx?sel=" + sel + "&o_price=" + o_price + "&Pattern=" + Pattern + "&appkey=" + appkey + "&appid=" + appid;
                    } else {
                        document.getElementById("but").disabled = false;
                        alert("支付金额必须大于零！");
                        return false;
                    }
                }

            }
        }

        function mos() {
            var Pattern = document.getElementById("Pattern").value;

            if (eval(Pattern) == 3) {

                document.getElementById("yykey").style.display = "";
                document.getElementById("spid").style.display = "";

            }
            else {
                document.getElementById("yykey").style.display = "none";
                document.getElementById("spid").style.display = "none";
            }

        }
    </script>
</head>
<body>
    <div class="data-table">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="text-align: right;">测试模式：</td>
                <td style="text-align: left;">
                    <select id="Pattern" onclick="mos()">
                        <option value="0">--选择测试模式--</option>
                        <option value="1">商务直客</option>
                        <option value="2">代理商模式</option>
                        <option value="3">自定义模式</option>
                    </select>
                </td>
            </tr>
            <tr id="yykey" style="display: none">
                <td style="text-align: right;">应用key</td>
                <td style="text-align: left;">
                    <input type="text" id="appkey" value="" /></td>
            </tr>
            <tr id="spid" style="display: none">
                <td style="text-align: right;">应用编号</td>
                <td style="text-align: left;">
                    <input type="text" id="appid" value="" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">支付模式：</td>
                <td style="text-align: left;">
                    <select id="sel">
                        <option value="0">--选择支付方式--</option>
                        <option value="1">支付宝</option>
                        <option value="2">微信</option>
                        <option value="3">银联</option>
                        <option value="4">微信公众号</option>
                        <option value="6">微信扫码</option>
                        <option value="7">支付宝扫码</option>
                        <option value="8">QQ钱包wap</option>
                        <option value="9">H5收银台</option>
                        <%int a = 0; if (a == 1)
                            {
                        %>
                        <option value="5">微信appid </option>
                        <% }%>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">支付金额：</td>
                <td style="text-align: left;">
                    <input type="text" id="price" value="0.01" />(单位元)</td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <input type="button" id="but" runat="server" onclick="yz()" value="确认选择" />
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%; text-align: center; height: 50px;">
        <%=massage%>
    </div>

</body>

</html>
