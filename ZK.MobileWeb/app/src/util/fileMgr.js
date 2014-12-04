/**
 * 文件上传，文件下载
 */
var hl = (hl || {});
hl.fileMgr = {
    opId : 111,
    inOpCode : 100,
    /**
     * 图片选取，返回路径存到hl.data.disk.imgBrowserPath
     */
    openImgBrowser : function(p) {
        uexImageBrowser.cbPick = function(opCode, dataType, data) {
            console.log('attachmentPath: ' + data);
            hl.data.disk.imgBrowserPath = data;

            hl.fileMgr.upLoad();
        }
        uexImageBrowser.pick();
    },
    /**
     * {
     *
     * }
     */
    upLoad : function(p) {
        var user = hl.data.user.get();
        var url = "http://192.168.1.21:8080/upload.ashx?from=web?"
        if(user != null) {
            url += "&userID=" + user.id;
        }
        var id = hl.data.disk.backList.last();
        if(id == -1) {
            url += "&pid=0";
        } else {
            url += "&pid=" + id.fid;
        }

        uexUploaderMgr.cbCreateUploader = hl.fileMgr.cbCreateUploader;
        uexUploaderMgr.onStatus = hl.fileMgr.onStatus;
       
        uexUploaderMgr.createUploader(111, url);
    },
    //创建上传对象回调
    cbCreateUploader : function(opId, dataType, data) {
        uexLog.sendLog("cbCreateUploader" + "," + opId + "," + dataType + "," + data);
        //alert("cbCreateUploader" + "," + opId + "," + dataType + "," + data) ;
        if(dataType == 2 && data == 0) {
            //上传文件到服务器
            hl.tpl.info.open();

            uexUploaderMgr.uploadFile(opId, hl.data.disk.imgBrowserPath, 'fileName', '1');
        }
    },
    //上传文件回调函数
    onStatus : function(opId, fileSize, percent, serverPath, status) {
        uexLog.sendLog("onStatus" + "," + opId + "," + fileSize + "," + percent + "," + serverPath + "," + status);
        //	alert("onStatus" + "," + opId + "," + fileSize + "," + percent + "," + serverPath + "," + status) ;
        switch (status) {
            case 0:
                hl.tpl.info.refresh("上传进度：" + percent + "%");
                // document.getElementById('percent').value = percent ;
                // alert("上传进度：" + percent);
                break;
            case 1:
                hl.tpl.info.refresh("上传成功!");
                hl.tpl.info.close();
                //  alert("上传成功，服务器路径为" + serverPath);
                uexUploaderMgr.closeUploader(opId);
                break;
            case 2:
                hl.tpl.info.refresh("上传失败!");
                hl.tpl.info.close();
                //    alert("上传失败!");
                uexUploaderMgr.closeUploader(opId);
                break;
            default:
                break;
        }
    },
    downLoad : function(p) {

        uexDownloaderMgr.createDownloader(inOpCode);
        /**
         * 下载状态监听方法
         * @param {Object} opCode
         * @param {Object} fileSize
         * @param {Object} percent
         * @param {Object} status
         */
        uexDownloaderMgr.onStatus = function(opCode, fileSize, percent, status) {
            switch (status) {
                case 0:
                    //下载过程中
                    //   $$('percentage').innerHTML = '文件大小：' + fileSize + '字节<br>下载进度：' + percent;
                    break;
                case 1:
                    //下载完成
                    console.log('下载完成');
                    uexDownloaderMgr.closeDownloader(opCode);
                    //下载完成要关闭下载对象
                    break;
                case 2:
                    //下载失败
                    console.log('下载失败');
                    uexDownloaderMgr.closeDownloader(opCode);
                    //下载失败要关闭下载对象
                    break;
            }
        }
        var cText = 0;
        var cJson = 1;
        var cInt = 2;
        /**
         * 创建下载对象的回调方法
         * @param {Object} opCode
         * @param {Object} dataType
         * @param {Object} data 0为成功；1为失败
         */
        uexDownloaderMgr.cbCreateDownloader = function(opCode, dataType, data) {
            if(dataType == 2 && data == 0) {
                console.log('创建成功');
                hl.fileMgr.startDownload();
            } else {
                console.log('创建失败');
            }
        }
        uexWidgetOne.cbError = function(opCode, errorCode, errorInfo) {
            console.log(errorInfo);
        }
        /**
         * 通过下载url获取下载对象的信息的回调方法
         * @param {Object} opCode
         * @param {Object} dataType
         * @param {Object} data
         */
        uexDownloaderMgr.cbGetInfo = function(opCode, dataType, data) {
            if(dataType == 1) {
                if(!isDefine(data)) {
                    console.log('无数据');
                    return;
                }
                console.log(data);
                var info = eval('(' + data + ')');
                $$('fileInfo').innerHTML = '文件路径：' + info.savePath + '<br>文件大小：' + info.fileSize + '<br>已下载：' + info.currentSize + '<br>下载时间：' + info.lastTime;
            }
        }
    },
    /**
     * 执行下载
     * download(String inOpCode, String inDLUrl, String inSavePath,String inMode)
     * @param {string} inOpCode 操作id
     * @param {string} inDLUrl 下载地址
     * @param {string} inSavePath 保存的地址
     * @param {string} inMode 下载模式 0不支持断点下载； 1支持断点下载
     */
    startDownload : function(p) {
        uexDownloaderMgr.download(inOpCode, $$('downloadPath').value, $$('savedPath').value, '1');
    },
    /**
     * 通过操作ID关闭下载对象
     */
    closeDownload : function(p) {
        uexDownloaderMgr.closeDownloader(inOpCode);
    },
    /**
     * 通过路径获取下载的文件信息
     */
    getInfo : function(p) {
        uexDownloaderMgr.getInfo($$('downloadPath').value);
    },
    /**
     * 通过路径清除未完成下载的任务
     * clearTask(String inDLUrl,String inClearMode)
     * @param {string} inDLUrl
     * @param {string} inClearMode 清除模式。0代表只清除此次下载任务，并不清除已经下载的目标临时文件。
     * 1代表清除此次下载任务，并且清除已经下载的目标临时文件。
     * 当目标文件已经成功下载到本地后，此操作不能不能清除此目标文件。默认为0。
     */
    clearInfo : function(p) {
        uexDownloaderMgr.clearTask($$('downloadPath').value);
    }
}