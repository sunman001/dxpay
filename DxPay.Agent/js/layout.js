// JavaScript Document
$(function () {
    ControlsGood();

});

function TabObj(obj, title, url) {
    $(obj).parent().parent().siblings().hide();
    $(obj).parent().parent().show();
    $(obj).parent().siblings().removeClass("selected");
    $(obj).parent().addClass("selected");
    top.document.getElementById("mainFrame").src = url;
}

function FrameToUrl(url) {
    top.document.getElementById("mainFrame").src = url;
}

//Tab控制函数
function tabs(tabObj) {
    var tabNum = $(tabObj).parent().index("li")
    //设置点击后的切换样式
    $(tabObj).parent().parent().find("li a").removeClass("selected");
    $(tabObj).addClass("selected");
    //根据参数决定显示内容
    $(".plate-tab-form").hide();
    $(".plate-tab-form").eq(tabNum).show();
}

//Tab控制函数
function dialogTab(tabObj) {
    var tabNum = $(tabObj).parent().index("li")
    //设置点击后的切换样式
    $(tabObj).parent().parent().find("li a").removeClass("selected");
    $(tabObj).addClass("selected");
    //根据参数决定显示内容
    $(".plate-dialog-tab-form").hide();
    $(".plate-dialog-tab-form").eq(tabNum).show();
}

//全选取消按钮函数
function checkAll(chkobj) {
    if (chkobj.checked) {
        $(".checkall:enabled").prop("checked", true);
    } else {
        $(".checkall:enabled").prop("checked", false);
    }
}

//执行回传函数
function ExePostBack(objId, objmsg, W, api) {
    var arguments = arguments.length;
    if ($(".checkall:checked").length < 1) {
        $.dialog.alert('对不起，请选中您要操作的记录！');
        return false;
    }

    var msg = "删除记录后不可恢复，您确定吗？";
    if (arguments >= 2) {
        msg = objmsg;
    }
    if (arguments <= 2) {
        $.dialog.confirm(msg, function () {
            __doPostBack(objId, '');
        });
    } else {
        W.$.dialog.confirm(msg, function () {
            __doPostBack(objId, '');
        }, function () { api.zindex(); }, api);
    }

    return false;
}

//检查是否有选中再决定回传
function CheckPostBack(objId, objmsg, W, api) {
    var arguments = arguments.length;
    var msg = "对不起，请选中您要操作的记录！";
    if (arguments >= 2) {
        msg = objmsg;
    }
    if ($(".checkall:checked").size() < 1) {
        if (arguments <= 2) {
            $.dialog.alert(msg);
        } else {
            W.$.dialog.alert(msg, function () { api.zindex(); }, api);
        }
        return false;
    }
    __doPostBack(objId, '');
    return false;
}

//执行回传无复选框确认函数
function ExeNoCheckPostBack(objId, objmsg, W, api) {
    var arguments = arguments.length;
    var msg = "删除记录后不可恢复，您确定吗？";
    if (arguments >= 2) {
        msg = objmsg;
    }
    if (arguments <= 2) {
        $.dialog.confirm(msg, function () {
            __doPostBack(objId, '');
        });
    } else {
        W.$.dialog.confirm(msg, function () {
            __doPostBack(objId, '');
        }, function () { api.zindex(); }, api);
    }
    return false;
}

//获取浏览器类型
function isIE() {
    if (!!window.ActiveXObject || "ActiveXObject" in window) {
        return true;
    } else {
        return false;
    }
}

//获取IE版本号
function IEVersion() {
    var rv = -1;
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    } else if (navigator.appName == 'Netscape') {
        var ua = navigator.userAgent;
        var re = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}

/*获取页面传值*/
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function ControlsGood() {
    $(".rule-single-checkbox-swtich").ruleSingleCheckboxSwitch();
    $(".rule-single-checkbox").ruleSingleCheckbox();
    $(".rule-multi-checkbox").ruleMultiCheckbox();
    $(".rule-multi-radio").ruleMultiRadio();
    $(".rule-single-select").ruleSingleSelect();
    $(".rule-multi-porp").ruleMultiPorp();
    $(".rule-singless-select").ruleSinglessSelect();
}

