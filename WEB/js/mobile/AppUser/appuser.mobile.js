// JavaScript Document

//查询用户列表
function SearchUser() {
    //当前页
    var CurrcentPage = $("#curr_page").val();
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(CurrcentPage, PageSize);
}

//加载数据
function LoadData(currPage, pageSize) {
    var url = "/AppUser/AppUserList?curr=" + currPage + "&psize=" + pageSize;
    var type = $("#s_type").val();
    var keys = $("#s_keys").val();
    var state = $("#s_state").val();
    var check = $("#s_check").val();
    var rzlx = $("#s_category").val();
    var sort = $("#s_sort").val();
    var relation_type = $("#relation_type").val();
    var RiskM = $("#RiskM").val();

    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx + "&relation_type=" + relation_type + "&RiskM=" + RiskM;
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(1, PageSize);
}

//勾选复选框展示手续费输入框
function CkeckType(obj) {
    var flag = $(obj).attr("checked");
    if (flag) {
        $("#u_poundage_lab").show();
        $("#u_poundage_tip").show();
    } else {
        $("#u_poundage_lab").hide();
        $("#u_poundage_tip").hide();
    }
}

//选择认证类型展示对应的输入项
function ChangeType(obj) {
    var stext = $(obj).find("option:selected").text();
    if (stext != "" || stext != null || stext != undefined) {
        if (stext == "个人") {

            $("#user_yyzz").hide();
            $("#user_qyfr").hide();
            $("#user_qyzcaddress").hide();
            $("#u_realname").parent().parent().parent().find("dt").text("真实姓名：");
            $("#u_realname_tip").html("*必填且1-6位汉字组成！");
            $("#u_name_tip").html("*必填且1-6位汉字组成！");
            $("#qysfz").hide();
        } else {

            $("#user_yyzz").show();
            $("#user_qyfr").show();
            $("#user_qyzcaddress").show();
            $("#u_realname").parent().parent().parent().find("dt").text("公司名称：");
            $("#u_realname_tip").html("*必填且3-30位汉字组成！");
            $("#u_name_tip").html("*必填且2-30位汉字组成！");
            $("#qysfz").show();
        }
    }
}

//选择支付类型呢展示对应的输入项
function ChangeBankType(obj) {
    $("#bank_fenhang").hide();
    $("#bank_kaihuren").hide();
    $("#bank_zhanghu").hide();
    if ($(obj).find("option:selected").text() == "支付宝") {
        $("#bank_fenhang").hide();
        $("#bank_kaihuren").show();
        $("#bank_zhanghu").show();
        $("#bank_zhanghu").find("dt").text("支付宝账户：");
    } else {
        $("#bank_fenhang").show();
        $("#bank_kaihuren").show();
        $("#bank_zhanghu").show();
        $("#bank_zhanghu").find("dt").text("银行账户：");
    }
}

//验证邮箱
function CheckEmail() {
    var email = $("#u_email").val();
    var isNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (!isNull && isMail) {
        var data = { cval: email, uid: $("#u_id").val() };
        $.post("/AppUser/CheckEmail", data, function (msg) {
            if (msg.success) {
                ModifyTipCss("u_email_tip", "已存在该邮箱地址！");
            } else {
                ModifySuccCss("u_email_tip");
            }
        });
    } else if (isNull) {
        ModifyTipCss("u_email_tip", "请输入邮箱地址！");
    } else if (!isMail) {
        ModifyTipCss("u_email_tip", "邮箱地址格式不正确！");
    }
}

//验证密码
function CheckPwd() {
    var pwd = $("#u_password").val();
    if (pwd.length < 6) {
        ModifyTipCss("u_password_tip", "密码不能小于6位！");
    } else {
        ModifySuccCss("u_password_tip");
    }
}

//验证真实姓名
function CheckRName() {
    var sType = $("#u_category").val();
    var rname = $("#u_realname").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    if (sType == "0") {
        if (!exp1.test(rname)) {
            ModifyTipCss("u_realname_tip", "真实姓名由1-6位汉字组成！");
        } else {
            ModifySuccCss("u_realname_tip");
        }
    } else if (sType == "1") {
        if (!exp2.test(rname)) {
            ModifyTipCss("u_realname_tip", "公司名称由3-30位汉字组成！");
        } else {
            ModifySuccCss("u_realname_tip");
        }
    }
}

