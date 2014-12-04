<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="SpecialTopicImgMag.aspx.cs" Inherits="ZK.Manage.SpecialTopic.SpecialTopicImgMag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">

    <link rel="stylesheet" href="/css/userInfo.css" />
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
     <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script src="../js/checkbox.js" type="text/javascript"></script>  
    <style type="text/css">
        .style3
        {
            width: 90px;
            height: 40px;
            text-align: right;
            font-size:12px;
        }
        td
        {
            border-bottom: 1px #e4e2da dashed;
            min-height: 30px;
            line-height: 150%;
            color: #333333;
            padding: 3px 5px 3px 10px;
        }
        select
        {
            border: solid 1px #C3DFEA;
        }
    </style>
    <script type="text/javascript">
        function CheckFile(f, p) {
            //判断图片类型
            var f = document.getElementById("File1").value;
            if (f == "")
            { alert("请上传图片"); return false; }
            else {
                if (!/\.(|GIF|JPG|PNG)$/.test(f)) {
                    alert("图片类型必须是.gif,jpg,png中的一种")
                    return false;
                }
            }
        }
        var specialId;
        var flag;
        var id;
        var del;

        $(function () {
            var url = location.search;

            var typestr = url.split('&');

            specialId = typestr[2].substr(10);
           
        })   
         //删除选中的项 现仅限于teachchannel 和 用该分页者
        function DeleteCheckedItems() {             
            //  记录被选择的项的ID
            var checkedlist = new Array();

            $("input[name='checkbox_1']").each(function (i, item) {                 
                if ($(item).attr("checked")) {                     
                       checkedlist.push($(item).attr("id"));  
                }
            });
               window.location = "/SpecialTopic/SpecialTopicImgMag.aspx?curp=topic&ty=imgMag&specialId=" + specialId + "&checkedlist=" + checkedlist;
           }     
         
           //删除选中的项
           function DeleteCheckedItems_ＭoralResource(btn_id) {
               var fileid = btn_id.substr(7);
               var ifcontinue = confirm("确实要删除选中项吗");
               if (!ifcontinue) {
                   return false;
               };
               $.ajax({
                   type: "Post",
                   url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                   data: { "Flag": "DeleteCheckedImgResource", "ID": fileid },
                   datatype: "text/json",
                   success: function (backdata) {

                       if (backdata == "文件夹不为空") {
                           alert("文件夹不为空，删除失败");
                           return;
                       }
                       if (backdata == "true" || backdata == "True") {
                           alert("删除成功");
                           GetDataForPaging(1, PagerDivID);
                           return;
                       }
                       else {
                           alert("删除失败");
                           return;
                       }
                   }
               });
           }

           //移动
           function Move1(id,flag) {
               var ifcontinue = confirm("确实要移动选中项吗");
              
               if (!ifcontinue) {
                   return false;
               };
               $.ajax({
                   type: "Post",
                   url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                   data: { "Flag": "MoveImgResource1", "ID": id, "moveFlag": flag },
                   datatype: "text/json",
                   success: function (backdata) {

                       if (backdata == "文件夹不为空") {
                           alert("文件夹不为空，移动失败");
                           return;
                       }
                       if (backdata == "true" || backdata == "True") {
                           alert("移动成功");
                           GetDataForPaging(1, PagerDivID);
                           return;
                       }
                       else {
                           alert("移动失败");
                           return;
                       }
                   }
               });
           }
    </script>

    <script type="text/javascript">
        //分页功能
        var PagerDivID = "div_Pager";
        var pagesize = 15;
        var lists = new Array();

        var strWhere = "";

        $(function () {

            GetDataForPaging(1, PagerDivID);
        });
        //页面加载时绑定列表
        //家在分页数据列表
        function GetDataForPaging(pageindex, PagerDivID) {

            $.ajax({
                type: "Post",
                url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                data: { "Flag": "GetSpecialImgListPaging","strWhere":specialId, "PageIndex": pageindex, "PageSize": pagesize },
                datatype: "text/json",
                success: function (backdata) {
                    if (backdata == "" || backdata == "[]") {
                        return;
                    };

                    var tempjson = $.parseJSON(backdata);
                    if (tempjson["DataList"] == "") {
                        $("#div_ForAllContent").css("visibility", "hidden");
                        return;
                    }
                    $("#div_ResourceContent").setTemplateURL("../pagetemples/SpecialTopic/SpecialImgMag.htm", null, null);
                    $("#div_ResourceContent").processTemplate(tempjson["DataList"]);
                    $("#div_ForAllContent").css("visibility", "visible");
                    CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >专题管理>专题图片管理
        </div>
        <div class="searchform">
            <span style="margin-left: 15px; font-size: 13px; font-family: 微软雅黑; font-weight: bold;">
                专题名称：</span> <span style="font-size: 13px; font-family: 微软雅黑; font-weight: bold;">
                    <asp:Label ID="lblSpecialTopic" runat="server" Text="XXXX专题"></asp:Label></span>
        </div>
        <div id="div_ForAllContent">
            <div id="div_ResourceContent" align="right">
            </div>
            <br />
            <div>
                <table width="95%">
                    <tr>
                         <td width="30%">
                            <div>
                                <span style="margin-right:10px;" >&nbsp;<input  type="checkbox"  id="hhhh" onclick="SelectAll()" />全选</span>
                                <input type="button" value="批量删除"   onclick="DeleteCheckedItems()" />
                              </div>
                               <%-- <input type="checkbox" class="check_ForAll" />
                                &nbsp;&nbsp; <a onclick="CheckAll()">全选</a>&nbsp;&nbsp;<input type="button" value="批量删除"
                                    onclick="DeleteCheckedItems()" /></div>--%>
                        </td>
                        <td align="right">
                            <div id="div_Pager">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #d2eefb;
            width: 98%; margin: 0px auto 8px; margin-top: 10px;" width="100%" id="tablesetting">
            <tbody>
                <tr class="line">
                    <td colspan="3">
                        <asp:Image ID="img_Special" runat="server" Width="256" Height="97" />
                    </td>
                </tr>
                <tr class="line">
                    <td class="style3" >
                        精彩专题图片：
                    </td>
                    <td colspan="2">
                        <input name="weblogo" class="input" runat="server" id="upFile" size="30" type="file"
                            onclick="CheckFile(this.value,this)" />
                    </td>
                </tr>
                <tr class="line">
                    <td class="style3" >
                        图片链接：
                    </td>
                    <td style="width: 400px;">
                        <%--<input name="filestorepath" class="input" runat="server" id="txtImgURL" size="50"  type="text"/>--%>
                        <asp:TextBox ID="txtImgURL" runat="server" Width="390px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnUploadImg" runat="server" Text="添加" OnClick="btnUploadImg_Click"
                            Style="height: 21px" />
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
    </div>
</asp:Content>