//智能浮动层函数
$.fn.smartFloat = function () {
    var position = function (element) {
        var top = element.position().top;
        var pos = element.css("position");
        $(window).scroll(function () {
            var scrolls = $(this).scrollTop();
            if (scrolls > top) {
                if (window.XMLHttpRequest) {
                    element.css({
                        position: "fixed",
                        top: 0
                    });
                } else {
                    element.css({
                        top: scrolls
                    });
                }
            } else {
                element.css({
                    position: pos,
                    top: top
                });
            }
        });
    };
    return $(this).each(function () {
        position($(this));
    });
};

//复选框
$.fn.ruleSingleCheckboxSwitch = function () {
    var singleCheckbox = function (parentObj) {
        //查找复选框
        var checkObj = parentObj.children('input:checkbox').eq(0);
        parentObj.children().hide();
        //添加元素及样式
        var newObj = $('<a href="javascript:;">'
		+ '<i class="off">关闭</i>'
		+ '<i class="on">启用</i>'
		+ '</a>').prependTo(parentObj);
        parentObj.addClass("single-checkbox");
        //判断是否选中
        if (checkObj.prop("checked") == true) {
            newObj.addClass("selected");
        }
        //检查控件是否启用
        if (checkObj.prop("disabled") == true) {
            newObj.css("cursor", "default");
            return;
        }
        //绑定事件
        $(newObj).click(function () {
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
                //checkObj.prop("checked", false);
            } else {
                $(this).addClass("selected");
                //checkObj.prop("checked", true);
            }
            checkObj.trigger("click"); //触发对应的checkbox的click事件
        });
    };
    return $(this).each(function () {
        singleCheckbox($(this));
    });
};

//复选框
$.fn.ruleSingleCheckbox = function () {
    var singleCheckbox = function (parentObj) {
        //查找复选框
        var checkObj = parentObj.children('input:checkbox').eq(0);
        parentObj.children().hide();
        //添加元素及样式
        var newObj = $('<a href="javascript:;">'
		+ '<i class="off">否</i>'
		+ '<i class="on">是</i>'
		+ '</a>').prependTo(parentObj);
        parentObj.addClass("single-checkbox");
        //判断是否选中
        if (checkObj.prop("checked") == true) {
            newObj.addClass("selected");
        }
        //检查控件是否启用
        if (checkObj.prop("disabled") == true) {
            newObj.css("cursor", "default");
            return;
        }
        //绑定事件
        $(newObj).click(function () {
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
                //checkObj.prop("checked", false);
            } else {
                $(this).addClass("selected");
                //checkObj.prop("checked", true);
            }
            checkObj.trigger("click"); //触发对应的checkbox的click事件
        });
    };
    return $(this).each(function () {
        singleCheckbox($(this));
    });
};

