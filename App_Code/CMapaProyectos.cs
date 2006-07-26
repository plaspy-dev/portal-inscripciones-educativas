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
    public int m_nEstudiantesB;
    public int m_nAulas;
    public int m_nDotacion;

	public CMapaProyectos()
	{
        m_szCodigo = "";
        m_nID = "";
        m_szValor = "";
        m_nEstudiantesB = 0;
        m_szEstado= m_szInstitucion = m_szMunicipio = m_szObjeto = "";
        m_nEjecucion = 0;
        m_nAulas = 0;
        m_nDotacion = 0;
	}
    public CMapaProyectos(string id, string codigo, string valor)
    {
        m_szCodigo = codigo;
        m_nID = id;
        m_szValor = valor;
        m_szEstado = m_szInstitucion = m_szMunicipio = m_szObjeto = "";
        m_nEjecucion = 0;
        m_nEstudiantesB = 0;
        m_nAulas = 0;
        m_nDotacion = 0;
    }
    public static CMapaProyectos ParseQuery(SqlDataReader reader)
    {
        CMapaProyectos proy= new CMapaProyectos(reader[1].ToString(),
            reader[2].ToString(),
            reader[3].ToString());
        proy.m_szMunicipio = reader[4].ToString();
        proy.m_szObjeto = reader[5].ToString();
        proy.m_szInstitucion = reader[6].ToString();
        proy.m_nEstudiantesB = Convert.ToInt32(reader[7].ToString());
        if(reader[8].ToString()!="")
            proy.m_nAulas = Convert.ToInt32(reader[8].ToString());
        if (reader[9].ToString() != "")
            proy.m_nDotacion = Convert.ToInt32(reader[9].ToString());
        return proy;
    }
    public static string GetQuery(string vigencia)
    {
        return "SELECT municipios.departamento,proyectos.id,proyectos.codigo,SUM(objeto.valor),municipios.nombre "
            + ",dbo.fn_GetObjetos(proyectos.id),insteducativas.nombre,ISNULL(proyectosi.estudiantesb,0)+ISNULL(proyectosi.estudiantesd,0) "
            + ",(SELECT SUM(objeto.cantidad) FROM objeto LEFT JOIN varios ON (varios.id=objeto.concepto) WHERE objeto.proyecto=proyectos.id AND varios.nombre LIKE '%const%aulas%') "
             + ",(SELECT SUM(objeto.cantidad) FROM objeto LEFT JOIN varios ON (varios.id=objeto.concepto) WHERE objeto.proyecto=proyectos.id AND varios.nombre NOT LIKE '%const%aulas%') "
        +" FROM proyectos "
        +" LEFT JOIN objeto ON (objeto.proyecto=proyectos.id) "
        +" LEFT JOIN municipios ON (municipios.id=proyectos.municipio)"
        + " LEFT JOIN insteducativas ON (insteducativas.id=proyectos.institucion)"
        + " LEFT JOIN proyectosi ON (proyectosi.id=proyectos.proyectoi)"
        + " WHERE proyectos.vigencia='" + vigencia + "'"
        + " GROUP BY proyectos.id,proyectos.codigo,municipios.departamento,municipios.nombre,insteducativas.nombre,proyectosi.estudiantesb,proyectosi.estudiantesd";
    }
}
