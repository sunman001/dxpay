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
        icon: info === "error" ? 2 : info === "ok" ? 1 : 0,
        time: 2000,
        skin: 'layer-ext-moon'
    }, callback === "" ? "" : callback);
}

//消息提示框
//content:提示内容
//info:提示类型,error:错误;info:信息;ok:成功
//callback:回调函数,格式：functiong（）{}
//times 时间
function ShowMsgtime(times, content, info, callback) {
    layer.msg(content, {
        icon: info === "error" ? 2 : info === "ok" ? 1 : 0,
        time: times == "" ? 500000 : times,
        skin: 'layer-ext-moon',
        shade: [0.5, '#000',true],
    }, callback === "" ? "" : callback);
}

//消息提示框
//content:提示内容
//info:提示类型,error:错误;info:信息;ok:成功
//callback:回调函数,格式：functiong（）{}
function _alert(content, info, time, callback) {
    var timeout = 2500;
    if (time) {
        timeout = time;
    }
    layer.msg(content, {
        icon: info === "error" ? 2 : info === "ok" ? 1 : 0,
        time: timeout,
        skin: 'layer-ext-moon'
    }, callback === "" ? "" : callback);
}

function PopupMessage(message) {
    layer.open({
        type: 1,
        area: '800px',
        shadeClose: true, //点击遮罩关闭
        maxmin: true,
        content: '\<\pre style="padding:20px;">' + message + '\<\/pre>'
    });
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
        area: [width + 'px', height + 'px'],
        maxmin: true,
        content: url,
        btn: ['保存', '取消'],
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
        icon: 16,
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