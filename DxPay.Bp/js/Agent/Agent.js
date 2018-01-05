
//保存代理商
function SaveAgent() {
    var fd = GetFormData("frm-agent-add");
    var yzmsg = "";
    fd.Classify = $.trim($("#Classify").val());
    var yexp = /^[0-9a-zA-Z]*$/g
    //验证登录名
    var LoginName = $.trim($("#LoginName").val());
    if (LoginName == "") {
        $("#yzLoginName").attr("class", "error on");
        $("#yzLoginName").html("请输入登录名");
        return false;
    }
    else if (yexp.test(LoginName)) {
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
                    $("#yzLoginName").attr("class", "error on");
                    $("#yzLoginName").html("已存在该登录名称！");
                    yzmsg += "已存在该登录名称";
                } else {
                    $("#yzLoginName").attr("class", "error");
                    $("#yzLoginName").html("");
                }
            }
        });
    }
    else {
        $("#yzLoginName").attr("class", "error on");
        $("#yzLoginName").html("登录名只能输入拼音和数字");
        yzmsg += "登录名只能输入拼音和数字";
    }
    //真实姓名
    var DisplayName = $.trim($("#DisplayName").val());
    var Classify = $.trim($("#Classify").val());
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    if (Classify == "0") {
        if (!exp1.test(DisplayName)) {

            $("#yzDisplayName").attr("class", "error on");
            $("#yzDisplayName").html("真实姓名由1-6位汉字组成！");
            return false;

        }
        else {
            $("#yzDisplayName").attr("class", "error ");
            $("#yzDisplayName").html("");
        }
    }
    else if (Classify == "1") {
        if (!exp2.test(DisplayName)) {

            $("#yzDisplayName").attr("class", "error on");
            $("#yzDisplayName").html("公司名称由3-30位汉字组成！");
            return false;


        }
        else {
            $("#yzDisplayName").attr("class", "error ");
            $("#yzDisplayName").html("");
        }
    }
    //验证密码
    var Password = $.trim($("#Password").val());
    if (Password == "") {
        $("#yzPassword").attr("class", "error on");
        $("#yzPassword").html("请输入密码！");
    }
    else {
        if (Password.length < 6) {

            $("#yzPassword").attr("class", "error on");
            $("#yzPassword").html("密码不能小于6位！");
            return false;
        }
        else {
            $("#yzPassword").attr("class", "error on");
            $("#yzPassword").html("");
        }
    }
    //验证QQ
    var qq = $.trim($("#QQ").val());
    if (isQQ(qq)) {

        $("#yzQQ").attr("class", "error");
        $("#yzQQ").html("");

    } else {

        $("#yzQQ").attr("class", "error on");
        $("#yzQQ").html("纯数字组成，5-16位之间！");
        return false;
    }

    //验证联系电话
    var MobilePhone = $.trim($("#MobilePhone").val());
    if (!isMobileOrPhone(MobilePhone)) {

        $("#yzMobilePhone").attr("class", "error on");
        $("#yzMobilePhone").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }
    else {
        $("#yzMobilePhone").attr("class", "error ");
        $("#yzMobilePhone").html("");
    }

    //验证开户银行全称
    var BankFullName = $.trim($("#BankFullName").val());

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(BankFullName)) {

        $("#yzBankFullName").attr("class", "error ");
        $("#yzBankFullName").html("");
    } else {

        $("#yzBankFullName").attr("class", "error on ");
        $("#yzBankFullName").html("开户行全称长度为5-30！");
        return false;
    }

    //验证开户名
    var exp4 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp10 = /^[\u4E00-\u9FA5]{2,30}$/;
    var BankAccountName = $.trim($("#BankAccountName").val());
    if (Classify == 0) {
        if (!exp4.test(BankAccountName)) {

            $("#yzBankAccountName").attr("class", "error on");
            $("#yzBankAccountName").html("真实姓名由1-6位汉字组成！");
            return false;

        }
        else {
            $("#yzBankAccountName").attr("class", "error ");
            $("#yzBankAccountName").html("");
        }
    }
    else {
        if (!exp10.test(BankAccountName)) {

            $("#yzBankAccountName").attr("class", "error on");
            $("#yzBankAccountName").html("开户名由2-30位汉字组成！");
            return false;

        }
        else {
            $("#yzBankAccountName").attr("class", "error ");
            $("#yzBankAccountName").html("");
        }

    }
    //验证开户账号
    var BankAccount = $.trim($("#BankAccount").val());
    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isNull = isRequestNotNull(BankAccount);//是否为空
    var isYyzz = rexp.test(BankAccount);//格式是否正确
    var data = { cval: BankAccount, uid: $("#id").val() };
    if (isNull) {
        $("#yzBankAccount").attr("class", "error on");
        $("#yzBankAccount").html("请输入开户账号！");
        return false;
    }
    else if (!isYyzz) {
        $("#yzBankAccount").attr("class", "error  on");
        $("#yzBankAccount").html(" 请输入正确的账号");
        return false;
    }
    else {
        $("#yzBankAccount").attr("class", "error ");
        $("#yzBankAccount").html("");
    }


    //验证联系地址
    var ContactAddress = $.trim($("#ContactAddress").val());
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(ContactAddress)) {

        $("#yzContactAddress").attr("class", "error on");
        $("#yzContactAddress").html("");

    } else {

        $("#yzContactAddress").attr("class", "error on");
        $("#yzContactAddress").html("联系地址长度为5-30！");
        return false;
    }
    //验证营业执照
    if (Classify == 1) {
        var BusinessLicenseNumber = $.trim($("#BusinessLicenseNumber").val());
        var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isNull = isRequestNotNull(BusinessLicenseNumber);//是否为空
        var isYyzz = exrp.test(BusinessLicenseNumber);//格式是否正确
        if (isNull) {
            $("#yzBusinessLicenseNumber").attr("class", "error on");
            $("#yzBusinessLicenseNumber").html("请输入营业执照！");
            return false;
        }
        else if (!isYyzz)
        {
            $("#yzBusinessLicenseNumber").attr("class", "error on");
            $("#yzBusinessLicenseNumber").html("由15或18位数字和字母组成！");
            return false;
        }
        else {
            $("#yzBusinessLicenseNumber").attr("class", "");
            $("#yzBusinessLicenseNumber").html("");

        }
    }
    else {
       
    }
    //验证身份证
    var IDCardNumber = $.trim($("#IDCardNumber").val());
    var isNull = isRequestNotNull(IDCardNumber);//是否为空   
    var isIdno = new clsIDCard(IDCardNumber).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: IDCardNumber, uid: $("#id").val() };
        $.ajax({
            type: "post",
            url: "/Agent/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#yzIDCardNumber").attr("class", "error on");
                    $("#yzIDCardNumber").html("已存在该身份证号！");
                    yzmsg += "已存在该身份证号!";

                } else {
                    $("#yzIDCardNumber").attr("class", "error");
                    $("#yzIDCardNumber").html("");
                }
            }

        });
    }
    else if (isNull) {
        $("#yzIDCardNumber").attr("class", "error on");
        $("#yzIDCardNumber").html("请输入身份证号！");
        return false;

    } else if (!isIdno) {
        $("#yzIDCardNumber").attr("class", "error on");
        $("#yzIDCardNumber").html("身份证号格式不正确！");
        return false;
    }
    else {
        $("#yzIDCardNumber").attr("class", "error ");
        $("#yzIDCardNumber").html("");
    }


    fd.BusinessLicensePhotoPath = fd.u_blicense;
    fd.PersonalPhotoPath = fd.u_photo;

    var isImgNull = isRequestNotNull(fd.PersonalPhotoPath);
    var isImg = CheckImgExtName(fd.PersonalPhotoPath);
    if (isImgNull) {
        ModifyTipCss("u_photo_tip", "请上传身份证！");

        return false;
    } else if (!isImg) {
        ModifyTipCss("u_photo_tip", "请上传身份证（.jpg.png.bmp.jpeg）！");
        yzmsg += "请选择正确格式的证件图片！";
        return false;
    }
    if (Classify == "1") {
        //获取企业身份证上传
        var isImgNull = isRequestNotNull(fd.BusinessLicensePhotoPath);
        var isImg = CheckImgExtName(fd.BusinessLicensePhotoPath);
        if (isImgNull) {
            ModifyTipCss("sfzficateyy", "请选择证件图片！");
            yzmsg += "请选择证件图片！";
            return false;
        } else if (!isImg) {
            ModifyTipCss("sfzficateyy", "请选择证件图片（.jpg.png.bmp.jpeg）！");
            yzmsg += "请选择正确格式的证件图片！";
            return false;
        }
    }
    if (yzmsg!="")
    {
       
        return false;
    }
    else
    {
        $("#btn_submit").attr("disabled", "disabled");
        $.post("/Agent/InsertAgent", fd, function (result) {
            $("#btn_submit").attr("disabled", false);

            if (result.success == 1) {
                window.parent.ShowMsg(result.msg, "ok", function () {
                    var name = "代理商列表";
                    var isLeaf = true;//是否套用
                    var id = $(this).attr("data-id");//id
                    var href = "/Agent/AgentList";//链接
                    closeIfram(name, isLeaf, href, id, 'child');
                });
            } else if (result.success == 9998) {
                window.parent.ShowMsg(result.msg, "error", "");

            } else if (result.success == 9999) {
                window.parent.ShowMsg(result.msg, "error", "");

            } else {

                window.parent.ShowMsg(result.msg, "error", "");
            }
            $("#btn_submit").removeAttr("disabled");
        });
    }
    }

