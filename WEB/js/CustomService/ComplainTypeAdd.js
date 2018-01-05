//添加修改方法
$(function () {

    $("#btnSaveAddorUpdateCustomType").click(function () {

        var id = $("#Id").val()
        var Name = $("#Name").val();

        if ($.trim(Name) != "") {
            $("#nameyz").attr("class", "Validform_checktip  Validform_right");
            $("#nameyz").html("验证通过");
        } else {
            $("#nameyz").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyz").html("请填写投诉类型名称！");
            return false;
        }

        var Description = $("#Description").val();
        if ($.trim(Description) != "") {
            $("#Descriptionyz").attr("class", "Validform_checktip  Validform_right");
            $("#Descriptionyz").html("验证通过");
        } else {
            $("#Descriptionyz").attr("class", "Validform_checktip Validform_wrong");
            $("#Descriptionyz").html("请填写投诉类型描述！");
            return false;
        }

        var url = "/CustomService/InsertOrUpdateAddType";
        var data = { Name: $.trim(Name), Description: $.trim(Description), id: $.trim(id) };
        $("#btnSaveAddorUpdateCustomType").attr("disabled", "disabled");

        $.post(url, data, function (retJson) {

            $("#btnSaveAddorUpdateCustomType").attr("disabled", false);
            if (retJson.success == 1) {

                window.parent.global.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
        })
    });

});

//验证投诉类型名称
function yzname() {
    var Name = $("#Name").val();
    if ($.trim(Name) != "") {
        $("#nameyz").attr("class", "Validform_checktip  Validform_right");
        $("#nameyz").html("验证通过");
    } else {
        $("#nameyz").attr("class", "Validform_checktip Validform_wrong");
        $("#nameyz").html("请填写投诉类型名称！");
        return false;
    }
}
//验证投诉类型描述
function yzDescription() {
    var Description = $("#Description").val();
    if ($.trim(Description) != "") {
        $("#Descriptionyz").attr("class", "Validform_checktip  Validform_right");
        $("#Descriptionyz").html("验证通过");
    } else {
        $("#Descriptionyz").attr("class", "Validform_checktip Validform_wrong");
        $("#Descriptionyz").html("请填写投诉类型描述！");
        return false;
    }
}

