<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <link href="/CSS/index.css" rel="stylesheet" type="text/css" />--%>
    <link href="/css/common_push.css" rel="stylesheet" type="text/css" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --资源推送频道
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="main_wrap">
        <div class="box" style="width: 1024px; margin-left: auto; margin-right: auto; overflow: hidden;">
            <div class="title">
                <h2>
                    资源推送>教学资源推送</h2>
            </div>
            <div class="box_cnt">
                <div class="message_alert">
                    请选择要推送到频道，点击图标进行下一步：
                </div>
                <div class="box_ta">
                    <div class="box_one" style="border: 1px double #0C0; margin-left: 65px;">
                        <table class="tb_one">
                            <tr bordercolor="">
                                <td>
                                    <a href="/Push/Teach?<%=ViewData["url_Param"]%>">
                                        <img src="/images/jiaoxue.png" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <p>
                                        教学资源</p>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <p>
                                        摆脱事务工作困扰，专精教学业务</p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box_two" style="border: 1px double #0C0;">
                        <table class="tb_one">
                            <tr>
                                <td>
                                    <a href="/Push/Moral?<%=ViewData["url_Param"]%>">
                                        <img src="/images/deyu.png" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <p>
                                        德育资源</p>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <p>
                                        有章可循，有据可依的素质教育</p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box_three" style="border: 1px double #0C0">
                        <table class="tb_one">
                            <tr>
                                <td>
                                    <a href="/Push/Admin?<%=ViewData["url_Param"]%>">
                                        <img src="/images/xingzheng.png" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <p>
                                        行政资源</p>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <p>
                                        全面掌控，一站管理</p>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
