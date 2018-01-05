
$(function () {
    //提交处理
    $("#btnComplaintCL").click(function () {
      

        var uids = $("#r_id").val();
        var c_result = $("#c_result").val();
        
        if ($.trim(c_result) == "") {
                $("#c_result_p").attr("class", "Validform_checktip Validform_wrong");
                $("#c_result_p").html("必须填写处理结果！");
                return false;
            }
        else {
            $("#r_remark_p").attr("class", "Validform_checktip  Validform_right");
            $("#r_remark_p").html("验证通过");

        }

        $.post("/REPORT/OrderAuditJG", { rid: uids, remark: c_result }, function (retJson) {
            if (retJson.success == 1) {
                window.location.href = "/REPORT/OrderAuditList";
                //window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.frames['mainFrame'].location.reload(); window.parent.layer.closeAll(); });
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
            $("#btnAuditorRefund").attr("disabled", false);
        })



    })
})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/REPORT/OrderAuditList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());

    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
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
//数据导出
function Searchdc() {
    var url = "/REPORT/OrderAuditDc?dc=dc";
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();

    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
    location.href = encodeURI(url);
    //if (auditstate == 1) {
    //   
    //} else {
    //    window.parent.ShowMsg("只能导出审核通过的数据！", "error", "");
    //    return false;
    //}
}






//查询用户列表
function selectUserLiset() {
    //当前页
    var CurrcentPage = $("#curr_page").val();
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(CurrcentPage, PageSize);
}
//加载数据
function LoadData(currPage, pageSize) {
    var url = "/Financial/RefundList.?curr=" + currPage + "&psize=" + pageSize;
    var type = $("#s_type").val();
    var keys = $("#s_keys").val();
    var state = $("#s_state").val();
    var check = $("#s_check").val();
    var rzlx = $("#s_category").val();
    var sort = $("#s_sort").val();
    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx;
    location.href = encodeURI(url);
}
//批量处理
function bulkassign() {
    var vals = "";
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
    
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals === "") {
        window.parent.ShowMsg("请选择订单异常信息！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("处理订单异常", 700, 380, "/REPORT/OrderAuditCL?rid=" + vals);
}
//单个审核
function complaintLC(rid) {


    if (rid === "") {
        window.parent.ShowMsg("请选择订单异常信息！", "error", "");
        return;
    }
    window.location.href = "/REPORT/OrderAuditCL?rid=" + rid
   // window.parent.ShouwDiaLogWan("处理订单异常", 700, 380, "/REPORT/OrderAuditCL?rid=" + rid);
}

