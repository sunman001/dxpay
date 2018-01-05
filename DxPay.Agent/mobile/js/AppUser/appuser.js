// JavaScript Document



$(document).ready(function () {

    $("#btnServiceFeeRatioGradeAdd").click(function () {
        //开发者ID
        var id = $("#id").val();
        var ServiceFeeRatioGradeId = $("#ServiceFeeRatioGradeId").val();
        if (ServiceFeeRatioGradeId == "" || ServiceFeeRatioGradeId < 0 || ServiceFeeRatioGradeId == "undefined") {
            ShowMsg("请选择费率比例", "error", "");
            return false;
        }
        $("#btnServiceFeeRatioGradeAdd").attr("disabled", "disabled");
        var url = "/AppUser/ScAdd";
        var data = { ServiceFeeRatioGradeId: $.trim(ServiceFeeRatioGradeId), id: $.trim(id) };
        $.post(url, data, function (retJson) {
            $("#btnServiceFeeRatioGradeAdd").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", "");
                window.location.href = "/AppUser/AppUserList";
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

    })

})
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
    url += "&stype=" + type + "&skeys=" + keys + "&state=" + state + "&scheck=" + check + "&s_sort=" + sort + "&scategory=" + rzlx;
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
            $("#qysfz").hide();
            $("#user_qyfr").hide();
            $("#user_qyzcaddress").hide();

        } else {

            $("#user_yyzz").show();
            $("#qysfz").show();
            $("#user_qyfr").show();
            $("#user_qyzcaddress").show();
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

//保存用户（新增）
function SaveUser() {
    //表单数据
    var fd = GetFormData("frm-user-add");
    fd.u_auditstate = 0;
    var tmsg = "";
    //验证邮箱
    var email = fd.u_email;
    var isMailNull = isRequestNull(email);//是否为空
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
                    ShowMsg("已存在该邮箱地址!", "error", "");
                    tmsg += "已存在该邮箱地址！";
                } else {

                }
            }
        });
    } else if (isMailNull) {
        ShowMsg("请输入邮箱地址!", "error", "");
        tmsg += "请输入邮箱地址！";
        return false;
    } else if (!isMail) {
        ShowMsg("邮箱地址格式不正确!", "error", "");
        tmsg += "邮箱地址格式不正确！";
        return false;
    }

    //验证密码
    var pwd = fd.u_password;
    if (pwd == "") {
        ShowMsg("密码不能小于6位！", "error", "");
        tmsg += "密码不能小于6位！";
        return false;
    }

    //验证联系电话
    var phone = fd.u_phone;
    if (!isMobile(phone)) {
        ShowMsg("联系电话为11位手机号！", "error", "");
        tmsg += "11位手机号！";
        return false;
    }

    //验证QQ号
    var qq = fd.u_qq;
    if (!isQQ(qq)) {
        ShowMsg("qq纯数字组成，5-16位之间！", "error", "");
        tmsg += "qq纯数字组成，5-16位之间！";
        return false;
    }

    //验证联系地址
    var addre = fd.u_address;
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!rexp.test(addre)) {
        ShowMsg("联系地址长度为5-30！", "error", "");
        tmsg += "联系地址长度为5-30！";
        return false;
    }

    //验证证件号码
    var stype = $("#u_category").val();
    fd.u_category = stype;//认证类型
    var rname = fd.u_realname;//真实姓名或公司名称
    var name = fd.u_name;//开户名
    var imgurl = "";//证件照片

    //验证身份证号
    var idno = fd.u_idnumber;
    var isIdnoNull = isRequestNull(idno);//是否为空
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
                    ShowMsg("已存在该身份证号！", "error", "");
                    tmsg += "已存在该身份证号！";
                } else {

                }
            }
        });
    } else if (isIdnoNull) {
        ShowMsg("请输入身份证号！", "error", "");
        tmsg += "请输入身份证号！";
        return false;
    } else if (!isIdno) {
        ShowMsg("身份证号格式不正确！", "error", "");
        tmsg += "身份证号格式不正确！";
        return false;
    }

    if (stype == "0") {
        fd.u_blicensenumber = "";
        

        //验证真实名称
        var exprname1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprname1.test(rname)) {
            ShowMsg("真实姓名由1-6位汉字组成！", "error", "");
            tmsg += "真实姓名由1-6位汉字组成！";
            return false;
        }

        //验证开户名
        var exprbnmae1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprbnmae1.test(name)) {
            ShowMsg("开户名由1-6位汉字组成！", "error", "");
            return false;
        }

        //验证证件照片
        imgurl = fd.u_photo;
        fd.u_blicense = "";
    } else {;
       
        //验证营业执照
        var yyzz = fd.u_blicensenumber;
        var yyzzExrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isYyzzNull = isRequestNull(yyzz);//是否为空
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
                        ShowMsg("已存在该营业执照！", "error", "");
                       
                        tmsg += "已存在该营业执照！";
                    } else {

                    }
                }
            });
        } else if (isYyzzNull) {
            ShowMsg("请输入营业执照！", "error", "");
            tmsg += "请输入营业执照！";
            return false;
        } else if (!isYyzz) {
            ShowMsg("营业执照由15或18位数字和字母组成！！", "error", "");
            tmsg += "营业执照由15或18位数字和字母组成！";
            return false;
        }

        //验证公司名称
        var exprname2 = /^[\u4E00-\u9FA5]{3,30}$/;
        if (!exprname2.test(rname)) {
            ShowMsg("公司名称由3-30位汉字组成！", "error", "");
            tmsg += "公司名称由3-30位汉字组成！";
            return false;
        }
        var exprbnmae2 = /^[\u4E00-\u9FA5]{2,30}$/;
        if (!exprbnmae2.test(name)) {
            ShowMsg("开户名由2-30位汉字组成！", "error", "");
            tmsg += "开户名由2-30位汉字组成！";
            return false;
        }
        var addre = $("#RegisteredAddress").val();
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (rexp.test(addre)) {
           
           
        } else {
            ShowMsg("企业注册地址长度为5-30！", "error", "");
            return false;
        }
        var rname = $("#BusinessEntity").val();
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

        if (!exp1.test(rname)) {
            ShowMsg("法人姓名由1-6位汉字组成！", "error", "");
            return false
        } else {
           
        }
       
    }
    
    //验证开户账号
    var khzh = fd.u_account;
    var yzkhzh = /^\d{1,30}$/;
    var yzkhzh1 = /^0?\d{9,11}|(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
    var isBankNull = isRequestNull(khzh);//是否为空
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
                    ShowMsg("已存在该开户账号！", "error", "");
                    tmsg += "已存在该开户账号！";
                } else {

                }
            }
        });
    } else if (isBankNull) {
        ShowMsg("请输入开户账号！", "error", "");
        tmsg += "请输入开户账号！";
        return false;
    } else if (!isKhzh) {
        ShowMsg("开户账号格式不正确！", "error", "");
        tmsg += "开户账号格式不正确！";
        return false;
    }

    //验证开户行全称
    var bname = fd.u_bankname;
    var yzbn = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!yzbn.test(bname)) {
        ShowMsg("开户行全称长度为5-30！", "error", "");
        tmsg += "开户行全称长度为5-30！";
        return false;
    }
    var imgurl = fd.u_photo;
    var imgurlSfz = fd.u_blicense;
    //获取证件照片的路径
    var isImgNull = isRequestNull(imgurl);
    var isImg = CheckImgExtName(imgurl);
    if (isImgNull) {
        ShowMsg("请选择身份证照片！", "error", "");
        tmsg += "请选择身份证照片！";
        return false;
    } else if (!isImg) {
        ShowMsg("请选择身份证照片（.jpg.png.bmp.jpeg）！", "error", "");
        tmsg += "请选择身份证照片！";
        return false;
    }
    if (stype == "1") {
        //获取企业身份证上传
        var isImgNull = isRequestNull(imgurlSfz);
        var isImg = CheckImgExtName(imgurlSfz);
        if (isImgNull) {
            ShowMsg("请选择营业执照照片！", "error", "");
            tmsg += "请选择证件图片！";
            return false;
        } else if (!isImg) {
            ShowMsg("请选择营业执照照片（.jpg.png.bmp.jpeg）！", "error", "");
            tmsg += "请选择营业执照照片（.jpg.png.bmp.jpeg）！";
            return false;
        }
    }
    fd.u_blicensenumber = $("#u_blicensenumber").val()

    //获取隐藏控件
    fd.u_state = 1;
    fd.u_topid = 0;
    //fd.u_poundage = 0;
    fd.u_count = 0;
    fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";
   // $("#btn-save-add").attr("disabled", "disabled");
    if (tmsg != "") {
       // $("#btn-save-add").removeAttr("disabled");
        return false;
    }
    else {
        $.post("/AppUser/AddUser", fd, function (result) {
            if (result.success == 1) {
                window.parent.ShowMsg(result.msg, "ok", "")
                window.location.href = "/AppUser/AppUserList";
              
            } else if (result.success == 9998) {
                window.parent.ShowMsg(result.msg, "error", "");
            } else if (result.success == 9999) {
                ShowMsg(result.msg, "error", "");
                window.top.location.href = retJson.Redirect;
            } else {
                ShowMsg(result.msg, "error", "");
            }
           // $("#btn-save-add").removeAttr("disabled");
        });
    }
    //$("#btn-save-add").removeAttr("disabled");
}

