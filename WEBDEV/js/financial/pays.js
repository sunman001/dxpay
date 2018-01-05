//提款
function btn_pay() {

    //提现金额
    var payMoney = $("#payMoney").val();
    var WithdrawalsType = $("#WithdrawalsType").val();
    //提现最小金额
    var WithdrawalsMinimum = $("#WithdrawalsMinimum").val();

    if (!isNull(payMoney) && eval(payMoney) >= eval(WithdrawalsMinimum)) {

        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("");
    }
    else {
        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("提现金额不能为空且必须大于等于" + WithdrawalsMinimum + "元！");
        return false;
    }

    //可提金额
    var ketiMoney = $("#ketiMoney").val();

    if (eval(payMoney) > eval(ketiMoney)) {
        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("提现金额不能超过可提金额！");
        return false;
    }
    else {
        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("");
    }

    //银行卡信息
    var b_bankid = $("#b_bankid").val();
    var payid = $("#payid").val();

    if (b_bankid == 0) {

        $("#yz_bankid").attr("class", "error");
        $("#yz_bankid").html("请选择要提现的银行卡！");
        return false;
    }
    else {

        $("#yz_bankid").attr("class", "error");
        $("#yz_bankid").html("");

    }
    //支付密码
    var PayPwd = $("#PayPwd").val();
    var isUpassNull = isNull(PayPwd), isUpass = isLenStrBetween(PayPwd, 6, 18);

    if (isUpassNull) {

        $("#yz_PayPwd").attr("class", "error");
        $("#yz_PayPwd").html("请输入支付密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_PayPwd").attr("class", "error");
        $("#yz_PayPwd").html("支付密码长度为6至18个字符！");
        return false;

    }
    else {

        $("#yz_PayPwd").attr("class", "error");
        $("#yz_PayPwd").html("");
    }


    var url = "/Financial/paysAdd";
    var data = { payMoney: $.trim(payMoney), b_bankid: $.trim(b_bankid), payid: $.trim(payid), PayPwd: $.trim(PayPwd), ketiMoney: $.trim(ketiMoney), WithdrawalsType: $.trim(WithdrawalsType) };

    $("#btnPays").attr("disabled", "disabled");

    $.post(url, data, function (retJson) {
        //判断是否登录、报错、有权限
        //CheckJsonData(retJson);
        if (retJson.success == 0) {
            window.parent.ShowMsg(retJson.msg, "error", "");
        }
        else {
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
            var name = "提款管理";
            var isLeaf = true;//是否套用
            var id = $(this).attr("data-id");//id
            var href = "/Financial/PaymentList";//链接
            closeIfram(name, isLeaf, href, id, 'child');
        }

        $("#btnPays").attr("disabled", false);
    })

   
}

//验证提现金额
function yzPayMoney() {

    var tst = /^\d{1,}(\.\d{1,2})?$/;
    //提现金额
    var payMoney = $("#payMoney").val();
    //提现最小金额
    var WithdrawalsMinimum = $("#WithdrawalsMinimum").val();

    if (!isNull(payMoney) && eval(payMoney) >= eval(WithdrawalsMinimum)) {

        if (!tst.test(payMoney)) {
            $("#yz_payMoney").attr("class", "error");
            $("#yz_payMoney").html("提现金额为整数或者保留两位小数！");
            return false;
        }
        else {
            $("#yz_payMoney").attr("class", "error");
            $("#yz_payMoney").html("");
        }

    }
    else {
        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("提现金额不能为空且必须大于等于" + WithdrawalsMinimum + "元！");
        return false;
    }

    //可提金额
    var ketiMoney = $("#ketiMoney").val();

    if (eval(payMoney) > eval(ketiMoney)) {
        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("提现金额不能超过可提金额！");
        return false;
    }
    else {
        $("#yz_payMoney").attr("class", "error");
        $("#yz_payMoney").html("");
    }

}

//验证支付密码
function yzPayPwd() {

    var PayPwd = $("#PayPwd").val();

    var isUpassNull = isNull(PayPwd), isUpass = isLenStrBetween(PayPwd, 6, 18);

    if (isUpassNull) {

        $("#yz_PayPwd").attr("class", "error");
        $("#yz_PayPwd").html("请输入支付密码！");
        return false;
    }
    else if (!isUpass) {

        $("#yz_PayPwd").attr("class", "error");
        $("#yz_PayPwd").html("支付密码长度为6至18个字符！");
        return false;

    }
    else {

        $("#yz_PayPwd").attr("class", "error");
        $("#yz_PayPwd").html("");
    }
}

//验证银行卡信息
function yzBankid() {

    var b_bankid = $("#b_bankid").val();

    if (b_bankid == 0) {

        $("#yz_bankid").attr("class", "error");
        $("#yz_bankid").html("请选择要提现的银行卡！");
        return false;
    }
    else {

        $("#yz_bankid").attr("class", "error");
        $("#yz_bankid").html("");

    }
}

//银行卡弹窗
function CkAuditStatus() {

    window.parent.ShouwDiaLogWan("银行卡提现金额", 900, 500, "/Financial/PayTc");

}

//选择银行卡信息
function btnPay(index) {
    //提现最小金额
    var WithdrawalsMinimum = $("#WithdrawalsMinimum").val();
    var valArr = new Array;
    var maxMoney = "";
    var msg = "";
    $("#table :checkbox[checked]").each(function (i) {

        if ($(this).val() > 0) {

            if (eval($("#maxMoney-" + $(this).val()).val()) >= eval(WithdrawalsMinimum)) {
                valArr[i] = $(this).val() + "," + $("#maxMoney-" + $(this).val()).val();
            }
            else {
                msg = "单卡限额必须大于等于" + WithdrawalsMinimum + "元!";
            }
        }
    });

    if (msg != "") {
        window.parent.ShowMsg(msg, "error", "");
        return false;
    }

    var amountArr = valArr.join('|');

    if (amountArr == "") {
        window.parent.ShowMsg("请选择数据！", "error", "");
        return;
    }

    window.parent.layer.getChildFrame("#b_bankid", index).val(amountArr);

    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}

//分页
function ArticleManage(pageIndex, pageSize) {

    var searchType = $.trim($("#searchType").val());
    var banknumber = $.trim($("#banknumber").val());
    var flag = $.trim($("#flag").val());

    var url = "/Financial/PayTc?pageIndexs=" + pageIndex + "&PageSize=" + pageSize + "&searchType=" + searchType + "&banknumber=" + banknumber + "&flag=" + flag;
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function btnUserBank() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}