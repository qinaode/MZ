<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZK.Controllers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/index.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .warp1
        {
            width: 1000px;
            height: auto !important;
            background-color: #fff;
            margin: 0 auto;
            border: 0px solid #fff;
            -webkit-box-shadow: 1px 1px 2px #fff;
            -moz-box-shadow: 1px 1px 2px #B4B4B4;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            try {

                if (window != parent) {
                    var a = parent.document.getElementsByTagName("iframe");
                    for (var i = 0; i < a.length; i++) {
                        if (a[i].contentWindow == window) {
                            var h1 = 0, h2 = 0, d = document, dd = d.documentElement;
                            a[i].parentNode.style.height = a[i].offsetHeight + "px";

                            a[i].style.height = "10px";
                            if (dd && dd.scrollHeight) h1 = dd.scrollHeight;
                            if (d.body) h2 = d.body.scrollHeight;
                            var h = Math.max(h1, h2);
                            if (document.all) { h += 4; }
                            if (window.opera) { h += 1; }
                            a[i].style.height = a[i].parentNode.style.height = h + "px";
                        }
                    }
                }
            } catch (ex) { }

        });
           
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
</head>
<body>
    <div class="warp1">
        <div class="down">
            <div>
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
                        <tr style="height:34px">
                            <td>
                                <div id="Div3">
                                    <%--<%=item["fileTypeName"]%>--%>
                                    <div id="Div1">
                                        <%if (Convert.ToInt32(item["fileTypeID"]) == 2)
                                          {
                                              if (item["filePath"].ToString().Contains('.'))
                                              {
                                                  string str = item["filePath"].ToString().Split('.')[item["filePath"].ToString().Split('.').Length - 1];
                                                  if (str == "xls" || str == "xlsx")
                                                  {%>
                                                                  <img src="../../themes/defaultpic/excel/32.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px"  />
                                                   <%}
                                                  if (str == "doc" || str == "docx")
                                                  {%>
                                                                <img src="../../themes/defaultpic/word/32.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px"/>
                                                   <%}
                                                  if (str == "ppt" || str == "pptx")
                                                  {%>
                                                                <img src="../../themes/defaultpic/ppt/32.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px"/>
                                                    <%}
                                                  else
                                                  {%>
                                                                <img src="../../themes/defaultpic/word/32.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px" />
                                                   <%}
                                              }
                                            }%>


                                        <%else
                                            { %>
                                               
                                                <%if (Convert.ToInt32(item["fileTypeID"]) == 3)
                                                  {%>                                                         
                                                       <img src="../../images/img.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px"/>
                                                   <%}
                                                  else if (Convert.ToInt32(item["fileTypeID"]) == 1)
                                                    {%>                                                         
                                                       <img src="../../images/video.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px"/>
                                                   <%}
                                                  else
                                                       {%>                                                         
                                                       <img src="../../themes/defaultpic/word/32.png" style="margin-top:5px; margin-bottom:5px;" width="34px" height="34px" />
                                                   <%}                                                  
                                           
                                          } %>
                                         
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div id="Div7" style="float: left;">
                                    <a target="_blank" href='/ContentPage/Index?file_id=<%=item["fileID"] %>&url_flag=Search'>
                                        <%=item["fileName"]%>
                                    </a>
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
        <div style="height:60px;"></div>
    </div>
</body>
</html>
