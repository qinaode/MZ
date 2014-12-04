using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Services;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;

namespace ZK.WebService
{
    /// <summary>
    /// ToHtml 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ToHtml : System.Web.Services.WebService
    {

        [WebMethod]
        public string FileToHtml(string filePath,string extname)
        {
            string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();
            fileExtension = extname;
            string strss = "|doc|docx|";
            string strss2 = "|xls|xlsx|";
            string strss3 = "|ppt|pptx|";
            if (strss.IndexOf("|" + fileExtension.Substring(1) + "|") > -1)
            {
                return wordToHtml(filePath);
            }
            else if (strss2.IndexOf("|" + fileExtension.Substring(1) + "|") > -1)
            {
                return ExcelToHtml(filePath);
            }
            else if (strss3.IndexOf("|" + fileExtension.Substring(1) + "|") > -1)
            {
                //return PPTToHtml(filePath);
                return ParsePPTToImages(filePath);
            }
            else
            { return ""; }

        }

        public string wordToHtml(string wordFileName)
        {
            try
            {
                //在此处放置用户代码以初始化页面
                Microsoft.Office.Interop.Word.ApplicationClass word = new Microsoft.Office.Interop.Word.ApplicationClass();
                Type wordType = word.GetType();

                Documents docs = word.Documents;

                //打开文件
                Type docsType = docs.GetType();
                Document doc = (Document)docsType.InvokeMember("Open",
                System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { wordFileName, true, true });

                //转换格式，另存为
                Type docType = doc.GetType();

                string wordSaveFileName = wordFileName.ToString();
                //string strSaveFileName = wordSaveFileName.Substring(0, wordSaveFileName.Length - 3) + "html";
                string strSaveFileName = this.getHtmlName(wordSaveFileName);
                object saveFileName = (object)strSaveFileName;

                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod,
                 null, doc, new object[] { saveFileName, WdSaveFormat.wdFormatFilteredHTML });

                docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod,
                 null, doc, null);

                //退出 Word
                wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod,
                 null, word, null);


            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "success";

        }

        private string ExcelToHtml(string xlsPath)
        {
            string htmlPath = this.getHtmlName(xlsPath);
            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = false;
                Object o = Missing.Value;

                /// _Workbook     xls=app.Workbooks.Open(xlsPath,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o);   

                _Workbook xls = app.Workbooks.Open(xlsPath, o, o, o, o, o, o, o, o, o, o, o, o);
                object fileName = htmlPath;
                object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml;//Html   

                // xls.SaveAs(ref     fileName,ref     format,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o,ref     o);   
                xls.SaveAs(fileName, format, o, o, o, o, XlSaveAsAccessMode.xlExclusive, o, o, o, o);
                //xls.SaveAs(
                object t = true;
                app.Quit();

                //Process[] myProcesses = Process.GetProcessesByName("EXCEL");
                //foreach (Process myProcess in myProcesses)
                //{
                //    myProcess.Kill();
                //}
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                return ex.ToString();
            }

            return "success";
        }

        private string PPTToHtml(string pptPath)
        {
            string saveFileName = this.getHtmlName(pptPath);

            try
            {

                Microsoft.Office.Interop.PowerPoint.Application ppt = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Core.MsoTriState m1 = new Microsoft.Office.Core.MsoTriState();
                Microsoft.Office.Core.MsoTriState m2 = new Microsoft.Office.Core.MsoTriState();
                Microsoft.Office.Core.MsoTriState m3 = new Microsoft.Office.Core.MsoTriState();
                Microsoft.Office.Interop.PowerPoint.Presentation pp = ppt.Presentations.Open(pptPath, m1, m2, m3);
                pp.SaveAs(saveFileName, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsHTML, Microsoft.Office.Core.MsoTriState.msoTriStateMixed);
                pp.Close();
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                return ex.ToString();
            }

            return "success";
        }

        #region PPT to Images

        private string ParsePPTToImages(string filepath)
        {

            string ext = System.IO.Path.GetExtension(filepath);
            string imagepath = filepath.Substring(0, filepath.Length - ext.Length)+"img";

            try
            {
                Microsoft.Office.Interop.PowerPoint.Application pptapplication = null;
                pptapplication = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentation ppt1 = pptapplication.Presentations.Open(filepath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                //ppt1.Slides[1].Export(imagepath, "jpg", 480, 320);
                ppt1.SaveCopyAs(imagepath, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsJPG, MsoTriState.msoFalse);
                //关闭
                ppt1.Close();
                pptapplication.Quit();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

        private string getHtmlName(string fileName)
        {
            return fileName + ".html";
            //string[] arrFileName = fileName.Split('.');
            //int nLen = arrFileName.GetLength(0);

            int nIdx = fileName.LastIndexOf('.');
            string strFileName = fileName.Substring(0, nIdx);

            return strFileName + ".html";
        }
    }
}
