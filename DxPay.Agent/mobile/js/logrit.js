$(function(){
    $(".fa-times-circle").click(function(){
        $(".txtipt-user").val("");
    });
    $(".fa-eye-slash").click(function(){
        $(this).hide(200);
        $(this).parents("li").find("input[type='password']").hide();
        $(this).parents("li").find(".fa-eye").show(200);
        $(this).parents("li").find("input[type='text']").show();
    })
    $(".fa-eye").click(function(){
        $(this).hide(200);
        $(this).parents("li").find("input[type='text']").hide();
        $(this).parents("li").find("input[type='password']").show();
        $(this).parents("li").find(".fa-eye-slash").show(200);
    })
    $(".txtipt-pwdp").keyup(function(){
        var pwd=$(this).val();
        $(".txtipt-pwdt").val(pwd);
    })
    $(".txtipt-pwdt").keyup(function(){
        var pwd=$(this).val();
        $(".txtipt-pwdp").val(pwd);
    })
})