//修改代理商
function SaveUpdateAgent() {
    //表单数据
    var fd = GetFormData("frm-agent-Edit");
    var yzmsg = "";
    fd.id = $("#id").val();
    fd.Classify = $.trim($("#Classify").val());
    //验证登录名
    var yexp = /^[0-9a-zA-Z]*$/g
    var LoginName = $.trim($("#LoginName").val());
    if (LoginName == "") {
        $("#yzLoginName").attr("class", "error on");
        $("#yzLoginName").html("请输入登录名");
        return false;
    }
   
    else if (yexp.test(LoginName)) {
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
                    $("#yzLoginName").attr("class", "error on");
                    $("#yzLoginName").html("已存在该登录名称！");
                    yzmsg += "已存在该登录名称";
                } else {
                    $("#yzLoginName").attr("class", "error");
                    $("#yzLoginName").html("");
                }
            }
        });
    }
    else {
        $("#yzLoginName").attr("class", "error on");
        $("#yzLoginName").html("登录名只能输入拼音和数字");
        yzmsg += "登录名只能输入拼音和数字";
    }
    //真实姓名
    var DisplayName = $.trim($("#DisplayName").val());
    var Classify = $.trim($("#Classify").val());

    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    if (Classify == "0") {
        if (!exp1.test(DisplayName)) {

            $("#yzDisplayName").attr("class", "error on");
            $("#yzDisplayName").html("真实姓名由1-6位汉字组成！");
            return false;

        }
        else {
            $("#yzDisplayName").attr("class", "error ");
            $("#yzDisplayName").html("");
        }
    }
    else if (Classify == "1") {
        if (!exp2.test(DisplayName)) {

            $("#yzDisplayName").attr("class", "error on");
            $("#yzDisplayName").html("公司名称由3-30位汉字组成！");
            return false;


        }
        else {
            $("#yzDisplayName").attr("class", "error ");
            $("#yzDisplayName").html("");
        }
    }
    //验证密码
    var Password = $.trim($("#Password").val());
    if (Password == "") {
        $("#yzPassword").attr("class", "error on");
        $("#yzPassword").html("请输入密码！");
    }
    else {
        if (Password.length < 6) {

            $("#yzPassword").attr("class", "error on");
            $("#yzPassword").html("密码不能小于6位！");
            return false;
        }
        else {
            $("#yzPassword").attr("class", "error on");
            $("#yzPassword").html("");
        }
    }
    //验证QQ
    var qq = $.trim($("#QQ").val());
    if (isQQ(qq)) {

        $("#yzQQ").attr("class", "error");
        $("#yzQQ").html("");

    } else {

        $("#yzQQ").attr("class", "error on");
        $("#yzQQ").html("纯数字组成，5-16位之间！");
        return false;
    }

    //验证联系电话
    var MobilePhone = $.trim($("#MobilePhone").val());
    if (!isMobileOrPhone(MobilePhone)) {

        $("#yzMobilePhone").attr("class", "error on");
        $("#yzMobilePhone").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }
    else {
        $("#yzMobilePhone").attr("class", "error ");
        $("#yzMobilePhone").html("");
    }

    //验证开户银行全称
    var BankFullName = $.trim($("#BankFullName").val());

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(BankFullName)) {

        $("#yzBankFullName").attr("class", "error ");
        $("#yzBankFullName").html("");
    } else {

        $("#yzBankFullName").attr("class", "error on ");
        $("#yzBankFullName").html("开户行全称长度为5-30！");
        return false;
    }

    //验证开户名
    var exp4 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp10 = /^[\u4E00-\u9FA5]{2,30}$/;
    var BankAccountName = $.trim($("#BankAccountName").val());
    if (Classify == 0) {
        if (!exp4.test(BankAccountName)) {

            $("#yzBankAccountName").attr("class", "error on");
            $("#yzBankAccountName").html("真实姓名由1-6位汉字组成！");
            return false;

        }
        else {
            $("#yzBankAccountName").attr("class", "error ");
            $("#yzBankAccountName").html("");
        }
    }
    else {
        if (!exp10.test(BankAccountName)) {

            $("#yzBankAccountName").attr("class", "error on");
            $("#yzBankAccountName").html("开户名由2-30位汉字组成！");
            return false;

        }
        else {
            $("#yzBankAccountName").attr("class", "error ");
            $("#yzBankAccountName").html("");
        }

    }
    //验证开户账号
    var BankAccount = $.trim($("#BankAccount").val());
    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isNull = isRequestNotNull(BankAccount);//是否为空
    var isYyzz = rexp.test(BankAccount);//格式是否正确
    var data = { cval: BankAccount, uid: $("#id").val() };
    if (isNull) {
        $("#yzBankAccount").attr("class", "error on");
        $("#yzBankAccount").html("请输入开户账号！");
        return false;
    }
    else if (!isYyzz) {
        $("#yzBankAccount").attr("class", "error  on");
        $("#yzBankAccount").html(" 请输入正确的账号");
        return false;
    }
    else {
        $("#yzBankAccount").attr("class", "error ");
        $("#yzBankAccount").html("");
    }


    //验证联系地址
    var ContactAddress = $.trim($("#ContactAddress").val());
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(ContactAddress)) {

        $("#yzContactAddress").attr("class", "error on");
        $("#yzContactAddress").html("");

    } else {

        $("#yzContactAddress").attr("class", "error on");
        $("#yzContactAddress").html("联系地址长度为5-30！");
        return false;
    }
    //验证营业执照
    if (Classify == 1) {
        var BusinessLicenseNumber = $.trim($("#BusinessLicenseNumber").val());
        var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isNull = isRequestNotNull(BusinessLicenseNumber);//是否为空
        var isYyzz = exrp.test(BusinessLicenseNumber);//格式是否正确
        if (isNull) {
            $("#yzBusinessLicenseNumber").attr("class", "error on");
            $("#yzBusinessLicenseNumber").html("请输入营业执照！");
            return false;
        }
        else if (!isYyzz) {
            $("#yzBusinessLicenseNumber").attr("class", "error on");
            $("#yzBusinessLicenseNumber").html("由15或18位数字和字母组成！");
            return false;
        }
        else {
            $("#yzBusinessLicenseNumber").attr("class", "");
            $("#yzBusinessLicenseNumber").html("");

        }
    }
    else {
        
    }
    //验证身份证
    var IDCardNumber = $.trim($("#IDCardNumber").val());
    var isNull = isRequestNotNull(IDCardNumber);//是否为空   
    var isIdno = new clsIDCard(IDCardNumber).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: IDCardNumber, uid: $("#id").val() };
        $.ajax({
            type: "post",
            url: "/Agent/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#yzIDCardNumber").attr("class", "error on");
                    $("#yzIDCardNumber").html("已存在该身份证号！");
                    yzmsg += "已存在该身份证号";
                } else {
                    $("#yzIDCardNumber").attr("class", "error");
                    $("#yzIDCardNumber").html("");
                }
            }

        });
    }
    else if (isNull) {
        $("#yzIDCardNumber").attr("class", "error on");
        $("#yzIDCardNumber").html("请输入身份证号！");
        return false;

    } else if (!isIdno) {
        $("#yzIDCardNumber").attr("class", "error on");
        $("#yzIDCardNumber").html("身份证号格式不正确！");
        return false;
    }
    else {
        $("#yzIDCardNumber").attr("class", "error ");
        $("#yzIDCardNumber").html("");
    }
    fd.BusinessLicensePhotoPath = fd.u_blicense;
    fd.PersonalPhotoPath = fd.u_photo;

    var isImgNull = isRequestNotNull(fd.PersonalPhotoPath);
    var isImg = CheckImgExtName(fd.PersonalPhotoPath);
    if (isImgNull) {
        ModifyTipCss("u_photo_tip", "请上传身份证！");

        return false;
    } else if (!isImg) {
        ModifyTipCss("u_photo_tip", "请上传身份证（.jpg.png.bmp.jpeg）！");
        msgr += "请选择正确格式的证件图片！";
        return false;
    }
    if (Classify == "1") {
        //获取企业身份证上传
        var isImgNull = isRequestNotNull(fd.BusinessLicensePhotoPath);
        var isImg = CheckImgExtName(fd.BusinessLicensePhotoPath);
        if (isImgNull) {
            ModifyTipCss("sfzficateyy", "请选择证件图片！");
            msgr += "请选择证件图片！";
            return false;
        } else if (!isImg) {
            ModifyTipCss("sfzficateyy", "请选择证件图片（.jpg.png.bmp.jpeg）！");
            msgr += "请选择正确格式的证件图片！";
            return false;
        }
    }
     fd.ServiceFeeRatioGradeId = $.trim($("#ServiceFeeRatioGradeId").val());
    //获取隐藏控件

    // fd.AuditState = $('input[name="u_auditstate"]:checked').val();//审核状态：-1 未通过 0 等待审核
    //  fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";

  
     if(yzmsg=="")
     {
         $.post("/Agent/UpdateAgents", fd, function (result) {
             $("#btn_submit").attr("disabled", "disabled");
             $("#btn-save-add").attr("disabled", false);

             if (result.success == 1) {
                 window.parent.ShowMsg(result.msg, "ok", function () {
                     var name = "代理商列表";
                     var isLeaf = true;//是否套用
                     var id = $(this).attr("data-id");//id
                     var href = "/Agent/AgentList";//链接
                     closeIfram(name, isLeaf, href, id, 'child');
                 });
             } else if (result.success == 9998) {
                 window.parent.ShowMsg(result.msg, "error", "");
                 $("#btn_submit").attr("disabled", "");
                 //return;
             } else if (result.success == 9999) {
                 window.parent.ShowMsg(result.msg, "error", "");
                 //  window.top.location.href = retJson.Redirect;
                 //return;
             } else {
                 window.parent.ShowMsg(result.msg, "error", "");
                 //return;
             }
             $("#btn_submit").removeAttr("disabled");
         });
     }
     else
     {
      

         return false;
     }
   

}

