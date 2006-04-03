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
    string m_szCurrentVigencia;
    string m_szQuery;
    DataTable m_pTabla;

	public CTablaEntes()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "";
        m_szCurrentID = "";
        m_szCurrentVigencia = "";
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
        m_szCurrentVigencia = HttpContext.Current.Session["identesvigencia"] == null ? "" : HttpContext.Current.Session["identesvigencia"].ToString();

        if (tabla!= null && tabla.m_szCurrentID == m_szCurrentID && tabla.m_szCurrentVigencia==m_szCurrentVigencia)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = new CConector();

        if (m_szCurrentID == null || m_szCurrentID.Length == 0 || m_szCurrentVigencia == null || m_szCurrentVigencia.Length==0)
        {
            m_szQuery = "SELECT codigo,nombre,NULL FROM eterritoriales ORDER BY nombre";
        }
        else
        {
            if (m_szCurrentVigencia == null)
                m_szCurrentVigencia = "";
            m_szQuery = "SELECT eterritoriales.codigo,eterritoriales.nombre,usuariosentes2.enteterritorial FROM eterritoriales "
                + "LEFT JOIN usuariosentes2 ON (usuariosentes2.enteterritorial=eterritoriales.codigo AND usuariosentes2.usuario="
                + m_szCurrentID;
            if (m_szCurrentVigencia.Length > 0)
                m_szQuery += " AND usuariosentes2.vigencia='" + m_szCurrentVigencia + "'";
            m_szQuery+= ") ORDER BY eterritoriales.nombre";
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
