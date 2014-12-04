<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ManageMyselfSendNotice</title>
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-jtemplates.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //截取用户的ID
            //var userID = "10024";
            GetNoticeListByUserID();
        });
//得到用户的通知列表通过用户ID
function GetNoticeListByUserID() {
    $.ajax({
        type: "post",
        url: "/SendNotice/GetNoticeListByUserID",
        datatype: "text/json",
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            if (json["DataList"] == "") {
                return;
            }
            $("#div_manageNotice").setTemplateURL("/pagetemples/ManageMyselfSendNotice.htm", null, null);
            $("#div_manageNotice").processTemplate(json["DataList"]);
            $("#div_manageNotice").css("visibility", "visible")
        }
    });
 }
 //删除事件
 function DelNotice(id) { 
 var strid=id.substr(4);
 $.ajax({
     type: "post",
     url: "/SendNotice/DelNotice",
     datatype: "text/json",
     data: { "id": strid },
     success: function (backdata) {
         if (backdata == "true" || backdata == "True") {
             alert("删除成功！");
             location.reload();
         }
         else {
             alert("删除失败！");
         }
     }
    });
 }
 //查看状态事件
    </script>
</head>

<body>
aaa
    <div id="div_manageNotice">
   
    </div>
</body>
</html>