//保存用户（编辑）
function SaveUpdateUser() {
    //表单数据
    var fd = GetFormData("frm-user-edit");
    fd.u_id = $("#u_id").val()
    var tmsg = "";
    //验证邮箱
    var email = fd.u_email;
    fd.u_auditstate = 0;
    //验证邮箱
    var isMailNull = isRequestNull(email);//是否为空
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
                    ShowMsg("已存在该邮箱地址!", "error", "");
                    tmsg += "已存在该邮箱地址！";
                } else {

                }
            }
        });
    } else if (isMailNull) {
        ShowMsg("请输入邮箱地址!", "error", "");
        tmsg += "请输入邮箱地址！";
        return false;
    } else if (!isMail) {
        ShowMsg("邮箱地址格式不正确！", "error", "");
        tmsg += "邮箱地址格式不正确！";
        return false;
    }

    //验证密码
    var pwd = fd.u_password;
    if (pwd == "") {
        ShowMsg("密码不能小于6位！", "error", "");
        tmsg += "密码不能小于6位！";
        return false;
    }

    //验证联系电话
    var phone = fd.u_phone;
    if (!isMobile(phone)) {
        ShowMsg("联系电话为11位手机号！", "error", "");
        tmsg += "11位手机号！";
        return false;
    }

    //验证QQ号
    var qq = fd.u_qq;
    if (!isQQ(qq)) {
        ShowMsg("qq纯数字组成，5-16位之间！", "error", "");
        tmsg += "qq纯数字组成，5-16位之间！";
        return false;
    }

    //验证联系地址
    var addre = fd.u_address;
    var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!rexp.test(addre)) {
        ShowMsg("联系地址长度为5-30！", "error", "");
        tmsg += "联系地址长度为5-30！";
        return false;
    }

    //验证证件号码
    var stype = $("#u_category").val();
    fd.u_category = stype;//认证类型
    var rname = fd.u_realname;//真实姓名或公司名称
    var name = fd.u_name;//开户名
    var imgurl = "";//证件照片

    //验证身份证号
    var idno = fd.u_idnumber;
    var isIdnoNull = isRequestNull(idno);//是否为空
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
                    ShowMsg("已存在该身份证号！", "error", "");
                    tmsg += "已存在该身份证号！";
                } else {

                }
            }
        });
    } else if (isIdnoNull) {
        ShowMsg("请输入身份证号！", "error", "");
        tmsg += "请输入身份证号！";
        return false;
    } else if (!isIdno) {
        ShowMsg("身份证号格式不正确！", "error", "");
        tmsg += "身份证号格式不正确！";
        return false;
    }

    if (stype == "0") {
        fd.u_blicensenumber = "";
       
        //验证真实名称
        var exprname1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprname1.test(rname)) {
            ShowMsg("真实姓名由1-6位汉字组成！", "error", "");
            tmsg += "真实姓名由1-6位汉字组成！";
            return false;
        }

        //验证开户名
        var exprbnmae1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (!exprbnmae1.test(name)) {
            ShowMsg("开户名由1-6位汉字组成！", "error", "");
            return false;
        }

       
    } else {

        //验证营业执照
        var yyzz = fd.u_blicensenumber;
        var yyzzExrp = /^([0-9a-zA-Z]{15}|[0-9a-zA-Z]{18})$/;
        var isYyzzNull = isRequestNull(yyzz);//是否为空
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
                        ShowMsg("开户名由1-6已存在该营业执照！", "error", "");
                        tmsg += "已存在该营业执照！";
                    } else {

                    }
                }
            });
        } else if (isYyzzNull) {
            ShowMsg("请输入营业执照！", "error", "");
            tmsg += "请输入营业执照！";
            return false;
        } else if (!isYyzz) {
            ShowMsg("营业执照由15或18位数字和字母组成！", "error", "");
            tmsg += "营业执照由15或18位数字和字母组成！";
            return false;
        }
        //验证公司名称
        var exprname2 = /^[\u4E00-\u9FA5]{3,30}$/;
        if (!exprname2.test(rname)) {
            ShowMsg("公司名称由3-30位汉字组成！", "error", "");
            tmsg += "公司名称由3-30位汉字组成！";
            return false;
        }
        var exprbnmae2 = /^[\u4E00-\u9FA5]{2,30}$/;
        if (!exprbnmae2.test(name)) {
            ShowMsg("开户名由2-30位汉字组成！", "error", "");
            tmsg += "开户名由2-30位汉字组成！";
            return false;
        }
        var rname = $("#BusinessEntity").val();
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;

        if (!exp1.test(rname)) {
            ShowMsg("法人姓名由1-6位汉字组成！", "error", "");
            return false
        } else {
            
        }
        var addre = $("#RegisteredAddress").val();
        var rexp = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
        if (rexp.test(addre)) {
           
        } else {
            ShowMsg("企业注册地址长度为5-30！", "error", "");
            return false;
        }


       
    }

    //验证开户账号
    var khzh = fd.u_account;
    var yzkhzh = /^\d{1,30}$/;
    var yzkhzh1 = /^0?\d{9,11}|(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
    var isBankNull = isRequestNull(khzh);//是否为空
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
                    ShowMsg("已存在该开户账号！", "error", "");
                  
                    tmsg += "已存在该开户账号！";
                } else {

                }
            }
        });
    } else if (isBankNull) {
        ShowMsg("请输入开户账号！", "error", "");
        tmsg += "请输入开户账号！";
        return false;
    } else if (!isKhzh) {
        ShowMsg("开户账号格式不正确！", "error", "");
        tmsg += "开户账号格式不正确！";
        return false;
    }

    //验证开户行全称
    var bname = fd.u_bankname;
    var yzbn = /^([\u4e00-\u9fa5]{5,30})|([A-Za-z0-9 ]{10,60})$/;
    if (!yzbn.test(bname)) {
        ShowMsg("开户行全称长度为5-30！", "error", "");
        tmsg += "开户行全称长度为5-30！";
        return false;
    }
    var imgurl = fd.u_photo;
    var imgurlSfz = $("#u_blicense").val();
    //获取证件照片的路径
    var isImgNull = isRequestNull(imgurl);
    var isImg = CheckImgExtName(imgurl);
    if (isImgNull) {
        ShowMsg("请选择证件图片！", "error", "");
        ModifyTipCss("u_photo_tip", "请选择证件图片！");
        return false;
    } else if (!isImg) {
        ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
        tmsg += "请选择正确格式的证件图片！";
        return false;
    }
    if (stype == "1") {
        //获取企业身份证上传
        var isImgNull = isRequestNull(imgurlSfz);
        var isImg = CheckImgExtName(imgurlSfz);
        if (isImgNull) {
            ShowMsg("请选择证件图片", "error", "");
         
            tmsg += "请选择证件图片！";
            return false;
        } else if (!isImg) {
            ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
            tmsg += "请选择正确格式的证件图片！";
            return false;
        }
    }
  
  
    fd.u_blicensenumber = $("#u_blicensenumber").val()
    fd.ServiceFeeRatioGradeId = $.trim($("#ServiceFeeRatioGradeId").val());
  
    if (tmsg == "") {
        fd.u_drawing = $('input[name="drawing"]:checked').val() == "1" ? $('input[name="drawing"]:checked').val() : "0";
        //获取认证状态
        fd.u_auditstate = $('input[name="u_auditstate"]:checked').val();
        $.post("/AppUser/UpdateUser", fd, function (result) {
            if (result.success == 1) {
                $("#btn-save-edit").removeAttr("disabled");
                window.parent.ShowMsg(result.msg, "ok", "")
                window.location.href = "/AppUser/AppUserList";
            } else if (result.success == 9998) {
                ShowMsg(result.msg, "error", "");
                //return;
            } else if (result.success == 9999) {
                window.parent.ShowMsg(result.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                //return;
            } else {
                window.parent.ShowMsg(result.msg, "error", "");
              
            }
          
        });
    } else {
     
        return false;
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
                document.getElementById("add").style.display = "none";
            } else {
               
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
                document.getElementById("adds").style.display = "none";
            } else {
                
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

function bulkassign() {
    var vals = "";
    $("#table :checkbox[checked]").each(function (i) {
        if (i > 0)
            vals += ",";
        vals += $(this).val();
    });
    if (vals === "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("指定商务", 580, 300, "/appuser/merchantlist?uids=" + vals);
}

function AssignToMerchant() {
    var target = $(this);
    var uids = $("#txt_uids").val();

    var mid = $("#a_user_id").val();

    if (mid === "") {
        window.parent.ShowMsg("请选择要指定的商务！", "error", "");
        return;
    }
    $.post("/appuser/assigntomerchant", { uid: uids, mid: mid }, function (result) {
        if (result.success === 1) {
            window.parent.ShowMsg(result.msg, "ok", function () {
                window.parent.global.reload();
                window.parent.layer.closeAll();
            });
        } else if (result.success === 9998) {
            window.parent.ShowMsg(result.msg, "error", "");
            $("#btnCreateMerchant").attr("disabled", "");
            //return;
        } else if (result.success === 9999) {
            window.parent.ShowMsg(result.msg, "error", "");
            window.top.location.href = retJson.Redirect;
            //return;
        } else {
            window.parent.ShowMsg(result.msg, "error", "");
            //return;
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

//选择开发者费率
function xzCoService() {
    window.parent.ShouwDiaLogWan("费率列表", 1000, 700, "/AppUser/CoServiceList");

}

//选择开发者用户
function xzfl(ServiceFeeRatioGradeId, ServiceFeeRatioGradeName, index) {
    alert(ServiceFeeRatioGradeName);
    window.top.document.getElementById("ServiceFeeRatioGradeName").value = ServiceFeeRatioGradeName;
    window.top.document.getElementById("ServiceFeeRatioGradeId").value = ServiceFeeRatioGradeId;
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}

//一键启用/禁止
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
            window.parent.ShowMsg(result.msg, "ok", function () {
                parent.location.reload();
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