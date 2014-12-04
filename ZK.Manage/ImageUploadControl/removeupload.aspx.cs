using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class removeupload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filename = Server.MapPath(Request.Params["filename"]);
            FileInfo TheFile = new FileInfo(filename);
            if (TheFile.Exists) File.Delete(filename);
        }
        catch (Exception ex)
        {
        }
    }
}