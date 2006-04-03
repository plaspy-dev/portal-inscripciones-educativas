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
/// Summary description for CDesembolsosConvenio
/// </summary>
public class CDesembolsosConvenio
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CDesembolsosConvenio()
	{
        m_pConector = null;
        m_pTabla = null;

        m_szQuery = "SELECT desembolsosconvenio.id AS id,desembolsosconvenio.numero AS numero,desembolsosconvenio.valor AS valor,"
        + "desembolsosconvenio.fecha AS fecha,desembolsosconvenio.comprobante AS comprobante,"
        + "desembolsosconvenio.fechacomprobante AS fechac, "
        + "desembolsosconvenio.observaciones AS observaciones FROM desembolsosconvenio WHERE desembolsosconvenio.convenio=%d";
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
        CDesembolsosConvenio tabla = (CDesembolsosConvenio)HttpContext.Current.Session["desembolsostabla"];
        m_szCurrentID = HttpContext.Current.Session["idconvenio"] == null ? "" : HttpContext.Current.Session["idconvenio"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = new CConector();

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
        HttpContext.Current.Session["desembolsostabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["desembolsostabla"] = null;
    }
    static public int GetNumero()
    {
        DataTable tabla = GetTabla();
        int numero=1;
        if (tabla != null)
            numero = tabla.Rows.Count + 1;
        return numero;
    }

    static public DataTable GetTabla()
    {
        CDesembolsosConvenio tabla = (CDesembolsosConvenio)HttpContext.Current.Session["desembolsostabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
