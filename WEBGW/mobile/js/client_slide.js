 (function(){
	$('.menu').on('touchend',function(){ //显示菜单
		$('.submenu').slideToggle();
		$('.bodys').slideToggle();
	});
	$('.bodys').on('touchend',function(){ //关闭菜单
		$('.submenu').hide();
		$('.bodys').hide();
	});	
    
    var str=location.href;  
	var urlmoveDiv = GetQueryString("str");
	moveDiv(urlmoveDiv);
	
 })();
 
 
function GetQueryString(name)
{
     var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
     var r = window.location.search.substr(1).match(reg);
     if(r!=null)return  unescape(r[2]); return null;
}

/** 滑动到指定的点  开始 **/

function moveDiv(n){
	
	var heightArr = [];
	var pages = $(".single-div");
	var pid = 1;		
	for(var i=1; i<pages.length+1; i++){
		heightArr[i] = parseInt($(".div-"+i).css("height"));
	}	
	
	var hnumbers = 0;
	for(var i=1; i<n; i++){
		hnumbers = hnumbers + heightArr[i];
	}
	$("html,body").animate({
		"scrollTop": hnumbers 
	},700);
}
/** 滑动到指定的点  结束 **/