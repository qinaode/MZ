<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="ZK.Controllers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>wenjiancontent</title>
    <link href="../../css/Diskpage.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script src="/Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/jquery-ui-1.10.3.custom/development-bundle/themes/base/jquery.ui.all.css">
    <script src="/Scripts/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/wangpan.js"></script>
    <script src="/js/disk_content.js" type="text/javascript"></script>
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
        $(function () {
            //双击打开文件夹
            $(".directory").dblclick(function () {
                window.parent.document.getElementById("iframe_content").setAttribute("src", "/Disk/content_pic?_type=" + _type + "&parent_id=" + file_id);
                window.location.href = "/Disk/content_pic?_type=" + _type + "&parent_id=" + file_id;

            });
        });
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
<body>
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
                    <!-- 用来记录文件的类型 文件 或 文件夹 -->
                    <input type="hidden" id="hidtype_<%=item.id %>" value="<%=item.file_type %>" />
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
        <li><a href="#"><span class="ui-icon ui-icon-disk"></span>分享</a></li>
        <li><a href="#"><span class="ui-icon ui-icon-disk"></span>共享</a></li>
        <li id="li_push"><a onclick="PushFile()" id="a_push" target="_blank"><span class="ui-icon ui-icon-zoomin">
        </span>推送</a></li>
        <li><a href="#"><span class="ui-icon ui-icon-zoomout"></span>重命名</a></li>
        <li><a href="#" onclick="DeleteFile()" id="delete"><span class="ui-icon ui-icon-print">
        </span>删除</a></li>
        <li><a href="#"><span class="ui-icon ui-icon-print"></span>移动</a> </li>
    </ul>
</body>
</html>
