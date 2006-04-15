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

public partial class galeria : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("", 1, false))
            return;
        // quitar de aqui
        Session["idcontratofoto"] = "80";
        EndPageLoad();
        Label lab = (Label)FindControl("m_STituloLargo");
        if (lab != null)
            lab.Text = "Registro Fotográfico";
    }
}