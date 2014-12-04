/*
包含内容：
点击切换显示方式  列表 或者 图标

根据不同类型获取该类型下的所有资源文件

所有资源的展示 图标显示 和 列表显示

回收站资源的展示 图标显示 和 列表显示

文件路径的相关函数

获取和设置路径数组 并赋值给路径标签

点击文件夹名返回相关的目录

*/

//点击切换显示方式  列表 或者 图标-----------------
function ChangeResShow(flag) {

    ShowType = flag;
    GetResContentByType(Ttype, "", '0');
}

//-------------------------------------------------

//根据不同类型获取该类型下的所有资源文件-----------
// num :0  第一次加载 绑定事件 1 不绑定事件
function GetResContentByType(rtype, pid, num) {
    FirstLoad = num;
    if (pid != "") {
        parent_id = pid;
    }
    Ttype = rtype;
    if (parent_id == "0") {
        GetFolderPathForRoot();
    }

    if (Ttype == "delete") {
        if (ShowType == 1) {
            GetDeleteContent_1(rtype);
        }
        else {
            GetDeleteContent_2(rtype);
        }
    }
    else {
        if (ShowType == 1) {
            GetContent_1(rtype);
        }
        else {
            GetContent_2(rtype);
        }
    }
}

//--------------------------------------------------


//所有资源的展示 图标显示 和 列表显示---------------

//根据类型获取相关类型的资源列表 图标显示
function GetContent_1(rtype) {

    $.ajax({
        type: "Post",
        url: "/DiskN/GetResContentByType",
        data: { "type": rtype, "parent_id": parent_id, "rankflag": rankflag },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                $("#div_content").html("");
            };

            var tempjson = $.parseJSON(backdata);

            $("#div_content").setTemplateURL("/pagetemples/ResContent.htm", null, null);
            $("#div_content").processTemplate(tempjson);

            if (FirstLoad == "0") {
                BindForEvent();
            }
            ShowOrHidenByType("other");
            ShowHideOperBtn();
        }
    });
}

//根据类型获取相关类型的资源列表 列表显示
function GetContent_2(rtype) {
    $.ajax({
        type: "Post",
        url: "/DiskN/GetResContentByType_2",
        data: { "type": rtype, "parent_id": parent_id, "rankflag": rankflag },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                $("#div_content").html("");
            };

            var tempjson = $.parseJSON(backdata);
            $("#div_content").setTemplateURL("/pagetemples/ResContent_2.htm", null, null);
            $("#div_content").processTemplate(tempjson);
            //            $("#Pager").html("<span class=\"link\" style=\"float: right; width: 500px\"><%=Html.Pager(\"page\", int.Parse(ViewData[\"pagesize\"].ToString()), int.Parse(ViewData[\"totalcount\"].ToString()))%></span>");
            // document.getElementById("Pager").appendChild("<span class=\"link\" style=\"float: right; width: 500px\"><%=Html.Pager(\"page\", 10, 100)%></span>");
            // BindForEvent("other");
          

            if (FirstLoad == "0") {
                BindForEvent();
            }
            ShowOrHidenByType("other");
            ShowHideOperBtn();
        }
    });
}

//-----------------------------------------------

//获取回收站的资源列表 图标显示 和 列表显示------

//获取回收站的资源列表 图标显示
function GetDeleteContent_1(rtype) {
    $.ajax({
        type: "Post",
        url: "/DiskN/GetResContentByType",
        data: { "type": rtype, "parent_id": parent_id, "rankflag": rankflag },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                $("#div_content").html("");
            };

            var tempjson = $.parseJSON(backdata);

            $("#div_content").setTemplateURL("/pagetemples/DeleteContent.htm", null, null);
            $("#div_content").processTemplate(tempjson);
            //BindForEvent("delete");
            ShowOrHidenByType("delete");

            if (FirstLoad == "0") {
                BindForEvent();
            }
            ShowHideOperBtn();
        }
    });
}

