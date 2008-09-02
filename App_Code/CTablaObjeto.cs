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
/// Summary description for CTablaObjeto
/// </summary>
public class CTablaObjeto
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CTablaObjeto()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "SELECT objetoinscrito.id AS id,varios.nombre AS Concepto,objetoinscrito.cantidad AS Cantidad,varios1.nombre AS Unidades,objetoinscrito.valor AS Valor FROM objetoinscrito"
            + " LEFT JOIN varios ON (varios.id=objetoinscrito.concepto) LEFT JOIN varios AS varios1 ON (varios1.id=objetoinscrito.unidades) "
            + " WHERE proyecto=%d";
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
        for (int i = 0; i < reader.FieldCount; i++)
        {
            m_pTabla.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
        }        
        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns[reader.GetName(0)] };
        return reader.FieldCount;
    }

    private DataTable GetDataTable()
    {
        CTablaObjeto tabla = (CTablaObjeto)HttpContext.Current.Session["objectostabla"];
        m_szCurrentID = HttpContext.Current.Session["idobjectos"] == null ? "" : HttpContext.Current.Session["idobjectos"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = CConector.GetConector();

        if (m_szCurrentID == null)
        {
            m_szCurrentID = "0";
        }

        m_pTabla = new DataTable();
        SqlDataReader reader = m_pConector.Query(m_szQuery.Replace("%d", m_szCurrentID));
        if (reader != null)
        {
            GetColumns(reader);
            object[] todos = new object[reader.FieldCount];
            while (reader.Read())
            {
                reader.GetValues(todos);
                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session["objectostabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["objectostabla"] = null;
    }

    static public DataTable GetTabla()
    {
        CTablaObjeto tabla = (CTablaObjeto)HttpContext.Current.Session["objectostabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
