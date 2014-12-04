<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>我的资料</title>
    <script src="/Content/easyui/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Content/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Content/easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/Content/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/Content/uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script src="/Content/Datagrid/Datagrid.js" type="text/javascript"></script>
    <link href="/Content/Datagrid/Datagrid.css" rel="stylesheet" type="text/css" />
    <link href="/Content/Datagrid/fileicon.css" rel="stylesheet" type="text/css" />
    <script src="/Content/win/win.js" type="text/javascript"></script>
    <link href="/Content/win/win.css" rel="stylesheet" type="text/css" />
    <link href="/Content/tip/tip.css" rel="stylesheet" type="text/css" />
    <script src="/Content/tip/tip.js" type="text/javascript"></script>
    <link href="/Content/menu/menu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        *
        {
            padding: 0;
            margin: 0;
        }
        a
        {
            text-decoration: none;
            color: #000;
        }
        ul
        {
            list-style-type: none;
        }
        .path
        {
            font-size: 14px;
            height: 40px;
            line-height: 40px;
            color: #0870B2;
            border-bottom: solid 1px #ccc;
            background: #f1f1f1;
            text-indent: 24px;
        }
        .operation
        {
            font-size: 14px;
            border-bottom: solid 1px #ccc;
            background: #fff;
            padding: 18px 0px;
        }
        .operation .items
        {
            height: 33px;
            line-height: 33px;
        }
        .operation .items li
        {
            float: left;
            cursor: pointer;
            text-align: center;
            color: #388FC9;
            margin-left: 8px;
            border: solid 1px #ddd;
            border-radius: 3px;
            padding: 0px 12px;
            background: #f7f7f7;
        }
        
        .operation .items li:hover
        {
            background: #e5f6ff;
            border: solid 1px #a8d0f1;
        }
        
        .operation .items li.back
        {
            width: 59px;
            background: url(/content/images/operation.gif) 0 0;
            border: none;
            padding: 0;
        }
        .operation .items li.back:hover
        {
            border: none;
            background: url(/content/images/operation.gif) 0 -70px;
        }
        .operation .items li.upload
        {
            border: none;
            padding: 0;
        }
        .operation .items li.upload:hover
        {
            border: none;
        }
        .operation .items li.line
        {
            cursor: default;
            width: 0px;
            border: none;
            border-left: dashed 1px #ccc;
            padding: 0;
            height: 33px;
            background: #fff;
            display: inline-block;
        }
       .nofilebg
        {
            background:url(/content/images/ziliao.jpg) no-repeat center center;
        }
        /*uploadify 上传按钮样式*/
        .uploadify:hover .uploadify-button
        {
            background-position: center bottom;
        }
        .swfupload
        {
            left: 74px;
        }
    </style>
    <script type="text/javascript">
        //全局变量
        var datagrid;
        var pid = 0; //标识文件夹层次
        var urls=[];//记录访问url用于返回操作
        urls.push("/diskn/CollectionFolderList");//记录初始url
        function LoadData(){
            $.ajax({
                url: "/diskn/CollectionFolderList",
                type: 'post',
                async: false,
                success: function (data) {
                    //如果没有数据，则显示图片
                    if (!$.isArray(data)||data.length==0) {
                        $("#center").addClass("nofilebg");
                    }
                },
                error: function () {
                    tip.Tip({ icon: "error", msg: "数据加载出错！" });
                }
            });
        datagrid = new Datagrid({
                id: "#filelist",
                url: "/diskn/CollectionFolderList",
                columns: [
                {
                    field: 'id'
                },
                {
                    checkbox: true,
                    field: 'fn',
                    title: '文件名',
                    width: '50%'
                },
                {
                    field: 'c_state',
                    title: '收集状态',
                    width: '15%'
                },
                {
                    field: 'qx_state',
                    title: '权限设置',
                    width: '15%'
                },
                {
                    field: 'ct',
                    title: '创建时间',
                    width: '20%'
                },
            ],
                OnCheck: function () {
                    //alert(datagrid.GetSelect()[0].id);

                },
                Rclick:function(e){
                      if(e.which==3){
                        var x=e.clientX+10;
                          var y=e.clientY+10;
                           popmenu.css({top:y,left:x}).show();
                           $(".ch").addClass("off").removeClass("on");
                           $(this).find(".ch").addClass("on").removeClass("off");
                      }
                }
            });
        };
        $(function () {
            upload = $("#file_upload_1").uploadify({
                // formData: { 'uid': global.uid },
                height: 33,
                width: 79,
                buttonImage: '/Content/uploadify/uploadbutton.png',
                buttonText: '',
                queueID: 'uplist',
                swf: '/Content/uploadify/uploadify.swf',
                uploader: '/Home/UploadFile',
                itemTemplate: '<ul class="itemlist" id="${fileID}" ><li class="yunicon">${fileName}</li><li class="size">${fileSize}</li><li class="progress"></li><li class="cancle" onclick="javascript:$(\'#${instanceID}\').uploadify(\'cancel\', \'${fileID}\')">&nbsp;</li><li class="progressbar"></li></ul>',
                onUploadStart: function (file) {
                    UploadStart(file);
                },
                onUploadProgress: function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                    UploadProgress(file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal);
                },
                onUploadSuccess: function (file, data, response) {
                    UploadSuccess(file, data, response);
                },
                onQueueComplete: function (queueData) {
                    QueueComplete(queueData);
                }
            });
            win = new Win({
                classname: ".upwin",
                Cancle: function () {
                }
            });
            tip = new Tip();
            parent.tip=tip;
            
            LoadData();

            popmenu=$(".popmenu").mouseleave(function(){
                $(this).hide();
            });
            popmenu.find("li>a").click(function(){
                popmenu.hide();
            })
            parent.datagrid=datagrid;
            //屏蔽浏览器右键菜单，和文本选择功能（兼容各浏览器）
            $(document).bind("contextmenu",function(){return false;}).bind("selectstart",function(){return false;});
        });
        function UploadStart(file) {
            upload.uploadify('settings', 'uploader', "/diskn/UploadFile?uid="+<%=ViewData["uid"] %>+"&pid="+pid+"&uptype=wyszj");
            win.Show().Up().SetTitle("正在上传");
        }
        function UploadProgress(file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal, _this) {

        }
        function UploadSuccess(file, data, response) {
            win.SetTitle("上传成功").ClearQ(6);
            $("#" + file.id).find(".progressbar").css("width", "0");
        }
        function QueueComplete(queueData) {
            win.Down().SetTitle("上传完毕");
            datagrid.Refresh();
            //去掉背景
            $("#center").removeClass("nofilebg");
        }
        //新建收集夹操作
        function NewCFolder() {
            datagrid.AddNewFolder("新建收集夹", function (foldername) {
                //alert(foldername);
                $.ajax({
                    url: '/diskn/DoNewCFolder',
                    type: 'post',
                    data: { foldername: foldername, pid: pid },
                    datatype: "json",
                    success: function (data) {
                        if (data.error == 0) {
                            //GetFileList(type, tempurl);
                            datagrid.Refresh();
                            //去掉背景
                           $("#center").removeClass("nofilebg");
                            tip.Tip({ icon: "ok", msg: data.msg });
                        } else {
                            tip.Tip({ icon: "error", msg: data.msg });
                        }
                    },
                    error: function () {
                        tip.Tip({ icon: "error", msg: "新建收集夹错误" });
                    }
                });
            });
        }

        //删除收集夹操作2014-11-1 by ao
        function DelCFolder(){
            var rows = datagrid.GetSelect();//获取选中的列
            if(rows.length<=0){
                tip.Tip({ icon: "error", msg: "未选择任何收集夹！" });
            }else{
                parent.$.messager.confirm('提示', '确认要删除选中的收集夹吗？', function (r) {
                    if(r){
                        var ids=[];
                        for (var i = 0; i < rows.length; i++) {
                           var c=rows[i];
                           ids.push(c.id);
                        }
                        $.ajax({
                            url:'/diskn/wyjzl_delcfolder',
                            data:{idStr:ids.toString()},
                            type:'post',
                            success:function(data){
                                if(data=="ok"){
                                    tip.Tip({ icon: "ok", msg: "删除收集夹成功！" });
                                    datagrid.Refresh();
                                }else{
                                     tip.Tip({ icon: "error", msg: "删除收集夹失败！" });
                                }
                            },
                            error:function(){
                                tip.Tip({ icon: "error", msg: "删除收集夹错误！" });
                            }
                        
                        });
                    }
                })
            }
        }

        function Rename() {
            var rows = datagrid.GetSelect();
            if(rows.length==1){
                 selrow=$(".on").parent().parent();
                var str = '<span class="n">\
                        <input type="text" class="txt" size="35" value="' + rows[0].n + '" />\
                        <a href="javascript:void(0)" class="btn_ok" title="确定" ></a>\
                        <a href="javascript:void(0)"  class="btn_cancle" title="取消"></a>\
                    </span>\ ';
            var item = $(str);
            //移除
            var oitem=selrow.find(".fname").find(".n").remove();
            //添加
            var txt=selrow.find(".fname").append(item).find(".txt").focus();

            //回车事件
            txt.keypress(function (e) {
                if (e.which == 13) {
                    item.find(".btn_ok").click();
                }
            });
            //动态绑定取消按钮事件
            item.find(".btn_cancle").click(function () {
                selrow.find(".fname").find(".n").remove();
                selrow.find(".fname").append(oitem);
            });
            item.find(".btn_ok").click(function () {
                $.ajax({
                    url: '/diskn/ReName',
                    type: 'post',
                    data: {id:rows[0].id, filename: selrow.find(".txt").val() },
                    success: function (data) {
                        if (data=="ok") {
                            datagrid.Refresh();
                        }
                    },
                    error: function () { 
                        
                    }
                });
            });
            }

        }
        function DownLoad() {
            var rows=datagrid.GetSelect();
            //console.info(rows);
            if (rows.length==1) {
                window.location.href = "/diskn/download?type=szl&id="+rows[0].id;
            }
            
        }
        function Delete(){
            var rows=datagrid.GetSelect();
            if (rows.length>0) {
                parent.$.messager.confirm('提示', '删除的文件(夹)将放入回收站<br>你确定要删除吗？', function (r) {
                   if(r){
                        var ids=[];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);
                        }
                        Update(ids.toString(),1,function(data){
                            if(data=="ok"){
                                 tip.Tip({ icon: "ok", msg: "删除成功！" });
                                 datagrid.Refresh();
                            }else{
                                tip.Tip({ icon: "error", msg: "删除失败！" });
                            }
                        }) 
                   }
                });
            }
  
        }
        //更新文件的操作(1代表删除，0代表还原)
        function Update(ids,value,callback) {
            $.ajax({
                url: '/diskn/UpdateFile',
                type: 'post',
                data: { ids: ids, value: value },
                success: function (data) {
                    if(callback) callback.call(this,data);
                },
                error: function () {
                    alert("操作错误！")
                }
            });
        }
            //移动文件的操作
        function Move() {
            var rows=datagrid.GetSelect();
            if (rows.length>0) {
                var ids=[];
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].id);
                }
                parent.$.modalDialog({
                    title: '文件移动',
                    width: 400,
                    height: 400,
                    href: "/diskn/move?pid="+pid,
                    modal: true,
                    buttons: [{
                        text: '确定',
                        handler: function () {
                            //执行移动操作
                            parent.$.move(ids.toString());
                            //刷新列表
                           datagrid.Refresh();
                        }
                    }, {
                        text: '取消',
                        handler: function () {
                            parent.$.modalDialog.handler.dialog('close');
                        }
                    }]
                });
            }
            
        }
        //共享操作
        function Share() {
            var rows=datagrid.GetSelect();
            if(rows.length<0){return;}
            //只支持单文件共享
            var url = "/diskn/Nshare?file_id="+rows[0].id;
            parent.$.modalDialog({
                title: '文件共享',
                width: 820,
                height: 550,
                content: '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;"></iframe>',
                modal: true
            });
        }
        //推送文件
       function PushFile() {
            var rows=datagrid.GetSelect();
            var ids=[];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].id);
            }
           window.open("/Push/Index?file_id=" + ids.toString());
        }
        function Back(){
           
           if(urls.length==1){
                datagrid.Reload(urls[0]);
           }else{
                urls.pop();
                datagrid.Reload(urls[urls.length-1]);
           }
           
        }

       function GetDataList(id){
        window.location.href="/diskn/wyszl_files?pid="+id;
       }

      function SetUsers() {
       var rows=datagrid.GetSelect();
        var l=rows.length;
        if(l==0){
             tip.Tip({ icon: "error", msg: "请选择要操作的收集夹！" });
        }else if(l>1){
             tip.Tip({ icon: "error", msg: "只能操作一个收集夹！" });
             datagrid.UnCheckAll();
        }else{
            var ids=[];
           for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].id);
           }
            var url = "/diskn/SetUsers?fids="+ids.toString();
            parent.$.modalDialog({
                title: '设置权限',
                width: 820,
                height: 550,
                content:'<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;"></iframe>',
                modal: true
            });
        }
       
    }
    function EndCollection(){
        var rows=datagrid.GetSelect();
        var l=rows.length;
        if(l==0){
             tip.Tip({ icon: "error", msg: "请选择要操作的收集夹！" });
        }else if(l>1){
             tip.Tip({ icon: "error", msg: "只能操作一个收集夹！" });
             datagrid.UnCheckAll();
        }else{
            $.ajax({
                url:'/diskn/SetCollectionState',
                type:'post',
                data:{folderid:rows[0].id,value:1},
                success:function(){
                     tip.Tip({ icon: "ok", msg: "状态设置成功！" });
                     datagrid.Refresh();
                },
                error:function(){
                    tip.Tip({ icon: "error", msg: "状态设置错误！" });
                }
            });
        }
    }
    function StartCollection(){
        var rows=datagrid.GetSelect();
        var l=rows.length;
        if(l==0){
             tip.Tip({ icon: "error", msg: "请选择要操作的收集夹！" });
        }else if(l>1){
             tip.Tip({ icon: "error", msg: "只能操作一个收集夹！" });
             datagrid.UnCheckAll();
        }else{
            $.ajax({
                url:'/diskn/SetCollectionState',
                type:'post',
                data:{folderid:rows[0].id,value:0},
                success:function(){
                     tip.Tip({ icon: "ok", msg: "状态设置成功！" });
                     datagrid.Refresh();
                },
                error:function(){
                    tip.Tip({ icon: "error", msg: "状态设置错误！" });
                }
            });
        }
    }
    </script>
</head>
<body class="easyui-layout">
    <div region="north" style="height: 111px;" border="false">
        <div>
        </div>
        <div class="path">
            我要收资料</div>
        <!--操作按钮-->
        <div class="operation">
            <ul class="items">
            <li class="back" onclick="Back()">返回</li>
            <li class="newcfolder" onclick="NewCFolder()">新建收集夹</li>
            <li class="delcfolder" onclick="DelCFolder()">删除</li>
            <li class="users" onclick="SetUsers()">设置权限</li>
            <li class="startcollection" onclick="StartCollection()">开始收集</li>
            <li class="endcollection" onclick="EndCollection()">结束收集</li>
            </ul>
        </div>
    </div>
    <div id="center" region="center" border="false">
        <!--数据表格-->
        <div id="filelist">
        </div>
    </div>
    <div class="upwin">
    </div>

     <!--右键菜单-->
    <ul class="popmenu">
        <li class="fx"><a href="javascript:void(0)" onclick="SetUsers();" >设置权限</a></li>
        <li class="share1"><a href="javascript:void(0)" onclick="StartCollection();" >开始收集</a></li>
        <li class="push"><a href="javascript:void(0)" onclick="EndCollection()" >结束收集</a></li>
    </ul>
</body>
</html>
