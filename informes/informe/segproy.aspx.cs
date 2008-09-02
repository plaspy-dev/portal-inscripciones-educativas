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

public partial class informes_informe_segproy : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (id == null || id == "")
            return;
        string modoi = Request.QueryString["modo"];
        if (modoi == null)
            modoi = "";

        PageLoad("conveniosm", 1, false);

        m_saCampos[0] = "";
        m_saTitulos[0] = "";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_saCampos[i];

        m_szIndice = "";

        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial)"
            ;

        m_szCondicion = " conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");

        if (id != "-1")
            m_szCondicionEx += " AND conveniosm.eterritorial=" + id;

        m_szQuery = "SELECT '????' AS '????'";
        // m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);

        EndPageLoad();
        // aqui redireccionamos

        CTablaEx tabla = new CTablaEx();
        tabla.Select();

        string url = "";
        url = HttpContext.Current.Request.ApplicationPath + "/print.aspx" + url + "?modo=" + modoi + "&titulo="+ Request.QueryString["titulo"];
        Response.Redirect(url);
    }
}
