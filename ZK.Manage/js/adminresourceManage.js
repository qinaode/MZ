
//分页功能
var PagerDivID = "div_Pager";
var pagesize = 15;
var lists = new Array();
var channelGroupName = "";
var strWhere = "";
$(function () {
    GetDataForPaging(1, PagerDivID);
});
//页面加载时绑定列表
//家在分页数据列表
function GetDataForPaging(pageindex, PagerDivID) {
    $.ajax({
        type: "Post",
        url: "../ashx/AdmintrationMag_Resource.ashx?_a=" + Math.random(),
        data: { "Flag": "GetAdminResourceListPaging", "strWhere": strWhere, "channelGroupName": channelGroupName, "PageIndex": pageindex, "PageSize": pagesize },
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
            $("#div_ResourceContent").setTemplateURL("../pagetemples/AdministrativeManagement/AdminResourceList.htm", null, null);
            $("#div_ResourceContent").processTemplate(tempjson["DataList"]);
            $("#div_ForAllContent").css("visibility", "visible");
            CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
        }
    });

}

//绑定条件查询列表
function GetAdminResourceListByCondition() {
    strWhere = $("#text_AdminResourceName").val();
    channelGroupName = $("#txt_channelGroupName").val();
    GetDataForPaging(1, PagerDivID);
}

//删除选中的项
function DeleteCheckedItems_AdminResource(btn_id) {
    //    DeleteCheckedItems("DeleteCheckedTeachResource", PagerDivID);
    var fileid = btn_id.substr(7);
    var ifcontinue = confirm("确实要删除选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/AdmintrationMag_Resource.ashx?_a=" + Math.random(),
        data: { "Flag": "DeleteCheckedAdminResource", "ID": fileid },
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
    $.dialog({ title: '添加资源到专题', width: '390px', height: '160px', content: 'url:AdminResourceSendEdit.aspx?tyid='+fileid+'&t=' + new Date().getTime().toString(), max: false, min: false });
}


//转码
function ConvertCheckedItems_Resource(fileid, file_type) {
    $.ajax({
        type: "Post",
        url: "../ashx/AdmintrationMag_Resource.ashx?_a=" + Math.random(),
        data: { "Flag": "Convert_Resource", "fileid": fileid, "filetype": file_type },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "true") {
                alert("后台转码开始");
            }
            else {
                alert("重新转换失败");
            }
        }
    });
}