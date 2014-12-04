var divhtml = $("#txtUser");
//--删除-----------
function div_deleted(id, e) {
    var divhtml = $("#txtUser");
    var pid = id;
    var obj = $(pid);

    var re = $(obj).parent('div');
    re.remove();
    // obj.remove();  
    //stopDefault(e);
    //stopBubble(e);
    //=========第二种方式=====
    e = e || window.event;
    if (e.preventDefault) {
        e.preventDefault();
        e.stopPropagation();
    } else {
        e.returnValue = false;
        e.cancelBubble = true;
    }
    var num = $(".resultvalue").length;
    if (num == 0) {
        $.dialog({ title: '发送公告范围', width: '590px', height: '500px', content: 'url:NoticeRange.aspx?_a=' + Math.random(), max: false, min: false });

    }
}
//=====================================
/*
function stopBubble(e) {
//如果提供了事件对象，则这是一个非IE浏览器  
if (e && e.stopPropagation) {
//因此它支持W3C的stopPropagation()方法  
e.stopPropagation();
} else {
//否则，我们需要使用IE的方式来取消事件冒泡   
window.event.cancelBubble = true;
}
return false;
}  

function stopDefault(e) {
if (e && e.preventDefault) {
//阻止默认浏览器动作(W3C)  
e.preventDefault();
e.stopPropagation();
} else {
//IE中阻止函数器默认动作的方式   
window.event.returnValue = false; 
}
return false;  
}
*/