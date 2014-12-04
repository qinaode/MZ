/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.group = {

    index : function(p) {
        this.list();
    },
    list : function(p) {
        p = (p || {});
        console.log("diskAllP:" + JSON.stringify(p));
        var loadstat = hl.diag.loading.show();
        // URL
        var url = hl.url.api("User/MyGroupInfoJson");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;
        var success = function(result) {
            hl.diag.loading.hide(loadstat);
            hl.diag.menu.hide();
            hl.tpl.diag.popStyleNoNav({
                id : "group",
                title : "组群",
                l : hl.biz.user.req,
                r : 0
            });
            hl.sys.hl$("up").style.display = "none";
            var main = $("#groupMain");

            var _h = $("#groupMain").height();
            var _w = $("#groupMain").width();

            var contentBg = ssdjs.dom.div(0, 0, _w, _h);
            contentBg.style.backgroundColor = "#fff";
            main.append(contentBg);

            // 搜索框
            var searchDiv = ssdjs.dom.div(0, 0, _w, 87);
            searchDiv.style.backgroundColor = "#c7c7c7";

            var inputW = ssdjs.dom.div(10, 15, _w - 20, 57);
            inputW.style.backgroundColor = "#fff";
            inputW.style.borderRadius = "10px";
            inputW.style.border = "1px solid #b3b3b3";

            var input = ssdjs.dom.input(0, 0, _w - 22, 57);
            input.id = "searchInput";
            input.style.border = "0px";
            input.style.backgroundColor = "transparent";
            //input.style.paddingLeft = "10px";
            //input.style.paddingRight = "10px";
            input.style.fontSize = "36px";
            inputW.appendChild(input);

            var inputBtn = ssdjs.dom.div(_w - 74, 7, 45, 45);

            inputBtn.style.backgroundImage = hl.url.cssimg("bsb1.png");
            inputBtn.style.backgroundRepeat = "no-repeat";
            inputW.appendChild(inputBtn);

            inputBtn.evtEnd(function(obj) {

                inputBtn.style.backgroundImage = hl.url.cssimg("bsb1.png");
                obj.style.marginTop = "0px";
            });
            inputBtn.evtMove(function(obj) {
                inputBtn.style.backgroundImage = hl.url.cssimg("bsb1.png");
                obj.style.marginTop = "0px";
            });
            inputBtn.evtStart(function(obj) {
                inputBtn.style.backgroundImage = hl.url.cssimg("bsb.png");
                obj.style.marginTop = "0px";
            });

            searchDiv.appendChild(inputW);

            // searchDiv.style.padding = "10px";
            main.append(searchDiv);

            var hscrollU = hl.tpl.scroll.v.create("hscrollM", 0, 87, _w, _h - 87);
            main.append(hscrollU);

            // 滑动区域scroll定义
            scrollU = hl.sys.scroll({
                id : "hscrollM",
                snap : false,
                vScrollbar : false,
                hScroll : false
            });

            var list = result.groups;
            if(list != null && list.length > 0) {
                for(var i = 0; i < list.length; i++) {
                    hl.tpl.scroll.v.append("hscrollM", hl.biz.group.item({
                        item : list[i]
                    }));
                }
                scrollU.refresh();
            }

        };
        var error = function(result) {
            console.log("失败");
            hl.diag.alert({
                m : "网路错误"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    item : function(p) {
        var item = p.item;
        var size = hl.sys.winSize();
        var divW = ssdjs.dom.div(null, null, size.x, 90);

        var head = ssdjs.dom.img(10, 12, 70, 70, hl.url.img("user/group.png"));
        divW.appendChild(head);

        var name = ssdjs.dom.text(100, 30, null, null, item.name);
        name.style.color = "#000";
        name.style.fontSize = "30px";
        divW.appendChild(name);
        return divW;

    }
}