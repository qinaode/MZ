<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHaveUser.aspx.cs" Inherits="ZK.Manage.BasicInfo.AddHaveUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../commonjs/jquery.1.9.0.min.js" type="text/javascript"></script>
    <%-- <script src="../js/CommonFUNC.js" type="text/javascript"></script>--%>
    <link href="../css/moralchanel.css" rel="stylesheet" type="text/css" />
    <link href="../css/saveandcancel.css" rel="stylesheet" type="text/css" />
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
    <script type="text/javascript">        //关闭对话框用的
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }

      
    </script>
</head>
<body>
    <form runat="server" id="Form1" name="queryfrom">
    <div >
        <div class="content">
            <div class="searchform">
                姓名：
                <asp:TextBox ID="txt_username" runat="server" value="" onfocus=""></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
            </div>
            <hr align="center" style="width: 100%;" />
            <div >
                <asp:CheckBoxList ID="cblRole" runat="server" Style="margin-left: 20px;" >
                </asp:CheckBoxList>
            </div>
               <hr align="center" style="width: 100%;" />
            <div style="height: 15px;">
            </div>
            <div style="float: right; margin-bottom: 20px;" class="ui_buttons">
                <asp:Button ID="btnSave" class="ui_state_highlight" runat="server" Text="保存" OnClick="btnSave_Click" />
                <a id="A1" runat="server" onclick="closeWindow()">
                    <asp:Button ID="Button1" runat="server" Text="取消" OnClientClick="closeWindow()" /></a>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
