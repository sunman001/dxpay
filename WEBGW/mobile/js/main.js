

		 (function(){
			$('.menu').on('touchend',function(){ //显示菜单
				$('.submenu').slideToggle();
				$('.bodys').slideToggle();
			});
			$('.bodys').on('touchend',function(){ //关闭菜单
				$('.submenu').hide();
				$('.bodys').hide();
			});	
		 })();
		 
	        var i = 86*2; //设置彩色滚动度数
	        var j = 106; //设置灰色滚动度数
	        var h = 130*2; //设置内容滚动
	        var f= 100;
	        var is_true = true;
	        var is_true2=true;
	        var is_true3=true;
	        var is_true4=true;
            (function() {})(
            	init() 
            );
            function init(){ //初始动画
            	colour();
            	gray();
            	describe();
            	hezuoall();
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
		    		
		    	},3000);
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
		    	},3000);
			} 
			function describe(){ //内容滚动
	       		setInterval (function(){
	       			$('.roll_left ul').attr('style','');
		    		describeroll();
		    		var first = $(".roll_left ul li:first");
		    		if(is_true){ 
		    			is_true = false ;
		    		}else{
		    		    $('.roll_left ul').append(first);
		    		}

		    	},3000);
			}
			function colourroll(){ //调用彩色滚动
			   $(".colour .cont ul").animate({left:-i+'px'},1000,'swing'); 			   
			}			
			function grayroll(){ //调用灰色滚动
			   $(".gray ul").animate({left:-j+'px'},1000,'swing'); 
			}
			function describeroll(){ //调用内容滚动
		   		$(".roll_left ul").animate({top:-h+'px'},1000,'swing'); 

			}	
			function hezuoall(){ //第五屏滚动
	        	setInterval (function(){
				   $('.cooperation .cont').attr('style','');
				   hezuo();
				   var first = $(".cooperation .cont ul:first");
		    		if(is_true){ 
		    			is_true = false ;
		    		}else{
		    		    $('.cooperation .cont').append(first);
		    		}
				},2000);				
			}	
	        function hezuo(){ //第五屏
                 $(".cooperation .cont").animate({left:-f+'%'},1000,'swing'); 	
			}
	        