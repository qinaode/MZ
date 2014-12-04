
var LessonID = "0";
var editor1;
var editor2;
var editor3;
var courseid; 
var gradeid;
var editionid;
var typeid = 0;
var orderid=0;
$(function () {
    var url = location.search; //获取url中"?"符后的字串
    var str;
    var parm;
    //根据参数 显示相应的样式 和 记录选中的课程 年级 版本
    if (url.indexOf("?") != -1) {
        str = url.split('=');
        parm = str[1];
        courseid = parm.split('_')[0].substr(1);
        gradeid = parm.split('_')[1].substr(1);
        editionid = parm.split('_')[2].substr(1);

        $(".CourseList").removeClass("Current_Course");
        $(".CourseList").removeClass("current");

        $("#TeachCou_" + courseid).addClass("Current_Course");
        $("#TeachCou_" + courseid).addClass("current");

        $(".GradeList").removeClass("Current_Grade");
        $(".GradeList").removeClass("current");

        $("#TeachGra_" + gradeid).addClass("Current_Grade");
        $("#TeachGra_" + gradeid).addClass("current");

        $(".EditionList").removeClass("Current_Edition");
        $(".EditionList").removeClass("current");

        $("#TeachEdi_" + editionid).addClass("Current_Edition");
        $("#TeachEdi_" + editionid).addClass("current");

    }
    else {

    }
    //初始加载课程信息，暂时不用，因第一次加载默认选择的是第一单元的第一课的信息
    // var lesid;
    // if ($(".LessonList").html() != null) {
    // lesid = $(".LessonList").first().attr("id").substr(9);
    //  GetInfoByLessonID(lesid);
    // }
    //课程列表点击事件的绑定
    $(".CourseList").click(function () {
        $(".CourseList").removeClass("Current_Course");
        $(".CourseList").removeClass("current");
        $(this).addClass("Current_Course");
        $(this).addClass("current");

        GetParamtersAndTurn();

    });
    //年级列表点击事件的绑定
    $(".GradeList").click(function () {
        $(".GradeList").removeClass("Current_Grade");
        $(".GradeList").removeClass("current");
        $(this).addClass("Current_Grade");
        $(this).addClass("current");

        GetParamtersAndTurn();
    });
    //版本列表点击事件绑定
    $(".EditionList").click(function () {
        $(".EditionList").removeClass("Current_Edition");
        $(".EditionList").removeClass("current");
        $(this).addClass("Current_Edition");
        $(this).addClass("current");

        GetParamtersAndTurn();
    });
    //初始化Ifram框信息//
    // document.getElementById("iframe_Filelist").src = "/Teach/AllLessonList?courseid=" + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    // if (LessonID == "0") {
    document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=0" + "&type=0" + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    //    }
    //    else {
    //        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=0" + "&type=0" + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    //    }
});
//点击条件时跳转，选择的年级，班级，版本改变时候拼接成参数子字符串id=C" + cid + "_G" + gid + "_E" + eid
function GetParamtersAndTurn() {

    var cid = $(".Current_Course").attr("id");
    var gid = $(".Current_Grade").attr("id");
    var eid = $(".Current_Edition").attr("id");
    if (typeof (cid) != "undefined") {
        cid = cid.substr(9);
    }
    else {
        window.location = "/Teach/Index";
        return;
    }
    if (typeof (gid) != "undefined") {
        gid = gid.substr(9);
    }
    else {
        window.location = "/Teach/Index";
        return;
    }
    if (typeof (eid) != "undefined") {
        eid = eid.substr(9);
    }
    else {
        window.location = "/Teach/Index";

    }
    window.location = "/Teach/Index?id=C" + cid + "_G" + gid + "_E" + eid;

};
//点击课程按钮的事件
function GetInfoByLessonID(lessonid) {
    $.ajax({
        type: "Post",
        url: "/Teach/GetInfoByLessonID",
        data: { "lessonID": lessonid },
        datatype: "json/text",
        success: function (backdata) {

            LessonID = lessonid;
            $("#p_TeachMB").html(backdata["teachMB"]);
            editor1.html(backdata["teachMB"]);
            $("#p_TeachZD").html(backdata["teachZD"]);
            editor2.html(backdata["teachZD"]);
            $("#p_TeachND").html(backdata["teachND"]);
            editor3.html(backdata["teachND"]);
            $("#txt_TeachMB").html(backdata["teachMB"]);
            $("#txt_TeachZD").html(backdata["teachZD"]);
            $("#txt_TeachND").html(backdata["teachND"]);
            $("#Edit_TeachMB").css("display", "block");
            $("#Edit_TeachZD").css("display", "block");
            $("#Edit_TeachND").css("display", "block");
            //            $("#chapter p").css("color", "black");
            //            $(".LessonList").css("color", "black");
            //            $("#lessonid_" + lessonid).css("color", "#f00");
            //初始化文件列表div
            $(".cur5").removeClass("current1");
            $(".cur4").removeClass("current1");
            $(".cur3").removeClass("current1");
            $(".cur2").addClass("current1");
        }
    });
    if (LessonID == "0") {
        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=0" + "&type=0" + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid; 
    }
    else {
        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=" + lessonid + "&type=0" + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid; 
    }
};


