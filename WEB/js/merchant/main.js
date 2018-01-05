// JavaScript Document

//查询用户列表
function SearchMerchant() {
    //当前页
    var CurrcentPage = $("#curr_page").val();
    //每页记录数
    var PageSize = $("#pagexz").val();
    LoadData(CurrcentPage, PageSize);
}

//加载数据
function LoadData(currPage, pageSize) {
    
    var url = "/merchant/list?curr=" + currPage + "&psize=" + pageSize;
    var keys = $("#s_keys").val();
    var types = $("#search_Type").val();

    var state = $("#s_state").val();
    var sort = $("#s_sort").val();
    url += "&skeys=" + keys + "&stype=" + types + "&state=" + state + "&s_sort=" + sort;
    location.href = encodeURI(url);
}

//选择每页显示数量
function pagexz() {
    //每页记录数
    var PageSize = $("#pagexz").val(); 
    LoadData(1, PageSize);
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


//批量更新
function doAll(obj) {
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
    $.post("/merchant/updatestate", { ids: vals, state: obj }, function (result) {
        if (result.success === 1) {
            window.parent.ShowMsg(result.msg, "ok", function () {
                window.parent.global.reload();
                window.parent.layer.closeAll();
            });
        } else if (result.success === 9998) {
            window.parent.ShowMsg(result.msg, "error", "");
            return;
        } else if (result.success === 9999) {
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
    window.parent.ShouwDiaLogWan("新增用户", 650, 300, "/merchant/create");
}

function checkLoginName() {
    var zzyz = /^[\w\W]{3,20}$/;
    var inputName = $("#m_loginname").val();
    if ($.trim(inputName) != "") {
        if (zzyz.test(inputName)) {
            $("#y_m_loginname").attr("class", "Validform_checktip  Validform_right");
            $("#y_m_loginname").html("验证通过");
        } else {
            $("#y_m_loginname").attr("class", "Validform_checktip Validform_wrong");
            $("#y_m_loginname").html("登录名长度不少于3且不超过20");
            return false;
        }
    } else {
        $("#y_m_loginname").attr("class", "Validform_checktip Validform_wrong");
        $("#y_m_loginname").html("登录名称不能为空");
        return false;
    }
    return false;
}

function checkPassword() {
    var zzyz = /^[\w\W]{6,20}$/;
    var inputName = $("#m_pwd").val();
    if ($.trim(inputName) != "") {
        if (zzyz.test(inputName)) {
            $("#yz_m_pwd").attr("class", "Validform_checktip  Validform_right");
            $("#yz_m_pwd").html("验证通过");
        } else {
            $("#yz_m_pwd").attr("class", "Validform_checktip Validform_wrong");
            $("#yz_m_pwd").html("密码长度不少于6且不超过20");
            return false;
        }
    } else {
        $("#yz_m_pwd").attr("class", "Validform_checktip Validform_wrong");
        $("#yz_m_pwd").html("密码不能为空");
        return false;
    }
    return false;
}

function checkRealName() {
    var zzyz = /^[\w\W]{3,20}$/;
    var inputName = $("#m_realname").val();
    if ($.trim(inputName) != "") {
        if (zzyz.test(inputName)) {
            $("#yz_m_realname").attr("class", "Validform_checktip  Validform_right");
            $("#yz_m_realname").html("验证通过");
        } else {
            $("#yz_m_realname").attr("class", "Validform_checktip Validform_wrong");
            $("#yz_m_realname").html("真实名长度不少于3且不超过20");
            return false;
        }
    } else {
        $("#yz_m_realname").attr("class", "Validform_checktip Validform_wrong");
        $("#yz_m_realname").html("真实名不能为空");
        return false;
    }
    return false;
}

//保存用户（新增）
function CreateMerchant() {
    $("#btnCreateMerchant").attr("disabled", "disabled");
    //表单数据
    var fd = GetFormData("frm_create_merchant");
    var tmsg = "";

    //验证密码
    var pwd = fd.m_pwd;
    if (pwd.length < 6) {
        ModifyTipCss("u_password_tip", "密码不能小于6位！");
        tmsg += "密码不能小于6位！";
    }

    if (tmsg === "") {
        $("#btnCreateMerchant").attr("disabled", "disabled");
        $.post("/merchant/create", fd, function (result) {
            $("#btnCreateMerchant").removeAttr("disabled");
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
    } else {
        window.parent.ShowMsg("有输入项为正确输入，请确认！", "error", "");
        $("#btnCreateMerchant").removeAttr("disabled");
    }
}

//编辑页面弹窗
function UpdateUser(obj) {
    window.parent.ShouwDiaLogWan("编辑用户", 650, 300, "/merchant/edit?id=" + obj);
}

//保存用户（编辑）
function ModifyMerchant() {
    $("#btnEditMerchant").attr("disabled", "disabled");
    //表单数据
    var fd = GetFormData("frm_edit_merchant");
    var tmsg = "";

    if (tmsg === "") {
        $("#btnEditMerchant").attr("disabled", "disabled");
        //获取认证状态
        fd.u_auditstate = $('input[name="u_auditstate"]:checked').val();
        $.post("/merchant/edit", fd, function (result) {
            if (result.success === 1) {
                window.parent.ShowMsg(result.msg, "ok", function () {
                    window.parent.global.reload();
                    window.parent.layer.closeAll();
                });
            } else if (result.success === 9998) {
                window.parent.ShowMsg(result.msg, "error", "");
                //return;
            } else if (result.success === 9999) {
                window.parent.ShowMsg(result.msg, "error", "");
                window.top.location.href = retJson.Redirect;
                //return;
            } else {
                window.parent.ShowMsg(result.msg, "error", "");
                //return;
            }
            $("#btnEditMerchant").removeAttr("disabled");
        });
    } else {
        window.parent.ShowMsg("有输入项为正确输入，请确认！", "error", "");
        $("#btnEditMerchant").removeAttr("disabled");
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
    $("#" + tid).attr("class", "Validform_checktip Validform_wrong");
    $("#" + tid).html(content);
}

//修改验证成功提示样式
//tid:提示控件id
function ModifySuccCss(tid) {
    $("#" + tid).attr("class", "Validform_checktip Validform_right");
    $("#" + tid).html("通过信息认证！");
}