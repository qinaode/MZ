<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var txtSearch = "";
        $(document).ready(function () {
            alert(0);
            $.ajax({
                type: "Post",
                url: "/User/UserListJosn",
                data: {},
                datatype: "json/text",
                success: function (backdata) {
                    alert(backdata);
                }
            });
        });
    </script>
</head>
<body>
    <div>
           aaaa
    </div>
</body>
</html>
