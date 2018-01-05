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
    var url = "/merchant/ChoiceMerchantList?curr=" + currPage + "&psize=" + pageSize;
    var types = $("#search_Type").val();
   
    var keys = $("#s_keys").val();
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



//选择商务
function yxuzyhuser(u_id, m_realname, index) {
    window.parent.layer.getChildFrame("#a_user_idyx", index).val(m_realname);
    window.parent.layer.getChildFrame("#a_user_id", index).val(u_id);
    window.parent.layer.getChildFrame("#yzkfz", index).attr("class", "Validform_checktip  Validform_right");
    window.parent.layer.getChildFrame("#yzkfz", index).html("验证通过");
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
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