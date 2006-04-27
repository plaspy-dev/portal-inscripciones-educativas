using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class fotos_contratos_fotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CConector.IsLogged(this))
            return;
        if (Request.QueryString["id"] == null || Request.QueryString["id"].Length == 0)
            return;
        MemoryStream stream = new MemoryStream();
        CConector conector = new CConector();
        SqlConnection connection = conector.GetConexion();
        try
        {
            SqlCommand command = new
              SqlCommand("SELECT foto FROM fotos WHERE id=" + Request.QueryString["id"], connection);
            byte[] image = (byte[])command.ExecuteScalar();
            stream.Write(image, 0, image.Length);
            Bitmap bitmap = new Bitmap(stream);
            int h= Request.QueryString["h"]==null ? 0 : Convert.ToInt32(Request.QueryString["h"]);
            int w = Request.QueryString["w"] == null ? 0 : Convert.ToInt32(Request.QueryString["w"]);
            Response.ContentType = "image/jpeg";
            if (h > 0 && w > 0)
            {
                Bitmap otro = new Bitmap(bitmap, new Size(100, 100));
                otro.Save(Response.OutputStream, ImageFormat.Jpeg);
            }
            else bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        }
        finally
        {
            connection.Close();
            stream.Close();
        }
    }
}
