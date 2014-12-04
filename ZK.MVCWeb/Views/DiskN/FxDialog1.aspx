<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FxDialog1</title>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        a
        {
            color: #000;
            text-decoration: none;
        }
        /*body{background:#f5f5f5;}*/
        #main
        {
            width: 550px;
            height: 400px;
        }
        .title
        {
            margin: 10px 0px 70px 0px;
            padding-left: 20px;
        }
        .key
        {
            margin-top: 100px;
            padding-left: 20px;
        }
        .pass
        {
            width: 188px;
            height: 30px;
            border: solid 1px #ccc;
            outline: none;
            display: none;
            text-indent:12px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var pass = $(".pass");
            $("#jm").click(function () {
                if ($(this).is(":checked")) {
                    pass.show();
                } else {
                    pass.hide();
                }
            });
        });
        parent.$.Next = function (ids) {
            var pass = $(".pass");
            var txt = pass.val().trim();
            if (txt == "") {
                //执行加密分享操作
            } else {
                //执行非加密分享操作
            }
            window.location.href = "/diskn/Fxdialog?password=" + txt + "&ids=" + ids;
        }
    </script>
</head>
<body>
    <div id="main">
        <img src="/DiskNN/images/FX/title_bg.gif" />
        <div class="content">
            <div class="title">
                你分享了<%=ViewData["filecount"]%>个文件</div>
            <img src="/DiskNN/images/FX/sharepic.gif" />
            <div class="key">
                <input type="checkbox" id="jm" />加密分享
                <input type="text" name="pass" class="pass" />
            </div>
        </div>
    </div>
</body>
</html>
