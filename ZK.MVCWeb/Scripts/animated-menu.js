$(document).ready(function(){
	
	//Fix Errors - http://www.learningjquery.com/2009/01/quick-tip-prevent-animation-queue-buildup/
	
	//Remove outline from links
	$("a").click(function(){
		$(this).blur();
	});
	
	//When mouse rolls over        
        $("#li_main").mouseover(function(){
		$(this).stop().animate({height:'80px'},{queue:false, duration:600, easing: 'easeOutBounce'})
	});
        $("#li_system").mouseover(function(){
		$(this).stop().animate({height:'190px'},{queue:false, duration:600, easing: 'easeOutBounce'})
	});
        $("#li_channel").mouseover(function(){
		$(this).stop().animate({height:'190px'},{queue:false, duration:600, easing: 'easeOutBounce'})
	});
        $("#li_ext").mouseover(function(){
		$(this).stop().animate({height:'120px'},{queue:false, duration:600, easing: 'easeOutBounce'})
	});
	
	//When mouse is removed
	$("li").mouseout(function(){
		$(this).stop().animate({height:'50px'},{queue:false, duration:600, easing: 'easeOutBounce'})
	});
	
});