//验证登录名
function yzLoginName() {
    var yexp = /^[0-9a-zA-Z]*$/g
    //验证登录名
    var LoginName = $.trim($("#LoginName").val());
    if (LoginName == "") {
        $("#yzLoginName").attr("class", "error on");
        $("#yzLoginName").html("请输入登录名");
        return false;
    }
    else if (yexp.test(LoginName)) {
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
                    $("#yzLoginName").attr("class", "error on");
                    $("#yzLoginName").html("已存在该登录名称！");
                    return false;
                } else {
                    $("#yzLoginName").attr("class", "error");
                    $("#yzLoginName").html("");
                }
            }
        });
    }
    else {
        $("#yzLoginName").attr("class", "error on");
        $("#yzLoginName").html("登录名只能输入拼音和数字");
    }
}

//验证真实姓名
function yzDisplayName() {
    var DisplayName = $.trim($("#DisplayName").val());
    var Classify = $.trim($("#Classify").val());

    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{3,30}$/;
    if (Classify == "0") {
        if (!exp1.test(DisplayName)) {

            $("#yzDisplayName").attr("class", "error on");
            $("#yzDisplayName").html("真实姓名由1-6位汉字组成！");
            return false;

        }
        else {
            $("#yzDisplayName").attr("class", "error ");
            $("#yzDisplayName").html("");
        }
    }
    else if (Classify == "1") {
        if (!exp2.test(DisplayName)) {

            $("#yzDisplayName").attr("class", "error on");
            $("#yzDisplayName").html("公司名称由3-30位汉字组成！");
            return false;


        }
        else {
            $("#yzDisplayName").attr("class", "error ");
            $("#yzDisplayName").html("");
        }
    }
}
//验证登录密码
function yzPassword() {
    var Password = $.trim($("#Password").val());
    if (Password == "") {
        $("#yzPassword").attr("class", "error on");
        $("#yzPassword").html("请输入密码！");
    }
    else {
        if (Password.length < 6) {

            $("#yzPassword").attr("class", "error on");
            $("#yzPassword").html("密码不能小于6位！");
            return false;
        }
        else {
            $("#yzPassword").attr("class", "error on");
            $("#yzPassword").html("");
        }
    }
}
//验证QQ
function yzQQ() {
    var qq = $.trim($("#QQ").val());
    if (isQQ(qq)) {

        $("#yzQQ").attr("class", "error");
        $("#yzQQ").html("");

    } else {

        $("#yzQQ").attr("class", "error on");
        $("#yzQQ").html("纯数字组成，5-16位之间！");
        return false;
    }
}
//验证手机号码
function yzMobilePhone() {

    //验证联系电话
    var MobilePhone = $.trim($("#MobilePhone").val());
    if (!isMobileOrPhone(MobilePhone)) {

        $("#yzMobilePhone").attr("class", "error on");
        $("#yzMobilePhone").html("11位手机号或固定电话(号码或区号-号码)！");
        return false;
    }
    else {
        $("#yzMobilePhone").attr("class", "error ");
        $("#yzMobilePhone").html("");
    }
}

