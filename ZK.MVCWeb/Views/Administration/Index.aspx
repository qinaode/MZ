<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZK.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --行政资源
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
    <script type="text/javascript">
        $(function () {
            var url = location.search;
            var typeid = url.substr(4);
            $(".title1").addClass("title_type");
            if (typeid == "") {
                $("#jxpd").removeClass("title_type");
            }
            else {
                $("#" + typeid).removeClass("title_type");
            }
            $(".cur2").click(function () {
                $(".cur3").removeClass("current1");
                $(".cur4").removeClass("current1");
                $(".cur5").removeClass("current1");
                $(this).addClass("current1");
            });


        })
        function getchannel(id) {

            window.location = "/Search/Index?id=" + id;

        }
       
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <div class="warp1">
        <div class="down">
            <div class="left">
                <p class="tit3">
                    <span class="inb">知识管理></span><span class="ino">行政资源</span>
                </p>
                <div class="teach">
                    <p class="tit">
                        行政资源
                    </p>
                    <p class="resouce1">
                        <volist name='channelgrouplist' id='val' offset ='0' length='5'>
                        <%Dictionary<int, string> dic = (Dictionary<int, string>)ViewData["GroupNameList"];
                          foreach (var item1 in dic)
                          {%>
                            <span class="gray1"><a href="/administration/Index?channelGroup_ID=<%=item1.Key %>"><%=item1.Value%></a></span>
                        <% }
                          %> 
                   </volist>
                    </p>
                    <%--<p class="resouce1">
                        <volist name='channelgrouplist' id='vo'>
                                    <span class="gray1">
                                          <a href="/Search/Index?ty="Nomal">工作规范及要求</a>
                                          <a href="/Search/Index?ty="pub" style="margin-left:45px;">信息公开</a>
                                          <a href="/Search/Index?ty="active" style="margin-left:45px;">活动资料</a>
                                          <a href="/Search/Index?ty="pubmag" style="margin-left:45px;">公开资料</a>
                                          <a href="/Search/Index?ty="concert" style="margin-left:45px;">办公会资料</a>
                                   </span>
                            </volist>
                    </p>--%>
                </div>
                <div class="erro">
                    <p>
                        文档呈现方式以文档或者文辑方式出现。</p>
                </div>
                <div class="tab">
                    <ul class="cur">
                        <li class="current">全部资料</li>
                        <%--<li class="cur1">资料夹</li>--%>
                    </ul>
                </div>
                <table width="100%" border="0">
                    <tr>
                        <td style="display: none;">
                            id
                        </td>
                        <th width="10%" scope="col">
                            格式
                        </th>
                        <th align="left" width="35%" scope="col">
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
                        <td style="display: none;">
                            <%=item["fileID"]%>
                        </td>
                        <td>
                            <div id="Div7">
                                <%--  <%=item["fileTypeName"]%>--%>
                                <div id="Div3">
                                    <%if (item["fileTypeName"].ToString() == "文档")
                                      { %>
                                    <img alt="" title="文档" src='/images/Icon/doc.png' style="margin-top:5px; margin-bottom:5px;" width="50px" height="50px" />
                                    <%} %>
                                    <%else if (item["fileTypeName"].ToString() == "图片")
                                        { %>
                                    <img alt="" title="图片" src='/images/Icon/img.png' style="margin-top:5px; margin-bottom:5px;" width="50px" height="50px" />
                                    <%} %>
                                    <%else if (item["fileTypeName"].ToString() == "视频")
                                        { %>
                                    <img alt="" title="视频" src='/images/Icon/video.png' style="margin-top:5px; margin-bottom:5px;" width="50px" height="50px" />
                                    <%} %>
                                    <%else if (item["fileTypeName"].ToString() == "音频")
                                        { %>
                                    <img alt="" title="音频" src='/images/Icon/audio.png' style="margin-top:5px; margin-bottom:5px;" width="50px" height="50px" />
                                    <%} %>
                                    <%else
                                        { %>
                                    <img alt="" title="其他" src='/images/Icon/other.jpg' style="margin-top:5px; margin-bottom:5px;" width="50px" height="50px" />
                                    <%} %>
                                </div>
                            </div>
                        </td>
                        <td align="left">
                            <div id="Div1" style="float: left;">
                                <a href="/ContentPage/Index?file_id=<%=item["fileID"] %>&url_flag=Admin">
                                    <%=item["fileName"]%></a>
                            </div>
                        </td>
                        <td>
                            <div id="Div10">
                                <%=item["USERNAME"]%>
                            </div>
                        </td>
                        <td>
                            <div id="Div11">
                                <%=item["clickNum"]%>
                            </div>
                        </td>
                        <td>
                            <div id="Div12">
                                <%=item["createTime"]%>
                            </div>
                        </td>
                    </tr>
                    <%} %>
                </table>
                <span class="link" style="float: right; width: 400px">
                    <%=Html.Pager(ViewData["page"].ToString(), int.Parse(ViewData["pagesize"].ToString()), int.Parse(ViewData["totlecount"].ToString()))%>
                </span>
            </div>
            <div class="right">
                <div class="right-top">
                    <p>
                        当前已有<span><%=ViewData["doc"]%></span>份文档</p>
                    <p>
                        当前已有<span><%=ViewData["video"]%></span>个视频</p>
                    <p>
                        当前已有<span><%=ViewData["pic"]%></span>张图片</p>
                </div>
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
                                <volist name="daylist['data']">
                                <%List<ZK.Model.View_AllFileList> v_allfilelistDay = (List<ZK.Model.View_AllFileList>)ViewData["v_allfilelistDay"];
                                  int i = 0;
                                  foreach (var item in v_allfilelistDay)
                                  {
                                      i++;
                                      if (i <= 8)
                                      {
                                      %>
                                      <li><a href="#"><%=i%></a><span onclick="javascript:window.location.href='/ContentPage/Index?file_id=<%=item.fileID %>&url_flag=Admin'"><%=item.fileName%></span></li>
                                  <%}
                                      else
                                      {
                                          break;
                                      }
                                  }%>

                                    
                                </volist>
                            </ul>
                            <ul class="contents2">
                                <volist name="weeklist['data']" id='vo'>
                                 <%List<ZK.Model.View_AllFileList> v_allfilelistWeek = (List<ZK.Model.View_AllFileList>)ViewData["v_allfilelistWeek"];
                                   int j = 0;
                                   foreach (var item in v_allfilelistWeek)
                                   {
                                       j++;
                                       if (j <= 8)
                                       {
                                      %>
                                      <li><a href="#"><%=j%></a><span onclick="javascript:window.location.href='/ContentPage/Index?file_id=<%=item.fileID %>&url_flag=Admin'"><%=item.fileName%></span></li>
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
                                      <li><a href="#"><%=k%></a><span onclick="javascript:window.location.href='/ContentPage/Index?file_id=<%=item.fileID %>&url_flag=Admin'"><%=item.fileName%></span></li>
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
                                <li  ><div style="margin-bottom:10px; margin-top:10px;"><span class="name">用户名</span><span class="type" >经验值</span></div></li>
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
    </div>
</asp:Content>
