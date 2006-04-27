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
using System.Collections;
public class CMapaConvenios
{
    public string m_szCodigo;
    public string m_nID;
    public string m_szValor;

	public CMapaConvenios()
	{
		m_szCodigo="";
        m_nID="";
        m_szValor="";
	}
    public CMapaConvenios(string id, string codigo, string valor)
    {
        m_szCodigo = codigo;
        m_nID = id;
        m_szValor = valor;
    }
    public static CMapaConvenios ParseQuery(SqlDataReader reader)
    {
        return new CMapaConvenios(reader[1].ToString(),
            reader[2].ToString(),
            reader[3].ToString());
    }
    public static string GetQuery(string vigencia)
    {
        return "SELECT (SELECT TOP 1 municipios.departamento FROM municipios WHERE municipios.eterritorial=conveniosm.eterritorial),"
        + "conveniosm.id,conveniosm.codigo"
        + ",conveniosm.valor"
        + " FROM conveniosm"
        + " WHERE conveniosm.vigencia='" + vigencia +"'";
    }
}
