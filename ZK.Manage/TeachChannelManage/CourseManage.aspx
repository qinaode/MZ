<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="CourseManage.aspx.cs" Inherits="ZK.Manage.TeachChannelManage.TeachChannel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
    教材管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link href="../css/userInfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <%--    <script src="../commonjs/jquery-1.4.4.min.js" type="text/javascript"></script>--%>
    <script src="../commonjs/lhgdialog/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
   <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script src="../js/checkbox.js" type="text/javascript"></script>
    <script src="../js/courseManage.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        function CheckOne() {           
            var j=0;
           var checkboxs = document.getElementsByName("checkbox_1");
            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i];
                if (e.checked) {
                    j++;
                    }
            }           
            if (j == checkboxs.length) {
                $("#hhhh").attr("checked", true);
            }
            else {
                $("#hhhh").attr("checked", false);
            }
       }
       function SelectAll() {
         if ($("#hhhh").attr("checked") != "checked") {
               var checkboxs = document.getElementsByName("checkbox_1");
               for (var i = 0; i < checkboxs.length; i++) {
                   var e = checkboxs[i];
                   e.checked = false;
               }
           }
           else {
               var checkboxs = document.getElementsByName("checkbox_1");
               for (var i = 0; i < checkboxs.length; i++) {
                   var e = checkboxs[i];
                   e.checked = true;
               }
           } 
       }

    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >教学管理 >教材管理</div>
        <div class="pageInfo" align="left">
            名称：<input type="text" id="text_CourseName" />
            &nbsp;&nbsp;<input type="button" id="btn_Search" value="搜索" onclick="GetCourseLIstByCondition()" />&nbsp;&nbsp;<input
                type="button" value="添加" onclick="AddNewItem_Click()" />
        </div>
        <div id="div_ForAllContent">
            <div id="div_courseContent" align="right">
            </div>
            <br />
            <div>
                <table width="95%">
                    <tr>
                        <td width="30%">
                           <div>
                             <span style="margin-right:10px;">&nbsp;<input  type="checkbox"  id="hhhh" onclick="SelectAll()" />全选 </span>
                            <input type="button" value="批量删除"   onclick="DeleteCheckedItems_Course()" />
                           </div>
                            <%--<div>
                                <input type="checkbox" class="check_ForAll" />  &nbsp;&nbsp; <a onclick="CheckAll()">全选</a>&nbsp;&nbsp;
                                <input type="button" value="批量删除" onclick="DeleteCheckedItems_Course()" />
                           </div>--%>
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
