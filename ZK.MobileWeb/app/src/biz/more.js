/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.more = {

    init : function() {
        hl.diag.menu.show({
            on : 3
        });
        hl.tpl.diag.pop({
            id : "more",
            title : "更多",
            l : 0,
            r : 0,
            on : 3
        });
        hl.sys.hl$("up").style.display = "none";
        var main = $("#moreMain");

        var _h = $("#moreMain").height();
        var _w = $("#moreMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollU = hl.tpl.scroll.v.create("hscrollM", 0, 0, _w, _h);
        main.append(hscrollU);
        console.log(" main.append(hscrollM)");
        // 滑动区域scroll定义
        scrollU = hl.sys.scroll({
            id : "hscrollM",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        var divTest = ssdjs.dom.div(null, null, _w, _h);

        var titleSet = ssdjs.dom.text(45, 20, null, null, "设置");

        titleSet.style.fontSize = "30px";
        divTest.appendChild(titleSet);

        var div0 = ssdjs.dom.div(20, 70, _w - 40, 261);
        div0.className = "ub ub-ver uba b-gra uc-a1 t-bla c-wh";
        divTest.appendChild(div0);
        var div1 = ssdjs.dom.div(null, null, null, 68);
        div1.className = "ubb b-gra uinn uc-t1 ";

        var t1 = ssdjs.dom.text(20, 30, null, null, "字体大小");
        t1.style.fontSize = "30px";
        div1.appendChild(t1);

        var icoR = ssdjs.dom.img(_w - 70, 29, 19, 29, hl.url.img("icon3.png"));
        div1.appendChild(icoR);

        div0.appendChild(div1);
        var div2 = ssdjs.dom.div(null, null, null, 68);
        div2.className = "ubb b-gra c-m1 uinn";

        var t1 = ssdjs.dom.text(20, 30, null, null, "消息通知");
        t1.style.fontSize = "30px";
        div2.appendChild(t1);

        var icoR = ssdjs.dom.img(_w - 70, 29, 19, 29, hl.url.img("icon3.png"));
        div2.appendChild(icoR);

        div0.appendChild(div2);
        var div3 = ssdjs.dom.div(null, null, null, 68);
        div3.className = "uinn uc-b1";

        var t1 = ssdjs.dom.text(20, 28, null, null, "安全与隐私");
        t1.style.fontSize = "30px";
        div3.appendChild(t1);

        var icoR = ssdjs.dom.img(_w - 70, 29, 19, 29, hl.url.img("icon3.png"));
        div3.appendChild(icoR);

        div0.appendChild(div3);

        var titleAbout = ssdjs.dom.text(45, 378, null, null, "关于");

        titleAbout.style.fontSize = "30px";
        divTest.appendChild(titleAbout);

        divTest.appendChild(titleSet);
        var div01 = ssdjs.dom.div(20, 426, _w - 40, 87);
        div01.className = "ub ub-ver uba b-gra uc-a1 t-bla c-wh";
        var t1 = ssdjs.dom.text(20, 28, null, null, "关于皓联");
        t1.style.fontSize = "30px";
        div01.appendChild(t1);

        var icoR = ssdjs.dom.img(_w - 70, 29, 19, 29, hl.url.img("icon3.png"));
        div01.appendChild(icoR);

        divTest.appendChild(div01);

        var btnExit = ssdjs.dom.div(10, 554, _w - 20, 70);

        var bg0 = ssdjs.dom.img(0, 0, _w - 20, 70, hl.url.img("more/exit0.png"));
        btnExit.appendChild(bg0);
        var bg1 = ssdjs.dom.img(0, 0, _w - 20, 70, hl.url.img("more/exit1.png"));
        bg1.style.display = "none";
        btnExit.appendChild(bg1);

        var text = ssdjs.dom.text(0, 18, _w - 20, null, "退出");
        text.style.textAlign = "center";
        text.style.color = "#ffffff";
        text.style.fontSize = "30px";
        btnExit.appendChild(text);

        btnExit.evtEnd(function(obj) {
            bg1.style.display = "none";
            // 清空登录信息
            hl.data.user.set({
                user : {
                }
            });
            // appCan退出程序
            uexWidgetOne.exit();

        });
        btnExit.evtMove(function(obj) {
            bg1.style.display = "none";
        });
        btnExit.evtStart(function(obj) {
            bg1.style.display = "";
        });
        divTest.appendChild(btnExit);

        hl.tpl.scroll.v.append("hscrollM", divTest);
    }
}