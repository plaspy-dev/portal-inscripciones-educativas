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

public partial class aplicativo_mapa_eterritoriales : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("eterritoriales", 4))
            return;

        m_saCampos[0] = "codigo";
        m_saCampos[1] = "nombre";
        m_saCampos[2] = "nit";
        m_saCampos[3] = "region";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saTitulos[0] = "Código";
        m_saTitulos[1] = "Nombre Entidad";
        m_saTitulos[2] = "NIT";
        m_saTitulos[3] = "Región";

        m_saCamposAlias[3] = "varios.nombre";

        m_szCondicion = " ( eterritoriales.nReservado IS NULL OR eterritoriales.nReservado!=-1 )";

        m_szJoin = " LEFT JOIN varios ON (varios.id=eterritoriales.region) ";

        if (m_CBRegion.SelectedIndex > 0)
            m_szCondicion += " AND eterritoriales.region=" + m_CBRegion.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);

        m_szIndice = m_saTitulos[0];
        m_ENit.Attributes["onKeypress"] = "isnb();";
        m_ECodigo.Attributes["onKeypress"] = "isnb();";

        FillComboBox(m_CBRegion, "SELECT id,nombre FROM varios WHERE tipo=4 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ECodigo.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_ENit.UpdateAfterCallBack = true;
        m_CBRegion.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_ECodigo.Visible = show;
        m_ENombre.Visible = show;
        m_ENit.Visible = show;
        m_CBRegion.Visible = show;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_ECodigo.Text = row[0].ToString();
        m_ENombre.Text = row[1].ToString();
        m_ENit.Text = row[2].ToString();

        ListItem item = m_CBRegion.Items.FindByText(row[3].ToString());
        m_CBRegion.SelectedIndex = item == null ? 0 : m_CBRegion.Items.IndexOf(item);

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ENombre.Text = "";
        m_ECodigo.Text = "";
        m_ENit.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saDatos[0] = m_ECodigo.Text;
        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_ENit.Text;
        m_saDatos[3] = m_CBRegion.SelectedValue;

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Ente " + m_ENombre.Text + " Insertado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[0] = m_ECodigo.Text;
        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_ENit.Text;
        m_saDatos[3] = m_CBRegion.SelectedValue;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Ente " + m_ENombre.Text + " Editado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE eterritoriales SET nReservado=-1 WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Ente " + m_ENombre.Text + " Eliminado");
        }
    }
    protected void m_CBRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[4].Visible = m_CBRegion.SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}