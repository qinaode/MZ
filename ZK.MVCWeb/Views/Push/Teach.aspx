<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    --教学资源推送
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="/CSS/index.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../css/common_push.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .box2
        {
            width: 50px;
            height: 15px;
            line-height: 15px;
            float: left;
            margin-right: 15px;
            
        }
        
        .btn3
        {
            display: block;
            line-height: 25px;
            text-align: center;
            float: left;
            color: #fff;
            font-weight: bold;
            margin-left: 100px;
            margin-top: 30px;
        }
        
        /*--弹出层样式--*/
        .bg
        {
            position: absolute;
            width: 100%;
            height: 100%;
            background-color: #000;
            z-index: 6;
            left: 0;
            top: 0;
            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            opacity: 0.5;
        }
        .add2
        {
            width: 535px;
            border: 1px solid #3E88CF;
            background-color: #fff;
            position: absolute;
            left: 50%;
            top: 5%;
            z-index: 50;
            margin-left: -259px;
        }
        .add-top2
        {
            width: 531px;
            height: 37px;
            background: url(/images/bgk.jpg) repeat-x;
            margin: 2px;
        }
        .add-top2 p
        {
            color: #fff;
            float: left;
            padding-left: 13px;
            font-weight: bold;
            font-size: 14px;
            line-height: 37px;
        }
        .add-top2 img
        {
            float: right;
            padding-right: 15px;
            padding-top: 5px;
        }
        .buttons
        {
            width: 260px;
            margin: 0 auto;
        }
        .return2
        {
            display: block;
            float: left;
            width: 94px;
            height: 28px;
            color: #fff;
            text-align: center;
            line-height: 28px;
            margin-top: 10px;
            margin-left: 20px;
            padding-bottom: 10px;
        }
        #choice
        {
            width: 407px;
            height: auto !important;
            height: 150px;
            min-height: 150px;
            border: 1px solid #E3E3E3;
            margin: 0 auto;
            margin-top: 10px;
        }
        .class
        {
            width: 386px;
            height: 30px;
            background-color: #f1f1f1;
            line-height: 30px;
            color: #646464;
            font-size: 14px;
            font-weight: bold;
            padding-left: 20px;
        }
        .school
        {
            width: 280px;
            margin: 0 auto;
        }
        #choice h3
        {
            float: left;
            width: 20px;
        }
        #choice h2
        {
            font-size: 16px;
            color: #646464;
            background: url(/images/check.png) no-repeat left;
            padding-left: 30px;
            margin-bottom: 10px;
        }
        #choice .chease
        {
            margin-left: 40px;
        }
        #choice .chease span
        {
            margin-left: 20px;
        }
        #choice div
        {
            margin-bottom: 16px;
        }
        #choice p
        {
            clear: both;
            width: 150px;
            text-align: center;
            display: none;
            color: #646464;
            background: url(/images/check1.png) no-repeat left;
            margin: 8px 0 0 60px;
        }
        #choice p input
        {
            margin-right: 10px;
        }
    </style>
    <script>
        var LessonID = ""; //用于记录选择的lessonID
        var file_id = "0";
        var oldid = "hidden_a";
        $(function () {

            //获取url里的参数
            var url = location.search; //获取url中"?"符后的字串
            if (url.indexOf("?") != -1) {
                file_id = url.substr(9);//获得文件ID
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
            var editionid = $("#S_Edition").val();//必选项
          
//            if (courseid == "-1") {
//                alert("请选择科目");
//                return;
//            }
//            if (gradeid == "-1") {
//                alert("请选择年级");
//                return;
//            }
//            if (editionid == "-1") {
//                alert("请选择版本");
//                return;
//            }
            
            $.ajax({
                type: "Post",
                url: "/Push/Teach_GetLessonList",
                data: { "courseid": courseid, "gradeid": gradeid, "editionid": editionid},
                datatype: "json",
                success: function (backdata) {
                    $("#choice").html("");
                    var strhtml = "   <div style='width:386px; height:30px; background-color:#f1f1f1; line-height:30px;color:#646464; font-size:14px; font-weight:bold; padding-left:20px;'>课程列表</div>";
                    strhtml = strhtml + "<div style='width:280px; margin:0 auto;'><h2>全部课程</h2> ";
                    $.each(backdata, function (i, item) {
                        if (item["lessonParent"] == 0) {
                            strhtml = strhtml + "<div class='cho' id='div1_" + item["lessonID"] + "'><h3><img src='/images/img.gif' id='Img_" + item["lessonID"] + "' onclick='ShowOrHiddenLesson(this.id)'/></h3><h2 style='margin-left:40px;'>" + item["lessonName"] + "</h2>";
                            $.each(backdata, function (j, itemlesson) {
                                if (itemlesson["lessonParent"] == item["lessonID"]) {
                                    strhtml = strhtml + "<p><a class='C_Lesson' id='Lesson_" + itemlesson["lessonID"] + "' href='javascript:void(0);' onclick='ChangeCurrentLesson(this.id)'>" + itemlesson["lessonName"] + "</a></p>";
                                }
                            })
                            strhtml = strhtml + "</div>";
                        }
                    });
                    $("#choice").html(strhtml);
                    //弹出层事件

                }
            });

            $("#div_BackGround").css("display", "block");
            $("#div_LessonContent").css("display", "block");

        }
        //展开或隐藏课程列表
        function ShowOrHiddenLesson(id) {
            var divid = "div1_" + id.substr(4);

            if ($("#" + id).attr("src") == "/images/img.gif") {
                $("#" + id).attr("src", "/images/img1.gif");
            }
            else {
                $("#" + id).attr("src", "/images/img.gif");
            }

            if ($("#" + divid + " p").css("display") == "block") {
                $("#" + divid + " p").css("display", "none")

            } else {
                $("#" + divid + " p").css("display", "block")

            }
        }
        //弹出层的返回按钮
        function ReturnBack() {
            $("#div_BackGround").css("display", "none");
            $("#div_LessonContent").css("display", "none");
        }
        //改变选择项
        function ChangeCurrentLesson(id) {
            document.getElementById(oldid).style.color = "Black";
            oldid = id;
            document.getElementById(id).style.color = "Red";

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
            //alert(txt_Lesson);
            //var lessonid = txt_Lesson;
            var lessonid = LessonID;
            var teachcateid = $("#Steachcate").val(); //所属分类可空

            var courseid = $("#S_Course").val();
            var gradeid = $("#S_Grade").val();
            var editionid = $("#S_Edition").val(); //必选项


            /*
            if (LessonID == "") {
                alert("课程不可为空");
                return;
            }
            if (teachcateid == "-1") {
                alert("请选择教学分类");
                return;
            }
            */
            //关键字
            var keyword = $("#txt_KeyWord").val();
            $.ajax({
                type: "Post",
                url: "/Push/Push_TeachFile",
                data: { "lessonid": lessonid, "keyword": keyword, "file_id": file_id, "teachcateid": teachcateid, "courseid": courseid, "gradeid": gradeid, "editionid": editionid },
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
        function S_Course_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_wrap" style="width: 1000px; margin-right: auto; margin-left: auto;
        overflow: hidden; border-width: 1px; border-color: Black; border-style: solid;">
        <div class="title">
            <h2>
                资源推送>教学资源推送</h2>
        </div>
        <div style="width: 870px; margin-right: auto; margin-left: auto; overflow: hidden;
            border-width: 2px; border-color: Green; border-style: solid; margin-top: 40px;
            margin-bottom: 40px;">
            <table width="90%" align="center" style="padding-bottom: 10px; padding-top: 5px;
                border-spacing: 10px; margin-top: 20px;" class="green2">
                <tr style="width:520px;">
                    <td style="width: 80px;">
                        O 选择科目
                    </td>
                    <td style="width: 40px;">
                        |
                    </td>
                    <td>
                        <select id="S_Course" style="width: 434px;" onclick="return S_Course_onclick()">
                            <option value="-1">请选择科目</option>
                            <%List<ZK.Model.ZK_Course> Teach_CourseList = (List<ZK.Model.ZK_Course>)ViewData["Teach_CourseList"];
                              foreach (var item in Teach_CourseList)
                              {%>
                            <option value="<%=item.courseID %>">
                                <%=item.courseName %></option>
                            <%}%>
                        </select><label style="color:Red;"> *</label></td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        O 选择年级
                    </td>
                    <td style="width: 40px;">
                        |
                    </td>
                    <td>
                        <select id="S_Grade" style="width: 435px;">
                            <option value="-1">请选择年级</option>
                            <%List<ZK.Model.ZK_Grade> Teach_GradeList = (List<ZK.Model.ZK_Grade>)ViewData["Teach_GradeList"];
                              foreach (var item in Teach_GradeList)
                              {%>
                            <option value="<%=item.gradeID %>">
                                <%=item.gradeName%></option>
                            <%}%>
                        </select>
                        <label style="color:Red;">*</label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        O 选择版本
                    </td>
                    <td style="width: 40px;">
                        |
                    </td>
                    <td>
                        <select id="S_Edition" style="width: 435px;">
                            <option value="-1">请选择版本</option>
                            <%List<ZK.Model.ZK_Edition> Teach_EditionList = (List<ZK.Model.ZK_Edition>)ViewData["Teach_EditionList"];
                              foreach (var item in Teach_EditionList)
                              {
                                  if (item.editionName == "人教版")
                                  {%> 
                            <option value="<%=item.editionID %>" selected="selected">
                                <%=item.editionName%></option>
                            <% 
}
                                  else
                                  {%>
                               <option value="<%=item.editionID %>" selected="selected">
                                <%=item.editionName%></option>
                             <% }
                              }
                               %>
                        </select>
                        (默认版本：人教版)</td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        O 属于课程
                    </td>
                    <td style="width: 40px;">
                        |
                    </td>
                    <td>
                        <input type="text" id="txt_Lesson" style="width: 430px;" />&nbsp;&nbsp;<a href="#"
                            onclick="CheckLesson()" class="green2">点此选择课程</a>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 80px;">
                        O 属于分类
                    </td>
                    <td style="width: 40px;">
                        |
                    </td>
                    <td>
                       <select id="Steachcate" style="width: 430px;">
                            <option value="-1">请选择分类</option>
                            <%List<ZK.Model.ZK_JXCategory> Teach_CategoryList = (List<ZK.Model.ZK_JXCategory>)ViewData["Teach_CategoryList"];
                              foreach (var item in Teach_CategoryList)
                              {%>
                            <option value="<%=item.CategoryId%>">
                                <%=item.CategoryName%></option>
                            <%}%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        O 关键字
                    </td>
                    <td style="width: 40px;">
                        |
                    </td>
                    <td rowspan="3">
                        <textarea style="width: 430px; height: 100px;" id="txt_KeyWord"></textarea>&nbsp;&nbsp;多个用空格或逗号隔开
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; margin:0; padding:0; vertical-align: top;">
                        O 常用关键字
                    </td>
                    <td style="width: 40px; vertical-align: top;">
                        |
                    </td>
                    <td align="left">
                        <div class="check" style="margin: 5px 0 0 0; font-size: 14px; color: #747474;
                            font-weight: bold;">
                            <%System.Data.DataTable listtags = (System.Data.DataTable)ViewData["listtags"];
                              foreach (System.Data.DataRow item in listtags.Rows)
                              {%>
                            <div class="box2" style="width: auto; height: 30px; text-align:left">
                                <a href="javascript:void(0);return false;" class="green2" onclick="AddNewKeyWord('<%=item["tagName"] %>')">
                                    <%=item["tagName"] %></a>
                            </div>
                            <%} %>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="clear: both">
            </div>
            <a href="javascript:void(0);return false;" onclick="Push_TeachFile()" class="btn3"
                style="margin-left: 220px; margin-bottom: 20px;">
                <img src="../../images/tijiao.png" />
            </a><a href="/Push/" class="btn3" style="margin-bottom: 20px;">
                <img src="../../images/chongzhi.png" /></a>
                <div style="width:100%; height:20px;"></div>
        </div>
    </div>
    <!-- 弹出层 -->
    <div class="bg" id="div_BackGround" style="display: none">
    </div>
    <div class="add2" id="div_LessonContent" style="display: none">
        <div class="add-top2">
            <p>
                选择课程
            </p>
            <img src="/images/close.png" class="close" style="cursor: pointer;" onclick="ReturnBack()" />
        </div>
        <div class="add-down">
            <div id="choice">
            </div>
            <a id="hidden_a"></a>
        </div>
        <div class="buttons">
            <a href="javascript:void(0);return false;" class="return2" onclick="SaveCheckedLesson()">
                <img src="../../images/button1.png" />
            </a><a href="javascript:void(0);return false;" class="return2" onclick="ReturnBack()">
                <img src="../../images/button2.png" />
            </a>
        </div>
    </div>
</asp:Content>
