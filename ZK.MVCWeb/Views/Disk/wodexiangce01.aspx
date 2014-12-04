<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的相册</title>
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script type="text/javascript" src="/js/wangpan.js"></script>
    <link href="/Scripts/jquery-ui-1.10.3.custom/development-bundle/themes/base/jquery.ui.all.css"
        rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-ui-1.10.3.custom/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <link href="/Scripts/uploadify-v2.1.0/example/css/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/uploadify-v2.1.0/swfobject.js"></script>
    <script src="/Scripts/uploadify-v2.1.0/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="/js/Disk_Operate.js" type="text/javascript"></script>
</head>
<body onload="MM_preloadImages('/imagesN/diskImages/fanhui01.png','/imagesN/diskImages/shangchuan01.png','/imagesN/diskImages/xinjianwjj01.png','/imagesN/diskImages/lixianxiazai01.png')">
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="20" height="38">
            </td>
            <td height="35" class="text">
                全部文件<span class="one">&gt;我的相册</span>
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
            <td width="65" height="40">
                <a href="/Disk/wodexiangce01">
                    <img src="/imagesN/diskImages/fanhui.png" alt="" width="63" height="36" border="0"
                        id="Image1" onmouseover="MM_swapImage('Image1','','/imagesN/diskImages/fanhui01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="80" height="40">
                <a href="javascript:void(0);return false;" onclick="Btn_UploadFiles('list')">
                    <img src="/imagesN/diskImages/shangchuan.png" alt="" width="78" height="36" border="0"
                        id="Image2" onmouseover="MM_swapImage('Image2','','/imagesN/diskImages/shangchuan01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="99" height="40" style="display: none;">
                <a href="javascript:void(0); return false;" onclick="Btn_CreateNewFolder('list')">
                    <img src="/imagesN/diskImages/xinjianwjj.png" alt="" width="97" height="36" border="0"
                        id="Image3" onmouseover="MM_swapImage('Image3','','/imagesN/diskImages/xinjianwjj01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="90" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/lixianxiazai.png" alt="" width="88" height="36" border="0"
                        id="Image4" onmouseover="MM_swapImage('Image4','','/imagesN/diskImages/lixianxiazai01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
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
                <a href="/Disk/wodexiangce01">
                    <img src="/imagesN/diskImages/liebiao_02.png" width="12" height="12" border="0" /></a>
            </td>
            <td width="14" height="40">
                <a href="/Disk/xiangce">
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
    <iframe id="iframe_content" flag="/Disk/content_list?_type=image&parent_id=0" frameborder="0"
        style="width: 100%;"></iframe>
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
        </tr>
    </table>
    <div id="createfolder" style="display: none;" align="center">
        <p style="font-size: 10; color: Black;">
            请输入文件夹名：</p>
        <input type="text" id="txt_foldername" style="width: 200px;" />
        <%--<div align="right">
            <input type="button" onclick="AddNewFolder()" value="确定添加" /></div>--%>
    </div>
    <div id="uploadfile" style="display: none;" align="center">
        <p>
            文件上传</p>
        <%--<p>
            <a href="javascript:$('#uploadify').uploadifyUpload()">上传</a>| <a href="javascript:$('#uploadify').uploadifyClearQueue()">
                取消上传</a>
        </p>--%>
        <div id="fileQueue" style="width: 450px; height: 400px; *border: 0px;">
        </div>
        <br />
        <div align="right">
            <input type="file" name="uploadify" id="uploadify" /></div>
        <%--<div align="right">
            <input type="button" onclick="AddNewFolder()" value="确定添加" /></div>--%>
    </div>
</body>
</html>
