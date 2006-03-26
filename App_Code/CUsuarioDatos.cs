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
/// Summary description for CUsuarioDatos
/// </summary>
public class CUsuarioDatos
{
    protected string m_szNombre;
    protected string m_szVigencia;
    protected string m_szID;
    protected string m_szGrupo;
    protected ArrayList m_alVigencias;
    protected CEntesTerritoriales m_pEntes;

    public CUsuarioDatos()
    {
        m_alVigencias = null;
        m_pEntes = null;
        m_szNombre = "";
        m_szVigencia = "";
        m_szID = "";
        m_szGrupo = "";
    }

    public CUsuarioDatos(string nombre, string vigencia, string id, string grupo)
    {
        m_szNombre = nombre;
        m_szVigencia = vigencia;
        m_szID = id;
        m_szGrupo = grupo;
        m_alVigencias = null;
        m_pEntes = null;
    }

    public void AddEnte(string nombre, int id)
    {
        if (m_pEntes == null)
            m_pEntes = new CEntesTerritoriales();
        m_pEntes.Add(nombre, id);
    }

    public void SetVigencias(ArrayList lista)
    {
        m_alVigencias = new ArrayList();
        for (int i = 0; i < lista.Count; i++)
            m_alVigencias.Add(lista[i]);
    }

    public void AddVigencia(string vigencia)
    {
        if (m_alVigencias == null)
            m_alVigencias = new ArrayList();
        m_alVigencias.Add(vigencia);
    }

    public string GetNombre()
    {
        return m_szNombre;
    }

    public string GetVigenciaActual()
    {
        return m_szVigencia;
    }

    public string GetID()
    {
        return m_szID;
    }

    public string GetGrupo()
    {
        return m_szGrupo;
    }

    public bool SetVigencia(string vigencia)
    {
        if (m_alVigencias == null)
            return false;
        for (int i = 0; i < m_alVigencias.Count; i++)
            if (m_alVigencias[i].ToString() == vigencia)
                return true;
        return false;
    }
    public void FillComboBox(Anthem.DropDownList lista)
    {
        if (m_pEntes != null)
            m_pEntes.FillComboBox(lista);
    }

    public string GetCondicion(string nombrecampoente)
    {
        return nombrecampoente + " IN (SELECT enteterritorial FROM usuariosentes WHERE documento=" + m_szID + ")";
    }
}
