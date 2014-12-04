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
            height: 343px;
            clear: both;
            margin-top: 8px;
        }
        .indexHead .banner
        {
            width: 1024px;
            height: 330px;
            overflow: hidden;
            float:left;
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
            background-image: url(/Images/other/Nongxy_18.jpg);
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
            background-image: url(/Images/other/Index_49.png);
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
            margin-top: 10px;
            overflow: hidden;
        }
        .indexCont .mainCont
        {
            float: left;
            width: 690px;
            position: relative;
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
            background-image: url(/Images/other/Classification_05.jpg);
            background-repeat: no-repeat;
            height: 31px;
            width: 96px;
            margin-right: 2px;
            line-height: 31px;
            color: #666;
            font-family: "微软雅黑";
            font-size: 14px;
        }
        .indexCont .mainCont .secMenu li a
        {
        }
        .indexCont .mainCont .secMenu .current
        {
            background-image: url(/Images/other/Classification_03.gif);
            background-repeat: no-repeat;
        }
        .indexCont .mainCont .secMenu .current a
        {
           <%-- color: #000000;--%>
            background-color: #616264;
        }
        .indexCont .mainCont .Resources li
        {
            padding: 8px;
            width: 90px;
            border: 1px solid #EFEFEF;
            float: left;
            margin-top: 10px;
            margin-bottom: 10px;
            margin-left: 25px;
        }
        .indexCont .mainCont .Resources li:hover
        {
            background-color: #F5F5F5;
        }
        .indexCont .mainCont .Resources li .resTitle
        {
            line-height: 20px;
            font-family: "微软雅黑";
            margin-top: 5px;
            font-size: 12px;
            color: #069;
        }
        .indexCont .mainCont .Resources li .resAttr
        {
            line-height: 30px;
            color: #999;
            overflow: hidden;
        }
        .indexCont .mainCont .Resources li .resAttr #resPage
        {
            float: left;
            margin-left: 5px;
        }
        .indexCont .mainCont .Resources li .resAttr #payStatus
        {
            float: right;
            margin-right: 5px;
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
 
        
        .indexCont .classMenu
        {
            position: relative;
            margin-top: 25px;
            background-color: #999;
            border: 1px solid #333;
            margin-bottom: 10px;
            height: 150px;
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
        }
        .indexCont .classMenu dt
        {
            background-image: url(/Images/other/Classification_subTit.gif);
            background-repeat: no-repeat;
            height: 38px;
            width: 248px;
            position: absolute;
            z-index: 2;
            top: -25px;
            left: 5px;
        }
        .indexCont .classMenu dt .t
        {
            line-height: 35px;
            font-family: "微软雅黑";
            font-size: 16px;
            color: #FFF;
            padding-left: 30px;
            float: left;
            width: 100px;
            letter-spacing: 3px;
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
            line-height: 50px;
            float: left;
            width: 251px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-right-color: #CCC;
            border-bottom-color: #CCC;
        }
        .indexCont .classMenu dd .t
        {
            padding-left: 25px;
            font-family: "微软雅黑";
            font-size: 14px;
            color: #FFF;
            letter-spacing: 3pt;
            float: left;
        }
        .indexCont .classMenu dd #subjectSum
        {
            float: left;
            padding-right: 15px;
            padding-left: 15px;
            border: 1px solid #E6E6E6;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            line-height: 20px;
            margin-top: 15px;
            margin-left: 15px;
            color: #E6E6E6;
            font-family: "微软雅黑";
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
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            //搜索标签
            $(".indexTop .searchArea .docs li").each(function (index) {
                $(this).click(function () {
                    $(".indexTop .searchArea .docs li").removeClass("current");
                    $(this).addClass('current');
                    $("#searchWord").focus();
                })
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


            //排行榜
            $(".indexCont .rightCont .ranking .rankMenu li").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().find(".rankMenu li").removeClass('current');
                    liNode.parent().parent().find(".rankRow").removeClass('current');

                    liNode.addClass('current');
                    $('.indexCont .rightCont .ranking .rankCont .rankRow').eq(index).addClass('current');
                })
            })

            //分类选项卡
            $(".indexCont .classMenu dd   ").each(function (index) {
                $(this).click(function () {
                    $(".indexCont .classMenu dd ").removeClass("current");
                    $(this).addClass('current');

                })
            })

            //选项卡
            $(".indexCont .mainCont .secMenu  li").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().find(".secMenu li").removeClass('current');
                    liNode.parent().parent().find(".Resources li").removeClass('current');

                    liNode.addClass('current');
                    liNode.parent().parent().find(".Resources li").eq(index).addClass('current');

                    GetTypeOfFile(index);

                })
            })
        });
    </script>
    <script type="text/javascript">
        var id = "all";
        var typeType = 0;

        function getchannel(cid) {
            document.getElementById("iframe_Filelist").src = "/Administration/AdministrationFileList?cid=" + cid + "&type=" + typeType;
            id = cid;
        }
        function GetTypeOfFile(type) {

            document.getElementById("iframe_Filelist").src = "/Administration/AdministrationFileList?cid=" + id + "&type=" + type;
            typeType = type;
            //            $("#iframe_Filelist").attr("src", "/Administration/AdministrationFileList?cid=" + id + "&type=" + type);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --行政资源频道
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="indexCont">
        <dl class="classMenu">
            <dt>
                <div class="t">
                    行政资源</div>
                <div id="subjectSum">
                    <% =ViewData["totlecount"]%></div>
            </dt>
            <%Dictionary<int, string> dic = (Dictionary<int, string>)ViewData["GroupNameList"];
              Dictionary<int, int> xzList = (Dictionary<int, int>)ViewData["ChannelGroupCount"];
              foreach (var item1 in dic)
              {%>
            <dd>
                <a id="<%=item1.Key %>" onclick="getchannel(this.id);">
                    <div class="t">
                        <%=item1.Value%></div>
                    <%foreach (var item in xzList)
                      {
                          if (item1.Key == item.Key)
                          {%>
                    <div id="subjectSum">
                        <%=item.Value%>
                    </div>
                    <%continue;
                          }
                      } %>
                </a>
            </dd>
            <% }
            %>
        </dl>
        <div class="mainCont">
            <%--<ul class="secMenu">
                <li class="current"><a id="all" onclick="GetTypeOfFile(this.id);">全部</a></li>
                <li><a id="doc" onclick="GetTypeOfFile(this.id);">文档</a></li>
                <li><a id="img" onclick="GetTypeOfFile(this.id);">图片</a></li>
                <li><a id="video" onclick="GetTypeOfFile(this.id);">视频</a></li>
            </ul>--%>
            <ul class="secMenu">
                <li class="current" style="cursor: pointer;">全部</li>
                <li style="cursor: pointer;">文档</li>
                <li style="cursor: pointer;">图片</li>
                <li style="cursor: pointer;">视频</li>
            </ul>
            <div class="menuFootLine">
            </div>
            <iframe id="iframe_Filelist" scrolling="no" src="/Administration/AdministrationFileList"
                frameborder="0" style="width: 100%; height: 800px;"></iframe>
        </div>
        <div class="rightCont">
            <div class="ranking">
                <div class="rankTit">
                    文档排行榜</div>
                <ul class="rankMenu">
                    <li class="current" style="cursor: pointer;">本日</li>
                    <li style="cursor: pointer;">本周</li>
                    <li style="cursor: pointer;">本月</li>
                </ul>
                <ul class="rankCont">
                    <li class="rankRow current">
                        <ul class="rankRowCont" style="height: 180px;">
                            <%System.Data.DataTable v_allfilelistDay = (System.Data.DataTable)ViewData["v_allfilelistDay"];
                              for (int i = 0; i < v_allfilelistDay.Rows.Count; i++)
                              {
                            %>
                            <li class="rankRowContLi">
                                <div class="No">
                                    <%=i+1 %></div>
                                <div class="resTitle">
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistDay.Rows[i]["fileID"] %>&url_flag=Admin" title="<%=v_allfilelistDay.Rows[i]["fileName"] %>">
                                      <%if (v_allfilelistDay.Rows[i]["fileName"].ToString().Length > 20)
                                         {%>
                                            <%=v_allfilelistDay.Rows[i]["fileName"].ToString().Substring(0, 20)%>...<%}
                                           else%>
                                               <%=v_allfilelistDay.Rows[i]["fileName"] %>
                                       </a></div>
                            </li>
                            <%
}
                            %>
                        </ul>
                    </li>
                    <li class="rankRow">
                        <ul class="rankRowCont" style="height: 180px;">
                            <%System.Data.DataTable v_allfilelistWeek = (System.Data.DataTable)ViewData["v_allfilelistWeek"];
                              for (int i = 0; i < v_allfilelistWeek.Rows.Count; i++)
                              { 
                            %>
                            <li class="rankRowContLi">
                                <div class="No">
                                    <%=i+1 %></div>
                                <div class="resTitle">
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistWeek.Rows[i]["fileID"] %>&url_flag=Admin" title="<%=v_allfilelistWeek.Rows[i]["fileName"] %>">
                                         <%if (v_allfilelistWeek.Rows[i]["fileName"].ToString().Length > 20)
                                         {%>
                                            <%=v_allfilelistWeek.Rows[i]["fileName"].ToString().Substring(0, 20)%>...<%}
                                           else%>
                                               <%=v_allfilelistWeek.Rows[i]["fileName"]%>
                                    </a>
                                </div>
                            </li>
                            <%
}
                            %>
                        </ul>
                    </li>
                    <li class="rankRow">
                        <ul class="rankRowCont" style="height: 180px;">
                            <%System.Data.DataTable v_allfilelistMonth = (System.Data.DataTable)ViewData["v_allfilelistMonth"];
                              for (int i = 0; i < v_allfilelistMonth.Rows.Count; i++)
                              {
                            %>
                            <li class="rankRowContLi">
                                <div class="No">
                                    <%=i+1 %></div>
                                <div class="resTitle">
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistMonth.Rows[i]["fileID"] %>&url_flag=Admin" title="<%=v_allfilelistMonth.Rows[i]["fileName"] %>">
                                         <%if (v_allfilelistMonth.Rows[i]["fileName"].ToString().Length > 20)
                                         {%>
                                            <%=v_allfilelistMonth.Rows[i]["fileName"].ToString().Substring(0, 20)%>...<%}
                                           else%>
                                               <%=v_allfilelistMonth.Rows[i]["fileName"]%>
                                    </a>
                                </div>
                            </li>
                            <%
}
                            %>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="ranking">
                <%Html.RenderAction("UserResCountRank", "Home"); %>
            </div>
        </div>
    </div>
</asp:Content>
