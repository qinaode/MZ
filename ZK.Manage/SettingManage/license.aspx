<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="license.aspx.cs" Inherits="ZK.Manage.SettingManage.license" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../commonjs/jquery.1.9.0.min.js" type="text/javascript"></script>
    <%-- <script src="../js/CommonFUNC.js" type="text/javascript"></script>--%>
    <link href="../css/moralchanel.css" rel="stylesheet" type="text/css" />
    <link href="../css/saveandcancel.css" rel="stylesheet" type="text/css" />
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script type="text/javascript">         //关闭对话框用的
         function closeWindow() {
             var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
             
             api.close();
         }

         function DeleteCheckedItems() {
             //  记录被选择的项的ID-_
             var checkedlist = new Array();
      //       alert("XXX");
             $("input[name='checkbox_1']").each(function (i, item) {
                 if ($(item).attr("checked")) {
                     checkedlist.push($(item).attr("id"));
                 }
             });

             window.location = "/SettingManage/license.aspx?checkedlist=" + checkedlist;
         } 
    </script>
</head>
<body>
    <form runat="server" id="Form1" name="queryfrom">
   <%-- <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="RoleToUser" 
        DataTextField="roleName" DataValueField="roleID">
        <asp:ListItem></asp:ListItem>
            </asp:CheckBoxList>
        <asp:SqlDataSource ID="RoleToUser" runat="server" 
        ConnectionString="Data Source=119.255.49.153;Initial Catalog=ZK_Data;Persist Security Info=True;User ID=zkUser;Password=zk@123" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="SELECT [roleID], [roleName] FROM [ZK_RoleList] ORDER BY [roleASC]">
    </asp:SqlDataSource>--%>
          <div class="content">
        <div class="searchform">
            角色：
            <asp:TextBox ID="txt_username" runat="server" value="" onfocus=""></asp:TextBox>
          
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
           </div>
        
        <hr align="center" style="width:100%;"/>
    <div >
    <asp:CheckBoxList ID="cblRole" runat="server" style="margin-left:20px;" >
    </asp:CheckBoxList>
      </div>
               <hr align="center" style="width:100%;"/>
             <%--<div style="margin-left:15px;">
            <span style="margin-right: 20px;">&nbsp;<input type="checkbox" onclick="CheckAll()"/>全选</span>
          </div>--%>
           <%--<div>
                             <span style="margin-right:20px;" class="fl">&nbsp;<input style="margin-left:18px;" type="checkbox" onclick="CheckAll()" />全选
            </span>
                              </div>--%>
          <div style="height:15px;"></div>
             <div style="float:right; margin-bottom:20px; padding-bottom:20px;" class="ui_buttons">
            <asp:Button ID="btnSave"  class="ui_state_highlight" runat="server" Text="保存" onclick="btnSave_Click" />
        <a id="A1" runat="server" onclick="closeWindow()">
            <asp:Button ID="Button1" runat="server" Text="取消" OnClientClick="closeWindow()" /></a>
    </div>
        </div>
    </form>
</body>
</html>
