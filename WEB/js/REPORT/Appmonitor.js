
$(function() {
    //添加应用监控
    $("#btnSaveAddAppmonitor").click(function() {
        var a_id = $.trim($("#a_id").val());
        var appName = $.trim($("#appName").val());
        var r_appid = $.trim($("#r_appid").val());
        var c_appname = $.trim($("#r_appname").val());
        var a_type = $("#a_type").val();
        if (a_id == 0 || a_id===null) {
            //验证所属应用
            if (appName !== "") {
                $("#appNameyy").attr("class", "Validform_checktip  Validform_right");
                $("#appNameyy").html("验证通过");
            } else {
                $("#appNameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#appNameyy").html("请选择应用");
                return false;
            }
        } else {
            if ($.trim(appName) != "") {
                if ($.trim(r_appid) != "") {
                    $("#appNameyy").attr("class", "Validform_checktip  Validform_right");
                    $("#appNameyy").html("验证通过");

                } else {
                    $("#appNameyy").attr("class", "Validform_checktip Validform_wrong");
                    $("#appNameyy").html("请选择应用");
                    return false;
                }
            } else {
                $("#appNameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#appNameyy").html("请选择应用");
                return false;
            }

        }
        
        //验证阀值
        var a_request = $.trim($("#a_request").val());
        var rex = /^\d+(\.\d{2})?$/;
        var isYyzz3 = rex.test(a_request); //格式是否正确
        if ($.trim(a_request) == "") {
            $("#requestyy").attr("class", "Validform_checktip Validform_wrong");
            $("#requestyy").html("请输入阀值");
            return false;
        } else if (!isYyzz3) {
            $("#requestyy").attr("class", "Validform_checktip Validform_wrong");
            $("#requestyy").html("阀值为整数或者保留两位小数！");
            return false;
        } else if ($.trim(a_request) == 0 && a_type !== '1') {
            $("#requestyy").attr("class", "Validform_checktip Validform_wrong");
            $("#requestyy").html("阀值必须大于0");
            return false;
        } else {
            $("#requestyy").attr("class", "Validform_checktip  Validform_right");
            $("#requestyy").html("验证通过");
        }
        //验证分钟数
        var a_minute = $.trim($("#a_minute").val());
        if ($.trim(a_minute) != "") {
            if (isNaN(a_minute)) {
                $("#minuteyy").attr("class", "Validform_checktip Validform_wrong");
                $("#minuteyy").html("分钟数只能为数字！");
                return false;
            } else {
                $("#minuteyy").attr("class", "Validform_checktip  Validform_right");
                $("#minuteyy").html("验证通过");
            }
        } else {
            $("#minuteyy").attr("class", "Validform_checktip Validform_wrong");
            $("#minuteyy").html("分钟数不能为空！");
            return false;
        }
        //验证晚上分钟数
        var NightMinute = $.trim($("#NightMinute").val());
        if ($.trim(NightMinute) != "") {
            if (isNaN(NightMinute)) {
                $("#NightMinuteyy").attr("class", "Validform_checktip Validform_wrong");
                $("#NightMinuteyy").html("分钟数只能为数字！");
                return false;
            } else {
                $("#NightMinuteyy").attr("class", "Validform_checktip  Validform_right");
                $("#NightMinuteyy").html("验证通过");
            }
        }
        //验证其他分钟数
        var OtherMinte = $.trim($("#OtherMinte").val());
        if ($.trim(OtherMinte) != "") {
            if (isNaN(OtherMinte)) {
                $("#OtherMinteyy").attr("class", "Validform_checktip Validform_wrong");
                $("#OtherMinteyy").html("分钟数只能为数字！");
                return false;
            } else {
                $("#OtherMinteyy").attr("class", "Validform_checktip  Validform_right");
                $("#OtherMinteyy").html("验证通过");
            }
        }
        
        var StartDay = $.trim($("#StartDay").val());
        var EndDay = $.trim($("#EndDay").val());
        var StartNight = $.trim($("#StartNight").val());
        var EndNight = $.trim($("#EndNight").val());
        var NightMinute = $.trim($("#NightMinute").val());
        var OtherMinte = $.trim($("#OtherMinte").val());
        $("#btnSaveAddAppmonitor").attr("disabled", "disabled");
        var data = {
            a_appidList: $.trim(appName),
            a_request: $.trim(a_request),
            DayMinute: $.trim(a_minute),
            a_appid: r_appid,
            a_id: a_id,
            StartDay: StartDay,
            EndDay: EndDay,
            StartNight: StartNight,
            EndNight: EndNight,
            NightMinute: NightMinute,
            OtherMinte: OtherMinte,
            a_type: a_type
        };
        var url = "/REPORT/InsertUpdateAppmonitor";
        $.post(url,
            data,
            function(retJson) {
                $("#btnSaveAddAppmonitor").attr("disabled", false);
                if (retJson.success == 1)  {
                    window.parent.ShowMsg(retJson.msg,
                        "ok",
                        function() {
                            window.parent.global.reload();
                            window.parent.layer.closeAll();
                        });
                } else if (retJson.success == 9998) {
                    window.parent.ShowMsg(retJson.msg, "error", "");
                    return false;
                } else if (retJson.success == 9999) {
                    window.parent.ShowMsg(retJson.msg, "error", "");
                    window.top.location.href = retJson.Redirect;
                    return false;
                } else if (retJson.success == 9997) {
                    window.top.location.href = retJson.Redirect;
                    return false;
                } else {
                    window.parent.ShowMsg(retJson.msg, "error", "");
                    return false;
                }

            })
    });

    if ($("#a_type").val() + '' === "1") {
        $("#thresholdController").hide();
    } else {
        $("#thresholdController").show();
    }

    $("#a_type").on("change",function() {
        var aType = $(this).val()+'';
        if (aType === "1") {
            $("#thresholdController").hide();
        } else {
            $("#thresholdController").show();
        }
    });
});
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/REPORT/AppmonitorList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());
    var a_type = $("#a_type").val();

    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end +"&a_type="+a_type;
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
    //if (auditstate == 1) {
    //   
    //} else {
    //    window.parent.ShowMsg("只能导出审核通过的数据！", "error", "");
    //    return false;
    //}
}
//添加应用投诉管理
function AddAPPlog() {
    window.parent.ShouwDiaLogWan("添加应用监控", 680, 560, "/REPORT/AppmonitorAdd");
}
//选择支付渠道

