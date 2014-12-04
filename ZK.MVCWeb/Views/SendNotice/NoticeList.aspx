<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%--<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>NoticeList</title>
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <script type="text/ecmascript">
        function ShowNoticeContent(id) {
            //alert("222");
             var strid = id.substr(4); //not_
             $.ajax({
                 type: "post",
                 url: "/SendNotice/GetNoticeItem",
                 datatype: "text/json",
                 data: { "strid": strid },
                 success: function (backdata) {
                     if (backdata == "") {
                         return;
                     }
                     else {

                         $("#NoticeContent").append(backdata);
                         alert($("#NoticeContent").html());
                     }
                 }
             });
  }
    </script>
</head>
<body>
    <div id="div_NoticeList">
    <% System.Data.DataSet dsNotice = (System.Data.DataSet)ViewData["noticeList"];
       Repeater1.DataSource = dsNotice;
       Repeater1.DataBind();
       %>
   <asp:Repeater runat="server" ID="Repeater1">
                    <HeaderTemplate>
                       <table>
                       <tr>
                           <th> 标题</th>
                           <th>发送人</th>
                           <th>发送时间</th>
                           <th>是否查看</th>
                       </tr>
                    </HeaderTemplate>
                    <ItemTemplate >
                     <tr id='not_<%# Eval("SID")%>' onclick='ShowNoticeContent(this.id)'>
                            <td><%# Eval("TITLE")%> </td>
                            <td><%# Eval("UserID")%></td>
                            <td><%# Eval("SENDTIME")%> </td>
                            <td><%# Eval("isSee")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
               <%-- 分页--%>
        <%-- <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
          CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            PrevPageText="上一页" ShowCustomInfoSection="Right" Width="60%" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，第页显示%PageSize%条"
           PageIndexBoxStyle="width:19px" AlwaysShow="true">
         </webdiyer:AspNetPager>--%>
    
    </div>
</body>
</html>