//验证联系电话
function CheckPhone() {
    var phone = $("#u_phone").val();
    if (isMobile(phone)) {
        ModifySuccCss("u_phone_tip");
    } else {
        ModifyTipCss("u_phone_tip", "联系电话为11位手机号！");
    }
}

//验证号码
function CheckQQ() {
    var qq = $("#u_qq").val();
    if (isQQ(qq)) {
        ModifySuccCss("u_qq_tip");
    } else {
        ModifyTipCss("u_qq_tip", "纯数字组成，5-16位之间！");
    }
}

//验证联系地址
function CheckAddRess() {
    var addre = $("#u_address").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(addre)) {
        ModifySuccCss("u_address_tip");
    } else {
        ModifyTipCss("u_address_tip", "联系地址长度为5-30！");
    }
}

//验证身份证号
function CheckIdno() {
    var idno = $("#u_idnumber").val();
    var isNull = isRequestNotNull(idno);//是否为空    
    var isIdno = new clsIDCard(idno).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: idno, uid: $("#u_id").val() };
        $.post("/AppUser/CheckIdno", data, function (msg) {
            if (msg.success) {
                ModifyTipCss("u_idnumber_tip", "已存在该身份证号！");
            } else {
                ModifySuccCss("u_idnumber_tip");
            }
        });
    } else if (isNull) {
        ModifyTipCss("u_idnumber_tip", "请输入身份证号！");
    } else if (!isIdno) {
        ModifyTipCss("u_idnumber_tip", "身份证号格式不正确！");
    }
}

//验证营业执照
function CheckYYZZ() {
    var yyzz = $("#u_blicensenumber").val();
    var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
    var isNull = isRequestNotNull(yyzz);//是否为空
    var isYyzz = exrp.test(yyzz);//格式是否正确
    if (!isNull && isYyzz) {
        var data = { cval: yyzz, uid: $("#u_id").val() };
        $.post("/AppUser/CheckYyzz", data, function (msg) {
            if (msg.success) {
                ModifyTipCss("u_blicensenumber_tip", "已存在该营业执照！");
            } else {
                ModifySuccCss("u_blicensenumber_tip");
            }
        });
    } else if (isNull) {
        ModifyTipCss("u_blicensenumber_tip", "请输入营业执照！");
    } else if (!isYyzz) {
        ModifyTipCss("u_blicensenumber_tip", "由15或18位数字和字母组成！");
    }
}

//验证开户账号
function CheckAccount() {
    var khzh = $("#u_account").val();
    var rexp = /^\d{1,30}$/;
    //var rexp1 = /^0?\d{9,11}|(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
    var isNull = isRequestNotNull(khzh);//是否为空
    var isYyzz = rexp.test(khzh);//格式是否正确
    if (!isNull && isYyzz) {
        var data = { cval: khzh, uid: $("#u_id").val() };
        $.post("/AppUser/CheckBankNo", data, function (msg) {
            if (msg.success) {
                ModifyTipCss("u_account_tip", "已存在该开户账号！");
            } else {
                ModifySuccCss("u_account_tip");
            }
        });
    } else if (isNull) {
        ModifyTipCss("u_account_tip", "请输入开户账号！");
    } else if (!isYyzz) {
        ModifyTipCss("u_account_tip", "开户账号格式不正确！");
    }
}

//验证开户名
function CheckName() {
    var sType = $("#u_category").val();
    var name = $("#u_name").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{2,30}$/;
    if (sType == "0") {
        if (!exp1.test(name)) {
            ModifyTipCss("u_name_tip", "开户名由1-6位汉字组成！");
        } else {
            ModifySuccCss("u_name_tip");
        }
    } else if (sType == "1") {
        if (!exp2.test(name)) {
            ModifyTipCss("u_name_tip", "开户名由2-30位汉字组成！");
        } else {
            ModifySuccCss("u_name_tip");
        }
    }
}

