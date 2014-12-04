/**
 * 入口文件
 */
var hlSm;
var hlIsFirst = false;
function hl$(id) {
    return document.getElementById(id);
}

/**
 * 屏幕尺寸
 */
function hlSize() {
    return {
        x : window['innerWidth'] || document.documentElement.clientWidth,
        y : window['innerHeight'] || document.documentElement.clientHeight
    };
}

function hlProgress(currname, curr, full) {
    var txt = currname + " " + curr + "%";
    hl$("loadPT").innerHTML = txt;
    hl$("loadPPP").style.width = ((196 * curr / 100) | 0) + "px";
}

function hlFirst() {
    hlIsFirst = true;
    hlscreen();
    viewport(true);
    hlPosLoad();
}

/**
 * 游戏入口
 */
function hlLoad() {
    //
    setTimeout(hlFirst, 3000);
    hlPosLoad();
    hlProgress("[游戏逻辑]", 1, 0);
    // 加载业务
    hlFuncs();
}

function mainTest() {
    var size = hlSize();
    var hlMain = hl$("main");
    hlMain.style.width = size.x + "px";
    hlMain.style.height = size.y + "px";

    var bg = ssdjs.dom.img(0, 0, size.x, size.y, sail.url.img("css/images/login_backgroup.jpg"));
    hlMain.appendChild(bg);
}

/**
 * 重新加载
 */
function hlRefresh() {
    location.reload();
}

/**
 * 加载失败
 */
function hlError(type) {
    var txt = "游戏加载失败,请<a href='javascript:hlRefresh();' class='sMid cRed bBlack'>刷新重试</a>。";
    hl$("loadPE").innerHTML = txt;
}

/**
 * 加载JS文件
 */
function hlFuncs() {
    console.log("Loading JS");
    // 需要加载的业务文件
    var files = [];
    if(hlRelease) {
        files = ["ssdjs.js", "socket.io.js", "iscroll.js", "jquery.js", "jquery.easing.js", "jquery.transit.min.js", "parcycle.js", "json2.js", "hl.biz.js", "hl.util.js", "hl.data.js"];
    } else {
        return hlAssets();
    }
    requirejs.onError = function(err) {
        // 加载失败，提醒用户刷新
        hlError(1);
    };
    require.config({
        waitSeconds : 30
    });
    function hlFuncLoad(idx) {
        require([hl.cfg.urlJs + files[idx] + "?v=" + hl.cfg.version], function($) {
            var len = files.length;
            if(idx >= len - 1) {
                hlAssets();
            } else {
                var percent = ((idx + 1) * 100 / len) | 0;
                hlProgress("[游戏逻辑]", percent, 0);
                hlFuncLoad(idx + 1);
            }
        });
    }

    hlFuncLoad(0);
}

/**
 * 加载资源文件
 */
