/**
 * 用户信息页
 */
var hl = (hl || {});
hl.biz = (hl.biz || {});
hl.biz.chat = {

    index : function(p) {
        hl.data.chat.onDuty.set(p.friend);
        hl.biz.chat.chat();
    },
    sendMessage : function(p) {
        p = (p || {});
        console.log("sendMessageReqP:" + JSON.stringify(p));

        // URL
        var url = hl.url.api("Chat/SendMessageChatInfo");

        var user = hl.data.user.get();
        var data = "userID=" + user.id;

        if(p.message != null) {
            data += "&message=" + p.message;
        }
        var friend = hl.data.chat.onDuty.get();
        data += "&toUserId=" + friend.id;

        var success = function(result) {
            console.log(result);
            if(result.code == 1) {
                hl.tpl.scroll.v.append("hscrollC", hl.biz.chat.chatItemSend({
                    item : {
                        text : p.message
                    }
                }));
            }
            scrollC.refresh();
            var mask = ssdjs.$("hscrollC");
            if(mask.ssdh > $("#chatMain").height()) {
                scrollC.scrollTo(0, 0 - mask.ssdh + $("#chatMain").height());
            }
            //  scrollC.scrollTo(0, 0 - mask.ssdh + $("#chatMain").height());

        };
        var error = function(result) {
            hl.diag.alert({
                m : "网路错误"
            });
            return;
        };
        hl.req(url, success, error, data);

    },
    receiveMessage : function(p) {
        if($("#chatMain") == null) {
            return;
        }
        hl.tpl.scroll.v.append("hscrollC", hl.biz.chat.chatItemReceive({
            item : {
                text : p.item.message
            }
        }));

        scrollC.refresh();
        var mask = ssdjs.$("hscrollC");
        scrollC.scrollTo(0, 0 - mask.ssdh + $("#chatMain").height());

    },
    chat : function(p) {
        console.log("chatP" + p);
        var p = (p || {});
        var titleValue = "聊天";
        var friend = hl.data.chat.onDuty.get();
        if(friend.name != null) {
            titleValue = friend.name;
        }
        hl.tpl.diag.upStyleChat({
            id : "chat",
            title : titleValue,
            l : hl.diag.up.close,
            r : 0,
        });
        var main = $("#chatMain");

        var _h = $("#chatMain").height();
        var _w = $("#chatMain").width();
        hl.sys.$("chatMain").style.backgroundColor = "#FFF";

        var hscrollC = hl.tpl.scroll.v.create("hscrollC", 0, 0, _w, _h);
        main.append(hscrollC);
        // 滑动区域scroll定义
        scrollC = hl.sys.scroll({
            id : "hscrollC",
            snap : false,
            vScrollbar : false,
            hScroll : false,
            momentum : true
        });
        var list = hl.biz.disk.dataTest;

        // 底部输入区域
        var size = hl.sys.winSize();

        var divW = ssdjs.dom.div(0, size.y - 94, size.x, 94);
        divW.style.backgroundImage = hl.url.cssimg("chat/bg.png");

        var inputW = ssdjs.dom.div(20, 15, size.x - 140, 70);
        inputW.style.backgroundColor = "#fff";
        inputW.style.borderRadius = "5px";
        inputW.style.border = "1px solid #b3b3b3";

        var input = ssdjs.dom.input(0, 0, size.x - 140, 60);
        input.id = "chatInput";
        input.style.border = "0px";
        input.style.backgroundColor = "transparent";
        input.style.fontSize = "30px";
        inputW.appendChild(input);

        divW.appendChild(inputW);

        var btnSend = ssdjs.dom.text(size.x - 120, 30, 120, null, "发送");
        btnSend.id = "btnSend";
        btnSend.style.textAlign = "center";
        btnSend.style.color = "#9f9f9f";
        btnSend.style.fontSize = "30px";
        divW.appendChild(btnSend);
        hl.diag.up.add(divW);

        // 显示未读消息
        var pppList = (hl.data.chat.receive.list || []);
        if(pppList.length > 0) {
            for(var i = 0; i < pppList.length; i++) {
                var onDuty = hl.data.chat.onDuty.get();
                if(pppList[i].id === onDuty.id) {
                    var list = pppList[i].list;
                    if(list != null && list.length > 0) {
                        for(var j = 0; j < list.length; j++) {
                            hl.tpl.scroll.v.append("hscrollC", hl.biz.chat.chatItemReceive({
                                item : {
                                    text : list[j].message
                                }
                            }));
                        }

                    }
                    pppList[i] = {};
                }
            }
        }
        scrollC.refresh();
        var mask = ssdjs.$("hscrollC");
        scrollC.scrollTo(0, 0 - mask.ssdh + $("#chatMain").height());

        $("#chatInput").bind("keyup", function(event) {
            var btnSend = hl.sys.$("btnSend");
            if($("#chatInput").val() != null && $("#chatInput").val() != "") {
                btnSend.style.color = "#61bc2d"
                btnSend.evtEnd(function(obj) {
                    btnSend.style.color = "#61bc2d"
                    hl.biz.chat.sendMessage({
                        message : $("#chatInput").val()
                    });
                    scrollC.refresh();
                    $("#chatInput").val("");
                    btnSend.style.color = "#9f9f9f";
                    btnSend.evtEnd(function(obj) {
                    });
                    btnSend.evtMove(function(obj) {
                    });
                    btnSend.evtStart(function(obj) {
                    });
                });
                btnSend.evtMove(function(obj) {
                    btnSend.style.color = "#61bc2d"
                });
                btnSend.evtStart(function(obj) {
                    btnSend.style.color = "#9f9f9f"
                });
            } else {
                btnSend.style.color = "#9f9f9f";
                btnSend.evtEnd(function(obj) {
                });
                btnSend.evtMove(function(obj) {
                });
                btnSend.evtStart(function(obj) {
                });
            }
        });
    },
    chatItemReceive : function(p) {
        var item = p.item;
        var size = hl.sys.winSize();
        var w = size.x;
        var h = 50 + Math.ceil(item.text.replace(/[^\x00-\xff]/g, 'AA').length / 28) * 33;
        // TODO 根据聊天信息确定高度
        var divW = ssdjs.dom.div(null, null, size.x, h + 10);
        var user = hl.data.chat.onDuty.get();
        var head = ssdjs.dom.img(0, 5, 80, 80, user.head);
        head.className = "grtx";

        divW.appendChild(head);

        var content = ssdjs.dom.div(90, -23, w - 130, h);
        // content.style.backgroundColor = "#dddddd";
        // content.style.borderRadius = "5px";
        var text = ssdjs.dom.text(0, 0, null, null, item.text);
        text.className = "triangle-border left";
        text.style.color = "#000";
        text.style.fontSize = "30px";
        content.appendChild(text);
        divW.appendChild(content);
        return divW;

    },
    chatItemSend : function(p) {

        var item = p.item;

        var size = hl.sys.winSize();
        var w = size.x;
        var h = 50 + Math.ceil(item.text.replace(/[^\x00-\xff]/g, 'AA').length / 28) * 33;

        // TODO 根据聊天信息确定高度
        var divW = ssdjs.dom.div(null, null, size.x, h + 10);
        var user = hl.data.user.get();
        var head = ssdjs.dom.img(w - 80, 5, 80, 80, hl.url.api(user.head));
        head.className = "grtx";

        divW.appendChild(head);

        var content = ssdjs.dom.div(40, -23, w - 130, h);

        //  content.className = "triangle-border left" ;
        // content.style.backgroundColor = "#abd448";
        // content.style.borderRadius = "5px";
        var text = ssdjs.dom.text(null, null, null, null, item.text);
        text.className = "triangle-border right";
        text.style.color = "#000";
        text.style.fontSize = "30px";
        // text.style.position = "relative";
        text.style.cssFloat = "right";
        content.appendChild(text);
        divW.appendChild(content);
        return divW;
    }
}