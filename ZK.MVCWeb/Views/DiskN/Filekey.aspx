<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="/Content/easyui/jquery-1.8.0.min.js" type="text/javascript"></script>
    <title>文件分享</title>
    <script type="text/javascript">
        $(function () {
            $("#btn_submit").click(function () {
                var pwd = $("#pwd").val().trim();
                if (pwd == "") {
                    $("#msg").html("请输入访问密码！");
                    return;
                }
                $("#data_form").submit();
            });
        });
    </script>
    <style type="text/css">
        *{margin:0;padding:0}
        body{background:#f4f4f4;}
        a{color:#000;text-decoration:none;}
        .c{width:970px;margin:0 auto;}
        .top{height:57px;width:100%;background:url(/content/images/file_title_bg.gif)}
        .msg{height:40px;line-height:40px;}
        .main{height:400px;border:solid 1px #ccc;padding:15px 15px;background:#fff;}
        .main .left,.main .right{float:left;}
        .main .right{width:500px;}
        .main .right h3{margin-top:40px;margin-bottom:20px;}
        .main .right p{line-height:30px;font-size:12px;}
        .main .right p .link{height:35px;border:solid 1px #ccc;border-radius:3px;width:250px;outline:0;}
        
        .main .right p .copy_btn{height:35px;width:70px;border:solid 1px #ccc;border-radius:3px;display:inline-block;text-align:center;cursor:pointer;}
        .main .right p .copy_btn:hover{background:#f3f3f3;}
        .right .download{font-size:18px;font-weight:bold;}
    </style>
</head>
<body>
    <div class="top">
        <div class="c">
            <img src="/Content/images/file_logosmall.gif" />
        </div>
    </div>
    <div class="msg c">
        <p>本站严禁上传包括反动，暴力，色情，违法及侵权内容的文件。</p>
    </div>
    <div class="main c">
        <form action="/diskn/files" method="post" id="data_form">
        <div class="right">
            <h3>该文件需要您输入访问密码才能查看</h3>
            <p style="color:#e45c27">请输入密码：</p>
            <input type="hidden" name="id" value="<%=ViewData["fileid"] %>" />
            <p style="height:37px;line-height:37px;">访问密码：<input id="pwd" name="pwd" class="link" value="" /> <a class="copy_btn" id="btn_submit">确定</a></p>
            <p id="msg" style="color:#e45c27"><%=ViewData["msg"] == null ? "" : ViewData["msg"]%></p>
            <p style="height:70px;line-height:70px;">
                <a  class="download">温馨提示：请放心使用!</a>
            </p>
        </div>
        </form>
        <div class="left">
            <img src="/Content/images/file_computer.gif" />
        </div>
    </div>
</body>
</html>
