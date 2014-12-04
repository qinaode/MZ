
//页面初始化-----------------------------------------------------------------------------------------------

var file_id = ""; //用来记录文件的id
var userid = '';
var dv_right = "dv_right"; //存放div的id;
var checkboxhtml = "<td  class='permissionCell'><input type='checkbox' checked='checked' name='resource_read'></td><td  class='permissionCell'><input type='checkbox' name='folder_create'></td><td  class='permissionCell'><input type='checkbox' name='folder_rename'></td><td  class='permissionCell'><input type='checkbox' name='folder_delete'></td><td class='permissionCell'><input type='checkbox' name='file.create'></td><td  class='permissionCell'><input type='checkbox' name='file_modify'></td><td  class='permissionCell'><input type='checkbox' name='file_rename'></td><td  class='permissionCell'><input type='checkbox' name='file_delete'></td><td  class='permissionCell'><input type='checkbox' name='permission_grant'></td>"; //获取到权限信息拼接的html代码
var headerhtml = ""; //存放右侧权限代码
var checkedname = new Array(); //存放
var userhtml = ""; //存放每一行权限html
var defaulthtml = ""; //存放默认权限对应的选择框HTML
var alluserhtml = ""; //所有用户的html,其他地方使用不用请求获 取
var sessionid = ""; //存放当前登录的用户id
var intnum = 1;
var ids = "";
var idsum = "";
$(function () {
    sessionid = $("#txtuserid").val();
    var defaulhtml = $('#defaultTable td:gt(0)').html();    // .children(":gt(0)").html();
    //alert(defaulhtml);
    //changedivleft('group');//设置左侧div的显示隐藏
    headerhtml = $("#gusertab").html(); // 赋值
    GetLeftGorup(); //加载左侧部门信息以及人员信息
    GetLeftUser(); //加载右侧分享人员信息
    // GetLeftHeaderHtml(); //获取全局配置的权限代码
   
   //带过来的参数-并选中--wj---
    var url = location.search; //?ids=XXXX;
    ids = url.substr(5);
    if (ids.length > 0) {
        idsum = ids.split(',');
        for (var i = 0; idsum.length; i++) {
            if (idsum[i].split('_')[0] == "dep") {
                $("#gcheck_" + idsum[i].split('_')[1]).attr("checked", true);
                adddeptoright(idsum[i].split('_')[1]);
            }
            else {
                $("#ucheck_" + idsum[i].split('_')[1]).attr("checked", true);
                addusertoright(idsum[i].split('_')[1]);
            }
        }
    }
})
///初始化完毕---------------------------------------------------------------------------------------------------------------

/*

*/
//以下是定义函数 

//点击删除按钮触发------------------------------------------
function deleted(id) {
    $("#tru_" + id).remove();
    $("#ucheck_" + id).attr("checked", false);

    $("#dep_" + id).remove();
    $("#gcheck_" + id).attr("checked", false);
}

//向右侧列表添加单个用户----------------------------------------------------------------------------------------
function addusertoright(id) {
    $.ajax({
        url: "/DiskN/AllUserInfoJson",
        type: "Post",
        datatype: "text/json",
        async: false,
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            userhtml = "";
            if ($("#tru_" + id).length > 0) {
            } else {
                $.each(json["list"], function (i, item) {
                    if (id == item["USERID"]) {
                        var tid = item["USERID"];
                        userhtml = userhtml + "<tr class='tr_share'  style=\"height:30px; line-height:10px;\" title='" + item["ACTUALNAME"] + "' uid='" + tid + "' id='tru_" + item['USERID'] + "' ><td style=\"width:30px; text-align:center;\"><img src='../../imagesN/sendNotice/users.png'/></td><td style=\" text-align:left; padding-bottom:0px;\">";
                        userhtml = userhtml + item["ACTUALNAME"] + "</td><td style=\"width:30px; text-align:center;\"><a href='#'  num='" + tid + "' class='LI_user' torf='true' id='a_" + tid + "' onclick='deleted(" + tid + ")'><img src='../../imagesN/sendNotice/dele.png'/></a></td>";
                        //                        userhtmladd(tid);
                    }
                });
            }
            $("#gusertab").children().append(userhtml);

        }
    });
}

