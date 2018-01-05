
$(function () {
    
    //添加应用
    $("#btnSaveAddApp").click(function () {
        var inputName = $.trim($("#inputName").val());
        var zzyz = /^[\w\W]{1,20}$/;
        if ($.trim(inputName) != "") {
            if (zzyz.test(inputName)) {
                $("#nameyy").attr("class", "Validform_checktip  Validform_right");
                $("#nameyy").html("验证通过");
            } else {
                $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#nameyy").html("应用名称长度不超过20");
                window.parent.ShowMsg("应用名称长度不超过20！", "error", "");
                return false;
            }
        } else {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("应用名称不能为空");
            window.parent.ShowMsg("应用名称不能为空！", "error", "");
            return false;
        }
        var a_platform_id = $.trim($("#a_platform_id").val());
        var valArr = new Array;
        $('input[name="zflx"]:checked').each(function (i) {
            valArr[i] = $(this).val();
        });
        var vals = valArr.join(',');
        if (vals == "") {
            window.parent.ShowMsg("请选择支付类型！", "error", "");
            return false;
        }
        var a_paymode_id = vals;
        var xzdx = $('input[name="zlxz"]:checked ').val();
        if ($.trim(xzdx) == "") {
            window.parent.ShowMsg("请选择应用类型！", "error", "");
            return false;
        }
        var aurl = $.trim($("#aurl").val());
        var testurl = /^((https|http)?:\/\/)[^\s]+/;
        if (testurl.test($.trim(aurl))) {
            $("#yzdz").attr("class", "Validform_checktip  Validform_right");
            $("#yzdz").html("验证通过");
        } else {
            $("#yzdz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzdz").html("请正确填写回调地址(必须以http://或者https://开头)");
            return false;
        }
        var o_showaddress = $("#o_showaddress").val();
        if (a_platform_id == 3) {
            if (testurl.test($.trim(o_showaddress))) {
                $("#yzo_showaddress").attr("class", "Validform_checktip  Validform_right");
                $("#yzo_showaddress").html("验证通过");
            } else {
                $("#yzo_showaddress").attr("class", "Validform_checktip Validform_wrong");
                $("#yzo_showaddress").html("请正确填写回调地址(必须以http://或者https://开头)");
                return false;
            }
        }
        var a_user_idyx = $.trim($("#a_user_idyx").val());
        var a_user_id = $.trim($("#a_user_id").val());
        if ($.trim(a_user_idyx) != "") {
            if ($.trim(a_user_id) != "") {
                if (!isNaN($.trim(a_user_id))) {
                    $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
                    $("#yzkfz").html("验证通过");
                } else {
                    $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
                    $("#yzkfz").html("请选择开发者");
                    return false;
                }
            } else {
                $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
                $("#yzkfz").html("请选择开发者");
                return false;
            }
        } else {
            $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz").html("请选择开发者");
            return false;
        }
        var a_appurl = $("#a_appurl").val();
        if (testurl.test($.trim(a_appurl))) {
            $("#yz_a_appurl").attr("class", "Validform_checktip  Validform_right");
            $("#yz_a_appurl").html("验证通过");
        } else {
            $("#yz_a_appurl").attr("class", "Validform_checktip Validform_wrong");
            $("#yz_a_appurl").html("请正确填写回调地址(必须以http://或者https://开头)");
            return false;
        }
        var a_appsynopsis = $.trim($("#a_appsynopsis").val());
        if ($.trim(a_appsynopsis) != "") {
            $("#yz_a_appsynopsis").attr("class", "Validform_checktip  Validform_right");
            $("#yz_a_appsynopsis").html("验证通过");
        } else {
            $("#yz_a_appsynopsis").attr("class", "Validform_checktip Validform_wrong");
            $("#yz_a_appsynopsis").html("应用简介不能为空");
            return false;
        }

        $("#btnSaveAddApp").attr("disabled", "disabled");
        var data = { a_name: $.trim(inputName), a_platform_id: $.trim(a_platform_id), a_paymode_id: $.trim(a_paymode_id), a_apptype_id: $.trim(xzdx), a_notifyurl: $.trim(aurl), a_user_id: $.trim(a_user_id), a_showurl: $.trim(o_showaddress), a_appurl: $.trim(a_appurl), a_appsynopsis: $.trim(a_appsynopsis) };
        var url = "/APP/InsertUpdateApp";
        $.post(url, data, function (retJson) {
            $("#btnSaveAddApp").attr("disabled", false);
            if (retJson.success == 1) {
                //window.parent.frames[window.top.global.currentTabId].location.reload();
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
    //修改应用
    $("#btnSaveUpdateApp").click(function () {
        var inputName = $.trim($("#inputName").val());
        var zzyz = /^[\w\W]{1,20}$/;
        if ($.trim(inputName) != "") {
            if (zzyz.test(inputName)) {
                $("#nameyy").attr("class", "Validform_checktip  Validform_right");
                $("#nameyy").html("验证通过");
            } else {
                $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#nameyy").html("应用名称长度不超过20");
                return false;
            }
        } else {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("应用名称不能为空");
            return false;
        }
        var a_platform_id = $.trim($("#a_platform_id").val());
        var valArr = new Array;
        $('input[name="zflx"]:checked').each(function (i) {
            valArr[i] = $(this).val();
        });
        var vals = valArr.join(',');
        if (vals == "") {
            window.parent.ShowMsg("请选择支付类型！", "error", "");
            return false;
        }
        var a_paymode_id = vals;
        var xzdx = $('input[name="zlxz"]:checked ').val();
        if ($.trim(xzdx) == "") {
            window.parent.ShowMsg("请选择应用类型！", "error", "");
            return false;
        }
        var aurl = $.trim($("#aurl").val());
        var testurl = /^((https|http)?:\/\/)[^\s]+/;
        if (testurl.test($.trim(aurl))) {
            $("#yzdz").attr("class", "Validform_checktip  Validform_right");
            $("#yzdz").html("验证通过");
        } else {
            $("#yzdz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzdz").html("请正确填写回调地址(必须以http://或者https://开头)");
            return false;
        }
        var o_showaddress = $("#o_showaddress").val();
        if (a_platform_id == 3) {
            if (testurl.test($.trim(o_showaddress))) {
                $("#yzo_showaddress").attr("class", "Validform_checktip  Validform_right");
                $("#yzo_showaddress").html("验证通过");
            } else {
                $("#yzo_showaddress").attr("class", "Validform_checktip Validform_wrong");
                $("#yzo_showaddress").html("请正确填写回调地址(必须以http://或者https://开头)");
                return false;
            }
        }
        var a_user_idyx = $.trim($("#a_user_idyx").val());
        var a_user_id = $.trim($("#a_user_id").val());
        if ($.trim(a_user_idyx) != "") {
            if ($.trim(a_user_id) != "") {
                if (!isNaN($.trim(a_user_id))) {
                    $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
                    $("#yzkfz").html("验证通过");
                } else {
                    $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
                    $("#yzkfz").html("请选择开发者");
                    return false;
                }
            } else {
                $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
                $("#yzkfz").html("请选择开发者");
                return false;
            }
        } else {
            $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz").html("请选择开发者");
            return false;
        }
        var a_appurl = $("#a_appurl").val();
        if (testurl.test($.trim(a_appurl))) {
            $("#yz_a_appurl").attr("class", "Validform_checktip  Validform_right");
            $("#yz_a_appurl").html("验证通过");
        } else {
            $("#yz_a_appurl").attr("class", "Validform_checktip Validform_wrong");
            $("#yz_a_appurl").html("请正确填写回调地址(必须以http://或者https://开头)");
            return false;
        }
        var a_appsynopsis = $.trim($("#a_appsynopsis").val());
        if ($.trim(a_appsynopsis) != "") {
            $("#yz_a_appsynopsis").attr("class", "Validform_checktip  Validform_right");
            $("#yz_a_appsynopsis").html("验证通过");
        } else {
            $("#yz_a_appsynopsis").attr("class", "Validform_checktip Validform_wrong");
            $("#yz_a_appsynopsis").html("应用简介不能为空");
            return false;
        }

        //var a_rid = $.trim($("#a_rid").val());
        //if ($.trim(a_rid) == 0) {
        //    $("#a_ridyy").attr("class", "Validform_checktip Validform_wrong");
        //    $("#a_ridyy").html("请选择开发者");
        //    return false;
        //} else {
        //    $("#a_ridyy").attr("class", "Validform_checktip  Validform_right");
        //    $("#a_ridyy").html("验证通过");
        //}

        var a_id = $.trim($("#a_id").val());
        var data = { a_name: $.trim(inputName), a_platform_id: $.trim(a_platform_id), a_paymode_id: $.trim(a_paymode_id), a_apptype_id: $.trim(xzdx), a_notifyurl: $.trim(aurl), a_user_id: $.trim(a_user_id), a_id: $.trim(a_id), a_showurl: $.trim(o_showaddress), a_appurl: $.trim(a_appurl), a_appsynopsis: $.trim(a_appsynopsis) };
        var url = "/APP/InsertUpdateApp";
        $("#btnSaveUpdateApp").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            $("#btnSaveUpdateApp").attr("disabled", false);
            if (retJson.success == 1) {
                // window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.frames['mainFrame'].location.reload(); window.parent.layer.closeAll(); });
                //window.parent.frames[window.top.global.currentTabId].location.reload();
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
    //添加或修改商品
    $("#btnSaveAddorUpdateGoods").click(function () {
        var g_app_id = $.trim($("#g_app_id").val());
        var g_id = $.trim($("#g_id").val());
        var g_name = $.trim($("#g_name").val());
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
        var pdlx = $.trim($("#g_saletype_id").val());
        var g_saletype_id = pdlx.split(',')[0];
        var g_price = "0";
        if (pdlx.split(',')[1] == "1") {
            g_price = $.trim($("#g_price").val());
            var tst = /^\d{1,}(\.\d{2})?$/;
            if (!tst.test(g_price)) {
                $("#yzjg").attr("class", "Validform_checktip Validform_wrong");
                $("#yzjg").html("销售价格为整数或者保留两位小数！");
                return false;
            } else {
                if (g_price > 0) {
                    $("#yzjg").attr("class", "Validform_checktip  Validform_right");
                    $("#yzjg").html("验证成功");
                } else {
                    $("#yzjg").attr("class", "Validform_checktip Validform_wrong");
                    $("#yzjg").html("销售价格不能小于或等于零！");
                    return false;
                }
            }
        }
        $("#btnSaveAddorUpdateGoods").attr("disabled", "disabled");
        var url = "/APP/InsertOrUpdateAddGOODS";
        var data = { g_name: $.trim(g_name), g_saletype_id: $.trim(g_saletype_id), g_price: $.trim(g_price), g_app_id: $.trim(g_app_id), g_id: $.trim(g_id) };
        $.post(url, data, function (retJson) {
            $("#btnSaveAddorUpdateGoods").attr("disabled", false);
            if (retJson.success == 1) {
                //window.parent.frames[window.top.global.currentTabId].location.reload();
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
    //展开或关闭商品信息
    $("td[name='zkgb']").click(function () {
        var zkgb = this.id;//获取点击的当前id
        var xs = "xs+" + zkgb;
        var cpr = $("#" + zkgb).find("i");
        var cls = cpr.hasClass("fa-plus-circle");
        //debugger;
        if (cls) {
            cpr.removeClass("fa-plus-circle");
            cpr.addClass("fa-minus-circle");
            document.getElementById(xs).style.display = "";
        } else {
            cpr.addClass("fa-plus-circle");
            cpr.removeClass("fa-minus-circle");
            document.getElementById(xs).style.display = "none";
        }
    })
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var regnumber = new RegExp("^[0-9]*$");
    var url = "/APP/AppList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var platformid = $.trim($("#platformid").val());
    var appType = $.trim($("#appType").val());
    var r_id = $.trim($("#r_id").val());
    var paytype = $.trim($("#paytype").val());
    if (searchType == 1) {
        var sea_name = $("#sea_name").val();
        if (!regnumber.test(sea_name)) {
            window.parent.ShowMsg("应用编号只能输入数字", "error", "");
            return false
        }
    }
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&platformid=" + platformid + "&appType=" + appType + "&r_id=" + r_id + "&paytype=" + paytype;
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
//添加应用弹窗
function AddAPPlog() {
    window.parent.ShouwDiaLogWan("添加应用", 950, 600, "/App/APPAdd");
}
//验证应用名称
function yzname() {
    var zzyz = /^[\w\W]{1,20}$/;
    var inputName = $("#inputName").val();
    if ($.trim(inputName) != "") {
        if (zzyz.test(inputName)) {
            $("#nameyy").attr("class", "Validform_checktip  Validform_right");
            $("#nameyy").html("验证通过");
        } else {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("应用名称长度不超过20");
            return false;
        }
    } else {
        $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#nameyy").html("应用名称不能为空");
        return false;
    }
}
//验证回调地址
function dzyz() {
    //var testurl = /^(\w+:\/\/)?\w+(\.\w+)+.*$/;
    var testurl = /^((https|http)?:\/\/)[^\s]+/;
    var aurl = $("#aurl").val();
    if (testurl.test($.trim(aurl))) {
        $("#yzdz").attr("class", "Validform_checktip  Validform_right");
        $("#yzdz").html("验证通过");
    } else {
        $("#yzdz").attr("class", "Validform_checktip Validform_wrong");
        $("#yzdz").html("请正确填写回调地址(必须以http://或者https://开头)");
        return false;
    }
}
//验证同步回调地址
function tbdzyz() {
    var testurl = /^((https|http)?:\/\/)[^\s]+/;
    var o_showaddress = $("#o_showaddress").val();
    if (testurl.test($.trim(o_showaddress))) {
        $("#yzo_showaddress").attr("class", "Validform_checktip  Validform_right");
        $("#yzo_showaddress").html("验证通过");
    } else {
        $("#yzo_showaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#yzo_showaddress").html("请正确填写回调地址(必须以http://或者https://开头)");
        return false;
    }
}
//验证应用地址
function a_appurlyz() {
    var testurl = /^((https|http)?:\/\/)[^\s]+/;
    var a_appurl = $("#a_appurl").val();
    if (testurl.test($.trim(a_appurl))) {
        $("#yz_a_appurl").attr("class", "Validform_checktip  Validform_right");
        $("#yz_a_appurl").html("验证通过");
    } else {
        $("#yz_a_appurl").attr("class", "Validform_checktip Validform_wrong");
        $("#yz_a_appurl").html("请正确填写应用地址(必须以http://或者https://开头)");
        return false;
    }
}
//验证应用简介
function yza_appsynopsis() {
    var a_appsynopsis = $.trim($("#a_appsynopsis").val());
    if ($.trim(a_appsynopsis) != "") {
        $("#yz_a_appsynopsis").attr("class", "Validform_checktip  Validform_right");
        $("#yz_a_appsynopsis").html("验证通过");
    } else {
        $("#yz_a_appsynopsis").attr("class", "Validform_checktip Validform_wrong");
        $("#yz_a_appsynopsis").html("应用简介不能为空");
        return false;
    }
}

//验证开发者
function kfz() {
    var a_user_idyx = $("#a_user_idyx").val();
    if ($.trim(a_user_idyx) == "") {
        $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
        $("#yzkfz").html("请选择开发者");
        return false;
    } else {
        $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
        $("#yzkfz").html("验证通过");
    }
}
//验证风险等级
function yza_rid() {
    var a_rid = $.trim($("#a_rid").val());
    if ($.trim(a_rid) == 0) {
        $("#a_ridyy").attr("class", "Validform_checktip Validform_wrong");
        $("#a_ridyy").html("请选择风控等级");
        return false;
    } else {
        $("#a_ridyy").attr("class", "Validform_checktip  Validform_right");
        $("#a_ridyy").html("验证通过");
    }
}
//修改应用弹窗
function UpdateApp(a_id) {

    window.parent.ShouwDiaLogWan("修改应用", 950, 600, "/APP/UpdateAPP?a_id=" + a_id);
}
//添加商品弹窗
function AddAppSp(a_id) {
    window.parent.ShouwDiaLogWan("添加商品", 655, 300, "/APP/AddGOODS?a_id=" + a_id);
}


//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择应用！", "error", "");
        return;
    }
    var url = "/APP/UpdateState";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            //window.parent.frames[window.top.global.currentTabId].location.reload();
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
//冻结或解冻应用
function Delete(a_id, state) {
    var url = "/APP/UpdateStateDt";
    var data = { a_id: a_id, state: state };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            //window.parent.frames[window.top.global.currentTabId].location.reload();
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
//查询子类应用
function xzyylx(id, a_apptype_id) {
    var data = { id: $.trim(id), a_apptype_id: a_apptype_id };
    var url = "/APP/SelectApp";
    $.post(url, data, function (msg) {
        $("#zlyy").html(msg);
        $("input[name='yyname']").removeClass("xzinput");
        $("input[name='yyname']").addClass("inpuwxz");
        $("#" + id).removeClass("inpuwxz");
        $("#" + id).addClass("xzinput");
    })
}
//编辑商品弹窗
function UpdateUserGoods(g_id) {
    window.parent.ShouwDiaLogWan("修改商品", 800, 350, "/APP/AddGOODS?g_id=" + g_id);
}
//冻结或解冻商品
function UpdateStateGoods(state, g_id) {
    var url = "/APP/UpdateStateSp";
    var data = { state: state, ids: g_id, type: "01" };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            //window.parent.frames[window.top.global.currentTabId].location.reload();
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
//验证商品
function xsxsjg() {
    var pdlx = $("#g_saletype_id").val();
    if (pdlx.split(',')[1] == "0") {
        document.getElementById("xsjg").style.display = "none";
    } else {
        document.getElementById("xsjg").style.display = "";

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

//弹窗开发者列表
function xzuser() {
    window.parent.ShouwDiaLogWan("选择开发者", 1000, 700, "/App/UserList");

}
//选择开发者用户
function yxuzyhuser(u_id, u_email, index) {
    window.parent.layer.getChildFrame("#a_user_idyx", index).val(u_email);
    window.parent.layer.getChildFrame("#a_user_id", index).val(u_id);
    window.parent.layer.getChildFrame("#yzkfz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#yzkfz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}
//查询用户列表
function selectUserLiset() {
    //当前页
    var CurrcentPage = $("#curr_page").val();
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(CurrcentPage, PageSize);
}
//加载数据
function LoadData(currPage, pageSize) {
    var url = "/APP/UserList?curr=" + currPage + "&psize=" + pageSize;
    var type = $("#s_type").val();
    var keys = $("#s_keys").val();
    var state = $("#s_state").val();
    var check = $("#s_check").val();
    var rzlx = $("#s_category").val();
    var sort = $("#s_sort").val();
    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx;
    location.href = encodeURI(url);
}
//判读支付平台
function glpt() {
    var a_platform_id = $.trim($("#a_platform_id").val());
    if (a_platform_id == "3") {
        document.getElementById("tbhddz").style.display = "";
        isH5("3");
    } else {
        IsSdk(a_platform_id);
        document.getElementById("tbhddz").style.display = "none";
    }
}

//判断是否为h5平台
function isH5(obj) {
    document.getElementById("paytype_5").checked = false;   
    if (obj == 3) {
        //if ($("#paytype_4").data("stat") == 1) {
        //    document.getElementById("paytype_4").disabled = false;
        //}
        if ($("#paytype_6").data("stat") == 1) {
            document.getElementById("paytype_6").disabled = false;
        }
        if ($("#paytype_7").data("stat") == 1) {
            document.getElementById("paytype_7").disabled = false;
        }
        var state = $("#paytype_5").data("stat");
        if (state == 1) {
            document.getElementById("paytype_5").disabled = true;
        }
    }
}
//判断是否为sdk时调用
function IsSdk(obj) {
    if (obj == 1 || obj == 2) {
       // document.getElementById("paytype_4").checked = false;
        document.getElementById("paytype_6").checked = false;
        document.getElementById("paytype_7").checked = false;
        if ($("#paytype_5").data("stat") == 1) {
            document.getElementById("paytype_5").disabled = false;
        }
        //if ($("#paytype_4").data("stat") == 1) {
        //    document.getElementById("paytype_4").disabled = true;
        //}
        if ($("#paytype_6").data("stat") == 1) {
            document.getElementById("paytype_6").disabled = true;
        }
        if ($("#paytype_7").data("stat") == 1) {
            document.getElementById("paytype_7").disabled = true;
        }
    }
}
//审核
function updateSh(aid) {

    window.parent.ShouwDiaLogWan("审核应用", 500, 350, "/APP/AppAuditing?a_id=" + aid);
}
//审核方法
function SaveUpdateAppAuditing() {
    var a_rid = $("#a_rid").val();

    if (a_rid == 0) {
        $("#a_ridyy").attr("class", "Validform_checktip Validform_wrong");
        $("#a_ridyy").html("请选择风控等级");
        return false;
    }

    var a_auditstate = $.trim($('input[name="a_auditstate"]:checked').val());
    var aid = $("#aid").val();

    var url = "/APP/UpdateAppAuditing";
    var data = { aid: $.trim(aid), a_rid: $.trim(a_rid), a_auditstate: $.trim(a_auditstate) };

    $.post(url, data, function (result) {

        if (result.success == 1) {
            window.parent.ShowMsg(result.msg, "ok", function () {
                window.parent.global.reload();
                window.parent.layer.closeAll();
            });
        }
        else {
            window.parent.ShowMsg(result.msg, "error", "");
        }

    })

}

//设置扣量
function getkl(obj) {
    window.parent.ShouwDiaLogWan("通道费率设置", 800, 510, "/App/AppKl?appid=" + obj);
}
