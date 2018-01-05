//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/AppUser/AppUserAddTc?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var s_type = $("#s_type").val();
    var s_keys = $("#s_keys").val();
    var status = $("#status").val();
    var AuditState = $("#AuditState").val();
    var searchDesc = $("#searchDesc").val();

    url += "&stype=" + s_type + "&skeys=" + s_keys + "&status=" + status + "&AuditState=" + AuditState + "&searchDesc=" + searchDesc;

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//列表查询
function AppUserSelect() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
