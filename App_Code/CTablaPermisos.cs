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
/// Summary description for CTablaPermisos
/// </summary>
public class CTablaPermisos
{
    CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CTablaPermisos()
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
        m_pTabla.Columns.Add("Todos", typeof(bool));
        m_pTabla.Columns.Add("id", typeof(int));
        m_pTabla.Columns.Add("Menu", typeof(string));
        m_pTabla.Columns.Add("Consulta", typeof(bool));
        m_pTabla.Columns.Add("Imprimir", typeof(bool));
        m_pTabla.Columns.Add("Insertar", typeof(bool));
        m_pTabla.Columns.Add("Modificar", typeof(bool));
        m_pTabla.Columns.Add("Eliminar", typeof(bool));
        
        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns["id"] };
        return reader.FieldCount;
    }

    private DataTable GetDataTable()
    {
        CTablaPermisos tabla = (CTablaPermisos)HttpContext.Current.Session["permisostabla"];
        m_szCurrentID = HttpContext.Current.Session["idpermisos"]==null ? "" : HttpContext.Current.Session["idpermisos"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = new CConector();

        if (m_szCurrentID == null || m_szCurrentID.Length == 0)
        {
            m_szQuery = "SELECT id,nombre,0 FROM menus WHERE url!='' ORDER BY nombre";
        }
        else
        {
            m_szQuery = "SELECT menus.id,menus.nombre,ISNULL(gruposupermisos.permisos, 0) FROM menus LEFT JOIN gruposupermisos ON (gruposupermisos.menu=menus.id AND gruposupermisos.grupo='"
                + m_szCurrentID
                +"') WHERE menus.url!='' ORDER BY menus.nombre";
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
                int permisos= Convert.ToInt32(reader[2]);
                todos[0] = permisos == 31;
                todos[1]= reader[0];
                todos[2] = reader[1];
                todos[3]= (permisos&1)==1;
                todos[4]= (permisos&2)==2;
                todos[5]= (permisos&4)==4;
                todos[6]= (permisos&8)==8;
                todos[7]= (permisos&16)==16;

                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session["permisostabla"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["permisostabla"] = null;
    }

    static public DataTable GetTabla()
    {
        CTablaPermisos tabla = (CTablaPermisos)HttpContext.Current.Session["permisostabla"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
