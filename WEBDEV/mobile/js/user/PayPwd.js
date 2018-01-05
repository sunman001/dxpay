
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

                ShowMsg("请输入支付原密码！", "error", "");
                return false;
            }
            else if (!isUpass) {

                ShowMsg("支付原密码长度为6至18个字符！", "error", "");
                return false;
            }
          
        }


        //支付密码
        var pwd = $("#u_paypwd").val();
        var isUpassNull = isNull(pwd), isUpass = isLenStrBetween(pwd, 6, 18);

        if (isUpassNull) {

            ShowMsg("请输入支付密码!", "error", "");
            return false;
        }
        else if (!isUpass) {

            ShowMsg("支付密码长度为6至18个字符！", "error", "");
            return false;
        }
       

        //重复验证支付密码
        var qrpwd = $("#u_qrpaypwd").val();
        var isUpassNull = isNull(qrpwd), isUpass = isLenStrBetween(qrpwd, 6, 18);
        var isPwdXd = isEqual(qrpwd, pwd);

        if (isUpassNull) {

            ShowMsg("请输入支付密码!", "error", "");
            return false;
        }
        else if (!isUpass) {

            ShowMsg("支付密码长度为6至18个字符！", "error", "");
            return false;

        }
        else if (!isPwdXd) {

            ShowMsg("两次输入的支付密码不一致！", "error", "");
            return false;

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

