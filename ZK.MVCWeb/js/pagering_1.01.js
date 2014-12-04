var cur_pageindex = 1;
//          定义全局Array lists  var lists=new Array();
//          直接调用函数：CreatePageControl(pageindex, pagesize, totalcount,PagerDivID, lists);
//          容器 id定位 dpager
//          绑定数据 GetDataForPaging(pageindex,PagerDivID); getdata 包括获取数据列表 和 获取页码条
function CreatePageControl(pageindex, pagesize, totalcount, PagerDivID, lists) {
    var pagecount = 0;
    if (totalcount != 0) {
        if (totalcount % pagesize == 0) {
            pagecount = Math.floor(totalcount / pagesize);
        }
        else {
            pagecount = Math.floor(totalcount / pagesize) + 1;
        }
      
    }
    var p = $("#" + PagerDivID);
    //$("#dpager").empty();
    p.empty();
    if (pagecount == 0) {
        return;
    }
    //首页
    $Firstpage = $("<a class='page pagebutton' id='Firstpage'>首页</a>");
    if (pageindex != 1) {
        $Firstpage.attr("href", "javascript:void(0)");
        $Firstpage.click(function () {
            GetClickedPage("Firstpage", lists);
            GetDataForPaging(1, PagerDivID);
            return false;
        });
    }
    else {
        $Firstpage.css("color", "gray").css("border-color", "gray");
        $Firstpage.addClass("NotVisible");
    }
    p.append($Firstpage);
    //上一页
    $Prevpage = $("<a class='page pagebutton' id='Prevpage'>上一页</a>");
    if (pageindex > 1) {
        $Prevpage.attr("href", "javascript:void(0)");
        $Prevpage.click(function () {
            GetDataForPaging(pageindex - 1, PagerDivID);
            //GetClickedPage("Prevpage", lists);
            return false;
        });
    }
    else {
        $Prevpage.css("color", "gray").css("border-color", "gray");
        $Prevpage.addClass("NotVisible");
    }
    p.append($Prevpage);
    //添加数字页码
    var pagestart = 0;
    var pageend = 0;
    if (pagecount <= 9) {
        pagestart = 1;
        pageend = pagestart + pagecount - 1;
    }
    else {
        if (pageindex <= 5) {
            pagestart = 1;
            pageend = 9;
        }
        else if (pageindex <= pagecount - 5) {
            pagestart = pageindex - 4;
            pageend = pagestart + 8;
        }
        else if (pageindex <= pagecount) {
            pageend = pagecount;
            pagestart = pageend - 8;
        }
        else {
            pagestart = 1;
            pageend = 9;
        }
    }
    for (var i = pagestart; i <= pageend; i++) {
        $Numpage = $("<a class='page' id='Numpage" + i + "'>" + i + "</a>");
        if (i != pageindex) {
            $Numpage.attr("href", "javascript:void(0)");
            $Numpage.click(function () {
                //alert("Numpage" + i);
                var num = parseInt($(this).text());
                cur_pageindex = num;
                GetClickedPage("Numpage" + num, lists);
                GetDataForPaging(num, PagerDivID);
            });
        }
        p.append($Numpage);
    }
    //下一页
    $Nextpage = $("<a class='page pagebutton' id='Nextpage'>下一页</a>");
    if (pageindex < pagecount) {
        $Nextpage.attr("href", "javascript:void(0)");
        $Nextpage.click(function () {
            //GetClickedPage("Nextpage", lists);
            GetDataForPaging(pageindex + 1, PagerDivID);
            return false;
        });
    }
    else {
        $Nextpage.css("color", "gray").css("border-color", "gray");
        $Nextpage.addClass("NotVisible");
    }
    p.append($Nextpage);
    //尾页
    $Lastpage = $("<a class='page pagebutton' id='Lastpage'>尾页</a>");
    if (pageindex < pagecount) {
        $Lastpage.attr("href", "javascript:void(0)");
        $Lastpage.click(function () {
            GetClickedPage("Lastpage", lists);
            GetDataForPaging(pagecount, PagerDivID);
            return false;
        });
    } else {
        $Lastpage.css("color", "gray").css("border-color", "gray");
        $Lastpage.addClass("NotVisible");
    }
    p.append($Lastpage);
    p.append($("<a>&nbsp;&nbsp;</a>"));
    p.append($("<a class='pagelb'>第</a>"));
    p.append($("<input type='text' style='width:20px;' id='pagenum'/>"));
    p.append($("<a class='pagelb'>页</a>"));
    $pageskip = $("<a class='page pagebutton' id='pageskip' href='javascript:void(0)'>转到</a>");
    p.append($pageskip);
    $pageother = $("<label class='pagelb'>当前为" + pageindex + "/" + pagecount + "页</label>");
    p.append($pageother);
    $totalcount = $("<label class='pagelb'>共" + totalcount + "条记录</label>");
    p.append($totalcount);
    //页码跳转
    $pageskip.click(function () {
        var Numstr = $("#pagenum").val();
        if (parseInt(Numstr, 10) <= pagecount) {
            GetDataForPaging(parseInt(Numstr, 10), PagerDivID);
        }
        return false;
    });

    $(".page:not(#Numpage" + pageindex + "):not(#Lastpage,#Firstpage,#Prevpage,#Nextpage)").hover(function () { $(this).addClass("page_hover"); }, function () { $(this).removeClass("page_hover") });
    $(".page:not(#Numpage" + pageindex + "):not(.NotVisible)").hover(function () { $(this).addClass("page_hover"); }, function () { $(this).removeClass("page_hover") });
    //$(".pagebutton").corner("6px");
    //设置点击过按钮样式
    ClickedPageShow(lists, pageindex);

}
//点击过的页码添加
function GetClickedPage(idName, lists) {
    var bool = true;
    for (var i = 0; i < lists.length; i++) {
        if (lists[i] == idName) {
            bool = false;
            break;
        }
    }
    if (bool == true) {
        lists.push(idName);
        //$("#" + idName).addClass("pagevisited");
    }
};
//点击过的页码显示
function ClickedPageShow(lists, pageindex) {
    for (var i = 0; i < lists.length; i++) {
        $("#" + lists[i]).addClass("pagevisited");
    }
    $("#Numpage" + pageindex).removeClass("pagevisited");
    $("#Numpage" + pageindex).addClass("pagecurrent");
};
//json日期格式转化为字符串日期格式
function DateFormat(JsonDate) {
    var dateint = parseInt(JsonDate.replace("/Date(", "").replace(")/", ""), 10);
    var formatedate = new Date(dateint);
    var month = formatedate.getMonth() + 1 < 10 ? "0" + (formatedate.getMonth() + 1) : formatedate.getMonth() + 1;
    var currentday = formatedate.getDate() < 10 ? "0" + (formatedate.getDate() + 1) : formatedate.getDate();
    return formatedate.getFullYear() + "-" + month + "-" + currentday + " " + formatedate.getHours() + ":" + formatedate.getMinutes() + ":" + formatedate.getSeconds();
};
function GetCurrPageIndex() {
    return cur_pageindex;
}