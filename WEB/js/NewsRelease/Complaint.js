
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
        $.post("/NewsRelease/ComplaintCLJG", { rid: uids, remark: c_result }, function (retJson) {
            $("#btnAuditorRefund").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
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
          
        })



    })



})
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/NewsRelease/GwComplaintList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
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



//批量处理
function bulkassign() {
    var vals = "";
    $("#table :checkbox[checked]").each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals === "") {
        window.parent.ShowMsg("请选择投诉信息！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("处理投诉", 580, 260, "/NewsRelease/ComplaintCL?rid=" + vals);
}
//单个审核
function complaintLC(rid) {


    if (rid === "") {
        window.parent.ShowMsg("请选择投诉信息！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("处理投诉", 580, 260, "/NewsRelease/ComplaintCL?rid=" + rid);
}

