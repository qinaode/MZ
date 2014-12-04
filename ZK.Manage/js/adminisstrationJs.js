
function moveUp(id) {
    $.ajax({
        type: "Post",
        url: "../ashx/AdmintrationMag.ashx?_a=" + Math.random(),
        data: { "ID": id },
        datatype: "text/json",
        success: function (backdata) {
            if (backdata != "-1" && backdata != "0") {
                    alert("上移成功，请刷新页面");
            }
            else {
                    alert("上移失败");
                }
        }
    });
    }

    function moveDown(id) {
        $.ajax({
            type: "Post",
            url: "../ashx/AdministrationMagDown.ashx?_a=" + Math.random(),
            data: { "ID": id },
            datatype: "text/json",
            success: function (backdata) {
                if (backdata != "-1" && backdata != "0") {
                    alert("下移成功，请刷新页面");
                }
                else {
                    alert("下移失败");
                }
            }
        });
    }