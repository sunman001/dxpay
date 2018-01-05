//分页
function ArticleManage(pageIndex, pageSize) {

    var url = "/Agent/CoServiceList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var searchDesc = $("#searchDesc").val();

    url += "&type=" + searchType + "&sea_name=" + sea_name + "&searchDesc=" + searchDesc;
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