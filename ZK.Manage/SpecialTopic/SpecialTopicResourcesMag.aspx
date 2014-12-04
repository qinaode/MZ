<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true" CodeBehind="SpecialTopicResourcesMag.aspx.cs" Inherits="ZK.Manage.SpecialTopic.SpecialTopicResourcesMag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
<link rel="stylesheet" href="/css/userInfo.css" />
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
<div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >专题管理>专题资源管理
        </div>
        <div class="searchform" >              
            <span style="margin-left:15px; font-size:13px; font-family:微软雅黑; font-weight:bold;">专题名称：</span>
            <span style="font-size:13px; font-family:微软雅黑; font-weight:bold;">
               <asp:Label ID="lblSpecialTopic" runat="server" Text="XXXX专题"></asp:Label></span>
        </div>   
         <div class="searchform">
            <span style="margin-left:15px; ">名称：</span>
            <asp:TextBox ID="txtFileName" runat="server" value="" onfocus=""></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
        </div>   
        
        <table width="100%" class="tableform text-center" border="0" cellspacing="0" >
            <tr class="userList" align="left" >
              <td style="display:none;">
                 </td>
                 <td style=" width:15px;">
                 </td>
                <td  >
                    名称
                </td>
                <td>
                    路径
                </td>                                
                <td>
                    上级目录
                </td>
                <td >
                    文件夹
                </td>
                <td>
                    所有者
                </td>                                
                <td>
                    文件类型
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rptSpecialtopicList" runat="server" OnItemCommand="AppListItem_Commond">
                <ItemTemplate>
                    <tr  align="left" class="nodeList" style="hover: background-color: #F8F7EF; margin-left:15px;">
                    <td style="display:none;">
                            <div id="Div1">
                                <%#Eval("ID")%>
                            </div>                             
                        </td>
                         <td style=" width:15px;">
                 </td>  
                         <td >
                            <div id="Div8">
                                <%#Eval("fileName")%>
                            </div>
                        </td>                      
                        <td>
                            <div id="Div2">
                                <%#Eval("imageURL")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div3">
                                <%#Eval("typeID")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div4">
                                <%#Eval("fileID")%>
                            </div>
                        </td>
                        <td>
                            <div id="Div5">
                                <%#Eval("USERNAME")%>
                            </div>
                        </td>
                        
                        <td>
                            <div id="Div6">
                                <%#BindFileType(Eval("fileType"))%>
                            </div>
                        </td>
                        <td>                                                       
                            <asp:LinkButton  ID="btn_MoveUp" runat="server"  CommandName="CN_btn_MoveUp" CommandArgument='<%# Eval("ID") %>' >
                                <img src="../image/up2.gif" width="11" height="10" />上移</asp:LinkButton>
                                
                                <asp:LinkButton  ID="btn_MoveDown" runat="server"  CommandName="CN_btn_MoveDown" CommandArgument='<%# Eval("ID") %>' >
                                <img src="../image/down.gif" width="11" height="10" /><span style="margin-left: -3px;"> 下移</span></asp:LinkButton>
                            
                                <asp:LinkButton ID="btn_Delete" CommandName="CN_btn_Delete" CommandArgument='<%# Eval("ID") %>'
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