//获取回收站的资源列表 列表显示
function GetDeleteContent_2(rtype) {
    $.ajax({
        type: "Post",
        url: "/DiskN/GetResContentByType_2",
        data: { "type": rtype, "parent_id": parent_id, "rankflag": rankflag },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                $("#div_content").html("");
            };

            var tempjson = $.parseJSON(backdata);
            $("#div_content").setTemplateURL("/pagetemples/DeleteContent_2.htm", null, null);
            $("#div_content").processTemplate(tempjson);
            // BindForEvent("delete");
            ShowOrHidenByType("delete");

            if (FirstLoad == "0") {
                BindForEvent();
            }
            ShowHideOperBtn();
        }
    });
}

//------------------------------------------------------------

//初始化 或 点击菜单按钮时触发 获取文件的路径
function GetFolderPathForRoot() {

    path_ids = new Array();
    path_names = new Array();

    var parent_Path = "";

    switch (Ttype) {
        case "all":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"all\",0)'>全部文件</a>";
            break;
        case "image":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"image\",0)'>我的相册</a>";
            break;
        case "video":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"video\",0)'>我的视频</a>";
            break;
        case "application":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"application\",0)'>我的文档</a>";
            break;
        case "toother":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"toother\",0)'>我分享给别人的</a>";
            break;
        case "tome":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"tome\",0)'>别人分享给我的</a>";
            break;
        case "delete":
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"delete\",0)'>回收站</a>";
            break;
        default:
            parent_Path = "<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"all\",0)'>全部文件</a>";
            break;
    }

    path_ids.push("0");
    path_names.push(parent_Path);
    //初始化
    Get_PathData("root");
}

//返回按钮的事件
function Btn_ReturnBack() {
    Get_PathData("back");
};


//获取和设置路径数组 并赋值给路径标签
function Get_PathData(operflag) {
    var pathstr = "";
    if (operflag == "root") {
        $("#a_path").html(path_names[0]);
        return;
    }
    $.ajax({
        url: "/DiskN/GetFolderName",
        type: "Post",
        data: { "folder_id": parent_id },
        datatype: "text",
        success: function (backdata) {
            if (operflag == "open") {
                path_ids.push(parent_id);
                //  path_names.push(backdata);
                path_names.push("<a href='javascript:void(0);return false;' class='folderpath' onclick='FolderPathClick(\"" + parent_id + "\",1)'>" + backdata + "</a>");

            }
            else if (operflag == "back") {
                if (path_ids.length > 1) {
                    path_ids.splice(path_ids.length - 1, 1);
                    path_names.splice(path_names.length - 1, 1);

                    parent_id = path_ids[path_ids.length - 1];
                    GetResContentByType(Ttype, "", '0');
                }
                else {

                    GetResContentByType(Ttype, "0", '0');
                }
            }

            for (var i = 0; i < path_names.length; i++) {
                if (i < path_names.length - 1) {
                    pathstr = pathstr + path_names[i] + " > ";
                }
                else {
                    pathstr = pathstr + path_names[i];
                }
            }
            $("#a_path").html(pathstr);
        }
    });
}

//点击文件夹路径返回到相关文件夹目录下
function FolderPathClick(folderid, type) {
    var pathstr = "";
    if (type == 1) {
        GetResContentByType(Ttype, folderid, "0");

        //查找出index 然后删除之后的所有项
        var pindex = path_ids.indexOf(folderid);

        path_names.splice(pindex + 1);
        path_ids.splice(pindex + 1);

    } //点击根目录
    else {
        GetResContentByType(Ttype, "0", "0");
        path_names.splice(1);
        path_ids.splice(1);

    }
    //拼接路径
    for (var i = 0; i < path_names.length; i++) {
        if (i < path_names.length - 1) {
            pathstr = pathstr + path_names[i] + " > ";
        }
        else {
            pathstr = pathstr + path_names[i];
        }
    }
    $("#a_path").html(pathstr);

}

