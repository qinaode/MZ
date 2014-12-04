/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.diskAll = {
    index : function(p) {
        hl.biz.diskAll.diskAll({
            fid : 0,
            title : "列表显示"
        });
    },
    diskAll2 : function(p) {
        p = (p || {});
        console.log("diskAllP:" + JSON.stringify(p));
        var loadstat = hl.diag.loading.show();
        // URL
        var url = hl.url.api("NetworkDisk/FileListInfo");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;
        if(p.fid != null && p.fid != 0) {
            data += "&folderId=" + p.fid;
        }
        var success = function(result) {
            hl.diag.loading.hide(loadstat);

            var titleMain = "我的网盘";
            if(p.title != null) {
                titleMain = p.title;
            }

            // 绘制页面

            var size = hl.sys.winSize();
            var main = ssdjs.dom.div(0, 0, size.x, size.y);
            main.id = p.fid + "diskAll";
            main.style.backgroundColor = "#123";
            main.style.zIndex = 9999;
            $("#pop").append(main);

            main.className = "fadeInLeft";

            if(p.fid != null && p.back == null) {
                hl.data.disk.backList.push({
                    fid : p.fid,
                    title : p.title
                });
            }

            var _w = size.x;
            var _h = size.y;

            var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
            main.appendChild(contentBg);

            // var hscrollA = hl.tpl.scroll.v.create("hscrollDA", 0, 0, _w, _h, null, "refresh");
            // main.appendChild(hscrollA);
            //
            // // 滑动区域scroll定义
            // scrollA = hl.sys.scroll({
            // id : "hscrollDA",
            // snap : false,
            // vScrollbar : true,
            // hScroll : false,
            // momentum : true
            // });
            // hl.biz.diskAll.testData = result.list;
            // if(hl.biz.diskAll.testData != null && hl.biz.diskAll.testData.length > 0) {
            //
            // for(var i = 0; i < hl.biz.diskAll.testData.length; i++) {
            // var temItem = hl.biz.diskAll.testData[i];
            // hl.tpl.scroll.v.append("hscrollDA", hl.biz.diskAll.item({
            // item : temItem
            // }));
            // }
            // }
            // scrollA.refresh();

            var bottom = ssdjs.dom.div(0, size.y - 94 - 97, size.x - 4, 98);
            bottom.style.backgroundColor = "#202020";
            bottom.style.border = "2px outset #474747";

            var _x = 0;
            var btnDel = hl.tpl.btnBottom("share", _x, 0, size.x / 4, 75, "menu/share.png", "共享", 0, function(obj) {
                // 开启多选
                hl.biz.diskAll.checkOn();

                // 底部菜单
                var size = hl.sys.winSize();
                var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
                // TODO 背景要修改成图片的
                bottom.style.backgroundColor = "#202020";
                bottom.style.border = "2px outset #474747";
                bottom.id = "bottomEdit";

                var _x = 0;
                var btnDelete = hl.tpl.btnMenu("delete", _x, 0, size.x, 75, "", "选择好友共享", 0, function(obj) {
                    var list = hl.biz.diskAll.testData;
                    var ids = "";
                    for(var i = 0; i < list.length; i++) {
                        var item = list[i];
                        if(hl.sys.$(item.id + "checkDA").status == "on") {
                            console.log(hl.sys.$(item.id + "checkDA").tag.id);
                            ids += hl.sys.$(item.id + "checkDA").tag.id + ",";
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
                    hl.data.disk.share.backFunction = hl.biz.diskAll.diskAll;
                    hl.data.disk.share.backFunctionP = p;
                    hl.biz.disk.choseFirend();
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

                var titleMain = "我的网盘";
                if(p.title != null) {
                    titleMain = p.title;
                }

                var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
                titleText.style.color = "#FFF";
                titleText.style.textAlign = "center";

                titleText.style.fontSize = "40px";
                title.appendChild(titleText);

                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    hl.biz.diskAll.checkOff();
                    var pp = p;
                    pp.back = 1;
                    hl.biz.diskAll.diskAll(pp);

                });
                title.appendChild(btnBack);

                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

                    // 顶部操作栏的修改
                    $("#title").remove();
                    var title = ssdjs.dom.div(0, 0, size.x, 94);
                    title.id = "title";
                    var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
                    title.appendChild(titleBg);

                    var titleMain = "我的网盘";
                    if(p.title != null) {
                        titleMain = p.title;
                    }
                    var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
                    titleText.style.color = "#FFF";
                    titleText.style.textAlign = "center";

                    titleText.style.fontSize = "40px";
                    title.appendChild(titleText);

                    var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {

                        hl.biz.diskAll.checkOff();

                        var pp = p;
                        pp.back = 1;
                        hl.biz.diskAll.diskAll(pp);

                    });
                    title.appendChild(btnBack);

                    var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                        hl.biz.diskAll.checkNo();
                        hl.biz.diskAll.chose(p);
                    });
                    title.appendChild(btnEdit);
                    hl.diag.pop.add(title);
                    hl.biz.diskAll.checkAll({
                        fid : p.parentFolder,
                        title : p.title
                    });
                });
                title.appendChild(btnEdit);

                hl.diag.pop.add(title);
            });
            bottom.appendChild(btnDel);
            _x += size.x / 4;
            var btnUp = hl.tpl.btnBottom("select", _x, 0, size.x / 4, 75, "menu/select.png", "删除", 0, function(obj) {
                var fid = 0;
                if(p.fid != null) {
                    fid = p.fid;
                }
                hl.biz.diskAll.chose(p);
            });
            bottom.appendChild(btnUp);
            _x += size.x / 4;
            var btnDown = hl.tpl.btnBottom("up", _x, 0, size.x / 4, 75, "menu/up.png", "上传", 0, function(obj) {
                hl.biz.diskAll.upLoadMenu();
            });
            bottom.appendChild(btnDown);
            _x += size.x / 4;
            var btnShare = hl.tpl.btnBottom("deal", _x, 0, size.x / 4, 75, "menu/deal.png", "管理", 0, function(obj) {
                // TODO 弹出菜单
                if(hl.tpl.menu.subMenuUp.status == 0) {
                    // TODO 传入本级文件夹信息
                    var fid = 0;
                    if(p.fid != null) {
                        fid = p.fid;
                    }
                    hl.tpl.menu.subMenuUp.show({
                        fid : fid,
                        title : p.title
                    });
                } else {
                    hl.tpl.menu.subMenuUp.hide();
                }

            });
            bottom.appendChild(btnShare);

            main.appendChild(bottom);

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
    upLoadMenu : function(p) {
        var size = hl.sys.winSize();
        var w = size.x;
        var h = 365;
        var divW = ssdjs.dom.div(0, size.y - h, w, h);
        divW.id = "upLoadMenu";
        var cw = 612;
        var bw = 502;
        if(w < cw) {
            cw = w - 20;
            bw = cw - 100;
        }
        var ch = 354;
        var cx = (w - cw) / 2;
        var divContent = ssdjs.dom.div(cx, 0, cw, ch);
        var bg = ssdjs.dom.img(0, 0, cw, ch, hl.url.img("upLoad/bg.png"));
        divContent.appendChild(bg);
        var _y = 0;
        var title = ssdjs.dom.text(0, 30, cw, ch / 4, "选择照片或者视频上传");
        title.style.color = "#505050";
        title.style.textAlign = "center";
        title.style.fontSize = "24px";
        divContent.appendChild(title);
        _y += ch / 4;
        var line = ssdjs.dom.div(0, _y, cw, 2);
        line.style.backgroundColor = "#d5d5d5";
        divContent.appendChild(line);

        var imgDiv = ssdjs.dom.div(0, _y, cw, ch / 4);
        imgDiv.evtEnd(function(obj) {
           hl.fileMgr.openImgBrowser() ;
        });

        var text = ssdjs.dom.text(0, 20, cw, ch / 4, "照片");
        text.style.color = "#5cb32b";
        text.style.textAlign = "center";
        text.style.fontSize = "30px";
        imgDiv.appendChild(text);
        divContent.appendChild(imgDiv);
        _y += ch / 4;
        var line = ssdjs.dom.div(0, _y, cw, 2);
        line.style.backgroundColor = "#d5d5d5";
        divContent.appendChild(line);
        var videoDiv = ssdjs.dom.div(0, _y, cw, ch / 4);
        var text = ssdjs.dom.text(0, 20, cw, ch / 4, "视频");
        text.style.color = "#5cb32b";
        text.style.textAlign = "center";
        text.style.fontSize = "30px";
        videoDiv.appendChild(text);
        divContent.appendChild(videoDiv);
        _y += ch / 4;
        var line = ssdjs.dom.div(0, _y, cw, 2);
        line.style.backgroundColor = "#d5d5d5";
        divContent.appendChild(line);
        var cancelDiv = ssdjs.dom.div(0, _y, cw, ch / 4);

        var btn = ssdjs.dom.div((cw - bw) / 2, 15, bw, 61);

        var bg0 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("upLoad/cancel0.png"));
        var bg1 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("upLoad/cancel1.png"));
        bg1.style.display = "none";
        btn.appendChild(bg0);
        btn.appendChild(bg1);
        var title = ssdjs.dom.text(0, 17, bw, null, "取消");
        title.style.color = "#fff";
        title.style.textAlign = "center";
        title.style.fontSize = "24px";
        btn.appendChild(title);

        //  btn.style.backgroundImage = hl.url.cssimg("upLoad/cancel0.png");
        btn.evtEnd(function(obj) {
            bg1.style.display = "none";
            //obj.style.backgroundImage = hl.url.cssimg("upLoad/cancel0.png");
            $("#upLoadMenu").hide();
            $("#upLoadMenu").remove();
        });
        btn.evtMove(function(obj) {
            bg1.style.display = "none";
            // obj.style.backgroundImage = hl.url.cssimg("upLoad/cancel0.png");
        });
        btn.evtStart(function(obj) {
            bg1.style.display = "";
            // obj.style.backgroundImage = hl.url.cssimg("upLoad/cancel1.png");
        });
        cancelDiv.appendChild(btn);

        divContent.appendChild(cancelDiv);

        divW.appendChild(divContent);

        hl.diag.pop.add(divW);

    },
    deleteFile : function(p) {
        p = (p || {});
        console.log("deleteFileP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("NetworkDisk/DeleteFiles");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;

        var list = hl.biz.diskAll.testData;
        var ids = "";
        for(var i = 0; i < list.length; i++) {
            var item = list[i];
            if(hl.sys.$(item.id + "checkDA").status == "on") {
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
            hl.biz.diskAll.diskAll({
                fid : p.parentFolder,
                title : p.title
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
    chose : function(p) {
        // 开启多选
        hl.biz.diskAll.checkOn();

        // 底部菜单
        var size = hl.sys.winSize();
        var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
        // TODO 背景要修改成图片的
        bottom.style.backgroundColor = "#202020";
        bottom.style.border = "2px outset #474747";
        bottom.id = "bottomEdit";

        var _x = 0;
        var btnDelete = hl.tpl.btnMenu("delete", _x, 0, size.x, 75, "", "删除", 0, function(obj) {
            hl.biz.diskAll.deleteFile({
                fid : p.parentFolder,
                title : p.title
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

        var titleMain = "我的网盘";
        if(p.title != null) {
            titleMain = p.title;
        }

        var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
        titleText.style.color = "#FFF";
        titleText.style.textAlign = "center";

        titleText.style.fontSize = "40px";
        title.appendChild(titleText);

        var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
            hl.biz.diskAll.checkOff();

            var pp = p;
            pp.back = 1;
            hl.biz.diskAll.diskAll(pp);

        });
        title.appendChild(btnBack);

        var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

            // 顶部操作栏的修改
            $("#title").remove();
            var title = ssdjs.dom.div(0, 0, size.x, 94);
            title.id = "title";
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleMain = "我的网盘";
            if(p.title != null) {
                titleMain = p.title;
            }
            var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);

            var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {

                hl.biz.diskAll.checkOff();
                hl.biz.diskAll.diskAll(p);

            });
            title.appendChild(btnBack);

            var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                hl.biz.diskAll.checkNo();
                hl.biz.diskAll.chose(p);
            });
            title.appendChild(btnEdit);
            hl.diag.pop.add(title);
            hl.biz.diskAll.checkAll({
                fid : p.parentFolder,
                title : p.title
            });
        });
        title.appendChild(btnEdit);

        hl.diag.pop.add(title);

    },
    /**
     * 多选打开
     */
    checkOn : function(p) {
        var list = hl.biz.diskAll.testData;
        if(list != null && list.length > 0) {
            for(var i = 0; i < list.length; i++) {
                var item = list[i];
                $("#" + item.id + "divMoveWDA").animate({
                    'left' : '60px'
                }, 200);

                hl.sys.$(item.id + "checkDA").style.display = "";
            }
        }
    },
    /**
     * 多选关闭
     */
    checkOff : function(p) {

        // TODO 判断是哪个列表确定list
        var list = hl.biz.diskAll.testData;
        if(list != null && list.length > 0) {
            for(var i = 0; i < list.length; i++) {
                var item = list[i];
                $("#" + item.id + "divMoveWDA").animate({
                    'left' : '0px'
                }, 200);
                hl.sys.$(item.id + "checkDA").style.display = "none";
            }
        }
    },
    /**
     * 全选
     */
    checkAll : function() {
        var list = hl.biz.diskAll.testData;
        if(list != null && list.length > 0) {
            for(var i = 0; i < list.length; i++) {
                var item = list[i];
                // icon状态的改变
                hl.sys.$(item.id + "checkDA").status = "on";
                hl.sys.$(item.id + "checkDA").style.backgroundPosition = "0px 0px";
            }
        }
    },
    /**
     * 全不选
     */
    checkNo : function() {
        var list = hl.biz.diskAll.testData;
        if(list != null && list.length > 0) {
            for(var i = 0; i < list.length; i++) {
                var item = list[i];
                // icon状态的改变
                hl.sys.$(item.id + "checkDA").status = "off";
                hl.sys.$(item.id + "checkDA").style.backgroundPosition = "0px -46px";
            }
        }
    },
    /**
     * 新建文件夹编辑页面
     */
    newFlolderDiag : function(p) {

        var infMain = hl.sys.hl$("pop");

        var size = hl.sys.winSize();

        var divMask = ssdjs.dom.div(0, 0, size.x, size.y);
        divMask.id = "hlMask";
        divMask.style.backgroundColor = "black";
        divMask.css("opacity", 0.53);
        infMain.appendChild(divMask);

        var w = 554;
        var iw = 462;

        if(w >= size.x) {
            w = size.x - 20;
            iw = size.x - 60
        }
        var h = 280;

        var divW = ssdjs.dom.div((size.x - w) / 2, (size.y - h) / 2, w, h);
        divW.id = "newFDiag";
        //  divW.style.backgroundImage = hl.url.cssimg("login/alertBg.png");
        //divW.style.borderRadius = "6px";
        var bg = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("login/alertBg.png"));
        //divW.style.backgroundImage = hl.url.cssimg("login/alertBg.png");
        divW.appendChild(bg);

        var title = ssdjs.dom.text(0, 35, w, null, "新建文件夹");
        title.style.textAlign = "center";
        title.style.color = "#2c2c2c";
        title.style.fontSize = "30px";
        divW.appendChild(title);

        var inputW = ssdjs.dom.div(35, 110, w - 70, 48);
        inputW.style.backgroundColor = "#fff";
        inputW.style.borderRadius = "5px";
        inputW.style.border = "1px solid #b3b3b3";

        var input = ssdjs.dom.input(0, 2, w - 70, 44);
        input.id = "newFInput";
        input.style.border = "0px";
        input.style.backgroundColor = "transparent";
        input.style.fontSize = "22px";
        inputW.appendChild(input);

        divW.appendChild(inputW);

        var btnOk = ssdjs.dom.div(36, 190, (w - 120) / 2, 51);
        var bg0 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("disk/yes0.png"));
        var bg1 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("disk/yes1.png"));
        bg1.style.display = "none";
        btnOk.appendChild(bg0);
        btnOk.appendChild(bg1);
        // btnOk.style.backgroundImage = hl.url.cssimg("disk/yes0.png");
        btnOk.evtEnd(function(obj) {
            //obj.style.backgroundImage = hl.url.cssimg("disk/yes0.png");
            bg1.style.display = "none";
            hl.biz.diskAll.newFlolderReq({
                name : $("#newFInput").val(),
                parentFolder : p.fid,
                title : p.title
            });
        });
        btnOk.evtMove(function(obj) {
            bg1.style.display = "none";
            //  obj.style.backgroundImage = hl.url.cssimg("disk/yes0.png");
        });
        btnOk.evtStart(function(obj) {
            bg1.style.display = "";
            // obj.style.backgroundImage = hl.url.cssimg("disk/yes1.png");
        });
        var text = ssdjs.dom.text(0, 12, (w - 120) / 2, null, "确定");
        text.style.color = "#fff";
        text.style.fontSize = "24px";
        text.style.textAlign = "center";
        btnOk.appendChild(text);
        divW.appendChild(btnOk);

        var btnCancel = ssdjs.dom.div(36 + (w - 120) / 2 + 50, 190, (w - 120) / 2, 52);
        var bg10 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("disk/cancel0.png"));
        var bg11 = ssdjs.dom.img(0, 0, "100%", "100%", hl.url.img("disk/cancel1.png"));
        bg11.style.display = "none";
        btnCancel.appendChild(bg10);
        btnCancel.appendChild(bg11);
        //  btnCancel.style.backgroundImage = hl.url.cssimg("disk/cancel0.png");
        btnCancel.evtEnd(function(obj) {
            bg11.style.display = "none";
            // obj.style.backgroundImage = hl.url.cssimg("disk/cancel0.png");
            $("#newFDiag").remove();
            $("#hlMask").remove();

        });
        btnCancel.evtMove(function(obj) {
            bg11.style.display = "none";
            // obj.style.backgroundImage = hl.url.cssimg("disk/cancel0.png");
        });
        btnCancel.evtStart(function(obj) {
            bg11.style.display = "";
            //   obj.style.backgroundImage = hl.url.cssimg("disk/cancel1.png");
        });
        var text = ssdjs.dom.text(0, 12, (w - 120) / 2, null, "取消");
        text.style.color = "#000";
        text.style.fontSize = "24px";
        text.style.textAlign = "center";
        btnCancel.appendChild(text);
        divW.appendChild(btnCancel);

        infMain.appendChild(divW)

    },
    /**
     * 新建文件夹请求
     */
    newFlolderReq : function(p) {
        p = (p || {});
        console.log("newFlolderReq请求参数:" + JSON.stringify(p));
        var loadstat = hl.diag.loading.show();

        // URL
        var url = hl.url.api("NetworkDisk/NewFolderInfo");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;
        if(p.name != null) {
            data += "&folderName=" + p.name;
        }
        if(p.parentFolder != null) {
            data += "&folderId=" + p.parentFolder;
        }

        var success = function(result) {
            hl.diag.loginLoading.hide(loadstat);

            hl.biz.diskAll.diskAll({
                fid : p.parentFolder,
                title : p.title,
                back : 1
            });
            if(result.code == 0) {
                hl.diag.alert({
                    m : "创建失败"
                });
            }
        };
        var error = function(result) {

            console.log(result);
            hl.diag.alert({
                m : "创建失败"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    back : function() {

        hl.data.disk.backList.pop();
        var id = hl.data.disk.backList.last();

        if(id == -1) {
            hl.biz.disk.index();
            return;
        }
        var item = hl.data.disk.backList.last();
        if(item == null) {
            hl.biz.disk.index();
            return;
        }

        hl.biz.diskAll.diskAll({
            fid : item.fid,
            title : item.title,
            back : 1
        });

    },
    refresh : function(p) {
        // p = (p || {});
        // console.log("diskAllP:" + JSON.stringify(p));
        // var loadstat = hl.diag.reloading.show();
        // // URL
        // var url = hl.url.api("NetworkDisk/FileListInfo");
        //
        // var user = hl.data.user.get();
        // var data = "userID=" + user.id;
        // var item = hl.data.disk.backList.last() ;
        // if(item != -1) {
        // data += "&folderId=" + item.fid;
        // }
        // var success = function(result) {
        // hl.diag.reloading.hide(loadstat);
        //
        //
        //
        // };
        // var error = function(result) {
        // console.log("失败");
        // hl.diag.alert({
        // m : "网路错误"
        // });
        // return;
        // };
        // hl.req(url, success, error, data);
        var item = hl.data.disk.backList.last();
        if(item == -1) {
            hl.biz.diskAll.diskAll({
                back : 1
            });
        } else {
            var pp = {
                fid : item.fid,
                back : 1
            };
            hl.biz.diskAll.diskAll(pp);
        }

    },
    diskAll : function(p) {
        p = (p || {});
        console.log("diskAllP:" + JSON.stringify(p));
        var loadstat = hl.diag.loading.show();
        // URL
        var url = hl.url.api("NetworkDisk/FileListInfo");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;
        if(p.fid != null && p.fid != 0) {
            data += "&folderId=" + p.fid;
        }
        var success = function(result) {
            hl.diag.loading.hide(loadstat);
            hl.diag.menu.hide();

            var titleMain = "我的网盘";
            if(p.title != null) {
                titleMain = p.title;
            }

            hl.tpl.diag.popStyle02({
                id : "da", // diskall
                title : titleMain,
                l : hl.biz.diskAll.back
            });

            if(p.fid != null && p.back == null) {
                hl.data.disk.backList.push({
                    fid : p.fid,
                    title : p.title
                });
            }

            var main = $("#daMain");
            var _h = $("#daMain").height();
            var _w = $("#daMain").width();

            var contentBg = ssdjs.dom.img(0, 0, _w, _h, hl.url.img("bg_1.jpg"));
            main.append(contentBg);

            var hscrollA = hl.tpl.scroll.v.create("hscrollDA", 0, 0, _w, _h, null, "refresh");
            main.append(hscrollA);

            // 滑动区域scroll定义
            scrollA = hl.sys.scroll({
                id : "hscrollDA",
                snap : false,
                vScrollbar : true,
                hScroll : false,
                momentum : true
            });
            hl.biz.diskAll.testData = result.list;
            if(hl.biz.diskAll.testData != null && hl.biz.diskAll.testData.length > 0) {

                for(var i = 0; i < hl.biz.diskAll.testData.length; i++) {
                    var temItem = hl.biz.diskAll.testData[i];
                    hl.tpl.scroll.v.append("hscrollDA", hl.biz.diskAll.item({
                        item : temItem
                    }));
                }
            }
            scrollA.refresh();

            var size = hl.sys.winSize();
            var bottom = ssdjs.dom.div(0, size.y - 94 - 97, size.x - 4, 98);
            bottom.style.backgroundColor = "#202020";
            bottom.style.border = "2px outset #474747";

            var _x = 0;
            var btnDel = hl.tpl.btnBottom("share", _x, 0, size.x / 4, 75, "menu/share.png", "共享", 0, function(obj) {
                // 开启多选
                hl.biz.diskAll.checkOn();

                // 底部菜单
                var size = hl.sys.winSize();
                var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 97);
                // TODO 背景要修改成图片的
                bottom.style.backgroundColor = "#202020";
                bottom.style.border = "2px outset #474747";
                bottom.id = "bottomEdit";

                var _x = 0;
                var btnDelete = hl.tpl.btnMenu("delete", _x, 0, size.x, 75, "", "选择好友共享", 0, function(obj) {
                    var list = hl.biz.diskAll.testData;
                    var ids = "";
                    for(var i = 0; i < list.length; i++) {
                        var item = list[i];
                        if(hl.sys.$(item.id + "checkDA").status == "on") {
                            console.log(hl.sys.$(item.id + "checkDA").tag.id);
                            ids += hl.sys.$(item.id + "checkDA").tag.id + ",";
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
                    hl.data.disk.share.backFunction = hl.biz.diskAll.diskAll;
                    hl.data.disk.share.backFunctionP = p;
                    hl.biz.disk.choseFirend();
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

                var titleMain = "我的网盘";
                if(p.title != null) {
                    titleMain = p.title;
                }

                var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
                titleText.style.color = "#FFF";
                titleText.style.textAlign = "center";

                titleText.style.fontSize = "40px";
                title.appendChild(titleText);

                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    hl.biz.diskAll.checkOff();
                    var pp = p;
                    pp.back = 1;
                    hl.biz.diskAll.diskAll(pp);

                });
                title.appendChild(btnBack);

                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全选", function(obj) {

                    // 顶部操作栏的修改
                    $("#title").remove();
                    var title = ssdjs.dom.div(0, 0, size.x, 94);
                    title.id = "title";
                    var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
                    title.appendChild(titleBg);

                    var titleMain = "我的网盘";
                    if(p.title != null) {
                        titleMain = p.title;
                    }
                    var titleText = ssdjs.dom.text(0, 24, size.x, 50, titleMain);
                    titleText.style.color = "#FFF";
                    titleText.style.textAlign = "center";

                    titleText.style.fontSize = "40px";
                    title.appendChild(titleText);

                    var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {

                        hl.biz.diskAll.checkOff();

                        var pp = p;
                        pp.back = 1;
                        hl.biz.diskAll.diskAll(pp);

                    });
                    title.appendChild(btnBack);

                    var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "全不选", function(obj) {
                        hl.biz.diskAll.checkNo();
                        hl.biz.diskAll.chose(p);
                    });
                    title.appendChild(btnEdit);
                    hl.diag.pop.add(title);
                    hl.biz.diskAll.checkAll({
                        fid : p.parentFolder,
                        title : p.title
                    });
                });
                title.appendChild(btnEdit);

                hl.diag.pop.add(title);
            });
            bottom.appendChild(btnDel);
            _x += size.x / 4;
            var btnUp = hl.tpl.btnBottom("select", _x, 0, size.x / 4, 75, "menu/select.png", "删除", 0, function(obj) {
                var fid = 0;
                if(p.fid != null) {
                    fid = p.fid;
                }
                hl.biz.diskAll.chose(p);
            });
            bottom.appendChild(btnUp);
            _x += size.x / 4;
            var btnDown = hl.tpl.btnBottom("up", _x, 0, size.x / 4, 75, "menu/up.png", "上传", 0, function(obj) {
                hl.biz.diskAll.upLoadMenu();
            });
            bottom.appendChild(btnDown);
            _x += size.x / 4;
            var btnShare = hl.tpl.btnBottom("deal", _x, 0, size.x / 4, 75, "menu/deal.png", "管理", 0, function(obj) {
                // TODO 弹出菜单
                if(hl.tpl.menu.subMenuUp.status == 0) {
                    // TODO 传入本级文件夹信息
                    var fid = 0;
                    if(p.fid != null) {
                        fid = p.fid;
                    }
                    hl.tpl.menu.subMenuUp.show({
                        fid : fid,
                        title : p.title
                    });
                } else {
                    hl.tpl.menu.subMenuUp.hide();
                }

            });
            bottom.appendChild(btnShare);

            main.append(bottom);

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
        console.log(p);
        var item = p.item;
        var size = hl.sys.winSize();
        var _w = size.x;
        var _h = 87;
        var divW = ssdjs.dom.div(null, null, _w, _h);

        var bgLine = ssdjs.dom.img(0, _h - 2, _w, 2, hl.url.img("line.jpg"));
        divW.appendChild(bgLine);

        var check = hl.tpl.btn.check3(item.id + "checkDA", 15, 24, "off", item, null);
        check.style.display = "none";
        divW.appendChild(check);

        var divMoveW = ssdjs.dom.div(0, 0, _w, _h);
        divMoveW.id = item.id + "divMoveWDA";

        var headImg = "disk/folder.png";
        var nameValue = item.file_name;
        if(item.file_type == 0) {
            // 文件
            var type = "def";
            type = nameValue.substr(nameValue.indexOf(".") + 1, nameValue.length);
            var head = hl.tpl.getFileIcon({
                x : 10,
                y : 7,
                name : type
            });

        }
        if(item.file_type == 1) {
            headImg = "disk/folder.png";
            var head = ssdjs.dom.img(10, 18, 94, 60, hl.url.img(headImg));
        }
        if(item.file_type == 2) {
            headImg = "disk/folderO.png";
            var head = ssdjs.dom.img(10, 18, 94, 60, hl.url.img(headImg));
        }
        if(item.file_type == 3) {
            headImg = "disk/folderI.png";
            var head = ssdjs.dom.img(10, 18, 94, 60, hl.url.img(headImg));
        }

        divMoveW.appendChild(head);

        var name = ssdjs.dom.text(115, 31, null, null, nameValue);
        name.style.fontSize = "30px";
        divMoveW.appendChild(name);

        if(item.file_type == 1 || item.file_type == 2 || item.file_type == 3) {
            // 文件夹
            divMoveW.evtEnd(function(obj) {
            	console.log("item-------" + item.id) ;
                hl.biz.diskAll.diskAll({
                    fid : item.id,
                    title : item.file_name
                });
            });
        } else if(item.file_type == 0) {
            // 文件
            // hl.biz.diskAll.fileDetail();
        }
        divW.appendChild(divMoveW);

        return divW;
    },
    testData : {
        list : [{
            "id" : 133,
            "file_type" : 1,
            "file_name" : "1",
            "file_size" : 0,
            "updated_at" : "2013/10/12 10:37:14",
            "path" : "/Files/Nfiles/f0/49/77/8d/f049778db88b1906532810b52971a236f29ffb40"
        }, {
            "id" : 152,
            "file_type" : 1,
            "file_name" : "教学计划",
            "file_size" : 0,
            "updated_at" : "2013/10/28 22:09:01",
            "path" : "/Files/Nfiles/f0/49/77/8d/f049778db88b1906532810b52971a236f29ffb40"
        }, {
            "id" : 154,
            "file_type" : 1,
            "file_name" : "项目管理",
            "file_size" : 0,
            "updated_at" : "2013/10/28 22:09:24",
            "path" : "/Files/Nfiles/f0/49/77/8d/f049778db88b1906532810b52971a236f29ffb40"
        }, {
            "id" : 130,
            "file_type" : 0,
            "file_name" : "新浪微博.png",
            "file_size" : 213976,
            "updated_at" : "2013/10/9 10:36:03",
            "path" : "/Files/Nfiles/ba/ec/a2/e6/baeca2e6117e2820f7331864c5bd7b06f48e15bf"
        }, {
            "id" : 131,
            "file_type" : 0,
            "file_name" : "office图标.jpg",
            "file_size" : 101570,
            "updated_at" : "2013/10/9 15:16:52",
            "path" : "/Files/Nfiles/e9/a5/9d/cd/e9a59dcd6026de69f60faef09b6e21e22176320d"
        }, {
            "id" : 132,
            "file_type" : 0,
            "file_name" : "utsing.png",
            "file_size" : 3966,
            "updated_at" : "2013/10/9 17:55:18",
            "path" : "/Files/Nfiles/d6/7f/db/a0/d67fdba091ebfdfa0f1cdb013a320b099b828a6d"
        }, {
            "id" : 137,
            "file_type" : 0,
            "file_name" : "Visio.ico",
            "file_size" : 91782,
            "updated_at" : "2013/10/12 11:33:27",
            "path" : "/Files/Nfiles/c4/eb/15/9d/c4eb159d6dfb00ce7a07874edc65c56afef850a2"
        }, {
            "id" : 138,
            "file_type" : 0,
            "file_name" : "PowerPoint.ico",
            "file_size" : 91782,
            "updated_at" : "2013/10/12 11:33:40",
            "path" : "/Files/Nfiles/dd/f4/21/21/ddf421217b83d14501e1b31f2496a6297eda1622"
        }, {
            "id" : 149,
            "file_type" : 0,
            "file_name" : "图片1.png",
            "file_size" : 1393905,
            "updated_at" : "2013/10/28 22:08:35",
            "path" : "/Files/Nfiles/9d/5a/44/c4/9d5a44c42199fea20c9bd6196efb51ea76bd3eae"
        }, {
            "id" : 150,
            "file_type" : 0,
            "file_name" : "微软产品报价-教育.xlsx",
            "file_size" : 71451,
            "updated_at" : "2013/10/28 22:08:36",
            "path" : "/Files/Nfiles/41/fa/57/05/41fa5705d590ceeb2dceef33a4bbcb4c18988a6b"
        }, {
            "id" : 155,
            "file_type" : 0,
            "file_name" : "任务.docx",
            "file_size" : 12404,
            "updated_at" : "2013/10/29 10:04:51",
            "path" : "/Files/Nfiles/c6/2a/29/36/c62a2936de09cdcba5f29147f04614a8c2cb5cd0"
        }, {
            "id" : 161,
            "file_type" : 0,
            "file_name" : "Video1.wmv",
            "file_size" : 1222651,
            "updated_at" : "2013/10/30 15:24:52",
            "path" : "/Files/Nfiles/16/1c/0f/8b/161c0f8b84b696f1321a08b45a62ce4820a14ad2"
        }, {
            "id" : 162,
            "file_type" : 0,
            "file_name" : "Video1.wmv",
            "file_size" : 1222651,
            "updated_at" : "2013/10/31 10:08:37",
            "path" : "/Files/Nfiles/16/1c/0f/8b/161c0f8b84b696f1321a08b45a62ce4820a14ad2"
        }, {
            "id" : 163,
            "file_type" : 0,
            "file_name" : "珍爱生命__牢记安全.doc",
            "file_size" : 36352,
            "updated_at" : "2013/11/1 14:31:46",
            "path" : "/Files/Nfiles/cb/10/89/bf/cb1089bf8bc521bc513cc0ccf63428611a39a605"
        }, {
            "id" : 165,
            "file_type" : 0,
            "file_name" : "五年级暑期家访工作计划.doc",
            "file_size" : 22528,
            "updated_at" : "2013/11/1 16:16:35",
            "path" : "/Files/Nfiles/9e/fb/39/8e/9efb398ea81d48273e28bdd42f680af337df8192"
        }, {
            "id" : 166,
            "file_type" : 0,
            "file_name" : "8年级2班学生成绩表.xls",
            "file_size" : 119296,
            "updated_at" : "2013/11/1 16:22:13",
            "path" : "/Files/Nfiles/48/31/e8/b8/4831e8b8282d1da63d3cb33aca396fec752b12e9"
        }, {
            "id" : 168,
            "file_type" : 0,
            "file_name" : "带有马字的成语.ppt",
            "file_size" : 31746,
            "updated_at" : "2013/11/1 16:22:14",
            "path" : "/Files/Nfiles/7c/0d/e4/f2/7c0de4f297ed0d5a2270ec25afd4aa2e8e5e90b1"
        }, {
            "id" : 170,
            "file_type" : 0,
            "file_name" : "四年级晨诵第九周.ppt",
            "file_size" : 98816,
            "updated_at" : "2013/11/1 16:22:15",
            "path" : "/Files/Nfiles/85/35/d4/7a/8535d47a0a9e15183f5f16189d89c4611b887434"
        }, {
            "id" : 174,
            "file_type" : 0,
            "file_name" : "minus.gif",
            "file_size" : 86,
            "updated_at" : "2013/11/4 15:51:00",
            "path" : "/Files/Nfiles/02/8f/04/ef/028f04efed2d8b481228f58689064f4c0b693914"
        }, {
            "id" : 175,
            "file_type" : 0,
            "file_name" : "Video112.wmv",
            "file_size" : 305459,
            "updated_at" : "2013/11/5 13:45:03",
            "path" : "/Files/Nfiles/91/50/c6/1b/9150c61bff78f2909bcf705c64885063c62bc778"
        }, {
            "id" : 182,
            "file_type" : 0,
            "file_name" : "sence.png",
            "file_size" : 0,
            "updated_at" : "2013/11/26 15:58:41",
            "path" : "/Files/Nfiles/de/5t/ij/o1/de5tijo12altmozgiwapjzayedd6p0r0gegpx0zv"
        }, {
            "id" : 183,
            "file_type" : 0,
            "file_name" : "??.png",
            "file_size" : 0,
            "updated_at" : "2013/11/26 16:21:12",
            "path" : "/Files/Nfiles/bo/jb/sl/oz/bojbsloz34d41u1unggjtdoyg93tvt7u5xfax203"
        }, {
            "id" : 184,
            "file_type" : 0,
            "file_name" : "20131121171530.jpg",
            "file_size" : 0,
            "updated_at" : "2013/11/26 16:21:40",
            "path" : "/Files/Nfiles/fl/4x/jd/gm/fl4xjdgmjd9zq86b8d28glm4c1cr8eetekv6x6u7"
        }, {
            "id" : 185,
            "file_type" : 0,
            "file_name" : "sence.png",
            "file_size" : 0,
            "updated_at" : "2013/11/26 16:21:41",
            "path" : "/Files/Nfiles/tf/py/4i/la/tfpy4ila0di4tfqkr42n2gar5xd5w5toqldfmbrv"
        }, {
            "id" : 187,
            "file_type" : 0,
            "file_name" : "QQ20131023181033.png",
            "file_size" : 0,
            "updated_at" : "2013/11/27 18:06:26",
            "path" : "/Files/Nfiles/vs/x1/kl/7k/vsx1kl7knkfovi2ofc6fuaht2djqwes2p3kf25ku"
        }, {
            "id" : 188,
            "file_type" : 0,
            "file_name" : "?? - ????.mp3",
            "file_size" : 0,
            "updated_at" : "2013/11/29 10:19:06",
            "path" : "/Files/Nfiles/0k/kh/hh/ao/0kkhhhaoe3ozzsfi8fke3yes98d8ja0nbuo3oukc"
        }, {
            "id" : 193,
            "file_type" : 0,
            "file_name" : "ZK.IMServer20131126.zip",
            "file_size" : 0,
            "updated_at" : "2013/11/29 10:23:45",
            "path" : "/Files/Nfiles/fz/tm/l9/da/fztml9dauww69kaxvvxoeovkr8mfvywqi2srsu5q"
        }]
    }
}