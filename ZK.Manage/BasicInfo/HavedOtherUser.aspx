<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HavedOtherUser.aspx.cs"
    Inherits="ZK.Manage.BasicInfo.HavedOtherUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <link href="../css/saveandcancel.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
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
    <script type="text/javascript">        //关闭对话框用的
        function closeWindow() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }
        function Cancel() {
            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.close();
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
        .searchform
        {
            line-height: 30px;
            background-color: #F9FCFD;
            margin-top: 2px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #7CCF8F;
            font-family: 微软雅黑;
            font-size: 14px;
        }
        
        a:-webkit-any-link
        {
            color: rgb(51, 51, 51);
            text-decoration: underline;
            cursor: auto;
        }
    </style>
    <script type="text/javascript">
        //分页功能
        var PagerDivID = "div_Pager";
        var pagesize = 15;
        var lists = new Array();

        var strWhere = "";
        var FormatType = "";
        $(function () {
            //            var url = location.search;
            alert(0);
            //            var typestr = url.split('&');
            //            FormatType = typestr[1].substr(6);

            GetDataForPaging(1, PagerDivID);
        });
        //页面加载时绑定列表
        //家在分页数据列表
        function GetDataForPaging(pageindex, PagerDivID) {
            alert(0);
            $.ajax({
                type: "Post",
                url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                data: { "Flag": "GetHaveOtherUserListPaging", "strWhere": strWhere, "PageIndex": pageindex, "PageSize": pagesize },
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
                    $("#div_ResourceContent").setTemplateURL("../pagetemples/basicInfo/OrgAndUser.htm", null, null);
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
    <script language="javascript" type="text/javascript">
        function selectAll(obj) {
            alert(0);
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
    </script>
</head>
<body style="font-family: 微软雅黑; font: 12px/1.5 arial,宋体;">
    <div>
        <form runat="server" id="queryfrom" name="queryfrom">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="searchform">
            <span style="margin-left: 0px;">姓名：</span>
            <asp:TextBox ID="txt_username" runat="server" value="" onfocus=""></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
        </div>
        <%--<div id="div_ForAllContent">
            <div id="div_ResourceContent" align="right">
            </div>
            <br />
            <div>
                <table width="95%">
                    <tr>
                         <td width="30%">
                            <div>
                             <span style="margin-right:10px;" >&nbsp;<input style="margin-left:13px;" type="checkbox" onclick="CheckAll()" />全选
            </span>
                     <input type="button" value="批量删除"   onclick="DeleteCheckedItems()" />
                              </div>                               
                        </td>
                        <td align="right">
                            <div id="div_Pager">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" Width="100%" OnPageIndexChanging="gvwDesignationName_PageIndexChanging"
                    AllowPaging="True" PageSize="15" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" PageIndex="0" Font-Bold="False" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <%--<ItemStyle Width="60px" VerticalAlign="Middle" />--%>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <%--<HeaderTemplate>
                        <input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                    </HeaderTemplate>--%>
                        </asp:TemplateField>
                        <asp:BoundField DataField="USERID" HeaderText="用户ID" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="USERID">
                            <%--<ItemStyle Width="100px" />--%>
                        </asp:BoundField>
                        <asp:BoundField DataField="USERNAME" HeaderText="用户名" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="USERNAME">
                            <%--<ItemStyle Width="120px" />--%>
                        </asp:BoundField>
                        <asp:BoundField DataField="NICKNAME" HeaderText="昵称" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="NICKNAME">
                            <%--<ItemStyle Width="120px" />--%>
                        </asp:BoundField>
                        <asp:BoundField DataField="ACTUALNAME" HeaderText="姓名" ReadOnly="True" HeaderStyle-HorizontalAlign="Left" SortExpression="ACTUALNAME">
                            <%--<ItemStyle Width="100px" />--%>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="性别" HeaderStyle-HorizontalAlign="Left">
                            <%--<ItemStyle Width="60px" />--%>
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%#GetSex(Eval("SEX"))%>' BorderWidth="0"
                                    ReadOnly="true" Width="20px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Left" Font-Names="微软雅黑" Font-Size="14px" BackColor="#F8F7EF" />
                    <PagerStyle HorizontalAlign="Right" Font-Size="14px" Height="45px" VerticalAlign="Middle"
                        Font-Bold="False" />
                    <PagerTemplate>
                        当前第:
                        <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                        页/共:
                        <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                        页
                        <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page">首页</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                            CommandName="Page">上一页</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page">下一页</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page">尾页</asp:LinkButton>
                        第
                        <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='' />页
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                            CommandName="Page" Text="转到" />
                    </PagerTemplate>
                    <RowStyle BorderColor="#F7DAF0" BorderStyle="Solid" BorderWidth="1px" Height="28px" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="float: right; margin-bottom: 20px;" class="ui_buttons">
            <asp:Button ID="btnSave" class="ui_state_highlight" runat="server" Text="保存" OnClick="btnSave_Click" />
            <a id="A1" runat="server">
                <asp:Button ID="Button1" runat="server" Text="取消" OnClientClick="Cancel()" /></a>
        </div>
        </form>
    </div>
</body>
</html>
