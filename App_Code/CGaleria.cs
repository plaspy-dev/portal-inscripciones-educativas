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
/// Summary description for CGaleria
/// </summary>
public class CGaleria : CTablaItems
{
	public CGaleria() : base(2)
	{
        m_szId = "idcontratofoto";

        m_szCampos[0]= m_szTitulos[0] = "id";
        m_szCampos[1] = m_szTitulos[1] = "fecha";
        string condicion = " WHERE contrato=%d ";
        m_szQuery = CConector.CrearSelectT("registro", m_szCampos, m_szTitulos, condicion);
	}
}
