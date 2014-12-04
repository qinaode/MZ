/**
 * 航海-游戏核心
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.hb = {
    hb : {
        _tick : 5000,
        _hb : 0,
        start : function() {
            hl.biz.hb.hb.get();
        },
        get : function() {
            var last = hl.data.pub.last();
            if(last == null) {
                last = 0;
            }
            var url = hl.url.api("Person/NewNoticeInfoJson");

            var sid = 0;
            // 默认值
            if(last != -1) {
                sid = last.item.SID;
            }
            var id = 0;
            id = hl.data.user.id;
            var data = "sid=" + sid;
            var user = hl.data.user.get();
            data += "&userID=" + user.id;
            //+ "&userId=" + id
            // var data = "";
            var success = function(result) {
                setTimeout(hl.biz.hb.hb.get, hl.biz.hb.hb._tick);
            };
            var error = function(result) {
                setTimeout(hl.biz.hb.hb.get, hl.biz.hb.hb._tick);
            };
            hl.req(url, success, error, data, true);
        }
    }
};
