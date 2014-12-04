<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LessonMagEdit.aspx.cs"
    Inherits="ZK.Manage.TeachChannelManage.LessonMagEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        function Cancel() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象              
            api.close();
        }              
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="searchform" id="div1" runat="server">
            <div>
                <table border='0' cellpadding='0' cellspacing='0'>
                    <tr>
                        <td style="width: 65px; float: right;">
                            教材名称：
                        </td>
                        <td>
                            <asp:Label ID="lblCourseName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 65px; float: right;">
                            年级名称：
                        </td>
                        <td>
                            <asp:Label ID="lblGradeName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            版本名称：
                        </td>
                        <td>
                            <asp:Label ID="lblEditionName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblLessonName" runat="server" Text=" 课程名称："></asp:Label>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:TextBox ID="txt_LessonName" runat="server" Style="width: 287px;"></asp:TextBox>
                                <span style="color: Red; margin-left: 1px">*</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblLessonDesc" runat="server" Text="课程描述："></asp:Label>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:TextBox ID="txtLessonDesc" runat="server" TextMode="MultiLine" Style="width: 285px;
                                    height: 80px;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="trUnit" runat="server">
                        <td>
                            所属单元：
                        </td>
                        <td>
                            <div style="float: left">
                                <label>
                                    <select name="selectMode" runat="server" id="selectMode"  style="width: 290px;">
                                        <option value="-1">请选择</option>
                                    </select>
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr id="trMB" runat="server">
                        <td valign="top">
                            <asp:Label ID="lblMB" runat="server" Text="教学目标："></asp:Label>
                        </td>
                        <td  >
                            <div style="float: left; ">
                                <asp:TextBox ID="txt_LessonMB" runat="server"  TextMode="MultiLine" Style="width: 285px;
                                    height: 50px;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="trND" runat="server">
                        <td valign="top">
                            <asp:Label ID="lblND" runat="server" Text="教学难点："></asp:Label>
                        </td>
                        <td >
                            <div style="float: left;  margin-top:5px;">
                                <asp:TextBox ID="txt_LessonND" runat="server"  TextMode="MultiLine" Style="width: 285px;
                                    height: 50px;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="trZD" runat="server">
                        <td valign="top">
                            <asp:Label ID="lblZD" runat="server" Text="教学重点："></asp:Label>
                        </td>
                        <td >
                            <div style="float: left; margin-top:5px;">
                                <asp:TextBox ID="txt_LessonZD" runat="server"  TextMode="MultiLine" Style="width: 285px;
                                    height: 50px;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 15px;">
            </div>
            <div style="float: right;" class="ui_buttons">
                <asp:Button class="ui_state_highlight" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                <a id="A1" runat="server" onclick="Cancel()">
                    <asp:Button ID="Button1" runat="server" Text="取消" /></a>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
