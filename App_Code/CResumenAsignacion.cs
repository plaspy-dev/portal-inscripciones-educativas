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
/// Summary description for CResumenAsignacion
/// </summary>
public class CResumenAsignacion : CTablaItems
{
	public CResumenAsignacion() : base(3)
	{
        m_szId = "id";

        m_szTitulos[0] = "Partida Asignada";
        m_szTitulos[1] = "Partida cubierta";
        m_szTitulos[2] = "Saldo partida";

        m_szCampos[0] = "ISNULL(vigencias.presupuesto,0)";
        m_szCampos[1] = "ISNULL(SUM(asignacionpresupuestal.partida),0)";
        m_szCampos[2] = "ISNULL(vigencias.presupuesto,0)-ISNULL(SUM(asignacionpresupuestal.partida),0)";

        string join = " LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.vigencia=vigencias.nombre) "
            ;
        string condicion = " WHERE vigencias.nombre='" + m_pUsuario.GetVigenciaActual() + "' ";
        string grupo = " GROUP BY vigencias.presupuesto";

        m_szQuery = CConector.CrearSelectT("vigencias", m_szCampos, m_szTitulos, join + condicion+ grupo);
	}
}