//向右侧列表添加单个部门----------------------------------------------------------------------------------------
function adddeptoright(id) {
    $.ajax({
        url: "/DiskN/GetAllDepInfoJson",
        type: "Post",
        datatype: "text/json",
        async: false,
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            userhtml = "";
            if ($("#dep_" + id).length > 0) {
            } else {
                $.each(json["list"], function (i, item) {

                    if (id == item["id"]) {
                        var tid = item["id"];
                        userhtml = userhtml + "<tr class='tr_share'  style=\"height:30px; line-height:10px;\" title='" + item["name"] + "' uid='" + tid + "' id='dep_" + item['id'] + "' ><td style=\"width:30px; text-align:center;\"><img src='../../imagesN/sendNotice/group.png'/></td><td style=\" text-align:left; padding-bottom:0px;\">";
                        userhtml = userhtml + item["name"] + "</td><td style=\"width:30px; text-align:center;\"><a href='#'  num='" + tid + "' class='LI_user' torf='true' id='a_" + tid + "' onclick='deleted(" + tid + ")'><img src='../../imagesN/sendNotice/dele.png' /></a></td>";

                    }
                });
            }
            $("#gusertab").children().append(userhtml);

        }
    });
}

//左侧部门选择框改变触发--------------------------------------------------------
//function groupcheckchange(depId,type) {
//    var up = $("#gcheck_" + depId);
//    if (up.attr("checked") != undefined & up.attr("checked") == true) {
//        if ($("#dep_" + depId).length > 0) {
//            return;
//        } else {                
//            adddeptoright(depId); //向右侧插入该部门
//        }

//    } else {
//        //        var sss = $("#gusertab").children("");
//        $("#dep_" + depId).remove();
//        //alert(sss.html());//移除对应右边的部门
//    }
//}


//左侧用户列表每个用户勾选框点击----------------------------
function usercheckchange(id) {
    var up = $("#ucheck_" + id);
    if (up.attr("checked") != undefined & up.attr("checked") == true) {
        if ($("#tru_" + id).length > 0) {
            return;
        } else {
            addusertoright(id); //向右侧插入该用户
        }

    } else {
        //        var sss = $("#gusertab").children("");
        $("#tru_" + id).remove();
        //alert(sss.html());//移除对应右边的用户
    }

}

// 点击确认------------------------------------------------
function btnOK() {
    //var obj = GetObj();//这个方法就是你获取到的数据
    var html = userAlltoEmail();
    var htmlids = sending();
    window.parent.document.getElementById("txtRange").innerHTML = html;
    window.parent.document.getElementById("ids").value = htmlids;
    closeWindow();
}

// 获得Ids------------------------------------------------
function sending() {
    var strpower = "";
    var strids = "";
    var sd = $("#gusertab .tr_share");
    sd.each(function () {
        var tid = $(this).attr("id");

        strids = strids + tid;
        strids = strids + ",";
    });
    //    strpower = strpower.substring(0, strpower.length - 1);
    strids = strids.substring(0, strids.length - 1);
    return strids;

}

//=============获得数据===========
function userAlltoEmail() {
    var XXuserhtml = "";
    var sd = $("#gusertab .tr_share");
    sd.each(function () {

        XXuserhtml = XXuserhtml + " <div class='resultvalue' style=\"vertical-align:middle; margin-top:5px; background:#e5e5e5; padding:0px 5px; margin:2px 7px 2px 0px; line-height:22px; float:left;\"><a href='javascript:void(0);' style=\"text-decoration:none;\" id='" + $(this).attr("id") + "' title='" + $(this).attr("title") + "'>";
        XXuserhtml = XXuserhtml + "<span style=\"color:Gray;\" >" + $(this).attr("title") + "</span><span class='addr_del' ck='del' onclick='div_deleted(" + $(this).attr("id") + ",event)'><img src='../../imagesN/sendNotice/dele.png' /></span></a></div>";
        XXuserhtml = XXuserhtml + "";
    });

    return XXuserhtml;
}

//切换左侧div显示隐藏-----------------------------------------------------
function changedivleft(name) {

    if (name == "user") {
        $("#txtbumen").css("color", "white");
        $("#txtrenyuan").css("color", "black");
        $(".member").css("background-image", "url(/imagesN/sharepage/title1.png)");
        $(".bumen").css("background-image", "url(/imagesN/sharepage/title2.png)");
        // background: url(/imagesN/sharepage/title2.png) no-repeat;
        $("#dv_left_u").css("display", "block");
        $("#dv_left_group").css("display", "none");
    } else {
        $("#txtbumen").css("color", "black");
        $("#txtrenyuan").css("color", "white");
        $(".bumen").css("background-image", "url(/imagesN/sharepage/title1.png)");
        $(".member").css("background-image", "url(/imagesN/sharepage/title2.png)");
        $("#dv_left_group").css("display", "block");
        $("#dv_left_u").css("display", "none");
    }

}

