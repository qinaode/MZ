/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.user2 = {
    init : function(p) {
        hl.sys.hl$("up").style.display = "none";
        console.log("hl.biz.user2.init()");
        p = {
            id : "user",
            title : "用户列表",
            l : 0,
            r : 0,
            on : 1 ,
            bottom : 0
        };

        var size = hl.sys.winSize();

        $("#pop").empty();
        var popMain = hl.sys.hl$("pop");

        popMain.style.left = 0 + "px";
        popMain.style.top = 0 + "px";
        popMain.style.width = size.x + "px";
        popMain.style.height = size.y + "px";

        var title = ssdjs.dom.div(0, 0, size.x, 91);
        var titleBg = ssdjs.dom.img(0, 0, size.x, 91, hl.url.img("title_bg.jpg"));
        // title.appendChild(titleBg);
        //
        // title.appendChild(hl.tpl.navigation.body({
        // x : 0,
        // y : 10,
        // w : size.x,
        // h : 80,
        // list : hl.data.user.user.group
        // }));
        var _w = size.x;
        var _h = 90;

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

        var hscrollN = hl.tpl.scroll.h.create("hscrollN", 2, 0, _w, _h);
        hscrollN.style.backgroundColor = "#555";
        popMain.appendChild(hscrollN);
        //        滑动区域scroll定义
        hscrollN = hl.sys.scroll({
            id : "hscrollN",
            hScrollbar : false,
            vScroll : false,
            arrow : "hscrollN"
        });

        p.list = [{
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }, {
            n : "皓联", // nick
            i : 0		 // id
        }]

        p.list = hl.data.user.user.group;

        if(p.list != null && p.list.length > 0) {
            for(var i = 0; i < p.list.length; i++) {
                hl.tpl.scroll.h.append("hscrollN", hl.tpl.navigation.item({
                    item : p.list[i],
                    i : i
                }));
            }
        }
        hscrollN.refresh();

        var content = ssdjs.dom.div(0, 91, size.x, size.y - 189);
        content.id = p.id + "Main";
        popMain.appendChild(content);

        var contentBg = ssdjs.dom.img(0, 0, size.x, size.y - 189, hl.url.img("bg_1.jpg"));
        //content.appendChild(contentBg);

        var bottomMenu = ssdjs.dom.div(0, size.y - 98, size.x, 98);
        var bottomMenuBg = ssdjs.dom.img(0, 0, size.x, 98, hl.url.img("mainMenu_bg.jpg"));
        bottomMenu.appendChild(bottomMenuBg);

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

        var btn0 = hl.tpl.btnBottom(_x, 0, size.x / 4, 75, "gerencenter.png", "个人中心", i0, function(obj) {
            hl.biz.person.init();
        });
        bottomMenu.appendChild(btn0);
        _x += size.x / 4;
        var btn1 = hl.tpl.btnBottom(_x, 0, size.x / 4, 75, "userlist.png", "用户列表", i1, function(obj) {
            hl.biz.user.req();
        });
        bottomMenu.appendChild(btn1);
        _x += size.x / 4;
        var btn2 = hl.tpl.btnBottom(_x, 0, size.x / 4, 75, "cloud.png", "我的网盘", i2, function(obj) {
            hl.biz.disk.init();
        });
        bottomMenu.appendChild(btn2);
        _x += size.x / 4;
        var btn3 = hl.tpl.btnBottom(_x, 0, size.x / 4, 75, "more.png", "更多", i3, function(obj) {
            hl.biz.more.init();
        });
        bottomMenu.appendChild(btn3);

        popMain.appendChild(bottomMenu);

        popMain.zIndex = "9999";
        popMain.style.display = "";

        var main = $("#userMain");
        main.css("background", "#fff");

        var _h = $("#userMain").height();
        var _w = $("#userMain").width();

        // 搜索框
        var searchDiv = ssdjs.dom.div(0, 0, _w, 61);
        searchDiv.style.backgroundColor = "#EEE";

        var inputW = ssdjs.dom.div(10, 12, _w - 20, 35);
        inputW.style.backgroundColor = "#fff";
        inputW.style.borderRadius = "5px";
        inputW.style.border = "1px solid #b3b3b3";

        var input = ssdjs.dom.input(0, 0, _w - 22, 38);
        input.id = "searchInput";
        input.style.border = "0px";
        input.style.backgroundColor = "transparent";
        //input.style.paddingLeft = "10px";
        //input.style.paddingRight = "10px";
        input.style.fontSize = "22px";
        inputW.appendChild(input);

        var inputBtn = ssdjs.dom.div(_w - 56, 6, 30, 28);

        inputBtn.style.backgroundImage = hl.url.cssimg("btn_search_box.gif");
        inputBtn.style.backgroundRepeat = "no-repeat";
        inputW.appendChild(inputBtn);

        inputBtn.evtEnd(function(obj) {

            hl.biz.user.req({
                txtSearch : $("#searchInput").val(),
            });

            inputBtn.style.backgroundImage = hl.url.cssimg("btn_search_box.gif");
            obj.style.marginTop = "0px";
        });
        inputBtn.evtMove(function(obj) {
            inputBtn.style.backgroundImage = hl.url.cssimg("btn_search_box1.gif");
            obj.style.marginTop = "0px";
        });
        inputBtn.evtStart(function(obj) {
            inputBtn.style.backgroundImage = hl.url.cssimg("btn_search_box1.gif");
            obj.style.marginTop = "0px";
        });

        searchDiv.appendChild(inputW);

        // searchDiv.style.padding = "10px";
        main.append(searchDiv);

        var hscrollU = hl.tpl.scroll.v.create("hscrollU", 5, 61, _w - 10, _h - 61);
        main.append(hscrollU);

        // 滑动区域scroll定义
        scrollU = hl.sys.scroll({
            id : "hscrollU",
            snap : false,
            vScrollbar : false,
            hScroll : false
        });

        for(var i = 0; i < hl.biz.user.listTest.length; i++) {
            var item = hl.biz.user.listTest[i];

            // 根据item是否有性别属性判断是人员或者部门
            if(item.SEX != null) {
                // 人员
                hl.tpl.scroll.v.append("hscrollU", hl.biz.user.personItem({
                    item : item
                }));
            } else {
                // 部门
                hl.tpl.scroll.v.append("hscrollU", hl.biz.user.groupItem({
                    item : item
                }));

            }

            // var tem = hl.biz.user.groupItem({
            // id : i,
            // x : 0,
            // y : 0,
            // w : _w,
            // h : 40,
            // t : item.gn,
            // list : item.list
            // });
            // hl.tpl.scroll.v.append("hscrollU", tem);
        }
        scrollU.refresh();

    },
    groupItem2 : function(p) {
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
        title.style.fontWeight = "bold";
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
    groupItem3 : function(p) {

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
        title.style.fontWeight = "bold";
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
    personItem : function(p) {
        var item = p.item;

        var size = hl.sys.winSize();
        var _w = size.x - 10;
        var _h = 100;
        divW = ssdjs.dom.div(null, null, _w, 100);

        var line = ssdjs.dom.img(0, _h - 4, _w, 4, hl.url.img("line.jpg"));
        divW.appendChild(line);

        var head = ssdjs.dom.img(10, 13, 70, 70, hl.url.img("h.jpg"));
        divW.appendChild(head);

        var nick = ssdjs.dom.text(120, 25, null, null, item.ACTUALNAME);
        divW.appendChild(nick);

        var sex = ssdjs.dom.img(135, 50, 28, 28, hl.url.img(item.SEX + ".png"));
        divW.appendChild(sex);

        var message = ssdjs.dom.img(_w - 50, 38, 32, 32, hl.url.img("chat.png"));
        divW.appendChild(message);

        return divW;
    },
    /**
     *  组
     */
    groupItem : function(p) {
        var item = p.item;

        var size = hl.sys.winSize();
        var _w = size.x - 10;
        var _h = 100;
        divW = ssdjs.dom.div(null, null, _w, 40);
        divW.style.margin = "5px";
        divW.style.backgroundColor = "#eee";

        var title = ssdjs.dom.text(20, 10, null, null, item.DEPARTNAME);
        title.style.fontWeight = "bold";
        divW.appendChild(title);

        divW.evtEnd(function(obj) {
            // TODO 请求接口
            hl.biz.user.req({
                depId : obj.tag.DEPARTID,
                group : {
                    n : obj.tag.DEPARTNAME,
                    i : obj.tag.DEPARTID
                }
            });
        });
        divW.tag = p.item;

        return divW;
    },
    req : function(p) {
        p = (p || {});
        console.log("userReqP:" + JSON.stringify(p));

        // URL

        var url = hl.url.api("User/DepJsonInfo1");
        if(p.group != null) {
            url = hl.url.api("User/UsersOfDep");
        }
        if(p.txtSearch != null) {
            url = hl.url.api("User/searchJsonInfo");
        }
        var data = "";
        if(p.txtSearch != null) {
            data += "txtSearch=" + p.txtSearch;
        }
        if(p.depId != null) {
            data += "&depId=" + p.depId;
        }
        var success = function(result) {
            console.log(result.listTest);

            // TODO 操作导航{遍历导航list ，if(存在){删除此项之后的}else{list.push({i:p.txtSearch,n:"sss"})}}
            if(p.group != null) {
                var i = 0;
                for( i = 0; i < hl.data.user.user.group.length; i++) {
                    if(hl.data.user.user.group[i].i == p.depId) {
                        // TODO 删除之后的

                        hl.data.user.user.group = hl.data.user.user.group.slice(0, i + 1);
                        i = -1;
                        // 防范
                        break;
                    }
                }
                if(i == hl.data.user.user.group.length) {
                    hl.data.user.user.group.push({
                        i : p.group.i,
                        n : p.group.n
                    });
                }
            } else {
                hl.data.user.user.group = hl.data.user.user.group.slice(0, 1);
            }

            hl.biz.user.listTest = result.listTest;
            hl.biz.user.init();

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
    dsList : function() {

    },
    listTest : [{
        DEPARTNAME : "销售部", // nick
        DEPARTID : 1		 // id
    }, {
        DEPARTNAME : "软件部",
        DEPARTID : 2
    }],
}