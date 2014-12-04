<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="NoticeManagementList.aspx.cs" Inherits="ZK.Manage.SysNoticeManagement.NoticeManagementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script language="javascript" type="text/jscript">
        function noticeMagAdd() {
            //添加  cancel: true, , ok: function () { Save_UpdateOrAdd(); }
            $.dialog({ title: '添加公告', width: '390px', height: '330px', content: 'url:NoticeManagementEdit.aspx?ty=add&t=' + new Date().getTime().toString(), max: false, min: false });

        }
        function noticeMagEdit(id) {
            $.dialog({ title: '编辑公告', width: '390px', height: '330px', content: 'url:NoticeManagementEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + id, max: false, min: false });

        }

//        //分页功能
//        var PagerDivID = "div_Pager";
//        var pagesize = 10;
//        var lists = new Array();
//        $(function () {            
//            GetDataForPaging(1, PagerDivID);
//        });
//        function GetDataForPaging(pageindex, PagerDivID) {           
//            $.ajax({
//                type: "Post",
//                url: "../ashx/OrgManage.ashx?_a=" + Math.random(),
//                data: { "Flag": "GetListPaging",                     
//                    "PageIndex": pageindex,
//                    "PageSize": pagesize
//                },
//                datatype: "text/json",
//                success: function (backdata) {
//                    if (backdata == "" || backdata == "[]") {
//                        return;
//                    };
//                    var tempjson = $.parseJSON(backdata);                      
//                    
//                    CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
//                }
//            });

        }
    </script>
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
        <form action="NoticeManagementList.aspx" runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >系统公告管理>公告管理</div>
        <div class="b d1">
            <ul>
                <li>系统消息发布后不能进行编辑，如发现填写的内容不正确，请及时删除，然后重新发布。</li>
                <li><span style="color: #2fb900">注意：客户端只接收最近30天内发布的系统消息。</span></li>
            </ul>
        </div>
        <div class="searchform">
            公告名称：
            <asp:TextBox ID="txtTitle" runat="server" value="" size="16"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
            <input type="button" value="添加" onclick="noticeMagAdd();" />
            <%--<asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" />--%>
        </div>
        <table width="100%" class="tableform text-center" border="0" cellspacing="0">
            <tr class="userInfo" align="left">
                <td style="display: none;">
                    id
                </td>
                <td>
                    标题&内容
                </td>
                <td>
                    链接
                </td>
                <td>
                    发送给
                </td>
                <td>
                    发布时间
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rptNoticeList" runat="server" OnItemCommand="UserListItem_Commond">
                <ItemTemplate>
                    <tr align="left">
                        <td style="display: none;">
                            <div id="Div1">
                                <%#Eval("SID")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div2">
                                <%#Eval("TITLE")%>
                            </div>
                            <div id="Div9">
                                <%#Eval("CONTENT")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div4">
                                <%#Eval("LINK")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div8">
                                <%#Eval("FORUSERTYPE")%>
                            </div>
                            <div id="Div5">
                                <%#Eval("SENDTO")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div6">
                                <%#Eval("SENDTIME")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div7">
                                
                                <input id="btnEdit" type="button" value="修改" onclick="noticeMagEdit( <%#Eval("SID")%>);" />
                               <asp:Button ID="btn_Delete" runat="server" Text="删除"  CommandName="CN_btn_Delete" CommandArgument='<%# Eval("SID") %>' />
                            </div>
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
                    CustomInfoSectionWidth="10%" ShowPageIndexBox="Auto">
                </webdiyer:AspNetPager>                  
            </span>
            
        </div>
        <div>
        </div>
        </form>
    </div>
</asp:Content>
