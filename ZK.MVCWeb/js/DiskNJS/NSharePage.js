
/*
1. 加载左侧部门信息               GetLeftGorup
2.加载左侧人员信息                 GetLeftUser
3. 右侧删除按钮事件               deleted
4. 向右侧添加或删除所有人员      ChangeRightUsers
5. 向右侧添加或删除一个人员        addusertoright
6.根据配置的权限生成对应的权限html  userhtmladd
7.部门选择框改变事件逻辑处理        groupcheckchange
8.向右侧添加或删除某个部门人员    changeuserbydepart
9.人员选择框改变事件逻辑处理        usercheckchange
10.屏蔽鼠标事件                       ForBindMouse 
11.点击确认共享                       shareing
12.分享并设置权限                     Postsharefolder
13.切换部门人员显示                  changedivleft 
14. 选中所有 box                    checkall  
15.根据pid折叠对应的子部门          FoldByPid
16.点击搜索按钮                    userserch 
17.搜索框失去焦点                  serchblur 
18.搜索框获得焦点                serchfocus
..
*/
//页面初始化-----------------------------------------------------------------------------------------------
var file_id = ""; //用来记录文件的id
var sessionid = ""; //存放当前登录的用户id
var file_type = "";
var dv_right = "dv_right"; //存放div的id;
var headerhtml = ""; //存放右侧权限代码
var checkedname = new Array(); //存放
var defaultchecname = new Array();
var userhtml = ""; //存放每一行权限html
var defaulthtml = ""; //存放默认权限对应的选择框HTML
var alluserhtml = ""; //所有用户的html,其他地方使用不用请求获 取
var temp = ""; //存放中间数据
var intnum = 1;
var inttemp = 0;
var alluserjson = ""; //缓存所有用户的信息
var shareduserjson = ""; //缓存以共享的用户信息（未使用） 
var configpowercheck = new Array(); //根据权限checkbox分配的值     :"1"为选中有权限  "0"为未选中无权限 共 9 个
var possesspowercheck = new Array(); //根据权限checkbox分配的值     :"1"为选中有权限  "0"为未选中无权限 共 9 个
var configfirstdata=new Array();//存放权限初始化数据
$(function () {
    // ForBindMouse("false"); //禁用鼠标的事件
    file_id = $("#txtuserid").attr("fileid");
    sessionid = $("#txtuserid").val(); //获取当前登录的用户
//    file_type= $("#txtfiletype").val();
    // var defaulhtml = $('#defaultTable td:gt(0)').html();
    headerhtml = $("#gusertab").html(); // 赋值
    configcheckadd();
    getalluserjson(); //把请求的用户信息缓存起来
    GetLeftGorup(); //加载左侧部门信息以及人员信息
    GetLeftUser(); //加载右侧分享人员信息
    Getsharedpower(); //加载已经共享的人员权限信息
    configload();
})
//--------
function configload() {
    var i = 0;
    if (configfirstdata.length > 0 & file_type!="0") {
        configpowercheck.length = 0;
        var eachtd = $("#tr_Defaul :checkbox");
        eachtd.each(function () {
           
            if (configfirstdata[i] == "1") {
               
                configpowercheck.push(" checked='checked' ");
                $(this).attr("checked", true);
            } else {
                configpowercheck.push(" ");
                $(this).attr("checked", false);
            }
            i = i + 1;
        })
    } 
}
//-------
function configcheckadd() {
    configpowercheck.length = 0;
    configpowercheck.push(" checked='checked' ");
    for (var i = 1; i < 9; i++) {
         configpowercheck.push(" ");
    }
}
//---------------------------------------------------------初始化完毕-------------------------------------------------------------------------- 
//获取所有人员信息缓存起来-------------------------------------
function getalluserjson() {
    $.ajax({
        url: "/DiskN/AllUserInfoJson?username="+sessionid,
        type: "Post",
        datatype: "text/json",
        async: false,
        success: function (backdata) {
            alluserjson = backdata;
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
//递归数据拼接--------------------
//function digui(item, pid, inum) {
//    if (item["list"] != undefined) {
//        group = group + "<li> <div id='_easyui_tree_" + inum + "' class='tree-node'><a href='#' id='" + item["id"] + "' onclick='FoldByPid(" + item["id"] + ")'><span id='tree_" + item["id"] + "'  class='tree-hit tree-expanded  tree-collapsed'></span></a>";
//        group = group + "<input type='checkbox' pid='" + pid + "'  ckid='" + item["id"] + "' id='gcheck_" + item["id"] + "'";
//        group = group + " onclick='groupcheckchange(" + item["id"] + ",\"node\")' ></input><label >";
//        group = group + item["name"];
//        group = group + "</label></div>";
//        group = group + "<ul  id='tree_ul_trg_" + item["id"] + "'  style='display:none;'>";
//        $.each(item["list"], function (j, itemj) {
//            digui(itemj, item["id"], inum);
//        });
//        group = group + " </ul></li>";
//    } else {
//        group = group + "<li><div><span class='tree-indent'></span><input type='checkbox' pid='" + pid + "' ckid='" + item["id"] + "' id='gcheck_" + item["id"] + "'";
//        group = group + " onclick='groupcheckchange(" + item["id"] + ",\"node\")' ></input><label >";
//        group = group + item["name"];
//        group = group + "</label></div></li>";
//    }
//    //group = group + " </li>";
//}
function digui(item, pid, inum) {
    var intceng = inum;
    if (item["list"] != undefined) {//记载的有子部门的部门
        group = group + "<li> <div  class='tree-node'>";
        for (var i = 0; i < intceng; i++) {
            group = group + "<span  class='tree-indent'></span>";
        }
        group = group + "<a href='#' id='" + item["id"] + "' onclick='FoldByPid(" + item["id"] + ")'><span id='tree_" + item["id"] + "'  class='tree-hit tree-expanded  tree-collapsed'></span></a>";
        group = group + "<span class='tree-icon tree-folder'><input type='checkbox' pid='" + pid + "'  ckid='" + item["id"] + "' id='gcheck_" + item["id"] + "'";
        group = group + " onclick='groupcheckchange(" + item["id"] + ",\"node\")' /></span><span  class='tree-title'>";
        group = group + item["name"];
        group = group + "</span></div>";
        group = group + "<ul  id='tree_ul_trg_" + item["id"] + "'  style='display:none;'>";//显示第二层
        intceng = intceng + 1;
        $.each(item["list"], function (j, itemj) {
           
            digui(itemj, item["id"], intceng);
        });
        group = group + " </ul></li>";
    } else {//加载最终子部门
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
//左侧用户div数据展示-------------------------------------------------------------------------------------------------------------
function GetLeftUser() {
    var json = $.parseJSON(alluserjson);
    users = "";
    users = users + "<table> ";
    $.each(json["list"], function (i, item) {
        users = users + "<tr><td>&nbsp;&nbsp;</td><td  width='18px' height='25px' valign='top'><input type='checkbox' pid='1' id='ucheck_" + item["USERID"] + "'";
        users = users + " onclick=' usercheckchange(" + item["USERID"] + ")' /></td><td valign='top'><label >";
        users = users + item["ACTUALNAME"];
        users = users + "</label></td></tr>";
    });
    users = users + "</table></div>";
    $("#dv_left_user").html(users);
}
//点击删除按钮触发------------------------------------------
function deleted(id) {
    $("#tru_" + id).remove();
    $("#ucheck_" + id).attr("checked", false);
}
//添加所有人到右侧列表----------------------------------------------------------------------------------------------
function ChangeRightUsers(action) {
    var json = $.parseJSON(alluserjson);
    userhtml = "";
    if (action == "add") {
        $.each(json["list"], function (i, item) {
            var tid = item['USERID'];
            userhtml = userhtml + "<tr  class='tr_share' uid='" + tid + "' id='tru_" + item['USERID'] + "' ><td>";
            userhtml = userhtml + item["ACTUALNAME"];
            userhtml = userhtml + "</td>";
            //userhtml = userhtml + checkboxhtml;
            userhtmladd(tid,configpowercheck,"config");
        });
        $("#gusertab").html(headerhtml + userhtml);
    } else if (action == "delete") {
        $("#gusertab").html(headerhtml);
    } else {
    }
};
//向右侧列表添加单个用户----------------------------------------------------------------------------------------
function addusertoright(id, checkboxs) {
   
    var json = $.parseJSON(alluserjson);
    userhtml = "";
    if ($("#tru_" + id).length > 0) {
    } else {
    $.each(json["list"], function (i, item) {
        if (id == item["USERID"]) {
            var tid = item["USERID"];
            userhtml = userhtml + "<tr class='tr_share' uid='" + tid + "' id='tru_" + item['USERID'] + "' ><td width='114' height='30' align='center'>";
            userhtml = userhtml + item["ACTUALNAME"] + "</td>";
            //  var checkboxs = new Array();
            //defaultchecname.push("1");
            //for (var i = 1; i < 9; i++) {
            // defaultchecname.push("0");
            // }
            userhtmladd(tid, configpowercheck, "config");
           
        }
    });

    }
    $("#gusertab").children().append(userhtml);
}
//遍历权限数组生成权限勾选框的HTML---------------
function userhtmladd(tid, checkboxs, action) {//需要一个标识是否为加载自己的权限 // 1.config 2.read
    var checkmodel = new Array();
    checkmodel = checkboxs;
   
    var resource_read = "";
    var folder_create = ""; var folder_rename = "";
    var folder_delete = ""; var file_create = "";
    var file_modify = ""; var file_rename = "";
    var file_delete = ""; var permission_grant = "";
    var ck = " checked='checked' ";
    var ce = " disabled='disabled' checked='checked' " //eachpowercheck
    resource_read = checkmodel[0];
    folder_create = checkmodel[1];
    folder_rename = checkmodel[2];
    folder_delete = checkmodel[3];
    file_create = checkmodel[4];
    file_modify = checkmodel[5];
    file_rename = checkmodel[6];
    file_delete = checkmodel[7];
    permission_grant = checkmodel[8];
    userhtml = userhtml + "<td  align='center' class='permissionCell'><input type='checkbox'  ";
    userhtml = userhtml + resource_read;
    userhtml = userhtml + " name='resource.read'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + folder_create;
    userhtml = userhtml + " id='folder.create" + tid + "'  name='folder.create'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + folder_rename;
    userhtml = userhtml + " id='folder.rename" + tid + "' name='folder.rename'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + folder_delete;
    userhtml = userhtml + " id='folder.delete" + tid + "'  name='folder.delete'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + file_create;
    userhtml = userhtml + " id='file.create" + tid + "' name='file.create'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + file_modify;
    userhtml = userhtml + " id='file.modify" + tid + "' name='file.modify'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + file_rename;
    userhtml = userhtml + "  id='file.rename" + tid + "' name='file.rename'></td><td width='40' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + file_delete;
    userhtml = userhtml + "  id='file.delete" + tid + "' name='file.delete'></td><td width='30' align='center' class='permissionCell'><input type='checkbox'";
    userhtml = userhtml + permission_grant;
    userhtml = userhtml + " id='permission.grant" + tid + "' name='permission.grant'>";
    userhtml = userhtml + "</td><td '><a align='center' href='#'  num='" + tid + "' class='LI_user' torf='true' id='a_" + tid + "' onclick='deleted(" + tid + ")'>"
    userhtml = userhtml + "<img src='/css/DiskN/images/delete.png' alt='删除'></img></a>";
    userhtml = userhtml + "</td>";
    userhtml = userhtml + "</tr>";
    return userhtml;
}
//初始化加载人员权限
function Getsharedpower() {
    $.ajax({
        url: "/DiskN/GetDefaulAccessData",
        type: "Post",
        datatype: "text/json",
        async: false,
        success: function (backdata) {
            shareduserjson = backdata;
            var fenzucon = backdata.split('@');
            defaultconfigfirstadd(fenzucon[0]);
            var splidata = fenzucon[1];
            userhtml = "";
            var arr = splidata.split(',');
            for (var i = 0; i < arr.length; i++) {
                possesspowercheck.length = 0;
                var tep = arr[i].split('_'); //存放的是每一个人对应信息权限
                file_type = tep[0];
              
                if (file_type == "0") {
                    configpowercheck.length = 0;
                    for (var i = 0; i < 9; i++) {
                        configpowercheck.push(" disabled='disabled' checked='checked'");
                    }
                  
                    return false;
                }
               
                if (tep.length <= 1 || file_type == "1") {//未找到以共享的人,文件未被共享过
                    return false;
                }
                var usid = tep[1];
                var usname = tep[2];
                for (var j = 3; j < tep.length; j++) {
                    if (file_type == "0") {
                        possesspowercheck.push("checked='checked' disabled='disabled'");
                    } else if (file_type == "2") {
                        $("#ucheck_" + usid).attr("checked", true);
                        if (tep[j] == 1) {
                            possesspowercheck.push("checked='checked'");
                        } else {
                            possesspowercheck.push(" ");
                        }
                    }
                }
                var ckecks = possesspowercheck;
                userhtml = userhtml + "<tr class='tr_share' uid='" + usid + "' id='tru_" + usid + "' ><td width='114' height='30' align='center'>";
                userhtml = userhtml + usname + "</td>";
                userhtmladd(usid, ckecks, "read");
                $("#gusertab").children().append(userhtml);
                userhtml = "";
            }
        }
    });
}
//向权限配置表格中初始化数据-------------------------------------
function defaultconfigfirstadd(ckeckdata) {
   // ckeckdata = "default_1_0_1_0_0_0_0_0_0";//测试数据
    var tempdata = ckeckdata.split('_');
    for (var i = 0; i < tempdata.length; i++) {
        if (i>0) {
            //首先向权限表格中设置数据
            configfirstdata.push(tempdata[i]);
            
        }
    }
}
//左侧部门选择框改变触发--------------------------------------------------------
function groupcheckchange(depId, typ) {
    var childbox = $("#dv_left_group :checkbox[pid=" + depId + "]");
    var thisckbox = $("#gcheck_" + depId);
    if (typ == 'root') {
        //向右侧加载所有人员
        if (thisckbox.attr("checked") != undefined & thisckbox.attr("checked") == true) {
            duiguicheckgroup(childbox, 'true');
            ChangeRightUsers('add');
            checkall(true, 'user', 1);
        } else {
            ChangeRightUsers('delete');
            duiguicheckgroup(childbox, 'false');
            checkall(false, 'user', 1);
        }
    } else {  //子部门触发
       //alert(222);
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
                pchecbox.attr("checked", false)
                cancelpboxchecked(pid1);
            } //如果父部门选中取消选中
        }
    }
}
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
            var id = $(this).attr("ckid");
            if (id != undefined) {
                var bumen = $("#dv_left_group :checkbox[pid=" + id + "]");
                duiguicheckgroup(bumen, 'true')
            }
        })
    } else {
        childbox.each(function () {  //把所有该部门下的子部门选中
            $(this).attr("checked", false);
            var id = $(this).attr("ckid");
            if (id != undefined) {
                var bumen = $("#dv_left_group :checkbox[pid=" + id + "]");
                duiguicheckgroup(bumen, 'false')
            }
        })
    }
}
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

