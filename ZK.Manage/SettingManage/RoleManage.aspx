<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="RoleManage.aspx.cs" Inherits="ZK.Manage.SettingManage.RoleManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link href="../css/userInfo.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <script src="../commonjs/lhgdialog/lhgcore.min.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script src="../js/checkbox.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //全选
        function setAllCheckboxState(name, state) {
            var elms = document.getElementsByName(name);
            for (var i = 0; i < elms.length; i++) {
                elms[i].checked = state;
            }
        }

        function DeleteCheckedItems() {
            var checkedlist = new Array();
            $("input[name='checkbox_1']").each(function (i, item) {

                if ($(item).attr("checked")) {
                    checkedlist.push($(item).attr("id"));
                }
            });
            window.location = "/SettingManage/RoleManage.aspx?checkedlist=" + checkedlist;
        }
        //编辑修改或添加
        function AddOrEditRole(roleid, flag, title) {

            $.dialog({ title: title, width: '300px', height: '130px', content: 'url:RoleAddOrEdit.aspx?_a=' + Math.random() + '&flag=' + flag + '&roleid=' + roleid, max: false, min: false });
        }
        var id;
        var roleflag;
        function UserList(id, roleflag) {
          
            var roleid = id.substr(5);

            $.dialog({ title: "角色授权人员", width: '300px', height: '310px', content: 'url:/BasicInfo/AddHaveUser.aspx?_a=' + Math.random() + '&roleid=' + roleid + '&roleuser=' + roleflag, max: false, min: false });
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="Form1" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >基础信息管理 >角色管理</div>
        <div class="searchform">
            <span style="margin-left: 15px;">角色名称：</span>
            <asp:TextBox ID="txtSpecialTypeName" runat="server" value="" onfocus=""></asp:TextBox>
            <%--<asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />--%>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
            <input type="button" id="btn_AddNew" onclick="AddOrEditRole('','0','添加角色')" value="添加" />
        </div>
        <table width="100%" class="tableform text-center" border="0" cellspacing="0">
            <tr class="userList" align="left">
                <td style="width: 15px;">
                </td>
                 <td >
                   选择
                </td>
                <td>
                    角色名称
                </td>
                <td>
                    角色描述
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="repeater1" runat="server" OnItemCommand="roleListItem_Commond">
                <ItemTemplate>
                    <tr align="left" class="nodeList" style="hover: background-color: #F8F7EF;">
                        <td style="width: 15px;">
                        </td>
                        <td>
                            <input type="checkbox" name="checkbox_1" id="<%#Eval("roleID")%>" onclick="CheckOne()" />
                        </td>
                        <td>
                            <div id="Div2">
                                <%#Eval("roleName")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div4">
                                <%#Eval("roleDesc")%>
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="btn_MoveUp" runat="server" CommandName="CN_btn_MoveUp" CommandArgument='<%# Eval("roleID") %>'>
                                <img src="../image/up2.gif" width="11" height="10" />上移</asp:LinkButton>
                            <asp:LinkButton ID="btn_MoveDown" runat="server" CommandName="CN_btn_MoveDown" CommandArgument='<%# Eval("roleID") %>'>
                                <img src="../image/down.gif" width="11" height="10" /><span style="margin-left: -3px;"> 下移</span></asp:LinkButton>
                            <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("roleID") %>'
                                runat="server"><img src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>
                            <%--      <asp:LinkButton ID="Button3" CommandName="CN_btn_Update" CommandArgument='<%# Eval("roleID") %>'
                                runat="server"><img src="../image/mof.gif" width="11" height="10" /><span style=" margin-left:-5px;"> 修改</span></asp:LinkButton> --%>
                            <a id="<%# Eval("roleID") %>" onclick="AddOrEditRole(this.id,'1','修改角色')">
                                <img src="../../image/mof.gif" width="9" height="9" />修改</a>
                                 <a id="user_<%# Eval("roleID") %>" onclick="UserList(this.id,'roleuser');">
                                <img src="../../image/mof.gif" width="9" height="9" />人员列表</a>
                            <%-- <asp:LinkButton ID="LinkButton1" CommandName="CN_btn_License" CommandArgument='<%# Eval("roleID") %>'
                                runat="server"><img src="" width="11" height="10" /><span style=" margin-left:-5px;"> 授权</span></asp:LinkButton>          --%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pageInfo">
            <div>
               <%-- <span style="margin-right: 10px;" class="fl">&nbsp;<input  style="margin-left:15px;"
                    type="checkbox" onclick="CheckAll()" />全选 </span>--%>
                     <span style="margin-right: 10px;" class="fl">&nbsp;<input  style="margin-left:15px;"
                    type="checkbox" id="hhhh" onclick="SelectAll()" />全选 </span>
                <input type="button" value="批量删除" onclick="DeleteCheckedItems()" />
            </div>
            <%--  <span style="margin-right: 20px;">&nbsp;<input type="checkbox" onclick="CheckAll()" />全选</span>
            <input type="button" value="批量删除" onclick="DeleteCheckedItems()" />--%>
        </div>
        </form>
    </div>
</asp:Content>
