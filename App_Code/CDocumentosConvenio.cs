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
/// Summary description for CDocumentosConvenio
/// </summary>
public class CDocumentosConvenio
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CDocumentosConvenio()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "SELECT documentosconveniom.id AS did,documentosconveniom.id AS iddoc,varios.id AS id,varios.nombre AS Documento,documentosconveniom.fecha AS Fecha "
            + ",documentosconveniom.numero AS Número,documentosconveniom.observaciones AS Observaciones FROM varios "
            + " LEFT JOIN documentosconveniom ON (documentosconveniom.documento=varios.id AND documentosconveniom.convenio=%d) "
            + " WHERE varios.tipo=45 AND (varios.nReservado IS NULL OR varios.nReservado!=-1) ";
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
            m_pTabla.Columns.Add(reader.GetName(i),i==0 ? typeof(bool) : reader.GetFieldType(i));
        }        
        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns[reader.GetName(2)] };
        return reader.FieldCount;
    }

    private DataTable GetDataTable()
    {
        CDocumentosConvenio tabla = (CDocumentosConvenio)HttpContext.Current.Session["documentosconvenio"];
        m_szCurrentID = HttpContext.Current.Session["idconvenio"] == null ? "" : HttpContext.Current.Session["idconvenio"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = (CConector)HttpContext.Current.Session["conector"];

        if (m_szCurrentID == null || m_szCurrentID=="")
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
                todos[0] = todos[0].ToString()=="" ? false : true;
                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session["documentosconvenio"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["documentosconvenio"] = null;
    }

    static public DataTable GetTabla()
    {
        CDocumentosConvenio tabla = (CDocumentosConvenio)HttpContext.Current.Session["documentosconvenio"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
