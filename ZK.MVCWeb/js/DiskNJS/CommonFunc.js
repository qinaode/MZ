/*
包含内容：

获取标签的绝对位置

获取标签的相对父容器位置

删除数组指定的值


*/

//获取不带扩展名的文件名
function GetFileNameWithoutExt(filename) {

    var flagsplit = filename.split('.');
    var filenamewithoutext = filename;
    if (flagsplit.length > 1) {
        var lastlength = flagsplit[flagsplit.length - 1].length;
        filenamewithoutext = filename.substr(0, filename.length - lastlength - 1);
    }
    return filenamewithoutext;
}


//获取标签的绝对位置
function GetElCoordinate(e) {
    var t = e.offsetTop;
    var l = e.offsetLeft;
    var w = e.offsetWidth;
    var h = e.offsetHeight;
    while (e = e.offsetParent) {
        t += e.offsetTop;
        l += e.offsetLeft;
    }
    return {
        top: t,
        left: l,
        width: w,
        height: h,
        bottom: t + h,
        right: l + w
    }
}


//获取标签的相对父容器位置
function GetElPositonToParent(e) {
    var t = e.offsetTop;
    var l = e.offsetLeft;
    var w = e.offsetWidth;
    var h = e.offsetHeight;
    return {
        top: t,
        left: l,
        width: w,
        height: h,
        bottom: t + h,
        right: l + w
    }
}

//删除数组指定的值
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
};
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};