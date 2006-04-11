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

public partial class sistema_parametros_personas : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("", 1))
            return;
        if (!PageLoad("personas", 12))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "nombre";
        m_saCampos[2] = "tipop";
        m_saCampos[3] = "grupop";
        m_saCampos[4] = "direccion";
        m_saCampos[5] = "telefono";
        m_saCampos[6] = "celular";
        m_saCampos[7] = "email";
        m_saCampos[8] = "cargo";
        m_saCampos[9] = "cedula";
        m_saCampos[10] = "empresa";
        m_saCampos[11] = "municipio";

        m_saTitulos[0] = "idpersona";
        m_saTitulos[1] = "Nombre";
        m_saTitulos[2] = "Tipo";
        m_saTitulos[3] = "Grupo";
        m_saTitulos[4] = "Dirección";
        m_saTitulos[5] = "Teléfono";
        m_saTitulos[6] = "Celular";
        m_saTitulos[7] = "E-Mail";
        m_saTitulos[8] = "Cargo";
        m_saTitulos[9] = "Cedula";
        m_saTitulos[10] = "Empresa";
        m_saTitulos[11] = "Municipio";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[2] = "varios.nombre";
        m_saCamposAlias[3] = "varios1.nombre";
        m_saCamposAlias[11] = "municipios.nombre";

        m_szIndice = m_saTitulos[0];
        m_szJoin = "LEFT JOIN varios ON (varios.id=personas.tipop) "
                + "LEFT JOIN varios AS varios1 ON (varios1.id=personas.grupop) "
                + "LEFT JOIN municipios ON (municipios.id=personas.municipio)";

        m_szCondicion = " (empresa!='-1' OR empresa IS NULL) ";
        // autofiltro
        if (m_CBGrupo.SelectedIndex > 0)
            m_szCondicion += " AND personas.grupop=" + m_CBGrupo.SelectedValue;
        if (m_CBTipo.SelectedIndex > 0)
            m_szCondicion += " AND personas.tipop=" + m_CBTipo.SelectedValue;
        if(m_CBMunicipio.SelectedIndex>0)
            m_szCondicion += " AND personas.municipio=" + m_CBMunicipio.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        m_baHidden[0] = true;

        FillComboBox(m_CBTipo, "SELECT id,nombre FROM varios WHERE tipo=3 AND (nReservado!=-1 OR nReservado IS NULL)ORDER BY nombre");
        FillComboBox(m_CBGrupo, "SELECT id,nombre FROM varios WHERE tipo=2 AND (nReservado!=-1  OR nReservado IS NULL) ORDER BY nombre");
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios ORDER BY nombre");

        m_szMensaje = "No se encontraron personas";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_CBTipo.UpdateAfterCallBack = true;
        m_CBGrupo.UpdateAfterCallBack = true;
        m_EDireccion.UpdateAfterCallBack = true;
        m_ETelefono.UpdateAfterCallBack = true;
        m_ECelular.UpdateAfterCallBack = true;
        m_EMail.UpdateAfterCallBack = true;
        m_ECargo.UpdateAfterCallBack = true;
        m_ECedula.UpdateAfterCallBack = true;
        m_EEmpresa.UpdateAfterCallBack = true;
        m_CBMunicipio.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_ENombre.Text = row[1].ToString();

        // busqueda en el combobox 
        ListItem item = m_CBTipo.Items.FindByText(row[2].ToString());
        m_CBTipo.SelectedIndex = item == null ? 0 : m_CBTipo.Items.IndexOf(item);

        item = m_CBGrupo.Items.FindByText(row[3].ToString());
        m_CBGrupo.SelectedIndex = item == null ? 0 : m_CBGrupo.Items.IndexOf(item);

        m_EDireccion.Text = row[4].ToString();
        m_ETelefono.Text = row[5].ToString();
        m_ECelular.Text = row[6].ToString();
        m_EMail.Text = row[7].ToString();
        m_ECargo.Text = row[8].ToString();
        m_ECedula.Text = row[9].ToString();
        m_EEmpresa.Text = row[10].ToString();

        item = m_CBMunicipio.Items.FindByText(row[11].ToString());
        m_CBMunicipio.SelectedIndex = item == null ? 0 : m_CBMunicipio.Items.IndexOf(item);

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ENombre.Text = "";
        //m_CBTipo.SelectedIndex = 0;
        //m_CBGrupo.SelectedIndex = 0;
        m_EDireccion.Text="";
        m_ETelefono.Text="";
        m_ECelular.Text="";
        m_EMail.Text="";
        m_ECargo.Text="";
        m_ECedula.Text="";
        m_EEmpresa.Text="";
        // m_CBMunicipio.SelectedIndex = 0;

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        m_saDatos[1]= m_ENombre.Text;
        m_saDatos[2] = m_CBTipo.SelectedValue;
        m_saDatos[3] = m_CBGrupo.SelectedValue;
        m_saDatos[4] = m_EDireccion.Text;
        m_saDatos[5] = m_ETelefono.Text;
        m_saDatos[6] = m_ECelular.Text;
        m_saDatos[7] = m_EMail.Text;
        m_saDatos[8] = m_ECargo.Text;
        m_saDatos[9] = m_ECedula.Text;
        m_saDatos[10] = m_EEmpresa.Text;
        m_saDatos[11] = m_CBMunicipio.SelectedValue;
        // incializar los datos a insertar

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Persona " + m_ENombre.Text + " Ingresada");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_CBTipo.SelectedValue;
        m_saDatos[3] = m_CBGrupo.SelectedValue;
        m_saDatos[4] = m_EDireccion.Text;
        m_saDatos[5] = m_ETelefono.Text;
        m_saDatos[6] = m_ECelular.Text;
        m_saDatos[7] = m_EMail.Text;
        m_saDatos[8] = m_ECargo.Text;
        m_saDatos[9] = m_ECedula.Text;
        m_saDatos[10] = m_EEmpresa.Text;
        m_saDatos[11] = m_CBMunicipio.SelectedValue;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Persona " + m_ENombre.Text + " Editada");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE personas SET empresa=-1 WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Persona " + m_ENombre.Text + " Eliminada");
        }
    }
    protected void m_CBTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[3].Visible = m_CBTipo.SelectedIndex == 0;
        m_LCResultados.Columns[4].Visible = m_CBGrupo.SelectedIndex == 0;
        m_LCResultados.Columns[12].Visible = m_CBMunicipio.SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBTipo_SelectedIndexChanged(sender, e);
    }
}