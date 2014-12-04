<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>回收站</title>
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
            background:url(/content/images/huishouzhan.jpg) no-repeat center center;
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
        var datagrid;
        function LoadData() {
            $.ajax({
                url: '/diskn/DeletedFileList',
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
                url: "/diskn/DeletedFileList",
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
                    title: '删除时间',
                    width: '30%'
                }
            ],
                OnCheck: function () {
                    //alert(datagrid.GetSelect()[0].id);
                }
                , Rclick: function (e) {
                    if (e.which == 3) {
                        var x = e.clientX + 10;
                        var y = e.clientY + 10;
                        popmenu.css({ top: y, left: x }).show();
                        $(".ch").addClass("off").removeClass("on");
                        $(this).find(".ch").addClass("on").removeClass("off");
                    }
                }
            });
        }
        $(function () {
            tip = new Tip();
            parent.tip = tip;
            //加载数据
            LoadData();
            popmenu = $(".popmenu").mouseleave(function () {
                $(this).hide();
            });
            popmenu.find("li>a").click(function () {
                popmenu.hide();
            })
            //屏蔽浏览器右键菜单，和文本选择功能（兼容各浏览器）
            $(document).bind("contextmenu", function () { return false; }).bind("selectstart", function () { return false; });
        });
       function GetDataList(id){
       }
       function HuanYuan() {
           var rows = datagrid.GetSelect();
           var ids = [];
           for (var i = 0; i < rows.length; i++) {
               ids.push(rows[i].id);
           }
           $.ajax({
               url: '/diskn/UpdateFile',
               type: 'post',
               data: { ids: ids.toString(), value: 0 },
               success: function (data) {
                   if (data == "ok") {
                       datagrid.Refresh();
                       tip.Tip({ icon: "ok", msg: "数据还原成功！" });
                   } else {
                       tip.Tip({ icon: "error", msg: "数据还原失败！" });
                   }

               },
               error: function () {
                   tip.Tip({ icon: "error", msg: "数据还原出错！" });
               }
           });
       }
       function ClearAllFiles() {
           parent.$.messager.confirm('提示', '清空后数据将不能恢复，你确定清空回收站吗？', function (r) {
               if (r) {
                   $.ajax({
                       url: '/diskn/ClearALLFiles',
                       type: 'post',
                       success: function (data) {
                           if (data == "ok") {
                               tip.Tip({ icon: "ok", msg: "清空回收站成功！" });
                                datagrid.Refresh();
                                //移除背景
                                $("#center").addClass("nofilebg");
                           } else {
                               tip.Tip({ icon: "error", msg: "清空回收站失败！" });
                           }

                       },
                       error: function () {
                           tip.Tip({ icon: "error", msg: "清空回收站出错！" });
                       }
                   });
               }
           });
         
       }
    </script>
</head>
<body class="easyui-layout">
    <div region="north" style="height: 111px;" border="false">
        <div>
        </div>
        <div class="path">回收站</div>
        <!--操作按钮-->
        <div class="operation">
            <ul class="items">
            <li class="hy" onclick="HuanYuan()">还原</li>
            <li class="clearall" onclick="ClearAllFiles()">清空回收站</li>
            </ul>
        </div>
    </div>
    <div id="center" region="center" border="false">
        <!--数据表格-->
        <div id="filelist">
            
        </div>
    </div>
         <!--右键菜单-->
    <ul class="popmenu">
        <li ><a href="javascript:void(0)" onclick="HuanYuan();" >还原</a></li>
    </ul>
</body>
</html>