//不同分组的checkbox的变化----------------------------------------
//function checkall(istrue, grouporuser, id) {
//    if (grouporuser == "group") {
//        if (istrue) {
//            $("#dv_left_group :checkbox[pid=" + id + "]").attr("checked", true);
//        } else {
//            $("#dv_left_group :checkbox[pid=" + id + "]").attr("checked", false);
//        }
//    } else if (grouporuser == "user") {
//        if (istrue) {
//            $("#dv_left_user :checkbox[pid=1]").attr("checked", true);
//        } else {
//            $("#dv_left_user :checkbox[pid=1]").attr("checked", false);
//        }
//    } else {
//        alert('Par Is User Or Group');
//    }
//}
///左侧部门div数据展示------------------------------------------------------


//左侧用户div数据展示-------------------------------------------------------------------------------------------------------------
function GetLeftUser() {
    $.ajax({
        url: "/DiskN/AllUserInfoJson",
        type: "Post",
        datatype: "text/json",
        async: false,
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            users = "";
            users = users + "<div class='fenlei'style='overflow: auto; width=100%;' ><table> ";
            $.each(json["list"], function (i, item) {
                users = users + "<tr><td>&nbsp;</td><td  width='18px' height='25px' valign='middle'><input type='checkbox' pid='1' id='ucheck_" + item["USERID"] + "'";
                users = users + " onclick=' usercheckchange(" + item["USERID"] + ")' ></input></td><td valign='middle'><label >";
                users = users + item["ACTUALNAME"];
                users = users + "</label></td></tr>";
            });
            users = users + "</table></div>";
            $("#dv_left_user").html(users);
        }
    });
    setTimeout(function () { }, 1000);
}
//点击搜索按钮--------------------------------------------
function userserch(th) {
    var serchval = $.trim($("#txt_serch").val());
    changedivleft('user');
    if (serchval != '请输入姓名' & serchval != '') {
        $.ajax({
            url: "/DiskN/SearchUserInfoJson?strName=" + serchval,
            type: "Post",
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "") {

                } else {
                    var json = $.parseJSON(backdata);
                    var temp = 1;
                    $.each(json["list"], function (i, item) {
                        // alert($("#ucheck_" + item["USERID"]).attr("checked"));
                        if (temp <= 1) {
                            $("#ucheck_" + item["USERID"]).parent().parent().siblings().css("display", "none");
                        }
                        $("#ucheck_" + item["USERID"]).parent().parent().css("display", "block");
                        temp = temp + 1;
                    });
                }
            }
        })
    } else if (serchval == '') {
        $("#dv_left_user :checkbox[pid=1]").parent().parent().css("display", "block");
    }
}
//搜索框失去焦点-----------------------------------------------------
function serchblur() {
    $("#txt_serch").css("color", "Gray");
    var serchval = $.trim($("#txt_serch").val());
    if ($.trim($("#txt_serch").val()) == '') {
        $("#txt_serch").css("color", "Gray");
        $("#txt_serch").val('请输入成员...');
        $("#dv_left_user :checkbox[pid=1]").parent().parent().css("display", "block");
    } else {

    }
}
//搜索框获得焦点------------------------------------------------------
function serchfocus() {
    $("#txt_serch").css("color", "Black");
    if ($.trim($("#txt_serch").val()) == '请输入成员...') {
        $("#txt_serch").val('');
    }
}

//根据pid折叠对应的子部门------------------------------------------------------------------------------------------------
//function FoldByPid(pid) {
//    var eachtd = $("#dv_left_group :checkbox[pid=" + pid + "]");
//    eachtd.each(function () {
//        if ($(this).parent().parent().css("display") != undefined & $(this).parent().parent().css("display") == "none") {
//            $(this).parent().parent().css("display", "block");
//        } else {
//            $(this).parent().parent().css("display", "none");
//        }
//    })
//}


//按钮切换事件-------------------------
function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}

function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}
//---------------------------------------------------

//根据pid折叠对应的子部门------------------------------------------------------------------------------------------------
function FoldByPid(pid) {
    $('#tree_' + pid).toggleClass("tree-collapsed");
    // $('#tree_ul_trg_' + pid).toggleClass("tree-expanded");
    var treeul = $('#tree_ul_trg_' + pid);
    if (treeul.css("display") == undefined || treeul.css("display") == "none") {
        treeul.css("display", "block");
    } else {
        treeul.css("display", "none");
    }
}





