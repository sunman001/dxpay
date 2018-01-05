//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/APP/GOODSList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc;
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
//编辑商品弹窗
function UpdateUser(g_id) {
    window.parent.ShouwDiaLogWan("修改商品", 800, 350, "/APP/AddGOODS?g_id=" + g_id);
}
//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table").find('input[type="checkbox"]:checked').each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择商品！", "error", "");
        return;
    }
    var url = "/APP/UpdateStateSp";
    var data = { state: state, ids: vals };
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
//验证商品
function xsxsjg() {
    var pdlx = $("#g_saletype_id").val();
    if (pdlx.split(',')[1] == "0") {
        document.getElementById("xsjg").style.display = "";
    } else {
        document.getElementById("xsjg").style.display = "none";
    }
}
//验证商品价格
function g_priceChange() {
    var tst = /^\d{1,}(\.\d{2})?$/;
    if (!tst.test($("#g_price").val())) {
        $("#yzjg").attr("class", "Validform_checktip Validform_wrong");
        $("#yzjg").html("销售价格为整数或者保留两位小数！");
        return false;
    } else {
        if ($("#g_price").val() > 0) {
            $("#yzjg").attr("class", "Validform_checktip  Validform_right");
            $("#yzjg").html("验证成功");
        } else {
            $("#yzjg").attr("class", "Validform_checktip Validform_wrong");
            $("#yzjg").html("销售价格不能小于或等于零！");
            return false;
        }
    }
}
//验证商品名称
function yzsp() {
    var g_name = $("#g_name").val();
    var zzyz = /^[\w\W]{1,20}$/;
    if ($.trim(g_name) != "") {
        if (zzyz.test(g_name)) {
            $("#spname").attr("class", "Validform_checktip  Validform_right");
            $("#spname").html("验证通过");
        } else {
            $("#spname").attr("class", "Validform_checktip Validform_wrong");
            $("#spname").html("商品名称长度不超过20");
            return false;
        }
    } else {
        $("#spname").attr("class", "Validform_checktip Validform_wrong");
        $("#spname").html("商品名称不能为空");
        return false;
    }
}
//添加商品
//function UpDateGoodsSp(index) {
//    var g_app_id = window.parent.layer.getChildFrame("#g_app_id", index).val();
//    var g_id = window.parent.layer.getChildFrame("#g_id", index).val();
//    var g_name = window.parent.layer.getChildFrame("#g_name", index).val();
//    var zzyz = /^[\w\W]{1,20}$/;
//    if ($.trim(g_name) != "") {
//        if (zzyz.test(g_name)) {
//            window.parent.layer.getChildFrame("#spname", index).attr("class", "Validform_checktip  Validform_right");
//            window.parent.layer.getChildFrame("#spname", index).html("验证通过");
//        } else {
//            window.parent.layer.getChildFrame("#spname", index).attr("class", "Validform_checktip Validform_wrong");
//            window.parent.layer.getChildFrame("#spname", index).html("商品名称长度不超过20");
//            return false;
//        }
//    } else {
//        window.parent.layer.getChildFrame("#spname", index).attr("class", "Validform_checktip Validform_wrong");
//        window.parent.layer.getChildFrame("#spname", index).html("商品名称不能为空");
//        return false;
//    }
//    var pdlx = window.parent.layer.getChildFrame("#g_saletype_id", index).val();
//    var g_saletype_id = pdlx.split(',')[0];
//    var g_price = "0";
//    if (pdlx.split(',')[1] == "1") {
//        g_price = window.parent.layer.getChildFrame("#g_price", index).val();
//        var tst = /^\d{1,}(\.\d{2})?$/;
//        if (!tst.test(g_price)) {
//            window.parent.layer.getChildFrame("#yzjg", index).attr("class", "Validform_checktip Validform_wrong");
//            window.parent.layer.getChildFrame("#yzjg", index).html("销售价格为整数或者保留两位小数！");
//            return false;
//        } else {
//            if (g_price > 0) {
//                window.parent.layer.getChildFrame("#yzjg", index).attr("class", "Validform_checktip  Validform_right");
//                window.parent.layer.getChildFrame("#yzjg", index).html("验证成功");
//            } else {
//                window.parent.layer.getChildFrame("#yzjg", index).attr("class", "Validform_checktip Validform_wrong");
//                window.parent.layer.getChildFrame("#yzjg", index).html("销售价格不能小于或等于零！");
//                return false;
//            }
//        }
//    }
//    var url = "/APP/InsertOrUpdateAddGOODS";
//    var data = { g_name: g_name, g_saletype_id: g_saletype_id, g_price: g_price, g_app_id: g_app_id, g_id: g_id };
//    $.post(url, data, function (retJson) {
//        if (retJson.success == 1) {
//            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.location.reload(); window.parent.layer.closeAll(); });
//        }
//        else if (retJson.success == 9998) {
//            window.parent.ShowMsg(retJson.msg, "error", "");
//            return false;
//        } else if (retJson.success == 9999) {
//            window.parent.ShowMsg(retJson.msg, "error", "");
//            window.top.location.href = retJson.Redirect;
//            return false;
//        }
//        else {
//            window.parent.ShowMsg(retJson.msg, "error", "");
//            return false;
//        }
//    })
//}