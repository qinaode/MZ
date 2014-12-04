/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //config.toolbar = "Basic";
    //config.toolbar = "full";
//    config.toolbar =
    [
    ['Source', '-','Save','NewPage', 'Preview'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
    ['Undo', 'Redo', '-', 'SelectAll', 'RemoveFormat'],
    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'ShowBlocks'], 
    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'],
    ['Image', 'Flash', 'Table', 'HorizontalRule', 'SpecialChar'],
    ['Font', 'FontSize'], ['TextColor', 'BGColor'], ['Maximize']
    ]; 

    config.filebrowserBrowseUrl = '../ckfinder/ckfinder.html'; //不要写成"~/ckfinder/..."或者"/ckfinder/..."
    config.filebrowserImageBrowseUrl = '../ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '../ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

//    config.filebrowserWindowWidth = '600';  //“浏览服务器”弹出框的size设置
//    config.filebrowserWindowHeight = '400';

}
CKFinder.SetupCKEditor(null, '../ckfinder/');//注意ckfinder的路径对应实际放置的位置