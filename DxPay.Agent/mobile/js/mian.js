
//消息提示框
//content:提示内容
//info:提示类型,error:错误;info:信息;ok:成功
//callback:回调函数,格式：functiong（）{}
function ShowMsg(content, info, callback) {
    layer.msg(content, {
        icon: info == "error" ? 2 : info == "ok" ? 1 : 0,
        time: 1000,
        skin: 'layer-ext-moon',
    }, callback == "" ? "" : callback);
}

//页面弹出框无按钮 
//caption:标题
//width:页面宽度
//height:页面高度
//url:页面地址
function ShouwDiaLogWan(caption, width, height, url) {
    layer.open({
        type: 2,
        title: caption,
        shadeClose: false,
        shade: 0.4,
        area: [width + 'px', height + 'px'],
        maxmin: true,
        content: url
    });
}

//关闭窗口
function btnCodesc() {
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
}

//列表展开关闭通用控制方法
function clickOpen(clickId) {
    var Icon = "Icon_" + clickId;
    var Open = "Open_" + clickId;
    var Opendisplay = $("#" + Open).css("display");
    if (Opendisplay == "none") {
        $("td[name='Open']").css("display", "none");
        $("i[name='Icon']").removeClass("fa-minus-circle");
        $("i[name='Icon']").addClass("fa-plus-circle");
        $("#" + Open).css("display", "");
        $("#" + Icon).removeClass("fa-plus-circle");
        $("#" + Icon).addClass("fa-minus-circle");
    } else {
        $("#" + Open).css("display", "none");
        $("#" + Icon).removeClass("fa-minus-circle");
        $("#" + Icon).addClass("fa-plus-circle");
    }
}

//刷新首页
function homerefresh() {
    var tabcnt = $('#tab-panel-content-wrapper .cont', parent.document);
    $(tabcnt[0]).find("iframe").attr("src", '/Home/Index');//homesrc
}
var pop = {
    showSearch: function (searchContainer) {
        layer.open({
            type: 1,
            title: "查询选项",
            closeBtn: 1,
            area: ["100%", '100%'],
            //skin: 'layui-layer-nobg',
            shadeClose: true,
            maxmin: false,
            anim: 1,
            content: $('.' + searchContainer)
        });
    }
};

