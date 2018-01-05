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

    $('#forget-pwd-hover').click(function () {
        //// alert('123');
        window.top.location.href = "/User/BackPwd";

    });
    $("#btn_sub").click(function () {
        //用户名
        var uname = $("#u_id").val();
        var isUnameNull = isNull(uname);
        var isUname = isEmail(uname);
        var _isMobile = isMobile(uname);
        if (isUnameNull || (!isUname && !_isMobile)) {
            if (isUnameNull) {
                $("#yzzh").attr("class", "lgn-error on");
                $("#yzzh").html("请输入用户名");
                $("#btn_sub").removeClass("on");
                return false;
            } else if (!isUname && !_isMobile) {
                $("#yzzh").attr("class", "lgn-error on");
                $("#yzzh").html("输入的格式不正确");
                $("#btn_sub").removeClass("on");
                return false;
            } else {
                $("#yzzh").attr("class", "lgn-error on");
                $("#yzzh").html("请输入用户名");
                $("#btn_sub").removeClass("on");
                return false;
            }
        }
        else {
            $("#yzzh").attr("class", "on");
            $("#yzzh").html("");
            $("#btn_sub").addClass("on");
        }
        //密码
        var upass = $("#u_pass").val();
        var isUpassNull = isNull(upass);
        var isUpass = isLenStrBetween(upass, 6, 18)
        if (isUpassNull || !isUpass) {
            if (isUpassNull) {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("请输入密码");
                $("#btn_sub").removeClass("on");
                return false;
            } else if (!isUpass) {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("密码长度为6至18个字符！");
                $("#btn_sub").removeClass("on");

            } else {
                $("#yzma").attr("class", "lgn-error on");
                $("#yzma").html("请输入密码");
                $("#btn_sub").removeClass("on");
            }

        } else {
            $("#yzma").attr("class", "on");
            $("#yzma").html("");
            $("#btn_sub").addClass("on");
        }

        var code = $("#code").val();
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
        var url = "/Home/UserLogin";
        var data = { u_name: $.trim(uname), u_pwd: $.trim(upass), code: $.trim(code) };
        $.post(url, data, function (result) {
            //判断是否登录、报错、有权限
            //CheckJsonData(result);
            if (result.success == 1) {
                window.location.href = encodeURI("/Home/Default");
            }
            else {
                ShowMsg(result.msg, "error", "");
                $("#btn_sub").removeClass("on");
            }
        })
    });



});

//验证用户名
function yzzh() {
    var uname = $("#u_id").val();
    var isUnameNull = isNull(uname);
    var isUname = isEmail(uname);
    var _isMobile = isMobile(uname);
    if (isUnameNull || (!isUname && !_isMobile)) {
        if (isUnameNull) {
            $("#yzzh").attr("class", "lgn-error on");
            $("#yzzh").html("请输入用户名");
            $("#btn_sub").removeClass("on");
            return false;
        } else if (!isUname && !_isMobile) {
            $("#yzzh").attr("class", "lgn-error on");
            $("#yzzh").html("输入的格式不正确");
            $("#btn_sub").removeClass("on");
            return false;
        } else {
            $("#yzzh").attr("class", "lgn-error on");
            $("#yzzh").html("请输入用户名");
            $("#btn_sub").removeClass("on");
            return false;
        }
    }
    else {
        $("#yzzh").attr("class", "on");
        $("#yzzh").html("");
        $("#btn_sub").addClass("on");
    }
}

//验证密码
function yzma() {
    var upass = $("#u_pass").val();
    var isUpassNull = isNull(upass);
    var isUpass = isLenStrBetween(upass, 6, 18)
    if (isUpassNull || !isUpass) {
        if (isUpassNull) {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("请输入密码");
            $("#btn_sub").removeClass("on");
            return false;
        } else if (!isUpass) {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("密码长度为6至18个字符！");
            $("#btn_sub").removeClass("on");

        } else {
            $("#yzma").attr("class", "lgn-error on");
            $("#yzma").html("请输入密码");
            $("#btn_sub").removeClass("on");
        }

    } else {
        $("#yzma").attr("class", "on");
        $("#yzma").html("");
        $("#btn_sub").addClass("on");
    }
}
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
