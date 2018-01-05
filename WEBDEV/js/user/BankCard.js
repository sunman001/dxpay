
$(function () {

    $("#btnBankCard").click(function () {

        var u_id = $("#u_id").val();

        //银行卡号
        var u_banknumber = $("#u_banknumber").val();
        var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
        var isBank = rexp.test(u_banknumber);//格式是否正确

        if (!isNull(u_banknumber) && isBank) {

            var pand = false;
            $.ajax({
                url: "/User/CheckBankCardNo?u_banknumber=" + u_banknumber + "&uid=" + $("#u_id").val(),
                type: "post",
                async: false,
                success: function (result) {
                    CheckJsonData(result);
                    if (result.success == 0) {
                        $("#yz_banknumber").attr("class", "error");
                        $("#yz_banknumber").html("");
                        pand = true;
                    } else {
                        $("#yz_banknumber").attr("class", "error");
                        $("#yz_banknumber").html(result.msg);
                        pand = false;
                    }
                }
            });
            if (pand == false) {
                return false;
            }

        }
        else if (isNull(u_banknumber)) {
            $("#yz_banknumber").attr("class", "error");
            $("#yz_banknumber").html("请输入银行卡号！");
            return false;
        }
        else if (!isBank) {
            $("#yz_banknumber").attr("class", "error");
            $("#yz_banknumber").html("银行卡格式输入错误！");
            return false;
        }
        else {
            $("#yz_banknumber").attr("class", "error");
            $("#yz_banknumber").html("");
        }

        //银行名称
        var u_bankname = $("#u_bankname").val();

        if (isNull(u_bankname)) {
            $("#yz_bankname").attr("class", "error");
            $("#yz_bankname").html("请输入银行名称！");
            return false;
        }
        else {
            $("#yz_bankname").attr("class", "error");
            $("#yz_bankname").html("");
        }

        //开户行名称
        var u_openbankname = $("#u_openbankname").val();
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;

        if (isNull(u_openbankname)) {
            $("#yz_openbankname").attr("class", "error");
            $("#yz_openbankname").html("请输入开户行名称！");
            return false;
        }
        else if (!rexp.test(u_openbankname)) {
            $("#yz_openbankname").attr("class", "error");
            $("#yz_openbankname").html("开户行名称长度为5-30！");
            return false;
        }
        else {
            $("#yz_openbankname").attr("class", "error");
            $("#yz_openbankname").html("");
        }

        //持卡人
        var u_name = $("#u_name").val();

        if (isNull(u_name)) {
            $("#yz_name").attr("class", "error");
            $("#yz_name").html("请输入持卡人姓名！");
            return false;
        }
        else {
            $("#yz_name").attr("class", "error");
            $("#yz_name").html("");

        }

        //开户行所在的省份
        var province = $("#u_province").val();

        if (isNull(province) || province == "省份") {
            $("#yz_province").attr("class", "error");
            $("#yz_province").html("请输入开户行所在的省份！");
            return false;
        }
        else {
            $("#yz_province").attr("class", "error");
            $("#yz_province").html("");
        }

        //开户行所在的地区
        var area = $("#u_area").val();

        if (isNull(area) || area == "城市") {
            $("#yz_area").attr("class", "error");
            $("#yz_area").html("请输入开户行所在的地区！");
            return false;
        }
        else {
            $("#yz_area").attr("class", "error");
            $("#yz_area").html("");
        }

        //付款标识
        var u_flag = $("#u_flag").val();

        if (u_flag == "0") {
            $("#yz_flag").attr("class", "error");
            $("#yz_flag").html("请选择付款标识！");
            return false;
        }
        else {
            $("#yz_flag").attr("class", "error");
            $("#yz_flag").html("");
        }


        var url = "/User/AddBankCard";
        var data = { u_banknumber: $.trim(u_banknumber), u_bankname: $.trim(u_bankname), u_openbankname: $.trim(u_openbankname), u_name: $.trim(u_name), u_id: u_id, u_area: $.trim(area), u_province: $.trim(province), u_flag: $.trim(u_flag) };

        $("#btnBankCard").attr("disabled", "disabled");

        $.post(url, data, function (retJson) {
            //判断是否登录、报错、有权限
            //CheckJsonData(retJson);
            if (retJson.success == 0) {

                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "ok", "");
                var name = "银行卡管理";
                var isLeaf = true;//是否套用
                var id = $(this).attr("data-id");//id
                var href = "/User/BankCardList";//链接
                closeIfram(name, isLeaf, href, id, 'child');
            }
            $("#btnBankCard").attr("disabled", false);
        })


    })


})



//验证银行卡号
function yz_banknumber() {

    var u_banknumber = $("#u_banknumber").val();
    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isBank = rexp.test(u_banknumber);//格式是否正确

    if (!isNull(u_banknumber) && isBank) {
        var data = { u_banknumber: $.trim(u_banknumber), uid: $("#u_id").val() };
        $.post("/User/CheckBankCardNo", data, function (msg) {
            if (msg.success) {
                $("#yz_banknumber").attr("class", "error");
                $("#yz_banknumber").html("已存在此银行卡账号！");
                return false;
            } else {
                $("#yz_banknumber").attr("class", "error");
                $("#yz_banknumber").html("");
            }
        });
    }
    else if (isNull(u_banknumber)) {
        $("#yz_banknumber").attr("class", "error");
        $("#yz_banknumber").html("请输入银行卡号！");
        return false;
    }
    else if (!isBank) {
        $("#yz_banknumber").attr("class", "error");
        $("#yz_banknumber").html("银行卡格式输入错误！");
        return false;
    }
    else {
        $("#yz_banknumber").attr("class", "error");
        $("#yz_banknumber").html("");
    }
}

//验证银行名称
function yz_bankname() {

    var u_bankname = $("#u_bankname").val();

    if (isNull(u_bankname)) {
        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("请输入银行名称！");
        return false;
    }
    else {
        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("");
    }

}

//验证开户行名称
function yz_openbankname() {

    var u_openbankname = $("#u_openbankname").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;


    if (isNull(u_openbankname)) {
        $("#yz_openbankname").attr("class", "error");
        $("#yz_openbankname").html("请输入开户行名称！");
        return false;
    }
    else if (!rexp.test(u_openbankname)) {
        $("#yz_openbankname").attr("class", "error");
        $("#yz_openbankname").html("开户行名称长度为5-30！");
        return false;
    }
    else {
        $("#yz_openbankname").attr("class", "error");
        $("#yz_openbankname").html("");
    }

}

//验证持卡人姓名
function yz_name() {

    var u_name = $("#u_name").val();

    if (isNull(u_name)) {
        $("#yz_name").attr("class", "error");
        $("#yz_name").html("请输入持卡人姓名！");
        return false;
    }
    else {
        $("#yz_name").attr("class", "error");
        $("#yz_name").html("");

    }

}

function upload()
{
    window.parent.ShouwDiaLogWan("导入银行卡数据", 800, 300, "/User/Import");
}

