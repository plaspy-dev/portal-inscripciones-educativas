using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class redirectflash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["tipomapa"]= Request.QueryString["tipodato"];
        Session["departamentomapa"] = Request.QueryString["departamento"];
        Response.Write("&respuesta=true");
        string algo = "datosdep.aspx?tipomapa=" + Request.QueryString["tipodato"] + "&departamento=" + Request.QueryString["departamento"];
        Response.Write("&mixml=datosdep.aspx?tipomapa=" + Request.QueryString["tipodato"] + Request.QueryString["departamento"]);
    }
}
