
$(function () {
    $(".list1").click(function () {
        $(".list2").removeClass("current");
        $(".content2").css("display", "none");
        $(this).addClass("current");
        $(".content1").css("display", "block");
    });
    $(".list2").click(function () {
        $(".list1").removeClass("current");
        $(".content1").css("display", "none");
        $(this).addClass("current");
        $(".content2").css("display", "block");
    });
    $(".lists1").click(function () {
        $(".lists2").removeClass("current");
        $(".contents2").css("display", "none");
        $(".lists3").removeClass("current");
        $(".contents3").css("display", "none");
        $(this).addClass("current");
        $(".contents1").css("display", "block");
    });
    $(".lists2").click(function () {
        $(".lists1").removeClass("current");
        $(".contents1").css("display", "none");
        $(".lists3").removeClass("current");
        $(".contents3").css("display", "none");
        $(this).addClass("current");
        $(".contents2").css("display", "block");
    });
    $(".lists3").click(function () {
        $(".lists1").removeClass("current");
        $(".contents1").css("display", "none");
        $(".lists2").removeClass("current");
        $(".contents2").css("display", "none");
        $(this).addClass("current");
        $(".contents3").css("display", "block");
    });
    $(".cur2").click(function () {
        $(".cur3").removeClass("current1");
        $(".cur4").removeClass("current1");
        $(".cur5").removeClass("current1");
        $(this).addClass("current1");
    });
    $(".cur3").click(function () {
        $(".cur2").removeClass("current1");
        $(".cur4").removeClass("current1");
        $(".cur5").removeClass("current1");
        $(this).addClass("current1");
    });
    $(".cur4").click(function () {
        $(".cur3").removeClass("current1");
        $(".cur2").removeClass("current1");
        $(".cur5").removeClass("current1");
        $(this).addClass("current1");

    });
    $(".cur5").click(function () {
        $(".cur3").removeClass("current1");
        $(".cur4").removeClass("current1");
        $(".cur2").removeClass("current1");
        $(this).addClass("current1");

    });
})

      









