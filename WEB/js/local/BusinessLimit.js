//批量更新权限状态
function getLid(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择权限！", "error", "");
        return;
    }
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/LIMIT/AjaxLimitState",
        data: "ids=" + vals + "&state=" + state,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); });
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
        }
    });
}

//批量更新管理员状态
function getUid(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择后台用户！", "error", "");
        return;
    }
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/LOCUSER/AjaxUpdateLcoUserState",
        data: "ids=" + vals + "&state=" + state,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); });
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
        }
    });
}

//添加权限弹出页面
function AddLimitDialog() {
 
    window.parent.ShouwDiaLogWan("添加权限", 600, 500, "/LIMIT/InsertBusinessLimit");
}

function addlimits() {
    var name = $.trim($("#inputName").val());
    var url = $.trim($("#inputUrl").val());
    var voids = $.trim($("#inputVoid").val());
    var values = $.trim($("#inputValue").val());
    var topid = $("#sellimit_id  option:selected").val();
    var state = $('#radOrvActnS input[name="state"]:checked ').val();
    var icon = $.trim($("#inputIcnn").val());
    if (isNotNull(name)) {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("权限名不能为空");
        return;
    }
    else {
        $("#checkName").attr("class", "Validform_checktip  Validform_right");
        $("#checkName").html("验证通过");
    }
    if (isNotNull(url)) {
        $("#checkUrl").attr("class", "Validform_checktip Validform_wrong");
        $("#checkUrl").html("权限页面不能为空");
        return;
    }
    else {
        $("#checkUrl").attr("class", "Validform_checktip  Validform_right");
        $("#checkUrl").html("验证通过");
    }
    var selectType = $("#sellimit_id  option:selected").attr("data-type");
    if (selectType > 1) {
        if (isNotNull(voids)) {
            $("#checkCode").attr("class", "Validform_checktip  Validform_wrong");
            $("#checkUrl").html("权限方法不能为空");
            return;
        } else {
            $("#checkCode").attr("class", "Validform_checktip  Validform_right");
            $("#checkCode").html("验证通过");
            url = url + "," + voids;
        }
    }
    if (isNotNull(values)) {
        $("#checkValue").attr("class", "Validform_checktip Validform_wrong");
        $("#checkValue").html("权限值不能为空");
        return;
    }
    else {
        $("#checkValue").attr("class", "Validform_checktip  Validform_right");
        $("#checkValue").html("验证通过");
    }
    if (!isInteger(values)) {
        $("#checkValue").attr("class", "Validform_checktip Validform_wrong");
        $("#checkValue").html("权限值只能填写数字");
        return;
    }
    else {
        $("#checkValue").attr("class", "Validform_checktip  Validform_right");
        $("#checkValue").html("验证通过");
    }
    $("#btnSave").attr("disabled", "disabled");
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/LIMIT/addBusinessLimit",
        data: "name=" + name + "&url=" + url + "&values=" + values + "&topid=" + topid + "&state=" + state + "&icon=" + icon,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
            $("#btnSave").attr("disabled", "");
        }
    });

}

function updateLimitTopid() {
    var selectType = $("#sellimit_id  option:selected").attr("data-type");
    if (selectType < 2) {
        $("#Voids").hide();
    }
    else {
        $("#Voids").show();
    }
}

//更新权限弹出页面
function UpdateLimitDialog(objId) {
    window.parent.ShouwDiaLogWan("更新权限", 600, 500, "/LIMIT/BusinessUpdateLimit?lid=" + objId);
};

function updatelimits(objid) {
    var lid = $("#inputLid").val();
    var name = $.trim($("#inputName").val());
    var url = $.trim($("#inputUrl").val());
    var voids = $.trim($("#inputVoid").val());
    var values = $.trim($("#inputValue").val());
    var topid = $("#sellimit_id  option:selected").val();
    var state = $('#radOrvActnS input[name="state"]:checked ').val();
    var icon = $.trim($("#inputIcnn").val());
    if (isNotNull(name)) {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("权限名不能为空");
        return;
    }
    else {
        $("#checkName").attr("class", "Validform_checktip  Validform_right");
        $("#checkName").html("验证通过");
    }
    if (isNotNull(url)) {
        $("#checkUrl").attr("class", "Validform_checktip Validform_wrong");
        $("#checkUrl").html("权限页面不能为空");
        return;
    }
    else {
        $("#checkUrl").attr("class", "Validform_checktip  Validform_right");
        $("#checkUrl").html("验证通过");
    }
    var selectType = $("#sellimit_id  option:selected").attr("data-type");
    if (selectType > 1) {
        if (isNotNull(voids)) {
            $("#checkCode").attr("class", "Validform_checktip  Validform_wrong");
            $("#checkUrl").html("权限方法不能为空");
            return;
        } else {
            $("#checkCode").attr("class", "Validform_checktip  Validform_right");
            $("#checkCode").html("验证通过");
            url = url + "," + voids;
        }
    }
    if (isNotNull(values)) {
        $("#checkValue").attr("class", "Validform_checktip Validform_wrong");
        $("#checkValue").html("权限值不能为空");
        return;
    }
    else {
        $("#checkValue").attr("class", "Validform_checktip  Validform_right");
        $("#checkValue").html("验证通过");
    }
    if (!isInteger(values)) {
        $("#checkValue").attr("class", "Validform_checktip Validform_wrong");
        $("#checkValue").html("权限值只能填写数字");
        return;
    }
    else {
        $("#checkValue").attr("class", "Validform_checktip  Validform_right");
        $("#checkValue").html("验证通过");
    }
    $("#btnSave").attr("disabled", "disabled");
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/LIMIT/BusinessAjaxUpdateLimit",
        data: "name=" + name + "&url=" + url + "&values=" + values + "&topid=" + topid + "&state=" + state + "&lid=" + lid + "&icon=" + icon,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
            $("#btnSave").attr("disabled", "");
        }
    });
}

