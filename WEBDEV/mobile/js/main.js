// JavaScript Document
//全局参数对象
var global = {
    //当前选中的选项卡ID
    currentTabId: "",
    //重新加载iframe中的页面
    reload: function () {
        window.frames[this.currentTabId].location.reload();
    }
};
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


//判断是否登录、报错、有权限
function CheckJsonData(retJson) {
    if (retJson.success == 9998) {
        ShowMsg(retJson.msg, "error", "");
        //return false;
    } else if (retJson.success == 9999) {
        ShowMsg(retJson.msg, "error", "");
        window.top.location.href = retJson.Redirect;
        //return false;
    } else if (retJson.success == 9997) {
        window.top.location.href = retJson.Redirect;
        //return false;
    }
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