//验证身份证号码
function yzIDCardNumber() {
    //验证身份证
    var IDCardNumber = $.trim($("#IDCardNumber").val());
    var isNull = isRequestNotNull(IDCardNumber);//是否为空   
    var isIdno = new clsIDCard(IDCardNumber).Valid;//格式是否正确
    if (!isNull && isIdno) {
        var data = { cval: IDCardNumber, uid: $("#id").val() };
        $.ajax({
            type: "post",
            url: "/Agent/CheckIdno",
            cache: false,
            async: false,
            dataType: "json",
            data: data,
            success: function (msg) {
                if (msg.success) {
                    $("#yzIDCardNumber").attr("class", "error on");
                    $("#yzIDCardNumber").html("已存在该身份证号！");
                    return false;
                } else {
                    $("#yzIDCardNumber").attr("class", "error");
                    $("#yzIDCardNumber").html("");
                }
            }

        });
    }
    else if (isNull) {
        $("#yzIDCardNumber").attr("class", "error on");
        $("#yzIDCardNumber").html("请输入身份证号！");
        return false;

    } else if (!isIdno) {
        $("#yzIDCardNumber").attr("class", "error on");
        $("#yzIDCardNumber").html("身份证号格式不正确！");
        return false;
    }
    else {
        $("#yzIDCardNumber").attr("class", "error ");
        $("#yzIDCardNumber").html("");
    }
}

