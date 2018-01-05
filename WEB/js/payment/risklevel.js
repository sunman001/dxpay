$(function () {
    //添加风险配置
    $("#btnSavefxpz").click(function () {
        var apptypeid = $.trim($("#apptypeid").val());
        var risklevelid = $.trim($("#risklevelid").val());
        var rid = $.trim($("#rid").val());
        if (apptypeid == 0) {
            $("#apptypeidyy").attr("class", "Validform_checktip Validform_wrong");
            $("#apptypeidyy").html("请选择应用类型！");
            return false;
        }
        if (risklevelid == 0) {
            $("#risklevelyy").attr("class", "Validform_checktip Validform_wrong");
            $("#risklevelyy").html("请选择风险等级！");
            return false;
        }
        var data = { r_apptypeid: $.trim(apptypeid), r_risklevel: $.trim(risklevelid), r_id: $.trim(rid) };
        $("#btnSavefxpz").attr("disabled", "disabled");
        var url = "/payment/AddRisklevel";
        $.post(url, data, function (retJson) {
            $("#btnSavefxpz").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () {
                    window.parent.global.reload();
                    window.parent.layer.closeAll();
                });
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
//选择应用类型判断
function xzapptype() {
    var apptypeid = $.trim($("#apptypeid").val());
    if (apptypeid == 0) {
        $("#apptypeidyy").attr("class", "Validform_checktip Validform_wrong");
        $("#apptypeidyy").html("请选择应用类型！");
        return false;
    } else {
        $("#apptypeidyy").attr("class", "Validform_checktip  Validform_right");
        $("#apptypeidyy").html("验证通过");
    }
}
//选择风险等级
function Selectrisklevel() {
    var risklevelid = $.trim($("#risklevelid").val());
    if (risklevelid == 0) {
        $("#risklevelyy").attr("class", "Validform_checktip Validform_wrong");
        $("#risklevelyy").html("请选择风险等级！");
        return false;
    } else {
        $("#risklevelyy").attr("class", "Validform_checktip  Validform_right");
        $("#risklevelyy").html("验证通过");
    }
}
//添加风险配置
function AddRisklevel() {
    window.parent.ShouwDiaLogWan("添加风险配置", 500, 350, "/payment/RisklevelAddOrUpdate");
}
//编辑
function UpdateEdit(type) {
    window.parent.ShouwDiaLogWan("添加风险配置", 500, 350, "/payment/RisklevelAddOrUpdate?rid=" + type);
}
//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选风险配置！", "error", "");
        return;
    }
    var url = "/payment/RisklevelUpdateState";
    var data = { state: state, ids: vals };
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
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/payment/RisklevelList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var apptypeid = $.trim($("#apptypeid").val());
    var risklevelid = $.trim($("#risklevelid").val());
    var state = $.trim($("#state").val());
    url += "&apptypeid=" + apptypeid + "&risklevelid=" + risklevelid + "&state=" + state;
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