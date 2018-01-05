$(function () {
    //验证是否显示通知状态查询
    $('#paymentstate').change(function () {
        var pdzftype = $("#paymentstate").val();
        if (pdzftype == 1) {
            document.getElementById("tztype").style.display = "";
        } else {
            $("#noticestate").val("");
            document.getElementById("tztype").style.display = "none";
        }
    })
    $('#paymentstate').change();

})
//验证是否显示通知状态查询
function xstztype() {
    var pdzftype = $("#paymentstate").val();
    if (pdzftype == 1) {
        document.getElementById("tztype").style.display = "";
    } else {
        $("#noticestate").val("");
        document.getElementById("tztype").style.display = "none";
    }
}
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/REPORT/OrderList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $.trim($("#searchType").val());
    var searchname = $.trim($("#searchname").val());
    var stime = $.trim($("#stime").val());
    var etime = $.trim($("#etime").val());
    var paymode = $.trim($("#paymode").val());
    var paymentstate = $.trim($("#paymentstate").val());
    var noticestate = $.trim($("#noticestate").val());
    var platformid = $.trim($("#platformid").val());
    var relationtype = $.trim($("#relationtype").val());

    url += "&searchType=" + $.trim(searchType) + "&searchname=" + $.trim(searchname) + "&stime=" + $.trim(stime) + "&etime=" + $.trim(etime) + "&paymode=" + $.trim(paymode) + "&paymentstate=" + $.trim(paymentstate) + "&noticestate=" + $.trim(noticestate) + "&platformid=" + $.trim(platformid) + "&relationtype=" + $.trim(relationtype);
    location.href = encodeURI(url);
}
function selectorderlistDc() {
    var url = "/REPORT/DcDev";
    var searchType = $.trim($("#searchType").val());
    var searchname = $.trim($("#searchname").val());
    var stime = $.trim($("#stime").val());
    var etime = $.trim($("#etime").val());
    var paymode = $.trim($("#paymode").val());
    var paymentstate = $.trim($("#paymentstate").val());
    var noticestate = $.trim($("#noticestate").val());
    var platformid = $.trim($("#platformid").val());
    url += "?searchType=" + $.trim(searchType) + "&searchname=" + $.trim(searchname) + "&stime=" + $.trim(stime) + "&etime=" + $.trim(etime) + "&paymode=" + $.trim(paymode) + "&paymentstate=" + $.trim(paymentstate) + "&noticestate=" + $.trim(noticestate) + "&platformid=" + platformid;
    location.href = encodeURI(url);
}
//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//列表查询
function selectorderlist() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//重发通知
function Orderrewire(code, ptime) {
    var url = "/REPORT/Orderrewire";
    var _code = '' + code;
    var data = { code: $.trim(_code), ptime: $.trim(ptime) };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", "");
        }
        else if (retJson.success == 9998) {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        } else if (retJson.success == 9999) {
            window.parent.ShowMsg(retJson.msg, "error", "");
            window.top.location.href = retJson.Redirect;
            return false;
        } else if (retJson.success == 9997) {
            window.top.location.href = retJson.Redirect;
            return false;
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
    })
}