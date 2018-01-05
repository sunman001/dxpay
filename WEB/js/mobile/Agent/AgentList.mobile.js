//分页
function LoadData(pageIndex, pageSize) {
    var url = "/Agent/AgentList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;

    var s_type = $("#s_type").val();
    var s_keys = $("#s_keys").val();
    var status = $("#status").val();
    var AuditState = $("#AuditState").val();
    var searchDesc = $("#searchDesc").val();

    url += "&stype=" + s_type + "&skeys=" + s_keys + "&status=" + status + "&AuditState=" + AuditState + "&searchDesc=" + searchDesc;

    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    LoadData(1, PageSize);
}

//列表查询
function selectBusinessPersonnel() {//查询
    var PageSize = $("#pagexz").val();
    LoadData(1, PageSize);
}

//修改状态
function UpdateState(id, state) {
    if (id === "") {
        window.parent.ShowMsg("请选择合作信息！", "error", "");
        return;
    }
    var url = "/Agent/UpdateState";
    var data = {
        state: state, id: id
    };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            window.parent.location.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () {
                window.parent.layer.closeAll();
            });
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



//新增页面弹窗
function AddAgents() {
    window.parent.ShouwDiaLogWan("新增代理商", 750, 632, "/Agent/AgentAdd");
}

//保存代理商（新增）
function SaveAgent() {


    //表单数据
    var fd = GetFormData("frm-user-add");
    var tmsg = "";
    var stype = $("#u_category").val();

    var relation_person_id = $("#relation_person_id").val();

    if (relation_person_id == "") {

        $("#relation_person_id_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#relation_person_id_tip").html("请选择商务!");
        return false;
    }

    var LoginName = fd.LoginName;

    if (isRequestNotNull(LoginName)) {

        $("#LoginName_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#LoginName_tip").html("请填写登录名称!");
        return false;
    } else {
        var data = { lname: LoginName, uid: $("#id").val() };
        $.ajax({
            type: "post",
            url: "/Agent/CheckLoName",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#LoginName_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#LoginName_tip").html("已存在该登录名称！");
                    tmsg += "已存在该登录名称！";
                } else {
                    $("#LoginName_tip").attr("class", "Validform_checktip Validform_right");
                    $("#LoginName_tip").html("验证通过！");
                }
            }
        });
    }

    //验证密码
    var pwd = fd.Password;
    if (pwd.length < 6) {

        $("#Password_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#Password_tip").html("密码不能小于6位！");
        return false;
    }


    //用户姓名
    var rname = fd.DisplayName;

    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    if (stype == "0") {
        if (!exp1.test(rname)) {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#DisplayName_tip").html("真实姓名由1-6位汉字组成！");
            return false;

        }
    } else if (stype == "1") {
        if (!exp2.test(rname)) {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#DisplayName_tip").html("公司名称由3-30位汉字组成！");
            return false;


        }
    }


    //验证邮箱
    var email = fd.EmailAddress;

    var isNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (!isNull && isMail) {
        var data = { cval: email, uid: $("#id").val() };

        $.ajax({
            type: "post",
            url: "/Agent/CheckEmail",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#EmailAddress_tip").html("已存在该邮箱地址！");
                    tmsg += "已存在该邮箱地址！";
                } else {
                    $("#LoginName_tip").attr("class", "Validform_checktip Validform_right");
                    $("#LoginName_tip").html("验证通过！");
                }
            }
        });

    } else if (isNull) {

        $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#EmailAddress_tip").html("请输入邮箱地址！");
        return false;


    } else if (!isMail) {

        $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#EmailAddress_tip").html("邮箱地址格式不正确！");

        return false;
    }

    //验证联系电话
    var phone = fd.MobilePhone;
    if (!isMobileOrPhone(phone)) {

        $("#MobilePhone_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#MobilePhonetip").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }

    //验证QQ号
    var qq = fd.QQ;

    if (isQQ(qq)) {

        $("#QQ_tip").attr("class", "Validform_checktip Validform_right");
        $("#QQ_tip").html("验证通过！");

    } else {

        $("#QQ_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#QQ_tip").html("纯数字组成，5-16位之间！");
        return false;
    }

    //网站
    var Website = fd.Website;

    //验证联系地址
    var addre = fd.ContactAddress;

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(addre)) {

        $("#ContactAddress_tip").attr("class", "Validform_checktip Validform_right");
        $("#ContactAddress_tip").html("验证通过！");

    } else {

        $("#ContactAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#ContactAddress_tip").html("联系地址长度为5-30！");
        return false;
    }


    //验证证件号码

    fd.Classify = stype;//认证类型
    var BankAccountName = fd.BankAccountName;//开户名
    var imgurl = "";//证件照片
    var imgurlSfz = "";

    //验证身份证号
    var idno = fd.IDCardNumber;

    var isNull = isRequestNotNull(idno);//是否为空    
    var isIdno = new clsIDCard(idno).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: idno, uid: $("#id").val() };

        $.ajax({
            type: "post",
            url: "/Agent/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#IDCardNumber_tip").html("已存在该身份证号！");
                    tmsg += "已存在该身份证号！";
                } else {
                    $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_right");
                    $("#IDCardNumber_tip").html("验证通过！");
                }
            }
        });

    } else if (isNull) {

        $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#IDCardNumber_tip").html("请输入身份证号！");
        return false;

    } else if (!isIdno) {

        $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#IDCardNumber_tip").html("身份证号格式不正确！");
        return false;
    }

    if (stype == "0") {

        //验证证件照片
        imgurl = $("#u_photo").val();

    }
    else {
        //验证营业执照编号
        var yyzz = $("#BusinessLicenseNumber").val();
        fd.BusinessLicenseNumber = yyzz;

        var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isNull = isRequestNotNull(yyzz);//是否为空
        var isYyzz = exrp.test(yyzz);//格式是否正确
        if (!isNull && isYyzz) {
            var data = { cval: yyzz, uid: $("#id").val() };

            $.ajax({
                type: "post",
                url: "/Agent/CheckYyzz",
                cache: false,
                async: false,
                dataType: "json",
                data: data,
                success: function (msg) {
                    if (msg.success) {
                        $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
                        $("#BusinessLicensePhotoPath_tip").html("已存在该营业执照！");
                        tmsg += "已存在该营业执照！";
                    } else {
                        $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_right");
                        $("#BusinessLicensePhotoPath_tip").html("验证通过！");
                    }
                }
            });

        } else if (isNull) {

            $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BusinessLicensePhotoPath_tip").html("请输入营业执照！");
            return false;

        } else if (!isYyzz) {

            $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BusinessLicensePhotoPath_tip").html("由15或18位数字和字母组成！");
            return false;
        }

        //验证证件照片
        imgurl = $("#u_photo").val();
        //营业执照
        imgurlSfz = $("#u_blicense").val();
    }

    //获取证件照片的路径
    var isImgNull = isRequestNotNull(imgurl);
    var isImg = CheckImgExtName(imgurl);
    if (isImgNull) {

        $("#u_photo_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#u_photo_tip").html("请选择证件图片！");
        tmsg += "请选择证件图片！";

    } else if (!isImg) {

        $("#u_photo_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#u_photo_tip").html("请选择证件图片（.jpg.png.bmp.jpeg）！");
        tmsg += "请选择证件图片（.jpg.png.bmp.jpeg）！";


    }
    else {
        $("#u_photo_tip").attr("class", "Validform_checktip Validform_right");
        $("#u_photo_tip").html("验证通过！");

    }
    if (stype == "1") {

        //获取企业身份证上传
        var isImgNull = isRequestNotNull(imgurlSfz);
        var isImg = CheckImgExtName(imgurlSfz);
        if (isImgNull) {

            $("#sfzficateyy").attr("class", "Validform_checktip Validform_wrong");
            $("#sfzficateyy").html("请选择证件图片！");
            tmsg += "请选择证件图片！";

        } else if (!isImg) {

            $("#sfzficateyy").attr("class", "Validform_checktip Validform_wrong");
            $("#sfzficateyy").html("请选择证件图片（.jpg.png.bmp.jpeg）！");
            tmsg += "请选择证件图片（.jpg.png.bmp.jpeg）！";
        }
        else {
            $("#sfzficateyy").attr("class", "Validform_checktip Validform_right");
            $("#sfzficateyy").html("验证通过！");

        }
    }


    //验证开户账号
    var khzh = fd.BankAccount;

    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isNull = isRequestNotNull(khzh);//是否为空
    var isYyzz = rexp.test(khzh);//格式是否正确
    if (!isNull && isYyzz) {
        var data = { cval: khzh, uid: $("#id").val() };

        $.ajax({
            type: "post",
            url: "/Agent/CheckBankNo",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {

                    $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#BankAccount_tip").html("已存在该开户账号！");
                    tmsg += "已存在该开户账号！";

                } else {
                    $("#BankAccount_tip").attr("class", "Validform_checktip Validform_right");
                    $("#BankAccount_tip").html("验证通过！");
                }
            }
        });

    } else if (isNull) {

        $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankAccount_tip").html("请输入开户账号！");
        return false;

    } else if (!isYyzz) {

        $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankAccount_tip").html("开户账号格式不正确！");
        return false;

    }

    //验证开户行全称
    var bname = fd.BankFullName;

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(bname)) {

        $("#BankFullName_tip").attr("class", "Validform_checktip Validform_right");
        $("#BankFullName_tip").html("验证通过！");

    } else {

        $("#BankFullName_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankFullName_tip").html("开户行全称长度为5-30！");
        return false;
    }

    fd.PersonalPhotoPath = $("#u_photo").val();
    fd.BusinessLicensePhotoPath = $("#u_blicense").val();
    //获取隐藏控件
    fd.State = 0;//状态[0:正常,1:冻结]
    fd.LoginCount = 0;//登录次数
    fd.AuditState = 0;//审核状态：-1 未通过 0 等待审核
    //所属商务信息
    fd.OwnerId = $("#relation_person_id").val();
    fd.OwnerName = $("#relation_person_names").val();

    fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";

    $("#btn-save-add").attr("disabled", "disabled");
    if (tmsg == "") {
        $.post("/Agent/InsertAgent", fd, function (retJson) {

            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () {
                    window.parent.layer.closeAll();
                });
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
    }
    else {
        $("#btn-save-add").removeAttr("disabled");
        return false;
    }


}


