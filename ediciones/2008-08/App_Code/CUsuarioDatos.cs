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
    protected string m_szNombreReal;

    public CUsuarioDatos()
    {
        m_alVigencias = null;
        m_pEntes = null;
        m_szNombre = "";
        m_szVigencia = "";
        m_szID = "";
        m_szGrupo = "";
        m_szNombreReal = "";
    }

    public CUsuarioDatos(string nombre, string vigencia, string id, string grupo, string nombrereal)
    {
        m_szNombre = nombre;
        m_szVigencia = vigencia;
        m_szID = id;
        m_szGrupo = grupo;
        m_szNombreReal = nombrereal;
        m_alVigencias = null;
        m_pEntes = null;
    }

    public void AddEnte(string nombre, int id, string vigencia, int proyectosi, int proyectos, int convenios)
    {
        if (m_pEntes == null)
            m_pEntes = new CEntesTerritoriales();
        m_pEntes.Add(nombre, id, vigencia, proyectosi, proyectos, convenios);
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

    public string GetLogin()
    {
        return m_szNombre;
    }

    public string GetNombre()
    {
        return m_szNombreReal;
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

    public bool GetVigencias(DropDownList lista)
    {
        if (m_alVigencias == null)
        {
            return false;
        }
        if (lista.Items.Count > 0)
            return true;

        for (int i = 0; i < m_alVigencias.Count; i++)
        {
            lista.Items.Add(new ListItem(m_alVigencias[i].ToString()));
            if (m_szVigencia == m_alVigencias[i].ToString())
                lista.SelectedIndex = i;
        }
        return true;
    }

    public bool SetVigencia(string vigencia)
    {
        if (m_alVigencias == null)
            return false;
        for (int i = 0; i < m_alVigencias.Count; i++)
            if (m_alVigencias[i].ToString() == vigencia)
            {
                m_szVigencia = vigencia;
                CConector.SetCookie("vigencia", vigencia);
                return true;
            }
        return false;
    }

    public bool SetVigencia()
    {
        if( m_alVigencias==null || m_alVigencias.Count==0)
            return false;
        if (SetVigencia(CConector.GetCookie("vigencia", m_alVigencias[0].ToString())))
            return true;
        return SetVigencia(m_alVigencias[0].ToString());
    }

    public void FillComboBox(Anthem.DropDownList lista)
    {
        if (m_pEntes != null)
            m_pEntes.FillComboBox(lista, m_szVigencia);
    }

    public void FillComboBoxInscritos(Anthem.DropDownList lista)
    {
        if (m_pEntes != null)
            m_pEntes.FillComboBoxInscritos(lista, false, m_szVigencia);
    }

    public void FillComboBoxProyectos(Anthem.DropDownList lista)
    {
        if (m_pEntes != null)
            m_pEntes.FillComboBoxProyectos(lista,false, m_szVigencia);
    }
    public void FillComboBoxConvenios(Anthem.DropDownList lista)
    {
        if (m_pEntes != null)
            m_pEntes.FillComboBoxConvenios(lista,false, m_szVigencia);
    }

    public string GetCondicion(string nombrecampoente)
    {
        return nombrecampoente + " IN (SELECT enteterritorial FROM usuariosentes2 WHERE usuario=" + m_szID + " AND vigencia='" + m_szVigencia +"')";
    }
}