//多项复选框
$.fn.ruleMultiCheckbox = function () {
    var multiCheckbox = function (parentObj) {
        parentObj.addClass("multi-checkbox"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        parentObj.find(":checkbox").each(function () {
            var indexNum = parentObj.find(":checkbox").index(this); //当前索引
            var newObj = $('<a href="javascript:;">' + parentObj.find('label').eq(indexNum).text() + '</a>').appendTo(divObj); //查找对应Label创建选项
            if ($(this).prop("checked") == true) {
                newObj.addClass("selected"); //默认选中
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                newObj.css("cursor", "default");
                return;
            }
            //绑定事件
            $(newObj).click(function () {
                if ($(this).hasClass("selected")) {
                    $(this).removeClass("selected");
                    //parentObj.find(':checkbox').eq(indexNum).prop("checked",false);
                } else {
                    $(this).addClass("selected");
                    //parentObj.find(':checkbox').eq(indexNum).prop("checked",true);
                }
                parentObj.find(':checkbox').eq(indexNum).trigger("click"); //触发对应的checkbox的click事件
                //alert(parentObj.find(':checkbox').eq(indexNum).prop("checked"));
            });
        });
    };
    return $(this).each(function () {
        multiCheckbox($(this));
    });
}

//多项选项PROP
$.fn.ruleMultiPorp = function () {
    var multiPorp = function (parentObj) {
        parentObj.addClass("multi-porp"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<ul></ul>').prependTo(parentObj); //前插入一个DIV
        parentObj.find(":checkbox").each(function () {
            var indexNum = parentObj.find(":checkbox").index(this); //当前索引
            var liObj = $('<li></li>').appendTo(divObj)
            var newObj = $('<a href="javascript:;">' + parentObj.find('label').eq(indexNum).text() + '</a><i></i>').appendTo(liObj); //查找对应Label创建选项
            if ($(this).prop("checked") == true) {
                liObj.addClass("selected"); //默认选中
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                newObj.css("cursor", "default");
                return;
            }
            //绑定事件
            $(newObj).click(function () {
                if ($(this).parent().hasClass("selected")) {
                    $(this).parent().removeClass("selected");
                } else {
                    $(this).parent().addClass("selected");
                }
                parentObj.find(':checkbox').eq(indexNum).trigger("click"); //触发对应的checkbox的click事件
                //alert(parentObj.find(':checkbox').eq(indexNum).prop("checked"));
            });
        });
    };
    return $(this).each(function () {
        multiPorp($(this));
    });
}

//多项单选
$.fn.ruleMultiRadio = function () {
    var multiRadio = function (parentObj) {
        parentObj.addClass("multi-radio"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        parentObj.find('input[type="radio"]').each(function () {
            var indexNum = parentObj.find('input[type="radio"]').index(this); //当前索引
            var newObj = $('<a href="javascript:;">' + parentObj.find('label').eq(indexNum).text() + '</a>').appendTo(divObj); //查找对应Label创建选项
            if ($(this).prop("checked") == true) {
                newObj.addClass("selected"); //默认选中
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                newObj.css("cursor", "default");
                return;
            }
            //绑定事件
            $(newObj).click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected");
                parentObj.find('input[type="radio"]').prop("checked", false);
                parentObj.find('input[type="radio"]').eq(indexNum).prop("checked", true);
                parentObj.find('input[type="radio"]').eq(indexNum).trigger("click"); //触发对应的radio的click事件
                //alert(parentObj.find('input[type="radio"]').eq(indexNum).prop("checked"));
            });
        });
    };
    return $(this).each(function () {
        multiRadio($(this));
    });
}

//单选下拉框
$.fn.ruleSingleSelect = function () {
    var singleSelect = function (parentObj) {
        parentObj.addClass("single-select"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        //创建元素
        var titObj = $('<a class="select-tit" href="javascript:;"><span></span><i class="fa fa-angle-down"></i></a>').appendTo(divObj);
        var itemObj = $('<div class="select-items"><ul></ul></div>').appendTo(divObj);
        var arrowObj = $('<i class="arrow "></i>').appendTo(divObj);
        var selectObj = parentObj.find("select").eq(0); //取得select对象
        //遍历option选项
        selectObj.find("option").each(function (i) {
            var indexNum = selectObj.find("option").index(this); //当前索引
            var liObj = $('<li>' + $(this).text() + '</li>').appendTo(itemObj.find("ul")); //创建LI
            if ($(this).prop("selected") == true) {
                liObj.addClass("selected");
                titObj.find("span").text($(this).text());
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                liObj.css("cursor", "default");
                return;
            }
            //绑定事件
            liObj.click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected"); //添加选中样式
                selectObj.find("option").prop("selected", false);
                selectObj.find("option").eq(indexNum).prop("selected", true); //赋值给对应的option
                titObj.find("span").text($(this).text()); //赋值选中值
                arrowObj.hide();
                itemObj.hide(); //隐藏下拉框
                selectObj.trigger("change"); //触发select的onchange事件
                //alert(selectObj.find("option:selected").text());
            });
        });
        //设置样式
        //titObj.css({ "width": titObj.innerWidth(), "overflow": "hidden" });
        //itemObj.children("ul").css({ "max-height": $(document).height() - titObj.offset().top - 62 });

        //检查控件是否启用
        if (selectObj.prop("disabled") == true) {
            titObj.css("cursor", "default");
            return;
        }
        //绑定单击事件
        titObj.click(function (e) {
            e.stopPropagation();
            if (itemObj.is(":hidden")) {
                //隐藏其它的下位框菜单
                $(".single-select .select-items").hide();
                $(".single-select .arrow").hide();
                //位于其它无素的上面
                $(".single-select").css("z-index", "4");
                parentObj.css("z-index", "5");
                //arrowObj.css("z-index", "1");
                //itemObj.css("z-index", "1");
                //显示下拉框
                arrowObj.show();
                itemObj.show();
            } else {
                //位于其它无素的上面
                arrowObj.css("z-index", "");
                itemObj.css("z-index", "");
                //隐藏下拉框
                arrowObj.hide();
                itemObj.hide();
            }
        });
        //绑定页面点击事件
        $(document).click(function (e) {
            selectObj.trigger("blur"); //触发select的onblure事件
            arrowObj.hide();
            itemObj.hide(); //隐藏下拉框
        });
    };
    return $(this).each(function () {
        singleSelect($(this));
    });
}

