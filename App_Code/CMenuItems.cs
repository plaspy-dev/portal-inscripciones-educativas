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
/// Summary description for CMenuItems
/// </summary>
public class CMenuItems
{
    protected ArrayList m_pItems;
    protected string m_szNombre;
    protected int m_nID;
    protected string m_szDescripcion;
    protected string m_szURL;
    protected int m_nPermiso;
    protected int m_nOrden;

    public CMenuItems()
    {
        m_pItems = null;
        m_szNombre = "";
        m_nID = 0;
        m_szDescripcion = "";
        m_szURL = "";
        m_nPermiso = 0;
        m_nOrden = 0;
    }

    public CMenuItems(string nombre, int id, string descripcion, string url,int orden)
    {
        m_pItems = null;
        m_szNombre = nombre;
        m_nID = id;
        m_szDescripcion = descripcion;
        m_szURL = url;
        m_nPermiso = 0;
        m_nOrden = orden;
    }

    public int AddMenu(string nombre, int id, string descripcion, string url, int padre, int orden)
    {
        if (m_nID == padre)
            return AddMenu(nombre, id, descripcion, url,orden);
        if (m_pItems == null)
            return 0;
        CMenuItems item = null;
        for (int i = 0; i < m_pItems.Count; i++)
        {
            item = (CMenuItems)m_pItems[i];
            if (item.m_nID == padre)
            {
                return item.AddMenu(nombre, id, descripcion, url,orden);
            }
            if (item.AddMenu(nombre, id, descripcion, url, padre,orden) != 0)
                return 1;
        }
        return 0;
    }

    public bool SetPermiso(int id, int permiso)
    {
        if (m_nID == id)
        {
            m_nPermiso = permiso;
            return true;
        }
        if (m_pItems != null)
        {
            CMenuItems item;
            for (int i = 0; i < m_pItems.Count; i++)
            {
                item = (CMenuItems)m_pItems[i];
                if (item.SetPermiso(id, permiso))
                    return true;
            }
        }
        return false;
    }

    public int AddMenu(string nombre, int id, string descripcion, string url, int orden)
    {
        if (m_pItems == null)
            m_pItems = new ArrayList();
        return m_pItems.Add(new CMenuItems(nombre, id, descripcion, url, orden));
    }

    public static string GetQuotedWord(string palabra)
    {
        return "'" + palabra + "'";
    }

    public string GetThisMenu()
    {
        if (m_nID == 0)
            return "";
        string total = "null,";
        total += GetQuotedWord(m_szNombre) + ",";
        total += GetQuotedWord(m_szURL) + ",";
        total += GetQuotedWord("") + ",";
        total += GetQuotedWord(m_szDescripcion);
        if (m_pItems != null)
            total += ",";
        return total + "\n";
    }

    protected bool CanShowMenu()
    {
        if (m_pItems != null)
        {
            CMenuItems item;
            for (int i = 0; i < m_pItems.Count; i++)
            {
                item = (CMenuItems)m_pItems[i];
                if (item.CanShowMenu())
                    return true;
            }
        }
        return m_nPermiso != 0 && m_nOrden!=-1;
    }

    public void SetPermisoEx(int permiso)
    {
        m_nPermiso = permiso;
        if (m_pItems != null)
        {
            CMenuItems item;
            for (int i = 0; i < m_pItems.Count; i++)
            {
                item = (CMenuItems)m_pItems[i];
                item.SetPermisoEx(permiso);
            }
        }
    }

    public string GetMenu()
    {
        if (!CanShowMenu())
            return "";
        string total = "[" + GetThisMenu();
        if (m_pItems != null)
        {
            CMenuItems item = null;
            int pos = 0;
            for (int i = 0; i < m_pItems.Count; i++)
            {
                item = (CMenuItems)m_pItems[i];
                if (item.CanShowMenu())
                {
                    if (pos != 0)
                    {
                        total += "_cmSplit,\n";
                    }
                    pos++;
                    total += item.GetMenu();
                    if (i != m_pItems.Count - 1)
                        total += ",\n";
                }
            }
        }
        return total + "]";
    }

    public int GetPermisos(string url)
    {
        if (url == m_szURL.ToLower())
            return m_nPermiso;
        if (m_pItems != null)
        {
            CMenuItems item;
            int permisos = 0;
            for (int i = 0; i < m_pItems.Count; i++)
            {
                item = (CMenuItems)m_pItems[i];
                if ((permisos = item.GetPermisos(url)) != -1)
                    return permisos;
            }
        }
        return -1;
    }

    public string GetFullPath(string url)
    {
        if (url == m_szURL.ToLower())
            return m_szNombre;
        if (m_pItems != null)
        {
            CMenuItems item;
            string path = "";
            for (int i = 0; i < m_pItems.Count; i++)
            {
                item = (CMenuItems)m_pItems[i];
                path = item.GetFullPath(url);
                if (path.Length != 0)
                    return m_szNombre + "/" + path;
            }
        }
        return "";
    }
}
