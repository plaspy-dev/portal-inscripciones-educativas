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


public class CTablaItems
{
    protected CConector m_pConector;
    protected string m_szCurrentID;
    protected string m_szQuery;
    protected DataTable m_pTabla;
    protected string [] m_szCampos;
    protected string[] m_szTitulos;
    protected string m_szId;
    protected CUsuarioDatos m_pUsuario;

    public CTablaItems()
    {
        m_pConector = CConector.GetConector();
        m_szCurrentID = "";
        m_szQuery = "";
        m_szId = "";
        m_pTabla = null;
        m_pUsuario = (CUsuarioDatos)HttpContext.Current.Session["usuario"];
    }

	public CTablaItems(int columnas)
	{
        m_pConector = CConector.GetConector();
        m_szCurrentID = "";
        m_szQuery = "";
        m_szId = "";
        m_pTabla = null;
        m_szCampos= new string[columnas];
        m_szTitulos = new string[columnas];
        m_pUsuario = (CUsuarioDatos)HttpContext.Current.Session["usuario"];
        for (int i = 0; i < m_szCampos.Length; i++)
            m_szCampos[i] = m_szTitulos[i] = "";
	}

    virtual protected int GetColumns(SqlDataReader reader)
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

    virtual protected DataTable GetDataTable()
    {
        CTablaItems tabla = (CTablaItems)HttpContext.Current.Session[this.ToString()];
        m_szCurrentID = HttpContext.Current.Session[m_szId] == null ? "" : HttpContext.Current.Session[m_szId].ToString();
        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
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
                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session[this.ToString()] = this;
        return m_pTabla;
    }

    virtual public void RebuilTable()
    {
        HttpContext.Current.Session[this.ToString()] = null;
    }

    virtual public int GetNumero()
    {
        DataTable tabla = GetTabla();
        int numero = 1;
        if (tabla != null)
            numero = tabla.Rows.Count + 1;
        return numero;
    }

    virtual public DataTable GetTabla()
    {
        CTablaItems tabla = (CTablaItems)HttpContext.Current.Session[this.ToString()];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    virtual public DataTable Select()
    {
        return GetDataTable();
    }

    virtual public DataTable Select(int id)
    {
        HttpContext.Current.Session[m_szId] = id.ToString();
        return GetDataTable();
    }
}
