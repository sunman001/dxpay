$(function () {
    //添加或修改应用类型
    $("#btnSaveAddorUpdateAppType").click(function () {
            var t_id =$.trim($("#t_id").val());
            var t_topid =$.trim($("#t_topid").val());
            if (t_topid > -1) {
                $("#yylx").attr("class", "Validform_checktip  Validform_right");
                $("#yylx").html("验证通过");
            } else {
                $("#yylx").attr("class", "Validform_checktip Validform_wrong");
                $("#yylx").html("请选择所属应用类型");
                return false;
            }
            var t_name = $.trim($("#t_name").val());
            var zzyz = /^[\w\W]{1,20}$/;
            if ($.trim(t_name) != "") {
                if (zzyz.test(t_name)) {
                    $("#yyname").attr("class", "Validform_checktip  Validform_right");
                    $("#yyname").html("验证通过");
                } else {
                    $("#yyname").attr("class", "Validform_checktip Validform_wrong");
                    $("#yyname").html("应用类型名称长度不超过20");
                    return false;
                }
            } else {
                $("#yyname").attr("class", "Validform_checktip Validform_wrong");
                $("#yyname").html("应用类型名称不能为空");
                return false;
            }
            var t_sort =$.trim($("#t_sort").val());
            if ($.trim(t_sort) != "") {
                if (!isNaN($.trim(t_sort))) {
                    $("#pxid").attr("class", "Validform_checktip  Validform_right");
                    $("#pxid").html("验证通过");
                } else {
                    $("#pxid").attr("class", "Validform_checktip Validform_wrong");
                    $("#pxid").html("排序为整数");
                    return false;
                }
            } else {
                $("#pxid").attr("class", "Validform_checktip Validform_wrong");
                $("#pxid").html("排序不能为空");
                return false;
            }
            $("#btnSaveAddorUpdateAppType").attr("disabled", "disabled");
            var data = { t_topid: $.trim(t_topid), t_name: $.trim(t_name), t_sort: $.trim(t_sort), t_id: $.trim(t_id) };
            var url = "/APP/InsertOrUpdateAddType";
            $.post(url, data, function (retJson) {
                if (retJson.success == 1) {
                    ShowMsg(retJson.msg, "ok", function () { window.location.reload(); window.parent.layer.closeAll(); });
                }
                else if (retJson.success == 9998) {
                    ShowMsg(retJson.msg, "error", "");
                    return false;
                } else if (retJson.success == 9999) {
                    ShowMsg(retJson.msg, "error", "");
                    window.top.location.href = retJson.Redirect;
                    return false;
                } else if (retJson.success == 9997) {
                    window.top.location.href = retJson.Redirect;
                    return false;
                }
                else {
                    ShowMsg(retJson.msg, "error", "");
                    return false;
                }
                $("#btnSaveAddorUpdateAppType").attr("disabled", false);
            })
    })
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/APP/AppTypeList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var yylx = $("#yylx").val();
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&yylx=" + yylx;
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
function Updatestate(state) {
    var valArr = new Array;
    $("#table").find('input[type="checkbox"]:checked').each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        ShowMsg("请选择应用类型！", "error", "");
        return;
    }
    var url = "/APP/PlUpdateState";
    var data = { state: state, ids: vals, type: "01" };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.location.reload();
            ShowMsg(retJson.msg, "ok", function () { });
        }
        else if (retJson.success == 9998) {
            ShowMsg(retJson.msg, "error", "");
            return false;
        } else if (retJson.success == 9999) {
            ShowMsg(retJson.msg, "error", "");
            window.top.location.href = retJson.Redirect;
            return false;
        } else if (retJson.success == 9997) {
            window.top.location.href = retJson.Redirect;
            return false;
        }
        else {
            ShowMsg(retJson.msg, "error", "");
            return false;
        }
    });
}
//添加应用类型弹窗
function AddAppType() {
    ShouwDiaLogWan("添加应用类型", 750, 350, "/App/AppTypeAdd");
}
//修改应用类型弹窗
function UpdateUser(t_id) {
    ShouwDiaLogWan("修改应用类型", 750, 350, "/App/AppTypeAdd?t_id=" + t_id);
}
//验证排序
function yzpx() {
    var t_sort = $("#t_sort").val();
    if ($.trim(t_sort) != "") {
        if (!isNaN($.trim(t_sort))) {
            $("#pxid").attr("class", "Validform_checktip  Validform_right");
            $("#pxid").html("验证通过");
        } else {
            $("#pxid").attr("class", "Validform_checktip Validform_wrong");
            $("#pxid").html("排序为整数");
            return false;
        }
    } else {
        $("#pxid").attr("class", "Validform_checktip Validform_wrong");
        $("#pxid").html("排序不能为空");
        return false;
    }
}
//验证应用类型名称
function yzyyname() {
    var zzyz = /^[\w\W]{1,20}$/;
    var t_name = $("#t_name").val();
    if ($.trim(t_name) != "") {
        if (zzyz.test(t_name)) {
            $("#yyname").attr("class", "Validform_checktip  Validform_right");
            $("#yyname").html("验证通过");
        } else {
            $("#yyname").attr("class", "Validform_checktip Validform_wrong");
            $("#yyname").html("应用类型名称长度不超过20");
            return false;
        }
    } else {
        $("#yyname").attr("class", "Validform_checktip Validform_wrong");
        $("#yyname").html("应用类型名称不能为空");
        return false;
    }
}
//验证所属应用类型
function yzlx() {
    var t_topid = $("#t_topid").val();
    if (t_topid > -1) {
        $("#yylx").attr("class", "Validform_checktip  Validform_right");
        $("#yylx").html("验证通过");
    } else {
        $("#yylx").attr("class", "Validform_checktip Validform_wrong");
        $("#yylx").html("请选择所属应用类型");
        return false;
    }
}