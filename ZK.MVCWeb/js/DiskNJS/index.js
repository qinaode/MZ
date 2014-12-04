
var username = "";
var parent_id = "0"; //记录当前的文件所在位置
//var parent_Path = "全部文件"; //用来记录文件的路径 用来返回
var path_ids = new Array(); //用来记录打开路径的ids
var path_names = new Array(); //用来记录打开路径的names
var FirstLoad = "1"; //首次加载

var isopenfolder = false; //用来记录是否打开文件夹  文件路径显示用
var Ttype = "all"; //记录选择的模块儿
var ShowType = 1; //用来记录当前显示模式 是 1 图标 2 列表 
var file_id = ""; //用来记录文件的id
var file_type = ""; //是文件或文件夹
var folder_id = "0"; //用来记录移动文件或文件时当前选中的文件夹
var rankflag = 'file_name'; //排序规则

var MXstart = 0;    //鼠标左键开始X坐标
var MYstart = 0;     //鼠标左键开始Y坐标
var MXend = 0;      //鼠标左键结束X坐标
var MYend = 0;      //鼠标左键结束Y坐标
var SelectIDS = new Array(); //用来装拖选的资源id
var IsSelected = false; //是否是拖拉选择

var isMouseDown = false;
var isDownMove = false;
//-----初始化-----------------------------------------
$(function () {


    username = $("#a_username").text();
    //设置页面各控件的位置
    SetHtmlElePosition();
    ShowType = "2"; //wuyanyan  2014-03-10
    //刚加载时初始化路径
    GetFolderPathForRoot();
    //屏蔽鼠标右击事件
    ForBindMouse("false");
    //获取资源页列表
    GetResContentByType("all", "0", "0");

    //初始化 新建文件夹的对话框
    $("#createfolder").dialog({
        autoOpen: false,
        height: 180,
        width: 280,
        modal: true,
        resizable: false,
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
    //初始化 重命名的对话框
    $("#ResetName").dialog({
        autoOpen: false,
        height: 180,
        width: 280,
        modal: true,
        resizable: false,
        buttons: {
            "确定": function () {
                if ($("#txt_filename").val() == "") {
                    alert("请输入文件名");
                    return false;
                }
                $("#ResetName").dialog("close");

                // ResetFileName($("#txt_filename").val());
                ResetFileName();
            },
            "取消": function () {
                $("#ResetName").dialog("close");
            }
        }
    });
    //--------------------------------------------------

    //初始化共享页面
    $("#ShareName").dialog({
        autoOpen: false,
        height: 521,
        width: 825,
        position: [400, 400],
        draggable: true,
        resizable: false,
        modal: true

    });

    //--------------------------------------------------
    //初始化 移动的对话框
    $("#MoveFile").dialog({
        autoOpen: false,
        height: 400,
        width: 450,
        modal: true,
        resizable: false,
        buttons: {
            "确定": function () {
                $("#MoveFile").dialog("close");
                MoveToOtherFolder();
            },
            "取消": function () {
                $("#MoveFile").dialog("close");
            }
        }
    });
    //-----------------------------------------------------

    //------初始化上传文件的弹出框--------------------------
    //    $("#uploadfile").dialog({
    //        autoOpen: false,
    //        height: 620,
    //        width: 500,
    //        modal: true,
    //        resizable: false,
    //        title: '文件上传',
    //        buttons: {
    //            "清除上传": function () {
    //                $('#uploadify').uploadifyClearQueue();
    //            },
    //            "完成": function () {
    //                $("#uploadfile").dialog("close");
    //                GetResContentByType(Ttype, "", "1");
    //            }
    //        }
    //    });
    //----------------------------------------------------

    //---------初始化文件上传的窗口-----------------------

    //    $("#uploadify").uploadify({
    //        'uploader': '/Scripts/uploadify-v2.1.0/uploadify.swf',
    //        'script': '/DiskN/UploadFiles?_parm=' + username + "_" + parent_id, //'UploadHandler.ashx',
    //        'cancelImg': '/Scripts/uploadify-v2.1.0/cancel.png',
    //        'folder': '/Files/NFiles',
    //        'queueID': 'fileQueue',
    //        'auto': true,
    //        'multi': true,
    //        'onInit': function () {
    //            //$("#fileQueue").append('<div id="11" class="uploadifyQueueItem"><div class="cancel"><a href=""><img src="2222" border="0" /></a></div><span class="fileName">filename</span><span class="percentage"></span><div class="uploadifyProgress"><div id="ProgressBar" class="uploadifyProgressBar"></div></div></div>');
    //        },
    //        'onComplete': function () {
    //            //            $("#uploadfile").dialog("close");
    //            //            GetResContentByType(Ttype, parent_id);
    //        }
    //    });

    $("#file_upload_1").uploadify({
        swf: '/js/uploadify/uploadify.swf',
        uploader: '/DiskN/UploadFiles?_parm=' + username + "_" + parent_id,
        width: 79,
        height: 33,
        queueID: 'uplist', //队列Div的Id
        // buttonClass: 'upbtn',
        buttonText: '上传',
        buttonImage: '/imagesN/diskImages/upbutton.png'
    });


    //--------------------------------------------------

    ////////窗体的点击事件 如果不继承 则return false
    $(".leftdiv").click(function () {
        $(".operabtn").css("display", "none");
        $(".operabtn2").addClass("hidden");
    });

    ///////////////////////////////////////////////////
    //此时 资源未加载 调用绑定事件函数 会出错
    // BindForEvent();

});

//----初始化完毕-----------------------------------

//禁用或启用鼠标按键
function ForBindMouse(bool) {
//    document.oncontextmenu = new Function("event.returnValue=" + bool + ";")
//    document.onselectstart = new Function("event.returnValue=" + bool + ";");
}


//绑定相应的函数事件
function BindForEvent() {

    //    ShowOrHidenByType(bindtype);

    //    $("#td_content").unbind("mouseover");
    //    $("#td_content").mouseover(function (e) {
    //        $(".div_list").removeClass("moveon");
    //    });
    //鼠标移上事件
    $(".div_list").unbind("mouseover");
    $(".div_list").mouseover(function (e) {
        // alert("abc");
        $(".div_list").removeClass("moveon");
        $(this).addClass("moveon");

    });

    //鼠标离开事件
    $(".div_list").unbind("mouseout");
    $(".div_list").mouseout(function (e) {
        // alert("abc");
        // $(".div_list").removeClass("moveon");
        $(this).removeClass("moveon");

    });

    //鼠标点击事件
    $(".div_fileinfo").unbind('mousedown');
    $('.div_fileinfo').mousedown(function (e) {
        var filetype = $(this).attr("type");
        switch (filetype) {
            case "file":
                file_type = "0";
                break;
            case "directory":
                file_type = "1";
                break;
            case "directory_share":
                file_type = "2";
                break;
            default:
                file_type = "0";
                break;

        }


        //  alert(file_type);
        // 3 右键 1 左键
        file_id = $(this).attr("id").substr(5);

        if (3 == e.which) {
            var topp = e.pageY;
            var leftp = e.pageX;
            ShowOperateDialog(topp, leftp);
        }
    })

    //鼠标按下后事件
    $("#td_content").unbind('mousedown');
    $("#td_content").mousedown(function (e) {

        if (3 == e.which) {
            return false;
        }
        $(".aContent").removeClass("selected");

        //列表
        //  if ($(this).hasClass("div_list")) {

        $(".xuan_image").attr("src", "/imagesN/diskImages/xuankuang.png");
        $(".xuan_image").addClass("unchecked");
        $(".div_list").removeClass("selected");
        // }
        if (1 == e.which) {
            MXstart = e.pageX;
            MYstart = e.pageY;
            MXend = e.pageX;
            MYend = e.pageY;
            $("#MouseMovediv").css("display", "block");
            $("#MouseMovediv").css("width", "0px");
            $("#MouseMovediv").css("height", "0px");
            isMouseDown = true;

        }
    });

    //鼠标移动事件
    $("#td_content").unbind('mousemove');
    $("#td_content").mousemove(function (e) {
        //window.event.button = 1 
        if (isMouseDown) {
            MXend = e.pageX;
            MYend = e.pageY;
            MouseMoveAddDiv();
            isDownMove = true;
        }
    });

    //鼠标抬起后事件
    $("#td_content").unbind('mouseup');
    $("#td_content").mouseup(function (e) {
        //        alert(IsSelected);
        //        alert(IsSelected);
        if (isMouseDown && IsSelected) {

            SelectIDS.splice(0, SelectIDS.length);

            $(".selected").each(function (i, item) {
                if (ShowType == 1) {
                    SelectIDS.push($(item).attr("id").substr(2));
                }
                else if (ShowType == 2) {
                    SelectIDS.push($(item).attr("id").substr(5));
                }
            })

            //alert(SelectIDS);
            if (SelectIDS.length > 0) {

                SelectShowHideOperBtn();
            }

        }
        if (!isDownMove) {

            $(".operabtn").css("display", "none");
            $(".operabtn2").addClass("hidden");
        }
        else {

            isDownMove = false;
        }

        if (1 == e.which) {
            MXstart = e.pageX;
            MYstart = e.pageY;
            isMouseDown = false;
            IsSelected = false;
            $("#MouseMovediv").css("display", "none");


        }
        //return false;
    });

    //点击资源内容事件
    $(".aContent").unbind('click');
    $(".aContent").click(function (e) {

        SelectIDS.splice(0, SelectIDS.length);

        $(".aContent").removeClass("selected");
        $(this).addClass("selected");
    });

    //div_list

    //点击资源内容事件
    $(".div_list td").unbind('click');
    $(".div_list td").click(function (e) {

        SelectIDS.splice(0, SelectIDS.length);

    });

    //普通标签的鼠标左键事件
    $("#table_body").unbind('click');
    $("#table_body").click(function (e) {
        var clickid = e.target.getAttribute("id");

        // 3 右键 1 左键
        if (1 == e.which) {
            HideOperateDialog();
            if (SelectIDS.length == 0) {
                $(".operabtn").css("display", "none");
                $(".operabtn2").addClass("hidden");
            }
        }
    });

    //----键盘事件------------------------------

    $("#table_body").unbind("keydown");
    $("#table_body").keydown(function (event) {
        alert(event.keyCode);
    });

    //-------------------------------------------

    //为文件夹绑定双击事件
    //  if (bindtype == "other") {
    $(".directory").unbind('dblclick');
    $(".directory").dblclick(function () {
        if (Ttype != "delete") {
            isopenfolder = false;
            $(".operabtn").css("display", "none");
            $(".operabtn2").addClass("hidden");

            var id = $(this).attr("id").substr(5);
            parent_id = id;
            Get_PathData("open");
            GetResContentByType(Ttype, "", "0");
        }
    });

    //    /////////////////////////////////////////////////////

}
//--------文件夹双击事件--------------------------
function FolderDoubleClick() {
    isopenfolder = false;
    $(".operabtn").css("display", "none");
    $(".operabtn2").addClass("hidden");

    var id = $(this).attr("id").substr(5);
    parent_id = id;
    Get_PathData("open");
    GetResContentByType(Ttype, "", '0');
}
//---------按键事件-------------------------------



//------------------------------------------------


//--------菜单栏按钮的显示和隐藏------------------
function ShowHideOperBtn() {
    if (Ttype != "delete") {
        //为文件和文件夹绑定单击事件--------------
        $(".div_fileinfo").unbind('click');
        $(".div_fileinfo").click(function () {

            //            if (event.keyCode == 13) {
            //                alert("abc");
            //            }
            //            else {
            //                alert("no");
            //            }
            //对列表显示的特殊标识

            if ($(this).hasClass("div_list")) {
                $(".div_list").removeClass("selected");
                $(this).addClass("selected");
                var infoid = $(this).attr("id").substr(5);
                $(".xuan_image").attr("src", "/imagesN/diskImages/xuankuang.png");
                $(".xuan_image").removeClass("checked");
                $(".xuan_image").addClass("unchecked");
                $("#Image_" + infoid).attr("src", "/imagesN/diskImages/xuankuang01.png");
                $("#Image_" + infoid).removeClass("unchecked");
                $("#Image_" + infoid).addClass("checked");
            }


            HideOperateDialog();
            //先隐藏全部
            $(".operabtn").css("display", "none");
            //显示相应的按钮
            $("#a_Delete").css("display", "block");
            $("#a_ReName").css("display", "block");
            $("#a_Remove").css("display", "block");
            if (!$(this).hasClass("directory")) {
                $("#a_Push").css("display", "block");
            }
            $("#a_Share").css("display", "inline");
            return false;
        });
    }
    else {
        //为文件和文件夹绑定单击事件--------------
        $(".div_fileinfo").unbind('click');
        $(".div_fileinfo").click(function () {

            HideOperateDialog();
            //先隐藏全部
            $(".operabtn2").removeClass("hidden");

            if ($(this).hasClass("div_list")) {
                $(".div_list").removeClass("selected");
                $(this).addClass("selected");
                var infoid = $(this).attr("id").substr(5);
                $(".xuan_image").attr("src", "/imagesN/diskImages/xuankuang.png");
                $(".xuan_image").removeClass("checked");
                $(".xuan_image").addClass("unchecked");
                $("#Image_" + infoid).attr("src", "/imagesN/diskImages/xuankuang01.png");
                $("#Image_" + infoid).removeClass("unchecked");
                $("#Image_" + infoid).addClass("checked");
            }
            return false;
        });
    }
}

///多选后显示或隐藏相关的操作按钮
function SelectShowHideOperBtn() {
    HideOperateDialog();
    if (Ttype != "delete") {
        //先隐藏全部
        $(".operabtn").css("display", "none");
        //显示相应的按钮
        $("#a_Delete").css("display", "block");
        //$("#a_Remove").css("display", "block");
        $("#a_Push").css("display", "block");
        for (var i = 0; i < SelectIDS.length; i++) {
            if ($("#div2_" + SelectIDS[i]).hasClass("directory")) {
                $("#a_Push").css("display", "none");
                break;
            }
        }
        $("#a_Share").css("display", "block");
    }
    else {
        $(".operabtn2").removeClass("hidden");
    }
    return false;

}
//显示或隐藏上排的按钮
function ShowOrHidenByType(thistype) {
    if (thistype == "other") {
        $("#td_upload").removeClass("hidden");
        $("#td_createfolder").removeClass("hidden");
        $("#td_download").removeClass("hidden");
        $("#td_other").removeClass("hidden");
        $(".td_other").removeClass("hidden");

        $("#td_revert").addClass("hidden");
        $("#td_delete").addClass("hidden");
        $("#td_delall").addClass("hidden");

    }
    else {
        $("#td_upload").addClass("hidden");
        $("#td_createfolder").addClass("hidden");
        $("#td_download").addClass("hidden");
        $("#td_other").addClass("hidden");
        $(".td_other").addClass("hidden");

        //--------

        //刚开始不显示还原和删除按钮
        $("#td_revert").addClass("hidden");
        $("#td_delete").addClass("hidden");
        $("#td_delall").removeClass("hidden");
    }

    //    //为文件夹绑定双击事件
    //    if (thistype == "other") {

    //        $(".directory").dblclick(function () {
    //            isopenfolder = false;
    //            $(".operabtn").css("display", "none");
    //            $(".operabtn2").addClass("hidden");

    //            var id = $(this).attr("id").substr(5);
    //            parent_id = id;
    //            Get_PathData("open");
    //            GetResContentByType(Ttype, "","1");
    //        });
    //    }
    /////////////////////////////////////////////////////

    //鼠标点击事件
    $(".div_fileinfo").unbind('mousedown');
    $('.div_fileinfo').mousedown(function (e) {

        var filetype = $(this).attr("type");
        switch (filetype) {
            case "file":
                file_type = "0";
                break;
            case "directory":
                file_type = "1";
                break;
            case "directory_share":
                file_type = "2";
                break;
            default:
                file_type = "0";
                break;

        }
        // 3 右键 1 左键
        file_id = $(this).attr("id").substr(5);

        if (3 == e.which) {
            var topp = e.pageY;
            var leftp = e.pageX;
            ShowOperateDialog(topp, leftp);

        }
    })
}

//设置搜索框的位置
function SetHtmlElePosition() {

    //------------搜索框设置---------------------------
    var objrank = GetElCoordinate(document.getElementById("hidden_search"));
    var atop = objrank.top + 5;
    var aleft = objrank.left;
    $("#search").css("top", atop);
    $("#search").css("left", aleft);
    // $("#search").css("display", "inline");

    //------------------------------------------------

    //--------整体table的大小设置---------------------

    var objtablehead = GetElCoordinate(document.getElementById("tr_head"));
    var cheight = document.documentElement.clientHeight - objtablehead.bottom - 10;

    $("#tr_content").css("height", cheight + "px");

    //------------------------------------------------
}