//验证开户行全称
function CheckBankName() {
    var bname = $("#u_bankname").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(bname)) {
        ModifySuccCss("u_bankname_tip");
    } else {
        ModifyTipCss("u_bankname_tip", "开户行全称长度为5-30！");
    }
}

//验证手续费
function CheckRate() {
    var is_sxf = $("#is_poundage").attr("checked");
    if (is_sxf) {
        var u_poundage = $("#u_poundage").val();
        var rex = /^\d{1}(\.\d{1,4})?$/;
        var isNull = isNotNull(u_poundage);
        var isRate = rex.test(u_poundage);
        if (isNull || isRate) {
            if (isNull) {
                var u_poundage = $("#u_poundage").val("0.00");
            }
            ModifySuccCss("u_poundage_tip");
        } else {
            if (!isRate) {
                ModifyTipCss("u_poundage_tip", "最大1位整数或者保留四位小数！");
            }
        }
    }
}

//验证企业法人
function Checkqyfr() {

    var rname = $("#BusinessEntity").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

    if (!exp1.test(rname)) {
        ModifyTipCss("BusinessEntity_tip", "法人姓名由1-6位汉字组成！");
    } else {
        ModifySuccCss("BusinessEntity_tip");
    }
}

//验证企业注册地址
function Checkqyzcaddress() {
    var addre = $("#RegisteredAddress").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(addre)) {
        ModifySuccCss("RegisteredAddress_tip");
    } else {
        ModifyTipCss("RegisteredAddress_tip", "企业注册地址长度为5-30！");
    }
}


//批量更新
function doAll(obj) {
    var vals = "";
    $("#table").find('input[type="checkbox"]:checked').each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    $.post("/AppUser/DoAll", { uids: vals, tag: obj }, function (result) {
        if (result.success == 1) {
            window.parent.location.reload();
            window.parent.ShowMsg(result.msg, "ok", function () {
                window.parent.layer.closeAll();
            });
        } else if (result.success == 9998) {
            window.parent.ShowMsg(result.msg, "error", "");
            return;
        } else if (result.success == 9999) {
            window.parent.ShowMsg(result.msg, "error", "");
            window.top.location.href = retJson.Redirect;
            return;
        } else {
            window.parent.ShowMsg(result.msg, "error", "");
            return;
        }
    });
}

//新增页面弹窗
function AddDlg() {
    window.parent.ShouwDiaLogWan("新增用户", 750, 632, "/AppUser/AppUserAdd");
}

