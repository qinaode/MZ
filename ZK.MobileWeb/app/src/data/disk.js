var hl = (hl || {});
hl.data = (hl.data || {});

hl.data.disk = {
	imgBrowserPath : "",
    // 文件列表浏览返回按钮的栈
    backList : {
        list : [],
        push : function(p) {
            hl.data.disk.backList.list.push(p);
        },
        pop : function() {
            if(hl.data.disk.backList.list.length > 0) {
                return hl.data.disk.backList.list.pop();
            } else {
                return -1;
            }
        },
        last : function() {
            if(hl.data.disk.backList.list.length > 0) {
                return hl.data.disk.backList.list[hl.data.disk.backList.list.length - 1];
            } else {
                return -1;
            }
        }
    },
    choseList : {
        /**
         * item : {id : 111,head :"sss.icon",}
         */
        list : [],
        add : function(p) {
            hl.data.disk.choseList.list.push(p.item);
        },
        remove : function(p) {
            var item = p.item;
            for(var i = 0; i < hl.data.disk.choseList.list.length; i++) {
                if(item.id == hl.data.disk.choseList.list[i].id) {
                    hl.data.disk.choseList.list[i] = {};
                }
            }
        },
        clear : function() {
            hl.data.disk.choseList.list = [];
        }
    },
    share : {
        fileIds : "",
        backFunction : null,
        backFunctionP : null ,
    }

}