<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ZK.Manage.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
    智客业务管理系统-首页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <style type="text/css">
        .style3
        {
            width: 14%;
            height: 40px;
            text-align: right;
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <form id="Form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #d2eefb;
        width: 98%; margin: 0px auto 8px;" width="100%" id="tablesetting">
        <tbody>
            <tr class="line">
                <td class="style3">
                    网站标题：
                </td>
                <td width="75%">
                    <label runat="server" id="txt_webtitle">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    版权：
                </td>
                <td>
                    <%--                    <input name="copyright" class="input" runat="server" id="" value="版权所有： Copyright © 2012-2016"
                        size="100" type="text">--%>
                    <label runat="server" id="txt_copyright">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    网站ICP备案号：
                </td>
                <td>
                    <%--  <input name="recordnum" class="input" runat="server" id="" value=""
                        size="30" type="text" >--%>
                    <label runat="server" id="txt_recordnum">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    网站LOGO：
                </td>
                <td style="padding-left: 20px;">
                    <asp:Image ID="img_Logo" runat="server" Width="256" Height="97" />
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    是否挂载转码模块：
                </td>
                <td>
                    <asp:RadioButton ID="rbtn_Open" runat="server" Checked="true" GroupName="1" Enabled="false" />开启&nbsp;&nbsp;<asp:RadioButton
                        ID="rbtn_Close" GroupName="1" runat="server" Enabled="false" />关闭
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    教学页每页文件显示数量：
                </td>
                <td>
                    <%--  <input name="teachnum" class="input" runat="server" id="" size="10" type="text">--%>
                    <label runat="server" id="txt_teachnum">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    德育页每页文件显示数量：
                </td>
                <td>
                    <%--   <input name="teachnum" class="input" runat="server" id="" size="10" type="text">--%>
                    <label runat="server" id="txt_Moralnum">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    行政页每页文件显示数量：
                </td>
                <td>
                    <%--     <input name="administrationnum" class="input" runat="server" id=""
                        size="10" type="text">--%>
                    <label runat="server" id="txt_administrationnum">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    搜索页每页文件显示数量：
                </td>
                <td>
                    <%--  <input name="searchnum" class="input" runat="server" id="" size="10"
                        type="text">--%>
                    <label runat="server" id="txt_searchnum">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    用户默认空间：
                </td>
                <td>
                    <%--   <input name="defaultspace" class="input" runat="server" id="" size="10"
                        type="text">--%>
                    <label runat="server" id="txt_defaultspace">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    文件存储位置：
                </td>
                <td>
                    <%--        <input name="filestorepath" class="input" runat="server" id="" size="70"
                        type="text">--%>
                    <label runat="server" id="txt_filestorepath">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    网站默认时间显示格式：
                </td>
                <td>
                    <%--  <input name="time_format" class="input" runat="server" id="" type="text">--%>
                    <label runat="server" id="txt_time_format">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <!-- 资源管理 -->
                <td class="style3">
                    文档：
                </td>
                <td>
                    <label runat="server" id="txt_Doc">
                    </label>
                    <%--        <input type="text" class="input" runat="server" id="" name="ext[1]">--%>
                </td>
            </tr>
            <tr class="line">
                <!-- 资源管理 -->
                <td class="style3">
                    视频：
                </td>
                <td>
                    <%--          <input type="text" class="input" runat="server" id="" name="ext[2]">--%>
                    <label runat="server" id="txt_Video">
                    </label>
                </td>
            </tr>
            <tr class="line">
                <!-- 资源管理 -->
                <td class="style3">
                    图片：
                </td>
                <td>
                    <%--   <input type="text" class="input" runat="server" id="" name="ext[3]">--%>
                    <label runat="server" id="txt_Photo">
                    </label>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</asp:Content>
