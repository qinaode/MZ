<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="ZK.Controllers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Teach_FileList</title>
    <link href="/CSS/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/Teach_Common.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <link href="../../css/pagecss.css" rel="stylesheet" type="text/css" />
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
</head>
<body>
    <br />
    <br />
    <br />
    <ul class="Resources">
        <!--列表数据开始-->
        <%List<ZK.Model.View_TeachFileList> DataList = (List<ZK.Model.View_TeachFileList>)ViewData["TeachFileList"];
          for (int i = 0; i < DataList.Count; i++)
          {
              if (i % 2 == 0)
              {
        %>
        <li class="current">
            <div width="100%" class="div_filelist1">
                <table width="98%" style="border: 0px;">
                    <tr>
                        <td rowspan="4" class="imginfo" style="width: 140px; text-align: center;">
                            <img style="width: 48px;" src="<%=DataList[i].imageURL %>" />
                        </td>
                        <td>
                            <p class="warter">
                                <a target="_parent" href="/ContentPage/Index?file_id=<%=DataList[i].fileID %>&url_flag=Teach">
                                    <%=DataList[i].fileName%></a></p>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            简介：<%=DataList[i].fileDesc%>
                        </td>
                        <td>
                        </td>
                        <td width="20%">
                            <p class="study">
                                <span>
                                    <%=DataList[i].clickNum%></span>人阅读</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            贡献人:<%=DataList[i].USERNAME%>&nbsp;&nbsp;创建时间：<%=DataList[i].createTime%>
                        </td>
                        <td>
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <%--<div class="subjectImg">
                            <img src="/ImagesN/subject01.jpg" width="53" height="68" /></div>
                        <div class="subjectCont">
                            <div class="subTitle">
                                《秋天的怀念》教学配套课件ppt</div>
                            <div class="summary">
                                <span class="vTit">简介：</span><span class="vCont">简介内容</span></div>
                            <div class="author">
                                <span class="vTit">贡献者：</span><span class="vCont">张三</span> <span class="vTit">创建时间：</span><span
                                    class="vCont">3个小时前</span>
                            </div>
                        </div>
                        <div class="subjectCount">
                            <span class="popCount">2321</span>人阅读<br />
                            <img src="/ImagesN/subject_06.jpg" width="14" height="14" />
                            （0次）
                        </div>--%>
        </li>
        <%}
              else
              {
        %>
        <li>
            <div width="100%" class="div_filelist2">
                <table width="98%" style="border: 0px;">
                    <tr>
                        <td rowspan="4" class="imginfo" style="width: 140px; text-align: center;">
                            <img style="width: 48px;" src="<%=DataList[i].imageURL %>" />
                        </td>
                        <td>
                            <p class="warter">
                                <a target="_parent" href="/ContentPage/Index?file_id=<%=DataList[i].fileID %>&url_flag=Teach">
                                    <%=DataList[i].fileName %></a></p>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            简介：<%=DataList[i].fileDesc %>
                        </td>
                        <td>
                        </td>
                        <td width="20%">
                            <p class="study">
                                <span>
                                    <%=DataList[i].clickNum %></span>人阅读</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            贡献人:<%=DataList[i].USERNAME %>&nbsp;&nbsp;创建时间：<%=DataList[i].createTime %>
                        </td>
                        <td>
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </li>
        <% 
}
          } %>
    </ul>
    <%if (DataList.Count != 0)
      { %>
    <!-- 分页-->
    <span class="link" style="float: right; width: 500px">
        <%=Html.Pager("page", int.Parse(ViewData["pagesize"].ToString()), int.Parse(ViewData["totlecount"].ToString()))%>
    </span>
    <%} %>
    <br />
    <br />
    <br />
</body>
</html>
