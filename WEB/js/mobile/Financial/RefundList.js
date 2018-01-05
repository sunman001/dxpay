
$(function () {
    function isMobileOrPhone(obj) {
        reg_mobile = /^(\+\d{2,3}\-)?\d{11}$/;
        reg_phone = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
        if (!reg_mobile.test(obj) && !reg_phone.test(obj)) {
            return false;
        } else {
            return true;
        }
    }
    //退款申请
    $("#btnSaveAddRefund").click(function () {
        //验证退款申请人
        var exp10 = /^[\u4E00-\u9FA5]{1,6}$/;
        var inputName = $("#r_name").val();
        if ($.trim(inputName) != "") {
            if (exp10.test(inputName)) {
                $("#nameyy").attr("class", "Validform_checktip  Validform_right");
                $("#nameyy").html("验证通过");
            }
            else {
                $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#nameyy").html("退款人姓名由1-6位汉字组成！");
                return false;
            }
        } else {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("退款人姓名不能为空");
            return false;
        }
        //验证手机号码
        var r_tel = $.trim($("#r_tel").val());

        var rexp1 = /[^\d]/g;
        var isYyzz = isMobileOrPhone(r_tel);//格式是否正确
        if (r_tel == "") {
            $("#u_phone_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#u_phone_tip").html("请填写手机号码")
        } else if (!isYyzz) {
            $("#u_phone_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#u_phone_tip").html("11位手机号或固定电话(号码或区号-号码)！")
            return false;
        }
        else {
            $("#u_phone_tip").attr("class", "Validform_checktip  Validform_right");
            $("#u_phone_tip").html("验证通过");

        }
        //验证选择商户
        var a_user_idyx = $.trim($("#a_user_idyx").val());
        var a_user_id = $.trim($("#a_user_id").val());
        if ($.trim(a_user_idyx) != "") {
            if ($.trim(a_user_id) != "") {
                if (!isNaN($.trim(a_user_id))) {
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
        } else {
            $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz").html("请选择应用");
            return false;
        }
        //验证支付流水号
        var r_tradeno = $.trim($("#r_tradeno").val());
        var isYyzz1 = rexp1.test(r_tradeno);//格式是否正确
        if ($.trim(r_tradeno) == "") {
            $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
            $("#yztradeno").html("请输入支付流水号");
            return false;
        }
        else if (isYyzz1) {
            $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
            $("#yztradeno").html("支付流水号格式只能为数字");
            return false;
        }
        else {
            $("#yztradeno").attr("class", "Validform_checktip  Validform_right");
            $("#yztradeno").html("验证通过");

        }
        //验证订单编号
        var r_code = $.trim($("#r_code").val());

        var isYyzz2 = rexp1.test(r_code);//格式是否正确
        if ($.trim(r_code) == "") {
            $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraCode").html("请输入订单编号");
            return false;
        }
        else if (isYyzz2) {
            $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraCode").html("输入的订单编号格式只能为数字");
            return false;
        }
        else {
            $("#yztraCode").attr("class", "Validform_checktip  Validform_right");
            $("#yztraCode").html("验证通过");

        }
        //验证实际支付金额
        var r_money = $.trim($("#r_money").val());
        var rex = /^\d{1,}(\.\d{2})?$/;
        var isYyzz4 = rex.test(r_money);//格式是否正确
        if ($.trim(r_money) == "") {
            $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraMoney").html("请输入实际支付金额");
            return false;
        }
        else if (!isYyzz4) {
            $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraMoney").html("实际支付金额为整数或者保留两位小数");
            return false;
        }
        else {
            if (r_money > 0) {
                $("#yztraMoney").attr("class", "Validform_checktip  Validform_right");
                $("#yztraMoney").html("验证通过");
            } else {
                $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
                $("#yztraMoney").html("实际支付金额不能小于或等于零");
                return false;
            }
        }

        //验证退款金额
        var r_price = $.trim($("#r_price").val());

        var isYyzz3 = rex.test(r_price);//格式是否正确
        if ($.trim(r_price) == "") {
            $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPrice").html("请输入退款金额");
            return false;
        } else if (!isYyzz3) {
            $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPrice").html("退款金额为整数或者保留两位小数");
            return false;
        }
        else {

            if (r_price > 0) {
                $("#yztrPrice").attr("class", "Validform_checktip  Validform_right");
                $("#yztrPrice").html("验证通过");
                if (parseInt(r_price) > parseInt(r_money)) {
                    $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
                    $("#yztrPrice").html("退款金额只能小于等于实际支付金额");
                    return false;
                }
            }

            else {
                $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
                $("#yztrPrice").html("退款金额不能小于或等于零");
                return false;
            }
        }



        var r_appid = $.trim($("#r_appid").val());
        $("#btnSaveAddRefund").attr("disabled", "disabled");
        var data = {
            r_name: $.trim(inputName), r_tel: $.trim(r_tel), r_userid: $.trim(a_user_id), r_tradeno: r_tradeno, r_code: r_code, r_price: r_price, r_money: r_money, r_date: $.trim($("#r_date").val()), r_appid: r_appid
        };
        var url = "/Financial/InsertUpdateRefund";
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.location.href = "/Financial/RefundList";
               // window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.frames['mainFrame'].location.reload(); window.parent.layer.closeAll(); });
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
            $("#btnSaveAddRefund").attr("disabled", false);
        })
    })

    //修改退款申请
    $("#btnSaveUpdateApp").click(function () {
        var inputName = $.trim($("#r_name").val());
        var exp = /^[\u4E00-\u9FA5]{1,6}$/;
        if ($.trim(inputName) != "") {
            if (exp.test(inputName)) {
                $("#nameyy").attr("class", "Validform_checktip  Validform_right");
                $("#nameyy").html("验证通过");
            } else {
                $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#nameyy").html("退款人姓名由1-6位汉字组成！");
                return false;
            }
        } else {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("申请退款人不能为空");
            return false;
        }
        //验证手机号码
        var r_tel = $.trim($("#r_tel").val());

        var rexp1 = /[^\d]/g;
        var isYyzz = isMobileOrPhone(r_tel);//格式是否正确
        if (r_tel == "") {
            $("#u_phone_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#u_phone_tip").html("请填写手机号码")
        } else if (!isYyzz) {
            $("#u_phone_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#u_phone_tip").html("11位手机号或固定电话(号码或区号-号码)！")
            return false;
        }
        else {
            $("#u_phone_tip").attr("class", "Validform_checktip  Validform_right");
            $("#u_phone_tip").html("验证通过");

        }
        //验证选择商户
        var a_user_idyx = $.trim($("#a_user_idyx").val());
        var a_user_id = $.trim($("#a_user_id").val());

        if ($.trim(a_user_idyx) != "") {
            if ($.trim(a_user_id) != "") {
                if (!isNaN($.trim(a_user_id))) {
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
        } else {
            $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz").html("请选择应用");
            return false;
        }

        //验证支付流水号
        var r_tradeno = $.trim($("#r_tradeno").val());
        var isYyzz1 = rexp1.test(r_tradeno);//格式是否正确
        if ($.trim(r_tradeno) == "") {
            $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
            $("#yztradeno").html("请输入支付流水号");
            return false;
        }
        else if (isYyzz1) {
            $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
            $("#yztradeno").html("支付流水号只能为数字");
            return false;
        }
        else {
            $("#yztradeno").attr("class", "Validform_checktip  Validform_right");
            $("#yztradeno").html("验证通过");

        }
        //验证订单编号
        var r_code = $.trim($("#r_code").val());
        var isYyzz2 = rexp1.test(r_code);//格式是否正确
        if ($.trim(r_code) == "") {
            $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraCode").html("请输入订单编号");
            return false;
        }
        else if (isYyzz2) {
            $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraCode").html("订单编号号只能为数字");
            return false;
        }
        else {
            $("#yztraCode").attr("class", "Validform_checktip  Validform_right");
            $("#yztraCode").html("验证通过");

        }

        var r_price = $.trim($("#r_price").val());
        var r_money = $.trim($("#r_money").val());
        var rex = /^\d+(\.\d{2})?$/;
        var isRate = rex.test(r_price);//格式是否正确
        if ($.trim(r_price) == "") {
            $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPrice").html("请输入退款金额");
            return false;
        }
        else if (!isRate) {
            $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPrice").html("输入退款金额格式不正确");
            return false;
        }
        else {

            if (r_price > 0) {
                if (parseInt(r_price) > parseInt(r_money)) {
                    $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
                    $("#yztrPrice").html("退款金额只能小于等于实际支付金额");
                    return false;
                }
                $("#yztrPrice").attr("class", "Validform_checktip  Validform_right");
                $("#yztrPrice").html("验证通过");

            }
            else {
                $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
                $("#yztrPrice").html("退款金额大于0");

            }

        }

        var ismoney = rex.test(r_money);//格式是否正确
        if ($.trim(r_money) == "") {
            $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraMoney").html("请输入实际支付金额");
            return false;
        }
        else if (!ismoney) {
            $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraMoney").html("输入实际支付金额格式不正确");
            return false;
        }
        else {
            $("#yztraMoney").attr("class", "Validform_checktip  Validform_right");
            $("#yztraMoney").html("验证通过");
        }
        var r_id = $.trim($("#r_id").val());
        var r_time = $.trim($("#r_time").val());
        var r_static = $.trim($("#r_static").val());
        var r_appid = $.trim($("#r_appid").val());
        var data = {
            r_id: r_id, r_name: $.trim(inputName), r_tel: $.trim(r_tel), r_userid: $.trim(a_user_id), r_tradeno: r_tradeno, r_code: r_code, r_price: r_price, r_money: r_money, r_date: $.trim($("#r_date").val()), r_time: r_time, r_static: r_static, r_appid: r_appid
        };
        var url = "/Financial/InsertUpdateRefund";
        $("#btnSaveUpdateApp").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            $("#btnSaveUpdateApp").attr("disabled", false);
            if (retJson.success == 1) {
                window.location.href = "/Financial/RefundList";
               window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.location.reload(); window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 0) {
                window.parent.ShowMsg(retJson.msg, "error", "");
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
    //提交审核
    $("#btnAuditorRefund").click(function () {
        var uids = $("#r_id").val();
        var r_remark = $("#r_remark").val();
        var r_state = $('input[name="rstate"]:checked').val();
        var r_payid = $("#c_payid").val();
        if ($.trim(r_state) == -1) {
            if ($.trim(r_remark) == "") {
                $("#r_remark_p").attr("class", "Validform_checktip Validform_wrong");
                $("#r_remark_p").html("如不通过，必须填写备注！");
                return false;
            }
        }
        else {

            if ($.trim(r_payid) == "") {
                $("#yzkfz1").attr("class", "Validform_checktip Validform_wrong");
                $("#yzkfz1").html("如通过，请选择渠道！");
                return false;
            }
            else {
                $("#yzkfz1").attr("class", "Validform_checktip  Validform_right");
                $("#yzkfz1").html("验证通过");
            }
            $("#r_remark_p").attr("class", "Validform_checktip  Validform_right");
            $("#r_remark_p").html("验证通过");

        }
        $("#btnAuditorRefund").attr("disabled", "disabled");
        $.post("/Financial/AuditorTORefund", { rid: uids, state: r_state, remark: r_remark, r_payid: r_payid }, function (retJson) {
            if (retJson.success == 1) {
                window.location.href = "/Financial/RefundList";
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
    var url = "/Financial/RefundList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
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

//选择支付渠道

function zfqd() {

    window.parent.ShouwDiaLogWan("选择支付渠道", 1000, 700, "/REPORT/InterfaceListTC");

}
//数据导出
function Searchdc() {
    var url = "/Financial/RefundDc?dc=dc";
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();

    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
    if (auditstate == 1) {
        location.href = encodeURI(url);
    } else {
        window.parent.ShowMsg("只能导出审核通过的数据！", "error", "");
        return false;
    }
}
//退款管理
function AddAPPlog() {
    window.location.href = "/Financial/RefundAdd";
    
}
//退款人姓名验证
function yzname() {
    var exp = /^[\u4E00-\u9FA5]{1,6}$/;
    var inputName = $("#r_name").val();
    if ($.trim(inputName) != "") {
        if (exp.test(inputName)) {
            $("#nameyy").attr("class", "Validform_checktip  Validform_right");
            $("#nameyy").html("验证通过");
        } else {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("退款人姓名由1-6位汉字组成！");
            return false;
        }
    } else {
        $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#nameyy").html("退款人姓名不能为空");
        return false;
    }
}
function shyy() {

   ShouwDiaLogWan("选择应用", 1000, 700, "/APP/AppListTC");

}


function CheckPhone() {
    var phone = $("#r_tel").val();
    var isYyzz = isMobileOrPhone(phone);//格式是否正确
    if (phone == "") {
        $("#u_phone_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#u_phone_tip").html("请填写手机号码")
    } else if (!isYyzz) {
        $("#u_phone_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#u_phone_tip").html("11位手机号或固定电话(号码或区号-号码)！")
        return false;
    }

    else {
        $("#u_phone_tip").attr("class", "Validform_checktip  Validform_right");
        $("#u_phone_tip").html("验证通过");
    }
}
//验证商户
function kfz() {
    var a_user_idyx = $("#a_user_idyx").val();
    if ($.trim(a_user_idyx) == "") {
        $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
        $("#yzkfz").html("请选择商户");
        return false;
    } else {
        $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
        $("#yzkfz").html("验证通过");
    }
}
//支付流水号验证
function CheckTradeno() {
    var r_tradeno = $("#r_tradeno").val();
    var rexp = /[^\d]/g;
    var isYyzz = rexp.test(r_tradeno);//格式是否正确
    if ($.trim(r_tradeno) == "") {
        $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
        $("#yztradeno").html("请输入支付流水号");
        return false;
    }
    else if (isYyzz) {
        $("#yztradeno").attr("class", "Validform_checktip Validform_wrong");
        $("#yztradeno").html("支付流水号格式只能为数字");
    }
    else {
        $("#yztradeno").attr("class", "Validform_checktip  Validform_right");
        $("#yztradeno").html("验证通过");
    }
}

//订单编号验证
function CheckCode() {
    var r_tradeno = $("#r_code").val();
    var rexp = /[^\d]/g;
    var isYyzz = rexp.test(r_tradeno);//格式是否正确
    if ($.trim(r_tradeno) == "") {
        $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
        $("#yztraCode").html("请输入订单编号");
        return false;
    }
    else if (isYyzz) {
        $("#yztraCode").attr("class", "Validform_checktip Validform_wrong");
        $("#yztraCode").html("订单编号格式只能为数字");
    }
    else {
        $("#yztraCode").attr("class", "Validform_checktip  Validform_right");
        $("#yztraCode").html("验证通过");
    }
}
//验证退款金额
function CheckPrice() {
    var r_tradeno = $("#r_price").val();
    var r_money = $("#r_money").val();
    var rex = /^\d{1,}(\.\d{2})?$/;
    var isYyzz = rex.test(r_tradeno);//格式是否正确
    if ($.trim(r_tradeno) == "") {
        $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
        $("#yztrPrice").html("请输入退款金额");
        return false;
    } else if (!isYyzz) {
        $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
        $("#yztrPrice").html("退款金额为整数或者保留两位小数");
        return false;
    }
    else {
        if (r_tradeno > 0) {
            if (parseInt(r_tradeno) > parseInt(r_money)) {
                $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
                $("#yztrPrice").html("退款金额只能小于等于实际支付金额");
                return false;
            }
            $("#yztrPrice").attr("class", "Validform_checktip  Validform_right");
            $("#yztrPrice").html("验证通过");
        } else {
            $("#yztrPrice").attr("class", "Validform_checktip Validform_wrong");
            $("#yztrPrice").html("退款金额不能小于或等于零");
            return false;
        }
    }
}
//验证实际支付金额
function CheckMoney() {
    var r_tradeno = $("#r_money").val();
    var rex = /^\d{1,}(\.\d{2})?$/;
    var isYyzz = rex.test(r_tradeno);//格式是否正确
    if ($.trim(r_tradeno) == "") {
        $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
        $("#yztraMoney").html("请输入实际支付金额");
        return false;
    }
    else if (!isYyzz) {
        $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
        $("#yztraMoney").html("输入实际支付金额为整数或者保留两位小数");
        return false;
    }
    else {
        if (r_tradeno > 0) {
            $("#yztraMoney").attr("class", "Validform_checktip  Validform_right");
            $("#yztraMoney").html("验证通过");
        } else {
            $("#yztraMoney").attr("class", "Validform_checktip Validform_wrong");
            $("#yztraMoney").html("输入实际支付金额不能小于或等于零");
            return false;
        }
    }
}

//修改应用弹窗
function UpdateRefund(r_id) {

  window.location.href = "/Financial/UpdateRefund?r_id=" + r_id
}
//弹窗开发者列表
function xzuser() {

   ShouwDiaLogWan("选择开发者", 1000, 700, "/App/UserList");

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
//批量审核
function bulkassign() {
    var vals = "";
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals === "") {
        window.parent.ShowMsg("请选择退款信息！", "error", "");
        return;
    }
    window.location.href = "/Financial/AuditorRefund?rid=" + vals;

}
//单个审核
function auditorRefund(rid) {
    if (rid === "") {
        window.parent.ShowMsg("请选择退款信息！", "error", "");
        return;
    }
    window.location.href = "/Financial/AuditorRefund?rid=" + rid
  
}


function yzyymc(a_id, a_name, a_user_id, u_realname, index) {

    parent.$('#r_appname').val(a_name);
    parent.$('#r_appid').val(a_id);
    parent.$('#a_user_idyx').val(u_realname);
    parent.$('#a_user_id').val(a_user_id);
    window.parent.layer.getChildFrame("#yzkfz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#yzkfz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
    parent.document.getElementById("r_appname").value = a_name;
    parent.document.getElementById("r_appid").value = a_id;
    parent.document.getElementById("a_user_idyx").value = u_realname;
    parent.document.getElementById("a_user_id").value = a_user_id;
}

