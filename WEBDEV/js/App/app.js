var msg = "";
$(function () {

    var u_auditstate = $("#u_auditstate").val();

    if (u_auditstate == 0 || u_auditstate == -1) {
        $("#btn_add").attr("disabled", "disabled");

        window.parent.ShowMsg("请先进行实名验证！", "error", "");
        return false;
    }

    //应用
    $("#btnyy").click(function () {


        //支付类型
        var a = document.getElementsByName("ck");
        var paytype = "";
        for (var i = 0; i < a.length; i++) {
            if (a[i].checked) {
                paytype += a[i].value + ",";
            }
        }
        paytype = paytype.substr(0, paytype.length - 1)
        if (paytype == "") {
            $("#idtype").attr("class", "error ");
            $("#idtype").html("请选择支付类型！");
            return false;
        }

        //应用名称
        var appname = $.trim($("#appname").val());
        if (isNull(appname)) {

            $("#yz_appname").attr("class", "error");
            $("#yz_appname").html("请输入应用名称！");
            return false;

        } else if (!isStrLength(appname)) {

            $("#yz_appname").attr("class", "error");
            $("#yz_appname").html("应用名称长度不能超过20！");
            return false;

        } else {

            $("#yz_appname").attr("class", "error");
            $("#yz_appname").html("");
        }

        //运营平台
        var terrace = $.trim($("#terrace").val());

        if (terrace == "0") {

            $("#yz_terrace").attr("class", "error");
            $("#yz_terrace").html("请选择运行平台！");
            return false;
        }
        else {
            $("#yz_terrace").attr("class", "error");
            $("#yz_terrace").html("");
        }
        //应用类型
        var xzdx = $.trim($("#zlyy").val());

        if (xzdx == "0") {

            $("#yz_zlyy").attr("class", "error");
            $("#yz_zlyy").html("请选择应用子类型！");
            return false;
        }
        else {
            $("#yz_zlyy").attr("class", "error");
            $("#yz_zlyy").html("");
        }

        //应用地址
        var yyaddress = $.trim($("#yyaddress").val());

        if (isNull(yyaddress)) {
            $("#yz_yyaddress").attr("class", "error");
            $("#yz_yyaddress").html("请输入应用地址！");
            return false;
        }
        else if (!isUri(yyaddress)) {

            $("#yz_yyaddress").attr("class", "error");
            $("#yz_yyaddress").html("请正确填写应用地址，必须以http或https开头！");
            return false;

        }
        else {
            $("#yz_yyaddress").attr("class", "error");
            $("#yz_yyaddress").html("");
        }

        //异步通知地址
        var appurl = $.trim($("#appurl").val());
        if (isNull(appurl)) {

            $("#yz_appurl").attr("class", "error");
            $("#yz_appurl").html("异步通知地址不能为空！");
            return false;

        } else if (!isUri(appurl)) {

            $("#yz_appurl").attr("class", "error");
            $("#yz_appurl").html("请正确填写通知地址，必须已http或https开头！");
            return false;

        } else {

            $("#yz_appurl").attr("class", "error");
            $("#yz_appurl").html("");

        }
        //同步通知地址
        var showurl = $.trim($("#showurl").val());
        if (terrace == "3") {
            if (isNull(showurl)) {

                $("#yz_showurl").attr("class", "error");
                $("#yz_showurl").html("同步通知地址不能为空！");
                return false;

            } else if (!isUri(showurl)) {

                $("#yz_showurl").attr("class", "error");
                $("#yz_showurl").html("请正确填写通知地址，必须已http或https开头！");
                return false;

            } else {

                $("#yz_showurl").attr("class", "error");
                $("#yz_showurl").html("");
            }
        }
        //应用简介
        var a_appsynopsis = $.trim($("#a_appsynopsis").val());

        if (a_appsynopsis.length < 30) {

            $("#yz_appsynopsis").attr("class", "error");
            $("#yz_appsynopsis").html("请填写详细的应用简介,必须大于30个字符！！");

            return false;
        }
        else {
            $("#yz_appsynopsis").attr("class", "error");
            $("#yz_appsynopsis").html("");
        }

        var a_id = $.trim($("#a_id").val());
        $("#btn_add").attr("disabled", "disabled");


        var data = { a_name: $.trim(appname), a_appurl: $.trim(yyaddress), a_platform_id: $.trim(terrace), a_paymode_id: $.trim(paytype), a_apptype_id: $.trim(xzdx), a_notifyurl: $.trim(appurl), a_showurl: $.trim(showurl), a_appsynopsis: $.trim(a_appsynopsis), a_id: $.trim(a_id) };

        var url = "/App/InsertUpdateApp";
        $.post(url, data, function (retJson) {
            //判断是否登录、报错、有权限
            //CheckJsonData(retJson);
            if (retJson.success == 0) {

                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "ok", "");
                var name = "应用列表";
                var isLeaf = true;//是否套用
                var id = $(this).attr("data-id");//id
                var href = "/App/AppList";//链接
                closeIfram(name, isLeaf, href, id, 'child');
            }
            $("#btn_add").attr("disabled", false);
        })
    })
})

