$(function () {

    //提交审核
    $("#btn_submit").click(function () {

        SubmitData();
    });

});



//验证身份证
function CheckIdNo() {
    var umail = $("#u_email").val();
    var temp = $("#u_idnumber").val();
    var isValNull = isNull(temp), isIdNo = new clsIDCard(temp).Valid;
    if (!isValNull && isIdNo) {
        $.ajax({
            url: "/User/CheckIdno?idno=" + temp + "&uname=" + umail,
            type: "post",
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 0) {
                    $("#yz_idnumber").attr("class", "error");
                    $("#yz_idnumber").html("");
                    return false;
                } else {

                    $("#yz_idnumber").attr("class", "error");
                    $("#yz_idnumber").html(result.msg);
                }
            }
        });
    } else if (isValNull) {

        $("#yz_idnumber").attr("class", "error");
        $("#yz_idnumber").html("请输入身份证号！");
        return false;

    } else if (!isIdNo) {
        $("#yz_idnumber").attr("class", "error");
        $("#yz_idnumber").html("身份证号格式不正确！");
        return false;
    }
}

//验证营业执照
function CheckYyzz() {
    var umail = $("#u_email").val();
    var yyzz = $("#u_blicensenumber").val();
    var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
    var isValNull = isNull(yyzz), isYyzz = exrp.test(yyzz);
    if (!isValNull && isYyzz) {
        $.ajax({
            url: "/User/CheckYyzz?yyzz=" + yyzz + "&uname=" + umail,
            type: "post",
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 0) {

                    $("#yz_blicensenumber").attr("class", "error");
                    $("#yz_blicensenumber").html("");

                } else {

                    $("#yz_blicensenumber").attr("class", "error");
                    $("#yz_blicensenumber").html(result.msg);
                }
            }
        });
    } else if (isValNull) {

        $("#yz_blicensenumber").attr("class", "error");
        $("#yz_blicensenumber").html("请输入营业执照！");
        return false;


    } else if (!isYyzz) {

        $("#yz_blicensenumber").attr("class", "error");
        $("#yz_blicensenumber").html("营业执照由15或18位数字和字母组成！");
        return false;
    }
}

//验证真实姓名或公司名称
function CheckNmae() {
    var rname = $("#u_realname").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    var rtype = $("#u_category").val();
    if (rtype == "0") {
        if (exp1.test(rname)) {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("");
            return false;

        } else {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("真实姓名由1-6位汉字组成！");
            return false;


        }
    } else if ((rtype == "1")) {
        if (exp2.test(rname)) {
            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("");
            return false;

        } else {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("公司名称由3-30位汉字组成！");
            return false;


        }
    }
}

//验证开户账号
function CheckAccount() {
    var umail = $("#u_email").val();
    var account = $("#u_account").val();
    var exrp =/^\d{1,30}$/;
    var isValNull = isNull(account), isKhzh = exrp.test(account);
    if (!isValNull && isKhzh) {
        $.ajax({
            url: "/User/CheckBankNo?account=" + account + "&uname=" + umail,
            type: "post",
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 0) {

                    $("#yz_account").attr("class", "error");
                    $("#yz_account").html("");

                } else {

                    $("#yz_account").attr("class", "error");
                    $("#yz_account").html(result.msg);


                }
            }
        });
    } else if (isValNull) {

        $("#yz_account").attr("class", "error");
        $("#yz_account").html("请输入开户账号！");
        return false;

    } else if (!isKhzh) {

        $("#yz_account").attr("class", "error");
        $("#yz_account").html("开户账号格式不正确！");
        return false;

    }
}

//验证开户名称
function CheckAccNmae() {
    var aname = $("#u_name").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{2,30}$/;
    var rtype = $("#u_category").val();
    if (rtype == "0") {

        if (exp1.test(aname)) {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("");
            return false;

        } else {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("开户名称由1-6位汉字组成！");
            return false;
        }
    } else if ((rtype == "1")) {

        if (exp2.test(aname)) {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("");
            return false;

        } else {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("开户名称由2-30位汉字组成！");
            return false;

        }
    }
}

