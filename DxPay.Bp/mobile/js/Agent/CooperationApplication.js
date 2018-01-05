//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/Agent/CooperationApplicationList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var status = $("#status").val();
    // var progress = $("#progress").val();
    // var r_begin = $.trim($("#stime").val());
    // var r_end = $.trim($("#etime").val());
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&status=" + status;
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

//修改状态
function UpdateState(id, state) {
    if (id === "") {
        window.parent.ShowMsg("请选择合作信息！", "error", "");
        return;
    }
    var url = "/Agent/UpdateState";
    var data = {
        state: state, id: id
    };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            parent.location.reload();
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
