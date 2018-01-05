//JavaScript Document

$(function () {
    //点击开发者类别，切换显示项
    $("input[name='u_category']").each(function (index) {
        $(this).click(function () {
            var temp = $(this).val();
            if (temp == "0") {
                $("#u_idno").show();
                $("#u_idno_img").show();
                $("#u_yyzz").hide();
                $("#u_yyzz_img").hide();
                $("#u_name_tip").html("<span class='need'>*</span> 真实姓名：");
            } else if (temp == "1") {
                $("#u_idno").hide();
                //$("#u_idno_img").hide();
                $("#u_yyzz").show();
                $("#u_yyzz_img").show();
                $("#u_name_tip").html("<span class='need'>*</span> 公司名称：");
            }
            //移除验证提示
            $(".control-group").find(".Validform_checktip").attr("class", "Validform_checktip");
            $(".control-group").find(".jm_verify_info").hide();
            $(".control-group").find(".jm_verify_content").find("input").removeClass("Validform_error");
        });
    });

    //提交审核
    $("#btn_submit").click(function () {
        //禁用按钮防止重复提交
        $("#btn_submit").attr("disabled", "disabled");
        SubmitData();
    });

});



//验证真实姓名或公司名称
function CheckNmae() {
    var rname = $("#u_realname").val();
    var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
    var exp2 = /^[\u4E00-\u9FA5]{8,30}$/;
    var rtype = $("input[name='u_category']:checked").val();
    if (rtype == "0") {
        if (exp1.test(rname)) {
            objToolTip("u_realname", 3, "");
        } else {
            objToolTip("u_realname", 4, "真实姓名由1-6位汉字组成！");
        }
    } else if ((rtype == "1")) {
        if (exp2.test(rname)) {
            objToolTip("u_realname", 3, "");
        } else {
            objToolTip("u_realname", 4, "公司名称由8-30位汉字组成！");
        }
    }
}

//保存数据
function SubmitData() {
    //取出表单数据
    var tmsg = "";
    var fdata = {};
    $("#frm-dev").find("input").each(function (index) {
        if (this.name != "") {
            fdata[this.name] = $(this).val();
        }
    });
    fdata.u_category = $("input[name='u_category']:checked").val();
    var umail = fdata.u_email;
    var u_type = fdata.u_category;
    var rname = fdata.u_realname;//真实姓名或公司名称
    var aname = fdata.u_name;//开户名
    if (u_type == "0") {

        //验证真实姓名
        var exp1 = /^[\u4E00-\u9FA5]{1,6}$/;
        if (exp1.test(rname)) {
            objToolTip("u_realname", 3, "");
        } else {
            objToolTip("u_realname", 4, "真实姓名由1-6位汉字组成！");
            tmsg += "真实姓名由1-6位汉字组成！";
            return;
        }
    }
    else {
        //验证公司名称
        var exp2 = /^[\u4E00-\u9FA5]{8,30}$/;
        if (exp2.test(rname)) {
            objToolTip("u_realname", 3, "");
        } else {
            objToolTip("u_realname", 4, "公司名称由8-30位汉字组成！");
            tmsg += "公司名称由8-30位汉字组成！";
            return;
        }

    }
    //提交
    if (isNull(tmsg)) {
        $.ajax({
            url: "/User/VerifyInfoNew",
            type: "post",
            data: fdata,
            async: false,
            success: function (result) {
                CheckJsonData(result);
                if (result.success == 1) {
                    ShowMsg(result.msg, "ok", function () {
                        window.location.href = "/User/DevVerifySucc";
                    });
                } else if (result.success == 2) {
                    ShowMsg(result.msg, "error", "");
                    window.location.href = "/Home/Login";
                } else {
                    ShowMsg(result.msg, "error", "");
                    //启用按钮
                    $("#btn_submit").removeAttr("disabled");
                }
            }
        });
    } else {
        ShowMsg("有输入项未正确输入，请确认！", "error", "");
        //启用按钮
        $("#btn_submit").removeAttr("disabled");
    }
}

//选择图片
function ChoseImg() {
    $("#certificatefile").trigger("click");
}
//选择身份证图片
function ChoseUpImage() {
    $("#certificatefilesfz").trigger("click");
}

//file控件一改变就上传
function FileChange() {
    var filedata = $("#certificatefile").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload();
        } else {
            ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
        }
    } else {
        ShowMsg("请选择证件图片！", "error", "");
    }
}
//上传图片
function ajaxUpload() {
    //获取原来的图片
    var rType = $("input[name='u_category']:checked").val();
    var iurl = "";
    if (rType == "0") {
        iurl = $("#u_photo").val();
    } else {
        iurl = $("#u_blicense").val();
    }
    $.ajaxFileUpload({
        url: '/User/UploadImg',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                if (rType == "0") {
                    $("#u_idno_img").find("img").attr("src", data.imgurl);
                    $("#u_photo").val(data.imgurl);
                } else {
                    $("#u_yyzz_img").find("img").attr("src", data.imgurl);
                    $("#u_blicense").val(data.imgurl);
                }
            } else {
                ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//企业上传身份证
function FileChangeSfz() {
    var filedata = $("#certificatefilesfz").val();
    var rType = $("input[name='u_category']:checked").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
                ajaxUploadSfz();
        } else {
            ShowMsg("请选择身份证件图片（.jpg.png.bmp.jpeg）！", "error", "");
        }
    } else {
        ShowMsg("请选择身份证件图片！", "error", "");
    }

}

//企业身份证上传图片
function ajaxUploadSfz() {
    var iurl = $("#sfzu_photo").val();
    $.ajaxFileUpload({
        url: '/User/UploadImgsfz',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefilesfz',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#u_idno_img").find("img").attr("src", data.imgurl);
                $("#u_photo").val(data.imgurl);
            } else {
                ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}