var hdwith=1200;
var hdheit=550;
var psterw=1000;
var psterh=550;

$(function(){

	//整屛滚动：默认展示第一屏
    $('.section1 .content').animate({"opacity":1,"top":"0px"},300);
    $('.idx-num').countUp();
    //整屛滚动
    /*if (typeof $.fn.fullpage.destroy == 'function') {
        $.fn.fullpage.destroy('all');
    }*/
    $('#fullpage').fullpage({
        navigation: true ,
        anchors: ['page1','page2', 'page3', 'page4', 'page5'],
        afterLoad: function(anchorLink, index){
            $('.section'+index+' .content').animate({
                "opacity":1,
                "top":"0px"
            },300)
        },
        onLeave: function(index, direction){

            $('.section'+index+' .content').animate({
                "opacity":0,
                "top":"300px"
            },300)                
        }
    });
    //忘记密码
    $(".forget-pwd-content").hover(function(){
        $(".forget-pwd-hover").show(200);
    },function(){
        $(".forget-pwd-hover").hide(200);
    });
    //图片滚动
    lads();
    window.onresize=function(){  
        lads();  
    }  
    
    $(".A_Demo").PicCarousel({
		"width":hdwith,		//幻灯片的宽度
		"height":hdheit,		//幻灯片的高度
		"posterWidth":psterw,	//幻灯片第一帧的宽度
		"posterHeight":psterh, //幻灯片第一张的高度
		"scale":0.6,		//记录显示比例关系
		"speed":1500,		//记录幻灯片滚动速度
		"autoPlay":true,	//是否开启自动播放
		"delay":1000,		//自动播放间隔
	});
    //合作
    jQuery(".idx-bank-slider").slide({ mainCell: ".bd ul", effect: "left", autoPlay: false });
    //关闭弹出框
    $(".icon-close-btn").click(function(){
    	$(".layer-fixed").hide(200);
    	$(".wp-layer").hide(100);
    })


    //第二屏
	var itwo = 155*4; //设置彩色滚动度数
	var jtwo = 175; //设置灰色滚动度数
	//var h = 190*3; //设置内容滚动
	var is_true = true;
	var is_true2=true;
	var is_true3=true;
	(function() {})(
		init() 
	);
	function init(){ //初始动画
		colour();
		gray();
		describe();
	}
	function colour(){ //彩色滚动动画 
			setInterval (function(){
				$('.colour .cont ul').attr('style','');
			colourroll();
			var first = $(".colour .cont ul li:first");
			if(is_true){ 
				is_true = false ;
			}else{
			    $('.colour .cont ul').append(first);
			}
			
		},4000);
	}
	function gray(){ //灰色滚动动画 
			setInterval (function(){
				$('.gray ul').attr('style','');
			grayroll();
			var first = $(".gray ul li:first");
			if(is_true2){ 
				is_true2 = false ;
			}else{
			    $('.gray ul').append(first);
			}
		},4000);
	} 
	function describe(){ //内容滚动
			setInterval (function(){
				$('.roll_left ul').attr('style','');
				$(".roll_left ul").removeClass('fadeInUp animated');
				describeroll();
			var first = $(".roll_left ul li:first");
			if(is_true){ 
				is_true = false ;
			}else{
			    $('.roll_left ul').append(first);
			}
		},4000);
	}
	function colourroll(){ //调用彩色滚动
	   $(".colour .cont ul").animate({left:-itwo+'px'},2000,'swing'); 			   
	}			
	function grayroll(){ //调用灰色滚动
	   $(".gray ul").animate({left:-jtwo+'px'},2000,'swing'); 
	}
	function describeroll(){ //调用内容滚动
		$(".roll_left ul").animate({top:'-930px'},2000,'swing'); 
		$(".roll_left ul").addClass('fadeInUp animated');
	};

})

//首页图片滚动大小设置
function lads(){
	var cltwinth=document.documentElement.clientWidth;//浏览器可见宽度；
	if(window.navigator.userAgent.indexOf('compatible') != -1){
		if (cltwinth < 1400) {
	    	hdheit=394;
	    	psterw=700;
	    	psterh=394;
	    	return;
		}else if(cltwinth < 1605){
	    	hdheit=480;
	    	psterw=860;
	    	psterh=480;
	    	return;
	    }
	}else{
	    if(cltwinth<1367){
	    	hdheit=394;
	    	psterw=700;
	    	psterh=394;
	    	return;
	    }else if(cltwinth<1445){
	    	hdheit=480;
	    	psterw=860;
	    	psterh=480;
	    	return;
	    }else if(cltwinth<1605){
	    	hdheit=480;
	    	psterw=860;
	    	psterh=480;
	    	return;
	    }
    }
	
	

}



