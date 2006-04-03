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
    protected ArrayList m_pVigencias;

    public CEntesTerritoriales()
    {
        m_pEntes = null;
        m_pIds = null;
        m_pVigencias = null;
    }

    public void Add(string ente, int id, string vigencia)
    {
        if (m_pEntes == null)
            m_pEntes = new ArrayList();
        if (m_pIds == null)
            m_pIds = new ArrayList();
        if (m_pVigencias == null)
            m_pVigencias = new ArrayList();
        m_pEntes.Add(ente);
        m_pIds.Add(id);
        m_pVigencias.Add(vigencia);
    }

    public int GetCount()
    {
        return m_pIds.Count;
    }

    public int GetId(int pos)
    {
        return (int)m_pIds[pos];
    }

    public void FillComboBox(Anthem.DropDownList lista, string vigencia)
    {
        FillComboBox(lista, false, vigencia);
    }

    public void FillComboBox(Anthem.DropDownList lista, bool force, string vigencia)
    {
        if (lista.Items.Count > 0)
            if (force)
                lista.Items.Clear();
            else return;
        lista.Items.Add(new ListItem("(Ninguno)", "-1"));
        for (int i = 0; i < m_pIds.Count; i++)
        {
            if( m_pVigencias[i].ToString()==vigencia)
                lista.Items.Add(new ListItem(m_pEntes[i].ToString(), m_pIds[i].ToString()));
        }
    }
}
