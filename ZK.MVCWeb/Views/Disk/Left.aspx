<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的网盘</title>
    <link type="text/css" rel="stylesheet" href="/css/wp.css" />
    <script type="text/javascript" src="/js/wangpan.js"></script>
    <style type="text/css">
        body
        {
            padding: 0px;
            margin-top: 10px;
            width: 165px;
            text-align: left;
            background: url(/imagesN/diskImages/zuoce_bg.jpg) repeat-y;
            color: #676767;
            font-size: 14px;
        }
    </style>
</head>
<body style="margin-left: 0px;">
    <table class="tb" width="100%" border="0" align="left" cellspacing="0">
        <tr>
            <td height="30" colspan="2" align="left" valign="middle">
                <a href="/Disk/quanbuwenjian" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/quanbuwenjian.png" width="12" height="12" border="0" />&nbsp;&nbsp;全部文件</a>
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="left">
                <a href="/Disk/xiangce" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/wodexiangce.png" width="12" height="12" border="0" />&nbsp;&nbsp;我的相册</a>
            </td>
        </tr>
        <tr>
            <td height="30" align="left">
                <a href="/Disk/wodeshipin" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/wodeshipin.png" width="12" height="12" border="0" />&nbsp;&nbsp;我的视频</a>
            </td>
        </tr>
        <tr>
            <td height="30" align="left">
                <a href="/Disk/wodewendang" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/wodewendang.png" width="12" height="12" border="0" />&nbsp;&nbsp;我的文档</a>
            </td>
        </tr>
        <tr>
            <td height="30" align="left">
                <a href="/Disk/wofenxiang" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/fenxiang_02.png" width="12" height="12" border="0" />&nbsp;&nbsp;我分享给别人的</a>
            </td>
        </tr>
        <tr>
            <td height="30" align="left">
                <a href="/Disk/bierenfenxgeiwod" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/fenxiang_01.png" width="12" height="12" border="0" />&nbsp;&nbsp;别人分享给我的</a>
            </td>
        </tr>
        <tr>
            <td height="30" align="left">
                <a href="/Disk/huishouzhan" target="mainFrame" class="zuocedh">
                    <img src="/imagesN/diskImages/huishouzhan.png" width="12" height="12" border="0" />&nbsp;&nbsp;回收站</a>
            </td>
        </tr>
    </table>
</body>
</html>
