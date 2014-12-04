<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --首页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
   
    </style>
    <script type="text/javascript">
        $(function () {
            $(".nav li").mouseenter(function () {
                $(this).prev().find("span").css("display", "none");
                $(this).find("span").css("display", "none");
                $(this).find("a").css("padding-left", "43px");
                $(this).find("a").css("padding-right", "43px");
            });
            $(".cur a").mouseleave(function () {
                $(this).css("padding-left", "43px");
            });
            $(".nav li").mouseleave(function () {
                $(this).prev().find("span").css("display", "block");
                $(this).find("span").css("display", "block");
                $(this).find("a").css("padding-left", "35px");
                $(".cur").find("a").css("padding-left", "43px");
                $(this).find("a").css("padding-right", "35px");
            });
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
        })
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <div class="warp1">
        <div class="down">
            <div class="left">
                <div class="left-top">
                    <p class="tit">
                        精彩专题</p>
                        <%ZK.Model.ZK_FileJP modelFileJP = ViewData["jpResouce"] as ZK.Model.ZK_FileJP; %>
                    <img src="<%=modelFileJP.imageURL %>" style="cursor: pointer" onclick="location.href='/<%=modelFileJP.fileID %>'"
                        title="<%=modelFileJP.fileName %>" alt="<%=modelFileJP.fileName %>"/>
                    <div class="subject">
                        <p class="sub-tit" style="cursor: pointer" onclick="location.href='/nbc_schoolyard/index.php/search/getfileid?file_id={$topinfo.file_id}'">
                            {$topinfo['file_name']}<span><a href="/nbc_schoolyard/index.php/search/research2">更多</a></span></p>
                        <ul class="sub-list">
                                 <%List<ZK.Model.ZK_FileJP> modelFileJPList = ViewData["jpResouceList"] as List<ZK.Model.ZK_FileJP>;
                                   if (modelFileJPList!=null)
                                   {
                                       foreach (var item in modelFileJPList)
                                       { %>
                                <li><A href="/nbc_schoolyard/index.php/search/getfileid?file_id=<%=item.fileID %>">&nbsp<%=item.fileName%></A></li>
                    <%}
                                   }
                                   else
                                   {%>
                                   <li><A href="/nbc_schoolyard/index.php/search/getfileid?file_id=">•&nbsp</A></li>
                                   <%} %>

                        </ul>
                    </div>
                </div>
                <div class="left-down">
                    <div class="one">
                        <p class="tit">
                            <a href="/nbc_schoolyard/index.php/page/teach">{$channel1}</a></p>
                        <div class="learn">
                            <foreach name="file1" item="vo">     
                            <dl class="learn-dl" title='{$vo.file_name}'>
                                <dt><a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$vo.file_id}"><img src="{$vo.image}"/></a></dt>
                                <dd class="learn-tit"><a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$vo.file_id}">{$vo.file_name|msubstr=###,0,8}</a></dd>
                            </dl>
			</foreach>
                        </div>
                    </div>
                    <div class="two">
                        <p class="tit">
                            <a href="/nbc_schoolyard/index.php/page/moral">{$channel2}</a></p>
                        <div class="learn">
                            <foreach name="file2" item="vo">     
                            <dl class="learn-dl" title='{$vo.file_name}'>
                                <dt><a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$vo.file_id}"><img src="{$vo.image}" /></a></dt>
                                <dd class="learn-tit"><a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$vo.file_id}">{$vo.file_name|msubstr=###,0,8}</a></dd>
                            </dl>
			</foreach>
                        </div>
                    </div>
                    <div class="three">
                        <p class="tit">
                            <a href="/nbc_schoolyard/index.php/page/administration">{$channel3}</a></p>
                        <div class="learn">
                            <foreach name="file3" item="vo">     
                            <dl class="learn-dl" title='{$vo.file_name}'>
                                <dt><a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$vo.file_id}"><img src="{$vo.image}"/></a></dt>
                                <dd class="learn-tit"><a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$vo.file_id}">{$vo.file_name|msubstr=###,0,8}</a></dd>
                            </dl>
			</foreach>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="right-top">
                    <p>
                        当前已有<span>{$doc}</span>份文档</p>
                    <p>
                        当前已有<span>{$video}</span>份视频</p>
                    <p>
                        当前已有<span>{$pic}</span>份图片</p>
                </div>
                <div class="right-cen">
                    <p class="tit1">
                        文档排行榜</p>
                    <div class="tabs">
                        <ul id="lists">
                            <li class="lists1 current">本日</li>
                            <li class="lists2">本周</li>
                            <li class="lists3">本月</li>
                        </ul>
                        <div id="contents">
                            <ul class="contents1" style="display: block;">
                                <!-- 文档日-->
                                <foreach name="daily" item="vo"> 
                                    <foreach name="vo" item="list">
                                        <li>
                                            <a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$list.file_id}" class="list-a">{$list['iii']+1} </a>
                                            <span onclick='location.href="/nbc_schoolyard/index.php/search/getfileid?file_id={$list.file_id}"' title='{$list.file_name}'>{$list.file_name|msubstr=###,0,15}</span>
                                        </li>
                                    </foreach>
                                </foreach>
                            </ul>
                            <ul class="contents2">
                                <!-- 文档本周 -->
                                <foreach name="week" item="vo"> 
                                    <foreach name="vo" item="list">
                                        <li>
                                            <a class="list-a" href="/nbc_schoolyard/index.php/search/getfileid?file_id={$list.file_id}">{$list['iii']+1}</a>
                                            <span onclick='location.href="/nbc_schoolyard/index.php/search/getfileid?file_id={$list.file_id}"'title='{$list.file_name}'>{$list.file_name|msubstr=###,0,15}</span>
                                        </li>
                                    </foreach>
                                </foreach>
                            </ul>
                            <ul class="contents3">
                                <!-- 文档本月 -->
                                <foreach name="month" item="vo">
                                    <foreach name="vo" item="list">
                                        <li>
                                            <a href="/nbc_schoolyard/index.php/search/getfileid?file_id={$list.file_id}" class="list-a">{$list['iii']+1}</a>
                                            <span onclick='location.href="/nbc_schoolyard/index.php/search/getfileid?file_id={$list.file_id}"'title='{$list.file_name}'>{$list.file_name|msubstr=###,0,15}</span>
                                        </li>
                                    </foreach>
                                </foreach>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="right-down">
                    <p class="tit1">
                        用户贡献榜</p>
                    <div class="tabs">
                        <ul id="list">
                            <li class="list1 current">本周积分排行</li>
                            <li class="list2">总积分排行</li>
                        </ul>
                        <div id="content">
                            <ul class="content1" style="display: block;">
                                <!-- 用户本周 -->
                                <li><span class="name">用户名</span><span class="type">经验值</span></li>
                                <li><a class="list-a" href="#">1</a>dafadf<span class="type">243</span></li>
                            </ul>
                            <ul class="content2">
                                <!-- 用户总积分-->
                                <li><span class="name">用户名</span><span class="type">经验值</span></li>
                                <li><a class="list-a" href="#">1</a>fdgfg<span class="type">243</span></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
