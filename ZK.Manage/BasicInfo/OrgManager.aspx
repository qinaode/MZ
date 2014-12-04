<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="OrgManager.aspx.cs" Inherits="ZK.Manage.BasicInfo.OrgManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
    智客业务管理系统-组织机构管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <script src="../inc/dtree_pOrgManger.js" type="text/javascript"></script>
    <script src="../js/MoveJs.js" type="text/javascript"></script>
    <script language="javascript" type="text/jscript">
        //添加组织机构
        function orgAdd(parentid) {
            //添加 ,cancel: true,ok: function () { orgAddSave();}
            $.dialog({ title: '添加组织机构', width: '330px', height: '120px', content: 'url:orgManagerEdit.aspx?ty=add&t=' + new Date().getTime().toString() + '&id=' + parentid, max: false, min: false });

        }

        function orgAdd(id) {
            $.dialog({ title: '添加子类组织机构', width: '330px', height: '120px', content: 'url:orgManagerEdit.aspx?ty=addchild&t=' + new Date().getTime().toString() + '&id=' + id, max: false, min: false });

        }
        //        function childCateEdit(id, pid, name) {
        //            $.dialog({ title: '编辑组织机构', width: '330px', height: '125px', content: 'url:orgManagerEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + id +'&pid='+pid+'&name='+name, max: false, min: false });

        //        }
        function childCateEdit1(id) {
            $.dialog({ title: '编辑组织机构', width: '330px', height: '120px', content: 'url:orgManagerEdit.aspx?ty=edit&t=' + new Date().getTime().toString() + '&id=' + id, max: false, min: false });

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >基础信息管理>组织机构管理</div>
           <div class="b d1">
            <ul>
                <li>可创建多级的部门列表，点击一个“部门”可修改部门名称。</li>
                <li><span style="color: #2fb900">提示：部门列表更改后，客户端需要重新登录才能刷新。</span></li>
            </ul>
        </div>
        <div class="searchform">
           <%-- 分类管理名称：
            <asp:TextBox ID="txtCategoryName" runat="server" value="" onfocus="" size="16"></asp:TextBox>--%>
            <%--<input name="txtCategoryName"  type="text" size="16" />--%>
           <%-- <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />--%>
           <p>
                <a href="javascript: d.openAll();"><input type="button" style="font-family:微软雅黑; font-size:9;" value="展开"/></a>
                 <a href="javascript: d.closeAll();"><input type="button" style="font-family:微软雅黑; font-size:9;" value="收缩"/></a>
            <input type="button" style="font-family:微软雅黑; font-size:9;" value="添加" onclick="orgAdd();" /> </p>
            <%-- <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" />--%>
        </div>
       
        <div class="whiteline">
        </div>
        <div class="dtree">
            <%--<p>
                <a href="javascript: d.openAll();">展开</a> | <a href="javascript: d.closeAll();">收缩</a></p>--%>
            <asp:Literal runat="server" ID="lblResult"></asp:Literal>
            <script type="text/javascript">
		<!--

                d = new dTree('d');
        <asp:Repeater ID="pCategoryList" runat="server">
            <ItemTemplate>
        d.add(<%#Eval("DEPARTID")%>, <%#Eval("PARENTDEPARTID")%>, '<%#Eval("DEPARTNAME")%>', '');
            </ItemTemplate>
       </asp:Repeater>
	   document.write(d);

		//-->

            </script>
            <div class="dTreeNode">
                <div style="float: left; margin: 0; padding-left: 15px; font-weight: bold;">
                    共<asp:Literal ID="litCount" runat="server"></asp:Literal>个部门</div>
            </div>
        </div>
        <div>
        </div>
        </form>
    </div>
</asp:Content>
