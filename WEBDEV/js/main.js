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

//添加选项卡
/*
name:标题;
isLeaf:是否追加tab；
href：链接；
id：用于tab的li选项卡处理；
type：判断是否是iframe内容追加内容；
*/
var homesrc = '';
function AddTab(name, isLeaf, href, id, type) {
    var tab = $("#tab-panel-menu-list");//tab
    var tabper=$("#tab-panel-content-wrapper");//追加内容
    var tabcnt = $("#tab-panel-content-wrapper .cont");//tab内容
    var prd = $(".tab-dvrep").innerWidth() - 80;//tab-ul父级宽度；
    var pageli = tab.find("li");//tab下的li

    homesrc = href;
    if (type == "child") {
        tab = $('#tab-panel-menu-list', parent.document);
        tabper = $("#tab-panel-content-wrapper", parent.document);
        tabcnt = $('#tab-panel-content-wrapper .cont', parent.document);
        prd = $(".tab-dvrep", parent.document).innerWidth() - 80;
        pageli = tab.find("li");
    }

    if (pageli.length > 0) {//判断是否有重复tab标签pageli[i].innerText.replace('×', '')
        for (var i = 0; i < pageli.length; i++) {
            if ($.trim($(pageli[i]).text()) === $.trim(name)) {
                tab.find("li").removeClass("active");
                tabcnt.removeClass("active");
                $(pageli[i]).addClass("active");
                $(tabcnt[i]).addClass("active");

                //定位tab
                var lnums = tab.find("li.active");
                var tns = 0;
                var bigb = tab.innerWidth() - prd;//滑动最大的像素
                if (tab.innerWidth() > prd) {
                    for (var i = 0; i < lnums.index() ; i++) {
                        tns += $(pageli[i]).outerWidth();
                    }
                    if (bigb < tns) {
                        tab.css("transform", "translate3d(-" + bigb + "px, 0px, 0px)");
                    } else {
                        tab.css("transform", "translate3d(-" + tns + "px, 0px, 0px)");
                    }
                }
                window.location.hash = href;
                if (type == "child") {
                    $(tabcnt[i]).find("iframe").attr("src", href);
                }
                return;
            }
        }
    }
    var baseUrl = '@Url.Content("~/")';
    id = "id_" + id;
    if (isLeaf !== undefined) {
        var hei = document.documentElement.clientHeight;//浏览器高度
        var wh = document.documentElement.clientWidth;//浏览器宽度
        var ifrhei = hei - 125;//iframe可见高度

        if (type == "child") {
            console.info(parent.document.documentElement.clientHeight);
            hei = document.documentElement.clientHeight + 125;
            wh = document.documentElement.clientWidth + 250;
            ifrhei =849;
            if(href=='/User/BankCardList')
            {
                ifrhei = parent.document.documentElement.clientHeight;
                if (ifrhei > 960) {
                    ifrhei = 850;
                }
            }
        }

        tab.find("li").removeClass("active");

        global.currentTabId = id;

        window.location.hash = href;
        tabcnt.removeClass("active");//$("#tab-panel-content-wrapper .cont")
        tab.append('<li class="active"><span data-href="' + href + '" onclick="tabmenu(this,\'' + global.currentTabId + '\')">' + name + '</span> <i class="fa fa-times-circle" onclick="tabClose(this)"></i></li>');
        tabper.append('<div class="cont active"><iframe name="' + global.currentTabId + '" src="' + href + '" height=' + ifrhei + '></iframe></div>');

        //tab溢出隐藏
        var prdwht = 0;
        var lnum = tab.find("li");//li的数量
        for (var i = 0; i < lnum.length; i++) {
            prdwht += $(lnum[i]).outerWidth();
            if (prdwht > prd) {
                tab.css("width", prdwht + 10 + 1);
                var tns = prdwht - prd - 1;
                tab.css("transform", "translate3d(-" + tns + "px, 0px, 0px)");
                tab.css("transition", "all 0.2s ease-out 0s");
            }
        }
    }
}

//计算iframe高度宽度
function height() {
    var hei = document.documentElement.clientHeight;//浏览器高度
    var wh = document.documentElement.clientWidth;//浏览器宽度
    var ifrhei = hei - 125;//iframe可见高度
    $("#tab-panel-content-wrapper").find("iframe").css("height", ifrhei);
}

//tab选项卡点击事件
function tabmenu(th, id) {
    $("#tab-panel-menu-list li").removeClass("active");
    $("#tab-panel-content-wrapper .cont").removeClass("active");
    var target = $(th);
    $(th).parents("li").addClass("active");
    var idx = $(th).parents("li").index();
    $($("#tab-panel-content-wrapper .cont").get(idx)).addClass("active");
    global.currentTabId = id;
    window.location.hash = target.attr("data-href");

}

//tab选项卡关闭
function tabClose(th) {
    var tabli = $("#tab-panel-menu-list li");
    var idx = $(th).parents("li").index();
    if (tabli.length > 1) {
        $("#tab-panel-menu-list li").removeClass("active");
        $("#tab-panel-content-wrapper .cont").removeClass("active");

        $(th).parents("li").remove();
        $($("#tab-panel-content-wrapper .cont").get(idx)).remove();

        var tablis = $("#tab-panel-menu-list li").length;
        $($("#tab-panel-menu-list li").get(tablis - 1)).addClass("active");
        $($("#tab-panel-content-wrapper .cont").get(tablis - 1)).addClass("active");

        //定位tab
        var prd = $(".tab-dvrep").innerWidth() - 80;//tab-ul父级宽度；
        var tab = $("#tab-panel-menu-list");//tab标题
        var pageli = tab.find("li");
        var prdwht = 0;
        if (tab.innerWidth() > prd) {
            for (var i = 0; i < pageli.length; i++) {
                prdwht += $(pageli[i]).outerWidth();
            }
            if (prdwht < prd) {
                tab.css("transform", "translate3d(0px, 0px, 0px)");
            } else {
                tab.css("width", prdwht + 10 + 1);
                tab.css("transform", "translate3d(-" + (prdwht - prd) + "px, 0px, 0px)");
            }
        }
    }
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

//关闭类似添加、编辑等iframe窗口
function closeIfram(name, isLeaf, href, id,type) {
    var tabli = $('#tab-panel-menu-list li', parent.document);//tab选项卡
    var tabcnt = $('#tab-panel-content-wrapper .cont', parent.document);//iframe
    var idx = $('#tab-panel-menu-list li[class="active"]', parent.document).index();//.attr("data-num");//当前操作的tab选项卡下标

    if (tabli.length > 1) {
        AddTab(name, isLeaf, href, id, type);
        $(tabli.get(idx)).remove();
        $(tabcnt.get(idx)).remove();
    }
}

//刷新首页
function homerefresh() {
    var tabcnt = $('#tab-panel-content-wrapper .cont', parent.document);
    $(tabcnt[0]).find("iframe").attr("src", homesrc);
}




