
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
                url: "/User/CheckBankCardNo?u_banknumber=" + u_banknumber + "&uid=" + u_id,
                type: "post",
                async: false,
                success: function (result) {
                    CheckJsonData(result);
                    if (result.success == 0) {

                        pand = true;
                    } else {

                        ShowMsg(result.msg, "error", "");
                        pand = false;
                    }
                }
            });
            if (pand == false) {
                return false;
            }

        }
        else if (isNull(u_banknumber)) {

            ShowMsg("请输入银行卡号！", "error", "");
            return false;
        }
        else if (!isBank) {

            ShowMsg("银行卡格式输入错误！", "error", "");
            return false;
        }


        //银行名称
        var u_bankname = $("#u_bankname").val();

        if (isNull(u_bankname)) {

            ShowMsg("请输入银行名称！", "error", "");
            return false;
        }


        //开户行名称
        var u_openbankname = $("#u_openbankname").val();
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;

        if (isNull(u_openbankname)) {

            ShowMsg("请输入开户行名称！", "error", "");
            return false;
        }
        else if (!rexp.test(u_openbankname)) {

            ShowMsg("开户行名称长度为5-30！", "error", "");
            return false;
        }


        //持卡人
        var u_name = $("#u_name").val();

        if (isNull(u_name)) {

            ShowMsg("请输入持卡人姓名！", "error", "");
            return false;
        }

        //开户行所在的省份
        var province = $("#u_province").val();

        if (isNull(province) || province == "省份") {

            ShowMsg("请输入开户行所在的省份！", "error", "");
            return false;
        }


        //开户行所在的地区
        var area = $("#u_area").val();

        if (isNull(area) || area == "城市") {

            ShowMsg("请输入开户行所在的地区！", "error", "");
            return false;
        }

        //付款标识
        var u_flag = $("#u_flag").val();

        if (u_flag == "0") {
            ShowMsg("请选择付款标识！", "error", "");
            return false;
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
                window.location.href = "/User/BankCardList";
            }
            $("#btnBankCard").attr("disabled", false);
        })


    })


})

