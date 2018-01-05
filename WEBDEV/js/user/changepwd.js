
var verify = {
    counter: 180,
    verifiedSuccess: true
};

var interval = null;

$(function () {

    $("#btn_ResetPwd").click(function () {

        var phoneNumber = $("#u_phone").val();
        var _isMobile = isMobile(phoneNumber);
        var code = $("#verified_code").val();

        if (!_isMobile) {
            $("#yzphone").attr("class", "lgn-error on");
            $("#yzphone").html("手机号码格式不正确！");
            return false;

        }
        if (code == "") {
            $("#yztelcode").attr("class", "lgn-error on");
            $("#yztelcode").html("请输入验证码");
            return false;
        }
        else {
            var url = "/User/clickChangePwd";
            var data = { code: $.trim(code), phone: $.trim(phoneNumber) };

            $.post(url, data, function (result) {

                if (result.success) {
                    verify.verifiedSuccess = true;
                    window.location.href = encodeURI("/user/ResetPwd?id=" + result.url);
                }
                else {

                    ShowMsg(result.message, "error", "");
                }
            });
        }

    });

    //获取重置密码的验证码
    $("#btn_get_verify_code").click(function () {

        var target = $(this);
        
        var phone = $("#u_phone").val();
        if (phone.length <= 0) {
            // ShowMsg('请输入手机号码', "error", "");
            $("#yzphone").attr("class", "lgn-error on");
            $("#yzphone").html("请输入手机号码！");
            return false;
        }
        else if (!isMobile(phone)) {
            //ShowMsg("手机号码格式不正确", "error", "");
            $("#yzphone").attr("class", "lgn-error on");
            $("#yzphone").html("手机号码格式不正确！");
            return false;
        }
        
        target.hide();
        verify.verifiedSuccess = false;
        $.ajax({
            url: '/user/GetVerifyCodePwd',
            type: 'POST',
            data: { u_phone: phone },
            success: function (response) {
                if (response.success) {
                    wait(target);
                    ShowMsg(response.message, "ok", "");
                  
                } else {
                    target.show();
                    //alert(response.message);
                    ShowMsg(response.message, "error", "");
                }
            }
        });
    });


})

//验证手机号码
function yzphone() {

    var phoneNumber = $("#u_phone").val();
    var _isMobile = isMobile(phoneNumber);
    if (phoneNumber.length <= 0) {
        $("#yzphone").attr("class", "lgn-error on");
        $("#yzphone").html("请输入手机号码！");

        return false;
    }
    else if (!_isMobile) {
        $("#yzphone").attr("class", "lgn-error on");
        $("#yzphone").html("手机号码格式不正确！");

        return false;
    }
    else {
        $("#yzphone").attr("class", "lgn-error");
        $("#yzphone").html("");
    }
}

//验证码
function yzcode() {

    var phoneNumber = $("#u_phone").val();
    var code = $("#verified_code").val();
    if ((code == "")) {
        $("#yztelcode").attr("class", "lgn-error on");
        $("#yztelcode").html("请输入验证码！");
        return false;
    }
    else {
        $("#yztelcode").attr("class", "lgn-error");
        $("#yztelcode").html("");
    }
}

function wait(target) {
    $("#btn_get_verify_code_disabled").show();
    target.hide();
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

