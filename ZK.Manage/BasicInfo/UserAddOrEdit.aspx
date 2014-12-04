<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAddOrEdit.aspx.cs"
    Inherits="ZK.Manage.BasicInfo.UserAddOrEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/saveandcancel.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        .td1
        {
            width: 70px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="form_title">
        <table style="border: 1px;">
            <tr>
                <td class="td1">
                    用户ID：
                </td>
                <td class="td2">
                    <%--<input runat="server" style="width: 260px;" type="text" id="" />--%>
                    <asp:TextBox ID="txtUserID" Style="width: 260px;" runat="server" ReadOnly="true"></asp:TextBox>
                    <span style="color: #ff0000;">&nbsp;*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;<span style="color: #2fb900;">非"0"开头的9位以内数字</span>
                </td>
            </tr>
            <tr>
                <td class="td1">
                    用户名：
                </td>
                <td class="td2">                       
                    <asp:TextBox ID="txtUserName" runat="server" Style="width: 260px;"></asp:TextBox>
                    <span style="color: #ff0000;">&nbsp;*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;<span style="color: #2fb900;">最大长度为20个字符</span>
                </td>
            </tr>
            <tr id="tr_Pwd" runat="server">
                <td class="td1">
                    登录密码：
                </td>
                <td class="td2"> 
                <%--<input runat="server" style="width: 260px;" type="Password" value="123456" id="txt_Pwd" />--%>                       
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"  Style="width: 260px;" ></asp:TextBox>
                </td>
            </tr>
            <tr id="tr1_Ok" runat="server">
                <td class="td1">
                    确认密码：
                </td>
                <td class="td2">
                    <%--<input runat="server" style="width: 260px;" type="Password" value="123456" id="txt_PwdOK" />--%>
                    <asp:TextBox ID="txtPwdOK" runat="server" TextMode="Password" Style="width: 260px;"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr_pwdtext" runat="server">
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;<span style="color: #2fb900">密码为空时，自动设置默认密码(123456)</span>
                </td>
            </tr>
            <tr id="tr_block1" runat="server">
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr id="tr_block2" runat="server">
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td1">
                    &nbsp;
                </td>
                <td class="td2" style="height: 40px;">
                    <input runat="server" type="checkbox" class="checkbox" id="check_Comm" value="0" />允许与外部帐号沟通
                </td>
            </tr>
            <tr>
                <td class="td1">
                    可用空间：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 158px;" type="text" id="txt_space" />
                    （单位：M）
                </td>
            </tr>
            <tr>
                <td class="td1">
                    昵称：
                </td>
                <td class="td2">
                    <%--  <input runat="server" style="width: 260px;" type="text" id="txt_nickname" />--%>
                    <asp:TextBox ID="txtnickname" runat="server" Style="width: 260px;"></asp:TextBox>
                    <span style="color: #ff0000;">&nbsp;*</span>
                </td>
            </tr>
            <tr>
                <td class="td1">
                    真实姓名：
                </td>
                <td class="td2">
                    <%--<input runat="server" style="width: 260px;" type="text" id="txt_actualname" />--%>
                    <asp:TextBox ID="txtactualname" runat="server" Style="width: 260px;"></asp:TextBox>
                    <span style="color: #ff0000;">&nbsp;*</span>
                </td>
            </tr>
            <tr>
                <td class="td1">
                    性别：
                </td>
                <td class="td2">
                    <select name="sex" style="width: 264px;" runat="server" id="select_Sex">
                        <option value="-1">请选择</option>
                        <option value="1">男</option>
                        <option value="0">女</option>
                    </select>
                     <span style="color: #ff0000;">&nbsp;*</span>
                </td>
            </tr>
            <tr>
                <td class="td1">
                    年龄：
                </td>
                <td class="td2">
                    <select name="age" style="width: 264px;" runat="server" id="select_Age">
                        <option value="-1">请选择</option>
                    </select>
                   
                </td>
            </tr>
            <tr id="trdisplayid" runat="server">
                <td class="td1">
                    所属部门：
                </td>
                <td class="td2">
                    <select name="department" style="width: 264px;" runat="server" id="select_departments">
                        <option value="-1">请选择</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="td1">
                    职位名称：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_jobtitle" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    工作编号：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_jobnumber" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    联系地址：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_address" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    电话：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_telephone" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    手机：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_mobile" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    传真：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_Fax" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    QQ：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_qq" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    MSN：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_msn" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    Email：
                </td>
                <td class="td2">
                    <input runat="server" style="width: 260px;" type="text" id="txt_email" />
                </td>
            </tr>
            <tr>
                <td class="td1">
                    个人主页：
                </td>
                <td class="td2" style="height: 40px;">
                    <input runat="server" style="width: 260px;" type="text" id="txt_homepage" />
                </td>
            </tr>
        </table>
    </div>
    <div class="ui_buttons">
        <asp:Button ID="btn_AddOrEdit" class="ui_state_highlight" runat="server" Text="添加"
            OnClick="btn_AddOrEdit_Click" />
        &nbsp;
        <%--<asp:Button ID="btn_Quit" runat="server" Text="取消" onclick="btn_Quit_Click" />--%>
        <asp:Button ID="btn_Quit" runat="server" OnClientClick="Cancel()" Text="取消" /></div>
    </form>
</body>
</html>
