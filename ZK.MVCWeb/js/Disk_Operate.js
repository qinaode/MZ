
//用来记录是全部 视频 相册 文档
var _type = "";

//用来记录 文件的父目录
var parent_id = "";

//用来标记是列表模式 还是图标模式
var flag = "";

$(function () {

    //找出所在的文件夹 
    document.getElementById("iframe_content").src = $("#iframe_content").attr("flag");
    var url = $("#iframe_content").attr("src").split('?');
    var urltemp = url[1].split('&');

    _type = urltemp[0].substr(6);
   
    parent_id = urltemp[1].substr(10);

    //初始化 新建文件夹的对话框
    $("#createfolder").dialog({
        autoOpen: false,
        height: 180,
        width: 280,
        modal: true,
        buttons: {
            "确定": function () {

                if ($("#txt_foldername").val() == "") {
                    alert("请输入文件夹名");
                    return false;
                }
                $("#createfolder").dialog("close");

                AddNewFolder($("#txt_foldername").val());
            },
            "取消": function () {
                $("#createfolder").dialog("close");
            }
        }
    });
    //-------------------------------------------------

    //初始化上传文件的弹出框
    $("#uploadfile").dialog({
        autoOpen: false,
        height: 620,
        width: 500,
        modal: true,
        buttons: {
            //            "浏览文件": function () {
            //                $('#uploadify').uploadifyUpload();
            //            },
            "取消上传": function () {
                $('#uploadify').uploadifyClearQueue();
            },
            "关闭": function () {
                $("#uploadfile").dialog("close");
            }
        }
    });
    //----------------------------------------------------

    //初始化上传文件的对话框
//    alert(_type);
//    alert(parent_id);
    $("#uploadify").uploadify({
        'uploader': '/Scripts/uploadify-v2.1.0/uploadify.swf',
        'script': '/DiskOper/UploadFiles?_parm=' + _type + "_" + parent_id, //'UploadHandler.ashx',
        'cancelImg': '/Scripts/uploadify-v2.1.0/cancel.png',
        'folder': '/Files/NFiles',
        'queueID': 'fileQueue',
        'auto': true,
        'multi': true
    });
    //-----------------------------------------------

    //---------------------------------------------------


});
//新建文件夹 按钮事件
function Btn_CreateNewFolder(pagetype) {
    flag = pagetype;
    $("#txt_foldername").val("");
    $("#createfolder").dialog("open");
}
function AddNewFolder(foldername) {
    var url = $("#iframe_content").attr("src").split('?');

    var urltemp = url[1].split('&');
    _type = urltemp[0].substr(6);
    parent_id = urltemp[1].substr(10);

    $.ajax({
        url: "/DiskOper/AddNewFoler",
        type: "post",
        data: { "foldername": foldername, "parent_id": parent_id },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                $("#iframe_content").attr("src", "/Disk/content_" + flag + "?_type=" + _type + "&parent_id=" + parent_id);
            }
            else {
                alert(backdata);
            }
        }
    });
}
//上传按钮事件
function Btn_UploadFiles(pagetype) {
    flag = pagetype;
    $("#uploadfile").dialog("open");
}


//拖拉鼠标选中事件
function SelectWhileMouseMove() {


}

//加载数据
//function DownLoadContent() {
//    $.ajax({
//        type: "Post",
//        url: "/Disk/content_pic2?_type=all&parent_id=0",
//        data: {},
//        datatype: "json/text",
//        success: function (backdata) {
//            if (backdata == "" || backdata == "[]") {
//                return;
//            };
//            var tempjson = $.parseJSON(backdata);

//            $("#div_content").setTemplateURL("../Views/Disk/ResourceContent.htm", null, null);
//            $("#div_content").processTemplate(tempjson);
//        }
//    });

//}