//左侧部门选择框改变触发---原来的方法-----------------------------------------------------
/*
function groupcheckchange(depId, typ) {
    var childbox = $("#dv_left_group :checkbox[pid=" + depId + "]");
    var thisckbox = $("#gcheck_" + depId);
    if (typ == 'root') {
        //向右侧加载所有人员
        if (thisckbox.attr("checked") != undefined & thisckbox.attr("checked") == true) {
            duiguicheckgroup(childbox, 'true');
            checkall(true, 'user', 1);
        } else {
            duiguicheckgroup(childbox, 'false');
            checkall(false, 'user', 1);
        }
    } else {  //子部门触发
        var pid1 = thisckbox.attr("pid");
        var pchecbox = $("#gcheck_" + pid1);
        var res = true;
        if (thisckbox.attr("checked") == true) {
            duiguicheckgroup(childbox, 'true');

            changeuserbydepart(depId, "add"); //改变右侧人员   
        } else {
            res = false;
            duiguicheckgroup(childbox, 'false');
            changeuserbydepart(depId, "delete");
            if (pchecbox.attr("checked") != undefined & pchecbox.attr("checked") == true) {
                //                pchecbox.attr("checked", false)
                cancelpboxchecked(pid1);
            } //如果父部门选中取消选中
        }
    }
}
*/
//从新写的方法============bywj===3.31================

function groupcheckchange(depId, typ) {//depID=28
    var thisckbox = $("#gcheck_" + depId);
    if (typ == 'root') {//根节点
        //向右侧加载部门并使得所有子部门的checkbox为不可使用
        var childbox = $("#groupul :checkbox");
        if (thisckbox.is(":checked")) {
            $("#gusertab tbody").html("")
            duiguicheckgroup(childbox, 'true');
            adddeptoright(depId);

        } else {
            duiguicheckgroup(childbox, 'false');
            deleted(depId);
//            var deplist = $("#dv_left_group :checkbox[pid=" + depId + "]");
//            deplist.each(function () {
//                if ($(this).is(":checked")) {
//                    var depid = $(this).attr("ckid");
//                    adddeptoright(depid);
//                }
//            });
        }
    } else {  //子部门触发，向右侧加载部门并使得所有子子部门的checkbox为不可使用
        var pid1 = thisckbox.attr("pid"); //tree_ul_trg_28   =1
        var pchecbox = $("#gcheck_" + pid1);  //父结点
        var childbox1 = $("#dv_left_group :checkbox[pid=" + depId + "]");
        //alert(thisckbox.is(":checked"));
        if (thisckbox.is(":checked")) {
            duiguicheckgroup(childbox1, 'true');
            adddeptoright(depId);
        } else {
            deleted(pid1); 
            duiguicheckgroup(childbox1, 'false');
            pchecbox.attr("checked", false);
            cancelpboxchecked(pid1);
            //如果父部门选中取消选中
            var deplist = $("#dv_left_group :checkbox[pid=" + pid1 + "]");
            deplist.each(function () {
                if ($(this).is(":checked")) {
                    var depid = $(this).attr("ckid");
                    adddeptoright(depid);
                }
                else {
                    var depid = $(this).attr("ckid");
                    deleted(depid);
                }
            });
        }
    }
};

//取消父部门选中----------------
function cancelpboxchecked(tid) {
        var pchecbox = $("#gcheck_" + tid);
        pchecbox.attr("checked", false);
        if (pchecbox.attr("pid") != undefined) {
            var pid1 = pchecbox.attr("pid");
            cancelpboxchecked(pid1)
        }
}
//递归选中子部门选框-------------------------
function duiguicheckgroup(childbox, torf) {
    if (torf == 'true') {
        childbox.each(function () {  //把所有该部门下的子部门选中
            $(this).attr("checked", true);
            var ckid = $(this).attr("ckid");
            var childbox1 = $("#dv_left_group :checkbox[pid=" + ckid + "]");
            duiguicheckgroup(childbox1, torf);

        })
    } else {
        childbox.each(function () {  //把所有该部门下的子部门选中
            $(this).attr("checked", false);
            var ckid = $(this).attr("ckid");
            var childbox1 = $("#dv_left_group :checkbox[pid=" + ckid + "]");
            duiguicheckgroup(childbox1, torf);
        })
    }

}

