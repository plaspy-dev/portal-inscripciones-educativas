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

public partial class inscripcion_inscripcion_inscripcion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectosi", 32))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "codigo";
        m_saCampos[2] = "eterritorial";
        m_saCampos[3] = "departamento";
        m_saCampos[4] = "municipio";
        m_saCampos[5] = "institucion";
        m_saCampos[6] = "nombreinstitucion";
        m_saCampos[7] = "criticidad";
        m_saCampos[8] = "tipo";
        m_saCampos[9] = "totalproyecto";
        m_saCampos[10] = "aportesmen";
        m_saCampos[11] = "pmen";
        m_saCampos[12] = "aportesmun";
        m_saCampos[13] = "paportesmun";
        m_saCampos[14] = "estudiantesb";
        m_saCampos[15] = "estudiantesd";
        m_saCampos[16] = "observaciones";
        m_saCampos[17] = "codigodane";
        m_saCampos[18] = "jornadas";
        m_saCampos[19] = "NoResolucion";
        m_saCampos[20] = "FechaResolucion";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Código";
        m_saTitulos[2] = "Entidad Territorial";
        m_saTitulos[3] = "Departamento";
        m_saTitulos[4] = "Municipio";
        m_saTitulos[5] = "ideducativa";
        m_saTitulos[6] = "Institución educativa";
        m_saTitulos[7] = "Criticidad";
        m_saTitulos[8] = "Tipo";
        m_saTitulos[9] = "Valor total proyecto";
        m_saTitulos[10] = "Valor aportes Entidad educativa";
        m_saTitulos[11] = "% Aportes Entidad educativa";
        m_saTitulos[12] = "Valor Cofinanciación";
        m_saTitulos[13] = "% Aportes Cofinanciación";
        m_saTitulos[14] = "Estudiantes beneficiados por aulas nuevas";
        m_saTitulos[15] = "Estudiantes beneficiados por dotación";
        m_saTitulos[16] = "Observaciones";
        m_saTitulos[17] = "Codigo DANE";
        // pestaña 2
        int anoactual = Convert.ToInt32(m_pUsuario.GetVigenciaActual());
        m_saTitulos[18] = "Jornadas";
        m_saTitulos[19] = "No. Resolución de Media";
        m_saTitulos[20] = "Fecha";
        m_saTitulos[21] = anoactual.ToString();
        m_saTitulos[22] = (anoactual - 1).ToString();
        m_saTitulos[23] = "Variación";
        m_saTitulos[24] = "Nombre rector";
        m_saTitulos[25] = "Dirección";
        m_saTitulos[26] = "Teléfono";
        m_saTitulos[27] = "Celular";
        m_saTitulos[28] = "Fax";
        m_saTitulos[29] = "E-Mail";
        m_saTitulos[30] = "ResEmergencia";
        m_saTitulos[31] = "FechaEmergencia";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[2] = "eterritoriales.nombre";
        m_saCamposAlias[3] = "departamentos.nombre";
        m_saCamposAlias[4] = "municipios.nombre";
        m_saCamposAlias[6] = "insteducativas.nombre";
        m_saCamposAlias[7] = "varios.nombre";
        m_saCamposAlias[8] = "varios1.nombre";
        m_saCamposAlias[9] = "SUM(objetoinscrito.valor)";
        m_saCamposAlias[10] = "(SUM(objetoinscrito.valor)*presupuestoi.porcentajeMEN)/100";
        m_saCamposAlias[11] = "CONVERT(VARCHAR, presupuestoi.porcentajeMEN)+'%'";
        m_saCamposAlias[12] = "(SUM(objetoinscrito.valor)*(100-presupuestoi.porcentajeMEN))/100";
        m_saCamposAlias[13] = "CONVERT(VARCHAR, (100-presupuestoi.porcentajeMEN))+'%'";
        m_saCamposAlias[17] = "insteducativas.codigo";
        m_saCamposAlias[18] = "institucionesvariacion.jornadas";
        m_saCamposAlias[21] = "institucionesvariacion.alumnos";
        m_saCamposAlias[22] = "last.alumnos";
        m_saCamposAlias[23] = "institucionesvariacion.alumnos-last.alumnos";
        m_saCamposAlias[24] = "rectores.nombre";
        m_saCamposAlias[25] = "rectores.direccion";
        m_saCamposAlias[26] = "rectores.telefono";
        m_saCamposAlias[27] = "rectores.celular";
        m_saCamposAlias[28] = "rectores.fax";
        m_saCamposAlias[29] = "rectores.email";
        m_saCamposAlias[30] = "proyectosemergencia.numero";
        m_saCamposAlias[31] = "proyectosemergencia.fecha";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectosi.eterritorial)"
            + " LEFT JOIN municipios ON (municipios.id=proyectosi.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectosi.institucion)"
            + " LEFT JOIN varios ON (varios.id=proyectosi.criticidad)"
            + " LEFT JOIN varios AS varios1 ON (varios1.id=proyectosi.tipo)"
            + " LEFT JOIN objetoinscrito ON (objetoinscrito.proyecto=proyectosi.id)"
            + " LEFT JOIN presupuestoi ON (presupuestoi.proyecto=proyectosi.id)"
            + " LEFT JOIN institucionesvariacion ON (institucionesvariacion.institucion=proyectosi.institucion AND institucionesvariacion.anolectivo=" + anoactual.ToString() + ")"
            + " LEFT JOIN institucionesvariacion AS last ON (last.institucion=proyectosi.institucion AND last.anolectivo=" + (anoactual + 1).ToString() + ")"
            + " LEFT JOIN rectores ON (rectores.institucion=proyectosi.institucion AND rectores.ano=" + anoactual.ToString() + ")"
            + " LEFT JOIN proyectosemergencia ON (proyectosemergencia.proyecto=proyectosi.id)"
            ;

        m_szCondicion = " proyectosi.vigencia='" +m_pUsuario.GetVigenciaActual() + "' AND (proyectosi.estudiantesb IS NULL OR proyectosi.estudiantesb!=-1) ";
        string gruoup = " GROUP BY ";
        for (int i = 0; i < 9; i++)
        {
            gruoup += m_saCamposAlias[i] + ",";
        }
        gruoup += "presupuestoi.porcentajeMEN";
        for (int i = 14; i < m_saCamposAlias.Length; i++)
        {
            if (i == 23)
                continue;
            gruoup += "," + m_saCamposAlias[i];
        }

        m_pUsuario.FillComboBox(m_EEnte);
        FillComboBox(m_EMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_EEnte.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        FillComboBox(m_CBJornadas, "SELECT nombre,nombre FROM varios WHERE tipo=41 AND (nReservado IS NULL OR nReservado!=-1) ");
        FillComboBox(m_CBCriticidad, "SELECT id,nombre FROM varios WHERE tipo=35 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBTipo, "SELECT id,nombre FROM varios WHERE tipo=13 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBUnidades, "SELECT id,nombre FROM varios WHERE tipo=42 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBConcepto, "SELECT id,nombre FROM varios WHERE tipo=15 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBPorcentaje, "SELECT nombre,nombre FROM varios WHERE tipo=43 AND (nReservado IS NULL OR nReservado!=-1)");


        // autofiltro
        if (m_EEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.eterritorial=" + m_EEnte.SelectedValue;
        else m_szCondicionEx += " AND " + m_pUsuario.GetCondicion("proyectosi.eterritorial");
        if (m_EMunicipio.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectosi.municipio=" + m_EMunicipio.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx + gruoup);
        m_szCondicion += gruoup;

        m_baHidden[0] = true;
        m_baHidden[5] = true;
        m_baHidden[16] = true;
        for (int i = 17; i < 29; i++)
            m_baHidden[i] = true;

        m_ECodigo.Attributes["onKeypress"] = "isnb();";
        m_EInstitucion.Attributes["onKeypress"] = "isnb();";

        m_ECantidad.Attributes["onKeypress"] = "isfloat(this);";
        m_ECantidad.Attributes["onblur"] = "tcomas(this);";
        m_ECantidad.Attributes["onfocus"] = "scomas(this);";
        m_EValor.Attributes["onKeypress"] = "isfloat(this);";
        m_EValor.Attributes["onblur"] = "tcomas(this);";
        m_EValor.Attributes["onfocus"] = "scomas(this);";
        m_EInstitucion.Attributes["onblur"] = "Verificacodigo(this);";

        if (!IsPostBack)
        {
            m_BEliminarObjeto.Style["display"] = "none";
            m_BAceptarObjeto.Style["display"] = "none";
            m_BCancelarObjeto.Style["display"] = "none";
            m_BInsertarObjeto.Style["display"] = "none";

            BoundField data = new BoundField();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LCObjetos.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Concepto";
            m_LCObjetos.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Cantidad";
            m_LCObjetos.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Unidades";
            m_LCObjetos.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Valor";
            m_LCObjetos.Columns.Add(data);
        }

        m_szMensaje = "No se encontraron proyectos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_ETitulo.UpdateAfterCallBack = true;
        m_nId.UpdateAfterCallBack = true;
        m_nIdInstitucion.UpdateAfterCallBack = true;
        m_ECodigo.UpdateAfterCallBack = true;
        m_EEnte.UpdateAfterCallBack = true;
        m_EMunicipio.UpdateAfterCallBack = true;
        m_EInstitucion.UpdateAfterCallBack = true;
        m_ENombreInstitucion.UpdateAfterCallBack = true;
        //pestaña 2.UpdateAfterCallBack = true;
        m_CBJornadas.UpdateAfterCallBack = true;
        m_EResolucion.UpdateAfterCallBack = true;
        m_EFechaResolucion.UpdateAfterCallBack = true;
        m_SActual.UpdateAfterCallBack = true;
        m_SAnterior.UpdateAfterCallBack = true;
        m_SVariacion.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_EDireccion.UpdateAfterCallBack = true;
        m_ETelefono.UpdateAfterCallBack = true;
        m_ECelular.UpdateAfterCallBack = true;
        m_EFax.UpdateAfterCallBack = true;
        m_EEmail.UpdateAfterCallBack = true;
        //pestaña 3.UpdateAfterCallBack = true;
        m_CBCriticidad.UpdateAfterCallBack = true;
        m_CBTipo.UpdateAfterCallBack = true;
        m_EResolucionEmergencia.UpdateAfterCallBack = true;
        m_EFechaEmergencia.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;
        m_ECantidad.UpdateAfterCallBack = true;
        m_CBUnidades.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_CBPorcentaje.UpdateAfterCallBack = true;
        m_EEstudiantesBeneficiados.UpdateAfterCallBack = true;
        m_EDotacion.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
        m_nIdObjeto.UpdateAfterCallBack = true;
        m_LCObjetos.UpdateAfterCallBack = true;
    }


    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        Session["idobjectos"] = m_nId.Value;
        m_LCObjetos.DataBind();
        // m_ENombre.Text = row[1].ToString();
        m_ETitulo.Text = "Proyecto " + row[1].ToString();
        m_ECodigo.Text = row[1].ToString();

        ListItem item = m_EEnte.Items.FindByText(row[2].ToString());
        m_EEnte.SelectedIndex = item == null ? 0 : m_EEnte.Items.IndexOf(item);
        FillComboBox(m_EMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_EEnte.SelectedValue + " AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre", true);
        SelectInCombo(m_EMunicipio,row[4].ToString());

        m_nIdInstitucion.Value = row[5].ToString();
        m_EInstitucion.Text = row[17].ToString();
        m_ENombreInstitucion.Text = row[6].ToString();
        // tab2
        item = m_CBJornadas.Items.FindByText(row[18].ToString());
        m_CBJornadas.SelectedIndex = item == null ? 0 : m_CBJornadas.Items.IndexOf(item);
        m_EResolucion.Text = row[19].ToString();
        m_EFechaResolucion.Text = GetFechaCorta(row[20].ToString());
        m_ENombre.Text = row[24].ToString();
        m_EDireccion.Text = row[25].ToString();
        m_ETelefono.Text = row[26].ToString();
        m_ECelular.Text = row[27].ToString();
        m_EFax.Text = row[28].ToString();
        m_EEmail.Text = row[29].ToString();
        // tab3

        item = m_CBCriticidad.Items.FindByText(row[7].ToString());
        m_CBCriticidad.SelectedIndex = item == null ? 0 : m_CBCriticidad.Items.IndexOf(item);
        item = m_CBTipo.Items.FindByText(row[8].ToString());
        m_CBTipo.SelectedIndex = item == null ? 0 : m_CBTipo.Items.IndexOf(item);
        m_EResolucionEmergencia.Text = row[30].ToString();
        m_EFechaEmergencia.Text = GetFechaCorta(row[31].ToString());

        item = m_CBPorcentaje.Items.FindByText(row[11].ToString());
        m_CBPorcentaje.SelectedIndex = item == null ? 0 : m_CBPorcentaje.Items.IndexOf(item);
        m_EEstudiantesBeneficiados.Text = row[14].ToString();
        m_EDotacion.Text = row[15].ToString();
        m_EObservaciones.Text = row[16].ToString();

        m_BEliminarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.Style["display"] = "none";

        m_BInsertarObjeto.Style["display"] = "";
        m_BInsertarObjeto.UpdateAfterCallBack = true;

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_ETitulo.Text = "Nuevo Proyecto";
        m_nId.Value = "";
        Session["idobjectos"] = "0";
        m_LCObjetos.DataBind();
        m_nIdObjeto.Value = "";
        m_ECodigo.Text = "";
        m_EInstitucion.Text = "";
        m_ENombreInstitucion.Text = "";
        m_nIdInstitucion.Value = "";
        m_EEnte.SelectedIndex = 0;
        m_EMunicipio.SelectedIndex = 0;
        //pestaña 2.Text = "";
        m_EResolucion.Text = "";
        m_EFechaResolucion.Text = "";
        m_SActual.Text = "";
        m_SAnterior.Text = "";
        m_SVariacion.Text = "";
        m_ENombre.Text = "";
        m_EDireccion.Text = "";
        m_ETelefono.Text = "";
        m_ECelular.Text = "";
        m_EFax.Text = "";
        m_EEmail.Text = "";
        //pestaña 3.Text = "";
        m_EResolucionEmergencia.Text = "";
        m_EFechaEmergencia.Text = "";
        m_ECantidad.Text = "";
        m_EValor.Text = "";
        m_EEstudiantesBeneficiados.Text = "";
        m_EDotacion.Text = "";
        m_EObservaciones.Text = "";


        m_BEliminarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.Style["display"] = "none";
        m_BInsertarObjeto.Style["display"] = "none";

        m_BInsertarObjeto.UpdateAfterCallBack = true;


        base.m_BCancelar_Click(sender, e);
    }

    protected bool UpdateJornadas()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saDatos[i] = m_saCampos[i] = "";
        SqlDataReader reader;
        if (m_CBJornadas.SelectedIndex > 0)
        {
            reader = m_pConector.Query("SELECT id FROM institucionesvariacion WHERE institucion=" + m_nIdInstitucion.Value + " AND anolectivo=" + m_pUsuario.GetVigenciaActual());
            if (reader != null && reader.Read())
            {
                return m_pConector.QueryMod("UPDATE institucionesvariacion SET jornadas=" + m_CBJornadas.SelectedValue + " WHERE id=" + reader[0].ToString()) != -1;
            }
            else
            {
                m_saCampos[0] = "institucion";
                m_saDatos[0] = m_nIdInstitucion.Value;
                m_saCampos[1] = "anolectivo";
                m_saDatos[1] = m_pUsuario.GetVigenciaActual();
                m_saCampos[2] = "jornadas";
                m_saDatos[2] = m_CBJornadas.SelectedValue;
                return m_pConector.QueryMod(CConector.CrearInsercion("institucionesvariacion", m_saCampos, m_saDatos)) != -1;
            }
        }
        return true;
    }

    protected bool UpdateResolucion()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saDatos[i] = m_saCampos[i] = "";
        m_saCampos[0] = "NoResolucion";
        m_saDatos[0] = GetString(m_EResolucion.Text);
        m_saCampos[1] = "FechaResolucion";
        m_saDatos[1] = GetDate(m_EFechaResolucion.Text);
        m_saCampos[2] = "criticidad";
        m_saDatos[2] = GetSeleccion(m_CBCriticidad.SelectedValue);
        m_saCampos[3] = "tipo";
        m_saDatos[3] = GetSeleccion(m_CBTipo.SelectedValue);
        m_saCampos[4] = "observaciones";
        m_saDatos[4] = GetString(m_EObservaciones.Text);
        m_saCampos[5] = "estudiantesb";
        m_saDatos[5] = m_EEstudiantesBeneficiados.Text;
        m_saCampos[6] = "estudiantesd";
        m_saDatos[6] = m_EDotacion.Text;

        return m_pConector.QueryMod(CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", m_nId.Value)) != -1;
    }

    protected bool UpdateRector()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saDatos[i] = m_saCampos[i] = "";

        m_saCampos[0] = "institucion";
        m_saDatos[0] = m_nIdInstitucion.Value;
        m_saCampos[1] = "ano";
        m_saDatos[1] = m_pUsuario.GetVigenciaActual();
        m_saCampos[2] = "nombre";
        m_saDatos[2] = m_ENombre.Text;
        m_saCampos[3] = "direccion";
        m_saDatos[3] = m_EDireccion.Text;
        m_saCampos[4] = "telefono";
        m_saDatos[4] = m_ETelefono.Text;
        m_saCampos[5] = "celular";
        m_saDatos[5] = m_ECelular.Text;
        m_saCampos[6] = "fax";
        m_saDatos[6] = m_EFax.Text;
        m_saCampos[7] = "email";
        m_saDatos[7] = m_EEmail.Text;

        SqlDataReader reader;
        reader = m_pConector.Query("SELECT id FROM rectores WHERE institucion=" + m_nIdInstitucion.Value + " AND ano=" + m_pUsuario.GetVigenciaActual());
        if (reader != null && reader.Read())
        {
            return m_pConector.QueryMod(CConector.CreateUpdate("rectores", m_saCampos, m_saDatos) + " WHERE id=" + reader[0].ToString()) != -1;
        }
        else return m_pConector.QueryMod(CConector.CrearInsercion("rectores", m_saCampos, m_saDatos)) != -1;
    }

    protected bool UpdateEmergencia()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saDatos[i] = m_saCampos[i] = "";

        //CREATE TABLE proyectosemergencia (
        //    proyecto BIGINT REFERENCES proyectosi,
        //    numero VARCHAR(255),
        //    fecha DATETIME,
        //    PRIMARY KEY(proyecto)
        //);

        m_saCampos[0] = "proyecto";
        m_saDatos[0] = m_nId.Value;
        m_saCampos[1] = "numero";
        m_saDatos[1] = m_EResolucionEmergencia.Text;
        m_saCampos[2] = "fecha";
        m_saDatos[2] = m_EFechaEmergencia.Text;

        if (m_saDatos[2].Length == 0 && m_saDatos[1].Length == 0)
        {
            return m_pConector.QueryMod("DELETE FROM proyectosemergencia WHERE proyecto=" + m_nId.Value) != -1;
        }
        SqlDataReader reader;
        reader = m_pConector.Query("SELECT proyecto FROM proyectosemergencia WHERE proyecto=" + m_nId.Value);
        if (reader != null && reader.Read())
        {
            return m_pConector.QueryMod(CConector.CreateUpdate("proyectosemergencia", m_saCampos, m_saDatos) + " WHERE proyecto=" + reader[0].ToString()) != -1;
        }
        else return m_pConector.QueryMod(CConector.CrearInsercion("proyectosemergencia", m_saCampos, m_saDatos)) != -1;
    }

    protected bool UpdatePresupuesto()
    {
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saDatos[i] = m_saCampos[i] = "";

        m_saCampos[0] = "proyecto";
        m_saDatos[0] = m_nId.Value;
        m_saCampos[1] = "porcentajeMEN";
        m_saDatos[1] = GetSeleccion(m_CBPorcentaje.SelectedValue);
        if (m_saDatos[1][0] != '%')
            m_saDatos[1]= m_saDatos[1].Replace("%", "");

        SqlDataReader reader;
        reader = m_pConector.Query("SELECT id FROM presupuestoi WHERE proyecto=" + m_nId.Value);
        if (reader != null && reader.Read())
        {
            return m_pConector.QueryMod(CConector.CreateUpdate("presupuestoi", m_saCampos, m_saDatos) + " WHERE id=" + reader[0].ToString()) != -1;
        }
        else return m_pConector.QueryMod(CConector.CrearInsercion("presupuestoi", m_saCampos, m_saDatos)) != -1;
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        if (m_nIdInstitucion.Value == "")
        {
            m_SMensaje.Text = "Por favor ingrese una institución";
            m_SMensaje.UpdateAfterCallBack = true;
            return;
        }
        m_saCampos[0] = "";

        m_saDatos[1] = m_ECodigo.Text;
        m_saDatos[2] = m_EEnte.SelectedValue;
        m_saCampos[3] = "vigencia";
        m_saDatos[3] = m_pUsuario.GetVigenciaActual();
        m_saDatos[4] = GetSeleccion(m_EMunicipio.SelectedValue);
        m_saDatos[5] = m_nIdInstitucion.Value;
        for (int i = 6; i < m_saCampos.Length; i++)
            m_saCampos[i] = "";

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;

        int id = m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos), true);
        if (id == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            m_nId.Value = id.ToString();
            Button boton = (Button)sender;
            if (boton != null && boton.ID == "m_BFinalizar")
            {
                // pestana 2
                if (UpdateJornadas() && UpdateResolucion() && UpdateRector() && UpdateEmergencia() && UpdatePresupuesto())
                {
                    base.AfterUpdate("Proyecto " + m_ECodigo.Text + " Editado");
                    MultiView1.ActiveViewIndex = 0;
                    SetView(0);
                }
                else
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                }
            }
            else base.AfterUpdate("Proyecto " + m_ECodigo.Text + " Ingresado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        if (m_nIdInstitucion.Value == "")
        {
            m_SMensaje.Text = "Por favor ingrese una institución";
            m_SMensaje.UpdateAfterCallBack = true;
            return;
        }
        m_saDatos[1] = m_ECodigo.Text;
        m_saDatos[2] = m_EEnte.SelectedValue;
        m_saDatos[4] = m_EMunicipio.SelectedValue;
        m_saDatos[5] = m_nIdInstitucion.Value;
        for (int i = 6; i < m_saCampos.Length; i++)
            m_saCampos[i] = "";
        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            if (UpdateJornadas() && UpdateResolucion() && UpdateRector() && UpdateEmergencia() && UpdatePresupuesto())
            {
                base.AfterUpdate("Proyecto " + m_ECodigo.Text + " Editado");
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

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM proyectosi WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Proyecto " + m_ECodigo.Text + " Eliminado");
        }
    }
    protected void m_EEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_EMunicipio.UpdateAfterCallBack = true;
        m_EMunicipio_SelectedIndexChanged(sender, e);
    }

    protected void m_EMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_BVerificar_Click(object sender, EventArgs e)
    {
        string id = "";
        string nombre = "No se encontro el código DANE " + m_EInstitucion.Text;
        SqlDataReader reader = m_pConector.Query("SELECT COUNT(*) FROM insteducativas WHERE codigo LIKE '" + m_EInstitucion.Text + "%'");
        if (reader != null && reader.Read())
        {
            if (Convert.ToInt32(reader[0]) == 1)
            {
                reader = m_pConector.Query("SELECT id,nombre,codigo FROM insteducativas WHERE codigo LIKE '" + m_EInstitucion.Text + "%'");
                if (reader != null && reader.Read())
                {
                    id = reader[0].ToString();
                    nombre = reader[1].ToString();
                    if (reader[2].ToString() != m_EInstitucion.Text)
                    {
                        m_EInstitucion.Text = reader[2].ToString();
                        m_EInstitucion.UpdateAfterCallBack = true;
                    }
                }
            }
        }
        m_nIdInstitucion.Value = id;
        m_nIdInstitucion.UpdateAfterCallBack = true;
        m_ENombreInstitucion.Text = nombre;
        m_ENombreInstitucion.UpdateAfterCallBack = true;
    }
    override protected void SetView(int index)
    {
        int inicio = 0, fin = 0;
        switch (index)
        {
            case 0:
                inicio = 2;
                fin = 18;
                break;
            case 1:
                inicio = 19;
                fin = 31;
                break;
            case 2:
                inicio = 2;
                fin = 18;
                m_BFinalizarE.Style["display"] = m_BAceptar.Style["display"];
                m_BFinalizar.Style["display"] = m_BAceptar.Style["display"] == "" ? "none" : "";
                break;
        }

        for (int i = 0; i < m_LCResultados.Columns.Count; i++)
        {
            m_LCResultados.Columns[i].Visible = false;
        }
        for (int i = inicio; i < fin; i++)
        {
            m_LCResultados.Columns[i].Visible = true;
        }
        switch (index)
        {
            case 0:
                m_LCResultados.Columns[0].Visible = true;
                m_LCResultados.Columns[6].Visible = false;
                m_LCResultados.Columns[9].Visible = false;
                break;
            case 1:
                m_LCResultados.Columns[2].Visible = true;
                m_LCResultados.Columns[3].Visible = true;
                m_LCResultados.Columns[4].Visible = true;
                m_LCResultados.Columns[5].Visible = true;
                m_LCResultados.Columns[7].Visible = true;
                break;
            case 2:
                m_LCResultados.Columns[4].Visible = false;
                m_LCResultados.Columns[6].Visible = false;
                break;
        }
        base.SetView(index);
    }

    protected void m_BInsertarObjeto_Click(object sender, EventArgs e)
    {
        string[] campos = new string[5];
        campos[0] = m_nId.Value;
        campos[1] = m_CBConcepto.SelectedValue;
        campos[2] = GetDouble(m_ECantidad.Text);
        campos[3] = m_CBUnidades.SelectedValue;
        campos[4] = GetDouble(m_EValor.Text);
        m_pConector.QueryMod(CConector.CrearInsercion("objetoinscrito", campos));
        m_BCancelarObjeto_Click(sender, e);
    }

    protected void m_BAceptarObjeto_Click(object sender, EventArgs e)
    {
        string[] campos = new string[4];
        string[] nombres = new string[4];
        nombres[0] = "concepto";
        campos[0] = m_CBConcepto.SelectedValue;
        nombres[1] = "cantidad";
        campos[1] = GetDouble(m_ECantidad.Text);
        nombres[2] = "unidades";
        campos[2] = m_CBUnidades.SelectedValue;
        nombres[3] = "valor";
        campos[3] = GetDouble(m_EValor.Text);
        m_pConector.QueryMod(CConector.CreateUpdate("objetoinscrito", nombres, campos) + CConector.GetCondicion("id", m_nIdObjeto.Value));
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
        m_BInsertarObjeto.Style["display"] = "";
        m_BInsertarObjeto.UpdateAfterCallBack = true;
        m_LCObjetos.DataBind();
        m_LCObjetos.UpdateAfterCallBack = true;

        m_nIdObjeto.Value = "";
        m_CBConcepto.SelectedIndex = 0;
        m_ECantidad.Text = "";
        m_CBUnidades.SelectedIndex = 0;
        m_EValor.Text = "";
        CTablaObjeto.RebuilTable();
        m_LCObjetos.DataBind();
        m_LCObjetos.SelectedIndex = -1;
        m_LCObjetos.UpdateAfterCallBack = true;

        m_nIdObjeto.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_CBUnidades.UpdateAfterCallBack = true;
        m_ECantidad.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;
    }

    protected void m_BEliminarObjeto_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM objetoinscrito WHERE id=" + m_nIdObjeto.Value);
        m_BCancelarObjeto_Click(sender, e);
    }

    protected void m_LCObjetos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow row = GetDataRow(m_LCObjetos, CTablaObjeto.GetTabla());
        if (row == null)
            return;

        m_nIdObjeto.Value = m_LCObjetos.SelectedDataKey.Value.ToString();
        ListItem item = m_CBConcepto.Items.FindByText(row[1].ToString());
        m_CBConcepto.SelectedIndex = item == null ? 0 : m_CBConcepto.Items.IndexOf(item);
        m_ECantidad.Text = GetFormatDouble(row[2].ToString());
        item = m_CBUnidades.Items.FindByText(row[3].ToString());
        m_CBUnidades.SelectedIndex = item == null ? 0 : m_CBUnidades.Items.IndexOf(item);
        m_EValor.Text = GetFormatDouble(row[4].ToString());

        m_nIdObjeto.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_CBUnidades.UpdateAfterCallBack = true;
        m_ECantidad.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;

        m_BEliminarObjeto.Style["display"] = "";
        m_BEliminarObjeto.UpdateAfterCallBack = true;
        m_BAceptarObjeto.Style["display"] = "";
        m_BAceptarObjeto.UpdateAfterCallBack = true;
        m_BCancelarObjeto.Style["display"] = "";
        m_BCancelarObjeto.UpdateAfterCallBack = true;
        m_BInsertarObjeto.Style["display"] = "none";
        m_BInsertarObjeto.UpdateAfterCallBack = true;
    }

    protected void m_LCObjetos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow(m_LCObjetos, CTablaObjeto.GetTabla(), e);
    }
}
