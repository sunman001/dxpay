
$(function () {
    //添加通知短信分组信息
    $("#btnSaveAdd").click(function () {
        var Id = $.trim($("#Id").val());
        //验证分组名称
        var rexp = /^([\u4e00-\u9fa5]{0,100})|([A-Za-z0-9 ]{10,60})$/;
        var Code = $.trim($("#Code").val());
        if ($.trim(Code) == "") {
            $("#Codetyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Codetyy").html("分组编码不能为空！");
            return false;
        }
        else {
            $("#Codetyy").attr("class", "Validform_checktip  Validform_right");
            $("#Codetyy").html("验证通过")
        }
        var Description = $.trim($("#Description").val());
        if ($.trim(Description) == "") {
            $("#Descriptionyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Descriptionyy").html("分组编码不能为空！");
            return false;
        }
        else {
            $("#Descriptionyy").attr("class", "Validform_checktip  Validform_right");
            $("#Descriptionyy").html("验证通过")
        }

        var NotifyMobileList = $.trim($("#NotifyMobileList").val());

        if ($.trim(NotifyMobileList) == "") {
            $("#NotifyMobileListyy").attr("class", "Validform_checktip Validform_wrong");
            $("#NotifyMobileListyy").html("手机号码不能为空！");
            return false;
        }
        else {
            var result = NotifyMobileList.split(",");
            for (var i = 0; i < result.length; i++) {
                if (!isMobileOrPhone(result[i])) {
                    $("#NotifyMobileListyy").attr("class", "Validform_checktip Validform_wrong");
                    $("#NotifyMobileListyy").html("第'" + i + "'个输入手机号码格式不对，必须是11位手机号或固定电话(号码或区号-号码)！！");
                    return false;
                }
            }
            $("#NotifyMobileListyy").attr("class", "Validform_checktip  Validform_right");
            $("#NotifyMobileListyy").html("验证通过")
        }
        var MessageTemplate = $.trim($("#MessageTemplate").val());
        if ($.trim(MessageTemplate) == "") {
            $("#MessageTemplateyy").attr("class", "Validform_checktip Validform_wrong");
            $("#MessageTemplateyy").html("短信模板不能为空！");
            return false;
        }
        else {
            if (MessageTemplate.length > 255) {
                $("#MessageTemplateyy").attr("class", "Validform_checktip Validform_wrong");
                $("#MessageTemplateyy").html("短信模板长度不能大于255！");
            }
            else {
                $("#MessageTemplateyy").attr("class", "Validform_checktip  Validform_right");
                $("#MessageTemplateyy").html("验证通过")
            }
        }

        var IntervalUnit = $.trim($("#IntervalUnit").val());

        //验证执行周期
        var IntervalValue = $.trim($("#IntervalValue").val());
        if ($.trim(IntervalValue) != "0") {
            if (isNaN(IntervalValue)) {
                $("#IntervalValueyy").attr("class", "Validform_checktip Validform_wrong");
                $("#IntervalValueyy").html("执行周期只能为数字！");
                return false;
            } else {
                $("#IntervalValueyy").attr("class", "Validform_checktip  Validform_right");
                $("#IntervalValueyy").html("验证通过");
            }
        } else {
            $("#IntervalValueyy").attr("class", "Validform_checktip Validform_wrong");
            $("#IntervalValueyy").html("执行周期不能等于0！");
            return false;
        }
        $("#btnSaveAdd").attr("disabled", "disabled");
        var IsAllowSendMessage = $.trim($("#IsAllowSendMessage").val());
        var valArr = new Array;
        $('input[name="zflx"]:checked').each(function (i) {
            valArr[i] = $(this).val();
        });
        var vals = valArr.join(',');
        if (vals == "") {
            window.parent.ShowMsg("请选择消息发送方式！", "error", "");
            return false;
        }
        var audioTelTempId = $("#AudioTelTempId").val();
        var audioTelTempContent = $("#AudioTelTempContent").val();
        var data = {
            Id: $.trim(Id), Code: $.trim(Code), Description: Description, NotifyMobileList: NotifyMobileList, NotifyMobileList: NotifyMobileList, MessageTemplate: MessageTemplate, IntervalUnit: IntervalUnit, IntervalValue: IntervalValue, IsAllowSendMessage: IsAllowSendMessage, SendMode: vals, AudioTelTempId: audioTelTempId, AudioTelTempContent:audioTelTempContent
        };
        var url = "/REPORT/InsertUpdateNotificaiton";
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.location.href = "/REPORT/NotificaitonList";
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
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/REPORT/NotificaitonList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var IntervalUnit = $("#IntervalUnit").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());

    url += "&type=" + searchType + "&IntervalUnit=" + IntervalUnit + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
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
//添加通知短信分组信息
function AddAPPlog() {
    window.location.href = "/REPORT/NotificaitonEdit";
  
}

function UpdateState(state) {
    var valArr = new Array;
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择通知短信分组信息！", "error", "");
        return;
    }
    var url = "/REPORT/IsEnabled";
    var data = {
        state: state, ids: vals
    };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.location.reload();
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

function Delete()
{
    var valArr = new Array;
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择通知短信分组信息！", "error", "");
        return;
    }
    var url = "/REPORT/IsDELETE";
    var data = {
        ids: vals
    };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.location.reload();
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

//验证分组名称
function yzName() {
    var Name = $.trim($("#Name").val());
    var rexp = /^([\u4e00-\u9fa5]{0,100})|([A-Za-z0-9 ]{10,60})$/;
    if ($.trim(Name) == "") {
        $("#Nameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#Nameyy").html("分组名称不能为空！");
        return false;
    }
    else {
        if (!rexp.test(Name)) {
            $("#Nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Nameyy").html("分组名称长度不能大于100！");
            return false;;
        }
        else {
            $("#Nameyy").attr("class", "Validform_checktip  Validform_right");
            $("#Nameyy").html("验证通过")
        }
    }
}
//验证编码
function yzCode() {
    var Code = $.trim($("#Code").val());
    if ($.trim(Code) == "") {
        $("#Codetyy").attr("class", "Validform_checktip Validform_wrong");
        $("#Codetyy").html("分组编码不能为空！");
        return false;
    }
    else {
        $("#Codetyy").attr("class", "Validform_checktip  Validform_right");
        $("#Codetyy").html("验证通过")

    }
}
//验证描述
function yzDescription() {
    var Description = $.trim($("#Description").val());
    if ($.trim(Description) == "") {
        $("#Descriptionyy").attr("class", "Validform_checktip Validform_wrong");
        $("#Descriptionyy").html("分组描述不能为空！");
        return false;
    }
    else {
        $("#Descriptionyy").attr("class", "Validform_checktip  Validform_right");
        $("#Descriptionyy").html("验证通过")
    }
}
//验证手机号码
function yzNotifyMobileList() {
    var NotifyMobileList = $.trim($("#NotifyMobileList").val());

    if ($.trim(NotifyMobileList) == "") {
        $("#NotifyMobileListyy").attr("class", "Validform_checktip Validform_wrong");
        $("#NotifyMobileListyy").html("手机号码不能为空！");
        return false;
    }
    else {
        var result = NotifyMobileList.split(",");
        for (var i = 0; i < result.length; i++) {
            if (!isMobileOrPhone(result[i])) {
                $("#NotifyMobileListyy").attr("class", "Validform_checktip Validform_wrong");
                var a = parseInt(i) + parseInt(1);
                $("#NotifyMobileListyy").html("第 " + a + "个输入手机号码格式不对，必须是11位手机号或固定电话(号码或区号-号码)！！");
                return false;
            }
        }
        $("#NotifyMobileListyy").attr("class", "Validform_checktip  Validform_right");
        $("#NotifyMobileListyy").html("验证通过")
    }
}
//验证短信模板
function yzMessageTemplate()
{
    var MessageTemplate = $.trim($("#MessageTemplate").val());
    if ($.trim(MessageTemplate) == "") {
        $("#MessageTemplateyy").attr("class", "Validform_checktip Validform_wrong");
        $("#MessageTemplateyy").html("短信模板不能为空！");
        return false;
    }
    else {
        if (MessageTemplate.length > 255) {
            $("#MessageTemplateyy").attr("class", "Validform_checktip Validform_wrong");
            $("#MessageTemplateyy").html("短信模板长度不能大于255！");
        }
        else {
            $("#MessageTemplateyy").attr("class", "Validform_checktip  Validform_right");
            $("#MessageTemplateyy").html("验证通过")
        }
    }
}

function yzIntervalValue()
{
    var IntervalValue = $.trim($("#IntervalValue").val());
    if ($.trim(IntervalValue) != "0") {
        if (isNaN(IntervalValue)) {
            $("#IntervalValueyy").attr("class", "Validform_checktip Validform_wrong");
            $("#IntervalValueyy").html("执行周期只能为数字！");
            return false;
        } else {
            $("#IntervalValueyy").attr("class", "Validform_checktip  Validform_right");
            $("#IntervalValueyy").html("验证通过");
        }
    } else {
        $("#IntervalValueyy").attr("class", "Validform_checktip Validform_wrong");
        $("#IntervalValueyy").html("执行周期不能等于0！");
        return false;
    }
}
//修改应用监控弹窗
function UpdateComplaint(c_id) {

    window.parent.ShouwDiaLogWan("修改通知短信分组信息", 950, 800, "/REPORT/NotificaitonEdit?c_id=" + c_id);
}

//修改应用监控弹窗
function UpdateMobile(c_id) {

    window.parent.ShouwDiaLogWan("修改通知短信分组信息", 950, 800, "/REPORT/NotificaitonEditMobile?c_id=" + c_id);
}




