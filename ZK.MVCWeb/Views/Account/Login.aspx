<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%=ViewData["webtitle"].ToString() %>--登录</title>
    <script type="text/javascript" src="/js/jquery1.42.min.js"></script>
    <script type="text/javascript" src="/js/jquery.SuperSlide.js"></script>
    <script type="text/javascript" src="/js/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript">
        $(function () {


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
        //检查登录并跳转
        function CheckLoginAndTurn(pu) {
            var $username = $("#username").val();
            var $pwd = $("#password").val();
            $.ajax({
                url: "/Account/CheckLoginN",
                type: "Post",
                data: { "pu": pu, "username": $username, "password": $pwd },
                datatype: "text",
                success: function (backdata) {
                    if (backdata == "true") {
                        if (pu == "") {
                            window.location = "/DiskN/CheckUser?username=" + $username;
                        }
                        else {
                            window.location = "/DiskN/CheckUser?username=" + $username;
                        }
                    }
                    else if (backdata == "unregister") {
                        alert("激活信息有误，请刷新页面");
                        return;
                    }
                    else {
                        alert("账号或密码错误");
                        return;
                    }
                }
            });
        }
    </script>
<link href="/CSS/mz/login.css" rel="stylesheet" />
<link href="/CSS/mz/comment.css" rel="stylesheet" />
</head>
<body onkeydown="if(event.keyCode==13) CheckLoginAndTurn('<%= ViewData["pu"] %>')">
<div class="loginpage">
  <div class="loginheader">
  </div>
     <div class="error-box"></div>
     <form class="registerform">
  <div class="logincontent">
     <input type="hidden" name="pu" value="<%= ViewData["pu"] %>"  />
      <div class="logincont">
          <ul>
             <li> <label class="l-login">用户名：</label><input type="text" name="username" id="username" class="txtbox"  title="请输入用户名"  value="输入账号"/></li>
             <li><label class="l-login">密&nbsp;&nbsp;码：</label><input type="password" name="password" id="password" class="txtbox" title="请输入密码" /></li>
            <label for="logonId" class="form-label"></label>
       <li class="login_button"><input type="button" value="登   陆" tabindex="4" 
               style="border-style: none; border-color: inherit; border-width: 0px; cursor: pointer; width: 411px; height: 64px;" 
               value="登&nbsp;&nbsp;&nbsp;&nbsp;录" 
               onclick="CheckLoginAndTurn('<%= ViewData["pu"] %>')"> </li>
       <div class="ui-form-explain"></div> 
          </ul>
      </div>
  </div>
  </form>
  
  <div class="loginfooter">
     <p>版权所有@北京皓联教育科技有限公司&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系电话：010-67867798</p>
  </div>
</div>
</body>
</html>
