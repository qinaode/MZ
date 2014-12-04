<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
    <link href="/Scripts/easyui/themes/default/easyuidb.css" rel="stylesheet" type="text/css" />   
     <link href="/css/DiskN/meigong.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        /*--弹出层样式--*/
        .bg
        {
            position: absolute;
            width: 100%;
            height: 100%;
            background-color: #999;
            z-index: 6;
            left: 0;
            top: 0;
            -moz-opacity: 0.5;
              filter: alpha(opacity=50);
            opacity: 0.5;
        }
        .moren
        {
            z-index: 50;
        }
    </style>
    <script src="/js/jquery1.42.min.js" type="text/javascript"></script>
    <script src="/js/DiskNJS/SharePage.js" type="text/javascript"></script>
</head>
<body onload="MM_preloadImages('/css/DiskN/images/icon4.png')">
    <div class="gxk">
        <input id="txtuserid" type="text" style="display: none;" value="<%=ViewData["username"].ToString() %>" fileid="<%=ViewData["fileid"].ToString() %>" />
       <%-- <input id="txtfiletype" type="text"  style="display: none;" value="<%=ViewData["file_type"].ToString() %>" />--%>
        <div class="left">
            <div class="title1">
                <img alt="icon" src="/css/DiskN/images/icon.png" />&nbsp;选择组织架构/成员账号
            </div>
            <div class="fenzu">
               <a href="javascript:void(0)" onclick="changedivleft('group')"> <div class="bumen gorucheck" id="bumen_selected" >
                         <div class="image_yuan">  <img alt="group" src="/css/DiskN/images/group.png" />
                       </div><div class="txt_yuan"> 部门</div>
                </div></a>
                <a href="javascript:void(0)"  onclick="changedivleft('user')"><div class="member" id="member_selected">            
                         <div class="image_yuan"> <img alt="group" src="/css/DiskN/images/group.png" />
                        </div> <div class="txt_yuan">成员</div>
                </div></a>
                <div class="clear">
                </div>
                <div id="dv_left_group" style="overflow: auto; display: block; width: 205px; height: 333px;">
                   <%-- <ul id="tt" class="easyui-tree" data-options="url:'tree_data1.json',method:'get',animate:true,checkbox:true">
                    </ul>--%>
                </div>
                <div id="dv_left_u" style="display: none;">
                    <div id='dv_change' style='overflow: auto; width: 205px;'>
                        <div class="sousuo">
                            <input id='txt_serch' onfocus='serchfocus()' onblur='serchblur()' onkeyup='userserch()'
                                type="text" name="name" class="username" title="" value=" 请输入群成员" style="border: 1px solid #cecece;
                                color: Gray;" />
                        </div>
                    </div>
                    <div id="dv_left_user" style="display: block; overflow: auto; width: 210px; height: 300px;">
                    </div>
                </div>
            </div>
        </div>
        <div class="right" >
            <div class="title2" id="dv_right" >
                <a href="javascript:void(0)" onclick="defaulthide()" id="a_configde">
                    <img alt="icon2" src="/css/DiskN/images/icon2.png" border="0" />&nbsp;设置默认权限</a>
            </div>
            <div class="quanxian" style="overflow: auto;">
                <table id="gusertab" width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr id="tr_head1" class="line">
                        <td class="tab" width="114" height="30" align="center" bgcolor="#c9c9c9">
                        </td>
                        <td class="tab" width="40" align="center" bgcolor="#c9c9c9">
                            默认
                        </td>
                        <td class="tab" width="120" colspan="3" align="center" bgcolor="#c9c9c9">
                            文件夹
                        </td>
                        <td class="tab" width="160" colspan="4" align="center" bgcolor="#c9c9c9">
                            文件
                        </td>
                        <td width="36" align="center" bgcolor="#c9c9c9">
                            权限
                        </td>
                        <td width="36" align="center" bgcolor="#c9c9c9">
                            &nbsp;
                        </td>
                    </tr>
                      <tr id="tr_head2" class="tb1">
                        <td class="tab" width="114" height="26" align="center">
                            用户名
                        </td>
                        <td class="tab" align="center">
                            读取
                        </td>
                        <td  align="center">
                            创建
                        </td>
                        <td  align="center">
                        更名
                        </td>
                        <td class="tab" align="center">
                        删除
                        </td>
                          <td  align="center">
                        创建
                        </td>
                         <td  align="center">
                        修改
                        </td>
                         <td  align="center">
                        更名
                        </td>
                         <td class="tab" align="center">
                        删除
                        </td>
                        <td align="center">
                            权限
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr bgcolor="#999999">
                        <td height="3" colspan="11" align="center" bgcolor="#888888">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clear">
            </div>
            <div class="footer">
                <div class="queding">
                    <a href="javascript:void(0)" onclick="shareing( '<%=ViewData["fileid"].ToString() %>')"
                       >立即共享</a>
                </div>
                <div class="quxiao">
                    <a href="javascript:void(0)" onclick=" closewindow('false')">取消</a>
                </div>
            </div>
        </div>
        <!-- 弹出层 -->
        <div class="bg" id="div_BackGround" style="display: none">
        </div>
        <div class="moren" id="jqi" >
            <div style="height:20px;"></div>
            <div class="biaoti">
                设置用户权限
            </div>
            <div class="line01">
                <img alt="line" src="/css/DiskN/images/line.png" />
            </div>
            <div class="tablemr">
                <table width="100%" border="0" cellspacing="0">
                    <tr bgcolor="#a3c5f3" class="co">
                        <td height="28" align="center" class="bk">
                            默认
                        </td>
                        <td colspan="3" align="center" class="bk">
                            文件夹
                        </td>
                        <td colspan="4" align="center" class="bk">
                            文件
                        </td>
                        <td align="center" class="bk">
                            权限
                        </td>
                    </tr>
                    <tr>
                        <td class="bk1" height="28" align="center">
                            读取
                        </td>
                        <td class="bk1" colspan="3" align="center">
                            创建&nbsp;&nbsp;更名&nbsp;&nbsp;删除
                        </td>
                        <td class="bk1" colspan="4" align="center">
                            创建&nbsp;&nbsp;修改&nbsp;&nbsp;更名&nbsp;&nbsp;删除
                        </td>
                        <td align="center">
                            权限
                        </td>
                    </tr>
                    <tr id="tr_Defaul">
                        <td class="bk1" align="center">
                            <input type="checkbox" checked="checked" name="resource_read" id="checkbox" />
                            <label for="checkbox">
                            </label>
                        </td>
                        <td align="center">
                            <input type="checkbox" name="folder_create" id="checkbox2" />
                            <label for="checkbox2">
                            </label>
                        </td>
                        <td align="center">
                            <input type="checkbox" name="folder_rename" id="checkbox3" />
                        </td>
                        <td class="bk1" align="center">
                            <input type="checkbox" name="folder_delete" id="checkbox4" />
                        </td>
                        <td align="center">
                            <input type="checkbox" name="file_create" id="checkbox5" />
                        </td>
                        <td align="center">
                            <input type="checkbox" name="file_modify" id="checkbox6" />
                        </td>
                        <td align="center">
                            <input type="checkbox" name="file_rename" id="checkbox7" />
                        </td>
                        <td class="bk1" align="center">
                            <input type="checkbox" name="file_delete" id="checkbox8" />
                        </td>
                        <td align="center">
                            <input type="checkbox" name="permission_grant" id="checkbox9" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="footer">
                <div class="quedingt">
                    <a href="#" onclick="defaultconfig()">确定</a>
                </div>
                <div class="quxiaot">
                    <a href="#" onclick="defaulthide()">取消</a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
