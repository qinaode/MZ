
/*说明（公共变量在index.js里定义

一。包含操作

文件下载   DownLoadFiles

搜索文件   GetSearchFiles

显示或隐藏右键菜单  ShowOperateDialog

显示或隐藏排序菜单   ShowRankeDialog

新建文件夹  AddNewFolder

重命名文件或文件夹

移动文件或文件夹

删除文件 到回收站

推送文件

共享文件ShareMyFiles()

还原文件

上传文件

*/

//-----文件下载--------------------------------------

function DownLoadFiles() {

    if (file_id == "" && SelectIDS.length == 0) {
        alert("请选择要下载的文件");
        return;
    }

    var param_ids = file_id;
    var versionids = new Array();
    var fileids = new Array();
    var param_verids = "";
    versionids.push($("#a_" + file_id).attr("versionid"));
    fileids.push(file_id);
    HideOperateDialog();

    if (SelectIDS.length > 0 && SelectIDS.length <= 15) {

        versionids.splice(0, 1);
        fileids.splice(0, 1);
        for (var i = 0; i < SelectIDS.length; i++) {
            if ($("#div2_" + SelectIDS[i]).attr("type") == "file") {
                versionids.push($("#a_" + SelectIDS[i]).attr("versionid"));
                fileids.push(SelectIDS[i]);
            }
        }
    }
    else if (SelectIDS.length > 15) {
        alert("最多只能打包15个文件");
        return;
    }
    param_verids = versionids.toString();
    param_ids = fileids.toString();
    if (versionids.length == 1) {
        window.location.href = "/DiskN/DownLoadFile?ids=" + param_ids + "&verids=" + param_verids;
    }
    else {
        window.location.href = "/DiskN/DownLoadFiles_M?ids=" + param_ids + "&verids=" + param_verids;
    }


}
//---------------------------------------------------

//-----搜索文件--------------------------------------

function GetSearchFiles() {

    var keyword = $("#searchInput").val();
    keyword = keyword.toLowerCase();
    var myReg = new RegExp(keyword);
    $(".li_rescontent").each(function (i, item) {
        var showflag = $(item).attr("showflag").toLowerCase();
        var filename = GetFileNameWithoutExt(showflag);
        if (!myReg.test(filename)) {
            $(item).addClass("searchdisplay");
        }
        else {
            $(item).removeClass("searchdisplay");
        }

    });

}
//---------------------------------------------------


//--显示或隐藏右键菜单-------------------------------

//显示右键菜单
function ShowOperateDialog(topp, leftp) {
    //看看有多少选中的项
    var items = 0;
    // alert($(".selected").count);
    $(".selected").each(function (i, item) {
        items = i + 1;
    });
    if (items > 1) {
        return;
    }
    $("#menu").css("top", topp);
    $("#menu").css("left", leftp);
    $("#menu").css("display", "block");

    if (file_type == "0") {
        $("#li_push").css("display", "block");
    }
    else {
        $("#li_push").css("display", "none");
    }
    $("#menu").menu();

}
//隐藏右键菜单
function HideOperateDialog() {
    $("#menu").css("display", "none");
}

//-----------------------------------------------------------------

//--显示或隐藏排序菜单---------------------------------------------
//显示排序菜单
function ShowRankeDialog() {

    var objrank = GetElCoordinate(document.getElementById("a_rank"));
    var atop = objrank.top - 5;
    var aleft = objrank.left;

    $("#ul_rank").css("top", atop);
    $("#ul_rank").css("left", aleft);
    $("#ul_rank").css("display", "inline");
    $("#ul_rank").menu();
    //$("#a_rank").css("display", "none");

}
//隐藏右键菜单
function HideRankDialog(rflag) {
    rankflag = rflag;
    $("#ul_rank").css("display", "none");
    if (rflag == 'file_name') {
        $("#a_rank").html("名称");
    }
    else if (rflag == 'file_size') {
        $("#a_rank").html("大小");
    }
    else if (rflag == 'created_at') {
        $("#a_rank").html("最近上传");
    } else if (rflag == 'updated_at') {
        $("#a_rank").html("修改日期");
    }
    // GetRankedData();
    GetResContentByType(Ttype, parent_id, '0');
}

//----------------------------------------------------------------

//新建文件夹 按钮事件---------------------------------------------
function Btn_CreateNewFolder() {

    $("#txt_foldername").val("");
    $("#createfolder").dialog("open");
}

function AddNewFolder(foldername) {
    $.ajax({
        url: "/DiskN/AddNewFoler",
        type: "post",
        data: { "foldername": foldername, "parent_id": parent_id },
        datatype: "text",
        success: function (backdata) {

            if (backdata == "true") {
                alert("新建成功");
                GetResContentByType(Ttype, "", '0');
            }
            else {
                alert("系统出错，请重新登录");
            }
            file_id = "";
        }
    });
}

//---------------------------------------------------------------------------

//重命名按钮事件-------------------------------------------------------------
function Btn_ResetName() {

    // var filename = $("#afn_" + file_id).attr("value");
    var filename = $("#a_" + file_id).attr("title");
    $("#txt_filename").val(filename);
    $("#ResetName").dialog("open");
}

