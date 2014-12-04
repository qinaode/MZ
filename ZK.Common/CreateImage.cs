using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using MSWord = Microsoft.Office.Interop.Word;
using MSExcel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.IO;
namespace ZK.Common
{
    public class CreateImage
    {

        //ppt 截图
        /// <summary>
        /// 截取ppt图片
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="imagepath">截取图片的路径</param>
        /// <returns>结果</returns>
        public bool CreatePPTImage(string filepath, string imagepath)
        {
            bool b = false;
            PowerPoint.Application pptapplication = null;
            pptapplication = new PowerPoint.Application();
            PowerPoint.Presentation ppt1 = pptapplication.Presentations.Open(filepath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
            ppt1.Slides[1].Export(imagepath, "jpg", 480, 320);
            //关闭
            ppt1.Close();
            pptapplication.Quit();
            b = true;
            return b;
        }
        public void CreatePDFImage(string fn_extend)
        {
            #region 导入pdf文件
            if (fn_extend == "pdf")
            {
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
            }
            #endregion
        }

    }
}