//表单类下拉列表
$.fn.ruleSinglessSelect = function () {
    var singleSelect = function (parentObj) {
        parentObj.addClass("single-select1"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        //创建元素
        var titObj = $('<a class="select-tit" href="javascript:;"><span></span><i class="fa fa-angle-down"></i></a>').appendTo(divObj);
        var itemObj = $('<div class="select-items"><ul></ul></div>').appendTo(divObj);
        var arrowObj = $('<i class="arrow "></i>').appendTo(divObj);
        var selectObj = parentObj.find("select").eq(0); //取得select对象
        //遍历option选项
        selectObj.find("option").each(function (i) {
            var indexNum = selectObj.find("option").index(this); //当前索引
            var liObj = $('<li>' + $(this).text() + '</li>').appendTo(itemObj.find("ul")); //创建LI
            if ($(this).prop("selected") == true) {
                liObj.addClass("selected");
                titObj.find("span").text($(this).text());
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                liObj.css("cursor", "default");
                return;
            }
            //绑定事件
            liObj.click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected"); //添加选中样式
                selectObj.find("option").prop("selected", false);
                selectObj.find("option").eq(indexNum).prop("selected", true); //赋值给对应的option
                titObj.find("span").text($(this).text()); //赋值选中值
                arrowObj.hide();
                itemObj.hide(); //隐藏下拉框
                selectObj.trigger("change"); //触发select的onchange事件
                //alert(selectObj.find("option:selected").text());
            });
        });
        //设置样式
        //titObj.css({ "width": titObj.innerWidth(), "overflow": "hidden" });
        //itemObj.children("ul").css({ "max-height": $(document).height() - titObj.offset().top - 62 });

        //检查控件是否启用
        if (selectObj.prop("disabled") == true) {
            titObj.css("cursor", "default");
            return;
        }
        //绑定单击事件
        titObj.click(function (e) {
            e.stopPropagation();
            if (itemObj.is(":hidden")) {
                //隐藏其它的下位框菜单
                $(".single-select .select-items").hide();
                $(".single-select .arrow").hide();
                //位于其它无素的上面
                //$(".single-select").css("z-index", "4");
                //parentObj.css("z-index", "5");
                //arrowObj.css("z-index", "1");
                //itemObj.css("z-index", "1");
                //显示下拉框
                arrowObj.show();
                itemObj.show();
            } else {
                //位于其它无素的上面
                arrowObj.css("z-index", "");
                itemObj.css("z-index", "");
                //隐藏下拉框
                arrowObj.hide();
                itemObj.hide();
            }
        });
        //绑定页面点击事件
        $(document).click(function (e) {
            selectObj.trigger("blur"); //触发select的onblure事件
            arrowObj.hide();
            itemObj.hide(); //隐藏下拉框
        });
    };
    return $(this).each(function () {
        singleSelect($(this));
    });
}

