<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <script src="../../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../../js/pagering_1.01.js" type="text/javascript"></script>
    <script src="../../Scripts/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../../js/NoticeManage.js" type="text/javascript"></script>
    <link href="../../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <link href="../../css/NoticeManage.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
//        $(function () {

//    });
       
    </script>
 <title>公告管理</title>  
</head>
<body>

  <div class="center">
    <div id='div_ItemSelect' class="div_ItemSelect">
    <input type="hidden" id="userID" value='<%=ViewData["userID"] %>' />
    </div>
    <div id="div_sendnot"style="display:none">
    <iframe name='ifNoticeList' src='/SendNotice/SendNotice?userID=<%=ViewData["userID"] %>' width='780' height='800' frameborder="0" id="ifNoticeList" ></iframe>
    </div>
    <div class="div_not" id="div_not">
    <div id="div_NoticeList" class="div_NoticeList">
    </div>
     <div id="div_Pager">
      </div>
      </div><fieldset style="border:0; width:770px; height:210px; float:left;" >  <legend id="msg">公告的内容是：</legend>
      <div id="div_NoticeContent" class="div_NoticeContent">
        </div></fieldset>
    
    </div>
</body>
</html>