function hlAssets() {
    function assetAdd(url) {
        ssdjs.assets.add(hl.cfg.urlImg + url + "?version=" + hl.cfg.version);
    }

    // 加载图片
    for(var i = 1; i <= 25; i++) {
        assetAdd("bg/a1/" + i + ".jpg");
        assetAdd("bg/a2/" + i + ".jpg");
        assetAdd("bg/a3/" + i + ".jpg");
        assetAdd("bg/a4/" + i + ".jpg");
    }

    // 加载图片
    assetAdd("bg/btn.png");
    assetAdd("bg/menu.png");
    assetAdd("icon30.png");
    assetAdd("icon40.png");
    assetAdd("icon100.png");
    assetAdd("nation_bg.png");
    assetAdd("nation_big.png");
    assetAdd("npc/npc.png");
    // pop
    assetAdd("pop/bg_1.png");
    assetAdd("pop/bg_2.png");
    assetAdd("pop/bg_b.png");
    assetAdd("pop/bg_lt.png");
    assetAdd("pop/bg_paper.png");
    assetAdd("pop/back.png");
    assetAdd("pop/close.png");
    assetAdd("pop/line.png");
    assetAdd("pop/opt_arrow.png");
    assetAdd("pop/opt_bot.png");
    assetAdd("pop/opt_mid.png");
    assetAdd("pop/opt_top.png");
    assetAdd("pop/title.png");
    assetAdd("pop/wood_bot.png");
    assetAdd("pop/wood_top.png");
    // info
    assetAdd("info/user.png");
    // fight
    assetAdd("fight/bg.jpg");
    assetAdd("form.png");
    assetAdd("fight/roundbg.png");
    assetAdd("fight/vs_bg.png");
    assetAdd("fight/vs_top.png");
    assetAdd("fight/explode.png");
    assetAdd("fight/defeat.png");
    assetAdd("fight/fireboom.png");
    assetAdd("fight/boom.png");
    assetAdd("fight/click.png");
    assetAdd("fight/fireboom.png");
    assetAdd("fight/effect/tiao1.png");
    assetAdd("fight/effect/tiao2.png");
    assetAdd("fight/effect/tiao3.png");
    assetAdd("fight/effect/tiao4.png");
    assetAdd("fight/effect/tiao5.png");
    assetAdd("fight/effect/tiao6.png");
    assetAdd("fight/effect/tiao7.png");
    assetAdd("fight/effect/tiao8.png");
    assetAdd("bg/progress/pb2.png");
    assetAdd("form.png");
    assetAdd("bg/progress/ps1.png");
    assetAdd("war/btn.png");
    // 升级
    // assetAdd("bg/levelUp/star.png");
    // assetAdd("bg/levelUp/levelUp.png");
    assetAdd("bg/levelUp/LvUpBg.jpg");
    assetAdd("bg/levelUp/LvUpline.png");

    // 开启宝箱
    assetAdd("treasure/words.png");
    assetAdd("treasure/starLv1.png");
    assetAdd("treasure/starLv2.png");
    assetAdd("treasure/starLv3.png");
    assetAdd("treasure/starLv4.png");
    assetAdd("treasure/starLv5.png");
    // 强化
    assetAdd("npc/commanderUp.jpg");
    assetAdd("npc/upBg.png");
    assetAdd("npc/mark.png");
    assetAdd("npc/storageUp.jpg");
    // 冒险
    assetAdd("fight/explore_bg.jpg");
    assetAdd("fight/explore_path.png");
    assetAdd("adventure/progress_bg.png");
    assetAdd("adventure/task_bg.png");
    assetAdd("adventure/title_bg.png");

    // 发现新城市
    assetAdd("bg/pub/findCityBook.png");
    assetAdd("bg/pub/findCityBookMid.png");
    assetAdd("bg/pub/xiushi.png");
    assetAdd("bg/pub/xiushi1.png");
    assetAdd("hl/c/1.jpg");
    assetAdd("hl/c/2.jpg");
    assetAdd("hl/c/3.jpg");
    assetAdd("hl/c/4.jpg");

    // 加载成功处理
    function assetLoaded(src, percent) {
        hlProgress("[资源文件]", percent, 0);
    }

    function assetError(src) {
        // 加载失败，提醒用户刷新
        hlError(2);
    }

    function assetsLoaded() {
        // 资源加载完成，开始获取数据
        if(hlIsFirst) {
            hlLogin();
        } else {
            setTimeout(hlLogin, 3000);
        }

    }

    // 开始加载资源
    if(ssdjs.assets.length() > 0) {
        ssdjs.assets.loadAll({
            onload : assetLoaded,
            onerror : assetError,
            onfinish : assetsLoaded
        })
    } else {
        assetsLoaded();
    }

}

/**
 * 登陆游戏
 */
