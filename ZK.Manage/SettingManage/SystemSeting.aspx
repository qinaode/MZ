<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="SystemSeting.aspx.cs" Inherits="ZK.Manage.SettingManage.SystemSeting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
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
        .input
        {
            width: 329px;
        }
        .style4
        {
            width: 14%;
            height: 100px;
            text-align: right;
        }
        .style5
        {
            height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <form runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #d2eefb;
        width: 98%; margin: 0px auto 8px;" width="100%" id="tablesetting">
        <tbody>
            <tr class="line">
                <td class="style3">
                    网站标题：
                </td>
                <td width="75%">
                    <input name="webtitle" class="input" runat="server" id="txt_webtitle" size="50" type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style4">
                    版权：
                </td>
                <td class="style5">
                   <%-- <input name="copyright" class="input" runat="server" id="txt_copyright" value="版权所有： Copyright © 2012-2016"
                        size="100" type="text" >--%>
                       <textarea  cols="500"  name="copyright" class="input" runat="server" rows="5" id="text_copyright" value="">版权所有： Copyright © 2012-2016</textarea> 

                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    网站ICP备案号：
                </td>
                <td>
                    <input name="recordnum" class="input" runat="server" id="txt_recordnum" value=""
                        size="30" type="text">
                </td>
            </tr>
            <tr class="line">
                <td colspan="2" style="padding-left: 200px;">
                    <asp:Image ID="img_Logo" runat="server" Width="256" Height="97" />
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    网站LOGO：
                </td>
                <td>
                    <input name="weblogo" class="input" runat="server" id="txt_weblogo" size="30" type="file">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    是否挂载转码模块：
                </td>
                <td>
                    <asp:RadioButton ID="rbtn_Open" runat="server" Checked="true" GroupName="1" />开启&nbsp;&nbsp;<asp:RadioButton
                        ID="rbtn_Close" GroupName="1" runat="server" />关闭
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    教学页每页文件显示数量：
                </td>
                <td>
                    <input name="teachnum" class="input" runat="server" id="txt_teachnum" size="10" type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    德育页每页文件显示数量：
                </td>
                <td>
                    <input name="teachnum" class="input" runat="server" id="txt_Moralnum" size="10" type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    行政页每页文件显示数量：
                </td>
                <td>
                    <input name="administrationnum" class="input" runat="server" id="txt_administrationnum"
                        size="10" type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    搜索页每页文件显示数量：
                </td>
                <td>
                    <input name="searchnum" class="input" runat="server" id="txt_searchnum" size="10"
                        type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    用户默认空间：
                </td>
                <td>
                    <input name="defaultspace" class="input" runat="server" id="txt_defaultspace" size="10"
                        type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    文件存储位置：
                </td>
                <td>
                    <input name="filestorepath" class="input" runat="server" id="txt_filestorepath" size="70"
                        type="text">
                </td>
            </tr>
            <tr class="line">
                <td class="style3">
                    网站默认时间显示格式：
                </td>
                <td>
                    <input name="time_format" class="input" runat="server" id="txt_time_format" type="text">
                </td>
            </tr>
            <tr class="line">
                <!-- 资源管理 -->
                <td class="style3">
                    文档：
                </td>
                <td>
                    <input type="text" class="input" runat="server" id="txt_Doc" name="ext[1]">
                </td>
            </tr>
            <tr class="line">
                <!-- 资源管理 -->
                <td class="style3">
                    视频：
                </td>
                <td>
                    <input type="text" class="input" runat="server" id="txt_Video" name="ext[2]">
                </td>
            </tr>
            <tr class="line">
                <!-- 资源管理 -->
                <td class="style3">
                    图片：
                </td>
                <td>
                    <input type="text" class="input" runat="server" id="txt_Photo" name="ext[3]">
                </td>
            </tr>
        </tbody>
    </table>
    <center>
        <asp:Button CssClass="btn" ID="btn_Submit" runat="server" Text="提交" 
            onclick="btn_Submit_Click" />
        <input name="Submit2" value=" 重 置 " class="btn" type="reset">
    </center>
    </form>
</asp:Content>