//修改代理商
function AgentUpdate(id) {

    if (id > 0) {

        window.parent.ShouwDiaLogWan("修改代理商", 750, 632, "/Agent/AgentUpdate?id=" + id)
    }
    else {
        window.parent.ShowMsg("请选择要修改的代理商！", "error", "");
        return;
    }

}

//审核代理商
function AgentshAuditState(id) {

    if (id > 0) {

        window.parent.ShouwDiaLogWan("审核代理商", 450, 200, "/Agent/AgentAuditing?userid=" + id)
    }
    else {
        window.parent.ShowMsg("请选择要审核的代理商！", "error", "");
        return;
    }

}

//修改代理商
function SaveUpdateAgent() {
    //表单数据
    var fd = GetFormData("frm-user-add");
    var tmsg = "";
    var stype = $("#u_category").val();
    var LoginName = fd.LoginName


    var relation_person_id = $("#relation_person_id").val();

    if (relation_person_id == "") {

        $("#relation_person_id_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#relation_person_id_tip").html("请选择商务!");
        return false;
    }


    if (isRequestNotNull(LoginName)) {

        $("#LoginName_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#LoginName_tip").html("请填写登录名称!");
        return false;
    } else {
        var data = { lname: LoginName, uid: $("#id").val() };
        $.ajax({
            type: "post",
            url: "/Agent/CheckLoName",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#LoginName_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#LoginName_tip").html("已存在该登录名称！");
                    tmsg += "已存在该登录名称！";
                } else {
                    $("#LoginName_tip").attr("class", "Validform_checktip Validform_right");
                    $("#LoginName_tip").html("验证通过！");
                }
            }
        });
    }



    //验证密码
    var pwd = fd.Password;
    if (pwd.length < 6) {

        $("#Password_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#Password_tip").html("密码不能小于6位！");
        return false;
    }


    //用户姓名
    var rname = fd.DisplayName;

    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;

    if (stype == "0") {

        if (!exp1.test(rname)) {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#DisplayName_tip").html("真实姓名由1-6位汉字组成！");
            return false;

        }
    } else if (stype == "1") {
        if (!exp2.test(rname)) {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#DisplayName_tip").html("公司名称由3-30位汉字组成！");
            return false;


        }
    }

    var email = fd.EmailAddress;

    var isNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (!isNull && isMail) {
        var data = { cval: email, uid: $("#id").val() };

        $.ajax({
            type: "post",
            url: "/Agent/CheckEmail",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#EmailAddress_tip").html("已存在该邮箱地址！");
                    tmsg += "已存在该邮箱地址！";
                } else {
                    $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_right");
                    $("#EmailAddress_tip").html("验证通过！");
                }
            }
        });

    } else if (isNull) {

        $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#EmailAddress_tip").html("请输入邮箱地址！");
        return false;


    } else if (!isMail) {

        $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#EmailAddress_tip").html("邮箱地址格式不正确！");

        return false;
    }

    //验证联系电话
    var phone = fd.MobilePhone;
    if (!isMobileOrPhone(phone)) {

        $("#MobilePhone_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#MobilePhonetip").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }

    //验证QQ号
    var qq = fd.QQ;

    if (isQQ(qq)) {

        $("#QQ_tip").attr("class", "Validform_checktip Validform_right");
        $("#QQ_tip").html("验证通过！");

    } else {

        $("#QQ_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#QQ_tip").html("纯数字组成，5-16位之间！");
        return false;
    }

    //网站
    var Website = fd.Website;

    //验证联系地址
    var addre = fd.ContactAddress;

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(addre)) {

        $("#ContactAddress_tip").attr("class", "Validform_checktip Validform_right");
        $("#ContactAddress_tip").html("验证通过！");

    } else {

        $("#ContactAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#ContactAddress_tip").html("联系地址长度为5-30！");
        return false;
    }


    //验证证件号码

    fd.Classify = stype;//认证类型
    var BankAccountName = fd.BankAccountName;//开户名
    var imgurl = "";//证件照片
    var imgurlSfz = "";

    //验证身份证号
    var idno = fd.IDCardNumber;

    var isNull = isRequestNotNull(idno);//是否为空    
    var isIdno = new clsIDCard(idno).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: idno, uid: $("#id").val() };

        $.ajax({
            type: "post",
            url: "/Agent/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#IDCardNumber_tip").html("已存在该身份证号！");
                    tmsg += "已存在该身份证号！";
                } else {
                    $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_right");
                    $("#IDCardNumber_tip").html("验证通过！");
                }
            }
        });

    } else if (isNull) {

        $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#IDCardNumber_tip").html("请输入身份证号！");
        return false;

    } else if (!isIdno) {

        $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#IDCardNumber_tip").html("身份证号格式不正确！");
        return false;
    }

    if (stype == "0") {

        //验证证件照片
        imgurl = $("#u_photo").val();

    }
    else {
        //验证营业执照编号
        var yyzz = $("#BusinessLicenseNumber").val();
        fd.BusinessLicenseNumber = yyzz;

        var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isNull = isRequestNotNull(yyzz);//是否为空
        var isYyzz = exrp.test(yyzz);//格式是否正确
        if (!isNull && isYyzz) {
            var data = { cval: yyzz, uid: $("#id").val() };

            $.ajax({
                type: "post",
                url: "/Agent/CheckYyzz",
                cache: false,
                async: false,
                dataType: "json",
                data: data,
                success: function (msg) {
                    if (msg.success) {
                        $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
                        $("#BusinessLicensePhotoPath_tip").html("已存在该营业执照！");
                        tmsg += "已存在该营业执照！";
                    } else {
                        $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_right");
                        $("#BusinessLicensePhotoPath_tip").html("验证通过！");
                    }
                }
            });

        } else if (isNull) {

            $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BusinessLicensePhotoPath_tip").html("请输入营业执照！");
            return false;

        } else if (!isYyzz) {

            $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BusinessLicensePhotoPath_tip").html("由15或18位数字和字母组成！");
            return false;
        }

        //验证证件照片
        imgurl = $("#u_photo").val();
        //营业执照
        imgurlSfz = $("#u_blicense").val();
    }

    //获取证件照片的路径
    var isImgNull = isRequestNotNull(imgurl);
    var isImg = CheckImgExtName(imgurl);
    if (isImgNull) {

        $("#u_photo_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#u_photo_tip").html("请选择证件图片！");
        tmsg += "请选择证件图片！";

    } else if (!isImg) {

        $("#u_photo_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#u_photo_tip").html("请选择证件图片（.jpg.png.bmp.jpeg）！");
        tmsg += "请选择证件图片（.jpg.png.bmp.jpeg）！";


    }
    else {
        $("#u_photo_tip").attr("class", "Validform_checktip Validform_right");
        $("#u_photo_tip").html("验证通过！");

    }
    if (stype == "1") {

        //获取企业身份证上传
        var isImgNull = isRequestNotNull(imgurlSfz);
        var isImg = CheckImgExtName(imgurlSfz);
        if (isImgNull) {

            $("#sfzficateyy").attr("class", "Validform_checktip Validform_wrong");
            $("#sfzficateyy").html("请选择证件图片！");
            tmsg += "请选择证件图片！";

        } else if (!isImg) {

            $("#sfzficateyy").attr("class", "Validform_checktip Validform_wrong");
            $("#sfzficateyy").html("请选择证件图片（.jpg.png.bmp.jpeg）！");
            tmsg += "请选择证件图片（.jpg.png.bmp.jpeg）！";
        }
        else {
            $("#sfzficateyy").attr("class", "Validform_checktip Validform_right");
            $("#sfzficateyy").html("验证通过！");

        }
    }

    //验证开户人姓名
    var sType = $("#u_category").val();
    var name = $("#BankAccountName").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{2,30}$/;
    if (sType == "0") {
        if (!exp1.test(name)) {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BankAccountName_tip").html("开户名由1-6位汉字组成！");
            return false;

        } else {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_right");
            $("#BankAccountName_tip").html("验证通过！");

        }
    } else if (sType == "1") {
        if (!exp2.test(name)) {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BankAccountName_tip").html("开户名由2-30位汉字组成！");
            return false;

        } else {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_right");
            $("#BankAccountName_tip").html("验证通过！");

        }
    }

    //验证开户账号
    var khzh = fd.BankAccount;

    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isNull = isRequestNotNull(khzh);//是否为空
    var isYyzz = rexp.test(khzh);//格式是否正确
    if (!isNull && isYyzz) {
        var data = { cval: khzh, uid: $("#id").val() };

        $.ajax({
            type: "post",
            url: "/Agent/CheckBankNo",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
                    $("#BankAccount_tip").html("已存在该开户账号！");
                    tmsg += "已存在该开户账号！";
                } else {
                    $("#BankAccount_tip").attr("class", "Validform_checktip Validform_right");
                    $("#BankAccount_tip").html("验证通过！");
                }
            }
        });

    } else if (isNull) {

        $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankAccount_tip").html("请输入开户账号！");
        return false;

    } else if (!isYyzz) {

        $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankAccount_tip").html("开户账号格式不正确！");
        return false;

    }

    //验证开户行全称
    var bname = fd.BankFullName;

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(bname)) {

        $("#BankFullName_tip").attr("class", "Validform_checktip Validform_right");
        $("#BankFullName_tip").html("验证通过！");

    } else {

        $("#BankFullName_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankFullName_tip").html("开户行全称长度为5-30！");
        return false;
    }


    //获取隐藏控件
    fd.PersonalPhotoPath = $("#u_photo").val();
    fd.BusinessLicensePhotoPath = $("#u_blicense").val();

    //所属商务信息
    fd.OwnerId = $("#relation_person_id").val();
    fd.OwnerName = $("#relation_person_names").val();


    fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";
    $("#btn-save-add").attr("disabled", "disabled");
    if (tmsg == "") {

        $.post("/Agent/UpdateAgents", fd, function (retJson) {


            if (retJson.success == 1) {
                window.parent.location.reload();
                window.parent.ShowMsg(retJson.msg, "ok", function () {
                    window.parent.layer.closeAll();
                });
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
    }
    else {
        $("#btn-save-add").removeAttr("disabled");
        return false;
    }


}


//批量更新
function Updatestate(obj) {
    var vals = "";
    $("#table :checkbox[checked]").each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    $.post("/Agent/CoAgentUpdate", { coid: vals, tag: obj }, function (result) {

        if (retJson.success == 1) {
            window.parent.location.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () {
                window.parent.layer.closeAll();
            });
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


//选择认证类型展示对应的输入项
function ChangeType(obj) {
    var stext = $(obj).find("option:selected").text();
    if (stext != "" || stext != null || stext != undefined) {
        if (stext == "个人") {

            $("#user_yyzz").hide();
            $("#DisplayName").parent().parent().parent().find("dt").text("真实姓名：");
            $("#DisplayName_tip").html("*必填且1-6位汉字组成！");
            $("#qysfz").hide();
        } else {

            $("#user_yyzz").show();
            $("#DisplayName").parent().parent().parent().find("dt").text("公司名称：");
            $("#DisplayName_tip").html("*必填且3-30位汉字组成！");
            $("#qysfz").show();
        }
    }
}

//验证登录名称
function CheckLoginName() {

    var LoginName = $("#LoginName").val();

    if (isRequestNotNull(LoginName)) {

        $("#LoginName_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#LoginName_tip").html("请填写登录名称!");
        return false;
    } else {

        var data = { lname: LoginName, uid: $("#id").val() };
        $.post("/Agent/CheckLoName", data, function (msg) {
            if (msg.success) {

                $("#LoginName_tip").attr("class", "Validform_checktip Validform_wrong");
                $("#LoginName_tip").html("已存在该登录名称！");
                return false;

            } else {
                $("#LoginName_tip").attr("class", "Validform_checktip Validform_right");
                $("#LoginName_tip").html("验证通过！");
            }
        });
    }
}

//验证邮箱
function CheckEmail() {
    var email = $("#EmailAddress").val();
    var isNull = isRequestNotNull(email);//是否为空
    var isMail = isEmail(email);//格式是否正确
    if (!isNull && isMail) {
        var data = { cval: email, uid: $("#id").val() };
        $.post("/Agent/CheckEmail", data, function (msg) {
            if (msg.success) {
                $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
                $("#EmailAddress_tip").html("已存在该邮箱地址！");
                return false;

            } else {
                $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_right");
                $("#EmailAddress_tip").html("验证通过！");
            }
        });
    } else if (isNull) {

        $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#EmailAddress_tip").html("请输入邮箱地址！");
        return false;


    } else if (!isMail) {

        $("#EmailAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#EmailAddress_tip").html("邮箱地址格式不正确！");

        return false;
    }
}

//验证密码
function CheckPwd() {
    var pwd = $("#Password").val();
    if (pwd.length < 6) {
        $("#Password_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#Password_tip").html("密码不能小于6位！");
        return false;
    } else {
        $("#Password_tip").attr("class", "Validform_checktip Validform_right");
        $("#Password_tip").html("验证通过！");
    }
}

//验证真实姓名
function CheckRName() {
    var sType = $("#u_category").val();
    var rname = $("#DisplayName").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    if (sType == "0") {
        if (!exp1.test(rname)) {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#DisplayName_tip").html("真实姓名由1-6位汉字组成！");
            return false;

        } else {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_right");
            $("#DisplayName_tip").html("验证通过！");

        }
    } else if (sType == "1") {
        if (!exp2.test(rname)) {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#DisplayName_tip").html("公司名称由3-30位汉字组成！");
            return false;


        } else {

            $("#DisplayName_tip").attr("class", "Validform_checktip Validform_right");
            $("#DisplayName_tip").html("验证通过！");

        }
    }
}

//验证联系电话
function CheckPhone() {
    var phone = $("#MobilePhone").val();
    if (isMobileOrPhone(phone)) {

        $("#MobilePhone_tip").attr("class", "Validform_checktip Validform_right");
        $("#MobilePhone_tip").html("验证通过！");

    } else {

        $("#MobilePhone_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#MobilePhone_tip").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }
}

//验证QQ号码
function CheckQQ() {
    var qq = $("#QQ").val();
    if (isQQ(qq)) {

        $("#QQ_tip").attr("class", "Validform_checktip Validform_right");
        $("#QQ_tip").html("验证通过！");

    } else {

        $("#QQ_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#QQ_tip").html("纯数字组成，5-16位之间！");
        return false;
    }
}

//验证联系地址
function CheckAddRess() {
    var addre = $("#ContactAddress").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(addre)) {

        $("#ContactAddress_tip").attr("class", "Validform_checktip Validform_right");
        $("#ContactAddress_tip").html("验证通过！");

    } else {

        $("#ContactAddress_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#ContactAddress_tip").html("联系地址长度为5-30！");
        return false;
    }
}

//验证身份证号
function CheckIdno() {
    var idno = $("#IDCardNumber").val();
    var isNull = isRequestNotNull(idno);//是否为空    
    var isIdno = new clsIDCard(idno).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: idno, uid: $("#id").val() };
        $.post("/Agent/CheckIdno", data, function (msg) {
            if (msg.success) {

                $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
                $("#IDCardNumber_tip").html("已存在该身份证号！");
                return false;


            } else {

                $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_right");
                $("#IDCardNumber_tip").html("验证通过！");
            }
        });
    } else if (isNull) {

        $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#IDCardNumber_tip").html("请输入身份证号！");
        return false;

    } else if (!isIdno) {

        $("#IDCardNumber_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#IDCardNumber_tip").html("身份证号格式不正确！");
        return false;

    }
}

//验证营业执照
function CheckYYZZ() {
    var yyzz = $("#BusinessLicenseNumber").val();
    var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
    var isNull = isRequestNotNull(yyzz);//是否为空
    var isYyzz = exrp.test(yyzz);//格式是否正确
    if (!isNull && isYyzz) {
        var data = { cval: yyzz, uid: $("#id").val() };
        $.post("/Agent/CheckYyzz", data, function (msg) {
            if (msg.success) {

                $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
                $("#BusinessLicensePhotoPath_tip").html("已存在该营业执照！");
                return false;

            } else {

                $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_right");
                $("#BusinessLicensePhotoPath_tip").html("验证通过！");

            }
        });
    } else if (isNull) {

        $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BusinessLicensePhotoPath_tip").html("请输入营业执照！");
        return false;

    } else if (!isYyzz) {

        $("#BusinessLicensePhotoPath_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BusinessLicensePhotoPath_tip").html("由15或18位数字和字母组成！");
        return false;

    }
}

//验证开户账号
function CheckAccount() {
    var khzh = $("#BankAccount").val();
    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isNull = isRequestNotNull(khzh);//是否为空
    var isYyzz = rexp.test(khzh);//格式是否正确
    if (!isNull && isYyzz) {
        var data = { cval: khzh, uid: $("#id").val() };
        $.post("/Agent/CheckBankNo", data, function (msg) {
            if (msg.success) {

                $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
                $("#BankAccount_tip").html("已存在该开户账号！");
                return false;

            } else {

                $("#BankAccount_tip").attr("class", "Validform_checktip Validform_right");
                $("#BankAccount_tip").html("验证通过！");
            }
        });
    } else if (isNull) {

        $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankAccount_tip").html("请输入开户账号！");
        return false;

    } else if (!isYyzz) {

        $("#BankAccount_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankAccount_tip").html("开户账号格式不正确！");
        return false;

    }
}

//验证开户名
function CheckName() {
    var sType = $("#u_category").val();
    var name = $("#BankAccountName").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{2,30}$/;
    if (sType == "0") {
        if (!exp1.test(name)) {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BankAccountName_tip").html("开户名由1-6位汉字组成！");
            return false;

        } else {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_right");
            $("#BankAccountName_tip").html("验证通过！");

        }
    } else if (sType == "1") {
        if (!exp2.test(name)) {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_wrong");
            $("#BankAccountName_tip").html("开户名由2-30位汉字组成！");
            return false;

        } else {

            $("#BankAccountName_tip").attr("class", "Validform_checktip Validform_right");
            $("#BankAccountName_tip").html("验证通过！");

        }
    }
}

//验证开户行全称
function CheckBankName() {
    var bname = $("#BankFullName").val();
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(bname)) {

        $("#BankFullName_tip").attr("class", "Validform_checktip Validform_right");
        $("#BankFullName_tip").html("验证通过！");

    } else {

        $("#BankFullName_tip").attr("class", "Validform_checktip Validform_wrong");
        $("#BankFullName_tip").html("开户行全称长度为5-30！");
        return false;
    }
}

///////////////////////////////////////////////////////////////////

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
    $("#" + tid).attr("class", "Validform_checktip Validform_right");
    $("#" + tid).html("通过信息认证！");
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
//审核代理商
function AgentAuditState() {
    var auditstate = $('input[name="u_auditstate"]:checked').val();

    var id = $("#userid").val();

    var url = "/Agent/UpdateAgentAuditState";
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

    });
}

//弹出商务信息
function relation_add() {
    window.parent.ShouwDiaLogWan("选择商务", 1000, 700, "/Agent/AgentAddBpTc");
}

//选择商务
function selectxzBpAgent(obj, obj1, index) {
    if (obj == null || obj == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return false;
    }
    window.parent.layer.getChildFrame("#relation_person_name", index).val($.trim(obj1));
    window.parent.layer.getChildFrame("#relation_person_id", index).val(obj);
    window.parent.layer.getChildFrame("#relation_person_names", index).val($.trim(obj1));
    window.parent.layer.getChildFrame("#relation_person_id_tip", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#relation_person_id_tip", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}

