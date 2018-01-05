$(function () {
    //提现金额
    $("#txjebtn").click(function () {
        var txje = $.trim($("#txje").val());
        if (txje != "") {
            if (!isNaN(txje)) {
                if (txje <= 0) {
                    window.parent.ShowMsg("最低提现金额不能小于零！", "error", "");
                    return;
                }
            } else {
                window.parent.ShowMsg("最低提现金额为整数！", "error", "");
                return;
            }
        } else {
            window.parent.ShowMsg("最低提现金额不能为空！", "error", "");
            return;
        }
        var txjeId = $.trim($("#txjeId").val());
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(txje), s_id: $.trim(txjeId), s_name: "zdtxje", s_remarks: "最低提现金额" };
        $("#txjebtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#txjebtn").attr("disabled", false);
        })
    })
    //账单生成日期
    $("#zdscrqbtn").click(function () {
        var zdscrq = $.trim($("#zdscrq").val());
        if (zdscrq != "") {
            if (!isNaN(zdscrq)) {
                if (zdscrq <= 0) {
                    window.parent.ShowMsg("账单生成日期不能小于零！", "error", "");
                    return;
                } else if (eval(zdscrq) > 31) {
                    window.parent.ShowMsg("账单生成日期不能大于31日！", "error", "");
                    return;
                }
            } else {
                window.parent.ShowMsg("账单生成日期为整数！", "error", "");
                return;
            }
        } else {
            window.parent.ShowMsg("账单生成日期不能为空！", "error", "");
            return;
        }
        var zdscrqID = $.trim($("#zdscrqID").val());
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(zdscrq), s_id: $.trim(zdscrqID), s_name: "zdscrq", s_remarks: "账单生成日期" };
        $("#zdscrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#zdscrqbtn").attr("disabled", false);
        })
    })
    //提款日期
    $("#tkrqbtn").click(function () {
        var zdscrq = $.trim($("#zdscrq").val());
        var tkrq = $.trim($("#tkrq").val());
        if (tkrq != "") {
            if (!isNaN(tkrq)) {
                if (tkrq <= 0) {
                    window.parent.ShowMsg("提款日期不能小于零！", "error", "");
                    return;
                } else if (eval(tkrq) > 31) {
                    window.parent.ShowMsg("提款日期不能大于31日！", "error", "");
                    return;
                }
                else if (eval(tkrq) < eval(zdscrq)) {
                    window.parent.ShowMsg("提款日期不能小于账单生成日期！", "error", "");
                    return;
                }
            } else {
                window.parent.ShowMsg("提款日期为整数！", "error", "");
                return;
            }
        } else {
            window.parent.ShowMsg("提款日期不能为空！", "error", "");
            return;
        }
        var tkrqID = $.trim($("#tkrqID").val());
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(tkrq), s_id: $.trim(tkrqID), s_name: "tkrq", s_remarks: "提款日期" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })

    //屏蔽应用类型设置
    $("#yylxtn").click(function () {
        var yylxid = $.trim($("#yylxid").val());
        var valArr = new Array;
        $('input[name="yylx"]:checked').each(function (i) {
            valArr[i] = $(this).val();
        });
        var yylx = valArr.join(',');
        if (yylx == "") {
            window.parent.ShowMsg("请选择应用类型！", "error", "");
            return false;
        }
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(yylx), s_id: $.trim(yylxid), s_name: "yylx", s_remarks: "屏蔽应用类型" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                //  window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })
    //屏蔽支付通道设置
    $("#zftdtn").click(function () {
        var zftdid = $.trim($("#zftdid").val());
        var valArr = new Array;
        $('input[name="zflx"]:checked').each(function (i) {
            valArr[i] = $(this).val();
        });
        var zflx = valArr.join(',');
        if (zflx == "") {
            window.parent.ShowMsg("请选择支付通道！", "error", "");
            return false;
        }
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(zflx), s_id: $.trim(zftdid), s_name: "zftd", s_remarks: "屏蔽支付通道" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                //  window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })
    //屏蔽城市
    $("#citytn").click(function () {
        var city = $("#city").val();
        var cityID = $.trim($("#cityID").val());
        if ($.trim(city) == "") {
            window.parent.ShowMsg("屏蔽城市不能为空！", "error", "");
            return false;
        }
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(city), s_id: $.trim(cityID), s_name: "city", s_remarks: "屏蔽城市" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                // window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })
    //商品显示名称
    $("#goodsnametn").click(function () {
        var goodsname = $("#goodsname").val();
        var goodsnameID = $.trim($("#goodsnameID").val());
        if ($.trim(goodsname) == "") {
            window.parent.ShowMsg("商品名称不能为空！", "error", "");
            return false;
        }
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(goodsname), s_id: $.trim(goodsnameID), s_name: "goodsname", s_remarks: "显示商品名称" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                //window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })
    //随机扣减金额
    $("#moneytn").click(function () {
        var moeny1 = $.trim($("#moeny1").val());
        var moeny2 = $.trim($("#moeny2").val());
        if ($.trim(moeny1) != "") {
            if (isNaN(moeny1)) {
                window.parent.ShowMsg("扣减金额为整数！", "error", "");
                return;
            }
        } else {
            window.parent.ShowMsg("金额不能为空！", "error", "");
            return;
        }
        if ($.trim(moeny1) != "") {
            if (isNaN(moeny2)) {
                window.parent.ShowMsg("扣减金额为整数！", "error", "");
                return;
            }
        } else {
            window.parent.ShowMsg("金额不能为空！", "error", "");
            return;
        }
        var money = " ";
        if (eval > moeny1 && moeny2) {
            money = moeny1 + "," + moeny2;
        }
        var moneyid = $.trim($("#moneyid").val());
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: money, s_id: $.trim(moneyid), s_name: "money", s_remarks: "随机扣减金额" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                //window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })
    //ip白名单
    $("#ipbmdidtn").click(function () {
        var ipbmd = $.trim($("#ipbmd").val());
        var ipbmdid = $.trim($("#ipbmdid").val());
        if ($.trim(ipbmd) == "") {
            window.parent.ShowMsg("ip白名单不能为空！", "error", "");
            return false;
        }
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(ipbmd), s_id: $.trim(ipbmdid), s_name: "ipbmd", s_remarks: "ip白名单" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                //window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })

    //保存屏蔽设置
    $("#pbszbtn").click(function () {

        var goodsname = $("#goodsname").val();
        if ($.trim(goodsname) == "") {
            window.parent.ShowMsg("商品名称不能为空！", "error", "");
            return false;
        }
        $("#goodsnametn").click();
        var pbsz = $('input[name="pbszstate"]:checked').val();
        var pbszid = $("#pbszid").val();
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(pbsz), s_id: $.trim(pbszid), s_name: "pbszstate", s_remarks: "屏蔽设置状态" };
        $("#tkrqbtn").attr("disabled", "disabled");
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })
    //保存服务器设置
    $("#bcfwqbtn").click(function () {
        var tz213 = $('input[name="tz213"]:checked').val();
        var tz213id = $("#tz213id").val();
        var tz153 = $('input[name="tz153"]:checked').val();
        var tz153id = $("#tz153id").val();
        var tz220 = $('input[name="tz220"]:checked').val();
        var tz220id = $("#tz220id").val();

        if (tz153 == "-1" && tz213 == "-1" && tz220 == "-1") {
            window.parent.ShowMsg("最少必须开启一台服务器！", "error", "");
            return false;
        }
        fwq153();
        fwq213();
        $("#tkrqbtn").attr("disabled", "disabled");
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(tz220), s_id: $.trim(tz220id), s_name: "tz220", s_remarks: "服务器220状态设置" };
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
            $("#tkrqbtn").attr("disabled", false);
        })
    })

    function fwq153() {
        var tz153 = $('input[name="tz153"]:checked').val();
        var tz153id = $("#tz153id").val();
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(tz153), s_id: $.trim(tz153id), s_name: "tz153", s_remarks: "服务器153状态设置" };
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                //window.parent.ShowMsg(retJson.msg, "ok", "");
                return true;
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
        })
    }
    function fwq213() {
        var tz213 = $('input[name="tz213"]:checked').val();
        var tz213id = $("#tz213id").val();
        var url = "/System/InsertOrUpdateSystem";
        var data = { s_value: $.trim(tz213), s_id: $.trim(tz213id), s_name: "tz213", s_remarks: "服务器213状态设置" };
        $.post(url, data, function (retJson) {
            if (retJson.success == 1) {
                // window.parent.ShowMsg(retJson.msg, "ok", "");
                return true;
            }
            else if (retJson.success == 9998) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            } else if (retJson.success == 9999) {
                window.parent.ShowMsg(retJson.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                return false;
            } else if (retJson.success == 9997) {
                window.top.location.href = retJson.Redirect;
                return false;
            }
            else {
                window.parent.ShowMsg(retJson.msg, "error", "");
                return false;
            }
        })
    }
})