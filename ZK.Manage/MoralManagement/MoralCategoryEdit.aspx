<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoralCategoryEdit.aspx.cs"
    Inherits="ZK.Manage.MoralManagement.MoralEdit" %>

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
        <div class="searchform">
            德育分类名称：
            <asp:TextBox ID="txtCategoryName" runat="server" value="" onfocus="" Style="width: 260px;"></asp:TextBox>
            </br> 德育分类描述：
            <%-- <textarea name="" cols="" rows=""></textarea>--%>
            <asp:TextBox ID="txtCategoryDesc" runat="server" value="" onfocus="" Style="width: 260px;"></asp:TextBox>
            </br> 所属德育分类：
            <label>
                <select name="select0" runat="server" id="cmbMoralCategory" style="width: 265px;">
                    <option>-请选择-</option>
                </select>
            </label>
        </div>
        <div style="height: 20px;">
        </div>
        <div style="float: right;" class="ui_buttons">
            <asp:Button class="ui_state_highlight" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
            <a id="A1" runat="server" onclick="closeWindow()">
                <asp:Button ID="Button1" runat="server" Text="取消" /></a>
            <%--<input type="button" ID="btnCancel" runat="server" value="取消" onclick="closeWindow()" />OnClick="btnCancel_Click"--%>
        </div>
    </div>
    </form>
</body>
</html>
