$(function () {
    //验证是否显示通知状态查询
    $('#paymentstate').change(function () {
        var pdzftype = $("#paymentstate").val();
        if (pdzftype == 1) {
            document.getElementById("tztype").style.display = "";
        } else {
            $("#noticestate").val("");
            document.getElementById("tztype").style.display = "none";
        }
    });
    $('#paymentstate').change();
})

//验证是否显示通知状态查询
function xstztype() {
    var pdzftype = $("#paymentstate").val();
    if (pdzftype == 1) {
        document.getElementById("tztype").style.display = "";
    } else {
        $("#noticestate").val("");
        document.getElementById("tztype").style.display = "none";
    }
}

//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/CustomService/OrderList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $.trim($("#searchType").val());
    var searchname = $.trim($("#searchname").val());
    var stime = $.trim($("#stime").val());
    var etime = $.trim($("#etime").val());
    var paymode = $.trim($("#paymode").val());
    var paymentstate = $.trim($("#paymentstate").val());
    var noticestate = $.trim($("#noticestate").val());
    var platformid = $.trim($("#platformid").val());
    var relationtype = $.trim($("#relationtype").val());

    url += "&searchType=" + $.trim(searchType) + "&searchname=" + $.trim(searchname) + "&stime=" + $.trim(stime) + "&etime=" + $.trim(etime) + "&paymode=" + $.trim(paymode) + "&paymentstate=" + $.trim(paymentstate) + "&noticestate=" + $.trim(noticestate) + "&platformid=" + $.trim(platformid) + "&relationtype=" + $.trim(relationtype);
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//列表查询
function selectorderlist() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//添加投诉弹窗
function AddComplain(o_code, u_id, o_app_id, o_interface_id) {

    window.parent.ShouwDiaLogWan("添加投诉", 800, 600, "/CustomService/ComplainOrderAdd?o_code=" + o_code + "&u_id=" + u_id + "&o_app_id=" + o_app_id + "&o_interface_id=" + o_interface_id);
}

//投诉类型
function ComplainTypeNameaAdd() {
    window.parent.ShouwDiaLogWan("选择投诉类型", 800, 600, "/CustomService/ComplainTypeTc");
}

//选择投诉类型
function selectxzComplain(obj, obj1, index) {
    if (obj == null || obj == "") {
        window.parent.ShowMsg("请选择用户！", "error", "");
        return;
    }
    window.parent.layer.getChildFrame("#ComplainTypeName", index).val(obj1);
    window.parent.layer.getChildFrame("#ComplainTypeId", index).val(obj);
    window.parent.layer.getChildFrame("#ComplainTypeyz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#ComplainTypeyz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}


//添加投诉
function btnSaveAddCustom() {

    var id = $("#Id").val();

    //订单号与订单表名称
    var OrderNumber = $("#OrderNumber").val();
    var UserId = $("#UserId").val();
    var AppId = $("#AppId").val();
    var ChannelId = $("#InterfaceId").val();

    //投诉类型与ID
    var ComplainTypeName = $("#ComplainTypeName").val();
    var ComplainTypeId = $("#ComplainTypeId").val();

    if (ComplainTypeName == "") {
        $("#ComplainTypeyz").attr("class", "Validform_checktip Validform_wrong");
        $("#ComplainTypeyz").html("请选择投诉类型！");
        return false;
    }
    else {
        $("#ComplainTypeyz").attr("class", "Validform_checktip  Validform_right");
        $("#ComplainTypeyz").html("验证通过");
    }

    var stime = $("#stime").val();
    var DownstreamStartTime = $("#DownstreamStartTime").val();
    var DownstreamEndTime = $("#DownstreamEndTime").val();
    var Envidence = "";

    //证据图片
    var Envidence1 = $("#Envidence1").val();
    if (Envidence1 != "") {
        Envidence += Envidence1 + ",";
    }
    var Envidence2 = $("#Envidence2").val();
    if (Envidence2 != "") {
        Envidence += Envidence2 + ",";
    }
    var Envidence3 = $("#Envidence3").val();
    if (Envidence3 != "") {
        Envidence += Envidence3 + ",";
    }
    var Envidence4 = $("#Envidence4").val();
    if (Envidence4 != "") {
        Envidence += Envidence4 + ",";
    }
    var url = "/CustomService/CustomAdd";
    var data = { OrderNumber: $.trim(OrderNumber), ComplainTypeName: $.trim(ComplainTypeName), ComplainTypeId: $.trim(ComplainTypeId), Envidence: $.trim(Envidence), ComplainDate: $.trim(stime), id: $.trim(id), UserId: $.trim(UserId), AppId: $.trim(AppId), DownstreamStartTime: $.trim(DownstreamStartTime), DownstreamEndTime: $.trim(DownstreamEndTime), ChannelId: $.trim(ChannelId) };
  
    $("#btnSaveAddCustom").attr("disabled", "disabled");

    $.post(url, data, function (retJson) {

        $("#btnSaveAddCustom").attr("disabled", false);
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

//file控件值有改变就上传证据1
function FileChange1() {

    var filedata = $("#certificatefile1").val();

    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload1();
        } else {
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传证据1
function ajaxUpload1() {

    iurl = $("#Envidence1").val();
    $.ajaxFileUpload({
        url: '/CustomService/UploadImg',
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
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传证据2
function ajaxUpload2() {

    iurl = $("#Envidence2").val();
    $.ajaxFileUpload({
        url: '/CustomService/UploadImg2',
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
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");

    }
}

//上传证据3
function ajaxUpload3() {

    iurl = $("#Envidence3").val();
    $.ajaxFileUpload({
        url: '/CustomService/UploadImg3',
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
            window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");

        }
    } else {
        window.parent.ShowMsg("请选择证件图片（.jpg.png.bmp.jpeg）！", "error", "");
    }
}

//上传证据4
function ajaxUpload4() {

    iurl = $("#Envidence4").val();
    $.ajaxFileUpload({
        url: '/CustomService/UploadImg4',
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





