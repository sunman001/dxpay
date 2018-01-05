//JavaScript Document

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
    //监听键盘按下事件
    $("body").keydown(function (e) {
        var curKey = e.which;
        if (curKey == 13) {
            $("#btn_sub").click();
            return false;
        }
    });
    //注册保存
    $("#btn_sub").click(function () {
        //指定商务
        var relation_person_id = $("#relation_person_id").val();     
        if (relation_person_id<0)
        {
            ShowMsg("请选择商务", "error", "");
            return false
        }
        else
        {
          
        }
        //验证通过
        //验证邮箱
        var uname = $("#u_id").val();
        var isUnameNull = isNull(uname);
        var isUname = isEmail(uname);
        if (isUnameNull || !isUname) {
            if (isUnameNull) {
                ShowMsg("请输入邮箱", "error", "");
                return false;
            } else if (!isUname) {
                ShowMsg("邮箱格式错误", "error", "");
                return false;
            } else {
                ShowMsg("请输入邮箱", "error", "");
                return false;
            }
        }
        else {
            $.ajax({
                url: "/User/CheckEmail?u_email=" + uname,
                type: "post",
                cache: false,
                async: false,
                success: function (result) {
                    //判断是否登录、报错、有权限
                    CheckJsonData(result);
                    if (result.success == 1) {
                        ShowMsg(result.msg, "error", "");
                    } else {
                       
                    }
                }
            });
        }
        //验证密码
        var upass = $("#u_pass").val();
        var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
        if (isUpassNull || !isUpass) {
            if (isUpassNull) {
                ShowMsg("请输入密码", "error", "");
                return false;
            } else if (!isUpass) {
                ShowMsg("密码长度为6至18个字符！", "error", "");
                return false;

            } else {
                ShowMsg("请输入密码！", "error", "");
                return false;
            }

        } else {
          
        }
        var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
        var reupass = $("#u_repass").val();
        var isRepassNull = isNull(reupass), isRepass = isLenStrBetween(reupass, 6, 18), isEquals = isEqual(reupass, upass);
        if (isUpassNull || !isUpass || !isEquals) {
            if (isUpassNull) {
                ShowMsg("请输入确认密码！", "error", "");
                return false;
            } else if (!isUpass) {
                ShowMsg("密码长度为6至18个字符！", "error", "");
                return false;

            } else if (!isEquals) {
                ShowMsg("两次输入的密码不一致！", "error", "");
                return false;

            } else {
                ShowMsg("请输入确认密码！", "error", "");
                return false;
            }
        } else {
           
        }
        var phoneNumber = $("#u_phone").val();
        var _isMobile = isMobile(phoneNumber);
        if (phoneNumber.length <= 0) {
            ShowMsg("请输入手机号码！", "error", "");
            return false;
        }
        else if (!_isMobile) {
            ShowMsg("请输入手机号码！", "error", "");
            return false;
          
        } else {
           
        }
        var code = $("#code").val();
        if ((code == "")) {
            ShowMsg("请输入验证码！", "error", "");
            return false;
        }
        else {
          
        }

        //提示信息
        //var verifiedCode = $("#verified_code").val();
        //if (verifiedCode.length <= 0) {
        //    $("#yztelcode").attr("class", "lgn-error on");
        //    $("#yztelcode").html("请输入手机验证码！");
        //    return false;
        //}



        var u_category = $("#u_category").val();

        $.ajax({
            url: "/User/UserReg?pv_code=" + $("#verified_code").val(),
            type: "post",
            data: { u_email: $.trim(uname), u_password: $.trim(upass), u_phone: $.trim(phoneNumber), relation_person_id: $.trim(relation_person_id), u_category: $.trim(u_category) },
            cache: false,
            async: false,
            success: function (result) {
                //判断是否登录、报错、有权限
                CheckJsonData(result);
                if (result.success === 1 || result.success) {
                    //注册后需要激活
                    //window.top.location.href = "/User/RegSuccess?u_mail=" + result.uname;
                    window.location.href = encodeURI("/Home/Default");
                    // window.top.location.href = "/User/DevVerifyNew?uname=" + result.uname;
                } else {
                    window.parent.ShowMsg(result.msg, "error", "");
                }
            }
        });
    });

   

    //激活
    $("#btn_active").click(function () {
        var uEmail = $("#u_email")
        $.ajax({
            url: "/User/MemberActive?u_email=" + uEmail,
            type: "post",
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 1) {
                    $(".jihuo").html("请到您的邮箱中点击激活链接来激活您的帐号！");
                }
            }
        });
    });

   

    var interval = null;
    //var counter = 60;
    //var verifiedSuccess = false;

    var verify = {
        counter: 180,
        verifiedSuccess: true
    };

    //获取注册的验证码
    $("#btn_get_verify_code").click(function () {
        // alert('123');
        var target = $(this);
        var phone = $("#u_phone").val();
        var code = $("#code").val();
        if (phone.length <= 0) {
            ShowMsg('请输入手机号码', "error", "");
            return false;
        }
        if (code.length <= 0) {
            ShowMsg('请输入图片验证码', "error", ""); //alert("请输入手机号码");
            return false;
        }
        else if (!isMobile(phone)) {
            ShowMsg("手机号码格式不正确", "error", "");
            return false;
        }
        target.hide();
        $.ajax({
            url: '/user/getverifycode',
            type: 'POST',
            data: { u_phone: phone, vc: code },
            success: function (response) {

                if (response.success) {
                    wait(target);
                    ShowMsg(response.message,"","")

                } else {
                    target.show();
                    //alert(response.message);
                    ShowMsg(response.message, "error", "");
                }
            }
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

    $("#verified_code").blur(function () {
        if (verify.verifiedSuccess) {
            return false;
        }
        var phone = $("#u_phone").val();
        if (phone.length <= 0) {
            return false;
        }
        if (!isMobile(phone)) {
            return false;
        }
        var code = $("#verified_code").val();
        if (code.length !== 6) {
            return false;
        }
        $.ajax({
            url: '/user/verifycode',
            type: 'POST',
            data: { u_phone: phone, code: code },
            success: function (response) {
                if (response.success) {
                    verify.verifiedSuccess = true;
                    $(".btn-code-show").hide();
                }
                ShowMsg(response.message, "error", "");
            }
        });
    });

});



