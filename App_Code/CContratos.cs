using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CContratos
/// </summary>
public class CContratos : CTablaItems
{
	public CContratos() : base(30)
	{
        m_szId = "idproyecto";

        m_szTitulos[0]="id";
        m_szTitulos[1]="Grupo";
        m_szTitulos[2]="Codigo Proyecto";
        m_szTitulos[3]="Entidad Territorial";
        m_szTitulos[4]="Departamento";
        m_szTitulos[5]="Municipio";
        m_szTitulos[6]="Institución educativa";
        m_szTitulos[7]="Objeto";
        m_szTitulos[8]="Numero de contrato";
        m_szTitulos[9]="Fecha Suscripcion del contrato";
        m_szTitulos[10]="Fecha acta de inicio del contrato";
        m_szTitulos[11]="Clase de contrato";
        m_szTitulos[12]="Objeto del contrato";
        m_szTitulos[13] = "Plazo (incluidas adiciones y suspensiones)";
        m_szTitulos[14] = "Fecha de vencimiento del contrato";
        m_szTitulos[15] = "Valor total contratado (incluidos adicionales)";
        m_szTitulos[16] = "Valor aportes Entidad educativa al contrato";
        m_szTitulos[17] = "Valor aportes municipio al  contrato (incluidas adiciones)";
        m_szTitulos[18] = "Valor avance de obra acumulado a último reporte";
        m_szTitulos[19] = "Fecha  del reporte";
        m_szTitulos[20] = "% de ejecución sobre el total del valor del contrato";
        m_szTitulos[21] = "Estado del contrato";
        m_szTitulos[22] = "Observaciones sobre revisión de informes";
        m_szTitulos[23] = "Historial del proyecto";
        m_szTitulos[24]=m_szCampos[24]="contratos.contratista";
        m_szTitulos[25]=m_szCampos[25]="contratos.rlegal";
        m_szTitulos[26] = m_szCampos[26] = "reporte.interventor";
        m_szTitulos[27] = m_szCampos[27] = "reporte.cargo";
        m_szTitulos[28] = m_szCampos[28] = "contratos.observaciones";
        m_szTitulos[29] = m_szCampos[29] = "MAX(registro.fecha)";
        

        m_szCampos[0]="contratos.id";
        m_szCampos[1]="grupos.nombre";
        m_szCampos[2]="proyectos.codigo";
        m_szCampos[3]="eterritoriales.nombre";
        m_szCampos[4]="departamentos.nombre";
        m_szCampos[5]="municipios.nombre";
        m_szCampos[6] = "insteducativas.nombre";
        m_szCampos[7]="proyectos.objeto";
        m_szCampos[8]="contratos.codigo";
        m_szCampos[9] = "contratos.fechas";
        m_szCampos[10] = "contratos.fechainicio";
        m_szCampos[11] = "clases.nombre";
        m_szCampos[12] = "objetos.nombre";
        m_szCampos[13] = "contratos.plazo";
        m_szCampos[14] = "DATEADD(dd,contratos.plazo,contratos.fechainicio)";
        m_szCampos[15] = "contratos.valor";
        m_szCampos[16] = "(contratos.valor*presupuesto.valormen)/SUM(objeto.valor)";
        m_szCampos[17] = "contratos.valor-(contratos.valor*presupuesto.valormen)/SUM(objeto.valor)";
        m_szCampos[18] = "reporte.valoravance";
        m_szCampos[19] = "reporte.fecha";
        m_szCampos[20] = "(contratos.valor/reporte.valoravance)*100";
        m_szCampos[21] = "estados.nombre";
        m_szCampos[22] = "reporte.observaciones";
        m_szCampos[23] = "contratos.historial";
        
        string grupo=" GROUP BY ";
        for (int i = 0; i < 14; i++)
            grupo += m_szCampos[i] + ",";
        for (int i = 21; i < m_szCampos.Length-1; i++)
            grupo += m_szCampos[i] + ",";
        grupo += m_szCampos[15] + "," + m_szCampos[18] + "," + m_szCampos[19] + ",presupuesto.valormen";

        string join = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=proyectos.eterritorial) "
            + " LEFT JOIN varios AS grupos ON (grupos.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectos.eterritorial)"
            + " LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion)"
            + " LEFT JOIN objeto ON (objeto.proyecto=proyectos.id)"
            + " LEFT JOIN presupuesto ON (presupuesto.proyecto=proyectos.id)"
            + " LEFT JOIN contratos ON (contratos.proyecto= proyectos.id)"
            + " LEFT JOIN varios AS clases ON (clases.id=contratos.clase)"
            + " LEFT JOIN varios AS objetos ON (objetos.id=contratos.objeto)"
            + " LEFT JOIN reporte ON (reporte.contrato=contratos.id)"
            + " LEFT JOIN varios AS estados ON (estados.id=contratos.estado)"
            + " LEFT JOIN registro ON (registro.contrato=contratos.id)"
            + " WHERE proyectos.id=%d AND contratos.id IS NOT NULL"
            ;
        m_szQuery = CConector.CrearSelectT("proyectos",m_szCampos, m_szTitulos, join + grupo);
        m_szCurrentID = "";
	}
}