//根据部门向右侧列表中插入用户（并判断）-----------------------------------------------------------
function changeuserbydepart(id, action) {
    $.ajax({
        url: "/DiskN/UserOfDepInfoJson?depId=" + id + "&username=" + sessionid,
        type: "Post",
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "") {
                return false;
            }
            var json = $.parseJSON(backdata);
            userhtml = "";
            $.each(json["list"], function (i, item) {
                if (action == "delete") {
                    $("#tru_" + item["id"]).remove();
                    $("#ucheck_" + item["id"]).attr("checked", false);
                } else {
                    if ($("#tru_" + item["id"]).length > 0) {
                    } else {
                        var tid = item["id"];
                        $("#ucheck_" + item["id"]).attr("checked", true);
                        userhtml = userhtml + "<tr class='tr_share' uid='" + tid + "' id='tru_" + item['id'] + "' ><td width='114' height='30' align='center'>";
                        userhtml = userhtml + item["name"] + "</td>";
                        // userhtml = userhtml + checkboxhtml;
                        userhtmladd(tid, configpowercheck, "config");
                    }
                }
            });
            $("#gusertab").children().append(userhtml);
        }
    });
}
//左侧用户列表每个用户勾选框点击----------------------------
function usercheckchange(id) {

    var up = $("#ucheck_" + id);
    if (up.attr("checked") != undefined & up.attr("checked") == true) {
        if ($("#tru_" + id).length > 0) {
            return;
        } else {
            checkedname.push("resource_read");
        
            addusertoright(id, checkedname); //向右侧插入该用户
        }
    } else {
        $("#tru_" + id).remove(); //移除对应右边的用户
    }

}
//屏蔽鼠标事件------------------ 
function ForBindMouse(bool) {
    //    document.oncontextmenu = new Function("event.returnValue=" + bool + ";")
    //    document.onselectstart = new Function("event.returnValue=" + bool + ";");
}
// 点击确认共享------------------------------------------------
function shareing(fid) {
   
    var strpower = "";
    var sd = $("#gusertab .tr_share");
    //alert(sd.length);
    if (sd.length != 0) {
        sd.each(function () {
            var tid = $(this).attr("id");
            //var tr_chiled = $(this).children(" :checkbox");
            var tr_chiled = $("#" + tid + " :checkbox");
            strpower = strpower + $("#" + tid).attr("uid");
            if (file_type != '0') {
                tr_chiled.each(function () {
                    if ($(this).attr("checked") != undefined & $(this).attr("checked") == true) {
                        strpower = strpower + "_1";
                    } else {
                        strpower = strpower + "_0";
                    }
                })
            }
            strpower = strpower + ",";
        });
        strpower = strpower.substring(0, strpower.length - 1);
        Postsharefolder(strpower, fid, file_type);
       
    } else {
        Postsharefolder("", fid, file_type);
       
    }
}
// 点击确认共享------------------------------------------------
function nshareing(fid) {

    var strpower = "";
    var sd = $("#gusertab .tr_share");
    //alert(sd.length);
    if (sd.length != 0) {
        sd.each(function () {
            var tid = $(this).attr("id");
            //var tr_chiled = $(this).children(" :checkbox");
            var tr_chiled = $("#" + tid + " :checkbox");
            strpower = strpower + $("#" + tid).attr("uid");
            if (file_type != '0') {
                tr_chiled.each(function () {
                    if ($(this).attr("checked") != undefined & $(this).attr("checked") == true) {
                        strpower = strpower + "_1";
                    } else {
                        strpower = strpower + "_0";
                    }
                })
            }
            strpower = strpower + ",";
        });
        strpower = strpower.substring(0, strpower.length - 1);
        NPostsharefolder(strpower, fid, file_type);

    } else {
       NPostsharefolder("", fid, file_type);

    }
}
//把文件夹分享给一个人，同时设置权限-------------------------------------------------------------------------
function Postsharefolder(strpower, fid, ftype) {
    if (ftype == "1" || ftype == "2") {
        ftype = 1;
    } else {

    }
    $.ajax({
        url: "/DiskN/ShareFolderInfoJson?userName=" + sessionid + "&shareIdsAndGrants=" + strpower + "&folderIds=" + fid + "&flag=" + ftype,
        type: "Post",
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == '1') {
                checkedname.length = 0;
                
                alert('共享成功!');
                closewindow('true');
            } else {
                checkedname.length = 0;
                alert('共享失败！');
            }
        }
    });
}
//为了不影响之前的操作，添加一组方法过渡。
function NShareing(fid) {

    var strpower = "";
    var sd = $("#gusertab .tr_share");
    //alert(sd.length);
    if (sd.length != 0) {
        sd.each(function () {
            var tid = $(this).attr("id");
            //var tr_chiled = $(this).children(" :checkbox");
            var tr_chiled = $("#" + tid + " :checkbox");
            strpower = strpower + $("#" + tid).attr("uid");
            if (file_type != '0') {
                tr_chiled.each(function () {
                    if ($(this).attr("checked") != undefined & $(this).attr("checked") == true) {
                        strpower = strpower + "_1";
                    } else {
                        strpower = strpower + "_0";
                    }
                })
            }
            strpower = strpower + ",";
        });
        strpower = strpower.substring(0, strpower.length - 1);
        NPostsharefolder(strpower, fid, file_type);

    } else {
        NPostsharefolder("", fid, file_type);

    }
}
//把文件夹分享给一个人，同时设置权限-------------------------------------------------------------------------
function NPostsharefolder(strpower, fid, ftype) {
    if (ftype == "1" || ftype == "2") {
        ftype = 1;
    } else {

    }
    $.ajax({
        url: "/DiskN/ShareFolderInfoJson?userName=" + sessionid + "&shareIdsAndGrants=" + strpower + "&folderIds=" + fid + "&flag=" + ftype,
        type: "Post",
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == '1') {
                checkedname.length = 0;
                parent.tip.Tip({ icon: "ok", msg: "共享成功！" });
                parent.datagrid.Refresh();
                parent.$.modalDialog.handler.dialog('close');

            } else {
                checkedname.length = 0;
                alert("共享失败！");
                //parent.tip.Tip({ icon: "error", msg: "共享失败！" });
            }
        }
    });
}
//过渡结束