function hlLogin() {
    // 音乐
    ssdjs.music.base = hl.cfg.urlSound;
    // 城区
    hlSm = new iScroll('town', {
        bounce : false,
        momentum : false,
        hScrollbar : false,
        vScrollbar : false,
        lockDirection : false,
        onScrollEnd : function() {
            if("sea" == hl.data.user.scene) {
                // hl.biz.port.steer.hide();
            }
        },
        onBeforeScrollStart : function() {
            // ssdjs.dom.CLICK = true;
            if("sea" == hl.data.user.scene) {
            }
        },
        onBeforeScrollMove : function() {
            ssdjs.dom.CLICK = false;
        },
        onBeforeScrollEnd : function() {
            setTimeout(function() {
                ssdjs.dom.CLICK = true;
            }, 100);
        }
    });
    hlVersion();
}

function hlVersion() {
    var url = hl.cfg.urlPortal + "service.json";
    function success(result) {
        var version = result.version;
        console.log(version);
        uexLog.sendLog("version : " + version);
        if(hl.cfg.version != version) {
            console.log("有版本更新");
            hl.biz.login.updating();
            window.ssdObj.snsUpdate(result.appurl + "hai_" + hl.cfg.version + ".zip");
            return;
        }
        var server = result.servers[0];
        hl.cfg.urlApi = server.url;
        // 登陆游戏
        if(ssdjs.client.isClient()) {
            hl.biz.login.loginPP();
            ssdjs.client.snsLogin();
            return;
        }
        hl.biz.login.auth();
    }

    function error(result) {
        alert("联网失败了" + JSON.stringify(result));
        hlVersion();
    }


    ssdjs.req.req(url, success, error, null, true, 5000);
}

function fullscreen() {
    // 隐藏状态栏
    window.scrollTo(0, 1);
    // resize
    setTimeout(resize, 1000);
}

function vpWidth() {
    var w = 640;
    if((/ipad/gi).test(navigator.appVersion)) {
        // w = 1024;
    }
    if(!ssdjs.browser.isTouch()) {
        w = 1024;
    }

    var size = ssdjs.browser.size();
    w = size.x;
    if(size.x < 640) {
        w = 640;
    }
    if(size.x > 1136) {
        // w = 1136
    }
    return w;
}

function viewport(revp) {
    var screen = hlscreen();
    // alert("screen.x=" + screen.x);
    // alert("screen.y=" + screen.y);
    var sz = hlSize();
    var isresize = revp || false;
    if(hlland == null || hlland != (sz.x > sz.y)) {
        hlland = (sz.x > sz.y);

        if(((sz.x > sz.y) && (screen.x < screen.y)) || ((sz.x < sz.y) && (screen.x > screen.y))) {
            var tmp = screen.x;
            screen.x = screen.y;
            screen.y = tmp;
        }

        var size = screen;
        var _w = 640;
        var _nw = 640;
        if((/ipad/gi).test(navigator.appVersion)) {
            _w = 768;
            _nw = 768;
        }
        if(size.x > size.y) {
            var tmp = (_nw * size.y / size.x) | 0;
            if(tmp > _nw) {
                _w = tmp;
            }
            if((/ipad/gi).test(navigator.appVersion)) {
                _w = 1024;
                _nw = 900;
            }
        }

        var scale = size.x < size.y ? (size.x / _nw) : (size.y / _nw);
        var ua = navigator.userAgent;
        if(ua.match(/iPhone/) || ua.match(/iPad/)) {
            scale = size.sw / _nw;
        }
        var vp = "width=" + _w + ",target-densitydpi=high-dpi" + ",initial-scale=" + scale + ",maximum-scale=" + scale + ",minimum-scale=" + scale;
        // $("#viewport").attr("content", vp);
        hl$("viewport").content = vp;

        // if(hlland) {
        // sz = hlSize();
        // var rotatemask = hl$("rotatemask");
        // rotatemask.style.height = sz.y + "px";
        // rotatemask.style.width = sz.x + "px";
        // rotatemask.style.display = "";
        //
        // var rotate = hl$("rotate");
        // rotate.style.display = "";
        // var innerHTML = rotate.innerHTML;
        // var x = (sz.x - 442) / 2;
        // var y = (sz.y - 226) / 2;
        // var img = '<div style="position:absolute;left:' + x + 'px;top:' + y + 'px;z-index:3999;"><img src="app/img/rotate.jpg" id="rotateImg"/></div>';
        // rotate.innerHTML = img;
        // } else {
        // var rotatemask = hl$("rotatemask");
        // rotatemask.style.display = "none";
        // var rotate = hl$("rotate");
        // rotate.style.display = "none";
        // }
    }
}