function zfqd() {

    window.parent.ShouwDiaLogWan("选择支付渠道", 1000, 700, "/REPORT/InterfaceListTC");

}
function UpdateState(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择应用监控信息！", "error", "");
        return;
    }
    var url = "/REPORT/UpdateState";
    var data = {
        state: state, ids: vals
    };
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

//获取选择的应用（用于添加）
function selectedApp(index) {
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

function shyy() {
    window.parent.ShouwDiaLogWan("选择应用", 680, 470, "/payment/AppListTC");
}
function shyyone() {
    window.parent.ShouwDiaLogWan("选择应用", 680, 470, "/APP/AppListTC");

}
//验证分钟数
function yzMinute() {
    var a_minute = $.trim($("#a_minute").val());
    if ($.trim(a_minute) != "") {
        if (isNaN(a_minute)) {
            $("#minuteyy").attr("class", "Validform_checktip Validform_wrong");
            $("#minuteyy").html("分钟数只能为数字！");
            return false;
        } else {
            $("#minuteyy").attr("class", "Validform_checktip  Validform_right");
            $("#minuteyy").html("验证通过");
        }
    } else {
        $("#minuteyy").attr("class", "Validform_checktip Validform_wrong");
        $("#minuteyy").html("分钟数不能为空！");
        return false;
    }
}

//验证晚上分钟数
function yzNightMinute ()
{
    var NightMinute = $.trim($("#NightMinute").val());
    if ($.trim(NightMinute) != "") {
        if (isNaN(NightMinute)) {
            $("#NightMinuteyy").attr("class", "Validform_checktip Validform_wrong");
            $("#NightMinuteyy").html("分钟数只能为数字！");
            return false;
        } else {
            $("#NightMinuteyy").attr("class", "Validform_checktip  Validform_right");
            $("#NightMinuteyy").html("验证通过");
        }
    } 

}

//验证其他分钟数
function yzOtherMinte()
{
    var OtherMinte = $.trim($("#OtherMinte").val());
    if ($.trim(OtherMinte) != "") {
        if (isNaN(OtherMinte)) {
            $("#OtherMinteyy").attr("class", "Validform_checktip Validform_wrong");
            $("#OtherMinteyy").html("分钟数只能为数字！");
            return false;
        } else {
            $("#OtherMinteyy").attr("class", "Validform_checktip  Validform_right");
            $("#OtherMinteyy").html("验证通过");
        }
    } 
}
//验证阀值
function yzrequest ()
{
    var a_request = $.trim($("#a_request").val());
    var aType = $("#a_type").val() + '';
    var rex = /^\d+(\.\d{2})?$/;
    var isYyzz3 = rex.test(a_request);//格式是否正确
    if ($.trim(a_request) == "") {
        $("#requestyy").attr("class", "Validform_checktip Validform_wrong");
        $("#requestyy").html("请输入阀值");
        return false;
    } else if (!isYyzz3) {
        $("#requestyy").attr("class", "Validform_checktip Validform_wrong");
        $("#requestyy").html("阀值为整数或者保留两位小数！");
        return false;
    }
    else if ($.trim(a_request) === 0) {
        if (aType !== "1") {
            $("#requestyy").attr("class", "Validform_checktip Validform_wrong");
            $("#requestyy").html("阀值必须大于0");
            return false;
        }
    }
    else {
        $("#requestyy").attr("class", "Validform_checktip  Validform_right");
        $("#requestyy").html("验证通过");
    }

}
//修改应用监控弹窗
function UpdateComplaint(c_id) {

    window.parent.ShouwDiaLogWan("修改应用监控信息", 680, 560, "/REPORT/AppmonitorEdit?c_id=" + c_id);
}




