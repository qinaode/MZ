﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>系统登录</title>
<link href="/css/login.css" rel="stylesheet" rev="stylesheet" type="text/css" media="all" />
<link href="/css/demo.css" rel="stylesheet" rev="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="/js/jquery1.42.min.js"></script>
<script type="text/javascript" src="/js/jquery.SuperSlide.js"></script>
<script type="text/javascript" src="/js/Validform_v5.3.2_min.js"></script>

<script>
    $(function () {

        $(".i-text").focus(function () {
            $(this).addClass('h-light');
        });

        $(".i-text").focusout(function () {
            $(this).removeClass('h-light');
        });

        $("#username").focus(function () {
            var username = $(this).val();
            if (username == '输入账号') {
                $(this).val('');
            }
        });

        $("#username").focusout(function () {
            var username = $(this).val();
            if (username == '') {
                $(this).val('输入账号');
            }
        });


        $("#password").focus(function () {
            var username = $(this).val();
            if (username == '输入密码') {
                $(this).val('');
            }
        });





        $(".registerform").Validform({
            tiptype: function (msg, o, cssctl) {
                var objtip = $(".error-box");
                cssctl(objtip, o.type);
                objtip.text(msg);
            },
            ajaxPost: false
        });

    });




</script>


</head>
<body>
<div class="header1" style=" text-align:left; margin:20px;">
  <img alt="logo" src="/imagesN/logo.jpg" height=90px>

</div>
<div class="banner">
<div class="login-aside">
  <div id="o-box-up"></div>
  <div id="o-box-down"  style="table-layout:fixed;">
   <div class="error-box"></div>
   <form class="registerform"action="/account/CheckLogin/">
   <div class="fm-item">
   <input type="hidden" name="pu" value="<%= ViewData["pu"] %>"  />
	   <label for="logonId" class="form-label">用户名：</label>
	   <input type="text" value="输入账号" maxlength="100" id="username" name="username" class="i-text"  datatype="s1-18" errormsg="用户名至少6个字符,最多18个字符！"  >    
       <div class="ui-form-explain"></div>
  </div>
  <div class="fm-item">
	   <label for="logonId" class="form-label">登陆密码：</label>
	   <input type="password" value="" maxlength="100" id="password" name="password" class="i-text" datatype="*1-16" nullmsg="请设置密码！" errormsg="密码范围在6~16位之间！">    
       <div class="ui-form-explain"></div>
  </div>
  <div class="fm-item">
	   <label for="logonId" class="form-label"></label>
	   <input type="submit" value="" tabindex="4" id="send-btn" class="btn-login"> 
       <div class="ui-form-explain"></div>
  </div>
  </form>
   </div>
</div>
	<div class="bd">
		<ul>
			<li style="background:url(/themes/theme-pic1.jpg) #CCE1F3 center 0 no-repeat;"><div style=" margin-top:50px; margin-left:550px; font-family:微软雅黑;font-size:35px; height:40px; line-height:40px;">智客知识管理系统</div></li>
			<li style="background:url(/themes/theme-pic2.jpg) #BCE0FF center 0 no-repeat;"><div style=" margin-top:50px; margin-left:550px; font-family:微软雅黑;font-size:35px; height:40px; line-height:40px;">智客知识管理系统</div></li>
		</ul>
	</div>

	<div class="hd"><ul></ul></div>
</div>
<script type="text/javascript">    jQuery(".banner").slide({ titCell: ".hd ul", mainCell: ".bd ul", effect: "fold", autoPlay: true, autoPage: true, trigger: "click" });</script>
<div class="banner-shadow"></div>
<div class="footer">
</div>
</body>
</html>
