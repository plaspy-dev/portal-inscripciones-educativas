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

public partial class aprobacion_convenios_aportes : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("aportes", 11))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1]="grupo";
        m_saCampos[2]="eterritorial";
        m_saCampos[3]="convenio";
        m_saCampos[4]="mes";
        m_saCampos[5]="origen";
        m_saCampos[6]="fecha";
        m_saCampos[7]="valor";
        m_saCampos[8]="estado";
        m_saCampos[9]="observaciones";
        m_saCampos[10]="certificado";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Entidad Territorial";
        m_saTitulos[3] = "No. de convenio";
        m_saTitulos[4] = "Mes aporte";
        m_saTitulos[5] = "Origen";
        m_saTitulos[6] = "Fecha";
        m_saTitulos[7] = "Valor";
        m_saTitulos[8] = "Estado";
        m_saTitulos[9] = "Observaciones";
        m_saTitulos[10]="certificado";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        // m_saCamposAlias[4] = "menuitem.nombre";
        m_saCamposAlias[1]="varios.nombre";
        m_saCamposAlias[2]="eterritoriales.nombre";
        m_saCamposAlias[3]="conveniosm.codigo";
        m_saCamposAlias[4]="DATEPART(mm,aportes.fecha)";
        m_saCamposAlias[5]="varios1.nombre";
        m_saCamposAlias[8] = "estados.nombre";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN conveniosm ON (conveniosm.eterritorial=aportes.eterritorial) "
            + " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=aportes.eterritorial)  AND asignacionpresupuestal.vigencia='" + m_pUsuario.GetVigenciaActual() + "'"
            + " LEFT JOIN varios ON (varios.id=asignacionpresupuestal.grupo) "
            + " LEFT JOIN varios AS tipo ON (tipo.id=conveniosm.tipo) "
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=aportes.eterritorial) "
            + " LEFT JOIN varios AS varios1 ON (varios1.id=aportes.origen) "
            + " LEFT JOIN varios AS estados ON (estados.id=aportes.estado) "
            ;
        m_szCondicion += " tipo.nombre LIKE '%marco%'";
        m_szCondicion += " AND conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() +"'";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("aportes.eterritorial");

        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND aportes.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBOrigen.SelectedIndex > 0)
            m_szCondicionEx += " AND aportes.origen=" + m_CBOrigen.SelectedValue;
        if( m_CBEstado.SelectedIndex>0)
            m_szCondicionEx += " AND aportes.estado=" + m_CBOrigen.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;
        m_baHidden[10] = true;

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBOrigen, "SELECT id,nombre FROM varios WHERE tipo=47 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBEstado, "SELECT id,nombre FROM varios WHERE tipo=58 AND (nReservado IS NULL OR nReservado!=-1)");

        m_EValor.Attributes["onKeypress"] = "isfloat(this);";
        m_EValor.Attributes["onblur"] = "tcomas(this);";
        m_EValor.Attributes["onfocus"] = "scomas(this);";
        m_szMensaje = "No se encontraron convenios";

        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CCertificado.UpdateAfterCallBack = true;
        m_CBOrigen.UpdateAfterCallBack = true;
        m_EFecha.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBEnte, row[2].ToString());
        SelectInCombo(m_CBOrigen, row[5].ToString());
        m_EFecha.Text= GetFechaCorta(row[6].ToString());      
        m_EValor.Text= GetFormatDouble(row[7].ToString());
        SelectInCombo(m_CBEstado,row[8].ToString());
        m_EObservaciones.Text= row[9].ToString();
        m_CCertificado.Checked = row[10].ToString() == "True";

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_EFecha.Text = "";
        m_EValor.Text = "";
        m_CBEstado.SelectedIndex = 0;
        m_EObservaciones.Text = "";
        m_CBOrigen.SelectedIndex = 0;
        m_CBEnte.SelectedIndex = 0;

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";
        m_saCampos[1] = "";
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saCampos[3] = "";
        m_saCampos[4] = "";
        m_saDatos[5] = m_CBOrigen.SelectedValue;
        m_saDatos[6] = GetFechaCorta(m_EFecha.Text);
        m_saDatos[7] = GetDouble(m_EValor.Text);
        m_saDatos[8] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[9] = GetString(m_EObservaciones.Text);
        m_saDatos[10] = m_CCertificado.Checked ? "1" : "0";

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Certificado " + m_CBEnte.Text + " Ingresado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saCampos[1] = "";
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saCampos[3] = "";
        m_saCampos[4] = "";
        m_saDatos[5] = m_CBOrigen.SelectedValue;
        m_saDatos[6] = GetFechaCorta(m_EFecha.Text);
        m_saDatos[7] = GetDouble(m_EValor.Text);
        m_saDatos[8] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[9] = GetString(m_EObservaciones.Text);
        m_saDatos[10] = m_CCertificado.Checked ? "1" : "0";

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Certificado " + m_CBEnte.Text + " Editado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM " + m_szTabla + " WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Certificado " + m_CBEnte.Text + " Eliminado");
        }
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBOrigen_SelectedIndexChanged(sender, e);
    }
    protected void m_CBOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}