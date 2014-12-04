<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>文档预览</title>
    <link href="../../css/CssDoc.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .title1 p
        {
            font-family: @黑体;
            font-size: larger;
            font-style: normal;
        }
        .warp
        {
            width: auto;
            height: auto !important;
            background-color: #ECECEC;
            margin: 0 auto;
            border: 1px solid #B4B4B4;
            -webkit-box-shadow: 1px 1px 2px #B4B4B4;
            -moz-box-shadow: 1px 1px 2px #B4B4B4;
            margin-bottom: 10px;
        }
        
        .bj
        {
            background: none repeat scroll 0 0 #C6CACF;
        }
        .foot
        {
            width: auto;
            height: 30px;
            line-height: 30px;
            text-align: center;
            margin-bottom: 10px;
            clear: both;
        }
        .Ncurrent
        {
            display: none;
        }
        .ForePage
        {
            text-align: right;
            width: 140px;
            height: 140px;
            margin-top: 150px;
            font-size: 20px;
            *background-image: url(/imagesN/ForePage.png);
        }
        .NextPage
        {
            text-align: left;
            width: 140px;
            height: 140px;
            margin-top: 150px;
            font-size: 20px;
            *background-image: url(/imagesN/NextPage.png);
        }
        .pptImg2
        {
            width:auto;
            height:auto;
        }
    </style>
    <script type="text/javascript">
        var TotalCount = 0;
        $(function () {
           DownLoadContent();
        });
        function ForePageClick() {
            var id = $(".current").attr("id");
            var index = parseInt(id.substr(4), 10);
            if (index > 1) {
                $("#" + id).removeClass("current");
                $("#" + id).addClass("Ncurrent");
                $("#img_" + (index - 1).toString()).addClass("current");
                $("#img_" + (index - 1).toString()).removeClass("Ncurrent");
            }
        }
        function NextPageClick() {
            var id = $(".current").attr("id");
            var index = parseInt(id.substr(4), 10);
            if (index < TotalCount) {
                $("#" + id).removeClass("current");
                $("#" + id).addClass("Ncurrent");
                $("#img_" + (index + 1).toString()).addClass("current");
                $("#img_" + (index + 1).toString()).removeClass("Ncurrent");
            }

        }

        //加载数据
        function DownLoadContent() {
            var url = location.search; //获取url中"?"符后的字串
            var file_id = "";
            if (url.indexOf("?") != -1) {
                file_id = url.substr(4);
            }
            else {
                return;
            }
            $.ajax({
                url: "/ContentPage/GetPPTContent?_id=" + file_id,
                type: "Post",
                data: {},
                datatype: "json/text",
                success: function (backdata) {
                    var imgcount = backdata.split(',')[0];
                    var dirpath = backdata.split(',')[1];
                    for (var i = 1; i <= parseInt(imgcount, 10); i++) {
                        var a_a = document.createElement("img");
                        a_a.setAttribute("class", "pptImg pptImg2");
                        a_a.setAttribute("src", dirpath + "/幻灯片" + i + ".jpg");
                        a_a.setAttribute("id", "img_" + i);
                        $("#Loadinggif").css("display", "none");
                        $(".warp").css("display", "block");
                        document.getElementById("div_content").appendChild(a_a);
                    }
                    $(".pptImg").each(function (i, item) {
                        if (i != 0) {
                            $(item).addClass("Ncurrent");
                        }
                        else {
                            $(item).addClass("current");
                        }
                        TotalCount = i + 1;
                    });
                    //                    alert(backdata["ImagePath"]);
                    //                    alert(backdata["fileCount"]);

                }
            });
        }
    </script>
</head>
<body class="bj" style="zoom: 1;">
    <div align="center" id="Loadinggif" style="margin-top: 120px;">
        <img src="/images/Loading.gif" />
        <br />
        <p>
            正在转换。。。</p>
    </div>
    <div class="warp" style="display: none;">
        <div align="center" class="title1">
            <p>
                <%=ViewData["PPTName"].ToString()%>
            </p>
        </div>
        <table style="width: 100%; height: auto; background-color: Gray;">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right;">
                    <div class="ForePage">
                        <a href="javascript:void(0);return false;" onclick="ForePageClick()">
                            <img src="/imagesN/ForePage.png" title="上一页" /></a>
                    </div>
                </td>
                <td align="center">
                    <div align="center" id="div_content">
                        <%--   <%  %>
                        <%for (int i = 1; i <= Convert.ToInt32(ViewData["fileCount"]); i++)
                          {
                        %>
                        <img class="pptImg" style="width: auto; height: auto;" src="<%=ViewData["ImagePath"].ToString()%>/幻灯片<%=i%>.jpg"
                            id="img_<%=i.ToString() %>" />
                        <%
                            } %>--%>
                    </div>
                </td>
                <td style="width: 10%; text-align: left;">
                    <div class="NextPage">
                        <a href="javascript:void(0);return false;" onclick="NextPageClick()" >
                            <img src="/imagesN/NextPage.png" title="下一页" /></a>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <div class="foot">
            <p>
                北京浩联教育</p>
        </div>
    </div>
    <script type="text/javascript">
        function iFrameHeight() {
            var ifm = document.getElementById("ifr_a");
            var subWeb = document.frames ? document.frames["ifr_a"].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                ifm.height = subWeb.body.scrollHeight;
            }
        }
    </script>
</body>
</html>
