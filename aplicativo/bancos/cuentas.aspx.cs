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

public partial class aplicativo_bancos_cuentas : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("cuentasbancarias", 5))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "numero";
        m_saCampos[2] = "tipo";
        m_saCampos[3] = "entidad";
        m_saCampos[4] = "sucursal";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[2] = "varios.nombre";
        m_saCamposAlias[3] = "varios1.nombre";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Número Cuenta";
        m_saTitulos[2] = "Tipo Cuenta";
        m_saTitulos[3] = "Entidad Bancaria";
        m_saTitulos[4] = "Sucursal";

        m_szJoin = " LEFT JOIN varios ON (varios.id=cuentasbancarias.tipo) "
            + " LEFT JOIN varios AS varios1 ON (varios1.id=cuentasbancarias.entidad) "
            ;

        m_szCondicion = " cuentasbancarias.sucursal!='-1' ";

        if (m_CBBanco.SelectedIndex > 0)
            m_szCondicion+= " AND cuentasbancarias.entidad=" +m_CBBanco.SelectedValue;
        if (m_CBTipo.SelectedIndex > 0)
            m_szCondicion += " AND cuentasbancarias.tipo=" + m_CBTipo.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        m_baHidden[0] = true;

        m_szIndice = m_saTitulos[0];

        FillComboBox(m_CBTipo, "SELECT id,nombre FROM varios WHERE tipo=28 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBBanco, "SELECT id,nombre FROM varios WHERE tipo=27 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ENumero.UpdateAfterCallBack = true;
        m_CBTipo.UpdateAfterCallBack = true;
        m_CBBanco.UpdateAfterCallBack = true;
        m_ESucursal.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_ENumero.Visible = show;
        m_CBTipo.Visible = show;
        m_CBBanco.Visible = show;
        m_ESucursal.Visible = show;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_ENumero.Text= row[1].ToString();

        ListItem item = m_CBTipo.Items.FindByText(row[2].ToString());
        m_CBTipo.SelectedIndex = item == null ? 0 : m_CBTipo.Items.IndexOf(item);

        item = m_CBBanco.Items.FindByText(row[3].ToString());
        m_CBBanco.SelectedIndex = item == null ? 0 : m_CBBanco.Items.IndexOf(item);
        m_ESucursal.Text = row[4].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ENumero.Text="";
        m_ESucursal.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        m_saDatos[1] = m_ENumero.Text;
        m_saDatos[2] = m_CBTipo.SelectedValue;
        m_saDatos[3] = m_CBBanco.SelectedValue;
        m_saDatos[4] = m_ESucursal.Text;

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Cuenta " + m_ENumero.Text + " Agregada");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[1] = m_ENumero.Text;
        m_saDatos[2] = m_CBTipo.SelectedValue;
        m_saDatos[3] = m_CBBanco.SelectedValue;
        m_saDatos[4] = m_ESucursal.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Cuenta " + m_ENumero.Text + " Editada");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM cuentasbancarias WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Cuenta " + m_ENumero.Text + " Eliminada");
        }
    }
    protected void m_CBTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[3].Visible = m_CBTipo.SelectedIndex == 0;
        m_LCResultados.Columns[4].Visible = m_CBBanco.SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }

    protected void m_CBBanco_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBTipo_SelectedIndexChanged(sender, e);
    }
}