//验证开户银行全称
function CheckBankName() {
    var bankname = $("#u_bankname").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(bankname)) {

        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("");
        return false;

    } else {

        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("开户行全称长度为5-30！");
        return false;
    }
}

//验证电话号码
//function CheckPhone() {
//    var phone = $("#u_phone").val();
//    if (isMobileOrPhone(phone)) {
//        objToolTip("u_phone", 3, "");
//    } else {
//        objToolTip("u_phone", 4, "联系电话为11位手机号或固定电话(号码或区号-号码)！");
//    }
//}

//验证QQ号码
function CheckQQ() {
    var qq = $("#u_qq").val();
    if (isQQ(qq)) {

        $("#yz_qq").attr("class", "error");
        $("#yz_qq").html("");
        return false;

    } else {

        $("#yz_qq").attr("class", "error");
        $("#yz_qq").html("QQ由纯数字组成，5-16位之间！");
        return false;

    }
}

//验证联系地址
function CheckAddress() {
    var address = $("#u_address").val();
    var isValNull = isNull(address);
    if (isValNull) {

        $("#yz_address").attr("class", "error");
        $("#yz_address").html("请填写联系地址！");
        return false;

    } else {
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (rexp.test(address)) {

            $("#yz_address").attr("class", "error");
            $("#yz_address").html("");
            return false;

        } else {

            $("#yz_address").attr("class", "error");
            $("#yz_address").html("联系地址长度为5-30！");
            return false;
        }
    }
}


//验证企业法人
function CheckBusinessEntity() {
    var rname = $("#BusinessEntity").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

    if (exp1.test(rname)) {

        $("#yz_BusinessEntity").attr("class", "error");
        $("#yz_BusinessEntity").html("");
        return false;

    } else {

        $("#yz_BusinessEntity").attr("class", "error");
        $("#yz_BusinessEntity").html("姓名由1-6位汉字组成！");
        return false;
    }
}

//企业注册地址
function CheckRegisteredAddress() {
    var address = $("#RegisteredAddress").val();
    var isValNull = isNull(address);
    if (isValNull) {

        $("#yz_RegisteredAddress").attr("class", "error");
        $("#yz_RegisteredAddress").html("请填写企业注册地址！");
        return false;

    } else {
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (rexp.test(address)) {

            $("#yz_RegisteredAddress").attr("class", "error");
            $("#yz_RegisteredAddress").html("");
            return false;

        } else {

            $("#yz_RegisteredAddress").attr("class", "error");
            $("#yz_RegisteredAddress").html("企业注册地址长度为5-30！");
            return false;
        }
    }
}



