

var file_type = "";
//显示右键菜单
function ShowOperateDialog(topp, leftp) {
    $("#menu").css("top", topp);
    $("#menu").css("left", leftp);
    $("#menu").css("display", "block");
    // file_type = $("#hidtype_" + file_id).val();
    if (file_type == "1") {
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
    if (SelectIDS.length > 0) {
        // $("#a_push").attr("href", "/Push/Index?file_id=" + SelectIDS.toString());
        window.open("/Push/Index?file_id=" + SelectIDS.toString());
    }
    else {
        //$("#a_push").attr("href", "/Push/Index?file_id=" + file_id);
        window.open("/Push/Index?file_id=" + file_id);
    }
    HideOperateDialog();
    return false;
}

//文件重命名
function ResetName() {
    HideOperateDialog();
    return false;
}

//禁用或启用鼠标按键
function ForBindMouse(bool) {
    document.oncontextmenu = new Function("event.returnValue=" + bool + ";")
    document.onselectstart = new Function("event.returnValue=" + bool + ";");
}

//删除文件 到回收站
function DeleteFile() {
    var param_ids = file_id;
    HideOperateDialog();
    if (SelectIDS.length > 0) {
        param_ids = SelectIDS.toString();
    }
    $.ajax({
        url: "/DiskN/UpdateFileInfo",
        type: "post",
        data: { "FileID": param_ids, "Flag": "Delete", "Value": "1" },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                //                alert("删除成功");
                if (SelectIDS.length > 0) {
                    for (var i = 0; i < SelectIDS.length; i++) {
                        $("#div1_" + SelectIDS[i]).css("display", "none");
                    }
                }
                else {
                    $("#div1_" + file_id).css("display", "none");
                }
                return false;
            }
            else {
                alert("删除失败");
                return false;
            }
        }
    });

}


//删除回收站文件
function DeleteFileInfo(flag) {

    HideOperateDialog();
    if (flag == "all") {
        file_id = "-1";
    }
    $.ajax({
        url: "/DiskN/DeleteFileInfo",
        type: "post",
        data: { "FileID": file_id },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                //                alert("删除成功");
                if (file_id == "-1") {
                    GetDeleteContent_1(Ttype);
                }
                else {
                    $("#div1_" + file_id).css("display", "none");
                }
            }
            else {
                alert("删除失败");
            }
        }
    });
    return false;
}
//还原文件 
function RevertFile() {
    HideOperateDialog();
    $.ajax({
        url: "/DiskN/UpdateFileInfo",
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
    return false;
}


