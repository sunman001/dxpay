$(function () {
    //添加或修改
    $("#btnSaveAddorUpdatpoundage").click(function () {
        var poundagesprice = $.trim($("#poundagesprice").val());
        if (poundagesprice != "") {
            if (!isNaN(poundagesprice)) {
                if (eval(poundagesprice) >= 0) {
                    $("#poundagespriceYz").attr("class", "Validform_checktip  Validform_right");
                    $("#poundagespriceYz").html("验证通过");
                } else {
                    $("#poundagespriceYz").attr("class", "Validform_checktip Validform_wrong");
                    $("#poundagespriceYz").html("开始区间价格必须大于等于零");
                    return false;
                }
            } else {
                $("#poundagespriceYz").attr("class", "Validform_checktip Validform_wrong");
                $("#poundagespriceYz").html("开始区间价格只能为整数");
                return false;
            }
        } else {
            $("#poundagespriceYz").attr("class", "Validform_checktip Validform_wrong");
            $("#poundagespriceYz").html("开始区间价格不能为空");
            return false;
        }
        var poundageeprice = $.trim($("#poundageeprice").val());
        if (poundageeprice != "") {
            if (!isNaN(poundageeprice)) {
                if (eval(poundageeprice) > 0) {
                    if (eval(poundageeprice) > eval(poundagesprice)) {
                        $("#poundageepriceYz").attr("class", "Validform_checktip  Validform_right");
                        $("#poundageepriceYz").html("验证通过");
                    } else {
                        $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
                        $("#poundageepriceYz").html("开始价格不能小于结束价格");
                        return false;
                    }
                } else {
                    $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
                    $("#poundageepriceYz").html("结束价格必须大于零");
                    return false;
                }
            } else {
                $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
                $("#poundageepriceYz").html("结束区间价格只能为整数");
                return false;
            }
        } else {
            $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
            $("#poundageepriceYz").html("结束区间价格不能为空");
            return false;
        }
        var poundagerate = $.trim($("#poundagerate").val());
        var tst = /^\d{1,3}(\.\d{1,4})?$/;
        if (!tst.test(poundagerate)) {
            $("#poundagerateYz").attr("class", "Validform_checktip Validform_wrong");
            $("#poundagerateYz").html("第三方通道手续费最大3位整数或者保留四位小数！");
            return false;
        } else {
            if (poundagerate > 0) {
                $("#poundagerateYz").attr("class", "Validform_checktip  Validform_right");
                $("#poundagerateYz").html("验证成功");
            } else {
                $("#poundagerateYz").attr("class", "Validform_checktip Validform_wrong");
                $("#poundagerateYz").html("第三方通道手续费不能小于或等于零！");
                return false;
            }
        }
        var poundageid = $.trim($("#poundageid").val());
        $("#btnSaveAddorUpdatpoundage").attr("disabled", "disabled");
        var data = { p_sprice: $.trim(poundagesprice), p_eprice: $.trim(poundageeprice), p_rate: $.trim(poundagerate), p_id: $.trim(poundageid) };
        var url = "/System/AddOrUpdateSettlement";
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
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
            $("#btnSaveAddorUpdatpoundage").attr("disabled", false);
        })
    })
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/System/SettlementList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
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
//添加结算弹窗
function AddSettlement() {
    window.parent.ShouwDiaLogWan("添加结算", 880, 350, "/System/SettlementAddOrUpdate");
}
function Updatepoundage(pid) {
    window.parent.ShouwDiaLogWan("修改结算", 880, 350, "/System/SettlementAddOrUpdate?p_id=" + pid);
}
//一键启用或禁用
function UpdateSettlementState(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择结算设置！", "error", "");
        return;
    }
    var url = "/System/UpdatepoundageState";
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
//验证开始价格
function yzpoundagesprice() {
    var poundagesprice = $.trim($("#poundagesprice").val());
    if (poundagesprice != "") {
        if (!isNaN(poundagesprice)) {
            if (eval(poundagesprice) >= 0) {
                $("#poundagespriceYz").attr("class", "Validform_checktip  Validform_right");
                $("#poundagespriceYz").html("验证通过");
            } else {
                $("#poundagespriceYz").attr("class", "Validform_checktip Validform_wrong");
                $("#poundagespriceYz").html("开始区间价格必须大于等于零");
                return false;
            }
        } else {
            $("#poundagespriceYz").attr("class", "Validform_checktip Validform_wrong");
            $("#poundagespriceYz").html("开始区间价格只能为整数");
            return false;
        }
    } else {
        $("#poundagespriceYz").attr("class", "Validform_checktip Validform_wrong");
        $("#poundagespriceYz").html("开始区间价格不能为空");
        return false;
    }
}
//验证结束价格
function yzpoundageeprice() {
    var poundageeprice = $.trim($("#poundageeprice").val());
    var poundagesprice = $.trim($("#poundagesprice").val());
    if (poundageeprice != "") {
        if (!isNaN(poundageeprice)) {
            if (eval(poundageeprice) > 0) {
                if (eval(poundageeprice) > eval(poundagesprice)) {
                    $("#poundageepriceYz").attr("class", "Validform_checktip  Validform_right");
                    $("#poundageepriceYz").html("验证通过");
                } else {
                    $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
                    $("#poundageepriceYz").html("开始价格不能小于结束价格");
                    return false;
                }
            } else {
                $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
                $("#poundageepriceYz").html("结束价格必须大于零");
                return false;
            }
        } else {
            $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
            $("#poundageepriceYz").html("结束区间价格只能为整数");
            return false;
        }
    } else {
        $("#poundageepriceYz").attr("class", "Validform_checktip Validform_wrong");
        $("#poundageepriceYz").html("结束区间价格不能为空");
        return false;
    }
}
//验证手续费
function yzpoundagerate() {
    var poundagerate = $.trim($("#poundagerate").val());
    var tst = /^\d{1,3}(\.\d{1,4})?$/;
    if (!tst.test(poundagerate)) {
        $("#poundagerateYz").attr("class", "Validform_checktip Validform_wrong");
        $("#poundagerateYz").html("第三方通道手续费最大3位整数或者保留四位小数！");
        return false;
    } else {
        if (poundagerate > 0) {
            $("#poundagerateYz").attr("class", "Validform_checktip  Validform_right");
            $("#poundagerateYz").html("验证成功");
        } else {
            $("#poundagerateYz").attr("class", "Validform_checktip Validform_wrong");
            $("#poundagerateYz").html("第三方通道手续费不能小于或等于零！");
            return false;
        }
    }
}