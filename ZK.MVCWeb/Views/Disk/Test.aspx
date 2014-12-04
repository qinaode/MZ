<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Uploadify</title>
    <link href="/Scripts/uploadify-v2.1.0/example/css/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="/Scripts/uploadify-v2.1.0/jquery-1.3.2.min.js"></script>--%>
    <script src="../../Scripts/jquery.1.9.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/uploadify-v2.1.0/swfobject.js"></script>
    <%--   <script type="text/javascript" src="/Scripts/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"></script>--%>
    <script src="../../Scripts/uploadify-v2.1.0/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                'uploader': '/Scripts/uploadify-v2.1.0/uploadify.swf',
                'script': '/Disk/UploadFiles', //'UploadHandler.ashx',
                'cancelImg': '/Scripts/uploadify-v2.1.0/cancel.png',
                'folder': '/Files/UploadFiles',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true
            });
        });  
    </script>
</head>
<body>
    <input type="file" name="uploadify" id="uploadify" />
    <p>
        <a href="javascript:$('#uploadify').uploadifyUpload()">上传</a>| <a href="javascript:$('#uploadify').uploadifyClearQueue()">
            取消上传</a>
    </p>
    <div id="fileQueue" style=" width:400px;">
    </div>
</body>
</html>
