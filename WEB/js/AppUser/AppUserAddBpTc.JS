//分页
function ArticleManage(pageIndex, pageSize) {

    var url = "/AppUser/AppUserAddBpTc?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var s_type = $("#s_type").val();
    var s_state = $("#s_state").val();
    var s_keys = $("#s_keys").val();

    url += "&s_type=" + s_type + "&s_keys=" + s_keys + "&s_state=" + s_state + "";

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function selectAppUserBpTc() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}