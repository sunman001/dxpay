$(function(){
    //导航
    $(".nav-lst > li").hover(function(){
        $(this).find(".nav-dcnt").show();
    },function(){
        $(this).find(".nav-dcnt").hide();
    });
    //$(".nav-smart > dd").click(function(){
    //    $(".nav-smart > dd").removeClass("on");
    //    $(this).addClass("on");
    //    $(".nav-lst > li").removeClass("on");
    //    $(this).parents("li").addClass("on");
    //});

    //关闭咨询
    $(".icon-close").click(function(){
        $('.idx-consult').animate({"right":"-330"},300);
        $(".idx-cslt").css('display','block');
        $('.bodys').css('display','none');
    });
    //打开咨询
    $(".idx-cslt").click(function(){
        $('.idx-consult').animate({"right":"0"},300);
        $(".idx-cslt").css('display','none');
        $('.bodys').css('display','block');
    });

    //导航添加样式
    $(window).scroll(function(){
        var widhight = $(window).scrollTop();//移动高度
        if(widhight>100){
            $(".login-menu").addClass("on");
        }else{
            $(".login-menu").removeClass("on");
        }
    });
    //关于我们
    $(".about-smart a").click(function () {
        var num = parseInt($(this).attr("href").split(/#/)[1]);
        about(num);
    });

    //导航定位-产品服务
    $(".product-smart a").click(function () {
        prod($(this), ".prod-");
    });

    //判断网址并锚定到指定位置
    var url = window.location.href;
    if (url.indexOf("about") > -1) {
        var num = $.trim(url.split(/#/)[1]);
        if (num == "") num = 0;
        auto_click(0,parseInt(num));
    } else if (url.indexOf("News") > -1) {
        var num = $.trim(url.split(/#/)[1]);
        if (num == "") num = 0;
        auto_click(0, parseInt(num));
    }
    else if (url.indexOf("product") > -1) {
        var num = $.trim(url.split(/#/)[1]);
        if (num == "") num = 0;
        auto_click(1, parseInt(num));
    }

})


//判断是否是ie8一下浏览器
var DEFAULT_VERSION = "8.0";
var ua = navigator.userAgent.toLowerCase();
var isIE = ua.indexOf("msie")>-1;
var safariVersion;
if(isIE){
    safariVersion =  ua.match(/msie ([\d.]+)/)[1];
    if(safariVersion <= DEFAULT_VERSION ){
        location.href = "/Index/loading";//location.href实现客户端页面的跳转  
    }
}

//关闭咨询
$('.bodys').bind('click', function(e) {  
    var e = e || window.event; //浏览器兼容性   
    var elem = e.target || e.srcElement;  
    while (elem) { //循环判断至跟节点，防止点击的是div子元素   
        if (elem.id && elem.id == 'idx-csult-fix') {  
            return;  
        }  
        elem = elem.parentNode;  
    }  
    $('.idx-consult').animate({"right":"-330"},300); 
    $(".idx-cslt").css('display','block');
    $(".bodys").css('display','none');
});  

//网站跳转锚定到指定位置
function auto_click(type, num) {
    if (type == 0) {
        about(num);
        smart(2,num);
    }
    else if (type == 1) {
        var ys = ".prod-" + num;
        var nums=parseInt($(ys).position().top)-90;
        $('body,html').animate({ scrollTop: nums }, 500);
        smart(1,num);
    }else {
        smart(0,0)
    }

};

//导航-跳转时选中样式处理
function smart(num,nums){
    //$(".nav-lst > li").removeClass("on");
    //$(".nav-smart > dd").removeClass("on");
    //$(".nav-lst > li").eq(num).addClass("on");
    //$(".nav-lst > li").eq(num).find(".nav-smart").find("dd").eq(nums).addClass("on")
}
//导航-关于我们定位
function about(num){
    $(".about-ul li").removeClass("on");
    $($(".about-ul li").get(num)).addClass("on");
    $(".about-tab section").hide();
    $(".about-tab"+num).show();
};
function prod(h, type) {
    var num = type + parseInt(h.attr("href").split(/#/)[1]);
    var nums=parseInt($(num).position().top)-90;
    $('body,html').animate({ scrollTop: nums }, 500);
}














