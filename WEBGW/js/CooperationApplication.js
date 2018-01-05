
$(function () {
    $("#send").click(function () {
        //验证姓名
        var name = $.trim($("#Name").val());
        if ($.trim(name) != "") {
            $("#yzname").attr("class", "error");
            $("#yzname").html("");
        } else {
            $("#yzname").attr("class", "error");
            $("#yzname").html("您的姓名不能为空");
            return false;
        }
        //验证手机号码
        var myreg = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
        var telephone = $.trim($("#MobilePhone").val());
        if ($.trim(telephone) != "") {
            if (!myreg.test($("#MobilePhone").val())) {
                $("#yzmobilePhone").attr("class", "error");
                $("#yzmobilePhone").html("请输入有效的手机号码！");
                return false;
            }
            else {
                $("#yzmobilePhone").attr("class", "error");
                $("#yzmobilePhone").html("");
            }
        }
        else {
            $("#yzmobilePhone").attr("class", "error");
            $("#yzmobilePhone").html("请输入手机号码");
            return false;
        }
      
        //验证QQ
        var reg = /^\d{5,10}$/;
        var QQ = $.trim($("#QQ").val());
        if ($.trim(QQ) != "") {
            if (!reg.test($("#QQ").val())) {
                $("#YZQQ").attr("class", "error");
                $("#YZQQ").html("请输入正确的QQ！");
                return false;
            }
            else {
                $("#YZQQ").attr("class", "error");
                $("#YZQQ").html("");
            }
        }
        else {
            $("#YZQQ").attr("class", "error");
            $("#YZQQ").html("请输入QQ");
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
                document.getElementById('wp-layer').style.display = 'block';
                document.getElementById("Name").value = "";
                document.getElementById("MobilePhone").value = "";
                document.getElementById("EmailAddress").value = "";
                document.getElementById("QQ").value = "";
               // document.getElementById("Website").value = "";
                document.getElementById("RequestContent").value = "";
                //window.ShowMsg(retJson.msg, "success", "发送成功");
                var e = e || window.event; //浏览器兼容性   
                var elem = e.target || e.srcElement;
                while (elem) { //循环判断至跟节点，防止点击的是div子元素   
                    if (elem.id && elem.id == 'idx-csult-fix') {
                        return;
                    }
                    elem = elem.parentNode;
                }
                $('.idx-consult').animate({ "right": "-330" }, 300);
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
//验证姓名
function yzname() {
    var name = $.trim($("#Name").val());
    if ($.trim(name) != "") {
        $("#yzname").attr("class", "error");
        $("#yzname").html("");
    } else {
        $("#yzname").attr("class", "error");
        $("#yzname").html("您的姓名不能为空");
        return false;
    }
}
//验证手机号码
function yzmobilePhone() {
    var myreg = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
    var telephone = $.trim($("#MobilePhone").val());
    if ($.trim(telephone) != "") {
        if (!myreg.test($("#MobilePhone").val())) {
            $("#yzmobilePhone").attr("class", "error");
            $("#yzmobilePhone").html("请输入有效的手机号码！");
            return false;
        }
        else {
            $("#yzmobilePhone").attr("class", "error");
            $("#yzmobilePhone").html("");
        }
    }
    else {
        $("#yzmobilePhone").attr("class", "error");
        $("#yzmobilePhone").html("请输入手机号码");
        return false;
    }
}

//验证qq
function yzQQ() {
    var reg = /^\d{5,10}$/
    var QQ = $.trim($("#QQ").val());
    if ($.trim(QQ) != "") {
        if (!reg.test($("#QQ").val())) {
            $("#YZQQ").attr("class", "error");
            $("#YZQQ").html("请输入正确的QQ！");
            return false;
        }
        else {
            $("#YZQQ").attr("class", "error");
            $("#YZQQ").html("");
        }
    }
    else {
        $("#YZQQ").attr("class", "error");
        $("#YZQQ").html("请输入QQ");
        return false;
    }
}


