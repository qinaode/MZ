<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="AdministartorResourceManage.aspx.cs" Inherits="ZK.Manage.AdministrativeManagement.AdministartorResourceManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
    行政资源管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link href="../css/userInfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/lhgdialog/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script src="../js/adminresourceManage.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >行政管理 >行政资源管理</div>
        <div class="pageInfo" align="left">
            名称：<input type="text" id="text_AdminResourceName" />&nbsp;&nbsp;&nbsp; 行政分类：<input
                type="text" id="txt_channelGroupName" />
            &nbsp;&nbsp;<input type="button" id="btn_Search" value="搜索" onclick="GetAdminResourceListByCondition()" />
        </div>
        <div id="div_ForAllContent">
            <div id="div_ResourceContent" align="right">
            </div>
            <br />
            <div>
                <table width="95%">
                    <tr>
                        <td width="30%">
                            <%-- <div>
                                <input type="checkbox" />
                                &nbsp;&nbsp; 全选&nbsp;&nbsp;<input type="button" value="批量删除" onclick="DeleteCheckedItems_TeachResource()" /></div>--%>
                        </td>
                        <td align="right">
                            <div id="div_Pager">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
