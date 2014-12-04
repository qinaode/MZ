var hl = (hl || {});
hl.data = (hl.data || {});

hl.data.chat = {

    receive : {
        add : function(p) {
            var item = p.item;
            var onDuty = (hl.data.chat.onDuty.get() || {
                id : -1,
                name : ""
            });
            if(onDuty.id == item.SENDER_USERID) {
                // TODO 像页面添加item ssdjs.$("hscrollC")
                if(ssdjs.$("hscrollC") != null) {
                    console.log("添加到页面");
                    hl.biz.chat.receiveMessage({
                        item : {
                            message : item.CONTENT
                        }
                    });
                } else {
                    console.log("添加到缓存");
                    hl.data.chat.receive.push(p);
                }

            } else {
                console.log("添加到缓存");
                hl.data.chat.receive.push(p);
                // TODO 添加提醒
            }

        },
        // {id : 10040 ,list :[{user:10040,message:"ssss",time:21212121212},{}]}
        list : [],
        push : function(p) {
            var item = p.item;
            var list = hl.data.chat.receive.list;
            for(var i = 0; i < list.length; ) {
                if(list[i].id != null && list[i].id != undefined) {
                    var tem = list[i];
                    if(item.SENDER_USERID == tem.id) {
                        list[i].list.push({
                            id : item.SENDER_USERID,
                            message : item.CONTENT,
                            time : item.SENDTIME
                        });
                        break;
                    }
                }
                i++;
            }
            if(i >= list.length) {
                list.push({
                    id : item.SENDER_USERID,
                    list : [{
                        id : item.SENDER_USERID,
                        message : item.CONTENT,
                        time : item.SENDTIME
                    }]
                });
            }
        }
    },

    onDuty : {
        friend : {}, // {name : "张三" , id : 10010}
        get : function() {
            return hl.data.chat.onDuty.friend;
        },
        set : function(p) {
            hl.data.chat.onDuty.friend = p
        }
    },
    // 存储聊天记录
    // chatList : [] // chatItem :{key : fid ,user : "fid"|| "me" ,text :"sss",time:21212121}
    history : {
        chatList : [],
        /**
         * p {fid :"1212"}
         */
        get : function(p) {
            var key = p.fid;
            return JSON.parse(localStorage.getItem(key));
        },
        /**
         * p :{
         * 	item :{key : fid ,user : "1212"|| "me" ,text :"sss",time:21212121}
         * }
         */
        set : function(p) {
            var key = p.item.key;
            hl.data.chat.historychatList = hl.data.chat.history.get(key);
            hl.data.chat.historychatList.push(p.item);
            localStorage.setItem(key, JSON.stringify(hl.data.chat.historychatList));
        }
    }

}