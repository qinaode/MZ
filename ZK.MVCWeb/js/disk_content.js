
var file_id = "";
var file_type = "";
var _type = "";
var parent_id = "";
var isMouseDown = false;
$(function () {

    //获取url里的参数
    var url = location.search; //获取url中"?"符后的字串
    if (url.indexOf("?") != -1) {
        var pars = url.substr(1).split('&');

        _type = pars[0].substr(6);
        parent_id = pars[1].substr(10);

    }
    //自动调整ifarme的大小
    AutoIframeSize();

    //屏蔽鼠标右击事件
    ForBindMouse("false");


    //鼠标点击事件
    $('.div_fileinfo').mousedown(function (e) {
        // 3 右键 1 左键
        file_id = $(this).attr("id").substr(5);

        if (3 == e.which) {
            var topp = e.pageY;
            var leftp = e.pageX;
            ShowOperateDialog(topp, leftp);
            AutoIframeSize();
        }
//        else if (1 == e.which) {
//            isMouseDown = true;
//            // $("#div2_" + file_id + " a").addClass("selected");
//            alert(isMouseDown);
//        }
    })
//    document.mousedown(function (e) {
//        isMouseDown = true;
//        alert(isMouseDown);
//    });
//    $(".div_fileinfo").mouseup(function (e) {
//        if (1 == e.which) {
//            isMouseDown = false;
//            alert(isMouseDown);
//        }
//    })

    //鼠标滑过a标签时
    $(".aContent").mouseover(function (e) {

       // $(this).addClass("selected");

    });
    //普通标签的鼠标左键事件 
    $(".hidemenu").mousedown(function (e) {

        // 3 右键 1 左键
        if (1 == e.which) {
            HideOperateDialog();

        }
    });

});

//显示右键菜单
function ShowOperateDialog(topp, leftp) {
    $("#menu").css("top", topp);
    $("#menu").css("left", leftp);
    $("#menu").css("display", "block");
    file_type = $("#hidtype_" + file_id).val();
    if (file_type == "1") {
        //        $("#a_push").addClass("ui-state-disabled");
        //        $("#a_push").unbind("onclick", "PushFile");
        $("#li_push").css("display", "none");
    }
    else {
        $("#a_push").removeClass("ui-state-disabled");
        $("#li_push").css("display", "block");
    }

    $("#menu").menu();

}
//隐藏右键菜单
function HideOperateDialog() {
    $("#menu").css("display", "none");
}


//文件推送
function PushFile() {
    $("#a_push").attr("href", "/Push/Index?file_id=" + file_id);
}

//文件重命名
function ResetName() {
    HideOperateDialog();


}

//禁用或启用鼠标按键
function ForBindMouse(bool) {

    document.oncontextmenu = new Function("event.returnValue=" + bool + ";")
}

//删除文件 到回收站
function DeleteFile() {

    HideOperateDialog();
    $.ajax({
        url: "/DiskOper/UpdateFileInfo",
        type: "post",
        data: { "FileID": file_id, "Flag": "Delete", "Value": "1" },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                //                alert("删除成功");
                $("#div1_" + file_id).css("display", "none");
            }
            else {
                alert("删除失败");
            }
        }
    });

}

//通过file_id 找出相应的文件夹下的文件

function GetChildrenList() {
    $.ajax({
        url: ""
    });
}

//自动调整ifarme的大小
function AutoIframeSize() {
    try {

        if (window != parent) {
            var a = parent.document.getElementsByTagName("iframe");
            for (var i = 0; i < a.length; i++) {
                if (a[i].contentWindow == window) {
                    var h1 = 0, h2 = 0, d = document, dd = d.documentElement;
                    a[i].parentNode.style.height = a[i].offsetHeight + "px";

                    a[i].style.height = "10px";
                    if (dd && dd.scrollHeight) h1 = dd.scrollHeight;
                    if (d.body) h2 = d.body.scrollHeight;
                    var h = Math.max(h1, h2);
                    if (document.all) { h += 8; }
                    if (window.opera) { h += 1; }
                    a[i].style.height = a[i].parentNode.style.height = h + "px";
                }
            }
        }
    } catch (ex) { }
}