//开户银行全称
function yzBankFullName() {
    var BankFullName = $.trim($("#BankFullName").val());

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(BankFullName)) {

        $("#yzBankFullName").attr("class", "error ");
        $("#yzBankFullName").html("");
    } else {

        $("#yzBankFullName").attr("class", "error on ");
        $("#yzBankFullName").html("开户行全称长度为5-30！");
        return false;
    }
}
//验证开户名
function yzBankAccountName() {
    var exp4 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp10 = /^[\u4E00-\u9FA5]{2,30}$/;
    var BankAccountName = $.trim($("#BankAccountName").val());
    var Classify = $.trim($("#Classify").val());
    if (Classify == 0) {
        if (!exp4.test(BankAccountName)) {

            $("#yzBankAccountName").attr("class", "error on");
            $("#yzBankAccountName").html("真实姓名由1-6位汉字组成！");
            return false;

        }
        else {
            $("#yzBankAccountName").attr("class", "error ");
            $("#yzBankAccountName").html("");
        }
    }
    else {
        if (!exp10.test(BankAccountName)) {

            $("#yzBankAccountName").attr("class", "error on");
            $("#yzBankAccountName").html("开户名由2-30位汉字组成！");
            return false;

        }
        else {
            $("#yzBankAccountName").attr("class", "error ");
            $("#yzBankAccountName").html("");
        }

    }
}
//验证开户账号
function yzBankAccount() {
    var BankAccount = $.trim($("#BankAccount").val());
    var rexp = /^[0-9]{14}|[0-9]{10}|[0-9]{15}|[0-9]{17}|[0-9]{18}$/;
    var isNull = isRequestNotNull(BankAccount);//是否为空
    var isYyzz = rexp.test(BankAccount);//格式是否正确
    if (isNull) {
        $("#yzBankAccount").attr("class", "error on");
        $("#yzBankAccount").html("请输入开户账号！");
        return false;

    } else if (!isYyzz)
        {
        $("#yzBankAccount").attr("class", "error on");
        $("#yzBankAccount").html("开户账号格式不正确！");
        return false;

    }
    else {
        $("#yzBankAccount").attr("class", "error ");
        $("#yzBankAccount").html("");
    }
}

