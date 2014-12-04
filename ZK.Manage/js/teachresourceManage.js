
//分页功能
var PagerDivID = "div_Pager";
var pagesize = 15;
var lists = new Array();

var strWhere = "";
//记录当前选中的教材 年级 版本
var C_CourseName = "请选择";
var C_CourseID = "null";
var C_GradeName = "请选择";
var C_GradeID = "null";
var C_EditionName = "请选择";
var C_EditionID = "null";
$(function () {
    GetCourseList();
    GetGradeList();
    GetEditionList();
    GetDataForPaging(1, PagerDivID);
});
//获取科目列表
function GetCourseList() {
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetCourseList", "strWhere": strWhere },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                return;
            };
            var tempjson = $.parseJSON(backdata);
            if (tempjson["DataList"] == "") {
                alert("年级为空，请先添加年级");
                return;
            }
            $("#div_CourseList").setTemplateURL("../pagetemples/TeachChannelManage/lesson_courseList.htm", null, null);
            $("#div_CourseList").processTemplate(tempjson["DataList"]);
        }
    });

}
//获取年级列表
function GetGradeList() {
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetGradeList", "strWhere": strWhere },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                return;
            };
            var tempjson = $.parseJSON(backdata);
            if (tempjson["DataList"] == "") {
                alert("年级为空，请先添加年级");
                return;
            }
            $("#div_GradeList").setTemplateURL("../pagetemples/TeachChannelManage/lesson_gradeList.htm", null, null);
            $("#div_GradeList").processTemplate(tempjson["DataList"]);
        }
    });
}
//获取版本列表
function GetEditionList() {
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetEditionList", "strWhere": strWhere },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                return;
            };
            var tempjson = $.parseJSON(backdata);
            if (tempjson["DataList"] == "") {
                alert("年级为空，请先添加年级");
                return;
            }
            $("#div_EditionList").setTemplateURL("../pagetemples/TeachChannelManage/lesson_editionList.htm", null, null);
            $("#div_EditionList").processTemplate(tempjson["DataList"]);
        }
    });
}
//页面加载时绑定列表
//家在分页数据列表
function GetDataForPaging(pageindex, PagerDivID) {
    if (C_CourseID == "null") {
        C_CourseID = "";
    }
    if (C_GradeID == "null") {
        C_GradeID = "";
    }

    if (C_EditionID == "null") {
        C_EditionID = "";
    }
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetTeachResourceListPaging",
            "strWhere": strWhere,
            "LessonCourse": C_CourseID,
            "LessonGrade": C_GradeID,
            "LessonEdition": C_EditionID,
            "PageIndex": pageindex,
            "PageSize": pagesize
        },
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
            $("#div_teachResourceContent").setTemplateURL("../pagetemples/TeachChannelManage/teachresourcelist.htm", null, null);
            $("#div_teachResourceContent").processTemplate(tempjson["DataList"]);
            $("#div_ForAllContent").css("visibility", "visible");
            CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
        }
    });

}

//绑定条件查询列表
function GetTeachResourceListByCondition() {
    GetCurrentSelectItems();
    if (C_CourseID == "null") {
        alert("请选择教材");
        return;
    }
    if (C_GradeID == "null") {
        alert("请选择年级");
        return;
    }
    if (C_EditionID == "null") {
        alert("请选择版本");
        return;
    }
    strWhere = $("#text_TeachFileName").val();
    GetDataForPaging(1, PagerDivID);
}
//获取当前选中的教材 年级 版本
function GetCurrentSelectItems() {
    C_CourseName = $("#select_course").val();
    C_CourseID = $("#select_course option:selected").attr("id").substr(13);

    C_GradeName = $("#select_grade").val();
    C_GradeID = $("#select_grade option:selected").attr("id").substr(12);

    C_EditionName = $("#select_edition").val();
    C_EditionID = $("#select_edition option:selected").attr("id").substr(14);

}

//删除选中的项
function DeleteCheckedItems_TeachResource(btn_id) {
    //    DeleteCheckedItems("DeleteCheckedTeachResource", PagerDivID);
    var fileid = btn_id.substr(7);
    var ifcontinue = confirm("确实要删除选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "DeleteCheckedTeachResource", "ID": fileid },
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

//推送选中的项
function SendCheckedItems_AdminResource(btn_id) {
    var fileid = btn_id.substr(5);
    //    var ifcontinue = confirm("确实要推送选中项吗");
    //    if (!ifcontinue) {
    //        return false;
    //    };
    $.dialog({ title: '添加资源到专题', width: '390px', height: '160px', content: 'url:TeachResourceSpecialEdit.aspx?tyid=' + fileid + '&t=' + new Date().getTime().toString(), max: false, min: false });
}


//转码
function ConvertCheckedItems_TeachResource(fileid, file_type) {
     

    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "Convert_TeachResource", "fileid": fileid, "filetype": file_type },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                alert("后台已开始转码");
            }
            else {
                alert("重新转换失败");
            }
        }
    });
}
