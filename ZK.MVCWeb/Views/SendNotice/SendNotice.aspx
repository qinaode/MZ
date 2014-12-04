<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发送公告</title>
    <link href="../../css/saveandcancel.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        img{ border: 0;}
        #Range{ border:0; margin:0; padding:0;}
    </style>
     <script src="../../js/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script src="../../js/ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <link href="../../Scripts/jquery-ui-1.10.3.custom/development-bundle/themes/base/jquery.ui.all.css"
        rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        var userID;
        $(function () {
            var paraString = location.search;
            //多个参数用&分隔，将参数字符串转为数组，使每个参数值存于一个数组元素中
            var paras = paraString.split("&");
            //每个数组元素中"="后面的值即参数值
            userID = paras[0].substr(paras[0].indexOf("=") + 1);

            //初始化公告范围页面
            $("#Range").dialog({
                autoOpen: false,
                height: 520,
                width: 623,
                draggable: true,
                resizable: false,
                modal: true
            });
        });
        function Show(id) {
            if (id == 0) {
                Link1.style.display = "none";
                Link2.style.display = "none";
            }
            if (id == 1) {
                Link1.style.display = "";
                Link2.style.display = "none";
            }
            if (id == 2) {
                Link1.style.display = "none";
                Link2.style.display = "";
            }
        }

        function SendNotice() {
            var title = $("#txtTitle").val();
            if (title == "") {
                alert("请输入标题！");
                return;
            }

            var content = $("#txtConnent").val();
            if (content == "") {
                alert("请输入内容！");
                return;
            }
            var range = $("#ids").val();
            if (range == "") {
                alert("请选择发送范围！");
                return;
            }
            var onlion = 0;
            if (document.getElementById('checkedOnline').checked == true) {
                onlion = 1;
            }
            var checkedval = "";
            $(".redio").each(function () {
                if ($(this).attr("checked")) {
                    checkedval = $(this).val();
                }
            })

            var linkText = "";
            //   var rdoSelectVal = document.getElementsByName("radioselect");
            if (checkedval == 1) {
                linkText = $("#txtLinkAddress").val();
                if (linkText == "") {
                    alert("请输入链接地址！");
                    return;
                }
                else {
                    linkText = $.trim($("#txtLinkAddress").val());
                    if (linkText.length >= 7) {
                        if (linkText.substr(0, 7) != "http://" && linkText.substr(0, 8) != "https://") {
                            // MessageBox.Show(this, "链接地址必须以'http://'或'https://'开头");
                            alert("链接地址必须以http://或https://开头");
                            return;
                        }
                    }
                    if (linkText.length < 7) {
                        //MessageBox.Show(this, "链接地址必须以http://或https://开头");
                        alert("链接地址必须以http://或https://开头");
                        return;
                    }
                }
            }
            if (checkedval == 2) {
                linkText = CKEDITOR.instances.htmlcode.getData();
//                alert(linkText);
//                linkText = linkText.substring(3, linkText.length - 5);                  
                if (linkText == "") {
                    alert("请输入网页编辑！");
                    return;
                }
            }

            $.ajax({
                type: "Post",
                url: "/SendNotice/BtnSendNotice",
                data: { "title": title, "content": content, "range": range, "onlion": onlion, "linkText": linkText, "checkedval": checkedval, "userID": userID },
                datatype: "text/json",
                success: function (backdata) {
                    if (backdata == "1") {
                        alert("公告发送成功！");
                        //                        title = "";
                        //                        content = "";

                    }
                    else {
                        alert("公告发送失败！");
                    }
                }
            });
        }
       
        //关闭对话框
        window.closeThisWindow = function () {
            $("#Range").dialog("close");
        };
        //选择公告范围
        function SelectNoticeRange() {
            var num = $(".resultvalue").length;

            if (num == 0) {
                $("#if_Range").attr("src", "/SendNotice/SendRange");
                $("#Range").dialog("open");
                return false;
            }
            else {
                var ids = $("#ids").val();
                $("#if_Range").attr("src", "/SendNotice/SendRange?ids="+ids);
                $("#Range").dialog("open");
            }


            // window.location = "/SendNotice/SendRange";
        }
        //清空内容
        function clearContent() {
//            alert(0);
//            $("#txtRange").html("");
//            $("#ids").val("");
//            $("#txtTitle").val("");
//            $("#txtConnent").val("");
//            $("#checkedOnline").attr("checked", false);
//            $("input[type=radio]").attr("checked", false);
//            $("#txtLinkAddress").val("");
//            $("#htmlcode").val("");
//            $("#txtLinkAddress").css("display", "none");
//            $("#htmlcode").css("display", "none");
//            Show(0);
            //alert(top.parent.window.$("ul li").first().html());
            top.parent.window.$("ul li").first().click();
           // alert(top.parent.window.$("ul li").first());
            //top.parent.setseleted();
            top.parent.ShowAllNotice();
        }
        function div_deleted(id, e) {
            var divhtml = $("#txtRange");
            var pid = id;
            var obj = $(pid);

            var re = $(obj).parent('div');
            re.remove();
            //            obj.remove();
            //=========第二种方式=====
            e = e || window.event;
            if (e.preventDefault) {
                e.preventDefault();
                e.stopPropagation();
            } else {
                e.returnValue = false;
                e.cancelBubble = true;
            }
            var num = $(".resultvalue").length;
            if (num == 0) {
                SelectNoticeRange();
            }
        }

    </script>
    <style type="text/css">
         table td{font-size: 12px;}
        .txtBox
        {
            width: 507px;
            vertical-align: middle;
            border-color: #9A9A9A;
            border: 1px solid #999;
            padding-left: 5px;
            padding-top: 1px;
            padding-bottom: 3px;
        }
        .new_title_box
        {
            background: #f5f8fd;
            border-bottom: 1px solid #e4e4e4;
            overflow: hidden;
            padding: 5px 7px;
        }
        .btn
        {
            border: 0px;
            text-align: center;
            background-color: transparent;
            color: #fff;
        }
        .btn-login-active
        {
            background-image: url(../../imagesN/sendNotice/send.png);
        }
        .btncancel
        {
            border: 0px;
            padding-top: 2px;
            text-align: center;
            background-image: url(../../imagesN/sendNotice/cancel.png);
        }
        .style1
        {
            width: 85px;
        }
    </style>
