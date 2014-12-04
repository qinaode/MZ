<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZK.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/Css/common.css" rel="stylesheet" type="text/css" />
    <link href="/Css/top.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .indexHead {
	width: 1024px;
	margin-right: auto;
	margin-left: auto;
	position: relative;
	overflow: hidden;
	height: 322px;
	clear: both;
	margin-top: 8px;
}
    .indexHead .banner {
	width: 1024px;
	height: 330px;
	overflow: hidden;
}
    .indexHead .newsList {
	float: right;
	width: 330px;
	margin-top: 8px;
}
    .indexHead .bannerDot {
	width: 400px;
	left: -61px;
	bottom: -2px;
	position: absolute;
	height: 40px;
	margin-left: 350px;
}
    .indexHead .bannerDot li {
	background-color: #000;
	float: left;
	height: 20px;
	width: 60px;
	margin-left: 10px;
	filter: alpha(opacity=30); /*IE滤镜，透明度50%*/
	-moz-opacity: 0.3; /*Firefox私有，透明度50%*/
	opacity: 0.3;/*其他，透明度50%*/
	border: 1px solid #FFF;
}
    .indexHead .bannerDot .current {
	background-color: #F00;
}
    .indexHead .newsList dt {
	font-family: "微软雅黑";
	font-size: 20px;
	line-height: 40px;
	font-weight: bold;
}
    .indexHead .newsList .wordLine {
	line-height: 25px;
	background-image: url(/ImagesN/Nongxy_18.jpg);
	background-repeat: no-repeat;
	background-position: 5px 12px;
	text-indent: 15px;
	font-family: "微软雅黑";
	font-size: 14px;
}
    .indexHead .newsList dd .pageCount {
	color: #CCC;
}
    .indexHead .newsList .action {
	line-height: 30px;
	border-top-width: 1px;
	border-top-style: dashed;
	border-top-color: #CCC;
	margin-top: 10px;
}
    .indexHead .newsList .action #more {
	float: left;
	width: 50px;
	margin-left: 15px;
}
    .indexHead .newsList .action #turnPage {
	background-image: url(/ImagesN/Index_49.png);
	background-repeat: no-repeat;
	background-position: 5px 12px;
	text-indent: 20px;
	float: right;
	margin-right: 15px;
}
    .indexCont {
	width: 1024px;
	margin-right: auto;
	margin-left: auto;
	margin-top: 10px;
	overflow: hidden;
}
    .indexCont .mainCont {
	float: left;
	width: 1024px;
	position: relative;
}
    .indexCont .mainCont .secMenu {
	overflow: hidden;
	height: 31px;
	position: absolute;
	left: 0px;
	top: 0px;
	z-index: 2;
}
    .indexCont .mainCont .secMenu li {
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
}
    .indexCont .mainCont .secMenu li a {
}
    .indexCont .mainCont .secMenu .current {
	background-image: url(/ImagesN/Classification_03.gif);
	background-repeat: no-repeat;
}
    .indexCont .mainCont .secMenu .current a {
	color: #000000;
}
    .indexCont .mainCont .Resources li {
	padding: 8px;
	width: 99px;
	border: 1px solid #EFEFEF;
	float: left;
	margin-top: 10px;
	margin-bottom: 10px;
	margin-left: 25px;
}
.indexCont .mainCont .Resources li:hover {
	background-color:#F5F5F5;
}
    .indexCont .mainCont .Resources li .resTitle {
	line-height: 20px;
	font-family: "微软雅黑";
	font-size: 14px;
	color: #069;
}
    .indexCont .mainCont .Resources li .resAttr {
	line-height: 30px;
	color: #999;
	overflow: hidden;
}
    .indexCont .mainCont .Resources li .resAttr #resPage {
	float: left;
	margin-left: 5px;
}
    .indexCont .mainCont .Resources li .resAttr #payStatus {
	float: right;
	margin-right: 5px;
}
    .indexCont .mainCont .Resources {
	overflow: hidden;
	margin-top: 40px;
}
    .indexCont .rightCont {
	float: right;
	width: 320px;
}
    .indexCont .rightCont .resCounter {
	line-height: 28px;
	font-family: "微软雅黑";
	font-size: 14px;
	text-align: center;
	background-color: #F4FAFF;
	border: 1px solid #E8F4FF;
}
    .indexCont .rightCont .resCounter .v {
	color: #F30;
	font-size: 18px;
}
    .indexCont .rightCont .ranking {
	padding-top: 10px;
}
    .indexCont .rightCont .ranking .rankTit {
	font-family: "微软雅黑";
	font-size: 16px;
	line-height: 30px;
	width: 100px;
	border-bottom-width: 2px;
	border-bottom-style: solid;
	border-bottom-color: #060;
	text-align: center;
}
    .indexCont .rightCont .ranking .rankMenu {
	line-height: 30px;
	overflow: hidden;
	margin-top: 10px;
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #D7D8D9;
}
    .indexCont .rightCont .ranking .rankMenu li {
	width: 106px;
	float: left;
	text-align: center;
	font-family: "微软雅黑";
	font-size: 16px;
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #FFF;
}
    .indexCont .rightCont .ranking .rankMenu .current {
	border-top-width: 1px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-top-style: solid;
	border-right-style: solid;
	border-bottom-style: solid;
	border-left-style: solid;
	border-top-color: #D7D8D9;
	border-right-color: #D7D8D9;
	border-bottom-color: #D7D8D9;
	border-left-color: #D7D8D9;
	background-color: #D7D8D9;
	color: #FFF;
}
    .indexCont .rightCont .ranking .rankCont {
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-right-style: solid;
	border-bottom-style: solid;
	border-left-style: solid;
	border-right-color: #D7D8D9;
	border-bottom-color: #D7D8D9;
	border-left-color: #D7D8D9;
}
    .indexCont .rightCont .ranking .rankCont .rankRow .rankRowCont .rankRowContLi {
	line-height: 30px;
	font-family: "微软雅黑";
	font-size: 14px;
	overflow: hidden;
	padding-left: 5px;
	margin-bottom: 5px;
}
    .indexCont .rightCont .ranking .rankCont .rankRow .rankRowCont .rankRowContLi .No {
	height: 20px;
	width: 20px;
	background-color: #999;
	float: left;
	text-align: center;
	line-height: 20px;
	margin-top: 5px;
	color: #FFF;
}
    .indexCont .rightCont .ranking .rankCont .rankRow .rankRowCont .rankRowContLi .resTitle {
	text-indent: 5px;
	float: left;
}
    .indexCont .rightCont .ranking .rankCont .rankRow {
	display: none;
}
    .indexCont .rightCont .ranking .rankCont .current {
	display: block;
}
    .bottom {
	clear: both;
	margin-top: 10px;
	padding: 10px;
	width: 1004px;
	margin-right: auto;
	margin-left: auto;
	overflow: hidden;
	background-color: #333;
}
    .bottom .userhelp {
	width: 400px;
	margin-right: auto;
	margin-left: auto;
	overflow: hidden;
	margin-bottom: 15px;
}
    .bottom .userhelp li {
	float: left;
	width: 200px;
	font-size: 16px;
	line-height: 25px;
	font-family: "微软雅黑";
	text-align: center;
}
    .bottom .copyright {
	line-height: 30px;
	
	overflow: hidden;
	width: 700px;
	margin-right: auto;
	margin-left: auto;
	font-family: "微软雅黑";
	font-size: 14px;
}
    .bottom .userhelp li a {
	color: #F5F5F5;
}
    .bottom .copyright #address {
	float: left;
}
    .bottom .copyright .connectUs {
	float: right;
}
    .indexCont .classMenu {
	position: relative;
	margin-top: 2px;
	background-color: #999;
	border: 1px solid #333;
	margin-bottom: 10px;
	height: 48px;
	-webkit-border-radius: 5px;
	-moz-border-radius: 5px;
	border-radius: 5px;
	-webkit-box-shadow: #666 0px 0px 5px;
	-moz-box-shadow: #666 0px 0px 5px;
	box-shadow: #666 0px 0px 5px;
	width: 1012px;
	margin-right: auto;
	margin-left: auto;
	padding-left: 3px;
}
    .indexCont .classMenu dt {
	background-image: url(/ImagesN/Classification_subTit.gif);
	background-repeat: no-repeat;
	height: 38px;
	width: 248px;
	position: absolute;
	z-index: 2;
	top: -25px;
	left: 5px;
}
    .indexCont .classMenu dt .t {
	line-height: 35px;
	font-family: "微软雅黑";
	font-size: 16px;
	color: #FFF;
	padding-left: 30px;
	float: left;
	width: 100px;
	letter-spacing: 3px;
}
    .indexCont .classMenu dt #subjectSum {
	float: left;
	font-family: "微软雅黑";
	line-height: 20px;
	margin-top: 9px;
	margin-left: 10px;
	padding-right: 8px;
	padding-left: 8px;
	border: 1px solid #515151;
	background-color: #64B10E;
	color: #FFF;
	-webkit-border-radius: 10px;
	-moz-border-radius: 10px;
	border-radius: 10px;
	-webkit-box-shadow: #666 0px 0px 5px;
	-moz-box-shadow: #666 0px 0px 5px;
	box-shadow: #666 0px 0px 5px;
}
    .indexCont .classMenu dd {
	line-height: 50px;
	float: left;
	width: 251px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-right-style: solid;
	border-bottom-style: solid;
	border-right-color: #CCC;
	border-bottom-color: #CCC;
	cursor: pointer;
}
    .indexCont .classMenu dd .t {
	padding-left: 25px;
	font-family: "微软雅黑";
	font-size: 14px;
	color: #FFF;
	letter-spacing: 3pt;
	float: left;
}
    .indexCont .classMenu dd #subjectSum {
	float: left;
	padding-right: 15px;
	padding-left: 15px;
	border: 1px solid #E6E6E6;
	-webkit-border-radius: 10px;
	-moz-border-radius: 10px;
	border-radius: 10px;
	line-height: 20px;
	margin-top: 15px;
	margin-left: 15px;
	color: #E6E6E6;
	font-family: "微软雅黑";
}
    .indexCont .mainCont .menuFootLine {
	height: 6px;
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #71ac67;
	overflow: hidden;
	background-color: #71af82;
	position: absolute;
	left: 0px;
	top: 30px;
	width: 1024px;
	z-index: 1;
}
    .indexCont .classMenu .noRightLine {
	border-right-width: 1px;
	border-right-style: solid;
	border-right-color: #999;
}
    .indexCont .classMenu .current a .t {
	color: #DAFF68;
}
    .indexCont .classMenu .current a #subjectSum {
	color: #DAFF68;
}
    </style>

    <script type="text/javascript" >
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

            //频道分类选项卡
            $(".indexCont .classMenu dd   ").each(function (index) {
                $(this).click(function () {
                    $(".indexCont .classMenu dd ").removeClass("current");
                    $(this).addClass('current');

                })
            })



            //选项卡
            $(".indexCont .mainCont .secMenu  li").each(function (index) {
                $(this).click(function () {
                    var liNode = $(this);
                    liNode.parent().parent().find(".secMenu li").removeClass('current');
                    liNode.parent().parent().find(".Resources li").removeClass('current');

                    liNode.addClass('current');
                    liNode.parent().parent().find(".Resources li").eq(index).addClass('current');

                    GetTypeOfFile(index);

                })
            })

        });
	</script>            
     
       <script type="text/javascript">
           var id = "all";
           var typeType = 0;
           var show = 0;
           var search = "";
           var typeId = 0;
           var type = 0;

           function GetType(getType) {
               FormatType = getType;

           }

           function Search() { 
         
               search = $("#searchWord").val();
               typeId = $("#typeID").val();
               if (FormatType == "pic") {
                   type = 2;
               }
               else if (FormatType == "doc" || FormatType == "pdf" || FormatType == "ppt") {
                   type = 1;
               }
               else if (FormatType == "vedio") {
                   type = 3;
               }
               else {
                   type = 0;
               }

               if (show == 1) {
                   document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicTable?cid=" + id + "&type=" + type + "&sid=1" + "&search=" + search + "&typeId=" + typeId;
               }
               else
                   document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicList?sid=0" + "&cid=" + id + "&type=" + type + "&search=" + search + "&typeId=" + typeId;

           }

           $(function () {
               var url = location.search;
                            
               $("#searchWord").val($("#txt1").val());

               search = $("#txt1").val();

               typeId = $("#typeID").val();
               
               document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicList?sid=0" + "&cid=" + id + "&type=" + type + "&search=" + search + "&typeId=" + typeId;
              
           })

           function getchannel(cid) {
               if (show == 1) {

                   document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicTable?cid=" + cid + "&type=" + typeType + "&sid=1" + "&search=" + search + "&typeId=" + typeId;
                   id = cid;
                   show = 1;
               }
               else {

                   document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicList?cid=" + cid + "&type=" + typeType + "&sid=0" + "&search=" + search + "&typeId=" + typeId;
                   id = cid;
                   show = 0;

               }
           }
           function GetTypeOfFile(type) {

               if (show == 1) {

                   document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicTable?cid=" + id + "&type=" + type + "&sid=1" + "&search=" + search + "&typeId=" + typeId;
                   typeType = type;
                   show = 1;
               }
               else {

                   document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicList?cid=" + id + "&type=" + type + "&sid=0" + "&search=" + search + "&typeId=" + typeId;
                   typeType = type;
                   show = 0;
               }
               $(".indexTop .searchArea .docs  li ").eq(1).removeClass('current');
               $(".indexTop .searchArea .docs  li ").eq(2).removeClass('current');
               $(".indexTop .searchArea .docs  li ").eq(3).removeClass('current');
               $(".indexTop .searchArea .docs  li ").eq(4).removeClass('current');
               $(".indexTop .searchArea .docs  li ").eq(5).removeClass('current');
               $(".indexTop .searchArea .docs  li ").eq(0).addClass('current');
           }

           function ShowChange() {
               
               document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicList?sid=0" + "&cid=" + id + "&type=" + typeType + "&search=" + search + "&typeId=" + typeId;
               document.getElementById("list1").src = "../../images/ico3.jpg";
               document.getElementById("list0").src = "../../images/ico4.jpg";
               show = 0;
           }
           function ShowList() {
               document.getElementById("iframe_Filelist").src = "/SpecialTopic/SpecialTopicTable?sid=1" + "&cid=" + id + "&type=" + typeType + "&search=" + search + "&typeId=" + typeId;
               document.getElementById("list1").src = "../../images/ico1.jpg";
               document.getElementById("list0").src = "../../images/ico2.jpg";
               show = 1;
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
--关键字搜索-<%=ViewData["key"] %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<input  type="text" style=" display:none;"  id="txt1" value="<%=ViewData["txt1"].ToString()%>"/>
<input  type="text" style=" display:none;"  id="typeID" value="<%=ViewData["typeId"]%>"/>
    <div class="indexCont">

    	<dl class="classMenu">
         <% List<int> listcount = (List<int>)ViewData["TotalNum_TMA"]; %>
            <dd >
                <a id="jxpd" onclick="getchannel(this.id);" ><div class="t">教学频道资源</div></a>
                <div id="subjectSum"><%=listcount[0] %></div>
            </dd>
          <dd>
            <a id="xzpd" onclick="getchannel(this.id);" ><div class="t">行政频道资源</div></a>
            <div id="subjectSum"><%=listcount[2] %></div>
          </dd>
            <dd>
                <a id="dypd" onclick="getchannel(this.id);" ><div class="t">德育频道资源</div> </a>
                <div id="subjectSum"><%=listcount[1] %></div>
            </dd>
            <dd class="noRightLine"><a href="#neo"><div class="t">电教频道资源</div></a><div id="subjectSum">85</div></dd>
        </dl>

    	<div class="mainCont">
        	
            <div>
            <ul class="secMenu">
                <li class="current" ">全部</li>
                <li >文档</li>
                <li >图片</li>
                <li >视频</li>
            </ul>
            <div style="float:right; margin-right:20px; margin-bottom:-20px; ">
                    <p style="float:left;">
                        显示方式：</p>                      
                        <img id="list0" alt="" src="../../images/ico4.jpg"   style="cursor: pointer; border:0px;" title="矩阵显示" onclick="ShowChange();" />
                        <img id="list1" alt="" src="../../images/ico3.jpg" style="cursor: pointer; border:0px;" title="列表显示"  onclick="ShowList();" />
              </div>

            <div class="menuFootLine"> </div>
            
              <iframe id="iframe_Filelist" scrolling="no"  frameborder="0" style="width: 100%; height: 800px; margin-top:30px;">
                    </iframe>
        </div>
        
    </div>
    </div>
</asp:Content>
