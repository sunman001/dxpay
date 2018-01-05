//JavaScript Document

//退出登录
function loginOut()
{
    $.ajax({
        url: "/Home/LoginOut",
        type: "post",
        cache: false,
        async: false,
        success: function (result) {
            CheckJsonData(result);
            if (result.success == 1) {
                window.top.location.href = result.gourl;
            }
        }
    });
}


