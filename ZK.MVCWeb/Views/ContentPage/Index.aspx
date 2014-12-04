<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    --智客知识管理平台
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .title_p
        {
            margin-top: 10px;
            margin-left: 30px;
            color: #0171D3;
            font-style: inherit;
            font-weight: bolder;
            font-size: large;
            font-family: 微软雅黑,Arial, Helvetica,;
        }
        .title_2 p
        {
            line-height: 28px;
            font-family: "微软雅黑";
            font-size: 14px;
            padding-left: 5px;
        }
    </style>
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
    <div class="warp1">
        <div class="down">
            <% ZK.Model.View_AllFileList allfilemodel = (ZK.Model.View_AllFileList)ViewData["FileInfo"]; %>
            <p class="tit3">
                <span class="inb">资源展示 &nbsp;&nbsp;>&nbsp;&nbsp;
                    <%=allfilemodel.channelName %>资源</span><%--<input type="button" style="height: 22px;
                        background-color: #EC870E; margin-left: 45px;" value="<< 返回" onclick="javascript:window.location.href='<%=ViewData["HistoryURL"] %>'" />--%>
                <a style="height: 22px; background-color: #EC870E; margin-left: 45px;" href="<%=ViewData["HistoryURL"] %>">
                    << 返回&nbsp;&nbsp;</a></p>
            <div style="clear: both">
            </div>
            <div class="click">
                <div class="click-left title_2">
                    <div style="height: 80px;">
                    </div>
                    <%--  <p class="title_p">
                        <%=allfilemodel.channelName %>资源</p>--%>
                    <p class="p">
                        名称：&nbsp;&nbsp;&nbsp;&nbsp;<%=allfilemodel.fileName %></p>
                    <p class="p">
                        频道：&nbsp;&nbsp;&nbsp;&nbsp;<%=allfilemodel.channelName %>频道</p>
                    <p class="p">
                        主题：&nbsp;&nbsp;&nbsp;&nbsp;<%=allfilemodel.cateName %></p>
                    <p class="p">
                        共享人&nbsp;&nbsp;：<%=allfilemodel.USERNAME %></p>
                    <p class="p">
                        创建时间：<%=allfilemodel.createTime %></p>
                    <p class="p">
                        点击：&nbsp;&nbsp;&nbsp;&nbsp;<span class="spal"><%=allfilemodel.clickNum.ToString()==""?0:allfilemodel.clickNum %></span>次</p>
                    <%--<p class="p">
                        文件大小：<span class="spal"><% %></span></p>--%>
                    <p class="p">
                        关键字：&nbsp;&nbsp; <span class="spal none">
                            <%=ViewData["KeyWords"] %></span>
                    </p>
                    <p class="button">
                        <a href="<%=allfilemodel.filePath %>" style="color: White;">点击下载</a></p>
                </div>
                <div class="click-right">
                    <%--<img src="<%=allfilemodel.imageURL %>" />--%>
                    <img src="/images/Model.jpg" /></div>
            </div>
            <div class="see">
                <span style="font-size: 17px; color: #0288D1">看过这个资源的还浏览过...</span></div>
            <div class="wel">
                <%System.Data.DataSet dsViewedList = (System.Data.DataSet)ViewData["ViewedList"];
                  int i = 0;
                %>
                <%foreach (System.Data.DataRow item in dsViewedList.Tables[0].Rows)
                  {
                      i++;
                      if (i > 10)
                      {
                          break;
                      }
                %>
                
                <foreach name="footdata" item="vo">
             		<dl style="width:421px; height:160px; float:left; margin-left:10px; margin-top:20px;">
		                <dt style="display:block;width:198px; height:130px; float:left;"><div align="center"><img alt="<%=item["fileName"]%>"  style="width:120px; margin-top:10px;" src="<%=item["imageURL"]%>" /></div></dt>
		                <dd style="float:left; line-height:25px; margin-left:15px; color:#717171; width:200px;">
                        <p>资源名称：<a title="<%=item["fileName"]%>" href="/ContentPage/Index?file_id=<%=item["fileID"] %>&url_flag=#">
                            <%if (item["fileName"].ToString().Length > 10)
                              {%> 
                            <%=item["fileName"].ToString().Substring(0, 10)%>...<%}
                              else%>
                                  <%=item["fileName"]%></a>
                        </p>  		                
                         <p>属于"<%=item["cateName"]%>"主题</p>
		                <p>由<%=item["USERNAME"]%>上传于：</p>
		                <p><span><%=item["createTime"]%></span></p>
		                <p>已被下载<%=item["clickNum"]%>次</p></dd>
                	</dl>                     
				</foreach>
                <%} %>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