//重命名 文件名
function ResetFileName() {
    var filename = $("#txt_filename").val();

    $.ajax({
        url: "/DiskN/UpdateFileInfo",
        type: "Post",
        data: { "Flag": "filename", "Value": filename, "FileID": file_id },
        success: function (backdata) {

            if (backdata == "true") {
                GetResContentByType(Ttype, parent_id, "0");
            }
            else {
                alert("重命名失败");
            }
            file_id = "";
        }
    });
}


//-----------------------------------------------------------------------

//移动文件按钮事件------------------------------------------------------
function Btn_MoveFile() {
    $("#MoveFile").dialog("open");
    GetFolderList("0", "li_0");
}

//获取文件夹列表
function GetFolderList(pid, div_id) {

    // alert($("#li_" + folder_id).attr("class"));
    if (folder_id == file_id) {
        return;
    }
    //当处于根目录下时刷新总列表
    if (pid == "0") {
        $("#li_0").html("<a class='folderlist' id=\"ac_0\" onclick='GetFolderList(0,\"li_0\")' href='javascript:void(0);return false;'>全部文件</a>");
        $("#li_0").removeClass("open");
    }
    folder_id = pid;

    //点击切换状态显示
    $(".folderlist").removeClass("selected");
    $("#ac_" + folder_id).addClass("selected");


    $.ajax({
        url: "/DiskN/GetFolderList",
        type: "Post",
        data: { "parent_id": pid },
        datatype: "text/json",
        success: function (backdata) {
            //            alert(backdata);
            if (backdata == "" || backdata == "[]") {
                return false;
            };
            var html1 = "<ul class='MF_UL'>";
            $.each(backdata, function (i, item) {
                var html2 = "";
                if (item["id"] != file_id) {
                    html2 = "<li id='li_" + item["id"] + "'><a id='ac_" + item["id"] + "' class='folderlist' onclick='GetFolderList(" + item["id"] + ",\"li_" + item["id"] + "\")' href='javascript:void(0);return false;'>" + item["file_name"] + "</a></li>";
                    html1 = html1 + html2;
                }
            });
            html1 = html1 + "</ul>";
            html1 = $("#" + div_id).html() + html1;

            //如果是第一次打开 则添加元素
            if (!$("#" + div_id).hasClass("open")) {
                $("#" + div_id).html(html1);
                $("#" + div_id).addClass("open");

            }

        }
    });

}
//移动文件夹
function MoveToOtherFolder() {
    $.ajax({
        url: "/DiskN/MoveToOtherFolder",
        type: "Post",
        data: { "file_id": file_id, "folder_id": folder_id },
        datatype: "text",
        success: function (backdata) {

            if (backdata == "true") {
                GetResContentByType(Ttype, "", "0");
            }
            else {
                alert("移动失败");
            }
            file_id = "";
        }
    });

}

//----------------------------------------------------------------------------

//删除文件 到回收站------------------------------------------------------------
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

                if (SelectIDS.length > 0) {
                    for (var i = 0; i < SelectIDS.length; i++) {
                        $("#div1_" + SelectIDS[i]).css("display", "none");
                    }
                }
                else {
                    $("#div1_" + file_id).css("display", "none");
                }
                file_id = "";
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
    else if (SelectIDS.length > 0) {
        file_id = SelectIDS.toString();
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
                    GetResContentByType(Ttype, "0", "1");
                }
                else {
                    if (SelectIDS.length > 0) {
                        for (var i = 0; i < SelectIDS.length; i++) {
                            $("#div1_" + SelectIDS[i]).css("display", "none");
                        }
                    }
                    else {
                        $("#div1_" + file_id).css("display", "none");
                    }
                }
                file_id = "";
            }
            else {
                alert("删除失败");
            }
        }
    });

    return false;
}
//-------------------------------------------------------------------------------------

//文件推送----------------------------------------------------------------------------
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
    file_id = "";
    return false;
}
//-------------------------------------------------------------------------------
//关闭共享文件对话框---------------------------------------------------------------------------------
window.closeThisWindow = function () {
    $("#ShareName").dialog("close");
};
//共享文件-------------------------------------------------------------------------------
function ShareMyFiles() {
//    var strfiletype = "";
//    if (file_type == "0") {
//        strfiletype = "file";
//    } else if (file_type == "1") {
//        strfiletype = "folder";
    //    }
    $("#if_share").attr("src", "/DiskN/Share?file_id=" + file_id + "&file_type=" + file_type);
    $("#ShareName").dialog("open");
    return false;
    window.location = "/DiskN/Share?file_id=" + file_id + "&file_type=" + file_type;
    //return false;
}
//-------------------------------------------------------------------------------

//还原文件-----------------------------------------------------------------------
function RevertFile() {
    HideOperateDialog();
    if (SelectIDS.length > 0) {
        file_id = SelectIDS.toString();
    }

    $.ajax({
        url: "/DiskN/UpdateFileInfo",
        type: "post",
        data: { "FileID": file_id, "Flag": "Delete", "Value": "0" },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {

                if (SelectIDS.length > 0) {
                    for (var i = 0; i < SelectIDS.length; i++) {
                        $("#div1_" + SelectIDS[i]).css("display", "none");
                    }
                }
                else {
                    $("#div1_" + file_id).css("display", "none");
                }
                file_id = "";
            }
            else {
                alert("还原失败");
            }
        }
    });

    return false;
}
//-------------------------------------------------------------------------

//-------------文件上传----------------------------------------------------
//上传按钮事件
function Btn_UploadFiles() {

    $("#uploadfile").dialog("open");

}


//-------------------------------------------------------------------------


