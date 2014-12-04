<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="SpecialTopicMag.aspx.cs" Inherits="ZK.Manage.SpecialTopic.SpecialTopicMag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
     <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script src="../js/checkbox.js" type="text/javascript"></script>
               
    <script language="javascript" type="text/jscript">
        function submitfrom(key) {
            document.getElementById("orderKey").value = key;
            this.queryfrom.submit();

            return false;
        }
        //全选
        function setAllCheckboxState(name, state) {
            var elms = document.getElementsByName(name);
            for (var i = 0; i < elms.length; i++) {
                elms[i].checked = state;
            }
        }
        var id;
        var flag;
        var Del;
        var Img;
        var Res;
        var IsLock;

        function AddSpecialTopic() {              
            $.dialog({ title: '添加专题', width: '390px', height: '190px', content: 'url:SpecialTopicEdit.aspx?ty=add&t=' + new Date().getTime().toString(), max: false, min: false });            
        }

        function SpecialTopicEdit(id) {
            var ide = id.substr(5);  
            $.dialog({ title: '编辑专题', width: '390px', height: '190px', content: 'url:SpecialTopicEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + ide, max: false, min: false });              
        }

        function IsLock(id, IsLock) {
            var idLock = id.substr(5);
            window.location = "/SpecialTopic/SpecialTopicMag.aspx?curp=topic&id=" + idLock + "&IsLock=" + IsLock;
        }

        function ImgEdit(id, Img) {
            var id0 = id.substr(4);            
            window.location = "/SpecialTopic/SpecialTopicMag.aspx?curp=topic&id=" + id0 + "&Img=" + Img;
        }
        function ResEdit(id, Res) {
            var id1 = id.substr(4);
            window.location = "/SpecialTopic/SpecialTopicMag.aspx?curp=topic&id=" + id1 + "&Res=" + Res;
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
                data: { "Flag": "DeleteCheckedMoralResource", "ID": fileid },
                datatype: "text/json",
                success: function (backdata) {

                    if (backdata == "文件夹不为空") {
                        alert("请先删除该专题下的资源和图片！");
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

        //删除选中的项 现仅限于teachchannel 和 用该分页者
        function DeleteCheckedItems() {     
            var checkedlist = new Array();
            $("input[name='checkbox_1']").each(function (i, item) {
               
                if ($(item).attr("checked")) {                     
                       checkedlist.push($(item).attr("id"));
                }
            });
               window.location = "/SpecialTopic/SpecialTopicMag.aspx?curp=topic&checkedlist=" + checkedlist;             
        }
    </script>
    <style type="text/css">
        .b1, .b2, .b3, .b4, .b1b, .b2b, .b3b, .b4b, .b
        {
            display: block;
            overflow: hidden;
        }
        .b1, .b2, .b3, .b1b, .b2b, .b3b
        {
            height: 1px;
        }
        .b2, .b3, .b4, .b2b, .b3b, .b4b, .b
        {
            border-left: 1px solid #ffefa6;
            border-right: 1px solid #ffefa6;
        }
        .b1, .b1b
        {
            margin: 0 3px;
            background: #ffefa6;
        }
        .b2, .b2b
        {
            margin: 0 2px;
            border-width: 2px;
        }
        .b3, .b3b
        {
            margin: 0 1px;
        }
        .b4, .b4b
        {
            height: 2px;
            margin: 0 1px;
        }
        .d1
        {
            padding: 0 8px 0 8px;
            background: #fff2b7;
            font-size: 13px;
            line-height: 18px;
            color: #808080;
        }
        .d1 ul
        {
            margin: 0 8px 0 18px;
            padding: 0;
        }
        .d1 li
        {
            margin: 2px 0 2px 0;
            list-style-type: disc;
            list-style-position: outside;
        }
    </style>

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
                data: { "Flag": "GetSpecialTopicListPaging", "strWhere": strWhere, "PageIndex": pageindex, "PageSize": pagesize },
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
                    $("#div_ResourceContent").setTemplateURL("../pagetemples/SpecialTopic/SpecialTopicList.htm", null, null);
                    $("#div_ResourceContent").processTemplate(tempjson["DataList"]);
                    $("#div_ForAllContent").css("visibility", "visible");
                    CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
                }
            });
        }

        //绑定条件查询列表
        function GetSearchListByCondition() {
            strWhere = $("#txtSpecialTypeName").val();
           
            GetDataForPaging(1, PagerDivID);
            
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >精彩专题管理 >专题管理
        </div>
        <div class="searchform" style="font-family:微软雅黑; font-size:14px;">
            <span style="margin-left:15px;">名称：</span>
            <input type="text" id="txtSpecialTypeName" />
          <%--  <asp:TextBox ID="txtSpecialTypeName"  value="" onfocus=""></asp:TextBox>--%>
            <%--<asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />--%>
             &nbsp;&nbsp;&nbsp;<input type="button" id="btnSearch" onclick="GetSearchListByCondition()"
                value="搜索" />
            &nbsp;&nbsp;&nbsp;<input type="button" id="btn_AddNew" onclick="AddSpecialTopic()"
                value="添加" />
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
                             <span style="margin-right:10px;" >&nbsp;<input style="margin-left:13px;" type="checkbox" id="hhhh"  onclick="SelectAll()" />全选
            </span>
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
        </form>
    </div>
</asp:Content>