</head>
<body style="font-family: lucida Grande,Verdana; font-size: 14px!important;">
    <div>
        <div class="new_title_box">
            <div class="left">
                <span class="bold black b_size ftn_t_bl" style="font-weight: bold;">发布新公告</span></div>
        </div>
        <table border='0' cellpadding='5' cellspacing='0' style="width:90%">
            <tr style="height: 5px; line-height: 5px;">
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <span style="color: #1e5494;">公告范围</span>
                </td>
                <td>
                    <div id="txtRange" style="width: 512px; height: 20px; vertical-align: middle; border: 1px solid #999;"
                        onclick="SelectNoticeRange();">
                    </div>
                    <input id="ids" type="text" style="display: none;" />
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    公告标题
                </td>
                <td>
                    <input type="text" id="txtTitle" class="txtBox" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="right" class="style1">
                    公告内容
                </td>
                <td>
                    <textarea id="txtConnent" style="width: 500px; height: 100px; float: left; padding-left: 10px;
                        font-size: 15px; font-family: lucida Grande,Verdana;" cols="70" rows="6"></textarea>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td class="style1">
                </td>
                <td>
                    <input type="checkbox" id="checkedOnline" />
                    <span style="margin-top: 0px;">只发给在线用户</span>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td align="right" class="style1">
                    网页链接
                </td>
                <td>
                    <input id="Radio1" type="radio" class="redio" name="radioselect" value="0" onclick="Show(this.value)" />无网页链接
                    <input id="Radio2" type="radio" class="redio" name="radioselect" value="1" onclick="Show(this.value)" />外部网页链接
                    <input id="Radio3" type="radio" class="redio" name="radioselect" value="2" onclick="Show(this.value)" />自定义网页内容
                </td>
            </tr>
            <tr id="Link1" style="display: none;">
                <td align="right" class="style1">
                    链接地址
                </td>
                <td>
                    <input type="text" class="txtBox" id="txtLinkAddress" />
                </td>
            </tr>
            <tr id="Link2" runat="server" style="display: none;">
                <td valign="top" align="right" class="style1">
                    网页编辑
                </td>
                <td>
                    <textarea class="ckeditor" id="htmlcode" name="htmlcode" style="width: 500px; border: 1px solid #999;
                        font-size: 15px;" cols="80" rows="10"></textarea>
                </td>
            </tr>
            <tr style="height: 15px;">
                <td colspan="2">
                </td>
            </tr>
        </table>
        <div id="Range">
            <iframe id="if_Range" frameborder="0" height="100%" width="100%;" framespacing="0" style=" border:0;"></iframe>
        </div>
        <div class="new_title_box">
            <input type="button" class="btn btn-login-active" id="btnSend" value="发送" style="cursor: pointer;
                margin-left: 60px; width: 60px; height: 26px; font-size: 15px;" onclick="SendNotice();" />
            <%-- <a class="btn_gray" onclick="SendNotice()" style="margin-right: 5px;">
                <img src="../../imagesN/sendNotice/send.png" class="ico_home" />发送</a>--%>
            <a id="A1" onclick="clearContent()">
                <input type="button" id="btnCancel" class="btncancel" style="cursor: pointer; width: 60px;
                    height: 26px; font-size: 15px;" value="取消"  /></a>
        </div>
    </div>
</body>
</html>
