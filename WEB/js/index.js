$(document).ready(function(){
            
    //左导航
    $('.nav > li > a').on('click', function () {
            
        var item = $(this).parent().find('.submenu');
        if (item) {
            $('.submenu').hide(200);
            var ds=$(this).parent().find('.submenu').css("display");
            if(ds=="block"){
                $('.nav > li').removeClass('active');
            }else{
                $(this).parent().find('.submenu').show(200);
            }
        }
        $('.nav > li').removeClass('active');
        $(this).parent().addClass('active');
    });

    //二级导航
    $(".submenu > li").on('click', function (e) {
        if ($(e.target).hasClass("btn-toggle")) {
        return;
        }
        $("body").removeClass("sidebar-expanded");
        $('.submenu > li').removeClass('active');
        $(this).addClass('active');
        $('.container').removeClass('nav-small');
        $('.nav-small .nav > li').unbind('hover');
    });

    //隐藏左导航
    $('.header-nav .btn-bars').click(function() {
        $('.container').toggleClass('nav-small');
    });

    //左导航缩小
    $('.nav > li').hover(function () {
        var wh = parseInt($(".nav-small .nav > li > a").width());
        if (wh == 25) {
            $('.nav > li').removeClass("active");
            $('.nav > li').find('.submenu').hide();
            $(this).addClass("active");
            $(this).find('.submenu').show(300);
        }
    });

    var user = 0;
    $(".head-btn-user").click(function () {
        if (user == 0) {
            $(".header-nav .dropdown-menu").show(200);
            $(this).addClass("active");
            user = 1;
        } else {
            $(".header-nav .dropdown-menu").hide(200);
            $(this).removeClass("active");
            user = 0;
        }
    });
});