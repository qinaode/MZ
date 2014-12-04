<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ToEmail</title>
    <script src="../../js/jquery1.42.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

        });
    </script>
</head>
<body>
    <div>
        Token:<%=ViewData["Access_Token"].ToString()%>
        <br />
        AuthKey:<%=ViewData["AuthKey_content"].ToString()%> 
        <br />
        新邮件数：<%=ViewData["emailcount_content"].ToString()%>
        <br />
        <%--实时消息：<%=ViewData["result"].ToString()%>--%>
    </div>
</body>
</html>
