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

public partial class aplicativo_mapa_municipios : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("municipios", 7))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "codigo";
        m_saCampos[2] = "departamento";
        m_saCampos[3] = "nombre";
        m_saCampos[4] = "totalhabitantes";
        m_saCampos[5] = "eterritorial";
        m_saCampos[6] = "region";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Código División política";
        m_saTitulos[2] = "Departamento";
        m_saTitulos[3] = "Nombre Municipio";
        m_saTitulos[4] = "Total Habitantes";
        m_saTitulos[5] = "Entidad Territorial";
        m_saTitulos[6] = "Región";

        for (int i = 0; i < m_saCamposAlias.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[2] = "Departamentos.nombre";
        m_saCamposAlias[5] = "eterritoriales.nombre";
        m_saCamposAlias[6] = "varios.nombre";

        m_szJoin = " LEFT JOIN Departamentos ON (Departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=municipios.eterritorial)"
            + " LEFT JOIN varios ON (varios.id=municipios.region)";

        m_szCondicion = " (municipios.nReservado IS NULL OR municipios.nReservado!=-1) ";

        // autofiltro
        if(m_CBDepartamento.SelectedIndex>0)
            m_szCondicion+=" AND municipios.departamento=" + m_CBDepartamento.SelectedValue;
        if(m_CBEnte.SelectedIndex>0)
            m_szCondicion+= " AND municipios.eterritorial=" + m_CBEnte.SelectedValue;
        if( m_CBRegion.SelectedIndex>0)
            m_szCondicion+=" AND municipios.region=" + m_CBRegion.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);

        m_baHidden[0] = true;
        m_szIndice = m_saTitulos[0];

        FillComboBox(m_CBRegion, "SELECT id,nombre FROM varios WHERE tipo=4 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBDepartamento, "SELECT codigo,nombre FROM Departamentos WHERE observaciones IS NULL OR observaciones NOT LIKE '-1'");
        FillComboBox(m_CBEnte, "SELECT codigo,nombre FROM eterritoriales ORDER BY nombre");

        m_ECodigo.Attributes["onKeypress"] = "isnb();";
        m_ETotal.Attributes["onKeypress"] = "isnb();";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ECodigo.UpdateAfterCallBack = true;
        m_CBDepartamento.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_ETotal.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBRegion.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_ECodigo.Visible = show;
        m_ENombre.Visible = show;
        m_CBDepartamento.Visible = show;
        m_ETotal.Visible = show;
        m_CBEnte.Visible = show;
        m_CBRegion.Visible = show;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_ECodigo.Text=row[1].ToString();
        ListItem item = m_CBDepartamento.Items.FindByText(row[2].ToString());
        m_CBDepartamento.SelectedIndex = item == null ? 0 : m_CBDepartamento.Items.IndexOf(item);
        m_ENombre.Text=row[3].ToString();
        m_ETotal.Text=row[4].ToString();
        item = m_CBEnte.Items.FindByText(row[5].ToString());
        m_CBEnte.SelectedIndex = item == null ? 0 : m_CBEnte.Items.IndexOf(item);
        item = m_CBRegion.Items.FindByText(row[6].ToString());
        m_CBRegion.SelectedIndex = item == null ? 0 : m_CBRegion.Items.IndexOf(item);
        
        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ECodigo.Text="";
        m_ENombre.Text = "";
        m_ETotal.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        m_saDatos[1] = m_ECodigo.Text;
        m_saDatos[2] = m_CBDepartamento.SelectedValue;
        m_saDatos[3] = m_ENombre.Text;
        m_saDatos[4] = m_ETotal.Text;
        m_saDatos[5] = m_CBEnte.SelectedValue;
        m_saDatos[6] = m_CBRegion.SelectedValue;

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Municipio " + m_ENombre.Text + " Agregado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        
        m_saDatos[1] = m_ECodigo.Text;
        m_saDatos[2] = m_CBDepartamento.SelectedValue;
        m_saDatos[3] = m_ENombre.Text;
        m_saDatos[4] = m_ETotal.Text;
        m_saDatos[5] = m_CBEnte.SelectedValue;
        m_saDatos[6] = m_CBRegion.SelectedValue;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Municipio " + m_ENombre.Text + " Editado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE municipios SET nReservado=-1 WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Municipio " + m_ENombre.Text + " Eliminado");
        }
    }
    protected void m_CBDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[3].Visible = m_CBDepartamento.SelectedIndex == 0;
        m_LCResultados.Columns[6].Visible = m_CBEnte.SelectedIndex == 0;
        m_LCResultados.Columns[7].Visible = m_CBRegion.SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBDepartamento_SelectedIndexChanged(sender, e);
    }
    protected void m_CBRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBDepartamento_SelectedIndexChanged(sender, e);
    }
}
