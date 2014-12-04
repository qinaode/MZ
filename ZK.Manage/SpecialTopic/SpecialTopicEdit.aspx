<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialTopicEdit.aspx.cs" Inherits="ZK.Manage.SpecialTopic.SpecialTopicEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/css/moralchanel.css" />
    <link rel="stylesheet" href="/css/saveandcancel.css" />
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        function closeWindow() {

            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();

        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="searchform" >
             <div> 
                <table border='0' cellpadding='0' cellspacing='0'>
                        <tr>
                               <td style="width:65px; float:right;">
                                      专题名称：
                               </td>
                               <td>
                                        <asp:TextBox ID="txtSpecialName" runat="server" value="" onfocus="" Style="width: 287px;"></asp:TextBox>
                                        <span style="color: Red; margin-left: 1px">*</span>
                               </td>
                        </tr>
                        <tr>
                               <td valign="top">
                                        专题描述：
                               </td>
                               <td>
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtSpecialDesc" runat="server" value=""  TextMode="MultiLine" Style="width: 285px; height:80px;"></asp:TextBox>
                                        </div>
                                        <%--<div style="float: left">
                                            <span style="color: Red; margin-left: 5px">*</span>
                                        </div>--%>
                               </td>
                        </tr>
                         <tr style="height:10px;">
                            <td></td>
                            <td></td>
                         </tr>                       
                        <%--<tr >
                                <td></td>
                               <td valign="middle">
                                   <div style="float: left;"><asp:CheckBox ID="checkedbIsUse" runat="server" /></div>
                                   <div style="float: left; margin-top:-5px;"> <span  style="margin-left: 5px;">启用</span></div>
                               </td>
                        </tr>--%>
                          
                </table>   
                </div>
                <div style="height: 10px;">
                </div>
                <div style="float: right;" class="ui_buttons">
            <asp:Button class="ui_state_highlight" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
            <a id="A1" runat="server" onclick="closeWindow()">
                <asp:Button ID="Button1" runat="server" Text="取消" /></a>              
        </div>        
        </div>
    </div>
    </form>
</body>
</html>
