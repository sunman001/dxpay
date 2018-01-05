function ShowMsg(content, info, callback) {
    layer.msg(content, {
        icon: info == "error" ? 2 : info == "ok" ? 1 : 0,
        time: 1000,
        skin: 'layer-ext-moon',
    }, callback == "" ? "" : callback);
}

//验证登录名称
var mess = "";
function logNameYz() {

    var myreg = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
    var logName = $("#logName").val();
    var isNull = isRequestNotNull(logName);//是否为空
    var isMail = isEmail(logName);//格式是否正确
    var isphone = myreg.test(logName);

    if (isNull) {
        $("#logNames").attr("class", "login-error on");
        $("#logNames").html("请输入登录邮箱或者手机号码！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";
        return false;

    } else if (!isphone && !isMail) {

        $("#logNames").attr("class", "login-error on");
        $("#logNames").html("邮箱或手机号码格式不正确！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";

        return false;

    }
    else {
        $("#logNames").attr("class", "login-error");
        document.getElementById("login-btn1").style.display = "none";
        document.getElementById("login-btn").style.display = "block";
    }

}
//验证密码
function logPwdYz() {

    var logPwd = $("#logPwd").val();
    var isUpassNull = isRequestNotNull(logPwd);//是否为空


    if (!isUpassNull && logPwd.length > 5) {
        $("#passwords").attr("class", "login-error");
        document.getElementById("login-btn1").style.display = "none";
        document.getElementById("login-btn").style.display = "block";

    } else if (isUpassNull) {

        $("#passwords").attr("class", "login-error on");
        $("#passwords").html("请输入登录密码！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";
        return false;


    } else if (logPwd.length < 6) {

        $("#passwords").attr("class", "login-error on");
        $("#passwords").html("密码长度大于6个字符！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";

        return false;
    }


}
//验证验证码
function yzmYz() {
    var yzm = $("#yzm").val();
    var isyzm = isRequestNotNull(yzm);//是否为空
    if (isyzm) {
        $("#yzmts").attr("class", "login-error on");
        $("#yzmts").html("请输入验证码！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";
        return false;
    }
    else {
        $("#yzmts").attr("class", "login-error");
        document.getElementById("login-btn1").style.display = "none";
        document.getElementById("login-btn").style.display = "block";
    }

}

if (mess == "") {
    document.getElementById("login-btn1").style.display = "none";
    document.getElementById("login-btn").style.display = "block";
}
else {
    document.getElementById("login-btn1").style.display = "block";
    document.getElementById("login-btn").style.display = "none";
}
//登录
function logs() {
    //验证登录邮箱

    var myreg = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
    var logName = $("#logName").val();
    var isNull = isRequestNotNull(logName);//是否为空
    var isMail = isEmail(logName);//格式是否正确
    var isphone = myreg.test(logName);

    if (isNull) {
        $("#logNames").attr("class", "login-error on");
        $("#logNames").html("请输入登录邮箱或者手机号码！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";
        return false;

    } else if (!isphone && !isMail) {

        $("#logNames").attr("class", "login-error on");
        $("#logNames").html("邮箱或手机号码格式不正确！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";

        return false;

    }
    else {
        $("#logNames").attr("class", "login-error");
        document.getElementById("login-btn1").style.display = "none";
        document.getElementById("login-btn").style.display = "block";
    }



    //验证登录密码
    var logPwd = $("#logPwd").val();
    var isUpassNull = isRequestNotNull(logPwd);//是否为空


    if (!isUpassNull && logPwd.length > 5) {
        $("#passwords").attr("class", "login-error");

    } else if (isUpassNull) {

        $("#passwords").attr("class", "login-error on");
        $("#passwords").html("请输入登录密码！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";
        return false;


    } else if (logPwd.length < 6) {

        $("#passwords").attr("class", "login-error on");
        $("#passwords").html("密码长度大于6个字符！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";

        return false;
    }
    //验证码
    var yzm = $("#yzm").val();
    var isyzm = isRequestNotNull(yzm);//是否为空
    if (isyzm) {
        $("#yzmts").attr("class", "login-error on");
        $("#yzmts").html("请输入验证码！");
        document.getElementById("login-btn1").style.display = "block";
        document.getElementById("login-btn").style.display = "none";
        return false;
    }
    else {
        $("#yzmts").attr("class", "login-error");
    }

    var url = "/Index/UserLogin";
    var data = { logName: logName, logPwd: logPwd, valCode: yzm }

    $.post(url, data, function (result) {
        switch (result.success) {
            case "0":
                ShowMsg(result.msg, "error", "");
                break;
            case "1":
                window.location.href = result.url;
                break;
            case "2":

                ShowMsg(result.msg, "error", "");
                break;
        }
    })
}
