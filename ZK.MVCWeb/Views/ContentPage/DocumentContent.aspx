<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>文档预览</title>
    <link href="../../css/CssDoc.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .warp
        {
            width: 1000px;
            height: auto !important;
            background-color: #fff;
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
            width: 1000px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            margin-bottom: 10px;
            clear: both;
        }
        /* .warp
        {
            border: 1px solid #A9B0B8;
            box-shadow: 0 0 11px 2px rgba(0, 0, 0, 0.3);
            margin-top: 10px;
        }*/
    </style>
    <script type="text/javascript">
        $(function () {
            DownLoadContent();
        });

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
                url: "/ContentPage/GetDocumentContent?_id=" + file_id,
                type: "Post",
                data: {},
                datatype: "json/text",
                success: function (backdata) {
                    $("#ifr_a").attr("src", backdata);
                    $("#Loadinggif").css("display", "none");
                    $(".warp").css("display", "block");


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
        <iframe name="ifr_a" id="ifr_a" onload="iFrameHeight()" frameborder="no" scrolling="no"
            style="align: center; width: 100%; border: 0; scrolling: no; marginwidth: 0;
            marginheight: 0;" src="" height="530"></iframe>
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
