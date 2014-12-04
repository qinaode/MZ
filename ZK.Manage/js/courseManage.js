
//分页功能
var PagerDivID = "div_Pager";
var pagesize = 10;
var lists = new Array();

var strWhere = "";
var AddOrEditFlag = "0";
$(function () {
    GetDataForPaging(1, PagerDivID);
});
//页面加载时绑定列表
//家在分页数据列表
function GetDataForPaging(pageindex, PagerDivID) {
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetCourseListPaging", "strWhere": strWhere, "PageIndex": pageindex, "PageSize": pagesize },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                return;
            };
            var tempjson = $.parseJSON(backdata);
            if (tempjson["DataList"] == "") {
                $("#div_ForAllContent").css("visibility", "hidden");
                return;
            }
            $("#div_courseContent").setTemplateURL("../pagetemples/TeachChannelManage/courseList.htm", null, null);
            $("#div_courseContent").processTemplate(tempjson["DataList"]);
            $("#div_ForAllContent").css("visibility", "visible");
            CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
        }
    });

}

//绑定条件查询列表
function GetCourseLIstByCondition() {
    strWhere = $("#text_CourseName").val();
    GetDataForPaging(1, PagerDivID);
}

//删除选中的项
function DeleteCheckedItems_Course() {
    DeleteCheckedItems("DeleteCheckedCourse", PagerDivID);
}

//修改选中的项
function UpdateThisItem_Course(btn_id) {
    var courseid = btn_id.substr(7);
    AddOrEditFlag = courseid;
    ShowNewDialog("修改教材");
    var $text_contentName = $("#txt_ContentName");
    var $text_contentDesc = $("#txt_ContentDesc");

    $text_contentName.val($("#lab_" + courseid).text());
    $text_contentDesc.val($("#lab_2" + courseid).html());

}
//添加教材
function AddNewItem_Click() {
    AddOrEditFlag = "0";
    ShowNewDialog("添加教材");
}
function Save_UpdateOrAdd() {
    var $text_contentName = $("#txt_ContentName");
    var $text_contentDesc = $("#txt_ContentDesc");
    if ($text_contentName.val() == "") {
        alert("教材名不可为空");
        return;
    }
    if (AddOrEditFlag == "0") {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "AddNewCourse", "CourseName": $text_contentName.val(), "CourseDesc": $text_contentDesc.val() },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata != "-1" && backdata != "0") {
                    alert("添加成功");
                    GetDataForPaging(1, PagerDivID);
                }
                else {
                    alert("有重名的教材名，添加失败");
                }
            }
        });
    }
    else {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "UpdateCourse", "CourseID": AddOrEditFlag, "CourseName": $text_contentName.val(), "CourseDesc": $text_contentDesc.val() },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "True" || backdata == "true") {
                    alert("修改成功");
                    GetDataForPaging(1, PagerDivID);
                }
                else {
                    alert("有重名的教材名,修改失败");
                }
            }
        });
    }
}
////添加教材
//function ShowNewDialog() {
//    //内容
//    var Content = $("<div id='div_text'></div>");
//    //p.append($("<a>&nbsp;&nbsp;</a>"));
//    Content.append($("<label>名称：&nbsp;&nbsp;</label>"));
//    Content.append($("<input type='text' id='txt_ContentName'/>"));
//    Content.append($("<br>"));
//    Content.append($("<br>"));
//    Content.append($("<label>描述：&nbsp;&nbsp;</label>"));
//    Content.append($("<input type='text' id='txt_ContentDesc'/>"));
//    $.dialog({ id: 'ItemID',
//        title: '添加教材',
//        width: '250px',
//        height: '150px',
//        content: Content,
//        lock: true,
//        cancel: true,
//        max: false,
//        min: false,
//        ok: function () { Save_UpdateOrAdd(); }
//    });
//}

//删除选中的项
function DeleteThisItem_Course(btn_id) {
    //    DeleteCheckedItems("DeleteCheckedTeachResource", PagerDivID);
    var courseID = btn_id.substr(7);
    var ifcontinue = confirm("确实要删除选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "DeleteCourse", "ID": courseID },
        datatype: "text/json",
        success: function (backdata) {

            if (backdata == "文件夹不为空") {
                alert("文件夹不为空，删除失败");
                return;
            }
            if (backdata == "true" || backdata == "True") {
                alert("删除成功");
                GetDataForPaging(1, PagerDivID);
                return;
            }
            else {
                alert("删除失败");
                return;
            }
        }
    });
}