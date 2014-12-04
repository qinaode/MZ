
var file_id = "";
$(function () {

    //自动调整ifarme的大小
    AutoIframeSize();

    //屏蔽鼠标右击事件
    document.oncontextmenu = new Function("event.returnValue=false;");
    //    document.onselectstart = new Function("event.returnValue=false;");

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
    })
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
    $("#menu").menu();

}
//隐藏右键菜单
function HideOperateDialog() {
    $("#menu").css("display", "none");
}


//删除文件 到回收站
function DeleteFile(flag) {

    HideOperateDialog();
    if (flag == "all") {
        file_id = "-1";
    }
    $.ajax({
        url: "/DiskOper/DeleteFileInfo",
        type: "post",
        data: { "FileID": file_id },
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
//还原文件 
function RevertFile() {
    HideOperateDialog();
    $.ajax({
        url: "/DiskOper/UpdateFileInfo",
        type: "post",
        data: { "FileID": file_id, "Flag": "Delete", "Value": "0" },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                //                alert("还原成功");
                $("#div1_" + file_id).css("display", "none");
            }
            else {
                alert("还原失败");
            }
        }
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
                    if (document.all) { h += 4; }
                    if (window.opera) { h += 1; }
                    a[i].style.height = a[i].parentNode.style.height = h + "px";
                }
            }
        }
    } catch (ex) { }
}