//保存数据
function SubmitData() {

    var umail = $("#u_email").val();
    var u_type = $("#u_category").val();
    var rname = $("#u_realname").val();//真实姓名或公司名称
    var aname = $("#u_name").val();//开户名
    var u_photo = $("#u_photo").val();//身份证正面图片
    var u_photof = $("#u_photof").val();//身份证反面图片
    var u_licence = $("#u_licence").val();//开户许可证
    var u_blicense = $("#u_blicense").val();//营业执照
    var account = $("#u_account").val(); //验证开户账号
    var bankname = $("#u_bankname").val();//开户行
    var qq = $("#u_qq").val();//QQ
    var address = $("#u_address").val();//联系地址

    //验证身份证
    var temp = $("#u_idnumber").val();
    var isValNull = isNull(temp), isIdNo = new clsIDCard(temp).Valid;
    if (!isValNull && isIdNo) {
        var pand = false;
        $.ajax({
            url: "/User/CheckIdno?idno=" + temp + "&uname=" + umail,
            type: "post",
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 0) {
                    $("#yz_idnumber").attr("class", "error");
                    $("#yz_idnumber").html("");
                    pand = true;
                } else {
                    $("#yz_idnumber").attr("class", "error");
                    $("#yz_idnumber").html(result.msg);
                    pand = false;
                }
            }
        });
        if (pand == false) {
            return false;
        }
    } else if (isValNull) {

        $("#yz_idnumber").attr("class", "error");
        $("#yz_idnumber").html("请输入身份证号！");
        return false;

    } else if (!isIdNo) {
        $("#yz_idnumber").attr("class", "error");
        $("#yz_idnumber").html("身份证号格式不正确！");
        return false;
    }

    //验证身份证正面照片
    if (isNull(u_photo)) {

        $("#u_photo_tip").attr("class", "error");
        $("#u_photo_tip").html("请选择身份证正面照片！");
        return false;
    }
    else {
        $("#u_photo_tip").attr("class", "error");
        $("#u_photo_tip").html("");
    }

    //验证身份反面证照片
    if (isNull(u_photof)) {

        $("#u_photo_tipf").attr("class", "error");
        $("#u_photo_tipf").html("请选择身份证反面照片！");
        return false;
    }
    else {
        $("#u_photo_tipf").attr("class", "error");
        $("#u_photo_tipf").html("");
    }

    //验证开户行许可证
    if (isNull(u_licence)) {

        $("#u_licence_tip").attr("class", "error");
        $("#u_licence_tip").html("请选择证件照片！");
        return false;
    }
    else {
        $("#u_licence_tip").attr("class", "error");
        $("#u_licence_tip").html("");
    }

    if (u_type == "0") {

        //验证真实姓名
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (exp1.test(rname)) {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("");

        } else {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("真实姓名由1-6位汉字组成！");
            return false;
        }

        //验证开户名称
        var expa = /^[\u4E00-\u9FA5]{1,6}$/;
        if (expa.test(aname)) {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("");

        } else {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("开户名称由1-6位汉字组成！");
            return false;
        }


    }
    else if (u_type == "1") {
        //验证营业执照编号
        var yyzz = $("#u_blicensenumber").val();

        var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isYyzzNull = isNull(yyzz), isYyzz = exrp.test(yyzz);
        if (!isYyzzNull && isYyzz) {
            var pand = false;
            $.ajax({
                url: "/User/CheckYyzz?yyzz=" + yyzz + "&uname=" + umail,
                type: "post",
                async: false,
                success: function (result) {
                    CheckJsonData(result);
                    if (result.success == 0) {
                        $("#yz_blicensenumber").attr("class", "error");
                        $("#yz_blicensenumber").html("");
                        pand = true;
                    }
                    else {
                        ShowMsg(result.msg, "error", "");
                        $("#yz_blicensenumber").attr("class", "error");
                        $("#yz_blicensenumber").html(result.msg);
                        pand = false;
                    }
                }
            });

            if (pand == false) {
                return false;
            }

        } else if (isYyzzNull) {

            $("#yz_blicensenumber").attr("class", "error");
            $("#yz_blicensenumber").html("请输入营业执照");
            return false;

        } else if (!isYyzz) {

            $("#yz_blicensenumber").attr("class", "error");
            $("#yz_blicensenumber").html("营业执照由15或18位数字和字母组成！");
            return false;

        }

        //验证企业法人
        var frname = $("#BusinessEntity").val();
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

        if (exp1.test(frname)) {

            $("#yz_BusinessEntity").attr("class", "error");
            $("#yz_BusinessEntity").html("");

        } else {

            $("#yz_BusinessEntity").attr("class", "error");
            $("#yz_BusinessEntity").html("企业法人姓名由1-6位汉字组成！");
            return false;
        }

        //验证企业注册地址
        var qyaddress = $("#RegisteredAddress").val();
        var isValNull = isNull(qyaddress);
        if (isValNull) {

            $("#yz_RegisteredAddress").attr("class", "error");
            $("#yz_RegisteredAddress").html("请填写企业注册地址！");
            return false;

        } else {
            var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
            if (rexp.test(qyaddress)) {

                $("#yz_RegisteredAddress").attr("class", "error");
                $("#yz_RegisteredAddress").html("");


            } else {

                $("#yz_RegisteredAddress").attr("class", "error");
                $("#yz_RegisteredAddress").html("企业注册地址长度为5-30！");
                return false;
            }
        }

        //验证公司名称
        var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
        if (exp2.test(rname)) {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("");


        } else {

            $("#yz_realname").attr("class", "error");
            $("#yz_realname").html("公司名称由3-30位汉字组成！");
            return false;
        }


        //验证营业执照照片
        if (isNull(u_blicense)) {
            $("#sfzficateyy").attr("class", "error");
            $("#sfzficateyy").html("请选择证件图片（.jpg.png.bmp.jpeg）！");
            return false;

        }
        else {
            $("#sfzficateyy").attr("class", "error");
            $("#sfzficateyy").html("");
        }


        //验证开户名称
        var exp = /^[\u4E00-\u9FA5]{2,30}$/;
        if (exp.test(aname)) {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("");

        } else {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("开户名称由2-30位汉字组成！");
            return false;
        }

    }

    //验证开户账号
    var exrpacc =/^\d{1,30}$/;
    var isAccNull = isNull(account), isKhzh = exrpacc.test(account);
    if (!isAccNull && isKhzh) {
        var pand = false;
        $.ajax({
            url: "/User/CheckBankNo?account=" + account + "&uname=" + umail,
            type: "post",
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 0) {
                    $("#yz_account").attr("class", "error");
                    $("#yz_account").html("");

                    pand = true;
                } else {
                    $("#yz_account").attr("class", "error");
                    $("#yz_account").html(result.msg);
                    pand = false;
                }
            }
        });
        if (pand == false) {
            return false;
        }
    } else if (isAccNull) {

        $("#yz_account").attr("class", "error");
        $("#yz_account").html("请输入开户账号！");
        return false;

    } else if (!isKhzh) {

        $("#yz_account").attr("class", "error");
        $("#yz_account").html("开户账号格式不正确！");
        return false;

    }

    //验证开户银行全称
    var rexpbn = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9]{10,60})$/;
    if (rexpbn.test(bankname)) {

        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("");

    } else {

        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("开户行全称长度为5-30！");
        return false;

    }

    //验证QQ

    if (isQQ(qq)) {

        $("#yz_qq").attr("class", "error");
        $("#yz_qq").html("");

    } else {

        $("#yz_qq").attr("class", "error");
        $("#yz_qq").html("QQ由纯数字组成，5-16位之间！");
        return false;
    }

    //验证联系地址

    var isAddNull = isNull(address);
    if (isAddNull) {

        $("#yz_address").attr("class", "error");
        $("#yz_address").html("联系地址不能为空！");
        return false;

    } else {
        var rexpadd = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (rexpadd.test(address)) {


            $("#yz_address").attr("class", "error");
            $("#yz_address").html("");

        } else {

            $("#yz_address").attr("class", "error");
            $("#yz_address").html("联系地址长度为5-30！");
            return false;
        }
    }
    //禁用按钮防止重复提交
    $("#btn_submit").attr("disabled", "disabled");


    var url = "/User/VerifyInfo";
    var data = {
        u_email: $.trim(umail), u_realname: $.trim(rname), u_qq: $.trim(qq), u_bankname: $.trim(bankname),
        u_name: $.trim(aname), u_account: $.trim(account), u_idnumber: $.trim(temp), u_photo: $.trim(u_photo),
        u_blicense: $.trim(u_blicense), u_blicensenumber: $.trim(yyzz), u_address: $.trim(address), BusinessEntity: $.trim(frname), RegisteredAddress: $.trim(qyaddress), u_photof: $.trim(u_photof), u_licence: $.trim(u_licence)
    };

    $.post(url, data, function (result) {

        if (result.success == 1) {
            ShowMsg(result.msg, "ok", "");
        }
        else {
            ShowMsg(result.msg, "error", "");
            //启用按钮
            $("#btn_submit").removeAttr("disabled");
        }

    });
}

