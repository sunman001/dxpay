
$(function () {
    //添加新闻
    $("#btnSaveAdd").click(function () {
       // var fd = GetFormData("frm-user-add");
        //验证新闻标题
        var n_title = $.trim($("#n_title").val());
        if ($.trim(n_title) != "") {
            $("#titleyy").attr("class", "Validform_checktip  Validform_right");
            $("#titleyy").html("验证通过");
                } 
               else {
            $("#titleyy").attr("class", "Validform_checktip Validform_wrong");
            $("#titleyy").html("请填写标题");
                return false;
        }
        var ue = UE.getEditor('editor').getContent();
        if ($.trim(ue) != "") {

            $("#editoryy").attr("class", "Validform_checktip  Validform_right");
            $("#editoryy").html("验证通过");
        }
        else {
            $("#editoryy").attr("class", "Validform_checktip Validform_wrong");
            $("#editoryy").html("请填写内容");
            return false;
        }
        var n_category = $("#n_category").val();
        var n_picture = $("#u_photo").val();
        var n_id = $("#n_id").val();
        var keywords = $("#keywords").val();
        var description = $("#description").val();
        $("#btnSaveAdd").attr("disabled", "disabled");
        var data = {
            n_id: n_id, n_title: $.trim(n_title), n_info: ue, n_category: $.trim(n_category), n_picture: n_picture, keywords: $.trim(keywords), description: $.trim(description)
        };
        var url = "/NewsRelease/InsertUpdateNewsRelease";
    
        $.post(url, data, function (retJson) {
           $("#btnSaveAddApp").attr("disabled", false);
            if (retJson.success == 1) {
                window.parent.ShowMsg(retJson.msg, "ok", function () { window.parent.global.reload(); window.parent.layer.closeAll(); });
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

//验证新闻标题
function yztitle()
{
    var n_title = $.trim($("#n_title").val());
    if ($.trim(n_title) != "") {

        $("#titleyy").attr("class", "Validform_checktip  Validform_right");
        $("#titleyy").html("验证通过");
    }
    else {
        $("#titleyy").attr("class", "Validform_checktip Validform_wrong");
        $("#titleyy").html("请填写标题");
        return false;
    }

}
//验证内容
function yzeditor ()
{
    var ue = UE.getEditor('editor').getContent();
    if ($.trim(ue) != "") {

        $("#editoryy").attr("class", "Validform_checktip  Validform_right");
        $("#editoryy").html("验证通过");
    }
    else {
        $("#editoryy").attr("class", "Validform_checktip Validform_wrong");
        $("#editoryy").html("请填写标题");
        return false;
    }
}

// file控件值有改变就上传
function FileChange() {
    var filedata = $("#certificatefile").val();
    if (filedata.length > 0) {
        var isImg = CheckImgExtName(filedata);
        if (isImg) {
            ajaxUpload();
        } else {
            window.parent.ShowMsg("请选择图片（.jpg.png.bmp.jpeg）！", "error", "");
            ModifyTipCss("u_photo_tip", "请选择图片（.jpg.png.bmp.jpeg）！");
        }
    } else {
        ModifyTipCss("u_photo_tip", "请选择图片！");
    }
}

//上传图片
function ajaxUpload() {
    var iurl = "";
    iurl = $("#u_photo").val();

    $.ajaxFileUpload({
        url: '/NewsRelease/UploadImg',
        type: 'post',
        secureuri: false,
        fileElementId: 'certificatefile',//文件上传控件的id属性
        dataType: 'json',
        data: { tid: '123', tname: 'lunis', purl: iurl },
        success: function (data, status) {
            if (data.success == "1") {
                $("#certificate").attr("src", data.imgurl);
                $("#u_photo").val(data.imgurl);
                ModifySuccCss("u_photo_tip");
            } else {
                ModifyTipCss("u_photo_tip", data.mess)
            }
        },
        error: function (data, status, e) {
            
        }
    });
    return;
}
//分页
function ArticleManage(pageIndex, pageSize) {
    var url = "/NewsRelease/NewsReleaseList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var searchType = $("#searchType").val();
    var sea_name = $("#sea_name").val();
    var SelectState = $("#SelectState").val();
    var searchDesc = $("#searchDesc").val();
    var auditstate = $("#auditstate").val();
    var r_begin = $.trim($("#stime").val());
    var r_end = $.trim($("#etime").val());
    url += "&type=" + searchType + "&sea_name=" + sea_name + "&SelectState=" + SelectState + "&searchDesc=" + searchDesc + "&auditstate=" + auditstate + "&r_begin=" + r_begin + "&r_end=" + r_end;
    location.href = encodeURI(url);
}
//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//列表查询
function serchlocuser() {//查询
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}
//添加新闻管理
function AddAPPlog() {
    window.parent.ShouwDiaLogWan("添加新闻", 900, 700, "/NewsRelease/NewsReleaseAdd");
}
//修改新闻
function UpdateComplaint(c_id) {

    window.parent.ShouwDiaLogWan("修改新闻", 900, 700, "/NewsRelease/NewsReleaseAdd?c_id=" + c_id);
}



