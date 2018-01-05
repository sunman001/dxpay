$(function () {
    $("#btnSaveAddApp").click(function () {
        var titleName = $.trim($("#titleName").val());
        var zzyz = /^[\w\W]{1,20}$/;
        if ($.trim(titleName) != "") {
            if (zzyz.test(titleName)) {
                $("#titleNameyy").attr("class", "Validform_checktip  Validform_right");
                $("#titleNameyy").html("验证通过");
            } else {
                $("#titleNameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#titleNameyy").html("公告标题长度不超过20");
                return false;
            }
        } else {
            $("#titleNameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#titleNameyy").html("公告标题不能为空！");
            return false;
        }
        var contentname = $.trim($("#contentname").val());
        var tszf = /^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$/;
        if ($.trim(contentname) != "") {
            $("#contentnameyy").attr("class", "Validform_checktip  Validform_right");
            $("#contentnameyy").html("验证通过");
        } else {
            $("#contentnameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#contentnameyy").html("公告内容不能为空！");
            return false;
        }
        var topname = $('input[name="topname"]:checked ').val();
        var n_top = 0;
        if ($.trim(topname) != "") {
            n_top = topname;
        }
        var n_id = $.trim($("#n_id").val());
        $("#btnSaveAddApp").attr("disabled", "disabled");
        var url = "/MessageManagement/insertOrUpdatenotice";
        var data = { n_title: $.trim(titleName), n_content: $.trim(contentname), n_top: n_top, n_id: $.trim(n_id) };
        $.post(url, data, function (retJson) {
         $("#btnSaveAddApp").attr("disabled", false);
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
         
        })
    })
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/MessageManagement/noticeList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    url += "&searchType=" + $.trim(searchType) + "&sea_name=" + $.trim(sea_name);
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
//发布公告弹窗
function addnotice() {
    window.parent.ShouwDiaLogWan("发布公告", 650, 320, "/MessageManagement/notice");
}
//修改公告弹窗
function Updatenotice(nid) {
    window.parent.ShouwDiaLogWan("发布公告", 650, 320, "/MessageManagement/notice?n_id=" + nid);
}
//一键删除公告
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择公告！", "error", "");
        return;
    }
    var url = "/MessageManagement/UpdateDelete";
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
//验证公告标题
function yztitleName() {
    var titleName = $.trim($("#titleName").val());
    var zzyz = /^[\w\W]{1,20}$/;
    if ($.trim(titleName) != "") {
        if (zzyz.test(titleName)) {
            $("#titleNameyy").attr("class", "Validform_checktip  Validform_right");
            $("#titleNameyy").html("验证通过");
        } else {
            $("#titleNameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#titleNameyy").html("公告标题长度不超过20");
            return false;
        }
    } else {
        $("#titleNameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#titleNameyy").html("公告标题不能为空！");
        return false;
    }
}
//验证公告内容
function yzcontentname() {
    var contentname = $.trim($("#contentname").val());
    var tszf = /^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$/;
    if ($.trim(contentname) != "") {
        $("#contentnameyy").attr("class", "Validform_checktip  Validform_right");
        $("#contentnameyy").html("验证通过");
    } else {
        $("#contentnameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#contentnameyy").html("公告内容不能为空！");
        return false;
    }
}