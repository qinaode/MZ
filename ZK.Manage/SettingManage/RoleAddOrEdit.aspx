<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAddOrEdit.aspx.cs" Inherits="ZK.Manage.SettingManage.RoleAddOrEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/moralchanel.css" rel="stylesheet" type="text/css" />
    <link href="../css/saveandcancel.css" rel="stylesheet" type="text/css" />
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
    <script type="text/javascript">//关闭对话框用的
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }      
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="form_title" id="div_Add" runat="server">

        <div class="searchform">           
            角色名称：
            <asp:TextBox ID="txtRoleName"  runat="server" value="" onfocus="" style="width:180px;"></asp:TextBox> 
             <span style="color: Red; margin-left: 1px">*</span>
            </br>
             角色描述：
            <asp:TextBox ID="txtRoleDesc"  runat="server" value="" onfocus="" style="width:180px;"></asp:TextBox>
            </br>
        </div>
        <div style="height:10px;">
        </div>

        <div style="float:right;" class="ui_buttons">
             <asp:Button class="ui_state_highlight" ID="btn_AddOrEdit" runat="server" Text="保存" OnClick="btnSave_Click" />
            <a id="A1" runat="server" onclick="closeWindow()">
                <asp:Button ID="Button1" runat="server" Text="取消" OnClientClick="closeWindow()" /></a>
        </div>
    </div>
    </form>
</body>
</html>
