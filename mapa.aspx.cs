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

public partial class mapa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CMapa mapa = CMapa.GetMapa("2003");
        }
        m_SUsuarioActual.Text = " ";
        m_SFechaActual.Text= CPageTemplate.GetFechaHora(DateTime.Now);
        m_STituloLargo.Text = "Mapa";
        m_SVersion.Text = CConector.GetCurrentVersion();
    }
}
