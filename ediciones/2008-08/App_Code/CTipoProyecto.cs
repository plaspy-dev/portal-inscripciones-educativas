using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for CTipoProyecto
/// </summary>
public class CTipoProyecto : CTablaItems
{
	public CTipoProyecto() : base (3)
	{
        m_szId = "idtipoproyectos";
        m_szTitulos[0] = "id";
        m_szTitulos[1] = "szReservado";
        m_szTitulos[2] = "nombre";

        m_szCampos[0] = "varios.id";
        m_szCampos[1] = "CHARINDEX(CONVERT(VARCHAR,varios.id),misma.szReservado)";
        m_szCampos[2] = "varios.nombre";

        m_szQuery = CConector.CrearSelectT("varios", m_szCampos, m_szTitulos, " LEFT JOIN varios AS misma ON (misma.id=%d) WHERE varios.tipo=13 AND (varios.nReservado IS NULL OR varios.nReservado!=-1)");
	}

    override protected int GetColumns(SqlDataReader reader)
    {
        if (reader == null || m_pTabla == null)
            return 0;
        for (int i = 0; i < reader.FieldCount; i++)
        {
            m_pTabla.Columns.Add(reader.GetName(i),i==1 ? typeof(bool) : reader.GetFieldType(i));
        }
        m_pTabla.PrimaryKey = new DataColumn[] { m_pTabla.Columns[reader.GetName(0)] };
        return reader.FieldCount;
    }
    override protected DataTable GetDataTable()
    {
        CTablaItems tabla = (CTablaItems)HttpContext.Current.Session[this.ToString()];
        m_szCurrentID = HttpContext.Current.Session[m_szId] == null ? "" : HttpContext.Current.Session[m_szId].ToString();
        if (tabla != null && tabla.m_szCurrentID == m_szCurrentID)
        {
            m_pTabla = tabla.m_pTabla;
            return m_pTabla;
        }
        if (m_szCurrentID == null || m_szCurrentID == "")
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
                todos[1] = todos[1].ToString() == "0" ? false : true;
                m_pTabla.Rows.Add(todos);
            }
        }
        HttpContext.Current.Session[this.ToString()] = this;
        return m_pTabla;
    }
}

