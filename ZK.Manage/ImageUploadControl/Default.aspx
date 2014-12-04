<%@ Page Title="" Language="C#" MasterPageFile="Default.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Literal runat="server" ID="ltrHTML"></asp:Literal>
    <script type="text/javascript">
        $().ready(function () {
            var counter = 0;
            $(function () {
                var btnUpload = $('#addImage');
                new AjaxUpload(btnUpload, {
                    action: 'saveupload.aspx',
                    name: 'uploadfile',
                    onSubmit: function (file, ext) {
                        $("#loading").show();
                    },
                    onComplete: function (file, response) {
                        var uploadedfile = "UserData/" + file;
                        $("#uploadImageWrapper").append("<div class='imageContainer offset'  id='current" + counter + "'><img height='65px' width='65px' src='" + uploadedfile + "' alt='" + uploadedfile + "'/><div id='close" + counter + "' class='close'  title='" + uploadedfile + "' onclick='RemoveImage(this);'><a ></a></div></div>");
                        $('#current' + counter).fadeIn('slow', function () {
                            $("#loading").hide();
                            $("#message").show();
                            $("#message").html("Added successfully!");
                            $("#message").fadeOut(3000);
                            counter++;
                        });
                    }
                });
            });
        });

        function RemoveImage(_this) {
            var counter = _this.id.replace('close', '');
            $("#loading").show();
            $.ajax({
                type: "POST",
                url: "removeupload.aspx",
                data: "filename=" + _this.getAttribute('title'),
                success: function (msg) {
                    $('#current' + counter).fadeOut('slow', function () {
                        $("#loading").hide();
                        $("#message").show();
                        $("#message").html("Removed successfully!");
                        $("#message").fadeOut(3000);
                    });
                }
            });
        }
    </script>
    <table id="imageUploader" cellpadding="0" cellspacing="0">
        <tr class="header">
            <td style="padding-left: 5px;">
                <a id="addImage" href="javascript:;">Add Image</a>
            </td>
        </tr>
        <tr class="body">
            <td valign="top">
                <div id="uploadImageWrapper">
                </div>
            </td>
        </tr>
        <tr class="footer">
            <td>
                <div id="loading" style="display: none">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <img width="20" height="20" src="Images/Loading.gif" alt="Loading..." />
                            </td>
                            <td>
                                Please wait...
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="message" style="color: Green">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
