/*
1 文件行点击事件 List_Info_Click


*/



//文件行点击事件
function List_Info_Click(this_id) {

    var infoid = this_id.substr(5);
    $(".xuan_image").attr("src", "/imagesN/diskImages/xuankuang.png");
    $(".xuan_image").removeClass("checked");
    $(".xuan_image").addClass("unchecked");
    $("#Image_" + infoid).attr("src", "/imagesN/diskImages/xuankuang01.png");
    $("#Image_" + infoid).removeClass("unchecked");
    $("#Image_" + infoid).addClass("checked");
}

