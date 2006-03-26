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
/// Summary description for CAdicionesConvenio
/// </summary>
public class CAdicionesConvenio
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CAdicionesConvenio()
	{
        m_pConector = null;
        m_pTabla = null;

        m_szQuery = "SELECT adicionesconvenio.id AS id,adicionesconvenio.valor AS Valor,"
            + "adicionesconvenio.fecha AS FechaV, adicionesconvenio.cdp AS CDP," 
            + " adicionesconvenio.fechacdp AS FechaCDP,adicionesconvenio.rdp AS RDP, adicionesconvenio.fechardp AS FechaRDP, "
            + "adicionesconvenio.plazo AS Plazo,adicionesconvenio.fecha AS FechaP,"
            +" varios.nombre AS Estado, adicionesconvenio.observaciones AS Observaciones"
            + " FROM adicionesconvenio "
            + " LEFT JOIN varios ON (varios.id=adicionesconvenio.estado) "
            + " WHERE adicionesconvenio.convenio=%d";
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
        m_pTabla.Columns.Add("Número",typeof(int));
        for (int i = 0; i < reader.FieldCount; i++)
        {
            m_pTabla.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
        }
        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns[reader.GetName(0)] };
        return reader.FieldCount;
    }

    private DataTable GetDataTable()
    {
        CAdicionesConvenio tabla = (CAdicionesConvenio)HttpContext.Current.Session["adicionestabla"];
        m_szCurrentID = HttpContext.Current.Session["idconvenio"] == null ? "" : HttpContext.Current.Session["idconvenio"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = (CConector)HttpContext.Current.Session["conector"];

        if (m_szCurrentID == null)
        {
            m_szCurrentID = "0";
        }

        m_pTabla = new DataTable();
        SqlDataReader reader = m_pConector.Query(m_szQuery.Replace("%d", m_szCurrentID));
        if (reader != null)
        {
            GetColumns(reader);
            object[] todos = new object[reader.FieldCount+1];
            int total = 1;
            while (reader.Read())
            {
                todos[0] = total;
                for( int i=0; i<reader.FieldCount; i++)
                    todos[i+1]= reader[i];
                if (todos[2].ToString() == "")
                    todos[3] = null;
                if (todos[8].ToString() == "")
                    todos[9] = null;
                m_pTabla.Rows.Add(todos);
                total++;
            }
        }
        HttpContext.Current.Session["adicionestabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["adicionestabla"] = null;
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
        CAdicionesConvenio tabla = (CAdicionesConvenio)HttpContext.Current.Session["adicionestabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
