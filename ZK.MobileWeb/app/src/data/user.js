var hl = (hl || {});
hl.data = (hl.data || {});

hl.data.user = {
    id : 0,
    loadStat : 0,
    user : {
        userName : "",
        password : -1,
        id : 0,
        nick : "",

        sex : 1,
        group : [{
            n : "皓联", // nick
            i : 0		 // id
        }]
    },

    get : function() {
        return JSON.parse(localStorage.getItem("userInfo"));
    },
    set : function(p) {
        localStorage.setItem("userInfo", JSON.stringify(p.user));
    },
    // 返回子部门列表
    listTest : [{
        n : "销售部", // nick
        i : 1		 // id
    }, {
        n : "软件部",
        i : 2
    }],

    //返回部门成员列表

    list : [{
        i : 111, // id
        s : 1, // sex
        n : "张三"		// nick
    }, {
        i : 1121,
        s : 0,
        n : "李四"
    }]

}