//保存用户（新增）
function SaveUser() {
    $("#btn-save-add").attr("disabled", "disabled");
    //表单数据
    var fd = GetFormData("frm-user-add");
    var tmsg = "";
    //验证所属上级姓名
    var relationId = fd.relation_person_id;
    var isnameNull = isRequestNotNull(relationId);//是否为空
    if (isnameNull) {
        ModifyTipCss("relation_person_id_tip", "请选择所属上级！");
        tmsg += "请选择所属上级！";
    }

    //验证邮箱
    var email = fd.u_email;
    var isMailNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (!isMailNull && isMail) {
        var data = { cval: email, uid: $("#u_id").val() };
        $.ajax({
            type: "post",
            url: "/AppUser/CheckEmail",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    ModifyTipCss("u_email_tip", "已存在该邮箱地址！");
                    tmsg += "已存在该邮箱地址！";
                } else {

                }
            }
        });

    } else if (isMailNull) {
        ModifyTipCss("u_email_tip", "请输入邮箱地址！");
        tmsg += "请输入邮箱地址！";
    } else if (!isMail) {
        ModifyTipCss("u_email_tip", "邮箱地址格式不正确！");
        tmsg += "邮箱地址格式不正确！";
    }

    //验证密码
    var pwd = fd.u_password;
    if (pwd.length < 6) {
        ModifyTipCss("u_password_tip", "密码不能小于6位！");
        tmsg += "密码不能小于6位！";
    }

    //验证联系电话
    var phone = fd.u_phone;
    if (!isMobile(phone)) {
        ModifyTipCss("u_phone_tip", "联系电话为11位手机号！");
        tmsg += "11位手机号！";
    }

    //验证QQ号
    var qq = fd.u_qq;
    if (!isQQ(qq)) {
        ModifyTipCss("u_qq_tip", "纯数字组成，5-16位之间！");
        tmsg += "纯数字组成，5-16位之间！";
    }

    //验证联系地址
    var addre = fd.u_address;
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!rexp.test(addre)) {
        ModifyTipCss("u_address_tip", "联系地址长度为5-30！");
        tmsg += "联系地址长度为5-30！";
    }

    //验证证件号码
    var stype = $("#u_category").val();
    fd.u_category = stype;//认证类型
    var rname = fd.u_realname;//真实姓名或公司名称
    var name = fd.u_name;//开户名
    var imgurl = "";//证件照片

    //验证身份证号
    var idno = fd.u_idnumber;
    var isIdnoNull = isRequestNotNull(idno);//是否为空
    var isIdno = new clsIDCard(idno).Valid;//格式是否正确
    if (!isIdnoNull && isIdno) {
        var data = { cval: idno, uid: $("#u_id").val() };

        $.ajax({
            type: "post",
            url: "/AppUser/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    ModifyTipCss("u_idnumber_tip", "已存在该身份证号！");
                    tmsg += "已存在该身份证号！";
                } else {

                }
            }
        });

    } else if (isIdnoNull) {
        ModifyTipCss("u_idnumber_tip", "请输入身份证号！");
        tmsg += "请输入身份证号！";
    } else if (!isIdno) {
        ModifyTipCss("u_idnumber_tip", "身份证号格式不正确！");
        tmsg += "身份证号格式不正确！";
    }

    if (stype == "0") {
        fd.u_blicensenumber = "";
       

        //验证真实名称
        var exprname1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprname1.test(rname)) {
            ModifyTipCss("u_realname_tip", "真实姓名由1-6位汉字组成！");
            tmsg += "真实姓名由1-6位汉字组成！";
        }

        //验证开户名
        var exprbnmae1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprbnmae1.test(name)) {
            ModifyTipCss("u_name_tip", "开户名由1-6位汉字组成！");
            tmsg += "开户名由1-6位汉字组成！";
        }

        //验证证件照片
        imgurl = fd.u_photo;
        fd.u_blicense = "";
    } else {;

        //验证营业执照
        var yyzz = fd.u_blicensenumber;
        var yyzzExrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isYyzzNull = isRequestNotNull(yyzz);//是否为空
        var isYyzz = yyzzExrp.test(yyzz);//格式是否正确
        if (!isYyzzNull && isYyzz) {
            var data = { cval: yyzz, uid: $("#u_id").val() };

            $.ajax({
                type: "post",
                url: "/AppUser/CheckYyzz",
                cache: false,
                async: false,
                dataType: "json",
                data: data,
                success: function (msg) {
                    if (msg.success) {
                        ModifyTipCss("u_blicensenumber_tip", "已存在该营业执照！");
                        tmsg += "已存在该营业执照！";
                    } else {

                    }
                }
            });

        } else if (isYyzzNull) {
            ModifyTipCss("u_blicensenumber_tip", "请输入营业执照！");
            tmsg += "请输入营业执照！";
        } else if (!isYyzz) {
            ModifyTipCss("u_blicensenumber_tip", "由15或18位数字和字母组成！");
            tmsg += "营业执照由15或18位数字和字母组成！";
        }

        //验证法人
        var BusinessEntity = fd.BusinessEntity;
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exp1.test(BusinessEntity)) {
            ModifyTipCss("BusinessEntity_tip", "法人姓名由1-6位汉字组成！");
            tmsg += "法人姓名由1-6位汉字组成！";
        }

        //验证企业注册地址
        var RegisteredAddress = fd.RegisteredAddress;
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (!rexp.test(RegisteredAddress)) {
            ModifyTipCss("RegisteredAddress_tip", "企业注册地址长度为5-30！");
            tmsg += "企业注册地址长度为5-30！";
        }

        //验证公司名称
        var exprname2 = /^[\u4E00-\u9FA5]{3,30}$/;
        if (!exprname2.test(rname)) {
            ModifyTipCss("u_realname_tip", "公司名称由3-30位汉字组成！");
            tmsg += "公司名称由8-30位汉字组成！";
        }

        //验证开户名
        var exprbnmae2 = /^[\u4E00-\u9FA5]{2,30}$/;
        if (!exprbnmae2.test(name)) {
            ModifyTipCss("u_name_tip", "开户名由2-30位汉字组成！");
            tmsg += "开户名由8-30位汉字组成！";
        }

        //验证证件照片
        imgurl = fd.u_photo;
        //营业执照
        imgurlSfz = fd.u_blicense;
    }

    //验证开户账号
    var khzh = fd.u_account;
    var yzkhzh = /^\d{1,30}$/;
    var yzkhzh1 = /^0?\d{9,11}|(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
    var isBankNull = isRequestNotNull(khzh);//是否为空
    var isKhzh = yzkhzh.test(khzh);//格式是否正确
    if (!isBankNull && isKhzh) {
        var data = { cval: khzh, uid: $("#u_id").val() };

        $.ajax({
            type: "post",
            url: "/AppUser/CheckBankNo",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    ModifyTipCss("u_account_tip", "已存在该开户账号！");
                    tmsg += "已存在该开户账号！";
                } else {

                }
            }
        });

    } else if (isBankNull) {
        ModifyTipCss("u_account_tip", "请输入开户账号！");
        tmsg += "请输入开户账号！";
    } else if (!isKhzh) {
        ModifyTipCss("u_account_tip", "开户账号格式不正确！");
        tmsg += "开户账号格式不正确！";
    }

    //验证开户行全称
    var bname = fd.u_bankname;
    var yzbn = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!yzbn.test(bname)) {
        ModifyTipCss("u_bankname_tip", "开户行全称长度为5-30！");
        tmsg += "开户行全称长度为5-30！";
    }

    //获取证件照片的路径
    var isImgNull = isRequestNotNull(imgurl);
    var isImg = CheckImgExtName(imgurl);
    if (isImgNull) {
        ModifyTipCss("u_photo_tip", "请选择证件图片！");
        tmsg += "请选择证件图片！";
    } else if (!isImg) {
        ModifyTipCss("u_photo_tip", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        tmsg += "请选择正确格式的证件图片！";
    }
    if (stype == "1") {
        //获取企业身份证上传
        var isImgNull = isRequestNotNull(imgurlSfz);
        var isImg = CheckImgExtName(imgurlSfz);
        if (isImgNull) {
            ModifyTipCss("sfzficateyy", "请选择证件图片！");
            tmsg += "请选择证件图片！";
        } else if (!isImg) {
            ModifyTipCss("sfzficateyy", "请选择证件图片（.jpg.png.bmp.jpeg）！");
            tmsg += "请选择正确格式的证件图片！";
        }
    }

    if (tmsg == "") {

        //获取隐藏控件
        fd.u_state = 1;
        fd.u_topid = 0;
        //fd.u_poundage = 0;
        fd.u_count = 0;

        fd.relation_type = $("#relation_type").val();

        fd.u_category = $("#u_category").val();


        fd.relation_person_id = $("#relation_person_id").val();

        fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";

        $.post("/AppUser/AddUser", fd, function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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
            $("#btn-save-add").removeAttr("disabled");
        });
    } else {

        $("#btn-save-add").removeAttr("disabled");
        return false;
    }

}

