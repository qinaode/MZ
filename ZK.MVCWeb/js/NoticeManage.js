
//GetMenuItem 得到左侧菜单
//GetNoticeByPaging得到通知公告
var PagerDivID = "div_Pager";
var pagesize = 10;
var userID = "";
var ownerID = "";
var lists = new Array();
var strWhere = "";
var curpageindex = 1;
var divData = "notList"; //表示要显示的数据
$(function () {
    //加载链接
    //alert( $("#userID").val());
    userID = $("#userID").val();
    ownerID = $("#userID").val();
    GetMenuItem();
    // .siblings()；
    //默认显示通至公告
    GetDataForPaging(1, PagerDivID);
    //$("#div_NoticeContent").html("122");
    //var div_Table = $("#div_NoticeList .div_table:even");
    //div_Table.addClass("hightColor");

});
//得到当前页

function setseleted(li) {
    var allLI = $("#div_ItemSelect ul li ");
    allLI.each(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        }
    });
    //以上是一处所有lib标签的selected类
    if ($(li).hasClass("selected")) {

    } else {
        $(li).addClass("selected");
    }
}
//菜单
function GetMenuItem() {
    var str = " <ul><li class='selected'   onclick = 'setseleted(this)'><a onclick='ShowAllNotice()'>通知公告</a></li>";
    $.ajax({
        type: "post",
        url: "/SendNotice/GetUserRoleByUserID",
        datatype: "text/json",
        data: { "userID": userID },
        success: function (backdata) {
            if (backdata == "true") { //有权限      
                str = str + " <li   onclick = 'setseleted(this)'><a onclick='ManageMyselfNotice()'>管理通知</a></li>";
                str = str + " <li   onclick = 'setseleted(this)'><a onclick='ShowSendNotice()'>发送通知</a></li></ul>"
                //str = str + " <li><a href='/SendNotice/SendNotice' target='div_NoticeList' >发送通知</a></li></ul>"
            }
            else {
                str = str + "<ul>";
            }
            $("#div_ItemSelect").append(str);
        }
    });
}
function ShowAllNotice() {
    $("#msg").html("公告的内容是：");
    $("#div_sendnot").css('display', 'none');
    $("#div_NoticeContent").parent('fieldset').css('display', 'block');
    $("#div_not").css('display', 'block');
    divData = "notList";
    GetDataForPaging(1, PagerDivID);
}


//每个人可以查看发给自己的公告,显示数据
function GetDataForPaging(pageindex, PagerDivID) {
    $("#div_NoticeList").html("");
    $("#div_Pager").html("");
    $("#div_NoticeContent").html("");
    if (divData == "notList") {
        $.ajax({
            type: "post",
            url: "/SendNotice/GetNoticeListByUserID",
            datatype: "text/json",
            data: { "userID": userID, "PageIndex": pageindex, "PageSize": pagesize },
            async: false,
            success: function (backdata) {
                var json = $.parseJSON(backdata);
                // alert((json["totalNumber"], 10));
                if (json["DataList"] == "") {
                    return;
                    $("#div_Pager").css('visibility', 'hidden');
                }
                $("#div_NoticeList").setTemplateURL("/pagetemples/NoticeList.htm", null, null);
                $("#div_NoticeList").processTemplate(json["DataList"]);
                //  $("#div_NoticeList").css("visibility", "visibility");
                $("#div_Pager").css('visibility', 'visibility');
                curpageindex = pageindex;
                CreatePageControl(pageindex, pagesize, parseInt(json["totalNumber"], 10), PagerDivID, lists);
            }
        });
    }
    else {
        $.ajax({
            type: "post",
            async: false,
            url: "/SendNotice/ManageNoticeByRole",
            datatype: "text/json",
            data: { "userID": userID, "PageIndex": pageindex, "PageSize": pagesize },
            success: function (backdata) {
                var json = $.parseJSON(backdata);
                if (json["DataList"] == "") {
                    return;
                }
                $("#div_NoticeList").setTemplateURL("/pagetemples/ManageNoticeByRole.htm", null, null);
                $("#div_NoticeList").processTemplate(json["DataList"]);
                $("#div_NoticeList").css("visibility", "visible");
                CreatePageControl(pageindex, pagesize, parseInt(json["totalNumber"], 10), PagerDivID, lists);
            }
        });

    }
};
//显示选中的NoticeItem，内容显示在contentDiv中
function ShowNoticeItem(id) {
    $("#div_NoticeContent").html("");
    var strid = id.substr(4);
    $.ajax({
        type: "post",
        url: "/SendNotice/GetNoticeItem",
        datatype: "text/json",
        data: { "strid": strid, "userID": userID },
        success: function (backdata) {
            if (backdata == "") {
                return;
            }
            //alert(curpageindex);
            //GetDataForPaging(curpageindex, PagerDivID);
            // alert($("#" + id + " td").last().html(""));
            $("#" + id + " td").last().html("已读");
            $("#" + id).removeClass("noread");
            $("#div_NoticeContent ").html(backdata);


        }
    });
};

