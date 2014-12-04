<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/index.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .a_CurrentLesson
        {
            color: Red;
        }
    </style>
    <script>
        var LessonID = ""; //用于记录选择的lessonID
        var file_id = "0";

        $(function () {

            //获取url里的参数
            var url = location.search; //获取url中"?"符后的字串
            if (url.indexOf("?") != -1) {
                file_id = url.substr(9);
            }
            $(".nav li").mouseenter(function () {
                $(this).prev().find("span").css("display", "none");
                $(this).find("span").css("display", "none");
                $(this).find("a").css("padding-left", "43px");
                $(this).find("a").css("padding-right", "43px");
            });
            $(".cur a").mouseleave(function () {
                $(this).css("padding-left", "43px");
            })
            $(".nav li").mouseleave(function () {
                $(this).prev().find("span").css("display", "block");
                $(this).find("span").css("display", "block");
                $(this).find("a").css("padding-left", "35px");
                $(".cur").find("a").css("padding-left", "43px");
                $(this).find("a").css("padding-right", "35px");

            })
        })
        //选择课程
        function CheckLesson() {
            //请求绑定数据
            var courseid = $("#S_Course").val();
            var gradeid = $("#S_Grade").val();
            var editionid = $("#S_Edition").val();
            if (courseid == "-1") {
                alert("请选择科目");
                return;
            }
            if (gradeid == "-1") {
                alert("请选择年级");
                return;
            }
            if (editionid == "-1") {
                alert("请选择版本");
                return;
            }
            $.ajax({
                type: "Post",
                url: "/Push/Teach_GetLessonList",
                data: { "courseid": courseid, "gradeid": gradeid, "editionid": editionid },
                datatype: "json",
                success: function (backdata) {
                    $("#choice").html("");
                    var strhtml = "   <div style='width:386px; height:30px; background-color:#f1f1f1; line-height:30px;color:#646464; font-size:14px; font-weight:bold; padding-left:20px;'>课程列表</div>";
                    strhtml = strhtml + "<div style='width:280px; margin:0 auto;'><h2>" + backdata[0]["classID"] + "</h2> ";
                    $.each(backdata, function (i, item) {
                        if (item["lessonParent"] == 0) {
                            strhtml = strhtml + "<div class='cho'><h3><img src='/images/img.gif' /></h3><h2 style='margin-left:40px;'>" + item["lessonName"] + "</h2>";
                            $.each(backdata, function (j, itemlesson) {
                                if (itemlesson["lessonParent"] == item["lessonID"]) {
                                    strhtml = strhtml + "<p><a class='C_Lesson' id='Lesson_" + item["lessonID"] + "' href='javascript:void(0);return false;' onclick='ChangeCurrentLesson(this.id)'>" + itemlesson["lessonName"] + "</a></p>";
                                }
                            })
                            strhtml = strhtml + "</div>";
                        }
                    });
                    $("#choice").html(strhtml);
                    //弹出层事件
                    $(".cho img").each(function () {
                        $(this).click(function () {
                            if ($(".cho p").css("display") == "block") {
                                $(".cho p").css("display", "none")
                                $(".cho img[src=/images/img1.gif]").attr("src", "/images/img.gif");
                            } else {
                                $(".cho p").css("display", "block")
                                $(".cho img[src=/images/img.gif]").attr("src", "/images/img1.gif");
                            }
                        })
                    });
                }
            });

            $("#div_BackGround").css("display", "block");
            $("#div_LessonContent").css("display", "block");

        }
        //弹出层的返回按钮
        function ReturnBack() {
            $("#div_BackGround").css("display", "none");
            $("#div_LessonContent").css("display", "none");
        }
        //改变选择项
        function ChangeCurrentLesson(id) {

            $(".C_Lesson").removeClass("a_CurrentLesson");
            $("#" + id).addClass("a_CurrentLesson");
            LessonID = id.substr(7);
        }
        //保存选择课程
        function SaveCheckedLesson() {
            $("#txt_Lesson").val($("#Lesson_" + LessonID).html());
            $("#div_BackGround").css("display", "none");
            $("#div_LessonContent").css("display", "none");
        }
        //添加新关键字
        function AddNewKeyWord(kd) {

            if ($("#txt_KeyWord").val() == "") {
                $("#txt_KeyWord").val($("#txt_KeyWord").val() + kd + " ");
            }
            else {
                $("#txt_KeyWord").val($("#txt_KeyWord").val() + " " + kd);
            }
        }
        //推送资源
        function Push_TeachFile() {
            //课程id
            var lessonid = LessonID;
            if (LessonID == "") {
                alert("课程不可为空");
                return;
            }
            //关键字
            var keyword = $("#txt_KeyWord").val();
            $.ajax({
                type: "Post",
                url: "/Push/Push_TeachFile",
                data: { "lessonid": lessonid, "keyword": keyword, "file_id": file_id },
                datatype: "text",
                success: function (backdata) {
                    if (backdata == "成功") {
                        alert("推送成功");
                        window.location.href = "/Home/Index";
                    }
                    else {
                        alert("推送失败");
                    }
                }
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --资源推送频道
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="warp">
        <p class="tit3">
            <span class="inb">资源推送 ></span><span class="ino"> 教学资源推送 </span>
        </p>
        <div class="picture">
            <p class="sort">
                <span>选择科目：</span><select id="S_Course"><option value="-1">请选择科目</option>
                    <%List<ZK.Model.ZK_Course> Teach_CourseList = (List<ZK.Model.ZK_Course>)ViewData["Teach_CourseList"];
                      foreach (var item in Teach_CourseList)
                      {%>
                    <option value="<%=item.courseID %>">
                        <%=item.courseName %></option>
                    <%}%>
                </select></p>
            <p class="sort">
                <span>选择年级：</span><select id="S_Grade"><option value="-1">请选择年级</option>
                    <%List<ZK.Model.ZK_Grade> Teach_GradeList = (List<ZK.Model.ZK_Grade>)ViewData["Teach_GradeList"];
                      foreach (var item in Teach_GradeList)
                      {%>
                    <option value="<%=item.gradeID %>">
                        <%=item.gradeName%></option>
                    <%}%></select></p>
            <p class="sort">
                <span>选择版本：</span><select id="S_Edition"><option value="-1">请选择版本</option>
                    <%List<ZK.Model.ZK_Edition> Teach_EditionList = (List<ZK.Model.ZK_Edition>)ViewData["Teach_EditionList"];
                      foreach (var item in Teach_EditionList)
                      {%>
                    <option value="<%=item.editionID %>">
                        <%=item.editionName%></option>
                    <%}%>
                </select></p>
            <p class="sort">
                <span>属于课程：<br />
                </span>
                <input type="text" id="txt_Lesson" /><a href="#" class="btn2" onclick="CheckLesson()">点此选择课程</a></p>
            <div style="clear: both">
            </div>
            <p class="keywords">
                关键字<span>（多个关键字用逗号或者空格隔开）</span>：</p>
            <p class="sort">
                <textarea style="width: 618px; height: 100px;" id="txt_KeyWord"></textarea></p>
            <div style="clear: both">
            </div>
            <div class="keywords">
                常用关键字(点击添加)：</div>
            <div class="check">
                <%System.Data.DataTable listtags = (System.Data.DataTable)ViewData["listtags"];
                  foreach (System.Data.DataRow item in listtags.Rows)
                  {%>
                <div class="box" style="width: 100px;">
                    <a href="javascript:void(0);return false;" onclick="AddNewKeyWord('<%=item["tagName"] %>')">
                        <%=item["tagName"] %></a></div>
                <%} %>
                <%--<a href="javascript:void(0);return false;" class="add" onclick="AddNewKeyWord()"
                    style="width: 50px;">添加</a>--%>
            </div>
            <div style="clear: both">
            </div>
            <a href="javascript:void(0);return false;" onclick="Push_TeachFile()" class="btn1 come">
                推送</a> <a href="/Push/" class="btn1">返回</a>
        </div>
    </div>
    <!-- 弹出层 -->
    <div class="bg" id="div_BackGround" style="display: none">
    </div>
    <div class="add2" id="div_LessonContent" style="display: none">
        <div class="add-top2">
            <p>
                选择课程</p>
            <img src="/images/erro.png" class="close" style="cursor: pointer;" onclick="ReturnBack()" />
        </div>
        <div class="add-down">
            <div id="choice">
            </div>
        </div>
        <div class="buttons">
            <a href="javascript:void(0);return false;" class="return2" onclick="SaveCheckedLesson()">
                确定</a> <a href="javascript:void(0);return false;" class="return2" onclick="ReturnBack()">
                    返回</a>
        </div>
    </div>
    <!--弹出层结束-->
</asp:Content>
