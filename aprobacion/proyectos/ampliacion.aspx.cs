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

public partial class aprobacion_proyectos_ampliacion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectosi", 11))
            return;
        m_saCampos[0] = "id";

        int ano = Convert.ToInt32(m_pUsuario.GetVigenciaActual());
        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Código del proyecto";
        m_saTitulos[3] = "Entidad Territorial";
        m_saTitulos[4] = "Institución Educativa";
        m_saTitulos[5] = ano.ToString();
        m_saTitulos[6] = (ano - 1).ToString();
        m_saTitulos[7] = "Variación";
        m_saTitulos[8] = "Concepto Entidad educativa";
        m_saTitulos[9] = "Observaciones";
        m_saTitulos[10] = "idproyecto";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[0]="institucionesvariacion.id";
        m_saCamposAlias[1]="grupo.nombre";
        m_saCamposAlias[2]="proyectosi.codigo";
        m_saCamposAlias[3]="eterritoriales.nombre";
        m_saCamposAlias[4]="insteducativas.nombre";
        m_saCamposAlias[5] = "institucionesvariacion.alumnos";
        m_saCamposAlias[6]="old.alumnos";
        m_saCamposAlias[7] = "old.alumnos-institucionesvariacion.alumnos";
        m_saCamposAlias[8]="concepto.nombre";
        m_saCamposAlias[9] = "institucionesvariacion.observaciones";
        m_saCamposAlias[10]="proyectosi.id";

        m_szIndice = m_saTitulos[10];
        m_szJoin = " LEFT JOIN insteducativas ON (insteducativas.id=proyectosi.institucion) "
            +" LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=proyectosi.eterritorial) "
            +" LEFT JOIN varios AS grupo ON (grupo.id=asignacionpresupuestal.grupo) "
            +" LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectosi.eterritorial) "
            +" LEFT JOIN institucionesvariacion ON (institucionesvariacion.institucion=proyectosi.institucion AND institucionesvariacion.anolectivo=" + ano.ToString() + ") "
            +" LEFT JOIN institucionesvariacion AS old ON (old.institucion=proyectosi.institucion AND old.anolectivo=" +(ano-1).ToString() + ") "
            + " LEFT JOIN varios AS concepto ON (concepto.id=proyectosi.conceptomen) "
            ;
        m_szCondicion = " (proyectosi.priorizacion>0 AND proyectosi.vigencia='" + m_pUsuario.GetVigenciaActual() +"') ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("proyectosi.eterritorial");
        // autofiltro
        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBConcepto, "SELECT id,nombre FROM varios WHERE tipo=49 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectosi WHERE priorizacion>0 AND eterritorial=" + m_CBEnte.SelectedValue
            + " AND proyectosi.vigencia='" + m_pUsuario.GetVigenciaActual() + "'", true);
        FillComboBox(m_CBInstitucion, "SELECT insteducativas.id,insteducativas.nombre FROM proyectosi "
        + "LEFT JOIN insteducativas ON (insteducativas.id=proyectosi.institucion) WHERE proyectosi.id=" + m_CBProyecto.SelectedValue, true);

        if (m_CBConcepto.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.conceptomen=" + m_CBConcepto.SelectedValue;
        if(m_CBProyecto.SelectedIndex>0)
            m_szCondicionEx +=" AND proyectosi.id=" + m_CBProyecto.SelectedValue;
        if(m_CBInstitucion.SelectedIndex>0)
            m_szCondicionEx+=" AND proyectosi.institucion=" + m_CBInstitucion.SelectedValue;
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.eterritorial=" + m_CBEnte.SelectedValue;
        

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;
        m_baHidden[10] = true;

        m_EActual.Attributes["onKeypress"] = "isnb();";
        m_szMensaje = "No se encontraron proyectos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBProyecto.UpdateAfterCallBack = true;
        m_CBInstitucion.UpdateAfterCallBack = true;
        m_EActual.UpdateAfterCallBack = true;
        m_SAnterior.UpdateAfterCallBack = true;
        m_SVariacion.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
        m_nIdProyecto.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = row[0].ToString();
        m_nIdProyecto.Value= row[10].ToString();
        SelectInCombo(m_CBEnte,row[3].ToString());
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectosi WHERE priorizacion>0 AND eterritorial=" + m_CBEnte.SelectedValue
            + " AND proyectosi.vigencia='" + m_pUsuario.GetVigenciaActual() + "'", true);
        SelectInCombo(m_CBProyecto, row[2].ToString());
        FillComboBox(m_CBInstitucion, "SELECT insteducativas.id,insteducativas.nombre FROM proyectosi "
        + "LEFT JOIN insteducativas ON (insteducativas.id=proyectosi.institucion) WHERE proyectosi.id=" + m_CBProyecto.SelectedValue, true);
        SelectInCombo(m_CBInstitucion, row[4].ToString());
        m_EActual.Text= row[5].ToString();
        m_SAnterior.Text = row[6].ToString();
        m_SVariacion.Text = row[7].ToString();
        
        SelectInCombo( m_CBConcepto, row[8].ToString());
        m_EObservaciones.Text= row[9].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_nIdProyecto.Value = "";
        m_EActual.Text = "";
        m_SAnterior.Text = "";
        m_SVariacion.Text = "";
        m_EObservaciones.Text = "";
        m_CBInstitucion.SelectedIndex = 0;
        m_CBConcepto.SelectedIndex = 0;
        m_CBProyecto.SelectedIndex = 0;
        m_CBEnte.SelectedIndex = 0;


        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = "";

        m_saCampos[0] = "institucion";
        m_saDatos[0] = GetSeleccion(m_CBInstitucion.SelectedValue);
        m_saCampos[1] = "anolectivo";
        m_saDatos[1] = m_pUsuario.GetVigenciaActual();
        m_saCampos[2] = "alumnos";
        m_saDatos[2] = m_EActual.Text;
        m_saCampos[3] = "observaciones";
        m_saDatos[3] = GetString(m_EObservaciones.Text);

        // editar
        string query = "";
        if( m_nId.Value.Length>0)
            query= CConector.CreateUpdate("institucionesvariacion", m_saCampos, m_saDatos) + CConector.GetCondicion("id", m_nId.Value);
        else query= CConector.CrearInsercion("institucionesvariacion", m_saCampos, m_saDatos);
        if (query.Length>0 && m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            if (m_pConector.QueryMod(CConector.CreateUpdate(m_szTabla, "conceptomen", GetSeleccion(m_CBConcepto.SelectedValue)) + CConector.GetCondicion("id", m_nIdProyecto.Value)) == -1)
            {
                m_SMensaje.Text = m_pConector.GetLastError();
                m_SMensaje.UpdateAfterCallBack = true;
            }
            base.AfterUpdate("Ampliación " + m_CBInstitucion.Text + " Actualizada");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_nId.Value.Length>0 && m_pConector.QueryMod("DELETE FROM institucionesvariacion WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Ampliación " + m_CBInstitucion.Text + " Eliminada");
        }
    }
    protected void m_CBConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBProyecto.UpdateAfterCallBack = true;
        m_CBConcepto_SelectedIndexChanged(sender, e);
    }
    protected void m_CBProyecto_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBInstitucion.UpdateAfterCallBack = true;
        m_CBConcepto_SelectedIndexChanged(sender, e);
    }
    protected void m_CBInstitucion_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBConcepto_SelectedIndexChanged(sender, e);
        if (m_CBInstitucion.SelectedIndex > 0)
        {
            m_LCResultados.SelectedIndex = 0;
            m_LCResultados_SelectedIndexChanged(m_LCResultados, e);
        }
        else if (m_CBInstitucion.SelectedIndex >= 0)
        {
            m_BCancelar_Click(sender, e);
            m_LCResultados.SelectedIndex = -1;
        }
    }
}