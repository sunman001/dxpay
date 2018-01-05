// JavaScript Document

//消息提示框
//content:提示内容
//info:提示类型,error:错误;info:信息;ok:成功
//callback:回调函数,格式：functiong（）{}
function ShowMsg(content, info, callback) {
    layer.msg(content, {
        icon: info === "error" ? 2 : info === "ok" ? 1 : 0,
        time: 1500
        //skin: 'layer-ext-moon'
    }, callback === "" ? "" : callback);
}

//页面弹出框
//caption:标题
//width:页面宽度
//height:页面高度
//url:页面地址
//callback:点击保存按钮的回调函数
function ShowDialog(caption, width, height, url, callback) {
    layer.open({
        type: 2,
        title: caption,
        shadeClose: false,
        shade: 0.4,
        area: ["100%", "100%"],
        maxmin: true,
        content: url,
        btn: ["保存", "取消"],
        yes: function (index, layero) {
            //$(layero).find("a.layui-layer-btn0").removeAttr("href");
            //$(layero).find("a.layui-layer-btn0").removeAttr('onclick');
            //$(layero).find("a.layui-layer-btn0").css("outline", "0 none");
            //$(layero).find("a.layui-layer-btn0").css("cursor", "default");
            //$(layero).find("a.layui-layer-btn0").hide();
            callback === "" ? "" : callback(index, layero);
        },
        cancel: function (index) {
            layer.close(index);
        }
    });
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
        //shade: 0.4,
        area: ["100%", "100%"],
        //maxmin: true,
        anim: 1,
        content: url
    });
}
//关闭窗口
function btnCodesc() {
    var indexs = parent.layer.getFrameIndex(window.name);
    window.top.layer.close(indexs);
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