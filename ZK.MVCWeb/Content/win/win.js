function Win(settings) {
    this.Initial(settings);
};
Win.prototype = {
    Initial: function (settings) {
        var str = '<div class="head">\
                        <span class="title">等待上传</span>\
                        <span class="min" title="最小化"></span>\
                        <span class="max" title="最大化"></span>\
                        <span class="close" title="关闭"></span>\
                     </div>\
                    <div class="uplist" id="uplist">\
                    </div>';
        var _t = this;
        _t.Settings = settings;
        _t.flag = "down";
        _t.Win = $(_t.Settings.classname);
        _t.Win.html(str);
        _t.Header = _t.Win.find(".head");
        _t.Title = _t.Header.find(".title");
        _t.Minbutton = _t.Header.find(".min");
        _t.Maxbutton = _t.Header.find(".max");
        _t.Closebutton = _t.Header.find(".close");
        _t.ItemList = _t.Win.find(".itemlist"); //上传文件队列
        _t.Minbutton.click(function () {
            _t.Down();
        });
        _t.Maxbutton.click(function () {
            _t.Up();
        });
        _t.Closebutton.click(function () {
            _t.Hide();
        });
        _t.Header.click(function () {
            _t.flag == "down" ? _t.Up() : _t.Down();
        });
        //暴露上传队列中的取消按钮点击事件
        _t.canclebtn = _t.Win.find(".cancle").live("click", function () {
            if (_t.Settings.Cancle) _t.Settings.Cancle.call(_t);
        });
        return this;
    },
    Settings: {
        classname: ".win",
        Uped: null,
        Cancle: null
    },
    Show: function () {
        this.Win.show();
        return this;
    },
    Hide: function () {
        this.Win.hide();
        this.ClearQ("all"); //默认关闭清除所有队列
        return this;
    },
    Down: function () {
        var _t = this;
        _t.Win.animate({ bottom: -360 }, 250, function () {
            _t.ShowMaxButton();
            _t.flag = "down";
        });

        return _t;
    },
    Up: function () {
        var _t = this;
        this.Win.animate({ bottom: 0 }, 250, function () {
            _t.ShowMinButton();
            _t.flag = "up";
            //执行参数暴露的Uped方法
            if (_t.Settings.Uped) _t.Settings.Uped.call(_t);
        });
        return _t;
    },
    ShowMaxButton: function () {
        this.Maxbutton.show();
        this.Minbutton.hide();
        return this;
    },
    ShowMinButton: function () {
        this.Maxbutton.hide();
        this.Minbutton.show();
        return this;
    },
    SetTitle: function (title) {
        this.Title.text(title);
        return this;
    },
    ClearQ: function (n) {
        var ItemList = this.Win.find(".itemlist");
        if (n == "all") {
            ItemList.remove();
        } else if (ItemList.size() >= n) {
            ItemList.first().remove();
        }
        return this;
    }
};