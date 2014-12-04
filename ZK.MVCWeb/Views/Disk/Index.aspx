<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>网盘首页</title>
</head>
<frameset rows="80,*" cols="*" frameborder="no" border="0" framespacing="0">
    <frame src="/Disk/Top" name="topFrame" scrolling="No" noresize="noresize" id="topFrame"
        title="topFrame" />
    <frameset cols="198,*" frameborder="no" border="0" framespacing="0">
        <frame src="/Disk/Left" name="leftFrame" scrolling="No" noresize="noresize" id="leftFrame"
            title="leftFrame" />
        <%--  <frame src="/Disk/Right" name="mainFrame" id="mainFrame" title="mainFrame" />--%>
        <frame src="/Disk/quanbuwenjian" name="mainFrame" id="mainFrame" title="mainFrame" />
        <noframes>
            <body>
            </body>
        </noframes>
    </frameset>
</frameset>
</html>
