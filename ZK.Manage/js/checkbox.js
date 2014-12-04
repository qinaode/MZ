function CheckOne() {
    var j = 0;
    var checkboxs = document.getElementsByName("checkbox_1");
    for (var i = 0; i < checkboxs.length; i++) {
        var e = checkboxs[i];
        if (e.checked) {
            j++;
        }
    }
    if (j == checkboxs.length) {
        $("#hhhh").attr("checked", true);
    }
    else {
        $("#hhhh").attr("checked", false);
    }
}
function SelectAll() {
    if ($("#hhhh").attr("checked") != "checked") {
        var checkboxs = document.getElementsByName("checkbox_1");
        for (var i = 0; i < checkboxs.length; i++) {
            var e = checkboxs[i];
            e.checked = false;
        }
    }
    else {
        var checkboxs = document.getElementsByName("checkbox_1");
        for (var i = 0; i < checkboxs.length; i++) {
            var e = checkboxs[i];
            e.checked = true;
        }
    }
}
$(function () {
    var checkboxs = document.getElementsByName("checkbox_1");
    var k = 0;
    for (var i = 0; i < checkboxs.length; i++) {
        var e = checkboxs[i];
        if (e.checked) {
            j++;
        }
        if (j == checkboxs.length) {
            $("#hhhh").attr("checked", true);
        }
        else {
            $("#hhhh").attr("checked", false);
         }
    }
});
