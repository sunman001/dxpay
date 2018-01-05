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
                ShowMsg("请输入邮箱或者手机号码", "error", "");
                $("#u_id").focus()
                return false;
            } else if (!isUname && !_isMobile) {
                ShowMsg("输入的格式不正确", "error", "");
                $("#u_id").focus()
                return false;
            } else {
                ShowMsg("请输入邮箱或者手机号码", "error", "");
                $("#u_id").focus()
              
                return false;
            }
        }
        else {
           
        }
        //密码
        var upass = $("#u_pass").val();
        var isUpassNull = isNull(upass);
        var isUpass = isLenStrBetween(upass, 6, 18)
        if (isUpassNull || !isUpass) {
            if (isUpassNull) {
                ShowMsg("请输入密码", "error", "");
                $("#u_pass").focus()
                return false;
            } else if (!isUpass) {
                ShowMsg("密码长度为6至18个字符！", "error", "");
                $("#u_pass").focus()
              

            } else {
                ShowMsg("请输入密码!", "error", "");
                $("#u_pass").focus()
            }

        } else {
           
        }

        var code = $("#code").val();
        if ((code == "")) {
            ShowMsg("请输入验证码", "error", "");        
            $("#yzcode").focus()
            return false;
        }
        else {
         
        }
        var url = "/Home/UserLogin";
        var data = { u_name: $.trim(uname), u_pwd: $.trim(upass), code: $.trim(code) };
        $.post(url, data, function (result) {
            //判断是否登录、报错、有权限
            if (result.success == 1) {
                window.location.href = encodeURI("/Home/Default");
            }
            else {
                ShowMsg(result.msg, "error", "");
            
            }
        })
    });

});
