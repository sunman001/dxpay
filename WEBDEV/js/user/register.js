//JavaScript Document
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
            $("#yzbp").attr("class", "lgn-error on");
            $("#yzbp").html("请选择商务");
            return false
        }
        else
        {
            $("#yzbp").attr("class", "lgn-error ");
            $("#yzbp").html("");
        }
        //验证通过
        //验证邮箱
        var uname = $("#u_id").val();
        var isUnameNull = isNull(uname);
        var isUname = isEmail(uname);
        if (isUnameNull || !isUname) {
            if (isUnameNull) {
                $("#yzemial").attr("class", "lgn-error on");
                $("#yzemial").html("请输入邮箱");
                return false;
            } else if (!isUname) {
                $("#yzemial").attr("class", "lgn-error on");
                $("#yzemial").html("邮箱格式错误");
                return false;
            } else {
                $("#yzemial").attr("class", "lgn-error on");
                $("#yzemial").html("请输入邮箱");
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
                        $("#yzemial").attr("class", "lgn-error on");
                        $("#yzemial").html(result.msg);
                    } else {
                        $("#yzemial").attr("class", "lgn-error ");
                        $("#yzemial").html("");
                    }
                }
            });
        }
        //验证密码
        var upass = $("#u_pass").val();
        var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
        if (isUpassNull || !isUpass) {
            if (isUpassNull) {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("请输入密码");
            } else if (!isUpass) {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("密码长度为6至18个字符！");

            } else {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("请输入密码！");
            }

        } else {
            $("#yzma").attr("class", "lgn-error ");
            $("#yzma").html("");
        }
        var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
        var reupass = $("#u_repass").val();
        var isRepassNull = isNull(reupass), isRepass = isLenStrBetween(reupass, 6, 18), isEquals = isEqual(reupass, upass);
        if (isUpassNull || !isUpass || !isEquals) {
            if (isUpassNull) {
                $("#yzqrma").attr("class", "lgn-error on");
                $("#yzqrma").html("请输入确认密码！");
            } else if (!isUpass) {
                $("#yzqrma").attr("class", "lgn-error on");
                $("#yzqrma").html("密码长度为6至18个字符！");

            } else if (!isEquals) {
                $("#yzqrma").attr("class", "lgn-error on");
                $("#yzqrma").html("两次输入的密码不一致");

            } else {
                $("#yzqrma").attr("class", "lgn-error on");
                $("#yzqrma").html("请输入确认密码")
            }
        } else {
            $("#yzqrma").attr("class", "lgn-error ");
            $("#yzqrma").html("")
        }
        var phoneNumber = $("#u_phone").val();
        var _isMobile = isMobile(phoneNumber);
        if (phoneNumber.length <= 0) {
            $("#yzphone").attr("class", "lgn-error on");
            $("#yzphone").html("请输入手机号码！");
        }
        else if (!_isMobile) {
            $("#yzphone").attr("class", "lgn-error on");
            $("#yzphone").html("手机号码格式不正确！");
        } else {
            $("#yzphone").attr("class", "lgn-error");
            $("#yzphone").html("");
            /*
            $.ajax({
                url: "/User/CheckPhone?phone=" + phoneNumber,
                type: "post",
                cache: false,
                async: false,
                success: function (result) {
                    //判断是否登录、报错、有权限
                    CheckJsonData(result);
                    if (result.success == 1) {
                        $("#yzphone").attr("class", "lgn-error on");
                        $("#yzphone").html(result.msg);
                    } else {
                        $("#yzphone").attr("class", "lgn-error on");
                        $("#yzphone").html("");
                    }
                }
            });
            */
        }
        var code = $("#code").val();
        if ((code == "")) {
            $("#yzcode").attr("class", "lgn-error lgn-ervlt on");
            $("#yzcode").html("请输入验证码");
            $("#yzcode").focus()
            return false;
        }
        else {
            $("#yzcode").attr("class", "lgn-error lgn-ervlt");
            $("#yzcode").html("");
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

    ////登录
    //$("#btn_login").click(function () {
    //    window.top.location.href = "/Home/Login";
    //});

    ////注册
    //$("#btn_reg").click(function () {
    //    window.top.location.href = "/User/Register";
    //});

    ////已有账号，登录
    //$("#btn_has_login").click(function () {
    //    window.top.location.href = "/Home/Login";
    //});

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

    //$("#btn_update_pwd").click(function () {
    //    $.ajax({
    //        url: "/user/getverifycodepwd",
    //        type: "post",
    //        data: { u_phone: u_phone },
    //        async: false,
    //        success: function (result) {
    //            if (result.success) {
    //                //window.location.href = "/home/login";
    //                ShowMsg(result.message, "success", "");
    //            } else {
    //                ShowMsg(result.message, "error", "");
    //            }
    //        }
    //    });
    //});

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
                    verify.verifiedSuccess = false;
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

    //获取重置密码的验证码
    //$("#btn_get_pwd_verify_code").click(function () {
    //    var target = $(this);
    //    var u_phone = $("#u_phone").val();
    //    var isUnameNull = isNull(u_phone), isUname = isMobile(u_phone);
    //    if (isUnameNull || !isUname) {
    //        if (isUnameNull) {
    //            msg = "请输入账号！";
    //        } else if (!isUname) {
    //            msg = "手机号码格式错误！";
    //        } else {
    //            msg = "请输入账号！";
    //        }
    //        objToolTip("u_phone", 4, msg);
    //    } else {
    //        target.hide();
    //        wait(target);
    //        $.ajax({
    //            url: "/User/getverifycodepwd?phone=" + u_phone,
    //            type: "post",
    //            cache: false,
    //            async: false,
    //            data: { u_phone: u_phone },
    //            success: function (result) {
    //                //判断是否登录、报错、有权限
    //                CheckJsonData(result);
    //                if (result.success) {
    //                    ShowMsg(result.message, "success", "");
    //                    $(".layer-for-phone").hide();
    //                    $(".layer-for-code").show();
    //                } else {
    //                    objToolTip("u_phone", 4, result.message);
    //                    clearInterval(interval);
    //                    verify.counter = 60;
    //                    target.show();
    //                    $(".btn-code-disabled span").text('60');
    //                    $(".btn-code-disabled").hide();
    //                }
    //            }
    //        });
    //    }
    //});

    //验证重置密码的验证码
    //$("#btn_check_pwd_verify_code").click(function () {
    //    var pwd_verify_code = $("#pwd_verify_code").val();
    //    var phone = $("#u_phone").val();
    //    if (pwd_verify_code.length !== 6) {
    //        msg = "请输入账号！";
    //        objToolTip("u_phone", 4, msg);
    //    } else {
    //        $.ajax({
    //            url: "/User/checkverifycodepwd?phone=" + u_phone,
    //            type: "post",
    //            cache: false,
    //            async: false,
    //            data: { u_phone: phone, code: pwd_verify_code },
    //            success: function (result) {
    //                //判断是否登录、报错、有权限
    //                CheckJsonData(result);
    //                if (result.success) {
    //                    //objToolTip("u_phone", 3, result.message);
    //                    //status = false;
    //                    //verify.verifiedSuccess = true;
    //                    //$(".btn-code-show").hide();
    //                    window.top.location.href = "/user/changepwd?phone=" + phone + "&code=" + pwd_verify_code;
    //                } else {
    //                    objToolTip("pwd_verify_code", 4, result.message);
    //                }
    //            }
    //        });
    //    }
    //});

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
        /*
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
        */
    });

});