//管理我曾经发送的通知
function ManageMyselfNotice() {
    $("#msg").html("查看列表是：");
    $("#div_sendnot").css('display', 'none');
    $("#div_NoticeContent").parent("fieldset").css('display', 'block'); //parent('fieldset')
    $("#div_not").css('display', 'block');
    divData = "managenot";
    GetDataForPaging(1, PagerDivID);
};

//得到查看列表
function ViewStateList(id) {
    $("#div_NoticeContent").html("");
    var strid = id.substr(4);
    //alert(strid);
    $.ajax({
        type: "post",
        url: "/SendNotice/ViewStateList",
        datatype: "text/json",
        data: { "strid": strid },
        success: function (backdata) {
            //alert(backdata);
            var json = $.parseJSON(backdata);
            if (json["DataList"] == "") {
                return;
            }

            //$("#div_NoticeContent").setTemplateURL("/pagetemples/ViewStateList.htm", null, null);
            // $("#div_NoticeContent").processTemplate(json["DataList"]);
            // $("#div_NoticeContent").css("visibility", "visible")

            var number = json["totalNumber"];
            var divjoin = "<div id='join'title='已读人员：' ><a>已读人员</a></div>";
            var divnotjoin = "<div id='notjoin' title='未读人员：'><a>未读人员</a></div>";
            if (number > 0) {
                var divul1 = "<p><ul>"; //没有查看
                var divul2 = "<p><ul>"; //chakan 
                var fanum = 0;
                var trnum = 0;
                for (var i = 0; i < number; i++) {
                    var flag = json["DataList"][i]["isSee"];
                    var user = json["DataList"][i]["userID"];
                    if (flag == false) {
                        divul1 = divul1 + "<li><a>" + json["DataList"][i]["ACTUALNAME"] + "</a></li>"
                    }
                    else {
                        divul2 = divul2 + "<li><a>" + json["DataList"][i]["ACTUALNAME"] + "</a></li>"
                    }
                }
                divul1 = divul1 + "</ul></p>";
                divul1 = divul1 + "</ul></p>";
                $("#div_NoticeContent").append(divjoin);
                //$("#div_NoticeContent").append("</br>");
                $("#div_NoticeContent").append(divnotjoin);
                $("#notjoin").append(divul1);
                $("#join").append(divul2);
                //$("#join ul li").length<=0
                if ($("#join ul li").length <= 0) {
                    $("#join").css('display', 'none');
                }
                if ($("#notjoin ul li").length <= 0) {
                    $("#notjoin").css('display', 'none');
                }
            }
            else {
                $("#div_NoticeContent").html("没有相关数据");

            }
        }
    });
};
//打开发送通知的对话框
function ShowSendNotice() {
    //$("#div_NoticeContent").css("visibility", "hidden");
    //alert($("#div_NoticeContent").parent('fieldset').html());
    $("#div_NoticeContent").parent('fieldset').css('display', 'none');
    $("#div_not").css('display', 'none');
    // var str = "<iframe name='ifNoticeList' src='/SendNotice/SendNotice' width='680' height='700' ></iframe>";
    document.getElementById("ifNoticeList").src = "";
    var src = "/SendNotice/SendNotice?userID=" + userID;
    document.getElementById("ifNoticeList").src = src;
    $("#div_sendnot").css('display', 'block');


}
//删除
function DelNotice(id) {
    var strid = id.substr(4);
    alert(strid);
    var flag = confirm("确定要删除吗？");
    if (flag == false) {
        return;
    }
    $.ajax({
        type: "post",
        url: "/SendNotice/DelNotice",
        datatype: "text/json",
        data: { "id": strid },
        success: function (backdata) {
            // alert(backdata)True;
            if (backdata == "true" || backdata == "True") {
                alert("删除成功！");
                //location.reload();
                GetDataForPaging(1, PagerDivID);
            }
            else {
                alert("删除失败！");
            }
        }
    });
};

function linkToFormat(str) {
    if (str == "") {
        return "#";
    }
    else {
        return str;
    }
}