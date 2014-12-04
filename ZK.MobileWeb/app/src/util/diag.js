/**
 * 对话框
 */
var hl = (hl || {});
hl.diag = {
    menu : {
        tick : 0,
        reKey : -1,

        show : function(p) {
            var bottomMenu = $("#iMenu");
            var size = hl.sys.winSize();

            bottomMenu.empty();
            var bottomMenuBg = ssdjs.dom.img(0, 0, "100%", 98, hl.url.img("mainMenu_bg.jpg"));
            bottomMenu.append(bottomMenuBg);
            var _x = 0;

            var i0 = 0;
            var i1 = 0;
            var i2 = 0;
            var i3 = 0;

            switch(p.on) {
                case 0 :
                    i0 = 1;
                    break;
                case 1 :
                    i1 = 1;
                    break;
                case 2 :
                    i2 = 1;
                    break;
                case 3 :
                    i3 = 1;
                    break;
            }
            var _ww = size.x / 4;

            var btn0 = hl.tpl.btnBottom("person", _x, 0, _ww, 75, "menu/person.png", "个人中心", i0, function(obj) {
                hl.biz.person.init();

            });
            bottomMenu.append(btn0);
            _x += _ww;
            var btn1 = hl.tpl.btnBottom("user", _x, 0, _ww, 75, "menu/user.png", "用户列表", i1, function(obj) {
                hl.biz.user.req();

            });
            bottomMenu.append(btn1);
            _x += _ww;
            var btn2 = hl.tpl.btnBottom("disk", _x, 0, _ww, 75, "menu/disk.png", "我的网盘", i2, function(obj) {
                hl.biz.disk.index();

            });
            bottomMenu.append(btn2);
            _x += _ww;
            var btn3 = hl.tpl.btnBottom("more", _x, 0, _ww, 75, "menu/more.png", "更多", i3, function(obj) {
                hl.biz.more.init();

            });
            bottomMenu.append(btn3);
            bottomMenu.show();
            hl.diag.menu.refresh();
        },
        hide : function() {
            var bottomMenu = $("#iMenu");
            bottomMenu.hide();
            hl.diag.menu.stopRefresh(hl.diag.menu.reKey);
        },
        refresh : function(p) {
            var time = setInterval(function() {
                // 聊天信息
                var numNoSee = 0;
                var pppList = (hl.data.chat.receive.list || []);
                if(pppList.length > 0) {
                    for(var i = 0; i < pppList.length; i++) {
                        if(pppList[i].list == null) {
                            continue;
                        }
                        numNoSee += pppList[i].list.length;
                    }
                }
                if(numNoSee != 0) {
                    $("#usermarkPNum").html(numNoSee);
                    $("#usermarkPub").show();
                }
            }, 1000);
            hl.diag.menu.reKey = time;
            return time;
        },
        stopRefresh : function(p) {
            clearInterval(p);
        }
    },
    alert : function(p) {

        var infMain = hl.sys.hl$("info");

        var size = hl.sys.winSize();

        var divMask = ssdjs.dom.div(0, 0, size.x, size.y);
        divMask.id = "hlMask";
        divMask.style.backgroundColor = "black";
        divMask.css("opacity", 0.53);
        infMain.appendChild(divMask);

        var w = 554;
        var bw = 462;

        if(w >= size.x) {
            w = size.x - 20;
            bw = size.x - 60
        }
        var h = 280;

        var divW = ssdjs.dom.div((size.x - w) / 2, (size.y - h) / 2, w, h);
        divW.id = "hlAlert";

        var bg = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("login/alertBg.png"));
        //divW.style.backgroundImage = hl.url.cssimg("login/alertBg.png");
        divW.appendChild(bg);

        var title = ssdjs.dom.text(0, 30, w, null, "安全提示");
        title.style.textAlign = "center";
        title.style.color = "#2c2c2c";
        title.style.fontSize = "30px";
        divW.appendChild(title);

        var message = ssdjs.dom.text(54, 90, w - 108, null, p.m);
        message.style.textAlign = "left";
        message.style.color = "#2c2c2c";
        message.style.fontSize = "24px";
        divW.appendChild(message);

        var btnOk = ssdjs.dom.div((w - bw) / 2, 177, bw, 59);

        var bg0 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("login/alertOk0.png"));
        var bg1 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("login/alertOk1.png"));
        bg1.style.display = "none";
        btnOk.appendChild(bg0);
        btnOk.appendChild(bg1);
        //  btnOk.style.backgroundImage = hl.url.cssimg("login/alertOk0.png");
        btnOk.evtEnd(function(obj) {
            bg1.style.display = "none";
            //  obj.style.backgroundImage = hl.url.cssimg("login/alertOk0.png");
            $("#info").empty();
        });
        btnOk.evtMove(function(obj) {
            bg1.style.display = "none";
            //  obj.style.backgroundImage = hl.url.cssimg("login/alertOk0.png");
        });
        btnOk.evtStart(function(obj) {
            bg1.style.display = "";
            //  obj.style.backgroundImage = hl.url.cssimg("login/alertOk1.png");
        });
        var text = ssdjs.dom.text(0, 12, bw, null, "确定");
        text.style.color = "#2c2c2c";
        text.style.fontSize = "30px";
        text.style.textAlign = "center";
        btnOk.appendChild(text);
        divW.appendChild(btnOk);
        infMain.appendChild(divW);

    },
    up2 : function(p) {
        console.log("up");
        console.log(p);
        var size = hl.sys.winSize();

        var w = 570;
        var h = 400;

        var divW = ssdjs.dom.div((size.x - w) / 2, (size.y - h) / 2, w, h);
        divW.id = "upMain";
        divW.style.backgroundColor = "#ffe";
        divW.style.borderRadius = "6px";

        var title = ssdjs.dom.text(10, 40, null, null, p.item.TITLE);
        divW.appendChild(title);

        var message = ssdjs.dom.text(20, 80, null, null, p.item.CONTENT);
        divW.appendChild(message);

        var btnW = 60;
        var btnH = 40;
        var btnClose = ssdjs.dom.div(w - 80, h - 60, btnW, btnH);
        var btnCloseText = ssdjs.dom.text(5, 5, null, null, "关闭");
        btnClose.appendChild(btnCloseText);
        btnClose.style.backgroundColor = "#eee";

        btnClose.evtEnd(function(obj) {
            $("#upMain").remove();
            btnClose.style.backgroundColor = "#eee";
        });

        btnClose.evtMove(function(obj) {
            btnClose.style.backgroundColor = "#eee";
        });
        btnClose.evtStart(function(obj) {
            btnClose.style.backgroundColor = "#222";
        });
        divW.appendChild(btnClose);
        var infMain = hl.sys.hl$("up");
        $("#up").empty();

        infMain.appendChild(divW);

        hl.sys.hl$("up").style.display = "";
    },
    pop : {
        show : function(p) {
            var p = (p || {});
            p.location = (p.location || {});
            var _w = 640;
            var _h = 960;
            var ws = ssdjs.browser.size();
            var _h = (p.location.h || 450);
            var _top = ((ws.y - _h) / 2) | 0;
            var _left = ((ws.x - _w) / 2) | 0;
            $("#pop").css("height", _h + "px");
            $("#pop").css("left", _left + "px");
            $("#pop").css("top", _top + "px");
            $("#pop").css("display", "");
            $("#pop").css("scale", "0.5");

        },
        hide : function(p) {
            $("#pop").hide();
            $("#pop").empty();
        },
        clear : function(p) {
            $("#pop").empty();
        },
        add : function(p) {
            $("#pop").append(p);
        }
    },
    up : {
        show : function(p) {
            var p = (p || {});
            // p.location = (p.location || {});
            // var _w = 640;
            // var _h = 960;
            // var ws = ssdjs.browser.size();
            // _w = ws.x;
            // _h = ws.y;
            // var _top = 0;
            // var _left = 1200;
            // $("#up").css("height", _h + "px");
            // $("#up").css("left", _left + "px");
            // $("#up").css("top", _top + "px");
            // $("#up").css("width", _w + "px");

            setTimeout(function() {            
                $("#up").toggleClass("animate fadeInLeft", true);
                $("#up").css("display", "");
            }, 100);
        },
        hide : function(p) {

            $("#up").toggleClass("animate fadeOutRight", true);
            // $("#up").hide();
            // $("#up").empty();

        },
        close : function(p) {

            setTimeout(function() {
                $("#up").toggleClass("animate fadeOutRight", true);
                // $("#up").hide();
                // $("#up").empty();
            }, 100);
        },
        clear : function(p) {
            $("#up").toggleClass("animate fadeOutRight", false);
            $("#up").toggleClass("animate fadeInLeft", false);
            $("#up").empty();
        },
        add : function(p) {
            $("#up").append(p);
        }
    },
    loading : {
        hide : function(param) {
            setTimeout(function() {
                $("#iLoading").hide();

                ssdjs.dom.CLICK = true;
            }, 200);
            if(param != null) {
                clearTimeout(param);
            } else {
                clearTimeout(hl.data.user.loadStat);
            }
        },
        show : function(param) {
            console.log("loading");
            // 更改tips
            // var ran = parseInt(Math.random() * 15 + 1);
            // var tips = sail.data.cfg.tips[ran];
            // $("#tips").html(tips);

            var t = setTimeout(function() {
                if(!$("#iLoading").is(":hidden")) {
                    hl.diag.alert({
                        m : "你的网络不太给力哦"
                    });
                    hl.diag.loading.hide();
                }
            }, 20000);

            ssdjs.dom.CLICK = false;

            $("#loadingBg").empty();
            var mask = hl.tpl.diag.loadingMask("loadingMask");
            $("#loadingBg").append(mask);

            var size = hl.sys.winSize();

            var cvs = ssdjs.dom.canvas(0, 200, size.x, 400);
            $("#loadingBg").append(cvs);
            var loadingObj = new loading(cvs, {
                radius : 30,
                circleLineWidth : 13
            });
            loadingObj.show();

            $("#iLoading").show();

            hl.data.user.loadStat = t;
            return t;
        }
    },
    loginLoading : {
        hide : function(param) {
            setTimeout(function() {
                $("#iLoading").hide();

                ssdjs.dom.CLICK = true;
            }, 200);
            if(param != null) {
                clearInterval($("#iLoading").setInterval);
                clearTimeout(param);
            } else {
                clearInterval($("#iLoading").setInterval);
                clearTimeout(hl.data.user.loadStat);
            }
        },
        show : function(param) {
            console.log("loading");
            // 更改tips
            // var ran = parseInt(Math.random() * 15 + 1);
            // var tips = sail.data.cfg.tips[ran];
            // $("#tips").html(tips);

            ssdjs.dom.CLICK = false;

            $("#loadingBg").empty();

            var size = hl.sys.winSize();
            var x = size.x;
            var y = size.y;
            var w = 245;
            var h = 177;
            var divW = ssdjs.dom.div(0, 0, size.x, size.y);
            divW.id = "loginLoad";

            var divMask = ssdjs.dom.div(0, 0, size.x, size.y);
            divMask.style.backgroundColor = "black";
            divMask.css("opacity", 0.53);
            divW.appendChild(divMask);

            var divLoading = ssdjs.dom.div((x - w) / 2, (y - h) / 2, w, h);
            divLoading.style.backgroundImage = hl.url.cssimg("login/loadingBg.png");
            divW.appendChild(divLoading);

            var cvs = ssdjs.dom.canvas(0, -15, w, h);
            $("#loadingBg").append(cvs);
            var loadingObj = new loginLoading(cvs, {
                radius : 13,
                circleLineWidth : 5
            });
            divLoading.appendChild(cvs);
            loadingObj.show();

            $("#iLoading").setInterval = loadingObj;

            var text = ssdjs.dom.text(0, 115, w, null, "等待登陆，请稍后。。。");
            text.style.color = "#ffffff";
            text.style.fontSize = "20px";
            text.style.textAlign = "center";
            divLoading.appendChild(text);
            $("#loadingBg").append(divW);

            $("#iLoading").show();

            var t = setTimeout(function() {
                if(!$("#iLoading").is(":hidden")) {
                    loadingObj.hide();
                    hl.diag.alert({
                        m : "你的网络不太给力哦"
                    });
                    hl.diag.loading.hide();
                }
            }, 20000);

            hl.data.user.loadStat = t;
            return t;
        }
    },
    opt : {
        hide : function() {
            hl.CLOSE_OPT = 0;
            $("#opt").css("display", "none");
        },
        show : function(dom) {
            hl.CLOSE_OPT = 1;
            $("#opt").html(dom);
            $("#opt").css("display", "");
        }
    },
    getAbsoluteLeft : function(object) {
        //获取控件左绝对位置

        o = object
        oLeft = o.offsetLeft
        while(o.offsetParent != null) {
            oParent = o.offsetParent
            oLeft += oParent.offsetLeft
            o = oParent
        }
        return oLeft
    }
}