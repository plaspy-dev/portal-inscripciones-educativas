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

public partial class conf_app : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("pragma", "no-cache");
        Response.CacheControl = "Private";
        Response.Expires = 0;
        if (!CConector.IsLogged(this))
        {
            Response.Redirect(HttpContext.Current.Request.ApplicationPath + "/default.aspx");
            Response.End();
            return;
        }
        if (!IsPostBack)
        {
            int max = CConector.GetCookie("maxcelda", 25);
            if(max>3)
                m_ELimite.Text = max.ToString();

            m_CBPagina.SelectedIndex = 1;
            string pagina= CConector.GetCookie("pager","20");
            for (int i = 0; i<m_CBPagina.Items.Count; i++)
            {
                if (m_CBPagina.Items[i].Value == pagina)
                {
                    m_CBPagina.SelectedIndex = i;
                    break;
                }
            }
            CUsuarioDatos usuario = (CUsuarioDatos)Session["usuario"];
            usuario.GetVigencias(m_CBVigenciasCombo);
            m_SUsuarioActual.Text = usuario.GetNombre();
            CDateFormat date = new CDateFormat();
            CDateFormat.GetComboBox(m_CBCampo1);
            CDateFormat.GetComboBox(m_CBCampo2);
            CDateFormat.GetComboBox(m_CBCampo3);
            CDateFormat.GetComboBox(m_CBCampo4);
            m_ESeparador1.Text = date.GetSeparador(1);
            m_ESeparador2.Text = date.GetSeparador(2);
            m_ESeparador3.Text = date.GetSeparador(3);
            CPageTemplate.SelectInComboValue(m_CBCampo1, date.GetCampo(1));
            CPageTemplate.SelectInComboValue(m_CBCampo2, date.GetCampo(2));
            CPageTemplate.SelectInComboValue(m_CBCampo3, date.GetCampo(3));
            CPageTemplate.SelectInComboValue(m_CBCampo4, date.GetCampo(4));
            m_SEjemplo.Text = date.GetFecha(DateTime.Now);
        }
        m_SFechaActual.Text = CPageTemplate.GetFechaHora(DateTime.Now);
        m_STituloLargo.Text = "/Configurar/Sistema";
    }
    public void GetVigencia()
    {
        CUsuarioDatos usuario = (CUsuarioDatos)Session["usuario"];
        Response.Write(usuario.GetVigenciaActual());
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        CConector.SetCookie("pager",m_CBPagina.SelectedValue);
        int max = 0;
        if( m_ELimite.Text.Length>0)
            max = Convert.ToInt32(m_ELimite.Text);
        CConector.SetCookie("maxcelda",max.ToString());
        CDateFormat date = new CDateFormat();
        date.SetSeparador(m_ESeparador1.Text, 1);
        date.SetSeparador(m_ESeparador2.Text, 2);
        date.SetSeparador(m_ESeparador3.Text, 3);
        date.SetCampo(m_CBCampo1.SelectedValue, 1);
        date.SetCampo(m_CBCampo2.SelectedValue, 2);
        date.SetCampo(m_CBCampo3.SelectedValue, 3);
        date.SetCampo(m_CBCampo4.SelectedValue, 4);
        date.Guardar();
        Label1.Text = "Configuración guardada";
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
    protected void m_BCorta_Click(object sender, EventArgs e)
    {
        m_ESeparador1.Text = "/";
        m_ESeparador2.Text = "/";
        m_ESeparador3.Text = "";
        CPageTemplate.SelectInComboValue(m_CBCampo1, "dd");
        CPageTemplate.SelectInComboValue(m_CBCampo2, "MM");
        CPageTemplate.SelectInComboValue(m_CBCampo3, "yy");
        m_CBCampo4.SelectedIndex = 0;
        m_ESeparador1.UpdateAfterCallBack = true;
        m_ESeparador2.UpdateAfterCallBack = true;
        m_ESeparador3.UpdateAfterCallBack = true;
        m_CBCampo1.UpdateAfterCallBack = true;
        m_CBCampo2.UpdateAfterCallBack = true;
        m_CBCampo3.UpdateAfterCallBack = true;
        m_CBCampo4.UpdateAfterCallBack = true;
        m_BAplicar_Click(sender, e);
    }
    protected void m_BLarga_Click(object sender, EventArgs e)
    {
        m_ESeparador1.Text = " ";
        m_ESeparador2.Text = " de ";
        m_ESeparador3.Text = "";
        CPageTemplate.SelectInComboValue(m_CBCampo1, "MMMM");
        CPageTemplate.SelectInComboValue(m_CBCampo2, "dd");
        CPageTemplate.SelectInComboValue(m_CBCampo3, "yyyy");
        m_CBCampo4.SelectedIndex = 0;
        m_ESeparador1.UpdateAfterCallBack = true;
        m_ESeparador2.UpdateAfterCallBack = true;
        m_ESeparador3.UpdateAfterCallBack = true;
        m_CBCampo1.UpdateAfterCallBack = true;
        m_CBCampo2.UpdateAfterCallBack = true;
        m_CBCampo3.UpdateAfterCallBack = true;
        m_CBCampo4.UpdateAfterCallBack = true;
        m_BAplicar_Click(sender, e);
    }
    protected void m_CBCampo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_BAplicar_Click(sender, e);
    }
    protected void m_BAplicar_Click(object sender, EventArgs e)
    {
        CDateFormat date = new CDateFormat();
        date.SetSeparador(m_ESeparador1.Text, 1);
        date.SetSeparador(m_ESeparador2.Text, 2);
        date.SetSeparador(m_ESeparador3.Text, 3);
        date.SetCampo(m_CBCampo1.SelectedValue, 1);
        date.SetCampo(m_CBCampo2.SelectedValue, 2);
        date.SetCampo(m_CBCampo3.SelectedValue, 3);
        date.SetCampo(m_CBCampo4.SelectedValue, 4);
        m_SEjemplo.Text = date.GetFecha(DateTime.Now);
        m_SEjemplo.UpdateAfterCallBack = true;
    }
}
