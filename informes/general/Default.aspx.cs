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

public partial class informes_general_Default : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("conveniosm", 86))
            return;

        m_saCampos[0] = "conveniosm.vigencia";
        m_saCampos[1] = "grupos.nombre";
        m_saCampos[2] = "eterritoriales.nombre";
        m_saCampos[3] = "departamentos.nombre";
        m_saCampos[4] = "municipios.nombre";
        m_saCampos[5] = "conveniosm.codigo";
        m_saCampos[6] = "conveniosm.valor";
        m_saCampos[7] = "conveniosm.fechaaprobacion";
        m_saCampos[8] = "(SELECT SUM(adicionesconvenio.valor) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[9] = "(SELECT MAX(adicionesconvenio.fecha) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[10] = "conveniosm.plazo";
        m_saCampos[11] = "(SELECT SUM(adicionesconvenio.plazo) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[12] = "tipos.nombre";
        m_saCampos[13] = "estados.nombre";
        m_saCampos[14] = "''";
        m_saCampos[15] = "conveniosm.rdp";
        m_saCampos[16] = "(SELECT DATEADD(dd,conveniosm.plazo+ISNULL(SUM(adicionesconvenio.plazo),0)-1,conveniosm.fechaaprobacion) FROM adicionesconvenio WHERE adicionesconvenio.convenio=conveniosm.id)";
        m_saCampos[17] = "''";
        m_saCampos[18] = "''";
        m_saCampos[19] = "(SELECT desembolsosconvenio.valor FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=1)";
        m_saCampos[20] = "(SELECT desembolsosconvenio.fecha FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=1)";
        m_saCampos[21] = "(SELECT desembolsosconvenio.valor FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=2)";
        m_saCampos[22] = "(SELECT desembolsosconvenio.fecha FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=2)";
        m_saCampos[23] = "(SELECT desembolsosconvenio.valor FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=3)";
        m_saCampos[24] = "(SELECT desembolsosconvenio.fecha FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=3)";
        m_saCampos[25] = "(SELECT desembolsosconvenio.valor FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=4)";
        m_saCampos[26] = "(SELECT desembolsosconvenio.fecha FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id AND desembolsosconvenio.numero=4)";
        m_saCampos[27] = "(SELECT SUM(desembolsosconvenio.valor) FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id)";
        m_saCampos[28] = "(SELECT conveniosm.valor-ISNULL(SUM(desembolsosconvenio.valor),0) FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=conveniosm.id)";
        m_saCampos[29] = "conveniosm.observaciones";
        m_saCampos[30] = "(SELECT SUM(rendimientosconvenio.valor) FROM rendimientosconvenio WHERE rendimientosconvenio.convenio=conveniosm.id)";
        m_saCampos[31] = "(SELECT MAX(rendimientosconvenio.fecha) FROM rendimientosconvenio WHERE rendimientosconvenio.convenio=conveniosm.id)";
        m_saCampos[32] = "''";
        m_saCampos[33] = "''";
        m_saCampos[34] = "''";
        // proyectos
        m_saCampos[35] = "proyectos.codigo";
        m_saCampos[36] = "proyectos.fechaaprobacion";
        m_saCampos[37] = "insteducativas.nombre";
        m_saCampos[38] = "insteducativas.codigo";
        m_saCampos[39] = "tipoproyecto.nombre";
        m_saCampos[40] = "dbo.fn_GetObjetos(proyectos.id)";
        m_saCampos[41] = "(SELECT SUM(objeto.valor) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[42] = "presupuesto.valormen";
        m_saCampos[43] = "(SELECT (presupuesto.valormen*100)/SUM(objeto.valor) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[44] = "(SELECT SUM(objeto.valor)-presupuesto.valormen FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[45] = "(SELECT ((SUM(objeto.valor)-presupuesto.valormen)*100)/SUM(objeto.valor) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[46] = "''";
        m_saCampos[47] = "''";
        m_saCampos[48] = "''";
        m_saCampos[49] = "''";
        m_saCampos[50] = "''";
        m_saCampos[51] = "''";
        m_saCampos[52] = "''";
        m_saCampos[53] = "''";
        m_saCampos[54] = "''";
        m_saCampos[55] = "''";
        m_saCampos[56] = "''";
        m_saCampos[57] = "''";
        m_saCampos[58] = "''";
        m_saCampos[59] = "''";
        m_saCampos[60] = "''";
        m_saCampos[61] = "''";
        m_saCampos[62] = "''";
        m_saCampos[63] = "''";
        m_saCampos[64] = "''";
        m_saCampos[65] = "''";
        // contratos
        m_saCampos[66] = "contratos.codigo";
        m_saCampos[67] = "contratos.fechas";
        m_saCampos[68] = "''";
        m_saCampos[69] = "''";
        m_saCampos[70] = "contratos.fechainicio";
        m_saCampos[71] = "''";
        m_saCampos[72] = "clases.nombre";
        m_saCampos[73] = "objetocontrato.nombre";
        m_saCampos[74] = "contratos.plazo";
        m_saCampos[75] = "DATEADD(dd,contratos.plazo-1,contratos.fechainicio)";
        m_saCampos[76] = "contratos.valor";
        m_saCampos[77] = "(SELECT contratos.valor*(presupuesto.valormen/SUM(objeto.valor)) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[78] = "(SELECT contratos.valor-(contratos.valor*(presupuesto.valormen/SUM(objeto.valor))) FROM objeto WHERE objeto.proyecto=proyectos.id)";
        m_saCampos[79] = "contratos.contratista";
        m_saCampos[80] = "reporte.valoravance";
        m_saCampos[81] = "reporte.fecha";
        m_saCampos[82] = "CASE WHEN reporte.valoravance=0 THEN 0 WHEN reporte.valoravance IS NULL THEN 0 ELSE (contratos.valor/reporte.valoravance)*100 END";
        m_saCampos[83] = "tiporeporte.nombre";
        m_saCampos[84] = "estadocontrato.nombre";
        m_saCampos[85] = "reporte.observaciones";


        m_saTitulos[0] = "Vigencia";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Entidad territorial";
        m_saTitulos[3] = "Departamento";
        m_saTitulos[4] = "Municipio";
        m_saTitulos[5] = "Número convenio marco";
        m_saTitulos[6] = "Valor convenio marco";
        m_saTitulos[7] = "Fecha aprobación";
        m_saTitulos[8] = "Valor adiciones";
        m_saTitulos[9] = "Fecha adición";
        m_saTitulos[10] = "Plazo inicial";
        m_saTitulos[11] = "Prorroga convenio";
        m_saTitulos[12] = "Tipo convenio";
        m_saTitulos[13] = "Estado del convenio";
        m_saTitulos[14] = "Saldo comprometido";
        m_saTitulos[15] = "No. Registro presupuestal";
        m_saTitulos[16] = "Fecha de vencimiento convenio";
        m_saTitulos[17] = "Vigencia poliza de manejo del tesorero";
        m_saTitulos[18] = "Forma de pago del convenio";
        m_saTitulos[19] = "Valor del desembolso 1";
        m_saTitulos[20] = "Fecha del desembolso 1";
        m_saTitulos[21] = "Valor del desembolso 2";
        m_saTitulos[22] = "Fecha del desembolso 2";
        m_saTitulos[23] = "Valor del desembolso 3";
        m_saTitulos[24] = "Fecha del desembolso 3";
        m_saTitulos[25] = "Valor del desembolso 4";
        m_saTitulos[26] = "Fecha del desembolso 4";
        m_saTitulos[27] = "Suma de los desembolsos del Entidad";
        m_saTitulos[28] = "Saldo de los desembolsos del Entidad";
        m_saTitulos[29] = "Observaciones del convenio";
        m_saTitulos[30] = "Valor Rendimientos finacieros";
        m_saTitulos[31] = "Fecha certificación rendimientos financieros";
        m_saTitulos[32] = "Resolución-Decreto-Mandato-Ordenanza";
        m_saTitulos[33] = "No. de resolución";
        m_saTitulos[34] = "Fecha de resolución";
        // proyectos
        m_saTitulos[35] = "No. de proyecto aprobado";
        m_saTitulos[36] = "Fecha de aprobación";
        m_saTitulos[37] = "Nombre de la institución educativa";
        m_saTitulos[38] = "Código DANE";
        m_saTitulos[39] = "Tipo proyecto";
        m_saTitulos[40] = "Objeto del proyecto";
        m_saTitulos[41] = "Valor total del proyecto";
        m_saTitulos[42] = "Valor partida asignada";
        m_saTitulos[43] = "% aportes Entidad educativa";
        m_saTitulos[44] = "Valor partida municipio";
        m_saTitulos[45] = "% aportes cofinanciación";
        m_saTitulos[46] = "Numero de aulas nuevas";
        m_saTitulos[47] = "Numero de aulas de reposicion";
        m_saTitulos[48] = "Numero de estudiantes promedio aula";
        m_saTitulos[49] = "Numero de jornadas";
        m_saTitulos[50] = "Total ampliación cobertura";
        m_saTitulos[51] = "Total estudiantes beneficiados";
        m_saTitulos[52] = "Total estudiantes beneficiados aulas";
        m_saTitulos[53] = "M2 aulas nuevas";
        m_saTitulos[54] = "Valor total aulas nuevas";
        m_saTitulos[55] = "$/M2 aula nueva";
        m_saTitulos[56] = "M2 aulas de reposicion";
        m_saTitulos[57] = "Valor total aulas de reposicion";
        m_saTitulos[58] = "$/M2 aula reposicion";
        m_saTitulos[59] = "Numero de baterias sanitarias";
        m_saTitulos[60] = "M2 de bateria sanitaria";
        m_saTitulos[61] = "Valor total bateria sanitaria";
        m_saTitulos[62] = "$/M2 bateria sanitaria";
        m_saTitulos[63] = "Numero de estudiantes beneficiados por dotación";
        m_saTitulos[64] = "Valor total dotación";
        m_saTitulos[65] = "Numero de sillas universitarias";
        // contratos
        m_saTitulos[66] = "Numero de contrato";
        m_saTitulos[67] = "Fecha suscripcion del contrato";
        m_saTitulos[68] = "No. Registro presupuestal recursos aportados por el Entidad";
        m_saTitulos[69] = "No. Registro presupuestal resursos aportados por el Mun";
        m_saTitulos[70] = "Fecha acta de inicio";
        m_saTitulos[71] = "Tipo de contrato";
        m_saTitulos[72] = "Clase de contrato";
        m_saTitulos[73] = "Objeto del contrato";
        m_saTitulos[74] = "Plazo (incluidas adiciones)";
        m_saTitulos[75] = "Fecha de vencimiento del contrato";
        m_saTitulos[76] = "Valor total contratado(Incluidas adiciones)";
        m_saTitulos[77] = "Valor aportes Entidad educativa al contrato";
        m_saTitulos[78] = "Valor aportes Mun al contrato";
        m_saTitulos[79] = "Contratista";
        m_saTitulos[80] = "Valor avance de obra";
        m_saTitulos[81] = "Fecha del reporte";
        m_saTitulos[82] = "% de ejecución sobre el valor";
        m_saTitulos[83] = "Tipo de reporte";
        m_saTitulos[84] = "Estado del contrato";
        m_saTitulos[85] = "Observaciones";



        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_saCampos[i];

        m_szJoin = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=conveniosm.eterritorial) "
            + " LEFT JOIN varios AS grupos ON (grupos.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial)"
            + " LEFT JOIN proyectos ON (proyectos.eterritorial=conveniosm.eterritorial AND proyectos.vigencia='" + m_pUsuario.GetVigenciaActual() + "')"
            + " LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN varios AS tipos ON (tipos.id=conveniosm.tipo)"
            + " LEFT JOIN varios AS estados ON (estados.id=conveniosm.estado)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion)"
            + " LEFT JOIN varios AS tipoproyecto ON (tipos.id=proyectos.tipo)"
            + " LEFT JOIN presupuesto ON (presupuesto.proyecto=proyectos.id)"
            + " LEFT JOIN contratos ON (contratos.proyecto=proyectos.id)"
            + " LEFT JOIN varios AS clases ON (clases.id=contratos.clase)"
            + " LEFT JOIN varios AS objetocontrato ON (objetocontrato.id=contratos.objeto)"
            + " LEFT JOIN reporte ON (reporte.contrato=contratos.id)"
            + " LEFT JOIN varios AS tiporeporte ON (tiporeporte.id=reporte.tipo)"
            + " LEFT JOIN varios AS estadocontrato ON (estadocontrato.id=contratos.estado)"
            ;

        m_szIndice = "";

        m_szCondicion = " conveniosm.vigencia='" + m_pUsuario.GetVigenciaActual() + "' ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");

        m_pUsuario.FillComboBox(m_CBEntidad);
        if (m_CBEntidad.Items.Count > 0)
            m_CBEntidad.Items[0].Text = "(Todos)";
        if (m_CBEntidad.SelectedIndex > 0)
            m_szCondicionEx += " AND proyectos.eterritorial=" + m_CBEntidad.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
       

        EndPageLoad();
        m_BAceptarP.Style["display"] = "";
        m_CBImprimir.Style["display"] = "";
    }

    virtual protected void m_BAceptarP_Click(object sender, EventArgs e)
    {
        CTablaEx tabla= new CTablaEx();
        tabla.Select();
        //imprimir
        base.m_BAceptarP_Click(sender, e);
    }

}
