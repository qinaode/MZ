<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .indexHead
        {
            width: 1024px;
            margin-right: auto;
            margin-left: auto;
            position: relative;
            overflow: hidden;
            height: 322px;
            clear: both;
            margin-top: 8px;
        }
        .indexHead .banner
        {
            width: 1024px;
            height: 330px;
            overflow: hidden;
        }
        .indexHead .newsList
        {
            float: right;
            width: 330px;
            margin-top: 8px;
        }
        .indexHead .bannerDot
        {
            width: 400px;
            left: -61px;
            bottom: -2px;
            position: absolute;
            height: 40px;
            margin-left: 350px;
        }
        .indexHead .bannerDot li
        {
            background-color: #000;
            float: left;
            height: 20px;
            width: 60px;
            margin-left: 10px;
            filter: alpha(opacity=30); /*IE滤镜，透明度50%*/
            -moz-opacity: 0.3; /*Firefox私有，透明度50%*/
            opacity: 0.3; /*其他，透明度50%*/
            border: 1px solid #FFF;
        }
        .indexHead .bannerDot .current
        {
            background-color: #F00;
        }
        .indexHead .newsList dt
        {
            font-family: "微软雅黑";
            font-size: 20px;
            line-height: 40px;
            font-weight: bold;
        }
        .indexHead .newsList .wordLine
        {
            line-height: 25px;
            background-image: url(/ImagesN/Nongxy_18.jpg);
            background-repeat: no-repeat;
            background-position: 5px 12px;
            text-indent: 15px;
            font-family: "微软雅黑";
            font-size: 14px;
        }
        .indexHead .newsList dd .pageCount
        {
            color: #CCC;
        }
        .indexHead .newsList .action
        {
            line-height: 30px;
            border-top-width: 1px;
            border-top-style: dashed;
            border-top-color: #CCC;
            margin-top: 10px;
        }
        .indexHead .newsList .action #more
        {
            float: left;
            width: 50px;
            margin-left: 15px;
        }
        .indexHead .newsList .action #turnPage
        {
            background-image: url(/ImagesN/Index_49.png);
            background-repeat: no-repeat;
            background-position: 5px 12px;
            text-indent: 20px;
            float: right;
            margin-right: 15px;
        }
        .indexCont
        {
            width: 1024px;
            margin-right: auto;
            margin-left: auto;
            margin-top: 5px;
            overflow: hidden;
        }
        .indexCont .mainCont
        {
            float: right;
            width: 690px;
        }
        .indexCont .mainCont .part1
        {
            position: relative;
            overflow: hidden;
        }
        .indexCont .mainCont .part2
        {
            position: relative;
            overflow: hidden;
            margin-top: 10px;
        }
        .indexCont .mainCont .secMenu
        {
            overflow: hidden;
            height: 31px;
            position: absolute;
            left: 0px;
            top: 0px;
            z-index: 2;
        }
        .indexCont .mainCont .secMenu li
        {
            text-align: center;
            float: left;
            background-color: #F4FAFF;
            background-image: url(/ImagesN/Classification_05.jpg);
            background-repeat: no-repeat;
            height: 31px;
            width: 96px;
            margin-right: 2px;
            line-height: 31px;
            color: #666;
            font-family: "微软雅黑";
            font-size: 14px;
            cursor: pointer;
        }
        .indexCont .mainCont .secMenu li a
        {
        }
        .indexCont .mainCont .secMenu .current
        {
            background-image: url(/ImagesN/Classification_03.gif);
            background-repeat: no-repeat;
        }
        .indexCont .mainCont .secMenu .current a
        {
            color: #000000;
        }
        
        .indexCont .mainCont .Resources
        {
            overflow: hidden;
            margin-top: 40px;
        }
        .indexCont .rightCont
        {
            float: right;
            width: 320px;
        }
        .indexCont .rightCont .resCounter
        {
            line-height: 28px;
            font-family: "微软雅黑";
            font-size: 14px;
            text-align: center;
            background-color: #F4FAFF;
            border: 1px solid #E8F4FF;
        }
        .indexCont .rightCont .resCounter .v
        {
            color: #F30;
            font-size: 18px;
        }
        .indexCont .rightCont .ranking
        {
            padding-top: 10px;
        }
        .indexCont .rightCont .ranking .rankTit
        {
            font-family: "微软雅黑";
            font-size: 16px;
            line-height: 30px;
            width: 100px;
            border-bottom-width: 2px;
            border-bottom-style: solid;
            border-bottom-color: #060;
            text-align: center;
        }
        .indexCont .rightCont .ranking .rankMenu
        {
            line-height: 30px;
            overflow: hidden;
            margin-top: 10px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #D7D8D9;
        }
        .indexCont .rightCont .ranking .rankMenu li
        {
            width: 106px;
            float: left;
            text-align: center;
            font-family: "微软雅黑";
            font-size: 16px;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #FFF;
        }
        .indexCont .rightCont .ranking .rankMenu .current
        {
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-top-color: #D7D8D9;
            border-right-color: #D7D8D9;
            border-bottom-color: #D7D8D9;
            border-left-color: #D7D8D9;
            background-color: #D7D8D9;
            color: #FFF;
        }
        .indexCont .rightCont .ranking .rankCont
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #D7D8D9;
            border-bottom-color: #D7D8D9;
            border-left-color: #D7D8D9;
        }
        .indexCont .rightCont .ranking .rankCont .rankRow .rankRowCont .rankRowContLi
        {
            line-height: 30px;
            font-family: "微软雅黑";
            font-size: 14px;
            overflow: hidden;
            padding-left: 5px;
            margin-bottom: 5px;
        }
        .indexCont .rightCont .ranking .rankCont .rankRow .rankRowCont .rankRowContLi .No
        {
            height: 20px;
            width: 20px;
            background-color: #999;
            float: left;
            text-align: center;
            line-height: 20px;
            margin-top: 5px;
            color: #FFF;
        }
        .indexCont .rightCont .ranking .rankCont .rankRow .rankRowCont .rankRowContLi .resTitle
        {
            text-indent: 5px;
            float: left;
        }
        .indexCont .rightCont .ranking .rankCont .rankRow
        {
            display: none;
        }
        .indexCont .rightCont .ranking .rankCont .current
        {
            display: block;
        }
        
        .indexCont .classMenu
        {
            position: relative;
            margin-top: 10px;
            background-color: #999;
            border: 1px solid #333;
            margin-bottom: 10px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: #666 0px 0px 5px;
            -moz-box-shadow: #666 0px 0px 5px;
            box-shadow: #666 0px 0px 5px;
            width: 1012px;
            margin-right: auto;
            margin-left: auto;
            padding-left: 3px;
            height: 60px;
        }
        .indexCont .classMenu dt
        {
        }
        .indexCont .classMenu dt .t
        {
            line-height: 60px;
            font-family: "微软雅黑";
            font-size: 14px;
            font-weight: bold;
            color: #FFF;
            padding-left: 10px;
            float: left;
            width: 100px;
            letter-spacing: 3px;
            background-color: Gray;
        }
        .indexCont .classMenu dt #subjectSum
        {
            float: left;
            font-family: "微软雅黑";
            line-height: 20px;
            margin-top: 9px;
            margin-left: 10px;
            padding-right: 8px;
            padding-left: 8px;
            border: 1px solid #515151;
            background-color: #64B10E;
            color: #FFF;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            -webkit-box-shadow: #666 0px 0px 5px;
            -moz-box-shadow: #666 0px 0px 5px;
            box-shadow: #666 0px 0px 5px;
        }
        .indexCont .classMenu dd
        {
            float: left;
            width: 175px;
            border-right-color: #CCC;
            border-bottom-color: #CCC;
            cursor: pointer;
        }
        .indexCont .classMenu dd .t
        {
            padding-left: 25px;
            font-family: "微软雅黑";
            font-size: 14px;
            color: #FFF;
            letter-spacing: 3pt;
            float: left;
            line-height: 30px;
        }
        .indexCont .classMenu dd #subjectSum
        {
            float: left;
            border: 1px solid #E6E6E6;
            height: 12px;
            width: 12px;
            margin-top: 6px;
            margin-left: 12px;
            line-height: 12px;
            background-color: #FFF;
        }
        .indexCont .mainCont .menuFootLine
        {
            height: 6px;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #71ac67;
            overflow: hidden;
            background-color: #71af82;
            position: absolute;
            left: 0px;
            top: 30px;
            width: 690px;
            z-index: 1;
        }
        .indexCont .classMenu .noRightLine
        {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #999;
        }
        .indexCont .classMenu .current a .t
        {
            color: #DAFF68;
        }
        .indexCont .classMenu .current a #subjectSum
        {
            color: #DAFF68;
        }
        .indexCont .classMenu .current .t
        {
            color: #DF7;
        }
        .indexCont .classMenu .current #subjectSum
        {
            background-image: url(/ImagesN/yes.gif);
            background-repeat: no-repeat;
            background-color: #FFF;
            background-position: center center;
        }
        .indexCont .mainCont .part1 .Resources li
        {
            display: none;
            font-family: "微软雅黑";
            font-size: 16px;
            padding: 5px;
            line-height: 28px;
        }
        .indexCont .mainCont .part1 .Resources .current
        {
            display: block;
        }
        .indexCont .mainCont .part2 .Resources li
        {
            display: none;
        }
        .indexCont .mainCont .part2 .Resources .current
        {
            display: block;
        }
        .indexCont .mainCont .part2 .sequence
        {
            position: absolute;
            top: 5px;
            right: 5px;
        }
        .indexCont .mainCont .part2 .Resources li
        {
            padding: 5px;
            overflow: hidden;
            background-color: #f4f9ff;
            margin-bottom: 6px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectImg
        {
            float: left;
            padding-right: 5px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont
        {
            float: left;
            width: 470px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCount
        {
            float: left;
            padding: 10px;
            line-height: 25px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont .subTitle
        {
            font-family: "微软雅黑";
            font-size: 16px;
            line-height: 28px;
            color: #366;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont .summary .vTit
        {
            font-weight: bold;
            line-height: 25px;
            font-size: 14px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont .summary .vCont
        {
            font-size: 14px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont .author
        {
            line-height: 25px;
            color: #C2C2C2;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont .author .vTit
        {
            font-weight: bold;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCont .author .vCont
        {
            margin-right: 10px;
        }
        .indexCont .mainCont .part2 .Resources li .subjectCount .popCount
        {
            font-family: "微软雅黑";
            font-size: 18px;
            color: #900;
        }
        .indexCont .unitList
        {
            float: left;
            width: 300px;
            border: 1px solid #71AF82;
            padding: 10px;
        }
        .indexCont .unitList .unitLi
        {
            background-image: url(/ImagesN/subject_07.jpg);
            background-repeat: repeat-y;
            background-position: 11px;
        }
        .indexCont .unitList .unitLi .unitCont .unitTitle
        {
            background-image: url(/ImagesN/subject_03.jpg);
            background-repeat: no-repeat;
            padding-left: 50px;
            line-height: 37px;
            font-family: "微软雅黑";
            font-size: 16px;
        }
        .indexCont .unitList .unitLi .shrinkTit
        {
            background-image: url(/ImagesN/subject_03.jpg);
            background-repeat: no-repeat;
            padding-left: 50px;
            line-height: 37px;
            font-family: "微软雅黑";
            font-size: 16px;
            display: block;
            height: 40px;
        }
        .indexCont .unitList .unitLi .unitCont .articleLi
        {
            padding-left: 70px;
            line-height: 30px;
            font-family: "微软雅黑";
            font-size: 14px;
        }
        .indexCont .unitList .unitLi.current .unitCont .unitTitle
        {
            background-image: url(/ImagesN/subject_10.jpg);
        }
        .indexCont .unitList .unitLi.current .unitCont .articleLi
        {
            background-image: url(/ImagesN/subject_15.jpg);
            background-repeat: repeat-y;
            background-position: 11px;
        }
        .indexCont .unitList .unitLi.current .unitCont .articleLi.current a
        {
            color: #8DC51C;
        }
        .indexCont .unitList .unitLi.current .unitCont .unitTitle a
        {
            color: #8AC51F;
        }
        .indexCont .unitList .unitLi .unitCont
        {
            display: none;
        }
        .indexCont .unitList .unitLi.current .shrinkTit
        {
            display: none;
        }
        .indexCont .unitList .unitLi.current .unitCont
        {
            display: block;
        }
        
        
    </style>
    <link href="../../css/Teach_Common.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/kindeditor-4.1.5/kindeditor.js" type="text/javascript"></script>
    <script src="../../Scripts/kindeditor-4.1.5/lang/zh_CN.js" type="text/javascript"></script>
    <script src="../../Scripts/kindeditor-4.1.5/plugins/code/prettify.js" type="text/javascript"></script>
    <script src="../../js/Teach_Index.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //搜索标签
            $(".indexTop .searchArea .docs li").each(function (index) {
                $(this).click(function () {
                    $(".indexTop .searchArea .docs li").removeClass("current");
                    $(this).addClass('current');
                    $("#searchWord").focus();
                });
            });

            //banner
            $(".indexHead .bannerDot li").each(function (index) {
                $(this).click(function () {
                    $(".indexHead .bannerDot li").removeClass("current");
                    $(this).addClass("current");

                    $(".indexHead .banner li").fadeOut(0);
                    $(".indexHead .banner li").eq(index).fadeIn(200);

                })
            });

            //循环
            setInterval(function () {
                var currentObj = $(".indexHead .banner li:visible");
                var currentObj_next = $(".indexHead .banner li:visible").next();

                var currentObj_s = $(".indexHead .bannerDot li.current");
                var currentObj_next_s = $(".indexHead .bannerDot li.current").next();

                currentObj.fadeOut(0);
                currentObj_s.removeClass("current");

                if (currentObj_next.is(":last")) {
                    currentObj_next = $(".indexHead .banner li:first");
                    currentObj_next_s = $(".indexHead .bannerDot li:first");
                }

                currentObj_next.fadeIn(200);
                currentObj_next_s.addClass("current");

            }, 5000) //每12秒钟切换一条，你可以根据需要更改


            //选项卡
            $(".indexCont .mainCont .part1 .secMenu li").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().find(".secMenu li").removeClass('current');
                    liNode.parent().parent().find(".Resources li").removeClass('current');

                    liNode.addClass('current');
                    liNode.parent().parent().find(".Resources li").eq(index).addClass('current');
                })
            })
            $(".indexCont .mainCont .part2 .secMenu li").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().find(".secMenu li").removeClass('current');
                    liNode.parent().parent().find(".Resources li").removeClass('current');

                    liNode.addClass('current');
                    liNode.parent().parent().find(".Resources li").eq(index).addClass('current');
                })
            })

            //目录选择
            $(".indexCont .unitList .unitLi .unitCont .articleLi").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().parent().find(".articleLi").removeClass("current");
                    liNode.parent().parent().parent().find(".unitLi").removeClass("current");

                    liNode.addClass("current");
                    liNode.parent().parent().addClass("current");
                });
            });
            $(".indexCont .unitList .unitLi .shrinkTit").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().find(".articleLi").removeClass("current");
                    liNode.parent().parent().find(".unitLi").removeClass("current");
                    liNode.parent().addClass("current");
                });
            });

            //subject select
            $(".indexCont .classMenu dd").each(function (index) {
                $(this).click(function () {
                    if ($(this).hasClass("current")) {
                        $(this).removeClass("current");
                    } else {
                        var ddNode = $(this);
                        ddNode.parent().find(".current").removeClass("current");
                        $(this).addClass("current");
                    }
                });
            });

        });
        //        function GetParamtersAndTurn() {
        //            var cid = $(".Current_Course").attr("id");
        //            var gid = $(".Current_Grade").attr("id");
        //            var eid = $(".Current_Edition").attr("id");
        //            cid = cid.substr(9);
        //            gid = gid.substr(9);
        //            eid = eid.substr(9);
        //            window.location = "/Teach/Index?id=C" + cid + "_G" + gid + "_E" + eid;

        //        }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --教学资源频道
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="indexCont">
        <dl class="classMenu">
            <dt>
                <div class="t">
                    选择课程:</div>
            </dt>
            <%List<ZK.Model.ZK_Course> listCourse = (List<ZK.Model.ZK_Course>)ViewData["Teach_CourseList"]; %>
            <%for (int i = 0; i < listCourse.Count; i++)
              {
                  if (i == 0)
                  {%>
            <dd class="current CourseList Current_Course" id="TeachCou_<%=listCourse[i].courseID %>">
                <div id="subjectSum" class="CourseCheck">
                </div>
                <div class="t">
                    <%=listCourse[i].courseName %></div>
            </dd>
            <%    
                   }
                  else if (i % 4 == 0)
                  {  %>
            <dd class="noRightLine CourseList" id="TeachCou_<%=listCourse[i].courseID %>">
                <div class="CourseCheck" id="subjectSum">
                </div>
                <div class="t">
                    <%=listCourse[i].courseName %></div>
            </dd>
            <%
}
                  else
                  {
            %>
            <dd class="CourseList" id="TeachCou_<%=listCourse[i].courseID %>">
                <div class="CourseCheck" id="subjectSum">
                </div>
                <div class="t">
                    <%=listCourse[i].courseName %></div>
            </dd>
            <%
                }
              }
            %>
        </dl>
        <dl class="classMenu">
            <dt>
                <div class="t">
                    选择年级:</div>
            </dt>
            <%List<ZK.Model.ZK_Grade> listGrade = (List<ZK.Model.ZK_Grade>)ViewData["Teach_GradeList"]; %>
            <%for (int j = 0; j < listGrade.Count; j++)
              {
                  if (j == 0)
                  {%>
            <dd class="current GradeList Current_Grade" id="TeachGra_<%=listGrade[j].gradeID %>">
                <div id="subjectSum">
                </div>
                <div class="t">
                    <%=listGrade[j].gradeName%></div>
            </dd>
            <%    
}
                  else if (j % 4 == 0)
                  {  %>
            <dd class="noRightLine GradeList" id="TeachGra_<%=listGrade[j].gradeID %>">
                <div id="subjectSum">
                </div>
                <div class="t">
                    <%=listGrade[j].gradeName%></div>
            </dd>
            <%
                }
                  else
                  {
            %>
            <dd class="t GradeList" id="TeachGra_<%=listGrade[j].gradeID %>">
                <div id="subjectSum">
                </div>
                <div class="t">
                    <%=listGrade[j].gradeName%></div>
            </dd>
            <%
                }
              } %>
        </dl>
        <dl class="classMenu">
            <dt>
                <div class="t">
                    选择版本:</div>
            </dt>
            <%List<ZK.Model.ZK_Edition> listEdition = (List<ZK.Model.ZK_Edition>)ViewData["Teach_EditionList"]; %>
            <%for (int k = 0; k < listEdition.Count; k++)
              {
                  if (k == 0)
                  {%>
            <dd class="current EditionList Current_Edition" id="TeachEdi_<%=listEdition[k].editionID %>">
                <div id="subjectSum">
                </div>
                <div class="t">
                    <%=listEdition[k].editionName%></div>
            </dd>
            <%    
}
                  else if (k % 4 == 0)
                  {  %>
            <dd class="noRightLine EditionList" id="TeachEdi_<%=listEdition[k].editionID %>">
                <div id="subjectSum">
                </div>
                <div class="t">
                    <%=listEdition[k].editionName%></div>
            </dd>
            <%
}
                  else
                  {
            %>
            <dd class="t EditionList" id="TeachEdi_<%=listEdition[k].editionID %>">
                <div id="subjectSum">
                </div>
                <div class="t">
                    <%=listEdition[k].editionName%></div>
            </dd>
            <%
                }
              } %>
        </dl>
        <div style=" clear:both;"></div>
        <!---树形列表start-->
       <% List<ZK.Model.ZK_Lesson> AllLessonList = (List<ZK.Model.ZK_Lesson>)ViewData["AllLessonList"];%>
          
          <%
           List<ZK.Model.ZK_Lesson> listunits = (List<ZK.Model.ZK_Lesson>)ViewData["Teach_UnitList"];
           if (listunits.Count > 0)
           {
               ZK.Controllers.TeachController tc = new ZK.Controllers.TeachController();
            %>
            <ul class="unitList "><li class="unitLi current"  onclick="ShowAllLesson()"><div class="shrinkTit"><a href="#" >全部</a></div> <dl class="unitCont">
                    <dt class="unitTitle"><a href="#neo">
                        全部</a></dt></dl></li>
            <%if (listunits.Count > 0)
              {
                  int f = 0;
                  foreach (var item in listunits)
                  {
                      f++;
                      if (f == 1)
                      {  
            %>
            <li class="unitLi">
                <div class="shrinkTit">
                    <a href="#neo">
                        <%=item.lessonName%></a></div>
                <dl class="unitCont">
                    <dt class="unitTitle"><a href="#neo">
                        <%=item.lessonName%></a></dt>
                    <% List<ZK.Model.ZK_Lesson> listLessons = tc.GetLessonByUnit(item.lessonID.ToString());
                       if (listLessons != null && listLessons.Count > 0)
                       {
                           for (int l = 0; l < listLessons.Count; l++)
                           {
                               if (f == 1 && l == 0)
                               {         
                    %>
                    <dd class="articleLi">
                        <a href="#neo" id="lessonid_<%=listLessons[l].lessonID %>" class="LessonList" onclick="GetInfoByLessonID(<%=listLessons[l].lessonID %>)">
                            <%=listLessons[l].lessonName%></a></dd>
                    <%
}
                               else
                               {
                    %>
                    <dd class="articleLi">
                        <a href="#neo" id="lessonid_<%=listLessons[l].lessonID %>" class="LessonList" onclick="GetInfoByLessonID(<%=listLessons[l].lessonID %>)">
                            <%=listLessons[l].lessonName%></a></dd>
                    <%
}
                           } %>
                </dl>
            </li>
            <%
}
                      }
                      else
                      {
            %>
            <li class="unitLi">
                <div class="shrinkTit">
                    <a href="#neo">
                        <%=item.lessonName%></a></div>
                <dl class="unitCont">
                    <dt class="unitTitle"><a href="#neo">
                        <%=item.lessonName%></a></dt>
                    <% List<ZK.Model.ZK_Lesson> listLessons = tc.GetLessonByUnit(item.lessonID.ToString());
                       if (listLessons != null && listLessons.Count > 0)
                       {
                           for (int l = 0; l < listLessons.Count; l++)
                           {
                               if (f == 1 && l == 0)
                               {         
                    %>
                    <dd class="articleLi">
                        <a href="#neo" id="A1" class="LessonList" onclick="GetInfoByLessonID(<%=listLessons[l].lessonID %>)">
                            <%=listLessons[l].lessonName%></a></dd>
                    <%
}
                               else
                               {
                    %>
                    <dd class="articleLi">
                        <a href="#neo" id="A2" class="LessonList" onclick="GetInfoByLessonID(<%=listLessons[l].lessonID %>)">
                            <%=listLessons[l].lessonName%></a></dd>
                    <%
}
                           }

                       }
                       else
                       { %>
                           <dd class="articleLi"></dd>
                       <%}%>
                </dl>
            </li>
            
                      
                      <% }
                  }
              }
            %>
        </ul>
    
        <!---树形列表end-->
        <div class="mainCont">
            <div class="part1">
                <ul class="secMenu">
                    <li class="current">教学目标</li>
                    <li>教学重点</li>
                    <li>教学难点</li>
                </ul>
                <div class="menuFootLine">
                </div>
                <ul class="Resources" style="float:left;">
                    <li class="current">
                        <table width="100%" height="100%" style="float:left; border:0px; margin:0;padding:0;">
                            <tr style=" width:100%;" ><%--教学目标显示--%>
                                <td style="width: 90%">
                                <div style="word-break: break-all; word-wrap: break-word;"> 
                                        <p name="aim" id="p_TeachMB" style="width:100%"> 
                                        </p> 
                                    </div>
                                    <div style="display:none; width: 100%" id="div_TeachMB">
                                        <textarea id="txt_TeachMB" style="width:100%; height: 30px"></textarea>
                                    </div>
                                </td>
                                <td align="center">
                                    <div style="background-repeat: no-repeat; float:right; width:100px; text-align: center;">
                                        <a id="Edit_TeachMB" href="javascript:void(0);return false;" style="display: none;
                                            color: #39a60d;" onclick="EditLessonInfo(this.id)">编辑</a></div>
                                </td>
                            </tr>
                        </table>
                    </li>
                    <li>
                        <table width="100%" height="100%">
                            <tr>
                                <td style="width:90%">
                                <div style="word-break: break-all; word-wrap: break-word;"> 
                                    <p name="keynote" id="p_TeachZD">
                                    </p>
                                    </div>
                                    <div style="display: none" id="div_TeachZD">
                                        <textarea id="txt_TeachZD" style="width: 100%; height: 30px"></textarea></div>
                                </td>
                                <td align="center">
                                    <div style="color: #39a60d; no-repeat; width: 100px; text-align: center;">
                                        <a id="Edit_TeachZD" href="javascript:void(0);return false;" style="display: none;
                                            color: #39a60d;" onclick="EditLessonInfo(this.id)">编辑</a></div>
                                </td>
                            </tr>
                        </table>
                    </li>
                    <li>
                        <table width="100%" height="100%">
                            <tr>
                                <td style="width:90%">
                                <div style="word-break: break-all; word-wrap: break-word;"> 
                                    <p name='difficulty' id="p_TeachND">
                                    </p>
                                    </div>
                                    <div style="display: none" id="div_TeachND">
                                        <textarea id="txt_TeachND" style="width: 100%; height: 30px"></textarea></div>
                                </td>
                                <td align="center">
                                <div style="clear:both;"></div>
                                    <div style="background-repeat: no-repeat; width: 100px; text-align: center;">
                                        <a id="Edit_TeachND" href="javascript:void(0);return false;" style="display: none;
                                            color: #39a60d;" onclick="EditLessonInfo(this.id)">编辑</a></div>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
            <div class="part2">
                <ul class="secMenu">
                    <li class="current" onclick="fileTypeChanged('0')">全部</li>
                    <li onclick="fileTypeChanged('1')">教案</li>
                    <li onclick="fileTypeChanged('2')">课件</li>
                    <li onclick="fileTypeChanged('3')">习题</li>
                </ul>
                <div class="sequence">
                    <select name="" onchange="Orderby(this);" >
                        <option value="0" >按热度排序</option>
                        <option value="1" >按最近排序</option>
                    </select></div>
                <div class="menuFootLine">
                </div>
                <iframe id="iframe_Filelist" scrolling="no" frameborder="0" style="width:100%; height: 800px;">
                </iframe>
            </div>
        </div>
        <%}
           else
           {%>
        <p style="font-size: 20px; line-height: 40px; font-weight: bold; text-align: center;">
            暂无相关信息</p>
        <% }%>
    </div>
</asp:Content>
