/*全选 和 反选  其中name为checkbox_1的name属性*/
function CheckAll() {
   
    if ($("#check_CheckAll").attr("checked")) {
     
        CheckReverse();
        return;
    }
    $("#check_CheckAll").attr("checked", "checked");
    $(".check_ForAll").attr("checked", "checked");
    $("input[name='checkbox_1']").attr("checked", "checked");
}
function CheckReverse() {
   
    $("#check_CheckAll").attr("checked", !$("#check_CheckAll").attr("checked"));
    $(".check_ForAll").attr("checked", !$(".check_ForAll").attr("checked"));
    $("input[name='checkbox_1']").each(function (i, item) {
        $(item).attr("checked", !$(item).attr("checked"));
    });
}

//删除选中的项 现仅限于teachchannel 和 用该分页者
function DeleteCheckedItems(Flag, PagerDivID) {

    //记录被选择的项的ID
    var checkedlist = new Array();
    $("input[name='checkbox_1']").each(function (i, item) {

        if ($(item).attr("checked")) {
            checkedlist.push($(item).attr("id"));
        }
    });
    if (checkedlist.length == 0) {
        alert("请选择要删除的项");
        return false;
    }
    var ifcontinue = confirm("确实要删除选中项吗");
    if (!ifcontinue) {
        return false;
    };
    $.ajax({
        type: "Post",
        url: "../ashx/TeachChannelManage.ashx?_a=" + Math.random(),
        data: { "Flag": Flag, "IDS": checkedlist.toString() },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata == "true" || backdata == "True") {
                alert("删除成功");
                GetDataForPaging(1, PagerDivID);
            }
            else {
                alert("删除失败");
            }
        }
    });

}

//显示弹出层  用于添加和修改项 传入参数 为标题
function ShowNewDialog(Title) {
    //内容
    var Content = $("<div id='div_Common'></div>");
    //p.append($("<a>&nbsp;&nbsp;</a>"));
    Content.append($("<label>名称：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_ContentName'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>描述：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_ContentDesc'/>"));
    $.dialog({
        id: 'ItemID',
        title: Title,
        width: '250px',
        height: '150px',
        content: Content,
        lock: true,
        cancel: true,
        max: false,
        min: false,
        ok: function () { Save_UpdateOrAdd(); }
    });
}


//为 课程专门定制
//显示弹出层 用于添加或修改对象 参数为弹出层的标题 
function ShowNewDialog_Lesson(Title) {

    //内容
    var Content = $("<div id='div_Lesson'></div>");
    //p.append($("<a>&nbsp;&nbsp;</a>"));
    Content.append($("<label>教材名称：&nbsp;&nbsp;</label>"));
    Content.append($("<label id='Dialog_CourseName'>暂无信息</label>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>年级名称：&nbsp;&nbsp;</label>"));
    Content.append($("<label id='Dialog_GradeName'>暂无信息</label>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>版本名称：&nbsp;&nbsp;</label>"));
    Content.append($("<label id='Dialog_EditionName'>暂无信息</label>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>课程名称：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_LessonName'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>课程描述：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_LessonDesc'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>所属单元：&nbsp;&nbsp;</label>"));    
    Content.append($("<div style='float:right;'id='div_LessonParent' width='153px'>所属单元</div>"));
    Content.append($("<label style='display:none;'>排序位置：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_LessonLevel'style='display:none;'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>教学目标：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_LessonMB'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>教学难点：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_LessonND'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>教学重点：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='txt_LessonZD'/>"));

    $.dialog({
        id: 'ItemID',
        title: Title,
        width: '380px',
        height: '330px',
        content: Content,
        lock: true,
        cancel: true,
        max: false,
        min: false,
        ok: function () { Save_UpdateOrAdd_Lesson(); }
    });
}
//为 单元专门定制
//显示弹出层 用于添加或修改对象 参数为弹出层的标题 
function ShowNewDialog_DanYuan(Title) {
    //内容
    var Content = $("<div id='div_Unit'></div>");
    //p.append($("<a>&nbsp;&nbsp;</a>"));
    Content.append($("<label>教材名称：&nbsp;&nbsp;</label>"));
    Content.append($("<label id='DanYuan_CourseName'>暂无信息</label>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>年级名称：&nbsp;&nbsp;</label>"));
    Content.append($("<label id='DanYuan_GradeName'>暂无信息</label>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>版本名称：&nbsp;&nbsp;</label>"));
    Content.append($("<label id='DanYuan_EditionName'>暂无信息</label>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>单元名称：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='DanYuan_LessonName'/>"));
    Content.append($("<br>"));
    Content.append($("<br>"));
    Content.append($("<label>单元描述：&nbsp;&nbsp;</label>"));
    Content.append($("<input type='text' id='DanYuan_LessonDesc'/>"));

    $.dialog({
        id: 'ItemID',
        title: Title,
        width: '380px',
        height: '280px',
        content: Content,
        lock: true,
        cancel: true,
        max: false,
        min: false,
        ok: function () { Save_UpdateOrAdd_DanYuan(); }
    });
}
