using System;
using System.Reflection;
using MSWord = Microsoft.Office.Interop.Word;
using MSExcel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Interop.Word;
namespace ZK.Common
{
    public class DocumentHelper
    {
        /// <summary>
        /// 截取word的第一张图片
        /// </summary>
        /// <param name="filepath">文件路径 带后缀名</param>
        /// <param name="savepath">图片路径 带后缀名</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>success 或 其他</returns>
        public static string WordToImage(string filepath, string savepath, int width, int height)
        {
            savepath = "d:\\mn.pptx";
            object Nothing = Missing.Value;
            object wordpath = filepath;
            MSWord.Application wordapp = new MSWord.Application();
            MSWord.Document worddoc = wordapp.Documents.Open(ref wordpath, ref  Nothing, ref Nothing, ref Nothing);
            MSWord.Range ra = worddoc.Range(ref Nothing, ref Nothing);

            object obj = null;
            // obj=worddoc.Content.Paragraphs[15].Range.get_Style();
            // obj = ra.Paragraphs.get_Style();
            object savepath2 = (object)savepath;
            obj = worddoc.Characters;
            object format = MSWord.WdSaveFormat.wdFormatDocument;
            try
            {
                worddoc.SaveAs(ref savepath2, ref Nothing, ref Nothing, ref Nothing);
                //worddoc.SaveAs(ref savepath2, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                worddoc.Close(ref Nothing, ref Nothing, ref Nothing);
                object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                wordapp.Quit(ref saveOption, ref Nothing, ref Nothing);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }



        }

        /// <summary>
        /// 截取Excel的第一张图片
        /// </summary>
        /// <param name="filepath">文件路径 带后缀名</param>
        /// <param name="savepath">图片路径 带后缀名</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>success 或 其他</returns>
        public static string ExcelToImage(string filepath, string savepath, int width, int height)
        {
            //处理xlsx文件
            #region 处理xlsx文件

            object Nothing = Missing.Value;
            MSExcel.Application excelapp = new MSExcel.Application();
            MSExcel.Workbook wbk = excelapp.Workbooks.Open(filepath, Nothing, Nothing, Nothing);
            wbk.SaveAs(savepath.TrimEnd('x'), Nothing, Nothing, Nothing);
            wbk.Close();
            excelapp.Quit();
            return "";
            #endregion
        }

        /// <summary>
        /// 截取ppt的第一张图片
        /// </summary>
        /// <param name="filepath">文件路径 带后缀名</param>
        /// <param name="savepath">图片路径 带后缀名</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>success 或 其他</returns>
        public static string PPTToImage(string filepath, string savepath, int width, int height)
        {
            //savepath = System.IO.Path.GetFileNameWithoutExtension(filepath) + ".jpg";

            try
            {
                Microsoft.Office.Interop.PowerPoint.Application pptapplication = null;
                pptapplication = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentation ppt1 = pptapplication.Presentations.Open(filepath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                ppt1.Slides[1].Export(savepath, "jpg", width, height);
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

        /// <summary>
        /// 截取PDF的第一张图片
        /// </summary>
        /// <param name="filepath">文件路径 带后缀名</param>
        /// <param name="savepath">图片路径 带后缀名</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>success 或 其他</returns>
        public static string PDFToImage(string filepath, string savepath, int width, int height)
        {
            #region 导入pdf文件

            // FileInfo f1 = new FileInfo(filename);
            // FileInfo f2 = new FileInfo("d:\\text.doc");
            // PDDocument doc = PDDocument.load(filename);
            // PDFTextStripper pdfStripper = new PDFTextStripper();
            // int a = doc.getNumberOfPages();
            // // a=pdfStripper.getStartPage();
            // PDDocumentInformation pddinfo = doc.getDocumentInformation();
            // string strtitle = pddinfo.getTitle();
            // PDDocumentCatalog pdfcatalog = doc.getDocumentCatalog();
            // //PDDocumentOutline pddoutline=pdfcatalog.getDocumentOutline();
            // //PdfContents pdfc=
            // string str = pdfStripper.getText(doc);

            //// MessageBox.Show(strtitle);
            // return;

            #endregion

            return "";
        }

    }
}

