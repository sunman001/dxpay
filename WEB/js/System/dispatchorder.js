$(function () {
    $("#btnSaveAddorUpdatddorder").click(function () {
        var apptyeid = $.trim($("#apptyeid").val());
        if (apptyeid == "0") {
            $("#yytypeyy").attr("class", "Validform_checktip Validform_wrong");
            $("#yytypeyy").html("请选择应用类型！");
            return false;
        } else {
            $("#yytypeyy").attr("class", "Validform_checktip  Validform_right");
            $("#yytypeyy").html("验证成功");
        }
        var ratio = $.trim($("#ratio").val());
        var tst = /^\d{1,3}(\.\d{1,4})?$/;
        if (!tst.test(ratio)) {
            $("#ratioYz").attr("class", "Validform_checktip Validform_wrong");
            $("#ratioYz").html("掉单比列最大3位整数或者保留四位小数！");
            return false;
        } else {
            //if (ratio > 0) {
                $("#ratioYz").attr("class", "Validform_checktip  Validform_right");
                $("#ratioYz").html("验证成功");
            //} else {
            //    $("#ratioYz").attr("class", "Validform_checktip Validform_wrong");
            //    $("#ratioYz").html("掉单比列不能小于或等于零！");
            //    return false;
            //}
        }
        var did = $.trim($("#did").val());
        var data = { d_apptyeid: apptyeid, d_ratio: ratio, d_id: did };
        $("#btnSaveAddorUpdatddorder").attr("disabled", "disabled");
        var url = "/System/AddOrUpdatedispatchorder";
        $.post(url, data, function (retJson) {
            $("#btnSaveAddorUpdatddorder").attr("disabled", false);
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
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/System/dispatchorderLsit?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
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
//添加
function adddispatchorderLsit() {
    window.parent.ShouwDiaLogWan("添加掉单", 600, 360, "/System/Adddispatchorder");
}
//编辑
function Updateorde(did) {
    window.parent.ShouwDiaLogWan("添加掉单", 600, 360, "/System/Adddispatchorder?did=" + did);
}
//验证手续费
function ratio() {
    var ratio = $.trim($("#ratio").val());
    var tst = /^\d{1,3}(\.\d{1,4})?$/;
    if (!tst.test(ratio)) {
        $("#ratioYz").attr("class", "Validform_checktip Validform_wrong");
        $("#ratioYz").html("掉单比列最大3位整数或者保留四位小数！");
        return false;
    } else {
        //if (ratio > 0) {
            $("#ratioYz").attr("class", "Validform_checktip  Validform_right");
            $("#ratioYz").html("验证成功");
        //} else {
            //$("#ratioYz").attr("class", "Validform_checktip Validform_wrong");
            //$("#ratioYz").html("掉单比列不能小于或等于零！");
            //return false;
        //}
    }
}
//验证应用
function yytype() {
    var apptyeid = $.trim($("#apptyeid").val());
    if (apptyeid == "0") {
        $("#yytypeyy").attr("class", "Validform_checktip Validform_wrong");
        $("#yytypeyy").html("请选择应用类型！");
        return false;
    } else {
        $("#yytypeyy").attr("class", "Validform_checktip  Validform_right");
        $("#yytypeyy").html("验证成功");
    }
}
//一键启用或禁用
function UpdateddState(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择掉单设置！", "error", "");
        return;
    }
    var url = "/System/UpdateDisoOderState";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
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
    });
}