function changeName() {
    var name = $.trim($("#inputName").val());
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("权限名不能为空");
        return;
    }
    else {
        $("#checkName").attr("class", "Validform_checktip  Validform_right");
        $("#checkName").html("验证通过");
    }
}
function changeUrl() {
    var url = $.trim($("#inputUrl").val());
    if (url == "") {
        $("#checkUrl").attr("class", "Validform_checktip Validform_wrong");
        $("#checkUrl").html("权限页面不能为空");
        return;
    }
    else {
        $("#checkUrl").attr("class", "Validform_checktip  Validform_right");
        $("#checkUrl").html("验证通过");
    }

}
function changes() {
    var voids = $.trim($("#inputVoid").val());
    if (voids != "") {
        $("#checkCode").attr("class", "Validform_checktip  Validform_right");
        $("#checkCode").html("验证通过");

    }
    else {
        $("#checkCode").attr("class", "Validform_checktip  Validform_wrong");
        $("#checkUrl").html("权限方法不能为空");
        return;
    }
}
function changeValue() {
    var values = $.trim($("#inputValue").val());
    if (values == "") {
        $("#checkValue").attr("class", "Validform_checktip Validform_wrong");
        $("#checkValue").html("权限值不能为空");
        return;
    }
    else {
        if (!isInteger(values)) {
            $("#checkValue").attr("class", "Validform_checktip Validform_wrong");
            $("#checkValue").html("权限值只能填写数字");
            return;
        }
        else {
            $("#checkValue").attr("class", "Validform_checktip  Validform_right");
            $("#checkValue").html("验证通过");
        }

    }
}



//角色弹出页面
function AddRoleDialog() {
    window.parent.ShouwDiaLogWan("添加角色", 600, 250, "/ROTE/AddBusinessRote");

}

function UpdateRoleDialog(objId) {
    window.parent.ShouwDiaLogWan("编辑角色", 600, 250, "/ROTE/RoleBusinessUpdate?rid=" + objId);
}

//设置权限弹出框
function UpdateRoleLimitDialog(objId) {
    window.parent.ShouwDiaLogWan("编辑角色权限", 800, 500, "/ROTE/UpdateRoleLimit?type=2&rid=" + objId);
}

function addrole() {
    var name = $("#inputName").val();
    var state = $('input[name="state"]:checked ').val();
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("角色名不能为空");
        return;
    }
    else {
        $("#checkName").attr("class", "Validform_checktip  Validform_right");
        $("#checkName").html("验证通过");
    }
    $("#btnSave").attr("disabled", "disabled");
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/ROTE/AddBusinessRoleAjax",
        data: "name=" + name + "&state=" + state,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });

            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
            $("#btnSave").attr("disabled", "");
        }
    });

}

function inputCheckRoleName() {
    var name = $.trim($("#inputName").val());
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("角色名不能为空");
        return;
    }
    else {
        $("#checkName").attr("class", "Validform_checktip  Validform_right");
        $("#checkName").html("验证通过");
    }
}

function updaterole() {
    var name = $.trim($("#inputName").val());
    var state = $('input[name="state"]:checked ').val();
    var rid = $("#inputRid").val();
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("角色名不能为空");
        return;
    }
    else {
        $("#checkName").attr("class", "Validform_checktip  Validform_right");
        $("#checkName").html("验证通过");
    }
    $("#btnSave").attr("disabled", "disabled");
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/ROTE/RoleBusinessUpdateAjax",
        data: "name=" + name + "&state=" + state + "&rid=" + rid,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
            $("#btnSave").attr("disabled", "");
        }
    });

}

function updaterolevalue() {
    var valArr = new Array;
    $("#RolePanel :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    var rid = $("#rid").val();
    $("#btnSave").attr("disabled", "disabled");
    $.ajax({
        cache: "False",
        type: "POST",
        url: "/ROTE/UpdateRoleLimitAjax",
        data: "rid=" + rid + "&value=" + vals,
        success: function (retJson) {
            $("#btnSave").attr("disabled", false)
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });

            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return;
            }
          
        }
    });

}