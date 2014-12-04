<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeRange.aspx.cs" Inherits="ZK.Manage.SystemMsg.NoticeRange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择公告范围</title>
  <%--  <link href="../js/Noticejs/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../js/Noticejs/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../js/Noticejs/easyui/demo/demo.css" rel="stylesheet" type="text/css" />--%>
    <link href="../js/Noticejs/easyui/themes/default/easyuidb.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            overflow: hidden;
        }
        *
        {
            border: 0;
        }
        
        .style1
        {
            height: 22px;
        }
        .style2
        {
            width: 54px;
        }
        # husertab
        {
            border: 1px solid Gray;
            border-top-width: 0px;
        }
        #tr_head1
        {
            background-color: #CCCCCC;
        }
        a
        {
            text-decoration: none;
        }
        #gusertab td
        {
            text-align: center;
            vertical-align: middle;
        }
        #jqi td
        {
            text-align: center;
            vertical-align: middle;
        }
        #tr_head2 td
        {
            border-bottom: 2px solid #585858;
            border-left: 1px solid #BDBDBD;
            border-right: 1px solid #BDBDBD;
        }
        #tr_head1 td
        {
            border-bottom: 1px solid #BDBDBD;
            border-left: 1px solid #BDBDBD;
            border-right: 1px solid #BDBDBD;
        }
        .ltrborder td
        {
            border-top: 1px solid #BDBDBD;
            border-left: 1px solid #BDBDBD;
            border-right: 1px solid #BDBDBD;
        }
        .lrbborder td
        {
            border-bottom: 1px solid #BDBDBD;
            border-left: 1px solid #BDBDBD;
            border-right: 1px solid #BDBDBD;
        }
        .lrborder td
        {
            border-left: 1px solid #BDBDBD;
            border-right: 1px solid #BDBDBD;
        }
        .style3
        {
            width: 40px;
        }
        .clear
        {
            clear: both;
        }
        .gonggao
        {
            padding: 0px;
            margin: 0px;
            height: 516px;
            width: 640px;
        }
        .title
        {
            width: 650px;
            background: #7b7b7b;
            margin: 0px;
            padding: 10px 10px 10px 20px;
            color: #fff;
            font-size: 14px;
            font-weight: bold;
        }
        .shut
        {
            float: right;
        }
        .content
        {
            margin: 10px 0px 10px 30px;
        }
        .left
        {
            float: left;
        }
        .tit2
        {
            width: 251px;
            height: 373px;
            border: 1px solid #cfcfcf;
            background: url(/../images/NoticeImage/title_bg.jpg) no-repeat;
        }
        .titu
        {
            overflow: auto;
            width: 260px;
            height: 373px;
            border: 1px solid #cfcfcf;
        }
        .bumen
        {
            float: left;
            height: 36px;
            width: 102px;
            margin-left: 15px;
            margin-top: 7px;
            padding-top: 12px;
            font-weight: bold;
            text-align: center;
            background: url(/../images/NoticeImage/title1.png) no-repeat;
        }
        .member
        {
            float: left;
            height: 36px;
            width: 102px;
            margin-left: 14px;
            margin-top: 7px;
            padding-top: 10px;
            font-weight: bold;
            text-align: center;
            color: #fff;
            background: url(/../images/NoticeImage/title2.png) no-repeat;
        }
        .center
        {
            float: left;
            margin: 200px 10px 0px 10px;
        }
        .right
        {
            float: left;
        }
        .rightcont
        {
            margin-top: 10px;
            width: 300px;
            height: 373px;
            border: 1px solid #cfcfcf;
        }
        .foote
        {
            height: 50px;
            background: #eff4f8;
        }
        #gusertab
        {
            width: 100%;
        }
        .dv_sp
        {
            height: 10px;
            width: 100%;
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
            background-image: url(/../images/NoticeImage/send.png);
        }
        .btncancel
        {
            border: 0px;
            padding-top: 2px;
            text-align: center;
            background-image: url(../images/NoticeImage/cancel.png);
        }
        .new_title_box
        {
            float: left;
            border-bottom: 1px solid #e4e4e4;
            overflow: hidden;
            padding: 15px 30px;
            width: 91%;
            text-align: right;
        }
        #txtrenyuan, #txtbumen
        {
            width: 32px;
            height: 19px;
        }
    </style>
    <script src="../js/jquery1.42.min.js" type="text/javascript"></script>
    <script src="../js/Noticejs/NoticeRange.js" type="text/javascript"></script>
    <script type="text/javascript">
        function closeWindow() {
            var api = frameElement.api,
            W = api.opener; // api.opener 为载加lhgdialog.min.js文件的页面的window对象
            api.reload();
            api.close();
        }
        function btnOK_click() {
            var obj = userAlltoEmail();
            var htmlids = sending();
            var api = frameElement.api; //调用父页面数据
            var W = api.opener; //获取父页面对象              
            W.document.getElementById('txtUser').innerHTML = obj;
            W.document.getElementById('ids').value = htmlids;
            api.close();
        }
        // var html = userAlltoEmail();
        //var htmlids = sending();
        //window.parent.document.getElementById("txtRange").innerHTML = html;
        // window.parent.document.getElementById("ids").value = htmlids;
        // closeWindow();

    </script>
