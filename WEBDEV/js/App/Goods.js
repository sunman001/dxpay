var msg = "";
$(function () {
    //添加商品弹窗
    $("#addsp").click(function () {
        var appid = $.trim($("#appid").val());
        window.parent.ShouwDiaLogWan("添加商品", 600, 350, "/App/AddGoods?appid=" + appid);
    })
    //添加商品
    $("#btnSaveAddorUpdateGoods").click(function () {
        var g_app_id = $.trim($("#appid").val());
        var g_id = $.trim($("#g_id").val());
        var g_name = $("#g_name").val();
        var zzyz = /^[\w\W]{1,20}$/;
        if ($.trim(g_name) != "") {
            if (zzyz.test(g_name)) {
                objToolTip("g_name", 3, "");
            } else {
                msg = "商品名称长度不超过20";
                objToolTip("g_name", 4, msg);
                return false;
            }
        } else {
            msg = "商品名称不能为空";
            objToolTip("g_name", 4, msg);
            return false;
        }
        var pdlx = $.trim($("#g_saletype_id").val());
        var g_saletype_id = pdlx.split(',')[0];
        var g_price = "0";
        if (pdlx.split(',')[1] == "1") {
            g_price = $.trim($("#g_price").val());
            var tst = /^\d{1,}(\.\d{2})?$/;
            if (!tst.test(g_price)) {
                msg = "销售价格为整数或者保留两位小数";
                objToolTip("g_price", 4, msg);
                return false;
            } else {
                if (g_price > 0) {
                    objToolTip("g_price", 3, "");
                } else {
                    msg = "销售价格不能小于或等于零！";
                    objToolTip("g_price", 4, msg);
                    return false;
                }
            }
        }
        $("#btnSaveAddorUpdateGoods").attr("disabled", "disabled");
        var url = "/App/InsertOrUpdateAddGOODS";
        var data = { g_name: $.trim(g_name), g_saletype_id: $.trim(g_saletype_id), g_price: $.trim(g_price), g_app_id: $.trim(g_app_id), g_id: $.trim(g_id) };
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.frames['mainFrame'].location.reload(); btnCodesc(); });
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
             $("#btnSaveAddorUpdateGoods").attr("disabled", false);
        })
    })
    //商品界面下一部
    $("#Goodslistbtn").click(function () {
        var appid = $.trim($("#appid").val());
        var listcount = $.trim($("#listcount").val());
        if (eval(listcount) > 0) {
            location.href = encodeURI("/App/pay?appid=" + appid);
        } else {
            window.parent.ShowMsg("请添加商品！", "error", "");
        }
    })
})
//编辑商品弹窗
function UpdateUser(g_id) {
    var appid = $.trim($("#appid").val());
    window.parent.ShouwDiaLogWan("添加商品", 600, 350, "/App/AddGoods?appid=" + appid + "&g_id=" + g_id);
}
//删除商品
function UpdateDelet(g_id) {
    var appid = $.trim($("#appid").val());
    var data = { appid: $.trim(appid), g_id: $.trim(g_id) };
    var url = "/App/UpdateDeletGoods";
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.frames['mainFrame'].location.reload(); });
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
//验证商品名称
function yzsp() {
    var g_name = $("#g_name").val();
    var zzyz = /^[\w\W]{1,20}$/;
    if ($.trim(g_name) != "") {
        if (zzyz.test(g_name)) {
            objToolTip("g_name", 3, "");
        } else {
            msg = "商品名称长度不超过20";
            objToolTip("g_name", 4, msg);
            return false;
        }
    } else {
        msg = "商品名称不能为空";
        objToolTip("g_name", 4, msg);
        return false;
    }
}
//验证商品价格
function g_priceChange() {
    var tst = /^\d{1,}(\.\d{2})?$/;
    if (!tst.test($("#g_price").val())) {
        msg = "销售价格为整数或者保留两位小数";
        objToolTip("g_price", 4, msg);
        return false;
    } else {
        if ($("#g_price").val() > 0) {
            objToolTip("g_price", 3, "");
        } else {
            msg = "销售价格不能小于或等于零";
            objToolTip("g_price", 4, msg);
            return false;
        }
    }
}
//验证商品
function xsxsjg() {
    var pdlx = $("#g_saletype_id").val();
    if (pdlx.split(',')[1] == "0") {
        document.getElementById("xsjg").style.display = "none";
    } else {
        document.getElementById("xsjg").style.display = "";

    }
}