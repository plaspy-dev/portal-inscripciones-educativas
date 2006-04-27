using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;

public class CMapaProyectos
{
    public string m_szCodigo;
    public string m_nID;
    public string m_szValor;
    public string m_szObjeto;
    public string m_szMunicipio;
    public string m_szInstitucion;
    public int m_nEjecucion;
    public string m_szEstado;

	public CMapaProyectos()
	{
        m_szCodigo = "";
        m_nID = "";
        m_szValor = "";
        m_szEstado= m_szInstitucion = m_szMunicipio = m_szObjeto = "";
        m_nEjecucion = 0;
	}
    public CMapaProyectos(string id, string codigo, string valor)
    {
        m_szCodigo = codigo;
        m_nID = id;
        m_szValor = valor;
        m_szEstado = m_szInstitucion = m_szMunicipio = m_szObjeto = "";
        m_nEjecucion = 0;
    }
    public static CMapaProyectos ParseQuery(SqlDataReader reader)
    {
        CMapaProyectos proy= new CMapaProyectos(reader[1].ToString(),
            reader[2].ToString(),
            reader[3].ToString());
        proy.m_szMunicipio = reader[4].ToString();
        proy.m_szObjeto = reader[5].ToString();
        proy.m_szInstitucion = reader[6].ToString();
        return proy;
    }
    public static string GetQuery(string vigencia)
    {
        return "SELECT municipios.departamento,proyectos.id,proyectos.codigo,SUM(objeto.valor),municipios.nombre "
            + ",dbo.fn_GetObjetos(proyectos.id),insteducativas.nombre "
        +" FROM proyectos "
        +" LEFT JOIN objeto ON (objeto.proyecto=proyectos.id) "
        +" LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
        + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion)"
        +" WHERE vigencia='" +vigencia+ "'"
        + " GROUP BY proyectos.id,proyectos.codigo,municipios.departamento,municipios.nombre,insteducativas.nombre";
    }
}
