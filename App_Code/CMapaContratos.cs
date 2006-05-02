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
public class CMapaContratos
{
    public string m_szCodigo;
    public string m_nID;
    public string m_szValor;
    public string m_szAvance;
    public string m_szIDProyecto;

	public CMapaContratos()
    {
        m_szCodigo = "";
        m_nID = "";
        m_szValor = "";
        m_szAvance = "";
        m_szIDProyecto = "";
	}
    public CMapaContratos(string id, string codigo, string valor)
    {
        m_szCodigo = codigo;
        m_nID = id;
        m_szValor = valor;
        m_szAvance = "";
        m_szIDProyecto = "";
    }
    public static CMapaContratos ParseQuery(SqlDataReader reader)
    {
        CMapaContratos cont= new CMapaContratos(reader[1].ToString(),
            reader[2].ToString(),
            reader[3].ToString());
        cont.m_szAvance = reader[4].ToString();
        cont.m_szIDProyecto = reader[5].ToString();
        return cont;
    }
    public static string GetQuery(string vigencia)
    {
        return "SELECT municipios.departamento,contratos.id,contratos.codigo,contratos.valor,reporte.valoravance,proyectos.id "
        + " FROM contratos "
        +" LEFT JOIN proyectos ON (proyectos.id=contratos.proyecto)"
        + " LEFT JOIN reporte ON (reporte.contrato=contratos.id)"
        +" LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
        +" WHERE proyectos.vigencia='" + vigencia+"'";
    }
}
