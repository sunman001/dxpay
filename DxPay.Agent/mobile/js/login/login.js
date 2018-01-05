

$(function () {
    //监听键盘按下事件
    $("body").keydown(function (e) {
        var curKey = e.which;
        if (curKey == 13) {
            $("#btn_sub").click();
            return false;
        }
    });
    $("#btn_sub").click(function () {
        //验证登录名
        var loginName = $("#loginName").val();
        var isloginName = isNull(loginName);
        if (isloginName) {
            ShowMsg("请输入登录名", "error", "");
            $("#loginName").focus()
            return false;
        }
        else {           
        }
        //验证密码
        var password = $("#password").val();
        var ispassword = isNull(password);
        var isUpass = isLenStrBetween(password, 6, 18)
        if (ispassword || !isUpass) {
            if (ispassword) {
                ShowMsg("请输入密码！", "error", "");
                $("#password").focus()
                return false;
            } else if (!isUpass) {
                ShowMsg("密码长度为6至18个字符！", "error", "");
                $("#password").focus()
                return false;
            } else {
                ShowMsg("请输入密码！", "error", "");
                $("#password").focus()
                return false;
            }

        } else {
           

        }
        //验证验证码
        var code = $("#code").val();
        var yzcode = $("#verify").val();
        if ((code == "")) {
            ShowMsg("请输入验证码！", "error", "");
            $("#yzcode").focus()
            return false;
        }
        else {
           
        }
        var url = "/Login/UserLogin";
        var data = { userName: loginName, userPwd: password, valCode: code };
        $.post(url, data, function (result) {
            //判断是否登录、报错、有权限
            if (result.success == 1) {
                window.location.href = encodeURI("/Home/Default");
            }
            else {
                ShowMsg(result.msg, "error", "");
                $("#btn_sub").removeClass("on");
            }
        })
    })

})