</head>
<body style="font-family: 微软雅黑; font-size: 14px;">
    <div id="dv_yonghu" style="margin-top: 10px; width: 100%;">
        <div style="float: left; margin-bottom: 5px;">
            <img src="../images/NoticeImage/icon.png" alt="icon" />选择组织架构/成员账号
            <img src="../images/NoticeImage/icon2.png" style="margin-left: 150px;" alt="icon2" />已添加的组织架构/成员账号
        </div>
        <div class="left">
            <div class="tit2">
                <a href="#" onclick="changedivleft('group')" style="width: 50px; height: 19px;">
                    <div class="bumen">
                        <img src="../images/NoticeImage/group.png" style="vertical-align: middle;" /><span
                            id="txtbumen" style="vertical-align: middle;">&nbsp;部门</span>
                    </div>
                </a><a href="#" onclick="changedivleft('user')" style="width: 50px; height: 19px;">
                    <div class="member">
                        <img src="../images/NoticeImage/user.png" alt="user" style="vertical-align: middle;" /><span
                            id="txtrenyuan" style="color: White; padding-top: -5px; vertical-align: middle;">&nbsp;成员</span>
                    </div>
                </a>
                <div class="clear">
                </div>
                <div id="dv_left_group" style="overflow: auto; display: block; width: 251px; height: 300px;">
                    <ul id="tt" class="easyui-tree" data-options="url:'tree_data1.json',method:'get',animate:true,checkbox:true">
                    </ul>
                </div>
                <div id="dv_left_u" style="display: none; width: 251px; height: 300px;">
                    <div id='dv_change' style='overflow: auto; width: 250px; height: 30px;'>
                        <input style="color: Gray; margin-left: 16px; width: 200px; vertical-align: middle;
                            padding-top: 3px; padding-left: 10px; margin-bottom: 3px; border: 1px solid"
                            id='txt_serch' type='text' onfocus='serchfocus()' onblur='serchblur()' onkeyup='userserch()'
                            value='请输入成员...' />
                    </div>
                    <div id="dv_left_user" style="display: block; width: 250px; overflow: auto; height: 270px;">
                    </div>
                </div>
            </div>
        </div>
        <div class="divcenter" style="float: left; margin-left: 13px; vertical-align: middle;
            margin-right: 13px; padding-top: 200px; height: 375px; text-align: center;">
            <img alt="" src="../images/NoticeImage/icon5.png" />
        </div>
        <div class="right">
            <div class="titu" id="dv_right">
                <table cellspacing="0" id="gusertab" cellpadding="2">
                    <tr id="tr_head2" style="height: 30px; line-height: 3px; display: none;">
                        <td style="display: none; width: 30px; text-align: center; padding-bottom: 2px; vertical-align: bottom">
                        </td>
                        <td style="display: none; width: 200px;">
                        </td>
                        <td style="display: none;">
                        </td>
                    </tr>
                </table>
                <table width="100%">
                </table>
            </div>
        </div>
    </div>
    <div style="line-height: 15px; height: 15px;">
        &nbsp;</div>
    <div style="float: right; margin-top: 25px; margin-right: 20px;">
        <%-- <a href="#" onclick="btnOK_click()" style=" margin-left:60px;">
            <label style=" padding-left:10px; color:White;font-size: 15px;   width:60px; display:inline-block; background:url('../images/NoticeImage/send.png') no-repeat; height: 26px;">确定
                    </label> </a>--%>
        <input type="button" class="btn btn-login-active" id="Button1" value="确定" style="cursor: pointer;
            margin-left: 50px; width: 59px; height: 27px; font-size: 15px;" onclick="btnOK_click();" />
        <%-- <input  type="button" value="确定" onclick="btnOK_click()"  style="background:url('../images/NoticeImage/send.png') no-repeat; cursor: pointer; width: 60px; height: 26px; margin-right:20px; color:White;"/>--%>
        <a id="A2" onclick="closeWindow()">
            <input type="button" id="Button2" class="btncancel" style="cursor: pointer; width: 59px;
                height: 27px; font-size: 15px;" value="取消" /></a>
    </div>
</body>
</html>
