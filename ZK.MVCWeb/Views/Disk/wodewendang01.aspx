<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>回收站</title>
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script type="text/javascript" src="/js/wangpan.js"></script>
</head>
<body onload="MM_preloadImages('/imagesN/diskImages/fanhui01.png','/imagesN/diskImages/shangchuan01.png','/imagesN/diskImages/xinjianwjj01.png','/imagesN/diskImages/lixianxiazai01.png')">
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="20" height="38">
            </td>
            <td height="35" class="text">
                全部文件<span class="one">&gt;我的文档</span>
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
                <a href="/Disk/wodewendang01">
                    <img src="/imagesN/diskImages/fanhui.png" alt="" width="63" height="36" border="0"
                        id="Image1" onmouseover="MM_swapImage('Image1','','/imagesN/diskImages/fanhui01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="80" height="40">
                <a href="#">
                    <img src="/imagesN/diskImages/shangchuan.png" alt="" width="78" height="36" border="0"
                        id="Image2" onmouseover="MM_swapImage('Image2','','/imagesN/diskImages/shangchuan01.png',1)"
                        onmouseout="MM_swapImgRestore()" /></a>
            </td>
            <td width="99" height="40">
                <a href="#">
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
                <a href="/Disk/wodewendang01">
                    <img src="/imagesN/diskImages/liebiao_02.png" width="12" height="12" border="0" /></a>
            </td>
            <td width="14" height="40">
                <a href="/Disk/wodewendang">
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
    <iframe src="/Disk/content_list?_type=application&parent_id=0" frameborder="0" style="width: 100%;">
    </iframe>
    <table class="tb1" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
        </tr>
    </table>
</body>
</html>
