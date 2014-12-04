
var strWhere = "";
var AddOrEditFlag = "0";
//分页功能
var PagerDivID = "div_Pager";
var pagesize = 10;
var lists = new Array();

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
//搜索按钮的点击事件
function GetLessonListByCondition() {

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
    GetLessonList();
}

//添加课程按钮事件
function AddNewLesson_Click() {
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
    AddOrEditFlag = "0";

    ShowNewDialog_Lesson("添加课程");

    GetUnitList();
    $("#Dialog_CourseName").html(C_CourseName);
    $("#Dialog_GradeName").html(C_GradeName);
    $("#Dialog_EditionName").html(C_EditionName);
}
//添加新单元
function AddNewItem_Click() {
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
    AddOrEditFlag = "0";
    ShowNewDialog_DanYuan("添加单元");
    $("#DanYuan_CourseName").html(C_CourseName);
    $("#DanYuan_GradeName").html(C_GradeName);
    $("#DanYuan_EditionName").html(C_EditionName);
}
//修改课程
function UpdateThisItem_Lesson(btn_id) {
//    var lessonid = btn_id; //btn_id.substr(7);
//    AddOrEditFlag = lessonid;
//    //获取lesson实体
//    $.ajax({
//        type: "Post",
//        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
//        data: { "Flag": "GetLessonModel", "LessonID": lessonid },
//        datatype: "text/json",
//        success: function (backdata) {
//            if (backdata == "" || backdata == "[]") {
//                alert("数据错误");
//                return;
//            }
//            var tempjson = $.parseJSON(backdata);
//           
//            ShowNewDialog_Lesson("修改课程");
//            GetUnitList();


//                        $("#lessonUnit_" + tempjson["lessonParent"]).attr("selected", "selected");

//            $("#select_unit").val(tempjson["lessonParent"]);
//            $("#Dialog_CourseName").html(C_CourseName);
//            $("#Dialog_GradeName").html(C_GradeName);
//            $("#Dialog_EditionName").html(C_EditionName);
//       
//            $("#txt_LessonName").val(tempjson["lessonName"]);
//            $("#txt_LessonDesc").val(tempjson["lessonDesc"]);
//            $("#txt_LessonLevel").val(tempjson["lessonLevel"]);
//            $("#txt_LessonMB").val(tempjson["teachMB"]);
//            $("#txt_LessonND").val(tempjson["teachND"]);
//            $("#txt_LessonZD").val(tempjson["teachZD"]);

//        }
//    });
    $.dialog({ title: '修改课程', width: '390px', height: '350px', content: 'url:LessonMagEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + btn_id, max: false, min: false });
}
//修改单元
function UpdateThisItem_DanYuan(btn_id) {
    var lessonid = btn_id.substr(11);
    AddOrEditFlag = lessonid;
    //获取lesson实体
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetLessonModel", "LessonID": lessonid },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                alert("数据错误");
                return;
            }
            var tempjson = $.parseJSON(backdata);

            ShowNewDialog_DanYuan("修改单元");
            $("#DanYuan_CourseName").html(C_CourseName);
            $("#DanYuan_GradeName").html(C_GradeName);
            $("#DanYuan_EditionName").html(C_EditionName);

            $("#DanYuan_LessonName").val(tempjson["lessonName"]);
            $("#DanYuan_LessonDesc").val(tempjson["lessonDesc"]);

        }
    });


}
//保存修改或添加的课程
function Save_UpdateOrAdd_Lesson() {

    var LessonName = $("#txt_LessonName").val();
    var LessonDesc = $("#txt_LessonDesc").val();
    var LessonParent = $("#select_unit option:selected").attr("id").substr(11);
    var LessonLevel = $("#txt_LessonLevel").val();
    var TeachMB = $("#txt_LessonMB").val();
    var TeachND = $("#txt_LessonND").val();
    var TeachZD = $("#txt_LessonZD").val();
    //添加
    if (AddOrEditFlag == "0") {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "AddNewLesson",
                "LessonName": LessonName,
                "LessonDesc": LessonDesc,
                "LessonParent": LessonParent,
                "LessonLevel": LessonLevel,
                "LessonCourse": C_CourseID,
                "LessonGrade": C_GradeID,
                "LessonEdition": C_EditionID,
                "TeachMB": TeachMB,
                "TeachND": TeachND,
                "TeachZD": TeachZD
            },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "0" || backdata == "-1") {
                    alert("重名，添加失败");
                    return;
                } else if (backdata == "班级不存在") {
                    alert(backdata);
                    return;
                }
                alert("添加成功");
                GetLessonList();
            }
        });
    }
    else {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "UpdateLesson",
                "LessonID": AddOrEditFlag,
                "LessonName": LessonName,
                "LessonDesc": LessonDesc,
                "LessonParent": LessonParent,
                "LessonLevel": LessonLevel,
                "TeachMB": TeachMB,
                "TeachND": TeachND,
                "TeachZD": TeachZD
            },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "true" || backdata == "True") {
                    alert("修改成功");
                    GetLessonList();
                } else {
                    alert("修改失败");
                    return;
                }
            }
        });
    }
}

