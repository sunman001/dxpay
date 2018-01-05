//JavaScript Document

$(function () {

    //保存按钮事件
    $("#btn_save").click(function () {

        UpdateData();
    });
});


//验证原密码
function CheckOldPwd() {
    var upass = $("#old_pwd").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);


    if (isUpassNull) {

        $("#yz_old_pwd").attr("class", "error");
        $("#yz_old_pwd").html("请输入原密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_old_pwd").attr("class", "error");
        $("#yz_old_pwd").html("原密码长度为6至18个字符！");
        return false;

    } else {

        $("#yz_old_pwd").attr("class", "error");
        $("#yz_old_pwd").html("");
    }

}

//验证新密码
function CheckNewPwd() {

    //新密码
    var upass = $("#new_pwd").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);

    if (isUpassNull) {

        $("#yz_new_pwd").attr("class", "error");
        $("#yz_new_pwd").html("请输入新密码！");
        return false;

    } else if (!isUpass) {

        $("#yz_new_pwd").attr("class", "error");
        $("#yz_new_pwd").html("新密码长度为6至18个字符！");
        return false;

    } else {

        $("#yz_new_pwd").attr("class", "error");
        $("#yz_new_pwd").html("");


    }

}

//验证确认密码
function CheckQrPwd() {

    //确认密码
    var upass = $("#qr_pwd").val();
    var new_pass = $("#new_pwd").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    var isPwdXd = isEqual(new_pass, upass);


    if (isUpassNull) {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("请输入确认密码！");
        return false;

    } else if (!isUpass) {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("确认密码长度为6至18个字符！");
        return false;

    } else if (!isPwdXd) {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("两次输入的密码不一致！");
        return false;

    } else {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("");
    }

}

//保存修改信息
function UpdateData() {

    var u_id = $("#u_id").val();
    //原密码
    var upass = $("#old_pwd").val();
    var isOldpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    //新密码
    var xpass = $("#new_pwd").val();
    var isXpassNull = isNull(xpass), isXpass = isLenStrBetween(xpass, 6, 18);
    //确认密码
    var qpass = $("#qr_pwd").val();
    var isQpassNull = isNull(qpass), isQpass = isLenStrBetween(qpass, 6, 18);
    var isPwdXd = isEqual(xpass, qpass);

    //验证原密码    

    if (isOldpassNull) {

        $("#yz_old_pwd").attr("class", "error");
        $("#yz_old_pwd").html("请输入原密码！");
        return false;
    }
    else if (isXpassNull) {

        $("#yz_new_pwd").attr("class", "error");
        $("#yz_new_pwd").html("请输入新密码！");
        return false;
    }
    else if (isQpassNull) {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("请输入确认密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_old_pwd").attr("class", "error");
        $("#yz_old_pwd").html("原密码长度为6至18个字符！");
        return false;
    }
    else if (!isXpass) {

        $("#yz_new_pwd").attr("class", "error");
        $("#yz_new_pwd").html("新密码长度为6至18个字符！");
        return false;

    }
    else if (!isQpass) {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("确认密码长度为6至18个字符！");
        return false;

    }
    else if (!isPwdXd) {

        $("#yz_qr_pwd").attr("class", "error");
        $("#yz_qr_pwd").html("两次输入的密码不一致！");
        return false;

    }
    else {

        $("#yz_old_pwd").attr("class", "error");
        $("#yz_old_pwd").html("");
    }


    var url = "/User/UpdateUserInfo";
    var data = { u_id: $.trim(u_id), upass: $.trim(upass), xpass: $.trim(xpass) };
    //防止重复提交
    $("#btn_save").attr("disabled", "disabled");

    $.post(url, data, function (result) {

        if (result.success == 1) {
            ShowMsg(result.msg, "ok", function () {
                parent.window.location.href = "/Login/Index";
            });
        } else if (result.success == 2) {

            ShowMsg(result.msg, "error", "");
            //启用按钮
            $("#btn_save").removeAttr("disabled");

        } else {

            ShowMsg(result.msg, "error", "");
            //启用按钮
            $("#btn_save").removeAttr("disabled");
        }

    })
}