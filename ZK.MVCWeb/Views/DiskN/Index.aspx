<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=ViewData["webtitle"].ToString() %>--网盘首页</title>
    <script src="/Content/easyui/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Content/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Content/easyui/extEasyUI.js" type="text/javascript"></script>
    <script src="/Content/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="/Content/easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Content/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
  
    <style type="text/css">
        *{padding:0;margin:0;}
        a{text-decoration:none;color:#000;}
        ul{list-style-type:none;}
        #top{height:60px;background:#41b400}
        #top .info
         { 
		     margin-top:5px;
             height:40px;
             line-height:40px;
             position:absolute;
             right:25px;
             top:5px;
             color:#fff;
          }
          #top .info a{color:#fff;}
		  #top .info a.touxiang{ background:url(/content/images/touxiang.png) no-repeat; padding:8px 0 10px 35px;}
        .logo{ margin-top:5px; margin-left:25px;display:inline-block;border:none;}
        
        #sidebar
          {
            width:198px;
            float:left;
            background:#f0f0f0;
            color: #676767;
            font-size: 14px;
            height:500px;
            border-right:solid 1px #ccc;
            /*z-index:100;*/
           }
        #sidebar ul.menu1
             {
                 width:198px;
                 text-align:left;
                 position:absolute;
                 top:10px;
                 left:0px;
                 
             }
             #sidebar ul.menu1 li
             {
                 width:198px;
                 line-height:35px;
             }
             #sidebar ul.menu1 li a
             {
                 font-size:14px;
                 color:#676767;
                 text-indent:75px;
                 display:block;
                 height:35px;
                 width:198px;
				 padding:6px 0;
                 line-height:35px;
				 border-top:1px solid #f0f0f0;
				 border-bottom:1px solid #f0f0f0;
             }
             #sidebar ul.menu1 li a:hover,#sidebar ul.menu1 li a.current
             {
                cursor:pointer;
                background:#e4f2db;
				border-top:1px solid #cbe0bf;
				border-bottom:1px solid #cbe0bf;
             }
             #sidebar ul.menu1 li .icon_nav
			 {
				 background:url(/content/images/icon_nav.png) no-repeat 0 0;
				 height:20px;
				 left:50px;
				 margin:-9px 0 0;
				 overflow:hidden;
				 position:absolute;
				 width:22px;
			 }
			  #sidebar ul.menu1 li .icon_ziliao 
			 {
				 background-position:0 0;
				 top:22px;
			 }
			 #sidebar ul.menu1 li .icon_shouziliao 
			 {
				 background-position:0 -30px;
				 top:70px;
			 }
			 #sidebar ul.menu1 li .icon_jiaoziliao 
			 {
				 background-position:0 -60px;
				 top:120px;
			 }
			 #sidebar ul.menu1 li .icon_gonggongziliao 
			 {
				 background-position:0 -90px;
				 top:168px;
			 }
			 #sidebar ul.menu1 li .icon_huishouzhan 
			 {
				 background-position:0 -120px;
				 top:216px;
			 }
    </style>
    <script type="text/javascript">
        $(function () {

            $("#sidebar > .menu1 > li > a").click(function () {
                $("#sidebar > .menu1 > li > a").removeClass("current");
                $(this).addClass("current");
            });
            $("#sidebar > .menu1 > li > a").first().click();
        });
    </script>
</head>
<body class="easyui-layout">
    
   <div id="top" region="north" border="false">
       <img src="/Content/images/logo_wangpan.png" class="logo" />
       <div class="info">
            <span>
                   <a  class="touxiang">欢迎使用智客网盘,<%=ViewData["username"]%></a>
            </span>
           |<a href="/account/ChangePWD" target="_parent"> 修改密码 </a>|<a href="/account/logout" target="_parent"> 退出</a></div>
   </div>
   <div id="sidebar" region="west" border="false">
       <ul class="menu1">
            <li><a href="/diskn/mydoc" target="main" class="current"><i class="icon_nav icon_ziliao"></i><span>我的资料</span></a></li>
            <li><a href="/diskn/wyszl_folder" target="main"><i class="icon_nav icon_shouziliao"></i><span>我要收资料</span></a></li>
            <li><a href="/diskn/wyjzl_folder" target="main"><i class="icon_nav icon_jiaoziliao"></i><span>我要交资料</span></a></li>
            <li><a href="/diskn/publicdoc" target="main"><i class="icon_nav icon_gonggongziliao"></i><span>公共资料库</span></a></li> 
            <li><a href="/diskn/recycle" target="main"><i class="icon_nav icon_huishouzhan"></i><span>回收站</span></a></li> 
            <li><a href="/home/index"><i class="icon_nav icon_ziliao"></i><span>知识库</span></a></li>
        </ul>
   </div>
   <div region="center" border="false">
      <iframe name="main" src="/diskn/mydoc" style="width:100%;height:98%;overflow: hidden" frameborder="0"></iframe>
   </div>
</body>
</html>
