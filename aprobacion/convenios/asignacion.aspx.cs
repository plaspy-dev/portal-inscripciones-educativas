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

public partial class aprobacion_convenios_asignacion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("asignacionpresupuestal", 12))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "grupo";
        m_saCampos[2]="eterritorial";
        m_saCampos[3]="partida";
        m_saCampos[4]="fecha";
        m_saCampos[5]="partidaf";
        m_saCampos[6]="fechaf";
        m_saCampos[11] = "observaciones";

        int anoactual = Convert.ToInt32(m_pUsuario.GetVigenciaActual());

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2]="Entidad Territorial";
        m_saTitulos[3]="Partida Preliminar";
        m_saTitulos[4]="Fecha";
        m_saTitulos[5]="Partida Definitiva";
        m_saTitulos[6]="Fecha d";
        m_saTitulos[7]="Fecha Certificado programa de apoyo";
        m_saTitulos[8]=anoactual.ToString();
        m_saTitulos[9]=(anoactual-1).ToString();
        m_saTitulos[10] = "Variación";
        m_saTitulos[11] = "Observaciones";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[1] = "varios.nombre";
        m_saCamposAlias[2] = "eterritoriales.nombre";
        m_saCamposAlias[7] = "(SELECT MAX(fecha) FROM aportes WHERE aportes.eterritorial=asignacionpresupuestal.eterritorial )";
        m_saCamposAlias[8] = "SUM(ampliacionmatricula.actual)";
        m_saCamposAlias[9] = "SUM(old.actual)";
        m_saCamposAlias[10] = "SUM(old.actual)-SUM(ampliacionmatricula.actual)";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN varios ON (varios.id=asignacionpresupuestal.grupo) "
            + "LEFT JOIN eterritoriales ON (eterritoriales.codigo=asignacionpresupuestal.eterritorial) "
            + " LEFT JOIN ampliacionmatricula ON (ampliacionmatricula.eterritorial=asignacionpresupuestal.eterritorial AND ampliacionmatricula.vigencia=" + anoactual.ToString() + ") "
            + " LEFT JOIN ampliacionmatricula AS old ON (old.eterritorial=asignacionpresupuestal.eterritorial AND old.vigencia=" +(anoactual-1).ToString() + ") "
            ;

        m_szCondicion = " asignacionpresupuestal.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        // autofiltro
        if (m_CBGrupo.SelectedIndex > 0)
            m_szCondicionEx += " AND asignacionpresupuestal.grupo=" + m_CBGrupo.SelectedValue;
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND asignacionpresupuestal.eterritorial=" + m_CBEnte.SelectedValue;
        else m_szCondicionEx += " AND " + m_pUsuario.GetCondicion("asignacionpresupuestal.eterritorial");

        string grupo = " GROUP BY ";
        for (int i = 0; i < 7; i++)
            grupo += m_saCamposAlias[i] + ",";
        grupo += m_saCamposAlias[11];
        grupo += ",asignacionpresupuestal.eterritorial";

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx + grupo);

        m_szCondicion += grupo;
        m_baHidden[0] = true;
        m_baHidden[6] = true;

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBGrupo, "SELECT * FROM varios WHERE tipo=5 AND (nReservado IS NULL OR nReservado!=-1)");

        m_EPartida.Attributes["onKeypress"] = "isfloat(this);";
        m_EPartida.Attributes["onblur"] = "tcomas(this);";
        m_EPartida.Attributes["onfocus"] = "scomas(this);";

        m_EPartidaDefinitiva.Attributes["onKeypress"] = "isfloat(this);";
        m_EPartidaDefinitiva.Attributes["onblur"] = "tcomas(this);";
        m_EPartidaDefinitiva.Attributes["onfocus"] = "scomas(this);";

        if (!IsPostBack)
        {
            CResumenAsignacion temp = new CResumenAsignacion();
            temp.RebuilTable();
        }

        m_szMensaje = "No se encontraron convenios";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBGrupo.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_EPartida.UpdateAfterCallBack = true;
        m_EFecha.UpdateAfterCallBack = true;
        m_EPartidaDefinitiva.UpdateAfterCallBack = true;
        m_EFechaDefinitiva.UpdateAfterCallBack = true;
        m_CCertificado.UpdateAfterCallBack = true;
        m_SFechaCertificado.UpdateAfterCallBack = true;
        m_EActual.UpdateAfterCallBack = true;
        m_SAnterior.UpdateAfterCallBack = true;
        m_SVariacion.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
    }

    
    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBGrupo, row[1].ToString());
        SelectInCombo(m_CBEnte, row[2].ToString());
        m_EPartida.Text=GetFormatDouble(row[3].ToString());
        m_EFecha.Text = GetFechaCorta(row[4].ToString());
        m_EPartidaDefinitiva.Text = GetFormatDouble(row[5].ToString());
        m_EFechaDefinitiva.Text = GetFechaCorta(row[6].ToString());
        m_CCertificado.Checked = row[7].ToString() != "";
        m_SFechaCertificado.Text = GetFechaCorta(row[7].ToString());
        m_EActual.Text = row[8].ToString();
        m_SAnterior.Text = row[9].ToString();
        m_SVariacion.Text = row[10].ToString();
        m_EObservaciones.Text = row[11].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value="";
        m_EPartida.Text="";
        m_EFecha.Text="";
        m_EPartidaDefinitiva.Text="";
        m_EFechaDefinitiva.Text="";
        m_CCertificado.Text="";
        m_SFechaCertificado.Text="";
        m_EActual.Text="";
        m_SAnterior.Text="";
        m_SVariacion.Text="";
        m_EObservaciones.Text="";
        m_CBGrupo.SelectedIndex = 0;
        m_CBEnte.SelectedIndex = 0;

        CResumenAsignacion temp = new CResumenAsignacion();
        temp.RebuilTable();
        m_DLResumen.DataBind();
        m_DLResumen.UpdateAfterCallBack = true;

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        m_saDatos[1]=m_CBGrupo.SelectedValue;
        m_saDatos[2]=m_CBEnte.SelectedValue;
        m_saDatos[3]=GetDouble(m_EPartida.Text);
        m_saDatos[4]=m_EFecha.Text;
        m_saDatos[5]=GetDouble(m_EPartidaDefinitiva.Text);
        m_saDatos[6]=m_EFechaDefinitiva.Text;
        m_saCampos[7]="vigencia";
        m_saDatos[7]=m_pUsuario.GetVigenciaActual();
        m_saCampos[8]="";
        m_saCampos[9]="";
        m_saCampos[10]="";
        m_saDatos[11]=GetString(m_EObservaciones.Text);

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Asignacion de " +  m_CBEnte.SelectedItem.Text + " Ingresada");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saDatos[1] = m_CBGrupo.SelectedValue;
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saDatos[3] = GetDouble(m_EPartida.Text);
        m_saDatos[4] = m_EFecha.Text;
        m_saDatos[5] = GetDouble(m_EPartidaDefinitiva.Text);
        m_saDatos[6] = m_EFechaDefinitiva.Text;
        m_saCampos[7] = "";
        m_saCampos[8] = "";
        m_saCampos[9] = "";
        m_saCampos[10] = "";
        m_saDatos[11] = GetString(m_EObservaciones.Text);

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Asignacion de " + m_CBEnte.SelectedItem.Text + " Editada");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM " +m_szTabla +" WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Asignacion de " + m_CBEnte.SelectedItem.Text + " Eliminada");
        }
    }
    protected void m_CBGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBEnte_SelectedIndexChanged(sender, e);
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}