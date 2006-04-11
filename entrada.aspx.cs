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

public partial class entrada : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("pragma", "no-cache");
        Response.CacheControl = "Private";
        Response.Expires = 0;
        if (!CConector.IsLogged(this))
        {
            Response.Redirect("default.aspx");
            Response.End();
            return;
        }
        if (!IsPostBack)
        {
            CUsuarioDatos usuario = (CUsuarioDatos)Session["usuario"];
            usuario.GetVigencias(m_CBVigenciasCombo);
            m_SUsuarioActual.Text = usuario.GetNombre();
        }
        m_SFechaActual.Text = CPageTemplate.GetFechaHora(DateTime.Now);
        m_STituloLargo.Text = "Entrada";
    }

    public void GetMenu()
    {
        CMenu menu = (CMenu)Session["menu"];
        if (menu == null)
        {
            menu = new CMenu(this.Page);
            Session.Add("menu", menu);
        }
        Response.Write(menu.GetMenu());
    }
    protected void m_CBVigenciasCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CUsuarioDatos usuario = (CUsuarioDatos)Session["usuario"];
        if (usuario.SetVigencia(m_CBVigenciasCombo.SelectedValue))
        {
            Response.Redirect(Request.Url.AbsolutePath);
            Response.End();
        }
    }
}