//验证联系地址
function yzContactAddress() {
    var ContactAddress = $.trim($("#ContactAddress").val());

    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (rexp.test(ContactAddress)) {

        $("#yzContactAddress").attr("class", "error");
        $("#yzContactAddress").html("");

    } else {

        $("#yzContactAddress").attr("class", "error on");
        $("#yzContactAddress").html("联系地址长度为5-30！");
        return false;
    }
}
//验证营业执照
function yzBusinessLicenseNumber() {
    var BusinessLicenseNumber = $.trim($("#BusinessLicenseNumber").val());
    var exrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
    var isNull = isRequestNotNull(BusinessLicenseNumber);//是否为空
    var isYyzz = exrp.test(BusinessLicenseNumber);//格式是否正确
    var BusinessLicenseNumber = $.trim($("#BusinessLicenseNumber").val());
    if (isNull) {

        $("#yzBusinessLicenseNumber").attr("class", "error on");
        $("#yzBusinessLicenseNumber").html("请输入营业执照！");
        return false;

    } else if (!isYyzz)
    {
        $("#yzBusinessLicenseNumber").attr("class", "error on");
        $("#yzBusinessLicenseNumber").html("由15或18位数字和字母组成！");
        return false;
    }
    else
    {
        $("#yzBusinessLicenseNumber").attr("class", "");
        $("#yzBusinessLicenseNumber").html("");

    }
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

//上传图片身份证
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
                document.getElementById("addphoto").style.display = "none";
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
    var iType = $("#Classify").val();
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
                document.getElementById("add").style.display = "none";
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
//选择认证类型展示对应的输入项
function ChangeType(obj) {
    var stext = $(obj).find("option:selected").text();
    var Classify = $.trim($("#Classify").val());
    if (Classify == 0) {
        $("#user_idno").show();
        $("#user_yyzz").hide();
        $("#qysfz").hide();
    }
    else {
       // $("#user_idno").hide();
        $("#user_yyzz").show();
        $("#qysfz").show();
    }
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
