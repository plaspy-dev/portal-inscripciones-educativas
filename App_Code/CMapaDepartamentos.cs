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

public class CMapaDepartamentos
{
    public int m_nColor;
    public int  m_nCodigo;
    public string m_szNombre;
    public string m_szMapa;
    public string m_szObservaciones;
    protected ArrayList m_pProyectos;
    protected ArrayList m_pConvenios;
    protected ArrayList m_pContratos;

	public CMapaDepartamentos()
	{
        m_nColor=0;
        m_nCodigo=0;
        m_szNombre="";
        m_szMapa = "";
        m_szObservaciones = "";
        m_pProyectos = null;
        m_pConvenios = null;
        m_pContratos = null;
	}

    public CMapaDepartamentos(string codigo,string nombre, string mapa, string color, string observaciones)
    {
        m_nColor = color=="" ? 0 : Convert.ToInt32(color);
        m_nCodigo = codigo=="" ? 0 : Convert.ToInt32(codigo);
        m_szNombre = nombre;
        m_szMapa = mapa;
        m_szObservaciones = observaciones;
        m_pProyectos = null;
        m_pConvenios = null;
        m_pContratos = null;
    }
    public ArrayList GetProyectos()
    {
        return m_pProyectos;
    }
    public ArrayList GetContratos()
    {
        return m_pContratos;
    }
    public ArrayList GetConvenios()
    {
        return m_pConvenios;
    }
    public int GetTotalConvenios()
    {
        return m_pConvenios == null ? 0 : m_pConvenios.Count;
    }
    public int GetTotalContratos()
    {
        return m_pContratos == null ? 0 : m_pContratos.Count;
    }
    public int GetTotalProyectos()
    {
        return m_pProyectos == null ? 0 : m_pProyectos.Count;
    }
    public decimal GetValorContratos()
    {
        if (m_pContratos == null)
            return 0;
        decimal total = 0;
        for (int i = 0; i < m_pContratos.Count; i++)
        {
            if(((CMapaContratos)m_pContratos[i]).m_szValor.Length>0)
                total += Convert.ToDecimal(((CMapaContratos)m_pContratos[i]).m_szValor);
        }
        return total;
    }
    public decimal GetValorProyectos()
    {
        if (m_pProyectos == null)
            return 0;
        decimal total = 0;
        for (int i = 0; i < m_pProyectos.Count; i++)
        {
            if (((CMapaProyectos)m_pProyectos[i]).m_szValor.Length > 0)
                total += Convert.ToDecimal(((CMapaProyectos)m_pProyectos[i]).m_szValor);
        }
        return total;
    }
    public decimal GetValorConvenios()
    {
        if (m_pConvenios== null)
            return 0;
        decimal total = 0;
        for (int i = 0; i < m_pConvenios.Count; i++)
        {
            if (((CMapaConvenios)m_pConvenios[i]).m_szValor.Length > 0)
                total += Convert.ToDecimal(((CMapaConvenios)m_pConvenios[i]).m_szValor);
        }
        return total;
    }
    public static string GetQuery()
    {
        return "SELECT codigo,nombre,mapa,color,observaciones FROM departamentos";
    }
    public static CMapaDepartamentos ParseQuery(SqlDataReader resultado)
    {
        return new CMapaDepartamentos(resultado[0].ToString(),
            resultado[1].ToString(),
            resultado[2].ToString(),
            resultado[3].ToString(),
            resultado[4].ToString());
    }
    public void AddProyecto(SqlDataReader reader)
    {
        if (m_pProyectos == null)
            m_pProyectos = new ArrayList();
        m_pProyectos.Add(CMapaProyectos.ParseQuery(reader));
    }
    public void AddConvenio(SqlDataReader reader)
    {
        if (m_pConvenios== null)
            m_pConvenios = new ArrayList();
        m_pConvenios.Add(CMapaConvenios.ParseQuery(reader));
    }
    public void AddContrato(SqlDataReader reader)
    {
        if (m_pContratos == null)
            m_pContratos = new ArrayList();
        m_pContratos.Add(CMapaContratos.ParseQuery(reader));
    }
}
