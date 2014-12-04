/*
Tip提示小插件 依赖jquery
made by ao
2014-3-31

使用方法
$(function () {
tip = new Tip();
});
function show() {
tip.Tip({ time: "2000", icon: "ok", msg: "这里是提示消息" });
}

改进：封装成jquery插件形式，避免全局冲突.
目前函数实现方式，会冲突(同名函数冲突问题)

*/
function Tip() {
};
Tip.prototype = {
    Settings: {
        time: "3000",
        icon: "ok",
        msg: "操作出错！"
    },
    Tip: function (settings) {
        var _t = this;
        var icon = settings.icon ? settings.icon : _t.Settings.icon;
        var msg = settings.msg ? settings.msg : _t.Settings.msg;
        var time = settings.time ? settings.time : _t.Settings.time;
        //获取对象,因为每个tip是独立的，所以要注册到不同的对象上
        //才能在fadeOut的回调函数中对应清除。不然只会清除最后一个。
        var that = {};
        that.tip = $('<div class="tip"></div>').appendTo($("body"));
        var iconspan = $('<span class="icon ' + icon + '"></span>').appendTo(that.tip);
        that.msg = $('<span class="msg">' + msg + '</span>').appendTo(that.tip);
        that.tip.fadeIn().delay(time).fadeOut("", function () {
            that.tip.remove();
        });
        return this;
    }
};