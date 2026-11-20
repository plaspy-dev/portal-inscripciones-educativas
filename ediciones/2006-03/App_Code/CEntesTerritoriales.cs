using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for CEntesTerritoriales
/// </summary>
public class CEntesTerritoriales
{
    protected ArrayList m_pEntes;
    protected ArrayList m_pIds;

    public CEntesTerritoriales()
    {
        m_pEntes = null;
        m_pIds = null;
    }

    public void Add(string ente, int id)
    {
        if (m_pEntes == null)
            m_pEntes = new ArrayList();
        if (m_pIds == null)
            m_pIds = new ArrayList();
        m_pEntes.Add(ente);
        m_pIds.Add(id);
    }

    public int GetCount()
    {
        return m_pIds.Count;
    }

    public int GetId(int pos)
    {
        return (int)m_pIds[pos];
    }

    public void FillComboBox(Anthem.DropDownList lista)
    {
        FillComboBox(lista, false);
    }

    public void FillComboBox(Anthem.DropDownList lista, bool force)
    {
        if (lista.Items.Count > 0)
            if (force)
                lista.Items.Clear();
            else return;
        lista.Items.Add(new ListItem("(Ninguno)", "-1"));
        for (int i = 0; i < m_pIds.Count; i++)
            lista.Items.Add(new ListItem(m_pEntes[i].ToString(), m_pIds[i].ToString()));
    }
}
