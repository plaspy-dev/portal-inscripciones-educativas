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
/// Summary description for CTablaSinPriorizar
/// </summary>
public class CTablaSinPriorizar
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CTablaSinPriorizar()
	{
        m_pConector = null;
        m_pTabla = null;
        CUsuarioDatos usuario = (CUsuarioDatos)HttpContext.Current.Session["usuario"];
        m_szQuery = "SELECT proyectosi.id AS id,proyectosi.codigo AS Código,insteducativas.nombre AS 'Institución educativa' FROM proyectosi "
            + "LEFT JOIN insteducativas ON insteducativas.id=proyectosi.institucion WHERE proyectosi.vigencia='" + usuario.GetVigenciaActual() 
            + "' AND proyectosi.eterritorial=%d "
            + "AND (proyectosi.priorizacion IS NULL OR proyectosi.priorizacion=0) ";
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
        CTablaSinPriorizar tabla = (CTablaSinPriorizar)HttpContext.Current.Session["sinpriorizartabla"];
        m_szCurrentID = HttpContext.Current.Session["idente"] == null ? "" : HttpContext.Current.Session["idente"].ToString();

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
        HttpContext.Current.Session["sinpriorizartabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["sinpriorizartabla"] = null;
    }

    static public DataTable GetTabla()
    {
        CTablaSinPriorizar tabla = (CTablaSinPriorizar)HttpContext.Current.Session["sinpriorizartabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
