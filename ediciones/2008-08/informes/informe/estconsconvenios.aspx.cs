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

public partial class informes_informe_estconsconvenios : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (id == null || id == "")
            return;
        string modoi= Request.QueryString["modo"];
        if (modoi == null)
            modoi = "";

        PageLoad("conveniosm", 10, false);

        m_saTitulos[0] = "Entidad Territorial";
        m_saTitulos[1] = "No. Convenio Marco";
        m_saTitulos[2] = "Valor inicial convenio marco";
        m_saTitulos[3] = "Fecha aprobacion convenio marco";
        m_saTitulos[4] = "Plazo convenio marco (Dias, incluye adiciones,suspenciones,etc)";
        m_saTitulos[5] = "Fecha vencimiento convenio marco";
        m_saTitulos[6] = "Tipo convenio";
        m_saTitulos[7] = "Estado convenio (En legalizacion,vigente, proceso de liquidación, liquidado)";
        m_saTitulos[8] = "Prorroga a convenios (días)";
        m_saTitulos[9] = "Nueva fecha de vencimiento";

        m_saCampos[0] = "eterritoriales.nombre";
        m_saCampos[1] = "conveniosm.codigo";
        m_saCampos[2] = "(SELECT conveniosm.valor+ISNULL(SUM(adicionesconvenio.valor),0) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[3] = "conveniosm.fechaaprobacion";
        m_saCampos[4] = "(SELECT conveniosm.plazo+ISNULL(SUM(adicionesconvenio.plazo),0) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[5] = "DATEADD(dd,conveniosm.plazo-1,conveniosm.fechaaprobacion)";
        m_saCampos[6] = "tipos.nombre";
        m_saCampos[7] = "estados.nombre";
        m_saCampos[8] = "(SELECT ISNULL(SUM(adicionesconvenio.plazo),0) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[9] = "(SELECT DATEADD(dd,conveniosm.plazo+ISNULL(SUM(adicionesconvenio.plazo),0)-1,conveniosm.fechaaprobacion) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";

         for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_saCampos[i];

        m_szIndice = "";

        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial)"
            + " LEFT JOIN varios AS tipos ON (tipos.id=conveniosm.tipo)"
            + " LEFT JOIN varios AS estados ON (estados.id=conveniosm.estado)"
            ;

        m_szCondicion = " conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");

        if(id!="-1")
            m_szCondicionEx += " AND conveniosm.eterritorial=" + id;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);

        EndPageLoad();
        // aqui redireccionamos

        CTablaEx tabla = new CTablaEx();
        tabla.Select();

        string url = "";
        url = HttpContext.Current.Request.ApplicationPath + "/print.aspx" + url + "?modo=" + modoi + "&titulo=" + Request.QueryString["titulo"];
        Response.Redirect(url);
    }
}

