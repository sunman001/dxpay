
//消息提示框
//content:提示内容
//info:提示类型,error:错误;info:信息;ok:成功
//callback:回调函数,格式：functiong（）{}
function ShowMsg(content, info, callback) {
    layer.msg(content, {
        icon: info == "error" ? 2 : info == "ok" ? 1 : 0,
        time: 1000,
        skin: 'layer-ext-moon',
    }, callback == "" ? "" : callback);
}

$(function () {

    //保存按钮事件
    $("#btn_ResetPwd").click(function () {

        rpwd();

    });


})

function rpwd() {
    var u_phone = $("#u_phone").val();

    if (u_phone == "") {
        ShowMsg("信息不完整，请重新操作！", "error", "");
        return false;
    }

    //新密码
    var xpass = $("#new_pwd").val();
    var isXpassNull = isNull(xpass), isXpass = isLenStrBetween(xpass, 6, 18);
    //确认密码
    var qpass = $("#qr_pwd").val();
    var isQpassNull = isNull(qpass), isQpass = isLenStrBetween(qpass, 6, 18);
    var isPwdXd = isEqual(xpass, qpass);


    if (isXpassNull) {

        ShowMsg("请输入新密码", "error", "");
        return false;
    }
    else if (isQpassNull) {

        ShowMsg("请输入确认密码", "error", "");
        return false;
    }
    else if (!isXpass) {

        ShowMsg("新密码长度为6至18个字符！", "error", "");
        return false;

    }
    else if (!isQpass) {

        ShowMsg("确认密码长度为6至18个字符！", "error", "");
        return false;

    }
    else if (!isPwdXd) {

        ShowMsg("两次输入的密码不一致！", "error", "");
        return false;

    }

    var url = "/User/ResetPwdAdd";
    var data = { u_phone: $.trim(u_phone), xpass: $.trim(xpass) };
    //防止重复提交
    $("#btn_ResetPwd").attr("disabled", "disabled");

    $.post(url, data, function (result) {

        if (result.success == 1) {
            ShowMsg(result.msg, "ok", function () {
                parent.window.location.href = "/Home/Login";
            });
        }
        else {

            ShowMsg(result.msg, "error", function () {
                parent.window.location.href = "/Home/Login";

            });
        }

    })

}

