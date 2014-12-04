<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回收站</title>
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script src="/Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/wangpan.js"></script>
    <link rel="stylesheet" href="/Scripts/jquery-ui-1.10.3.custom/development-bundle/themes/base/jquery.ui.all.css">
    <script src="/Scripts/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/disk_huishouzhan.js"></script>
    <style type="text/css">
        .ui-menu
        {
            /* 自定义宽度 */
            width: 120px;
        }
    </style>
    <script type="text/javascript">
        $(function () {



        });
    </script>
</head>
<body onload="MM_preloadImages('/imagesN/diskImages/huanyuanwenjian01.png','/imagesN/diskImages/shanchu01.png','/imagesN/diskImages/qingkong01.png')">
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="20" height="38">
            </td>
            <td height="35" class="text">
                回收站
            </td>
            <td width="180" height="35">
                <img src="/imagesN/diskImages/sousoukuang.png" width="180" height="26" />
            </td>
            <td width="10" height="38">
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr align="center" valign="bottom">
            <td width="10" height="40">
                &nbsp;
            </td>
            <td width="100" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/huanyuanwenjian.png" width="97" height="36" border="0"
                        id="Image1" onmouseover="MM_swapImage('Image1','','/imagesN/diskImages/huanyuanwenjian01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="68" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/shanchu.png" width="66" height="36" border="0" id="Image2"
                        onmouseover="MM_swapImage('Image2','','/imagesN/diskImages/shanchu01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="90" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/qingkong.png" width="88" height="36" border="0" id="Image3"
                        onmouseover="MM_swapImage('Image3','','/imagesN/diskImages/qingkong01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="90" height="40">
                &nbsp;
            </td>
            <td height="40">
                &nbsp;
            </td>
            <td height="40" align="right">
                排序：<img src="/imagesN/diskImages/paixu.png" width="12" height="12" />日期
            </td>
            <td width="5" height="40">
            </td>
            <td width="14" height="40">
                &nbsp;<a href="/Disk/huishouzhan01"><img src="/imagesN/diskImages/liebiao_01.png"
                    width="12" height="12" border="0" /></a>
            </td>
            <td width="14" height="40">
                &nbsp;<a href="/Disk/huishouzhan"><img src="/imagesN/diskImages/liebiao_03.png" width="12"
                    height="12" border="0" /></a>
            </td>
            <td width="10" height="40">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" class="tb2">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <ul style="list-style-type: none;" class="hidemenu">
        <%List<ZK.Model.miniyun_files> filelist = (List<ZK.Model.miniyun_files>)ViewData["filelist"]; %>
        <%foreach (var item in filelist)
          {
              item.mime_type = ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type);
              string strclass = "file";
              if (item.file_type == 0)
              {
                  strclass = "directory";
              }
        %>
        <li style="margin-left: 10px; cursor: pointer;" id="div1_<%=item.id %>">
            <div style="width: 120px; height: 120px; float: left;" align="center" class="div_fileinfo <%=strclass %>"
                id="div2_<%=item.id %>">
                <a class="two" title="<%=item.file_name %>">
                    <img src="<%=ZK.Common.GetFileImage.getListImageForDisk(Convert.ToInt32(item.mime_type)) %>"
                        border="0" align="absmiddle" /><br />
                    <%=item.file_name%>
                </a>
            </div>
        </li>
        <%
            } %>
    </ul>
    <ul id="menu" style="display: none; position: absolute; top: 100px; left: 100px;">
        <li><a href="#" onclick="RevertFile()"><span class="ui-icon ui-icon-disk"></span>还原</a></li>
        <li><a href="#" onclick="DeleteFile('one')"><span class="ui-icon ui-icon-disk"></span>
            删除</a></li>
        <li><a href="#" onclick="DeleteFile('all')"><span class="ui-icon ui-icon-print"></span>
            清空</a></li>
    </ul>
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
        </tr>
    </table>
</body>
</html>
