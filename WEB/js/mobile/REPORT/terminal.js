$(function () {
 
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/REPORT/terminalList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $.trim($("#searchType").val());
    var searchname = $.trim($("#searchname").val());
    var nettype = $.trim($("#nettype").val());
    var searchDesc = $.trim($("#searchDesc").val());
    url += "&searchType=" + $.trim(searchType) + "&searchname=" + $.trim(searchname) + "&nettype=" + $.trim(nettype) + "&searchDesc=" + $.trim(searchDesc);
    location.href = encodeURI(url);
}
//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//列表查询
function selectorderlist() {//查询
    var pagexz = $("#pageIndex").val();
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}