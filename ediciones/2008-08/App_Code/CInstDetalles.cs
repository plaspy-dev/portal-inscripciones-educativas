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
/// Summary description for CInstDetalles
/// </summary>
public class CInstDetalles : CTablaItems
{
	public CInstDetalles() : base(10)
	{
        m_szId = "instid";

        m_szTitulos[0]="Calendario";
        m_szTitulos[1] = "Tipo de población";
        m_szTitulos[2] = "Sector";
        m_szTitulos[3] = "Zona";
        m_szTitulos[4] = "Barrio";
        m_szTitulos[5] = "Dirección";
        m_szTitulos[6] = "Teléfonos";
        m_szTitulos[7] = "Fax";
        m_szTitulos[8] = "Web";
        m_szTitulos[9] = "E-Mail";

        m_szCampos[0] = "varios.nombre";
        m_szCampos[1] = "varios1.nombre";
        m_szCampos[2] = "varios2.nombre";
        m_szCampos[3] = "varios5.nombre";
        m_szCampos[4] = "insteducativas.barrioinspeccion";
        m_szCampos[5] = "insteducativas.direccion";
        m_szCampos[6] = "insteducativas.telefonos";
        m_szCampos[7] = "insteducativas.fax";
        m_szCampos[8] = "insteducativas.web";
        m_szCampos[9] = "insteducativas.email";

        string join = " LEFT JOIN varios ON (varios.id=insteducativas.calendario)"
            + " LEFT JOIN varios AS varios1 ON (varios1.id=insteducativas.tpoblacion)"
            + " LEFT JOIN varios AS varios2 ON (varios2.id=insteducativas.sector)"
            + " LEFT JOIN varios AS varios5 ON (varios5.id=insteducativas.zona)"
           ;
        string condicion = " WHERE insteducativas.id='%d' ";

        m_szQuery = CConector.CrearSelectT("insteducativas", m_szCampos, m_szTitulos, join + condicion);
	}
}
