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

public partial class informes_informe_segconproy : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (id == null || id == "")
            return;
        string modoi = Request.QueryString["modo"];
        if (modoi == null)
            modoi = "";

        PageLoad("conveniosm", 8, false);

        m_saTitulos[0] = "Entidad Territorial";
        m_saTitulos[1] = "Numero de convenio marco";
        m_saTitulos[2] = "Municipio";
        m_saTitulos[3] = "Nombre de la institución educativa";
        m_saTitulos[4] = "Valor proyecto aprobado";
        m_saTitulos[5] = "Partida asignada Entidad proyecto aprobado";
        m_saTitulos[6] = "Valor contratado (Incluido adicionales)";
        m_saTitulos[7] = "% de ejecución sobre valor contratado";

        m_saCampos[0] = "eterritoriales.nombre";
        m_saCampos[1] = "conveniosm.codigo";
        m_saCampos[2] = "municipios.nombre";
        m_saCampos[3] = "insteducativas.nombre";
        m_saCampos[4] = "(SELECT SUM(objeto.valor) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[5] = "presupuesto.valormen";
        m_saCampos[6] = "SUM(contratos.valor)";
        m_saCampos[7] = "(SELECT CONVERT(DECIMAL(20,2),(SUM(reporte.valoravance)/SUM(contratos.valor))*100) FROM contratos LEFT JOIN reporte ON (reporte.contrato=contratos.id) WHERE contratos.proyecto=proyectos.id )";
            // "(SELECT CONVERT(DECIMAL(20,2),(SUM(reporte.valoravance)/SUM(objeto.valor))*100) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_saCampos[i];

        m_szIndice = "";

        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial)"
            + " LEFT JOIN proyectos ON (proyectos.eterritorial=conveniosm.eterritorial AND proyectos.vigencia='" + m_pUsuario.GetVigenciaActual() + "') "
            + " LEFT JOIN contratos ON (contratos.proyecto=proyectos.id) "
            + " LEFT JOIN reporte ON (reporte.contrato=contratos.id)"
            + " LEFT JOIN municipios ON (proyectos.municipio=municipios.id) "
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion) "
            + " LEFT JOIN presupuesto ON (presupuesto.proyecto=proyectos.id)"
            ;

        m_szCondicion = " conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");

        if (id != "-1")
            m_szCondicionEx += " AND conveniosm.eterritorial=" + id;
        
        m_szCondicionEx += " GROUP BY ";
        for (int i = 0; i < 4; i++)
        {
            m_szCondicionEx += m_saCamposAlias[i] + ",";
        }
        m_szCondicionEx += m_saCamposAlias[5] + ",proyectos.id";

        m_szCondicionEx += " ORDER BY eterritoriales.nombre";

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);

        m_bClean = true;

        EndPageLoad();
        // aqui redireccionamos
        
        CTablaEx tabla = new CTablaEx();
        tabla.Select();

        string url = "";
        url = HttpContext.Current.Request.ApplicationPath + "/print.aspx" + url + "?modo=" + modoi + "&titulo=" + Request.QueryString["titulo"];
        Response.Redirect(url);
    }
}
