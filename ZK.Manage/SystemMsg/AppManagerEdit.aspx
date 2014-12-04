<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppManagerEdit.aspx.cs" Inherits="ZK.Manage.SystemMsg.AppManagerEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="/css/saveandcancel.css" />
    <link rel="stylesheet" href="/css/moralchanel.css" />
    <script src="../commonjs/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.js" type="text/javascript"></script>
    <script type="text/javascript">
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }
        function Cancel() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象                    
            api.close();
        }       
    </script>
    <script type="text/javascript">
        function CheckFile(f, p) {
            //判断图片类型
            var f = document.getElementById("File1").value;
            if (f == "")
            { alert("请上传图片"); return false; }
            else {
                if (!/\.(|ICO)$/.test(f)) {
                    alert("图片类型必须是.ico格式！")
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div class="searchform" id="div1" runat="server" >
             <div> 
                <table border='0' cellpadding='0' cellspacing='0'>
                        <tr>
                               <td style="width:65px; float:right;">
                                      应用名称：
                               </td>
                               <td colspan="3">
                                        <asp:TextBox ID="txtAppName" runat="server" value="" onfocus="" Style="width: 287px;"></asp:TextBox>
                                        <span style="color: Red; margin-left: 1px">*</span>
                               </td>
                        </tr>
                            <tr>
                               <td style="width:65px; float:right;">
                                      应用分类：
                               </td>
                               <td colspan="3">
                                        <asp:TextBox ID="txtAppCategory" runat="server" value="" onfocus="" Style="width: 287px;"></asp:TextBox>
                                        <span style="color: Red; margin-left: 1px">*</span>
                               </td>
                        </tr>
                        <tr>
                               <td valign="top">
                                       应用介绍：
                               </td>
                               <td colspan="3">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtAppIntroduce" runat="server" value=""  TextMode="MultiLine" Style="width: 285px; height:100px;"></asp:TextBox>
                                        </div>
                                        
                               </td>
                        </tr>

                        <tr style="height:6px;"></tr>
                          <tr runat="server" id="tr">
                               <td valign="top"  >
                                       应用图标：
                               </td>
                               <td colspan="3">
                                        <div style="float: left;">
                                             <input id="fPicture" runat="server" type="file"  size="50" style=" line-height: 26px; height: 26px;" onclick="CheckFile(this.value,this)" />
                                        </div>
                                        <div style="float: left">
                                            <span style="color: Red; margin-left: 54px">*</span>
                                        </div>
                               </td>
                        </tr>
                          <tr>
                               <td valign="top">
                                       应用URL：
                               </td>
                               <td colspan="3">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtAppURL" runat="server" value=""  TextMode="MultiLine" Style="width: 285px; height:50px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <span style="color: Red; margin-left: 5px">*</span>
                                        </div>
                               </td>
                        </tr>
                           <tr>
                               <td>
                                       请求方式：
                               </td>
                               <td colspan="3">
                                       <label>
                                            <select name="selectMode" runat="server" id="selectMode" style="width: 290px;">
                                                <option value="0">GET</option>
                                                <option  value="1">POST</option>
                                            </select>
                                      </label>
                               </td>
                        </tr>
                           <tr>
                               <td valign="top">
                                       POST参数：
                               </td>
                               <td colspan="3">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtPost" runat="server" value=""  TextMode="MultiLine" Style="width: 285px; height:50px;"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <span style="color: Red; margin-left: 5px">*</span>
                                        </div>
                               </td>
                        </tr>
                         <tr>
                               <td>
                                       自动弹出：
                               </td>
                               <td colspan="3">
                                       <label>
                                            <select name="selectPopUp" runat="server" id="selectPopUp" style="width: 290px;">
                                                <option value="0">关闭</option>
                                                <option  value="1">开启</option>
                                            </select>
                                      </label>
                               </td>
                        </tr>
                           <tr>
                               <td>
                                       浏览器：
                               </td>
                               <td colspan="3">
                                       <label>
                                            <select name="selectBrowser" runat="server" id="selectBrowser" style="width: 290px;">
                                                <option value="0">系统默认</option>
                                                <option  value="1">客户端嵌入</option>
                                                <option  value="2">客户端嵌入（弹出：系统默认）</option>
                                            </select>
                                      </label>
                               </td>
                        </tr> 
                         <tr>
                               <td style="width:65px; float:right;">
                                      页面宽度：
                               </td>
                               <td >
                                        <asp:TextBox ID="txtWith" runat="server" value="" onfocus="" Style="width: 100px;"></asp:TextBox>
                                       
                               </td>
                                <td style="width:65px; float:right;">
                                      页面高度：
                               </td>
                               <td >
                                        <asp:TextBox ID="txtHeight" runat="server" value="" onfocus="" Style="width: 100px;"></asp:TextBox>
                                       
                               </td>
                        </tr>  
                        <tr>
                               <td>
                                       快捷方式：
                               </td>
                               <td colspan="3">
                                       <label>
                                            <select name="selectShortCut" runat="server" id="selectShortCut" style="width: 290px;">
                                                <option value="0">应用列表</option>
                                                <option  value="1">用户信息栏</option>
                                                <option  value="2">快速启动栏</option>
                                            </select>
                                      </label>
                               </td>
                        </tr>
                </table>   
                </div>
                <div style="height: 10px;">
                </div>
                <div style="float: right;" class="ui_buttons">
            <asp:Button class="ui_state_highlight" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
            <a id="A1" runat="server" onclick="Cancel()">
                <asp:Button ID="Button1" runat="server" Text="取消" /></a>              
        </div>        
        </div>

             <div id="div2" class="searchform" runat="server">
                <div style="height: 10px;">
                </div>
                     <table border='0' cellpadding='0' cellspacing='0'>
                         <tr >
                               <td  style="width:65px; float:right;">
                                       应用图标：
                               </td>
                               <td colspan="3">
                                        <div style="float: left;">
                                             <input id="upFile" runat="server" type="file"   
                                                 style=" line-height: 26px; height: 26px; width: 172px;" 
                                                 onclick="CheckFile(this.value,this)" />
                                        </div>
                                        <div style="float: left">
                                            <span style="color: Red; margin-left: 45px">*</span>
                                        </div>
                               </td>
                        </tr>
                        <tr>
                                <td colspan="2" style="height:8px;"></td>
                        </tr>
                        <tr >
                            <td>
                                    &nbsp;
                            </td>
                             <td>
                                     <div style="float: left;" class="ui_buttons">
            <asp:Button class="ui_state_highlight" ID="btnUpLoad" runat="server" Text="上传" OnClick="btnUpLoad_Click" />
            <a id="A2" runat="server" onclick="Cancel()">
                <asp:Button ID="Button3" runat="server" Text="取消" /></a>              
        </div>        
                            </td>
                        </tr>
                     </table>
            </div>
            
    </div>
    </form>
</body>
</html>
