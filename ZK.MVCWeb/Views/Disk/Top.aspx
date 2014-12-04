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
            margin: 0px;
            padding: 0px;
            background: #4dc708;
        }
    </style>
</head>
<body>
    <div class="topbar_wrap">
        <div class="topbar">
            <h1>
            </h1>
            <p>
                <%=((ZK.Model.USERS)(Session["user"])).USERNAME%>
                | <a href="/account/logout" target="_parent">退出</a></p>
        </div>
    </div>
</body>
</html>
