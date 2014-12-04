<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ZK_ml.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="/CSS/index.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../css/common_push.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .box2
        {
            width: 50px;
            height: 15px;
            line-height: 15px;
            float: left;
            margin-left: 35px;
            margin-bottom: 26px;
        }
        .btn3
        {
            display: block;
            line-height: 25px;
            text-align: center;
            float: left;
            color: #fff;
            font-weight: bold;
            margin-left: 100px;
            margin-top: 30px;
        }
    </style>
    <script language="javascript">
        var file_id = "0";
        $(function () {

            //获取url里的参数
            var url = location.search; //获取url中"?"符后的字串
            if (url.indexOf("?") != -1) {
                file_id = url.substr(9);
            }
            $(".nav li").mouseenter(function () {
                $(this).prev().find("span").css("display", "none");
                $(this).find("span").css("display", "none");
                $(this).find("a").css("padding-left", "43px");
                $(this).find("a").css("padding-right", "43px");
            });
            $(".cur a").mouseleave(function () {
                $(this).css("padding-left", "43px");
            })
            $(".nav li").mouseleave(function () {
                $(this).prev().find("span").css("display", "block");
                $(this).find("span").css("display", "block");
                $(this).find("a").css("padding-left", "35px");
                $(".cur").find("a").css("padding-left", "43px");
                $(this).find("a").css("padding-right", "35px");

            })
        })

        //添加新关键字
        function AddNewKeyWord(kd) {

            if ($("#txt_KeyWord").val() == "") {
                $("#txt_KeyWord").val($("#txt_KeyWord").val() + kd + " ");
            }
            else {
                $("#txt_KeyWord").val($("#txt_KeyWord").val() + " " + kd);
            }
        }
        //推送资源
        function Push_OtherFile() {

            var channelgroupid = $("#Select_MoralCG").val();

            //关键字
            var keyword = $("#txt_KeyWord").val();
            $.ajax({
                type: "Post",
                url: "/Push/Push_OtherFile",
                data: { "channelgroupid": channelgroupid, "keyword": keyword, "file_id": file_id },
                datatype: "text",
                success: function (backdata) {
                    if (backdata == "成功") {
                        alert("推送成功");
                        window.location.href = "/Home/Index";
                    }
                    else {
                        alert("推送失败");
                    }
                }
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    --资源推送频道
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_wrap" style="width: 1000px; margin-right: auto; margin-left: auto;
        overflow: hidden; border-width: 1px; border-color: Black; border-style: solid;
        margin-top: 30px;">
        <div class="title">
            <h2>
                资源推送>德育资源推送</h2>
        </div>
        <div align="center" class="green2" style="width: 800px; margin-right: auto; margin-left: auto;
            overflow: hidden; border-width: 2px; border-color: Green; border-style: solid;
            margin-top: 50px; margin-bottom: 50px;">
            <table width="85%" align="center" style="padding-bottom: 10px; padding-top: 5px;
                margin-top: 20px;">
                <tr>
                    <td>
                        <span class="green2">O 选择德育类别</span>
                    </td>
                    <td width="40px">
                        <span>|</span>
                    </td>
                    <td>
                        <select id="Select_MoralCG" style="width: 500px;">
                            <%System.Data.DataTable MoralCGlist = (System.Data.DataTable)ViewData["MoralCGlist"];
                              foreach (System.Data.DataRow item in MoralCGlist.Rows)
                              {
                                  if (item["channelGroupName"].ToString() != "德育分类")
                                  {
                            %>
                            <option value="<%=item["channelGroupID"] %>">
                                <%=item["channelGroupName"]%></option>
                            <% }
                              }%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>O 关键字</span>
                    </td>
                    <td>
                        <span>|</span>
                    </td>
                    <td>
                        <textarea style="width: 500px; height: 100px;" id="txt_KeyWord"></textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr style="padding-top: 10px;">
                    <td>
                        <span>O 常用关键字</span>
                    </td>
                    <td>
                        <span>|</span>
                    </td>
                    <td rowspan="3">
                        <%System.Data.DataTable listtags = (System.Data.DataTable)ViewData["listtags"];
                          foreach (System.Data.DataRow item in listtags.Rows)
                          {%>
                        <div class="box2" style="width: auto; height: 30px; margin-left: 5px;">
                            <a href="javascript:void(0);return false;" class="green2" onclick="AddNewKeyWord('<%=item["tagName"] %>')">
                                <%=item["tagName"]%></a></div>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td style="color: Red;">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div style="clear: both">
            </div>
            <a href="#" onclick="Push_OtherFile()" class="btn3 come" style="margin-left: 220px;
                margin-bottom: 20px;">
                <img src="../../images/tijiao.png" /></a> <a href="/Push/Index" class="btn3" style="margin-bottom: 20px;">
                    <img src="../../images/chongzhi.png" /></a>
        </div>
        </div>
    </div>
</asp:Content>
