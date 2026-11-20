using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CContratosSaldos
/// </summary>
public class CContratosSaldos : CTablaItems
{
	public CContratosSaldos() : base(11)
	{
        m_szId = "idproyecto";

        m_szTitulos[0] = "id";
        m_szTitulos[1] = "Grupo";
        m_szTitulos[2] = "Codigo Proyecto";
        m_szTitulos[3] = "Entidad Territorial";
        m_szTitulos[4] = "Departamento";
        m_szTitulos[5] = "Municipio";
        m_szTitulos[6] = "Diferencia valor aprobado proyecto y valor contratado";
        m_szTitulos[7] = "Diferencia valor aporte Entidad educativa y valor contratado Entidad";
        m_szTitulos[8] = "Diferencia valor aportes MUN y valor contratado municipio";
        m_szTitulos[9] = "Estado";
        m_szTitulos[10] = "Observaciones";

        m_szCampos[0]="proyectos.id";
        m_szCampos[1]="grupos.nombre";
        m_szCampos[2]="proyectos.codigo";
        m_szCampos[3]="eterritoriales.nombre";
        m_szCampos[4]="departamentos.nombre";
        m_szCampos[5]="municipios.nombre";
        m_szCampos[6] = "(SELECT SUM(objeto.valor)-SUM(contratos.valor) FROM contratos WHERE contratos.proyecto=proyectos.id)";
        m_szCampos[7] = "''"; // "presupuesto.valormen-(SUM(contratos.valor)*(presupuesto.valormen/SUM(objeto.valor)))";
        m_szCampos[8] = "''";// "(SUM(objeto.valor)-presupuesto.valormen)-(SUM(contratos.valor)-(SUM(contratos.valor)*(presupuesto.valormen/SUM(objeto.valor))))";
        m_szCampos[9]="estados.nombre";
        m_szCampos[10] = "saldosp.observaciones";

        string join = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=proyectos.eterritorial AND asignacionpresupuestal.vigencia='" + m_pUsuario.GetVigenciaActual() + "') "
            + " LEFT JOIN varios AS grupos ON (grupos.id=asignacionpresupuestal.grupo)"
            + " LEFT JOIN eterritoriales ON (eterritoriales.codigo=proyectos.eterritorial)"
            + " LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
            + " LEFT JOIN departamentos ON (departamentos.codigo=municipios.departamento)"
            + " LEFT JOIN objeto ON (objeto.proyecto=proyectos.id)"
            + " LEFT JOIN presupuesto ON (presupuesto.proyecto=proyectos.id)"
            + " LEFT JOIN saldosp ON (saldosp.proyecto= proyectos.id)"
            + " LEFT JOIN varios AS estados ON (estados.id= saldosp.estado)"
            + " WHERE proyectos.id=%d"
            ;
        string grupo = " GROUP BY ";
        for (int i = 0; i < 6; i++)
            grupo += m_szCampos[i] + ",";
        for (int i = 9; i < m_szCampos.Length; i++)
            grupo += m_szCampos[i] + ",";
        grupo += "presupuesto.valormen";

        m_szQuery = CConector.CrearSelectT("proyectos", m_szCampos, m_szTitulos, join + grupo);
	}
}
