<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="OrgAndUserSetting.aspx.cs" Inherits="ZK.Manage.BasicInfo.OrgAndUserSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <style type="text/css">
        .treeView
        {
            overflow: auto;
            margin-top: 25px;
            margin-left: 15px;
            height: 100%;
            border-width: 1px;
            font-size: 16px;
        }
        .info
        {
            width: 96%;
        }
        .style1
        {
            width: 10%;
        }
        
        .GridTextBox
        {
            background-color: transparent;
            border: none;
            width: 100%;
            font-size: 12px;
            font-family: 微软雅黑;
        }
    </style>
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
        var id;
        var Pwd;
        var Del;
        var IsLock;
        var depId;
        var title;
        var flag;
        var userid;

        function AddOrEditUser(depId, flag, title) {

            $.dialog({ title: title, width: '450px', height: '680px', content: 'url:UserAddOrEdit.aspx?IsDepOrUser=dep&_a=' + Math.random() + '&flag=' + flag + '&depId=' + depId, lock: true, max: false, min: false });
        }
        function EditUser(userid, flag, title, depId) {

            $.dialog({ title: title, width: '450px', height: '680px', content: 'url:UserAddOrEdit.aspx?IsDepOrUser=dep&_a=' + Math.random() + '&flag=' + flag + '&depId=' + depId + '&userid=' + userid, lock: true, max: false, min: false });
        }
        //编辑修改或添加
        function AddOrEditRole(id, title) {

            $.dialog({ title: title, width: '300px', height: '310px', content: 'url:/../SettingManage/license.aspx?_a=' + Math.random() + '&userid=' + id, lock: true, max: false, min: false });
        }
        function AddHaveUser(flag, title, userid) {

            //            $.dialog({ title: title, width: '300px', height: '310px', content: 'url:AddHaveUser.aspx?_a=' + Math.random() + '&depName=' + flag + '&depId=' + userid, max: false, min: false });
            $.dialog({ title: title, width: '710px', height: '640px', content: 'url:HavedOtherUser.aspx?_a=' + Math.random() + '&depName=' + flag + '&depId=' + userid, lock: true, max: false, min: false });
        }
       
    </script>
    <%--<script type="text/javascript">
        //分页功能
        var PagerDivID = "div_Pager";
        var pagesize = 15;
        var lists = new Array();

        var depId = "";

        var strName = "";
        var struserid = "";

        $(function () {

            //GetDataForPaging(1, PagerDivID,depId);
        });

        //页面加载时绑定列表
        //加载分页数据列表
        function GetDataForPaging(pageindex, PagerDivID, depId) {

            $.ajax({
                type: "Post",
                url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                data: { "Flag": "GetDataTreeNodesPaging", "strName": strName, "struserid": struserid, "depId": depId, "PageIndex": pageindex, "PageSize": pagesize },
                datatype: "text/json",
                success: function (backdata) {
                    if (backdata == "" || backdata == "[]") {
                        return;
                    };

                    var tempjson = $.parseJSON(backdata);
                    if (tempjson["DataList"] == "") {
                        $("#div_ForAllContent").css("visibility", "hidden");
                        return;
                    }

                    $("#div_ResourceContent").setTemplateURL("../pagetemples/basicInfo/OrgAndUser.htm", null, null);
                    $("#div_ResourceContent").processTemplate(tempjson["DataList"]);
                    $("#div_ForAllContent").css("visibility", "visible");
                    CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
                }
            });
        }


        //绑定条件查询列表
        function GetSearchListByCondition() {
            strName = $("#txt_username").val();
            struserid = $("#txt_userid").val();

            GetDataForPaging(1, PagerDivID, depId);

        }


    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >基础信息管理 >组织用户配置</div>
        <table border='0' cellpadding='0' cellspacing='0' width="100%">
            <tr>
                <td id="LeftProduct" valign="top" class="style1" style="width: 200px;">
                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>--%>
                    <asp:TreeView ID="TreeProduct" runat="server" ExpandDepth="1" CssClass="treeView"
                        Font-Names="微软雅黑" Font-Size="14px" OnSelectedNodeChanged="TreeProduct_SelectedNodeChanged"
                        Width="100%" ImageSet="Simple">
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <NodeStyle Font-Names="微软雅黑" Font-Size="14px" ForeColor="Black" HorizontalPadding="0px"
                            Height="27px" NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Bold="True" Font-Italic="False" Font-Underline="True" ForeColor="#5555DD"
                            HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
                <td valign="top">
                    <div class="content">
                        <div class="searchform">
                            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>--%>
                            <span style="margin-left: 15px;">姓名：</span>
                            <asp:TextBox ID="txt_username" runat="server" value=""></asp:TextBox>
                            <%--<input type="text" id="txt_username" />--%>
                            账号：
                            <%--<input type="text" id="txt_userid" />--%>
                            <asp:TextBox ID="txt_userid" runat="server" value=""></asp:TextBox>
                            <%--<input type="button" id="btnSearch" onclick="GetSearchListByCondition()" value="搜索" />--%>
                            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnHaveUser" OnClick="btnHaveUser_Click" Text="从当前用户中选择" />
                            <%--<input type="button" id="btnHaveUser" onclick="AddHaveUser('','0','添加用户')"   value="从当前用户中选择" />--%>
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" />
                            <%--<input type="button" id="btn_AddNew" onclick="AddOrEditUser('','0','添加用户')"   value="新增" />--%>
                            <%--  </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TreeProduct" EventName="SelectedNodeChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>
                        <div>
                            <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" OnPageIndexChanging="gvwDesignationName_PageIndexChanging"
                                AllowPaging="True" PageSize="15" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound"
                                OnRowCreated="GridView1_RowCreated" PageIndex="0" SelectedIndex="-1" >
                                <Columns>
                                    <asp:BoundField DataField="" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;状态" HeaderStyle-HorizontalAlign="Left" ReadOnly="True"
                                        SortExpression="">
                                        <%--<ItemStyle Width="10%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="USERID" HeaderText="用户ID" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="USERID">
                                        <%--<ItemStyle Width="8%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="USERNAME" HeaderText="用户名" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="USERNAME">
                                        <%--<ItemStyle Width="10%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NICKNAME" HeaderText="昵称" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="NICKNAME">
                                        <%--<ItemStyle Width="10%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ACTUALNAME" HeaderText="姓名" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="ACTUALNAME">
                                        <%--<ItemStyle Width="10%" />--%>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="性别">
                                        <%--<ItemStyle Width="6%" />--%>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%#GetSex(Eval("SEX"))%>' BorderWidth="0" Width="40px"
                                                ReadOnly="true" BorderColor="#F8F7EF" CssClass="GridTextBox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AGE" HeaderText="年龄" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="AGE">
                                        <%--<ItemStyle Width="5%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MOBILE" HeaderText="手机" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="MOBILE">
                                        <%--<ItemStyle Width="10%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JOBTITLE" HeaderText="职位" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="JOBTITLE">
                                        <%--<ItemStyle Width="10%" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOGINTIMES" HeaderText="登录数" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="LOGINTIMES">
                                        <%--<ItemStyle Width="4%" />--%>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_Lock" Visible='<%#Eval("userlock").ToString()=="1"?false:true %>'
                                                CommandName="CN_btn_Lock" CommandArgument='<%# Eval("UserID") %>' runat="server"><img src="../image/lock.gif" width="11" height="10" />锁定</asp:LinkButton>
                                            <asp:LinkButton ID="Button1" Visible='<%#Eval("userlock").ToString()=="0"?false:true %>'
                                                CommandName="CN_btn_UnLock" CommandArgument='<%# Eval("UserID") %>' runat="server"><img src="../image/unlock.gif" width="11" height="10" />解锁</asp:LinkButton>
                                            <asp:LinkButton ID="Button3" CommandName="CN_btn_Update" CommandArgument='<%# Eval("UserID") %>'
                                                runat="server"><img src="../image/mof.gif" width="11" height="10" /><span style=" margin-left:-5px;"> 修改</span></asp:LinkButton>
                                            <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("UserID") %>'
                                                runat="server"><img src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton1" CommandName="CN_btn_Role" CommandArgument='<%# Eval("UserID") %>'
                                                runat="server"><img src="../image/mof.gif" width="11" height="10" /><span style=" margin-left:-5px;"> 授权角色</span></asp:LinkButton>
                                            <asp:LinkButton ID="Button2" CommandName="CN_btn_InitPWD" CommandArgument='<%#Eval("UserID") %>'
                                                runat="server"><img src="../image/pwd.gif" width="11" height="10" />初始化密码</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="14px" BackColor="#F8F7EF" />
                                <PagerStyle HorizontalAlign="Right" Font-Size="14px" Height="45px" VerticalAlign="Middle" />
                                <PagerTemplate>
                                    当前第:
                                    <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                                    页/共:
                                    <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                                    页
                                    <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page">首页</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                                        CommandName="Page">上一页</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page">下一页</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page">尾页</asp:LinkButton>
                                    第
                                    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='' />
                                    页
                                    <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                                        CommandName="Page" Text="转到" />
                                </PagerTemplate>
                                <RowStyle BorderColor="#F7DAF0" BorderStyle="Solid" BorderWidth="1px" 
                                    Height="28px" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#F8F7EF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <%--  </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TreeProduct" EventName="SelectedNodeChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        </form>
    </div>
</asp:Content>
