// JavaScript Document

function ArticleManage(pageIndex, pageSize, ptype) {
    var url = "/Financial/PaymentList?curr=" + pageIndex + "&psize=" + pageSize + "&rtype=" + ptype;
    var types = $("#searchType option:selected").val();
    var searchKey = $("#searchKey").val();
    var stime = $("#stime").val();
    var etime = $("#etime").val();
    var sort = $("#searchDesc option:selected").val();
    var searchTotal = $("#searchTotal option:selected").val();
    var tag = $("#s_state").val();
    url += "&s_type=" + types + "&s_key=" + searchKey + "&s_sort=" + sort + "&s_field=" + searchTotal + "&s_begin=" + stime + "&s_end=" + etime + "&s_state=" + tag;
    document.location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function SerachUserReport() {
    var pagexz = $("#p_currcent").val();
    var PageSize = $("#pagexz").val();
    ArticleManage(pagexz, PageSize, "total");
}

//更新交易号
function UpdatePay(p_id) {
    var p_val = $("#p_dealno_" + p_id).val();
    if (!isNotNull(p_val)) {
        $.ajax({
            url: '/Financial/AjaxUpdateDealno',
            type: 'post',
            cache: false,
            async: false,
            data: { pid: p_id, dealno: p_val },
            success: function (result) {
                if (result.success == 1) {
                    window.parent.ShowMsg(result.msg, "ok", function () {
                        window.parent.global.reload();
                        window.parent.layer.closeAll();
                    });
                }
                else if (result.success == 9998) {
                    window.parent.ShowMsg(result.msg, "error", "");
                    return false;
                } else if (result.success == 9999) {
                    window.parent.ShowMsg(result.msg, "error", "");
                    window.top.location.href = result.Redirect;
                    return false;
                } else if (result.success == 9997) {
                    window.top.location.href = result.Redirect;
                    return false;
                }
                else {
                    window.parent.ShowMsg(result.msg, "error", "");
                    return false;
                }
            }
        });
    } else {
        window.parent.ShowMsg("请输入交易号", "error", function () {
            $("#p_dealno_" + p_id).focus();
        });
    }
}