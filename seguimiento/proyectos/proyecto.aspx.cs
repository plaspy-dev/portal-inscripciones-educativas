using System;
using System.Data;
using System.Data.SqlClient;
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
    public int m_nPermisosRegistro;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectos", 16))
            return;

        m_nPermisosRegistro = m_pMenu.GetPermisos(HttpContext.Current.Request.ApplicationPath + "/fotos/proyectos/galeria.aspx");

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
        m_saCamposAlias[8] = "dbo.fn_GetObjetos(proyectos.id)";
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
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("proyectos.eterritorial");

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_LCProyecto, "SELECT id,codigo FROM proyectos WHERE vigencia='" + m_pUsuario.GetVigenciaActual()
            + "' AND eterritorial=" + m_CBEnte.SelectedValue, true);

        if (m_LCProyecto.SelectedIndex > 0)
            m_szCondicionEx+= " AND proyectos.id=" + m_LCProyecto.SelectedValue;
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.eterritorial=" + m_CBEnte.SelectedValue;
        

        string grupo= " GROUP BY ";
        for (int i = 0; i < 8; i++)
        {
            grupo += m_saCamposAlias[i] + ",";
        }
        grupo += m_saCamposAlias[10] + "," + m_saCamposAlias[14] + "," + m_saCamposAlias[15];

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx + grupo);
        m_szCondicion += grupo;
        m_baHidden[0] = true;
        m_baHidden[15] = true;

        // pestaña 2
        m_EValorContratado.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorContratado.Attributes["onblur"] = "tcomas(this);";
        m_EValorContratado.Attributes["onfocus"] = "scomas(this);";
        m_EPlazoContrato.Attributes["onKeypress"] = "isnb();";
        m_EValorAvance.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorAvance.Attributes["onblur"] = "tcomas(this);";
        m_EValorAvance.Attributes["onfocus"] = "scomas(this);";

        m_EPlazoI.Attributes["onKeypress"] = "isnb();";
        
        // 55 
        FillComboBox(m_CBClaseContrato, "SELECT * FROM varios WHERE tipo=55 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBEstadoContrato, "SELECT * FROM varios WHERE tipo=32 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBObjeto, "SELECT * FROM varios WHERE tipo=37 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBTipoR, "SELECT * FROM varios WHERE tipo=56 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBEstadoSaldo, "SELECT * FROM varios WHERE tipo=57 AND (nReservado IS NULL OR nReservado!=-1)");

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
            data.DataField = data.HeaderText = "Grupo";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Codigo Proyecto";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Entidad Territorial";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Departamento";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Municipio";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Institución educativa";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Objeto";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Numero de contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Fecha Suscripcion del contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Fecha acta de inicio del contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Clase de contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Objeto del contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Plazo (incluidas adiciones y suspensiones)";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Fecha de vencimiento del contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Valor total contratado (incluidos adicionales)";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Valor aportes Entidad educativa al contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Valor aportes municipio al  contrato (incluidas adiciones)";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Valor avance de obra acumulado a último reporte";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Fecha  del reporte";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "% de ejecución sobre el total del valor del contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Estado del contrato";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Observaciones sobre revisión de informes";
            m_LCContratos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Historial del proyecto";
            m_LCContratos.Columns.Add(data);

            m_LCContratos.Style["display"]="none";

             // pestaña 3
            data = new BoundField();
            data.Visible = false;
            data.DataField = data.HeaderText = "id";
            m_LCContratosSaldos.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Grupo";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Codigo Proyecto";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Entidad Territorial";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Departamento";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Municipio";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Diferencia valor aprobado proyecto y valor contratado ";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Diferencia valor aporte Entidad educativa y valor contratado Entidad";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Diferencia valor aportes MUN y valor contratado municipio";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Estado";
            m_LCContratosSaldos.Columns.Add(data);
            data = new BoundField();
            data.DataField = data.HeaderText = "Observaciones";
            m_LCContratosSaldos.Columns.Add(data);

            m_LCContratosSaldos.Style["display"] = "none";
        }
        m_szMensaje = "No se encontraron proyectos";
        m_BSubir.Style["display"] = (m_nPermisosRegistro & 4)==4 ? "" : "none";

        if ((m_nPermisos & 8) == 0)
        {
            m_BFinalizarE.Style["display"] = "none";
        }
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
        m_CBObjeto.UpdateAfterCallBack = true;
        m_EPlazoContrato.UpdateAfterCallBack = true;
        m_SFechaV.UpdateAfterCallBack = true;
        m_EValorContratado.UpdateAfterCallBack = true;
        m_SValorMen.UpdateAfterCallBack = true;
        m_SValorMun.UpdateAfterCallBack = true;
        m_EContratista.UpdateAfterCallBack = true;
        m_ERepresentante.UpdateAfterCallBack = true;
        m_EValorAvance.UpdateAfterCallBack = true;
        m_EFechaR.UpdateAfterCallBack = true;
        m_SPEjecucion.UpdateAfterCallBack = true;
        m_EObservacionesReporte.UpdateAfterCallBack = true;
        m_EHistorial.UpdateAfterCallBack = true;
        m_EFechaRegistro.UpdateAfterCallBack = true;
        m_EInterventor.UpdateAfterCallBack = true;
        m_ECargo.UpdateAfterCallBack = true;
        m_EContratoI.UpdateAfterCallBack = true;
        m_EPlazoI.UpdateAfterCallBack = true;
        m_EFechaAI.UpdateAfterCallBack = true;
        m_SFechaVI.UpdateAfterCallBack = true;
        m_SFechaVI.UpdateAfterCallBack = true;
        m_EObservacionesContrato.UpdateAfterCallBack = true;
        m_CBTipoR.UpdateAfterCallBack = true;

        m_nIdContrato.UpdateAfterCallBack = true;
        m_LCContratos.UpdateAfterCallBack = true;
        // pestaña 3

        m_LCContratosSaldos.UpdateAfterCallBack = true;
        m_EObservacionesSaldo.UpdateAfterCallBack = true;
        m_CBEstadoSaldo.UpdateAfterCallBack = true;
        m_SDifMen.UpdateAfterCallBack = true;
        m_SDifMun.UpdateAfterCallBack = true;
        m_SDifProy.UpdateAfterCallBack = true;
        // pestaña 4
        m_EInforme.UpdateAfterCallBack = true;
        m_EActa.UpdateAfterCallBack = true;
        m_EInterventoria.UpdateAfterCallBack = true;
        m_EHacienda.UpdateAfterCallBack = true;
        m_EMunicipio.UpdateAfterCallBack = true;
        m_EObservacionesFinal.UpdateAfterCallBack = true;
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

        m_BTab3.CausesValidation = false;
        m_BTab4.CausesValidation = false;

        m_BGaleria.Visible = m_nPermisosRegistro>0;
        m_BGaleria.UpdateAfterCallBack = true;

        // pestaña 2
        Session["idproyecto"]=m_nId.Value;
        m_nIdContrato.Value = "";
        m_LCContratos.DataBind();
        m_BEliminarContrato.Style["display"] = "none";
        m_BAceptarContrato.Style["display"] = "none";
        m_BCancelarContrato.Style["display"] = "none";
        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarContrato.Style["display"] = "";
            m_BInsertarContrato.UpdateAfterCallBack = true;
        }

        // pestaña 3
        m_LCContratosSaldos.DataBind();
        CContratosSaldos saldos = new CContratosSaldos();
        DataTable tabla= saldos.GetTabla();
        if (tabla != null && tabla.Rows.Count>0)
        {
            m_SDifProy.Text = GetFormatDouble(tabla.Rows[0][6].ToString());
            m_SDifMen.Text = GetFormatDouble(tabla.Rows[0][7].ToString());
            m_SDifMun.Text = GetFormatDouble(tabla.Rows[0][8].ToString());
            SelectInCombo(m_CBEstadoSaldo, tabla.Rows[0][9].ToString());
            m_EObservacionesSaldo.Text = tabla.Rows[0][10].ToString();
        }

        SqlDataReader reader = m_pConector.Query("SELECT * FROM documentosproyectof WHERE proyecto=" +m_nId.Value);
        if (reader != null && reader.Read()) {

            m_EInforme.Text= GetFechaCorta(reader[1].ToString());
            m_EActa.Text= GetFechaCorta(reader[2].ToString());
            m_EInterventoria.Text= GetFechaCorta(reader[3].ToString());
            m_EHacienda.Text= GetFechaCorta(reader[4].ToString());
            m_EMunicipio.Text= GetFechaCorta(reader[5].ToString());
            m_EObservacionesFinal.Text= reader[6].ToString();
        }
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
        m_CBEnte.SelectedIndex = 0;

        m_BGaleria.Visible = false;
        m_BGaleria.UpdateAfterCallBack = true;

        // pestaña 2
        m_EContrato.Text = "";
        m_EFechaS.Text = "";
        m_EActaInicio.Text = "";
        m_CBClaseContrato.SelectedIndex=0;
        m_CBObjeto.SelectedIndex = 0;
        m_EPlazoContrato.Text = "";
        m_SFechaV.Text = "";
        m_EValorContratado.Text = "";
        m_SValorMen.Text = "";
        m_SValorMun.Text = "";
        m_EContratista.Text = "";
        m_ERepresentante.Text = "";
        m_EValorAvance.Text = "";
        m_EFechaR.Text = "";
        m_SPEjecucion.Text = "";
        m_EObservacionesReporte.Text = "";
        m_EHistorial.Text = "";
        m_EFechaRegistro.Text = "";
        m_EInterventor.Text = "";
        m_ECargo.Text = "";
        m_EFechaAI.Text = "";
        m_EContratoI.Text = "";
        m_SFechaVI.Text = "";
        m_EPlazoI.Text = "";
        m_SFechaVI.Text = "";
        m_EObservacionesContrato.Text = "";
        m_CBTipoR.SelectedIndex = 0;

        Session["idconvenio"] = "";
        m_nIdContrato.Value = "";
        m_LCContratos.DataBind();
        m_BEliminarContrato.Style["display"] = "none";
        m_BAceptarContrato.Style["display"] = "none";
        m_BCancelarContrato.Style["display"] = "none";
        m_BInsertarContrato.Style["display"] = "none";

        m_BInsertarContrato.UpdateAfterCallBack = true;

        // pestaña 4
        m_LCContratosSaldos.DataBind();
        m_EObservacionesSaldo.Text = "";
        m_CBEstadoSaldo.SelectedIndex = 0;
        m_SDifMen.Text = "";
        m_SDifMun.Text = "";
        m_SDifProy.Text = "";
        // pestaña 5
        m_EInforme.Text = "";
        m_EActa.Text = "";
        m_EInterventoria.Text = "";
        m_EHacienda.Text = "";
        m_EMunicipio.Text = "";
        m_EObservacionesFinal.Text = "";

        m_BTab3.CausesValidation = true;
        m_BTab4.CausesValidation = true;

        m_BCancelarContrato_Click(sender, e);
        m_STitulo.Text = "Seleccione un proyecto";
        base.m_BCancelar_Click(sender, e);
    }

    protected bool UpdateFechas()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0]="proyecto";
        m_saCampos[1]="informef";
        m_saCampos[2]="actat";
        m_saCampos[3]="cinterventoria";
        m_saCampos[4]="chacienda";
        m_saCampos[5]="cmunicipal";
        m_saCampos[6]="observaciones";
        
        m_saDatos[0]=m_nId.Value;
        m_saDatos[1]=GetDate(m_EInforme.Text);
        m_saDatos[2]=GetDate(m_EActa.Text);
        m_saDatos[3]=GetDate(m_EInterventoria.Text);
        m_saDatos[4]=GetDate(m_EHacienda.Text);
        m_saDatos[5]=GetDate(m_EMunicipio.Text);
        m_saDatos[6]=GetString(m_EObservacionesFinal.Text);

        string query = "";
        if (m_pConector.QueryString("SELECT proyecto FROM documentosproyectof WHERE proyecto=" + m_nId.Value) == "")
        {
            query = CConector.CrearInsercion("documentosproyectof", m_saCampos, m_saDatos);
        }
        else
        {
            m_saCampos[0] = "";
            query = CConector.CreateUpdate("documentosproyectof", m_saCampos, m_saDatos) + CConector.GetCondicion("proyecto", m_nId.Value);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool UpdateSaldos()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0] = "proyecto";
        m_saCampos[1] = "estado";
        m_saCampos[2] = "observaciones";

        m_saDatos[0] = m_nId.Value;
        m_saDatos[1] = GetSeleccion(m_CBEstadoSaldo.SelectedValue);
        m_saDatos[2] = GetString(m_EObservacionesSaldo.Text);

        string query = "";
        if (m_pConector.QueryString("SELECT proyecto FROM saldosp WHERE proyecto=" + m_nId.Value) == "")
        {
            query = CConector.CrearInsercion("saldosp", m_saCampos, m_saDatos);
        }
        else
        {
            m_saCampos[0] = "";
            query = CConector.CreateUpdate("saldosp", m_saCampos, m_saDatos) + CConector.GetCondicion("proyecto", m_nId.Value);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";
        // m_saDatos[1] = m_ENombre.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, "observaciones", GetString(m_EObservaciones.Text)) + CConector.GetCondicion("id", m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            if (UpdateSaldos() && UpdateFechas())
            {
                base.AfterUpdate("Proyecto " + m_LCProyecto.SelectedItem.Text + " Editado");
                MultiView1.ActiveViewIndex = 0;
                SetView(0);
            }
            else
            {
                m_SMensaje.Text = m_pConector.GetLastError();
                m_SMensaje.UpdateAfterCallBack = true;
            }
        }
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
        if (m_LCProyecto.SelectedIndex > 0)
        {
            m_LCResultados.SelectedIndex = 0;
            m_LCResultados_SelectedIndexChanged(m_LCResultados, e);
        }
        else
        {
            m_LCResultados.SelectedIndex = -1;
            m_BCancelar_Click(sender, e);
        }
    }
    // pestaña 2
    protected void m_BInsertarContrato_Click(object sender, EventArgs e)
    {

        string[] campos = new string[13];
        string[] datos = new string[13];

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
        campos[10]="contratista";
        campos[11]="rlegal";
        campos[12]="historial";

        datos[0] = m_nId.Value;
        datos[1] = m_EContrato.Text;
        datos[2] = GetDate(m_EFechaS.Text);
        datos[3] = GetDate(m_EActaInicio.Text);
        datos[4] = GetSeleccion(m_CBClaseContrato.SelectedValue);
        datos[5] = GetSeleccion(m_CBObjeto.SelectedValue);
        datos[6] = GetDouble(m_EPlazoContrato.Text);
        datos[7] = GetDouble(m_EValorContratado.Text);
        datos[8] = GetSeleccion(m_CBEstadoContrato.SelectedValue);
        datos[9] = GetString(m_EObservacionesContrato.Text);
        datos[10]=m_EContratista.Text;
        datos[11]=m_ERepresentante.Text;
        datos[12]=m_EHistorial.Text;

        int id= m_pConector.QueryMod(CConector.CrearInsercion("contratos", campos, datos), true);
        if(id!=-1) {
            for (int i = 0; i < campos.Length; i++)
                campos[i] = datos[i]= "";
            campos[0]="contrato";
            campos[1]="valoravance";
            campos[2]="fecha";
            campos[3]="tipo";
            campos[4]="interventor";
            campos[5]="cargo";
            campos[6]="observaciones";
            campos[7]="ncontrato";
            campos[8]="acta";
            campos[9]="plazo";

            datos[0]=id.ToString();
            datos[1]=GetDouble(m_EValorAvance.Text);
            datos[2]=GetDate(m_EFechaR.Text);
            datos[3]=GetSeleccion(m_CBTipoR.SelectedValue);
            datos[4]=GetString(m_EInterventor.Text);
            datos[5]=GetString(m_ECargo.Text);
            datos[6]=GetString(m_EObservacionesReporte.Text);
            datos[7] = GetString(m_EContratoI.Text);
            datos[8] = GetDate(m_EFechaAI.Text);
            datos[9] = GetString(m_EPlazoI.Text);

            m_pConector.QueryMod(CConector.CrearInsercion("reporte", campos, datos));
            for (int i = 0; i < campos.Length; i++)
                campos[i] = datos[i] = "";
            campos[0]="contrato";
            datos[0]=id.ToString();
            m_pConector.QueryMod(CConector.CrearInsercion("saldos", campos, datos));
            m_pConector.QueryMod(CConector.CrearInsercion("documentoscontrato", campos, datos));
        }
        m_BCancelarContrato_Click(sender, e);
    }
    protected void m_BAceptarContrato_Click(object sender, EventArgs e)
    {
        string[] campos = new string[13];
        string[] datos = new string[13];

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
        campos[10] = "contratista";
        campos[11] = "rlegal";
        campos[12] = "historial";

        datos[0] = m_nId.Value;
        datos[1] = m_EContrato.Text;
        datos[2] = GetDate(m_EFechaS.Text);
        datos[3] = GetDate(m_EActaInicio.Text);
        datos[4] = GetSeleccion(m_CBClaseContrato.SelectedValue);
        datos[5] = GetSeleccion(m_CBObjeto.SelectedValue);
        datos[6] = GetDouble(m_EPlazoContrato.Text);
        datos[7] = GetDouble(m_EValorContratado.Text);
        datos[8] = GetSeleccion(m_CBEstadoContrato.SelectedValue);
        datos[9] = GetString(m_EObservacionesContrato.Text);
        datos[10] = m_EContratista.Text;
        datos[11] = m_ERepresentante.Text;
        datos[12] = m_EHistorial.Text;

        m_pConector.QueryMod(CConector.CreateUpdate("contratos", campos, datos) + CConector.GetCondicion("id", m_nIdContrato.Value));
        
        for (int i = 0; i < campos.Length; i++)
            campos[i] = datos[i] = "";
        campos[0] = "";
        campos[1] = "valoravance";
        campos[2] = "fecha";
        campos[3] = "tipo";
        campos[4] = "interventor";
        campos[5] = "cargo";
        campos[6] = "observaciones";
        campos[7] = "ncontrato";
        campos[8] = "acta";
        campos[9] = "plazo";

        datos[1] = GetDouble(m_EValorAvance.Text);
        datos[2] = GetDate(m_EFechaR.Text);
        datos[3] = GetSeleccion(m_CBTipoR.SelectedValue);
        datos[4] = GetString(m_EInterventor.Text);
        datos[5] = GetString(m_ECargo.Text);
        datos[6] = GetString(m_EObservacionesReporte.Text);
        datos[7] = GetString(m_EContratoI.Text);
        datos[8] = GetDate(m_EFechaAI.Text);
        datos[9] = GetString(m_EPlazoI.Text);

        if (m_pConector.QueryString("SELECT contrato FROM reporte WHERE contrato=" + m_nIdContrato.Value) == "")
        {
            campos[0] = "contrato";
            datos[0] = m_nIdContrato.Value;
            m_pConector.QueryMod(CConector.CrearInsercion("reporte", campos, datos));
        } else m_pConector.QueryMod(CConector.CreateUpdate("reporte", campos, datos) + CConector.GetCondicion("contrato", m_nIdContrato.Value));

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
        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarContrato.Style["display"] = "";
            m_BInsertarContrato.UpdateAfterCallBack = true;
        }

        m_nIdContrato.Value = "";
        m_EContrato.Text = "";
        m_EFechaS.Text = "";
        m_EActaInicio.Text = "";
        m_CBClaseContrato.SelectedIndex = 0;
        m_CBObjeto.SelectedIndex = 0;
        m_EPlazoContrato.Text = "";
        m_SFechaV.Text = "";
        m_EValorContratado.Text = "";
        m_SValorMen.Text = "";
        m_SValorMun.Text = "";
        m_EContratista.Text = "";
        m_ERepresentante.Text = "";
        m_EValorAvance.Text = "";
        m_EFechaR.Text = "";
        m_SPEjecucion.Text = "";
        m_EObservacionesReporte.Text = "";
        m_EHistorial.Text = "";
        m_EFechaRegistro.Text = "";
        m_EInterventor.Text = "";
        m_ECargo.Text = "";
        m_EContratoI.Text = "";
        m_EFechaAI.Text = "";
        m_EPlazoI.Text = "";
        m_SFechaVI.Text = "";
        m_EObservacionesContrato.Text = "";
        m_CBTipoR.SelectedIndex = 0;
        
        CContratos contratos=new CContratos();
        contratos.RebuilTable();

        m_LCContratos.DataBind();
        m_LCContratos.SelectedIndex = -1;
        m_LCContratos.UpdateAfterCallBack = true;


        m_nIdContrato.UpdateAfterCallBack = true;
        m_EContrato.UpdateAfterCallBack = true;
        m_EFechaS.UpdateAfterCallBack = true;
        m_EActaInicio.UpdateAfterCallBack = true;
        m_CBClaseContrato.UpdateAfterCallBack = true;
        m_CBObjeto.UpdateAfterCallBack = true;
        m_EPlazoContrato.UpdateAfterCallBack = true;
        m_SFechaV.UpdateAfterCallBack = true;
        m_EValorContratado.UpdateAfterCallBack = true;
        m_SValorMen.UpdateAfterCallBack = true;
        m_SValorMun.UpdateAfterCallBack = true;
        m_EContratista.UpdateAfterCallBack = true;
        m_ERepresentante.UpdateAfterCallBack = true;
        m_EValorAvance.UpdateAfterCallBack = true;
        m_EFechaR.UpdateAfterCallBack = true;
        m_SPEjecucion.UpdateAfterCallBack = true;
        m_EObservacionesReporte.UpdateAfterCallBack = true;
        m_EHistorial.UpdateAfterCallBack = true;
        m_EFechaRegistro.UpdateAfterCallBack = true;
        m_EInterventor.UpdateAfterCallBack = true;
        m_ECargo.UpdateAfterCallBack = true;
        m_EFechaAI.UpdateAfterCallBack = true;
        m_EContratoI.UpdateAfterCallBack = true;
        m_EPlazoI.UpdateAfterCallBack = true;
        m_SFechaVI.UpdateAfterCallBack = true;
        m_EObservacionesContrato.UpdateAfterCallBack = true;
        m_CBTipoR.UpdateAfterCallBack = true;

        m_STitulo.Text = "Proyecto " + m_LCProyecto.SelectedItem.Text;
        m_STitulo.UpdateAfterCallBack = true;
    }

    protected void m_BEliminarContrato_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM registro WHERE contrato=" + m_nIdContrato.Value);
        m_pConector.QueryMod("DELETE FROM reporte WHERE contrato=" + m_nIdContrato.Value);
        m_pConector.QueryMod("DELETE FROM saldos WHERE contrato=" + m_nIdContrato.Value);
        m_pConector.QueryMod("DELETE FROM documentoscontrato WHERE contrato=" + m_nIdContrato.Value);
        m_pConector.QueryMod("DELETE FROM contratos WHERE id=" + m_nIdContrato.Value);
        m_BCancelarContrato_Click(sender, e);
    }
    protected void m_LCContratos_SelectedIndexChanged(object sender, EventArgs e)
    {
        CContratos contratos = new CContratos();
        DataRow row = GetDataRow(m_LCContratos, contratos.GetTabla());
        if (row == null)
            return;

        m_nIdContrato.Value = m_LCContratos.SelectedDataKey.Value.ToString();
        m_EContrato.Text= row[8].ToString();
        m_EFechaS.Text= GetFechaCorta(row[9].ToString());
        m_EActaInicio.Text = GetFechaCorta(row[10].ToString());
        SelectInCombo(m_CBClaseContrato, row[11].ToString());
        SelectInCombo(m_CBObjeto, row[12].ToString());
        m_EPlazoContrato.Text= row[13].ToString();
        m_SFechaV.Text = m_SFechaVI.Text = GetFechaCorta(row[14].ToString());
        m_EValorContratado.Text= GetFormatDouble(row[15].ToString());
        m_EValorAvance.Text= GetFormatDouble(row[18].ToString());
        m_EFechaR.Text= GetFechaCorta(row[19].ToString());
        SelectInCombo(m_CBEstadoContrato, row[21].ToString());
        m_EObservacionesReporte.Text= row[22].ToString();
        m_EHistorial.Text=row[23].ToString();
        m_SValorMen.Text = GetFormatDouble(row[16].ToString());
        m_SValorMun.Text= GetFormatDouble(row[17].ToString());
        m_EContratista.Text = row[24].ToString();
        m_ERepresentante.Text = row[25].ToString();
        m_SPEjecucion.Text = row[20].ToString();
        m_EInterventor.Text = row[26].ToString();
        m_ECargo.Text = row[27].ToString();
        m_EObservacionesContrato.Text = row[28].ToString();
        m_EFechaRegistro.Text = GetFechaCorta(row[29].ToString());
        m_EContratoI.Text = row[30].ToString();
        m_EFechaAI.Text = GetFechaCorta(row[31].ToString());
        m_EPlazoI.Text = row[32].ToString();
        m_SFechaVI.Text = GetFechaCorta(row[33].ToString());

        m_nIdContrato.UpdateAfterCallBack = true;
        m_EContrato.UpdateAfterCallBack = true;
        m_EFechaS.UpdateAfterCallBack = true;
        m_EActaInicio.UpdateAfterCallBack = true;
        m_CBClaseContrato.UpdateAfterCallBack = true;
        m_CBObjeto.UpdateAfterCallBack = true;
        m_EPlazoContrato.UpdateAfterCallBack = true;
        m_SFechaV.UpdateAfterCallBack = true;
        m_EValorContratado.UpdateAfterCallBack = true;
        m_SValorMen.UpdateAfterCallBack = true;
        m_SValorMun.UpdateAfterCallBack = true;
        m_EContratista.UpdateAfterCallBack = true;
        m_ERepresentante.UpdateAfterCallBack = true;
        m_EValorAvance.UpdateAfterCallBack = true;
        m_EFechaR.UpdateAfterCallBack = true;
        m_SPEjecucion.UpdateAfterCallBack = true;
        m_EObservacionesReporte.UpdateAfterCallBack = true;
        m_EHistorial.UpdateAfterCallBack = true;
        m_EFechaRegistro.UpdateAfterCallBack = true;
        m_EInterventor.UpdateAfterCallBack = true;
        m_ECargo.UpdateAfterCallBack = true;

        m_EContratoI.UpdateAfterCallBack = true;
        m_EFechaAI.UpdateAfterCallBack = true;
        m_EPlazoI.UpdateAfterCallBack = true;
        m_SFechaVI.UpdateAfterCallBack = true;
        m_EObservacionesContrato.UpdateAfterCallBack = true;
        m_CBTipoR.UpdateAfterCallBack = true;

        if ((m_nPermisos & 16) == 16)
        {
            m_BEliminarContrato.Style["display"] = "";
            m_BEliminarContrato.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 8) == 8)
        {
            m_BAceptarContrato.Style["display"] = "";
            m_BAceptarContrato.UpdateAfterCallBack = true;
        }
        m_BCancelarContrato.Style["display"] = "";
        m_BCancelarContrato.UpdateAfterCallBack = true;
        m_BInsertarContrato.Style["display"] = "none";
        m_BInsertarContrato.UpdateAfterCallBack = true;

        m_STitulo.Text = "Proyecto " + m_LCProyecto.SelectedItem.Text + " - Contrato " + m_EContrato.Text;
        m_STitulo.UpdateAfterCallBack = true;
    }
    protected void m_LCContratos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CContratos tabla= new CContratos();
        ParseRow((GridView)sender, tabla.GetTabla(), e);
    }
    override protected void SetView(int index)
    {
        m_LCResultados.Style["display"] = (index == 0 || index == 3) ? "" : "none";
        m_LCContratos.Style["display"] = index == 1 ? "" : "none";
        m_LCContratosSaldos.Style["display"] = index == 2 ? "" : "none";
        base.SetView(index);
    }

    protected void m_LCContratosSaldos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CContratosSaldos tabla = new CContratosSaldos();
        ParseRow((GridView)sender, tabla.GetTabla(), e);
    }
    protected void m_BSubir_Click(object sender, EventArgs e)
    {
        int len = m_ERegistro.PostedFile.ContentLength;
        if (m_nId.Value == "")
        {
            m_SMensaje.Text = "Por favor seleccione un proyecto";
            return;
        }
        if (len <= 0)
        {
            m_SMensaje.Text = "Por favor seleccione un archivo";
            return;
        }
        if (m_ERegistro.PostedFile.ContentType.IndexOf("jpeg")<0)
        {
            m_SMensaje.Text = "Por favor seleccione un archivo con formato JPEG";
            return;
        }
        byte[] pic = new byte[len];
        m_ERegistro.PostedFile.InputStream.Read(pic, 0, len);
        CConector conector = new CConector();
        SqlConnection connection = conector.GetConexion();
        string query = "INSERT INTO fotos (fecha,proyecto,foto) VALUES (" + (m_EFechaRegistro.Text.Length > 0 ? ("'" + m_EFechaRegistro.Text + "'") : "GETDATE()") + ",@proyecto,@foto)";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@proyecto", m_nId.Value);
        cmd.Parameters.AddWithValue("@foto", pic);
        cmd.ExecuteNonQuery();
        m_SMensaje.Text = "Foto Agregada";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Anthem.Manager.AddScriptForClientSideEval("window.open('" + HttpContext.Current.Request.ApplicationPath + "/fotos/proyectos/galeria.aspx?id=" + m_nId.Value +"&proyecto=" + m_LCProyecto.SelectedItem.Text+ "')");
    }
}
