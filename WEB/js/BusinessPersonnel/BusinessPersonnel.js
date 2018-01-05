
$(document).ready(function () {

    //添加或修改商务信息
    $("#btnBusinessPersonnel").click(function () {

        var yzmg = "";
        var Id = $("#id").val();
        //登录名称
        var loname = $("#LoginName").val();
        var yexp = /^[0-9a-zA-Z]*$/g
        if (isRequestNotNull(loname)) {

            $("#y_LoginName").attr("class", "Validform_checktip Validform_wrong");
            $("#y_LoginName").html("请填写登录名称!");
            return false;

        } else if (yexp.test(loname)) {
            var data = { lname: loname, uid: Id };
            $.ajax({
                type: "post",
                url: "/BusinessPersonnel/CheckLoName",
                cache: false,
                async: false,
                dataType: "json",
                data: data,
                success: function (msg) {
                    if (msg.success) {
                        $("#y_LoginName").attr("class", "Validform_checktip Validform_wrong");
                        $("#y_LoginName").html("已存在该登录名称！")
                        yzmg += "已存在该登录名称";
                    } else {
                        $("#y_LoginName").attr("class", "Validform_checktip Validform_right");
                        $("#y_LoginName").html("验证通过！");
                    }
                }
            });
        }
        else
        {
            $("#y_LoginName").attr("class", "Validform_checktip Validform_checktip Validform_wrong");
            $("#y_LoginName").html("登录名只能输入拼音和数字");
        }
        //验证密码
        var pwd = $("#Passwords").val();
        if (pwd.length < 6) {

            $("#y_Passwords").attr("class", "Validform_checktip Validform_wrong");
            $("#y_Passwords").html("密码不能小于6位！");
            return false;

        } else {

            $("#y_Passwords").attr("class", "Validform_checktip  Validform_right");
            $("#y_Passwords").html("验证通过");
        }
        //验证姓名
        var name = $("#DisplayName").val();
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

        if (!exp1.test(name)) {

            $("#y_DisplayName").attr("class", "Validform_checktip Validform_wrong");
            $("#y_DisplayName").html("姓名由1-6位汉字组成！");
            return false;
        }
        else {
            $("#y_DisplayName").attr("class", "Validform_checktip  Validform_right");
            $("#y_DisplayName").html("验证通过");
        }

        //验证邮箱
        var email = $("#EmailAddress").val();
        var isNull = isRequestNotNull(email);//是否为空
        var isMail = isEmail(email);//格式是否正确
        if (isNull) {

            $("#y_EmailAddress").attr("class", "Validform_checktip Validform_wrong");
            $("#y_EmailAddress").html("请输入邮箱地址！");
            return false;

        } else if (!isMail) {

            $("#y_EmailAddress").attr("class", "Validform_checktip Validform_wrong");
            $("#y_EmailAddress").html("邮箱地址格式不正确！");
            return false;
        }
        else {
            $("#y_EmailAddress").attr("class", "Validform_checktip  Validform_right");
            $("#y_EmailAddress").html("验证通过");
        }

        //验证电话
        var phone = $("#MobilePhone").val();

        if (isMobileOrPhone(phone)) {

            $("#y_MobilePhone").attr("class", "Validform_checktip  Validform_right");
            $("#y_MobilePhone").html("验证通过");
        }
        else {
            $("#y_MobilePhone").attr("class", "Validform_checktip Validform_wrong");
            $("#y_MobilePhone").html("11位手机号或固定电话(号码或区号-号码)！");
            return false;
        }

        //验证QQ
        var qq = $("#QQ").val();
        if (isQQ(qq)) {

            $("#y_QQ").attr("class", "Validform_checktip  Validform_right");
            $("#y_QQ").html("验证通过");

        } else {

            $("#y_QQ").attr("class", "Validform_checktip Validform_wrong");
            $("#y_QQ").html("纯数字组成，5-16位之间！");
            return false;
        }

        var Website = $("#Website").val();
        var selrole_id = $("#selrole_id").val();
        $("#btnBusinessPersonnel").attr("disabled", "disabled");
        var data = { LoginName: $.trim(loname), Password: $.trim(pwd), DisplayName: $.trim(name), EmailAddress: $.trim(email), MobilePhone: $.trim(phone), qq: $.trim(qq), Website: $.trim(Website), Id: $.trim(Id), RoleId: selrole_id };
        var url = "/BusinessPersonnel/InsertOrUpdateBusinessPersonnel";
        if(yzmg=="")
        {
            $.post(url, data, function (retJson) {
                $("#btnBusinessPersonnel").attr("disabled", false);
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
            $("#btnBusinessPersonnel").removeAttr("disabled");          

        }
        else
        {
            $("#btnBusinessPersonnel").removeAttr("disabled");
            return false;
        }
       
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
    $.post("/BusinessPersonnel/CoAll", { coid: vals, tag: obj }, function (result) {
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


//分页
function ArticleManage(pageIndex, pageSize) {

    var url = "/BusinessPersonnel/BusinessPersonnelList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var s_type = $("#s_type").val();
    var s_state = $("#s_state").val();
    var s_keys = $("#s_keys").val();

    url += "&s_type=" + s_type + "&s_keys=" + s_keys + "&s_state=" + s_state + "";

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function selectBusinessPersonnel() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//添加商务人员弹窗
function Addsw() {
    window.parent.ShouwDiaLogWan("添加商务信息", 950, 600, "/BusinessPersonnel/BusinessPersonnelAdd");
}

//修改商务弹窗
function UpdateSW(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择要修改的商务信息！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("修改商务信息", 950, 600, "/BusinessPersonnel/BusinessPersonnelAdd?id=" + id);
}

//验证登录名称
function CheckLoginName() {
    var loname = $("#LoginName").val();
    debugger
    var yexp = /^[0-9a-zA-Z]*$/g
    if (isRequestNotNull(loname)) {
        $("#y_LoginName").attr("class", "Validform_checktip Validform_wrong");
        $("#y_LoginName").html("请填写登录名称!");
        return false;
    } else if (yexp.test(loname))
       {
        var data = { lname: loname, uid: $("#id").val() };
        $.post("/BusinessPersonnel/CheckLoName", data, function (msg) {
            if (msg.success) {
                $("#y_LoginName").attr("class", "Validform_checktip Validform_wrong");
                $("#y_LoginName").html("已存在该登录名称！");
                return false;

            } else {
                $("#y_LoginName").attr("class", "Validform_checktip Validform_right");
                $("#y_LoginName").html("验证通过！");
            }
        });
    } else {
        $("#y_LoginName").attr("class", "Validform_checktip Validform_checktip Validform_wrong");
        $("#y_LoginName").html("登录名只能输入拼音和数字");
    }

}

//验证密码
function CheckPasswords() {
    var pwd = $("#Passwords").val();
    if (pwd.length < 6) {

        $("#y_Passwords").attr("class", "Validform_checktip Validform_wrong");
        $("#y_Passwords").html("密码不能小于6位！");
        return false;

    } else {

        $("#y_Passwords").attr("class", "Validform_checktip  Validform_right");
        $("#y_Passwords").html("验证通过");
    }
}

//验证姓名
function CheckDisplayName() {
    var name = $("#DisplayName").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

    if (!exp1.test(name)) {

        $("#y_DisplayName").attr("class", "Validform_checktip Validform_wrong");
        $("#y_DisplayName").html("姓名由1-6位汉字组成！");
        return false;
    }
    else {
        $("#y_DisplayName").attr("class", "Validform_checktip  Validform_right");
        $("#y_DisplayName").html("验证通过");
    }

}

//验证邮箱
function CheckEmailAddress() {
    var email = $("#EmailAddress").val();
    var isNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (isNull) {

        $("#y_EmailAddress").attr("class", "Validform_checktip Validform_wrong");
        $("#y_EmailAddress").html("请输入邮箱地址！");
        return false;

    } else if (!isMail) {

        $("#y_EmailAddress").attr("class", "Validform_checktip Validform_wrong");
        $("#y_EmailAddress").html("邮箱地址格式不正确！");
        return false;
    }
    else {
        $("#y_EmailAddress").attr("class", "Validform_checktip  Validform_right");
        $("#y_EmailAddress").html("验证通过");
    }
}


//验证联系电话
function CheckMobilePhone() {
    var phone = $("#MobilePhone").val();

    if (isMobileOrPhone(phone)) {

        $("#y_MobilePhone").attr("class", "Validform_checktip  Validform_right");
        $("#y_MobilePhone").html("验证通过");
    }
    else {
        $("#y_MobilePhone").attr("class", "Validform_checktip Validform_wrong");
        $("#y_MobilePhone").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }
}

//验证qq号码
function CheckQQ() {
    var qq = $("#QQ").val();
    if (isQQ(qq)) {

        $("#y_QQ").attr("class", "Validform_checktip  Validform_right");
        $("#y_QQ").html("验证通过");

    } else {

        $("#y_QQ").attr("class", "Validform_checktip Validform_wrong");
        $("#y_QQ").html("纯数字组成，5-16位之间！");
        return false;
    }
}

