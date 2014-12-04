<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoList.aspx.cs" Inherits="ZK.Manage.BasicInfo.UserInfoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/common.css" />
    <link rel="stylesheet" href="/css/top.css" />
    <script src="../commonjs/jquery.1.9.0.min.js" type="text/javascript"></script>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            try {

                if (window != parent) {
                    var a = parent.document.getElementsByTagName("iframe");
                    for (var i = 0; i < a.length; i++) {
                        if (a[i].contentWindow == window) {
                            var h1 = 0, h2 = 0, d = document, dd = d.documentElement;
                            a[i].parentNode.style.height = a[i].offsetHeight + "px";

                            a[i].style.height = "10px";
                            if (dd && dd.scrollHeight) h1 = dd.scrollHeight;
                            if (d.body) h2 = d.body.scrollHeight;
                            var h = Math.max(h1, h2);
                            if (document.all) { h += 4; }
                            if (window.opera) { h += 1; }
                            a[i].style.height = a[i].parentNode.style.height = h + "px";
                        }
                    }
                }
            } catch (ex) { }

        });

        function AddOrEditUser(userid, flag, title) {
            $.dialog({ title: title, width: '500px', height: '720px', content: 'url:UserAddOrEdit.aspx?_a=' + Math.random() + '&flag=' + flag + '&userid=' + userid, max: false, min: false });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" class="tableform text-center" border="0" cellspacing="0">
            <tr class="userList" align="left">
                <td style="width: 65px;">
                    状态
                </td>
                <td>
                    用户ID
                </td>
                <td>
                    用户名
                </td>
                <td>
                    昵称
                </td>
                <td>
                    姓名
                </td>
                <td>
                    性别
                </td>
                <td>
                    年龄
                </td>
                <td>
                    手机
                </td>
                <td>
                    所属部门
                </td>
                <td>
                    职位
                </td>
                <td>
                    登录数
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rptUserList" runat="server" OnItemCommand="UserListItem_Commond">
                <ItemTemplate>
                    <tr class="nodeList" align="left" style="hover: background-color: #F8F7EF;">
                        <td height="28">
                        </td>
                        <td>
                            <div id="Div2">
                                <%#Eval("userid")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div1">
                                <%#Eval("username")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div3">
                                <%#Eval("nickname")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div4">
                                <%#Eval("actualname")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div5">
                                <%#Eval("sex")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div6">
                                <%#Eval("age") %>
                            </div>
                        </td>
                        <td>
                            <div id="Div7">
                                <%#Eval("mobile") %>
                            </div>
                        </td>
                        <td>
                            <div id="Div8">
                                <%#Eval("departname")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div9">
                                <%#Eval("jobtitle")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div10">
                                <%#Eval("logintimes") %>
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("UserID") %>'
                                runat="server"><img src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>
                            <asp:LinkButton ID="btn_Lock" Visible='<%#Eval("userlock").ToString()=="1"?false:true %>'
                                CommandName="CN_btn_Lock" CommandArgument='<%# Eval("UserID") %>' runat="server"><img src="../image/lock.gif" width="11" height="10" />锁定</asp:LinkButton>
                            <asp:LinkButton ID="Button1" Visible='<%#Eval("userlock").ToString()=="0"?false:true %>'
                                CommandName="CN_btn_UnLock" CommandArgument='<%# Eval("UserID") %>' runat="server"><img src="../image/unlock.gif" width="11" height="10" />解锁</asp:LinkButton>
                            <asp:LinkButton ID="Button3" CommandName="CN_btn_Update" CommandArgument='<%# Eval("UserID") %>'
                                runat="server"><img src="../image/mof.gif" width="11" height="10" /><span style=" margin-left:-5px;"> 修改</span></asp:LinkButton>
                            <%--<a runat="server" commandname="CN_btn_Update" commandargument='<%# Eval("UserID") %>'>修改
                            </a>--%>
                            <asp:LinkButton ID="Button2" CommandName="CN_btn_InitPWD" CommandArgument='<%# Eval("UserID") %>'
                                runat="server"><img src="../image/pwd.gif" width="11" height="10" />初始化密码</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pageInfo">
            <%-- <span class="fl">&nbsp;<input type="checkbox" onclick="setAllCheckboxState('items',this.checked)" />全选
                <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                <input type="button" value="锁定" />
                <asp:Button ID="btnInitPWD" runat="server" Text="登录密码初始化" OnClick="btnInitPWD_Click" />
              <input type="button" value="群发短信" />
                <input type="button" value="群发Email" /></span>--%>
            <span class="fr">
                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="pno" CurrentPageButtonClass="cpb"
                    runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                    PrevPageText="上一页" ShowInputBox="Never" OnPageChanged="AspNetPager1_PageChanged"
                    CustomInfoTextAlign="Left" LayoutType="Table" UrlPaging="True" CustomInfoHTML="共有%RecordCount% 条记录"
                    CustomInfoSectionWidth="10%" ShowPageIndexBox="Auto" PageSize="15">
                </webdiyer:AspNetPager>
            </span>
        </div>
    </div>
    </form>
</body>
</html>
