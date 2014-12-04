<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的网盘</title>
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script src="/Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/jquery-ui-1.10.3.custom/development-bundle/themes/base/jquery.ui.all.css">
    <script src="/Scripts/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/wangpan.js"></script>
    <script src="/js/disk_content.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-menu
        {
            /* 自定义宽度 */
            width: 120px;
        }
        .selected
        {
            *border: 1px solid blue;
            background-color:#f4fff9;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //            $("#dialog-modal").dialog({
            //                height: 140,
            //                modal: false
            //            });

            //双击打开文件夹
            $(".directory").dblclick(function () {
                window.parent.document.getElementById("iframe_content").setAttribute("src", "/Disk/content_pic?_type=" + _type + "&parent_id=" + file_id);
                window.location.href = "/Disk/content_pic?_type=" + _type + "&parent_id=" + file_id;

            });

        });
    </script>
</head>
<body class="div_mousedown">
    <div style="width: 100%; height: 100%;">
        <ul style="list-style-type: none;" class="hidemenu">
            <%List<ZK.Model.miniyun_files> filelist = (List<ZK.Model.miniyun_files>)ViewData["filelist"]; %>
            <%foreach (var item in filelist)
              {
                  item.mime_type = ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type);
                  string strclass = "file";
                  if (item.file_type == 1)
                  {
                      strclass = "directory";
                  }
            %>
            <li style="margin-left: 10px; cursor: pointer;" id="div1_<%=item.id %>">
                <div style="width: 120px; height: 150px; float: left;" align="center" class="div_fileinfo <%=strclass %>"
                    id="div2_<%=item.id %>">
                    <!-- 用来记录文件的类型 文件 或 文件夹 -->
                    <input type="hidden" id="hidtype_<%=item.id %>" value="<%=item.file_type %>" />
                    <a class="two aContent" title="<%=item.file_name %>">
                        <img src="<%=ZK.Common.GetFileImage.getListImageForDisk(Convert.ToInt32(item.mime_type)) %>"
                            border="0" align="absmiddle" /><br />
                        <p id="afn_<%=item.id %>">
                            <%=item.file_name%></p>
                    </a>
                    <%--  <input type="text" id="tfn_<%=item.id %>" class="inputname" value="<%=item.file_name%>"
                    style="display: none;" />--%>
                </div>
            </li>
            <%
                } %>
        </ul>
        <ul id="menu" style="display: none; position: absolute; top: 100px; left: 100px;">
            <li><a href="#"><span class="ui-icon ui-icon-disk"></span>分享</a></li>
            <li><a href="#"><span class="ui-icon ui-icon-disk"></span>共享</a></li>
            <li id="li_push"><a onclick="PushFile()" id="a_push" target="_blank"><span class="ui-icon ui-icon-zoomin">
            </span>推送</a></li>
            <li><a href="#" onclick="ResetName()"><span class="ui-icon ui-icon-zoomout"></span>重命名</a></li>
            <li><a href="#" onclick="DeleteFile()" id="delete"><span class="ui-icon ui-icon-print">
            </span>删除</a></li>
            <li><a href="#"><span class="ui-icon ui-icon-print"></span>移动</a> </li>
        </ul>
    </div>
</body>
</html>
