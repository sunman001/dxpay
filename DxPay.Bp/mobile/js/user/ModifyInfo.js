//JavaScript Document

$(function () {

    //保存按钮事件
    $("#btn_save").click(function () {

        UpdateData();
    });
});

//保存修改信息
function UpdateData() {

    var u_id = $("#u_id").val();
    //原密码
    var upass = $("#old_pwd").val();
    var isOldpassNull = isNull(upass), isUpass = isLenStrBetween(upass, 6, 18);
    //新密码
    var xpass = $("#new_pwd").val();
    var isXpassNull = isNull(xpass), isXpass = isLenStrBetween(xpass, 6, 18);
    //确认密码
    var qpass = $("#qr_pwd").val();
    var isQpassNull = isNull(qpass), isQpass = isLenStrBetween(qpass, 6, 18);
    var isPwdXd = isEqual(xpass, qpass);

    //验证原密码    

    if (isOldpassNull) {
        ShowMsg("请输入原密码", "error", "")
        return false;
    }
    else if (isXpassNull) {
        ShowMsg("请输入新密码", "error", "")
       
        return false;
    }
    else if (isQpassNull) {
        ShowMsg("请输入确认密码", "error", "")
        return false;
    }
    else if (!isUpass) {
        ShowMsg("原密码长度为6至18个字符！", "error", "")
      
        return false;
    }
    else if (!isXpass) {
        ShowMsg("新密码长度为6至18个字符！", "error", "")
       
        return false;

    }
    else if (!isQpass) {

         ShowMsg("确认密码长度为6至18个字符！", "error", "")
      
        return false;

    }
    else if (!isPwdXd) {
        ShowMsg("两次输入的密码不一致！", "error", "")
        return false;

    }
    else {

      
    }


    var url = "/User/UpdateUserInfo";
    var data = { u_id: $.trim(u_id), upass: $.trim(upass), xpass: $.trim(xpass) };
    //防止重复提交
    $("#btn_save").attr("disabled", "disabled");

    $.post(url, data, function (result) {

        if (result.success == 1) {
            ShowMsg(result.msg, "ok", function () {
                parent.window.location.href = "/Login/Index";
            });
        } else if (result.success == 2) {

            ShowMsg(result.msg, "error", "");
            //启用按钮
            $("#btn_save").removeAttr("disabled");

        } else {

            ShowMsg(result.msg, "error", "");
            //启用按钮
            $("#btn_save").removeAttr("disabled");
        }

    })
}