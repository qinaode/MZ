<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgManagerEdit.aspx.cs" ValidateRequest="false"
    Inherits="ZK.Manage.SystemMsg.MsgManagerEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="/css/saveandcancel.css" />
    <link rel="stylesheet" href="/css/moralchanel.css" />
    <link rel="stylesheet" href="/ckeditor/contents.css" />
    <script src="../commonjs/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <script type="text/javascript">
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }

        function Show(id) {  
            if (id == "Radio1") {
                Link1.style.display = "none";
                Link2.style.display = "none";  
            }
            if (id == "Radio2") {
//                $("#Link1").css("visiblity", "visible");
                Link1.style.display = "";
                  Link2.style.display = "none";
            }
            if (id == "Radio3") {
                Link1.style.display = "none";
                Link2.style.display = "";   
            }
        }         
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="searchform">
        <div>
            <table border='0' cellpadding='0' cellspacing='0'>
                <tr>
                    <td style="width: 65px; float: right;">
                        标题：
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" value="" onfocus="" Style="width: 287px;"></asp:TextBox>
                        <span style="color: Red; margin-left: 1px">*</span><span style="color: Green; margin-left: 1px">最大长度为50个字符</span>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        内容：
                    </td>
                    <td>
                        <div style="float: left;">
                            <asp:TextBox ID="txtConnent" runat="server" value="" TextMode="MultiLine" Style="width: 285px;
                                height: 100px;"></asp:TextBox>
                        </div>
                        <div style="float: left">
                            <span style="color: Red; margin-left: 5px">*</span><span style="color: Green; margin-left: 1px">最大长度为1024个字符</span>
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
                                <option value="-1">内部账号+外部账号</option>
                                <option value="1">内部账号</option>
                                <option value="0">外部账号</option>
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
                        <span style="color: Green; margin-left: 5px">请输入单个用户ID</span>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="checkedOnline" runat="server" />
                        <span style="margin-top: -5px;">只发给在线用户</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        网页链接：
                    </td>
                    <td>
                        <input id="Radio1" runat="server"  type="radio" value="0" name="rad" onclick="Show(this.id)"/>无网页链接
                        <input id="Radio2" runat="server" type="radio" value="1" name="rad" onclick="Show(this.id)" />外部网页链接
                        <input id="Radio3" runat="server" type="radio" value="2" name="rad" onclick="Show(this.id)" />自定义网页内容
<%--
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">无网页链接</asp:ListItem>
                            <asp:ListItem Value="1">外部网页链接</asp:ListItem>
                            <asp:ListItem Value="2">自定义网页内容</asp:ListItem>
                        </asp:RadioButtonList>--%>
                    </td>
                </tr>
                <tr id="Link1" runat="server" style="display:none;">
                    <td>
                        链接地址：
                    </td>
                    <td>
                        <asp:TextBox  ID="txtLinkAddress" runat="server" value="" onfocus="" Style="width: 285px;"></asp:TextBox>
                    </td>
                </tr>
                <tr id="Link2" runat="server" style="display:none;" >
                    <td valign="top">
                        网页编辑：
                    </td>
                    <td>
                        <textarea class="ckeditor" cols="80" runat="server" rows="10" id="txthtml" name="htmlcode" style="width: 500px;"></textarea>
                    </td>
                </tr>
                <tr style="height: 10px;">
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="ui_buttons" style="float: left;">
                        <asp:Button class="ui_state_highlight" ID="Button1" runat="server" Text="发送" OnClick="btnSave_Click" />
                        <a id="A1" runat="server" onclick="closeWindow()">
                            <asp:Button ID="Button4" runat="server" Text="取消" /></a>
                              <asp:Button ID="btnSendMessage" runat="server" Text="发送即时通信消息" 
                            onclick="btnSendMessage_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 10px;">
        </div>

        <%--<div style=" left:80px;" class="ui_buttons">
                    <asp:Button class="ui_state_highlight" ID="Button2" runat="server" Text="保存" OnClick="btnSave_Click" />
                    <a id="A2" runat="server" onclick="closeWindow()">
                    <asp:Button ID="Button3" runat="server" Text="取消" /></a>              
                </div>--%>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel> --%>
    </form>
</body>
</html>