//选择图片
function ChoseImg() {
    $("#certificatefile").trigger("click");
}
//选择身份证图片
function ChoseUpImage() {
    $("#certificatefilesfz").trigger("click");
}

//file控件值有改变就上传
function FileChange() {
    var filedata = $("#certificatefile").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload();
        } else {
            window.parent.ShowMsg("请选择身份证正面照片（.jpg.png.bmp.jpeg）！", "error", "");
            ModifyTipCss("u_photo_tip", "请选择身份证正面照片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ModifyTipCss("u_photo_tip", "请选择身份证正面图片！");
    }
}

//上传图片身份证
function ajaxUpload() {

    iurl = $("#u_photo").val();
    $.ajaxFileUpload({
        url: '/User/UploadImg',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {

                $("#certificate").attr("src", data.imgurlroot);
                $("#u_photo").val(data.imgurl);
                ModifySuccCss("u_photo_tip");
                document.getElementById("add").style.display = "none";

            } else {
                ModifyTipCss("u_photo_tip", data.mess)
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//身份证反面
function FileChangef() {
    var filedata = $("#certificatefilef").val();

    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUploadf();
        } else {
            window.parent.ShowMsg("请选择身份证反面图片（.jpg.png.bmp.jpeg）！", "error", "");
            ModifyTipCss("u_photo_tipf", "请选择身份证反面图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ModifyTipCss("u_photo_tipf", "请选择身份证反面图片！");
    }
}

//上传身份证反面图片
function ajaxUploadf() {

    iurl = $("#u_photof").val();
    $.ajaxFileUpload({
        url: '/User/UploadImgF',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefilef',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#certificatef").attr("src", data.imgurlroot);
                $("#u_photof").val(data.imgurl);
                document.getElementById("add1").style.display = "none";

                ModifySuccCss("u_photo_tipf");
            } else {
                ModifyTipCss("u_photo_tipf", data.mess)
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}


function FileChangexkz() {
    var filedata = $("#licencefilef").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUploadxkz();
        } else {
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            ModifyTipCss("u_licence_tip", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ModifyTipCss("u_licence_tip", "请选择证件图片！");
    }
}
function ajaxUploadxkz() {

    iurl = $("#licencef").val();
    $.ajaxFileUpload({
        url: '/User/UploadImgxkz',
        type: 'post',
        secureuri: false,
        fileElementId: 'licencefilef',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#licencef").attr("src", data.imgurlroot);
                $("#u_licence").val(data.imgurl);
                document.getElementById("add2").style.display = "none";
                ModifySuccCss("u_licence_tip");
            } else {
                ModifyTipCss("u_licence_tip", data.mess)
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//企业上传营业执照
function FileChangeSfz() {
    var filedata = $("#sfzcertificatefile").val();
    var iType = $("#u_category").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            if (iType == "1") {
                ajaxUploadSfz();
            }
        } else {
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            ModifyTipCss("sfzficateyy", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ModifyTipCss("sfzficateyy", "请选择证件图片！");
    }

}

//企业上传营业执照
function ajaxUploadSfz() {

    var iurl = $("#u_blicense").val();

    $.ajaxFileUpload({
        url: '/User/UploadImgsfz',
        type: 'post',
        secureuri: false,
        fileElementId: 'sfzcertificatefile',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {

                $("#sfzficate").attr("src", data.imgurlroot);
                $("#u_blicense").val(data.imgurl);
                ModifySuccCss("sfzficateyy");
                document.getElementById("adds").style.display = "none";
            } else {
                ModifyTipCss("sfzficateyy", data.mess)
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//修改验证失败提示样式
//tid:提示控件id
//content:提示内容
function ModifyTipCss(tid, content) {
    $("#" + tid).attr("class", "error on");
    $("#" + tid).html(content);
}
//修改验证成功提示样式
//tid:提示控件id
function ModifySuccCss(tid) {
    $("#" + tid).attr("class", "error");
    $("#" + tid).html("");
}