//保存修改或添加的单元
function Save_UpdateOrAdd_DanYuan() {
    var LessonName = $("#DanYuan_LessonName").val();
    var LessonDesc = $("#DanYuan_LessonDesc").val();

    //添加
    if (AddOrEditFlag == "0") {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "AddNewLesson",
                "LessonName": LessonName,
                "LessonDesc": LessonDesc,
                "LessonParent": "",
                "LessonLevel": "",
                "LessonCourse": C_CourseID,
                "LessonGrade": C_GradeID,
                "LessonEdition": C_EditionID,
                "TeachMB": "",
                "TeachND": "",
                "TeachZD": ""
            },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "0" || backdata == "-1") {
                    alert("重名，添加失败");
                    return;
                } else if (backdata == "班级不存在") {
                    alert(backdata);
                    return;
                }
                alert("添加成功");
                GetLessonList();
            }
        });
    }
    else {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "UpdateLesson",
                "LessonID": AddOrEditFlag,
                "LessonName": LessonName,
                "LessonDesc": LessonDesc,
                "LessonParent": "",
                "LessonLevel": "",
                "TeachMB": "",
                "TeachND": "",
                "TeachZD": ""
            },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "true" || backdata == "True") {
                    alert("修改成功");
                    GetLessonList();
                } else {
                    alert("修改失败");
                    return;
                }
            }
        });
    }
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

//单项删除
function DeleteCheckedItem_Lesson(delete_id) {

    var ifcontinue = confirm("确实要删除选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "DeleteCheckedLesson", "IDS": delete_id },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "true" || backdata == "True") {
                alert("删除成功");
                GetLessonList();
            }
            else {
                alert("删除失败");
            }
        }
    });

}

//上移  
function UpCheckedItem_Lesson(up_id) {

    var ifcontinue = confirm("确实要上移选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "UpCheckedLesson", "IDS": up_id, "Dir": "Up" },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "NoMove") {
                alert("第一级节点无法进行移动！");
            }
            else {
                if (backdata == "true" || backdata == "True") {
                    alert("上移成功");
                    GetLessonList();
                }
                else {
                    alert("上移失败");
                }
            }
        }
    });

}
//下移  
function DownCheckedItem_Lesson(down_id) {

    var ifcontinue = confirm("确实要下移选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "DownCheckedLesson", "IDS": down_id, "Dir": "Down" },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "true" || backdata == "True") {
                alert("下移成功");
                GetLessonList();
            }
            else {
                alert("下移失败");
            }
            if (backdata == "NoMove") {
                alert("第一级节点无法进行移动！");
            }
        }
    });

}

//获取单元列表
function GetLessonList() {
    GetCurrentSelectItems();
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetLessonList",
            "LessonParent": "0",
            "LessonCourse": C_CourseID,
            "LessonGrade": C_GradeID,
            "LessonEdition": C_EditionID
        },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                return;
            };
            if (backdata == "班级不存在") {
                alert("班级不存在");

            }
            var tempjson = $.parseJSON(backdata);
            if (tempjson["DataList"] == "") {
                return;
            }

            //绑定树结构
            d = new dTree('dl');
            TempData = tempjson["DataList"];
            d.add(1, -1, "课程列表", "");
            $.each(tempjson["DataList"], function (index, item) {
                d.add(parseInt(item["lessonID"], 10), parseInt(item["lessonParent"], 10), item["lessonName"] + "<a visible='none' id='" + item["lessonID"] + "'></a>", "");
            });
            var str1 = "<a href='javascript: dl.openAll();'>展开</a> | <a href='javascript: dl.closeAll();'>收缩</a>";
            $("#div_Tree").html(str1 + d.toString());
        }
    });

}
//获取单元列表
function GetUnitList() {
    GetCurrentSelectItems();
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetUnitList",
            "LessonParent": "0",
            "LessonCourse": C_CourseID,
            "LessonGrade": C_GradeID,
            "LessonEdition": C_EditionID
        },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "" || backdata == "[]") {
                return;
            };
            if (backdata == "班级不存在") {
                alert("班级不存在");

            }
            var tempjson = $.parseJSON(backdata);
            if (tempjson["DataList"] == "") {
                return;
            }
            $("#div_LessonParent").setTemplateURL("../pagetemples/TeachChannelManage/lesson_unitlist.htm", null, null);
            $("#div_LessonParent").processTemplate(tempjson["DataList"]);

        }
    });

}