//编辑页面弹窗
function UpdateUser(obj) {
    window.parent.ShouwDiaLogWan("编辑用户", 750, 525, "/AppUser/AppUserEdit?uid=" + obj);
}

//保存用户（编辑）
function SaveUpdateUser() {
    $("#btn-save-edit").attr("disabled", "disabled");
    //表单数据
    var fd = GetFormData("frm-user-edit");
    var tmsg = "";

    //验证所属上级姓名
    var relationId = fd.relation_person_id;
    var isnameNull = isRequestNotNull(relationId);//是否为空
    if (isnameNull) {
        ModifyTipCss("relation_person_id_tip", "请选择所属上级！");
        tmsg += "请选择所属上级！";

    }

    //验证邮箱
    var email = fd.u_email;
    var isMailNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (!isMailNull && isMail) {
        var data = { cval: email, uid: $("#u_id").val() };
        $.ajax({
            type: "post",
            url: "/AppUser/CheckEmail",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    ModifyTipCss("u_email_tip", "已存在该邮箱地址！");
                    tmsg += "已存在该邮箱地址！";
                } else {

                }
            }
        });
    } else if (isMailNull) {
        ModifyTipCss("u_email_tip", "请输入邮箱地址！");
        tmsg += "请输入邮箱地址！";
    } else if (!isMail) {
        ModifyTipCss("u_email_tip", "邮箱地址格式不正确！");
        tmsg += "邮箱地址格式不正确！";
    }

    //验证密码
    var pwd = fd.u_password;
    if (pwd.length < 6) {
        ModifyTipCss("u_password_tip", "密码不能小于6位！");
        tmsg += "密码不能小于6位！";
    }

    //验证联系电话
    var phone = fd.u_phone;
    if (!isMobile(phone)) {
        ModifyTipCss("u_phone_tip", "联系电话为11位手机号！");
        tmsg += "11位手机号！";
    }


    //验证QQ号
    var qq = fd.u_qq;
    if (!isQQ(qq)) {
        ModifyTipCss("u_qq_tip", "纯数字组成，5-16位之间！");
        tmsg += "纯数字组成，5-16位之间！";
    }

    //验证联系地址
    var addre = fd.u_address;
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!rexp.test(addre)) {
        ModifyTipCss("u_address_tip", "联系地址长度为5-30！");
        tmsg += "联系地址长度为5-30！";
    }

    //验证证件号码
    var stype = $("#u_category").val();
    fd.u_category = stype;//认证类型
    var rname = fd.u_realname;//真实姓名或公司名称
    var name = fd.u_name;//开户名
    var imgurl = "";//证件照片

    //验证身份证号
    var idno = fd.u_idnumber;
    var isIdnoNull = isRequestNotNull(idno);//是否为空
    var isIdno = new clsIDCard(idno).Valid;//格式是否正确
    if (!isIdnoNull && isIdno) {
        var data = { cval: idno, uid: $("#u_id").val() };

        $.ajax({
            type: "post",
            url: "/AppUser/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    ModifyTipCss("u_idnumber_tip", "已存在该身份证号！");
                    tmsg += "已存在该身份证号！";
                } else {

                }
            }
        });
  
    } else if (isIdnoNull) {
        ModifyTipCss("u_idnumber_tip", "请输入身份证号！");
        tmsg += "请输入身份证号！";
    } else if (!isIdno) {
        ModifyTipCss("u_idnumber_tip", "身份证号格式不正确！");
        tmsg += "身份证号格式不正确！";
    }

    if (stype == "0") {
        fd.u_blicensenumber = "";
      
        //验证真实名称
        var exprname1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprname1.test(rname)) {
            ModifyTipCss("u_realname_tip", "真实姓名由1-6位汉字组成！");
            tmsg += "真实姓名由1-6位汉字组成！";
        }

        //验证开户名
        var exprbnmae1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprbnmae1.test(name)) {
            ModifyTipCss("u_name_tip", "开户名由1-6位汉字组成！");
            tmsg += "开户名由1-6位汉字组成！";
        }

        //验证证件照片
        imgurl = fd.u_photo;
        fd.u_blicense = "";
    } else {;

        //验证营业执照
        var yyzz = fd.u_blicensenumber;
        var yyzzExrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isYyzzNull = isRequestNotNull(yyzz);//是否为空
        var isYyzz = yyzzExrp.test(yyzz);//格式是否正确
        if (!isYyzzNull && isYyzz) {
            var data = { cval: yyzz, uid: $("#u_id").val() };

            $.ajax({
                type: "post",
                url: "/AppUser/CheckYyzz",
                cache: false,
                async: false,
                dataType: "json",
                data: data,
                success: function (msg) {
                    if (msg.success) {
                        ModifyTipCss("u_blicensenumber_tip", "已存在该营业执照！");
                        tmsg += "已存在该营业执照！";
                    } else {

                    }
                }
            });

        } else if (isYyzzNull) {
            ModifyTipCss("u_blicensenumber_tip", "请输入营业执照！");
            tmsg += "请输入营业执照！";
        } else if (!isYyzz) {
            ModifyTipCss("u_blicensenumber_tip", "由15或18位数字和字母组成！");
            tmsg += "营业执照由15或18位数字和字母组成！";
        }

        //验证法人
        var BusinessEntity = fd.BusinessEntity;
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exp1.test(BusinessEntity)) {
            ModifyTipCss("BusinessEntity_tip", "法人姓名由1-6位汉字组成！");
            tmsg += "法人姓名由1-6位汉字组成！";
        }

        //验证企业注册地址
        var RegisteredAddress = fd.RegisteredAddress;
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (!rexp.test(RegisteredAddress)) {
            ModifyTipCss("RegisteredAddress_tip", "企业注册地址长度为5-30！");
            tmsg += "企业注册地址长度为5-30！";
        }

        //验证公司名称
        var exprname2 = /^[\u4E00-\u9FA5]{3,30}$/;
        if (!exprname2.test(rname)) {
            ModifyTipCss("u_realname_tip", "公司名称由3-30位汉字组成！");
            tmsg += "公司名称由8-30位汉字组成！";
        }

        //    //验证开户名
        var exprbnmae2 = /^[\u4E00-\u9FA5]{2,30}$/;
        if (!exprbnmae2.test(name)) {
            ModifyTipCss("u_name_tip", "开户名由2-30位汉字组成！");
            tmsg += "开户名由8-30位汉字组成！";
        }

        //    //验证证件照片
        imgurl = fd.u_blicense;
        imgurlSfz = fd.sfzu_photo;
        fd.u_photo = imgurlSfz;
    }

    //验证开户账号
    var khzh = fd.u_account;
    var yzkhzh = /^\d{1,30}$/;
    var yzkhzh1 = /^0?\d{9,11}|(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
    var isBankNull = isRequestNotNull(khzh);//是否为空
    var isKhzh = yzkhzh.test(khzh);//格式是否正确
    if (!isBankNull && isKhzh) {
        var data = { cval: khzh, uid: $("#u_id").val() };

        $.ajax({
            type: "post",
            url: "/AppUser/CheckBankNo",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    ModifyTipCss("u_account_tip", "已存在该开户账号！");
                    tmsg += "已存在该开户账号！";
                } else {

                }
            }
        });

       
    } else if (isBankNull) {
        ModifyTipCss("u_account_tip", "请输入开户账号！");
        tmsg += "请输入开户账号！";
    } else if (!isKhzh) {
        ModifyTipCss("u_account_tip", "开户账号格式不正确！");
        tmsg += "开户账号格式不正确！";
    }

    //验证开户行全称
    var bname = fd.u_bankname;
    var yzbn = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!yzbn.test(bname)) {
        ModifyTipCss("u_bankname_tip", "开户行全称长度为5-30！");
        tmsg += "开户行全称长度为5-30！";
    }

    //获取证件照片的路径
    var isImgNull = isRequestNotNull(imgurl);
    var isImg = CheckImgExtName(imgurl);
    if (isImgNull) {
        ModifyTipCss("u_photo_tip", "请选择证件图片！");
        tmsg += "请选择证件图片！";
    } else if (!isImg) {
        ModifyTipCss("u_photo_tip", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        tmsg += "请选择正确格式的证件图片！";
    }
    if (stype == "1") {
        //获取企业身份证上传
        var isImgNull = isRequestNotNull(imgurlSfz);
        var isImg = CheckImgExtName(imgurlSfz);
        if (isImgNull) {
            ModifyTipCss("sfzficateyy", "请选择证件图片！");
            tmsg += "请选择证件图片！";
        } else if (!isImg) {
            ModifyTipCss("sfzficateyy", "请选择证件图片（.jpg.png.bmp.jpeg）！");
            tmsg += "请选择正确格式的证件图片！";
        }
    }
    if (tmsg == "") {

      
        fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";

        fd.relation_type = $("#relation_type").val();

        fd.u_category = $("#u_category").val();


        fd.relation_person_id = $("#relation_person_id").val();


        $.post("/AppUser/UpdateUser", fd, function (retJson) {
            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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
            $("#btn-save-edit").removeAttr("disabled");

        });
    } else {
        $("#btn-save-edit").removeAttr("disabled");
        return false;

    }
}