//编辑lesson内容
function EditLessonInfo(btnid) {
    var id = btnid.substr(5);
    var textcontent = "";
    if (id == "TeachMB") {
        editor1.sync();
        textcontent = editor1.html();
    }
    else if (id == "TeachZD") {
        editor2.sync();
        textcontent = editor2.html();
    }
    else {
        editor3.sync();
        textcontent = editor3.html();
    }
    if ($("#" + btnid).html() == "编辑") {
        $("#p_" + id).css("display", "none");
        $("#div_" + id).css("display", "block");
        $("#" + btnid).html("保存");
        return;
    }

    $("#p_" + id).css("display", "block");
    $("#div_" + id).css("display", "none");
    $("#" + btnid).html("编辑");
    //修改后台数据
    $.ajax({
        type: "Post",
        url: "/Teach/UpdateLessonInfo",
        data: { "Flag": id, "Content": textcontent, "LessonID": LessonID },
        datatype: "text",
        success: function (backdata) {
            if (backdata == "True" || backdata == "true") {
                //alert("修改成功");
                $("#p_" + id).html(textcontent);
            }
            else {
                alert("修改失败");
                //$("#txt_" + id).html($("#p_" + id).html());
            }
        }
    });
};

function fileTypeChanged(type) {
    if (LessonID == "0") {
        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=0"+ "&type=" + type + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    }
    else {
        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=" + LessonID + "&type=" + type + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    }
    
    typeid = type;
};

function Orderby(obj) {
    var orderType = obj.value;
    if (LessonID == "0") {
        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=0&type=" + typeid + "&orderType=" + orderType + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    }
    else {
        document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=" + LessonID + "&type=" + typeid + "&orderType=" + orderType+ "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;;
    }
    orderid = orderType;
};

//加载在线编辑器 1
KindEditor.ready(function (K) {
    editor1 = K.create('#txt_TeachMB', {
        resizeType: 0
    });
});
//加载在线编辑器 1
KindEditor.ready(function (K) {
    editor2 = K.create('#txt_TeachZD', {
        resizeType: 0
    });
});
//加载在线编辑器 1
KindEditor.ready(function (K) {
    editor3 = K.create('#txt_TeachND', {
        resizeType: 0
    });
});

function ShowAllLesson() {
    //alert(22);
    //document.getElementById("iframe_Filelist").src = "/Teach/Teach_FileList?lesid=0" + "&type=0" + "&orderType=" + orderid + "&courseid= " + courseid + "&gradeid= " + gradeid + "&editionid=" + editionid;
    GetParamtersAndTurn();
};
