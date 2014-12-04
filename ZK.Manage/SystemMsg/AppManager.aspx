<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="AppManager.aspx.cs" Inherits="ZK.Manage.SystemMsg.AppManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
智客业务管理系统-应用管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script language="javascript" type="text/jscript">
        var id;
        var flag;
        var del;

        function noticeMagAdd() {
            //添加  cancel: true, , ok: function () { Save_UpdateOrAdd(); }
            $.dialog({ title: '添加应用', width: '395px', height: '520px', content: 'url:AppManagerEdit.aspx?ty=add&t=' + new Date().getTime().toString(), max: false, min: false });

        }
        function noticeMagEdit(id) {
            $.dialog({ title: '编辑应用', width: '395px', height: '520px', content: 'url:AppManagerEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + id, max: false, min: false });

        }
        function noticeIconEdit(id) {
            $.dialog({ title: '修改应用图标', width: '320px', height: '102px', content: 'url:AppManagerEdit.aspx?ty=editicon&t=' + new Date().getTime().toString() + '&id=' + id, max: false, min: false });

        }
        
        function Move(id, flag) {
            window.location = "/SystemMsg/AppManager.aspx?curp=disk&id=" + id + "&flag=" + flag;
        }
        function DeleteCheckedItems(id,del) {
            window.location = "/SystemMsg/AppManager.aspx?curp=disk&id=" + id + "&Del=" + del;
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
        var channelGroupName = "";

        $(function () {
            GetDataForPaging(1, PagerDivID);
        });
        //页面加载时绑定列表
        //家在分页数据列表
        function GetDataForPaging(pageindex, PagerDivID) {
            $.ajax({
                type: "Post",
                url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                data: { "Flag": "GetAppListPaging", "PageIndex": pageindex, "PageSize": pagesize },
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
                    $("#div_ResourceContent").setTemplateURL("../pagetemples/AppManager/AppManagerList.htm", null, null);
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
        <form runat="server" id="Form1" name="queryfrom">
        <input type="hidden" name="orderKey" id="Hidden1" value="ID" />
        <div class="pagePath">
            首页 >系统配置管理>应用管理</div>
        <div class="b d1">
            <ul>
                <li style="margin-left:8px;">在这里添加只供内部帐号使用的Web应用。</li>
                <li style="margin-left:8px;">集成本地系统与应用请参考“编程接口.doc”技术文档。</li>
                <li style="margin-left:8px;"><span style="color: #2fb900">提示：应用列表更改后，客户端需要重新登录才能刷新。</span></li>
            </ul>
        </div>
        <div class="searchform">
            <input type="button" value="添加" style="margin-left:15px;" onclick="noticeMagAdd();" />
        </div>
        
       <%-- <div class="pageInfo">
            <span class="fr">
                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="pno" CurrentPageButtonClass="cpb"
                    runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                    PrevPageText="上一页" ShowInputBox="Never" OnPageChanged="AspNetPager1_PageChanged"
                    CustomInfoTextAlign="Left" LayoutType="Table" UrlPaging="True" CustomInfoHTML="共有%RecordCount% 条记录"
                    CustomInfoSectionWidth="10%" ShowPageIndexBox="Auto" PageSize="2">
                </webdiyer:AspNetPager>
            </span>
        </div>--%>

         <%--<div id="div_Pager">
                            </div>--%>

                            <div id="div_ForAllContent">
            <div id="div_ResourceContent" align="right">
            </div>
            <br />
            <div>
                <table width="95%">
                    <tr>
                        <td width="30%">
                            <%-- <div>
                                <input type="checkbox" />
                                &nbsp;&nbsp; 全选&nbsp;&nbsp;<input type="button" value="批量删除" onclick="DeleteCheckedItems_TeachResource()" /></div>--%>
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
