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

public partial class inscripcion_priorizacion_prioridad : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectosi", 9))
            return;
        m_saCampos[0] = "id";
        m_saCampos[1]="";
        m_saCampos[2]="";
        m_saCampos[3]="";
        m_saCampos[4]="codigo";
        m_saCampos[5]="";
        m_saCampos[6]="";
        m_saCampos[7]="";
        m_saCampos[8] = "priorizacion";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Entidad Territorial";
        m_saTitulos[2] = "Departamento";
        m_saTitulos[3] = "Municipio";
        m_saTitulos[4] = "Número";
        m_saTitulos[5] = "Institución educativa";
        m_saTitulos[6] = "Criticidad";
        m_saTitulos[7] = "Tipo";
        m_saTitulos[8] = "Bloqueado";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1] = "eterritoriales.nombre";
        m_saCamposAlias[2] = "departamentos.nombre";
        m_saCamposAlias[3] = "municipios.nombre";
        m_saCamposAlias[5] = "insteducativas.nombre";
        m_saCamposAlias[6] = "varios.nombre";
        m_saCamposAlias[7] = "varios1.nombre";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectosi.eterritorial)"
            + " LEFT JOIN municipios ON (municipios.id=proyectosi.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectosi.institucion)"
            + " LEFT JOIN varios ON (varios.id=proyectosi.criticidad)"
            + " LEFT JOIN varios AS varios1 ON (varios1.id=proyectosi.tipo)";
        
        m_szCondicion = " proyectosi.vigencia='" + m_pUsuario.GetVigenciaActual() + "' AND (proyectosi.estudiantesb IS NULL OR proyectosi.estudiantesb!=-1) ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("proyectosi.eterritorial");

        m_pUsuario.FillComboBoxInscritos(m_CBEnte);
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_CBEnte.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectosi WHERE municipio=" + m_CBMunicipio.SelectedValue, true);
        if (!IsPostBack)
        {
            m_CBEstado.Items.Add(new ListItem("(Ninguno)", "-1"));
            m_CBEstado.Items.Add(new ListItem("(Bloqueado)", "0"));
        }

        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBMunicipio.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.municipio=" + m_CBMunicipio.SelectedValue;
        if (m_CBProyecto.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.id=" + m_CBProyecto.SelectedValue;
        if (m_CBEstado.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.priorizacion=0";

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;
        m_szMensaje = "No se encontraron proyectos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBProyecto.UpdateAfterCallBack = true;
        m_SInstitucion.UpdateAfterCallBack = true;
        m_SObjeto.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBEnte, row[1].ToString());
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_CBEnte.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        SelectInCombo(m_CBMunicipio, row[3].ToString());
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectosi WHERE municipio=" + m_CBMunicipio.SelectedValue, true);
        SelectInCombo(m_CBProyecto, row[4].ToString());
        m_SInstitucion.Text = row[5].ToString();
        m_CBEstado.SelectedIndex= row[8].ToString()=="0" ? 1 : 0;

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        m_nId.Value="";
        m_CBEstado.SelectedIndex=0;
        m_CBEnte.SelectedIndex = 0;
        m_CBMunicipio.SelectedIndex = 0;
        m_CBProyecto.SelectedIndex = 0;
        m_SInstitucion.Text = "";
        m_SObjeto.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        // m_saDatos[1] = m_ENombre.Text;
        for (int i = 0; i < m_saCampos.Length; i++)
        {
            m_saDatos[i]= m_saCampos[i]="";
        }
        m_saCampos[0] = "priorizacion";
        m_saDatos[0] = m_CBEstado.SelectedIndex>0 ? "0" : "%NULL";

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Proyecto " + m_CBProyecto.SelectedItem.Text + " " + (m_CBEstado.SelectedIndex>0 ? "Bloqueado" : "Desbloqueado" ) );
        }
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBEstado_SelectedIndexChanged(sender, e);
    }
    protected void m_CBMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBProyecto.UpdateAfterCallBack = true;
        m_CBEstado_SelectedIndexChanged(sender, e);
    }
    protected void m_CBProyecto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_CBProyecto.SelectedIndex > 0)
        {
            m_CBEstado_SelectedIndexChanged(sender, e);
            m_LCResultados.SelectedIndex = 0;
            m_LCResultados_SelectedIndexChanged(m_LCResultados, e);
        }
        else
        {
            m_BCancelar_Click(m_BCancelar, e);
        }
    }
    protected void m_CBEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow((GridView)sender, CTablaEx.GetTabla(), e);
        if (e.Row.DataItemIndex != -1)
        {
            e.Row.Cells[9].Text = e.Row.Cells[9].Text == "0" ? "Si" : "No";
        }
    }
}