//手续费设置
function getsxf(obj) {
    window.parent.ShouwDiaLogWan("手续费设置", 500, 310, "/AppUser/UserSxF?userid=" + obj);
}
//设置扣量
function getkl(obj) {
    window.parent.ShouwDiaLogWan("扣量设置", 800, 510, "/AppUser/UserKl?userid=" + obj);
}
//审核数据
function getAuditing(obj) {
    window.parent.ShouwDiaLogWan("审核数据", 450, 200, "/AppUser/AppUserAuditing?userid=" + obj);
}

//获取表单数据
//fid:表单id
function GetFormData(fid) {
    var data = {};
    $("#" + fid).find("input").each(function (index) {
        if (this.name != "") {
            data[this.name] = $(this).val();
        }
    });
    return data;
}


//修改验证失败提示样式
//tid:提示控件id
//content:提示内容
function ModifyTipCss(tid, content) {
    $("#" + tid).attr("class", "Validform_checktip Validform_wrong");
    $("#" + tid).html(content);
}

//修改验证成功提示样式
//tid:提示控件id
function ModifySuccCss(tid) {
    $("#" + tid).attr("class", " Validform_right");
    $("#" + tid).html("通过信息认证！");
}

//file控件值有改变就上传
function FileChange() {
    var filedata = $("#certificatefile").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload();
        } else {
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            ModifyTipCss("u_photo_tip", "请选择证件图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ModifyTipCss("u_photo_tip", "请选择证件图片！");
    }
}

