<body>
    <link href="/content/ztree/css/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="/content/ztree/jquery.ztree.core-3.5.min.js" type="text/javascript"></script>
    <style type="text/css">
        .ztree li a, .ztree li a.curSelectedNode
        {
            padding: 5px 5px;
        }
        .ztree li span.button.switch
        {
            margin-top: 5px;
        }
        .ztree li span.button
        {
            width: 20px;
            height: 18px;
        }
        .ztree li span.button.ico_open
        {
            background: url(/content/images/btn_icon.gif);
            margin-right: 2px;
            background-position: -1px -225px;
            vertical-align: top;
        }
        .ztree li span.button.ico_close
        {
            background: url(/content/images/btn_icon.gif);
            margin-right: 2px;
            background-position: -1px -205px;
            vertical-align: top;
        }
        .ztree li span.button.ico_docu
        {
            background: url(/content/images/btn_icon.gif);
            margin-right: 2px;
            background-position: -1px -205px;
            vertical-align: top;
        }
    </style>
    <script type="text/javascript">
        
        var tree;
        var setting = {
            view: {
                dblClickExpand: false,
                showLine: false,
                selectedMulti: false
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "pId",
                    rootPId: ""
                }
            },
            callback: {
            //                beforeClick: function (treeId, treeNode) {
            //                    var zTree = $.fn.zTree.getZTreeObj("tree");
            //                    if (treeNode.isParent) {
            //                        zTree.expandNode(treeNode);
            //                        return false;
            //                    } else {
            //                        demoIframe.attr("src", treeNode.file + ".html");
            //                        return true;
            //                    }
            //                }
        }
    };
    $(function () {
        //            var zNodes = [
        //		{ id: 1, pId: 0, name: "根目录", open: false, iconClose: "/disknn/images/close.png", iconOpen: "/disknn/images/open.png" },
        //		{ id: 2, pId: 1, name: "文件夹2", icon: "/disknn/images/close.png" },
        //        { id: 3, pId: 1, name: "文件夹2", icon: "/disknn/images/close.png" },
        //        { id: 4, pId: 1, name: "文件夹2", icon: "/disknn/images/close.png" }
        //	];
        $.ajax({
            url: '/diskn/GetFolderJson',//返回的文件夹json数据不包括当前选中的文件夹
            type: 'post',
            datatype: 'json',
            success: function (zNodes) {
                tree = $.fn.zTree.init($("#tree"), setting, zNodes);
            },
            error: function () {

            }
        });
    });
    parent.$.move = function (ids) {
        var nodes = tree.getSelectedNodes();
        if (nodes.length > 0) {
            var pid = nodes[0].id;
            $.ajax({
                url: '/diskn/move',
                type: 'post',
                datatype: 'json',
                data: { ids: ids, pid: pid },
                success: function (data) {
                    //如果移动成功则关闭对话框
                    parent.$.modalDialog.handler.dialog('close');
                },
                error: function () {

                }
            });
        } else {
            alert("请选择文件夹！");
        }
    }
    function Test() {
        var treeObj = $.fn.zTree.getZTreeObj("tree");
        var nodes = treeObj.getSelectedNodes();
        console.info(nodes);
    }
    </script>
    <ul class="ztree" id="tree">
    </ul>
</body>
