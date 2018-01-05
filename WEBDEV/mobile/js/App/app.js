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
            ShowMsg("请选择支付类型！", "error", "");
            return false;
        }
        
        //应用名称
        var appname = $.trim($("#appname").val());
        if (isNull(appname)) {
            ShowMsg("请输入应用名称！", "error", "");
         
            return false;

        } else if (!isStrLength(appname)) {

            ShowMsg("应用名称长度不能超过20！", "error", "");
        
            return false;

        } else {

        }

        //运营平台
        var terrace = $.trim($("#terrace").val());

        if (terrace == "0") {
            ShowMsg("请选择运行平台！", "error", "");
            return false;
        }
        else {
           
        }
        //应用类型
        var xzdx = $.trim($("#zlyy").val());

        if (xzdx == "0") {
            ShowMsg("请选择应用子类型！", "error", "");
            return false;
        }
        else {
           
        }

        //应用地址
        var yyaddress = $.trim($("#yyaddress").val());

        if (isNull(yyaddress)) {
            ShowMsg("请输入应用地址！", "error", "");
            return false;
        }
        else if (!isUri(yyaddress)) {
            ShowMsg("请正确填写应用地址，必须以http或https开头！", "error", "");
            return false;
        }
        else {
           
        }

        //异步通知地址
        var appurl = $.trim($("#appurl").val());
        if (isNull(appurl)) {
            ShowMsg("异步通知地址不能为空！", "error", "");
            return false;

        } else if (!isUri(appurl)) {

            ShowMsg("请正确填写通知地址，必须已http或https开头！", "error", "");
           
            return false;

        } else {


        }
        //同步通知地址
        var showurl = $.trim($("#showurl").val());
        if (terrace == "3") {
            if (isNull(showurl)) {
                ShowMsg("同步通知地址不能为空！", "error", "");
                return false;

            } else if (!isUri(showurl)) {
                ShowMsg("请正确填写通知地址，必须已http或https开头！", "error", "");
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
            ShowMsg("请填写详细的应用简介,必须大于30个字符！", "error", "");
            return false;
        }
        else {
           
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
                window.location.href = "/App/AppList";
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


