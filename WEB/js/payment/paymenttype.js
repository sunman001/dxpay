
$(document).ready(function () {

    

})

//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/payment/PaymenttypeList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    location.href = encodeURI(url);
}
//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//修改支付类型弹窗
function UpdatePaymentType(pid) {
    window.parent.ShouwDiaLogWan("修改支付通道", 340, 200, "/payment/paymentupdate?pid=" + pid);
}

//一键启用或禁用
function Updatestate(state, pid) {
    var url = "/payment/PaymentTypeUpdateState";
    var data = { state: state, ids: pid };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); });
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
    });
}


