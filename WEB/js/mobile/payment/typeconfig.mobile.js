$(function () {
    //添加或修改
    $("#btnSavezftdcs").click(function () {
        var paymodeid = $.trim($("#paymodeid").val());
        if ($.trim(paymodeid) > 0) {
            $("#paymodeidyy").attr("class", "Validform_checktip  Validform_right");
            $("#paymodeidyy").html("验证通过");
        } else {
            $("#paymodeidyy").attr("class", "Validform_checktip Validform_wrong");
            $("#paymodeidyy").html("请选择支付类型！");
            return false;
        }
        var paymenttype = $.trim($("#paymenttype").val());
        var type = paymenttype.split(',')[0];
        if ($.trim(type) > 0) {
            $("#zftdyy").attr("class", "Validform_checktip  Validform_right");
            $("#zftdyy").html("验证通过");
        } else {
            $("#zftdyy").attr("class", "Validform_checktip Validform_wrong");
            $("#zftdyy").html("请选择支付通道！");
            return false;
        }
        var Label = $.trim($("#Label").val());
        if ($.trim(Label) != "") {
            if (isChinese(Label)) {
                $("#Labelyy").attr("class", "Validform_checktip  Validform_right");
                $("#Labelyy").html("验证通过");
            } else {
                $("#Labelyy").attr("class", "Validform_checktip Validform_wrong");
                $("#Labelyy").html("只能输入中文字符！");
                return false;
            }
        } else {
            $("#Labelyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Labelyy").html("标签名称不能为空！");
            return false;
        }
        var FieldName = $.trim($("#FieldName").val());
        if ($.trim(FieldName) != "") {
            if (isEnglishStr(FieldName)) {
                $("#FieldNameyy").attr("class", "Validform_checktip  Validform_right");
                $("#FieldNameyy").html("验证通过");
            } else {
                $("#FieldNameyy").attr("class", "Validform_checktip Validform_wrong");
                $("#FieldNameyy").html("字段名称只能为英文！");
                return false;
            }
        } else {
            $("#FieldNameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#FieldNameyy").html("字段名称不能为空！");
            return false;
        }

        var InputType = $.trim($("#InputType").val());
        if ($.trim(InputType) != "") {
            $("#InputTypeyy").attr("class", "Validform_checktip  Validform_right");
            $("#InputTypeyy").html("验证通过");
        } else {
            $("#InputTypeyy").attr("class", "Validform_checktip Validform_wrong");
            $("#InputTypeyy").html("请选择数据类型！");
            return false;
        }
        var Description = $.trim($("#Description").val());
        if ($.trim(Description) != "") {
            $("#Descriptionyy").attr("class", "Validform_checktip  Validform_right");
            $("#Descriptionyy").html("验证通过");
        } else {
            $("#Descriptionyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Descriptionyy").html("描述不能为空！");
            return false;
        }
        var id = $.trim($("#Id").val());
        $("#btnSavezftdcs").attr("disabled", "disabled");
        var data = { PaymentTypeId: $.trim(type), Label: $.trim(Label), Id: $.trim(id), FieldName: $.trim(FieldName), InputType: $.trim(InputType), Description: $.trim(Description) };
        var url = "/payment/typeconfigAddOrUpdate";
        $.post(url, data, function (retJson) {
            $("#btnSavezftdcs").attr("disabled", false);
            if (retJson.success == 1) {
                window.location.href = "/payment/typeconfiglist";
                
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

//添加
function Addtypeconfig() {
    window.location.href = "/payment/typeconfigAdd";
}
//选择支付类型
function zflxtype() {
    var paymodeid = $.trim($("#paymodeid").val());
    var zftdid = $.trim($("#zftdid").val());
    alert(paymodeid);
    var data = { p_type: paymodeid, zftdid: zftdid };
    $.post("/payment/SelectPaymenttype", data, function (datastr) {
        $("#paymenttype").find("option").each(function () {
            $(this).remove();
        });//移除原有下拉列表值
        $("#paymenttype").append(datastr);//添加值
        $("#paymenttypeht").ruleSingleSelect();//重新加载下拉列表样式
    })
}

//验证标签名称
function yzLabel() {
    var Label = $.trim($("#Label").val());
    if ($.trim(Label) != "") {
        if (isChinese(Label)) {
            $("#Labelyy").attr("class", "Validform_checktip  Validform_right");
            $("#Labelyy").html("验证通过");
        } else {
            $("#Labelyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Labelyy").html("只能输入中文字符！");
            return false;
        }
    } else {
        $("#Labelyy").attr("class", "Validform_checktip Validform_wrong");
        $("#Labelyy").html("标签名称不能为空！");
        return false;
    }
}
//验证字段名称
function yzFieldName() {
    var FieldName = $.trim($("#FieldName").val());
    if ($.trim(FieldName) != "") {
        if (isEnglishStr(FieldName)) {
            $("#FieldNameyy").attr("class", "Validform_checktip  Validform_right");
            $("#FieldNameyy").html("验证通过");
        } else {
            $("#FieldNameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#FieldNameyy").html("字段名称只能为英文！");
            return false;
        }
    } else {
        $("#FieldNameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#FieldNameyy").html("字段名称不能为空！");
        return false;
    }
}
//验证描述
function yzDescription() {
    var Description = $.trim($("#Description").val());
    if ($.trim(Description) != "") {
        $("#Descriptionyy").attr("class", "Validform_checktip  Validform_right");
        $("#Descriptionyy").html("验证通过");
    } else {
        $("#Descriptionyy").attr("class", "Validform_checktip Validform_wrong");
        $("#Descriptionyy").html("描述不能为空！");
        return false;
    }
}

//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/payment/typeconfiglist?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate;
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
//修改
function  UpdatetypeConfig(id)
{
    window.location.href = "/payment/typeconfigAdd?id=" + id;
}

//选择支付类型
function zflxtype() {
    var paymodeid = $.trim($("#paymodeid").val());
    var zftdid = $.trim($("#zftdid").val());
    var data = { p_type: paymodeid, zftdid: zftdid };
    $.post("/payment/SelectPaymenttype", data, function (datastr) {
        $("#paymenttype").find("option").each(function () {
            $(this).remove();
        });//移除原有下拉列表值
        $("#paymenttype").append(datastr);//添加值
        $("#paymenttypeht").ruleSingleSelect();//重新加载下拉列表样式      
    })
}

function Updatestate(state)
{
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择支付参数！", "error", "");
        return;
    }
    var url = "/payment/Updatetypeconfig";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.ShowMsg(retJson.msg, "ok", function () {
                window.location.href = "/payment/typeconfiglist";
           // window.parent.global.reload();
            });
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
}
