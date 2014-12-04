<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --资源推送
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <div class="down">
        <p class="tit3">
            <span class="inb">资源推送 > </span><span class="ino">选择频道 </span>
        </p>
        <div class="picture">
            <p class="tit4">
                请选择要推送的频道，点击图标进行下一步：</p>
            <dl class="pic-dl">
                <dt><a href="/Push/Teach?<%=ViewData["url_Param"] %>">
                    <img src="/images/tu6.jpg" /></a></dt>
                <dd>
                    <a href="/Push/Teach?<%=ViewData["url_Param"] %>">教学频道</a></dd>
            </dl>
            <dl class="pic-dl">
                <dt><a href="/Push/Moral?<%=ViewData["url_Param"] %>">
                    <img src="/images/tu7.jpg" /></a></dt>
                <dd>
                    <a href="/Push/Moral?<%=ViewData["url_Param"] %>">德育频道</a></dd>
            </dl>
            <dl class="pic-dl">
                <dt><a href="/Push/Admin?<%=ViewData["url_Param"] %>">
                    <img src="/images/tu8.jpg" /></a></dt>
                <dd>
                    <a href="/Push/Admin?<%=ViewData["url_Param"] %>">行政频道</a></dd>
            </dl>
        </div>
    </div>
</asp:Content>
