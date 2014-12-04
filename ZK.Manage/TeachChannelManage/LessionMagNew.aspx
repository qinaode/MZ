<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="LessionMagNew.aspx.cs" Inherits="ZK.Manage.TeachChannelManage.LessionMagNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <script src="../inc/dtree_TeachChannel.js" type="text/javascript"></script>
    <%--<script src="../inc/dtree_lession.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/jscript">
        function AddUnit(courseId, editionId, gradeId) {
            $.dialog({ title: '添加单元', width: '390px', height: '290px', content: 'url:LessonMagEdit.aspx?ty=addUnit&t=' + new Date().getTime().toString() + '&courseId=' + courseId + '&editionId=' + editionId + '&gradeId=' + gradeId, lock: true, max: false, min: false });
        }
        function AddLession(courseId, editionId, gradeId) {
            $.dialog({ title: '添加课程', width: '390px', height: '500px', content: 'url:LessonMagEdit.aspx?ty=addLession&t=' + new Date().getTime().toString() + '&courseId=' + courseId + '&editionId=' + editionId + '&gradeId=' + gradeId, lock: true, max: false, min: false });
        }
        function CateEdit(id, pid) {
           
            if (pid == 0) {
                $.dialog({ title: '编辑单元', width: '390px', height: '290px', content: 'url:LessonMagEdit.aspx?ty=edit&flag=Pid&t=' + new Date().getTime().toString() + '&id=' + id, lock: true, max: false, min: false });
            }
            else {
                $.dialog({ title: '编辑课程', width: '390px', height: '500px', content: 'url:LessonMagEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + id, lock: true, max: false, min: false });
            }
        }

        function Search(courseId, editionId, gradeId) {
            var courseID = courseId;
            var editionID = editionId;
            var gradeID = gradeId;
            window.location = "/TeachChannelManage/LessionMagNew.aspx?curp=teach&flag=search&courseId=" + courseID + "&editionId=" + editionID + "&gradeId=" + gradeID;
          
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
        <form action="LessionMagNew.aspx" runat="server" id="queryfrom" name="queryfrom">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >教学管理 >课程管理</div>
        <div class="searchform">
            <span style="margin-left: 15px;">科目：</span>
            <label>
                <select name="CourseList" runat="server" id="cmbCourseList" style="width: 120px;">
                    <option>-请选择-</option>
                </select>
            </label>
            年级：
            <label>
                <select name="GradeList" runat="server" id="cmbGradeList" style="width: 120px;">
                    <option>-请选择-</option>
                </select>
            </label>
            版本：
            <label>
                <select name="EditionList" runat="server" id="cmbEditionList" style="width: 120px;">
                    <option>-请选择-</option>
                </select>
            </label>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
            <asp:Button ID="btnAdd" runat="server" Text="添加单元" OnClick="btnAdd_Click" />
            <asp:Button ID="btnLession" runat="server" Text="添加课程" OnClick="btnLession_Click" />
        </div>
        <div>
            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
            <div style="float: left; width: 15px">
            </div>
            <div class="dtree">
                <%--       <p>
                    <a href="javascript: dl.openAll();">展开</a> | <a href="javascript: dl.closeAll();">收缩</a></p>--%>
                <asp:Literal runat="server" ID="Literal1"></asp:Literal>
                <script type="text/javascript">
     
                d = new dTree('d');
        <asp:Repeater ID="pCategoryList" runat="server">
            <ItemTemplate>
                d.add('<%#Eval("lessonID")%>', '<%#Eval("lessonParent")%>', '<%#Eval("lessonName")%>', '');
            </ItemTemplate>
       </asp:Repeater>
	   document.write(d);
 
                </script>
                <div class="dTreeNode">
                    <div style="float: left; margin: 0; padding-left: 15px; font-weight: bold;">
                        共<asp:Literal ID="litCount" runat="server"></asp:Literal>个记录</div>
                </div>
            </div>
            <%--  </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
            </asp:UpdatePanel>--%>
        </div>
        <div>
        </div>
        </form>
    </div>
</asp:Content>
