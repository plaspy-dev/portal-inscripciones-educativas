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

public partial class viabilizacion_proyectos_viabilizacion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectos", 49))
            return;

        m_saCampos[0] = "id";
        m_saCampos[2]="codigo";
        m_saCampos[3]="eterritorial";
        m_saCampos[5]="municipio";
        m_saCampos[6]="institucion";
        m_saCampos[7]="objeto";
        m_saCampos[13]="conceptoviabilizacion";
        m_saCampos[14]="estado";
        m_saCampos[15]="ultimarevision";
        m_saCampos[16]="observaciones";
        m_saCampos[17] = "fechaaprobacion";
        m_saCampos[18] = "tipo";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Código del proyecto";
        m_saTitulos[3] = "Entidad Territorial";
        m_saTitulos[4] = "Departamento";
        m_saTitulos[5] = "Municipio";
        m_saTitulos[6] = "Institución educativa";
        m_saTitulos[7] = "Objeto";
        m_saTitulos[8] = "Valor Total Proyecto";
        m_saTitulos[9] = "Valor Aportes Entidad educativa";
        m_saTitulos[10] = "%Aportes Entidad educativa";
        m_saTitulos[11] = "Valor aportes cofinanciación";
        m_saTitulos[12] = "%Aportes cofinanciación";
        m_saTitulos[13] = "Concepto de viabilidad";
        m_saTitulos[14] = "Estado";
        m_saTitulos[15] = "Fecha de última revisión";
        m_saTitulos[16] = "Observaciones";
        m_saTitulos[17] = "Fecha de aprobación";
        // pestaña 5
        m_saTitulos[18] = "Tipo";
        m_saTitulos[19] = "Numero de aulas nuevas";
        m_saTitulos[20] = "Numero de aulas reposición";
        m_saTitulos[21] = "Numero de estudiantes promedio aula";
        m_saTitulos[22] = "Numero de jornadas";
        m_saTitulos[23] = "Total ampliación de cobertura";
        m_saTitulos[24] = "Total estudiantes beneficiados aulas nuevas";
        m_saTitulos[25] = "Total estudiantes beneficiados aulas reposición";
        m_saTitulos[26] = "Numero de baños";
        m_saTitulos[27] = "Aparatos sanitarios";
        m_saTitulos[28] = "Numero sillas";
        m_saTitulos[29] = "Valor total dotación";
        m_saTitulos[30] = "Numero de estudiantes beneficiados por dotación";
        m_saTitulos[31] = "Observaciones I";

        m_saTitulos[32] = "M2 de aulas";
        m_saTitulos[33] = "Valor total aulas";
        m_saTitulos[34] = "Valor/m2 aula";
        m_saTitulos[35] = "M2 de baños";
        m_saTitulos[36] = "Valor total baños";
        m_saTitulos[37] = "Valor/m2 baños";
        m_saTitulos[38] = "M2 circulaciones";
        m_saTitulos[39] = "Valor total circulaciones";
        m_saTitulos[40] = "Valor/m2 circulaciones";
        m_saTitulos[41] = "Índice de ocupación";
        m_saTitulos[42] = "Índice construcción";
        m_saTitulos[43] = "Valor total proyecto";
        m_saTitulos[44] = "Area total proyecto";
        m_saTitulos[45]="Valor total M2 proyecto";
        m_saTitulos[46] = "Observaciones A";
        m_saTitulos[47]="Observaciones d";
        m_saTitulos[48]="Observaciones t";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[1] = "grupo.nombre";
        m_saCamposAlias[2] = "proyectos.codigo";
        m_saCamposAlias[3] = "eterritoriales.nombre";
        m_saCamposAlias[4] = "departamentos.nombre";
        m_saCamposAlias[5] = "municipios.nombre";
        m_saCamposAlias[6] = "insteducativas.nombre";
        m_saCamposAlias[7] = "dbo.fn_GetObjetos(proyectos.id)";
        m_saCamposAlias[8] = "SUM(objeto.valor)";
        m_saCamposAlias[9] = "presupuesto.valormen";
        m_saCamposAlias[10] = "(presupuesto.valormen*100)/SUM(objeto.valor)";
        m_saCamposAlias[11] = "SUM(objeto.valor)-presupuesto.valormen";
        m_saCamposAlias[12] = "((SUM(objeto.valor)-presupuesto.valormen)*100)/SUM(objeto.valor)";
        m_saCamposAlias[13] = "concepto.nombre";
        m_saCamposAlias[14] = "estado.nombre";

        // pestaña 5
        m_saCamposAlias[18] = "tipos.nombre";
        m_saCamposAlias[19] = "infraestructura.aulasn";
        m_saCamposAlias[20] = "infraestructura.aulasr";
        m_saCamposAlias[21] = "infraestructura.estudiantesp";
        m_saCamposAlias[22] = "infraestructura.jornadas";
        m_saCamposAlias[23] = "";
        m_saCamposAlias[24] = "";
        m_saCamposAlias[25] = "";
        m_saCamposAlias[26] = "infraestructura.banos";
        m_saCamposAlias[27] = "infraestructura.asanitarios";
        m_saCamposAlias[28] = "infraestructura.sillas";
        m_saCamposAlias[29] = "infraestructura.valord";
        m_saCamposAlias[30] = "";
        m_saCamposAlias[31] = "observacionesproyecto.infraestructura";

        // pestaña 6
        m_saCamposAlias[32] = "areas.aulas";
        m_saCamposAlias[33] = "valor.aulas";
        m_saCamposAlias[34] = "valor.m2aula";
        m_saCamposAlias[35] = "areas.banos";
        m_saCamposAlias[36] = "valor.banos";
        m_saCamposAlias[37] = "valor.m2banos";
        m_saCamposAlias[38] = "areas.circulaciones";
        m_saCamposAlias[39] = "valor.circulaciones";
        m_saCamposAlias[40] = "valor.m2circulacion";
        m_saCamposAlias[41] = "areas.indiceocupacion";
        m_saCamposAlias[42] = "areas.indiceconstruccion";
        m_saCamposAlias[43] = "";
        m_saCamposAlias[44] = "";
        m_saCamposAlias[45] = "";
        m_saCamposAlias[46] = "observacionesproyecto.valores";
        m_saCamposAlias[47] = "observacionesproyecto.documentos";
        m_saCamposAlias[48] = "observacionesproyecto.tecnicas";
        
        m_szIndice = m_saTitulos[0];
        m_pUsuario.FillComboBoxInscritos(m_CBEnte);
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_CBEnte.SelectedValue, true);
        FillComboBox(m_CBProyecto, "SELECT proyectosi.id,proyectosi.codigo FROM proyectosi LEFT JOIN varios ON varios.id=proyectosi.estado WHERE varios.nReservado=1 AND proyectosi.vigencia='" 
            +m_pUsuario.GetVigenciaActual() +"' AND proyectosi.eterritorial=" + m_CBEnte.SelectedValue + (m_CBMunicipio.SelectedIndex>0 ? (" AND proyectosi.municipio=" + m_CBMunicipio.SelectedValue) : "" ) , true);
        FillComboBox(m_CBConcepto, "SELECT id,nombre FROM varios WHERE tipo=15 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBConceptoViablizacion, "SELECT id,nombre FROM varios WHERE tipo=50 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBTipo, "SELECT id,nombre FROM varios WHERE tipo=13 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");
        FillComboBox(m_CBEstado, "SELECT id,nombre FROM varios WHERE tipo=60 AND (nReservado IS NULL OR nReservado!=-1) ORDER BY nombre");

        m_szJoin = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=proyectos.eterritorial) AND asignacionpresupuestal.vigencia='" + m_pUsuario.GetVigenciaActual() + "' "
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectos.eterritorial)"
            + " LEFT JOIN varios AS grupo ON (grupo.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion)"
            + " LEFT JOIN objeto ON (objeto.proyecto=proyectos.id)"
            + " LEFT JOIN presupuesto ON (presupuesto.proyecto=proyectos.id)"
            + " LEFT JOIN varios AS concepto ON (concepto.id=proyectos.conceptoviabilizacion)"
            + " LEFT JOIN varios AS estado ON (estado.id=proyectos.estado)"
            + " LEFT JOIN infraestructura ON (infraestructura.proyecto=proyectos.id) "
            + " LEFT JOIN observacionesproyecto ON (observacionesproyecto.proyecto=proyectos.id)"
            + " LEFT JOIN varios AS tipos ON (tipos.id=proyectos.tipo)"
            + " LEFT JOIN valor ON (valor.proyecto=proyectos.id)"
            + " LEFT JOIN areas ON (areas.proyecto=proyectos.id)"
            ;

        m_szCondicion = " proyectos.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("proyectos.eterritorial");
        // autofiltro
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBMunicipio.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.municipio=" + m_CBMunicipio.SelectedValue;
        if (m_CBProyecto.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.proyectoi=" + m_CBProyecto.SelectedValue;
        if (m_CBEstado.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.estado=" + m_CBEstado.SelectedValue;
        if (m_CBConceptoViablizacion.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.conceptoviabilizacion" + m_CBConceptoViablizacion.SelectedValue;

        string grupo= " GROUP BY ";
        for (int i = 0; i < 7; i++)
        {
            grupo += m_saCamposAlias[i] + ",";
        }
        for (int i = 13; i < m_saCamposAlias.Length; i++)
        {
            if (m_saCamposAlias[i].Length == 0)
            {
                m_saCamposAlias[i] = "''";
            }
            else grupo += m_saCamposAlias[i] + ",";
        }
        grupo += m_saCamposAlias[9];

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx+ grupo);
        m_szCondicion += grupo;
        m_baHidden[0] = true;
        m_baHidden[17] = true;
        // pestaña 5
        for (int i = 18; i < m_baHidden.Length; i++)
            m_baHidden[i] = true;

        m_EValor.Attributes["onKeypress"] = "isfloat(this);";
        m_EValor.Attributes["onblur"] = "tcomas(this);";
        m_EValor.Attributes["onfocus"] = "scomas(this);";

        m_ECantidad.Attributes["onKeypress"] = "isfloat(this);";
        m_ECantidad.Attributes["onblur"] = "tcomas(this);";
        m_ECantidad.Attributes["onfocus"] = "scomas(this);";

        m_EValorPartidaMEN.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorPartidaMEN.Attributes["onblur"] = "tcomas(this);";
        m_EValorPartidaMEN.Attributes["onfocus"] = "scomas(this);";

        // objeto
        if (!IsPostBack)
        {
            CTablaObjetoVia.RebuilTable();

            Session["idobjectos"] = "";
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
            data.DataField = data.HeaderText = "Valor";
            m_LCObjetos.Columns.Add(data);
        }
        // pestaña 5
        m_EAulasN.Attributes["onKeypress"] = "isnb();";
        m_EAulasR.Attributes["onKeypress"] = "isnb();";
        m_EEstudiantesP.Attributes["onKeypress"] = "isnb();";
        m_EJornadas.Attributes["onKeypress"] = "isnb();";
        m_STotalACobertura.Attributes["onKeypress"] = "isnb();";
        m_STotalBeneficiados.Attributes["onKeypress"] = "isnb();";
        m_SBeneficiadosReposicion.Attributes["onKeypress"] = "isnb();";
        m_EBanos.Attributes["onKeypress"] = "isnb();";
        m_EAparatosS.Attributes["onKeypress"] = "isnb();";
        m_ESillas.Attributes["onKeypress"] = "isnb();";

        m_EValorTotalD.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorTotalD.Attributes["onblur"] = "tcomas(this);";
        m_EValorTotalD.Attributes["onfocus"] = "scomas(this);";

        m_SBeneficiadosD.Attributes["onKeypress"] = "isnb();";

        //pestaña 6
        m_EAulas.Attributes["onKeypress"] = "isfloat(this);";
        m_EBanosV.Attributes["onKeypress"] = "isfloat(this);";
        m_ECirculaciones.Attributes["onKeypress"] = "isfloat(this);";
        m_EM2Aulas.Attributes["onKeypress"] = "isfloat(this);";
        m_EM2Banos.Attributes["onKeypress"] = "isfloat(this);";
        m_EM2Circulaciones.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorM2Aula.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorM2Banos.Attributes["onKeypress"] = "isfloat(this);";
        m_EValorM2Circulaciones.Attributes["onKeypress"] = "isfloat(this);";
        m_EIndiceOcupacion.Attributes["onKeypress"] = "isfloat(this);";
        m_EIndiceConstruccion2.Attributes["onKeypress"] = "isfloat(this);";

        m_EAulas.Attributes["onblur"] = "tcomas(this);";
        m_EBanosV.Attributes["onblur"] = "tcomas(this);";
        m_ECirculaciones.Attributes["onblur"] = "tcomas(this);";
        m_EM2Aulas.Attributes["onblur"] = "tcomas(this);";
        m_EM2Banos.Attributes["onblur"] = "tcomas(this);";
        m_EM2Circulaciones.Attributes["onblur"] = "tcomas(this);";
        m_EValorM2Aula.Attributes["onblur"] = "tcomas(this);";
        m_EValorM2Banos.Attributes["onblur"] = "tcomas(this);";
        m_EValorM2Circulaciones.Attributes["onblur"] = "tcomas(this);";
        m_EIndiceOcupacion.Attributes["onblur"] = "tcomas(this);";
        m_EIndiceConstruccion2.Attributes["onblur"] = "tcomas(this);";

        m_EAulas.Attributes["onfocus"] = "scomas(this);";
        m_EBanosV.Attributes["onfocus"] = "scomas(this);";
        m_ECirculaciones.Attributes["onfocus"] = "scomas(this);";
        m_EM2Aulas.Attributes["onfocus"] = "scomas(this);";
        m_EM2Banos.Attributes["onfocus"] = "scomas(this);";
        m_EM2Circulaciones.Attributes["onfocus"] = "scomas(this);";
        m_EValorM2Aula.Attributes["onfocus"] = "scomas(this);";
        m_EValorM2Banos.Attributes["onfocus"] = "scomas(this);";
        m_EValorM2Circulaciones.Attributes["onfocus"] = "scomas(this);";
        m_EIndiceOcupacion.Attributes["onfocus"] = "scomas(this);";
        m_EIndiceConstruccion2.Attributes["onfocus"] = "scomas(this);";

        m_szMensaje = "No se encontraron proyectos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_STitulo.UpdateAfterCallBack = true;
        m_nId.UpdateAfterCallBack = true;
        m_nIdInstitucion.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBProyecto.UpdateAfterCallBack = true;
        m_SInstitucion.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
        m_ENITFirma.UpdateAfterCallBack = true;
        m_ENombreFirma.UpdateAfterCallBack = true;
        m_ERepresentante.UpdateAfterCallBack = true;
        m_EDireccion.UpdateAfterCallBack = true;
        m_ETelefono.UpdateAfterCallBack = true;
        m_EFax.UpdateAfterCallBack = true;
        m_EMail.UpdateAfterCallBack = true;

        // pestana 2
        m_SValorTotal.UpdateAfterCallBack = true;
        m_EValorPartidaMEN.UpdateAfterCallBack = true;
        m_SPorcentajeMEN.UpdateAfterCallBack = true;
        m_SValorMun.UpdateAfterCallBack = true;
        m_SPorcentajeMun.UpdateAfterCallBack = true;
        m_CBConceptoViablizacion.UpdateAfterCallBack = true;
        m_EFechaAprobacion.UpdateAfterCallBack = true;

        // pestaña 3
        m_EObservacionesDocumentos.UpdateAfterCallBack = true;

        // pestaña 4
        m_EObservacionesTecnicas.UpdateAfterCallBack = true;

        // pestaña 5
        m_CBTipo.UpdateAfterCallBack = true;
        m_EAulasN.UpdateAfterCallBack = true;
        m_EAulasR.UpdateAfterCallBack = true;
        m_EEstudiantesP.UpdateAfterCallBack = true;
        m_EJornadas.UpdateAfterCallBack = true;
        m_STotalACobertura.UpdateAfterCallBack = true;
        m_STotalBeneficiados.UpdateAfterCallBack = true;
        m_SBeneficiadosReposicion.UpdateAfterCallBack = true;
        m_EBanos.UpdateAfterCallBack = true;
        m_EAparatosS.UpdateAfterCallBack = true;
        m_ESillas.UpdateAfterCallBack = true;
        m_EValorTotalD.UpdateAfterCallBack = true;
        m_SBeneficiadosD.UpdateAfterCallBack = true;
        m_EObservacionesInfraestructura.UpdateAfterCallBack = true;
        //pestaña 6
        m_SValorTotalProyecto.UpdateAfterCallBack = true;
        m_EAulas.UpdateAfterCallBack = true;
        m_EBanosV.UpdateAfterCallBack = true;
        m_ECirculaciones.UpdateAfterCallBack = true;
        m_SAreaTotal.UpdateAfterCallBack = true;
        m_EM2Aulas.UpdateAfterCallBack = true;
        m_EM2Banos.UpdateAfterCallBack = true;
        m_EM2Circulaciones.UpdateAfterCallBack = true;
        m_SValorM2Total.UpdateAfterCallBack = true;
        m_EValorM2Aula.UpdateAfterCallBack = true;
        m_EValorM2Banos.UpdateAfterCallBack = true;
        m_EValorM2Circulaciones.UpdateAfterCallBack = true;
        m_EIndiceOcupacion.UpdateAfterCallBack = true;
        m_EIndiceConstruccion2.UpdateAfterCallBack = true;
        m_EObservacionesValores.UpdateAfterCallBack = true;

    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;
        
        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_STitulo.Text = "Proyecto " + row[2].ToString();
        SelectInCombo(m_CBEnte, row[3].ToString());
        FillComboBox(m_CBMunicipio, "SELECT id,nombre FROM municipios WHERE eterritorial=" + m_CBEnte.SelectedValue, true);
        SelectInCombo(m_CBMunicipio, row[5].ToString());
        FillComboBox(m_CBProyecto, "SELECT proyectosi.id,proyectosi.codigo FROM proyectosi LEFT JOIN varios ON varios.id=proyectosi.estado WHERE varios.nReservado=1 AND proyectosi.vigencia='"
            + m_pUsuario.GetVigenciaActual() + "' AND proyectosi.eterritorial=" + m_CBEnte.SelectedValue + (m_CBMunicipio.SelectedIndex > 0 ? (" AND proyectosi.municipio=" + m_CBMunicipio.SelectedValue) : ""), true);
        SelectInCombo(m_CBProyecto, row[2].ToString());
        m_SInstitucion.Text = row[6].ToString();
        SelectInCombo(m_CBEstado, row[14].ToString());
        m_EObservaciones.Text= row[16].ToString();
        m_EFechaUltima.Text= row[15].ToString();

        // responsable
        SqlDataReader reader = m_pConector.Query("SELECT nit,nombre,representante,direccion,telefono,fax,email FROM responsableviabilizacion WHERE proyecto=" + m_nId.Value);
        if (reader != null && reader.Read())
        {
            m_ENITFirma.Text= reader[0].ToString();
            m_ENombreFirma.Text= reader[1].ToString();
            m_ERepresentante.Text= reader[2].ToString();
            m_EDireccion.Text= reader[3].ToString();
            m_ETelefono.Text= reader[4].ToString();
            m_EFax.Text= reader[5].ToString();
            m_EMail.Text= reader[6].ToString();
        }

        // pestaña2
        m_SValorTotal.Text = GetFormatDouble(row[8].ToString());
        m_EValorPartidaMEN.Text= GetFormatDouble(row[9].ToString());
        m_SPorcentajeMEN.Text= GetFormatDouble(row[10].ToString()) +"%";
        m_SValorMun.Text = GetFormatDouble(row[11].ToString());
        m_SPorcentajeMun.Text = GetFormatDouble(row[12].ToString())+"%";
        SelectInCombo(m_CBConceptoViablizacion, row[13].ToString());
        m_EFechaAprobacion.Text = GetFechaCorta(row[17].ToString());
        m_EObservaciones.Text= row[16].ToString();

        //objeto
        Session["idobjectos"] = m_nId.Value;
        m_LCObjetos.DataBind();
        m_LCObjetos.UpdateAfterCallBack = true;

        m_BEliminarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.Style["display"] = "none";

        if ((m_nPermisos & 4) == 4)
        {
            m_BInsertarObjeto.Style["display"] = "";
            m_BInsertarObjeto.UpdateAfterCallBack = true;
        }

        // pestaña 3
        Session["idproyecto"] = m_nId.Value;
        m_LCDocumentos.DataBind();

        // pestaña 4
        m_LCTecnicos.DataBind();

        // pestaña 5

        SelectInCombo(m_CBTipo, row[18].ToString());
        m_EAulasN.Text=row[19].ToString();
        m_EAulasR.Text=row[20].ToString();
        m_EEstudiantesP.Text=row[21].ToString();
        m_EJornadas.Text=row[22].ToString();
        m_STotalACobertura.Text = row[23].ToString();
        m_STotalBeneficiados.Text = row[24].ToString();
        m_SBeneficiadosReposicion.Text = row[25].ToString();
        m_EBanos.Text=row[26].ToString();
        m_EAparatosS.Text=row[27].ToString();
        m_ESillas.Text=row[28].ToString();
        m_EValorTotalD.Text= GetFormatDouble(row[29].ToString());
        m_SBeneficiadosD.Text = row[30].ToString();
        m_EObservacionesInfraestructura.Text=row[31].ToString();

        // pestana 6
        
        m_EM2Aulas.Text=GetFormatDouble(row[32].ToString());
        m_EAulas.Text=GetFormatDouble(row[33].ToString());
        m_EValorM2Aula.Text=GetFormatDouble(row[34].ToString());
        m_EM2Banos.Text=GetFormatDouble(row[35].ToString());
        m_EBanosV.Text=GetFormatDouble(row[36].ToString());
        m_EValorM2Banos.Text=GetFormatDouble(row[37].ToString());
        m_EM2Circulaciones.Text=GetFormatDouble(row[38].ToString());
        m_ECirculaciones.Text=GetFormatDouble(row[39].ToString());
        m_EValorM2Circulaciones.Text=GetFormatDouble(row[40].ToString());
        m_EIndiceOcupacion.Text=GetFormatDouble(row[41].ToString());
        m_EIndiceConstruccion2.Text=GetFormatDouble(row[42].ToString());
        m_SValorTotalProyecto.Text=GetFormatDouble(row[43].ToString());
        m_SAreaTotal.Text=GetFormatDouble(row[44].ToString());
        m_SValorM2Total.Text=GetFormatDouble(row[45].ToString());
        m_EObservacionesValores.Text = row[46].ToString();

        m_EObservacionesDocumentos.Text= row[47].ToString();
        m_EObservacionesTecnicas.Text = row[48].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_STitulo.Text = "Seleccione un Proyecto";
        m_nId.Value = "";
        m_nIdInstitucion.Value = "";
        m_SInstitucion.Text = "";
        m_CBEstado.SelectedIndex=0;
        m_EObservaciones.Text = "";
        m_ENITFirma.Text = "";
        m_ENombreFirma.Text = "";
        m_ERepresentante.Text = "";
        m_EDireccion.Text = "";
        m_ETelefono.Text = "";
        m_EFax.Text = "";
        m_EMail.Text = "";
        m_CBProyecto.SelectedIndex = 0;
        m_CBEnte.SelectedIndex = 0;
        m_CBMunicipio.SelectedIndex = 0;

        // pestaña 2
        m_SValorTotal.Text = "";
        m_EValorPartidaMEN.Text = "";
        m_SPorcentajeMEN.Text = "";
        m_SValorMun.Text = "";
        m_SPorcentajeMun.Text = "";
        m_CBConceptoViablizacion.SelectedIndex = 0;
        m_EFechaAprobacion.Text = "";

        // objeto
        Session["idobjectos"] = "0";
        m_LCObjetos.DataBind();
        m_nIdObjeto.Value = "";

        m_BEliminarObjeto.Style["display"] = "none";
        m_BAceptarObjeto.Style["display"] = "none";
        m_BCancelarObjeto.Style["display"] = "none";
        m_BInsertarObjeto.Style["display"] = "none";

        m_BInsertarObjeto.UpdateAfterCallBack = true;

        // pestaña 3
        m_EObservacionesDocumentos.Text = "";
        Session["idproyecto"] = "";
        m_LCDocumentos.DataBind();

        // pestaña 4
        m_EObservacionesTecnicas.Text = "";
        m_LCTecnicos.DataBind();
        // pestaña 5
        m_CBTipo.SelectedIndex=0;
        m_EAulasN.Text = "";
        m_EAulasR.Text = "";
        m_EEstudiantesP.Text = "";
        m_EJornadas.Text = "";
        m_STotalACobertura.Text = "";
        m_STotalBeneficiados.Text = "";
        m_SBeneficiadosReposicion.Text = "";
        m_EBanos.Text = "";
        m_EAparatosS.Text = "";
        m_ESillas.Text = "";
        m_EValorTotalD.Text = "";
        m_SBeneficiadosD.Text = "";
        m_EObservacionesInfraestructura.Text = "";

        //pestaña 6
        m_SValorTotalProyecto.Text = "";
        m_EAulas.Text = "";
        m_EBanosV.Text = "";
        m_ECirculaciones.Text = "";
        m_SAreaTotal.Text = "";
        m_EM2Aulas.Text = "";
        m_EM2Banos.Text = "";
        m_EM2Circulaciones.Text = "";
        m_SValorM2Total.Text = "";
        m_EValorM2Aula.Text = "";
        m_EValorM2Banos.Text = "";
        m_EValorM2Circulaciones.Text = "";
        m_EIndiceOcupacion.Text = "";
        m_EIndiceConstruccion2.Text = "";
        m_EObservacionesValores.Text = "";
        if(sender==null || sender.ToString()!="1")
            base.m_BCancelar_Click(sender, e);
    }

    protected bool InsertarTecnicos()
    {
        string szTabla = "aspectostecnicos";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0] = "proyecto";
        m_saDatos[0] = m_nId.Value;
        m_saCampos[1] = "aspecto";
        m_saCampos[2] = "cantidad";
        m_saCampos[3] = "observaciones";

        bool check;
        string query;

        DataTable tabla = CAspectosTecnicos.GetTabla();

        for (int i = 0; i < m_LCTecnicos.Rows.Count; i++)
        {

            DataRow row = tabla.Rows[i];
            query = "";
            check = ((CheckBox)m_LCTecnicos.Rows[i].Cells[0].Controls[1]).Checked;
            m_saDatos[1] = row["id"].ToString();
            m_saDatos[2] = GetString(((TextBox)m_LCTecnicos.Rows[i].Cells[2].Controls[1]).Text);
            m_saDatos[3] = GetString(((TextBox)m_LCTecnicos.Rows[i].Cells[3].Controls[1]).Text);

            if (row != null && row[1].ToString() != "")
            {
                if (check) // actualizar
                {
                    query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", row[1].ToString());
                }
                else
                {
                    query = "DELETE FROM " + szTabla + " WHERE id=" + row[1].ToString();
                }
            }
            else if (check)
            {
                query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
            }
            if (query.Length > 0 && m_pConector.QueryMod(query) == -1)
            {
                return false;
            }
        }
        return true;
    }

    protected bool InsertarDocumentos()
    {
        string szTabla = "documentoproyecto";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";
        
        m_saCampos[0] = "proyecto";
        m_saDatos[0] = m_nId.Value;
        m_saCampos[1] = "documento";
        m_saCampos[2] = "fecha";
        m_saCampos[3] = "numero";
        m_saCampos[4] = "observaciones";
        
        bool check;
        string query;

        DataTable tabla = CDocumentosProyecto.GetTabla();

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
                    query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", row[1].ToString());
                }
                else
                {
                    query = "DELETE FROM " + szTabla + " WHERE id=" + row[1].ToString();
                }
            }
            else if (check)
            {
                query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
            }
            if (query.Length > 0 && m_pConector.QueryMod(query) == -1)
            {
                return false;
            }
        }
        return true;
    }

    protected bool InsertarObservaciones(bool insertar)
    {
        string szTabla = "observacionesproyecto";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0] = "proyecto";
        m_saCampos[1] = "documentos";
        m_saCampos[2] = "tecnicas";
        m_saCampos[3] = "infraestructura";
        m_saCampos[4] = "valores";

        m_saDatos[0] = m_nId.Value;
        m_saDatos[1] = GetString(m_EObservacionesDocumentos.Text);
        m_saDatos[2] = GetString(m_EObservacionesTecnicas.Text);
        m_saDatos[3] = GetString(m_EObservacionesInfraestructura.Text);
        m_saDatos[4] = GetString(m_EObservacionesValores.Text);

        string query = "";
        if (insertar)
        {
            query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
        }
        else
        {
            m_saDatos[0] = "";
            query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool InsertarAreas(bool insertar)
    {
        string szTabla = "areas";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0] = "proyecto";
        m_saCampos[1]="aulas";
        m_saCampos[2]="banos";
        m_saCampos[3]="circulaciones";
        m_saCampos[4]="indiceocupacion";
        m_saCampos[5]="indiceconstruccion";

        m_saDatos[0] = m_nId.Value;
        m_saDatos[1]= GetDouble(m_EM2Aulas.Text);
        m_saDatos[2]= GetDouble(m_EM2Banos.Text);
        m_saDatos[3]= GetDouble(m_EM2Circulaciones.Text);
        m_saDatos[4]= GetDouble(m_EIndiceOcupacion.Text);
        m_saDatos[5] = GetDouble(m_EIndiceConstruccion2.Text);

        string query = "";
        if (insertar)
        {
            query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
        }
        else
        {
            m_saDatos[0] = "";
            query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool InsertarValores(bool insertar)
    {
        string szTabla = "valor";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0] = "proyecto";
        m_saCampos[1]="aulas";
        m_saCampos[2]="banos";
        m_saCampos[3]="circulaciones";
        m_saCampos[4]="m2aula";
        m_saCampos[5]="m2banos";
        m_saCampos[6]="m2circulacion";

        m_saDatos[0] = m_nId.Value;
        m_saDatos[1]= GetDouble(m_EAulas.Text);
        m_saDatos[2]= GetDouble(m_EBanosV.Text);
        m_saDatos[3]= GetDouble(m_ECirculaciones.Text);
        m_saDatos[4]= GetDouble(m_EValorM2Aula.Text);
        m_saDatos[5]= GetDouble(m_EValorM2Banos.Text);
        m_saDatos[6] = GetDouble(m_EValorM2Circulaciones.Text);

        string query = "";
        if (insertar)
        {
            query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
        }
        else
        {
            m_saDatos[0] = "";
            query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool InsertarInfraestructura(bool insertar)
    {

        string szTabla = "infraestructura";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0]="proyecto";
        m_saCampos[1]="aulasn";
        m_saCampos[2]="aulasr";
        m_saCampos[3]="estudiantesp";
        m_saCampos[4]="jornadas";
        m_saCampos[5]="banos";
        m_saCampos[6]="asanitarios";
        m_saCampos[7]="sillas";
        m_saCampos[8]="valord";

        m_saDatos[0]=m_nId.Value;
        m_saDatos[1]=m_EAulasN.Text;
        m_saDatos[2]=m_EAulasR.Text;
        m_saDatos[3]=m_EEstudiantesP.Text;
        m_saDatos[4]=m_EJornadas.Text;
        m_saDatos[5]=m_EBanos.Text;
        m_saDatos[6]=m_EAparatosS.Text;
        m_saDatos[7]=m_ESillas.Text;
        m_saDatos[8]=GetDouble(m_EValorTotalD.Text);

        string query = "";
        if (insertar)
        {
            query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
        }
        else
        {
            m_saDatos[0] = "";
            query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool InsertarPresupuesto(bool insertar)
    {
        string szTabla = "presupuesto";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0]="proyecto";
        m_saCampos[1]="valormen";

        m_saDatos[0] = m_nId.Value;
        m_saDatos[1] = GetDouble(m_EValorPartidaMEN.Text);
        string query = "";
        if (insertar)
        {
            query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
        }
        else
        {
            query = CConector.CreateUpdate(szTabla, m_saCampos[1], m_saDatos[1]) + CConector.GetCondicion(m_saCampos[0], m_saDatos[0]);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected bool InsertarResponsable(bool insertar)
    {
        string szTabla = "responsableviabilizacion";
        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCampos[i] = m_saDatos[i] = "";

        m_saCampos[0]="proyecto";
        m_saCampos[1]="nit";
        m_saCampos[2]="nombre";
        m_saCampos[3]="representante";
        m_saCampos[4]="direccion";
        m_saCampos[5]="telefono";
        m_saCampos[6]="fax";
        m_saCampos[7]="email";

        m_saDatos[0]=m_nId.Value;
        m_saDatos[1]=GetString(m_ENITFirma.Text);
        m_saDatos[2]=GetString(m_ENombreFirma.Text);
        m_saDatos[3]=GetString(m_ERepresentante.Text);
        m_saDatos[4]=GetString(m_EDireccion.Text);
        m_saDatos[5]=GetString(m_ETelefono.Text);
        m_saDatos[6]=GetString(m_EFax.Text);
        m_saDatos[7] = GetString(m_EMail.Text);
        string query = "";
        if (!insertar)
        {
            query = m_pConector.QueryString("SELECT proyecto FROM responsableviabilizacion WHERE proyecto=" + m_nId.Value);
            if (query == "")
                insertar = true;
        }

        if (insertar)
        {
            query = CConector.CrearInsercion(szTabla, m_saCampos, m_saDatos);
        }
        else
        {
            m_saCampos[0] = "";
            query = CConector.CreateUpdate(szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("proyecto", m_saDatos[0]);
        }
        return m_pConector.QueryMod(query) != -1;
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "proyectoi";
        m_saDatos[0] = m_CBProyecto.SelectedValue;
        m_saCampos[1] = "vigencia";
        m_saDatos[1] = m_pUsuario.GetVigenciaActual();
        m_saDatos[2] = m_CBProyecto.SelectedItem.Text;
        m_saDatos[3] = m_CBEnte.SelectedValue;
        m_saDatos[5] = m_CBMunicipio.SelectedValue;
        m_saDatos[6] = m_nIdInstitucion.Value;
        m_saDatos[13] = GetSeleccion(m_CBConceptoViablizacion.SelectedValue);
        m_saDatos[14] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[15] = "%GETDATE()";
        m_saDatos[16] = GetString(m_EObservaciones.Text);
        m_saDatos[17] = m_EFechaAprobacion.Text;
        m_saDatos[18] = GetSeleccion(m_CBTipo.SelectedValue);

        // incializar los datos a insertar
        int id= m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos), true);
        if ( id== -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            m_nId.Value = id.ToString();
            if (InsertarResponsable(true) && InsertarPresupuesto(true) && InsertarDocumentos() && InsertarTecnicos()
                && InsertarInfraestructura(true) && InsertarValores(true) && InsertarAreas(true) && InsertarObservaciones(true)
                )
            {
                base.AfterUpdate("Proyecto " + m_CBProyecto.SelectedItem.Text + " Ingresado");
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

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[13] = GetSeleccion(m_CBConceptoViablizacion.SelectedValue);
        m_saDatos[14] = GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[15] = "%GETDATE()";
        m_saDatos[16] = GetString(m_EObservaciones.Text);
        m_saDatos[17] = m_EFechaAprobacion.Text;
        m_saDatos[18] = GetSeleccion(m_CBTipo.SelectedValue);

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            if (InsertarResponsable(false) && InsertarPresupuesto(false) && InsertarDocumentos() && InsertarTecnicos()
                && InsertarInfraestructura(false) && InsertarValores(false) && InsertarAreas(false) && InsertarObservaciones(false))
            {
                base.AfterUpdate("Proyecto " + m_CBProyecto.SelectedItem.Text + " Editado");
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
        if (
            m_pConector.QueryMod("DELETE FROM areas WHERE proyecto=" + m_nId.Value) == -1 ||
            m_pConector.QueryMod("DELETE FROM valor WHERE proyecto=" + m_nId.Value) == -1 ||
            m_pConector.QueryMod("DELETE FROM infraestructura WHERE proyecto=" + m_nId.Value) == -1 ||
            m_pConector.QueryMod("DELETE FROM aspectostecnicos WHERE proyecto=" + m_nId.Value) == -1 ||
            m_pConector.QueryMod("DELETE FROM documentoproyecto WHERE proyecto=" + m_nId.Value) == -1 || 
            m_pConector.QueryMod("DELETE FROM presupuesto WHERE proyecto=" + m_nId.Value)==-1 ||
            m_pConector.QueryMod("DELETE FROM responsableviabilizacion WHERE proyecto=" + m_nId.Value)==-1 ||
            m_pConector.QueryMod("DELETE FROM " + m_szTabla + " WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Proyecto " + m_CBProyecto.SelectedItem.Text + " Eliminado");
        }
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBMunicipio.UpdateAfterCallBack = true;
        m_CBProyecto.UpdateAfterCallBack = true;

        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
        m_CBProyecto_SelectedIndexChanged(sender, e);
    }
    protected void m_CBMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBProyecto.UpdateAfterCallBack = true;
        m_CBProyecto_SelectedIndexChanged(sender, e);
    }
    protected void m_CBProyecto_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.UpdateAfterCallBack = true;
        int ente= m_CBEnte.SelectedIndex;
        int municipio = m_CBMunicipio.SelectedIndex;

        if (m_CBProyecto.SelectedIndex > 0)
        {
            m_LCResultados.SelectedIndex = m_LCResultados.Rows.Count > 0 ? 0 : -1;
            if (m_LCResultados.SelectedIndex == 0)
            {
                m_LCResultados_SelectedIndexChanged(m_LCResultados, e);
            }
            else
            {
                int sel = m_CBProyecto.SelectedIndex;
                m_BCancelar_Click("1", e);
                m_CBProyecto.SelectedIndex = sel;
            }
            SqlDataReader reader= m_pConector.Query("SELECT insteducativas.nombre,insteducativas.id FROM proyectosi LEFT JOIN insteducativas ON insteducativas.id=proyectosi.institucion WHERE proyectosi.id=" + m_CBProyecto.SelectedValue);
            if (reader != null && reader.Read())
            {
                m_SInstitucion.Text = reader[0].ToString();
                m_nIdInstitucion.Value = reader[1].ToString();
            }
        }
        else
        {
            m_SInstitucion.Text = "";
            m_nIdInstitucion.Value = "";
            m_BCancelar_Click("1", e);
            m_LCResultados.SelectedIndex = -1;
        }
        m_CBEnte.SelectedIndex = ente;
        m_CBMunicipio.SelectedIndex = municipio;
        m_SInstitucion.UpdateAfterCallBack = true;
        m_nIdInstitucion.UpdateAfterCallBack = true;
    }

    // funciones de objeto
    protected void m_BInsertarObjeto_Click(object sender, EventArgs e)
    {
        string[] campos = new string[5];
        campos[0] = m_nId.Value;
        campos[1] = m_CBConcepto.SelectedValue;
        campos[2] = GetDouble(m_ECantidad.Text);
        campos[3] = "";
        campos[4] = GetDouble(m_EValor.Text);
        m_pConector.QueryMod(CConector.CrearInsercion("objeto", campos));
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
        campos[2] = "";
        nombres[3] = "valor";
        campos[3] = GetDouble(m_EValor.Text);
        m_pConector.QueryMod(CConector.CreateUpdate("objeto", nombres, campos) + CConector.GetCondicion("id", m_nIdObjeto.Value));
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

        m_LCObjetos.DataBind();
        m_LCObjetos.UpdateAfterCallBack = true;

        m_nIdObjeto.Value = "";
        m_CBConcepto.SelectedIndex = 0;
        m_ECantidad.Text = "";
        m_EValor.Text = "";
        CTablaObjetoVia.RebuilTable();
        m_LCObjetos.DataBind();
        m_LCObjetos.SelectedIndex = -1;
        m_LCObjetos.UpdateAfterCallBack = true;

        // tabla principal
        CTablaEx.RebuilTable();
        m_LCResultados.DataBind();
        m_LCResultados.UpdateAfterCallBack = true;

        m_nIdObjeto.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_ECantidad.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;
    }

    protected void m_BEliminarObjeto_Click(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM objeto WHERE id=" + m_nIdObjeto.Value);
        m_BCancelarObjeto_Click(sender, e);
    }

    protected void m_LCObjetos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow row = GetDataRow(m_LCObjetos, CTablaObjetoVia.GetTabla());
        if (row == null)
            return;

        m_nIdObjeto.Value = m_LCObjetos.SelectedDataKey.Value.ToString();
        ListItem item = m_CBConcepto.Items.FindByText(row[1].ToString());
        m_CBConcepto.SelectedIndex = item == null ? 0 : m_CBConcepto.Items.IndexOf(item);
        m_ECantidad.Text = GetFormatDouble(row[2].ToString());
        m_EValor.Text = GetFormatDouble(row[4].ToString());

        m_nIdObjeto.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_ECantidad.UpdateAfterCallBack = true;
        m_CBConcepto.UpdateAfterCallBack = true;

        if ((m_nPermisos & 16) == 16)
        {
            m_BEliminarObjeto.Style["display"] = "";
            m_BEliminarObjeto.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 8) == 8)
        {
            m_BAceptarObjeto.Style["display"] = "";
            m_BAceptarObjeto.UpdateAfterCallBack = true;
        }
        m_BCancelarObjeto.Style["display"] = "";
        m_BCancelarObjeto.UpdateAfterCallBack = true;
        m_BInsertarObjeto.Style["display"] = "none";
        m_BInsertarObjeto.UpdateAfterCallBack = true;
    }

    protected void m_LCObjetos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow(m_LCObjetos, CTablaObjetoVia.GetTabla(), e);
    }

    // funciones de documentos
    protected void m_LBDocumentos_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            TextBox box = (TextBox)e.Item.Cells[3].Controls[0];
            box.Text = GetFechaCorta(box.Text);
        }
    }

    override protected void SetView(int index)
    {
        int inicio = 0, fin = 0;
        if (index == 3)
        {
            inicio = 19;
            fin = 33;
        }
        else if (index == 4)
        {
            inicio = 33;
            fin = m_saTitulos.Length-1;
            m_BFinalizarE.Style["display"] = m_BAceptar.Style["display"];
            if ((m_nPermisos & 4) == 4)
                m_BFinalizar.Style["display"] = m_BAceptar.Style["display"] == "" ? "none" : "";
            else m_BFinalizar.Style["display"] = "none";
            if ((m_nPermisos & 8) == 0)
                m_BFinalizar.Style["display"] = "none";
        } else 
        {
            inicio = 2;
            fin = 18;
        }

        for (int i = 0; i < m_LCResultados.Columns.Count; i++)
        {
            m_LCResultados.Columns[i].Visible = false;
        }
        for (int i = inicio; i < fin; i++)
        {
            m_LCResultados.Columns[i].Visible = true;
        }
        if (index == 3 || index==4)
        {
            m_LCResultados.Columns[2].Visible=true;
            m_LCResultados.Columns[3].Visible=true;
            m_LCResultados.Columns[4].Visible=true;
            m_LCResultados.Columns[6].Visible=true;
            m_LCResultados.Columns[7].Visible=true;
            m_LCResultados.Columns[8].Visible=true;
        }
        else if( index==0)
        {
            m_LCResultados.Columns[0].Visible = true;
        }
        base.SetView(index);
    }
    protected void m_CBConceptoViablizacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBConceptoViablizacion_SelectedIndexChanged(sender, e);
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
    protected void m_LCTecnicos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            TextBox text = (TextBox)e.Row.Cells[2].Controls[1];
            text.Attributes["onKeypress"] = "isnb();";
        }
    }
}