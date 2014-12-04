
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

function GetDataForPaging(pageindex, PagerDivID) {
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "GetGradeListPaging", "strWhere": strWhere, "PageIndex": pageindex, "PageSize": pagesize },
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

            $("#div_gradeContent").setTemplateURL("../pagetemples/TeachChannelManage/gradeList.htm", null, null);
            $("#div_gradeContent").processTemplate(tempjson["DataList"]);
            $("#div_ForAllContent").css("visibility", "visible");
            CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
        }
    });

}
//绑定条件查询列表
function GetGradeLIstByCondition() {
    strWhere = $("#text_GradeName").val();
    GetDataForPaging(1, PagerDivID);
}

//删除选中的项
function DeleteCheckedItems_Grade() {
    DeleteCheckedItems("DeleteCheckedGrades", PagerDivID);
}

//修改选中的项
function UpdateThisItem_Grade(btn_id) {
   
    var id = btn_id.substr(7);
    AddOrEditFlag = id;
    ShowNewDialog("修改年级");
    var $text_contentName = $("#txt_ContentName");
    var $text_contentDesc = $("#txt_ContentDesc");

    $text_contentName.val($("#lab_" + id).html());
    $text_contentDesc.val($("#lab_2" + id).html());

}
//添加新项
function AddNewItem_Click() {
    AddOrEditFlag = "0";
    ShowNewDialog("添加年级");
}
//保存修改或添加
function Save_UpdateOrAdd() {
    var $text_contentName = $("#txt_ContentName");
    var $text_contentDesc = $("#txt_ContentDesc");
    if ($text_contentName.val() == "") {
        alert("名称不可为空");
        return;
    }
    if (AddOrEditFlag == "0") {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "AddNewGrade", "GradeName": $text_contentName.val(), "GradeDesc": $text_contentDesc.val() },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata != "-1" && backdata != "0") {
                    alert("添加成功");
                    GetDataForPaging(1, PagerDivID);
                }
                else {
                    alert("添加失败");
                }
            }
        });
    }
    else {
        $.ajax({
            type: "Post",
            url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
            data: { "Flag": "UpdateGrade", "GradeID": AddOrEditFlag, "GradeName": $text_contentName.val(), "GradeDesc": $text_contentDesc.val() },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata == "True" || backdata == "true") {
                    alert("修改成功");
                    GetDataForPaging(1, PagerDivID);
                }
                else {
                    alert("修改失败");
                }
            }
        });
    }
}

//删除选中的项
function DeleteThisItem_Grade(btn_id) {
    //    DeleteCheckedItems("DeleteCheckedTeachResource", PagerDivID);
    var gradeID = btn_id.substr(7);
    var ifcontinue = confirm("确实要删除选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": "DeleteGrade", "ID": gradeID },
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