//切换左侧div人员和部门切换显示-----------------------------------------------------
function changedivleft(name) {
    if (name == "user") {
        $("#dv_left_u").css("display", "block");
        $("#bumen_selected").removeClass("gorucheck");
        $("#member_selected").addClass("gorucheck");
        $("#dv_left_group").css("display", "none");
    } else {
        $("#dv_left_group").css("display", "block");
        $("#bumen_selected").addClass("gorucheck");
        $("#member_selected").removeClass("gorucheck");
        $("#dv_left_u").css("display", "none");
    }
}
//不同分组的checkbox的全选功能----------------------------------------
function checkall(istrue, grouporuser, id) {
    if (grouporuser == "group") {
        if (istrue) {
            $("#dv_left_group :checkbox").attr("checked", true);
        } else {
            $("#dv_left_group :checkbox").attr("checked", false);
        }
    } else if (grouporuser == "user") {
        if (istrue) {
            $("#dv_left_user :checkbox[pid=1]").attr("checked", true);
            $("#dv_left_user :checkbox[ppid=1]").attr("checked", true);
        } else {
            $("#dv_left_user :checkbox[pid=1]").attr("checked", false);
            $("#dv_left_user :checkbox[ppid=1]").attr("checked", false);
        }
    } else {
        //alert('Par Is User Or Group');
    }
}
//点击搜索按钮-------------------------------------------- 
function userserch(th) {
    var serchval = $.trim($("#txt_serch").val());
    changedivleft('user');
    if (serchval != '请输入群成员' & serchval != '') {
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
        $("#txt_serch").val('请输入群成员');
        $("#dv_left_user :checkbox[pid=1]").parent().parent().css("display", "block");
    } else {

    }
}
//搜索框获得焦点------------------------------------------------------
function serchfocus() {
    $("#txt_serch").css("color", "Black");
    if ($.trim($("#txt_serch").val()) == '请输入群成员') {
        $("#txt_serch").val('');
    }
}
//默认权限配置div显示和隐藏-------------------------------------------------
function defaulthide() {
    if (file_type == "0") {
        return;
    }
    if ($("#jqi").css("display") == "block" & $("#jqi").css("display") != undefined) {
        //$("#jqi").css("display", "none");
        $(".moren").css("display", "none");
        $("#div_BackGround").css("display", "none");

    } else {

        //$("#jqi").css("display", "block ");
        $(".moren").css("display", "block");
        $("#div_BackGround").css("display", "block");
    }
}
//加载默认权限配置--------------------------------------------------------
function defaultconfig() {
    configpowercheck.length = 0; 
    var eachtd = $("#tr_Defaul :checkbox");
    eachtd.each(function () {
        if ($(this).attr("checked") != undefined & $(this).attr("checked") == true) {
            configpowercheck.push(" checked='checked' ");
        } else {
            configpowercheck.push(" ");
        }
    })
    $("#jqi").css("display", "none");
    $("#div_BackGround").css("display", "none");
}
//点击取消按钮--------------------------------------------
function closewindow(type) {
    window.parent.closeThisWindow(type);
}
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

