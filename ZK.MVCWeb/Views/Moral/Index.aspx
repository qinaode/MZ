<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --德育资源
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        var index2 = 1;
        var scrollLoop = function () {
            if (index2 == 5) {
                index2 = 1;
                $("#slideshow").scrollLeft(0);
            }
            $("#slideshow").stop().animate({ scrollLeft: 712 * index2 }, 1000);

            $("#slideshow-nav li a").removeClass("active");
            $("#slideshow-nav li:eq(" + index2 % 4 + ") a").addClass("active");
            index2++;
        }

        var t = setInterval(function () { scrollLoop() }, 2000);



        $("#slideshow li").each(function () {    //划过停止  离开继续
            $(this).mouseenter(function () {
                clearInterval(t);
            }).mouseleave(function () {
                t = setInterval(function () { scrollLoop() }, 2000);
            })
        })

        $("#slideshow-nav li").each(function (index) {   //鼠标点击切换图片
            $(this).click(function () {
                clearInterval(t);
                $(this).parent().find("a").removeClass("active");
                $(this).find("a").addClass("active");
                $("#slideshow").stop().animate({ scrollLeft: 712 * index }, 1000);
                index2 = index;
                setTimeout(function () {
                    t = setInterval(function () { scrollLoop() }, 2000);
                }, 10);
                return false;  //防止a链接跳转

            });

        })

        
    </script>
    <script>
        $(function () {

            $(".list1").click(function () {
                $(".list2").removeClass("current");
                $(".content2").css("display", "none");
                $(this).addClass("current");
                $(".content1").css("display", "block");
            });
            $(".list2").click(function () {
                $(".list1").removeClass("current");
                $(".content1").css("display", "none");
                $(this).addClass("current");
                $(".content2").css("display", "block");
            });
            $(".lists1").click(function () {
                $(".lists2").removeClass("current");
                $(".contents2").css("display", "none");
                $(".lists3").removeClass("current");
                $(".contents3").css("display", "none");
                $(this).addClass("current");
                $(".contents1").css("display", "block");
            });
            $(".lists2").click(function () {
                $(".lists1").removeClass("current");
                $(".contents1").css("display", "none");
                $(".lists3").removeClass("current");
                $(".contents3").css("display", "none");
                $(this).addClass("current");
                $(".contents2").css("display", "block");
            });
            $(".lists3").click(function () {
                $(".lists1").removeClass("current");
                $(".contents1").css("display", "none");
                $(".lists2").removeClass("current");
                $(".contents2").css("display", "none");
                $(this).addClass("current");
                $(".contents3").css("display", "block");
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <div class="warp1">
        <div class="center">
            <p class="tit2">
                <span class="inb">知识管理></span><span class="ino">德育资源</span><!--<span class="inr">分享</span><span class="ine">收藏到书签</span>--></p>
            <div class="banner">
                <div class="banner-left">
                    <p class="abs">
                        <span>全部德育资源</span><br />
                        <%=ViewData["TotalFileNum"].ToString()%></p>
                    <div style="margin-top: 50px;">
                        <ul class="list" style="overflow: auto; margin-top: 0; padding-top: 0px;">
                            <volist> 
                        <%Dictionary<string, int> dic = (Dictionary<string, int>)ViewData["GroupNameList"];
                          Dictionary<string, string> dic2 = (Dictionary<string, string>)ViewData["GroupDescList"];
                          foreach (var item1 in dic)
                          {
                              if (item1.Key != "德育分类")
                              {
                              %>
                              <li><a><%=item1.Key%></a><span>(<%=item1.Value%>)</span></li>
                          <%}
                          }%>
                        
                        </volist>
                            <%--<div class="btn">
                        创建德育活动</div>--%>
                        </ul>
                    </div>
                </div>
                <div class="banner-right">
                    <div id="slideshow" class="main">
                        <ul class="slides">
                            <volist>
                                <li ><a >
                                    <img src="/images/banner1.jpg" />a标签</a></li>

                                <li ><a >
                                    <img src="/images/banner2.jpg" />a标签</a></li>

                                <li ><a >
                                    <img src="/images/banner3.jpg" />a标签</a></li>
                                       <li ><a >
                                    <img src="/images/banner4.jpg" />a标签</a></li>
                            </volist>
                        </ul>
                    </div>
                    <div class="main">
                        <div id="slideshow-nav">
                            <ul>
                                <volist>
                                <li><a href="#lam1" class="active"></a></li>
                                <li><a href="#lam2"></a></li>
                                <li><a href="#lam3"></a></li>
                                <li><a href="#lam4"></a></li>
                                 </volist>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="down">
            <div class="left1">
                <div class="left-down1">
                    <%List<ZK.Model.View_OtherFileList> v_otherlist = (List<ZK.Model.View_OtherFileList>)ViewData["v_otherfilelist"];
                      foreach (var item in dic)
                      {
                          if (item.Key == "德育分类")
                          {
                              continue;
                          } %>
                    <volist name="list">
                    <div class="two1" style="overflow:auto; height:100%;">
                         
                              <p class="tit">
                            <%=item.Key %>&nbsp;&nbsp;（<%=dic2[item.Key] %>）  </p> 
                            <div class="learn1">
                             <volist id='val' offset ='0' length='6'>
                            <%int a = 1;
                              foreach (var item2 in v_otherlist)
                              {

                                  if (a > 12)
                                  {
                                      break;
                                  }
                                  a++;
                                  if (item2.cateName == item.Key)
                                  {%>
                               
                                <dl class="learn-dl1">
                                    <dt><a href="/ContentPage/Index?file_id=<%=item2.fileID %>&url_flag=Moral"><img src="<%=item2.imageURL %>" style="width:95px;" /></a></dt>
                                    <dd class="learn-tit1"><a href="/ContentPage/Index?file_id=<%=item2.fileID %>&url_flag=Moral"><%=item2.fileName %></a></dd>
                                </dl>
                             
                                  <%}
                              } %> 
                              </volist>                        
                       </div>
                       </div>
                    </volist>
                    <%} %>
                </div>
            </div>
            <div class="right1">
                <div class="right-cen">
                    <p class="tit1">
                        文档排行榜
                    </p>
                    <div class="tabs">
                        <ul id="lists">
                            <li class="lists1 current">本日</li>
                            <li class="lists2">本周</li>
                            <li class="lists3">本月</li>
                        </ul>
                        <div id="contents">
                            <ul class="contents1" style="display: block;">
                                <volist >
                                <%List<ZK.Model.View_AllFileList> v_allfilelistDay = (List<ZK.Model.View_AllFileList>)ViewData["v_allfilelistDay"];
                                  int i = 0;
                                  foreach (var item in v_allfilelistDay)
                                  {
                                      i++;
                                      if (i <= 8)
                                      {
                                      %>
                                      <li><a href="#"><%=i%></a><span onclick="javascript:window.location.href='/ContentPage/Index?file_id=<%=item.fileID %>&url_flag=Moral'"><%=item.fileName%></span></li>
                                  <%}
                                      else
                                      {
                                          break;
                                      }
                                  }%>

                                    
                                </volist>
                            </ul>
                            <ul class="contents2">
                                <volist >
                                 <%List<ZK.Model.View_AllFileList> v_allfilelistWeek = (List<ZK.Model.View_AllFileList>)ViewData["v_allfilelistWeek"];
                                   int j = 0;
                                   foreach (var item in v_allfilelistWeek)
                                   {
                                       j++;
                                       if (j <= 8)
                                       {
                                      %>
                                      <li><a href="#"><%=j%></a><span onclick="javascript:window.location.href='/ContentPage/Index?file_id=<%=item.fileID %>&url_flag=Moral'"><%=item.fileName%></span></li>
                                  <%}
                                       else
                                       {
                                           break;
                                       }
                                   }%>
                                </volist>
                            </ul>
                            <ul class="contents3">
                                <volist name="monthlist['data']" id='vo'>
                                  <%List<ZK.Model.View_AllFileList> v_allfilelistMonth = (List<ZK.Model.View_AllFileList>)ViewData["v_allfilelistMonth"];
                                    int k = 0;
                                    foreach (var item in v_allfilelistMonth)
                                    {
                                        k++;
                                        if (k <= 8)
                                        {
                                      %>
                                      <li><a href="#"><%=k%></a><span onclick="javascript:window.location.href='/ContentPage/Index?file_id=<%=item.fileID %>&url_flag=Moral'"><%=item.fileName%></span></li>
                                  <%}
                                        else
                                        {
                                            break;
                                        }
                                    }%>
                                </volist>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="right-down">
                    <p class="tit1">
                        用户贡献榜
                    </p>
                    <div class="tabs">
                        <ul id="list">
                            <li class="list1 current">本周积分排行</li>
                            <li class="list2">总积分排行</li>
                        </ul>
                        <div id="content">
                            <ul class="content1" style="display: block;">
                                <li><span class="name">用户名</span><span class="type">经验值</span></li>
                                <volist name="datalist['data']" id='vo' offset="0" length='8'>
                                 <%Dictionary<string, int> dicTotalCountWeek = (Dictionary<string, int>)ViewData["dicForCountWeek"];
                                   int m = 0;
                                   foreach (var item in dicTotalCountWeek)
                                   {
                                       m++;
                                       if (m <= 8)
                                       {
                                      %>
                                  
                                       <li><a href="#"><%=m%></a><span onclick='location.href=""'><%=item.Key %></span><span class="type"><%=item.Value %></span></li>
                                  <%}
                                   }
                                     %>
                                </volist>
                            </ul>
                            <ul class="content2">
                                <li><span class="name">用户名</span><span class="type">经验值</span></li>
                                <volist name="datalist['data']" id='vo' offset="0" length='8'>
                                 <%Dictionary<string, int> dicTotalCount = (Dictionary<string, int>)ViewData["dicForCount"];
                                   int l = 0;
                                   foreach (var item in dicTotalCount)
                                   {
                                       l++;
                                       if (l <= 8)
                                       {
                                      %>
                                  
                                       <li><a href="#"><%=l %></a><span onclick='location.href=""'><%=item.Key %></span><span class="type"><%=item.Value %></span></li>
                                  <%}
                                   }
                                     %>
                                </volist>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
        <!--        <div class="foot">
            <p>版权所有：北京阳光伟业科技发展有限公司   Copyright© 2013</p>
        </div>-->
    </div>
</asp:Content>
