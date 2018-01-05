// JavaScript Document

function ArticleManage(pageIndex, pageSize) {
    var url = "/Financial/PaymentList?curr=" + pageIndex + "&psize=" + pageSize;
    ////var types = $("#searchType option:selected").val();
    ////var searchKey = $("#searchKey").val();
    //var stime = $("#stime").val();
    //var etime = $("#etime").val();
    //var sort = $("#searchDesc option:selected").val();
    //var searchTotal = $("#searchTotal option:selected").val();
    //var tag = $("#s_state").val();
    ////url += "&s_type=" + types + "&s_key=" + searchKey + "&s_sort=" + sort + "&s_field=" + searchTotal + "&s_begin=" + stime + "&s_end=" + etime + "&s_state=" + tag;
    //url += "&s_begin=" + stime + "&s_end=" + etime + "&s_state=" + tag + "&s_field=" + searchTotal + "&s_sort=" + sort;
    document.location.href = encodeURI(url);
}

//查询
function SerachUserReport() {
    var pagexz = $("#p_currcent").val();
    var PageSize = $("#p_pagesize").val();
    ArticleManage(pagexz, PageSize);
}

//查看账单列表
function ViewDetail(idlist) {
    parent.window.document.getElementById("mainFrame").src = "/Financial/BillDetail?bidlist=" + idlist;
}

//返回提款管理
function BackList() {
    parent.window.document.getElementById("mainFrame").src = "/Financial/PayMentList";
}