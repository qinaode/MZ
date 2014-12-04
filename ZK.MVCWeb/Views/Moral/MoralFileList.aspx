<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
  <%@ Import Namespace="ZK.Controllers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/CSS/index.css" rel="stylesheet" type="text/css" />
     <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <link href="../../css/pagecss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .div_fileInfo
        {
            float: left;
            width: 690px;
            position: relative;
        }
        
        .div_fileInfo .Resources
        {
            overflow: hidden;
            margin-top: 40px;
        }
        .div_fileInfo .Resources li
        {
            padding: 8px;
            width: 90px;
            height:123px;
            border: 1px solid #EFEFEF;
            float: left;
            margin-top: 10px;
            margin-bottom: 10px;
            margin-left: 25px;
            list-style-type: none;
        }
        .div_fileInfo li:hover
        {
            background-color: #F5F5F5;
        }
        .div_fileInfo .Resources li .resTitle
        {
            line-height: 20px;
            font-family: "微软雅黑";
            margin-top: 5px;
            font-size: 12px;
            color: #575757;
        }
        .div_fileInfo .Resources li .resAttr
        {
            line-height: 30px;
            color: #999;
            overflow: hidden;
        }
        .div_fileInfo .Resources li .resAttr #resPage
        {
            float: left;
            margin-left: 15px;
        }
        .div_fileInfo .Resources li .resAttr #payStatus
        {
            float: right;
            margin-right: 5px;
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
            var url = location.search;
            var typeParm = url.substr(5); 
            if (typeParm == "") {
                id = "all";
            }
            else {
                var typestr = typeParm.split('&');
                id = typestr[0];
              
                typeType = typestr[1].substr(5);
               
            }
        })         
    </script>
</head>
<body>
    <div class="div_fileInfo">
        <ul style="margin-left: -10px;" class="Resources">
            <%List<ZK.Model.View_OtherFileList> allfilemodel = (List<ZK.Model.View_OtherFileList>)ViewData["FileInfo"]; %>
            <%foreach (var item in allfilemodel)
              {%>
            <li style="text-align: center">
            <div>
             <a target="_parent"  href='/ContentPage/Index?file_id=<%=item.fileID%>&url_flag=Moral'>
                    <img src="<%=item.imageURL %>" style="border: 0px; width:48px; height:48px;" />
                <div class="resTitle">
                     <%if (item.fileName.ToString().Length > 12)
                      {%>
                    <%=item.fileName.ToString().Substring(0, 12)%>...<%}
                      else%>
                    <%=item.fileName%>
                 </div>
                   </a>
                   </div>
                   
                    <div class="resAttr">
                        <span id="resPage">
                            <%=item.USERNAME%></span><span id="payStatus"></span></div>
            </li>
            <%}%>
        </ul>
        <div>
            <span class="link" style="float: right; width: 400px">
                <%=Html.Pager(ViewData["page"].ToString(), int.Parse(ViewData["pagesize"].ToString()), int.Parse(ViewData["totlecount"].ToString()))%>
            </span>
        </div>
       
    </div>
</body>
</html>
