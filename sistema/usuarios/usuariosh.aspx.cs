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

public partial class sistema_usuarios_usuariosh : CHelpPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ArrayList values = new ArrayList();
            Cargar(values);

            ayuda_ayuda masterpage = (ayuda_ayuda)Master;

            masterpage.Rep.DataSource = values;
            masterpage.Rep.DataBind();

            masterpage.Titulo = m_szTitulo;
            masterpage.Descripcion = m_szDescripcion;
        }
    }
}
