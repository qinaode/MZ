<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=ViewData["webtitle"].ToString() %>--系统注册</title>
    <%--<link href="/css/login.css" rel="stylesheet" rev="stylesheet" type="text/css" media="all" />
    <link href="/css/demo.css" rel="stylesheet" rev="stylesheet" type="text/css" media="all" />--%>
    <script type="text/javascript" src="/js/jquery1.42.min.js"></script>
    <script type="text/javascript" src="/js/jquery.SuperSlide.js"></script>
    <script type="text/javascript" src="/js/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".i-text").focus(function () {
                $(this).addClass('h-light');
            });

            $(".i-text").focusout(function () {
                $(this).removeClass('h-light');
            });

            $("#ActiveCode").focus(function () {
                var username = $(this).val();
                if (username == '输入激活码') {
                    $(this).val('');
                }
            });

            $("#ActiveCode").focusout(function () {
                var username = $(this).val();
                if (username == '') {
                    $(this).val('输入激活码');
                }
            });
        });
        function SubmitActiveCode() {
            var code = $("#ActiveCode").val();
            if (code == "") {
                alert("请输入激活码");
                return;
            }
            $.ajax({
                url: "/Account/CheckForCode",
                type: "post",
                data: { "ActiveCode": code },
                datatype: "text",
                success: function (backdata) {
                    if (backdata == "true") {
                        alert("注册成功，请重新登录");
                        window.location.href = "/Account/Login";
                    }
                    else {
                        alert("注册失败");
                    }

                },
                error: function (backdata) {
                    alert("注册失败");
                }
            });
        }
    </script>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
        }
        .clear
        {
            clear: both;
        }
        .banner
        {
            padding: 0px;
            margin: 0;
            font-size: 18px;
            font-weight: bold;
        }
        .header
        {
            margin: 0 auto;
            width: 980px;
        }
        .logo
        {
            position: relative;
            float: left;
            margin-top: 50px;
            margin-bottom: 10px;
        }
        .title
        {
            position: relative;
            float: left;
            margin-top: 78px;
            padding-left: 10px;
        }
        .content
        {
            margin: 0 auto;
            background: url(/imagesN/content_bg.jpg) repeat-x;
            height: 500px;
        }
        .cont
        {
            width: 980px;
            margin: 0 auto;
            height: 500px;
            background: url(/imagesN/content.jpg) no-repeat;
        }
        .loginForm
        {
            position: relative;
            float: right;
            top: 98px;
            right: 70px;
            border: 1px solid #CCC;
            width: 296px;
            height: 300px;
            background: #f2fbf7;
            border-radius: 10px;
            font-size: 16px;
            text-align: left;
            color: #fff;
            font-weight: bold;
        }
        .loginForm p
        {
            color: #666;
            font-size: 14px;
            padding-left: 20px;
        }
        .loginForm ul.userlist
        {
            background: #65b43b;
            padding-top: 20px;
            padding-bottom: 15px;
            margin: 0px;
            -webkit-border-top-left-radius: 10px;
            -webkit-border-top-right-radius: 10px;
        }
        /*.btn{width:110px; height:30px; text-align:center; cursor:pointer; font-size:14px; display:inline-block; line-height:38px; outline:0; background-color:transparent; border-radius:3px;}*/
        .username
        {
            width: 260px;
            height: 30px;
            border-radius: 3px;
            margin-top: 13px;
            color: #9cb5cd;
            font-size: 14px;
            text-indent: 12px;
        }
        .pwd
        {
            width: 191px;
            height: 30px;
            border-radius: 3px;
            margin-top: 13px;
            color: #9cb5cd;
            font-size: 14px;
            text-indent: 12px;
        }
        .btn
        {
            margin-top: 23px;
            margin-left: 20px;
            width: 258px;
            height: 38px;
            text-align: center;
            line-height: 38px;
            background-color: transparent;
            background-image: url(/imagesN/button.png);
            color: #fff;
            font-size: 16px;
            font-weight: bold;
        }
        .btn-login-active
        {
            background-image: url(/imagesN/button1.png);
        }
    </style>
</head>
<body onload="MM_preloadImages('/imagesN/button1.png')">
    <div class="banner">
        <div class="header">
            <div class="logo">
                <a href="http://www.hoolian.cn">
                    <img alt="" src="/imagesN/LOGO.png" border="0" id="Image1" /></a></div>
            <div class="title">
                | 校园知识管理系统</div>
            <div class="clear">
            </div>
        </div>
        <div class="content">
            <div class="cont">
                <div class="loginForm">
                    <ul class="userlist">
                        该系统尚未激活，请激活后使用</ul>
                    <form class="registerform" action="/account/CheckLogin/">
                    <br />
                    <br />
                    <p>
                        激活码：</p>
                    <p>
                        <input type="text" name="ActiveCode" id="ActiveCode" class="username" title="输入激活码" value="输入激活码"
                            style="border: 1px solid #999;" /></p>
                            
                    <button type="button" class="btn btn-login-active" id="loginBtn" style="border: 0px;
                        cursor: pointer;" onclick="SubmitActiveCode()">
                        激&nbsp;&nbsp;活</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
