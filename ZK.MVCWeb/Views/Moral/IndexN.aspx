<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    
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
            font-size: 14px;
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
        .indexCont .subjectList .subTitle
        {
            border-bottom-width: 3px;
            border-bottom-style: solid;
            border-bottom-color: #090;
            overflow: hidden;
            height: 35px;
        }
        .indexCont .subjectList .subTitle .subTLi
        {
            padding-right: 15px;
            padding-left: 15px;
            margin-left: 15px;
            float: left;
            background-color: #F3F3F3;
            font-family: "微软雅黑";
            font-size: 14px;
            -webkit-border-radius: 15px;
            -moz-border-radius: 15px;
            border-radius: 15px;
            line-height: 22px;
            cursor: pointer;
        }
        .indexCont .subjectList
        {
            padding-top: 8px;
            padding-bottom: 8px;
        }
        .indexCont .subjectList .subTitle .current
        {
            background-color: #063;
            color: #FFF;
        }
        .indexCont .subjectList .subCont
        {
            background-color: #F3F3F3;
        }
        .indexCont .subjectList .subCont .subCLi .subCLiCont .subCLiLi
        {
            margin-top: 5px;
            margin-left: 10px;
            float: left;
            border: 1px solid #9C3;
            background-color: #FFF;
        }
        .indexCont .subjectList .subCont .subCLi .subCLiCont
        {
            overflow: hidden;
        }
        .indexCont .subjectList .subCont .subCLi
        {
            padding-bottom: 15px;
            padding-top: 15px;
            display: none;
        }
        .indexCont .subjectList .subCont .current
        {
            display: block;
        }
        .indexCont .subjectList .subCont .subCLi.current .subCLiCont .subCLiLi a
        {
            padding-right: 10px;
            padding-left: 10px;
            line-height: 22px;
        }
        
        .indexCont .subjectList .subCont .subCLi .subCLiCont .subCLiLi .current
        {
            background-color: #063;
            color: #FFF;
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


            //资源选项卡
            $(".indexCont .mainCont .secMenu li").each(function (index) {
                $(this).click(function () {
                    //                    $(".indexCont .mainCont .secMenu li").removeClass("current");
                    //                    $(this).addClass('current');
                    //                    //$.ajax()...动态刷新内容
                    var liNode = $(this);
                    liNode.parent().parent().find(".secMenu li").removeClass('current');
                    liNode.parent().parent().find(".Resources li").removeClass('current');

                    liNode.addClass('current');
                    liNode.parent().parent().find(".Resources li").eq(index).addClass('current');

                    GetTypeOfFile(index);
                })
            });

            //排行榜
            $(".indexCont .subjectList .subTitle .subTLi").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().find(".subTLi").removeClass('current');
                    liNode.parent().parent().find(".subCont .subCLi").removeClass('current');

                    liNode.addClass('current');
                    $('.indexCont .subjectList .subCont .subCLi').eq(index).addClass('current');

                })
            })


            //            //分类选项卡
            //            $(".indexCont .subjectList .subCont .subCLi .subCLiCont .subCLiLi ").each(function (index) {
            //                $(this).click(function () {
            //                    var liNode = $(this);
            //                    liNode.parent().find(".subCLiLi").removeClass('current');
            //                    liNode.parent().parent().find(".subCLiCont  ").removeClass('current');
            //                    liNode.parent().parent().parent().find(".subCLi ").removeClass('current');

            //                    liNode.addClass('current');
            //                    $('.indexCont .subjectList .subCont .subCLi .subCLiCont .subCLiLi').eq(index).addClass('current');
            //                   
            //                })
            //            })

            //分类选项卡
            $(".indexCont .subjectList .subCont .subCLi .subCLiCont .subCLiLi").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().parent().parent().find(".subCLiLi").removeClass("current");
                    liNode.parent().parent().parent().parent().find(".subCont").removeClass("current");

                    liNode.addClass("current");
                    liNode.parent().parent().parent().addClass("current");

                });
            });

        });
    </script>
    <script type="text/javascript">
        var id = "all";
        var typeType = 0;
        var type = 0;

        function getchannel(cid) {
            document.getElementById("iframe_Filelist").src = "/Moral/MoralFileList?cid=" + cid + "&type=" + typeType;
            id = cid;
        }
        function GetTypeOfFile(type) {
            document.getElementById("iframe_Filelist").src = "/Moral/MoralFileList?cid=" + id + "&type=" + type;
            typeType = type;
        }

        function getMoralchannel(moralId) {
            document.getElementById("iframe_Filelist").src = "/Moral/MoralFileList?mid=" + moralId + "&type=" + typeType;
            id = moralId;
        }
        function getMoralAll() {
            document.getElementById("iframe_Filelist").src = "/Moral/MoralFileList?cid=all&type=0";
            $(".indexCont .mainCont .secMenu  li ").eq(1).removeClass('current');
            $(".indexCont .mainCont .secMenu  li ").eq(2).removeClass('current');
            $(".indexCont .mainCont .secMenu  li ").eq(3).removeClass('current');
            $(".indexCont .mainCont .secMenu  li ").eq(4).removeClass('current');
            $(".indexCont .mainCont .secMenu  li ").eq(0).addClass('current');    
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --德育资源频道
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="indexHead">
        <ul class="banner">
            <%--<li class="current">
                <img src="/ImagesN/Classification_banner01.jpg" width="1023" height="330" /></li>
            <li>
                <img src="/ImagesN/Classification_banner01.jpg" width="1023" height="330" /></li>--%>
            <%Dictionary<string, string> imgdic = (Dictionary<string, string>)ViewData["ppt_img"];
              if (imgdic != null)
              {
                  foreach (var item in imgdic)
                  {
                      int i = 0;
                      if (String.IsNullOrEmpty(item.Value) && String.IsNullOrEmpty(item.Key)&&i==0)                   
                      {
            %>
            <li class="current" style="text-align:center">
                <a href="<% =item.Value%>"><img src="<%=item.Key%>" style=" border: 0px; width:1024px;height:330px;margin:0;padding:0;" /></a></li>
            <%
                        }
                      else
                      {  
            %>
            <li style="text-align:center">
                <a href="<% =item.Value%>"><img src="<%=item.Key%>"  style=" border: 0px; width:1024px;height:330px;margin:0;padding:0;" /></a></li>
            <%         
                        }
                i++;
                  }
             
            %>
        </ul>
        <ul class="bannerDot">
            <%for (int i = 0; i < imgdic.Count; i++)
              {
                  if (i == 0)
                  { %>
            <li class="current" style="text-align:center"></li>
            <%}
                  else
                  {%>
            <li style="text-align:center"></li>
            <% }
              }
              }%>
        </ul>
    </div>
    <div class="indexCont">
        <div class="subjectList">
            <ul class="subTitle">
                <li class="subTLi current" id="all" onclick="getMoralAll();">全部</li>
                <%Dictionary<string, int> dic1 = (Dictionary<string, int>)ViewData["GroupNameList"];
                  foreach (var item in dic1)
                  {%>
                <%--<li class="subTLi " id="<%=item1.Value %>" onclick="getMoralchannel(this.id);">--%>
                <li class="subTLi " id="<%=item.Value%>" onclick="getMoralchannel(this.id);">
                    <%=item.Key %>
                </li>
                <% }
                %>
            </ul>
            <ul class="subCont">
                <li class="subCLi current">
                    <ul class="subCLiCont">
                        <li><span style="margin-left: 30px; font-size: 14px; font-family:微软雅黑; color: Green;">
                            注：显示所有的德育资源。</span></li>
                    </ul>
                </li>
                <%List<ZK.Model.ZK_ChannelGroup> morallist = (List<ZK.Model.ZK_ChannelGroup>)ViewData["GroupMoralNameList"];
                  ZK.Controllers.MoralController tc = new ZK.Controllers.MoralController();
                  foreach (var item1 in morallist)
                  {
                      List<ZK.Model.ZK_ChannelGroup> listLessons = tc.GetLessonByUnit(item1.channelGroupID.ToString());
                      if (listLessons.Count > 0)
                      {
                          foreach (var item in listLessons)
                          { %>
                <li class="subCLi ">
                    <ul class="subCLiCont ">
                        <li class="subCLiLi "><a id="<%=item.channelGroupID%>" onclick="getchannel(this.id);">
                            <%=item.channelGroupName%></a></li>
                    </ul>
                </li>
                <% }
                      }
                      else
                      { %>
                <li class="subCLi ">
                    <ul class="subCLiCont">
                        <li class="subCLiLi current"></li>
                    </ul>
                </li>
                <%}
                  }                   
                %>
            </ul>
        </div>
        <div class="mainCont">
            <ul class="secMenu">
                <li class="current">全部</li>
                <li>文档</li>
                <li>图片</li>
                <li>视频</li>
            </ul>
            <div class="menuFootLine">
            </div>
            <iframe id="iframe_Filelist" scrolling="no" src="/Moral/MoralFileList" frameborder="0"
                style="width: 100%; height: 800px;"></iframe>
        </div>
        <div class="rightCont">
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
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistDay.Rows[i]["fileID"] %>&url_flag=Moral" title="<%=v_allfilelistDay.Rows[i]["fileName"] %>">
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
                        <ul class="rankRowCont" style="height: 180px;">
                            <%System.Data.DataTable v_allfilelistWeek = (System.Data.DataTable)ViewData["v_allfilelistWeek"];
                              for (int i = 0; i < v_allfilelistWeek.Rows.Count; i++)
                              {
                            %>
                            <li class="rankRowContLi">
                                <div class="No">
                                    <%=i+1 %></div>
                                <div class="resTitle">
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistWeek.Rows[i]["fileID"] %>&url_flag=Moral" title="<%=v_allfilelistWeek.Rows[i]["fileName"] %>">
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
                                    <a href="/ContentPage/Index?file_id= <%=v_allfilelistMonth.Rows[i]["fileID"] %>&url_flag=Moral" title="<%=v_allfilelistMonth.Rows[i]["fileName"] %>">
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
