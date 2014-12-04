<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .path .filters
        {
            list-style-type: none;
            position: absolute;
            top: 0;
            left: 100px;
        }
        .path .filters li
        {
            float: left;
            text-indent:0px;
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
            background: url(/content/images/ziliao.jpg) no-repeat center center;
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
        
        .filters li.search
        {
            margin-top: 5px;
            margin-left:100px;
            border: none;
            cursor: default;
            height: 33px;
            line-height: 33px;
        }
        .filters li.search .searchtxt
        {
            border: solid 1px #ccc;
            border-radius: 5px;
            padding: 7px 5px;
            outline: none;
        }
        
        .filters li.search .searchtxt:focus
        {
            border: solid 1px #388FC9;
        }
        .filters li.search .searchbtn
        {
            background: url(/content/images/search_btn.gif);
            display: inline-block;
            width: 33px;
            height: 33px;
            outline: none;
        }
        .filters li.search .msg
        {
            color: #e45c27;
        }
        .filters li a
        {
            color:#999;
           margin-left:10px;
         }

        .filters li a.sel
        {
            font-weight: bold;
            border-radius: 3px;
            padding: 5px;
            color: #FDFAFA;
            background: #7BADF8;
            border: solid 1px #5279F0;
        }
    </style>
    <script type="text/javascript">
        //全局变量
        var pid = 0; //标识文件夹层次
        var urls=[];//记录访问url用于返回操作
        urls.push("/diskn/GetFileDataList?actions=all&pid="+pid);//记录初始url
        var datagrid;
        function ExitFile(url){
              $.ajax({
                url: url,
                type: 'post',
                async: false,
                success: function (data) {
                    //如果没有数据，则显示图片
                    if (!$.isArray(data)||data.length==0) {
                        $("#center").addClass("nofilebg");
                    }else{
                       $("#center").removeClass("nofilebg");
                    }
                },
                error: function () {
                    tip.Tip({ icon: "error", msg: "数据加载出错！" });
                }
            });
        }
        function LoadData(){
        var url="/diskn/GetFileDataList?actions=all&pid="+pid;
        ExitFile(url);
        datagrid = new Datagrid({
                id: "#filelist",
                url: url,
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
                    field: 'fz',
                    title: '大小',
                    width: '20%'
                },
                {
                    field: 'ut',
                    title: '上传时间',
                    width: '30%'
                }

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
        }
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

            var searchurl='';
            var filter='';
            
            //文件筛选
            $(".fl").click(function(){
                $(".fl").removeClass("sel");
                $(this).addClass("sel");
                var url='';
                var val=$(this).attr("type");
                if(val=="0"){
                    searchurl='/diskn/GetFileDataList?actions=all&pid='+pid;
                    filter='';
                }else{
                    searchurl='/diskn/GetFileDataList?actions=all&pid='+pid+'&filter='+val;
                    filter=val;
                }
                datagrid.Reload(searchurl);
            });
            //文件搜索
            $(".searchbtn").click(function(){
                var keyword=$.trim($(this).siblings(".searchtxt").val());
                if(filter!=''){
                    searchurl='/diskn/GetFileDataList?actions=all&pid='+pid+'&keyword='+keyword+'&filter='+filter;
                }else{
                    searchurl='/diskn/GetFileDataList?actions=all&pid='+pid+'&keyword='+keyword;
                }
                datagrid.Reload(searchurl);
                
            });
        });
        function UploadStart(file) {
            var userId=<%=ViewData["uid"]%>;

            var url="/diskn/UploadFile?uid="+userId+"&pid="+pid;
            upload.uploadify('settings', 'uploader',url);
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
            //移除背景
            $("#center").removeClass("nofilebg");
        }
        //新建文件夹操作
        function NewFolder() {
            datagrid.AddNewFolder("新建文件夹", function (foldername) {
                //alert(foldername);
                $.ajax({
                    url: '/diskn/DoNewFolder',
                    type: 'post',
                    data: { foldername: foldername, pid: pid },
                    datatype: "json",
                    success: function (data) {
                        if (data.error == 0) {
                            datagrid.Refresh();
                            //移除背景
                           $("#center").removeClass("nofilebg");
                            tip.Tip({ icon: "ok", msg: data.msg });
                        } else {
                            tip.Tip({ icon: "error", msg: data.msg });
                        }
                    },
                    error: function () {
                        tip.Tip({ icon: "error", msg: "新建文件夹错误" });
                    }
                });
            });
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
            if(rows.length==0){
                tip.Tip({ icon: "error", msg: "没有选择任何文件(夹)" });
            }
            else if (rows.length==1 && rows[0].ft==0) {
                window.location.href = "/diskn/download?type=mydoc&id="+rows[0].id;
            }
            else
            {
               var idList=[];
               for (var i = 0; i < rows.length; i++) {
                  idList.push(rows[i].id);
               }
               //console.info(idList.toString());
              // tip.Tip({ icon: "error", msg: "只支持单文件下载" });
               window.location.href = "/diskn/DownLoad_M?type=mydoc&ids="+idList.toString();
            }
            
        }
        function Delete(){
            var rows=datagrid.GetSelect();
            if (rows.length>0) {
                if(CheckShareToFolder(rows)){
                     tip.Tip({ icon: "error", msg: "选中列表中含有共享给别人的文件夹，请先取消共享后在删除！" });
                     return;
                }
                if(CheckShareFolder(rows)){
                     tip.Tip({ icon: "error", msg: "选中列表中含有别人共享过来的文件夹，不能删除！" });
                     return;
                }
                parent.$.messager.confirm('提示', '删除的文件(夹)将放入回收站<br>你确定要删除吗？', function (r) {
                   if(r){
                        var ids=[];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);
                        }
                        Update(ids.toString(),1,function(data){
                            if(data=="ok"){
                                 tip.Tip({ icon: "ok", msg: "删除成功！" });
                                  ExitFile();
                                 datagrid.Refresh();
                            }else{
                                tip.Tip({ icon: "error", msg: "删除失败！" });
                            }
                        }) 
                   }
                });
            }
  
        }

        //检查选中的列表中是否有共享给别人的文件

        function CheckShareToFolder(rows){
            for (var i = 0; i < rows.length; i++) {
                var row=rows[i];
                if(row.ft==2){
                    return true;
                }
            }
            return false;
        }
        //检查选中列表是否有共享文件夹
        function CheckShareFolder(rows){
            for (var i = 0; i < rows.length; i++) {
                var row=rows[i];
                if(row.ft==3){
                    return true;
                }
            }
            return false;
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
            } else{
                tip.Tip({ icon: "error", msg: "请选择要移动的文件！" });
            };
            
        }
        //共享操作
        function Share() {
            var rows=datagrid.GetSelect();
            if(rows.length<0){tip.Tip({ icon: "error", msg: "请选择要共享的文件！" });return;}
            if(rows.length>1){tip.Tip({ icon: "error", msg: "一次只能共享一个文件(夹)！" });datagrid.UnCheckAll();return;}
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
        //取消共享操作
        function UnShare() {
            var rows=datagrid.GetSelect();
            if(rows.length<0){tip.Tip({ icon: "error", msg: "请选择要取消共享的文件！" });return;}
            if(rows.length>1){tip.Tip({ icon: "error", msg: "一次只能操作一个文件(夹)！" });datagrid.UnCheckAll();return;}
            var row=rows[0];
            var type=row.ft;
            if(type!=2){
                tip.Tip({ icon: "error", msg: "该文件夹不是共享文件夹！" });
                return;
            }

            parent.$.messager.confirm('提示', '确定取消共享当前文件夹吗？', function (r) {
                   if(r){
                    //取消共享文件夹：1.将该文件状态变为普通文件夹 2.删除共享文件夹时在对应用户中生成的文件夹
                    var url = "/diskn/Unshare?file_id="+rows[0].id;
                    $.ajax({
                        url:url,
                        type:'post',
                        success:function(data){
                            if(data=="ok"){
                                datagrid.Refresh();
                            }else{
                                tip.Tip({ icon: "error", msg: "取消共享失败！" });
                            }
                        },
                        error:function(){
                            tip.Tip({ icon: "error", msg: "取消共享错误！" });
                        }
                    });
                  }
              });

        }
        //推送文件
       function PushFile() {
            var rows=datagrid.GetSelect();
            if (rows&&rows.length>0) {
                //文件推送检测
                for (var i = 0; i < rows.length; i++) {
                    if(rows[i].ft!=0){
                        tip.Tip({ icon: "error", msg: "只支持文件推送！" });
                        datagrid.UnCheckAll();
                        return;
                     }
                }
                //文件推送检测结束
                var ids=[];
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].id);
                }
                window.open("/Push/Index?file_id=" + ids.toString());
            }else{
                tip.Tip({ icon: "error", msg: "请选择要推送的文件！" });
            }
        }
        //返回按钮
        function Back(){
           if(urls.length==1){
                ExitFile(urls[0]);
                datagrid.Reload(urls[0]);
                
           }else{
                urls.pop();
                if(urls.length==1){pid=0;}//最外层文件夹
                 ExitFile(urls[urls.length-1]);
                datagrid.Reload(urls[urls.length-1]);
           }
        }

       function GetDataList(id){
         var Data=datagrid.Data;
         var row;
         for (var i = 0; i < Data.length; i++) {
            if(Data[i].id==id){
                row=Data[i];
            }
         }
         if (row&&(row.ft==1||row.ft==2||row.ft==3)) {
            pid=row.id;
            var url="/diskn/GetFileDataList?actions=all&pid="+pid;
            if(row.uid!=0){
                url+=("&uid="+row.uid);
            }
            urls.push(url);
            //如果没有文件显示图片
            ExitFile(url);
            datagrid.Reload(url); 
         }else {
            //只支持单文件下载
            window.location.href = "/diskn/download?type=mydoc&id="+row.id;
         }
           
       }

       function Fx(){
            var rows=datagrid.GetSelect();
            if (rows&&rows.length>0) {
                
            //文件分享检测（只有文件才能分享）
                for (var i = 0; i < rows.length; i++) {
                    if(rows[i].ft!=0){
                        tip.Tip({ icon: "error", msg: "只支持文件分享！" });
                        datagrid.UnCheckAll();
                        return;
                     }
                }
             //文件分享检测结束
                var ids=[];
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].id);
                }
               var url="/diskn/FxDialog1?filecount="+rows.length;
               parent.$.modalDialog({
                    title: '文件共享',
                    width: 575,
                    height: 540,
                    content: '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;"></iframe>',
                    modal: true,
                    buttons: [{
                        text: '确定',
                        handler: function () {
                            //执行移动操作
                             parent.$.Next(ids.toString());
                        }
                    }, {
                        text: '取消',
                        handler: function () {
                            parent.$.modalDialog.handler.dialog('close');
                        }
                    }]
                });
            }else{
                tip.Tip({ icon: "error", msg: "请选择要分享的文件！" });
            }
       }
    </script>
