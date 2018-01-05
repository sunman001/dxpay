// JavaScript Document

function ArticleManage(pageIndex, pageSize) {
    var url = "/Financial/BillList?curr=" + pageIndex + "&psize=" + pageSize;

    var stime = $("#stime").val();
    var etime = $("#etime").val();
    var searchname = $("#searchname").val();

    var searchType = $("#searchType").val();

    if (eval(searchType) > 0) {
        url += "&searchType=" + $.trim(searchType)+"";
    }
    if (searchname) {
        url += "&searchname=" + $.trim(searchname);
    }
  

    url += "&stime=" + $.trim(stime) + "&etime=" + $.trim(etime);
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function SerachBillList() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

function ysBillList(type) {

    var name = "营收列表";
    var isLeaf = true;//是否套用
    var id = $(this).attr("data-id");//id
    var href = "/Report/AppReport?time=" + type;//链接
    AddTab(name, isLeaf, href, 'AppReport' + id, 'child');
}