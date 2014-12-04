<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FxDialog</title>
    <style type="text/css">
        *{margin:0;padding:0}
        a{color:#000;text-decoration:none;}
        /*body{background:#f5f5f5;}*/
        #main{width:550px;height:400px;}
        #textbox{width:530px;height:320px;border:solid 1px #c6c6c6;margin:10px 10px;position:relative;}
        #textbox .link{width:500px;margin:10px auto;}
        #textbox .link .copy, #textbox .link .view{float:right;margin-left:10px;color:#4682d1}
        #icons{list-style-type:none;border-top:solid 1px #c6c6c6;position:absolute;bottom:0;left:0;width:520px;padding-left:10px;}
        #icons li{float:left;height:33px;padding:10px 0;margin-left:5px;}
        #icons li a{display:inline-block;padding: 3px 3px;color:#4682d1;font-size:18px;font-family:微软雅黑}
        #icons li a:hover{text-decoration:underline;}
        #icons li img{cursor:pointer;}
    </style>
</head>
<body>
    <div id="main">
        <img src="/DiskNN/images/FX/title_bg.gif" />
        <div id="textbox">
            <%
                foreach (var url in ViewData["urls"] as List<string>)
                {
             %>
           <div class="link">
                <a target="_blank" href="<%=url%>"><%=url%></a>
                <a class="view" target="_blank" href="<%=url%>">查看</a>
                <%--<a class="copy" href="javascript:void(0)">复制</a>--%>
            </div>
             <%   }
                
             %>

            <ul id="icons">
                <li><img src="/DiskNN/images/FX/icon1.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon2.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon3.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon4.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon5.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon6.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon7.gif" /></li>
                <li><img src="/DiskNN/images/FX/icon8.gif" /></li>
                <li><a href="javascript:void(0)">更多</a></li>
            </ul>
        </div>
    </div>
</body>
</html>
