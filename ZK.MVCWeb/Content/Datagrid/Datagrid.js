function Datagrid(settings) {
    this.Initial(settings);
};
Datagrid.prototype = {
    Setting: {
        id: "#filelist",
        OnCheck: null,
        url: null,
        columns: []
    },
    Data: [], //列表json数组
    //loadsuccess 加载成功
    State: "normal", //标识datagrid的状态,normal addfolder rename loading等
    Initial: function (settings) {
        var _t = this;
        if (settings != {}) _t.Setting = settings;
        _t.datagrid = $(_t.Setting.id);
        //添加表头
        _t.header = _t.AddHeader();
        _t.hfname = _t.header.find(".fname");
        _t.hcheckbox = _t.hfname.find(".ch");
        _t.hfsize = _t.header.find(".fisize");
        _t.hmtime = _t.header.find(".mtime");
        //加载数据
        _t.Load();
        /*注册事件*/
        //表头复选框点击事件
        _t.hcheckbox.click(function () {
            $(this).hasClass("off") ? _t.CheckAll() : _t.UnCheckAll();
            if (_t.Setting.OnCheck) _t.Setting.OnCheck.call(_t);
        });
        //列表复选框点击事件
        _t.datagrid.find(".datalist").find(".ch").live("click", function () {
            var _this = $(this);
            if (_this.hasClass("on")) {
                _this.addClass("off").removeClass("on");
                _this.parent().parent().removeClass("datalisthover");
            } else {
                _this.addClass("on").removeClass("off");
                _this.parent().parent().addClass("datalisthover");
            }
            if (_t.Setting.OnCheck) _t.Setting.OnCheck.call(_t);
        });
    },
    //AddHeader应该是私有函数,不能让外部调用(先这样)
    AddHeader: function () {
        var _t = this;
        var columns = _t.Setting.columns;
        var str = '<ul class="header">';
        for (var k in columns) {
            var column = columns[k];

            if (column.checkbox) {
                str += '<li class="fname" style="width:' + column.width + '"><span class="ch off"></span>' + column.title + '</li>';
            } else if (column.field == "id") { }
            else {
                str += '<li  style="width:' + column.width + '">' + column.title + '</li>';
            }

        }
        str += '</ul>';
        return $(str).appendTo(this.datagrid);
    },
    //Load函数应该是私有的，不能被外部访问(待改进)
    //执行成功后回调callbacksuccess函数
    Load: function (callbacksuccess) {
        var _t = this;
        //设置了url属性才发请求
        if (!_t.Setting.url) {
            return;
        }
        $.ajax({
            url: _t.Setting.url,
            type: 'post',
            datatype: 'json',
            success: function (data) {
                //如果没有fid就返回
                //if (!data[0].id) { return; }
                //如果没有数据就返回
                if (!$.isArray(data) || data.length == 0) {
                    $("#center").addClass("nofilebg");
                    return;
                } else {
                    $("#center").removeClass("nofilebg");
                };
                _t.Data = data;
                for (var k in data) {
                    _t.AddItem(data[k])
                }
                if (callbacksuccess) callbacksuccess.call(_t);
                $(".datalist").mousedown(function (e) {
                    if (_t.Setting.Rclick) _t.Setting.Rclick.call(this, e)
                });
            },
            error: function () {
                //alert("数据加载错误！");
            }
        });
    },
    //指定一个url地址重新加载数据
    Reload: function (url) {
        this.Setting.url = url;
        this.Refresh();
    },
    //发送请求刷新列表(不能直接用Data数据刷新，用Data不是及时更新方式)。
    //刷新方式跟结构有关，这里是采用先移除后添加的方式实现刷新。
    Refresh: function () {
        var _t = this;
        _t.RemoveAllItem();
        //加载成功后执行回调函数
        _t.Load(function () {
            _t.UnCheckAll(); //取消选择(主要是取消掉表头的选中状态)
            _t.State = "normal"; //初始化话状态
        });
    },
    //AddItem添加列应该是私有函数,不能让外部调用(先这样)
    AddItem: function (item) {
        var _t = this;
        var columns = _t.Setting.columns;
        var str = '<ul class="datalist">';
        for (var k in columns) {
            var column = columns[k];
            //如果没有该字段值，用'&nbsp;'撑开宽度
            var value = item[column.field] ? item[column.field] : '&nbsp;';
            if (column.field == "id") {
                str += '<li style="display:none" fid="' + item.id + '"></li>';
            }
            else if (column.checkbox) {
                str += '<li class="fname">\
                        <span class="ch off"></span>\
                        <span class="sicon ' + item.icon + '"></span>\
                        <span class="n">\
                            <a href="javascript:void(0)" onclick="GetDataList(' + item.id + ')" >' + value + '</a>\
                        </span>\
                    </li> ';
            } else {
                str += '<li class="' + column.field + '" style="width:' + column.width + '">' + value + '</li>';
            }

        }
        str += '</ul>';
        return this.datagrid.append(str);
    },
    CheckAll: function () {
        var _t = this;
        _t.datagrid.find(".fname").find(".ch").addClass("on").removeClass("off");
        _t.datagrid.find(".datalist").addClass("datalisthover");
        return _t;
    },
    UnCheckAll: function () {
        $(".fname").find(".ch").addClass("off").removeClass("on");
        $(".datalist").removeClass("datalisthover");
        return this;
    },
    //删除所有列表项（不包含表头）
    RemoveAllItem: function () {
        this.datagrid.find(".datalist").remove();
    },
    //返回所有选中的rows json数组数据
    GetSelect: function () {
        var _t = this;
        var selrows = _t.datagrid.find(".datalist").find(".on");
        var rows = [];
        selrows.each(function (i) {

            var selid = $(this).parent().parent().find("[fid]").attr("fid");
            for (var i = 0; i < _t.Data.length; i++) {
                if (_t.Data[i].id == selid) {
                    rows.push(_t.Data[i]);
                }
            }

        });
        return rows;
    },
    AddNewFolder: function (foldername, DoNewFolder) {
        var _t = this;
        if (_t.State == "newfolder") return _t;
        var fname = foldername ? foldername : "新建文件夹";

        var columns = _t.Setting.columns;
        var str = '<ul class="datalist">';
        for (var k in columns) {
            var column = columns[k];
            if (column.checkbox) {
                str += '<li class="fname">\
                    <span class="ch off"></span>\
                    <span class="sicon folder"></span>\
                    <span class="n">\
                        <input type="text" class="txt" size="35" value="' + fname + '" />\
                        <a href="javascript:void(0)" class="btn_ok" title="确定" ></a>\
                        <a href="javascript:void(0)" class="btn_cancle" title="取消"></a>\
                    </span>\
                </li>';
            } else if (column.field == "id") { }
            else {
                str += '<li class="' + column.field + '" style="width:' + column.width + '">—</li>';
            }
        }
        var dataItem = $(str);
        _t.header.after(dataItem);
        _t.State = "newfolder";
        //取消按钮事件
        dataItem.find(".btn_cancle").click(function () {
            dataItem.remove();
            _t.State = "normal";
        })
        //确定按钮事件
        dataItem.find(".btn_ok").click(function () {
            var foldername = $(".n>.txt").val();
            if (DoNewFolder) {
                DoNewFolder.call(_t, foldername);
                //_t.State = "normal";

            };
        });
        return _t;
    }
};

