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

public partial class seguimiento_proyectos_proyecto : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectos", 16))
            return;

        m_saCampos[0] = "id";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Código de proyecto";
        m_saTitulos[3] = "Entidad territorial";
        m_saTitulos[4] = "Departamento";
        m_saTitulos[5] = "Municipio";
        m_saTitulos[6] = "Institución educativa - sede";
        m_saTitulos[7] = "Tipo";
        m_saTitulos[8] = "Objeto";
        m_saTitulos[9] = "Valor total proyecto";
        m_saTitulos[10] = "Valor aportes Entidad educativa";
        m_saTitulos[11] = "% aportes Entidad educativa";
        m_saTitulos[12] = "Valor aportes cofinanciación";
        m_saTitulos[13] = "% aportes cofinanciación";
        m_saTitulos[14] = "Observaciones";
        m_saTitulos[15] = "fechadeaprobacion";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1] = "grupos.nombre";
        m_saCamposAlias[2] = "proyectos.codigo";
        m_saCamposAlias[3] = "eterritoriales.nombre";
        m_saCamposAlias[4] = "departamentos.nombre";
        m_saCamposAlias[5] = "municipios.nombre";
        m_saCamposAlias[6] = "insteducativas.nombre";
        m_saCamposAlias[7] = "tipos.nombre";
        m_saCamposAlias[8] = "proyectos.objeto";
        m_saCamposAlias[9] = "SUM(objeto.valor)";
        m_saCamposAlias[10] = "presupuesto.valormen";
        m_saCamposAlias[11] = "(presupuesto.valormen*100)/SUM(objeto.valor)";
        m_saCamposAlias[12] = "SUM(objeto.valor)-presupuesto.valormen";
        m_saCamposAlias[13] = "((SUM(objeto.valor)-presupuesto.valormen)*100)/SUM(objeto.valor)";
        m_saCamposAlias[14] = "proyectos.observaciones";
        m_saCamposAlias[15] = "proyectos.fechaaprobacion";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=proyectos.eterritorial) "
            + " LEFT JOIN varios AS grupos ON (grupos.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectos.eterritorial)"
            + " LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN varios AS tipos ON (tipos.id=proyectos.tipo)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion)"
            + " LEFT JOIN objeto ON (objeto.proyecto=proyectos.id)"
            + " LEFT JOIN presupuesto ON (presupuesto.proyecto=proyectos.id)"
            ;
        m_szCondicion = " proyectos.vigencia='" +m_pUsuario.GetVigenciaActual() + "' ";
        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_LCProyecto, "SELECT id,codigo FROM proyectos WHERE vigencia='" + m_pUsuario.GetVigenciaActual()
            + "' AND eterritorial=" + m_CBEnte.SelectedValue, true);
        if (m_LCProyecto.SelectedIndex > 0)
            m_szCondicion = " AND proyectos.id=" + m_LCProyecto.SelectedValue;
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicion += " AND proyectos.eterritorial=" + m_CBEnte.SelectedValue;
        else m_szCondicion += " AND " + m_pUsuario.GetCondicion("proyectos.eterritorial");

        m_szCondicion += " GROUP BY ";
        for (int i = 0; i < 9; i++)
        {
            m_szCondicion += m_saCamposAlias[i] + ",";
        }
        m_szCondicion += m_saCamposAlias[10] + "," + m_saCamposAlias[14] + "," + m_saCamposAlias[15];

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        m_baHidden[0] = true;
        m_baHidden[15] = true;

        // pestaña 2
        m_EValorContratado.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorContratado.Attributes["onblur"] = "tcomas(this);";
        m_EValorContratado.Attributes["onfocus"] = "scomas(this);";
        m_EPlazoContrato.Attributes["onKeypress"] = "isnb();";
        FillComboBox(m_CBClaseContrato, "SELECT * FROM varios WHERE tipo=23 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBEstadoContrato, "SELECT * FROM varios WHERE tipo=32 AND (nReservado IS NULL OR nReservado!=-1)");

        if (!IsPostBack)
        {
            m_BEliminarContrato.Style["display"] = "none";
            m_BAceptarContrato.Style["display"] = "none";
            m_BCancelarContrato.Style["display"] = "none";
            m_BInsertarContrato.Style["display"] = "none";

            BoundField data = new BoundField();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LCContratos.Columns.Add(data);

            data = new BoundField();
            data.DataField = "codigo";
            data.HeaderText = "Número de contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "fechas";
            data.HeaderText = "Fecha de suscricpción";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "fechainicio";
            data.HeaderText = "Fecha de inicio";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "clase";
            data.HeaderText = "Clase";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "objeto";
            data.HeaderText = "Objeto";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "plazo";
            data.HeaderText = "Plazo";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "valor";
            data.HeaderText = "Valor";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "estado";
            data.HeaderText = "Estado";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "observaciones";
            data.HeaderText = "Observaciones";
            m_LCContratos.Columns.Add(data);
        }
        // pestaña 3
        m_ENoSuspencion.Attributes["onKeypress"] = "isnb();";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_STitulo.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_LCProyecto.UpdateAfterCallBack = true;
        m_SInstitucion.UpdateAfterCallBack = true;
        m_SFechaAprobacion.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
        // pestaña 2
        m_EContrato.UpdateAfterCallBack = true;
        m_EFechaS.UpdateAfterCallBack = true;
        m_EActaInicio.UpdateAfterCallBack = true;
        m_CBClaseContrato.UpdateAfterCallBack = true;
        m_EObjetoContrato.UpdateAfterCallBack = true;
        m_EPlazoContrato.UpdateAfterCallBack = true;
        m_EValorContratado.UpdateAfterCallBack = true;
        m_CBEstadoContrato.UpdateAfterCallBack = true;
        m_EObservacionesContrato.UpdateAfterCallBack = true;
        m_nIdContrato.UpdateAfterCallBack = true;
        m_LCContratos.UpdateAfterCallBack = true;
        // pestaña 3
        m_SContratoModificaciones.UpdateAfterCallBack = true;
        m_SNoAdicion.UpdateAfterCallBack = true;
        m_EFechaAdicion.UpdateAfterCallBack = true;
        m_ENoSuspencion.UpdateAfterCallBack = true;
        m_EFechaSuspencion.UpdateAfterCallBack = true;
        m_EFechaReinicio.UpdateAfterCallBack = true;
        m_EObservacionesModificacion.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_CBEnte.Visible=show;
        m_LCProyecto.Visible = show;
        m_SInstitucion.Visible = show;
        m_SFechaAprobacion.Visible = show;
        m_EObservaciones.Visible = show;
        // pestaña 2
        m_EContrato.Visible = show;
        m_EFechaS.Visible = show;
        m_EActaInicio.Visible = show;
        m_CBClaseContrato.Visible = show;
        m_EObjetoContrato.Visible = show;
        m_EPlazoContrato.Visible = show;
        m_EValorContratado.Visible = show;
        m_CBEstadoContrato.Visible = show;
        m_EObservacionesContrato.Visible = show;
        m_LCContratos.Visible = show;
        // pestaña 3
        m_SContratoModificaciones.Visible = show;
        m_SNoAdicion.Visible = show;
        m_EFechaAdicion.Visible = show;
        m_ENoSuspencion.Visible = show;
        m_EFechaSuspencion.Visible = show;
        m_EFechaReinicio.Visible = show;
        m_EObservacionesModificacion.Visible = show;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBEnte, row[3].ToString());
        FillComboBox(m_LCProyecto, "SELECT id,codigo FROM proyectos WHERE vigencia='" + m_pUsuario.GetVigenciaActual()
            + "' AND eterritorial=" + m_CBEnte.SelectedValue, true);
        SelectInCombo(m_LCProyecto, row[2].ToString());
        m_SInstitucion.Text = row[6].ToString();
        m_SFechaAprobacion.Text = GetFechaCorta(row[15].ToString());
        m_EObservaciones.Text= row[14].ToString();
        m_STitulo.Text = "Proyecto " + m_LCProyecto.SelectedItem.Text;

        // pestaña 2
        Session["idproyecto"]=m_nId.Value;
        m_nIdContrato.Value = "";
        m_LCContratos.DataBind();
        m_BEliminarContrato.Style["display"] = "none";
        m_BAceptarContrato.Style["display"] = "none";
        m_BCancelarContrato.Style["display"] = "none";
        m_BInsertarContrato.Style["display"] = "";
        m_BInsertarContrato.UpdateAfterCallBack = true;

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_LCProyecto.SelectedIndex=0;
        m_SInstitucion.Text = "";
        m_SFechaAprobacion.Text = "";
        m_EObservaciones.Text = "";
        // pestaña 2
        m_EContrato.Text = "";
        m_EFechaS.Text = "";
        m_EActaInicio.Text = "";
        m_CBClaseContrato.SelectedIndex=0;
        m_EObjetoContrato.Text = "";
        m_EPlazoContrato.Text = "";
        m_EValorContratado.Text = "";
        m_CBEstadoContrato.SelectedIndex=0;
        m_EObservacionesContrato.Text = "";

        Session["idconvenio"] = "";
        m_nIdContrato.Value = "";
        m_LCContratos.DataBind();
        m_BEliminarContrato.Style["display"] = "none";
        m_BAceptarContrato.Style["display"] = "none";
        m_BCancelarContrato.Style["display"] = "none";
        m_BInsertarContrato.Style["display"] = "none";

        m_BInsertarContrato.UpdateAfterCallBack = true;
        // pestaña 3
        m_SContratoModificaciones.Text = "";
        m_SNoAdicion.Text = "";
        m_EFechaAdicion.Text = "";
        m_ENoSuspencion.Text = "";
        m_EFechaSuspencion.Text = "";
        m_EFechaReinicio.Text = "";
        m_EObservacionesModificacion.Text = "";
        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        // m_saDatos[1] = m_ENombre.Text;

        // editar
        //string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        //if (m_pConector.QueryMod(query) == -1)
        //{
        //    m_SMensaje.Text = m_pConector.GetLastError();
        //    m_SMensaje.UpdateAfterCallBack = true;
        //}
        //else
        //{
            base.AfterUpdate("");
        // }
    }

    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCProyecto.UpdateAfterCallBack = true;
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_LCProyecto_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
        m_LCResultados.SelectedIndex = 0;
        m_LCResultados_SelectedIndexChanged(m_LCResultados, e);
    }
    // pestaña 2
    protected void m_BInsertarContrato_Click(object sender, EventArgs e)
    {
        string[] campos = new string[10];
        string[] datos = new string[10];

        campos[0] = "proyecto";
        campos[1] = "codigo";
        campos[2] = "fechas";
        campos[3] = "fechainicio";
        campos[4] = "clase";
        campos[5] = "objeto";
        campos[6] = "plazo";
        campos[7] = "valor";
        campos[8] = "estado";
        campos[9] = "observaciones";

        datos[0] = m_nId.Value;
        datos[1] = m_EContrato.Text;
        datos[2] = m_EFechaS.Text;
        datos[3] = m_EActaInicio.Text;
        datos[4] = m_CBClaseContrato.SelectedValue;
        datos[5] = m_EObjetoContrato.Text;
        datos[6] = m_EPlazoContrato.Text;
        datos[7] = GetDouble(m_EValorContratado.Text);
        datos[8] = GetSeleccion(m_CBEstadoContrato.SelectedValue);
        datos[9] = m_EObjetoContrato.Text;

        m_pConector.QueryMod(CConector.CrearInsercion("contratos", campos, datos));
        m_BCancelarContrato_Click(sender, e);
    }
    protected void m_BAceptarContrato_Click(object sender, EventArgs e)
    {
        string[] campos = new string[10];
        string[] datos = new string[10];

        campos[0] = "proyecto";
        campos[1] = "codigo";
        campos[2] = "fechas";
        campos[3] = "fechainicio";
        campos[4] = "clase";
        campos[5] = "objeto";
        campos[6] = "plazo";
        campos[7] = "valor";
        campos[8] = "estado";
        campos[9] = "observaciones";

        datos[0] = m_nId.Value;
        datos[1] = m_EContrato.Text;
        datos[2] = m_EFechaS.Text;
        datos[3] = m_EActaInicio.Text;
        datos[4] = m_CBClaseContrato.SelectedValue;
        datos[5] = GetString(m_EObjetoContrato.Text);
        datos[6] = m_EPlazoContrato.Text;
        datos[7] = GetDouble(m_EValorContratado.Text);
        datos[8] = GetSeleccion(m_CBEstadoContrato.SelectedValue);
        datos[9] = GetString(m_EObjetoContrato.Text);

        m_pConector.QueryMod(CConector.CreateUpdate("contratos", campos, datos) + CConector.GetCondicion("id", m_nIdContrato.Value));
        m_BCancelarContrato_Click(sender, e);
    }
    protected void m_BCancelarContrato_Click(object sender, EventArgs e)
    {
        m_BEliminarContrato.Style["display"] = "none";
        m_BEliminarContrato.UpdateAfterCallBack = true;
        m_BAceptarContrato.Style["display"] = "none";
        m_BAceptarContrato.UpdateAfterCallBack = true;
        m_BCancelarContrato.Style["display"] = "none";
        m_BCancelarContrato.UpdateAfterCallBack = true;
        m_BInsertarContrato.Style["display"] = "";
        m_BInsertarContrato.UpdateAfterCallBack = true;

        m_nIdContrato.Value = "";
        m_EContrato.Text = "";
        m_EFechaS.Text = "";
        m_EActaInicio.Text = "";
        m_CBClaseContrato.SelectedIndex=0;
        m_EObjetoContrato.Text = "";
        m_EPlazoContrato.Text = "";
        m_EValorContratado.Text = "";
        m_CBEstadoContrato.SelectedIndex=0;
        m_EObservacionesContrato.Text = "";
        
        CContratos.RebuilTable();
        m_LCContratos.DataBind();
        m_LCContratos.SelectedIndex = -1;
        m_LCContratos.UpdateAfterCallBack = true;


        m_nIdContrato.UpdateAfterCallBack = true;
        m_EContrato.UpdateAfterCallBack = true;
        m_EFechaS.UpdateAfterCallBack = true;
        m_EActaInicio.UpdateAfterCallBack = true;
        m_CBClaseContrato.UpdateAfterCallBack = true;
        m_EObjetoContrato.UpdateAfterCallBack = true;
        m_EPlazoContrato.UpdateAfterCallBack = true;
        m_EValorContratado.UpdateAfterCallBack = true;
        m_CBEstadoContrato.UpdateAfterCallBack = true;
        m_EObservacionesContrato.UpdateAfterCallBack = true;


        m_STitulo.Text = "Proyecto " + m_LCProyecto.SelectedItem.Text;
        m_STitulo.UpdateAfterCallBack = true;
    }

    protected void m_BEliminarContrato_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM contratos WHERE id=" + m_nIdContrato.Value);
        m_BCancelarContrato_Click(sender, e);
    }
    protected void m_LCContratos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow row = GetDataRow(m_LCContratos, CContratos.GetTabla());
        if (row == null)
            return;

        m_nIdContrato.Value = m_LCContratos.SelectedDataKey.Value.ToString();
        m_EContrato.Text = row[1].ToString();
        m_EFechaS.Text = GetFechaCorta(row[2].ToString());
        m_EActaInicio.Text = GetFechaCorta(row[3].ToString());
        SelectInCombo(m_CBClaseContrato,row[4].ToString());
        m_EObjetoContrato.Text = row[5].ToString();
        m_EPlazoContrato.Text = row[6].ToString();
        m_EValorContratado.Text = GetFormatDouble(row[7].ToString());
        SelectInCombo(m_CBEstadoContrato,row[8].ToString());
        m_EObservacionesContrato.Text = row[9].ToString();

        m_nIdContrato.UpdateAfterCallBack = true;
        m_EContrato.UpdateAfterCallBack = true;
        m_EFechaS.UpdateAfterCallBack = true;
        m_EActaInicio.UpdateAfterCallBack = true;
        m_CBClaseContrato.UpdateAfterCallBack = true;
        m_EObjetoContrato.UpdateAfterCallBack = true;
        m_EPlazoContrato.UpdateAfterCallBack = true;
        m_EValorContratado.UpdateAfterCallBack = true;
        m_CBEstadoContrato.UpdateAfterCallBack = true;
        m_EObservacionesContrato.UpdateAfterCallBack = true;

        m_BEliminarContrato.Style["display"] = "";
        m_BEliminarContrato.UpdateAfterCallBack = true;
        m_BAceptarContrato.Style["display"] = "";
        m_BAceptarContrato.UpdateAfterCallBack = true;
        m_BCancelarContrato.Style["display"] = "";
        m_BCancelarContrato.UpdateAfterCallBack = true;
        m_BInsertarContrato.Style["display"] = "none";
        m_BInsertarContrato.UpdateAfterCallBack = true;

        m_STitulo.Text = "Proyecto " + m_LCProyecto.SelectedItem.Text + " - Contrato " + m_EContrato.Text;
        m_STitulo.UpdateAfterCallBack = true;
    }
    protected void m_LCContratos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
