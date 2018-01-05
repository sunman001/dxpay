//批量更新权限状态
function getLid(state) {
    var valArr = new Array;
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
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
                window.parent.location.reload(); 
                window.parent.ShowMsg(retJson.msg, "ok", function () { });
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
    $("#table").find("input[type='checkbox']:checked").each(function (i) {
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
                window.parent.location.reload(); 
                window.parent.ShowMsg(retJson.msg, "ok", function () { });
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
    window.parent.ShouwDiaLogWan("添加权限", 800, 500, "/LIMIT/AddLimit");
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
        url: "/LIMIT/AjaxAddLimit",
        data: "name=" + name + "&url=" + url + "&values=" + values + "&topid=" + topid + "&state=" + state + "&icon=" + icon,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload(); 
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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
    window.parent.ShouwDiaLogWan("更新权限", 800, 500, "/LIMIT/UpdateLimit?lid=" + objId);
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
        url: "/LIMIT/AjaxUpdateLimit",
        data: "name=" + name + "&url=" + url + "&values=" + values + "&topid=" + topid + "&state=" + state + "&lid=" + lid + "&icon=" + icon,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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

//添加管理员弹出页面
function AddLocuserDialog() {
    window.parent.ShouwDiaLogWan("添加管理员", 800, 600, "/LOCUSER/AddUsers");
}

//编辑管理员弹出页面
function UpdateUser(objId) {
    window.parent.ShouwDiaLogWan("编辑管理员", 800, 600, "/LOCUSER/UpdateUsers?u_id=" + objId);
}

function addlcoaluser() {
    $("#btnSave").attr("disabled", "disabled");
    var name = $.trim($("#inputName").val());
    var uid = $("#inputUid").val();
    var pwd = $.trim($("#inputPwd").val());
    var pwdTrue = $.trim($("#inputPwdTrue").val());
    var realname = $.trim($("#inputRealname").val());
    var department = $.trim($("#inputDepartment").val());
    var position = $("#inputPosition").val();
    var roteId = $("#selrole_id").val();
    var state = $('#radOrvActnS input[name="state"]:checked ').val();

    var tmsg = "";
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("登录名不能为空");
        tmsg += "登录名不能为空";
    } else {
        $.ajax({
            url: "/LOCUSER/ExistsUserName",
            type: "post",
            cache: false,
            async: false,
            data: { u_name: name, u_id: uid },
            success: function (msg) {
                if (msg.success) {
                    $("#checkName").attr("class", "Validform_checktip Validform_wrong");
                    $("#checkName").html(msg.mess);
                    tmsg += msg.mess;
                } else {
                    $("#checkName").attr("class", "Validform_checktip Validform_right");
                    $("#checkName").html("验证通过");
                }
            }
        });
    }

    if (pwd.length < 6) {
        $("#inputPwdtext").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwdtext").html("密码不能小于6位");
        tmsg += "密码不能小于6位";
    } else {
        $("#inputPwdtext").attr("class", "Validform_checktip Validform_right");
        $("#inputPwdtext").html("验证成功");
    }

    if (pwdTrue.length < 6) {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwd2text").html("确认密码不能小于6位");
        tmsg += "确认密码不能小于6位";
    } else {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_right");
        $("#inputPwd2text").html("验证成功");
    }

    if (pwd != pwdTrue) {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwd2text").html("2次密码不一致");
        tmsg += "2次密码不一致";
    } else {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_right");
        $("#inputPwd2text").html("验证成功");
    }

    if (realname == "") {
        $("#checkRealName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkRealName").html("真实姓名不能为空");
        tmsg += "真实姓名不能为空";
    } else if (isChinaName(realname) == false) {
        $("#checkRealName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkRealName").html("真实姓名填写中文");
        tmsg += "真实姓名填写中文";
    } else {
        $("#checkRealName").attr("class", "Validform_checktip  Validform_right");
        $("#checkRealName").html("验证通过");
    }

    if (department == "") {
        $("#checkDep").attr("class", "Validform_checktip Validform_wrong");
        $("#checkDep").html("部门不能为空");
        tmsg += "部门不能为空";
    } else {
        $("#checkDep").attr("class", "Validform_checktip  Validform_right");
        $("#checkDep").html("验证通过");
    }

    if (position == "") {
        $("#checkPos").attr("class", "Validform_checktip Validform_wrong");
        $("#checkPos").html("职位不能为空");
        tmsg += "职位不能为空";
    } else {
        $("#checkPos").attr("class", "Validform_checktip  Validform_right");
        $("#checkPos").html("验证通过");
    }

    //手机号码
    var mobilenumber = $.trim($("#mobilenumber").val());
    if (isMobileOrPhone(mobilenumber)) {
        $("#Checkmobilenumber").attr("class", "Validform_checktip  Validform_right");
        $("#Checkmobilenumber").html("验证通过");
    } else {
        $("#Checkmobilenumber").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkmobilenumber").html("11位手机号或固定电话(号码或区号-号码)！");
        tmsg += "11位手机号或固定电话(号码或区号-号码)！";
    }
    //邮箱验证
    var emailaddress = $.trim($("#emailaddress").val());
    var isNull = isRequestNotNull(emailaddress);//是否为空
    var isMail = isEmail(emailaddress);//格式是否正确
    if (!isNull && isMail) {
        $("#Checkemailaddress").attr("class", "Validform_checktip  Validform_right");
        $("#Checkemailaddress").html("验证通过");
    }
    else if (isNull) {
        $("#Checkemailaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkemailaddress").html("请输入邮箱地址");

    } else if (!isMail) {
        $("#Checkemailaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkemailaddress").html("邮箱地址格式不正确");
        tmsg += "邮箱地址格式不正确";
    }
    //qq
    var qq = $.trim($("#qq").val());
    if (isQQ(qq)) {
        $("#Checkqq").attr("class", "Validform_checktip  Validform_right");
        $("#Checkqq").html("验证通过");

    } else {
        $("#Checkqq").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkqq").html("纯数字组成，5-16位之间！");
        tmsg += "纯数字组成，5-16位之间！";

    }
    if (tmsg == "") {
        $.ajax({
            cache: "False",
            type: "POST",
            url: "/LOCUSER/AjaxAddUser",
            data: "name=" + name + "&pwd=" + pwd + "&realName=" + realname + "&department=" + department + "&position=" + position + "&roteId=" + roteId + "&state=" + state+"&mobilenumber=" + mobilenumber + "&emailaddress=" + emailaddress + "&qq=" + qq,
            success: function (retJson) {
                if (retJson.success == 1) {
                    window.parent.location.reload(); 
                    window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
                }
                else if (retJson.success == 2) {
                    window.parent.ShowMsg(retJson.msg, "error", "");
                    return;
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
                $("#btnSave").removeAttr("disabled");
            }
        });
    } else {
        window.parent.ShowMsg("有输入项未正确输入，请检查！", "error", "");
        $("#btnSave").removeAttr("disabled");
    }

}

function updatelcoaluser() {
    $("#btnSave").attr("disabled", "disabled");
    var name = $.trim($("#inputName").val());
    var uid = $("#inputUid").val();
    var pwd = $.trim($("#inputPwd").val());
    var pwdTrue = $.trim($("#inputPwdTrue").val());
    var realname = $.trim($("#inputRealname").val());
    var department = $.trim($("#inputDepartment").val());
    var position = $.trim($("#inputPosition").val());
    var roteId = $("#selrole_id").val();
    var state = $('#radOrvActnS input[name="state"]:checked ').val();

    var tmsg = "";
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("登录名不能为空");
        tmsg += "登录名不能为空";
    } else {
        $.ajax({
            url: "/LOCUSER/ExistsUserName",
            type: "post",
            cache: false,
            async: false,
            data: { u_name: name, u_id: uid },
            success: function (msg) {
                if (msg.success) {
                    $("#checkName").attr("class", "Validform_checktip Validform_wrong");
                    $("#checkName").html(msg.mess);
                    tmsg += msg.mess;
                } else {
                    $("#checkName").attr("class", "Validform_checktip Validform_right");
                    $("#checkName").html("验证通过");
                }
            }
        });
    }

    if (pwd.length < 6) {
        $("#inputPwdtext").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwdtext").html("密码不能小于6位");
        tmsg += "密码不能小于6位";
    } else {
        $("#inputPwdtext").attr("class", "Validform_checktip Validform_right");
        $("#inputPwdtext").html("验证成功");
    }

    if (pwdTrue.length < 6) {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwd2text").html("确认密码不能小于6位");
        tmsg += "确认密码不能小于6位";
    } else {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_right");
        $("#inputPwd2text").html("验证成功");
    }

    if (pwd != pwdTrue) {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwd2text").html("2次密码不一致");
        tmsg += "2次密码不一致";
    } else {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_right");
        $("#inputPwd2text").html("验证成功");
    }

    if (realname == "") {
        $("#checkRealName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkRealName").html("真实姓名不能为空");
        tmsg += "真实姓名不能为空";
    } else if (isChinaName(realname) == false) {
        $("#checkRealName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkRealName").html("真实姓名填写中文");
        tmsg += "真实姓名填写中文";
    } else {
        $("#checkRealName").attr("class", "Validform_checktip  Validform_right");
        $("#checkRealName").html("验证通过");
    }

    if (department == "") {
        $("#checkDep").attr("class", "Validform_checktip Validform_wrong");
        $("#checkDep").html("部门不能为空");
        tmsg += "部门不能为空";
    } else {
        $("#checkDep").attr("class", "Validform_checktip  Validform_right");
        $("#checkDep").html("验证通过");
    }

    if (position == "") {
        $("#checkPos").attr("class", "Validform_checktip Validform_wrong");
        $("#checkPos").html("职位不能为空");
        tmsg += "职位不能为空";
    } else {
        $("#checkPos").attr("class", "Validform_checktip  Validform_right");
        $("#checkPos").html("验证通过");
    }
    //手机号码
    var mobilenumber = $.trim($("#mobilenumber").val());
    if (isMobileOrPhone(mobilenumber)) {
        $("#Checkmobilenumber").attr("class", "Validform_checktip  Validform_right");
        $("#Checkmobilenumber").html("验证通过");
    } else {
        $("#Checkmobilenumber").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkmobilenumber").html("11位手机号或固定电话(号码或区号-号码)！");
        tmsg += "11位手机号或固定电话(号码或区号-号码)！";
    }
    //邮箱验证
    var emailaddress = $.trim($("#emailaddress").val());
    var isNull = isRequestNotNull(emailaddress);//是否为空
    var isMail = isEmail(emailaddress);//格式是否正确
    if (!isNull && isMail) {
        $("#Checkemailaddress").attr("class", "Validform_checktip  Validform_right");
        $("#Checkemailaddress").html("验证通过");
    }
    else if (isNull) {
        $("#Checkemailaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkemailaddress").html("请输入邮箱地址");

    } else if (!isMail) {
        $("#Checkemailaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkemailaddress").html("邮箱地址格式不正确");
        tmsg += "邮箱地址格式不正确";
    }
    //qq
    var qq = $.trim($("#qq").val());
    if (isQQ(qq)) {
        $("#Checkqq").attr("class", "Validform_checktip  Validform_right");
        $("#Checkqq").html("验证通过");

    } else {
        $("#Checkqq").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkqq").html("纯数字组成，5-16位之间！");
        tmsg += "纯数字组成，5-16位之间！";

    }
    if (tmsg == "") {
        $.ajax({
            cache: "False",
            type: "POST",
            url: "/LOCUSER/AjaxUpdateUser",
            data: "name=" + name + "&pwd=" + pwd + "&realName=" + realname + "&department=" + department + "&position=" + position + "&roteId=" + roteId + "&state=" + state + "&id=" + uid+"&mobilenumber=" + mobilenumber + "&emailaddress=" + emailaddress + "&qq=" + qq,
            success: function (retJson) {
                if (retJson.success == 1) {
                    window.parent.location.reload();
                    window.parent.ShowMsg(retJson.msg, "ok", function () {
                        window.parent.layer.closeAll();
                    });
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
                $("#btnSave").removeAttr("disabled");
            }
        });
    } else {
        window.parent.ShowMsg("有输入项未正确输入，请检查！", "error", "");
        $("#btnSave").removeAttr("disabled");
    }
}

function inputPwdChange() {
    var pwd = $.trim($("#inputPwd").val());
    var pwdTrue = $.trim($("#inputPwdTrue").val());

    if (pwd.length < 6) {
        $("#inputPwdtext").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwdtext").html("密码不能小于6位");
        return;
    }
    else {
        $("#inputPwdtext").attr("class", "Validform_checktip Validform_right");
        $("#inputPwdtext").html("验证成功");
    }
}

function inputPwdChange2() {
    var pwd = $.trim($("#inputPwd").val());
    var pwdTrue = $.trim($("#inputPwdTrue").val());
    if (pwdTrue.length < 6) {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwd2text").html("确认密码不能小于6位");
        return;
    }
    else {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_right");
        $("#inputPwd2text").html("验证成功");
    }
    if (pwd != pwdTrue) {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_wrong");
        $("#inputPwd2text").html("2次密码不一致");
        return;
    }
    else {
        $("#inputPwd2text").attr("class", "Validform_checktip Validform_right");
        $("#inputPwd2text").html("验证成功");
    }
}

function inputCheckName() {
    var name = $.trim($("#inputName").val());
    var uid = $("#inputUid").val();
    if (name == "") {
        $("#checkName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkName").html("登录名不能为空");
        return;
    } else {
        $.ajax({
            url: "/LOCUSER/ExistsUserName",
            type: "post",
            cache: false,
            async: false,
            data: { u_name: name, u_id: uid },
            success: function (msg) {
                if (msg.success) {
                    $("#checkName").attr("class", "Validform_checktip Validform_wrong");
                    $("#checkName").html(msg.mess);
                    return;
                } else {
                    $("#checkName").attr("class", "Validform_checktip Validform_right");
                    $("#checkName").html("验证通过");
                }
            }
        });
    }
}

function inputCheckRealName() {
    var realName = $.trim($("#inputRealname").val());
    var reg = new RegExp("[\\u4E00-\\u9FFF]+", "g");
    if (realName == "") {
        $("#checkRealName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkRealName").html("真实姓名不能为空");
        return;
    }
    else if (isChinaName(realName) == false) {
        $("#checkRealName").attr("class", "Validform_checktip Validform_wrong");
        $("#checkRealName").html("真实姓名填写中文");
        return;
    }
    else {
        $("#checkRealName").attr("class", "Validform_checktip  Validform_right");
        $("#checkRealName").html("验证通过");
    }
}

function inputCheckDep() {
    var department = $.trim($("#inputDepartment").val());
    if (department == "") {
        $("#checkDep").attr("class", "Validform_checktip Validform_wrong");
        $("#checkDep").html("部门不能为空");
        return;
    }
    else {
        $("#checkDep").attr("class", "Validform_checktip  Validform_right");
        $("#checkDep").html("验证通过");
    }
}

function inputCheckPos() {
    var position = $.trim($("#inputPosition").val());
    if (position == "") {
        $("#checkPos").attr("class", "Validform_checktip Validform_wrong");
        $("#checkPos").html("职位不能为空");
        return;
    }
    else {
        $("#checkPos").attr("class", "Validform_checktip  Validform_right");
        $("#checkPos").html("验证通过");
    }
}

//验证手机号码
function Checkmobilenumber() {
    var mobilenumber = $.trim($("#mobilenumber").val());
    if (isMobileOrPhone(mobilenumber)) {
        $("#Checkmobilenumber").attr("class", "Validform_checktip  Validform_right");
        $("#Checkmobilenumber").html("验证通过");
    } else {
        $("#Checkmobilenumber").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkmobilenumber").html("11位手机号或固定电话(号码或区号-号码)！");

    }
}
//验证邮箱地址
function Checkemailaddress() {
    var emailaddress = $.trim($("#emailaddress").val());
    var isNull = isRequestNotNull(emailaddress);//是否为空
    var isMail = isEmail(emailaddress);//格式是否正确
    if (!isNull && isMail) {
        $("#Checkemailaddress").attr("class", "Validform_checktip  Validform_right");
        $("#Checkemailaddress").html("验证通过");
    }
    else if (isNull) {
        $("#Checkemailaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkemailaddress").html("请输入邮箱地址");

    } else if (!isMail) {
        $("#Checkemailaddress").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkemailaddress").html("邮箱地址格式不正确");

    }
}
//验证qq
function Checkqq() {
    var qq = $.trim($("#qq").val());
    if (isQQ(qq)) {
        $("#Checkqq").attr("class", "Validform_checktip  Validform_right");
        $("#Checkqq").html("验证通过");

    } else {
        $("#Checkqq").attr("class", "Validform_checktip Validform_wrong");
        $("#Checkqq").html("纯数字组成，5-16位之间！");

    }
}

//角色弹出页面
function AddRoleDialog() {
    window.parent.ShouwDiaLogWan("添加角色", 800, 250, "/ROTE/AddRole");

}

function UpdateRoleDialog(objId) {
    window.parent.ShouwDiaLogWan("编辑角色", 800, 250, "/ROTE/RoleUpdate?rid=" + objId);
}

//设置权限弹出框
function UpdateRoleLimitDialog(objId) {
    window.parent.ShouwDiaLogWan("编辑角色权限", 800, 600, "/ROTE/UpdateRoleLimit?type=0&rid=" + objId);
}

function addrole() {
    var name = $("#inputName").val();
    var state = $('#radOrvActnS input[name="state"]:checked ').val();
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
        url: "/ROTE/AddRoleAjax",
        data: "name=" + name + "&state=" + state,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });

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
    var state = $('#radOrvActnS input[name="state"]:checked ').val();
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
        url: "/ROTE/RoleUpdateAjax",
        data: "name=" + name + "&state=" + state + "&rid=" + rid,
        success: function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });

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