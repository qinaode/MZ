<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择公告范围</title>
    <style type="text/css">
        *
        {
            margin: 0;
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
            background: url(/imagesN/sharepage/title_bg.jpg) no-repeat;
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
            background: url(/imagesN/sharepage/title1.png) no-repeat;
        }
        .member
        {
            float: left;
            height: 36px;
            width: 101px;
            margin-left: 15px;
            margin-top: 7px;
            padding-top: 12px;
            font-weight: bold;
            text-align: center;
            color: #fff;
            background: url(/imagesN/sharepage/title2.png) no-repeat;
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
            background-image: url(../../imagesN/sendNotice/send.png);
        }
        .btncancel
        {
            border: 0px;
            padding-top: 2px;
            text-align: center;
            background-image: url(../../imagesN/sendNotice/cancel.png);
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
    </style>
    <script src="../../js/jquery1.42.min.js" type="text/javascript"></script>
    <script src="../../js/DiskNJS/sendNotice.js" type="text/javascript"></script>
    <%--<link href="../../Scripts/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />    
    <link href="../../Scripts/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/easyui/demo/demo.css" rel="stylesheet" type="text/css" />--%>
     <link href="../../Scripts/easyui/themes/default/easyuidb.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript">
        function closeWindow() {
            window.parent.closeThisWindow();
        }
    </script>
</head>
<body style="font-family: 微软雅黑; font-size: 12px;">
    <div id="dv_yonghu" style="margin-top: 0px;">
        <div style="float: left; margin-bottom: 5px;">
            <img src="../../imagesN/sharepage/icon.png" alt="icon" />选择组织架构/成员账号
            <img src="../../imagesN/sharepage/icon2.png" style="margin-left: 154px;" alt="icon2" />已添加的组织架构/成员账号
        </div>
        <div class="left">
            <div class="tit2">
                <a href="#" onclick="changedivleft('group')" style="cursor:pointer;">
                    <div class="bumen">
                        <img src="../../imagesN/sharepage/group.png" style="vertical-align: middle; border: 0px;" /><span
                            id="txtbumen" style="vertical-align: middle;">&nbsp;部门</span>
                    </div>
                </a><a href="#" onclick="changedivleft('user')" style="cursor:pointer;">
                    <div class="member">
                        <img src="../../imagesN/sharepage/user.png" alt="user" style="vertical-align: middle;
                            border: 0px;" />&nbsp;<span id="txtrenyuan" style="color: White; padding-top: -5px;
                                vertical-align: middle;">成员</span>
                    </div>
                </a>
                <div class="clear">
                </div>
                <div id="dv_left_group" style="overflow: auto; display: block; width: 251px; height: 300px;">
                    <ul id="tt" class="easyui-tree" data-options="url:'tree_data1.json',method:'get',animate:true,checkbox:true">
                    </ul>
                </div>
                <div id="dv_left_u" style="display: none; width: 251px; height: 300px;">
                    <div id='dv_change' style='overflow: auto; width: 240px; height: 30px;'>
                        <input style="color: Gray; margin-left:14px; padding-left: 10px; width: 200px; border: 1px solid #999; padding-bottom: 3px;
                            padding-top: 3px; vertical-align:middle;" id='txt_serch' type='text' onfocus='serchfocus()' onblur='serchblur()'
                            onkeyup='userserch()' value='请输入成员...' />
                    </div>
                    <div id="dv_left_user" style="display: block; width: 250px; overflow: auto; height: 270px;">
                    </div>
                </div>
            </div>
        </div>
        <div class="divcenter" style="float: left; margin-left: 15px; margin-right:15px; height: 187px; width:31px; padding-top: 187px;">
            <img alt="" src="../../imagesN/sendNotice/icon5.png" />
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
    <div style="float: right; margin-top: 25px; margin-right: 20px;">
        <input type="button" class="btn btn-login-active" id="Button1" value="确定" style="cursor: pointer;
            margin-left: 60px; width: 60px; height: 26px; font-size: 15px;" onclick="btnOK();" />
        <a id="A2" onclick="closeWindow()">
            <input type="button" id="Button2" class="btncancel" style="cursor: pointer; width: 60px;
                height: 26px; font-size: 15px;" value="取消" /></a>
    </div>
</body>
</html>
