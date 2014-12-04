<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage"
    ValidateRequest="false" %>

<%@ Import Namespace="ZK.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --教学资源
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/Teach_Common.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <script src="../../Scripts/kindeditor-4.1.5/kindeditor.js" type="text/javascript"></script>
    <script src="../../Scripts/kindeditor-4.1.5/lang/zh_CN.js" type="text/javascript"></script>
    <script src="../../Scripts/kindeditor-4.1.5/plugins/code/prettify.js" type="text/javascript"></script>
    <script src="../../js/Teach_Index.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".cur2").click(function () {
                $(".cur3").removeClass("current1");
                $(".cur4").removeClass("current1");
                $(".cur5").removeClass("current1");
                $(this).addClass("current1");
            });
            $(".cur3").click(function () {
                $(".cur2").removeClass("current1");
                $(".cur4").removeClass("current1");
                $(".cur5").removeClass("current1");
                $(this).addClass("current1");
            });
            $(".cur4").click(function () {
                $(".cur3").removeClass("current1");
                $(".cur2").removeClass("current1");
                $(".cur5").removeClass("current1");
                $(this).addClass("current1");

            });
            $(".cur5").click(function () {
                $(".cur3").removeClass("current1");
                $(".cur4").removeClass("current1");
                $(".cur2").removeClass("current1");
                $(this).addClass("current1");

            });
            $(".CourseList").click(function () {
                $(".CourseList").removeClass("Current_Course");
                $(this).addClass("Current_Course");
                GetParamtersAndTurn();

            });
            $(".GradeList").click(function () {
                $(".GradeList").removeClass("Current_Grade");
                $(this).addClass("Current_Grade");
                GetParamtersAndTurn();
            });
            $(".EditionList").click(function () {
                $(".EditionList").removeClass("Current_Edition");
                $(this).addClass("Current_Edition");
                GetParamtersAndTurn();
            });

        })
        function GetParamtersAndTurn() {
            var cid = $(".Current_Course").attr("id");
            var gid = $(".Current_Grade").attr("id");
            var eid = $(".Current_Edition").attr("id");
            cid = cid.substr(9);
            gid = gid.substr(9);
            eid = eid.substr(9);
            window.location = "/Teach/Index?id=C" + cid + "_G" + gid + "_E" + eid;

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <div class="warp1">
        <div class="down">
            <p class="tit2">
                <span class="inb">知识管理></span><span class="ino">教学资源</span><!--<span class="inr">分享</span><span class="ine">收藏到书签</span>--></p>
            <div class="top">
                <div class="top-left">
                    <p class="ject1">
                        科目</p>
                    <img src="/images/images.jpg" class="image1" />
                    <ul class="ject-ul" id='subject'>
                        <%List<ZK.Model.ZK_Course> listCourse = (List<ZK.Model.ZK_Course>)ViewData["Teach_CourseList"]; %>
                        <%foreach (var item in listCourse)
                          {%>
                        <li style="cursor: pointer;" class="CourseList" id="TeachCou_<%=item.courseID %>"><a>
                            <%=item.courseName %></a></li>
                        <%} %>
                    </ul>
                </div>
                <div class="top-cen">
                    <p class="ject1">
                        年级</p>
                    <img src="/images/images.jpg" class="image1" />
                    <ul class="ject-ul" id="grade">
                        <%List<ZK.Model.ZK_Grade> listGrade = (List<ZK.Model.ZK_Grade>)ViewData["Teach_GradeList"]; %>
                        <%foreach (var item in listGrade)
                          {%>
                        <li style="cursor: pointer;" class="GradeList" id="TeachGra_<%=item.gradeID %>"><a>
                            <%=item.gradeName %></a></li>
                        <%} %>
                    </ul>
                </div>
                <div class="top-right">
                    <p class="ject1">
                        版本</p>
                    <img src="/IMAGES/images.jpg" class="image1" />
                    <ul class="ject-ul" id="version">
                        <%List<ZK.Model.ZK_Edition> listEdition = (List<ZK.Model.ZK_Edition>)ViewData["Teach_EditionList"]; %>
                        <%foreach (var item in listEdition)
                          {%>
                        <li style="cursor: pointer;" class="EditionList" id="TeachEdi_<%=item.editionID %>">
                            <a>
                                <%=item.editionName %></a></li>
                        <%} %>
                    </ul>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div class="under" id='data'>
                <div class="under-left">
                    <!---树形列表start-->
                    <%
                        List<ZK.Model.ZK_Lesson> listunits = (List<ZK.Model.ZK_Lesson>)ViewData["Teach_UnitList"];
                        ZK.Controllers.TeachController tc = new ZK.Controllers.TeachController();
                    %>
                    <%if (listunits.Count > 0)
                      {
                          foreach (var item in listunits)
                          {%>
                    <div class="text" id="chapter">
                        <h3>
                            <%=item.lessonName%></h3>
                        <% List<ZK.Model.ZK_Lesson> listLessons = tc.GetLessonByUnit(item.lessonID.ToString());
                           if (listLessons != null && listLessons.Count > 0)
                           {
                               foreach (var lesson in listLessons)
                               {%>
                        <p style="cursor: pointer;"  id="lessonid_<%=lesson.lessonID %>" class="LessonList"
                            onclick="GetInfoByLessonID(<%=lesson.lessonID %>)">
                            <%=lesson.lessonName%></p>
                        <%}
                           } %>
                    </div>
                    <%
                        }
                      } %>
                    <!---树形列表end-->
                </div>
                <div style="width: 709px; float: left; height: auto !important; min-height: 1100px;">
                    <div style="width: 697px; background-color: #EDF5FF; padding: 5px; height: auto;
                        overflow: hidden;" id="teach">
                        <!---文档内容start-->
                        <p class="up-p">
                            教学目标</p>
                        <div class="up-div">
                            <p name="aim" id="p_TeachMB">
                            </p>
                            <div style="display: none; width: 100%" id="div_TeachMB">
                                <textarea id="txt_TeachMB" style="width: 100%; height: 30px"></textarea>
                            </div>
                            <a id="Edit_TeachMB" href="javascript:void(0);return false;" style="display: none"
                                onclick="EditLessonInfo(this.id)">编辑</a>
                        </div>
                        <p class="up-p1">
                            教学重点</p>
                        <div class="up-div1">
                            <p name="keynote" id="p_TeachZD">
                            </p>
                            <div style="display: none" id="div_TeachZD">
                                <textarea id="txt_TeachZD" style="width: 100%; height: 30px"></textarea></div>
                            <a id="Edit_TeachZD" href="javascript:void(0);return false;" style="display: none"
                                onclick="EditLessonInfo(this.id)">编辑</a>
                        </div>
                        <p class="up-p2">
                            教学难点</p>
                        <div class="up-div2">
                            <p name='difficulty' id="p_TeachND">
                            </p>
                            <div style="display: none" id="div_TeachND">
                                <textarea id="txt_TeachND" style="width: 100%; height: 30px"></textarea></div>
                            <a id="Edit_TeachND" href="javascript:void(0);return false;" style="display: none"
                                onclick="EditLessonInfo(this.id)">编辑</a>
                        </div>
                        <!---文档内容end-->
                    </div>
                    <div class="tab1">
                        <ul class="cur1">
                            <li class="cur2 current1" onclick="fileTypeChanged('0')">全部<span id="allcount"></span></li>
                            <li class="cur3" onclick="fileTypeChanged('1')">教案</li>
                            <li class="cur4" onclick="fileTypeChanged('2')">课件</li>
                            <li class="cur5" onclick="fileTypeChanged('3')">习题</li>
                        </ul>
                        <p class="new">
                            排序：<!--<span>精品</span><span>免费</span><span>更多页数</span>--><span>最新</span></p>
                    </div>
                    <iframe id="iframe_Filelist" scrolling="no" frameborder="0" style="width: 100%; height: 1350px;">
                    </iframe>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
