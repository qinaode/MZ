<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminResourceSendEdit.aspx.cs" Inherits="ZK.Manage.AdministrativeManagement.AdminResourceSendEdit" %>

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
                               <td style="width:80px; float:right;">
                                      资源名称：
                               </td>
                               <td>
                                   <asp:Label ID="lblName" runat="server" Text="XXX名称"></asp:Label>                                        
                               </td>
                        </tr>                       
                        <tr>
                               <td>
                                       所属专题：
                               </td>
                               <td>
                                       <label>
                                            <select name="cmbSpecialTopic" runat="server" id="cmbSpecialTopic" style="width: 290px;">
                                              
                                            </select>
                                      </label>
                               </td>
                        </tr>
                        <tr>
                               <td>
                                       个性化名称：
                               </td>
                               <td>
                                        <asp:TextBox ID="txtSpecialName" runat="server" value="" onfocus="" Style="width: 285px;"></asp:TextBox>
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
