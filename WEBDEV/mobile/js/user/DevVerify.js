$(function () {

    //提交审核
    $("#btn_submit").click(function () {

        SubmitData();
    });

});
//保存数据
function SubmitData() {

    var umail = $("#u_email").val();
    var u_type = $("#u_category").val();
    var rname = $("#u_realname").val();//真实姓名或公司名称
    var aname = $("#u_name").val();//开户名
    var u_photo = $("#u_photo").val();//身份证图片
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
    } else if (isValNull) {

        ShowMsg("请输入身份证号", "error", "");
        return false;

    } else if (!isIdNo) {
        ShowMsg("身份证号格式不正确", "error", "");
        return false;
    }

    //验证身份证照片
    if (isNull(u_photo)) {
        ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
        return false;
    }

    if (u_type == "0") {

        //验证真实姓名
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (exp1.test(rname)) {

        } else {
            ShowMsg("真实姓名由1-6位汉字组成！", "error", "");
            return false;
        }
        //验证开户名
        var expa = /^[\u4E00-\u9FA5]{1,6}$/;
        if (expa.test(aname)) {

        } else {
            ShowMsg("开户名由1-6位汉字组成！", "error", "");
            return false;
        }


    } else if (u_type == "1") {
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
                        pand = true;
                    }
                    else {
                        ShowMsg(result.msg, "error", "");
                        pand = false;
                    }
                }
            });
            if (pand == false) {
                return false;
            }
        } else if (isYyzzNull) {

            ShowMsg("请输入营业执照", "error", "");
            return false;

        } else if (!isYyzz) {

            ShowMsg("营业执照由15或18位数字和字母组成！", "error", "");
            return false;

        }

        //验证企业法人
        var frname = $("#BusinessEntity").val();
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

        if (exp1.test(frname)) {



        } else {
            ShowMsg("企业法人姓名由1-6位汉字组成！", "error", "");
            return false;
        }

        //验证企业注册地址
        var qyaddress = $("#RegisteredAddress").val();
        var isValNull = isNull(qyaddress);
        if (isValNull) {
            ShowMsg("请填写企业注册地址！", "error", "");
            return false;

        } else {
            var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
            if (rexp.test(qyaddress)) {

            } else {
                ShowMsg("企业注册地址长度为5-30！", "error", "");
                return false;
            }
        }

        //验证公司名称
        var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
        if (exp2.test(rname)) {

        } else {
            ShowMsg("公司名称由3-30位汉字组成！", "error", "");
            return false;
        }



        //验证营业执照照片
        if (isNull(u_blicense)) {
            ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            return false;
        }


        //验证开户名
        var exp = /^[\u4E00-\u9FA5]{2,30}$/;
        if (exp.test(aname)) {

            $("#yz_name").attr("class", "error");
            $("#yz_name").html("");

        } else {
            ShowMsg("开户名由2-30位汉字组成！", "error", "");
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
    } else if (isAccNull) {

        ShowMsg("请输入开户账号", "error", "");
        return false;

    } else if (!isKhzh) {

        ShowMsg("开户账号格式不正确!", "error", "");
        return false;
    }

    //验证开户银行全称
    var rexpbn = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9]{10,60})$/;
    if (rexpbn.test(bankname)) {

        $("#yz_bankname").attr("class", "error");
        $("#yz_bankname").html("");

    } else {
        ShowMsg("开户行全称长度为5-30！", "error", "");
        return false;

    }

    //验证QQ

    if (isQQ(qq)) {


    } else {
        ShowMsg("QQ由纯数字组成，5-16位之间！", "error", "");
        return false;
    }

    //验证联系地址

    var isAddNull = isNull(address);
    if (isAddNull) {
        ShowMsg("联系地址不能为空！", "error", "");
        return false;

    } else {
        var rexpadd = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (rexpadd.test(address)) {


            $("#yz_address").attr("class", "error");
            $("#yz_address").html("");

        } else {
            ShowMsg("联系地址长度为5-30！", "error", "");
            return false;
        }
    }
    //禁用按钮防止重复提交
    $("#btn_submit").attr("disabled", "disabled");

    var url = "/User/VerifyInfo";
    var data = {
        u_email: $.trim(umail), u_realname: $.trim(rname), u_qq: $.trim(qq), u_bankname: $.trim(bankname),
        u_name: $.trim(aname), u_account: $.trim(account), u_idnumber: $.trim(temp), u_photo: $.trim(u_photo),
        u_blicense: $.trim(u_blicense), u_blicensenumber: $.trim(yyzz), u_address: $.trim(address), BusinessEntity: $.trim(frname), RegisteredAddress: $.trim(qyaddress)
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
            ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            // ModifyTipCss("u_photo_tip", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
        //ModifyTipCss("u_photo_tip", "请选择证件图片！");
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
               // ModifySuccCss("u_photo_tip");
                document.getElementById("add").style.display = "none";
            } else {
               // ModifyTipCss("u_photo_tip", data.mess)
                ShowMsg(data.mess, "error", "");
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
            ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            // ModifyTipCss("sfzficateyy", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
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
                //ModifySuccCss("sfzficateyy");
               
                document.getElementById("adds").style.display = "none";
            } else {
                // ModifyTipCss("sfzficateyy", data.mess)
                ShowMsg(data.mess, "error", "");
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