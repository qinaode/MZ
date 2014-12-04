<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/lhgdialog/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <style type="text/css">
        .indexCont
        {
            width: 1024px;
            margin-right: auto;
            margin-left: auto;
            margin-top: 10px;
            overflow: hidden;
        }
        
        .bottom
        {
            clear: both;
            margin-top: 10px;
            padding: 10px;
            width: 1004px;
            margin-right: auto;
            margin-left: auto;
            overflow: hidden;
            background-color: #333;
        }
        
        .bottom .userhelp
        {
            width: 400px;
            margin-right: auto;
            margin-left: auto;
            overflow: hidden;
            margin-bottom: 15px;
        }
        
        .bottom .userhelp li
        {
            float: left;
            width: 200px;
            font-size: 16px;
            line-height: 25px;
            font-family: "微软雅黑";
            text-align: center;
        }
        
        .bottom .copyright
        {
            line-height: 30px;
            color: #fff;
            overflow: hidden;
            width: 700px;
            margin-right: auto;
            margin-left: auto;
            font-family: "微软雅黑";
            font-size: 14px;
        }
        
        .bottom .userhelp li a
        {
            color: #F5F5F5;
        }
        
        .bottom .copyright #address
        {
            float: left;
        }
        
        .bottom .copyright .connectUs
        {
            float: right;
        }
        
        .indexCont .classMenu
        {
            position: relative;
            margin-top: 25px;
            background-color: #ececec;
            border: 1px solid #999;
            margin-bottom: 10px;
            height: 400px;
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
        
        .indexCont .classMenu .tit
        {
            background-image: url(/ImagesN/Classification_subTit.gif);
            background-repeat: no-repeat;
            height: 38px;
            position: absolute;
            z-index: 2;
            top: -25px;
            left: 5px;
            font-family: "微软雅黑";
            font-size: 16px;
            color: #FFF;
            letter-spacing: 3px;
            line-height: 35px;
            text-indent: 30px;
            padding-right: 20px;
        }
        
        .indexCont .classMenu #mediaShow
        {
            padding: 15px;
            float: left;
        }
        
        .indexCont .classMenu .subInfo
        {
            float: left;
            width: 410px;
            font-family: "微软雅黑";
            font-size: 14px;
            line-height: 28px;
            padding: 10px;
        }
        
        .indexCont .classMenu .subInfo .subOP
        {
            padding-top: 15px;
        }
        
        .indexCont .aboutSubject
        {
            margin-top: 10px;
        }
        
        .indexCont .aboutSubject #subTitle
        {
            font-family: "微软雅黑";
            font-size: 16px;
            line-height: 35px;
            text-indent: 15px;
            border-bottom-width: 5px;
            border-bottom-style: solid;
            border-bottom-color: #090;
        }
        
        .Resources li
        {
            padding: 8px;
            width: 90px;
            border: 1px solid #EFEFEF;
            float: left;
            margin-top: 10px;
            margin-bottom: 10px;
            margin-left: 18px;
        }
        
        .Resources li:hover
        {
            background-color: #F5F5F5;
        }
        
        .Resources li .resTitle
        {
            line-height: 20px;
            font-family: "微软雅黑";
            font-size: 14px;
            color: #069;
        }
        
        .Resources li .resAttr
        {
            line-height: 30px;
            color: #999;
            overflow: hidden;
        }
        
        .Resources li .resAttr #resPage
        {
            float: left;
            margin-left: 5px;
        }
        
        .Resources li .resAttr #payStatus
        {
            float: right;
            margin-right: 5px;
        }
        
        .Resources
        {
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">
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

            //资源选项卡
            $(".indexCont .mainCont .secMenu li").each(function (index) {
                $(this).click(function () {
                    $(".indexCont .mainCont .secMenu li").removeClass("current");
                    $(this).addClass('current');
                    //$.ajax()...动态刷新内容
                })
            });

        });
        //图片显示
        function ShowPicDialog(pid) {
            //内容
            var Content = $("<div id='div_Common'></div>");
            //p.append($("<a>&nbsp;&nbsp;</a>"));
            Content.append($("<img src='" + pid + "' style='max-width:800px;'/>"));
            $.dialog({
                id: 'ItemID',
                title: "",
                width: '800px',
                height: '600px',
                content: Content,
                lock: true,
                cancel: true,
                max: false,
                min: false,
                padding: "0px",
                ok: false,
                button: [
                        {
                            name: '查看原图',
                            callback: function () {
                                ViewPic(pid);

                            },
                            focus: true
                        }
                    ]
            });
        }

        function ViewPic(pid) {
            //内容
            var Content = $("<div id='div_Common'></div>");
            //p.append($("<a>&nbsp;&nbsp;</a>"));
            Content.append($("<img src='" + pid + "' />"));
            $.dialog({
                id: 'ItemID1',
                title: "",
                width: 'auto',
                height: 'auto',
                content: Content,
                lock: true,
                cancel: true,
                max: false,
                min: false,
                padding: "0px",
                left: "0px",
                top: "0px",
                ok: false
            });
        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --<%=ViewData["title"] %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="indexCont">
        <div class="classMenu">
            <div class="tit">
                资源预览
            </div>
            <% ZK.Model.View_AllFileList allfilemodel = (ZK.Model.View_AllFileList)ViewData["FileInfo"]; %>
            <div id="mediaShow">
                <%
                    //视频
                    if (allfilemodel.fileTypeID == 1)
                    {
                %>
                <%
                    if ((int)ViewData["trastatus"] == 2)//已转换 可以预览
                    {
                %>
                <%--<a href="javascript:void(0);return false;" title="点击预览">
                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
                        height="369" width="548">
                        <param name="movie" value="/Files/Tools/flvplayer.swf?vcastr_file=<%=ViewData["ConvertPathNoEXT"].ToString() %>.flv">
                        <param name="quality" value="high">
                        <param name="allowFullScreen" value="true" />
                        <embed src="/Files/Tools/flvplayer.swf?vcastr_file=<%=ViewData["ConvertPath"].ToString() %>.flv"
                            quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash"
                            width="548" height="369"> 
</embed>
                    </object>
                </a>--%>
                <!-- 我爱播放器(52player.com)/代码开始 -->
                <input type="text" style="display: none;" id="txt_flvpath" value="<%=ViewData["filepath"].ToString() %>" />
                <input type="hidden" id="hid_logo" value="<%=ViewData["VideoLogo"].ToString() %>" />
                <script src="/Scripts/CuPlayerMiniV4/swfobject.js" type="text/javascript"></script>
                <div class="video" id="CuPlayer">
                    <strong>我爱播放器 提示：您的Flash Player版本过低，请<a href="http://www.52player.com/">点此进行网页播放器升级</a>！</strong></div>
                <script type="text/javascript">

                    var flvpath = $("#txt_flvpath").val() + ".flv";
                    var startimagepath = $("#txt_flvpath").val() + ".png";
                    var logopath = $("#hid_logo").val();
                    var so = new SWFObject("/Scripts/CuPlayerMiniV4/CuPlayerMiniV4.swf", "CuPlayerV4", "548", "369", "9", "#000000");
                    so.addParam("allowfullscreen", "true");
                    so.addParam("allowscriptaccess", "always");
                    so.addParam("wmode", "opaque");
                    so.addParam("quality", "high");
                    so.addParam("salign", "lt");
                    so.addVariable("CuPlayerSetFile", "/Scripts/CuPlayerMiniV4/CuPlayerSetFile.xml"); //播放器配置文件地址,例SetFile.xml、SetFile.asp、SetFile.php、SetFile.aspx
                    //                    so.addVariable("CuPlayerFile", "test.mp4"); //视频文件地址
                    so.addVariable("CuPlayerImage", startimagepath); //视频略缩图,本图片文件必须正确
                    so.addVariable("CuPlayerWidth", "548"); //视频宽度
                    so.addVariable("CuPlayerHeight", "369"); //视频高度
                    so.addVariable("CuPlayerAutoPlay", "no"); //是否自动播放
                    so.addVariable("CuPlayerLogo", logopath); //Logo文件地址
                    so.addVariable("CuPlayerFile", flvpath);
                    so.addVariable("CuPlayerPosition", "bottom-right"); //Logo显示的位置
                    so.write("CuPlayer");
                </script>
                <!-- 我爱播放器(52player.com)/代码结束 -->
                <% }
                    else if (ZK.Common.ModelSettings.IsModelData)
                    {
                %>
                <!-- 我爱播放器(52player.com)/代码开始 -->
                <input type="text" style="display: none;" id="Text1" value="<%=ViewData["ConvertPathNoEXT"].ToString() %>" />
                <input type="hidden" id="Hidden1" value="<%=ViewData["VideoLogo"].ToString() %>" />
                <script src="/Scripts/CuPlayerMiniV4/swfobject.js" type="text/javascript"></script>
                <div class="video" id="Div1">
                    <strong>我爱播放器 提示：您的Flash Player版本过低，请<a href="http://www.52player.com/">点此进行网页播放器升级</a>！</strong></div>
                <script type="text/javascript">

                    var flvpath = $("#txt_flvpath").val() + ".flv";
                    var startimagepath = $("#txt_flvpath").val() + ".png";
                    var logopath = $("#hid_logo").val();
                    var so = new SWFObject("/Scripts/CuPlayerMiniV4/CuPlayerMiniV4.swf", "CuPlayerV4", "548", "369", "9", "#000000");
                    so.addParam("allowfullscreen", "true");
                    so.addParam("allowscriptaccess", "always");
                    so.addParam("wmode", "opaque");
                    so.addParam("quality", "high");
                    so.addParam("salign", "lt");
                    so.addVariable("CuPlayerSetFile", "/Scripts/CuPlayerMiniV4/CuPlayerSetFile.xml"); //播放器配置文件地址,例SetFile.xml、SetFile.asp、SetFile.php、SetFile.aspx
                    //                    so.addVariable("CuPlayerFile", "test.mp4"); //视频文件地址
                    so.addVariable("CuPlayerImage", startimagepath); //视频略缩图,本图片文件必须正确
                    so.addVariable("CuPlayerWidth", "548"); //视频宽度
                    so.addVariable("CuPlayerHeight", "369"); //视频高度
                    so.addVariable("CuPlayerAutoPlay", "no"); //是否自动播放
                    so.addVariable("CuPlayerLogo", logopath); //Logo文件地址
                    so.addVariable("CuPlayerFile", flvpath);
                    so.addVariable("CuPlayerPosition", "bottom-right"); //Logo显示的位置
                    so.write("CuPlayer");
                </script>
                <!-- 我爱播放器(52player.com)/代码结束 -->
                <% 
                    }
                    else if ((int)ViewData["trastatus"] == 1 || (int)ViewData["trastatus"] == 0)//尚未转换 或 正在转换
                    {
                %><div style="width: 548px; height: 369px;">
                    <img src="/imagesN/Video_Convertting.jpg" style="width: 548px; height: 369px;" /></div>
                <%
                    }
                    else//不支持  转换失败
                    {
                %><div style="width: 548px; height: 369px;">
                    <img src="/imagesN/Video_NotSupport.jpg" style="width: 548px; height: 369px;" />
                </div>
                <%
                    }
                    }//图片
                    else if (allfilemodel.fileTypeID == 3)
                    {%>
                <div style="width: 548px; height: 369px;">

                    <a href="javascript:void(0);return false;" title="点击预览" id="<%=ZK.Controllers.ContentPageController.GetImageUrlPath(allfilemodel.fileID.ToString(),"base")%>" onclick="ShowPicDialog(this.id)">
                        <img src="<%=ZK.Controllers.ContentPageController.GetImageUrlPath(allfilemodel.fileID.ToString(),"256")%>"
                            style="width: 100%; height: 100%;" /></a>
                </div>
                <%}//word excel
                    else if (allfilemodel.fileTypeID == 2 || allfilemodel.fileTypeID == 6)
                    {
                %>
                <a href="/ContentPage/DocumentContent?_id=<%=allfilemodel.fileID %>" title="点击预览"
                    id="A2" target="_blank">
                    <img src="<%= ZK.Common.GetFileImage.getContentImage(allfilemodel.fileTypeID,allfilemodel.filePath)%>"
                        width="548" height="369" /></a>
                <%
                    }
                    //ppt
                    else if (allfilemodel.fileTypeID == 7)
                    {
                %>
                <a href="/ContentPage/PPTContent?_id=<%=allfilemodel.fileID %>" title="点击预览" id="A1"
                    target="_blank">
                    <img src="<%= ZK.Common.GetFileImage.getContentImage(allfilemodel.fileTypeID,allfilemodel.filePath)%>"
                        width="548" height="369" /></a>
                <%  
                    }
                    else
                    {
                       
                %>
                <a href="javascript:void(0);return false;" title="点击预览">
                    <img src="<%= ZK.Common.GetFileImage.getContentImage(allfilemodel.fileTypeID,allfilemodel.filePath)%>"
                        width="548" height="369" /></a>
                <%} %>
            </div>
            <div class="subInfo">
                <div class="subWord">
                    名称：<%=allfilemodel.fileName %>
                </div>
                <div class="subWord">
                    属于“<%=allfilemodel.channelName %>”频道
                </div>
                <div class="subWord">
                    属于“<%=allfilemodel.cateName %>”主题
                </div>
                <div class="subWord">
                    由“<%=allfilemodel.USERNAME %>”上传于：<%=allfilemodel.createTime %>
                </div>
                <div class="subWord">
                    已被点击
                    <%=allfilemodel.clickNum.ToString()==""?0:allfilemodel.clickNum %>
                    次<%=allfilemodel.fileName %>
                </div>
                <div class="subWord">
                    文件大小：43509760
                </div>
                <div class="subWord">
                    关键字：
                    <%=ViewData["KeyWords"] %>
                </div>
                <div class="subOP">
                    <a href="/ContentPage/DownLoad?filepath=<%=ViewData["filepath"].ToString() %>&filename=<%=allfilemodel.fileName %>"
                        id="a_download">
                        <img src="/ImagesN/subDetail_09.jpg" width="116" height="30" /></a> <a href="#neo"
                            onclick="alert('未实现 ');">
                            <img src="/ImagesN/subDetail_08.jpg" width="116" height="30" style="display:none;" /></a>
                </div>
            </div>
        </div>
        <div class="aboutSubject">
            <div id="subTitle">
                看过这个资料的人还浏览过
            </div>
            <ul class="Resources">
                <%System.Data.DataSet dsViewedList = (System.Data.DataSet)ViewData["ViewedList"];
                  int i = 0;
                %>
                <%if (dsViewedList != null && dsViewedList.Tables.Count > 0)
                  {
                      foreach (System.Data.DataRow item in dsViewedList.Tables[0].Rows)
                      {
                          i++;
                          if (i > ZK.Common.ModelSettings.VisitedFilesCount)
                          {
                              break;
                          }
                %>
                <li style="text-align: center;">
                    <div>
                    <a title="<%=item["fileName"]%>" href="/ContentPage/Index?file_id=<%=item["fileID"] %>&url_flag=#" style="color: #069;">
                    <div>
                    <img alt="<%=item["fileName"]%>" style="width: 48px; height: 48px;" src="<%=ZK.Common.GetFileImage.getListImage(Convert.ToInt32(item["filetypeid"]),item["imageURL"].ToString())%>" />
                    </div>
                    <div class="resTitle">
                        
                            <%if (item["fileName"].ToString().Length > 10)
                              {%>
                            <%=item["fileName"].ToString().Substring(0, 10)%>...<%}
                              else%>
                            <%=item["fileName"]%>
                    </div>
                     </a>
                    </div>
                    <div class="resAttr">
                        <%--<span id="Span1">
                            <%=item["cateName"]%></span><span id="Span2">--%>点击<%=item["clickNum"]%>次<%--</span>--%>
                    </div>
                </li>
                <%}
                  } %>
            </ul>
        </div>
    </div>
</asp:Content>
