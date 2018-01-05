
//录入响应记录
/**
 * isCopy:是否转发给指定值班人员
*/
function btnSaveAddRespond(isCopy) {
    //大类
    var MainCategory = $("#MainCategory").val();
    //小类
    var SubCategory = $("#SubCategory").val();
    //值班人
    var WatchId = $("#WatchId").val();
    if (WatchId == "0") {

        $("#WatchIdYz").attr("class", "Validform_checktip Validform_wrong");
        $("#WatchIdYz").html("请选择值班人！");
        return false;
    }
    else {
        $("#WatchIdYz").attr("class", "Validform_checktip  Validform_right");
        $("#WatchIdYz").html("验证通过");
    }

    //开发者EMAIL
    var developerEmail = $("#a_user_idyx").val();

    //开发者ID
    var developerId = $("#a_user_id").val();
    if (developerId === "0") {

        $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
        $("#yzkfz").html("请选择开发者！");
        return false;
    }
    else {
        $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
        $("#yzkfz").html("验证通过");
    }

    //提问时间
    var AskDate = $("#AskDate").val();
    //提问截图
    var AskScreenshot = $("#Ask").val();
    if (AskScreenshot == "") {

        $("#AskYz").attr("class", "Validform_checktip Validform_wrong");
        $("#AskYz").html("请上传提问截图！");
        return false;
    }
    else {
        $("#AskYz").attr("class", "Validform_checktip  Validform_right");
        $("#AskYz").html("验证通过");
    }
    //响应时间
    var ResponseDate = $("#ResponseDate").val();
    //响应截图
    var ResponseScreenshot = $("#Response").val();
    if (ResponseScreenshot == "") {
        $("#ReYz").attr("class", "Validform_checktip Validform_wrong");
        $("#ReYz").html("请上传响应截图！");
        return false;
    }
    else {
        $("#ReYz").attr("class", "Validform_checktip  Validform_right");
        $("#ReYz").html("验证通过");
    }
    //处理详情
    var HandleDetails = $("#HandleDetails").val();
    //处理完成时间
    var CompletedDate = $("#CompletedDate").val();

    var EvidenceScreenshot = "";

    //证据图片
    var Envidence1 = $("#Envidence1").val();
    if (Envidence1 != "") {
        EvidenceScreenshot += Envidence1 + ",";
    }
    var Envidence2 = $("#Envidence2").val();
    if (Envidence2 != "") {
        EvidenceScreenshot += Envidence2 + ",";
    }
    var Envidence3 = $("#Envidence3").val();
    if (Envidence3 != "") {
        EvidenceScreenshot += Envidence3 + ",";
    }
    var Envidence4 = $("#Envidence4").val();
    if (Envidence4 != "") {
        EvidenceScreenshot += Envidence4 + ",";
    }
    var id = $("#Id").val();
    var url = "/Workorder/RespondInsertAdd";
    var data = { Id: id, MainCategory: $.trim(MainCategory), SubCategory: $.trim(SubCategory), WatchId: $.trim(WatchId), AskDate: $.trim(AskDate), AskScreenshot: $.trim(AskScreenshot), ResponseDate: $.trim(ResponseDate), ResponseScreenshot: $.trim(ResponseScreenshot), HandleDetails: $.trim(HandleDetails), CompletedDate: $.trim(CompletedDate), EvidenceScreenshot: $.trim(EvidenceScreenshot), DeveloperId: $.trim(developerId),DeveloperEmail:$.trim(developerEmail), IsCopy: isCopy
    };

    $("#btnSaveAddRespond").attr("disabled", "disabled");

    $.post(url, data, function (retJson) {

        $("#btnSaveAddRespond").attr("disabled", false);
        if (retJson.success == 1) {

            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.layer.closeAll(); });
        }
        else {
            window.parent.ShowMsg(retJson.msg, "error", "");
            return false;
        }
    })

}

