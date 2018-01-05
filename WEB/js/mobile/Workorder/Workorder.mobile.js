
$(function () {
    //添加工单
    $("#AddWorkorder").click(function () {
        var watchIdsOfTheDay = $.trim($("#watchIdsOfTheDay").val());
        var title = $.trim($("#title").val());
        var catalog = $("#catalog").val();
        var content = $.trim($("#content").val());
        if ($.trim(title) == "") {
            $("#yztitle").attr("class", "Validform_checktip Validform_wrong");
            $("#yztitle").html("请输入工单标题");
            return false;
        }
        else {
            $("#yztitle").attr("class", "Validform_checktip  Validform_right");
            $("#yztitle").html("验证通过");
        }
        if ($.trim(content) == "") {
            $("#yzcontent").attr("class", "Validform_checktip Validform_wrong");
            $("#yzcontent").html("请输入工单内容");
            return false;
        }
        else {
            $("#yzcontent").attr("class", "Validform_checktip  Validform_right");
            $("#yzcontent").html("验证通过");
        }

        $("#AddWorkorder").attr("disabled", "disabled");
        var data = {
            watchIdsOfTheDay: watchIdsOfTheDay, title: title, catalog: catalog, content: content
        };
        var url = "/Workorder/InsertWorkorder";
        $.post(url, data, function (retJson) {
            $("#AddWorkorder").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                ShowMsg(retJson.msg, "error", "");
                return false;
            }

        })
    })
    //回复交流以及处理
    $("#btnWorkorderCL").click(function () {
        var id = $.trim($("#id").val());
        var progress = $.trim($("#progress").val());
        var status = $.trim($("#status").val());
        var handleResultDescription = $.trim($("#handleResultDescription").val());
        if ($.trim(handleResultDescription) == "") {
            $("#yzhandleResultDescription").attr("class", "Validform_checktip Validform_wrong");
            $("#yzhandleResultDescription").html("请输入交流描述");
            return false;
        }
        else {
            $("#yzhandleResultDescription").attr("class", "Validform_checktip  Validform_right");
            $("#yzhandleResultDescription").html("验证通过");
        }

        $("#btnWorkorderCL").attr("disabled", "disabled");
        var data = {
            id: id, progress: progress, status: status, handleResultDescription: handleResultDescription
        };
        var url = "/Workorder/WorkorderCLJG";
        $.post(url, data, function (retJson) {
            $("#btnWorkorderCL").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                ShowMsg(retJson.msg, "error", "");
                return false;
            }
        })

    })
    //关闭原因
    $("#btnWorkorderGB").click(function () {
        var id = $.trim($("#r_id").val());
        var closeReason = $.trim($("#closeReason").val());
        if ($.trim(closeReason) == "") {
            $("#yzcloseReason").attr("class", "Validform_checktip Validform_wrong");
            $("#yzcloseReason").html("请输入工单关闭原因");
            return false;
        }
        else {
            $("#yzcloseReason").attr("class", "Validform_checktip  Validform_right");
            $("#yzcloseReason").html("验证通过");
        }
        var state = -1
        var url = "/Workorder/UpdateState";
        var data = {
            ids: id,
            state: state,
            closeReason: closeReason
        };
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                ShowMsg(retJson.msg, "error", "");
                return false;
            }
        });
    })

    //处理完成工单
    $("#btnWorkorderWC").click(function () {
        var id = $.trim($("#id").val());
        var handleResultDescription = $.trim($("#handleResultDescription").val());
        var data = {
            id: id, handleResultDescription: handleResultDescription
        };
        var url = "/Workorder/WorkorderWCJG";
        $.post(url, data, function (retJson) {
            $("#btnWorkorderWC").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                ShowMsg(retJson.msg, "error", "");
                return false;
            }
        })
    })

    //处理人申诉
    $("#btnWorkorderCLSHS").click(function () {
        var id = $.trim($("#id").val());
        var state = $('#radOrvActnS input[name="ty"]:checked ').val();
        var handlerReason = $.trim($("#handlerReason").val());
        if (state == 0) {
            if ($.trim(handlerReason) == "") {
                $("#yzhandlerReason").attr("class", "Validform_checktip Validform_wrong");
                $("#yzhandlerReason").html("请输入申诉原因");
                return false;
            }
            else {
                $("#yzhandlerReason").attr("class", "Validform_checktip  Validform_right");
                $("#yzhandlerReason").html("验证通过");
            }
        }
        var data = {
            id: id, handlerReason: handlerReason, state: state
        };
        var url = "/Workorder/WorkorderCLSHSJG";
        $.post(url, data, function (retJson) {
            $("#btnWorkorderCLSHS").attr("disabled", false);
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


//修改状态
function UpdateState(id) {
    if (id === "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("关闭工单", 620, 260, "/Workorder/WorkorderGB?id=" + id);

}

//处理工单
function WorkorderCL(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("处理工单", 800, 700, "/Workorder/WorkorderCL?id=" + id);

}

//完成工单
function WorkorderWC(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("完成工单", 580, 400, "/Workorder/WorkorderWC?id=" + id);
}
//评价是否合理
function WorkorderCLSHS(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("评价是否合理", 580, 400, "/Workorder/WorkorderCLSHS?id=" + id);
}
//查看详情
function WorkorderInfo(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("工单详细信息", 800, 700, "/Workorder/WorkorderInfo?id=" + id);
}
//分页
function ArticleManage(pageIndex, pageSize) {

    var url = "/Workorder/WorkorderList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var status = $("#status").val();
    var progress = $("#progress").val();
    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());

    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&status=" + status + "&progress=" + progress + "&r_begin=" + r_begin + "&r_end=" + r_end;
    location.href = encodeURI(url);
}

//workordercl视图页分页
function ArticleManagecl(pageIndex, pageSize) {
    var url = "/Workorder/WorkorderCL?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var id = $("#id").val();
    url += "&id=" + id;
    url += "&jl=2";
    location.href = encodeURI(url);
}

//workorderinfo视图页分页
function ArticleManageinfo(pageIndex, pageSize) {
    var url = "/Workorder/WorkorderInfo?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var id = $("#id").val();
    url += "&id=" + id;
    url += "&jl=2";
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//选择每页处理页面显示数量
function pagexzCL() {
    var PageSize = $("#pagexzCL").val();
    ArticleManagecl(1, PageSize);
}

//添加工单
function AddAPPlog() {
    window.parent.ShouwDiaLogWan("添加工单", 800, 630, "/Workorder/WorkorderAdd");
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

//验证工单标题
function yztitle() {
    var title = $.trim($("#title").val());
    if ($.trim(title) == "") {
        $("#yztitle").attr("class", "Validform_checktip Validform_wrong");
        $("#yztitle").html("请输入工单标题");
        return false;
    }
    else {
        $("#yztitle").attr("class", "Validform_checktip  Validform_right");
        $("#yztitle").html("验证通过");
    }
}
//验证工单内容
function yzcontent() {
    var content = $.trim($("#content").val());
    if ($.trim(content) == "") {
        $("#yzcontent").attr("class", "Validform_checktip Validform_wrong");
        $("#yzcontent").html("请输入工单内容");
        return false;
    }
    else {
        $("#yzcontent").attr("class", "Validform_checktip  Validform_right");
        $("#yzcontent").html("验证通过");
    }
}

//验证交流描述
function yzhandleResultDescription() {
    var handleResultDescription = $.trim($("#handleResultDescription").val());
    if ($.trim(handleResultDescription) == "") {
        $("#yzhandleResultDescription").attr("class", "Validform_checktip Validform_wrong");
        $("#yzhandleResultDescription").html("请输入交流描述");
        return false;
    }
    else {
        $("#yzhandleResultDescription").attr("class", "Validform_checktip  Validform_right");
        $("#yzhandleResultDescription").html("验证通过");
    }
}

//验证关闭原因
function yzcloseReason() {
    var closeReason = $.trim($("#closeReason").val());
    if ($.trim(closeReason) == "") {
        $("#yzcloseReason").attr("class", "Validform_checktip Validform_wrong");
        $("#yzcloseReason").html("请输入工单关闭原因");
        return false;
    }
    else {
        $("#yzcloseReason").attr("class", "Validform_checktip  Validform_right");
        $("#yzcloseReason").html("验证通过");
    }
}

//评价工单
function WorkorderPJ(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("评价工单", 600, 400, "/Workorder/WorkorderPF?id=" + id);
}


//提交人申诉
function WorkorderTJSHS(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择工单！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("关闭是否合理", 580, 400, "/Workorder/WorkorderTJSHS?id=" + id);
}

//验证处理人不同意评价的原因
function yzhandlerReason() {

    var state = $('#radOrvActnS input[name="ty"]:checked ').val();

    var handlerReason = $.trim($("#handlerReason").val());
    if (state == 0) {
        if ($.trim(handlerReason) == "") {
            $("#yzhandlerReason").attr("class", "Validform_checktip Validform_wrong");
            $("#yzhandlerReason").html("请输入原因");
            return false;
        }
        else {
            $("#yzhandlerReason").attr("class", "Validform_checktip  Validform_right");
            $("#yzhandlerReason").html("验证通过");
        }
    }
}