function previewImage(file, preview, max_width, max_height) {
    var parmCount = arguments.length;
    var MAXWIDTH = parmCount < 3 ? 250 : max_width;
    var MAXHEIGHT = parmCount < 3 ? 250 : max_height;
    preview = parmCount < 2 ? "preview" : preview;

    var div = document.getElementById(preview);
    if (file.files && file.files[0]) {
        div.innerHTML = '<img id=preview_img>';
        var img = document.getElementById('preview_img');
        img.onload = function () {
            var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
            img.width = rect.width;
            img.height = rect.height;
            img.style.marginLeft = rect.left + 'px';
            img.style.marginTop = rect.top + 'px';
        }

        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);

    }
    else {
        var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
        file.select();
        var src = document.selection.createRange().text;
        div.innerHTML = '<img id="preview_img">';
        var img = document.getElementById('preview_img');
        img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
        var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
        status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
        div.innerHTML = "<div id='divhead' style='width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;margin-left:" + rect.left + "px;" + sFilter + src + "\"'></div>";

    }
}

function clacImgZoomParam(maxWidth, maxHeight, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > maxWidth || height > maxHeight) {
        rateWidth = width / maxWidth;
        rateHeight = height / maxHeight;

        if (rateWidth > rateHeight) {
            param.width = maxWidth;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = maxHeight;
        }
    }

    param.left = Math.round((maxWidth - param.width) / 2);
    param.top = Math.round((maxHeight - param.height) / 2);
    return param;
}

//========================基于lhgdialog插件========================
//可以自动关闭的提示，基于lhgdialog插件
//function jsprint(msgtitle, url, msgcss, callback, api, W) {
//    var argnum = arguments.length;
//    var iconurl = "";
//    switch (msgcss) {
//        case "Success":
//            iconurl = "32X32/succ.png";
//            break;
//        case "Error":
//            iconurl = "32X32/fail.png";
//            break;
//        default:
//            iconurl = "32X32/hits.png";
//            break;
//    }
//    if (argnum == 6) {
//        W.$.dialog.tips(msgtitle, 2, iconurl, function () {
//            callback();
//        }, api);
//    } else {

//        $.dialog.tips(msgtitle, 2, iconurl);
//        //执行回调函数
//        //执行回调函数
//        if (url == "" || url == "javascript:") {
//            if (argnum >= 4) {
//                //setTimeout(callback(), 6000);//提示框显示3秒后在执行操作
//                callback();
//            }
//        } else {
//            if (url == "back") {
//                frames["mainFrame"].history.back(-1);
//            } else if (url != "" && url != "back") {
//                frames["mainFrame"].location.href = url;
//            }
//        }
//    }
//}

//弹出一个Dialog窗口
//function jsdialog(msgtitle, msgcontent, url, msgcss, callback, api, W) {
//    var iconurl = "";
//    var argnum = arguments.length;
//    switch (msgcss) {
//        case "Success":
//            iconurl = "success.gif";
//            break;
//        case "Error":
//            iconurl = "error.gif";
//            break;
//        default:
//            iconurl = "alert.gif";
//            break;
//    }
//    if (argnum == 7) {
//        var dialog = W.$.dialog({
//            title: msgtitle,
//            content: msgcontent,
//            fixed: true,
//            min: false,
//            max: false,
//            lock: true,
//            icon: iconurl,
//            ok: true,
//            parent: api,
//            close: function () {
//                //执行回调函数
//                if (url == "") {
//                    callback();
//                } else {
//                    if (url == "back") {
//                        history.back(-1);
//                    } else {
//                        location.href = url;
//                    }
//                }
//            }
//        });
//        //dialog.index();
//    } else {
//        var dialog = $.dialog({
//            title: msgtitle,
//            content: msgcontent,
//            fixed: true,
//            min: false,
//            max: false,
//            lock: true,
//            icon: iconurl,
//            ok: true,
//            close: function () {
//                //执行回调函数
//                if (url == "" || url == "javascript:") {
//                    if (argnum >= 5) {
//                        callback();
//                    }
//                } else {
//                    if (url == "back") {
//                        frames["mainFrame"].history.back(-1);
//                    } else if (url != "" && url != "back") {
//                        frames["mainFrame"].location.href = url;
//                    }
//                }
//            }
//        });
//        //dialog.index();
//    }
//}

