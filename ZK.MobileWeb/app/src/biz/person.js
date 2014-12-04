/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.person = {
    messageDetail : function(p) {
        var item = p.item;
        hl.diag.menu.hide();
        hl.tpl.diag.upStyleNoNav({
            id : "md",
            title : "公告",
            l : hl.diag.up.close,
            // hl.biz.person.pubMessage,
            r : 0
        });
        var main = $("#mdMain");

        var _h = $("#mdMain").height();
        var _w = $("#mdMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollU = hl.tpl.scroll.v.create("hscrollM", 0, 0, _w, _h);
        main.append(hscrollU);

        // 滑动区域scroll定义
        scrollU = hl.sys.scroll({
            id : "hscrollM",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        var divContent = ssdjs.dom.div(null, null, _w, _h);

        var title = ssdjs.dom.text(30, 30, _w - 60, null, item.t);
        title.style.color = "#505050";
        title.style.textAlign = "center";
        title.style.fontSize = "24px";
        divContent.appendChild(title);

        var contentText = ssdjs.dom.text(30, 90, _w - 60, null, item.c);
        contentText.style.color = "#505050";
        contentText.style.fontSize = "20px";
        divContent.appendChild(contentText);

        hl.tpl.scroll.v.append("hscrollM", divContent);
        scrollU.refresh();
    },
    /**
     * 个性签名编辑页面
     */
    say : function(p) {
        hl.diag.menu.hide();
        hl.tpl.diag.popStyleNoNav({
            id : "me",
            title : "我的资料",
            l : hl.biz.person.detail,
            r : 0
        });
        hl.sys.hl$("up").style.display = "none";
        var main = $("#meMain");

        var _h = $("#meMain").height();
        var _w = $("#meMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollU = hl.tpl.scroll.v.create("hscrollM", 0, 0, _w, _h);
        main.append(hscrollU);

        // 滑动区域scroll定义
        scrollU = hl.sys.scroll({
            id : "hscrollM",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        var divContent = ssdjs.dom.div(null, null, _w, _h);

        var div01 = ssdjs.dom.div(10, 10, _w - 20, 208);
        div01.className = "ub ub-ver uba b-gra uc-a1 t-bla c-wh";

        var input = ssdjs.dom.textarea(1, 0, _w - 22, 208);
        input.id = "sayInput";
        input.style.border = "0px";
        input.style.backgroundColor = "transparent";
        input.style.fontSize = "22px";
        div01.appendChild(input);
        var user = hl.data.user.get();
        input.placeholder = user.say;
        divContent.appendChild(div01);

        hl.tpl.scroll.v.append("hscrollM", divContent);

    },
    /**
     * 我的资料
     */
    detail : function(p) {
        hl.diag.menu.hide();
        hl.tpl.diag.popStyleNoNav({
            id : "me",
            title : "我的资料",
            l : hl.biz.person.init,
            r : 0
        });
        hl.sys.hl$("up").style.display = "none";
        var main = $("#meMain");

        var _h = $("#meMain").height();
        var _w = $("#meMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollU = hl.tpl.scroll.v.create("hscrollM", 0, 0, _w, _h);
        main.append(hscrollU);

        // 滑动区域scroll定义
        scrollU = hl.sys.scroll({
            id : "hscrollM",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        var divTest = ssdjs.dom.div(null, null, _w, _h);

        var div01 = ssdjs.dom.div(20, 10, _w - 40, 108);
        div01.className = "ub ub-ver uba b-gra uc-a1 t-bla c-wh";
        var t1 = ssdjs.dom.text(20, 36, null, null, "头像");
        t1.style.fontSize = "30px";
        div01.appendChild(t1);
        var user = hl.data.user.get();
        var head = ssdjs.dom.img(_w - 210, 14, 80, 80, hl.url.api(user.head));
        head.className = "grtx";
        div01.appendChild(head);

        var icoR = ssdjs.dom.img(_w - 85, 39, 19, 29, hl.url.img("icon3.png"));
        div01.appendChild(icoR);

        divTest.appendChild(div01);

        var div02 = ssdjs.dom.div(20, 188, _w - 40, 87);
        div02.evtEnd(function(obj) {
            hl.biz.person.say();
        });
        div02.className = "ub ub-ver uba b-gra uc-a1 t-bla c-wh";
        var t2 = ssdjs.dom.text(10, 28, null, null, "个性签名");
        t2.style.fontSize = "30px";
        div02.appendChild(t2);

        var icoR = ssdjs.dom.img(_w - 85, 29, 19, 29, hl.url.img("icon3.png"));
        div02.appendChild(icoR);

        divTest.appendChild(div02);

        var div0 = ssdjs.dom.div(20, 340, _w - 40, 261);
        div0.className = "ub ub-ver uba b-gra uc-a1 t-bla c-wh";
        divTest.appendChild(div0);
        var div1 = ssdjs.dom.div(null, null, null, 68);
        div1.className = "ubb b-gra uinn uc-t1 ";

        var t1 = ssdjs.dom.text(20, 30, null, null, "昵称");
        t1.style.fontSize = "30px";
        div1.appendChild(t1);

        var nick = ssdjs.dom.text(75, 30, _w - 190, null, user.nick);
        nick.style.fontSize = "30px";
        nick.style.color = "#9f9f9f";
        nick.style.textAlign = "right";
        div1.appendChild(nick);

        var icoR = ssdjs.dom.img(_w - 85, 29, 19, 29, hl.url.img("icon3.png"));
        div1.appendChild(icoR);

        div0.appendChild(div1);
        var div2 = ssdjs.dom.div(null, null, null, 68);
        div2.className = "ubb b-gra c-m1 uinn";

        var t1 = ssdjs.dom.text(20, 30, null, null, "性别");
        t1.style.fontSize = "30px";

        div2.appendChild(t1);

        var sexText = "男";
        if(user.sex == 0) {
            sexText = "女";
        }
        var sex = ssdjs.dom.text(275, 30, _w - 390, null, sexText);
        sex.style.fontSize = "30px";
        sex.style.color = "#9f9f9f";
        sex.style.textAlign = "right";
        div2.appendChild(sex);

        var icoR = ssdjs.dom.img(_w - 85, 29, 19, 29, hl.url.img("icon3.png"));
        div2.appendChild(icoR);

        div0.appendChild(div2);
        var div3 = ssdjs.dom.div(null, null, null, 68);
        div3.className = "uinn uc-b1";

        var t1 = ssdjs.dom.text(20, 28, null, null, "生日");
        t1.style.fontSize = "30px";
        div3.appendChild(t1);

        var bir = ssdjs.dom.text(275, 30, _w - 390, null, user.birthday);
        bir.style.fontSize = "30px";
        bir.style.color = "#9f9f9f";
        bir.style.textAlign = "right";
        div3.appendChild(bir);

        var icoR = ssdjs.dom.img(_w - 85, 29, 19, 29, hl.url.img("icon3.png"));
        div3.appendChild(icoR);

        div0.appendChild(div3);

        hl.tpl.scroll.v.append("hscrollM", divTest);
    },
    /**
     *
     */
    personItem : function(id, x, y, w, h, ico, txt, fn) {
        var btn = ssdjs.dom.div(x, y, w, h);
        // var bg = ssdjs.dom.img(0, 0, w, h, hl.url.img("bg.jpg"));
        // btn.appendChild(bg);
        var bgLine = ssdjs.dom.img(0, h - 2, w, 2, hl.url.img("line.jpg"));

        var head = ssdjs.dom.img(20, 12, 102, 102, hl.url.img(ico));
        btn.appendChild(head);

        var message = ssdjs.dom.div(97, -4, 45, 45);
        message.id = id + "psm";
        message.style.backgroundImage = hl.url.cssimg("pub/imnb.png");
        var messageNum = ssdjs.dom.text(0, 7, 45, null, 5);
        messageNum.style.textAlign = "center";
        messageNum.id = id + "psmn";
        // personSystemMessageNum
        messageNum.style.color = "#fff";
        messageNum.style.fontSize = "24px";
        message.appendChild(messageNum);
        message.style.display = "none";
        btn.appendChild(message);

        var txt = ssdjs.dom.text(135, 45, null, null, txt);
        txt.style.color = "#505050";
        txt.style.fontSize = "36px";
        btn.appendChild(txt);

        btn.appendChild(bgLine);
        btn.evtEnd(function(obj) {
            if(fn != null) {
                fn();
            }
        });
        return btn;
    },
    /**
     * 删除公司公告
     */
    deletePubMessage : function() {
        var list = hl.data.pub.get();
        for(var i = 0; i < list.length; i++) {
            if(list[i].s == 2) {
                continue;
            }
            var item = list[i];
            if(hl.sys.$(item.item.SID + "checkPub").status == "on") {
                list[i].s = 2;
            }
        }
        hl.data.pub.update({
            list : list
        });
        hl.biz.person.pubMessage();
    },
    markSysMessage : function() {

        // TODO 判断是哪个列表确定list
        var list = hl.data.pub.get();
        for(var i = 0; i < list.length; i++) {
            if(list[i].s == 2) {
                continue;
            }
            var item = list[i];
            if(hl.sys.$(item.item.SID + "checkPub").status == "on") {
                list[i].s = 1;
            }
        }
        hl.data.pub.update({
            list : list
        });
        hl.biz.person.pubMessage();
    },
    init : function(p) {
        hl.sys.hl$("up").style.display = "none";
        console.log("hl.person.init");

        hl.diag.menu.show({
            on : 0
        });

        var size = hl.sys.winSize();

        $("#pop").empty();
        var popMain = hl.sys.hl$("pop");

        var topBg = ssdjs.dom.img(0, 0, size.x, 227, hl.url.img("geren_bg.png"));
        popMain.appendChild(topBg);

        // 网络头像
        var user = hl.data.user.get();
        var head = ssdjs.dom.img(40, 45, 139, 139, hl.url.api(user.head));
        head.className = "grtx";

        popMain.appendChild(head);

        var userInfo = hl.data.user.get();
        var nick = ssdjs.dom.text(240, 80, null, null, userInfo.nick);
        nick.className = "sMid";
        nick.style.color = "#FFF";

        popMain.appendChild(nick);

        var say = ssdjs.dom.text(240, 130, size.x - 350, null, user.say);
        say.style.color = "#FFF";

        popMain.appendChild(say);

        var content = ssdjs.dom.div(0, 227, size.x, size.y - 325);
        content.style.backgroundColor = "#f0f0f0";
        popMain.appendChild(content);

        var hscrollUU = hl.tpl.scroll.v.create("hscrollUU", 0, 227, size.x, size.y - 325);
        // hscrollUU.style.backgroundColor = "#f0f0f0";
        popMain.appendChild(hscrollUU);
        console.log(" main.append(hscrollUU)");
        // 滑动区域scroll定义
        scrollUU = hl.sys.scroll({
            id : "hscrollUU",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        var myInfo = hl.biz.person.personItem("person", null, null, size.x, 125, "person/person.png", "我的资料", function(obj) {
            hl.biz.person.detail();
        });
        hl.tpl.scroll.v.append("hscrollUU", myInfo);

        var sysMessage = hl.biz.person.personItem("sys", null, null, size.x, 125, "person/sys.png", "系统通知", function(obj) {
            console.log("sysMessage");
        });
        hl.tpl.scroll.v.append("hscrollUU", sysMessage);

        var pub = hl.biz.person.personItem("pub", null, null, size.x, 125, "person/pub.png", "通知公告", function(obj) {
            hl.biz.person.pubMessage();
        });
        hl.tpl.scroll.v.append("hscrollUU", pub);
        scrollUU.refresh();
        popMain.style.display = "";

        // 公司公告初始值
        var numNoSee = 0;
        var pppList = (hl.data.pub.get() || []);
        if(pppList.length > 0) {
            for(var i = 0; i < pppList.length; i++) {
                if(pppList[i].s == 0) {
                    numNoSee++;
                }
            }
        }
        if(numNoSee != 0) {
            $("#pubpsm").show();
            $("#pubpsmn").html(numNoSee);
        }

        // 公司公告动态刷新
        var time = setInterval(function() {
            var numNoSee = 0;
            var ppp2List = hl.data.pub.get();
            if(ppp2List.length > 0) {
                for(var i = 0; i < ppp2List.length; i++) {
                    if(ppp2List[i].s == 0) {
                        numNoSee++;
                    }
                }
            }
            if(numNoSee != 0) {
                // 页面更新
                $("#pubpsm").show();
                $("#pubpsmn").html(numNoSee);
                // 底部导航更新
                $("#personmarkPNum").html(numNoSee);
                $("#personmarkPub").show();
            }
        }, 1000);
    },
    req : function(p) {
        p = (p || {});
        console.log("person请求参数:" + JSON.stringify(p));
        // URL
        var url = "http://192.168.1.21:61095/User/LoginInfoJosn";
        var data = "";
        if(p.id != null) {
            data += "id=" + p.id;
        }
        if(p.pw != null) {
            data += "&pw=" + p.pw;
        }

        var success = function(result) {

            hl.biz.person.init();
            alert("成功了");
            console.log(result);

        };
        var error = function(result) {
            console.log(result);
            console.log("失败");
            return;
        };
        hl.req(url, success, error, data);
    },
    /**
     * 我的资料
     */
    myInfo : function(p) {

    },
    /**
     * 系统消息
     */
    sysMessage : function(p) {
        // var list = hl.data.sysMessage.get();
        hl.tpl.diag.pop({
            id : "sysMessage",
            title : "系统公告",
            l : hl.biz.person.init,
            r : 1,
            on : 0

        });
        var main = $("#sysMessageMain");

        var _h = $("#sysMessageMain").height();
        var _w = $("#sysMessageMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollS = hl.tpl.scroll.v.create("hscrollS", 0, 0, _w, _h);
        main.append(hscrollS);
        console.log(" main.append(hscrollS)");
        // 滑动区域scroll定义
        scrollS = hl.sys.scroll({
            id : "hscrollS",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        if(hl.data.pub.list != null && hl.data.pub.list.length > 0) {
            for(var i = 0; i < hl.data.pub.list.length; i++) {
                hl.tpl.scroll.v.append("hscrollS", hl.biz.person.sysMessageItem({
                    item : hl.data.pub.list[i]
                }));
            }
            scrollS.refresh();

        }

    },
    sysMessageItem : function(p) {

        var item = p.item;
        var size = hl.sys.winSize();
        var w = size.x;
        var h = 122;

        var divW = ssdjs.dom.div(null, null, w, h);
        var bgLine = ssdjs.dom.img(0, h - 2, w, 2, hl.url.img("line.jpg"));
        divW.appendChild(bgLine);

        var check = hl.tpl.btn.check(item.item.SID + "checkPub", 15, 17, "off", null);
        check.style.display = "none";
        divW.appendChild(check);

        var divMoveW = ssdjs.dom.div(0, 0, w, h);
        divMoveW.id = item.item.SID + "divMWPub";
        divW.appendChild(divMoveW);
        divMoveW.evtEnd(function(obj) {
            hl.biz.person.messageMark(obj.tag);
            hl.biz.person.messageDetail({
                item : {
                    t : obj.tag.item.TITLE,
                    c : obj.tag.item.CONTENT,
                }
            });

            // var w = 570;
            // if(size.x < 570) {
            // w = size.x;
            // }
            //
            // hl.tpl.diag.upStyle01({
            // location : {
            // w : w,
            // h : 400
            // },
            // item : {
            // t : obj.tag.item.TITLE,
            // c : obj.tag.item.CONTENT,
            // }
            // });

        });
        divMoveW.tag = item;

        var title = ssdjs.dom.text(45, 23, null, null, item.item.TITLE);
        // item.c
        title.style.color = "#505050";
        title.style.fontSize = "30px";

        divMoveW.appendChild(title);

        var content = ssdjs.dom.text(45, 65, null, null, item.item.CONTENT);
        // item.c
        content.style.color = "#9f9f9f";
        content.style.fontSize = "30px";
        divMoveW.appendChild(content);

        var time = ssdjs.dom.text(w - 100, 23, null, null, ssdjs.fmt.date(item.item.SENDTIME, "MM-dd"));
        // item.c
        time.style.color = "#9f9f9f";
        time.style.fontSize = "30px";
        divW.appendChild(time);

        //item.s == 0
        if(item.s == 0) {
            var mark = ssdjs.dom.img(10, 24, 30, 30, hl.url.img("pub/imns.png"));
            divMoveW.appendChild(mark);
        }

        return divW;

    },
    /**
     * 公司公告
     */
    pubMessage : function(p) {
        // var list = hl.data.sysMessage.get();
        hl.tpl.diag.popStyleNoNav({
            id : "pubMessage",
            title : "通知公告",
            l : hl.biz.person.init,
            r : hl.biz.person.edit
        });
        var main = $("#pubMessageMain");

        var _h = $("#pubMessageMain").height();
        var _w = $("#pubMessageMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollS = hl.tpl.scroll.v.create("hscrollS", 0, 0, _w, _h);
        main.append(hscrollS);

        // 滑动区域scroll定义
        scrollS = hl.sys.scroll({
            id : "hscrollS",
            snap : false,
            vScrollbar : true,
            hScroll : false,
            momentum : true,
            hideScrollbar : true
        });

        var sList = hl.data.pub.get();

        if(sList != null && sList.length > 0) {
            for(var i = 0; i < sList.length; i++) {
                if(sList[i].s == 2) {
                    continue;
                }
                hl.tpl.scroll.v.append("hscrollS", hl.biz.person.sysMessageItem({
                    item : sList[i]
                }));
            }
            scrollS.refresh();

        }
        hl.diag.menu.hide();

    },
    edit : function(p) {

        // 开启多选
        hl.biz.person.checkOn();

        // 底部菜单
        var size = hl.sys.winSize();
        var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
        // TODO 背景要修改成图片的
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";
        bottom.id = "bottomEdit";
        var _x = 0;

        // TODO 初始进入是删除按钮为不可点,待用户选择item之后删除按钮变为可点
        var btnDelete = hl.tpl.btnMenu("delete", _x, 0, size.x, 75, "", "删除", 0, function(obj) {
            hl.biz.person.deletePubMessage();
        });
        bottom.appendChild(btnDelete);
        $("#pop").append(bottom);
        $("#bottomEdit").animate({
            'top' : (size.y - 98 ) + 'px'
        }, 200);

        // 顶部操作栏的修改
        $("#title").remove();
        var title = ssdjs.dom.div(0, 0, size.x, 94);
        title.id = "title";
        var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
        title.appendChild(titleBg);

        var titleText = ssdjs.dom.text(0, 24, size.x, 50, "公司公告");
        titleText.style.color = "#FFF";
        titleText.style.textAlign = "center";
        titleText.style.fontSize = "40px";
        title.appendChild(titleText);

        var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
            hl.biz.person.pubMessage();
        });
        title.appendChild(btnBack);

        var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

            // 顶部操作栏的修改
            $("#title").remove();
            var title = ssdjs.dom.div(0, 0, size.x, 94);
            title.id = "title";
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, "公司公告");
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";
            titleText.style.fontSize = "40px";
            title.appendChild(titleText);

            var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                hl.biz.person.pubMessage();
            });
            title.appendChild(btnBack);

            var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                hl.biz.person.checkNo();
                hl.biz.person.edit();
            });
            title.appendChild(btnEdit);
            hl.diag.pop.add(title);
            hl.biz.person.checkAll();
        });
        title.appendChild(btnEdit);

        hl.diag.pop.add(title);

    },
    /**
     * 多选打开
     */
    checkOn : function(p) {

        // TODO 判断是哪个列表确定list
        var list = hl.data.pub.get();
        for(var i = 0; i < list.length; i++) {
            if(list[i].s == 2) {
                continue;
            }
            var item = list[i].item;

            $("#" + item.SID + "divMWPub").animate({
                'left' : '60px'
            }, 200);

            hl.sys.$(item.SID + "checkPub").style.display = "";
        }
    },
    /**
     * 多选关闭
     */
    checkOff : function(p) {

        // TODO 判断是哪个列表确定list
        var list = hl.data.pub.get();
        for(var i = 0; i < list.length; i++) {
            if(list[i].s == 2) {
                continue;
            }
            var item = list[i].item;

            $("#" + item.SID + "divMWPub").animate({
                'left' : '0px'
            }, 200);
            hl.sys.$(item.SID + "checkPub").style.display = "none";
        }
    },
    /**
     * 全选
     */
    checkAll : function() {
        var list = hl.data.pub.get();
        for(var i = 0; i < list.length; i++) {
            if(list[i].s == 2) {
                continue;
            }
            var item = list[i].item;
            // icon状态的改变
            hl.sys.$(item.SID + "checkPub").status = "on";
            hl.sys.$(item.SID + "checkPub").style.backgroundPosition = "0px 0px";
        }
    },
    /**
     * 全不选
     */
    checkNo : function() {
        var list = hl.data.pub.get();
        for(var i = 0; i < list.length; i++) {
            if(list[i].s == 2) {
                continue;
            }
            var item = list[i].item;
            // icon状态的改变
            hl.sys.$(item.SID + "checkPub").status = "off";
            hl.sys.$(item.SID + "checkPub").style.backgroundPosition = "0px -46px";
        }
    },
    messageMark : function(p) {
        console.log(p);
        var list = hl.data.pub.get();
        if(list != null && list.length > 0) {
            for(var i = 0; i < list.length; i++) {
                if(list[i].item.SID == p.item.SID) {
                    list[i].s = 1;
                    break;
                }
            }
        }
        hl.data.pub.update({
            list : list
        });
        hl.biz.person.pubMessage();
    },
    /**
     * 公司公告
     */
    pub : function(ps) {

    }
}