var hl = (hl || {});
hl.data = (hl.data || {});

hl.data.sysMessage = {
    list : [],
    append : function(p) {
        hl.data.sysMessage.list.push({
            s : 0, // 用于标记该公告是否已读
            item : p.item
        })

        // 可加存储上限，比如只存储最近100条
        if(hl.data.sysMessage.list.length > 100) {
            hl.data.sysMessage.list.shift()
        }
    },
    /**
     * 清楚所有公告
     */
    clear : function() {

    },
    /**
     * 标为已读
     * 需要传递公告的ID
     */
    mark : function(p) {
        // 遍历数组查找指定元素，   s:0 ----> s:1

    }
};
hl.data.pub = {
    list : [],
    get : function() {
        return JSON.parse(localStorage.getItem("pubList"));
    },
    set : function(p) {
        localStorage.setItem("pubList", JSON.stringify(p.list));
    },
    update : function(p) {
        localStorage.setItem("pubList", JSON.stringify(p.list));
    },
    last : function() {
        var list = hl.data.pub.get();

        if(list != null && list.length > 0) {
            return list[list.length - 1];
        } else {
            return -1;
        }

    },
    append : function(p) {

        var list = (hl.data.pub.get() || []);

        list.push({
            s : 0, // 用于标记该公告是否已读
            item : p.item
        })

        // 可加存储上限，比如只存储最近100条
        if(hl.data.pub.list.length > 100) {
            hl.data.pub.list.shift()
        }

        localStorage.setItem("pubList", JSON.stringify(list));
    },
    /**
     * 清楚所有公告
     */
    clear : function() {

    },
    /**
     * 标为已读
     * 需要传递公告的ID
     */
    mark : function(p) {
        // 遍历数组查找指定元素，   s:0 ----> s:1

    }
}