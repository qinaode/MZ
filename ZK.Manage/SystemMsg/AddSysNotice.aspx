<%@ Page Title="" Language="C#" MasterPageFile="~/ZKManage.Master" AutoEventWireup="true"
    CodeBehind="AddSysNotice.aspx.cs" Inherits="ZK.Manage.SystemMsg.AddSysNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphJs" runat="server">
    <script src="../commonjs/jquery-1.4.4.min.js" type="text/javascript"></script>
    <%--<script src="../js/jquery-1.10.1.js" type="text/javascript"></script>--%>
    <script src="../commonjs/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/addSysNotice.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        function closeWindow() {
        //            var api = frameElement.api, W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
        //            api.reload();
        //            api.close();
        //        }
        function Show(id) {
            if (id == 0) {
                Link1.style.display = "none";
                Link2.style.display = "none";
            }
            if (id == 1) {//1
                //$("#Link1").css("visiblity", "visible");
                Link1.style.display = "";
                Link2.style.display = "none";
            }
            if (id == 2) {//2
                Link1.style.display = "none";
                Link2.style.display = "";
            }
        }
        function ShowUser() {
            var num = $(".resultvalue").length;
            if (num == 0) {
                $.dialog({ title: '发送公告范围', width: '620px', height: '500px', content: 'url:NoticeRange.aspx?_a=' + Math.random(), max: false, min: false, scroll: true });
            }
            else {
                return;
            }
        }
        //        $(function () {
        //            var htmlids = sending();
        //            W.document.getElementById('ids').value = htmlids; 
        //        alert(W.document.getElementById('ids').value);
        //        })
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
                if ($(this).attr("checked") == true) {
                    checkedval = $(this).val();
                }
            })

            var linkText = "";
            //            var rdoSelectVal = document.getElementsByName("radioselect");
            if (checkedval == 1) {
                linkText = $("#txtLinkAddress").val();
                if (linkText == "") {
                    alert("请输入链接地址！");
                    return;
                }
            }
            if (checkedval == 2) {
                linkText = CKEDITOR.instances.htmlcode.getData();
                if (linkText == "") {
                    alert("请输入网页编辑！");
                    return;
                }
            }

            $.ajax({
                type: "Post",
                url: "../ashx/NoticeRange.ashx?a_" + Math.random(),
                data: { "Flag": "SendNotice", "title": title, "content": content, "range": range, "onlion": onlion, "linkText": linkText, "checkedval": checkedval },
                datatype: "text/json",
                success: function (backdata) {
                    if (backdata == "1") {
                        //alert("公告发送成功！");
                        //window.location = "../SystemMsg/AddSysNotice.aspx";
                        window.location = "../SystemMsg/MsgManagerNew.aspx?curp=notice";
                    }
                    else if (backdata == 0) {
                        alert("已存在该标题的公告");
                        // alert("公告发送失败！");
                    }
                    else {
                        alert("公告发送失败！");
                    }
                }
            });

        }




    </script>
    <link href="../css/NoticeAdd.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        
        .txtBox
        {
            width: 502px;
            vertical-align: middle;
            border-color: #9A9A9A;
            border: 1px solid #999;
            padding-left: 10px;
            padding-top: 4px;
            padding-bottom: 5px;
            font-size: 15px;
        }
        .content .pagePath
        {
            background-color: #ECF5FA;
            border-bottom: 1px solid #7CCF8F;
            font-family: "微软雅黑";
            font-size: 14px;
            line-height: 30px;
            text-indent: 15px;
        }
        .th_title
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="content">
        <form runat="server" id="queryfrom" name="queryfrom">
        <input type="hidden" name="orderKey" id="orderKey" value="ID" />
        <div class="pagePath">
            首页 >公告管理>发布公告
        </div>
        <div class="searchform">
            <table cellpadding='5' cellspacing='5' id="table_uu" style="margin-left: 50px; margin-top: 20px;">
                <tr>
                    <td class="th_title">
                        <span style="color: #1e5494;">公告范围</span>
                    </td>
                    <td class="style1">
                        <div id="txtUser" style="width: 502px; height: 25px; vertical-align: middle; border: 1px solid #999;
                            float: left;" onclick="ShowUser()">
                        </div>
                        <input id="ids" type="text" style="display: none;" />
                    </td>
                </tr>
                <tr>
                    <td class="th_title">
                        标题：
                    </td>
                    <td class="style1">
                        <%--<asp:TextBox ID="txtTitle" runat="server" value="" onfocus="" Width="500px" Height="25px"></asp:TextBox>--%>
                        <input type="text" id="txtTitle" value="" style="width: 500px; height: 25px;" />
                        &nbsp;<span style="color: Red;">*</span><span style="color: Green;">最大长度为50个字符</span>
                    </td>
                </tr>
                <tr>
                    <td class="th_title">
                        内容：
                    </td>
                    <td class="style1">
                        <div style="float: left; width: 500px;">
                            <%--<asp:TextBox ID="txtConnent" runat="server" value="" TextMode="MultiLine" 
                                Height="200px" Width="500px"></asp:TextBox>--%>
                            <textarea id="txtConnent" style="width: 502px; height: 250px"></textarea>
                        </div>
                        <div style="float: left">
                            &nbsp; <span style="color: Red; margin-left: 1px">*</span><span style="color: Green;
                                margin-left: 1px">最大长度为1024个字符</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="style1">
                        <input type="checkbox" id="checkedOnline" style="width: 10px; height: 10px;" />
                        <span style="margin-top: -5px;">只发给在线用户</span>
                    </td>
                </tr>
                <tr>
                    <td class="th_title">
                        网页链接：
                    </td>
                    <td class="style1">
                        <input id="Radio1" type="radio" class="redio" value="0" name="radioselect" onclick="Show(this.value)" />无网页链接
                        <input id="Radio2" type="radio" value="1" class="redio" name="radioselect" onclick="Show(this.value)" />外部网页链接
                        <input id="Radio3" type="radio" value="2" class="redio" name="radioselect" onclick="Show(this.value)" />自定义网页内容
                    </td>
                </tr>
                <tr id="Link1" style="display: none;">
                    <td>
                        链接地址：
                    </td>
                    <td class="style1">
                        <%--<asp:TextBox  ID="txtLinkAddress" runat="server" value="" onfocus="" Width="500px" Height="25px"></asp:TextBox>--%>
                        <input id="txtLinkAddress" style="width: 500px; height: 25px;" />
                    </td>
                </tr>
                <tr id="Link2" style="display: none;">
                    <td class="th_title">
                        网页编辑：
                    </td>
                    <td class="style1">
                        <textarea class="ckeditor" id="htmlcode" name="htmlcode" rows="10" cols="80" style="width: 500px;
                            height: 400px;"></textarea>
                    </td>
                </tr>
                <tr style="height: 10px;">
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="style2">
                        <%--<asp:Button class="td_send" ID="Button1" runat="server" Text="发送" OnClick="btnSave_Click" />--%>
                        <%--<a id="A1" runat="server" onclick="closeWindow()">
                            <asp:Button ID="Button4" runat="server" Text="取消" class="td_send" /></a>
                              <asp:Button class="td_send" ID="btnSendMessage" runat="server" Text="发送即时通信消息" 
                            onclick="btnSendMessage_Click" />--%>
                        <input type="button" value="发  送" onclick="SendNotice() " style="color: White; background-color: #00AEFF;
                            height: 26px; width: 64px; margin-left: 50px; vertical-align: middle;" />
                        <%--<input type="button" value="取消" style="margin-bottom: 0px" />
                            <input type="button" value="发送即时通信消息" />--%>
                    </td>
                </tr>
            </table>
            <div style="height: 10px;">
            </div>
        </div>
        </form>
        <div id="dv_yonghu" style="display:none;">
            <div id="dv_change"> 
                <dl> 
                    <dd onclick="changedivleft('group')"> 部门</dd>
                    <dd onclick="changedivleft('user')">人员</dd>
                </dl>
            <div id="dv_left_group"></div>
            <div id="dv_left_user">></div>
          </div> 
            <div id="div_right" >
                 <table cellspacing="0";cellpadding="2"  id="gusertab" >
                        <tr>
                            <th width="60px"> 用户名</th>
                            <th width="40px" class="permissionTab">操作</th>
                            <th width="40px" class="permissionTab"></th>
                         </tr>
                 </table>
          </div>
           <div style="clear:both;"></div>
           <div  style=" float:left; width:500px"> <input type="button" onclick="userAlltoEmail()" value="确定" />
                   <a href="#" onclick="window.location ='/DiskN/CheckUser?username=admin'"><label> 关闭</label></a>
          </div>
        </div>
    </div>
</asp:Content>
