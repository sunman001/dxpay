$(function(){
    
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

    //关闭左侧导航
    $(".fa-times-circle").click(function(){
        $(".sec-nav").addClass("sec-nav-active");
    });

    //打开左侧导航
    $(".btn-nav").click(function(){
        $(".sec-nav").removeClass("sec-nav-active");
    });

})