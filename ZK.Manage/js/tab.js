//----------页面初始化----------
var XXuserhtml = "";
var file_id = ""; //用来记录文件的id
var dv_right = "dv_right"; //存放div的id;
var headerhtml = ""; //存放右侧权限代码
var alluserhtml = ""; //所有用户的html,其他地方使用不用请求获取
var checkboxhtml = "<td class='permissionCell'><input type='checkbox' name='folder_delete'/></td>";
$(function () {
    GetLeftGorup(); //加载左侧部门信息以及人员信息
    GetLeftUser(); //加载右侧分享人员信息
   setTimeout(function () {
   }, 1000);

})
//----------------左侧加载用户组织机构-------------
function GetLeftGorup() {
    $.ajax({
        url: "../ashx/NoticeRange.ashx?_a=" + Math.random(),
        type: "Post",
        datatype: "text/json",
        data: { "Flag": "AllDepInfoJson", "Pardep": "0" },
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            group = "";
            group = group + "<ul>";
            group = group + "<li><input type='checkbox' pid='-1'  id='ckdep_" + json["id"] + "' onclick=' checkAllDep(" + json["id"] + ")' ></input><label >";
            group = group + json["name"];
            group = group + "</label>";
            $.each(json["list"], function (i, item) {
                group = group + "<li><input type='checkbox' pid='1' id='ckdep_" + item["id"] + "'  onclick='groupcheckchange(" + item["id"] + ")' ></input><label >";
                group = group + item["name"];
                group = group + "</label></li>";
            });
            group = group + "</li></ul>";
            $("#dv_left_group").html(group);
            alluserhtml = group; //将生成的所有人员的html代码保存起来，搜索的时候，不用请求了。

        }
    });
  setTimeout(function () { }, 1000);
}

//----------切换左侧div显示隐藏-------------
function changedivleft(name) {
    if (name == "user") {
        $("#dv_left_user").css("display", "block");
        $("#dv_left_group").css("display", "none");
    } else {
        $("#dv_left_group").css("display", "block");
        $("#dv_left_user").css("display", "none");
    }
}
//---------------左侧加载用户成员------------------------
function GetLeftUser() {
    $.ajax({
        url: "../ashx/NoticeRange.ashx?_a=" + Math.random(),
        type: "Post",
        datatype: "text/json",
        data: { "Flag": "AllUserInfoJson" },
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            users = "";
            users = users + "<ul>";
            $.each(json["list"], function (i, item) {
                users = users + "<li ><input  id='cku_" + item["USERID"] + "' type='checkbox' pid='1' onclick=' usercheckchange(" + item["USERID"] + ")' ></input><label >";
                users = users + item["ACTUALNAME"];
                users = users + "</label></li>";
            });
            users = users + "</ul>";
            $("#dv_left_user").html(users);
        }
    });
 setTimeout(function () { }, 1000);
}
//不同分组的checkbox的变化---暂时没有用到-------------------------------------
function checkall(istrue, grouporuser) {
    if (grouporuser == "group") {
        if (istrue) {
            $("#dv_left_group :checkbox[pid=1]").attr("checked", true);
        } else {
            $("#dv_left_group :checkbox[pid=1]").attr("checked", false);
        }
    } else if (grouporuser == "user") {
        if (istrue) {
            $("#dv_left_user :checkbox[pid=1]").attr("checked", true);
        } else {
            $("#dv_left_user :checkbox[pid=1]").attr("checked", false);
        }
    } else {
        alert('Par Is User Or Group');
    }
}

//-------------左侧用户列表每个用户勾选框点击---------------
function usercheckchange(id) {

    var up = $("#cku_" + id);
    //if (up.attr("checked") == undefined || up.attr("checked") == true)
    if (up.attr("checked") == true)
    {
        if ($("#u_" + id).length > 0) {
            return;
        } else {
            addusertoright(id); //向右侧插入该用户
        }

    } else {
        $("#u_" + id).remove();
    }

}

//-------------左侧部门列表每个部门勾选框点击---------------
function groupcheckchange(depId) {
   
    var cp = $("#ckdep_" + depId);
    //alert(cp.attr("checked") == true);
    if (cp.attr("checked") == true) {
        if ($("#dep_" + depId).length > 0) {
            return;
        } else {
            adddeptoright(depId);
            
             //向右侧插入该用户
        }
    } else {
       $("#dep_"+id).remove();
     }
   checkAllDep(depId);}

//==母部门选定，子部门全选===
function checkAllDep(depid) {
    // alert($('#dv_left_group input[pid="' + depid + '"'));
    alert(depid);
    var cp = $("#ckdep_" + depId);
    var dep = $('#dv_left_group').find('input');
    dep.each(function () {
        alert(22);
        if ($(this).attr('pid') == depid) {
            $(this).attr('checked', false);
            $(this).attr("disabled", "disabled");
        }
    });
    alert(dep.length);
    alert(22);
    //alert(cp.attr("checked"));
//    if(cp.attr("checked")==true){
//     
//    }
 }

