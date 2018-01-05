
$(function () {

    $("#btn_PayPwd").click(function () {

        //用户ID
        var uid = $("#u_id").val();
        //原密码
        var oldpwd = "";
        //用于判断是否验证原密码，为空不验证
        var ypwd = $("#ypwd").val();
        if (!isNull(ypwd)) {

            oldpwd = $("#old_paypwd").val();
            var isUpassNull = isNull(oldpwd), isUpass = isLenStrBetween(oldpwd, 6, 18);

            if (isUpassNull) {

                $("#yz_old_paypwd").attr("class", "error");
                $("#yz_old_paypwd").html("请输入支付原密码！");
                return false;
            }
            else if (!isUpass) {

                $("#yz_old_paypwd").attr("class", "error");
                $("#yz_old_paypwd").html("支付原密码长度为6至18个字符！");
                return false;

            }
            else {

                $("#yz_old_paypwd").attr("class", "error");
                $("#yz_old_paypwd").html("");
            }
        }


        //支付密码
        var pwd = $("#u_paypwd").val();
        var isUpassNull = isNull(pwd), isUpass = isLenStrBetween(pwd, 6, 18);

        if (isUpassNull) {

            $("#yz_u_paypwd").attr("class", "error");
            $("#yz_u_paypwd").html("请输入支付密码！");
            return false;
        }
        else if (!isUpass) {

            $("#yz_u_paypwd").attr("class", "error");
            $("#yz_u_paypwd").html("支付密码长度为6至18个字符！");
            return false;

        }
        else {

            $("#yz_u_paypwd").attr("class", "error");
            $("#yz_u_paypwd").html("");
        }

        //重复验证支付密码
        var qrpwd = $("#u_qrpaypwd").val();
        var isUpassNull = isNull(qrpwd), isUpass = isLenStrBetween(qrpwd, 6, 18);
        var isPwdXd = isEqual(qrpwd, pwd);

        if (isUpassNull) {

            $("#yz_u_qrpaypwd").attr("class", "error");
            $("#yz_u_qrpaypwd").html("请输入支付密码！");
            return false;
        }
        else if (!isUpass) {

            $("#yz_u_qrpaypwd").attr("class", "error");
            $("#yz_u_qrpaypwd").html("支付密码长度为6至18个字符！");
            return false;

        }
        else if (!isPwdXd) {

            $("#yz_u_qrpaypwd").attr("class", "error");
            $("#yz_u_qrpaypwd").html("两次输入的支付密码不一致！");
            return false;

        }
        else {

            $("#yz_u_qrpaypwd").attr("class", "error");
            $("#yz_u_qrpaypwd").html("");
        }

        var url = "/User/UpdateUserPayPwd";
        var data = { uid: $.trim(uid), pwd: $.trim(pwd), oldpwd: oldpwd };

        //防止重复提交
        $("#btn_PayPwd").attr("disabled", "disabled");
        $.post(url, data, function (result) {

            if (result.success == 1) {
                ShowMsg(result.msg, "ok", "");
            }
            else {

                ShowMsg(result.msg, "error", "");
            }
            //启用按钮
            $("#btn_PayPwd").removeAttr("disabled");
        })

    })

})

//验证原支付密码
function CheckOldpaypwd() {
    var pwd = $("#old_paypwd").val();
    var isUpassNull = isNull(pwd), isUpass = isLenStrBetween(pwd, 6, 18);

    if (isUpassNull) {

        $("#yz_old_paypwd").attr("class", "error");
        $("#yz_old_paypwd").html("请输入支付原密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_old_paypwd").attr("class", "error");
        $("#yz_old_paypwd").html("支付原密码长度为6至18个字符！");
        return false;

    }
    else {

        $("#yz_old_paypwd").attr("class", "error");
        $("#yz_old_paypwd").html("");
    }
}


//验证支付密码
function CheckPayPwd() {

    var pwd = $("#u_paypwd").val();
    var isUpassNull = isNull(pwd), isUpass = isLenStrBetween(pwd, 6, 18);

    if (isUpassNull) {

        $("#yz_u_paypwd").attr("class", "error");
        $("#yz_u_paypwd").html("请输入支付密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_u_paypwd").attr("class", "error");
        $("#yz_u_paypwd").html("支付密码长度为6至18个字符！");
        return false;

    }
    else {

        $("#yz_u_paypwd").attr("class", "error");
        $("#yz_u_paypwd").html("");
    }
}

//验证再次输入支付密码
function CheckQrPayPwd() {
    var pwd2 = $("#u_paypwd").val();
    var pwd = $("#u_qrpaypwd").val();
    var isUpassNull = isNull(pwd), isUpass = isLenStrBetween(pwd, 6, 18);
    var isPwdXd = isEqual(pwd, pwd2);

    if (isUpassNull) {

        $("#yz_u_qrpaypwd").attr("class", "error");
        $("#yz_u_qrpaypwd").html("请输入支付密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_u_qrpaypwd").attr("class", "error");
        $("#yz_u_qrpaypwd").html("支付密码长度为6至18个字符！");
        return false;

    }
    else if (!isPwdXd) {

        $("#yz_u_qrpaypwd").attr("class", "error");
        $("#yz_u_qrpaypwd").html("两次输入的支付密码不一致！");
        return false;

    }
    else {

        $("#yz_u_qrpaypwd").attr("class", "error");
        $("#yz_u_qrpaypwd").html("");
    }
}