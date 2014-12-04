<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="ManagementPpt.aspx.cs" Inherits="ZK.Manage.MoralManagement.ManagementPpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/checkbox.js" type="text/javascript"></script>
    <style type="text/css">
        .style3
        {
            width: 90px;
            height: 40px;
            text-align: right;
            font-size: 12px;
        }
        td
        {
            border-bottom: 1px #e4e2da dashed;
            min-height: 30px;
            line-height: 150%;
            color: #333333;
            padding: 3px 5px 3px 10px;
        }
        select
        {
            border: solid 1px #C3DFEA;
        }
    </style>
    <script type="text/javascript">
        //删除选中的项 现仅限于teachchannel 和 用该分页者--%>
        function DeleteCheckedItems() {
            //  记录被选择的项的ID-_
            var checkedlist = new Array();

            $("input[name='checkbox_1']").each(function (i, item) {
                if ($(item).attr("checked")) {
                    checkedlist.push($(item).attr("id"));
                }
            });

            window.location = "/MoralManagement/ManagementPpt.aspx?checkedlist=" + checkedlist;
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
        .style4
        {
            height: 32px;
        }
        .cl1
        {
            background-color: #8E8E8E;
        }
        .cl2
        {
            background-color: #FBFBFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form id="queryfrom" name="queryfrom" runat="server">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >德育频道管理 >幻灯片管理
        </div>
        <div class="searchform">
            图片名称：<asp:TextBox ID="txtMoralPicName" runat="server" Width="291px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
        </div>
      
        <table width="100%" class="tableform text-center" border="0" cellspacing="0" style="font-size: 12">
            <tr class="userList" align="left">
                <td>
                    选择
                </td>
                <td>
                    图片名称
                </td>
                <td>
                    路径
                </td>
                <td>
                    图片状态
                </td>
                <td>
                    图片链接
                </td>
                <td>
                    显示排序
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="actionItem_Commond">
                <ItemTemplate>
                    <%--  <tr align="left" class="nodeList <%#Eval("pic_state").ToString()=="1"?"cl1":"cl2"%>">--%>
                    <tr align="left" class="nodeList">
                        <td height="32">
                            <input type="checkbox" name="checkbox_1" id="<%#Eval("pic_id")%>" onclick="CheckOne()" />
                        </td>
                        <td>
                            <%#Eval("pic_name")%>
                        </td>
                        <td>
                            <%#Eval("pic_path")%>
                        </td>
                        <td>
                            <%#Eval("pic_state").ToString()=="1"?"未启用":"启用"%>
                        </td>
                        <td>
                            <%#Eval("pic_link")%>
                        </td>
                        <td>
                            <%#Eval("pic_order")%>
                        </td>
                        <td>
                            <div id="action">
                                <asp:LinkButton ID="btn_MoveUp" runat="server" CommandName="CN_btn_MoveUp" CommandArgument='<%# Eval("pic_id") %>'>
                                <img src="../image/up2.gif" width="11" height="10" />上移</asp:LinkButton>
                                <asp:LinkButton ID="btn_MoveDown" runat="server" CommandName="CN_btn_MoveDown" CommandArgument='<%# Eval("pic_id") %>'>
                                <img src="../image/down.gif" width="11" height="10" /><span style="margin-left: -3px;"> 下移</span></asp:LinkButton>
                                <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("pic_id") %>'
                                    runat="server"><img src="../image/del3.gif" width="9" height="9" />删除</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" CommandName="CN_btn_State" CommandArgument='<%# Eval("pic_id") %>'
                                    runat="server"><img src="../image/del_uc.gif" width="9" height="9" />  <%#Eval("pic_state").ToString()=="1"?"启用":"未启用"%></asp:LinkButton>
                                <%-- <asp:LinkButton ID="LinkButton2" Visible='<%#Eval("pic_state")=="0"?false:true %>' CommandName="CN_btn_State" CommandArgument='<%# Eval("pic_id") %>'
                                    runat="server"><img src="../image/del_uc.gif" width="9" height="9" />未启用</asp:LinkButton>--%>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pageInfo">
            <span style="margin-right: 20px;">&nbsp;<input type="checkbox" id="hhhh" onclick="SelectAll()" />全选
                <%-- <asp:Button ID="delList" runat="server" Text="批量删除2" 
                OnClick="delList_Click" />--%>
                <input type="button" value="批量删除" onclick="DeleteCheckedItems()" />
                <span class="fr">
                    <webdiyer:AspNetPager ID="AspNetPager1" CssClass="pno" CurrentPageButtonClass="cpb"
                        runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                        PrevPageText="上一页" ShowInputBox="Never" OnPageChanged="AspNetPager1_PageChanged"
                        CustomInfoTextAlign="Left" LayoutType="Table" UrlPaging="True" CustomInfoHTML="共有%RecordCount% 条记录"
                        CustomInfoSectionWidth="10%" ShowPageIndexBox="Auto">
                    </webdiyer:AspNetPager>
                </span>
              
            </span>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #d2eefb;
            width: 98%; margin: 0px auto 8px; margin-top: 10px;" width="100%" id="tablesetting">
            <tbody>
             
                <tr class="line">
                    <td class="style3">
                        幻灯片图片：
                    </td>
                    <td colspan="2">
                        <%--<input name="moralpptImag" type="file" size="50" runat="server" id="File1" />--%>
                        <input name="moralpptImag" class="input" runat="server" id="txt_Moralppt" size="30"
                            type="file" />
                    </td>
                </tr>
                <tr class="line">
                    <td class="style3">
                        图片链接：
                    </td>
                    <td style="width: 400px;">
                        <%--<input name="filestorepath" class="input" runat="server" id="txtImgURL" size="50"  type="text"/>--%>
                        <asp:TextBox ID="txtAddLink" runat="server" Width="390px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnImglink" runat="server" Text="添加" OnClick="btnImglink_Click" Style="height: 21px" />
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
    </div>
</asp:Content>
