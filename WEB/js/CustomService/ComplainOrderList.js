//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/CustomService/ComplainOrderList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var SeachDate = $("#SeachDate").val();
    var stime = $("#stime").val();
    var etime = $("#etime").val();
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&SeachDate=" + SeachDate + "&stime=" + stime + "&etime=" + etime;
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

//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择投诉信息！", "error", "");
        return;
    }
    var url = "/CustomService/PlUpdateStateOrder";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
    });
}

//修改投诉弹窗
function UpdateComplainOrder(id) {
    window.parent.ShouwDiaLogWan("修改投诉", 800, 600, "/CustomService/ComplainOrderUpdate?id=" + id);
}

//处理投诉弹窗
function ComplainHandlerAdd(id)
{
    window.parent.ShouwDiaLogWan("处理投诉", 800, 500, "/CustomService/ComplainHandler?id=" + id);
}

//处理方法
function btnSaveCustomHandler()
{
    var HandleResult = $("#HandleResult").val();
    var id = $("#Id").val();

    if ($.trim(HandleResult) != "") {
        $("#HandleResultyz").attr("class", "Validform_checktip  Validform_right");
        $("#HandleResultyz").html("验证通过");
    } else {
        $("#HandleResultyz").attr("class", "Validform_checktip Validform_wrong");
        $("#HandleResultyz").html("请填写处理结果！");
        return false;
    }

    var url = "/CustomService/ComplainHandlerAdd";
    var isRefund = $.trim($('input[name="IsRefund"]:checked').val());
    var data = { HandleResult: $.trim(HandleResult), id: $.trim(id), IsRefund: $.trim(isRefund) };

    $("#btnCustomHandler").attr("disabled", "disabled");

    $.post(url, data, function (retJson) {

        $("#btnCustomHandler").attr("disabled", false);
        if (retJson.success == 1) {

            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
    })
}