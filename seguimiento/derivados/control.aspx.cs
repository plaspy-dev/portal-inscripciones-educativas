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

public partial class seguimiento_derivados_control : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("derivados", 12))
            return;
        m_saCampos[0] = "id";
        m_saCampos[1] = "codigo";
        m_saCampos[2] = "eterritorial";
        m_saCampos[3] = "proyecto";
        m_saCampos[4] = "aprobacion";
        m_saCampos[5] = "plazo";
        m_saCampos[6] = "estado";
        m_saCampos[7] = "";
        m_saCampos[8] = "valor";
        m_saCampos[9] = "rpe";
        m_saCampos[10] = "rpm";
        m_saCampos[11] = "observaciones";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Número";
        m_saTitulos[2] = "Entidad territorial";
        m_saTitulos[3] = "Proyecto";
        m_saTitulos[4] = "Fecha aprobación";
        m_saTitulos[5] = "Plazo"; 
        m_saTitulos[6] = "Estado";
        m_saTitulos[7] = "Fecha vencimiento";
        m_saTitulos[8] = "Valor";
        m_saTitulos[9] = "Registro Presupuestal Entidad";
        m_saTitulos[10] = "Registro Presupuestal Municipio";
        m_saTitulos[11] = "Observaciones";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[2] = "eterritoriales.nombre";
        m_saCamposAlias[3] = "proyectos.codigo";
        m_saCamposAlias[6] = "varios.nombre";
        m_saCamposAlias[7] = "DATEADD(dd,derivados.plazo,aprobacion)";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=derivados.eterritorial) "
            + " LEFT JOIN varios ON (varios.id=derivados.estado)"
            +" LEFT JOIN proyectos ON (proyectos.id=derivados.proyecto)";

        m_szCondicion = " derivados.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("derivados.eterritorial");
        // autofiltro
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND derivados.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBProyecto.SelectedIndex > 0)
            m_szCondicionEx += " AND derivados.proyecto=" + m_CBProyecto.SelectedValue;
        if (m_CBEstado.SelectedIndex > 0)
            m_szCondicionEx += " AND derivados.estado=" + m_CBEstado.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBEstado, "SELECT id,nombre FROM varios WHERE tipo=59 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBProyecto, "SELECT id,codigo FROM proyectos WHERE vigencia='"+m_pUsuario.GetVigenciaActual() + "' AND eterritorial=" + m_CBEnte.SelectedValue, true);
        m_szMensaje = "No se encontraron convenios";

        m_EPlazo.Attributes["onKeypress"] = "isnb();";
        m_EValor.Attributes["onKeypress"] = "isfloat(this);";
        m_EValor.Attributes["onblur"] = "tcomas(this);";
        m_EValor.Attributes["onfocus"] = "scomas(this);";

        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBProyecto.UpdateAfterCallBack = true;
        m_ENumero.UpdateAfterCallBack = true;
        m_EFechaAprobacion.UpdateAfterCallBack = true;
        m_EPlazo.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_SFechaV.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_ERPM.UpdateAfterCallBack = true;
        m_ERPE.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();

        m_ENumero.Text = row[1].ToString();
        SelectInCombo(m_CBEnte, row[2].ToString());
        SelectInCombo(m_CBProyecto, row[3].ToString());
        m_EFechaAprobacion.Text= GetFechaCorta(row[4].ToString());
        m_EPlazo.Text=row[5].ToString();
        SelectInCombo(m_CBEstado, row[6].ToString());        
        m_SFechaV.Text= GetFechaCorta(row[7].ToString());
        m_EValor.Text= GetFormatDouble(row[8].ToString());
        m_ERPE.Text=row[9].ToString();
        m_ERPM.Text=row[10].ToString();
        m_EObservaciones.Text=row[11].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_CBEnte.SelectedIndex = 0;
        m_CBProyecto.SelectedIndex = 0;
        m_ENumero.Text = "";
        m_EFechaAprobacion.Text = "";
        m_EPlazo.Text = "";
        m_CBEstado.SelectedIndex = 0;
        m_SFechaV.Text = "";
        m_EValor.Text = "";
        m_ERPM.Text = "";
        m_ERPE.Text = "";
        m_EObservaciones.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";
        m_saCampos[7] = "vigencia";

        m_saDatos[1] = m_ENumero.Text;
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saDatos[3] = m_CBProyecto.SelectedValue;
        m_saDatos[4] = GetDate(m_EFechaAprobacion.Text);
        m_saDatos[5] = m_EPlazo.Text;
        m_saDatos[6] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[7] = m_pUsuario.GetVigenciaActual();
        m_saDatos[8] = GetDouble(m_EValor.Text);
        m_saDatos[9] = GetString(m_ERPE.Text);
        m_saDatos[10] = GetString(m_ERPM.Text);
        m_saDatos[11] = GetString(m_EObservaciones.Text);

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Convenio " + m_ENumero.Text + " Agregado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[1] = m_ENumero.Text;
        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saDatos[3] = m_CBProyecto.SelectedValue;
        m_saDatos[4] = GetDate(m_EFechaAprobacion.Text);
        m_saDatos[5] = m_EPlazo.Text;
        m_saDatos[6] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[8] = GetDouble(m_EValor.Text);
        m_saDatos[9] = GetString(m_ERPE.Text);
        m_saDatos[10] = GetString(m_ERPM.Text);
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
            base.AfterUpdate("Convenio " + m_ENumero.Text + " Editado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM derivados WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Convenio " + m_ENumero.Text + " Eliminado");
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
    }
    protected void m_CBEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}