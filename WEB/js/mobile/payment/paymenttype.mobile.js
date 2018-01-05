
$(document).ready(function () {

    $("#btnCostRatio").click(function () {

        var pid = $("#id").val();
        var CostRatio = $("#CostRatio").val();
        var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
        var rexCostRatio = rex.test(CostRatio); //格式是否正确

        if (isRequestNotNull(CostRatio)) {

            $("#y_CostRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#y_CostRatio").html("请填写通道成本费率!");
            return false;

        }
        else if (!rexCostRatio) {
            $("#y_CostRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#y_CostRatio").html("请输入整数或者小数最多保留四位!");
            return false;
        }
        else {

            $("#y_CostRatio").attr("class", "Validform_checktip  Validform_right");
            $("#y_CostRatio").html("验证通过");
        }

        $("#btnCostRatio").attr("disabled", "disabled");

        var data = { pid: $.trim(pid), CostRatio: $.trim(CostRatio) };
        var url = "/payment/UpdatePayCostRatio";

        $.post(url, data, function (retJson) {
            $("#btnCostRatio").attr("disabled", false);
            if (retJson.success == 1) {

                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }

        })

    })

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
     
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.location.reload(); });
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

//修改通道成本费率
function UpdatePayCR(pid) {
    window.parent.ShouwDiaLogWan("修改通道成本费率", 500, 200, "/payment/PayMenttypeCostRatio?pid=" + pid);
}

//验证通道成本费
function CheckCostRatio() {
    var CostRatio = $("#CostRatio").val();

    var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
    var rexCostRatio = rex.test(CostRatio); //格式是否正确

    if (isRequestNotNull(CostRatio)) {

        $("#y_CostRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#y_CostRatio").html("请填写通道成本费率!");
        return false;

    }
    else if (!rexCostRatio) {
        $("#y_CostRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#y_CostRatio").html("请输入整数或者小数最多保留四位!");
        return false;
    }
    else {

        $("#y_CostRatio").attr("class", "Validform_checktip  Validform_right");
        $("#y_CostRatio").html("验证通过");
    }
}