//h5 显示同步通知地址
function cjyz() {
    var terrace = $("#terrace").val();
    if (terrace == "3") {
        document.getElementById("tbdzxs").style.display = "";
        isH5("3");
    } else {
        IsSdk(terrace);
        document.getElementById("tbdzxs").style.display = "none";
    }
}
//判断是否为h5平台
function isH5(obj) {
    document.getElementById("paytype_5").checked = false;
    if (obj == 3) {
        if ($("#paytype_4").data("stat") == 1) {
            document.getElementById("paytype_4").disabled = false;
        }
        if ($("#paytype_6").data("stat") == 1) {
            document.getElementById("paytype_6").disabled = false;
        }
        if ($("#paytype_7").data("stat") == 1) {
            document.getElementById("paytype_7").disabled = false;
        }
        var state = $("#paytype_5").data("stat");
        if (state == 1) {
            document.getElementById("paytype_5").disabled = true;
        }
    }
}
//判断是否为sdk时调用
function IsSdk(obj) {
    if (obj == 1 || obj == 2) {
        document.getElementById("paytype_4").checked = false;
        document.getElementById("paytype_6").checked = false;
        document.getElementById("paytype_7").checked = false;
        if ($("#paytype_5").data("stat") == 1) {
            document.getElementById("paytype_5").disabled = false;
        }
        if ($("#paytype_4").data("stat") == 1) {
            document.getElementById("paytype_4").disabled = true;
        }
        if ($("#paytype_6").data("stat") == 1) {
            document.getElementById("paytype_6").disabled = true;
        }
        if ($("#paytype_7").data("stat") == 1) {
            document.getElementById("paytype_7").disabled = true;
        }
    }
}

//验证应用地址
function yzyyads() {
    var yyaddress = $.trim($("#yyaddress").val());

    if (isNull(yyaddress)) {
        $("#yz_yyaddress").attr("class", "error");
        $("#yz_yyaddress").html("请输入应用地址！");
        return false;
    }
    else if (!isUri(yyaddress)) {

        $("#yz_yyaddress").attr("class", "error");
        $("#yz_yyaddress").html("请正确填写应用地址，必须以http或https开头！");
        return false;

    }
    else {
        $("#yz_yyaddress").attr("class", "error");
        $("#yz_yyaddress").html("");
    }

}

//验证应用名称
function yzappname() {
    var appname = $.trim($("#appname").val());
    if (isNull(appname)) {

        $("#yz_appname").attr("class", "error");
        $("#yz_appname").html("请输入应用名称！");
        return false;

    } else if (!isStrLength(appname)) {

        $("#yz_appname").attr("class", "error");
        $("#yz_appname").html("应用名称长度不能超过20！");
        return false;

    } else {

        $("#yz_appname").attr("class", "error");
        $("#yz_appname").html("");
    }
}
//验证通知地址
function yzurl() {
    var appurl = $.trim($("#appurl").val());
    if (isNull(appurl)) {

        $("#yz_appurl").attr("class", "error");
        $("#yz_appurl").html("通知地址不能为空！");
        return false;

    } else if (!isUri(appurl)) {

        $("#yz_appurl").attr("class", "error");
        $("#yz_appurl").html("请正确填写通知地址，必须以http或https开头！");
        return false;

    } else {

        $("#yz_appurl").attr("class", "error");
        $("#yz_appurl").html("");
    }
}
//验证同步地址
function tbyzurl() {
    var showurl = $.trim($("#showurl").val());
    if (isNull(showurl)) {

        $("#yz_showurl").attr("class", "error");
        $("#yz_showurl").html("通知地址不能为空！");
        return false;

    } else if (!isUri(showurl)) {

        $("#yz_showurl").attr("class", "error");
        $("#yz_showurl").html("请正确填写通知地址，必须以http或https开头！");
        return false;

    } else {

        $("#yz_showurl").attr("class", "error");
        $("#yz_showurl").html("");
    }
}
//查询子类应用
function xzyylx() {


    var id = $("#xzyylx").val();

    var a_apptype_id = $("#a_apptype_id").val();

    if (id > 0) {

        var data = { id: $.trim(id), a_apptype_id: a_apptype_id, paymodeid: 0 };
        var url = "/APP/SelectApp";
        $.post(url, data, function (msg) {
            //debugger;
            $("#zlyy").html(msg);
            $(".rule-singless-select").ruleSinglessSelect();
        })
    }

}

function change() {
    $("#idtype").attr("class", "error");
    $("#idtype").html("");
}
