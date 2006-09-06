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

public partial class aprobacion_proyectos_aprobacion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectosi", 18))
            return;
        m_saCampos[0] = "id";
        m_saCampos[1] = "grupo";
        m_saCampos[2] = "codigo";
        m_saCampos[3] = "eterritorial";
        m_saCampos[4] = "departamento";
        m_saCampos[5] = "municipio";
        m_saCampos[6] = "institucion";
        m_saCampos[7] = "priorizacion";
        m_saCampos[8] = "tipo";
        m_saCampos[9] = "objeto";
        m_saCampos[10] = "valor";
        m_saCampos[11] = "aportesmen";
        m_saCampos[12] = "poraportesmen";
        m_saCampos[13] = "valorcoofinanciacion";
        m_saCampos[14] = "porcooffinanciacion";
        m_saCampos[15] = "estado";
        m_saCampos[16] = "conceptomen";
        m_saCampos[17] = "observacionesa";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Código de proyecto";
        m_saTitulos[3] = "Entidad Territorial";
        m_saTitulos[4] = "Departamento";
        m_saTitulos[5] = "Municipio";
        m_saTitulos[6] = "Institución educativa";
        m_saTitulos[7] = "No. Priorización";
        m_saTitulos[8] = "Tipo";
        m_saTitulos[9] = "Objeto";
        m_saTitulos[10] = "Valor Total Proyecto";
        m_saTitulos[11] = "Valor Aportes Entidad educativa";
        m_saTitulos[12] = "% Aportes Entidad educativa";
        m_saTitulos[13] = "Valor Aportes cofinanciación";
        m_saTitulos[14]="% Aportes cofinanciación";
        m_saTitulos[15]="Estado";
        m_saTitulos[16]="Concepto Entidad educativa";
        m_saTitulos[17]="Observaciones";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1]="grupo.nombre";
        m_saCamposAlias[3]="eterritoriales.nombre";
        m_saCamposAlias[4]="departamentos.nombre";
        m_saCamposAlias[5]="municipios.nombre";
        m_saCamposAlias[6]="insteducativas.nombre";
        m_saCamposAlias[8]="tipo.nombre";
        m_saCamposAlias[10]="SUM(objetoinscrito.valor)";
        m_saCamposAlias[11] = "(SUM(objetoinscrito.valor)*presupuestoi.porcentajeMEN)/100";
        m_saCamposAlias[12] = "presupuestoi.porcentajeMEN";
        m_saCamposAlias[13] = "(SUM(objetoinscrito.valor)*(100-presupuestoi.porcentajeMEN))/100";
        m_saCamposAlias[14] = "100-presupuestoi.porcentajeMEN";
        m_saCamposAlias[15] = "estado.nombre";
        m_saCamposAlias[16] = "concepto.nombre";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=proyectosi.eterritorial) AND asignacionpresupuestal.vigencia='" + m_pUsuario.GetVigenciaActual() + "' "
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectosi.eterritorial)"
            + " LEFT JOIN municipios ON (municipios.id=proyectosi.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectosi.institucion)"
            + " LEFT JOIN varios AS tipo ON (tipo.id=proyectosi.tipo)"
            + " LEFT JOIN varios AS grupo ON (grupo.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN objetoinscrito ON (objetoinscrito.proyecto=proyectosi.id)"
            + " LEFT JOIN presupuestoi ON (presupuestoi.proyecto=proyectosi.id)"
            + " LEFT JOIN varios AS estado ON (estado.id=proyectosi.estado)"
            + " LEFT JOIN varios AS concepto ON (concepto.id=proyectosi.conceptomen)"
            ;

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBEstado, "SELECT id,nombre FROM varios WHERE tipo=48 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectosi WHERE priorizacion>=0 AND eterritorial=" + m_CBEnte.SelectedValue
            + " AND proyectosi.vigencia='" + m_pUsuario.GetVigenciaActual() + "'", true);
        FillComboBox(m_CBConcepto, "SELECT id,nombre FROM varios WHERE tipo=49 AND (nReservado IS NULL OR nReservado!=-1)");

        m_szCondicion += " (proyectosi.priorizacion>=0 AND proyectosi.vigencia='"+m_pUsuario.GetVigenciaActual() + "' ) ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("proyectosi.eterritorial");

        if(m_CBEstado.SelectedIndex>0)
            m_szCondicionEx+=" AND proyectosi.estado="+ m_CBEstado.SelectedValue;
        if( m_CBProyecto.SelectedIndex>0)
            m_szCondicionEx+= " AND proyectosi.id=" + m_CBProyecto.SelectedValue;
        if (m_CBConcepto.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.conceptomen=" + m_CBConcepto.SelectedValue;
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.eterritorial=" + m_CBEnte.SelectedValue;
        
        string grupo= " GROUP BY ";
        for (int i = 0; i < 10; i++)
            grupo += m_saCamposAlias[i] + ",";
        grupo += m_saCamposAlias[12];
        for( int i=15; i<m_saCamposAlias.Length; i++)
            grupo += "," + m_saCamposAlias[i];

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx + grupo);
        m_szCondicion += grupo;
        m_baHidden[0] = true;

        m_nPermisos &= ~16;
        m_nPermisos &= ~4;
        m_szMensaje = "No se encontraron proyectos";

        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBProyecto.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBEnte, row[3].ToString());
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectosi WHERE priorizacion>=0 AND eterritorial=" + m_CBEnte.SelectedValue
            + " AND proyectosi.vigencia='" + m_pUsuario.GetVigenciaActual() + "'", true);
        SelectInCombo(m_CBProyecto, row[2].ToString());
        SelectInCombo(m_CBEstado, row[15].ToString());
        SelectInCombo(m_CBConcepto, row[16].ToString());
        m_EObservaciones.Text= row[17].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_EObservaciones.Text = "";
        m_CBProyecto.SelectedIndex = 0;
        m_CBEnte.SelectedIndex = 0;
        m_CBEstado.SelectedIndex = 0;
        m_CBConcepto.SelectedIndex = 0;

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = "";

        m_saCampos[0] = "estado";
        m_saDatos[0] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saCampos[1] = "conceptomen";
        m_saDatos[1] = GetSeleccion(m_CBConcepto.SelectedValue);
        m_saCampos[2] = "observacionesa";
        m_saDatos[2] = GetString(m_EObservaciones.Text);

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Proyecto " + m_CBProyecto.Text + " Actualizado");
        }
    }

    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBProyecto.UpdateAfterCallBack = true;
        m_CBEstado_SelectedIndexChanged(sender, e);
    }
    protected void m_CBProyecto_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBEstado_SelectedIndexChanged(sender, e);
        if (m_CBProyecto.SelectedIndex > 0)
        {
            m_LCResultados.SelectedIndex = 0;
            m_LCResultados_SelectedIndexChanged(m_LCResultados, e);
        }
        else if (m_LCResultados.SelectedIndex >= 0)
        {
            m_BCancelar_Click(sender, e);
            m_LCResultados.SelectedIndex = -1;
        }
    }
    protected void m_CBEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}