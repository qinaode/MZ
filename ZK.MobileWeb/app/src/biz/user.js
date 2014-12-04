/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.user = {
    init : function(p) {
        hl.diag.menu.show({
            on : 1
        });
        var p = (p || {});
        console.log(p);

        hl.tpl.diag.pop({
            id : "user",
            title : "用户列表",
            l : 0,
            r : 0
        });
        hl.sys.hl$("up").style.display = "none";

        var main = $("#userMain");
        main.css("background", "#fff");

        var _h = $("#userMain").height();
        var _w = $("#userMain").width();

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

        if(p.search != null) {
            if(hl.biz.user.resultData.listTest != null) {
                for(var i = 0; i < hl.biz.user.resultData.listTest.length; i++) {

                    var tItem = hl.biz.user.resultData.listTest[i];

                    var tem = hl.biz.user.friendItem({
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

            var divW = ssdjs.dom.div(null, null, _w, 60);
            var divTitle = ssdjs.dom.div(null, 10, _w, 49);
            divTitle.style.backgroundColor = "#c7c7c7";
            var title = ssdjs.dom.text(25, 11, null, null, "组群");
            title.style.fontSize = "26px";
            title.style.color = "#fff";
            divTitle.appendChild(title);
            var bgLine = ssdjs.dom.img(0, 48, _w, 2, hl.url.img("line.jpg"));
            divTitle.appendChild(bgLine);
            divW.appendChild(divTitle);
            hl.tpl.scroll.v.append("hscrollU", divW);

            var divW = ssdjs.dom.div(null, null, _w, 87);
            divW.evtEnd(function(obj) {
                hl.biz.group.index();
            });
            var head = ssdjs.dom.img(22, 8, 70, 70, hl.url.img("user/group.png"));
            var message = ssdjs.dom.img(_w - 50, 28, 21, 29, hl.url.img("user/iconRight.png"));
            divW.appendChild(message);

            divW.appendChild(head);
            hl.tpl.scroll.v.append("hscrollU", divW);

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
                var tem = hl.biz.user.groupItemNew({
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

        // 刷新页面实时提示消息
        // 公司公告动态刷新
        var time = setInterval(function() {
            var pppList = (hl.data.chat.receive.list || []);
            if(pppList.length > 0) {
                for(var i = 0; i < pppList.length; i++) {
                    if(pppList[i].list == null || pppList[i].list == undefined) {
                        continue;
                    }
                    var id = pppList[i].id;
                    var num = pppList[i].list.length;
                    if(num != null && num > 0) {
                        $("#" + id + "messageNum").html(num);
                        $("#" + id + "message").show();
                    }
                }
            }

        }, 1000);
    },
    groupItem : function(p) {
        var divW = ssdjs.dom.div(null, null, p.w - 10, p.h);
        divW.style.margin = "5px";

        var divTitle = ssdjs.dom.div(p.x, p.y, p.w - 10, p.h);
        divTitle.evtEnd(function(obj) {
            var idd = p.id + "Content";
            if($("#" + idd).is(":hidden")) {
                $("#" + idd).slideDown(10);
                divW.style.margin = "5px";

                divW.style.height = (p.h + p.list.length * 100) + "px";

                var mask = ssdjs.$("hscrollU");
                mask.ssdh = (mask.ssdh || 0) + p.list.length * 100;
                ssdjs.$("hscrollUmain").style.height = mask.ssdh + "px";
                scrollU.refresh();
            } else {
                $("#" + idd).slideUp(10);
                divW.style.height = p.h + "px";
                var mask = ssdjs.$("hscrollU");
                mask.ssdh = (mask.ssdh || 0) - p.list.length * 100;
                ssdjs.$("hscrollUmain").style.height = mask.ssdh + "px";
                scrollU.refresh();
            }

        });
        divTitle.style.backgroundColor = "#eee";

        var title = ssdjs.dom.text(20, 10, null, null, p.t);
      
        divTitle.appendChild(title);

        divW.appendChild(divTitle);

        var divContent = ssdjs.dom.div(0, p.h, p.w - 10, p.list.length * 100);
        divContent.id = p.id + "Content";
        divContent.style.display = "none";

        var _y = 0;
        for(var i = 0; i < p.list.length; i++) {

            divContent.appendChild(hl.biz.user.friendItem({
                x : 0,
                y : _y,
                w : p.w - 10,
                h : 100,
                item : p.list[i]
            }));
            _y += 100;

        }

        divW.appendChild(divContent);
        $("#aaa").slideUp(500);

        return divW;

    },
    /**
     *
     */
    groupItem2 : function(p) {

        var size = hl.sys.winSize();
        var _w = size.x - 10;
        var _h = 40;

        var divW = ssdjs.dom.div(0, 0, _w, _h);
        divW.style.margin = "5px";

        var divTitle = ssdjs.dom.div(0, 0, _w, _h);
        divTitle.style.backgroundColor = "#eee";

        divTitle.evtEnd(function(obj) {
            var idd = p.id + "Content";
            if($("#" + idd).is(":hidden")) {
                $("#" + idd).slideDown(10);
                divW.style.height = (p.h + p.list.length * 100) + "px";
                var mask = ssdjs.$("hscrollU");
                mask.ssdh = (mask.ssdh || 0) + p.list.length * 100;
                ssdjs.$("hscrollUmain").style.height = mask.ssdh + "px";
                scrollU.refresh();
            } else {
                $("#" + idd).slideUp(10);
                divW.style.height = _h + "px";
                var mask = ssdjs.$("hscrollU");
                mask.ssdh = (mask.ssdh || 0) - p.list.length * 100;
                ssdjs.$("hscrollUmain").style.height = mask.ssdh + "px";
                scrollU.refresh();
            }
        });
        var title = ssdjs.dom.text(20, 10, null, null, p.t);
      
        divTitle.appendChild(title);
        divW.appendChild(divTitle);

        var divContent = ssdjs.dom.div(0, _h, _w, p.list.length * 100);
        divContent.id = p.id + "Content";
        divContent.style.display = "none";

        var _y = 0;
        for(var i = 0; i < p.list.length; i++) {

            divContent.appendChild(hl.biz.user.friendItem({
                x : 0,
                y : _y,
                w : p.w - 10,
                h : 100,
                item : p.list[i]
            }));
            _y += 100;

        }

        divW.appendChild(divContent);
        return divW;
    },
    /**
     *{
     * 	location : {x : null, y: null, w : 630, h : 40},
     * 	item : {name : "string", id : 11 ,list : [] ,}
     * }
     */
    groupItemNew : function(p) {
        console.log("groupItemNewP");
        console.log(p);

        var location = (p.location || {});
        var item = (p.item || {});
        item.list = (item.list || []);

        var size = hl.sys.winSize();
        var _x = (p.location.x || null);
        var _y = (p.location.y || null);
        var _w = (p.location.w || size.x);
        var _h = (p.location.h || 46);

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
        var title = ssdjs.dom.text(25, 8, null, null, item.name);
        title.style.fontSize = "28px";

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

                    divContent.appendChild(hl.biz.user.friendItem({
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

                    divContent.appendChild(hl.biz.user.groupItemNew({
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
    /**
     *{
     * 	location : {x : null, y: null, w : 630, h : 40},
     * 	item : {ACTUALNAME : "string", USERID : 11 ,SEX : 1 ,}
     * }
     */
    friendItem : function(p) {

        var location = (p.location || {});
        var item = (p.item || {});

        var size = hl.sys.winSize();
        var _x = (p.location.x || null);
        var _y = (p.location.y || null);
        var _w = (p.location.w - p.location.x || size.x - 10);
        var _h = (p.location.h || 108);

        var divW = ssdjs.dom.div(_x, _y, _w, _h);

        var check = hl.tpl.btn.check(item.USERID + "usercheck", 15, 32, "off", null);
        check.style.display = "none";

        divW.appendChild(check);

        var line = ssdjs.dom.img(0, _h - 2, _w, 2, hl.url.img("line.jpg"));
        divW.appendChild(line);

        var divMoveW = ssdjs.dom.div(0, 0, _w, _h);
        divMoveW.id = item.USERID + "userdivMoveW";
        divW.appendChild(divMoveW);

        var headIcon = hl.url.img("user/head.jpg");
        if(item.FACEFILE != "/Files/Faces/") {
            headIcon = hl.url.api(item.FACEFILE);
        }
        var head = ssdjs.dom.img(22, 19, 70, 70, headIcon);
        divMoveW.appendChild(head);

        var message = ssdjs.dom.div(65, -4, 45, 45);
        message.id = item.USERID + "message";
        message.style.backgroundImage = hl.url.cssimg("pub/imnb.png");
        var messageNum = ssdjs.dom.text(0, 7, 45, null, 1);
        messageNum.style.textAlign = "center";
        messageNum.id = item.USERID + "messageNum";
        messageNum.style.color = "#fff";
        messageNum.style.fontSize = "24px";
        message.appendChild(messageNum);
        divMoveW.appendChild(message);
        message.style.display = "none";

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

        divW.evtEnd(function(obj) {
            // 关闭消息提示
            $("#" + item.USERID + "messageNum").hide();

            hl.biz.chat.index({
                friend : {
                    id : item.USERID,
                    name : item.ACTUALNAME,
                    head : headIcon
                }
            });
        });
        return divW;
    },
    req : function(p) {
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

            if(p.txtSearch != null) {
                hl.biz.user.init({
                    search : 1
                });
            } else {
                hl.biz.user.init();
            }

            console.log(result);
        };
        var error = function(result) {
            console.log("失败");
            console.log(result);
            hl.diag.alert({
                m : "网络错误"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    dsList : function() {

    },
    resultData : {
        item : {
            "id" : 1,
            "name" : "教育",
            list : [{
                "id" : 28,
                "name" : "软件部",
                list : [{
                    "USERID" : 1012,
                    "ACTUALNAME" : "dsf",
                    "SEX" : -1
                }, {
                    "USERID" : 1867,
                    "ACTUALNAME" : "ertert",
                    "SEX" : -1
                }, {
                    "USERID" : 4444,
                    "ACTUALNAME" : "4444",
                    "SEX" : 1
                }, {
                    "USERID" : 4564,
                    "ACTUALNAME" : "rty",
                    "SEX" : -1
                }, {
                    "USERID" : 5435,
                    "ACTUALNAME" : "xczv",
                    "SEX" : -1
                }, {
                    "USERID" : 6645,
                    "ACTUALNAME" : "gh",
                    "SEX" : -1
                }, {
                    "USERID" : 10022,
                    "ACTUALNAME" : "薄建超",
                    "SEX" : 1
                }, {
                    "USERID" : 10024,
                    "ACTUALNAME" : "吴战磊",
                    "SEX" : 1
                }, {
                    "USERID" : 10034,
                    "ACTUALNAME" : "闫金毅",
                    "SEX" : 1
                }, {
                    "USERID" : 10040,
                    "ACTUALNAME" : "许建成",
                    "SEX" : 1
                }, {
                    "USERID" : 10041,
                    "ACTUALNAME" : "服务器",
                    "SEX" : 0
                }, {
                    "USERID" : 32453,
                    "ACTUALNAME" : "sdf",
                    "SEX" : -1
                }, {
                    "USERID" : 46546,
                    "ACTUALNAME" : "fdg",
                    "SEX" : -1
                }, {
                    "USERID" : 111101,
                    "ACTUALNAME" : "gfdgdfg",
                    "SEX" : -1
                }, {
                    "USERID" : 207984,
                    "ACTUALNAME" : "2",
                    "SEX" : -1
                }, {
                    "USERID" : 234324,
                    "ACTUALNAME" : "wer",
                    "SEX" : -1
                }, {
                    "USERID" : 546456,
                    "ACTUALNAME" : "dfsg",
                    "SEX" : -1
                }]
            }, {
                "id" : 30,
                "name" : "行政部",
                list : [{
                    "USERID" : 10012,
                    "ACTUALNAME" : "赵辉荣",
                    "SEX" : 0
                }, {
                    "USERID" : 10019,
                    "ACTUALNAME" : "王风玲",
                    "SEX" : 0
                }, {
                    "USERID" : 10020,
                    "ACTUALNAME" : "杨静",
                    "SEX" : 0
                }, {
                    "USERID" : 10033,
                    "ACTUALNAME" : "刘利艳",
                    "SEX" : 0
                }]
            }, {
                "id" : 27,
                "name" : "销售部",
                list : [{
                    "USERID" : 1012,
                    "ACTUALNAME" : "dsf",
                    "SEX" : -1
                }, {
                    "USERID" : 1867,
                    "ACTUALNAME" : "ertert",
                    "SEX" : -1
                }, {
                    "USERID" : 6546,
                    "ACTUALNAME" : "sdf",
                    "SEX" : -1
                }, {
                    "USERID" : 10021,
                    "ACTUALNAME" : "秦立",
                    "SEX" : 1
                }, {
                    "USERID" : 10025,
                    "ACTUALNAME" : "范碧江",
                    "SEX" : 1
                }, {
                    "USERID" : 10027,
                    "ACTUALNAME" : "朱靖湘",
                    "SEX" : 1
                }, {
                    "USERID" : 10028,
                    "ACTUALNAME" : "张晓峰",
                    "SEX" : 1
                }, {
                    "USERID" : 10002137,
                    "ACTUALNAME" : "dsfdsf",
                    "SEX" : 1
                }]
            }, {
                "id" : 31,
                "name" : "规划部",
                list : [{
                    "USERID" : 10013,
                    "ACTUALNAME" : "高飞",
                    "SEX" : 1
                }, {
                    "USERID" : 10014,
                    "ACTUALNAME" : "刘聪",
                    "SEX" : 1
                }, {
                    "USERID" : 10017,
                    "ACTUALNAME" : "文威",
                    "SEX" : 1
                }, {
                    "USERID" : 10018,
                    "ACTUALNAME" : "杨超",
                    "SEX" : 1
                }, {
                    "USERID" : 10030,
                    "ACTUALNAME" : "曹欣然",
                    "SEX" : 1
                }, {
                    "USERID" : 10031,
                    "ACTUALNAME" : "刘洋",
                    "SEX" : 0
                }, {
                    "USERID" : 10032,
                    "ACTUALNAME" : "王文峰",
                    "SEX" : 1
                }]
            }, {
                "id" : 32,
                "name" : "硬件部",
                list : [{
                    "USERID" : 10035,
                    "ACTUALNAME" : "邵阳",
                    "SEX" : 1
                }, {
                    "USERID" : 10036,
                    "ACTUALNAME" : "赵志辉",
                    "SEX" : 1
                }, {
                    "USERID" : 10037,
                    "ACTUALNAME" : "高校明",
                    "SEX" : 1
                }, {
                    "USERID" : 10038,
                    "ACTUALNAME" : "吕云龙",
                    "SEX" : 1
                }, {
                    "USERID" : 10039,
                    "ACTUALNAME" : "赵林",
                    "SEX" : 1
                }]
            }, {
                "id" : 33,
                "name" : "咨询部",
                list : [{
                    "USERID" : 10016,
                    "ACTUALNAME" : "咨询刘聪",
                    "SEX" : 1
                }]
            }, {
                "id" : 34,
                "name" : "财务部",
                list : [{
                    "id" : 29,
                    "name" : "其它",
                    list : [{
                        "id" : "otherId",
                        "name" : "otherName",
                        list : [{
                            "USERID" : 4444,
                            "ACTUALNAME" : "4444",
                            "SEX" : 1
                        }, {
                            "USERID" : 10000,
                            "ACTUALNAME" : "dsfgdsf",
                            "SEX" : 1
                        }, {
                            "USERID" : 1000000,
                            "ACTUALNAME" : "sadfsadf",
                            "SEX" : 1
                        }, {
                            "USERID" : 10000021,
                            "ACTUALNAME" : "张三",
                            "SEX" : 1
                        }, {
                            "USERID" : 10002133,
                            "ACTUALNAME" : "sdafds",
                            "SEX" : 1
                        }, {
                            "USERID" : 10002134,
                            "ACTUALNAME" : "xczv",
                            "SEX" : 1
                        }, {
                            "USERID" : 10002135,
                            "ACTUALNAME" : "22",
                            "SEX" : -1
                        }, {
                            "USERID" : 10002136,
                            "ACTUALNAME" : "44",
                            "SEX" : 1
                        }]
                    }]
                }]
            }]
        }
    },

}