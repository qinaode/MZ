var hl = (hl || {});
hl.biz = (hl.biz || {});

hl.biz.login = {
    init : function() {

        var size = hl.sys.winSize();

        var hlMain = hl.sys.hl$("main");

        hlMain.style.left = 0 + "px";
        hlMain.style.top = 0 + "px";
        hlMain.style.width = size.x + "px";
        hlMain.style.height = size.y + "px";

        // 检测默认登录
        var sUser = hl.data.user.get();
        if(sUser != null) {
            if(sUser.userName != null && sUser.userName != "") {
                hl.biz.login.login({
                    id : sUser.userName,
                    pw : sUser.password
                });
                return;
            }
        }

        var bg = ssdjs.dom.img(0, 0, size.x, size.y, hl.url.img("login_backgroup.jpg"));
        hlMain.appendChild(bg);

        var _x = 0;
        var _y = 0;

        var logo = ssdjs.dom.img((size.x - 190) / 2, 100, 190, 188, hl.url.img("login/logo.png"));
        hlMain.appendChild(logo);
        _y += 394;

        // var _ww = size.x - 40 ;
        // var _hh =
        if(size.y < 960) {
            var _hh = size.y - 500;
            _y = _hh;
        }

        var _ww = 570;
        if(size.x < 640) {
            _ww = size.x * 57 / 64;
        }

        var user = hl.tpl.inputUser("user", (size.x - _ww) / 2, _y, _ww, 93);
        hlMain.appendChild(user);
        _y += 93;

        var password = hl.tpl.inputPassWord("pass", (size.x - _ww) / 2, _y, _ww, 93, "password");
        hlMain.appendChild(password);
        _y += 150;

        var loginBtn = hl.tpl.btnLogin((size.x - 573) / 2, _y, function(obj) {
            if($("#user").val() == "") {
                return hl.diag.alert({
                    m : "请输入用户名"
                });
            }
            if($("#pass").val() == "") {
                return hl.diag.alert({
                    m : "请输入密码"
                });
            }

            hl.biz.login.login({
                id : $("#user").val(),
                pw : $("#pass").val()
            });
        });

        hlMain.appendChild(loginBtn);

    },
    /*
     * 请求登录
     */
    login : function(p) {
        p = (p || {});
        console.log("login请求参数:" + JSON.stringify(p));
        var loadstat = hl.diag.loginLoading.show();

        // URL
        var url = hl.url.api("User/LoginInfoJosn");
        var data = "";
        if(p.id != null) {
            data += "txtUserNmeOrUserId=" + p.id;
        }
        if(p.pw != null) {
            data += "&txtPWD=" + p.pw;
        }

        var success = function(result) {
            hl.diag.loginLoading.hide(loadstat);
            console.log("成功");
            if(result.LoginInfo == "ErrorPwd") {
                return hl.diag.alert({
                    m : "密码错误"
                });

            }
            if(result.LoginInfo == "NoUser") {
                return hl.diag.alert({
                    m : "用户名错误"
                });
            }
            if(result.LoginInfo == "OK") {
                $("#main").hide();

                hl.data.user.set({
                    user : {
                        userName : p.id,
                        password : p.pw,
                        id : result.userId,
                        nick : result.strnick,
                        head : result.strPic,
                        say : result.strSign,
                        sex : result.sex,
                        birthday : result.birthday
                    }
                });
                // 开始心跳
                hl.biz.hb.hb.start();
                hl.biz.person.init();
            }
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
    t : function() {
        // var ss = {
        // "username" : "OK"
        // };
        //
        // var obj = jQuery.parseJSON('[{"username":"OK"},{"userid":10040}]');
        // console.log(obj);
        // console.log(JSON.stringify(ss));
        //
        // return;
        var user = hl.data.user.get();
        $.ajax({
            type : "post",
            async : true,
            dataType : "jsonp",
            jsonp : "jcb",
            jsonpCallback : "jcb_1",
            url : hl.url.api("Chat/SendMessageChatInfo"),
            data : {
                "userID" : user.id,
                "toUserId" : 100025,
                "message" : "aaaaaaaaaaaaaassssssssssssssdddddd"
            },
            beforeSend : function(XMLHttpRequest) {
                //ShowLoading();
            },
            success : function(data, textStatus) {
                console.log(data);
            },
            complete : function(XMLHttpRequest, textStatus) {
                console.log(XMLHttpRequest);
                console.log(textStatus);
            },
            error : function(XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus)
            }
        });
    },
}