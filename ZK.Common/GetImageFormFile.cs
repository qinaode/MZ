using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;

namespace ZK.Common
{
    public class GetImageFormFile
    {
        #region PPT to Images

        private string GetPPTImage(string filepath)
        {

            string imagepath = System.IO.Path.GetFileNameWithoutExtension(filepath) + ".jpg";

            try
            {
                Microsoft.Office.Interop.PowerPoint.Application pptapplication = null;
                pptapplication = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentation ppt1 = pptapplication.Presentations.Open(filepath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                ppt1.Slides[1].Export(imagepath, "jpg", 480, 320);
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
    }
}
