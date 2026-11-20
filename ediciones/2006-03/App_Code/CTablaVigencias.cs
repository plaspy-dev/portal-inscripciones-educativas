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
/// Summary description for CTablaVigencias
/// </summary>
public class CTablaVigencias
{
    CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CTablaVigencias()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "SELECT nombre FROM vigencias ORDER BY nombre";
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
        m_pTabla.Columns.Add("autorizados", typeof(bool));
        m_pTabla.Columns.Add("Vigencia", typeof(string));
        
        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns["Vigencia"] };
        return reader.FieldCount;
    }

    private DataTable GetDataTable()
    {
        CTablaVigencias tabla = (CTablaVigencias)HttpContext.Current.Session["vigenciastabla"];
        m_szCurrentID = HttpContext.Current.Session["idvigencias"] == null ? "" : HttpContext.Current.Session["idvigencias"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = (CConector)HttpContext.Current.Session["conector"];

        if (m_szCurrentID == null)
        {
            m_szCurrentID = "";
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
                todos[0] = m_szCurrentID.IndexOf(reader[0].ToString())!=-1;
                todos[1] = reader[0];

                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session["vigenciastabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["vigenciastabla"] = null;
    }

    static public DataTable GetTabla()
    {
        CTablaVigencias tabla = (CTablaVigencias)HttpContext.Current.Session["vigenciastabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
