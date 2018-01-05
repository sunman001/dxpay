
$(document).ready(function () {

    //添加或修改值班表
    $("#btnScheduling").click(function () {
        var s_id = $.trim($("#s_id").val());
        //值班人
        var WatchId = $.trim($("#WatchId").val());
        if (WatchId > -1) {
            $("#yywatch").attr("class", "Validform_checktip  Validform_right");
            $("#yywatch").html("验证通过");
        } else {
            $("#yywatch").attr("class", "Validform_checktip Validform_wrong");
            $("#yywatch").html("请选择值班人");
            return false;
        }
        //开始时间
        var WatchstartDate = $.trim($("#stime").val());

        if (stime != "") {
            $("#yyTime").attr("class", "Validform_checktip  Validform_right");
            $("#yyTime").html("验证通过");
        }
        else {
            $("#yyTime").attr("class", "Validform_checktip Validform_wrong");
            $("#yyTime").html("请选择值班日期");
            return false;
        }

        $("#btnScheduling").attr("disabled", "disabled");
        var data = { WatchId: $.trim(WatchId), WatchstartDate: $.trim(WatchstartDate), id: $.trim(s_id) };

        var url = "/Workorder/InsertOrUpdateScheduling";
        $.post(url, data, function (retJson) {
            $("#btnScheduling").attr("disabled", false);
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

    //工单评分
    $("#btnWorkordeScore").click(function () {

        var id = $.trim($("#r_id").val());

        //分数
        var Score = $.trim($("#Score").val());

        if (Score > -1) {
            $("#yyScore").attr("class", "Validform_checktip  Validform_right");
            $("#yyScore").html("验证通过");
        }
        else {
            $("#yyScore").attr("class", "Validform_checktip Validform_wrong");
            $("#yyScore").html("请选择分数");
            return false;
        }

        //内容
        var scorereason = $.trim($("#scorereason").val());

        $("#btnWorkordeScore").attr("disabled", "disabled");

        var data = { id: $.trim(id), score: $.trim(Score), scorereason: $.trim(scorereason) }
        var url = "/Workorder/WorkorderPFJG";

        $.post(url, data, function (retJson) {

            $("#btnWorkordeScore").attr("disabled", false);
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

    //工单申述
    $("#btnWorkordershs").click(function () {

        var id = $("#id").val();
        var handleResultDescription = $("#handleResultDescription").val();

        var syes = $('input:radio:checked').val();

        //不同意必填原因
        if (syes == 2) {
            if (handleResultDescription != "") {
                $("#yyhandleResultDescription").attr("class", "Validform_checktip  Validform_right");
                $("#yyhandleResultDescription").html("验证通过");
            }
            else {
                $("#yyhandleResultDescription").attr("class", "Validform_checktip Validform_wrong");
                $("#yyhandleResultDescription").html("请填写不同意原因");
                return false;
            }

        }
        else {
            $("#yyhandleResultDescription").attr("class", "Validform_checktip  Validform_right");
            $("#yyhandleResultDescription").html("验证通过");
        }



        $("#btnWorkordershs").attr("disabled", "disabled");

        var data = { id: $.trim(id), nans: $.trim(syes), handleResultDescription: $.trim(handleResultDescription) };
        var url = "/Workorder/WorkorderTJSHSJG";

        $.post(url, data, function (retJson) {

            $("#btnWorkordershs").attr("disabled", false);
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

    var url = "/Workorder/SchedulingList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var WatchId = $("#s_name").val();
    var WatchstartDate = $("#stime").val();
    var WatchEndDate = $("#etime").val();

    url += "&WatchId=" + WatchId + "&WatchstartDate=" + WatchstartDate + "&WatchEndDate=" + WatchEndDate;

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function selectScheduling() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//添加值班表弹窗
function AddSdl() {
    window.parent.ShouwDiaLogWan("添加值班表", 600, 450, "/Workorder/AddScheduling");
}

function AddSdlP()
{
    window.parent.ShouwDiaLogWan("批量排班", 600, 450, "/Workorder/CreateSchedule");
}
//修改值班表弹窗
function UpdateSdl(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择值班信息！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("换班单", 600, 450, "/Workorder/EidtScheduling?s_id=" + id);
}

//验证值班人
function yzWatch() {

    var WatchId = $("#WatchId").val();
    if (WatchId > -1) {
        $("#yywatch").attr("class", "Validform_checktip  Validform_right");
        $("#yywatch").html("验证通过");
    } else {
        $("#yywatch").attr("class", "Validform_checktip Validform_wrong");
        $("#yywatch").html("请选择值班人");
        return false;
    }
}

//验证值班开始时间
function yzstart() {

    var stime = $("#stime").val();
    if (stime != "") {
        $("#yyTime").attr("class", "Validform_checktip  Validform_right");
        $("#yyTime").html("验证通过");
    }
    else {
        $("#yyTime").attr("class", "Validform_checktip Validform_wrong");
        $("#yyTime").html("请选择值班日期");
        return false;
    }

}

//验证评分
function yzScore() {
    var Score = $("#Score").val();
    if (Score > -1) {
        $("#yyScore").attr("class", "Validform_checktip  Validform_right");
        $("#yyScore").html("验证通过");
    }
    else {
        $("#yyScore").attr("class", "Validform_checktip Validform_wrong");
        $("#yyScore").html("请选择分数");
        return false;
    }
}

//验证原因
function yzhandleResultDescription() {



    var syes = $('input:radio:checked').val();

    var handleResultDescription = $("#handleResultDescription").val();

    if (syes == 2) {

        if ($.trim(handleResultDescription) != "") {

            $("#yyhandleResultDescription").attr("class", "Validform_checktip  Validform_right");
            $("#yyhandleResultDescription").html("验证通过");

        }
        else {

            $("#yyhandleResultDescription").attr("class", "Validform_checktip Validform_wrong");
            $("#yyhandleResultDescription").html("请填写不同意原因");
            return false;
        }

    }
    else {

        $("#yyhandleResultDescription").attr("class", "Validform_checktip  Validform_right");
        $("#yyhandleResultDescription").html("验证通过");
    }
}

