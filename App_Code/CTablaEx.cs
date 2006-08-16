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

public class CTablaEx
{
    CConector   m_pConector;
    string      m_szQuery;
    string      m_szID;
    DataTable   m_pTabla;
    bool[]      m_baHidden;
    
	public CTablaEx()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "";
        m_szID = "";
        m_baHidden = null;
	}

    public void SetConector(CConector conector)
    {
        m_pConector = conector;
    }

    public void SetQuery(string query)
    {
        m_szQuery = query;
    }

    public void SetID(string id)
    {
        m_szID = id;
    }

    private int GetColumns(SqlDataReader reader) {

        if (reader == null || m_pTabla==null)
            return 0;
        int total=0;
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (m_baHidden == null || !m_baHidden[i])
            {
                m_pTabla.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                total++;
            }
        }
        if(m_szID.Length!=0)
            m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns[m_szID] };
        return total;
    }

    private DataTable GetDataTable(bool session)
    {
        if (session)
        {
            CTablaEx tabla = (CTablaEx)HttpContext.Current.Session["resultados"];
            m_szQuery = HttpContext.Current.Session["query"].ToString();
            if (tabla != null && tabla.m_szQuery == m_szQuery)
            {
                m_pTabla = tabla.m_pTabla;
                return m_pTabla;
            }
        }
        else
        {
            if(HttpContext.Current.Session["query"]==null)
                return null;
            m_szQuery = HttpContext.Current.Session["query"].ToString();
        }

        m_pConector = CConector.GetConector();
        m_szID = HttpContext.Current.Session["indice"].ToString();

        m_pTabla = new DataTable();
        SqlDataReader reader = m_pConector.Query(m_szQuery);
       if (reader != null)
        {
            int total= GetColumns(reader);
           if(m_baHidden==null)
               total= reader.FieldCount;
            while (reader.Read())
            {
                object[] todos = new object[total];
                if (m_baHidden!=null)
                {
                    int pos=0;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if(!m_baHidden[i]) {
                            todos[pos] = reader[i];
                            pos++;
                        }
                    }
                }
                else
                {
                    reader.GetValues(todos);
                }
                m_pTabla.Rows.Add(todos);
            }
        }
        if(session)
            HttpContext.Current.Session["resultados"] = this;
        return m_pTabla;
    }

    static public int GetCount()
    {
        CTablaEx tabla = (CTablaEx)HttpContext.Current.Session["resultados"];
        if (tabla == null || tabla.m_pTabla==null)
            return 0;
        return tabla.m_pTabla.Rows.Count;
    }

    static public void RebuilTable() {
        HttpContext.Current.Session["resultados"] = null;
    }

    static public DataTable GetTabla()
    {
        CTablaEx tabla = (CTablaEx)HttpContext.Current.Session["resultados"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable(true);
    }

    public DataTable SelectPrint()
    {
        m_baHidden = (bool[])HttpContext.Current.Session["hiddencolsprint"];
        GetDataTable(false);
        return m_pTabla;
    }
}
