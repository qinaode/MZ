using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class saveupload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpFileCollection uploadedFiles = Request.Files;
        int i = 0;
        if (uploadedFiles.Count > 0)
        {
            while (!(i == uploadedFiles.Count))
            {
                HttpPostedFile userPostedFile = uploadedFiles[i];
                if (userPostedFile.ContentLength > 0)
                {
                    string filename = userPostedFile.FileName.Substring(userPostedFile.FileName.LastIndexOf("\\") + 1);
                    userPostedFile.SaveAs(Path.Combine(Server.MapPath("UserData"), filename));
                }
                i += 1;
            }
        }
    }
}