function resize() {
    viewport();

    // 隐藏状态栏
    // window.scrollTo(0, 1);
    // setTimeout(redraw, 1000);

    redraw();
}

function redraw() {

    // 调整窗口
    var size = hlSize();
    // 游戏宽度
    var hlH = size.y;
    console.log("winH" + hlH);
    var hlW = size.x;
    var max = vpWidth();
    if(hlW >= max) {
        hlW = max;
    }
    // 弹出框屏幕居中
    var hlL = 0;
    // if(size.x > 640) {
    // popL = ((size.x - 640) / 2) | 0;
    // }
    var pop = $("#pop");
    pop.css("left", "0px");
    pop.css("height", (hlH - 98) + "px");
    var chat = $("#chat");
    chat.css("left", "0px");
    chat.css("height", (hlH - 98) + "px");
    var up = $("#up");
    up.css("left", hlL + "px");
    up.css("height", hlH + "px");
    up.css("opacity", 0);
    // info.menu
    var iMenu = $("#iMenu");
    iMenu.css("left", "0px");
    iMenu.css("width", "100%");
    iMenu.css("top", (hlH - 98) + "px");
    iMenu.css("height", "98px");
    // body
    $("body").css("height", (size.y + 0) + "px");
    // main
    var _w = size.x;
    var max = vpWidth();
    if(_w >= max) {
        _w = max;
    }
    var main = $("#main");
    main.css("height", size.y + "px");
    main.css("width", _w + "px");
    if(size.x > _w) {
        main.css("left", ((size.x - _w) / 2) | 0 + "px");
    } else {
        main.css("left", "0px");
    }

}

function hlPosLoad() {
    var size = hlSize();
    // loading
    var pLoad = hl$("pLoad");
    pLoad.style.position = "absolute";
    pLoad.style.left = (((size.x - 136) / 2) | 0) + "px";
    pLoad.style.top = ((((size.y - 127) / 2) - 83) | 0) + "px";
    // tips
    var tips = hl$("tips");
    tips.style.left = "0px";
    tips.style.width = size.x + "px";
    tips.style.position = "absolute";
    tips.style.top = (size.y / 2 | 0) + "px";
    // progress
    var progress = hl$("loadProgress");
    progress.style.position = "absolute";
    progress.style.color = "white";
    progress.style.textAlign = "center";
    progress.style.top = ((size.y / 2 | 0) + 70) + "px";
    // progress img
    var pprog = hl$("loadPP");
    pprog.style.left = (((size.x - 200) / 2) | 0) + "px";
    pprog.style.position = "absolute";
    var pimg = hl$("loadPPP");
    pimg.style.position = "absolute";

    // progress text
    var ptext = hl$("loadPT");
    ptext.className = "sSmall cYbtn bBlack alignC";
    ptext.style.position = "absolute";
    ptext.style.marginTop = "1px";

    // progress error
    var perr = hl$("loadPE");
    perr.className = "sSmall cYbtn bBlack alignC";
    perr.style.position = "absolute";
    perr.style.top = "40px";

    var bg = hl$("loadingBg");
    bg.style.width = size.x + "px";
    bg.style.height = size.y + "px";
    bg.style.backgroundColor = "black";
    //
    var hlground = hl$("hlground");
    hlground.style.width = size.x + "px";
    hlground.style.height = (size.y + 200) + "px";
}