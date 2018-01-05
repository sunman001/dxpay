$(function () {
    $('body').on('.select_user', 'click', function () {
        alert($(this).text());
    });

})
//手动提款分页
function ArticleManage(pageIndex, pageSize, ptype) {
    var url = "/Financial/ManualList?curr=" + pageIndex + "&psize=" + pageSize + "&rtype=" + ptype;
    var username = $("#username").val();
    var userid = $.trim($("#userid").val());
    var sort = $("#searchDesc").val();
    var tag = $("#s_state").val();
    url += "&username=" + username + "&userid=" + userid + "&s_sort=" + sort + "&s_state=" + tag;
    location.href = encodeURI(url);
}
//用户加载数据分页
function LoadData(currPage, pageSize) {
    var url = "/APP/UserList?curr=" + currPage + "&psize=" + pageSize;
    var type = $("#s_type").val();
    var keys = $("#s_keys").val();
    var state = $("#s_state").val();
    var check = $("#s_check").val();
    var rzlx = $("#s_category").val();
    var sort = $("#s_sort").val();
    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx;
    location.href = encodeURI(url);
}
//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//查询
function SerachUserReport() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//弹窗开发者列表
function xzuser() {
    window.parent.ShouwDiaLogWan("选择开发者", 1000, 700, "/Financial/UserList");
}
//查询用户列表
function selectUserLiset() {
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(1, PageSize);
}
//选择开发者用户
function yxuzyhuser(u_id, u_email, index) {
    window.top.document.getElementById("username").value = u_email;
    window.top.document.getElementById("userid").value = u_id;
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}
function Manual() {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择提款账单！", "error", "");
        return false;
    }
    var data = { ids: vals };
    var url = "/Financial/ManuaTk";
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            $("#btnSaveUpdateApp").attr("disabled", false);
            //window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.frames['mainFrame'].location.reload(); window.parent.layer.closeAll(); });
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
        
    })
}

