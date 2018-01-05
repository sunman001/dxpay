$(function () {
    //添加或修改支付配置
    $("#btnSavezfpz").click(function () {
        var typeid = $.trim($("#paymenttype").val());
        var type = typeid.split(',')[1];
        var tszf = /^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$/;//验证特殊字符正则表达式
        var lid = $.trim($("#lid").val());
        var l_str = "";//组装配置信息
        var data = "";//数据集
        var l_isenable = $.trim($("#l_isenable").val())
        if (eval(l_isenable) == 1 || eval(l_isenable) == 4) {
            $("#l_isenableyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_isenableyy").html("验证通过");
        } else {
            $("#l_isenableyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_isenableyy").html("请选择通道状态！");
            return false;
        }

        var l_risk = $.trim($("#l_risk").val())
        if (eval(l_risk) < 0 || eval(l_risk) > 2) {
            $("#l_riskyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_riskyy").html("请选择风控类型！");
            return false;
        } else {
            $("#l_riskyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_riskyy").html("验证通过");
        }
        var paymodeid = $.trim($("#paymodeid").val());
        if ($.trim(paymodeid) == 0) {
            $("#zftypeyy").attr("class", "Validform_checktip Validform_wrong");
            $("#zftypeyy").html("请选择支付类型！");
            return false;
        }
        var l_apptypeid = "";
        var appNameid = $.trim($("#appName").val());
        if ($.trim(appNameid) == "") {
            $("#appNameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#appNameyy").html("请选择应用！");
            return false;
        } else {
            $("#appNameyy").attr("class", "Validform_checktip  Validform_right");
            $("#appNameyy").html("验证通过");
            l_apptypeid = appNameid;
        }

        var corporateName = $.trim($("#corporateName").val());
        if ($.trim(corporateName) != "") {
            $("#corporateNameyy").attr("class", "Validform_checktip  Validform_right");
            $("#corporateNameyy").html("验证通过");
        } else {
            $("#corporateNameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#corporateNameyy").html("申请公司不能为空！");
            return false;
        }
        var paypx = $.trim($("#paypx").val());
        if ($.trim(paypx) != "") {
            if (isNaN(paypx)) {
                $("#paypxyy").attr("class", "Validform_checktip Validform_wrong");
                $("#paypxyy").html("排序只能是整数！");
                return false;
            } else {
                $("#paypxyy").attr("class", "Validform_checktip  Validform_right");
                $("#paypxyy").html("验证通过");
            }
        } else {
            $("#paypxyy").attr("class", "Validform_checktip Validform_wrong");
            $("#paypxyy").html("排序不能为空！");
            return false;
        }
        //验证日收入最大金额
        var test = /^\+?[0-9]*$/
        var daymoney = $.trim($("#l_daymoney").val());
        if ($.trim(daymoney) != "") {
            if (!test.test(daymoney)) {
                $("#daymoneyyy").attr("class", "Validform_checktip Validform_wrong");
                $("#daymoneyyy").html("日最大金额只能输入整数！");
                return false;
            } else {

                $("#daymoneyyy").attr("class", "Validform_checktip  Validform_right");
                $("#daymoneyyy").html("验证通过");

            }
        } else {
            $("#daymoneyyy").attr("class", "Validform_checktip Validform_wrong");
            $("#daymoneyyy").html("日最大收入不能为空！");
            return false;
        }
        var test1 = /^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
        //验证单笔最小金额
        var l_minimum = $.trim($("#l_minimum").val());
        if ($.trim(l_minimum) != "") {
            if (!test1.test(l_minimum)) {
                $("#l_minimumyy").attr("class", "Validform_checktip Validform_wrong");
                $("#l_minimumyy").html("单笔最小金额只能输入整数！");
                return false;
            } else {
                $("#l_minimumyy").attr("class", "Validform_checktip  Validform_right");
                $("#l_minimumyy").html("验证通过");
            }
        } else {
            $("#l_minimumyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_minimumyy").html("单笔最小金额不能为空！");
            return false;
        }
        //验证单笔最大金额
        var l_maximum = $.trim($("#l_maximum").val());
        if ($.trim(l_maximum) != "") {
            if (!test.test(l_maximum)) {
                $("#l_maximumyy").attr("class", "Validform_checktip Validform_wrong");
                $("#l_maximumyy").html("单笔最大金额只能输入整数！");
                return false;
            } else {

                $("#l_maximumyy").attr("class", "Validform_checktip  Validform_right");
                $("#l_maximumyy").html("验证通过");

            }
        } else {
            $("#l_maximumyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_maximumyy").html("单笔最大金额不能为空！");
            return false;
        }

        //验证动态参数
        var boolconif = true;
        $("input[name='payconfig'],textarea[name='payconfig']").each(function () {
            var name = $(this).attr("data-label");
            boolconif = yzpayconfig($(this), name);
            if (boolconif == false) {
                return false;
            }
        })
        if (boolconif == false) {
            return false;
        }
        var arr = [];
        var configObjs = [];
        $("input[name='payconfig'],textarea[name='payconfig']").each(function (i) {
            arr.push($.trim($(this).val()));
            var fieldName = '"fieldName":"' + $(this).attr("id") + '"';
            var value = '"value":"' + $(this).val() + '"';
            var label = '"label":"' + $(this).attr("data-label") + '"';
            var json = "{" + fieldName + "," + value + "," + label + "}";
            configObjs.push(json);
        })
        var configjson = "[" + configObjs.join(',') + "]";
        var vals = arr.join(',');
        debugger;
        data = { l_isenable: $.trim(l_isenable), l_sort: $.trim(paypx), l_paymenttype_id: $.trim(typeid.split(',')[0]), l_str: $.trim(vals), l_id: $.trim(lid), l_apptypeid: l_apptypeid, l_corporatename: corporateName, l_jsonstr: $.trim(configjson), l_risk: $.trim(l_risk), l_daymoney: $.trim(daymoney), l_minimum: $.trim(l_minimum), l_maximum: $.trim(l_maximum) };
        $("#btnSavezfpz").attr("disabled", "disabled");
        var url = "/payment/InterfaceAddOrUpdate";
        $.post(url, data, function (retJson) {
            $("#btnSavezfpz").attr("disabled", false);
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
    //验证动态参数
    $("body").on('blur', "input[name='payconfig'],textarea[name='payconfig']", function () {
        var name = $(this).attr("data-label");
        yzpayconfig($(this), name);
    })

    //添加修改支付通道成本费率
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
    var url = "/payment/InterfaceList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var risk = $("#risk").val();
    var risl = $("#risl").val();
    if ((searchType == "4" || searchType == "1") && isNaN(sea_name)) {
        window.parent.ShowMsg("只能输入整数！", "error", "");
        return false;
    }
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&risk=" + risk + "&risl=" + risl;
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
//应用弹窗
function apptc() {
    var l_risk = $.trim($("#l_risk").val());
    var appid = $.trim($("#appName").val());
    switch (l_risk) {
        case "2":
            $("#l_riskyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_riskyy").html("验证通过");
            window.parent.ShouwDiaLogWan("选择通道池", 1000, 700, "/payment/ChannelPoolTC?SelectId=appName&judge=appNameyy");
            break;
        case "1":
            $("#l_riskyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_riskyy").html("验证通过");
            window.parent.ShouwDiaLogWan("选择应用", 1000, 700, "/payment/AppListTC?appstr=" + appid);
            break;
        case "0":
            $("#l_riskyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_riskyy").html("验证通过");
            window.parent.ShouwDiaLogWan("选择风险等级", 1000, 700, "/payment/RisklevelListTc?appstr=" + appid);
            break;
        default:
            $("#l_riskyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_riskyy").html("请选择风控类型！");
            return false;
            break;
    }
}
//添加支付配置弹窗
function AddInterface() {
    window.parent.ShouwDiaLogWan("添加支付配置", 900, 650, "/payment/InterfaceAdd");
}
//修改支付配置弹窗
function UpdateInterface(lid) {
    window.parent.ShouwDiaLogWan("修改支付配置", 900, 650, "/payment/InterfaceAdd?lid=" + lid);
}
//设置支付配置为可用
function UpdateSeate(lid) {
    var url = "/payment/payUpdateSeate";
    var data = { lid: lid };
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
//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择配置接口！", "error", "");
        return;
    }
    var url = "/payment/UpdateState";
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
//选择支付类型
function zflxtype() {
    var paymodeid = $.trim($("#paymodeid").val());
    var zftdid = $.trim($("#zftdid").val());
    var data = { p_type: paymodeid, zftdid: zftdid };
    $.post("/payment/SelectPaymenttype", data, function (datastr) {
        $("#paymenttype").find("option").each(function () {
            $(this).remove();
        });//移除原有下拉列表值
        $("#paymenttype").append(datastr);//添加值
        $("#paymenttypeht").ruleSingleSelect();//重新加载下拉列表样式
        var zftdid = $.trim($("#zftdid").val());
        if (zftdid > 0) {
            xzzfpztype();
        }
    })
}
//选择支付通道
function xzzfpztype() {
    var typeid = $.trim($("#paymenttype").val());
    var type = typeid.split(',')[0];
    var url = "/payment/SelectTypeConfig";
    var jsonstr = $.trim($("#jsonstr").val());
    var zftdid = $.trim($("#zftdid").val());
    var data = { typeid: $.trim(type), jsonstr: $.trim(jsonstr), zftdid: $.trim(zftdid) };
    $.post(url, data, function (data) {
        $("#payconfig").html(data);
    })
}
//验证申请公司名称
function yzCorporateName() {
    var corporateName = $.trim($("#corporateName").val());
    if ($.trim(corporateName) != "") {
        $("#corporateNameyy").attr("class", "Validform_checktip  Validform_right");
        $("#corporateNameyy").html("验证通过");
    } else {
        $("#corporateNameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#corporateNameyy").html("申请公司不能为空！");
        return false;
    }
}
//验证动态参数不能为空
function yzpayconfig(obj, name) {
    var yazid = obj.attr("id");
    yazid = yazid + "yy";
    var value = $.trim(obj.val());
    if ($.trim(value) != "") {
        $("#" + yazid).attr("class", "Validform_checktip  Validform_right");
        $("#" + yazid).html("验证通过");
        return true;
    } else {
        $("#" + yazid).attr("class", "Validform_checktip Validform_wrong");
        $("#" + yazid).html(name + "不能为空！");
        return false;
    }
}
//验证排序
function yzpaypx() {
    var paypx = $.trim($("#paypx").val());
    if ($.trim(paypx) != "") {
        if (isNaN(paypx)) {
            $("#paypxyy").attr("class", "Validform_checktip Validform_wrong");
            $("#paypxyy").html("排序只能是整数！");
            return false;
        } else {
            $("#paypxyy").attr("class", "Validform_checktip  Validform_right");
            $("#paypxyy").html("验证通过");
        }
    } else {
        $("#paypxyy").attr("class", "Validform_checktip Validform_wrong");
        $("#paypxyy").html("排序不能为空！");
        return false;
    }
}
//验证日收入最大金额
function yzdaymoney() {
    var test = /^\+?[0-9]*$/
    var daymoney = $.trim($("#l_daymoney").val());
    if ($.trim(daymoney) != "") {
        if (!test.test(daymoney)) {


            $("#daymoneyyy").attr("class", "Validform_checktip Validform_wrong");
            $("#daymoneyyy").html("日最大收入只能输入整数！");
            return false;
        } else {

            $("#daymoneyyy").attr("class", "Validform_checktip  Validform_right");
            $("#daymoneyyy").html("验证通过");

        }
    } else {
        $("#daymoneyyy").attr("class", "Validform_checktip Validform_wrong");
        $("#daymoneyyy").html("日最大收入不能为空！");
        return false;
    }

}
//验证单笔最小金额
function yzl_minimum() {
    // var test = /^\+?[0-9]*$/
    var test = /^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
    var daymoney = $.trim($("#l_minimum").val());
    if ($.trim(daymoney) != "") {
        if (!test.test(daymoney)) {
            $("#l_minimumyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_minimumyy").html("单笔最小金额只能输入整数！");
            return false;
        } else {

            $("#l_minimumyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_minimumyy").html("验证通过");
        }
    } else {
        $("#l_minimumyy").attr("class", "Validform_checktip Validform_wrong");
        $("#l_minimumyy").html("单笔最小金额不能为空！");
        return false;
    }

}
//验证单笔最大金额
function yzl_maximum() {
    var test = /^\+?[0-9]*$/
    var daymoney = $.trim($("#l_maximum").val());
    if ($.trim(daymoney) != "") {
        if (!test.test(daymoney)) {
            $("#l_maximumyy").attr("class", "Validform_checktip Validform_wrong");
            $("#l_maximumyy").html("单笔最大金额只能输入整数！");
            return false;
        } else {

            $("#l_maximumyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_maximumyy").html("验证通过");

        }
    } else {
        $("#l_maximumyy").attr("class", "Validform_checktip Validform_wrong");
        $("#l_maximumyy").html("单笔最大金额不能为空！");
        return false;
    }

}
//获取选择的用户（用于添加）
function selectxzuser(index) {
    var valArr = new Array;
    $("#apptable :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = $.trim((valArr.join(',')).replace("on,", ""));
    if (vals == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    window.parent.layer.getChildFrame("#appName", index).val(vals);
    window.parent.layer.getChildFrame("#r_appid", index).val(vals);
    window.parent.layer.getChildFrame("#appNameyy", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#appNameyy", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}

//支付通道ID
function PayChannelStatusCheck(tid) {
    window.parent.ShouwDiaLogWan("手动检测支付通道状态", 360, 560, "/paymentmonitor/monitor?tid=" + tid);
}
//清空切换选择
function empty() {
    $("#appName").val("");
}
//风险等级查询显示
function fxdjcx() {
    var risk = $.trim($("#risk").val());
    if (risk == 0) {
        document.getElementById("fxdjcss").style.display = "";
    } else {
        document.getElementById("fxdjcss").style.display = "none";
    }
}

//修改通道成本费率
function UpdatePayCR(pid) {
    window.parent.ShouwDiaLogWan("修改通道成本费率", 500, 251, "/payment/PayMenttypeCostRatio?lid=" + pid);
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