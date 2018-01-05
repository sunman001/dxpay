//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/CustomService/ComplainType?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc;
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

//添加投诉类型弹窗
function AddType() {
    window.parent.ShouwDiaLogWan("添加投诉类型", 800, 400, "/CustomService/ComplainTypeAdd");
}

//修改投诉类型弹窗
function UpdateCustom(id) {
    window.parent.ShouwDiaLogWan("修改投诉类型", 800, 400, "/CustomService/ComplainTypeAdd?id=" + id);
}

//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择投诉类型！", "error", "");
        return;
    }
    var url = "/CustomService/PlUpdateState";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
    });
}