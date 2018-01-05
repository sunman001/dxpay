/* 手机端*/
$(function () {
    //添加应用投诉
    $("#btnSaveAddComplaint").click(function () {
        //验证所属应用
        
        var c_appname = $.trim($("#r_appname").val());
        //alert(c_appname);
        var c_appId = $.trim($("#r_appid").val());
        //alert(c_appId);
        if ($.trim(c_appname) != "") {
            if ($.trim(c_appId) != "") {
                $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
                $("#yzkfz").html("验证通过");
               
            } else {
                $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
                $("#yzkfz").html("请选择应用");
                return false;
            }
        } else {
            $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz").html("请选择应用");
            return false;
        }

        //验证支付渠道
        var c_payname = $.trim($("#c_payname").val());
        var c_payid = $.trim($("#c_payid").val());
        if ($.trim(c_payname) != "") {
            if ($.trim(c_payid) != "") {
                if (!isNaN($.trim(c_payid))) {
                    $("#yzkfz1").attr("class", "Validform_checktip  Validform_right");
                    $("#yzkfz1").html("验证通过");
                } else {
                    $("#yzkfz1").attr("class", "Validform_checktip Validform_wrong");
                    $("#yzkfz1").html("请选择支付渠道");
                    return false;
                }
            } else {
                $("#yzkfz1").attr("class", "Validform_checktip Validform_wrong");
                $("#yzkfz1").html("请选择支付渠道");
                return false;
            }
        } else {
            $("#yzkfz1").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz1").html("请选择支付渠道");
            return false;
        }

        var rexp1 = /[^\d]/g;
        //验证交易流水号
        var c_tradeno = $.trim($("#c_tradeno").val());
        var isYyzz1 = rexp1.test(c_tradeno);//格式是否正确
        if ($.trim(c_tradeno) == "") {
            $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
            $("#yztradeno").html("请输入交易流水号");
            return false;
        }
        else if (isYyzz1) {
            $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
            $("#yztradeno").html("交易流水号格式只能为数字");
            return false;
        }
        else {
            $("#yztradeno").attr("class", "Validform_checktip  Validform_right");
            $("#yztradeno").html("验证通过");
        }


        //验证订单编号
        var c_code = $.trim($("#c_code").val());

        var isYyzz2 = rexp1.test(c_code);//格式是否正确
        if ($.trim(c_code) == "") {
            $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraCode").html("请输入订单编号");
            return false;
        }
        else if (isYyzz2) {
            $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraCode").html("订单编号格式只能为数字");
            return false;
        }
        else {
            $("#yztraCode").attr("class", "Validform_checktip  Validform_right");
            $("#yztraCode").html("验证通过");

        }
        //验证付款金额
        var c_money = $.trim($("#c_money").val());
        var rex = /^\d+(\.\d{2})?$/;
        var isYyzz3 = rex.test(c_money);//格式是否正确
        if ($.trim(c_money) == "") {
            $("#yztrPriceMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPriceMoney").html("请输入付款金额");
            return false;
        } else if (!isYyzz3) {
            $("#yztrPriceMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPriceMoney").html("付款金额为整数或者保留两位小数！");
            return false;
        }
        else if ($.trim(c_money) ==0)
        {
            $("#yztrPriceMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPriceMoney").html("付款金额必须大于0");
            return false;
        }
        else {
            $("#yztrPriceMoney").attr("class", "Validform_checktip  Validform_right");
            $("#yztrPriceMoney").html("验证通过");
        }
        var c_times = $.trim($("#c_times").val());
        var c_datimes = $.trim($("#c_datimes").val());
        var a_user_id = $.trim($("#a_user_id").val());
        var c_tjtimes = $.trim($("#c_tjtimes").val());
        var c_state = $.trim($("#c_state").val());
        var c_id = $.trim($("#c_id").val());
        var c_reason = $.trim($("#c_reason").val());
     
        $("#btnSaveAddComplaint").attr("disabled", "disabled");
        var data = {
            c_appid: $.trim(c_appId), c_userid: $.trim(a_user_id), c_payid: $.trim(c_payid), c_tradeno: c_tradeno, c_code: c_code, c_money: c_money, c_times: c_times, c_datimes: c_datimes, c_id: c_id, c_tjtimes: c_tjtimes, c_state: c_state, c_reason: c_reason
        };
        var url = "/REPORT/InsertUpdateComplaint";
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.location.href = "/REPORT/ComplaintList";              
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
            $("#btnSaveAddApp").attr("disabled", false);
        })
    })
    //提交处理
    $("#btnComplaintCL").click(function () {
      

        var uids = $("#r_id").val();
        var c_result = $("#c_result").val();
        
        if ($.trim(c_result) == "") {
                $("#c_result_p").attr("class", "Validform_checktip Validform_wrong");
                $("#c_result_p").html("必须填写处理结果！");
                return false;
            }
        else {
            $("#r_remark_p").attr("class", "Validform_checktip  Validform_right");
            $("#r_remark_p").html("验证通过");

        }

        $.post("/REPORT/ComplaintCLJG", { rid: uids, remark: c_result }, function (retJson) {
            if (retJson.success == 1) {
                window.location.href = "/REPORT/ComplaintList";
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
            $("#btnAuditorRefund").attr("disabled", false);
        })



    })



})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/REPORT/ComplaintList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());

    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
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
//数据导出
function Searchdc() {
    var url = "/REPORT/ComplaintDc?dc=dc";
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();

    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
    location.href = encodeURI(url);
}
//添加应用投诉管理
function AddAPPlog() {
    window.location.href = "/REPORT/ComplaintAdd";
}
//选择支付渠道

