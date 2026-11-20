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

public partial class aplicativo_institucion_ieducativas : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("", 1))
            return;

        if (!PageLoad("insteducativas", 17))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "codigo";
        m_saCampos[2] = "nombre";
        m_saCampos[3] = "calendario";
        m_saCampos[4] = "tpoblacion";
        m_saCampos[5] = "sector";
        m_saCampos[6] = "departamento";
        m_saCampos[7] = "municipio";
        m_saCampos[8] = "modalidad";
        m_saCampos[9] = "rector";
        m_saCampos[10] = "zona";
        m_saCampos[11] = "barrioinspeccion";
        m_saCampos[12] = "direccion";
        m_saCampos[13] = "telefonos";
        m_saCampos[14] = "fax";
        m_saCampos[15] = "web";
        m_saCampos[16] = "email";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Código DANE";
        m_saTitulos[2] = "Nombre";
        m_saTitulos[3] = "Calendario Escolar";
        m_saTitulos[4] = "Tipo de Población";
        m_saTitulos[5] = "Sector Educativo";
        m_saTitulos[6] = "Departamento";
        m_saTitulos[7] = "Municipio";
        m_saTitulos[8] = "Modalidad";
        m_saTitulos[9] = "Rector";
        m_saTitulos[10] = "Zona";
        m_saTitulos[11] = "Barrio Inspección";
        m_saTitulos[12] = "Dirección";
        m_saTitulos[13] = "Teléfonos";
        m_saTitulos[14] = "Fax";
        m_saTitulos[15] = "Web";
        m_saTitulos[16] = "E-Mail";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[3] = "varios.nombre";
        m_saCamposAlias[4] = "varios1.nombre";
        m_saCamposAlias[5] = "varios2.nombre";
        m_saCamposAlias[6]="departamentos.nombre";
        m_saCamposAlias[7] = "municipios.nombre";
        m_saCamposAlias[8] = "varios4.nombre";
        m_saCamposAlias[9] = "personas.nombre";
        m_saCamposAlias[10] = "varios5.nombre";
        m_saCamposAlias[6] = "departamentos.nombre";

        m_szJoin = " LEFT JOIN varios ON (varios.id=insteducativas.calendario)"
            + " LEFT JOIN varios AS varios1 ON (varios1.id=insteducativas.tpoblacion)"
            + " LEFT JOIN varios AS varios2 ON (varios2.id=insteducativas.sector)"
            + " LEFT JOIN varios AS varios5 ON (varios5.id=insteducativas.zona)"
            + " LEFT JOIN varios AS varios4 ON (varios4.id=insteducativas.modalidad)"
            + " LEFT JOIN personas ON (personas.id=insteducativas.rector)"
            + " LEFT JOIN municipios ON (municipios.id=insteducativas.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            ;
        m_szCondicion = " principal IS NULL ";

        if(m_CBCalendario.SelectedIndex>0)
           m_szCondicion+="AND insteducativas.calendario=" + m_CBCalendario.SelectedValue;
        if(m_CBTipo.SelectedIndex>0)
           m_szCondicion+="AND insteducativas.tpoblacion=" + m_CBTipo.SelectedValue;
        if(m_CBSector.SelectedIndex>0)
           m_szCondicion+="AND insteducativas.sector=" + m_CBSector.SelectedValue;
        if(m_CBDepartamento.SelectedIndex>0)
            m_szCondicion += "AND municipios.departamento=" + m_CBDepartamento.SelectedValue;
        if(m_CBMunicipio.SelectedIndex>0)
           m_szCondicion+="AND insteducativas.Municipio=" + m_CBMunicipio.SelectedValue;
        if(m_CBModalidad.SelectedIndex>0)
           m_szCondicion+="AND insteducativas.modalidad=" + m_CBModalidad.SelectedValue;
        if(m_CBRector.SelectedIndex>0)
           m_szCondicion+="AND insteducativas.rector=" + m_CBRector.SelectedValue;
        if(m_CBZona.SelectedIndex>0)
            m_szCondicion += "AND insteducativas.zona=" + m_CBZona.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        m_baHidden[0] = true;
        m_szIndice = m_saTitulos[0];


        FillComboBox(m_CBCalendario, "SELECT id,nombre FROM varios WHERE tipo=33 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBTipo, "SELECT id,nombre FROM varios WHERE tipo=10 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBSector, "SELECT id,nombre FROM varios WHERE tipo=6 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBDepartamento, "SELECT codigo,nombre FROM departamentos ORDER BY nombre");
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE departamento=" + m_CBDepartamento.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        FillComboBox(m_CBModalidad, "SELECT id,nombre FROM varios WHERE tipo=7 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBRector, "SELECT personas.id,personas.nombre FROM personas LEFT JOIN varios ON (varios.id=personas.grupop) WHERE personas.municipio=" + m_CBMunicipio.SelectedValue + " AND varios.nombre LIKE 'rector' ORDER BY personas.nombre", true);
        FillComboBox(m_CBZona, "SELECT id,nombre FROM varios WHERE tipo=12 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");

        m_ECodigo.Attributes["onKeypress"] = "isnb();";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ECodigo.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_CBCalendario.UpdateAfterCallBack = true;
        m_CBTipo.UpdateAfterCallBack = true;
        m_CBSector.UpdateAfterCallBack = true;
        m_CBDepartamento.UpdateAfterCallBack = true;
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBModalidad.UpdateAfterCallBack = true;
        m_CBRector.UpdateAfterCallBack = true;
        m_CBZona.UpdateAfterCallBack = true;
        m_EBarrio.UpdateAfterCallBack = true;
        m_EDireccion.UpdateAfterCallBack = true;
        m_ETelefono.UpdateAfterCallBack = true;
        m_EFax.UpdateAfterCallBack = true;
        m_EWeb.UpdateAfterCallBack = true;
        m_EMail.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_ECodigo.Visible = show;
        m_ENombre.Visible = show;
        m_CBCalendario.Visible = show;
        m_CBTipo.Visible = show;
        m_CBSector.Visible = show;
        m_CBDepartamento.Visible = show;
        m_CBMunicipio.Visible = show;
        m_CBModalidad.Visible = show;
        m_CBRector.Visible = show;
        m_CBZona.Visible = show;
        m_EBarrio.Visible = show;
        m_EDireccion.Visible = show;
        m_ETelefono.Visible = show;
        m_EFax.Visible = show;
        m_EWeb.Visible = show;
        m_EMail.Visible = show;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_ECodigo.Text = row[1].ToString();
        m_ENombre.Text = row[2].ToString();

        ListItem item = m_CBCalendario.Items.FindByText(row[3].ToString());
        m_CBCalendario.SelectedIndex = item == null ? 0 : m_CBCalendario.Items.IndexOf(item);

        item = m_CBTipo.Items.FindByText(row[4].ToString());
        m_CBTipo.SelectedIndex = item == null ? 0 : m_CBTipo.Items.IndexOf(item);

        item = m_CBSector.Items.FindByText(row[5].ToString());
        m_CBSector.SelectedIndex = item == null ? 0 : m_CBSector.Items.IndexOf(item);

        item = m_CBDepartamento.Items.FindByText(row[6].ToString());
        m_CBDepartamento.SelectedIndex = item == null ? 0 : m_CBDepartamento.Items.IndexOf(item);
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE departamento=" + m_CBDepartamento.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        item = m_CBMunicipio.Items.FindByText(row[7].ToString());
        m_CBMunicipio.SelectedIndex = item == null ? 0 : m_CBMunicipio.Items.IndexOf(item);
        item = m_CBModalidad.Items.FindByText(row[8].ToString());
        m_CBModalidad.SelectedIndex = item == null ? 0 : m_CBModalidad.Items.IndexOf(item);
        FillComboBox(m_CBRector, "SELECT personas.id,personas.nombre FROM personas LEFT JOIN varios ON (varios.id=personas.grupop) WHERE personas.municipio=" + m_CBMunicipio.SelectedValue + " AND varios.nombre LIKE 'rector' ORDER BY personas.nombre", true);
        item = m_CBRector.Items.FindByText(row[9].ToString());
        m_CBRector.SelectedIndex = item == null ? 0 : m_CBRector.Items.IndexOf(item);
        item = m_CBZona.Items.FindByText(row[10].ToString());
        m_CBZona.SelectedIndex = item == null ? 0 : m_CBZona.Items.IndexOf(item);

        m_EBarrio.Text= row[11].ToString();
        m_EDireccion.Text= row[12].ToString();
        m_ETelefono.Text= row[13].ToString();
        m_EFax.Text= row[14].ToString();
        m_EWeb.Text= row[15].ToString();
        m_EMail.Text= row[16].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ECodigo.Text = "";
        m_ENombre.Text = "";
        m_EBarrio.Text = "";
        m_EDireccion.Text = "";
        m_ETelefono.Text = "";
        m_EFax.Text = "";
        m_EWeb.Text = "";
        m_EMail.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        m_saDatos[1]=m_ECodigo.Text;
        m_saDatos[2]=m_ENombre.Text;
        m_saDatos[3]=m_CBCalendario.SelectedValue;
        m_saDatos[4]=m_CBTipo.SelectedValue;
        m_saDatos[5]=m_CBSector.SelectedValue;
        m_saCampos[6]="";
        m_saDatos[7]=m_CBMunicipio.SelectedValue;
        m_saDatos[8]=m_CBModalidad.SelectedValue;
        m_saDatos[9]=m_CBRector.SelectedIndex>0 ? m_CBRector.SelectedValue : "";
        m_saDatos[10]=m_CBZona.SelectedValue;
        m_saDatos[11]=m_EBarrio.Text;
        m_saDatos[12]=m_EDireccion.Text;
        m_saDatos[13]=m_ETelefono.Text;
        m_saDatos[14]=m_EFax.Text;
        m_saDatos[15]=m_EWeb.Text;
        m_saDatos[16] = m_EMail.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Institución " + m_ENombre.Text + " Insertada");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        m_saDatos[1] = m_ECodigo.Text;
        m_saDatos[2] = m_ENombre.Text;
        m_saDatos[3] = m_CBCalendario.SelectedValue;
        m_saDatos[4] = m_CBTipo.SelectedValue;
        m_saDatos[5] = m_CBSector.SelectedValue;
        m_saCampos[6] = "";
        m_saDatos[7] = m_CBMunicipio.SelectedValue;
        m_saDatos[8] = m_CBModalidad.SelectedValue;
        m_saDatos[9] = m_CBRector.SelectedIndex > 0 ? m_CBRector.SelectedValue : "";
        m_saDatos[10] = m_CBZona.SelectedValue;
        m_saDatos[11] = m_EBarrio.Text;
        m_saDatos[12] = m_EDireccion.Text;
        m_saDatos[13] = m_ETelefono.Text;
        m_saDatos[14] = m_EFax.Text;
        m_saDatos[15] = m_EWeb.Text;
        m_saDatos[16] = m_EMail.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Institución " + m_ENombre.Text + " Editada");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM insteducativas WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Institución " + m_ENombre.Text + " Eliminada");
        }
    }
    protected void m_CBCalendario_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[4].Visible = m_CBCalendario.SelectedIndex == 0;
        m_LCResultados.Columns[5].Visible = m_CBTipo.SelectedIndex == 0;
        m_LCResultados.Columns[6].Visible = m_CBSector.SelectedIndex == 0;
        m_LCResultados.Columns[7].Visible = m_CBDepartamento.SelectedIndex == 0;
        m_LCResultados.Columns[8].Visible = m_CBMunicipio.SelectedIndex == 0;
        m_LCResultados.Columns[9].Visible = m_CBModalidad.SelectedIndex == 0;
        m_LCResultados.Columns[10].Visible = m_CBRector.SelectedIndex == 0;
        m_LCResultados.Columns[11].Visible = m_CBZona.SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
    protected void m_CBSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
    protected void m_CBDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
    protected void m_CBMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBRector.UpdateAfterCallBack = true;
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
    protected void m_CBModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
    protected void m_CBRector_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
    protected void m_CBZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBCalendario_SelectedIndexChanged(sender, e);
    }
}