//打开一个最大化的Dialog
//function ShowMaxDialog(tit, url) {
//    $.dialog({
//        title: tit,
//        content: 'url:' + url,
//        min: false,
//        max: false,
//        lock: false
//    }).max();
//}

//通过ID 关闭Dialog
//function dialog_close(dialogid) {
//    $.dialog({ id: dialogid }).close();
//}

//
//function ShowDialog(tit, url, w, h, resize, dialogid, W, api) {
//    var argnum = arguments.length;
//    var width = argnum >= 4 ? w : 800;
//    var height = argnum >= 4 ? h : 600;
//    var ww = $(top.window).width();
//    var wh = $(top.window).height();

//    //width = wrap.offsetWidth - width;
//    //height = wrap.offsetHeight - height;

//    width = width == "auto" ? "auto" : (width > ww ? ww : width);

//    var resize = argnum >= 5 ? resize : false;
//    if (argnum == 6) {
//        var sdialog = $.dialog({
//            id: dialogid,
//            title: tit,
//            content: 'url:' + url,
//            min: true,
//            max: true,
//            lock: true,
//            width: width == "auto" ? "auto" : (width > ww ? ww : width),
//            height: height == "auto" ? "auto" : (height > wh ? wh : height),
//            padding: '5px',
//            fixed: true,
//            resize: resize
//        });
//        sdialog.zindex();

//    } else if (argnum > 6) {
//        var sdialog = W.$.dialog({
//            id: dialogid,
//            title: tit,
//            content: 'url:' + url,
//            min: true,
//            max: true,
//            lock: true,
//            parent: api,
//            width: width == "auto" ? "auto" : (width > ww ? ww : width),
//            height: height == "auto" ? "auto" : (height > wh ? wh : height),
//            padding: '5px',
//            fixed: true,
//            resize: resize,
//            close: function () {
//                api.zindex();
//            }
//        });
//        sdialog.zindex();

//    } else {
//        var sdialog = $.dialog({
//            title: tit,
//            content: 'url:' + url,
//            min: true,
//            max: true,
//            lock: true,
//            width: width == "auto" ? "auto" : (width > ww ? ww : width),
//            height: height == "auto" ? "auto" : (height > wh ? wh : height),
//            padding: '5px',
//            fixed: true,
//            resize: resize
//        });
//        sdialog.zindex();
//    }

//}

//========================基于Validform插件========================
//初始化验证表单
$.fn.initValidform = function (ValidDataType) {
    var argnum = arguments.length;
    var checkValidform = function (formObj) {
        $(formObj).Validform({
            datatype: argnum == 1 ? ValidDataType : null,
            tiptype: function (msg, o, cssctl) {
                /*msg：提示信息;
                o:{obj:*,type:*,curform:*}
                obj指向的是当前验证的表单元素（或表单对象）；
                type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
                curform为当前form对象;
                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；*/
                //全部验证通过提交表单时o.obj为该表单对象;
                if (!o.obj.is("form")) {
                    //定位到相应的Tab页面
                    if (o.obj.is(o.curform.find(".Validform_error:first"))) {
                        var tabobj = o.obj.parents(".plate-tab-form"); //显示当前的选项
                        var tabindex = $(".plate-tab-form").index(tabobj); //显示当前选项索引
                        if (!$(".plate-tab ul li").eq(tabindex).children("a").hasClass("selected")) {
                            $(".plate-tab ul li a").removeClass("selected");
                            $(".plate-tab ul li").eq(tabindex).children("a").addClass("selected");
                            $(".plate-tab-form").hide();
                            tabobj.show();
                        }
                    }
                    //页面上不存在提示信息的标签时，自动创建;
                    if (o.obj.parents("dd").find(".Validform_checktip").length == 0) {
                        o.obj.parents("dd").append("<span class='Validform_checktip' />");
                        o.obj.parents("dd").next().find(".Validform_checktip").remove();
                    }
                    var objtip = o.obj.parents("dd").find(".Validform_checktip");
                    cssctl(objtip, o.type);
                    objtip.text(msg);
                }
            },
            showAllError: true
        });
    };
    return $(this).each(function () {
        checkValidform($(this));
    });
}

