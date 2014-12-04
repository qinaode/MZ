<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZK.Manage.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>皓联产品--知识库管理平台</title>
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
        .banben
        {
            background: #e8e8e8;
            padding: 5px;
            margin: 0px;
        }
        .banbencont
        {
            margin: 0 auto;
            width: 980px;
            text-align: right;
        }
        .content
        {
            margin: 0 auto;
            background: url(images/content_bg1.jpg) repeat-x;
            height: 236px;
            text-align: center;
        }
        .text
        {
            position: relative;
            float: left;
        }
        .login
        {
            margin: 0 auto;
            width: 980px;
            text-align: right;
            padding-right: 550px;
            padding-top: 10px;
        }
        .username
        {
            width: 270px;
            height: 33px;
            background: url(images/user.png) no-repeat;
            padding-left: 40px;
            color: #9cb5cd;
            font-size: 14px;
            text-indent: 12px;
            line-height:33px;
            
        }
        .pwd
        {
            width: 270px;
            height: 33px;
            padding-left: 40px;
            background: url(images/psd.png) no-repeat;
            color: #9cb5cd;
            font-size: 14px;
            text-indent: 12px;
               line-height:33px;
        }
        .btn
        {
            width: 311px;
            height: 39px;
            text-align: center;
            background-color: transparent;
            color: #000;
            font-size: 16px;
            font-weight: bold;
        }
        .btn-login-active
        {
            background: url(images/login.png) no-repeat;
        }
        .line
        {
            text-align: center;
            margin-top: 30px;
            padding: 0px;
        }
        .footer
        {
            text-align: center;
            font-size: 14px;
        }
          .style1
        {
            color: #FF0000;
        }
    </style>
    <script src="../js/md5.js" type="text/javascript"></script>
    <script src="inc/jquery-1.6.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        String.prototype.trim = function () {
            return this.replace(/(^\s*)(\s*$)/g, '');
        }
        function chkform() {   
     
            var form = document.getElementById('form');
            if (form.userId.value.trim() == "") {
                document.getElementById('errstr').innerText = '请输入管理员帐号';
                return false;
            }
            if (form.userPwd.value.trim() == "") {
                document.getElementById('errstr').innerText = '请输入登录密码';
                return false;
            }
           
            var md5Value = hex_md5(form.userPwd.value.trim());
            md5Value = md5Value.toUpperCase();
            if (md5Value == "") {
                alert('登录密码MD5加密出错！');
                return false;
            }
            form.userPwd.value = md5Value;
            return true;
        } //chkform

    </script>
    <script type="text/javascript">
        $(function () {
            $("#userId").focus(function () {
                var username = $(this).val();
                if (username == '输入账号') {
                    $(this).val('');
                }
            });

            $("#userId").focusout(function () {
                var username = $(this).val();
                if (username == '') {
                    $(this).val('输入账号');
                }
            });

        });
    </script>
</head>
<body onload="MM_preloadImages('images/button1.png')">
    <div class="banner">
        <div class="header">
            <div class="logo">
                <a href="http://www.hoolian.cn">
                    <img alt="" src="images/LOGO.png" border="0" id="Image1" /></a></div>
            <div class="clear">
            </div>
        </div>
        <div class="banben">
            <div class="banbencont">
                ZK service management system v1.0</div>
        </div>
        <div class="content">
            <div class="cont">
                <img alt="" src="images/content1.jpg" />
            </div>
        </div>
        <div class="login">
            <form runat="server" class="logink" id="form">
            <p>
                用户名：<input runat="server" type="text" id="userId" class="username" title="请输入用户名"
                    value="输入账号" style="border: 1px solid #999" /></p>
            <p>
                密&nbsp; 码：<input runat="server" type="password" id="userPwd" class="pwd" title="请输入密码"
                    value="" style="border: 1px solid #999;" /></p>
                   <p> <span class="style1" id="errstr">
                    <asp:Literal ID="litTips" runat="server"></asp:Literal></span>   </p>
            <p>
                <asp:Button runat="server" ID="subBt" Text="登录" class="btn btn-login-active" OnClientClick="return chkform();"
                    Style="border: 0px; cursor: pointer;" OnClick="subBt_Click" />
            </p>
            </form>
        </div>
        <div class="line">
            <img alt="" src="images/line.png" />
        </div>
        <div class="footer">
            <p>
                北京皓联教育科技有限公司 服务热线：010-88888888<br />
            </p>
            <p>
                Copyright © 2012~2015, haolian Education Beijing Information Technology Co. All
                rights reserved.</p>
        </div>
    </div>
</body>
</html>