function zfqd() {
    window.parent.ShouwDiaLogWan("选择支付渠道", 1000, 750, "/REPORT/InterfaceListTC");

}
function shyy() {
 window.parent.ShouwDiaLogWan("选择应用", 1000, 700, "/APP/AppListTC");

}

//验证交易流水号
function yztradeno() {
    var rexp1 = /[^\d]/g;
    var c_tradeno = $.trim($("#c_tradeno").val());
    var isYyzz1 = rexp1.test(c_tradeno);//格式是否正确
    if ($.trim(c_tradeno) == "") {
        $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
        $("#yztradeno").html("请输入交易流水号");
        return false;
    }
    else if (isYyzz1) {
        $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
        $("#yztradeno").html("交易流水号格式只能为数字");
        return false;
    }
    else {
        $("#yztradeno").attr("class", "Validform_checktip  Validform_right");
        $("#yztradeno").html("验证通过");

    }
}

//订单编号验证
function CheckCode() {
    var rexp1 = /[^\d]/g;
    var c_code = $.trim($("#c_code").val());
    var isYyzz2 = rexp1.test(c_code);//格式是否正确
    if ($.trim(c_code) == "") {
        $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
        $("#yztraCode").html("请输入订单编号");
        return false;
    }
    else if (isYyzz2) {
        $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
        $("#yztraCode").html("输入的订单编号格式不正确");
        return false;
    }
    else {
        $("#yztraCode").attr("class", "Validform_checktip  Validform_right");
        $("#yztraCode").html("验证通过");

    }
}

//验证付款价格
function CheckPrice() {
    var c_money = $.trim($("#c_money").val());
    var rex = /^\d+(\.\d{2})?$/;
    var isYyzz3 = rex.test(c_money);//格式是否正确
    if ($.trim(c_money) == "") {
        $("#yztrPriceMoney").attr("class", "Validform_checktip Validform_wrong");
        $("#yztrPriceMoney").html("请输入付款金额");
        return false;
    } else if (!isYyzz3) {
        $("#yztrPriceMoney").attr("class", "Validform_checktip Validform_wrong");
        $("#yztrPriceMoney").html("付款金额格式只能为数字");
        return false;
    }
    else if ($.trim(c_money) == 0) {
        $("#yztrPriceMoney").attr("class", "Validform_checktip Validform_wrong");
        $("#yztrPriceMoney").html("付款金额必须大于0");
        return false;
    }
    else {
        $("#yztrPriceMoney").attr("class", "Validform_checktip  Validform_right");
        $("#yztrPriceMoney").html("验证通过");
    }
}



//修改应用弹窗
function UpdateComplaint(c_id) {

    window.location.href = "/REPORT/ComplaintAdd?c_id=" + c_id;
    
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
    var url = "/Financial/RefundList.?curr=" + currPage + "&psize=" + pageSize;
    var type = $("#s_type").val();
    var keys = $("#s_keys").val();
    var state = $("#s_state").val();
    var check = $("#s_check").val();
    var rzlx = $("#s_category").val();
    var sort = $("#s_sort").val();
    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx;
    location.href = encodeURI(url);
}
//批量处理
function bulkassign() {
    var vals = "";
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals === "") {
        window.parent.ShowMsg("请选择应用投诉信息！", "error", "");
        return;
    }
    window.location.href = "/REPORT/ComplaintCL?rid=" + vals  
}
//单个审核
function complaintLC(rid) {

    if (rid === "") {
        window.parent.ShowMsg("请选择应用投诉信息！", "error", "");
        return;
    }
    window.location.href = "/REPORT/ComplaintCL?rid=" + rid
    
}

