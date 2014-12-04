<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAddOrEdit.aspx.cs"
    Inherits="ZK.Manage.SettingManage.AdminAddOrEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/saveandcancel.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }      
    </script>
    <style type="text/css">
        .td1
        {
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="form_title" id="div_Add" runat="server">
        <table style="border: 1px;">
            <tr>
                <td class="td1">
                    管理员账号:
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_AdminName" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    描述:
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_description" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td1">
                    登录密码:
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="password" id="txt_Pwd" />
                </td>
            </tr>
            <tr>
                <td>
                    确认密码：
                </td>
                <td>
                    <input type="password" id="txt_Pwd2" runat="server" style="width: 260px;" />
                </td>
            </tr>
             <tr>
                <td class="td1">
                    &nbsp;
                </td>
                <td class="td2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="form_title" id="div_InitPWD" runat="server">
        <table style="border: 1px;">
            <tr>
                <td class="td1">
                    旧密码:
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="password" id="txt_OldPwd" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td1">
                    新密码：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="password" id="txt_NewPwd" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    &nbsp;
                </td>
                <td class="td2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td1">
                    重复新密码：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="password" id="txt_NewPwd2" />
                </td>
            </tr>
             <tr>
                <td class="td1">
                    &nbsp;
                </td>
                <td class="td2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="ui_buttons">
        <asp:Button ID="btn_AddOrEdit" class="ui_state_highlight" runat="server" Text="保存"
            OnClick="btn_AddOrEdit_Click" />
        &nbsp;<asp:Button ID="btn_Quit" runat="server" OnClientClick="closeWindow()" Text="取消" /></div>
    </form>
</body>
</html>
