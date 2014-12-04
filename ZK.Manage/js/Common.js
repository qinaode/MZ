StringFormat = (function () {
    return ({
        IsNullOrEmpty: function (str) {
            if (str !== null && str !== "") {
                return str;
            }
            else {
                return "";
            }
        }
    });
})();