</head>
<body class="easyui-layout">
    <div region="north" style="height: 111px;" border="false">
        <div>
        </div>
        <div class="path">
            我的资料
            <!--筛选选项卡-->
            <ul class="filters">
                <li><a href="javascript:void(0)" type="0" class="sel fl">全&nbsp;&nbsp;部</a></li><li><a type="3" href="javascript:void(0)" class="fl">
                    别人共享</a></li><li><a type="2" href="javascript:void(0)" class="fl">我的共享</a></li>
                <!--搜索选项卡-->
                <li class="search">
                    <input type="text"  class="searchtxt" placeholder="输入文件(夹)名" />
                    <a class="searchbtn" href="javascript:void(0)" title="搜索">&nbsp;</a> <span
                        class="msg"></span></li>
            </ul>
        </div>
        <!--操作按钮-->
        <div class="operation">
            <ul class="items">
                <li class="back" onclick="Back()">返回</li>
                <li class="upload">
                    <div id="file_upload_1" class="uploadify" style="height: 30px; width: 120px;">
                    </div>
                </li>
                <li class="newfolder" onclick="NewFolder()"><i></i>新建文件夹</li>
                <li class="download" onclick="DownLoad()"><i></i>离线下载</li>
                <li class="line"></li>
                <li class="rename" onclick="Rename()">重命名</li>
                <li class="del" onclick="Delete()">删除</li>
                <li class="move" onclick="Move()">移动</li>
                <li class="line"></li>
                <li class="fx" onclick="Fx()">分享</li>
                <li class="shared" onclick="Share()">共享</li>
                <li class="shared" onclick="UnShare()">取消共享</li>
                <li class="push" onclick="PushFile()">推送</li>
            </ul>
        </div>
    </div>
    <div id="center" region="center" border="false">
        <!--数据表格-->
        <div id="filelist">
        </div>
    </div>
    <!--上传窗口-->
    <div class="upwin">
    </div>
    <!--右键菜单-->
    <ul class="popmenu">
        <li class="fx"><a href="javascript:void(0)" onclick="Fx();">分享</a></li>
        <li class="share1"><a href="javascript:void(0)" onclick="Share();">共享</a></li>
        <li class="push"><a href="javascript:void(0)" onclick="PushFile()">推送</a></li>
        <li class="line"></li>
        <li class="rename"><a href="javascript:void(0)" onclick="Rename()">重命名</a></li>
        <li class="del"><a href="javascript:void(0)" onclick="Delete()">删除</a></li>
        <li class="move"><a href="javascript:void(0)" onclick="Move()">移动</a></li>
        <li class="line"></li>
        <li class="down"><a href="javascript:void(0)" onclick="DownLoad()">离线下载</a></li>
    </ul>
</body>
</html>
