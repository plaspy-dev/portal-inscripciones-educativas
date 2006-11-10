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
using System.Collections;

public class CAspectosTecnicos
{
	CConector m_pConector;
    string m_szCurrentID;
    string m_szQuery;
    DataTable m_pTabla;

	public CAspectosTecnicos()
	{
        m_pConector = null;
        m_pTabla = null;
        m_szQuery = "SELECT aspectostecnicos.id AS did,aspectostecnicos.id AS iddoc,varios.id AS id,varios.nombre AS Documento"
            + ",aspectostecnicos.cantidad AS Cantidad,aspectostecnicos.observaciones AS Observaciones FROM varios "
            + " LEFT JOIN aspectostecnicos ON (aspectostecnicos.aspecto=varios.id AND aspectostecnicos.proyecto=%d) "
            + " WHERE varios.tipo=52 AND (varios.nReservado IS NULL OR varios.nReservado!=-1) ";
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
        CAspectosTecnicos tabla = (CAspectosTecnicos)HttpContext.Current.Session["aspectoproyecto"];
        m_szCurrentID = HttpContext.Current.Session["idproyecto"] == null ? "" : HttpContext.Current.Session["idproyecto"].ToString();

        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        m_pConector = CConector.GetConector();

        if (m_szCurrentID == null || m_szCurrentID=="")
        {
            m_szCurrentID = "0";
        }

        // buscamos entradas repetidas para todos los elementos y los eliminamos
        string eliminar = "SELECT varios.id AS id,COUNT(*) FROM varios LEFT JOIN aspectostecnicos ON (aspectostecnicos.aspecto=varios.id AND aspectostecnicos.proyecto=%d) WHERE varios.tipo=52 AND (varios.nReservado IS NULL OR varios.nReservado!=-1) GROUP BY varios.id HAVING COUNT(*)>1;";
        SqlDataReader temp = m_pConector.Query(eliminar.Replace("%d", m_szCurrentID));
        if(temp!=null) {
            ArrayList lista = new ArrayList();
            while(temp.Read()) 
                lista.Add(temp[0].ToString());
            for(int i=0; i<lista.Count; i++) {

                ArrayList ids = new ArrayList();
                temp= m_pConector.Query("SELECT id FROM aspectostecnicos WHERE aspecto=" + lista[i].ToString() + " AND proyecto=" + m_szCurrentID);
                if (temp != null)
                {
                    while (temp.Read())
                    {
                        ids.Add(temp[0].ToString());
                    }
                }
                for(int j=1; j<ids.Count; j++) {

                    m_pConector.QueryMod("DELETE FROM aspectostecnicos WHERE id=" + ids[j].ToString());
                }
            }
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
        HttpContext.Current.Session["aspectoproyecto"] = this;
        return m_pTabla;
    }

    static public void RebuilTable()
    {
        HttpContext.Current.Session["aspectoproyecto"] = null;
    }

    static public DataTable GetTabla()
    {
        CAspectosTecnicos tabla = (CAspectosTecnicos)HttpContext.Current.Session["aspectoproyecto"];
        if (tabla == null)
            return null;
        return tabla.m_pTabla;
    }

    public DataTable Select()
    {
        return GetDataTable();
    }
}
