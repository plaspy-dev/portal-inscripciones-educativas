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
/// Summary description for CObjetoInscrito
/// </summary>
public class CObjetoInscrito : CTablaItems
{
    public CObjetoInscrito() : base(3)
	{
        m_szId = "idproyectoinsc";

        m_szTitulos[0] = "Concepto";
        m_szTitulos[1] = "Cantidad";
        m_szTitulos[2] = "Valor";

        m_szCampos[0] = "varios.nombre";
        m_szCampos[1] = "objetoinscrito.cantidad";
        m_szCampos[2] = "objetoinscrito.valor";

        m_szQuery= "SELECT varios.nombre AS Concepto,objetoinscrito.cantidad AS Cantidad,objetoinscrito.valor AS Valor FROM objetoinscrito"
            + " LEFT JOIN varios ON (varios.id=objetoinscrito.concepto) "
            + " WHERE proyecto=%d";
	}
}
