function ShowMsg(content, info, callback) {
    layer.msg(content, {
        icon: info == "error" ? 2 : info == "ok" ? 1 : 0,
        time: 1000,
        skin: 'layer-ext-moon',
    }, callback == "" ? "" : callback);
}
$(function () {
    $("#send").click(function () {
        //验证姓名
        var name = $.trim($("#Name").val());
        if ($.trim(name) != "") {
           
        } else {
            ShowMsg("您的姓名不能为空!", "error", "")
            return false;
        }
        //验证手机号码
        var myreg = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
        var telephone = $.trim($("#MobilePhone").val());
        if ($.trim(telephone) != "") {
            if (!myreg.test($("#MobilePhone").val())) {
                ShowMsg("请输入有效的手机号码!", "error", "")
                return false;
            }
            else {
               
            }
        }
        else {
            ShowMsg("请输入手机号码!", "error", "")
            return false;
        }
   
        //验证QQ
        var reg = /^\d{5,10}$/;
        var QQ = $.trim($("#QQ").val());
        if ($.trim(QQ) != "") {
            if (!reg.test($("#QQ").val())) {
                ShowMsg("请输入正确的QQ!", "error", "")
               
                return false;
            }
            else {
             
            }
        }
        else {
            ShowMsg("请输入QQ!", "error", "")
            return false;
        }
        var EmailAddress = $.trim($("#EmailAddress").val());
        var Website = $('input[name="Website"]:checked ').val();
        var RequestContent = $.trim($("#RequestContent").val());

        $("#send").attr("disabled", "disabled");
        var data = {
            name: name, MobilePhone: telephone, EmailAddress: EmailAddress, QQ: QQ, Website: Website, RequestContent: RequestContent
        };
        var url = "/Index/addCooperationApplication";
        $.post(url, data, function (retJson) {
            $("#send").attr("disabled", false);
            if (retJson.success == 1) {
              //  document.getElementById('wp-layer').style.display = 'block';
                document.getElementById("Name").value = "";
                document.getElementById("MobilePhone").value = "";
                document.getElementById("EmailAddress").value = "";
                document.getElementById("QQ").value = "";
                document.getElementById("RequestContent").value = "";
                ShowMsg("您的信息我们已经收到，稍后我们会联系您！", "success", "");
               // window.location.href = "~/index/index";
               
            //    var e = e || window.event; //浏览器兼容性   
            //    var elem = e.target || e.srcElement;
            //    while (elem) { //循环判断至跟节点，防止点击的是div子元素   
            //        if (elem.id && elem.id == 'idx-csult-fix') {
            //            return;
            //        }
            //        elem = elem.parentNode;
            //    }
            //    $('.idx-consult').animate({ "right": "-330" }, 300);
            }
            else if (retJson.success == 9998) {
                ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                ShowMsg(retJson.msg, "error", "");
                return false;
            }

        })



    });
});






/*****************************************login验证******************************************************/

//登录
function logs() {
    //验证登录邮箱
    var myreg = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
    var logName = $("#logName").val();
    var isNull = isRequestNotNull(logName);//是否为空
    var isMail = isEmail(logName);//格式是否正确
    var isphone = myreg.test(logName);

    if (isNull) {
        ShowMsg("请输入登录邮箱或者手机号码!", "error","")      
       // document.getElementById("login-btn1").style.display = "block";
       // document.getElementById("login-btn").style.display = "none";
        return false;

    } else if (!isphone && !isMail) {

        ShowMsg("邮箱或手机号码格式不正确!", "error", "")
     
       // document.getElementById("login-btn1").style.display = "block";
       // document.getElementById("login-btn").style.display = "none";

        return false;

    }
    else {
     
       // document.getElementById("login-btn1").style.display = "none";
       // document.getElementById("login-btn").style.display = "block";
    }



    //验证登录密码
    var logPwd = $("#logPwd").val();
    var isUpassNull = isRequestNotNull(logPwd);//是否为空


    if (!isUpassNull && logPwd.length > 5) {
        $("#passwords").attr("class", "login-error");

    } else if (isUpassNull) {
        ShowMsg("请输入登录密码!", "error", "")
       // document.getElementById("login-btn1").style.display = "block";
       // document.getElementById("login-btn").style.display = "none";
        return false;


    } else if (logPwd.length < 6) {
        ShowMsg("密码长度大于6个字符！", "error", "")
        
       // document.getElementById("login-btn1").style.display = "block";
       // document.getElementById("login-btn").style.display = "none";
        return false;
    }
    //验证码
    var yzm = $("#yzm").val();
    var isyzm = isRequestNotNull(yzm);//是否为空
    if (isyzm) {
        ShowMsg("请输入验证码！", "error", "")
       
       // document.getElementById("login-btn1").style.display = "block";
       // document.getElementById("login-btn").style.display = "none";
        return false;
    }
    else {
      
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