//验证邮箱
function yzemial() {
    var uname = $("#u_id").val();
    var isUnameNull = isNull(uname);
    var isUname = isEmail(uname);
    if (isUnameNull || !isUname) {
        if (isUnameNull) {
            $("#yzemial").attr("class", "lgn-error on");
            $("#yzemial").html("请输入邮箱");
            return false;
        } else if (!isUname) {
            $("#yzemial").attr("class", "lgn-error on");
            $("#yzemial").html("邮箱格式错误");
            return false;
        } else {
            $("#yzemial").attr("class", "lgn-error on");
            $("#yzemial").html("请输入邮箱");
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
                    $("#yzemial").attr("class", "lgn-error on");
                    $("#yzemial").html(result.msg);
                } else {
                    $("#yzemial").attr("class", "lgn-error ");
                    $("#yzemial").html("");
                }
            }
        });
    }
}
//验证密码
function yzma() {
    var upass = $("#u_pass").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    if (isUpassNull || !isUpass) {
        if (isUpassNull) {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("请输入密码");
        } else if (!isUpass) {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("密码长度为6至18个字符！");

        } else {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("请输入密码！");
        }

    } else {
        $("#yzma").attr("class", "lgn-error ");
        $("#yzma").html("");
    }
}
//确认密码
function yzqrma() {
    var upass = $("#u_pass").val();
    var isUpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    var reupass = $("#u_repass").val();
    var isRepassNull = isNull(reupass), isRepass = isLenStrBetween(reupass, 6, 18), isEquals = isEqual(reupass, upass);
    if (isUpassNull || !isUpass || !isEquals) {
        if (isUpassNull) {
            $("#yzqrma").attr("class", "lgn-error on");
            $("#yzqrma").html("请输入确认密码！");
        } else if (!isUpass) {
            $("#yzqrma").attr("class", "lgn-error on");
            $("#yzqrma").html("密码长度为6至18个字符！");

        } else if (!isEquals) {
            $("#yzqrma").attr("class", "lgn-error on");
            $("#yzqrma").html("两次输入的密码不一致");

        } else {
            $("#yzqrma").attr("class", "lgn-error on");
            $("#yzqrma").html("请输入确认密码")
        }
    } else {
        $("#yzqrma").attr("class", "lgn-error ");
        $("#yzqrma").html("")
    }
}
//验证手机号码
function yzphone() {
    var phoneNumber = $("#u_phone").val();
    var _isMobile = isMobile(phoneNumber);
    if (phoneNumber.length <= 0) {
        $("#yzphone").attr("class", "lgn-error on");
        $("#yzphone").html("请输入手机号码！");
    }
    else if (!_isMobile) {
        $("#yzphone").attr("class", "lgn-error on");
        $("#yzphone").html("手机号码格式不正确！");
    } else
    {
        $("#yzphone").attr("class", "lgn-error");
        $("#yzphone").html("");
        /*
        $.ajax({
            url: "/User/CheckPhone?phone=" + phoneNumber,
            type: "post",
            cache: false,
            async: false,
            success: function (result) {
                //判断是否登录、报错、有权限
                CheckJsonData(result);
                if (result.success === 1 || result.success) {
                    $("#yzphone").attr("class", "lgn-error");
                    $("#yzphone").html(result.msg);
                } else {
                    $("#yzphone").attr("class", "lgn-error on");
                    $("#yzphone").html(result.msg);
                }
            }
        });
        */
    }
}

//验证验证码
function yzcode() {
    var code = $("#code").val();
    if ((code == "")) {
        $("#yzcode").attr("class", "lgn-error lgn-ervlt on");
        $("#yzcode").html("请输入验证码");
        $("#yzcode").focus()
        return false;
    }
    else {
        $("#yzcode").attr("class", "lgn-error lgn-ervlt");
        $("#yzcode").html("");
    }
}


