<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeManagementEdit.aspx.cs" Inherits="ZK.Manage.SysNoticeManagement.NoticeManagementEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="/css/saveandcancel.css" />
    <link rel="stylesheet" href="/css/moralchanel.css" />
    <script src="../commonjs/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
    <script type="text/javascript">
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div class="searchform" >
             <div> 
                <table border='0' cellpadding='0' cellspacing='0'>
                        <tr>
                               <td style="width:65px; float:right;">
                                      标题：
                               </td>
                               <td>
                                        <asp:TextBox ID="txtTitle" runat="server" value="" onfocus="" Style="width: 285px;"></asp:TextBox>
                                        <span style="color: Red; margin-left: 5px">*</span>
                               </td>
                        </tr>
                        <tr>
                               <td valign="top">
                                        内容：
                               </td>
                               <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtConnent" runat="server" value=""  TextMode="MultiLine" Style="width: 285px; height:100px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <span style="color: Red; margin-left: 5px">*</span>
                                        </div>
                               </td>
                        </tr>
                        <tr>
                               <td>
                                       接收范围：
                               </td>
                               <td>
                                       <label>
                                            <select name="selectRange" runat="server" id="cmbRange" style="width: 290px;">
                                                <option value="0">内部账号+外部账号</option>
                                                <option  value="1">内部账号</option>
                                                <option  value="2">外部账号</option>
                                            </select>
                                      </label>
                               </td>
                        </tr>
                        <tr>
                               <td>
                                       指定账号：
                               </td>
                               <td>
                                        <asp:TextBox ID="txtUser" runat="server" value="" onfocus="" Style="width: 285px;"></asp:TextBox>
                               </td>
                        </tr>
                        <tr >
                                <td></td>
                               <td valign="middle">
                                   <div style="float: left;"><asp:CheckBox ID="checkedOnline" runat="server" /></div>
                                   <div style="float: left;"> <span  style="margin-left: 5px;">只发给在线用户</span></div>
                               </td>
                        </tr>
                        <tr>
                               <td>
                                    网页链接：
                               </td>
                               <td>
                                   <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">无网页链接</asp:ListItem>
                                        <asp:ListItem Value="1">外部网页链接</asp:ListItem>
                                        <asp:ListItem Value="2">自定义网页内容</asp:ListItem>
                                   </asp:RadioButtonList>
                                  <%-- <asp:RadioButton ID="radNo" runat="server" Checked="true" Text="无网页链接" />
                                   <asp:RadioButton ID="radOut" runat="server"  Text="外部网页链接"/>
                                   <asp:RadioButton ID="radDefine" runat="server" Text="自定义网页内容" />--%>
                               </td>
                        </tr>  
                </table>   
                </div>
                <div style="height: 10px;">
                </div>
                <div style="float: right;" class="ui_buttons">
            <asp:Button class="ui_state_highlight" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
            <a id="A1" runat="server" onclick="closeWindow()">
                <asp:Button ID="Button1" runat="server" Text="取消" /></a>              
        </div>        
        </div>
    </div>
    </form>
</body>
</html>
