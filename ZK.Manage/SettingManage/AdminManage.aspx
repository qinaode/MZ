<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="AdminManage.aspx.cs" Inherits="ZK.Manage.SettingManage.AdminManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <%--    <script src="../commonjs/lhgdialog.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/jscript">
        function submitfrom(key) {
            document.getElementById("orderKey").value = key;
            this.queryfrom.submit();

            return false;
        }
        //全选
        function setAllCheckboxState(name, state) {
            var elms = document.getElementsByName(name);
            for (var i = 0; i < elms.length; i++) {
                elms[i].checked = state;
            }
        }
        function AddOrEditAdmin(adminname, flag, title) {
            $.dialog({ title: title, width: '400px', height: '215px', content: 'url:AdminAddOrEdit.aspx?_a=' + Math.random() + '&flag=' + flag + '&adminname=' + adminname, max: false, min: false });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <div class="pagePath">
            首页 >系统配置管理 >系统管理员管理</div>
        <div class="searchform" align="left">
            &nbsp;&nbsp;&nbsp;<input type="button" id="btn_AddNew" onclick="AddOrEditAdmin('','0','添加管理员')"
                value="添加管理员" />&nbsp;&nbsp;
        </div>
        <table width="100%" class="tableform text-center" border="0" cellspacing="0">
            <tr class="userList" align="left">
               
                <td>
                    管理员名
                </td>
                <td>
                    描述
                </td>
                <td>
                    是否登录
                </td>
                <td>
                    最近登录时间
                </td>
                <td>
                    操作
                </td>
            </tr>
            <form runat="server" id="queryfrom" name="queryfrom">
            <asp:Repeater ID="rptUserList" runat="server" OnItemCommand="UserListItem_Commond">
                <ItemTemplate>
                    <tr align="left" align="left" class="nodeList" style="hover: background-color: #F8F7EF;">
                       
                        <td>
                            <div id="Div2">
                                <%#Eval("adminname")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div3">
                                <%#Eval("description")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div4">
                                <%#Eval("logined")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div6">
                                <%#Eval("lastlogintime") %>
                            </div>
                        </td>
                        <td>
                           
                            <asp:LinkButton  ID="Button2" CommandName="CN_btn_UpdatePWD" CommandArgument='<%# Eval("adminname") %>'
                                runat="server"><img src="../image/pwd.gif" width="11" height="10" />修改密码</asp:LinkButton>
                            <asp:LinkButton   ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("adminname") %>'
                                runat="server"> <img src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>
                                
                           
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </form>
        </table>
    </div>
</asp:Content>