$.fn.initValidDialogform = function (ValidDataType) {
    var argnum = arguments.length;
    var checkValidform = function (formObj) {
        $(formObj).Validform({
            datatype: argnum == 1 ? ValidDataType : null,
            tiptype: function (msg, o, cssctl) {
                /*msg：提示信息;
                o:{obj:*,type:*,curform:*}
                obj指向的是当前验证的表单元素（或表单对象）；
                type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
                curform为当前form对象;
                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；*/
                //全部验证通过提交表单时o.obj为该表单对象;

                if (!o.obj.is("form")) {
                    //定位到相应的Tab页面

                    if (o.obj.is(o.curform.find(".Validform_error:first"))) {
                        var tabobj = o.obj.parents(".plate-dialog-tab-form"); //显示当前的选项

                        var tabindex = $(".plate-dialog-tab-form").index(tabobj); //显示当前选项索引

                        if (!$(".plate-tab ul li").eq(tabindex).children("a").hasClass("selected")) {
                            $(".plate-tab ul li a").removeClass("selected");
                            $(".plate-tab ul li").eq(tabindex).children("a").addClass("selected");
                            $(".plate-dialog-tab-form").hide();
                            tabobj.show();
                        }
                    }
                    //页面上不存在提示信息的标签时，自动创建;
                    if (o.obj.parents("dd").find(".Validform_checktip").length == 0) {
                        o.obj.parents("dd").append("<span class='Validform_checktip' />");
                        o.obj.parents("dd").next().find(".Validform_checktip").remove();
                    }
                    var objtip = o.obj.parents("dd").find(".Validform_checktip");

                    cssctl(objtip, o.type);
                    objtip.text(msg);

                }
            },
            showAllError: true
        });
    };
    return $(this).each(function () {
        checkValidform($(this));
    });
}

$.fn.initValidformAuto = function (ValidDataType) {
    var argnum = arguments.length;
    var checkValidform = function (formObj) {
        $(formObj).Validform({
            //btnSubmit: ".form-filed-btn",
            datatype: argnum == 1 ? ValidDataType : null,
            tiptype: function (msg, o, cssctl) {
                /*msg：提示信息;
                o:{obj:*,type:*,curform:*}
                obj指向的是当前验证的表单元素（或表单对象）；
                type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
                curform为当前form对象;
                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；*/
                //全部验证通过提交表单时o.obj为该表单对象;
                if (!o.obj.is("form")) {
                    //页面上不存在提示信息的标签时，自动创建;
                    if (o.obj.parents("dd").find(".Validform_checktip").length == 0) {
                        o.obj.parents("dd").append("<span class='Validform_checktip' />");
                        o.obj.parents("dd").next().find(".Validform_checktip").remove();
                    }
                    var objtip = o.obj.parents("dd").find(".Validform_checktip");
                    cssctl(objtip, o.type);
                    objtip.text(msg);
                }
            },
            showAllError: true
        });
    };
    return $(this).each(function () {
        checkValidform($(this));
    });
}

$.fn.initNowTime = function () {
    var mytime = function (o) {
        mytime.panle = o;
        mytime.showTime = function (o) {
            var today = new Date();
            var y = today.getFullYear(); //getYear();
            var month = today.getMonth() + 1;
            var d = today.getDate();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();
            var day = today.getDay();
            var Day = new Array(7);
            Day[0] = "星期日"
            Day[1] = "星期一"
            Day[2] = "星期二"
            Day[3] = "星期三"
            Day[4] = "星期四"
            Day[5] = "星期五"
            Day[6] = "星期六"
            h = mytime.checkTime(h);
            m = mytime.checkTime(m);
            s = mytime.checkTime(s);
            var str = y + "年" + month + "月" + d + "日 " + " " + Day[day] + " " + h + ":" + m + ":" + s;
            $(o).text(str);
        }
        mytime.checkTime = function (i) {
            if (i < 10)
            { i = "0" + i }
            return i
        }
        mytime.showTime(mytime.panle);
        window.setInterval(function () { mytime.showTime(mytime.panle); }, 1000);

        return mytime;
    };
    return $(this).each(function () {
        mytime($(this));
    });
}

