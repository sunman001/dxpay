//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/Agent/AgentList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var s_type = $("#s_type").val();
    var s_keys = $("#s_keys").val();
    var status = $("#status").val();
    var AuditState = $("#AuditState").val();
    var searchDesc = $("#searchDesc").val();

    url += "&stype=" + s_type + "&skeys=" + s_keys + "&status=" + status + "&AuditState=" + AuditState + "&searchDesc=" + searchDesc;

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//列表查询
function selectBusinessPersonnel() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//修改状态
function UpdateState(id, state) {
    debugger;
    if (id === "") {
        window.parent.ShowMsg("请选择合作信息！", "error", "");
        return;
    }
    var url = "/Agent/UpdateState";
    var data = {
        state: state, id: id
    };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { });
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
    });

    //修改商务
    function AgentUpdate(id) {

        if (id > 0) {

            window.parent.ShouwDiaLogWan("修改代理商", 750, 632, "/Agent/AgentUpdate?id=" + id)
        }
        else {
            window.parent.ShowMsg("请选择要修改的代理商！", "error", "");
            return;
        }

    }


}

$(document).ready(function () {

    $("#btnServiceFeeRatioGradeAdd").click(function () {
        //开发者ID
        var id = $("#id").val();
        var ServiceFeeRatioGradeId = $("#ServiceFeeRatioGradeId").val();
        if (ServiceFeeRatioGradeId == "" || ServiceFeeRatioGradeId < 0 || ServiceFeeRatioGradeId == "undefined") {
            ShowMsg("请选择费率比例", "error", "");
            return false;
        }
        $("#btnServiceFeeRatioGradeAdd").attr("disabled", "disabled");

        var url = "/Agent/ScAdd";
        var data = { ServiceFeeRatioGradeId: $.trim(ServiceFeeRatioGradeId), id: $.trim(id) };
        $.post(url, data, function (retJson) {
            $("#btnServiceFeeRatioGradeAdd").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
                var name = "代理商列表";
                var isLeaf = true;//是否套用
                var id = $(this).attr("data-id");//id
                var href = "/Agent/AgentList";//链接
                closeIfram(name, isLeaf, href, id, 'child');
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

//批量更新
function Updatestate(obj) {
    
    var vals = "";
    $("#table :checkbox[checked]").each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    $.post("/Agent/CoAgentUpdate", { coid: vals, tag: obj }, function (result) {
        if (result.success == 1) {
            window.parent.ShowMsg(result.msg, "ok", function () {
                window.parent.global.reload();
                window.parent.layer.closeAll();
            });
         
        } else if (result.success == 9998) {
            window.parent.ShowMsg(result.msg, "error", "");
            return;
        } else if (result.success == 9999) {
            window.parent.ShowMsg(result.msg, "error", "");
            window.top.location.href = retJson.Redirect;
            return;
        } else {
            window.parent.ShowMsg(result.msg, "error", "");
            return;
        }
    });
}
