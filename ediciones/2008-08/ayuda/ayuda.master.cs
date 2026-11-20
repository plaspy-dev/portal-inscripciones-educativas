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

public partial class ayuda_ayuda : System.Web.UI.MasterPage
{
    public string Titulo {
        set {
            m_LTitulo.Text = value;
        }
    }
    public string Descripcion {
        set {
            m_SDescripcion.Text = value;
        }
    }
    public Repeater Rep {
        get {
            return Repeater1;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        m_LTitulo.NavigateUrl = Request.Url.AbsolutePath;
    }
}
