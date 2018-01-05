
$(document).ready(function () {

    $("#btnServiceFeeRatioGradeAdd").click(function () {

        //费率等级ID
        var ServiceFeeRatioGradeId = $("#ServiceFeeRatioGradeId").val();
        
        if (ServiceFeeRatioGradeId > 0) {

            $("#yzfl").attr("class", "Validform_checktip  Validform_right");
            $("#yzfl").html("验证通过");

        }
        else {

            $("#yzfl").attr("class", "Validform_checktip  Validform_wrong");
            $("#yzfl").html("请选择费率等级！");
            return false;
        }
        
        //开发者ID
        var id = $("#id").val();

        if (id > 0) {

            $("#yzfl").attr("class", "Validform_checktip  Validform_right");
            $("#yzfl").html("验证通过");
        }
        else {
            $("#yzfl").attr("class", "Validform_checktip  Validform_wrong");
            $("#yzfl").html("请关闭弹窗，先选择开发者！");
            return false;
        }

        $("#btnServiceFeeRatioGradeAdd").attr("disabled", "disabled");

        var url = "/Agent/ScAdd";
        var data = { ServiceFeeRatioGradeId: $.trim(ServiceFeeRatioGradeId), id: $.trim(id) };

        $.post(url, data, function (retJson) {
            $("#btnServiceFeeRatioGradeAdd").attr("disabled", false);
            if (retJson.success == 1) {

                window.parent.global.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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

    var url = "/Agent/userList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var s_type = $("#s_type").val();
    var s_keys = $("#s_keys").val();
    var s_sort = $("#s_sort").val();
    var s_category = $("#s_category").val();

    url += "&stype=" + s_type + "&skeys=" + s_keys + "&scategory=" + s_category + "&s_sort=" + s_sort;
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//列表查询
function selectUserLiset() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//设置直客开发者费率
function UpdateUserKfZ(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择开发者！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("设置费率", 700, 300, "/Agent/ServiceChargeAdd?id=" + id);
}

//选择开发者费率
function xzCoService() {
    window.parent.ShouwDiaLogWan("费率列表", 1000, 700, "/Agent/CoServiceList");

}

//选择开发者用户
function xzfl(ServiceFeeRatioGradeId, ServiceFeeRatioGradeName, index) {
    window.parent.layer.getChildFrame("#ServiceFeeRatioGradeName", index).val(ServiceFeeRatioGradeName);
    window.parent.layer.getChildFrame("#ServiceFeeRatioGradeId", index).val(ServiceFeeRatioGradeId);
    window.parent.layer.getChildFrame("#yzfl", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#yzfl", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}