//上传身份证图片
function ajaxUpload() {

    iurl = $("#u_photo").val();
    $.ajaxFileUpload({
        url: '/AppUser/UploadImg',
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
        url: '/AppUser/UploadImgsfz',
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


function AssignToMerchant() {
    var target = $(this);
    var uids = $("#txt_uids").val();

    var mid = $("#a_user_id").val();

    if (mid === "") {
        window.parent.ShowMsg("请选择要指定的商务！", "error", "");
        return;
    }
    $.post("/appuser/assigntomerchant", { uid: uids, mid: mid }, function (retJson) {

        if (retJson.success == 1) {
            window.parent.location.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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

    });
}

$(function () {
    $(".btn-merchant-assign").click(function () {
        $('.btn-merchant-assign').removeClass("selected");
        $(this).addClass("selected");
        $("#txt_mid").val($(this).data("mid"));
    });
});


//选择商务或者代理商
function relation_type_add() {
    var relation_type = $.trim($("#relation_type").val());
    switch (relation_type) {
        case "2":
            $("#l_riskyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_riskyy").html("验证通过");
            window.parent.ShouwDiaLogWan("选择代理商", 1000, 700, "/AppUser/AppUserAddTc");
            break;
        case "1":
            $("#l_riskyy").attr("class", "Validform_checktip  Validform_right");
            $("#l_riskyy").html("验证通过");
            window.parent.ShouwDiaLogWan("选择商务", 1000, 700, "/AppUser/AppUserAddBpTc");
            break;
    }
}


//选择代理商
function selectxzAppUserAgent(obj, obj1, index) {

    if (obj == null || obj == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    window.parent.layer.getChildFrame("#relation_person_name", index).val(obj1);
    window.parent.layer.getChildFrame("#relation_person_id", index).val(obj);
    window.parent.layer.getChildFrame("#relation_person_id_tip", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#relation_person_id_tip", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}


//审核
function UserAuditstate() {

    var auditstate = $('input[name="u_auditstate"]:checked').val();

    var id = $("#userid").val();

    var url = "/AppUser/CheckAuditing";
    var data = { u_auditstate: auditstate, userid: id };

    $.post(url, data, function (retJson) {

        if (retJson.success == 1) {
            window.parent.location.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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


//特批弹窗页面
function gettepi(obj) {
    window.parent.ShouwDiaLogWan("开发者特批", 450, 300, "/AppUser/AppUserTePi?userid=" + obj);
}

//特批方法
function UserUpdateTePi() {
    var url = "/AppUser/AppUserUpdateTePi";
    var data = $(".plate-form").serialize();

    $.post(url, data, function (retJson) {

        if (retJson.success == 1) {
            window.parent.location.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
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