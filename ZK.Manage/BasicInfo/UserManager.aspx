<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="UserManager.aspx.cs" Inherits="ZK.Manage.BasicInfo.UserManager" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
    智客知识管理系统管理后台-用户管理</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <link rel="stylesheet" href="/css/userInfo.css" />
    <link rel="stylesheet" href="/css/productCategory.css" />
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/CommonFUNC.js" type="text/javascript"></script>
    <link href="../css/pagering_1.01.css" rel="stylesheet" type="text/css" />
    <script src="../commonjs/jquery-jtemplates.js" type="text/javascript"></script>
    <script src="../commonjs/pagering_1.01.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
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
        var Pwd;
        var Del;
        var IsLock;
        var userid;
        var title;
        var flag;

        function AddOrEditUser(userid, flag, title) {

            $.dialog({ title: title, width: '450px', height: '680px', content: 'url:UserAddOrEdit.aspx?IsDepOrUser=user&_a=' + Math.random() + '&flag=' + flag + '&userid=' + userid, lock: true, max: false, min: false });
        }
        //编辑修改或添加
        function AddOrEditRole(id, title) {
            var userid = id.substr(5);

            $.dialog({ title: title, width: '300px', height: '310px', content: 'url:/../SettingManage/license.aspx?_a=' + Math.random() + '&userid=' + userid, lock: true, max: false, min: false });
        }

        function Delete(id, Del) {
            var idDel = id.substr(7);
            //alert(id);
           // alert("在此");
            window.location = "/BasicInfo/UserManager.aspx?curp=system&id=" + idDel + "&Del=" + Del;
        }
      
        function IsLock(id, IsLock) {
            var id11 = id.substr(5);
           
            window.location = "/BasicInfo/UserManager.aspx?curp=system&id=" + id11 + "&IsLock=" + IsLock;
        }


        function PwdEdit(id, Pwd) {
            var pwdid = id.substr(4);
            
            window.location = "/BasicInfo/UserManager.aspx?curp=system&id=" + pwdid + "&Pwd=" + Pwd;
        }
        //分页功能
        var PagerDivID = "div_Pager";
        var pagesize = 15;
        var lists = new Array();

        var staste = "";
        var strName = "";
        var struserid = "";
        $(function () {

            GetDataForPaging(1, PagerDivID);
        });
        //页面加载时绑定列表
        //加载分页数据列表
        function GetDataForPaging(pageindex, PagerDivID) {

            $.ajax({
                type: "Post",
                url: "../ashx/AppManager.ashx?_a=" + Math.random(),
                data: { "Flag": "GetUserMagListPaging", "strName": strName, "struserid": struserid, "staste": staste, "PageIndex": pageindex, "PageSize": pagesize },
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

                    $("#div_ResourceContent").setTemplateURL("../pagetemples/basicInfo/UserMag.htm", null, null);
                    $("#div_ResourceContent").processTemplate(tempjson["DataList"]);
                    $("#div_ForAllContent").css("visibility", "visible");
                    CreatePageControl(pageindex, pagesize, parseInt(tempjson["TotalNumber"], 10), PagerDivID, lists);
                }
            });
        }

        //绑定条件查询列表
        function GetSearchListByCondition() {
            strName = $("#txt_username").val();
            struserid = $("#txt_userid").val();             
            staste = $("#select_userstate").val();
            GetDataForPaging(1, PagerDivID);

        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >客户服务 >会员信息</div>
        <div class="searchform">
           <%-- 姓名：--%>
            <asp:TextBox ID="txt_username1" runat="server" value="" onfocus="" Visible="false"></asp:TextBox>
             <span style="margin-left:15px;">  姓名：</span>
            <input type="text" id="txt_username" />
            账号：
            <asp:TextBox runat="server" ID="txt_userid1" type="text" size="16" Visible="false"></asp:TextBox>
                 <input type="text" id="txt_userid" />
            <%--组织机构：--%><label style="display:none;"><select
                id="select_Depart" runat="server">
            </select></label>
            <label>
                状态：<select name="select" id="select_userstate" >
                    <option value="-1" >全部状态</option>
                    <option value="0">锁定</option>
                    <option value="1">解锁</option>
                </select>&nbsp;&nbsp;
            </label>
            <input type="button" id="btnSearch" onclick="GetSearchListByCondition()"
                value="搜索" />
            <%--<asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />--%>
            &nbsp;&nbsp;&nbsp;<input type="button" id="btn_AddNew" onclick="AddOrEditUser('','0','添加用户')"
                value="添加" />
        </div>
        <%-- <div class="sort">
            排序： <a href="javascript:void(0);" onclick="javascript:submitfrom('userlevel');" class="lightblue">
                按级别</a> ｜ <a href="javascript:void(0);" onclick="javascript:submitfrom('onlineDay');"
                    class="lightblue">按登录次数</a> ｜ <a href="javascript:void(0);" onclick="javascript:submitfrom('commentCount');"
                        class="lightblue">按评论数</a></div>--%>
                 <div id="div_ForAllContent">
            <div id="div_ResourceContent" align="right">
            </div>
            <br />
            <div>
                <table width="95%">
                    <tr>
                         <td width="30%">
                             &nbsp;
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
