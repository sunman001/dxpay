//JavaScript Document

$(function () {

    //保存按钮事件
    $("#btn_update_pwd").click(function () {
        //防止重复提交
        $("#btn_update_pwd").attr("disabled", "disabled");
        UpdateData();
    });
});

//验证新密码
function CheckNewPwd() {
    //新密码
    var upass = $("#new_pwd").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    if (!isUpassNull && isUpass) {
        objToolTip("new_pwd", 3, "");
    } else {
        if (isUpassNull) {
            objToolTip("new_pwd", 4, "请输入密码！");
        } else if (!isUpass) {
            objToolTip("new_pwd", 4, "密码长度为6至18个字符！");
        } else {
            objToolTip("new_pwd", 3, "");
        }
    }
}

//验证确认密码
function CheckQrPwd() {
    //确认密码
    var upass = $("#qr_pwd").val();
    var new_pass = $("#new_pwd").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    var isPwdXd = isEqual(new_pass, upass);
    if (!isUpassNull && isUpass && isPwdXd) {
        objToolTip("qr_pwd", 3, "");
    } else {
        if (isUpassNull) {
            objToolTip("qr_pwd", 4, "请输入确认密码！");
        } else if (!isUpass) {
            objToolTip("qr_pwd", 4, "确认密码长度为6至18个字符！");
        } else if (!isPwdXd) {
            objToolTip("qr_pwd", 4, "两次输入的密码不一致！");
        } else {
            objToolTip("qr_pwd", 3, "");
        }
    }
}

//保存修改信息
function UpdateData() {
    //取出表单数据
    var tmsg = "";

    //验证密码
    var xpass = $("#new_pwd").val();
    var isXpassNull = isNull(xpass), isXpass = isLenStrBetween(xpass, 6, 18);
    if (!isXpassNull && isXpass) {
        objToolTip("new_pwd", 3, "");
    } else {
        if (isXpassNull) {
            objToolTip("new_pwd", 4, "请输入密码！");
            tmsg += "请输入密码！";
        } else if (!isXpass) {
            objToolTip("new_pwd", 4, "密码长度为6至18个字符！");
            tmsg += "密码长度为6至18个字符！";
        } else {
            objToolTip("new_pwd", 3, "");
        }
    }

    //验证确认密码 
    var qpass = $("#qr_pwd").val();
    var isQpassNull = isNull(qpass), isQpass = isLenStrBetween(qpass, 6, 18);
    var isPwdXd = isEqual(xpass, qpass);
    if (!isQpassNull && isQpass && isPwdXd) {
        objToolTip("qr_pwd", 3, "");
    } else {
        if (isQpassNull) {
            objToolTip("qr_pwd", 4, "请输入确认密码！");
            tmsg += "请输入确认密码！";
        } else if (!isQpass) {
            objToolTip("qr_pwd", 4, "确认密码长度为6至18个字符！");
            tmsg += "确认密码长度为6至18个字符！";
        } else if (!isPwdXd) {
            objToolTip("qr_pwd", 4, "两次输入的密码不一致！");
            tmsg += "两次输入的密码不一致！";
        } else {
            objToolTip("qr_pwd", 3, "");
        }
    }

    if (isNull(tmsg)) {
        var u_phone = $("#u_phone").val();
        var code = $("#u_code").val();
        $.ajax({
            url: "/user/changepwd",
            type: "post",
            data: { phone: u_phone, code: code, pwd: xpass },
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success) {
                    ShowMsg(result.message, "ok", function () {
                        window.top.location.href = "/Home/Login";
                    });
                } else {
                    ShowMsg(result.message, "error", "");
                    //启用按钮
                    $("#btn_update_pwd").removeAttr("disabled");
                }
            }
        });
    } else {
        ShowMsg("有输入项未正确输入，请确认！", "error", "");
        //启用按钮
        $("#btn_update_pwd").removeAttr("disabled");
    }
}