//根据部门向右侧列表中插入用户（并判断）-----------------------------------------------------------
function changeuserbydepart(id, action) {
    $.ajax({
        url: "/DiskN/GetAllDepInfoJson",
        type: "Post",
        datatype: "text/json",
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            userhtml = "";
            if (action == "delete") {
                $("#dep_" + id).remove();
                $("#gcheck_" + id).attr("checked", false);
            }
            else {
                if ($("#dep_" + id).length > 0) {
                } else {
                    $.each(json["list"], function (i, item) {
                        if (id == item["id"]) {
                            var tid = item["id"];
                            userhtml = userhtml + "<tr class='tr_share'  style=\"height:30px; line-height:10px;\" title='" + item["name"] + "' uid='" + tid + "' id='dep_" + item['id'] + "' ><td style=\"width:30px; text-align:center;\"><img src='../../imagesN/sendNotice/group.png'/></td><td style=\" text-align:left; padding-bottom:0px;\">";
                            userhtml = userhtml + item["name"] + "</td><td style=\"width:30px; text-align:center;\"><a href='#'  num='" + tid + "' class='LI_user' torf='true' id='a_" + tid + "' onclick='deleted(" + tid + ")'><img src='../../imagesN/sendNotice/dele.png' /></a></td>";
                        }
                    });
                }
                $("#gusertab").children().append(userhtml);
            }
        }
    });
}


///左侧部门div数据展示------------------------------------------------------
function GetLeftGorup() {
    $.ajax({
        url: "/DiskN/AllDepInfoJson",
        type: "Post",
        datatype: "text/json",
        async: false,
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            group = "<div id='groupul' ><ul style='display: block' class='easyui-tree tree'> ";
            group = group + "<li style='display:block;'> <div  class='tree-node'><a href='#'   onclick='FoldByPid(" + json["id"] + ")'><span id='tree_" + json["id"] + "' class='tree-hit tree-expanded'></span></a>";
            group = group + "<span class='tree-icon tree-folder' > <input type='checkbox' pid='" + json["pid"] + "' ckid='" + json["id"] + "'  id='gcheck_" + json["id"] + "' onclick=' groupcheckchange(" + json["id"] + ",\"root\")' /></span> ";
            group = group + "<span  class='tree-title' >" + json["name"] + "</span></div><ul  id='tree_ul_trg_" + json["id"] + "' style='display: block;'>";
            $.each(json["list"], function (i, item) {
                digui(item, json["id"], intnum); //递归加载部门信息
            });
            group = group + "</ul></li></ul></div>";
            $("#dv_left_group").html(group);
            alluserhtml = group; //将生成的所有人员的html代码保存起来，搜索的时候，不用请求了。
        }
    });
}

function digui(item, pid, inum) {
    var intceng = inum;
    if (item["list"] != undefined) {
        group = group + "<li> <div  class='tree-node'>";
        for (var i = 0; i < intceng; i++) {
            group = group + "<span  class='tree-indent'></span>";
        }
        group = group + "<a href='#' id='" + item["id"] + "' onclick='FoldByPid(" + item["id"] + ")'><span id='tree_" + item["id"] + "'  class='tree-hit tree-expanded  tree-collapsed'></span></a>";
        group = group + "<span class='tree-icon tree-folder'><input type='checkbox' pid='" + pid + "'  ckid='" + item["id"] + "' id='gcheck_" + item["id"] + "'";
        group = group + " onclick='groupcheckchange(" + item["id"] + ",\"node\")' /></span><span  class='tree-title'>";
        group = group + item["name"];
        group = group + "</span></div>";
        group = group + "<ul  id='tree_ul_trg_" + item["id"] + "'  style='display:none;'>"; //显示第二层
        intceng = intceng + 1;
        $.each(item["list"], function (j, itemj) {

            digui(itemj, item["id"], intceng);
        });
        group = group + " </ul></li>";
    } else {
        group = group + "<li><div class='tree-node'>";
        for (var i = 0; i < intceng; i++) {
            group = group + "<span  class='tree-indent'></span>";
        }
        group = group + "<span  class='tree-indent'></span>";
        group = group + "<span class='tree-icon tree-folder'><input type='checkbox' pid='" + pid + "' ckid='" + item["id"] + "' id='gcheck_" + item["id"] + "'";
        group = group + " onclick='groupcheckchange(" + item["id"] + ",\"node\")' /></span><span class='tree-title' >";
        group = group + item["name"];
        group = group + "</span></div></li>";
    }
    //group = group + " </li>";
}
