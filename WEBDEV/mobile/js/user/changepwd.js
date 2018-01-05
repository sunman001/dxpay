
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

var verify = {
    counter: 180,
    verifiedSuccess: true
};


$(function () {

    $("#btn_ResetPwd").click(function () {

        var phoneNumber = $("#u_phone").val();
        var _isMobile = isMobile(phoneNumber);
        var code = $("#verified_code").val();

        if (phoneNumber.length <= 0) {

            ShowMsg("请输入手机号码", "error", "");
            return false;

        }
        else if (!_isMobile) {

            ShowMsg("手机号码格式不正确", "error", "");
            return false;

        }
        if ((code == "")) {

            ShowMsg("请输入验证码", "error", "");
            return false;
        }

        var url = "/User/ChangePwd";
        var data = { code: $.trim(code), phone: $.trim(phoneNumber) };

        $.post(url, data, function (result) {

            if (result.success == 3) {

                window.location.href = encodeURI("/user/ResetPwd?id=" + result.url);
            }
            else {

                ShowMsg(result.msg, "error", "");
            }
        });

    });

    //获取重置密码的验证码
    $("#btn_get_verify_code").click(function () {

        var target = $(this);
        var phone = $("#u_phone").val();
        if (phone.length <= 0) {

            ShowMsg('请输入手机号码', "error", "");
            return false;
        }
        else if (!isMobile(phone)) {

            ShowMsg("手机号码格式不正确", "error", "");
            return false;
        }
        target.hide();
        $.ajax({
            url: '/user/GetVerifyCodePwd',
            type: 'POST',
            data: { u_phone: phone },
            success: function (response) {
                if (response.success) {
                    wait(target);

                } else {
                    target.show();
                    //alert(response.message);
                    ShowMsg(response.message, "error", "");
                }
            }
        });
    });


});


function wait(target) {
    $("#btn_get_verify_code_disabled").show();
    verify.counter = 60;
    interval = setInterval(function countdown() {
        verify.counter--;
        if (verify.counter <= 0) {
            clearInterval(interval);
            verify.counter = 60;
            if (!verify.verifiedSuccess) {
                target.show();
                $("#btn_get_verify_code_disabled  span").text('60');
                $("#btn_get_verify_code_disabled").hide();
            }
        }
        $("#btn_get_verify_code_disabled span").text(verify.counter);
    }, 1000);
}

function ShowMsg(content, info, callback) {
    layer.msg(content, {
        icon: info == "error" ? 2 : info == "ok" ? 1 : 0,
        time: 1000,
        skin: 'layer-ext-moon',
    }, callback == "" ? "" : callback);
}

