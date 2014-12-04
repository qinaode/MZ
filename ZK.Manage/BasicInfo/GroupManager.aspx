<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true" CodeBehind="GroupManager.aspx.cs" Inherits="ZK.Manage.BasicInfo.GroupManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
     <link rel="stylesheet" href="/css/userInfo.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >基础信息管理 >群组管理</div>
        <div class="searchform">   
            查找条件：
            <%--<label>
                <select id="select_Group" runat="server" onserverchange="select_Group_onserverchange">
                    <option>全部群组</option>
                    <option>群组ID</option>
                    <option>群组名称</option>
                </select>
            </label>--%>

            <asp:DropDownList ID="ddlGroups" runat="server" Width="90px" 
                onselectedindexchanged="ddlGroups_SelectedIndexChanged"  AutoPostBack="true">
                <asp:ListItem  Value="0">全部群组</asp:ListItem>
                <asp:ListItem  Value="1">群组ID</asp:ListItem>
                <asp:ListItem  Value="2">群组名称</asp:ListItem>
            </asp:DropDownList>
             <%--<div runat="server" id="Divdisplay" >--%>
                <asp:Label ID="lblShowTxt" runat="server" Text="群组ID:"></asp:Label>
                <asp:TextBox runat="server" ID="txtType"  size="16"></asp:TextBox>
            <%--</div> --%>              
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
        </div>
        
        <table width="100%" class="tableform text-center" border="0" cellspacing="0">
            <tr class="userList" align="left">
                 <td style="width: 65px;">
                    &nbsp;
                </td>
                <td>
                    群名称(群ID)&简介
                </td>
                <td>
                    人数
                </td>
                <td>
                    设置
                </td>
                <td>
                    拥有者
                </td>
                <td>
                    创建时间
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rptGroupList" runat="server" OnItemCommand="GroupListItem_Commond">
                <ItemTemplate>
                    <tr class="nodeList" align="left" style=" hover:background-color: #F8F7EF;">
                             <td >
                            <div id="Div8" align="center">
                                <img src='../image/group.gif' width="40px" height="40px" />
                            </div>
                        </td>
                        <td>
                            <div id="Div2">
                                <%#Eval("GROUPNAME")%>(<%#Eval("GROUPID")%>)                                   
                            </div>
                            <div id="Div11">
                                简介：<%#Eval("INTRODUCTION")%></div>
                        </td>
                        <td>
                            <div id="Div1">
                                群成员：<%#Eval("membercount")%></div>
                            </div>
                            <div id="Div6">
                                管理员：<%#Eval("managercount")%></div>
                        </td>
                        <td>
                            <div id="Div3">
                                <%#BindSeting(Eval("JOINSETTING"))%>
                            </div>
                        </td>
                        <td>
                            <div id="Div4">
                                拥有者：<%#Eval("OWNERID")%></div>
                        </td>
                           <td>
                            <div id="Div5">
                                创造者：<%#Eval("CREATORID")%></div>
                            <div id="Div7">
                                创建时间：<%#Eval("CREATETIME")%></div>
                        </td>
                        <td>                             
                            <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("GROUPID") %>'
                                runat="server"><img src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>                             
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pageInfo">              
            <span class="fr">
                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="pno" CurrentPageButtonClass="cpb"
                    runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                    PrevPageText="上一页" ShowInputBox="Never" OnPageChanged="AspNetPager1_PageChanged"
                    CustomInfoTextAlign="Left" LayoutType="Table" UrlPaging="True" CustomInfoHTML="共有%RecordCount% 条记录"
                    CustomInfoSectionWidth="10%" ShowPageIndexBox="Auto" PageSize="15">
                </webdiyer:AspNetPager>
            </span>
        </div>
        </form>
    </div>
</asp:Content>
