

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
            $("#yzzh").attr("class", "lgn-error on");
            $("#yzzh").html("请输入用户名");
            $("#btn_sub").removeClass("on");
            return false;
        }
        else {
            $("#yzzh").attr("class", "on");
            $("#yzzh").html("");
            $("#btn_sub").addClass("on");
        }
        //验证密码
        var password = $("#password").val();
        var ispassword = isNull(password);
        var isUpass = isLenStrBetween(password, 6, 18)
        if (ispassword || !isUpass) {
            if (ispassword) {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("请输入密码");
                $("#btn_sub").removeClass("on");
                return false;
            } else if (!isUpass) {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("密码长度为6至18个字符！");
                $("#btn_sub").removeClass("on");
                return false;
            } else {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("请输入密码");
                $("#btn_sub").removeClass("on");
                return false;
            }

        } else {
            $("#yzma").attr("class", "on");
            $("#yzma").html("");
            $("#btn_sub").addClass("on");
        }
        //验证验证码
        var code = $("#code").val();
        var yzcode = $("#verify").val();
        if ((code == "")) {
            $("#yzcode").attr("class", "lgn-error lgn-ervlt on");
            $("#yzcode").html("请输入验证码");
            $("#btn_sub").removeClass("on");
            $("#yzcode").focus()
            return false;
        }
        else {
            $("#yzcode").attr("class", "lgn-error lgn-ervlt");
            $("#yzcode").html("");
            $("#btn_sub").addClass("on");
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
//验证用户名
function yzzh()
{
    var loginName = $("#loginName").val();
    var isloginName = isNull(loginName);
    if (isloginName) {
        $("#yzzh").attr("class", "lgn-error on");
        $("#yzzh").html("请输入用户名");
        $("#btn_sub").removeClass("on");
        return false;
    }
    else {
        $("#yzzh").attr("class", "on");
        $("#yzzh").html("");
        $("#btn_sub").addClass("on");
    }
}
//验证密码
function yzma() {
    var password = $("#password").val();
    var ispassword = isNull(password);
    var isUpass = isLenStrBetween(password, 6, 18)
    if (ispassword || !isUpass) {
        if (ispassword) {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("请输入密码");
            $("#btn_sub").removeClass("on");
            return false;
        } else if (!isUpass) {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("密码长度为6至18个字符！");
            $("#btn_sub").removeClass("on");
            return false;
        } else {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("请输入密码");
            $("#btn_sub").removeClass("on");
            return false;
        }

    } else {
        $("#yzma").attr("class", "on");
        $("#yzma").html("");
        $("#btn_sub").addClass("on");
    }
}

//验证码
function yzcode() {
    var code = $("#code").val();
    var yzcode = $("#verify").val();
    if ((code == "")) {
        $("#yzcode").attr("class", "lgn-error lgn-ervlt on");
        $("#yzcode").html("请输入验证码");
        $("#btn_sub").removeClass("on");
        $("#yzcode").focus()
        return false;
    }
    else {
        $("#yzcode").attr("class", "lgn-error lgn-ervlt");
        $("#yzcode").html("");
        $("#btn_sub").addClass("on");
    }

}