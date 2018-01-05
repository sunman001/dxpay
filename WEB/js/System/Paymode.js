$(function () {
    //修改接口费率
    $("#btnPayRate").click(function () {

        var pid = $("#id").val();
        var p_rate = $("#p_rate").val();
        var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
        var rexp_rate = rex.test(p_rate); //格式是否正确

        if (isRequestNotNull(p_rate)) {

            $("#y_p_rate").attr("class", "Validform_checktip Validform_wrong");
            $("#y_p_rate").html("请填写接口费率!");
            return false;

        }
        else if (!rexp_rate) {
            $("#y_p_rate").attr("class", "Validform_checktip Validform_wrong");
            $("#y_p_rate").html("请输入整数或者小数最多保留四位!");
            return false;
        }
        else {

            $("#y_p_rate").attr("class", "Validform_checktip  Validform_right");
            $("#y_p_rate").html("验证通过");
        }

        $("#btnPayRate").attr("disabled", "disabled");

        var data = { pid: $.trim(pid), p_rate: $.trim(p_rate) };
        var url = "/System/UpdatePayRart";

        $.post(url, data, function (retJson) {
            $("#btnPayRate").attr("disabled", false);
            if (retJson.success == 1) {

                window.parent.global.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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

    })

})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/System/PaymodeList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    url += "&searchType=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc;
    location.href = encodeURI(url);
}
//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//列表查询
function serchlocuser() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//一键启用或禁用
function UpdatePaymodeState(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择支付类型！", "error", "");
        return;
    }
    var url = "/System/UpdatePaymodeState";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { });
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
//添加支付类型弹窗
function AddPaymode() {
    window.parent.ShouwDiaLogWan("添加支付类型", 880, 400, "/System/PaymodeAddOrUpdate");
}



//修改支付类型弹窗
function Updatepaymode(pid) {
    window.parent.ShouwDiaLogWan("修改支付类型", 880, 400, "/System/PaymodeAddOrUpdate?p_id=" + pid);
}
//验证应用类型名称
function yzPaymodeName() {
    var zzyz = /^[\w\W]{1,20}$/;
    var PaymodeName = $.trim($("#PaymodeName").val());
    if ($.trim(PaymodeName) != "") {
        if (zzyz.test(PaymodeName)) {
            $("#PaymodeNameYz").attr("class", "Validform_checktip  Validform_right");
            $("#PaymodeNameYz").html("验证通过");
        } else {
            $("#PaymodeNameYz").attr("class", "Validform_checktip Validform_wrong");
            $("#PaymodeNameYz").html("支付类型名称长度不超过20");
            return false;
        }
    } else {
        $("#PaymodeNameYz").attr("class", "Validform_checktip Validform_wrong");
        $("#PaymodeNameYz").html("支付类型名称不能为空");
        return false;
    }
}
//验证第三方通道手续费
function yzPaymodeRate() {
    var PaymodeRate = $.trim($("#PaymodeRate").val());
    var tst = /^\d{1,3}(\.\d{1,4})?$/;
    if (!tst.test(PaymodeRate)) {
        $("#PaymodeRateYz").attr("class", "Validform_checktip Validform_wrong");
        $("#PaymodeRateYz").html("第三方通道手续费最大3位整数或者保留四位小数！");
        return false;
    } else {
        if (PaymodeRate > 0) {
            $("#PaymodeRateYz").attr("class", "Validform_checktip  Validform_right");
            $("#PaymodeRateYz").html("验证成功");
        } else {
            $("#PaymodeRateYz").attr("class", "Validform_checktip Validform_wrong");
            $("#PaymodeRateYz").html("第三方通道手续费不能小于或等于零！");
            return false;
        }
    }
}
//选择支付接口配置
function xzzfpz() {
    var paymodeid = $.trim($("#paymodeid").val());
    var auditstate = 0;
    switch (paymodeid) {
        case "1":
            auditstate = 0;
            break;
        case "2":
            auditstate = 1;
            break;
        case "3":
            auditstate = 2;
            break;

    }
    window.parent.ShouwDiaLogWan("选择支付配置", 1000, 700, "/System/SelectInterface?auditstate=" + auditstate);
}
//确认选择支付配置
function selectzfpz(index) {
    var valArr = new Array;
    $("#tablezfpz :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = $.trim((valArr.join(',')).replace("on,", ""));
    if (vals == "") {
        window.parent.ShowMsg("请选择支付配置！", "error", "");
        return;
    }
    window.parent.layer.getChildFrame("#interface", index).val(vals);
    window.parent.layer.getChildFrame("#interfaceYz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#interfaceYz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}


//修改接口费率
function UpdatePayJKF(pid) {
    window.parent.ShouwDiaLogWan("修改接口费率", 500, 251, "/System/PaymodeRartAdd?pid=" + pid);
}

//验证接口费率
function Check_p_rate() {
    var p_rate = $("#p_rate").val();

    var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
    var rexp_rate = rex.test(p_rate); //格式是否正确

    if (isRequestNotNull(p_rate)) {

        $("#y_p_rate").attr("class", "Validform_checktip Validform_wrong");
        $("#y_p_rate").html("请填写接口费率!");
        return false;

    }
    else if (!rexp_rate) {
        $("#y_p_rate").attr("class", "Validform_checktip Validform_wrong");
        $("#y_p_rate").html("请输入整数或者小数最多保留四位!");
        return false;
    }
    else {

        $("#y_p_rate").attr("class", "Validform_checktip  Validform_right");
        $("#y_p_rate").html("验证通过");
    }
}
