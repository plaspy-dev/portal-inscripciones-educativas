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

public partial class informes_institucion_Default : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("insteducativas", 5))
            return;
        m_saCampos[0] = "id";
        m_saCampos[3] = "codigo";
        m_saCampos[4] = "nombre";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Entidad territorial";
        m_saTitulos[2] = "Municipio";
        m_saTitulos[3] = "Código DANE";
        m_saTitulos[4] = "Nombre";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1] = "eterritoriales.nombre";
        m_saCamposAlias[2] = "municipios.nombre";

        m_szIndice = m_saTitulos[0];

        m_szJoin = " LEFT JOIN municipios ON (municipios.id=insteducativas.municipio)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=municipios.eterritorial)";

        FillComboBox(m_CBEnte, "SELECT codigo,nombre FROM eterritoriales ORDER BY nombre");
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_CBEnte.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        
        if (Session["busqueda"] == null || Session["busqueda"] == "")
        {
            if (m_CBEnte.SelectedIndex > 0)
                m_szCondicion = "municipios.eterritorial=" + m_CBEnte.SelectedValue;
            if (m_CBMunicipio.SelectedIndex > 0)
                m_szCondicion = "insteducativas.municipio=" + m_CBMunicipio.SelectedValue;
        }
        else
        {
            if (Session["busqueda"] == "insteducativas.nombre")
            {
                m_szCondicion = "insteducativas.nombre LIKE '%" + m_ENombre.Text + "%'";
            }
            else
            {
                m_szCondicion = "insteducativas.codigo LIKE '%" + m_ECodigo.Text + "%'";
            }
        }
        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        m_baHidden[0] = true;
        if (!IsPostBack)
        {
            m_szMensaje = "Seleccione o busque una institución";
        }
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nID.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_ECodigo.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nID.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBEnte, row[1].ToString());
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_CBEnte.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        SelectInCombo(m_CBMunicipio, row[2].ToString());
        m_ENombre.Text = row[4].ToString();
        m_ECodigo.Text = row[3].ToString();
        
        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["busqueda"] = "";

        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicion = "municipios.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBMunicipio.SelectedIndex > 0)
            m_szCondicion = "insteducativas.municipio=" + m_CBMunicipio.SelectedValue;
        SetQuery(CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion));

        m_CBMunicipio.UpdateAfterCallBack = true;
        m_LCResultados.Columns[1].Visible = m_CBEnte.SelectedIndex == 0;
        m_LCResultados.SelectedIndex = -1;
        m_LCResultados.DataBind();
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["busqueda"] = "";

        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicion = "municipios.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBMunicipio.SelectedIndex > 0)
            m_szCondicion = "insteducativas.municipio=" + m_CBMunicipio.SelectedValue;
        SetQuery(CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion));

        m_LCResultados.Columns[2].Visible = m_CBMunicipio.SelectedIndex == 0;
        m_LCResultados.SelectedIndex = -1;
        m_LCResultados.DataBind();
        m_LCResultados.UpdateAfterCallBack = true;
    }

    protected void m_BCodigo_Click(object sender, EventArgs e)
    {
        Session["busqueda"] = "insteducativas.codigo";
        m_szCondicion = "insteducativas.codigo LIKE '%" + m_ECodigo.Text + "%'";
        SetQuery(CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion));
        m_LCResultados.SelectedIndex = -1;
        m_LCResultados.DataBind();
        m_LCResultados.UpdateAfterCallBack = true;
    }

    protected void m_BNombre_Click(object sender, EventArgs e)
    {
        Session["busqueda"] = "insteducativas.nombre";
        m_szCondicion = "insteducativas.nombre LIKE '%" + m_ENombre.Text + "%'";
        SetQuery(CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion));
        m_LCResultados.SelectedIndex = -1;
        m_LCResultados.DataBind();
        m_LCResultados.UpdateAfterCallBack = true;
    }
}
