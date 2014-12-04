//鼠标拖动 画遮盖层
function MouseMoveAddDiv() {
    IsSelected = true;
    if (MXend > MXstart) {
        $("#MouseMovediv").css("width", (MXend - MXstart).toString() + "px");
        $("#MouseMovediv").css("left", MXstart + "px");
    }
    else {
        $("#MouseMovediv").css("width", (MXstart - MXend).toString() + "px");
        $("#MouseMovediv").css("left", MXend + "px");
    }
    if (MYend > MYstart) {
        $("#MouseMovediv").css("height", (MYend - MYstart).toString() + "px");
        $("#MouseMovediv").css("top", MYstart + "px");
    }
    else {
        $("#MouseMovediv").css("height", (MYstart - MYend).toString() + "px");
        $("#MouseMovediv").css("top", MYend + "px");
    }


    //判断元素是否在选择层中

    for (var i = 0; i < document.getElementsByName("aContent").length; i++) {

        var obj = GetElCoordinate(document.getElementsByName("aContent")[i]);
        var dleft = obj.left;
        var dright = obj.right;
        var dtop = obj.top;
        var dbottom = obj.bottom;

        var $objid = document.getElementsByName("aContent")[i].getAttribute("id");
        //alert($obj);
        var b1 = false;
        var b2 = false;
        var b3 = false;
        var b4 = false;

        var c1 = false;
        var c2 = false;
        var c3 = false;
        var c4 = false;

        //从左到右
        if (MXstart < MXend) {
            b1 = dleft >= MXstart && dleft <= MXend;
            b2 = dright >= MXstart && dright <= MXend;

            c1 = dleft <= MXstart && dright >= MXend;
            c2 = dleft >= MXstart && dright <= MXend;
        }
        else {
            b1 = dleft >= MXend && dleft <= MXstart;
            b2 = dright >= MXend && dright <= MXstart;

            c1 = dleft <= MXend && dright >= MXstart;
            c2 = dleft >= MXend && dright <= MXstart;
        }
        //从上到下
        if (MYstart < MYend) {
            b3 = dtop >= MYstart && dtop <= MYend;
            b4 = dbottom >= MYstart && dbottom <= MYend;

            c3 = dtop <= MYstart && dbottom >= MYend;
            c4 = dtop >= MYstart && dbottom <= MYend;

        }
        else {

            b3 = dtop >= MYend && dtop <= MYstart;
            b4 = dbottom >= MYend && dbottom <= MYstart;

            c3 = dtop <= MYend && dbottom >= MYstart;
            c4 = dtop >= MYend && dbottom <= MYstart;
        }
        if (b1 || b2 || c1 || c2) {
            if (b3 || b4 || c3 || c4) {
                $("#" + $objid).addClass("selected");
                List_AddClass($objid);
            }
            else {
                $("#" + $objid).removeClass("selected");
                List_RemoveClass($objid);
            }
        } else {
            $("#" + $objid).removeClass("selected");
            List_RemoveClass($objid);
        }

    } //----------------------for 结束--------------


}

function List_AddClass(infoid) {
     infoid = infoid.substr(5);
    $("#Image_" + infoid).attr("src", "/imagesN/diskImages/xuankuang01.png");
    $("#Image_" + infoid).removeClass("unchecked");
    $("#Image_" + infoid).addClass("checked");
}
function List_RemoveClass(infoid) {
     infoid = infoid.substr(5);
    $("#Image_" + infoid).attr("src", "/imagesN/diskImages/xuankuang.png");
    $("#Image_" + infoid).addClass("unchecked");
    $("#Image_" + infoid).removeClass("checked");
}

//鼠标拖动 画遮盖层
function MouseMoveAddDiv_BF() {
    IsSelected = true;
    if (MXend > MXstart) {
        $("#MouseMovediv").css("width", (MXend - MXstart).toString() + "px");
        $("#MouseMovediv").css("left", MXstart + "px");
    }
    else {
        $("#MouseMovediv").css("width", (MXstart - MXend).toString() + "px");
        $("#MouseMovediv").css("left", MXend + "px");
    }
    if (MYend > MYstart) {
        $("#MouseMovediv").css("height", (MYend - MYstart).toString() + "px");
        $("#MouseMovediv").css("top", MYstart + "px");
    }
    else {
        $("#MouseMovediv").css("height", (MYstart - MYend).toString() + "px");
        $("#MouseMovediv").css("top", MYend + "px");
    }


    //判断元素是否在选择层中

    for (var i = 0; i < document.getElementsByName("aContent").length; i++) {

        var obj = GetElCoordinate(document.getElementsByName("aContent")[i]);
        var dleft = obj.left;
        var dright = obj.right;
        var dtop = obj.top;
        var dbottom = obj.bottom;

        var $objid = document.getElementsByName("aContent")[i].getAttribute("id");
        //alert($obj);
        var b1 = false;
        var b2 = false;
        var b3 = false;
        var b4 = false;

        var c1 = false;
        var c2 = false;
        var c3 = false;
        var c4 = false;

        //
        if (MXstart < MXend) {
            b1 = dleft >= MXstart && dleft <= MXend;
            b2 = dright >= MXstart && dright <= MXend;

            c1 = dleft <= MXstart && dright >= MXend;
            c2 = dleft >= MXstart && dright <= MXend;
        }
        else {
            b1 = dleft >= MXend && dleft <= MXstart;
            b2 = dright >= MXend && dright <= MXstart;

            c1 = dleft >= MXend && dright <= MXstart;
            c2 = dleft >= MXend && dright <= MXstart;
        }
        if (MYstart < MYend) {
            b3 = dtop >= MYstart && dtop <= MYend;
            b4 = dbottom >= MYstart && dbottom <= MYend;

            c3 = dtop <= MYstart && dbottom >= MYend;
            c4 = dtop >= MYstart && dbottom <= MYend;

        }
        else {

            b3 = dtop >= MYend && dtop <= MYstart;
            b4 = dbottom >= MYend && dbottom <= MYstart;

            c3 = dtop <= MYend && dbottom >= MYstart;
            c4 = dtop >= MYend && dbottom <= MYstart;
        }
        if (b1 || b2 || c1 || c2) {
            if (b3 || b4 || c3 || c4) {
                $("#" + $objid).addClass("selected");
                List_AddClass($objid);
            }
            else {
                $("#" + $objid).removeClass("selected");
                List_RemoveClass($objid);
            }
        } else {
            $("#" + $objid).removeClass("selected");
            List_RemoveClass($objid);
        }

    } //----------------------for 结束--------------


}