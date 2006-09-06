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

public partial class informes_informe_segconvenios : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (id == null || id == "")
            return;
        string modoi = Request.QueryString["modo"];
        if (modoi == null)
            modoi = "";

        PageLoad("conveniosm", 11, false);

        m_saTitulos[0] = "Entidad Territorial";
        m_saTitulos[1] = "No. Convenio Marco";
        m_saTitulos[2] = "Valor convenio";
        m_saTitulos[3] = "% promedio de avance de obra";
        m_saTitulos[4] = "Total proyectos aprobados";
        m_saTitulos[5] = "Proyectos en cierre";
        m_saTitulos[6] = "Proyectos en terminacion de obras";
        m_saTitulos[7] = "Proyectos en ejecución";
        m_saTitulos[8] = "Proyectos en reintegro de recursos";
        m_saTitulos[9] = "Proyectos suspendidos";
        m_saTitulos[10] = "Proyectos en reporte del ente territorial";

        m_saCampos[0] = "eterritoriales.nombre";
        m_saCampos[1] = "conveniosm.codigo";
        m_saCampos[2] = "(SELECT conveniosm.valor+ISNULL(SUM(adicionesconvenio.valor),0) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[3] = "(SELECT (ISNULL(SUM(DISTINCT reporte.valoravance),0)/SUM(objeto.valor))*100 FROM proyectos LEFT JOIN objeto ON (objeto.proyecto=proyectos.id) LEFT JOIN contratos ON (contratos.proyecto=proyectos.id) LEFT JOIN reporte ON (reporte.contrato=contratos.id) WHERE proyectos.eterritorial=conveniosm.eterritorial AND proyectos.vigencia='" + m_pUsuario.GetVigenciaActual() + "')";
        m_saCampos[4] = "(SELECT COUNT(*) FROM proyectos WHERE proyectos.eterritorial=conveniosm.eterritorial AND proyectos.vigencia='" + m_pUsuario.GetVigenciaActual() + "')";
        m_saCampos[5] = "'?'";
        m_saCampos[6] = "'?'";
        m_saCampos[7] = "'?'";
        m_saCampos[8] = "'?'";
        m_saCampos[9] = "'?'";
        m_saCampos[10] = "'?'";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_saCampos[i];

        m_szIndice = "";

        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial)"
            ;

        m_szCondicion = " conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");

        if (id != "-1")
            m_szCondicionEx += " AND conveniosm.eterritorial=" + id;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);

        EndPageLoad();
        // aqui redireccionamos

        CTablaEx tabla = new CTablaEx();
        tabla.Select();

        string url = "";
        url = HttpContext.Current.Request.ApplicationPath + "/print.aspx" + url + "?modo=" + modoi + "&titulo="+ Request.QueryString["titulo"];
        Response.Redirect(url);
    }
}
