
$(function () {
    //添加服务费等级信息
    $("#btnSaveAdd").click(function () {
        //验证服务费等级名称
        var Name = $.trim($("#Name").val());
        if ($.trim(Name) == "") {
            $("#yzName").attr("class", "Validform_checktip Validform_wrong");
            $("#yzName").html("请输入服务费等级名称");
            return false;
        }
        else {
            $("#yzName").attr("class", "Validform_checktip  Validform_right");
            $("#yzName").html("验证通过");
        }
        //验证开发者的服务费比例
        var ServiceFeeRatio = $.trim($("#ServiceFeeRatio").val());
        var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
        var isYyzz3 = rex.test(ServiceFeeRatio); //格式是否正确
        if ($.trim(ServiceFeeRatio) == "") {
            $("#YZServiceFeeRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZServiceFeeRatio").html("请输入开发者的服务费比例");
            return false;
        } else if (!isYyzz3) {
            $("#YZServiceFeeRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZServiceFeeRatio").html("开发者的服务费比例为整数或者保留两位小数！");
            return false;
        } else if ($.trim(ServiceFeeRatio) == 0) {
            $("#YZServiceFeeRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZServiceFeeRatio").html("开发者的服务费比例必须大于0");
            return false;
        } else {
            $("#YZServiceFeeRatio").attr("class", "Validform_checktip  Validform_right");
            $("#YZServiceFeeRatio").html("验证通过");
        }
        //验证直客提成比例
        var CustomerWithoutAgentRatio = $.trim($("#CustomerWithoutAgentRatio").val());
        var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
        var isYyzz3 = rex.test(CustomerWithoutAgentRatio); //格式是否正确
        if ($.trim(CustomerWithoutAgentRatio) == "") {
            $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZCustomerWithoutAgentRatio").html("请输入直客提成比例");
            return false;
        } else if (!isYyzz3) {
            $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZCustomerWithoutAgentRatio").html("直客提成比例为整数或者保留两位小数！");
            return false;
        } else if ($.trim(CustomerWithoutAgentRatio) == 0) {
            $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZCustomerWithoutAgentRatio").html("直客提成比例必须大于0");
            return false;
        } else {
            $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip  Validform_right");
            $("#YZCustomerWithoutAgentRatio").html("验证通过");
        }
        //验证商务对代理商的提成比例
        var BusinessPersonnelAgentRatio = $.trim($("#BusinessPersonnelAgentRatio").val());
        var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
        var isYyzz3 = rex.test(BusinessPersonnelAgentRatio); //格式是否正确
        if ($.trim(BusinessPersonnelAgentRatio) == "") {
            $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZBusinessPersonnelAgentRatio").html("请输入商务对代理商的提成比例");
            return false;
        } else if (!isYyzz3) {
            $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZBusinessPersonnelAgentRatio").html("商务对代理商的提成比例为整数或者保留两位小数！");
            return false;
        } else if ($.trim(BusinessPersonnelAgentRatio) == 0) {
            $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZBusinessPersonnelAgentRatio").html("商务对代理商的提成比例必须大于0");
            return false;
        } else {
            $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip  Validform_right");
            $("#YZBusinessPersonnelAgentRatio").html("验证通过");
        }

        //验证代理商提成比例
        var AgentPushMoneyRatio = $.trim($("#AgentPushMoneyRatio").val());
        var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
        var isYyzz3 = rex.test(AgentPushMoneyRatio); //格式是否正确
        if ($.trim(AgentPushMoneyRatio) == "") {
            $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZAgentPushMoneyRatio").html("请输入代理商提成比例");
            return false;
        } else if (!isYyzz3) {
            $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZAgentPushMoneyRatio").html("代理商提成比例为整数或者保留两位小数！");
            return false;
        } else if ($.trim(AgentPushMoneyRatio) == 0) {
            $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip Validform_wrong");
            $("#YZAgentPushMoneyRatio").html("代理商提成比例必须大于0");
            return false;
        } else {
            $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip  Validform_right");
            $("#YZAgentPushMoneyRatio").html("验证通过");
        }
        var Id = $.trim($("#id").val());
        var Description = $("#Description").val();

        $("#btnSaveAdd").attr("disabled", "disabled");
        var data = {
            Name: Name, ServiceFeeRatio: ServiceFeeRatio, CustomerWithoutAgentRatio: CustomerWithoutAgentRatio, BusinessPersonnelAgentRatio: BusinessPersonnelAgentRatio, AgentPushMoneyRatio: AgentPushMoneyRatio, Id: Id, Description: $.trim(Description)
        };
        var url = "/System/InsertUpdateSerViceFeeRationGrade";
        $.post(url, data, function (retJson) {
            $("#btnSaveAdd").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.location.reload();
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

    var url = "/System/ServiceFeeRatioGradeList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var searchDesc = $("#searchDesc").val();

    url += "&type=" + searchType + "&sea_name=" + sea_name + "&searchDesc=" + searchDesc;
    location.href = encodeURI(url);
}




//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}


//添加服务费信息
function AddAPPlog() {

    window.parent.ShouwDiaLogWan("添加服务费信息", 800, 630, "/System/SerViceFeeRationGradeAdd");
}
function Update(id) {
    window.parent.ShouwDiaLogWan("修改服务费信息", 800, 630, "/System/SerViceFeeRationGradeAdd?id=" + id);
}

//关闭处理工单页面
function btnCodesc() {
    window.parent.layer.closeAll();
}
//列表查询
function serchlocuser() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//验证服务费等级名称
function yzName() {
    var Name = $.trim($("#Name").val());
    if ($.trim(Name) == "") {
        $("#yzName").attr("class", "Validform_checktip Validform_wrong");
        $("#yzName").html("请输入服务费等级名称");
        return false;
    }
    else {
        $("#yzName").attr("class", "Validform_checktip  Validform_right");
        $("#yzName").html("验证通过");
    }
}

//验证开发者服务费比例
function YZServiceFeeRatio() {
    var ServiceFeeRatio = $.trim($("#ServiceFeeRatio").val());
    var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
    var isYyzz3 = rex.test(ServiceFeeRatio); //格式是否正确
    if ($.trim(ServiceFeeRatio) == "") {
        $("#YZServiceFeeRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZServiceFeeRatio").html("请输入开发者的服务费比例");
        return false;
    } else if (!isYyzz3) {
        $("#YZServiceFeeRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZServiceFeeRatio").html("开发者的服务费比例为整数或者保留两位小数！");
        return false;
    } else if ($.trim(ServiceFeeRatio) == 0) {
        $("#YZServiceFeeRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZServiceFeeRatio").html("开发者的服务费比例必须大于0");
        return false;
    } else {
        $("#YZServiceFeeRatio").attr("class", "Validform_checktip  Validform_right");
        $("#YZServiceFeeRatio").html("验证通过");
    }
}
//验证直客提成比例
function YZCustomerWithoutAgentRatio() {
    var CustomerWithoutAgentRatio = $.trim($("#CustomerWithoutAgentRatio").val());
    var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
    var isYyzz3 = rex.test(CustomerWithoutAgentRatio); //格式是否正确
    if ($.trim(CustomerWithoutAgentRatio) == "") {
        $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZCustomerWithoutAgentRatio").html("请输入直客提成比例");
        return false;
    } else if (!isYyzz3) {
        $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZCustomerWithoutAgentRatio").html("直客提成比例为整数或者保留两位小数！");
        return false;
    } else if ($.trim(CustomerWithoutAgentRatio) == 0) {
        $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZCustomerWithoutAgentRatio").html("直客提成比例必须大于0");
        return false;
    } else {
        $("#YZCustomerWithoutAgentRatio").attr("class", "Validform_checktip  Validform_right");
        $("#YZCustomerWithoutAgentRatio").html("验证通过");
    }
}
//商务对代理商的提成比例
function YZBusinessPersonnelAgentRatio() {
    var BusinessPersonnelAgentRatio = $.trim($("#BusinessPersonnelAgentRatio").val());
    var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
    var isYyzz3 = rex.test(BusinessPersonnelAgentRatio); //格式是否正确
    if ($.trim(BusinessPersonnelAgentRatio) == "") {
        $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZBusinessPersonnelAgentRatio").html("请输入商务对代理商的提成比例");
        return false;
    } else if (!isYyzz3) {
        $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZBusinessPersonnelAgentRatio").html("商务对代理商的提成比例为整数或者保留两位小数！");
        return false;
    } else if ($.trim(BusinessPersonnelAgentRatio) == 0) {
        $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZBusinessPersonnelAgentRatio").html("商务对代理商的提成比例必须大于0");
        return false;
    } else {
        $("#YZBusinessPersonnelAgentRatio").attr("class", "Validform_checktip  Validform_right");
        $("#YZBusinessPersonnelAgentRatio").html("验证通过");
    }
}
//代理商提成比例
function YZAgentPushMoneyRatio() {
    var AgentPushMoneyRatio = $.trim($("#AgentPushMoneyRatio").val());
    var rex = /^([1-9]\d{0,15}|0)(\.\d{1,4})?$/;
    var isYyzz3 = rex.test(AgentPushMoneyRatio); //格式是否正确
    if ($.trim(AgentPushMoneyRatio) == "") {
        $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZAgentPushMoneyRatio").html("请输入代理商提成比例");
        return false;
    } else if (!isYyzz3) {
        $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZAgentPushMoneyRatio").html("代理商提成比例为整数或者保留两位小数！");
        return false;
    } else if ($.trim(AgentPushMoneyRatio) == 0) {
        $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip Validform_wrong");
        $("#YZAgentPushMoneyRatio").html("代理商提成比例必须大于0");
        return false;
    } else {
        $("#YZAgentPushMoneyRatio").attr("class", "Validform_checktip  Validform_right");
        $("#YZAgentPushMoneyRatio").html("验证通过");
    }
}
