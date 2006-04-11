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

public partial class aprobacion_convenios_amplacion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("ampliacionmatricula", 9))
            return;

        int ano = Convert.ToInt32(m_pUsuario.GetVigenciaActual());

        m_saCampos[0] = "id";
        m_saCampos[1] = "grupo";
        m_saCampos[2] = "eterritorial";
        m_saCampos[3] = "convenio";
        m_saCampos[4] = "actual";
        m_saCampos[5] = "anterior";
        m_saCampos[6] = "variacion";
        m_saCampos[7] = "concepto";
        m_saCampos[8] = "observaciones";

        m_saTitulos[0] = "id";
        m_saTitulos[1]="Grupo";
        m_saTitulos[2]="Entidad Territorial";
        m_saTitulos[3]="No. de convenio";
        m_saTitulos[4]=ano.ToString();
        m_saTitulos[5]=(ano-1).ToString();
        m_saTitulos[6]="Variación";
        m_saTitulos[7]="Concepto Entidad educativa";
        m_saTitulos[8]="Observaciones";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[1]="varios.nombre";
        m_saCamposAlias[2]="eterritoriales.nombre";
        m_saCamposAlias[3]="conveniosm.codigo";
        m_saCamposAlias[5]="misma.actual";
        m_saCamposAlias[6]="misma.actual-ampliacionmatricula.actual";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN conveniosm ON (conveniosm.eterritorial=ampliacionmatricula.eterritorial) "
            + " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=ampliacionmatricula.eterritorial) "
            + " LEFT JOIN varios ON (varios.id=asignacionpresupuestal.grupo) "
            + " LEFT JOIN varios AS tipo ON (tipo.id=conveniosm.tipo) "
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=ampliacionmatricula.eterritorial) "
            + " LEFT JOIN ampliacionmatricula AS misma ON (misma.eterritorial=ampliacionmatricula.eterritorial AND misma.vigencia='" + (ano-1).ToString() + "') "
            ;
        // autofiltro
        m_szCondicion+=" tipo.nombre LIKE '%marco%'";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("ampliacionmatricula.eterritorial");

        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx+= " AND ampliacionmatricula.eterritorial=" + m_CBEnte.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;

        m_pUsuario.FillComboBox(m_CBEnte);
        m_EActual.Attributes["onKeypress"] = "isnb();";
        m_szMensaje = "No se encontraron convenios";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_EActual.UpdateAfterCallBack = true;
        m_SAnterior.UpdateAfterCallBack = true;
        m_SVariacion.UpdateAfterCallBack = true;
        m_EConcepto.UpdateAfterCallBack = true;
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
        m_EActual.Text= row[4].ToString();
        m_SAnterior.Text = row[5].ToString();
        m_SVariacion.Text = row[6].ToString();
        m_EConcepto.Text = row[7].ToString();
        m_EObservaciones.Text= row[8].ToString();
        
        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_EActual.Text = "";
        m_SAnterior.Text = "";
        m_SVariacion.Text = "";
        m_EObservaciones.Text = "";
        m_EConcepto.Text = "";
        m_CBEnte.SelectedIndex = 0;
        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";
        m_saCampos[1] = "vigencia";
        m_saDatos[1] = m_pUsuario.GetVigenciaActual();
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saCampos[3] = "";
        m_saDatos[4] = m_EActual.Text;
        m_saCampos[5] = "";
        m_saCampos[6] = "";
        m_saDatos[7] = m_EConcepto.Text;
        m_saDatos[8] = m_EObservaciones.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Ampliación " + m_CBEnte.Text + " Insertada");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        // m_saDatos[1] = m_ENombre.Text;
        m_saCampos[1] = "";
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saCampos[3] = "";
        m_saDatos[4] = m_EActual.Text;
        m_saCampos[5] = "";
        m_saCampos[6] = "";
        m_saDatos[7] = m_EConcepto.Text;
        m_saDatos[8] = m_EObservaciones.Text;
        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Ampliación " + m_CBEnte.Text + " Editada");
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
            base.AfterUpdate("Ampliación " + m_CBEnte.Text + " Eliminada");
        }
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}