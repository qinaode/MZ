<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="ZK.Controllers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回收站</title>
    <link href="../../css/Diskpage.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script src="/Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/jquery-ui-1.10.3.custom/development-bundle/themes/base/jquery.ui.all.css">
    <script src="/Scripts/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/wangpan.js"></script>
    <script src="../../js/disk_huishouzhan.js" type="text/javascript"></script>
    <style type="text/css">
        a
        {
            text-decoration: none;
            color: #000;
        }
        a:hover
        {
            text-decoration: underline;
        }
        .ui-menu
        {
            /* 自定义宽度 */
            width: 120px;
        }
    </style>
    <script type="text/javascript">
        function MM_changeProp(objId, x, theProp, theValue) { //v9.0
            var obj = null; with (document) {
                if (getElementById)
                    obj = getElementById(objId);
            }
            if (obj) {
                if (theValue == true || theValue == false)
                    eval("obj.style." + theProp + "=" + theValue);
                else eval("obj.style." + theProp + "='" + theValue + "'");
            }
        }
    </script>
</head>
<body onload="MM_preloadImages('/imagesN/diskImages/huanyuanwenjian01.png','/imagesN/diskImages/shanchu01.png','/imagesN/diskImages/qingkong01.png')">
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="20" height="38">
            </td>
            <td height="35" class="text">
                回收站<span class="one"></span>
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
                <a href="/Disk/wodewendang01">
                    <img src="/imagesN/diskImages/huanyuanwenjian.png" alt="" width="97" height="36"
                        border="0" id="Image1" onmouseover="MM_swapImage('Image1','','/imagesN/diskImages/huanyuanwenjian01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="70" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/shanchu.png" alt="" width="66" height="36" border="0"
                        id="Image2" onmouseover="MM_swapImage('Image2','','/imagesN/diskImages/shanchu01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="90" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/qingkong.png" alt="" width="88" height="36" border="0"
                        id="Image3" onmouseover="MM_swapImage('Image3','','/imagesN/diskImages/qingkong01.png',1)"
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
                <a href="/Disk/huishouzhan01">
                    <img src="/imagesN/diskImages/liebiao_02.png" width="12" height="12" border="0" /></a>
            </td>
            <td width="14" height="40">
                <a href="/Disk/huishouzhan">
                    <img src="/imagesN/diskImages/liebiao_03.png" width="12" height="12" border="0" /></a>
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
    <div class="hidemenu">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="10">
                    &nbsp;
                </td>
                <td width="18">
                    <img src="/imagesN/diskImages/xuankuang.png" width="17" height="17" />
                </td>
                <td colspan="2">
                    文件名
                </td>
                <td width="234">
                    下载次数
                </td>
                <td width="150">
                    大小
                </td>
                <td width="50" align="right">
                    日期
                </td>
                <td width="14" align="right">
                    <img src="/imagesN/diskImages/xiala.png" width="7" height="7" />
                </td>
                <td width="8">
                    &nbsp;
                </td>
                <td width="27">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="10">
                    <hr />
                </td>
            </tr>
            <%List<ZK.Model.miniyun_files> Lists = (List<ZK.Model.miniyun_files>)ViewData["wenjianlist"]; %>
            <%foreach (var item in Lists)
              {
                  string strclass = "file";
                  if (item.file_type == 0)
                  {
                      strclass = "directory";
                  }
                  item.mime_type = ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type);
            %>
            <tr valign="middle" id="div1_<%=item.id.ToString() %>" onmouseover="MM_changeProp('div1_<%=item.id.ToString() %>','','backgroundColor','#f0fef1','TR')"
                onmouseout="MM_changeProp('div1_<%=item.id.ToString() %>','','backgroundColor','#ffffff','TR')"
                class="div_fileinfo <%=strclass %>">
                <td width="10" height="35">
                    <a href="#"></a>
                </td>
                <td width="18">
                    <a href="#" onclick="MM_swapImage('Image_<%=item.id.ToString() %>','','/imagesN/diskImages/xuankuang01.png',1)"
                        onmouseup="MM_swapImgRestore()">
                        <img src="/imagesN/diskImages/xuankuang.png" width="17" height="17" border="0" id="Image_<%=item.id.ToString() %>" /></a>
                </td>
                <td width="36">
                    <a href="#">
                        <img src="<%=ZK.Common.GetFileImage.getListImageForDisk(Convert.ToInt32(item.mime_type)) %>"
                            width="33" height="28" border="0" /></a>
                </td>
                <td width="515">
                    <a href="#">
                        <%=item.file_name %></a>
                </td>
                <td>
                    <a href="#">暂缺</a>
                </td>
                <td>
                    <a href="#">
                        <%=item.file_size %></a>
                </td>
                <td colspan="2" align="right">
                    <a href="#">
                        <%=item.created_at %></a>
                </td>
                <td>
                    <a href="#"></a>
                </td>
                <td>
                    <a href="#"></a>
                </td>
            </tr>
            <%     
                } %>
            <tr valign="middle" id="div_0" onmouseover="MM_changeProp('div_0','','backgroundColor','#f0fef1','TR')"
                onmouseout="MM_changeProp('div_0','','backgroundColor','#ffffff','TR')" class="div_fileinfo">
                <td width="10" height="35">
                    <a href="#"></a>
                </td>
                <td width="18">
                    <a href="#" onclick="MM_swapImage('Image5','','/imagesN/diskImages/xuankuang01.png',1)"
                        onmouseup="MM_swapImgRestore()">
                        <img src="/imagesN/diskImages/xuankuang.png" width="17" height="17" border="0" id="Image5" /></a>
                </td>
                <td width="36">
                    <a href="#">
                        <img src="0" width="33" height="28" border="0" /></a>
                </td>
                <td width="515">
                    <a href="#">测试文件名</a>
                </td>
                <td>
                    <a href="#">暂缺</a>
                </td>
                <td>
                    <a href="#">10M</a>
                </td>
                <td colspan="2" align="right">
                    <a href="#">2013/12/11</a>
                </td>
                <td>
                    <a href="#"></a>
                </td>
                <td>
                    <a href="#"></a>
                </td>
            </tr>
            <tr>
                <td width="10">
                    &nbsp;
                </td>
                <td width="18">
                    &nbsp;
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td colspan="2" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <span class="link" style="float: right; width: 500px">
            <%=Html.Pager("page", int.Parse(ViewData["pagesize"].ToString()), int.Parse(ViewData["totalcount"].ToString()))%>
        </span>
    </div>
    <div style="height: 10px;">
        &nbsp;</div>
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
