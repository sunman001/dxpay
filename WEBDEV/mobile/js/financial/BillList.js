// JavaScript Document

function ArticleManage(pageIndex, pageSize) {
    var url = "/Financial/BillList?curr=" + pageIndex + "&psize=" + pageSize;

    var stime = $("#stime").val();
    var etime = $("#etime").val();

    url += "&stime=" + stime + "&etime=" + etime;
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

    window.location.href = "/Report/AppReport?time=" + type;
    //var name = "营收列表";
    //var isLeaf = true;//是否套用
    //var id = $(this).attr("data-id");//id
    //var href = "/Report/AppReport?time=" + type;//链接
    //AddTab(name, isLeaf, href, 'AppReport' + id, 'child');
}