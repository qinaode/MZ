//绑定node checkbox点击事件
$(function () {
    $(".datalist > .fname > .ch").click(function () {
        if ($(this).hasClass("on")) {
            $(this).addClass("off").removeClass("on");
            $(this).parent().parent().removeClass("datalisthover");
        } else {
            $(this).addClass("on").removeClass("off");
            $(this).parent().parent().addClass("datalisthover");
        }
    });
})

//选中所有节点
function checkall() {
    $(".fname").find(".ch").addClass("on").removeClass("off");
    $(".datalist").addClass("datalisthover");
}
//取消选中所有节点
function uncheckall() {
    $(".fname").find(".ch").addClass("off").removeClass("on");
    $(".datalist").removeClass("datalisthover");
}
//新建文件夹取消按钮函数
function cancle(_this) {
    $(_this).parent().parent().parent().remove();
}
//单击表头checkbox事件
function CheckOrUncheck(_this) {
    if ($(_this).hasClass("off")) {
        checkall();
    } else {
        uncheckall();
    }
}
//单击node的checkbox事件
function NodeCheckOrUncheck(_this) {
    var __this = $(_this);
    if (__this.hasClass("on")) {
        __this.addClass("off").removeClass("on");
        __this.parent().parent().removeClass("datalisthover");
    } else {
        __this.addClass("on").removeClass("off");
        __this.parent().parent().addClass("datalisthover");
    }
}

//获取所有选中节点集合
function GetAllNodes() {
    var nodes = [];
    var ids = [];
    $(".datalist").each(function () {
        if ($(this).find(".ch").hasClass("on")) {
            var node = $(this);
            var fid = node.attr("fid");
            //var fname = node.attr("fname");
            ids.push(fid);
            nodes.push({ _this: node, fid: fid, fname: null, fsize: null, mtime: null });
        }
    });
    nodes.ids = ids;
    return nodes;
}

//e.which：1.左键2.中键3.右键
$(".datalist").mousedown(function (e) {
    //                console.info(e);
    //                console.info(e.srcElement);
    //                console.info($(e.srcElement));
    //如果是鼠标右键，则在节点当前位置弹出菜单
    var x = e.pageX, y = e.pageY;
    if (3 == e.which) {


        var node = $(this);
        var fid = node.attr("fid");
        if (prenode) {
            prenode._this.find(".ch").removeClass("on").addClass("off");
            prenode._this.removeClass("datalisthover");
        }
        node.find(".ch").removeClass("off").addClass("on");
        node.addClass("datalisthover");
        var cnode = { "_this": node, "fid": fid, "fname": null, "fsize": null, "mtime": null };
        prenode = cnode;
        //弹出菜单
        if (GetAllNodes().length > 1) {
            $(".popmenu").find(".rename").hide();
        } else {
            $(".popmenu").find(".rename").show();
        }
        $(".popmenu").hide().css({ "left": x, "top": y }).fadeIn(500);

    }
});