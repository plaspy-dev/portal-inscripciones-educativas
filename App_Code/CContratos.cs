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
/// Summary description for CContratos
/// </summary>
public class CContratos
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CContratos()
	{
        m_pConector = null;
        m_pTabla = null;

        m_szQuery = "SELECT contratos.id AS id, contratos.codigo AS codigo, "
        + " contratos.fechas AS fechas, contratos.fechainicio AS fechainicio, clases.nombre AS clase,"
        +" contratos.objeto AS objeto, contratos.plazo AS plazo, contratos.valor AS valor,"
        +" estados.nombre AS estado, contratos.observaciones "
        +" FROM contratos"
        +" LEFT JOIN varios AS clases ON (clases.id=contratos.clase)"
        +" LEFT JOIN varios AS estados ON (estados.id=contratos.estado)"
        + " WHERE contratos.proyecto=%d" ;
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
        CContratos tabla = (CContratos)HttpContext.Current.Session["contratostabla"];
        m_szCurrentID = HttpContext.Current.Session["idproyecto"] == null ? "" : HttpContext.Current.Session["idproyecto"].ToString();

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
            object[] todos = new object[reader.FieldCount];
            while (reader.Read())
            {
                reader.GetValues(todos);
                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session["contratostabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["contratostabla"] = null;
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
        CContratos tabla = (CContratos)HttpContext.Current.Session["contratostabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
