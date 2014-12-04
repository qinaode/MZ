<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZK.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --搜索
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .title_type a
        {
            color: rgb(192, 192, 192);
        }
        .title_type span
        {
            color: #275A8D; /* 初始化颜色*/
        }
    </style>
    <script type="text/javascript">
        var typeChannel = "jxpd";
        var typeType = "0";
        $(function () {
            var url = location.search;
            var typeParm = url.substr(5);
            if (typeParm == "") {
                typeChannel = "jxpd";
            }
            else {
                var typestr = typeParm.split('&');
                typeChannel = typestr[0];
                typeType = typestr[1].substr(5);
            }
            $(".title1").addClass("title_type");
            $(".title2").addClass("title_type");
            if (typeChannel == "") {
                $("#jxpd").removeClass("title_type");
            }
            else {
                $("#" + typeChannel).removeClass("title_type");
            }
            if (typeType != "0") {
                $("#" + typeType).removeClass("title_type");
                $("#" + typeType).css("color", "Red"); //选中颜色
            }
            $(".cur2").click(function () {
                $(".cur3").removeClass("current1");
                $(".cur4").removeClass("current1");
                $(".cur5").removeClass("current1");
                $(this).addClass("current1");
            });
        })

        function getchannel(cid) {
            window.location = "/Search/Index?cid=" + cid + "&type=0";
        }
        function GetTypeOfFile(type) {
            window.location = "/Search/Index?id=" + typeChannel + "&type=" + type;

        }                           
    </script>
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
    <script type="text/javascript">
        $(function () {
            $("table tr:odd").css("background", "#fff")
            $("table tr:even").css("background", "#f0f8fc")
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
            })
        })
    </script>
    <style type="text/css">
        .title_type a
        {
            color: rgb(192, 192, 192);
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <div class="warp1">
        <div class="down">
            <div class="pin">
                <p class="tit">
                    频道
                </p>
                <p class="resouce">
                    <% List<int> listcount = (List<int>)ViewData["TotalNum_TMA"]; %>
                    <span class="gray title1" id="jxpd" onclick="getchannel(this.id);"><a style="cursor: pointer;">
                        教学资源</a> <span class="red">
                            <%=listcount[0] %></span> </span><span class="gray title1" id="dypd" onclick="getchannel(this.id);">
                                <a style="cursor: pointer;">德育资源</a> <span class="red">
                                    <%=listcount[1] %></span> </span><span class="gray title1" id="xzpd" onclick="getchannel(this.id);">
                                        <a style="cursor: pointer">行政资源</a> <span class="red">
                                            <%=listcount[2] %></span> </span>
                </p>
                <p class="both title1">
                    <%List<int> listcount_type = (List<int>)ViewData["TotalNum_Type"]; %>
                    一共为您找到了
                    <%=listcount_type[0] %>
                    个资源，其中<%=listcount_type[2] %>个<span class="ico title2" id="T_Doc" onclick="GetTypeOfFile(this.id)"
                        style="cursor: pointer">文档</span>，
                    <%=listcount_type[3] %>个<span class="ico title2" id="T_Photo" onclick="GetTypeOfFile(this.id)"
                        style="cursor: pointer">图片</span>，
                    <%=listcount_type[1] %>个<span class="ico title2" id="T_Video" onclick="GetTypeOfFile(this.id)"
                        style="cursor: pointer">视频</span>，
                    <%=listcount_type[4] %>个<span class="ico title2" id="T_WAV" onclick="GetTypeOfFile(this.id)"
                        style="cursor: pointer">音频</span>，
                    <%=listcount_type[5] %>个<span class="ico title2" id="T_Other" onclick="GetTypeOfFile(this.id)"
                        style="cursor: pointer">其他</span>
                </p>
                <div class="way">
                    <p>
                        显示方式：</p>
                    <a href="#">
                        <img alt="" src="../../images/ico2.jpg" style="cursor: pointer" title="矩阵显示" /></a></div>
                <table width="100%" border="0">
                    <tbody>
                        <tr style="background-color: rgb(240, 248, 252); background-position: initial initial;
                            background-repeat: initial initial;">
                            <th width="10%" scope="col">
                                格式
                            </th>
                            <th width="35%" scope="col" align="left">
                                文档名称
                            </th>
                            <th width="15%" scope="col">
                                上传者
                            </th>
                            <th width="12%" scope="col">
                                下载量
                            </th>
                            <th width="20%" scope="col">
                                上传时间
                            </th>
                        </tr>
                        <%System.Data.DataSet dsCourse = (System.Data.DataSet)ViewData["Administrative_ResourcesList"]; %>
                        <%foreach (System.Data.DataRow item in dsCourse.Tables[0].Rows)
                          {%>
                        <tr>
                            <td>
                                <div id="Div3">
                                    <%--<%=item["fileTypeName"]%>--%>
                                    <div id="Div1">
                                        <%if (item["fileTypeName"].ToString() == "文档")
                                          { %>
                                        <img alt="" title="文档" src='/images/Icon/doc.png' style="margin-top: 5px; margin-bottom: 5px;"
                                            width="50px" height="50px" />
                                        <%} %>
                                        <%else if (item["fileTypeName"].ToString() == "图片")
                                            { %>
                                        <img alt="" title="图片" src='/images/Icon/img.png' style="margin-top: 5px; margin-bottom: 5px;"
                                            width="50px" height="50px" />
                                        <%} %>
                                        <%else if (item["fileTypeName"].ToString() == "视频")
                                            { %>
                                        <img alt="" title="视频" src='/images/Icon/video.png' style="margin-top: 5px; margin-bottom: 5px;"
                                            width="50px" height="50px" />
                                        <%} %>
                                        <%else if (item["fileTypeName"].ToString() == "音频")
                                            { %>
                                        <img alt="" title="音频" src='/images/Icon/audio.png' style="margin-top: 5px; margin-bottom: 5px;"
                                            width="50px" height="50px" />
                                        <%} %>
                                        <%else
                                            { %>
                                        <img alt="" title="其他" src='/images/Icon/other.jpg' style="margin-top: 5px; margin-bottom: 5px;"
                                            width="50px" height="50px" />
                                        <%} %>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div id="Div7" style="float: left;">
                                    <a target="_blank" href='/ContentPage/Index?file_id=<%=item["fileID"] %>&url_flag=Search'>
                                        <%=item["fileName"]%>
                                </div>
                            </td>
                            <td>
                                <div id="Div10">
                                    <%=item["USERNAME"]%>
                                </div>
                            </td>
                            <td>
                                <div id="Div11">
                                    <%=item["clicknum"]%>
                                </div>
                            </td>
                            <td>
                                <div id="Div12">
                                    <%=item["createTime"]%>
                                </div>
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
                <span class="link" style="float: right; width: 500px">
                    <%=Html.Pager(ViewData["page"].ToString(), int.Parse(ViewData["pagesize"].ToString()), int.Parse(ViewData["totlecount"].ToString()))%>
                    <%-- <%=Html.Pager("page", 10,100)%>--%>
                </span>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
