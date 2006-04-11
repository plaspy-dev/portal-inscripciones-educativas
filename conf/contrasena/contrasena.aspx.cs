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

public partial class conf_contrasena : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("usuarios2", 1))
            return;
        m_saCampos[0] = "clave";
        m_saTitulos[0] = "Clave";
        m_szIndice = m_saTitulos[0];
        m_szCondicion = "id=" + m_pUsuario.GetID();
        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCampos, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        EndPageLoad();
        m_BAceptar.Style["display"]="";
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        // m_saDatos[1] = m_ENombre.Text;
        string clave = m_pConector.QueryString(m_szQuery);
        if (clave == m_EActual.Text)
        {
            m_saDatos[0] = m_ENueva.Text;
            string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", m_pUsuario.GetID());
            if (m_pConector.QueryMod(query) == -1)
            {
                m_SMensaje.Text = m_pConector.GetLastError();
            }
            else
            {
                m_SMensaje.Text="Contraseña Cambiada";
            }
        }
        else
        {
            System.Threading.Thread.Sleep(2000);
            m_SMensaje.Text = "La contraseña actual no coincide";
        }
    }
}