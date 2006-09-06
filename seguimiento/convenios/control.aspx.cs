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

public partial class seguimiento_convenios_control : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("conveniosm", 23))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "";
        m_saCampos[2] = "";
        m_saCampos[3] = "codigo";
        m_saCampos[4] = "";
        m_saCampos[5] = "";
        m_saCampos[6] = "valor";
        m_saCampos[7] = "";
        m_saCampos[8] = "fechaaprobacion";
        m_saCampos[9] = "";
        m_saCampos[10] = "plazo";
        m_saCampos[11] = "";
        m_saCampos[12] = "";
        m_saCampos[13] = "";
        m_saCampos[14] = "estado";
        m_saCampos[15] = "observaciones";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Ente territorial";
        m_saTitulos[3] = "Numero de convenio";
        m_saTitulos[4] = "Tipo";
        m_saTitulos[5] = "Valor total convenio (incluye adiciones)";
        m_saTitulos[6] = "Valor inicial convenio";
        m_saTitulos[7] = "Valor adiciones convenio";
        m_saTitulos[8] = "Fecha de aprobación";
        m_saTitulos[9] = "Plazo total (incluye prorrogas)";
        m_saTitulos[10] = "Plazo inicial convenio";
        m_saTitulos[11] = "Plazo prorrogas convenio";
        m_saTitulos[12] = "Valor desembolsado";
        m_saTitulos[13] = "Fecha de vencimiento";
        m_saTitulos[14] = "Estado";
        m_saTitulos[15] = "Observaciones";
        // pestaña 6
        m_saTitulos[16] = "Convenio";
        m_saTitulos[17] = "Informe";
        m_saTitulos[18] = "Fecha Informe";
        m_saTitulos[19] = "Documentos Completos";
        m_saTitulos[20] = "Acta liquidación";
        m_saTitulos[21] = "Fecha liquidación";
        m_saTitulos[22] = "Observaciones liquidación";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1] = "grupos.nombre";
        m_saCamposAlias[2] = "eterritoriales.nombre";
        m_saCamposAlias[4] = "tipos.nombre";
        m_saCamposAlias[5] = "conveniosm.valor+ISNULL(SUM(adicionesconvenio.valor),0)";
        m_saCamposAlias[7] = "SUM(adicionesconvenio.valor)";
        m_saCamposAlias[9] = "conveniosm.plazo+ISNULL(SUM(adicionesconvenio.plazo),0)";
        m_saCamposAlias[11] = "SUM(adicionesconvenio.plazo)";
        m_saCamposAlias[12] = "(SELECT SUM(desembolsosconvenio.valor) FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id)";
        m_saCamposAlias[13] = "DATEADD(dd,conveniosm.plazo+ISNULL(SUM(adicionesconvenio.plazo),0)-1,conveniosm.fechaaprobacion)";
        m_saCamposAlias[14] = "estados.nombre";
        // pestaña 6
        m_saCamposAlias[16] = "liquidacionconvenio.convenio";
        m_saCamposAlias[17] = "liquidacionconvenio.informe";
        m_saCamposAlias[18] = "liquidacionconvenio.fechainforme";
        m_saCamposAlias[19] = "liquidacionconvenio.documentos";
        m_saCamposAlias[20] = "liquidacionconvenio.actaliquidacion";
        m_saCamposAlias[21] = "liquidacionconvenio.fechaliquidacion";
        m_saCamposAlias[22] = "liquidacionconvenio.observaciones";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=conveniosm.eterritorial) AND asignacionpresupuestal.vigencia='" + m_pUsuario.GetVigenciaActual() + "' "
            +" LEFT JOIN varios AS grupos ON (grupos.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial)"
            + " LEFT JOIN varios AS tipos ON (tipos.id=conveniosm.tipo)"
            + " LEFT JOIN varios AS estados ON (estados.id=conveniosm.estado)"
            + " LEFT JOIN adicionesconvenio ON (adicionesconvenio.convenio=conveniosm.id)"
            // + " LEFT JOIN desembolsosconvenio ON (desembolsosconvenio.convenio=conveniosm.id)"
            + " LEFT JOIN liquidacionconvenio ON (liquidacionconvenio.convenio=conveniosm.id)"
            ;
        m_szCondicion = " (conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ) ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBConvenio, "SELECT id,codigo FROM conveniosm WHERE vigencia='" + m_pUsuario.GetVigenciaActual()
            + "' AND eterritorial=" + m_CBEnte.SelectedValue, true);
        FillComboBox(m_CBEstado, "SELECT id,nombre FROM varios WHERE tipo=53 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBEstadoConvenio, "SELECT id,nombre FROM varios WHERE tipo=32 AND (nReservado IS NULL OR nReservado!=-1)");
        CFecha.FillMeses(m_CBMes);
        CFecha.FillAnos(m_LCAno, Convert.ToInt32(m_pUsuario.GetVigenciaActual()));
        FillComboBox(m_CBEstadoRendimiento, "SELECT id,nombre FROM varios WHERE tipo=54 AND (nReservado IS NULL OR nReservado!=-1)");
        // autofiltro
        if (m_CBConvenio.SelectedIndex > 0)
            m_szCondicionEx += " AND conveniosm.id=" + m_CBConvenio.SelectedValue;
        if (m_CBEstadoConvenio.SelectedIndex > 0)
            m_szCondicionEx += " AND conveniosm.estado=" + m_CBEstadoConvenio.SelectedValue;
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND conveniosm.eterritorial=" + m_CBEnte.SelectedValue;
        string grupo = " GROUP BY ";
        for (int i = 0; i < 5; i++)
        {
            grupo += m_saCamposAlias[i] + ",";
        }
        for (int i = 14; i < m_saCamposAlias.Length; i++)
        {
            grupo += m_saCamposAlias[i] + ",";
        }
        grupo += m_saCamposAlias[6] + "," + m_saCamposAlias[8] + ", " + m_saCamposAlias[10];

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx + grupo);
        m_szCondicion += grupo;
        m_baHidden[0] = true;
        for (int i = 16; i < m_saTitulos.Length; i++)
            m_baHidden[i] = true;

        m_nPermisos &= ~4;
        m_nPermisos &= ~16;

        // pestaña 2
        m_EAdicionValor.Attributes["onKeypress"] = "isfloat(this);";
        m_EAdicionValor.Attributes["onblur"] = "tcomas(this);";
        m_EAdicionValor.Attributes["onfocus"] = "scomas(this);";
        m_EProrroga.Attributes["onKeypress"] = "isnb();";

        if (!IsPostBack)
        {
            m_BEliminarObjeto.Style["display"] = "none";
            m_BAceptarObjeto.Style["display"] = "none";
            m_BCancelarObjeto.Style["display"] = "none";
            m_BInsertarObjeto.Style["display"] = "none";

            BoundField data = new BoundField();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "Número";
            data.HeaderText = "Adicional no.";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "Valor";
            data.HeaderText = "Adición en valor";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "FechaV";
            data.HeaderText = "Fecha adición";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "CDP";
            data.HeaderText = "Certificado de disponibilidad presupuestal";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "FechaCDP";
            data.HeaderText = "Fecha certificado";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "RDP";
            data.HeaderText = "Registro presupuestal";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "FechaRDP";
            data.HeaderText = "Fecha registro";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "Plazo";
            data.HeaderText = "Prorroga en días calendario";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "FechaP";
            data.HeaderText = "Fecha prorroga";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "Estado";
            data.HeaderText = "Estado";
            m_LCAdiciones.Columns.Add(data);

            data = new BoundField();
            data.DataField = "Observaciones";
            data.HeaderText = "Observaciones";
            m_LCAdiciones.Columns.Add(data);

            m_LCAdiciones.Style["display"] = "none";

            m_SNoAdicion.Text = CAdicionesConvenio.GetNumero().ToString();
        }
        // pestaña 4
        m_EValorDesembolso.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorDesembolso.Attributes["onblur"] = "tcomas(this);";
        m_EValorDesembolso.Attributes["onfocus"] = "scomas(this);";

        if (!IsPostBack)
        {
            m_BEliminarDesembolso.Style["display"] = "none";
            m_BAceptarDesembolso.Style["display"] = "none";
            m_BCancelarDesembolso.Style["display"] = "none";
            m_BInsertarDesembolso.Style["display"] = "none";

            BoundField data = new BoundField();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LCDesembolsos.Columns.Add(data);

            data = new BoundField();
            data.DataField = "numero";
            data.HeaderText = "Número";
            m_LCDesembolsos.Columns.Add(data);

            data = new BoundField();
            data.DataField = "valor";
            data.HeaderText = "Valor";
            m_LCDesembolsos.Columns.Add(data);

            data = new BoundField();
            data.DataField = "fecha";
            data.HeaderText = "Fecha";
            m_LCDesembolsos.Columns.Add(data);

            data = new BoundField();
            data.DataField = "comprobante";
            data.HeaderText = "Comprobante";
            m_LCDesembolsos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "fechac";
            data.HeaderText = "Fecha comprobante";
            m_LCDesembolsos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "observaciones";
            data.HeaderText = "Observaciones";
            m_LCDesembolsos.Columns.Add(data);
            m_LCDesembolsos.Style["display"] = "none";
        }
        // pestaña 5
        m_EValorRendimiento.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorRendimiento.Attributes["onblur"] = "tcomas(this);";
        m_EValorRendimiento.Attributes["onfocus"] = "scomas(this);";
        if (!IsPostBack)
        {
            m_BEliminarRendimiento.Style["display"] = "none";
            m_BAceptarRendimiento.Style["display"] = "none";
            m_BCancelarRendimiento.Style["display"] = "none";
            m_BInsertarRendimiento.Style["display"] = "none";

            BoundField data = new BoundField();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LCRendimientos.Columns.Add(data);

            data = new BoundField();
            data.DataField = "valor";
            data.HeaderText = "Valor";
            m_LCRendimientos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "mes";
            data.HeaderText = "Mes";
            m_LCRendimientos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "ano";
            data.HeaderText = "Año";
            m_LCRendimientos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "fecha";
            data.HeaderText = "Fecha";
            m_LCRendimientos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "estado";
            data.HeaderText = "Estado";
            m_LCRendimientos.Columns.Add(data);
            data = new BoundField();
            data.DataField = "observaciones";
            data.HeaderText = "Observaciones";
            m_LCRendimientos.Columns.Add(data);
            m_LCRendimientos.Style["display"] = "none";
        }

        m_szMensaje = "No se encontraron convenios";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_STitulo.UpdateAfterCallBack = true;
        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBConvenio.UpdateAfterCallBack = true;
        m_SValor.UpdateAfterCallBack = true;
        m_CBEstadoConvenio.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
        // pestaña 2
        m_SNoAdicion.UpdateAfterCallBack = true;
        m_EAdicionValor.UpdateAfterCallBack = true;
        m_EFechaAdicion.UpdateAfterCallBack = true;
        m_ECDPAdicion.UpdateAfterCallBack = true;
        m_EFechaCDP.UpdateAfterCallBack = true;
        m_ERDPAdicion.UpdateAfterCallBack = true;
        m_EFechaRDP.UpdateAfterCallBack = true;
        m_EProrroga.UpdateAfterCallBack = true;
        m_EFechaProrroga.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_EObservacionesAdicion.UpdateAfterCallBack = true;
        m_LCAdiciones.UpdateAfterCallBack = true;
        m_nIdAdicion.UpdateAfterCallBack = true;
        // pestaña 3
        m_SNIT.UpdateAfterCallBack = true;

        // pestaña 4
        m_EValorDesembolso.UpdateAfterCallBack = true;
        m_EFechaDesembolso.UpdateAfterCallBack = true;
        m_EComprobanteDesembolso.UpdateAfterCallBack = true;
        m_EFechaComprobanteDesembolso.UpdateAfterCallBack = true;
        m_SBancoDesembolso.UpdateAfterCallBack = true;
        m_SCuentaDesembolso.UpdateAfterCallBack = true;
        m_STipoCuentaDesembolso.UpdateAfterCallBack = true;
        m_EObservacionesDesembolso.UpdateAfterCallBack = true;
        m_LCDesembolsos.UpdateAfterCallBack = true;
        m_nIdDesembolso.UpdateAfterCallBack = true;
        // pestaña 5
        m_EValorRendimiento.UpdateAfterCallBack = true;
        m_CBMes.UpdateAfterCallBack = true;
        m_LCAno.UpdateAfterCallBack = true;
        m_EFechaComprobanteRendimiento.UpdateAfterCallBack = true;
        m_CBEstadoRendimiento.UpdateAfterCallBack = true;
        m_EObservacionesRendimiento.UpdateAfterCallBack = true;
        m_nIdRendimiento.UpdateAfterCallBack = true;
        m_LCRendimientos.UpdateAfterCallBack = true;
        // pestaña 6
        m_CInformeFinal.UpdateAfterCallBack = true;
        m_EFechaInforme.UpdateAfterCallBack = true;
        m_CDocumentosCompletos.UpdateAfterCallBack = true;
        m_CActaLiquidacion.UpdateAfterCallBack = true;
        m_EFechaActa.UpdateAfterCallBack = true;
        m_EObservacionesLiquidacion.UpdateAfterCallBack = true;
    }


    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        SelectInCombo(m_CBEnte, row[2].ToString());
        FillComboBox(m_CBConvenio, "SELECT id,codigo FROM conveniosm WHERE vigencia='" + m_pUsuario.GetVigenciaActual()
            + "' AND eterritorial=" + m_CBEnte.SelectedValue, true);
        SelectInCombo(m_CBConvenio, row[3].ToString());
        m_STitulo.Text = "Convenio " + row[3].ToString();
        m_SValor.Text = GetFormatDouble(row[5].ToString());
        SelectInCombo(m_CBEstadoConvenio, row[14].ToString());
        m_EObservaciones.Text = row[15].ToString();

        // pestaña 2
        m_BEliminarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.Style["display"] = "none";

        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarObjeto.Style["display"] = "";
            m_BInsertarObjeto.UpdateAfterCallBack = true;
        }

        Session["idliquidacion"] = row[16].ToString();
        Session["idconvenio"] = m_nId.Value;
        m_nIdAdicion.Value = "";
        m_LCAdiciones.DataBind();
        m_SNoAdicion.Text = CAdicionesConvenio.GetNumero().ToString();

        // pestaña 3
        m_LCDocumentos.DataBind();
        // pestaña 4
        m_nIdDesembolso.Value = "";
        m_LCDesembolsos.DataBind();
        m_BEliminarDesembolso.Style["display"] = "none";
        m_BAceptarDesembolso.Style["display"] = "none";
        m_BCancelarDesembolso.Style["display"] = "none";

        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarDesembolso.Style["display"] = "";
            m_BInsertarDesembolso.UpdateAfterCallBack = true;
        }

        // pestaña 5
        CRendimientosConvenio.RebuilTable();
        m_nIdRendimiento.Value = "";
        m_LCRendimientos.DataBind();
        m_BEliminarRendimiento.Style["display"] = "none";
        m_BAceptarRendimiento.Style["display"] = "none";
        m_BCancelarRendimiento.Style["display"] = "none";

        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarRendimiento.Style["display"] = "";
            m_BInsertarRendimiento.UpdateAfterCallBack = true;
        }
        // pestaña 6

        m_CInformeFinal.Checked = row[17].ToString() == "True";
        m_EFechaInforme.Text = GetFechaCorta(row[18].ToString());
        m_CDocumentosCompletos.Checked = row[19].ToString() == "True";
        m_CActaLiquidacion.Checked = row[20].ToString() == "True";
        m_EFechaActa.Text = GetFechaCorta(row[21].ToString());
        m_EObservacionesLiquidacion.Text = row[22].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_STitulo.Text = "Seleccione un convenio";
        m_nId.Value = "";
        m_CBConvenio.SelectedIndex = 0;
        m_SValor.Text = "";
        m_EObservaciones.Text = "";
        // pestaña 2
        m_SNoAdicion.Text = "";
        m_EAdicionValor.Text = "";
        m_EFechaAdicion.Text = "";
        m_ECDPAdicion.Text = "";
        m_EFechaCDP.Text = "";
        m_ERDPAdicion.Text = "";
        m_EFechaRDP.Text = "";
        m_EProrroga.Text = "";
        m_EFechaProrroga.Text = "";
        m_CBEstado.SelectedIndex = 0;
        m_EObservacionesAdicion.Text = "";
        m_CBEnte.SelectedIndex = 0;
        m_CBConvenio.SelectedIndex = 0;

        Session["idconvenio"] = "";
        Session["idliquidacion"] = "";
        m_nIdAdicion.Value = "";
        m_LCAdiciones.DataBind();

        m_SNoAdicion.Text = CAdicionesConvenio.GetNumero().ToString();

        m_BEliminarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.Style["display"] = "none";
        m_BInsertarObjeto.Style["display"] = "none";

        m_BInsertarObjeto.UpdateAfterCallBack = true;

        // pestaña 3
        m_SNIT.Text = "";
        m_LCDocumentos.DataBind();

        // pestaña 4
        m_EValorDesembolso.Text = "";
        m_EFechaDesembolso.Text = "";
        m_EComprobanteDesembolso.Text = "";
        m_EFechaComprobanteDesembolso.Text = "";
        m_SBancoDesembolso.Text = "";
        m_SCuentaDesembolso.Text = "";
        m_STipoCuentaDesembolso.Text = "";
        m_EObservacionesDesembolso.Text = "";
        m_nIdDesembolso.Value = "";
        m_LCDesembolsos.DataBind();
        m_BEliminarDesembolso.Style["display"] = "none";
        m_BAceptarDesembolso.Style["display"] = "none";
        m_BCancelarDesembolso.Style["display"] = "none";
        m_BInsertarDesembolso.Style["display"] = "none";

        m_BInsertarDesembolso.UpdateAfterCallBack = true;
        // pestaña 5
        m_EValorRendimiento.Text = "";
        m_CBMes.SelectedIndex = 0;
        m_LCAno.SelectedIndex = 0;
        m_EFechaComprobanteRendimiento.Text = "";
        m_CBEstadoRendimiento.SelectedIndex = 0;
        m_EObservacionesRendimiento.Text = "";
        m_nIdRendimiento.Value = "";
        m_LCRendimientos.DataBind();
        m_BEliminarRendimiento.Style["display"] = "none";
        m_BAceptarRendimiento.Style["display"] = "none";
        m_BCancelarRendimiento.Style["display"] = "none";
        m_BInsertarRendimiento.Style["display"] = "none";
        m_BInsertarRendimiento.UpdateAfterCallBack = true;

        m_CInformeFinal.Checked = false;
        m_EFechaInforme.Text = "";
        m_CDocumentosCompletos.Checked = false;
        m_CActaLiquidacion.Checked = false;
        m_EFechaActa.Text = "";
        m_EObservacionesLiquidacion.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected bool ActualizarLiquidacion()
    {
        m_szTabla = "liquidacionconvenio";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0] = "convenio";
        m_saCampos[1] = "informe";
        m_saCampos[2] = "fechainforme";
        m_saCampos[3] = "documentos";
        m_saCampos[4] = "actaliquidacion";
        m_saCampos[5] = "fechaliquidacion";
        m_saCampos[6] = "observaciones";
        m_saDatos[0] = m_nId.Value;
        m_saDatos[1] = m_CInformeFinal.Checked ? "1" : "0";
        m_saDatos[2] = GetDate(m_EFechaInforme.Text);
        m_saDatos[3] = m_CDocumentosCompletos.Checked ? "1" : "0";
        m_saDatos[4] = m_CActaLiquidacion.Checked ? "1" : "0";
        m_saDatos[5] = GetDate(m_EFechaActa.Text);
        m_saDatos[6] = GetString(m_EObservacionesLiquidacion.Text);

        string liquidadcion = Session["idliquidacion"] == null ? "" : Session["idliquidacion"].ToString();
        string query = "";
        if (liquidadcion.Length == 0)
        {
            query = CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos);
        } else {
            m_saDatos[0] = "";
            query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("convenio", m_nId.Value);
        }
        m_szTabla = "conveniosm";
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool ActualizarDocumentos()
    {
        DataTable tabla = CDocumentosConvenio.GetTabla();
        for (int i = 0; i < m_saCampos.Length; i++)
        {
            m_saCampos[i] = m_saDatos[i] = "";
        }
        // variosconveniom
        // documentosconveniom
        m_saCampos[0] = "convenio";
        m_saDatos[0] = m_nId.Value;
        m_saCampos[1] = "documento";
        m_saCampos[2] = "fecha";
        m_saCampos[3] = "numero";
        m_saCampos[4] = "observaciones";
        string query = "";
        bool check;
        for (int i = 0; i < m_LCDocumentos.Rows.Count; i++)
        {
            DataRow row = tabla.Rows[i];
            query = "";
            check = ((CheckBox)m_LCDocumentos.Rows[i].Cells[0].Controls[1]).Checked;
            m_saDatos[1] = row["id"].ToString();
            m_saDatos[2] = ((TextBox)m_LCDocumentos.Rows[i].Cells[3].Controls[1]).Text;
            m_saDatos[3] = ((TextBox)m_LCDocumentos.Rows[i].Cells[4].Controls[1]).Text;
            m_saDatos[4] = ((TextBox)m_LCDocumentos.Rows[i].Cells[5].Controls[1]).Text;

            if (row != null && row[1].ToString() != "")
            {
                if (check) // actualizar
                {
                    query = CConector.CreateUpdate("documentosconveniom", m_saCampos, m_saDatos) + CConector.GetCondicion("id", row[1].ToString());
                }
                else
                {
                    query = "DELETE FROM documentosconveniom WHERE id=" + row[1].ToString();
                }
            }
            else if (check)
            {
                query = CConector.CrearInsercion("documentosconveniom", m_saCampos, m_saDatos);
            }
            if (query.Length > 0 && m_pConector.QueryMod(query) == -1)
            {
                return false;
            }
        }
        return true;
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        m_saCampos[3] = "";
        m_saCampos[6] = "";
        m_saCampos[8] = "";
        m_saCampos[10] = "";
        m_saDatos[14] = GetSeleccion(m_CBEstadoConvenio.SelectedValue);
        m_saDatos[15] = GetString(m_EObservaciones.Text);

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            if (ActualizarDocumentos() && ActualizarLiquidacion())
            {
                MultiView1.ActiveViewIndex = 0;
                SetView(0);
                base.AfterUpdate("Convenio " + m_CBConvenio.SelectedItem.Text + " Editado");
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
        m_CBConvenio.UpdateAfterCallBack = true;
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBConvenio_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
        if (m_CBConvenio.SelectedIndex > 0)
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
    protected void m_CBEstadoConvenio_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }

    // procesos con las adicioens
    protected void m_BInsertarObjeto_Click(object sender, EventArgs e)
    {
        string[] campos = new string[10];
        string[] datos= new string[10];

        campos[0] = "convenio";
        campos[1] = "valor";
        campos[2] = "plazo";
        campos[3] = "fecha";
        campos[4] = "cdp";
        campos[5] = "fechacdp";
        campos[6] = "rdp";
        campos[7] = "fechardp";
        campos[8] = "estado";
        campos[9] = "observaciones";

        datos[0]=m_nId.Value;
        datos[1] = GetDouble(m_EAdicionValor.Text);
        datos[2] = m_EProrroga.Text;
        datos[3] = m_EAdicionValor.Text == "" ? m_EFechaProrroga.Text : m_EFechaAdicion.Text;
        datos[4] = m_ECDPAdicion.Text;
        datos[5] = m_EFechaCDP.Text;
        datos[6] = m_ERDPAdicion.Text;
        datos[7] = m_EFechaRDP.Text;
        datos[8] = m_CBEstado.SelectedValue;
        datos[9] = GetString(m_EObservacionesAdicion.Text);

        m_pConector.QueryMod(CConector.CrearInsercion("adicionesconvenio", campos, datos));
        m_BCancelarObjeto_Click(sender, e);
    }

    protected void m_BAceptarObjeto_Click(object sender, EventArgs e)
    {
        string[] campos = new string[10];
        string[] datos = new string[10];

        campos[0] = "convenio";
        campos[1] = "valor";
        campos[2] = "plazo";
        campos[3] = "fecha";
        campos[4] = "cdp";
        campos[5] = "fechacdp";
        campos[6] = "rdp";
        campos[7] = "fechardp";
        campos[8] = "estado";
        campos[9] = "observaciones";

        datos[0] = m_nId.Value;
        datos[1] = GetDouble(m_EAdicionValor.Text);
        datos[2] = m_EProrroga.Text;
        datos[3] = m_EAdicionValor.Text == "" ? m_EFechaProrroga.Text : m_EFechaAdicion.Text;
        datos[4] = m_ECDPAdicion.Text;
        datos[5] = m_EFechaCDP.Text;
        datos[6] = m_ERDPAdicion.Text;
        datos[7] = m_EFechaRDP.Text;
        datos[8] = m_CBEstado.SelectedValue;
        datos[9] = GetString(m_EObservacionesAdicion.Text);

        m_pConector.QueryMod(CConector.CreateUpdate("adicionesconvenio", campos, datos) + CConector.GetCondicion("id", m_nIdAdicion.Value));
        m_BCancelarObjeto_Click(sender, e);
    }

    protected void m_BCancelarObjeto_Click(object sender, EventArgs e)
    {
        m_BEliminarObjeto.Style["display"] = "none";
        m_BEliminarObjeto.UpdateAfterCallBack = true;
        m_BAceptarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.UpdateAfterCallBack = true;
        m_BCancelarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.UpdateAfterCallBack = true;
        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarObjeto.Style["display"] = "";
            m_BInsertarObjeto.UpdateAfterCallBack = true;
        }

        m_nIdAdicion.Value = "";
        m_EAdicionValor.Text = "";
        m_EFechaAdicion.Text = "";
        m_ECDPAdicion.Text = "";
        m_EFechaCDP.Text = "";
        m_ERDPAdicion.Text = "";
        m_EFechaRDP.Text = "";
        m_EProrroga.Text = "";
        m_EFechaProrroga.Text = "";
        m_CBEstado.SelectedIndex = 0;
        m_EObservacionesAdicion.Text = "";

        CAdicionesConvenio.RebuilTable();
        m_LCAdiciones.DataBind();
        m_LCAdiciones.SelectedIndex = -1;
        m_LCAdiciones.UpdateAfterCallBack = true;

        m_SNoAdicion.Text = CAdicionesConvenio.GetNumero().ToString();

        m_nIdAdicion.UpdateAfterCallBack = true;
        m_SNoAdicion.UpdateAfterCallBack = true;
        m_EAdicionValor.UpdateAfterCallBack = true;
        m_EFechaAdicion.UpdateAfterCallBack = true;
        m_ECDPAdicion.UpdateAfterCallBack = true;
        m_EFechaCDP.UpdateAfterCallBack = true;
        m_ERDPAdicion.UpdateAfterCallBack = true;
        m_EFechaRDP.UpdateAfterCallBack = true;
        m_EProrroga.UpdateAfterCallBack = true;
        m_EFechaProrroga.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_EObservacionesAdicion.UpdateAfterCallBack = true;
    }

    protected void m_BEliminarObjeto_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM adicionesconvenio WHERE id=" + m_nIdAdicion.Value);
        m_BCancelarObjeto_Click(sender, e);
    }

    protected void m_LCAdiciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow row = GetDataRow(m_LCAdiciones, CAdicionesConvenio.GetTabla());
        if (row == null)
            return;

        m_nIdAdicion.Value = m_LCAdiciones.SelectedDataKey.Value.ToString();
        m_SNoAdicion.Text = row[0].ToString();
        m_EAdicionValor.Text= GetFormatDouble(row[2].ToString());
        m_EFechaAdicion.Text = GetFechaCorta(row[3].ToString());
        m_ECDPAdicion.Text= row[4].ToString();
        m_EFechaCDP.Text= GetFechaCorta(row[5].ToString());
        m_ERDPAdicion.Text= row[6].ToString();
        m_EFechaRDP.Text= GetFechaCorta(row[7].ToString());
        m_EProrroga.Text= row[8].ToString();
        m_EFechaProrroga.Text= GetFechaCorta(row[9].ToString());
        SelectInCombo(m_CBEstado, row[10].ToString());
        m_EObservacionesAdicion.Text= row[11].ToString();        

        m_nIdAdicion.UpdateAfterCallBack = true;
        m_SNoAdicion.UpdateAfterCallBack = true;
        m_EAdicionValor.UpdateAfterCallBack = true;
        m_EFechaAdicion.UpdateAfterCallBack = true;
        m_ECDPAdicion.UpdateAfterCallBack = true;
        m_EFechaCDP.UpdateAfterCallBack = true;
        m_ERDPAdicion.UpdateAfterCallBack = true;
        m_EFechaRDP.UpdateAfterCallBack = true;
        m_EProrroga.UpdateAfterCallBack = true;
        m_EFechaProrroga.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_EObservacionesAdicion.UpdateAfterCallBack = true;

        if ((m_nPermisos & 16) == 16)
        {
            m_BEliminarObjeto.Style["display"] = "";
            m_BEliminarObjeto.UpdateAfterCallBack = true;
        }

        if((m_nPermisos&8)==8) 
        {
            m_BAceptarObjeto.Style["display"] = "";
            m_BAceptarObjeto.UpdateAfterCallBack = true;
        }
        m_BCancelarObjeto.Style["display"] = "";
        m_BCancelarObjeto.UpdateAfterCallBack = true;
        m_BInsertarObjeto.Style["display"] = "none";
        m_BInsertarObjeto.UpdateAfterCallBack = true;
    }

    protected void m_LCAdiciones_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow(m_LCAdiciones, CAdicionesConvenio.GetTabla(), e);
    }
    // pestaña 4

    protected void m_BInsertarDesembolso_Click(object sender, EventArgs e)
    {
        string[] campos = new string[7];
        string[] datos = new string[7];

        campos[0] = "convenio";
        campos[1] = "numero";
        campos[2] = "valor";
        campos[3] = "fecha";
        campos[4] = "comprobante";
        campos[5] = "fechacomprobante";
        campos[6] = "observaciones";

        datos[0] = m_nId.Value;
        datos[1] = CDesembolsosConvenio.GetNumero().ToString();
        datos[2] = GetDouble(m_EValorDesembolso.Text);
        datos[3] = m_EFechaDesembolso.Text;
        datos[4] = m_EComprobanteDesembolso.Text;
        datos[5] = m_EFechaComprobanteDesembolso.Text;
        datos[6] = GetString(m_EObservacionesDesembolso.Text);

        m_pConector.QueryMod(CConector.CrearInsercion("desembolsosconvenio", campos, datos));
        m_BCancelarDesembolso_Click(sender, e);
    }
    protected void m_BAceptarDesembolso_Click(object sender, EventArgs e)
    {
        string[] campos = new string[7];
        string[] datos = new string[7];

        campos[0] = "convenio";
        campos[1] = "numero";
        campos[2] = "valor";
        campos[3] = "fecha";
        campos[4] = "comprobante";
        campos[5] = "fechacomprobante";
        campos[6] = "observaciones";

        datos[0] = "";
        datos[1] = "";
        datos[2] = GetDouble(m_EValorDesembolso.Text);
        datos[3] = GetDate(m_EFechaDesembolso.Text);
        datos[4] = GetString(m_EComprobanteDesembolso.Text);
        datos[5] = GetDate(m_EFechaComprobanteDesembolso.Text);
        datos[6] = GetString(m_EObservacionesDesembolso.Text);

        m_pConector.QueryMod(CConector.CreateUpdate("desembolsosconvenio", campos, datos) + CConector.GetCondicion("id", m_nIdDesembolso.Value));
        m_BCancelarDesembolso_Click(sender, e);
    }
    protected void m_BCancelarDesembolso_Click(object sender, EventArgs e)
    {
        m_BEliminarDesembolso.Style["display"] = "none";
        m_BEliminarDesembolso.UpdateAfterCallBack = true;
        m_BAceptarDesembolso.Style["display"] = "none";
        m_BAceptarDesembolso.UpdateAfterCallBack = true;
        m_BCancelarDesembolso.Style["display"] = "none";
        m_BCancelarDesembolso.UpdateAfterCallBack = true;
        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarDesembolso.Style["display"] = "";
            m_BInsertarDesembolso.UpdateAfterCallBack = true;
        }

        m_nIdDesembolso.Value = "";
        m_EValorDesembolso.Text = "";
        m_EFechaDesembolso.Text = "";
        m_EComprobanteDesembolso.Text = "";
        m_EFechaComprobanteDesembolso.Text = "";
        m_EObservacionesDesembolso.Text = "";

        CDesembolsosConvenio.RebuilTable();
        m_LCDesembolsos.DataBind();
        m_LCDesembolsos.SelectedIndex = -1;
        m_LCDesembolsos.UpdateAfterCallBack = true;

        m_EValorDesembolso.UpdateAfterCallBack = true;
        m_EFechaDesembolso.UpdateAfterCallBack = true;
        m_EComprobanteDesembolso.UpdateAfterCallBack = true;
        m_EFechaComprobanteDesembolso.UpdateAfterCallBack = true;
        m_EObservacionesDesembolso.UpdateAfterCallBack = true;
        m_nIdDesembolso.UpdateAfterCallBack = true;
    }
    protected void m_BEliminarDesembolso_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM desembolsosconvenio WHERE id=" + m_nIdDesembolso.Value);
        m_BCancelarDesembolso_Click(sender, e);
    }
    protected void m_LCDesembolsos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow row = GetDataRow(m_LCDesembolsos, CDesembolsosConvenio.GetTabla());
        if (row == null)
            return;

        m_nIdDesembolso.Value = m_LCDesembolsos.SelectedDataKey.Value.ToString();
        m_EValorDesembolso.Text = GetFormatDouble(row[2].ToString());
        m_EFechaDesembolso.Text = GetFechaCorta(row[3].ToString());
        m_EComprobanteDesembolso.Text = row[4].ToString();
        m_EFechaComprobanteDesembolso.Text = GetFechaCorta(row[5].ToString());
        m_EObservacionesDesembolso.Text = row[6].ToString();

        m_nIdDesembolso.UpdateAfterCallBack = true;

        m_EValorDesembolso.UpdateAfterCallBack = true;
        m_EFechaDesembolso.UpdateAfterCallBack = true;
        m_EComprobanteDesembolso.UpdateAfterCallBack = true;
        m_EFechaComprobanteDesembolso.UpdateAfterCallBack = true;
        m_EObservacionesDesembolso.UpdateAfterCallBack = true;

        if ((m_nPermisos & 16) == 16)
        {
            m_BEliminarDesembolso.Style["display"] = "";
            m_BEliminarDesembolso.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 8) == 8)
        {
            m_BAceptarDesembolso.Style["display"] = "";
            m_BAceptarDesembolso.UpdateAfterCallBack = true;
        }
        m_BCancelarDesembolso.Style["display"] = "";
        m_BCancelarDesembolso.UpdateAfterCallBack = true;
        m_BInsertarDesembolso.Style["display"] = "none";
        m_BInsertarDesembolso.UpdateAfterCallBack = true;
    }
    protected void m_LCDesembolsos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow(m_LCDesembolsos, CDesembolsosConvenio.GetTabla(), e);
    }
    // pestaña 5
    protected void m_BInsertarRendimiento_Click(object sender, EventArgs e)
    {
        string[] campos = new string[7];
        string[] datos = new string[7];

        campos[0] = "convenio";
        campos[1] = "valor";
        campos[2] = "mes";
        campos[3] = "ano";
        campos[4] = "fecha";
        campos[5] = "estado";
        campos[6] = "observaciones";

        datos[0] = m_nId.Value;
        datos[1] = GetDouble(m_EValorRendimiento.Text);
        datos[2] = m_CBMes.SelectedValue;
        datos[3] = m_LCAno.SelectedValue;
        datos[4] = m_EFechaComprobanteRendimiento.Text;
        datos[5] = m_CBEstadoRendimiento.SelectedValue;
        datos[6] = GetString(m_EObservacionesRendimiento.Text);

        m_pConector.QueryMod(CConector.CrearInsercion("rendimientosconvenio", campos, datos));
        m_BCancelarRendimiento_Click(sender, e);
    }

    protected void m_CBAceptarRendimiento_Click(object sender, EventArgs e)
    {
        string[] campos = new string[7];
        string[] datos = new string[7];

        campos[0] = "convenio";
        campos[1] = "valor";
        campos[2] = "mes";
        campos[3] = "ano";
        campos[4] = "fecha";
        campos[5] = "estado";
        campos[6] = "observaciones";

        datos[0] = "";
        datos[1] = GetDouble(m_EValorRendimiento.Text);
        datos[2] = m_CBMes.SelectedValue;
        datos[3] = m_LCAno.SelectedValue;
        datos[4] = m_EFechaComprobanteRendimiento.Text;
        datos[5] = m_CBEstadoRendimiento.SelectedValue;
        datos[6] = GetString(m_EObservacionesRendimiento.Text);

        m_pConector.QueryMod(CConector.CreateUpdate("rendimientosconvenio", campos, datos) + CConector.GetCondicion("id", m_nIdRendimiento.Value));
        m_BCancelarRendimiento_Click(sender, e);
    }
    protected void m_BCancelarRendimiento_Click(object sender, EventArgs e)
    {
        m_BEliminarRendimiento.Style["display"] = "none";
        m_BEliminarRendimiento.UpdateAfterCallBack = true;
        m_BAceptarRendimiento.Style["display"] = "none";
        m_BAceptarRendimiento.UpdateAfterCallBack = true;
        m_BCancelarRendimiento.Style["display"] = "none";
        m_BCancelarRendimiento.UpdateAfterCallBack = true;
        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarRendimiento.Style["display"] = "";
            m_BInsertarRendimiento.UpdateAfterCallBack = true;
        }

        m_nIdRendimiento.Value = "";
        m_EValorRendimiento.Text = "";
        m_CBMes.SelectedIndex=0;
        m_LCAno.SelectedIndex=0;
        m_EFechaComprobanteRendimiento.Text = "";
        m_CBEstadoRendimiento.SelectedIndex=0;
        m_EObservacionesRendimiento.Text = "";

        CRendimientosConvenio.RebuilTable();
        m_LCRendimientos.DataBind();
        m_LCRendimientos.SelectedIndex = -1;
        m_LCRendimientos.UpdateAfterCallBack = true;

        m_nIdRendimiento.UpdateAfterCallBack = true;
        m_EValorRendimiento.UpdateAfterCallBack = true;
        m_CBMes.UpdateAfterCallBack = true;
        m_LCAno.UpdateAfterCallBack = true;
        m_EFechaComprobanteRendimiento.UpdateAfterCallBack = true;
        m_CBEstadoRendimiento.UpdateAfterCallBack = true;
        m_EObservacionesRendimiento.UpdateAfterCallBack = true;
    }
    protected void m_BEliminarRendimiento_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM rendimientosconvenio WHERE id=" + m_nIdRendimiento.Value);
        m_BCancelarRendimiento_Click(sender, e);
    }
    protected void m_LCRendimientos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow row = GetDataRow(m_LCRendimientos, CRendimientosConvenio.GetTabla());
        if (row == null)
            return;

        m_nIdRendimiento.Value = m_LCRendimientos.SelectedDataKey.Value.ToString();
        m_EValorRendimiento.Text= GetFormatDouble(row[1].ToString());
        SelectInCombo(m_CBMes, row[2].ToString());
        SelectInCombo(m_LCAno, row[3].ToString());
        m_EFechaComprobanteRendimiento.Text = GetFechaCorta(row[4].ToString());
        SelectInCombo(m_CBEstadoRendimiento, row[5].ToString());
        m_EObservacionesRendimiento.Text = row[6].ToString();

        m_nIdRendimiento.UpdateAfterCallBack = true;

        m_EValorRendimiento.UpdateAfterCallBack = true;
        m_CBMes.UpdateAfterCallBack = true;
        m_LCAno.UpdateAfterCallBack = true;
        m_EFechaComprobanteRendimiento.UpdateAfterCallBack = true;
        m_CBEstadoRendimiento.UpdateAfterCallBack = true;
        m_EObservacionesRendimiento.UpdateAfterCallBack = true;

        if ((m_nPermisos & 16) == 16)
        {
            m_BEliminarRendimiento.Style["display"] = "";
            m_BEliminarRendimiento.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 8) == 8)
        {
            m_BAceptarRendimiento.Style["display"] = "";
            m_BAceptarRendimiento.UpdateAfterCallBack = true;
        }
        m_BCancelarRendimiento.Style["display"] = "";
        m_BCancelarRendimiento.UpdateAfterCallBack = true;
        m_BInsertarRendimiento.Style["display"] = "none";
        m_BInsertarRendimiento.UpdateAfterCallBack = true;
    }
    protected void m_LCRendimientos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow(m_LCRendimientos, CRendimientosConvenio.GetTabla(), e);
    }

    override protected void SetView(int index)
    {
        switch (index)
        {
            case 5:
                m_BFinalizarE.Style["display"] = m_BAceptar.Style["display"];
                break;
        }
        m_LCDesembolsos.Style["display"] = index == 3 ? "" : "none";
        m_LCAdiciones.Style["display"] = index == 1 ? "" : "none";
        m_LCRendimientos.Style["display"] = index == 4 ? "" : "none";
        m_LCResultados.Style["display"] = (index != 1 && index!=3 && index!=4) ? "" : "none";
        base.SetView(index);
    }
    protected void m_LCDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            TextBox text = (TextBox)e.Row.Cells[3].Controls[1];
            text.Attributes["readonly"] = "true";
            text.Text = GetFechaCorta(text.Text);
            HtmlInputButton boton = (HtmlInputButton)e.Row.Cells[3].Controls[2];
            boton.Attributes["onclick"] = "setFecha('" + m_LCDocumentos.ID + "_ctl" + ((e.Row.DataItemIndex + 2) < 10 ? "0" : "")
            + (e.Row.DataItemIndex + 2) + "_m_EFecha');";
        }
    }
}