//百度编辑器调用
/*
$.fn.UEditor = function (width, height) {
    var TFormUEditor = function (editor, ewidth, eheight) {
        if (arguments.length == 3) {
            UE.getEditor(editor, {
                initialFrameWidth: ewidth,
                initialFrameHeight: eheight
            });
        } else {
            UE.getEditor(editor, {
                initialFrameHeight: eheight
            });
        }
    }

    if (arguments.length == 2) {
        return $(this).each(function () {
            TFormUEditor($(this).attr("id"), width, height);
        });
    } else {
        return $(this).each(function () {
            TFormUEditor($(this).attr("id"), 200);
        });
    }
}

$.fn.UEditorSimple = function (width, height) {
    var TFormUEditor = function (editor, ewidth, eheight) {
        if (arguments.length == 3) {
            UE.getEditor(editor, {
                initialFrameWidth: ewidth,
                initialFrameHeight: eheight,
                toolbars: [['source', 'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'link', 'unlink', 'anchor', 'imagenone', 'imageleft', 'imageright', 'imagecenter', 'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'insertframe', 'pagebreak', 'template', 'background']]
            });
        } else {
            UE.getEditor(editor, {
                initialFrameHeight: eheight,
                toolbars: [['source', 'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'link', 'unlink', 'anchor', 'imagenone', 'imageleft', 'imageright', 'imagecenter', 'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'insertframe', 'pagebreak', 'template', 'background']]
            });
        }
    }

    if (arguments.length == 2) {
        return $(this).each(function () {
            TFormUEditor($(this).attr("id"), width, height);
        });
    } else {
        return $(this).each(function () {
            TFormUEditor($(this).attr("id"), 200);
        });
    }
}*/

$.fn.ruleIMG = function (width, height) {
    var TruleIMG = function (obj, ewidth, eheight) {
        //var table = $("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"></table>").prependTo($(obj).parent()).css({ "width": width + "px", "height": height + "px", "text-align": "center", "vertical-align": "middle" });
        //var tr = $("<tr></tr>").prependTo($(table));
        //var td = $("<td></td>").prependTo($(tr));
        //$(td).append($(obj)).css({ "width": width + "px", "height": height + "px", "text-align": "center", "vertical-align": "middle", "padding": "0px", "margin": "0px" });

        $(obj).load(function () {
            var img = new Image();
            img.src = $(this).attr("src");
            var rect = clacImgZoomParam(width, height, img.width, img.height);
            $(this).css({ "width": rect.width + "px", "height": rect.height + "px", "marginLeft": rect.left + "px", "marginTop": rect.top + "px" });
            //document.write("width" + rect.width + "px" + "height" + rect.height + "px" + "marginLeft" + rect.left + "px" + "marginTop" + rect.top + "px");
        });

        var clacImgZoomParam = function (maxWidth, maxHeight, width, height) {
            var param = { top: 0, left: 0, width: width, height: height };
            if (width > maxWidth || height > maxHeight) {
                rateWidth = width / maxWidth;
                rateHeight = height / maxHeight;

                if (rateWidth > rateHeight) {
                    param.width = maxWidth;
                    param.height = Math.round(height / rateWidth);
                } else {
                    param.width = Math.round(width / rateHeight);
                    param.height = maxHeight;
                }
            }

            param.left = Math.round((maxWidth - param.width) / 2);
            param.top = Math.round((maxHeight - param.height) / 2);
            return param;
        }
    }
    return $(this).each(function () {
        TruleIMG($(this), width, height);
    });

}

function mainHeightAuto() {
    var winHeight = $(window).height();
    var bodyHeight = $("body").height();
    if (winHeight > bodyHeight) {
        $(".copyright").css({ "position": "absolute", "left": "0px", "right": "0px", "bottom": "0px" });
    } else {
        $(".copyright").removeAttr("style");
    }
}