//验证开发者
function kfz() {
    var a_user_idyx = $("#a_user_idyx").val();
    if ($.trim(a_user_idyx) == "") {
        $("#yzkfz").attr("class", "Validform_checktip Validform_wrong");
        $("#yzkfz").html("请选择开发者");
        return false;
    } else {
        $("#yzkfz").attr("class", "Validform_checktip  Validform_right");
        $("#yzkfz").html("验证通过");
    }
}



//file控件值有改变就上传提问截图
function FileAsk() {

    var filedata = $("#AskScreenshotfile").val();

    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUploadAsk();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传提问截图
function ajaxUploadAsk() {

    iurl = $("#Ask").val();
    $.ajaxFileUpload({
        url: '/Workorder/UploadImgAsk',
        type: 'post',
        secureuri: false,
        fileElementId: 'AskScreenshotfile',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#AskScreenshot").attr("src", data.imgurlroot);
                $("#Ask").val(data.imgurl);

            } else {
                window.parent.ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//file控件值有改变就上传响应截图
function FileResponse() {

    var filedata = $("#ResponseScreenshotfile").val();

    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUploadResp();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传响应截图
function ajaxUploadResp() {

    iurl = $("#Response").val();
    $.ajaxFileUpload({
        url: '/Workorder/UploadImgResponse',
        type: 'post',
        secureuri: false,
        fileElementId: 'ResponseScreenshotfile',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#ResponseScreenshot").attr("src", data.imgurlroot);
                $("#Response").val(data.imgurl);

            } else {
                window.parent.ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}


///////////////////////证据上传///////////////////////

//file控件值有改变就上传证据1
function FileChange1() {

    var filedata = $("#certificatefile1").val();

    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload1();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传证据1
function ajaxUpload1() {

    iurl = $("#Envidence1").val();
    $.ajaxFileUpload({
        url: '/Workorder/UploadImg',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile1',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#EnvidenceXS1").attr("src", data.imgurlroot);
                $("#Envidence1").val(data.imgurl);

            } else {
                window.parent.ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//file控件值有改变就上传证据2
function FileChange2() {
    var filedata = $("#certificatefile2").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload2();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传证据2
function ajaxUpload2() {

    iurl = $("#Envidence2").val();
    $.ajaxFileUpload({
        url: '/Workorder/UploadImg2',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile2',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#EnvidenceXS2").attr("src", data.imgurlroot);
                $("#Envidence2").val(data.imgurl);

            } else {
                window.parent.ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//file控件值有改变就上传证据3
function FileChange3() {
    var filedata = $("#certificatefile3").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload3();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

    }
}

//上传证据3
function ajaxUpload3() {

    iurl = $("#Envidence3").val();
    $.ajaxFileUpload({
        url: '/Workorder/UploadImg3',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile3',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#EnvidenceXS3").attr("src", data.imgurlroot);
                $("#Envidence3").val(data.imgurl);

            } else {
                window.parent.ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//file控件值有改变就上传证据4
function FileChange4() {
    var filedata = $("#certificatefile4").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload4();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传证据4
function ajaxUpload4() {

    iurl = $("#Envidence4").val();
    $.ajaxFileUpload({
        url: '/Workorder/UploadImg4',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile4',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#EnvidenceXS4").attr("src", data.imgurlroot);
                $("#Envidence4").val(data.imgurl);

            } else {
                window.parent.ShowMsg(data.mess, "error", "");
            }
        },
        error: function (data, status, e) {
            //alert(e);
        }
    });
    return;
}

//弹窗开发者列表
function xzuser() {
    window.parent.ShouwDiaLogWan("选择开发者", 1000, 700, "/App/UserList");

}
//选择开发者用户
function yxuzyhuser(u_id, u_email, index) {
    window.parent.layer.getChildFrame("#a_user_idyx", index).val(u_email);
    window.parent.layer.getChildFrame("#a_user_id", index).val(u_id);
    window.parent.layer.getChildFrame("#yzkfz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#yzkfz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}