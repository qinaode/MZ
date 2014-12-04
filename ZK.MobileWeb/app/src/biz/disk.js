/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.disk = {
    index : function() {
        hl.diag.menu.show({
            on : 2
        });
        hl.tpl.diag.pop({
            id : "disk",
            title : "我的网盘",
            l : 0,
            r : 0,
        });
        //hl.sys.hl$("up").style.display = "none";
        var main = $("#diskMain");

        var _h = $("#diskMain").height();
        var _w = $("#diskMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollD = hl.tpl.scroll.v.create("hscrollD", 0, 0, _w, _h);
        main.append(hscrollD);
        console.log(" main.append(hscrollD)");
        // 滑动区域scroll定义
        scrollD = hl.sys.scroll({
            id : "hscrollD",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        var _hh = _h;

        // 九宫格
        var divW = ssdjs.dom.div(null, null, _w, _hh);

        var lineH = ssdjs.dom.div(_w / 2, 50, 1, _hh - 80);
        lineH.style.backgroundColor = "#a2a2a2"
        divW.appendChild(lineH);
        var lineV1 = ssdjs.dom.div(50, _hh / 3, _w - 100, 2);
        lineV1.style.backgroundColor = "#a2a2a2"
        divW.appendChild(lineV1);
        var lineV2 = ssdjs.dom.div(50, _hh * 2 / 3, _w - 100, 2);
        lineV2.style.backgroundColor = "#a2a2a2"
        divW.appendChild(lineV2);

        var left = (_w / 2 - 130) / 2;
        var top = 60;
        var w = _w / 2;

        var _hhh = _hh / 3;

        var allDiv = hl.biz.disk.indexItem({
            location : {
                x : 0,
                y : 0,
                w : w,
                h : _hhh,
            },
            item : {
                ico : "disk/all.png",
                txt : "全部",
                type : 0
            }
        });
        divW.appendChild(allDiv);
        var imgDiv = hl.biz.disk.indexItem({
            location : {
                x : w,
                y : 0,
                w : w,
                h : _hhh,
            },
            item : {
                ico : "disk/picture.png",
                txt : "图片",
                type : 2
            }
        });
        divW.appendChild(imgDiv);

        var officeDiv = hl.biz.disk.indexItem({
            location : {
                x : 0,
                y : _hhh,
                w : w,
                h : _hhh,
            },
            item : {
                ico : "disk/office.png",
                txt : "文档",
                type : 1
            }
        });
        divW.appendChild(officeDiv);

        var musicDiv = hl.biz.disk.indexItem({
            location : {
                x : w,
                y : _hhh,
                w : w,
                h : _hhh,
            },
            item : {
                ico : "disk/music.png",
                txt : "音频",
                type : 4
            }
        });
        divW.appendChild(musicDiv);

        var voideDiv = hl.biz.disk.indexItem({
            location : {
                x : 0,
                y : _hhh * 2,
                w : w,
                h : _hhh,
            },
            item : {
                ico : "disk/voide.png",
                txt : "视频",
                type : 3
            }
        });
        divW.appendChild(voideDiv);

        var otherDiv = hl.biz.disk.indexItem({
            location : {
                x : w,
                y : _hhh * 2,
                w : w,
                h : _hhh,
            },
            item : {
                ico : "disk/other.png",
                txt : "其他",
                type : 5
            }
        });
        divW.appendChild(otherDiv);

        hl.tpl.scroll.v.append("hscrollD", divW);
        scrollD.refresh();

    },
    imgList : function(p) {
        hl.diag.menu.hide();
        var titleMain = "我的网盘";
        hl.tpl.diag.popStyle02({
            id : "dl", // diskList
            title : "图片",
            l : hl.biz.disk.index,
        });
        hl.sys.hl$("up").style.display = "none";
        var main = $("#dlMain");

        var _h = $("#dlMain").height();
        var _w = $("#dlMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollD = hl.tpl.scroll.v.create("hscrollDL", 0, 0, _w, _h);
        main.append(hscrollD);

        // 滑动区域scroll定义
        scrollD = hl.sys.scroll({
            id : "hscrollDL",
            snap : false,
            vScrollbar : true,
            hScroll : false,
            momentum : true,
            hideScrollbar : true
        });
        var list = hl.biz.disk.subDataTest;
        if(list != null && list.length > 0) {

            for(var i = 0; i < list.length; i += 4) {
                var temList = [list[i], list[i + 1], list[i + 2], list[i + 3]];
                if((i + 3) > list.length) {
                    temList = [list[i], list[i + 1], list[i + 2]];
                }
                if((i + 2) > list.length) {
                    temList = [list[i], list[i + 1]];
                }
                if((i + 1) > list.length) {
                    temList = [list[i]];
                }

                hl.tpl.scroll.v.append("hscrollDL", hl.biz.disk.imgListItem({
                    list : temList
                }));

                console.log("aaaaa" + i);
            }
            console.log("dddd" + i);
        }
        scrollD.refresh();

        var size = hl.sys.winSize();
        var bottom = ssdjs.dom.div(0, size.y - 91 - 97, size.x - 4, 98);
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";

        var _x = 0;

        var btnDel = hl.tpl.btnBottom("share", _x, 0, size.x / 2, 75, "menu/share.png", "共享", 0, function(obj) {
            hl.biz.disk.checkOn({
                type : 2
            });
            // 底部菜单
            var size = hl.sys.winSize();
            var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
            // TODO 背景要修改成图片的
            bottom.style.backgroundColor = "#202020";
            bottom.style.border = "2px outset #474747";
            bottom.id = "bottomEdit";

            var _x = 0;
            var btnchoseFirend = hl.tpl.btnMenu("choseFirend", _x, 0, size.x, 75, "", "选择好友共享", 0, function(obj) {
                hl.biz.disk.choseFirend();
            });
            bottom.appendChild(btnchoseFirend);
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

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";
          
            titleText.style.fontSize = "40px";
            title.appendChild(titleText);

            var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                hl.biz.disk.checkOff();
                hl.biz.disk.imgList();

            });
            title.appendChild(btnBack);

            var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

                // 顶部操作栏的修改
                $("#title").remove();
                var title = ssdjs.dom.div(0, 0, size.x, 94);
                title.id = "title";
                var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
                title.appendChild(titleBg);

                var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
                titleText.style.color = "#FFF";
                titleText.style.textAlign = "center";
              
                titleText.style.fontSize = "40px";
                title.appendChild(titleText);

                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {

                    hl.biz.disk.checkOff();
                    hl.biz.disk.imgList();

                });
                title.appendChild(btnBack);

                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                    hl.biz.disk.checkNo();
                    hl.biz.disk.chose({
                        type : p.type
                    });
                });
                title.appendChild(btnEdit);
                hl.diag.pop.add(title);
                hl.biz.disk.checkAll();
            });
            title.appendChild(btnEdit);

            hl.diag.pop.add(title);

        });
        bottom.appendChild(btnDel);
        _x += size.x / 2;
        var btnUp = hl.tpl.btnBottom("select", _x, 0, size.x / 2, 75, "menu/select.png", "删除", 0, function(obj) {
            hl.biz.disk.chose({
                type : 2
            });
            // TODO menu --> 删除

        });
        bottom.appendChild(btnUp);

        main.append(bottom);

    },
    imgListItem : function(p) {
        var list = p.list;
        var size = hl.sys.winSize();
        var w = size.x;
        var h = size.x / 4;
        var divW = ssdjs.dom.div(null, null, w, h);
        var _x = 0;
        for(var i = 0; i < list.length; i++) {
            divW.appendChild(hl.biz.disk.imgItem({
                _x : _x,
                item : list[i]
            }));
            _x += size.x / 4;
        }
        return divW;
    },
    imgItem : function(p) {
        if(p.item == null) {
            var divW = ssdjs.dom.div(null, null, null, null);
            return divW;
        }
        var size = hl.sys.winSize();
        var w = size.x / 4;
        var h = size.x / 4;
        var item = p.item;
        var divW = ssdjs.dom.div(p._x, 0, w, h);

        var img = ssdjs.dom.img(2, 2, w - 4, h - 4, hl.url.api(item.path));
        divW.appendChild(img);
        var check = hl.tpl.btn.check(item.id + "checkD", w - 47, h - 47, "off", null);
        check.style.display = "none";
        divW.appendChild(check);

        return divW;
    },
    imgDetail : function(p) {
        console.log(p);
        var titleMain = p.item.n;
        var size = hl.sys.winSize();
        var item = p.item;

        hl.tpl.diag.popStyle03({
            id : "dl",
            title : titleMain,
            l : hl.biz.disk.subReq,
            lP : {
                type : 2
            }
        });

        var main = hl.sys.$("dlMain");

        var _h = $("#dlMain").height();
        var _w = $("#dlMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.appendChild(contentBg);

        var contentImg = ssdjs.dom.img((size.x - 100) / 2, 250, 100, 100, hl.url.img(headImg + ".png"));
        main.appendChild(contentImg);

        var size = hl.sys.winSize();
        var bottom = ssdjs.dom.div(0, size.y - 91 - 97, size.x - 4, 98);
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";

        var _x = 0;

        var btnDel = hl.tpl.btnBottom("share", _x, 0, size.x / 2, 75, "menu/share.png", "共享", 0, function(obj) {
            hl.biz.disk.choseFirend();
        });
        bottom.appendChild(btnDel);
        _x += size.x / 2;
        var btnUp = hl.tpl.btnBottom("select", _x, 0, size.x / 2, 75, "menu/select.png", "删除", 0, function(obj) {
            hl.biz.disk.deleteImg({
                id : p.item.id
            });
        });
        bottom.appendChild(btnUp);

        main.append(bottom);

    },
    /**
     * {
     * location:{x:0,y:0,w:320,h:290},
     * item:{txt:"",ico:"",type:1},
     * fn:
     * }
     */
    indexItem : function(p) {
        var x = p.location.x;
        var y = p.location.y;
        var w = p.location.w;
        var h = p.location.h;
        var item = p.item;
        var divW = ssdjs.dom.div(x, y, w, h);
        divW.evtEnd(function(obj) {
            if(item.type != 0) {
                hl.biz.disk.subReq({
                    type : item.type
                });
            } else if(item.type == 0) {
                hl.biz.diskAll.index();
            }

        });
        var ico = ssdjs.dom.img((w - 130) / 2, (h - 130) / 2 - 10, 130, 130, hl.url.img(item.ico));
        divW.appendChild(ico);
        var text = ssdjs.dom.text(0, (h - 130) / 2 + 140, w, null, item.txt);
        text.style.fontSize = "24px"
        text.style.textAlign = "center";
        divW.appendChild(text);
        return divW;
    },
    share : function(p) {
        p = (p || {});
        console.log("shareP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("NetworkDisk/ShareFolderInfo");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;

        var friendIds = "";

        for(var i = 0; i < hl.data.disk.choseList.list.length; i++) {
            var item = hl.data.disk.choseList.list[i];
            if(item.id != null) {
                friendIds += item.id + ","
            }
        }
        friendIds = friendIds.slice(0, friendIds.length - 1);

        if(friendIds != "") {
            data += "&shareUserIds=" + friendIds;
        }
        if(hl.data.disk.share.fileIds != null) {
            data += "&folderIds=" + hl.data.disk.share.fileIds;
        }
        var success = function(result) {
            console.log(result);
            // TODO 跳转到文件页面
            var pp = hl.data.disk.share.backFunctionP;
            pp.back = 1;
            hl.data.disk.share.backFunction(pp);
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
    /**
     *
     * @param {Object} p{fid:192,fname:"aa.txt"}
     */
    downLoad : function(p) {
        var p = (p || {
            fid : 192,
            fname : "aa.dll"
        });

        var url = hl.url.api("NetworkDisk/DownLoadFile");
        url += "?userID=" + user.id;
        if(p.fid != null) {
            url += "&fileid=" + p.fid;
        }
        var savePath = "wgt://data/HLdown/" + p.fname;
        var inOpCode = 1;

        uexDownloaderMgr.createDownloader(inOpCode);
        var cText = 0;
        var cJson = 1;
        var cInt = 2;
        /**
         * 创建下载对象的回调方法
         * @param {Object} opCode
         * @param {Object} dataType
         * @param {Object} data 0为成功；1为失败
         */
        uexDownloaderMgr.cbCreateDownloader = function(opCode, dataType, data) {
            if(dataType == 2 && data == 0) {
                console.log('创建成功');
                uexLog.sendLog('Log:' + 'OKK');
                uexDownloaderMgr.download(inOpCode, url, savePath, '1');
            } else {
                console.log('创建失败');
                uexLog.sendLog('Log:' + 'NOO');
            }
        }
        uexWidgetOne.cbError = function(opCode, errorCode, errorInfo) {
            console.log(errorInfo);
        }
        /**
         * 通过下载url获取下载对象的信息的回调方法
         * @param {Object} opCode
         * @param {Object} dataType
         * @param {Object} data
         */
        uexDownloaderMgr.cbGetInfo = function(opCode, dataType, data) {
            if(dataType == 1) {
                if(!isDefine(data)) {
                    console.log('无数据');
                    return;
                }
                console.log(data);
                var info = eval('(' + data + ')');

                var asd = '文件路径：' + info.savePath + '<br>文件大小：' + info.fileSize + '<br>已下载：' + info.currentSize + '<br>下载时间：' + info.lastTime;
                var title = ssdjs.dom.text(0, 400, size.x, null, asd);
                title.style.zIndex = "999999";
                hl.sys.$("pop").appendChild(title);

            }
        }
        uexDownloaderMgr.onStatus = function(opCode, fileSize, percent, status) {
            switch (status) {
                case 0:
                    //下载过程中
                    //  $('xjc').innerHTML =
                    var ss = 'size：' + fileSize + '<br>percent：' + percent;
                    hl.diag.alert({
                        m : ss
                    });
                    var asd = '文件路径：' + info.savePath + '<br>文件大小：' + info.fileSize + '<br>已下载：' + info.currentSize + '<br>下载时间：' + info.lastTime;
                    var title = ssdjs.dom.text(0, 400, size.x, null, ss);
                    title.style.zIndex = "999999";
                    hl.sys.$("pop").appendChild(title);
                    uexLog.sendLog('Log:' + ss);
                    break;
                case 1:
                    //下载完成
                    console.log('下载完成');
                    hl.diag.alert({
                        m : "下载完成"
                    });
                    uexLog.sendLog('Log:' + 'ok');
                    uexDownloaderMgr.closeDownloader(opCode);
                    //下载完成要关闭下载对象
                    break;
                case 2:
                    //下载失败
                    console.log('下载失败');
                    uexLog.sendLog('Log:' + 'no');

                    uexDownloaderMgr.closeDownloader(opCode);
                    //下载失败要关闭下载对象
                    break;
            }
        }
    },
    upLoad : function() {

    },
    init : function() {
        hl.diag.menu.show({
            on : 2
        });
        hl.tpl.diag.pop({
            id : "disk",
            title : "我的网盘",
            l : 0,
            r : 0,
        });
        hl.sys.hl$("up").style.display = "none";
        var main = $("#diskMain");

        var _h = $("#diskMain").height();
        var _w = $("#diskMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollD = hl.tpl.scroll.v.create("hscrollD", 0, 0, _w, _h);
        main.append(hscrollD);
        console.log(" main.append(hscrollD)");
        // 滑动区域scroll定义
        scrollD = hl.sys.scroll({
            id : "hscrollD",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });
        var data = hl.biz.disk.dataTest;

        hl.tpl.scroll.v.append("hscrollD", hl.biz.disk.item({
            item : {
                w : _w,
                h : 180,
                head : "office.png",
                t : "文档",
                n : data.doc.Num,
                s : data.doc.Size,
                type : 1
            }
        }));
        hl.tpl.scroll.v.append("hscrollD", hl.biz.disk.item({
            item : {
                w : _w,
                h : 180,
                head : "picture.png",
                t : "图片",
                n : data.pic.Num,
                s : data.pic.Size,
                type : 2
            }
        }));
        hl.tpl.scroll.v.append("hscrollD", hl.biz.disk.item({
            item : {
                w : _w,
                h : 180,
                head : "voide.png",
                t : "视频",
                n : data.video.Num,
                s : data.video.Size,
                type : 3
            }
        }));
        hl.tpl.scroll.v.append("hscrollD", hl.biz.disk.item({
            item : {
                w : _w,
                h : 180,
                head : "music.png",
                t : "音频",
                n : data.audio.Num,
                s : data.audio.Size,
                type : 4
            }
        }));
        hl.tpl.scroll.v.append("hscrollD", hl.biz.disk.item({
            item : {
                w : _w,
                h : 180,
                head : "other.png",
                t : "其他",
                n : data.others.Num,
                s : data.others.Size,
                type : 5
            }
        }));

        scrollD.refresh();

    },
    item : function(p) {
        console.log(p);
        var item = p.item;

        var divW = ssdjs.dom.div(null, null, item.w, item.h);
        divW.evtEnd(function(obj) {
            hl.biz.disk.subReq({
                type : item.type
            });
        });
        var bgLine = ssdjs.dom.img(0, item.h - 2, item.w, 2, hl.url.img("line.jpg"));
        divW.appendChild(bgLine);

        var iconR = ssdjs.dom.img(item.w - 40, 80, 19, 29, hl.url.img("icon3.png"));
        divW.appendChild(iconR);

        var head = ssdjs.dom.img(10, 35, 102, 103, hl.url.img(item.head));
        divW.appendChild(head);

        var title = ssdjs.dom.text(130, 70, null, null, item.t);
        title.style.color = "#000";
        title.style.fontSize = "20px";
        divW.appendChild(title);

        var detail = ssdjs.dom.text(150, 93, null, null, "(" + item.n + "个人文件," + item.s + "KB" + ")");
        detail.style.color = "#000";
        detail.style.fontSize = "20px";
        divW.appendChild(detail);

        return divW;

    },
    req : function(p) {
        p = (p || {});
        console.log("userReqP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("NetworkDisk/NetworkDiskListJson");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;

        var success = function(result) {
            console.log(result);
            hl.biz.disk.dataTest = result;
            hl.biz.disk.init();
            console.log(result);
        };
        var error = function(result) {
            console.log("失败");
            console.log(result);
            hl.diag.alert({
                m : "网路错误"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    dataTest : {
        doc : {
            "Num" : 0,
            "Size" : 0
        },
        pic : {
            "Num" : 0,
            "Size" : 0
        },
        video : {
            "Num" : 0,
            "Size" : 0
        },
        audio : {
            "Num" : 0,
            "Size" : 0
        },
        others : {
            "Num" : 0,
            "Size" : 0
        }
    },

    // 二级列表请求
    subReq : function(p) {
        p = (p || {});
        console.log("userReqP:" + JSON.stringify(p));
        var loadstat = hl.diag.loading.show();
        // URL
        var url = hl.url.api("NetworkDisk/FileListJson");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;
        if(p.type != null) {
            data += "&type=" + p.type;
        }

        var success = function(result) {
            hl.diag.loading.hide(loadstat);
            console.log(result);
            hl.biz.disk.subDataTest = result.list;
            if(p.type == 2) {
                hl.biz.disk.imgList();
            } else {
                hl.biz.disk.subList({
                    type : p.type
                });
            }

            console.log(result);
        };
        var error = function(result) {
            hl.diag.loading.hide(loadstat);
            console.log("失败");
            console.log(result);
            hl.diag.alert({
                m : "网路错误"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    /**
     * 文件列表
     */
    subList : function(p) {
        hl.diag.menu.hide();
        var titleMain = "我的网盘";
        switch (p.type) {
            case 1:
                titleMain = "文档";
                break;
            case 2:
                return hl.biz.disk.imgList();
                break;

            case 3:
                titleMain = "视频";
                break;

            case 4:
                titleMain = "音频";
                break;

            case 5:
                titleMain = "其他";
                break;

        }
        hl.tpl.diag.popStyle02({
            id : "dl", // diskList
            title : titleMain,
            l : hl.biz.disk.index,
        });
        hl.sys.hl$("up").style.display = "none";
        var main = $("#dlMain");

        var _h = $("#dlMain").height();
        var _w = $("#dlMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.append(contentBg);

        var hscrollD = hl.tpl.scroll.v.create("hscrollDL", 0, 0, _w, _h);
        main.append(hscrollD);

        // 滑动区域scroll定义
        scrollD = hl.sys.scroll({
            id : "hscrollDL",
            snap : false,
            vScrollbar : true,
            hScroll : false,
            momentum : true,
            hideScrollbar : true
        });
        var dlList = hl.biz.disk.subDataTest;
        if(dlList != null && dlList.length > 0) {

            for(var i = 0; i < dlList.length; i++) {
                hl.tpl.scroll.v.append("hscrollDL", hl.biz.disk.subItem2({
                    // w : _w,
                    // h : 100,
                    item : {
                        n : dlList[i].file_name,
                        ut : dlList[i].updated_at,
                        t : p.type,
                        i : dlList[i].id,
                    }
                }));
            }
        }
        scrollD.refresh();

        var size = hl.sys.winSize();
        var bottom = ssdjs.dom.div(0, size.y - 91 - 97, size.x - 4, 98);
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";

        var _x = 0;

        var btnDel = hl.tpl.btnBottom("share", _x, 0, size.x / 2, 75, "menu/share.png", "共享", 0, function(obj) {
            hl.biz.disk.checkOn({
                type : p.type
            });
            // 底部菜单
            var size = hl.sys.winSize();
            var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
            // TODO 背景要修改成图片的
            bottom.style.backgroundColor = "#202020";
            bottom.style.border = "2px outset #474747";
            bottom.id = "bottomEdit";

            var _x = 0;
            var btnchoseFirend = hl.tpl.btnMenu("choseFirend", _x, 0, size.x, 75, "", "选择好友共享", 0, function(obj) {
                var list = hl.biz.disk.subDataTest;
                var ids = "";
                for(var i = 0; i < list.length; i++) {
                    var item = list[i];
                    if(hl.sys.$(item.id + "checkD").status == "on") {
                        console.log(hl.sys.$(item.id + "checkD").tag.id);
                        ids += hl.sys.$(item.id + "checkD").tag.id + ",";
                    }
                }
                if(ids != "") {
                    ids = ids.substr(0, ids.length - 1);
                }

                if(ids == "") {
                    return hl.diag.alert({
                        m : "请选择要共享的文件"
                    });
                }
                hl.data.disk.share.fileIds = ids;
                hl.data.disk.share.backFunction = hl.biz.disk.subList;
                hl.data.disk.share.backFunctionP = p;

                hl.biz.disk.choseFirend();
            });
            bottom.appendChild(btnchoseFirend);
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

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";
           
            titleText.style.fontSize = "40px";
            title.appendChild(titleText);

            var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                hl.biz.disk.checkOff();
                hl.biz.disk.subList({
                    type : p.type
                });

            });
            title.appendChild(btnBack);

            var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

                // 顶部操作栏的修改
                $("#title").remove();
                var title = ssdjs.dom.div(0, 0, size.x, 94);
                title.id = "title";
                var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
                title.appendChild(titleBg);

                var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
                titleText.style.color = "#FFF";
                titleText.style.textAlign = "center";
            
                titleText.style.fontSize = "40px";
                title.appendChild(titleText);

                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {

                    hl.biz.disk.checkOff();
                    hl.biz.disk.subList({
                        type : p.type
                    });

                });
                title.appendChild(btnBack);

                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                    hl.biz.disk.checkNo();
                    hl.biz.disk.chose({
                        type : p.type
                    });
                });
                title.appendChild(btnEdit);
                hl.diag.pop.add(title);
                hl.biz.disk.checkAll();
            });
            title.appendChild(btnEdit);

            hl.diag.pop.add(title);

        });
        bottom.appendChild(btnDel);
        _x += size.x / 2;
        var btnUp = hl.tpl.btnBottom("select", _x, 0, size.x / 2, 75, "menu/select.png", "删除", 0, function(obj) {
            hl.biz.disk.chose({
                type : p.type
            });
            // TODO menu --> 删除

        });
        bottom.appendChild(btnUp);

        main.append(bottom);

    },
    chose : function(p) {

        var titleMain = "";
        switch (p.type) {
            case 1:
                titleMain = "文档";
                break;
            case 2:
                titleMain = "图片";
                break;

            case 3:
                titleMain = "视频";
                break;

            case 4:
                titleMain = "音频";
                break;

            case 5:
                titleMain = "其他";
                break;
        }

        // 开启多选
        hl.biz.disk.checkOn();

        // 底部菜单
        var size = hl.sys.winSize();
        var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
        // TODO 背景要修改成图片的
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";
        bottom.id = "bottomEdit";

        var _x = 0;
        var btnDelete = hl.tpl.btnMenu("delete", _x, 0, size.x, 75, "", "删除", 0, function(obj) {
            hl.biz.disk.deleteFile({
                type : p.type
            });
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

        var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
        titleText.style.color = "#FFF";
        titleText.style.textAlign = "center";
     
        titleText.style.fontSize = "40px";
        title.appendChild(titleText);

        var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
            hl.biz.disk.checkOff();
            hl.biz.disk.subList({
                type : p.type
            });

        });
        title.appendChild(btnBack);

        var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

            // 顶部操作栏的修改
            $("#title").remove();
            var title = ssdjs.dom.div(0, 0, size.x, 94);
            title.id = "title";
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";
         
            titleText.style.fontSize = "40px";
            title.appendChild(titleText);

            var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {

                hl.biz.disk.checkOff();
                hl.biz.disk.subList({
                    type : p.type
                });

            });
            title.appendChild(btnBack);

            var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                hl.biz.disk.checkNo();
                hl.biz.disk.chose({
                    type : p.type
                });
            });
            title.appendChild(btnEdit);
            hl.diag.pop.add(title);
            hl.biz.disk.checkAll();
        });
        title.appendChild(btnEdit);

        hl.diag.pop.add(title);

    },
    deleteImg : function(p) {
        p = (p || {});
        console.log("deleteFileP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("NetworkDisk/DeleteFiles");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;

        var list = hl.biz.disk.subDataTest;
        var ids = "";

        if(p.id != null) {
            data += "&fileIds=" + p.id;
        }

        var success = function(result) {
            console.log(result);
            hl.biz.disk.subReq({
                type : p.type
            });
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
    deleteFile : function(p) {
        p = (p || {});
        console.log("deleteFileP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("NetworkDisk/DeleteFiles");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;

        var list = hl.biz.disk.subDataTest;
        var ids = "";
        for(var i = 0; i < list.length; i++) {
            var item = list[i];
            if(hl.sys.$(item.id + "checkD").status == "on") {
                ids += item.id + ",";
            }
        }
        if(ids != "") {
            ids = ids.substr(0, ids.length - 1);
        }

        if(ids != "") {
            data += "&fileIds=" + ids;
        }
        if(ids == "") {
            hl.diag.alert({
                m : "请选择文件"
            });
            return;
        }

        var success = function(result) {
            console.log(result);
            hl.biz.disk.subReq({
                type : p.type
            });
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
    /**
     * 多选打开
     */
    checkOn : function(p) {
        var p = (p || {});

        // TODO 判断是哪个列表确定list
        var list = hl.biz.disk.subDataTest;
        for(var i = 0; i < list.length; i++) {
            var item = list[i];
            if(p.type != null && p.type == 2) {
                hl.sys.$(item.id + "checkD").style.display = "";
            } else {
                $("#" + item.id + "divMoveWD").animate({
                    'left' : '60px'
                }, 200);

                hl.sys.$(item.id + "checkD").style.display = "";
            }
        }
    },
    /**
     * 多选关闭
     */
    checkOff : function(p) {
        var p = (p || {});
        // TODO 判断是哪个列表确定list
        var list = hl.biz.disk.subDataTest;
        for(var i = 0; i < list.length; i++) {
            var item = list[i];
            if(p.type != null && p.type == 2) {
                hl.sys.$(item.id + "checkD").style.display = "none";
            } else {
                $("#" + item.id + "divMoveWD").animate({
                    'left' : '0px'
                }, 200);
                hl.sys.$(item.id + "checkD").style.display = "none";
            }
        }
    },
    /**
     * 全选
     */
    checkAll : function() {
        var list = hl.biz.disk.subDataTest;
        for(var i = 0; i < list.length; i++) {

            var item = list[i];
            // icon状态的改变
            hl.sys.$(item.id + "checkD").status = "on";
            hl.sys.$(item.id + "checkD").style.backgroundPosition = "0px 0px";
        }
    },
    /**
     * 全不选
     */
    checkNo : function() {
        var list = hl.biz.disk.subDataTest;
        for(var i = 0; i < list.length; i++) {

            var item = list[i];
            // icon状态的改变
            hl.sys.$(item.id + "checkD").status = "off";
            hl.sys.$(item.id + "checkD").style.backgroundPosition = "0px -46px";
        }
    },
    subItem : function(p) {
        console.log(p);
        var item = p.item;

        var size = hl.sys.winSize();
        var _w = size.x - 10;
        var _h = 100;

        var divW = ssdjs.dom.div(null, null, _w, _h);

        var bgLine = ssdjs.dom.img(0, _h - 2, _w, 2, hl.url.img("line.jpg"));
        divW.appendChild(bgLine);

        var iconR = ssdjs.dom.img(_w - 40, 34, 19, 29, hl.url.img("icon3.png"));
        divW.appendChild(iconR);

        var headImg = "other";
        switch (item.t) {
            case 1:
                headImg = "office";
                break;
            case 2:
                headImg = "picture";
                break;

            case 3:
                headImg = "voide";
                break;

            case 4:
                headImg = "music";
                break;

            case 5:
                headImg = "other";
                break;

        }

        var head = ssdjs.dom.img(25, 10, 80, 80, hl.url.img(headImg + ".png"));
        divW.appendChild(head);

        var name = ssdjs.dom.text(130, 30, null, null, item.n);
        name.style.color = "#000";
        name.style.fontSize = "20px";
        divW.appendChild(name);

        var updatedAt = ssdjs.dom.text(130, 60, null, null, item.ut);
        updatedAt.style.color = "#000";
        updatedAt.style.fontSize = "20px";
        divW.appendChild(updatedAt);

        // divW.evtEnd(function(obj) {
        // hl.biz.disk.detail({
        // item : obj.tag,
        // type : item.t
        // });
        // });
        divW.tag = item;

        //
        // divW.appendChild(btnDownLoad);

        // divW.evtEnd(function(obj, evt) {
        // console.log("ssssssssss");
        // var point = ssdjs.events.getPoint(evt);
        // var opt = hl.tpl.diag.opt("tttt", point.x, point.y, 340);
        // // nick
        //
        // if(item.nick != null) {
        // var nick = ssdjs.dom.text(0, 20, 160, null, "www");
        // nick.className = "cGray sSmall";
        // nick.style.textAlign = "center";
        // nick.style.overflow = "hidden";
        // opt.appendChild(nick);
        // }
        //
        // hl.diag.opt.show(opt);
        // });
        return divW;

    },
    subItem2 : function(p) {
        console.log(p);
        var item = p.item;

        var size = hl.sys.winSize();
        var _w = size.x - 10;
        var _h = 100;

        var divW = ssdjs.dom.div(null, null, _w, _h);

        var check = hl.tpl.btn.check3(item.i + "checkD", 15, 30, "off", item, null);
        check.style.display = "none";

        divW.appendChild(check);

        var divMOveW = ssdjs.dom.div(0, 0, _w, _h);
        divMOveW.id = item.i + "divMoveWD";
        divW.appendChild(divMOveW);

        var bgLine = ssdjs.dom.img(0, _h - 2, _w, 2, hl.url.img("line.jpg"));
        divW.appendChild(bgLine);

        var iconR = ssdjs.dom.img(_w - 40, 34, 19, 29, hl.url.img("icon3.png"));
        divW.appendChild(iconR);

        var headImg = "other";
        switch (item.t) {
            case 1:
                headImg = "office";
                break;
            case 2:
                headImg = "picture";
                break;

            case 3:
                headImg = "voide";
                break;

            case 4:
                headImg = "music";
                break;

            case 5:
                headImg = "other";
                break;

        }
        var nameValue = item.n
        var type = "def";
        type = nameValue.substr(nameValue.indexOf(".") + 1, nameValue.length);
        var head = hl.tpl.getFileIcon({
            x : 25,
            y : 14,
            name : type
        });
        divMOveW.appendChild(head);

        var name = ssdjs.dom.text(130, 30, null, null, item.n);
        name.style.color = "#000";
        name.style.fontSize = "20px";
        divMOveW.appendChild(name);

        var updatedAt = ssdjs.dom.text(130, 60, null, null, item.ut);
        updatedAt.style.color = "#000";
        updatedAt.style.fontSize = "20px";
        divMOveW.appendChild(updatedAt);

        // divMOveW.evtEnd(function(obj) {
        // hl.biz.disk.detail({
        // item : obj.tag,
        // type : item.t
        // });
        // });
        divMOveW.tag = item;

        //
        // divW.appendChild(btnDownLoad);

        // divW.evtEnd(function(obj, evt) {
        // console.log("ssssssssss");
        // var point = ssdjs.events.getPoint(evt);
        // var opt = hl.tpl.diag.opt("tttt", point.x, point.y, 340);
        // // nick
        //
        // if(item.nick != null) {
        // var nick = ssdjs.dom.text(0, 20, 160, null, "www");
        // nick.className = "cGray sSmall";
        // nick.style.textAlign = "center";
        // nick.style.overflow = "hidden";
        // opt.appendChild(nick);
        // }
        //
        // hl.diag.opt.show(opt);
        // });
        return divW;

    },
    subDataTest : {},

    detail : function(p) {
        console.log(p);
        var titleMain = p.item.n;
        var size = hl.sys.winSize();
        var item = p.item;

        var headImg = "other";
        switch (p.type) {
            case 1:
                headImg = "office";
                break;
            case 2:
                headImg = "picture";
                break;

            case 3:
                headImg = "voide";
                break;

            case 4:
                headImg = "music";
                break;

            case 5:
                headImg = "other";
                break;

        }

        console.log(titleMain + "---" + "detail");
        hl.tpl.diag.popStyle03({
            id : "dl", // diskList
            title : titleMain,
            l : hl.biz.disk.subReq,
            lP : {
                type : p.type
            }
        });
        hl.sys.$("up").style.display = "none";
        var main = hl.sys.$("dlMain");

        var _h = $("#dlMain").height();
        var _w = $("#dlMain").width();

        var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
        main.appendChild(contentBg);

        var contentImg = ssdjs.dom.img((size.x - 100) / 2, 250, 100, 100, hl.url.img(headImg + ".png"));
        main.appendChild(contentImg);

        var title = ssdjs.dom.text(0, 400, size.x, null, "暂不支持该文件格式预览，请下载后打开");
        title.id = "xjc";
        title.style.textAlign = "center";
        title.style.color = "#111";
        title.style.fontSize = "20px";
        main.appendChild(title);

        var bottom = ssdjs.dom.div(0, size.y - 91 - 97, size.x - 4, 98);
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";

        var _x = 0;

        var btnDown = hl.tpl.btnBottom("download", _x, 0, size.x / 4, 75, "download.png", "下载", 0, function(obj) {
            hl.biz.disk.downLoad({
                fid : obj.tag.i,
                fname : obj.tag.n
            });
        });
        btnDown.tag = item;
        bottom.appendChild(btnDown);
        _x += size.x / 4;
        var btnUp = hl.tpl.btnBottom("up", _x, 0, size.x / 4, 75, "up.png", "上传", 0, function(obj) {

        });
        //  bottom.appendChild(btnUp);
        _x += size.x / 4;
        var btnDel = hl.tpl.btnBottom("delete", _x, 0, size.x / 4, 75, "delete.png", "删除", 0, function(obj) {
            // TODO 删除确认页

            hl.biz.disk.deleteFile({
                type : p.type
            });

        });
        bottom.appendChild(btnDel);
        _x += size.x / 4;
        var btnShare = hl.tpl.btnBottom("share", _x, 0, size.x / 4, 75, "share.png", "共享", 0, function(obj) {

            // 进入用户列表选择共享对象
            hl.biz.user.chose();

            // 获取选择呢结果
            hl.biz.user.choseResult();

            // 共享
            hl.biz.disk.share();

        });
        bottom.appendChild(btnShare);

        main.appendChild(bottom);

    },
    choseFirend : function(p) {
        p = (p || {});
        console.log("userReqP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("User/UserListLoad");
        if(p.txtSearch != null) {
            url = hl.url.api("User/searchJsonInfo");
        }
        var data = "";
        if(p.txtSearch != null) {
            data += "txtSearch=" + p.txtSearch;
        }
        var success = function(result) {
            console.log("成功");
            hl.biz.user.resultData = result;

            hl.data.disk.choseList.clear();
            // 初始化选择列表
            hl.tpl.diag.pop({
                id : "cuser",
                title : "选择联系人",
                l : hl.biz.disk.index,
                r : 0
            });

            var main = $("#cuserMain");
            main.css("background", "#fff");

            var _h = $("#cuserMain").height();
            var _w = $("#cuserMain").width();

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

                hl.biz.user.req({
                    txtSearch : $("#searchInput").val(),
                });

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

            var hscrollU = hl.tpl.scroll.v.create("hscrollU", 0, 87, _w, _h - 87);
            main.append(hscrollU);

            // 滑动区域scroll定义
            scrollU = hl.sys.scroll({
                id : "hscrollU",
                snap : false,
                vScrollbar : true,
                hScroll : false,
                momentum : true,
                hideScrollbar : true
            });

            if(p.txtSearch != null) {
                if(hl.biz.user.resultData.listTest != null) {
                    for(var i = 0; i < hl.biz.user.resultData.listTest.length; i++) {
                        var tItem = hl.biz.user.resultData.listTest[i];
                        var tem = hl.biz.disk.friendItem({
                            location : {
                                x : null, // 在此修改缩进缩进
                                y : null,
                                w : null,
                                h : 108,
                            },
                            item : tItem
                        });
                        hl.tpl.scroll.v.append("hscrollU", tem);
                    }
                    scrollU.refresh();
                }

            } else {
                // var titleItem = hl.biz.user.resultData.item;
                // var divW = ssdjs.dom.div(null, null, _w, 60);
                // var divTitle = ssdjs.dom.div(null, 10, _w, 50);
                // divTitle.style.backgroundColor = "#c7c7c7";
                // var title = ssdjs.dom.text(20, 15, null, null, titleItem.name);
                // title.style.color = "#fff";
                // divTitle.appendChild(title);
                // var bgLine = ssdjs.dom.img(0, 48, _w, 2, hl.url.img("line.jpg"));
                // divTitle.appendChild(bgLine);
                // divW.appendChild(divTitle);
                // hl.tpl.scroll.v.append("hscrollU", divW);

                var list = hl.biz.user.resultData.item.list;
                for(var i = 0; i < list.length; i++) {
                    var tItem = list[i]
                    var tem = hl.biz.disk.groupItem({
                        location : {
                            x : null,
                            y : null,
                            w : null,
                            h : null
                        },
                        item : {
                            name : tItem.name,
                            id : tItem.id,
                            list : tItem.list ,
                        },
                        isOpen : 1
                    });
                    hl.tpl.scroll.v.append("hscrollU", tem);
                }

                scrollU.refresh();
            }

            hl.biz.disk.choseFriendrefresh(p);

            console.log(result);
        };
        var error = function(result) {
            console.log("失败");
            console.log(result);
            hl.diag.alert({
                m : "登陆失败"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    groupItem : function(p) {
        console.log("groupItemNewP");
        console.log(p);

        var location = (p.location || {});
        var item = (p.item || {});
        item.list = (item.list || []);

        var size = hl.sys.winSize();
        var _x = (p.location.x || null);
        var _y = (p.location.y || null);
        var _w = (p.location.w || size.x);
        var _h = (p.location.h || 49);

        var iconOff = "user/00.png";
        var iconOn = "user/01.png";
        var titleColor = "#fff";

        var divW = ssdjs.dom.div(null, null, _w, _h);
        divW.id = item.id + "Main";
        divW.HH = _h;
        // divW.style.margin = "5px";

        var _xx = _x;
        var _ww = _w;

        var divTitle = ssdjs.dom.div(_xx, 0, _ww, _h);
        divTitle.id = item.id + "Title";
        divTitle.style.backgroundColor = "#c7c7c7";
        var bgLine = ssdjs.dom.img(0, _h - 2, _ww, 2, hl.url.img("line.jpg"));
        divTitle.appendChild(bgLine);
        var title = ssdjs.dom.text(25, 11, null, null, item.name);
        title.style.fontSize = "26px";

        divTitle.appendChild(title);

        // var icon = ssdjs.dom.div(20, 15, 18, 18);
        //
        // icon.style.backgroundRepeat = "no-repeat";
        // icon.id = item.id + "titileIcon";
        // divTitle.appendChild(icon);

        divW.appendChild(divTitle);

        var _hhh = 100;

        var absoluteLeft = divTitle.offsetLeft;
        console.log(absoluteLeft + item.name);
        // switch(size.x - _w) {
        // case 0 :
        // iconOff = "user/10.png";
        // iconOn = "user/11.png";
        // titleColor = "#ffb400";
        //
        // break;
        // case 20 :
        // iconOff = "user/20.png";
        // iconOn = "user/21.png";
        // titleColor = "#a68ff7";
        //
        // break;
        // case 40 :
        // iconOff = "user/30.png";
        // iconOn = "user/31.png";
        // titleColor = "#6898d1";
        //
        // break;
        // case 60 :
        // iconOff = "user/40.png";
        // iconOn = "user/41.png";
        // titleColor = "#e94d63";
        //
        // break;
        // default:
        // iconOff = "user/10.png";
        // iconOn = "user/11.png";
        // titleColor = "#ffb400";
        //
        // }
        //
        //   icon.style.backgroundImage = hl.url.cssimg(iconOn);
        title.style.color = titleColor;

        // divTitle.evtEnd(function(obj) {
        // var idd = obj.tag.id + "Content";
        // if($("#" + idd).is(":hidden")) {
        // $("#" + idd).slideDown(10);
        // hl.sys.$(obj.tag.id + "titileIcon").style.backgroundImage = hl.url.cssimg(iconOn);
        // //  hl.sys.$(obj.tag.id + "Title").className = "change";
        // // var newH4Mian = hl.sys.hl$(obj.tag.id + "Main").HH + hl.sys.hl$(obj.tag.id + "Content").HH;
        // // hl.sys.hl$(obj.tag.id + "Main").style.height = newH4Mian + "px";
        //
        // // TODO 各层父节点的高度变化
        // var pId = $("#" + idd).parent().attr("id");
        // console.log("父节点" + pId);
        // while(pId != "hscrollUmain") {
        // hl.sys.hl$(pId).HH = hl.sys.hl$(pId).HH + hl.sys.hl$(obj.tag.id + "Content").HH;
        // console.log(pId + "__" + hl.sys.hl$(pId).HH);
        // hl.sys.hl$(pId).style.height = hl.sys.hl$(pId).HH + "px";
        // pId = $("#" + pId).parent().attr("id");
        // }
        //
        // var mask = ssdjs.$("hscrollU");
        // mask.ssdh = (mask.ssdh || 0) + hl.sys.hl$(obj.tag.id + "Content").HH;
        // ssdjs.$("hscrollUmain").style.height = mask.ssdh + "px";
        // scrollU.refresh();
        // } else {
        // //   hl.sys.$(obj.tag.id + "Title").className = "";
        // hl.sys.$(obj.tag.id + "titileIcon").style.backgroundImage = hl.url.cssimg(iconOff);
        // $("#" + idd).slideUp(10);
        // divW.style.height = _h + "px";
        // var pId = $("#" + idd).parent().attr("id");
        // console.log("父节点" + pId);
        // while(pId != "hscrollUmain") {
        // hl.sys.hl$(pId).HH = hl.sys.hl$(pId).HH - hl.sys.hl$(obj.tag.id + "Content").HH;
        // console.log(pId + "__" + hl.sys.hl$(pId).HH);
        // hl.sys.hl$(pId).style.height = hl.sys.hl$(pId).HH + "px";
        // pId = $("#" + pId).parent().attr("id");
        // }
        //
        // var mask = ssdjs.$("hscrollU");
        // mask.ssdh = (mask.ssdh || 0) - hl.sys.hl$(obj.tag.id + "Content").HH;
        // ssdjs.$("hscrollUmain").style.height = mask.ssdh + "px";
        // scrollU.refresh();
        // }
        // });
        divTitle.tag = item;

        var divContent = ssdjs.dom.div(_xx, _h, _ww, 0);
        divContent.HH = 0;
        divContent.id = item.id + "Content";
        divContent.style.display = "none";

        // 默认收起

        // 向content中添加列表，要判断列表项是组项还是成员向。
        if(item.list.length > 0) {

            if(item.list[0].SEX != null) {

                // 有性别属性的为成员项
                var _yy = 0;
                var _hh = 108;
                for(var i = 0; i < item.list.length; i++) {

                    divContent.appendChild(hl.biz.disk.friendItem({
                        location : {
                            x : 0, // 在此修改缩进缩进
                            y : _yy,
                            w : _ww,
                            h : _hh,
                        },
                        item : item.list[i]
                    }));
                    _yy += _hh;
                }
                divContent.HH = _yy;
                divContent.style.height = _yy + "px";
            } else {

                // 组项
                var _yy = 0;
                var _hh = 46;
                for(var i = 0; i < item.list.length; i++) {

                    var tItem = item.list[i];

                    divContent.appendChild(hl.biz.disk.groupItem({
                        location : {
                            x : 20,
                            y : null,
                            w : _ww - 20,
                            h : null
                        },
                        item : {
                            name : tItem.name,
                            id : tItem.id,
                            list : tItem.list ,
                        },
                        isOpen : 1
                    }));
                    _yy += _hh;
                }
                divContent.HH = _yy;
                divContent.style.height = _yy + "px";

            }
        }

        divW.appendChild(divContent);

        if(p.isOpen != null) {
            divW.HH = divW.HH + divContent.HH;
            divW.style.height = divW.HH + "px";
            divContent.style.display = "";
        }
        return divW;
    },
    friendItem : function(p) {
        var location = (p.location || {});
        var item = (p.item || {});

        var size = hl.sys.winSize();
        var _x = (p.location.x || null);
        var _y = (p.location.y || null);
        var _w = (p.location.w - p.location.x || size.x - 10);
        var _h = (p.location.h || 108);

        var divW = ssdjs.dom.div(_x, _y, _w, _h);

        var check = hl.tpl.btn.check2(item.USERID + "usercheck", 15, 32, "off", item, hl.biz.disk.choseFriendrefresh);

        divW.appendChild(check);

        var line = ssdjs.dom.img(0, _h - 2, _w, 2, hl.url.img("line.jpg"));
        divW.appendChild(line);

        var divMoveW = ssdjs.dom.div(60, 0, _w - 60, _h);
        divMoveW.id = item.USERID + "userdivMoveW";
        divW.appendChild(divMoveW);

        var headIcon = hl.url.img("user/head.jpg");
        if(item.FACEFILE != "/Files/Faces/") {
            headIcon = hl.url.api(item.FACEFILE);
        }
        var head = ssdjs.dom.img(10, 19, 70, 70, headIcon);
        divMoveW.appendChild(head);

        // var messageIcon = ssdjs.dom.img(75, 8, 10, 10, hl.url.img("h.jpg"));
        // messageIcon.id = item.id + "fmIcon";
        // messageIcon.style.display = "none";
        // divMoveW.appendChild(messageIcon);
        //
        var nick = ssdjs.dom.text(110, 39, null, null, item.ACTUALNAME);
        nick.style.fontSize = "30px";
        divMoveW.appendChild(nick);
        //
        // var sex = ssdjs.dom.img(135, 50, 28, 28, hl.url.img(item.SEX + ".png"));
        // divMoveW.appendChild(sex);
        //
        // var message = ssdjs.dom.img(_w - 50, 38, 32, 32, hl.url.img("chat.png"));
        // divW.appendChild(message);

        return divW;
    },
    choseFriendrefresh : function(p) {
        var bottomMenu = $("#iMenu");
        var size = hl.sys.winSize();

        bottomMenu.empty();
        var bottomMenuBg = ssdjs.dom.img(0, 0, "100%", 98, hl.url.img("mainMenu_bg.jpg"));
        bottomMenu.append(bottomMenuBg);

        // 横向滑动区域
        var hscrollN = hl.tpl.scroll.h.create("hscrollN", 2, 15, size.x - 146, 68);
        bottomMenu.append(hscrollN);
        hscrollN = hl.sys.scroll({
            id : "hscrollN",
            hScrollbar : false,
            vScroll : false,
            arrow : "hscrollN"
        });

        var list = hl.data.disk.choseList.list;
        var num = 0;

        for(var i = 0; i < list.length; i++) {
            if(list[i].id == null) {
                continue;
            }
            num++;
            hl.tpl.scroll.h.append("hscrollN", hl.biz.disk.choseFrienditem({
                item : list[i]
            }));

        }
        hl.tpl.scroll.h.append("hscrollN", hl.biz.disk.choseFrienditem({
            item : {
                id : -1
            }
        }));
        hscrollN.refresh();
        // 确认按钮

        var divBtn = ssdjs.dom.div(size.x - 140, 18, 136, 62);
        if(num < 1) {
            divBtn.style.backgroundImage = hl.url.cssimg("share/btn3.png");
            var text = ssdjs.dom.text(0, 16, 136, null, "确定");
            text.style.color = "#FFF";
            text.style.textAlign = "center";
            text.style.fontSize = "24px";
            divBtn.appendChild(text);
        } else {
            divBtn.style.backgroundImage = hl.url.cssimg("share/btn0.png");
            var text = ssdjs.dom.text(0, 16, 136, null, "确定(" + num + ")");
            text.style.color = "#fff";
            text.style.textAlign = "center";
            text.style.fontSize = "24px";
            divBtn.appendChild(text);
            divBtn.evtEnd(function(obj) {
                obj.style.backgroundImage = hl.url.cssimg("share/btn0.png");
                hl.biz.disk.share();
            });
            divBtn.evtMove(function(obj) {
                obj.style.backgroundImage = hl.url.cssimg("share/btn0.png");
            });
            divBtn.evtStart(function(obj) {
                obj.style.backgroundImage = hl.url.cssimg("share/btn1.png");
            });
        }
        bottomMenu.append(divBtn);

        $("#iMenu").show();
    },
    choseFrienditem : function(p) {
        console.log(p);
        var item = p.item;
        var divW = ssdjs.dom.div(null, null, 72, 68);
        var headDiv = ssdjs.dom.div(2, null, 68, 68);
        divW.appendChild(headDiv);
        if(item.id == -1) {
            headDiv.style.backgroundImage = hl.url.cssimg("share/add.png");
        } else {
            var headIcon = hl.url.img("user/head.jpg");
            if(item.head != "/Files/Faces/") {
                headIcon = hl.url.api(item.head);
            }

            var head = ssdjs.dom.img(0, 0, 68, 68, headIcon);
            headDiv.appendChild(head);
        }
        return divW;
    },
    /**
     * 文件列表
     */
    // http://dlwt.csdn.net/fd.php?i=653965954076012&s=8be24ca73294d4dc9dc65b486db7145d

    fileDownLoad : function(p) {

        var user = hl.data.user.get();
        var userID = user.ids;

        var fileId = "";
        var form = $("<form>");
        form.attr('style', 'display:none');
        form.attr('target', '');
        form.attr('method', 'post');
        form.attr('action', 'http://192.168.1.21:8080/NetworkDisk/DownLoadFile?');
        var input1 = $('<input>');
        input1.attr('type', 'hidden');
        input1.attr('name', 'fileid');
        input1.attr('value', '165');
        var input2 = $('<input>');
        input2.attr('type', 'hidden');
        input2.attr('name', 'userID');
        input2.attr('value', '10040');
        $('body').append(form);
        form.append(input1);
        form.append(input2);

        form.submit();
        form.remove();

    },
}