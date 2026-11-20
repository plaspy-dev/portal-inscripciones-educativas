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

public class CMapa
{
    protected ArrayList m_pDepartamentos;
    protected CConector m_pConector;
    protected string m_szVigencia;

	public CMapa(string vigencia)
	{
        m_szVigencia = vigencia;
        m_pConector = new CConector();
        m_pDepartamentos = null;
	}
    public static CMapa GetMapa(string vigencia)
    {
        CMapa mapa = GetMapa();
        if (mapa == null || mapa.m_szVigencia!=vigencia)
        {
            mapa = new CMapa(vigencia);
            mapa.BuildAll();
            HttpContext.Current.Session["mapa"] = mapa;
        }
        return mapa;
    }
    public static CMapa GetMapa()
    {
        return HttpContext.Current.Session["mapa"] == null ? null : (CMapa)HttpContext.Current.Session["mapa"];
    }
    public ArrayList GetDepartamentos()
    {
        return m_pDepartamentos;
    }
    public string BuildDepartamentos()
    {
        SqlDataReader reader = m_pConector.Query(CMapaDepartamentos.GetQuery());
        if(reader==null)
            return m_pConector.GetLastError();
        if (m_pDepartamentos == null)
            m_pDepartamentos = new ArrayList();

        while (reader.Read())
        {
            m_pDepartamentos.Add(CMapaDepartamentos.ParseQuery(reader));
        }
        return "";
    }
    public CMapaDepartamentos GetDepartamento(int codigo)
    {
        if (m_pDepartamentos == null)
            return null;
        for (int i = 0; i < m_pDepartamentos.Count; i++)
        {
            CMapaDepartamentos dep = (CMapaDepartamentos)m_pDepartamentos[i];
            if (dep.m_nCodigo == codigo)
                return dep;
        }
        return null;
    }
    public CMapaDepartamentos GetDepartamento(string nombremapa)
    {
        if (m_pDepartamentos == null)
            return null;
        for (int i = 0; i < m_pDepartamentos.Count; i++)
        {
            CMapaDepartamentos dep = (CMapaDepartamentos)m_pDepartamentos[i];
            if (dep.m_szMapa == nombremapa)
                return dep;
        }
        return null;
    }
    public string BuildConvenios()
    {
        SqlDataReader reader = m_pConector.Query(CMapaConvenios.GetQuery(m_szVigencia));
        if (reader == null)
            return m_pConector.GetLastError();
        CMapaDepartamentos dep;
        while(reader.Read()) {
            if (!reader.IsDBNull(0))
            {
                dep = GetDepartamento(Convert.ToInt32(reader[0]));
                if (dep != null)
                    dep.AddConvenio(reader);
            }
        }
        return "";
    }
    public string BuildContratos()
    {
        SqlDataReader reader = m_pConector.Query(CMapaContratos.GetQuery(m_szVigencia));
        if (reader == null)
            return m_pConector.GetLastError();
        CMapaDepartamentos dep;
        while (reader.Read())
        {
            if (!reader.IsDBNull(0))
            {
                dep = GetDepartamento(Convert.ToInt32(reader[0]));
                if (dep != null)
                    dep.AddContrato(reader);
            }
        }
        return "";
    }
    public string BuildProyectos()
    {
        SqlDataReader reader = m_pConector.Query(CMapaProyectos.GetQuery(m_szVigencia));
        if (reader == null)
            return m_pConector.GetLastError();
        CMapaDepartamentos dep;
        while (reader.Read())
        {
            if (!reader.IsDBNull(0))
            {
                dep = GetDepartamento(Convert.ToInt32(reader[0]));
                if (dep != null)
                    dep.AddProyecto(reader);
            }
        }
        return "";
    }
    public string BuildAll()
    {
        string result = BuildDepartamentos();
        if (result.Length > 0)
            return result;
        result = BuildConvenios();
        if (result.Length > 0)
            return result;
        result = BuildProyectos();
        if (result.Length > 0)
            return result;
        result = BuildContratos();
        return result;
    }
}
