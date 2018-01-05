$(function () {
    //发布消息
    $("#btnSaveAddmessage").click(function () {
        var receiver = $.trim($("#a_user_id").val());
        if ($.trim(receiver) == "") {
            $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
            $("#yzkfz").html("请选择接收人！");
            return false;
        } else {
            $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
            $("#yzkfz").html("验证通过");
        }
        var contentname = $.trim($("#contentname").val());
        var tszf = /^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$/;
        if ($.trim(contentname) != "") {
            $("#contentnameyy").attr("class", "Validform_checktip  Validform_right");
            $("#contentnameyy").html("验证通过");
        } else {
            $("#contentnameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#contentnameyy").html("消息内容不能为空！");
            return false;
        }
        var m_id = $.trim($("#m_id").val());
        $("#btnSaveAddmessage").attr("disabled", "disabled");
        var url = "/MessageManagement/InserOrUpdatemessage";
        var data = { m_receiver: $.trim(receiver), m_content: $.trim(contentname), m_id: $.trim(m_id) };
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
            $("#btnSaveAddmessage").attr("disabled", false);
        })
    })
    //回复消息
    $("#btnSaveRemessage").click(function () {
        var contentname = $.trim($("#contentname").val());
        var tszf = /^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$/;
        if ($.trim(contentname) != "") {
            if (!tszf.test(contentname)) {
                window.parent.ShowMsg("消息内容不输入特殊字符", "error", "");
                return false;
            }
        } else {
            window.parent.ShowMsg("消息内容不能为空", "error", "");
            return false;
        }
        var m_topid = $.trim($("#topid").val());
        var userid = $.trim($("#userid").val());
        $("#btnSaveRemessage").attr("disabled", "disabled");
        var url = "/MessageManagement/ReplyMessageUser";
        var data = { m_topid: $.trim(m_topid), m_content: $.trim(contentname), m_receiver: $.trim(userid) };
        $.post(url, data, function (retJson) {
            $("#btnSaveRemessage").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.top.location.href = "/MessageManagement/messagelist"; });
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
    var url = "/MessageManagement/messagelist?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
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
//发布消息弹窗
function addmessage() {
    window.parent.ShouwDiaLogWan("发布消息", 580, 260, "/MessageManagement/message?type=dd");
}
//修改消息弹窗
function Updatemessage(mid) {
    window.parent.ShouwDiaLogWan("编辑消息", 580, 260, "/MessageManagement/message?m_id=" + mid + "&type=up");
}
function selectUser(type) {
    if (type == "dd") {
        xzuser();
    } else {
        UpdateUser();
    }
}
//修改选择
function UpdateUser() {
    window.parent.ShouwDiaLogWan("选择开发者", 1000, 700, "/App/UserList");
}
//选择开发者弹窗
function xzuser() {
    window.parent.ShouwDiaLogWan("选择开发者", 1000, 700, "/MessageManagement/UserList");
}
//加载开发者用户数据
function LoadData(currPage, pageSize) {
    var url = "/MessageManagement/UserList?curr=" + currPage + "&psize=" + pageSize;
    var type = $("#s_type").val();
    var keys = $("#s_keys").val();
    var state = $("#s_state").val();
    var check = $("#s_check").val();
    var rzlx = $("#s_category").val();
    var sort = $("#s_sort").val();
    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx;
    location.href = encodeURI(url);
}
//查询用户列表
function selectUserLiset() {
    //当前页
    var CurrcentPage = $("#curr_page").val();
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(CurrcentPage, PageSize);
}
//获取选择的用户（用于添加）
function selectxzuser(index) {
    var valArr = new Array;
    $("#tableUser :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = $.trim((valArr.join(',')).replace("on,", ""));
    if (vals == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    window.parent.layer.getChildFrame("#a_user_id", index).val(vals);
    window.parent.layer.getChildFrame("#yzkfz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#yzkfz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
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
        $("#contentnameyy").html("消息内容不能为空！");
        return false;
    }
}
//一键删除消息
function UpdatestateMessage(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择消息！", "error", "");
        return;
    }
    var url = "/MessageManagement/UpdateDeleteMessage";
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