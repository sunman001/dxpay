$(function(){

    $(".nav > li:first-child").addClass("active");

    //左导航
    $('.nav > li > a').on('click', function () {
        var item = $(this).parent().find('.submenu');
        if (item) {
            $('.submenu').hide(200);
            var ds=$(this).parent().find('.submenu').css("display");
            if(ds=="block"){
                $('.nav > li').removeClass('active');
                $(this).find('.fa-add').removeClass("on");
                //alert(1)
            }else{
                $(this).parent().find('.submenu').show(200);
                $('.fa-add').removeClass("on");
                $(this).find('.fa-add').addClass("on");
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

    //图表tab切换
    $(".terminal-nav li").click(function () {
        $(".terminal-nav li").removeClass("active");
        $(this).addClass("active");
    })
})