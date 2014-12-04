<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="MsgManagerNew.aspx.cs" Inherits="ZK.Manage.SystemMsg.MsgManagerNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <style type="text/css">
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
        .table  tr
        {   
            border-left-style:none;            
        }
    </style>
    <style type="text/css">
        .b1, .b2, .b3, .b4, .b1b, .b2b, .b3b, .b4b, .b
        {
            display: block;
            overflow: hidden;
        }
        .b1, .b2, .b3, .b1b, .b2b, .b3b
        {
            height: 1px;
        }
        .b2, .b3, .b4, .b2b, .b3b, .b4b, .b
        {
            border-left: 1px solid #ffefa6;
            border-right: 1px solid #ffefa6;
        }
        .b1, .b1b
        {
            margin: 0 3px;
            background: #ffefa6;
        }
        .b2, .b2b
        {
            margin: 0 2px;
            border-width: 2px;
        }
        .b3, .b3b
        {
            margin: 0 1px;
        }
        .b4, .b4b
        {
            height: 2px;
            margin: 0 1px;
        }
        .d1
        {
            padding: 0 8px 0 8px;
            background: #fff2b7;
            font-size: 13px;
            line-height: 18px;
            color: #808080;
        }
        .d1 ul
        {
            margin: 0 8px 0 18px;
            padding: 0;
        }
        .d1 li
        {
            margin: 2px 0 2px 0;
            list-style-type: disc;
            list-style-position: outside;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >系统公告管理>公告管理</div>
        <div class="b d1">
            <ul>
                <li style="margin-left: 8px;">系统消息发布后不能进行编辑，如发现填写的内容不正确，请及时删除，然后重新发布。</li>
                <li style="margin-left: 8px;"><span style="color: #2fb900">注意：客户端只接收最近30天内发布的系统消息。</span></li>
            </ul>
        </div>
        <div class="searchform" style="font-family: 微软雅黑; font-size: 14px;">
            <span style="margin-left: 13px;">公告名称：</span>
            <asp:TextBox ID="txtTitle" runat="server" value="" size="16"></asp:TextBox>
            <asp:Button ID="btnSearch" Style="font-family: 微软雅黑; font-size: 9;" runat="server"
                Text="搜索" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="GridView1" CssClass="table" runat="server" Width="100%" OnPageIndexChanging="gvwDesignationName_PageIndexChanging"
            AllowPaging="True" PageSize="15" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
            CellPadding="4" ForeColor="#333333" GridLines="None" PageIndex="0" SelectedIndex="-1">
            <Columns>
                <asp:BoundField DataField="" HeaderText="" ReadOnly="True" HeaderStyle-HorizontalAlign="Left"
                    SortExpression="TITLE">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="10px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <img alt="" src="../image/sysmsg.gif" width="40px" height="40px" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="SID" HeaderText="" Visible="false" HeaderStyle-HorizontalAlign="Left"
                    ReadOnly="True" SortExpression="SID">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TITLE" HeaderText="标题" ReadOnly="True" HeaderStyle-HorizontalAlign="Left"
                    SortExpression="TITLE">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="CONTENT" HeaderText="内容" ReadOnly="True" HeaderStyle-HorizontalAlign="Left"
                    SortExpression="CONTENT">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="链接">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%#GetLink(Eval("LINK"))%>' BorderWidth="0"
                            Width="20px" ReadOnly="true" BorderColor="#F8F7EF" CssClass="GridTextBox"></asp:TextBox>
                    </ItemTemplate>
                     <ItemStyle Width="3%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="发送给">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSendTo" runat="server" Text='<%#GetSendToUser(Eval("SENDTO"),Eval("SID"))%>' BorderWidth="0"
                             ReadOnly="true" BorderColor="#F8F7EF" CssClass="GridTextBox"></asp:TextBox>
                    </ItemTemplate>
                     <ItemStyle Width="15%" />
                </asp:TemplateField>
             
                <asp:BoundField DataField="SENDTIME" HeaderText="时间" ReadOnly="True" HeaderStyle-HorizontalAlign="Left"
                    SortExpression="SENDTIME">
                     <ItemStyle Width="10%" />
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("SID") %>'
                            runat="server"><img alt="" src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>
                    </ItemTemplate>
                     <ItemStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="14px" BackColor="#F8F7EF" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle HorizontalAlign="Right"  Font-Size="14px" Height="45px" VerticalAlign="Middle" />
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
                    CommandName="Page" Text="转到" />  &nbsp; &nbsp;&nbsp;
            </PagerTemplate>
            <RowStyle BorderColor="#F7DAF0" BorderWidth="1px" Height="28px" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#F8F7EF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        </form>
    </div>
</asp:Content>
