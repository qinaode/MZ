<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="lessonManage.aspx.cs" Inherits="ZK.Manage.TeachChannelManage.lessonManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
    课程管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link href="../css/userInfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/productCategory.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/lhgdialog/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script src="../inc/dtree_TeachChannel.js" type="text/javascript"></script>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/lessonManage.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >教学管理 >课程管理</div>
        <div class="pageInfo" align="left">
            <table>
                <tr align="center">
                    <td width="60px" align="right">
                        科目：
                    </td>
                    <td>
                        <div id="div_CourseList">
                        </div>
                    </td>
                    <td width="60px" align="right">
                        年级：
                    </td>
                    <td>
                        <div id="div_GradeList">
                        </div>
                    </td>
                    <td width="60px" align="right">
                        版本:
                    </td>
                    <td>
                        <div id="div_EditionList">
                        </div>
                    </td>
                    <td width="70px">
                        <input type="button" id="btn_Search" value="搜索" onclick="GetLessonListByCondition()" />
                    </td>
                    <td>
                        <input type="button" value="添加单元" onclick="AddNewItem_Click()" />
                    </td>
                    <td width="70px">
                        <input type="button" value="添加课程" onclick="AddNewLesson_Click()" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="dtree">
            <%-- <p>
                <a href="javascript: dl.openAll();">展开</a> | <a href="javascript: dl.closeAll();">收缩</a></p>--%>
            <div id="div_Tree">
            </div>
        </div>
    </div>
</asp:Content>
