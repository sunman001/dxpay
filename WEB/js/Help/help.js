
$(document).ready(function () {

    //添加或修改分类表
    $("#btnClass").click(function () {
        var ID = $.trim($("#ID").val());
        var ParentID = $.trim($("#ParentID").val());
        var ClassName = $("#ClassName").val();
        if (ClassName == "") {
            $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
            $("#nameyy").html("请填写分类名称");
            return false;
        }
        else {
            $("#nameyy").attr("class", "Validform_checktip Validform_right");
            $("#nameyy").html("验证通过");
        }

        var rep = /^\+?[0-9][0-9]*$/;
        var Sort = $("#Sort").val();
        if (Sort == "") {
            $("#SortValue").attr("class", "Validform_checktip Validform_wrong");
            $("#SortValue").html("请填写排序");
            return false;
        }
        else {
            if (!rep.test(Sort)) {
                $("#SortValue").attr("class", "Validform_checktip Validform_wrong");
                $("#SortValue").html("排序只能为数字");
                return false;
            }
            else {
                $("#SortValue").attr("class", "Validform_checktip Validform_right");
                $("#SortValue").html("验证通过");
            }
        }
        var Icon = $("#Icon").val();
        var Description = $("#Description").val();
        var Type = $("#Type").val();
    
        $("#btnClass").attr("disabled", "disabled");
        var data = { ID: $.trim(ID), ParentID: $.trim(ParentID), ClassName: $.trim(ClassName), Sort: $.trim(Sort), Icon: $.trim(Icon), Description: $.trim(Description), Type: $.trim(Type) };
        var url = "/Help/AddorEidt";
        $.post(url, data, function (retJson) {
            $("#btnClass").attr("disabled", false);
            if (retJson.success == 1) {

                window.parent.global.reload();
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
    })
    //添加或者修改内容
    $("#btnContentSave").click(function () {
        var Type = $("#Type").val();
        var Title = $.trim($("#Title").val());
        if ($.trim(Title) != "") {
            $("#Titleyy").attr("class", "Validform_checktip Validform_right");
            $("#Titleyy").html("验证通过");

        }
        else {
            $("#Titleyy").attr("class", "Validform_checktip Validform_wrong");
            $("#Titleyy").html("请填写标题");
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
       
        var ID = $.trim($("#ID").val());
        var PrentID = $.trim($("#PrentID").val());
        if (PrentID==0)
        {
            window.parent.ShowMsg("请选择父类", "error", "");
            return false;
        }
        var ClassId = $.trim($("#ClassId").val());
        if (ClassId<0)
        {
            window.parent.ShowMsg("请选择子类", "error", "");
            return false;
        }
        var ISOverhead = $("#ISOverhead").val();
        if (ISOverhead==1)
        {
            ISOverhead=true
        }
        else
        {
            ISOverhead=false
        }
        $("#btnContentSave").attr("disabled", "disabled");
        var data = { ID: $.trim(ID), PrentID: $.trim(PrentID), ClassId: $.trim(ClassId), Title: $.trim(Title), Content: ue, ISOverhead: ISOverhead, Type: Type };

        var url = "/Help/AddorEditContent";
        $.post(url, data, function (retJson) {
            $("#btnContentSave").attr("disabled", false);
            if (retJson.success == 1) {

                window.parent.global.reload();
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
    })

    })



//分页
function ArticleManage(pageIndex, pageSize) {

    var url = "/Help/ClassificationList?pageIndexs=" + pageIndex + "&PageSize=" + pageSize;
    var ClassName = $("#ClassName").val();
    var Type = $("#Type").val();
    var ParentID = $("#PrentID").val();
    var sType = $("#sType").val();
    url += "&ClassName=" + ClassName + "&Type=" + Type + "&ParentID=" + ParentID + "&sType=" + sType
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//查询
function selectScheduling() {
    var PageSize = $("#pagexz").val();
    ArticleManage(1, PageSize);
}

//添加分类弹窗
function AddSdl() {
    window.parent.ShouwDiaLogWan("添加分类", 800, 600, "/Help/AddClassification");
}

//修改分类弹窗
function UpdateSdl(id) {
    if (id == "") {
        window.parent.ShowMsg("请选择内容！", "error", "");
        return;
    }
    window.parent.ShouwDiaLogWan("修改分类", 800, 600, "/Help/AddClassification?Id=" + id);
}



function YzSort()
{
    var rep = /^\+?[0-9][0-9]*$/;
    var Sort = $("#Sort").val();
    if (Sort=="")
    {
        $("#SortValue").attr("class", "Validform_checktip Validform_wrong");
        $("#SortValue").html("请填写排序");
        return false;
    }
    else
    {
        if (!rep.test(Sort)) 
        {
            $("#SortValue").attr("class", "Validform_checktip Validform_wrong");
            $("#SortValue").html("排序只能为数字");
            return false;
        }
        else
        {
            $("#SortValue").attr("class", "Validform_checktip Validform_right");
         $("#SortValue").html("验证通过");
        }
    }
}

//验证分类名称
function yzname()
{
    var ClassName = $("#ClassName").val();
    if (ClassName=="")
    {
        $("#nameyy").attr("class", "Validform_checktip Validform_wrong");
        $("#nameyy").html("请填写分类名称");
        return false;
    }
    else
    {
        $("#nameyy").attr("class", "Validform_checktip Validform_right");
        $("#nameyy").html("验证通过");
    }
}

//一键启用或禁用
function Updatestate(state) {
    var valArr = new Array;
    $("#table :checkbox[checked]").each(function (i) {
        valArr[i] = $(this).val();
    });
    var vals = valArr.join(',');
    if (vals == "") {
        window.parent.ShowMsg("请选择信息！", "error", "");
        return;
    }
    var url = "/Help/UpdateState";
    var data = { state: state, ids: vals };
    $.post(url, data, function (retJson) {
        if (retJson.success == 1) {
            //window.parent.frames[window.top.global.currentTabId].location.reload();
            window.parent.global.reload();
            window.parent.ShowMsg(retJson.msg, "ok", function () { });
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
//获取子类
function getPrent()
{
    var PrentID = $("#PrentID").val();
    var ClassID = $("#ClassIdHID").val();
    if (PrentID > 0) {

        var data = { PrentID: $.trim(PrentID), ClassID: ClassID };
        var url = "/Help/SelectClassId";
        $.post(url, data, function (msg) {
            $("#ClassId").html(msg);
            $(".rule-single-select").ruleSingleSelect();
        })
    }
}
//获取父类
function getclass()
{
    var PrentID = $("#PrentIDHID").val();
    var Type = $("#Type").val();
    var data = { PrentID: PrentID, Type: Type };
    var url = "/Help/SelectPrentId";
    $.post(url, data, function (msg) {
        $("#PrentID").html(msg);
        $(".rule-single-select").ruleSingleSelect();
    })
}

//验证标题
function yztitle()
{
   
    var Title = $.trim($("#Title").val());
    if ($.trim(Title) != "")
    {
        $("#Titleyy").attr("class", "Validform_checktip Validform_right");
        $("#Titleyy").html("验证通过");
        return false;
    }
    else {
        $("#Titleyy").attr("class", "Validform_checktip Validform_wrong");
        $("#Titleyy").html("请填写标题");
        return false;
       
    }
}
function yzeditor()
{
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
}