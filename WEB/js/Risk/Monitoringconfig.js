
$(document).ready(function () {
    //添加或修改值班表
    $("#btnSave").click(function () {
        $("#btnSave").attr("disabled", "disabled");
        var WhichHourList = [];
        $("#fileList").find("tr").each(function (i) {
            var tdArr = $(this).children();
            var hours = tdArr.eq(0).text();
            var Threshold = tdArr.eq(1).find("input[name='Threshold']").val();
            var TypeId = $.trim($("#TypeId").val());
            //验证值
            var rex = /^\d{1}(\.\d{1,4})?$/;
            if (TypeId==0)
            {
                  if ($.trim(Threshold) != 0) {
                   if (!rex.test(Threshold)) {
                    window.parent.ShowMsg("成功率只能是1位整数或者保留四位小数！", "error", "");
                    return;
                }
            }
            }
            else
            {
                if ($.trim(Threshold) != 0) {
                    var tex = /^\+?[0-9][0-9]*$/
                    if (!tex.test(Threshold)) {
                        window.parent.ShowMsg("请求频率请填写整数！", "error", "");
                        return;
                    }
                }
            }
            
            var el = { WhichHour: hours, Threshold: Threshold };
            WhichHourList.push(el);
        });

        var TargetId = $.trim($("#TargetId").val());
        var TypeId = $.trim($("#TypeId").val());
        var c_payid = $.trim($("#c_payid").val());
        //验证关联ID
        if (TargetId > 0) {
            if (c_payid <= 0) {
                window.parent.ShowMsg("请选择支付通道或者通道池！", "error", "");
                return;
            }
        }
        var rex = /^\+?[0-9][0-9]*$/
        var IntervalOfRecover = $("#IntervalOfRecover").val();
        var TargetId = $.trim($("#TargetId").val());
        if (TargetId == 1) {
            if ($.trim(IntervalOfRecover) != "") {
                if (!rex.test(IntervalOfRecover)) {
                    window.parent.ShowMsg("时间间隔必须为整数！", "error", "");
                    return;
                }
            }
        }
        var Id = $.trim($("#id").val());
        var url = "/Risk/AddorEidt";
        var data = {
            WhichHourLists: WhichHourList, TargetId: TargetId, TypeId: TypeId, RelatedId: c_payid, Id: Id, IntervalOfRecover
            : IntervalOfRecover
        };
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (retJson) { //call successfull
                $("#btnSave").attr("disabled", false);
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
            },
            error: function (xhr) {
                //error occurred
            }
        });
    })

})


//分页
function ArticleManage(pageIndex, pageSize) {

    var url = "/Risk/MonitoringconfigList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var TypeId = $("#TypeId").val();
    var TargetId = $("#TargetId").val();
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    url += "&TypeId=" + TypeId + "&TargetId=" + TargetId + "&searchType=" + searchType + "&sea_name=" + sea_name;
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function selectScheduling() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//添加值班表弹窗
function AddSdl() {
    window.parent.ShouwDiaLogWan("添加监控配置", 800, 600, "/Risk/AddMonitoringconfig");
}
//修改值班表弹窗
function UpdateSdl(TypeId, TargetId, RelatedId) {


    window.parent.ShouwDiaLogWan("修改监控配置", 800, 600, "/Risk/AddMonitoringconfig?TypeId=" + TypeId + "&TargetId=" + TargetId + "&RelatedId=" + RelatedId);
}

//验证
function getRelate() {
    var TargetId = $.trim($("#TargetId").val());
    document.getElementById("c_payname").value = "";
    document.getElementById("c_payid").value = "";
    if (TargetId == 0) {
        document.getElementById("divRelatedId").style = " display:none";
        document.getElementById("IntervalOfRecoverth").style = " display:none";


    }
    else if (TargetId == 1) {
        document.getElementById("divRelatedId").style = " display:block";
        document.getElementById("Relatedhtml").innerHTML = "请选择通道";
        document.getElementById("IntervalOfRecoverth").style = " display:block";


    }
    else if (TargetId == 2) {
        document.getElementById("divRelatedId").style = " display:block";
        document.getElementById("Relatedhtml").innerHTML = "请选择通道池";
        document.getElementById("IntervalOfRecoverth").style = " display:none";


    }
}
//验证阀值
function yzThreshold(obj) {
    var rex = /^\d{1}(\.\d{1,4})?$/;
    var TypeId = $.trim($("#TypeId").val());
    var bl = $("#" + obj).val();
    if (TypeId == "0") {
        if ($.trim(bl) != 0) {
            if (!rex.test(bl)) {
                window.parent.ShowMsg("只能是1位整数或者保留四位小数！", "error", "");
                return false;
            }
        }
    } else {
        var tex = /^\+?[0-9][0-9]*$/
        if (!tex.test(bl)) {
            window.parent.ShowMsg("请求频率请填写整数！", "error", "");
            return false;
        }
    }
}

function yzIntervalOfRecover() {
    var rex = /^\+?[0-9][0-9]*$/
    var a = $("#IntervalOfRecover").val();
    var TargetId = $.trim($("#TargetId").val());
    if (TargetId == 1) {
        if ($.trim(a) != "") {
            if (!rex.test(a)) {
                window.parent.ShowMsg("时间间隔必须为整数！", "error", "");
                return false;
            }
        }
    }

}
function shyy() {
    var TargetId = $.trim($("#TargetId").val());
    if (TargetId == 1) {
        window.parent.ShouwDiaLogWan("选择通道", 900, 700, "/report/InterfaceListTC");
    }
    if (TargetId == 2) {
        window.parent.ShouwDiaLogWan("选择通道池", 900, 700, "/payment/ChannelPoolTC?SelectId=c_payid&judge=yzkfz1&SelectName=c_payname");
    }

}





