$(function(){
	
	//top menu
	$(".top .menuTop .menuLi .mtli").each(function(index){
		
		 $(this).click(function(){
			 var liNode=$(this);
			 $('.top .menuTop .menuLi .mtli').removeClass('current');
			 liNode.addClass('current');
			 
			 $('.top .menuSec_bak .menuSec .msli').animate({opacity: "0.1"},0)
			 $('.top .menuSec_bak .menuSec .msli').removeClass('current');
			 
			 var liNode_c = $('.top .menuSec_bak .menuSec .msli').eq(index);
			 liNode_c.animate({opacity: "1"},200)
			 liNode_c.addClass('current');
			 })
		})
	
	
	//menu more APP
	$('.top .menuSec_bak .menuSec .msli').each(function(index){
		
		var liTt =0;
		$(this).find('.menuSec_c .msl_c').each(function(index_c){
				liTt=index_c+1;
			})
		if(liTt>10){
			var source = $(this).html();
			source +="<div id='morebt'><img src='/image/down.png' /></div>";
			$(this).html(source);
			}
	})
	
	//menu more click show
	$('.top .menuSec_bak .menuSec .msli #morebt').each(function(index){
		$(this).toggle(function(){
			$(this).parent().find('.menuSec_c').css({'overflow':'visible','background-color':'#2a6c90','height':'140px','border':'1px solid #062b4f','-moz-opacity':'0.9','-khtml-opacity':'0.9','opacity':'0.9'});
			$(this).parent().find('.menuSec_c .msl_c').css({'border':'1px solid #2a6c90'});
			$(this).html('<img src="/image/up.png" />');
		},function(){
			$(this).parent().find('.menuSec_c').css({'overflow':'hidden','background-color':'','height':'68px','border':'1px solid #062b4f'});
			$(this).parent().find('.menuSec_c .msl_c').css({'border':'1px solid #062b4f'});
			$(this).html('<img src="/image/down.png" />');
		})
	})
	
	//Forum edit - farm
	$(".content .leftMenu li").each(function(index){
		
		 $(this).click(function(){
			 var liNode=$(this);
			 $('.content .leftMenu li').removeClass('current');
			 liNode.addClass('current');
			 
			 $('.content .rightMain .comming').removeClass('current');
			 $('.content .rightMain .comming').eq(index).addClass('current');
			 })
		})
	
	//dictLi del
	$('.content .productList .dictLi .dLi').each(function(index){
		$(this).hover(function(){
			var source = $(this).html();
			source +="<div class='del'>╳</div>";
			$(this).html(source);
		},function(){
			$(this).find('.del').remove();;
		})
	})
	
	//newsCate edit
	$('.content .productList .cateLi .cLi').each(function(index){
		$(this).hover(function(){
			var source = $(this).html();
			//source +='<div class="cOp"><a href="#neo">删除</a> ｜ <a href="#neo">编辑</a> ｜ <a href="#neo">状态</a></div>';
			//$(this).html(source);
			$(this).find('.cOp').slideDown("fast");
		},function(){
			$(this).find('.cOp').slideUp("fast");
		})
	})
	
	
	
	//Sys user photo add.
	$('.acStyle tr td #userPhoto').each(function(index){
		$(this).hover(function(){
			var source = $(this).html();
			$(this).find('#uploadBt').slideDown("fast");
		},function(){
			$(this).find('#uploadBt').slideUp("fast");
		})
	})
	
	//.annStyle tr td .ppic .ingrLi
	$('.annStyle tr td .ppic .ingrLi').each(function(index){
		$(this).hover(function(){
			var source = $(this).html();
			$(this).find('#uploadBt').slideDown("fast");
		},function(){
			$(this).find('#uploadBt').slideUp("fast");
		})
	})
	
	//.annStyle tr td .taskList li
	$('.annStyle tr td .taskList li').each(function(index){
		$(this).hover(function(){
			var source = $(this).html();
			$(this).find('#opBt').slideDown("fast");
		},function(){
			$(this).find('#opBt').slideUp("fast");
		})
	})
	
});