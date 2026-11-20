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

/// <summary>
/// Summary description for CTablaEntes
/// </summary>
public class CTablaEntes
{
    CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CTablaEntes()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "";
        m_szCurrentID = "";
	}

    public void SetConector(CConector conector)
    {
        m_pConector = conector;
    }

    public void SetQuery(string query)
    {
        m_szQuery = query;
    }

    private int GetColumns(SqlDataReader reader)
    {

        if (reader == null || m_pTabla == null)
            return 0;
        m_pTabla.Columns.Add("autorizado", typeof(bool));
        m_pTabla.Columns.Add("id", typeof(int));
        m_pTabla.Columns.Add("Nombre", typeof(string));

        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns["id"] };
        return reader.FieldCount;
    }

    private DataTable GetDataTable()
    {
        CTablaEntes tabla = (CTablaEntes)HttpContext.Current.Session["entestabla"];
        m_szCurrentID = HttpContext.Current.Session["identes"] == null ? "" : HttpContext.Current.Session["identes"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = (CConector)HttpContext.Current.Session["conector"];

        if (m_szCurrentID == null || m_szCurrentID.Length == 0)
        {
            m_szQuery = "SELECT codigo,nombre,NULL FROM eterritoriales ORDER BY nombre";
        }
        else
        {
            m_szQuery = "SELECT eterritoriales.codigo,eterritoriales.nombre,usuariosentes.enteterritorial FROM eterritoriales "
                + "LEFT JOIN usuariosentes ON (usuariosentes.enteterritorial=eterritoriales.codigo AND usuariosentes.documento=" 
                + m_szCurrentID + ") ORDER BY eterritoriales.nombre";
        }
        m_pTabla = new DataTable();
        SqlDataReader reader = m_pConector.Query(m_szQuery);
        if (reader != null)
        {
            GetColumns(reader);
            int total = m_pTabla.Columns.Count;
            while (reader.Read())
            {
                object[] todos = new object[total];
                todos[0] = !reader.IsDBNull(2);
                todos[1] = reader[0];
                todos[2] = reader[1];

                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session["entestabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["entestabla"] = null;
    }

    static public DataTable GetTabla()
    {
        CTablaEntes tabla = (CTablaEntes)HttpContext.Current.Session["entestabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
