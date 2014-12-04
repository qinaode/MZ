/**
 *
 */
var hl = (hl || {});
hl.tpl = {
    info : {
        open : function(p) {
            var size = hl.sys.winSize();
            var w = size.x - 20;
            var h = 100;
            var divW = ssdjs.dom.div((size.x - w) / 2, (size.y - 100) / 2, w, h);
            divW.style.backgroundColor = "black";
            divW.css("opacity", 0.53);
            divW.id = "infos";
            hl.diag.pop.add(divW);
            var text = ssdjs.dom.text(0, 20, w, 50, "");
            text.id = "infotext";
            text.style.color = "#fff";
            text.style.textAlign = "center";
            text.style.fontSize = "24px";
            divW.appendChild(text);
        },
        refresh : function(p) {
            $("#infotext").html(p);
        },
        close : function(p) {
            setTimeout(function() {
                $("#infos").hide();
                $("#infos").remove();
            }, 2000);
        }
    },

    /**
     * p : {x:0,y:0,name:"txt"}
     */
    getFileIcon : function(p) {
        console.log(p);
        var divW = ssdjs.dom.div(p.x, p.y, 72, 72);
        var type = "def";

        for(var i = 0; i < hl.file.typeList.length; i++) {
            if(hl.file.typeList[i] == p.name) {
                type = p.name;
                break;
            }
        }

        for(var i = 0; i < hl.file.videoList.length; i++) {
            if(hl.file.videoList[i] == p.name) {
                type = "video";
                break;
            }
        }
        divW.style.backgroundImage = hl.url.cssimg("file/" + type + ".png");
        console.log("file/" + type + ".png");
        if(type == "video") {
            var text = ssdjs.dom.text(0, 9, null, null, p.name);
            text.style.fontSize = "18px";
            text.style.color = "#fff";
            divW.appendChild(text);
        }

        return divW;
    },
    menu : {
        subMenuUp : {
            status : 0,
            show : function(p) {
                hl.tpl.menu.subMenuUp.status = 1;
                var size = hl.sys.winSize();
                var w = 205;
                var h = 100;
                var x = size.x - w;
                var divW = ssdjs.dom.div(x, size.y, w, h);
                divW.id = "diskSubMenuUp";
                divW.style.backgroundColor = "#5f5f5f";

                var down = ssdjs.dom.div(0, 0, w, h / 2);
                var downIcon = ssdjs.dom.img(20, 13, 25, 25, hl.url.img("menu/down.png"));
                down.appendChild(downIcon);
                var downText = ssdjs.dom.text(80, 13, null, null, "下载");
                downText.style.color = "#ffffff";
                downText.style.fontSize = "20px";
                down.appendChild(downText);
                divW.appendChild(down);

                var newFile = ssdjs.dom.div(0, h / 2, w, h / 2);
                newFile.evtEnd(function(obj) {
                    hl.biz.diskAll.newFlolderDiag({
                        fid : p.fid,
                        title : p.title
                    });
                });
                var newIcon = ssdjs.dom.img(20, 13, 25, 25, hl.url.img("menu/down.png"));
                down.appendChild(newIcon);
                var newText = ssdjs.dom.text(80, 13, null, null, "新建文件夹");
                newText.style.color = "#ffffff";
                newText.style.fontSize = "20px";
                newFile.appendChild(newText);
                divW.appendChild(newFile);

                var folderIcon = ssdjs.dom.img(20, 63, 25, 25, hl.url.img("menu/folder.png"));
                divW.appendChild(folderIcon);

                $("#pop").append(divW);
                $("#diskSubMenuUp").animate({
                    'top' : (size.y - 195 ) + 'px'
                }, 200);
            },
            hide : function() {
                hl.tpl.menu.subMenuUp.status = 0;
                var size = hl.sys.winSize();
                $("#diskSubMenuUp").animate({
                    'top' : (size.y ) + 'px'
                }, 200);
            }
        }
    },
    loading : {
        show : function() {
            var size = hl.sys.winSize();
            var x = size.x;
            var y = size.y;
            var w = 245;
            var h = 177;
            var divW = ssdjs.dom.div(0, 0, size.x, size.y);
            divW.id = "loginLoad";
            divW.style.backgroundColor = "black";
            divW.css("opacity", 0.53);

            var divLoading = ssdjs.dom.div((x - w) / 2, (y - h) / 2, w, y);
            divLoading.style.backgroundImage = hl.url.cssimg("login/loginBg.png");
            divW.appendChild(divLoading);

            var text = ssdjs.dom.text(0, 115, w, null, "等待登录，请稍后。。。");
            text.style.color = "#ffffff";
            text.style.fontSize = "20px";
            divW.appendChild(text);

        },
        hide : function() {

        }
    },
    format : {
        kbSize : function(size) {
            var kb = "";
            var mb = "";
            var gb = "";

            if(size / 1048576 > 0) {
                gb = parseInt(size / 1048576) + "GB";
                size = size % 1048576;
            }

            if(size / 1024 > 0) {
                mb = parseInt(size / 1024) + "MB";
                size = size % 1024;
            }
            if(size > 0) {
                kb = size + "KB";
            }
            return gb + mb + kb;
        }
    },
    btn : {
        check : function(id, x, y, status, fn) {

            var divW = ssdjs.dom.div(x, y, 45, 45);
            divW.style.backgroundImage = hl.url.cssimg("check.png");
            divW.style.backgroundRepeat = "repeat-Y";
            divW.style.backgroundPosition = "0px -46px";
            //  divW.style.backgroundSize = "45px 45px";
            divW.id = id;
            divW.status = status;

            divW.evtEnd(function(obj) {
                if(obj.disabled) {
                    return;
                }
                if(fn != null) {
                    fn(obj);
                }
                if(divW.status == "on") {
                    divW.status = "off";
                    obj.style.backgroundPosition = "0px -46px";
                    // TODO 从已选列表中移除

                } else {
                    divW.status = "on";
                    obj.style.backgroundPosition = "0px 0px";
                    // TODO 加入已选列表
                }
            });
            return divW;

        },
        check2 : function(id, x, y, status, tag, fn) {

            var divW = ssdjs.dom.div(x, y, 45, 45);
            divW.style.backgroundImage = hl.url.cssimg("check.png");
            divW.style.backgroundRepeat = "repeat-Y";
            divW.style.backgroundPosition = "0px -46px";
            //  divW.style.backgroundSize = "45px 45px";
            divW.id = id;
            divW.status = status;
            divW.tag = tag;

            divW.evtEnd(function(obj) {
                if(obj.disabled) {
                    return;
                }
                if(divW.status == "on") {
                    divW.status = "off";
                    obj.style.backgroundPosition = "0px -46px";
                    // TODO 从已选列表中移除
                    hl.data.disk.choseList.remove({
                        item : {
                            id : obj.tag.USERID,
                            head : obj.tag.FACEFILE
                        }
                    });

                } else {
                    divW.status = "on";
                    obj.style.backgroundPosition = "0px 0px";
                    // TODO 加入已选列表
                    hl.data.disk.choseList.add({
                        item : {
                            id : obj.tag.USERID,
                            head : obj.tag.FACEFILE
                        }
                    });
                }

                if(fn != null) {
                    fn(obj);
                }
            });
            return divW;

        },
        check3 : function(id, x, y, status, tag, fn) {

            var divW = ssdjs.dom.div(x, y, 45, 45);
            divW.style.backgroundImage = hl.url.cssimg("check.png");
            divW.style.backgroundRepeat = "repeat-Y";
            divW.style.backgroundPosition = "0px -46px";
            //  divW.style.backgroundSize = "45px 45px";
            divW.id = id;
            divW.status = status;
            divW.tag = tag;

            divW.evtEnd(function(obj) {
                if(obj.disabled) {
                    return;
                }
                if(divW.status == "on") {
                    divW.status = "off";
                    obj.style.backgroundPosition = "0px -46px";

                } else {
                    divW.status = "on";
                    obj.style.backgroundPosition = "0px 0px";

                }

                if(fn != null) {
                    fn(obj);
                }
            });
            return divW;

        },
        b1 : function(text, x, y, fn) {
            var btn = ssdjs.dom.div(x, y, 132, 58);
            btn.style.backgroundColor = "green";
            btn.style.backgroundPosition = "0px 0px";
            btn.evtEnd(function(obj) {
                if(obj.disabled) {
                    return;
                }
                if(fn != null) {
                    fn(obj);
                }
                btn.style.backgroundColor = "green";
                obj.style.backgroundPosition = "0px 0px";
                obj.style.marginTop = "0px";
            });
            btn.evtMove(function(obj) {
                if(obj.disabled) {
                    return;
                }
                //  btn.style.backgroundColor = "red";
                obj.style.backgroundPosition = "0px 0px";
                obj.style.marginTop = "0px";
            });
            btn.evtStart(function(obj) {

                if(obj.disabled) {
                    return;
                }
                btn.style.backgroundColor = "red";
                obj.style.backgroundPosition = "0px -58px";
                obj.style.marginTop = "2px";
            });
            // icon + text
            var wrap = ssdjs.dom.div(-2, 12, 132, 58 - 12);
            wrap.style.textAlign = "center";
            var inner = ssdjs.dom.div();
            inner.className = "inner_box clearfix";

            // text
            if(text != null) {
                var txt = ssdjs.dom.text();
                txt.innerHTML = text;
                txt.className = "sLte";
                txt.style.cssFloat = "left";
                inner.appendChild(txt);
            }
            wrap.appendChild(inner);
            btn.appendChild(wrap);
            return btn;
        },
    },
    inputUser : function(id, x, y, width, maxlength, type) {
        var wrap = ssdjs.dom.div(x, y, width, 93);

        var bgl = ssdjs.dom.div(0, 0, 100, 93);
        bgl.style.backgroundImage = hl.url.cssimg("user.png");
        bgl.style.backgroundPosition = "0px 0px";
        wrap.appendChild(bgl);

        var bgm = ssdjs.dom.div(100, 0, width - 150, 93);
        bgm.style.backgroundImage = hl.url.cssimg("user.png");
        bgm.style.backgroundRepeat = "repeat-x";
        bgm.style.backgroundPosition = "-100px 0px";
        bgm.style.backgroundSize = (width) + "px 93px";
        wrap.appendChild(bgm);

        var bgr = ssdjs.dom.div(width - 50, 0, 50, 93);
        bgr.style.backgroundImage = hl.url.cssimg("user.png");
        bgr.style.backgroundPosition = "-520px 0px";
        wrap.appendChild(bgr);

        var text = ssdjs.dom.text(65, 35, null, null, "用户名");
        text.style.color = "#9098a3";
        text.style.fontSize = "26px";
        //  wrap.appendChild(text);

        var input = ssdjs.dom.input(60, 3, (width - 80), 90);
        input.id = id;
        if(maxlength != null && maxlength > 0) {
            input.maxLength = maxlength;
        }
        if(type != null) {
            input.type = type;
        }

        input.style.border = "0px";
        input.style.backgroundColor = "transparent";
        input.style.paddingLeft = "10px";
        input.style.paddingRight = "10px";
        input.style.fontSize = "22px";
        wrap.appendChild(input);

        return wrap;
    },
    inputPassWord : function(id, x, y, width, maxlength, type) {
        var wrap = ssdjs.dom.div(x, y, width, 93);

        var bgl = ssdjs.dom.div(0, 0, 100, 93);
        bgl.style.backgroundImage = hl.url.cssimg("password.png");
        bgl.style.backgroundPosition = "0px 0px";
        wrap.appendChild(bgl);

        var bgm = ssdjs.dom.div(100, 0, width - 150, 93);
        bgm.style.backgroundImage = hl.url.cssimg("password.png");
        bgm.style.backgroundRepeat = "repeat-x";
        bgm.style.backgroundPosition = "-100px 0px";
        bgm.style.backgroundSize = (width) + "px 93px";
        wrap.appendChild(bgm);

        var bgr = ssdjs.dom.div(width - 50, 0, 50, 93);
        bgr.style.backgroundImage = hl.url.cssimg("password.png");
        bgr.style.backgroundPosition = "-520px 0px";
        wrap.appendChild(bgr);

        var text = ssdjs.dom.text(65, 33, null, null, "密&nbsp&nbsp码");
        text.style.color = "#9098a3";
        text.style.fontSize = "26px";
        // wrap.appendChild(text);

        var input = ssdjs.dom.input(60, 1, (width - 80), 90);
        input.id = id;
        if(maxlength != null && maxlength > 0) {
            input.maxLength = maxlength;
        }
        if(type != null) {
            input.type = type;
        }

        input.style.border = "0px";
        input.style.backgroundColor = "transparent";
        input.style.paddingLeft = "10px";
        input.style.paddingRight = "10px";
        input.style.fontSize = "22px";
        wrap.appendChild(input);

        return wrap;
    },
    btnLogin : function(x, y, fn) {
        var size = hl.sys.winSize();

        var _w = 573;
        if(size.x < 640) {
            _w = size.x * 57 / 64;
            x = (size.x - _w) / 2;
        }

        var btn = ssdjs.dom.div(x, y, _w, 70);
        var bg0 = ssdjs.dom.img(0, 0, _w, 70, hl.url.img("login/login1.png"));
        btn.appendChild(bg0);
        var bg1 = ssdjs.dom.img(0, 0, _w, 70, hl.url.img("login/login2.png"));
        bg1.style.display = "none";
        btn.appendChild(bg1);

        var text = ssdjs.dom.text(0, 18, _w, null, "登录");
        text.style.textAlign = "center";
        text.style.color = "#ffffff";
        text.style.fontSize = "30px";
        btn.appendChild(text);

        btn.evtEnd(function(obj) {
            if(fn != null) {
                fn(obj);
            }
            bg1.style.display = "none";
        });
        btn.evtMove(function(obj) {
            bg1.style.display = "none";
        });
        btn.evtStart(function(obj) {
            bg1.style.display = "";
        });
        return btn;
    },
    btnPerson : function(x, y, w, h, ico, txt, fn) {
        var btn = ssdjs.dom.div(x, y, w, h);
        var bg = ssdjs.dom.img(0, 0, w, h, hl.url.img("bg.jpg"));
        btn.appendChild(bg);
        var bgLine = ssdjs.dom.img(0, h - 2, w, 2, hl.url.img("line.jpg"));
        var bgLine0 = ssdjs.dom.img(0, 0, w, 2, hl.url.img("line.jpg"));
        btn.appendChild(bgLine0);

        var ico = ssdjs.dom.img(20, 20, 58, 58, hl.url.img(ico));
        btn.appendChild(ico);

        var txt = ssdjs.dom.text(100, 40, null, null, txt);
        btn.appendChild(txt);

        var icoR = ssdjs.dom.img(w - 50, 43, 15, 15, hl.url.img("icon4.png"));
        btn.appendChild(icoR);

        btn.appendChild(bgLine);
        btn.evtEnd(function(obj) {
            if(fn != null) {
                fn(obj);

            }

        });
        btn.evtMove(function(obj) {

        });
        btn.evtStart(function(obj) {

        });
        return btn;
    },
    btnTitleL : function(x, y, txt, fn) {
        var btn = ssdjs.dom.div(x, y, 100, 59);
        btn.id = "back";
        btn.style.backgroundImage = hl.url.cssimg("pub/back0.png");

        var txt = ssdjs.dom.text(27, 14, null, null, txt);
        txt.style.color = "#fff";
        txt.style.fontSize = "28px";
        btn.appendChild(txt);

        btn.evtEnd(function(obj) {
            btn.style.backgroundImage = hl.url.cssimg("pub/back0.png");
            if(fn != null) {
                fn(obj);
            }
        });
        btn.evtMove(function(obj) {

        });
        btn.evtStart(function(obj) {
            btn.style.backgroundImage = hl.url.cssimg("pub/back1.png");

        });
        return btn;
    },
    btnTitleR : function(x, y, txt, fn) {
        var btn = ssdjs.dom.div(x, y, 102, 59);
        btn.style.backgroundImage = hl.url.cssimg("pub/edit0.png");

        var txt = ssdjs.dom.text(0, 14, 104, null, txt);
        txt.style.color = "#fff";
        txt.style.fontSize = "28px";
        txt.style.textAlign = "center";
        btn.appendChild(txt);

        btn.evtEnd(function(obj) {
            btn.style.backgroundImage = hl.url.cssimg("pub/edit0.png");

            if(fn != null) {
                fn(obj);
            }
        });
        btn.evtMove(function(obj) {

        });
        btn.evtStart(function(obj) {
            btn.style.backgroundImage = hl.url.cssimg("pub/edit1.png");

        });
        return btn;
    },
    btnBottom : function(id, x, y, w, h, ico, txt, isOn, fn) {
        var btn1 = ssdjs.dom.div(x, y, w - 20, h - 5);
        btn1.id = id + "btnB";
        //
        // var wrap = ssdjs.dom.div(0, 0, w-20, h + 10);
        // wrap.id = id + "btnBack";
        // wrap.style.backgroundColor = "#67ca31";
        // wrap.style.borderRadius = "4px";
        var width = w - 20;
        // var bgl = ssdjs.dom.div(0, 0, 20, 92);
        // bgl.style.backgroundImage = hl.url.cssimg("pub/btnBottom.png");
        // bgl.style.backgroundPosition = "0px 0px";
        // wrap.appendChild(bgl);
        // var bgm = ssdjs.dom.div(20, 0, width - 40, 92);
        // bgm.style.backgroundImage = hl.url.cssimg("pub/btnBottom.png");
        // bgm.style.backgroundRepeat = "repeat-x";
        // bgm.style.backgroundPosition = "-20px 0px";
        // bgm.style.backgroundSize = width + "px 92px";
        // wrap.appendChild(bgm);
        // var bgr = ssdjs.dom.div(width - 20, 0, 20, 92);
        // bgr.style.backgroundImage = hl.url.cssimg("pub/btnBottom.png");
        // bgr.style.backgroundPosition = "-123px 0px";
        // wrap.appendChild(bgr);

        txt.className = "bottomTextOff";
        if(isOn) {
            // btn1.appendChild(wrap);
            btn1.className = "bottomOn";

        } else {
            btn1.className = "bottomOff";
            //txt.className = "bottomTextOn";
        }

        var icon = ssdjs.dom.img(null, null, 48, 48, hl.url.img(ico));
        btn1.appendChild(icon);
        var txt = ssdjs.dom.text(0, 55, w - 10, null, txt);
        txt.style.textAlign = "center";
        txt.style.fontSize = "20px";

        var message = ssdjs.dom.div(width / 2, -4, 45, 45);
        message.id = id + "markPub";
        message.style.backgroundImage = hl.url.cssimg("pub/imnb.png");
        var messageNum = ssdjs.dom.text(0, 7, 45, null, "");
        messageNum.style.textAlign = "center";
        messageNum.id = id + "markPNum";

        messageNum.style.color = "#fff";
        messageNum.style.fontSize = "24px";
        message.appendChild(messageNum);
        btn1.appendChild(message);
        message.style.display = "none";

        btn1.appendChild(txt);
        if(id === "person") {
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
                $("#personmarkPNum").html(numNoSee);
                $("#personmarkPub").show();
            }
        }
        if(id === "user") {
            // 聊天
            var numNoSee = 1;
            var pppList = (hl.data.chat.receive.list || []);
            if(pppList.length > 0) {
                for(var i = 0; i < pppList.length; i++) {
                    if(pppList[i].list == null || pppList[i].list == undefined) {
                        continue;
                    }
                    numNoSee += pppList[i].list.length;
                }
            }
            if(numNoSee != 0) {

                $("#usermarkPNum").html(numNoSee);
                $("#usermarkPub").show();
            }
        }

        btn1.evtEnd(function(obj) {
            if(fn != null) {
                fn(obj);
            }
        });
        btn1.evtMove(function(obj) {

        });
        btn1.evtStart(function(obj) {
        });
        return btn1;
    },
    /**
     * 底部菜单按钮
     */
    btnMenu : function(id, x, y, w, h, icon, text, isOn, fn) {
        var divW = ssdjs.dom.div(x, y, w, h);
        divW.id = id + "btnMenu";

        var txt = ssdjs.dom.text(0, (h - 30) / 2, w, null, text);
        txt.style.textAlign = "center";
        txt.className = "cWhite sBig";
        divW.appendChild(txt);

        divW.evtEnd(function(obj) {
            if(fn != null) {
                fn(obj);
            }
        });
        divW.evtMove(function(obj) {

        });
        divW.evtStart(function(obj) {
        });
        return divW;
    },
    personItem : function(p) {
        console.log(p);

    },
    /**
     * 页面滚动
     */
    scroll : {
        /**
         * 分页
         */
        pager : function(id, nextpage, tag) {
            var mask = ssdjs.$(id);
            if(ssdjs.pager.isLast(tag.pager)) {
                mask.nextpage = null;
                mask.tag = null;
            } else {
                if(nextpage != null) {
                    mask.nextpage = nextpage;
                }
                if(tag != null) {
                    if(tag.pager != null) {
                        tag.pager = ssdjs.pager.next(tag.pager);
                    }
                    mask.tag = tag;
                }
            }
        },
        h : {
            /**
             * 生成滑动区域
             * @param {Object} id domId
             * @param {Object} x left值
             * @param {Object} y top值
             * @param {Object} width 宽度
             * @param {Object} height 高度
             * @param {Object} arrow 可选，箭头样式，如：arrow
             */
            create : function(id, x, y, width, height, arrow) {
                // mask
                var mask = ssdjs.dom.div(x, y, width, height);
                mask.id = id;
                mask.ssdw = 0;
                // container
                var container = ssdjs.dom.div();
                container.id = id + "main";
                mask.appendChild(container);

                if(arrow != null) {
                    // arrow
                    var _y = ((height - 33) / 2) || 0;
                    // showLeft
                    var arrL = ssdjs.dom.div(0, _y, 27, 33);
                    arrL.id = id + "L";
                    arrL.style.backgroundImage = hl.url.cssimg("menu/" + arrow + ".png");
                    arrL.css("Animation", "aniArrL 0.8s linear infinite", true);
                    mask.appendChild(arrL);
                    // showRight
                    var arrR = ssdjs.dom.div(width - 30, _y, 27, 33);
                    arrR.id = id + "R";
                    arrR.style.backgroundImage = hl.url.cssimg("menu/" + arrow + ".png");
                    arrR.css("Animation", "aniArrR 0.8s linear infinite", true);
                    mask.appendChild(arrR);
                }
                return mask;
            },
            /**
             * 往滑动区域添加元素-尾部
             * @param {Object} id domId
             * @param {Object} item 元素
             */
            append : function(id, item) {
                var mask = ssdjs.$(id);
                var container = ssdjs.$(id + "main");
                item.style.cssFloat = "left";
                container.appendChild(item);
                mask.ssdw = (mask.ssdw || 0) + $(item).width();
                container.style.width = mask.ssdw + "px";
            },
            /**
             * 往滑动区域添加元素-头部
             * @param {Object} id domId
             * @param {Object} item 元素
             */
            appendL : function(id, item) {
                var mask = ssdjs.$(id);
                var container = ssdjs.$(id + "main");
                item.style.cssFloat = "left";
                container.insertBefore(item, container.firstChild);
                mask.ssdw = (mask.ssdw || 0) + $(item).width();
                container.style.width = mask.ssdw + "px";
            }
        },
        v : {
            /**
             * 生成滑动区域
             * @param {Object} id domId
             * @param {Object} x left值
             * @param {Object} y top值
             * @param {Object} width 宽度
             * @param {Object} height 高度
             * @param {Object} arrow 可选，箭头样式，如：arrow
             */
            create : function(id, x, y, width, height, arrow, refresh) {
                // mask
                var mask = ssdjs.dom.div(x, y, width, height);
                mask.id = id;
                mask.ssdh = 0;

                // container
                var container = ssdjs.dom.div();
                container.id = id + "main";

                mask.appendChild(container);
                if(refresh != null) {
                    var pullDown = ssdjs.dom.div(null, null, width, 80);
                    pullDown.id = "pullDown";
                    // var pullIcon = ssdjs.dom.div();
                    var pullText = ssdjs.dom.text(0, 30, "100%", null, "滑动刷新");
                    pullText.style.textAlign = "center";

                    pullText.id = "pullDownLabel";
                    pullText.className = "pullDownLabel";

                    // pullDown.appendChild(pullIcon);
                    pullDown.appendChild(pullText);
                    container.appendChild(pullDown);
                    pullDown.style.display = "none";
                }

                if(arrow != null) {
                    // arrow
                    var _y = ((height - 33) / 2) || 0;
                    // showLeft
                    var arrL = ssdjs.dom.div(0, _y, 27, 33);
                    arrL.id = id + "L";
                    arrL.style.backgroundImage = hl.url.cssimg("menu/" + arrow + ".png");
                    arrL.css("Animation", "aniArrL 0.8s linear infinite", true);
                    mask.appendChild(arrL);
                    // showRight
                    var arrR = ssdjs.dom.div(603, _y, 27, 33);
                    arrR.id = id + "R";
                    arrR.style.backgroundImage = hl.url.cssimg("menu/" + arrow + ".png");
                    arrR.css("Animation", "aniArrR 0.8s linear infinite", true);
                    mask.appendChild(arrR);
                }
                return mask;
            },
            /**
             * 往滑动区域添加元素-尾部
             * @param {Object} id domId
             * @param {Object} item 元素
             */
            append : function(id, item) {
                var mask = ssdjs.$(id);
                var container = ssdjs.$(id + "main");
                if(container) {
                    container.appendChild(item);
                    mask.ssdh = (mask.ssdh || 0) + $(item).height();
                    container.style.height = mask.ssdh + "px";
                }
            },
            /**
             * 往滑动区域添加元素-头部
             * @param {Object} id domId
             * @param {Object} item 元素
             */
            appendL : function(id, item) {
                var mask = ssdjs.$(id);
                var container = ssdjs.$(id + "main");
                if(container) {
                    container.insertBefore(item, container.firstChild);
                    mask.ssdh = (mask.ssdh || 0) + $(item).height();
                    container.style.height = mask.ssdh + "px";
                }
            }
        }
    },
    diag : {
        wrapage : function(p) {

        },
        top : function(p) {
            var size = hl.sys.winSize();
            var title = ssdjs.dom.div(0, 0, size.x, 94);
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";
            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    p.l();
                });
                title.appendChild(btnBack);
            }

            if(p.r) {
                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "编辑", function(obj) {
                });
                title.appendChild(btnEdit);
            }
            return title;
        },
        bottom01 : function(p) {
            var size = hl.sys.winSize();

            var bottom = ssdjs.dom.div(0, size.y, size.x - 4, 98);
            bottom.style.backgroundColor = "#505050";
            bottom.style.border = "2px outset #474747";
            bottom.id = "bottom";

            var btnYes = hl.tpl.btn.b1("确认", 20, 20, function(obj) {
                p.fnYES();
            });
            bottom.appendChild(btnYes);

            var btnNo = hl.tpl.btn.b1("取消", 420, 20, function(obj) {
                $("#bottom").animate({
                    'top' : (size.y) + 'px'
                }, 200);
                $("#bottom").remove();

                p.fnNO();
            });
            bottom.appendChild(btnNo);
            $("#pop").append(bottom);
            $("#bottom").animate({
                'top' : (size.y - 98 ) + 'px'
            }, 200);

            // return bottom;
        },
        pop : function(p) {
            console.log(p);

            var size = hl.sys.winSize();

            $("#pop").empty();
            var popMain = hl.sys.hl$("pop");

            popMain.style.left = 0 + "px";
            popMain.style.top = 0 + "px";
            popMain.style.width = size.x + "px";
            popMain.style.height = size.y + "px";

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";
            //  titleText.style.fontWeight = "bold";
            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l();
                });
                title.appendChild(btnBack);
            }

            if(p.r) {
                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "编辑", function(obj) {
                    console.log("编辑");
                });
                title.appendChild(btnEdit);
            }

            popMain.appendChild(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 189);
            content.id = p.id + "Main";
            popMain.appendChild(content);

            var contentBg = ssdjs.dom.img(0, 0, size.x, size.y - 189, hl.url.img("bg_1.jpg"));
            //content.appendChild(contentBg);

            popMain.zIndex = "9999";
            popMain.style.display = "";

        },
        popStyle02 : function(p) {
            console.log(p);

            var size = hl.sys.winSize();

            $("#pop").empty();
            var popMain = hl.sys.hl$("pop");

            popMain.style.left = 0 + "px";
            popMain.style.top = 0 + "px";
            popMain.style.width = size.x + "px";
            popMain.style.height = size.y + "px";

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l();
                    btnBack.tag = p.lp;
                });
                title.appendChild(btnBack);
            }

            popMain.appendChild(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 189);
            content.id = p.id + "Main";
            popMain.appendChild(content);

            popMain.zIndex = "9999";
            popMain.style.display = "";

        },
        popStyleChat : function(p) {
            console.log(p);

            var size = hl.sys.winSize();

            $("#pop").empty();
            var popMain = hl.sys.hl$("pop");

            popMain.style.left = 0 + "px";
            popMain.style.top = 0 + "px";
            popMain.style.width = size.x + "px";
            popMain.style.height = size.y + "px";

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l();
                    btnBack.tag = p.lp;
                });
                title.appendChild(btnBack);
            }

            popMain.appendChild(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 187);
            content.id = p.id + "Main";
            popMain.appendChild(content);

            popMain.zIndex = "9999";
            popMain.style.display = "";

        },
        popStyle03 : function(p) {
            console.log("popStyle02");

            var size = hl.sys.winSize();

            $("#pop").empty();
            var popMain = hl.sys.hl$("pop");

            popMain.style.left = 0 + "px";
            popMain.style.top = 0 + "px";
            popMain.style.width = size.x + "px";
            popMain.style.height = size.y + "px";

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l(p.lP);
                });
                title.appendChild(btnBack);
            }

            popMain.appendChild(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 189);
            content.id = p.id + "Main";
            popMain.appendChild(content);

            popMain.zIndex = "9999";
            popMain.style.display = "";

        },
        /**
         * 无底部导航栏,多用于二级页面操作菜单弹出时
         */
        popStyleNoNav : function(p) {
            console.log("popStyleNoNav");
            var size = hl.sys.winSize();
            $("#pop").empty();
            var popMain = hl.sys.hl$("pop");
            popMain.style.left = 0 + "px";
            popMain.style.top = 0 + "px";
            popMain.style.width = size.x + "px";
            popMain.style.height = size.y + "px";

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            title.id = "title";
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l(p.lP);
                });
                title.appendChild(btnBack);
            }
            if(p.r) {
                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "编辑", function(obj) {
                    console.log("编辑");
                    p.r();
                });
                title.appendChild(btnEdit);
            }

            popMain.appendChild(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 91);
            content.id = p.id + "Main";
            popMain.appendChild(content);

            popMain.zIndex = "9999";
            popMain.style.display = "";

        },
        opt : function(id, x, y, height) {
            // if(hl.CLOSE_OPT == 2) {
            // //return;
            // }

            var _top = y;
            var _height = height;
            var size = ssdjs.browser.size();
            if(_top + _height > size.y) {
                _top = size.y - _height - 10;
            }
            var _left = x;
            if(_left + 173 > size.x) {
                _left = size.x - 173 - 10;
            }

            var div = ssdjs.dom.div(_left, _top, 173, _height);

            var mid = ssdjs.dom.div(0, 17, 162, (height - 17 - 18));
            //   mid.style.backgroundImage = hl.url.cssimg("pop/opt_mid.png");
            mid.style.backgroundColor = "#999";
            div.appendChild(mid);

            var top = ssdjs.dom.img(0, 0, 162, 19, hl.url.img("pop/opt_top.png"));
            div.appendChild(top);

            var bot = ssdjs.dom.img(0, (height - 18), 162, 18, hl.url.img("pop/opt_bot.png"));
            div.appendChild(bot);

            var c = ssdjs.dom.div(0, 0, 162, height);
            c.id = id;
            div.appendChild(c);
            return div;
        },
        loadingMask : function(id) {
            var size = ssdjs.browser.size();
            var div = ssdjs.dom.div(0, 0, size.x, size.y);
            div.id = id + "mask";
            div.style.backgroundColor = "black";
            div.css("opacity", 0.01);
            return div;
        },
        upStyle01Data : {
            location : {
                //  x : 0,
                // y : 0,
                w : 570,
                h : 400
            },
            item : {
                t : "标题",
                c : "内容"
            }
        },

        /**
         * {
         * location :{
         * 	//x : 0,
         * 	//y :0,
         * 	w : 0,
         * 	h : 0
         * },
         * item :{
         * 	t:"标题",
         *  c:"内容"
         * }}
         */
        upStyle01 : function(p) {

            var p = (p || hl.tpl.diag.upStyle01Data);
            hl.diag.up.clear();

            var _w = (p.location.w || 570);
            var _h = (p.location.h || 400);
            var _x = 0;
            var _y = 0;

            //  hl.sys.$("up").style.border = "1px solid #bababa";

            var titleW = ssdjs.dom.div(0, 0, _w, 60);
            _y += 60;
            titleW.style.borderTopLeftRadius = "6px";
            titleW.style.borderTopRightRadius = "6px";
            titleW.style.backgroundColor = "#47ad0f";
            titleW.style.border = "1px solid #bababa";

            var tltleText = ssdjs.dom.text(0, 16, _w, null, p.item.t);
            tltleText.style.color = "#ffffff";
            tltleText.style.fontSize = "24px";
            tltleText.style.textAlign = "center";
            titleW.appendChild(tltleText);
            hl.diag.up.add(titleW);
            var contentW = ssdjs.dom.div(0, _y, _w, _h - _y);
            contentW.style.border = "1px solid #bababa";
            contentW.style.borderBottomLeftRadius = "6px";
            contentW.style.borderBottomRightRadius = "6px";
            contentW.style.backgroundColor = "#fff";

            var contentText = ssdjs.dom.text(30, 30, _w - 60, null, p.item.c);
            contentText.style.color = "#505050";
            contentText.style.fontSize = "20px";
            contentW.appendChild(contentText);
            hl.diag.up.add(contentW);

            var btnW = 75;
            var btnH = 38;
            var btnClose = ssdjs.dom.div(_w - 100, _h - 60, btnW, btnH);

            var bgOn = ssdjs.dom.img(0, 0, btnW, btnH, hl.url.img("pub/back.png"));
            btnClose.appendChild(bgOn);
            // var bgOff = ssdjs.dom.img(0, 0, btnW, btnH, hl.url.img("btn1.png"));
            // bgOff.style.display = "none";
            // btnClose.appendChild(bgOff);

            var btnCloseText = ssdjs.dom.text(0, 6, btnW, null, "返回");
            btnCloseText.className = "sLte";
            btnCloseText.style.color = "#ffffff";
            btnCloseText.style.textAlign = "center";
            btnClose.appendChild(btnCloseText);

            btnClose.evtEnd(function(obj) {
                hl.diag.up.hide();
                // bgOff.style.display = "none";
            });

            btnClose.evtMove(function(obj) {
                // bgOff.style.display = "none";
            });
            btnClose.evtStart(function(obj) {
                // bgOff.style.display = "";
            });
            hl.diag.up.add(btnClose);

            hl.diag.up.show();
        },
        /**
         * 无底部导航栏,多用于二级页面操作菜单弹出时
         */
        upStyleNoNav : function(p) {
            console.log("upStyleNoNav");
            var size = hl.sys.winSize();
            hl.diag.up.clear();

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            title.id = "title";
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l(p.lP);
                });
                title.appendChild(btnBack);
            }
            if(p.r) {
                var btnEdit = hl.tpl.btnTitleR(size.x - 124, 14, "编辑", function(obj) {
                    console.log("编辑");
                    p.r();
                });
                title.appendChild(btnEdit);
            }

            hl.diag.up.add(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 91);
            content.id = p.id + "Main";
            hl.diag.up.add(content);
            hl.diag.up.show();
        },
        upStyleChat : function(p) {
            console.log(p);
            var size = hl.sys.winSize();
            hl.diag.up.clear();

            var title = ssdjs.dom.div(0, 0, size.x, 94);
            var titleBg = ssdjs.dom.img(0, 0, size.x, 94, hl.url.img("pub/title_bg.png"));
            title.appendChild(titleBg);

            var titleText = ssdjs.dom.text(0, 24, size.x, 50, p.title);
            titleText.style.color = "#FFF";
            titleText.style.textAlign = "center";

            titleText.style.fontSize = "40px";
            title.appendChild(titleText);
            if(p.l) {
                var btnBack = hl.tpl.btnTitleL(20, 14, "返回", function(obj) {
                    console.log("返回");
                    p.l();
                    btnBack.tag = p.lp;
                });
                title.appendChild(btnBack);
            }

            hl.diag.up.add(title);

            var content = ssdjs.dom.div(0, 91, size.x, size.y - 187);
            content.id = p.id + "Main";

            hl.diag.up.add(content);
            hl.diag.up.show();

        },
    },
    navigation : {
        body : function(p) {

            var item = p.item;
            var size = hl.sys.winSize();

            var _x = 2;
            var _y = 9;
            var _w = size.x - 10;
            var _h = 80;

            var divW = ssdjs.dom.div(_x, _y, _w, _h);
            divW.id = "popMenu";

            // 横向滑动区域
            var hscrollN = hl.tpl.scroll.h.create("hscrollN", 0, 0, _w, _h);
            hscrollN.style.backgroundColor = "#eee";
            divW.appendChild(hscrollN);
            //        滑动区域scroll定义
            hscrollN = hl.sys.scroll({
                id : "hscrollN",
                hScrollbar : false,
                vScroll : false,
                arrow : "hscrollN"
            });

            if(p.list != null && p.list.length > 0) {
                for(var i = 0; i < p.list.length; i++) {
                    hl.tpl.scroll.h.append("hscrollN", hl.tpl.navigation.item({
                        item : p.list[i],
                        i : i
                    }));

                }
            }
            hscrollN.refresh();
            return divW;
        },
        item : function(p) {
            var item = p.item;
            var size = hl.sys.winSize();

            var _x = -20;
            var _y = 35;
            var _w = 156;
            var _h = 56;
            var divW = ssdjs.dom.div(null, null, _w - 20, _h);

            var itemDiv = ssdjs.dom.div(_x, _y, _w - 1, _h - 1);
            //  itemDiv.style.border = "1px solid #b3b3b3";
            //  itemDiv.style.margin = "5px";
            // itemDiv.style.backgroundColor = "#eee";
            if(p.i == 0) {
                itemDiv.style.backgroundImage = hl.url.cssimg("t1On.png");
            } else {
                itemDiv.style.backgroundImage = hl.url.cssimg("t2On.png");
            }

            // itemDiv.style.backgroundColor = "#eee";
            var text = ssdjs.dom.text(0, 20, _w - 10, null, item.n);
            text.style.textAlign = "center";
            itemDiv.appendChild(text);

            itemDiv.evtEnd(function(obj) {
                // 判断是否是根目录，是根目录则请求初始接口
                if(p.i == 0) {
                    console.log("根目录请求");
                    hl.biz.user.req();
                } else {
                    hl.biz.user.req({
                        depId : item.i,
                        group : {
                            n : obj.tag.n,
                            i : obj.tag.i
                        }
                    });
                }
                if(p.i == 0) {
                    itemDiv.style.backgroundImage = hl.url.cssimg("t1On.png");
                } else {
                    itemDiv.style.backgroundImage = hl.url.cssimg("t2On.png");
                }
            });
            itemDiv.tag = item;
            itemDiv.evtMove(function(obj) {
                if(p.i == 0) {
                    itemDiv.style.backgroundImage = hl.url.cssimg("t1On.png");
                } else {
                    itemDiv.style.backgroundImage = hl.url.cssimg("t2On.png");
                }
            });
            itemDiv.evtStart(function(obj) {
                if(p.i == 0) {
                    itemDiv.style.backgroundImage = hl.url.cssimg("t1Off.png");
                } else {
                    itemDiv.style.backgroundImage = hl.url.cssimg("t2Off.png");
                }
            });

            divW.appendChild(itemDiv);
            return divW;
        }
    }
}