<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Resources{ padding-bottom:10px;}
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
            width: 687px;
            height: 343px;
            overflow: hidden;
            float: left;
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
            left: 0px;
            bottom: 0px;
            position: absolute;
            height: 40px;
            padding-left: 200px;
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
            background-image: url(Images/Nongxy_18.jpg);
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
            background-image: url(Images/Index_49.png);
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
            width: 688px;
            border: 1px solid #D7D8D9;
        }
        .indexCont .mainCont .secMenu
        {
            background-color: #ececec;
            overflow: hidden;
            height: 50px;
        }
        .indexCont .mainCont .secMenu li
        {
            text-align: center;
            float: left;
            background-color: #ececec;
            line-height: 43px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            margin: 0px;
            padding: 3px 5px 2px 5px;
        }
        .indexCont .mainCont .secMenu .current a
        {
        }
        .indexCont .mainCont .secMenu li a
        {
            color: #666;
            font-family: "微软雅黑";
            font-size: 16px;
        }
        .indexCont .mainCont .secMenu li a
        {
            padding-right: 61px;
            padding-left: 61px;
        }
        .indexCont .mainCont .secMenu .current
        {
            background-color: #6ca144;
        }
        .indexCont .mainCont .secMenu .current a
        {
            color: #fff;
        }
        .indexCont .mainCont .Resources li
        {
            padding: 8px;
            width: 90px;
            height: 130px;
            line-height: 130px;
            float: left;
            margin-top: 8px;
            margin-bottom: 8px;
            margin-left: 25px;
            border: 1px solid #ECECEC;
        }
        .indexCont .mainCont .Resources li:hover
        {
            background-color: #F5F5F5;
        }
        .indexCont .mainCont .Resources li .resTitle
        {
            line-height: 20px;
            font-family: "微软雅黑";
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
            margin-left: 15px;
        }
        .indexCont .mainCont .Resources li .resAttr #payStatus
        {
            float: right;
            margin-right: 5px;
        }
        .indexCont .mainCont .Resources
        {
            overflow: hidden;
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
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "Post",
                url: "/Home/getByType",
                data: { "typeID": 0 },
                datatype: "json/text",
                success: function (backdata) {

                    $(".Resources").html("");
                    if (backdata == "") {
                        return;
                    }

                    $.each(backdata, function (i, item) {
                        if (i < 20) {
                            //Resources首页中资源列表显示
                            //var strItem = " 	<li style=\"text-align:center\"><div class=\"resImage\"><a  href='/ContentPage/Index?file_id=" + item["fileID"] + "&url_flag=Home'><img src=" + item["imageURL"] + " style='width:48px;height:48px; border:0px;' /></a></div>";
                            //                            strItem = strItem + "<div class=\"resTitle\"><a  href='/ContentPage/Index?file_id=" + item["fileID"] + "&url_flag=Home'>" + item["fileName"].substring(0, 40) + "</a></div>";
                            var strItem = "<li style=\"text-align:center\"><div><a  href='/ContentPage/Index?file_id=" + item["fileID"] + "&url_flag=Home'><div class=\"resImage\"><img src=" + item["imageURL"] + " style='width:48px;height:48px; border:0px;' /></div>";
                            if (item["fileName"].toString().length > 12) {
                                strItem = strItem + "<div class=\"resTitle\">" + item["fileName"].substring(0, 12) + "...</div></a></div>";
                            }
                            else
                                strItem = strItem + "<div class=\"resTitle\">" + item["fileName"] + "</div></a><div>";


                            strItem = strItem + "<div class=\"resAttr\"><span id=\"resPage\">" + item["ownerID"] + "</span><span id=\"payStatus\"></span></div> </li>";
                            $(".Resources").html($(".Resources").html() + strItem);
                        }
                        else {
                            return;
                        }

                    })
                }
            });

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

            //资源选项卡
            $(".indexCont .mainCont .secMenu li").each(function (index) {
                $(this).click(function () {
                    $(".indexCont .mainCont .secMenu li").removeClass("current");
                    $(this).addClass('current');

                    $.ajax({
                        type: "Post",
                        url: "/Home/getByType",
                        data: { "typeID": index },
                        datatype: "json/text",
                        success: function (backdata) {

                            $(".Resources").html("");
                            if (backdata == "") {
                                return;
                            }

                            $.each(backdata, function (i, item) {
                                if (i < 20) {
                                    //                                    alert(item["imageURL"]);
                                    //var strItem = " 	<li style=\"text-align:center\"><div class=\"resImage\"><a href='/ContentPage/Index?file_id=" + item["fileID"] + "&url_flag=Home'><img src=" + item["imageURL"] + "  style='width:48px;height:48px; border:0px;' /></a></div>";
                                    var strItem = "<li style=\"text-align:center\"><div><a href='/ContentPage/Index?file_id=" + item["fileID"] + "&url_flag=Home'><div class=\"resImage\"><img src=" + item["imageURL"] + "  style='width:48px;height:48px; border:0px;' /></div>";
                                    if (item["fileName"].toString().length > 12) {
                                        strItem = strItem + "<div class=\"resTitle\">" + item["fileName"].substring(0, 12) + "...</div><a></div>";
                                    }
                                    else
                                        strItem = strItem + "<div class=\"resTitle\">" + item["fileName"] + "</div><a></div>";

                                    strItem = strItem + "<div class=\"resAttr\"><span id=\"resPage\">" + item["USERNAME"] + "</span><span id=\"payStatus\"></span></div> </li>";
                                    $(".Resources").html($(".Resources").html() + strItem);
                                }
                                else {
                                    return;
                                }
                            })
                        }
                    });
                })
            });

            Get_filelistRank("ul_weekrank", "week");
            Get_filelistRank("ul_monthrank", "month");

        });
        //////////////获取周排行榜/////////
        function Get_filelistRank(divid, flag) {
            $.ajax({
                url: "/Home/GetRankList",
                type: "Post",
                datatype: "text/json",
                data: { "flag": flag },
                success: function (backdata) {
                    var JsonData = $.parseJSON(backdata);
                    $("#" + divid).html("");
                    var html = "";

                    $.each(JsonData["DataList"], function (i, item) {
                        html = html + '<li class="rankRowContLi"><div class="No">' + i + 1 + '</div>';
                        html = html + '<div class="resTitle">';
                        html = html + '<a href="/ContentPage/Index?file_id=' + item["fileID"] + '&url_flag=Home" title="' + item["fileName"] + '">';
                        if (item["fileName"].toString().length > 20) {
                            html = html + item["fileName"].toString().substring(0, 20) + "...";
                        }
                        else {
                            html = html + item["fileName"];
                        }
                        html = html + '  </a> </div> </li>';

                    });

                    $("#" + divid).html(html);
                }
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --首页
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="indexHead">
        <ul class="banner">
            <%List<ZK.Model.ZK_FileJPPic> imgList = (List<ZK.Model.ZK_FileJPPic>)ViewData["imgList"];%>
            <%foreach (var item in imgList)
              {
                  
            %>
            <li class="current"><a href="<%=item.imageURL%>">
                <img src="<%=item.imageName%>" width="687" height="343" /></a></li>
            <% }
            %>

        </ul>
        <ul class="bannerDot">
            <% int imgI = 1;
               foreach (var item in imgList)
               {
                   if (imgI == 1)
                   {%>
            <li class="current"></li>
            <%}
                   else
                   {%>
            <li></li>
            <%}
                   imgI++;
               } %>
            <%-- <li class="current"></li>
            <li></li>--%>
        </ul>
        <dl class="newsList">
            <dt><a href="#neo">
                <%=ViewData["JPTitle"] %></a></dt>
            <%List<ZK.Model.ZK_FileJP> modelFileJPList = ViewData["jpResouceList"] as List<ZK.Model.ZK_FileJP>;
              if (modelFileJPList != null && modelFileJPList.Count > 0)
              {
                  for (int i = 0; i < 10; i++)
                  {
                      if (modelFileJPList.Count <= i)
                      {
                          break;
                      }
            %>
            <dd class="wordLine">
                <a href="/ContentPage/Index?file_id=<%=modelFileJPList[i].fileID %>&url_flag=Home">
                    <%=modelFileJPList[i].fileName%></a> <span class="pageCount">
                        <%=modelFileJPList[i].fileType %></span></dd>
            <%}
              }%>
            <dd class="action">
                <a href="#neo" id="more"></a>
                <div id="turnPage">
                    <a href="/SpecialTopic/Index?typeId=<%=ViewData["JPTypeID"]%>"><span style="font-size: 12px;
                        font-family:微软雅黑;">更多>></span> </a>
                </div>
            </dd>
        </dl>
    </div>
    <div class="indexCont">
        <div class="mainCont">
            <ul class="secMenu">
                <li class="current"><a href="#neo">全部分类</a></li>
                <li><a href="#neo">教学</a></li>
                <li><a href="#neo">德育</a></li>
                <li><a href="#neo">行政</a></li>
            </ul>
            <ul class="Resources">
            </ul>
        </div>
        <div class="rightCont">
            <div class="resCounter" align="center">
                <table class="tb_Count">
                    <tr>
                        <td style="width: 30px;">
                            <img src="/imagesN/film.png" />
                        </td>
                        <td>
                            当前已有
                        </td>
                        <td>
                            <span class="v">
                                <%=ViewData["video"]%></span>
                        </td>
                        <td>
                            个视频
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px">
                            <img src="/imagesN/doc.png" />
                        </td>
                        <td>
                            当前已有
                        </td>
                        <td>
                            <span class="v">
                                <%=ViewData["doc"]%></span>
                        </td>
                        <td>
                            份文档
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px">
                            <img src="/imagesN/pic.png" />
                        </td>
                        <td>
                            当前已有
                        </td>
                        <td>
                            <span class="v">
                                <%=ViewData["pic"]%></span>
                        </td>
                        <td>
                            张图片
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px">
                            <img src="/imagesN/music.png" />
                        </td>
                        <td>
                            当前已有
                        </td>
                        <td>
                            <span class="v">
                                <%=ViewData["audio"]%></span>
                        </td>
                        <td>
                            张音频
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px">
                            <img src="/imagesN/other.png" />
                        </td>
                        <td>
                            当前已有
                        </td>
                        <td>
                            <span class="v">
                                <%=ViewData["other"]%></span>
                        </td>
                        <td>
                            个其他资源
                        </td>
                    </tr>
                </table>
            </div>
            <div class="ranking">
                <div class="rankTit">
                    资源排行榜</div>
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
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistDay.Rows[i]["fileID"] %>&url_flag=Home"
                                        title="<%=v_allfilelistDay.Rows[i]["fileName"] %>">
                                        <%if (v_allfilelistDay.Rows[i]["fileName"].ToString().Length > 20)
                                          {%>
                                        <%=v_allfilelistDay.Rows[i]["fileName"].ToString().Substring(0, 20)%>...<%}
                                          else%>
                                        <%=v_allfilelistDay.Rows[i]["fileName"] %>
                                    </a>
                                </div>
                            </li>
                            <%
}
                            %>
                        </ul>
                    </li>
                    <li class="rankRow">
                        <ul class="rankRowCont" style="height: 180px;" id="ul_weekrank">
                        </ul>
                    </li>
                    <li class="rankRow">
                        <ul class="rankRowCont" style="height: 180px;" id="ul_monthrank">
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="ranking">
                <%Html.RenderAction("UserResCountRank", "Home"); %></div>
        </div>
    </div>
</asp:Content>