//------------左侧部门列表勾选部门到右侧------
function adddeptoright(id) {
    $.ajax({
        url: "../ashx/NoticeRange.ashx?_a=" + Math.random(),
        type: "Post",
        datatype: "text/json",
        data: { "Flag": "AllDepInfoJson" },
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            userhtml = "";
            if ($("#dep" + id).length > 0) {
                return;
            } else {
                $.each(json["list"], function (i, item) {
                    if (id == item["id"]) {
                        //                                                userhtml = userhtml + "<tr class='tr_share' id='tru_" + item['id'] + "' ><td>";
                        //                                                userhtml = userhtml + item["name"] + "</td>";
                        //                                                userhtml = userhtml + checkboxhtml;
                        //                                                userhtml = userhtml + "<td><a href='#'  num='" + item['id'] + "' class='LI_user' torf='true' id='a_" + item['id'] + "' onclick='deleted(" + item['id'] + ")'>"
                        //                                                userhtml = userhtml + "删除</a>"
                        //                                                userhtml = userhtml + "</td>";
                        //                                                userhtml = userhtml + "</tr>";
                        userhtml = userhtml + "<a href='#' class='select_item'  id='dep_" + item['id'] + "' title='" + item["name"] + "'>";
                        userhtml = userhtml + "<span class='icon_diff icon_party'></span>" + item["name"] + "<span class='addr_del' ck='del' forid='dep" + item['id'] + "' bparty='true' onclick='deleted(dep_" + item['id'] + ")'>×</span></a>";
                    }
                });
            }
            //$("#gusertab").children().append(userhtml);
            $("#div_right").append(userhtml);

        }
    });
}




//点击删除按钮触发------------------------------------------
function deleted(id) {
    $(id).remove();
    $("#ck" + $(id).attr("id")).attr("checked", false);
    //alert($("#ck" + $(id).attr("id")).attr("checked"));
}

//---------------------向右侧列表添加单个用户------------
function addusertoright(id) {
    $.ajax({
        url: "../ashx/NoticeRange.ashx?_a=" + Math.random(),
        type: "Post",
        datatype: "text/json",
        data: { "Flag": "AllUserInfoJson" },
        success: function (backdata) {
            var json = $.parseJSON(backdata);
            userhtml = "";
            if ($("#u_" + id).length > 0) {
            } else {
                $.each(json["list"], function (i, item) {
                    if (id == item["USERID"]) {
//                        userhtml = userhtml + "<tr class='tr_share' hid='" + item['USERID'] + "' hname='" + item["ACTUALNAME"] + "'  id='tru_" + item['USERID'] + "' ><td>";
//                        userhtml = userhtml + item["ACTUALNAME"] + "</td>";
//                        userhtml = userhtml + checkboxhtml;
//                        userhtml = userhtml + "<td><a href='#'  num='" + item['USERID'] + "' class='LI_user' torf='true' id='a_" + item['USERID'] + "' onclick='deleted(" + item['USERID'] + ")'>"
//                        userhtml = userhtml + "删除</a>"
//                        userhtml = userhtml + "</td>";
//                                                userhtml = userhtml + "</tr>";
                        userhtml = userhtml + "<a href='javascript:void(0);' class='select_item' title='" + item["ACTUALNAME"] + "' alias='00' id='u_" + item['USERID'] + "'>";
                        userhtml = userhtml + "<span class='icon_diff icon_man'></span>" + item["ACTUALNAME"] + "<span class='addr_del' ck='del' forid='u_" + item['USERID'] + "' bparty='false' onclick='deleted(u_" + item['USERID'] + ")'>×</span></a>";

                    }
                });
            }
             //$("#gusertab").children().append(userhtml);
            $("#div_right").append(userhtml);

        }
    });
}

//==========点击确定按钮暂无用============

function butok() {
    //var obj = GetObj();//这个方法就是你获取到的数据
    var obj = userAlltoEmail();

    var api = frameElement.api; //调用父页面数据  
    var W = api.opener; //获取父页面对象  
    //此处是向父页面传值
    W.document.getElementById('txtUser').value = obj;
    closeWindow()
}
//=============获得数据===========
function userAlltoEmail() {
    //var user = $(".tr_share");
    var user = $(".select_item");
    user.each(function () {
        //XXuserhtml = XXuserhtml + "<span class='sp' >" + $(this).attr("hname") + "</span><span class='addr_del' ck='del' id='" + $(this).attr("hid") + "'>×</span></a>";
        //XXuserhtml = XXuserhtml + "<div class='addr_base addr_normal'style='display:inline; float:left;'><b nid=" + $(this).attr("hid") + ">" + $(this).attr("hname") + "</b><a href='javascript:void(0);' class='addr_del' ck='delSelectedItem'>×</a><input type='hidden' name='partyid' value='" + $(this).attr("hname") + "'></div>";
       
        XXuserhtml = XXuserhtml + "<a href='javascript:void(0);' class='ub' id='" + $(this).attr("id") + "' title='" + $(this).attr("title") + "'>";
        XXuserhtml = XXuserhtml + "<span class='sp' >" + $(this).attr("title") + "</span><span class='addr_del' ck='del' onclick='div_deleted(" + $(this).attr("id") + ",event)'>×</span></a>";
        XXuserhtml = XXuserhtml + "";
    });
    return XXuserhtml;
  
}
//=======无用的=====
function result(res) {
    $.ajax({
        url: "../SystemMsg/AddSysNotice.aspx?_a=" + Math.random(),
        type: "Post",
        datatype: "text/json",
        data: { "result": res },
        success:function (backdata){
            window.location = "../SystemMsg/AddSysNotice.aspx?res=" + res;
        }
    });
    
}

//======退出或取消===========
function closeWindow() {
    var api = frameElement.api, 
    W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
    api.reload();
    api.close();
}
