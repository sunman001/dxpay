<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="JMALI.demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>盾行支付测试demo</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" />
</head>
<body>

    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">请求参数
            </h3>
        </div>
        <div class="panel-body">
            <table class="table" style="font-size: 12px;">
                <tr>
                    <td style="text-align: right;">商户订单号（bizcode）：</td>
                    <td>
                        <input type="text" id="bizcode" value="<%=DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(111111, 666666).ToString() %>" maxlength="64" />（必填）
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">应用编号（appid）：</td>
                    <td>
                        <input type="text" id="appid" value="" />（必填）
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">通知地址（address）：</td>
                    <td>
                        <input type="text" id="address" value="" maxlength="200" />（可选，不传就已后台配置为准）
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">H5同步通知地址（showaddress）：</td>
                    <td>
                        <input type="text" id="showaddress" value="" maxlength="200" />（可选，不传就已后台配置为准）
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">支付模式（paytype）：</td>
                    <td>
                        <select id="paytype">
                            <option value="0">--选择支付方式--</option>
                            <option value="1">支付宝</option>
                            <option value="2">微信</option>
                            <option value="3">银联</option>
                            <option value="4">微信公众号</option>
                            <option value="5">微信APP</option>
                            <option value="6">微信扫码</option>
                            <option value="7">支付宝扫码</option>
                            <option value="8">QQ钱包wap</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">商品名称（goodsname）：</td>
                    <td>
                        <input type="text" id="goodsname" value="" maxlength="16" />（必填）
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">商品价格（price）：</td>
                    <td>
                        <input type="text" id="price" value="1" />
                        (单位元)（必填，如果是小数，最多保留两位小数）</td>
                </tr>
                <tr>
                    <td style="text-align: right;">商户私有信息（privateinfo）：</td>
                    <td>
                        <input type="text" id="privateinfo" value="" maxlength="64" />（可选，放置需要回传的信息(utf-8)）</td>
                </tr>
                <tr>
                    <td style="text-align: right;">appkey：</td>
                    <td>
                        <input type="text" id="appkey" value="" />（必填）</td>
                </tr>
                <tr>
                    <td style="text-align: right;">termkey：</td>
                    <td>
                        <input type="text" id="termkey" value="<%=DateTime.Now.ToString("HHmm") + new Random().Next(111111, 999999).ToString() %>" />（H5模式可为空）</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <input type="button" id="but" runat="server" onclick="btnDemo()" value="确认提交" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">展示数据
            </h3>
        </div>
        <div class="panel-body">
            <table class="table" style="font-size: 12px; word-wrap: break-word; word-break: break-all;">
                <tr>
                    <td style="text-align: right; width: 80px;"></td>
                    <td>时间戳 timestamp：（10位unix时间戳），签名 sign：（签名md5（price+ bizcode+ timestamp +appkey）32位大写）
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 80px;">请求的参数：</td>
                    <td>
                        <%=ShowParameter%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 80px;">时间戳：</td>
                    <td>
                        <%=ShowTimestamp%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 80px;">加密前签名的字符串：</td>
                    <td>
                        <%=ShowSignstr%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 80px;">加密后的签名字符串：</td>
                    <td>

                        <%=ShowSign%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 80px;">拼装的url：</td>
                    <td>
                        <%=ShowUrl %>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 80px;">参数验证提示：</td>
                    <td>
                        <%=ShowMassage %>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">

    function btnDemo() {
        document.getElementById("but").disabled = true;

        var bizcode = document.getElementById("bizcode").value.replace(/(^\s*)|(\s*$)/g, "");
        var appid = document.getElementById("appid").value.replace(/(^\s*)|(\s*$)/g, "");
        var address = document.getElementById("address").value.replace(/(^\s*)|(\s*$)/g, "");
        var showaddress = document.getElementById("showaddress").value.replace(/(^\s*)|(\s*$)/g, "");
        var paytype = document.getElementById("paytype").value.replace(/(^\s*)|(\s*$)/g, "");
        var goodsname = document.getElementById("goodsname").value.replace(/(^\s*)|(\s*$)/g, "");
        var price = document.getElementById("price").value.replace(/(^\s*)|(\s*$)/g, "");
        var privateinfo = document.getElementById("privateinfo").value.replace(/(^\s*)|(\s*$)/g, "");
        var appkey = document.getElementById("appkey").value.replace(/(^\s*)|(\s*$)/g, "");
        var termkey = document.getElementById("termkey").value.replace(/(^\s*)|(\s*$)/g, "");

        if (bizcode == "") {
            alert("请填写商户订单号！");
            document.getElementById("but").disabled = false;
            return false;
        }
        if (appid == "") {
            alert("请填写应用编号！");
            document.getElementById("but").disabled = false;
            return false;
        }
        if (goodsname == "") {
            alert("请填写商品名称！");
            document.getElementById("but").disabled = false;
            return false;
        }
        var tst = /^\d{1,}(\.\d{1,2})?$/;
        if (!tst.test(price)) {
            alert("支付金额为整数或者保留两位小数！");
            document.getElementById("but").disabled = false;
            return false;
        }
        if (eval(price) < 0) {
            alert("支付金额必须大于零！");
            document.getElementById("but").disabled = false;
            return false;
        }
        if (appkey == "") {
            alert("请填写appkey！");
            document.getElementById("but").disabled = false;
            return false;
        }

        window.location.href = "demo.aspx?bizcode=" + bizcode + "&appid=" + appid + "&address=" + address + "&showaddress=" + showaddress + "&paytype=" + paytype + "&goodsname=" + goodsname + "&price=" + price + "&privateinfo=" + privateinfo + "&appkey=" + appkey + "&termkey=" + termkey;
    }


</script>
