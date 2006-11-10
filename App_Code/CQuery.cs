using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

public class CQuery
{
    protected string m_szTabla;
    protected ArrayList m_saCampos;
    protected ArrayList m_saValores;
    protected ArrayList m_baQuoted;

    public CQuery(string tabla)
    {
        m_szTabla = tabla;
        m_saCampos = new ArrayList();
        m_saValores = new ArrayList();
        m_baQuoted = new ArrayList();
    }

    public string GetTabla()
    {

        return m_szTabla;
    }

    public void SetTabla(string tabla) {

        m_szTabla = tabla;
    }

    public static CQuery operator +(CQuery query, string valor)
    {

        query[""] = valor;
        return query;
    }

    public int GetCampo(string campo)
    {
        int index = -1;
        for (int i = 0; i < m_saCampos.Count; i++)
            if (m_saCampos[i].ToString() == campo)
            {
                index = i;
                break;
            }
        if (index == -1)
        {
            index = m_saCampos.Add(campo);
            m_saValores.Add("");
            m_baQuoted.Add(true);
        }
        return index;
    }

    public void AddCampo(string campo, string valor, bool quotes)
    {

        m_saValores[GetCampo(campo)] = valor;
        m_baQuoted[GetCampo(campo)] = quotes;
    }

    public void Clean()
    {

        m_saCampos.Clear();
        m_saValores.Clear();
        m_baQuoted.Clear();
    }

    static public string GetValor(string valor)
    {

        if (valor == null || valor.Length == 0)
            return "NULL";
        if (valor == "SYSDATE()")
            return valor;
        string otro = valor;
        otro = otro.Replace("\\", "\\\\");
        // otro = otro.Replace("\"", "\\\"");
        otro = otro.Replace("'", "''");
        return "'" + otro + "'";
    }

    public string GetUpdate(string condicion)
    {
        if (m_saValores.Count == 0)
            return "";

        string query;
        string valor;

        query = "UPDATE " + m_szTabla + " SET ";
        for (int i = 0; i < m_saCampos.Count; i++)
        {

            if (i != 0)
                query += ",";
            valor = ((bool)m_baQuoted[i]) ? GetValor(m_saValores[i].ToString()) : m_saValores[i].ToString();
            query += m_saCampos[i] + "=" + valor;
        }
        if (condicion.Length != 0)
            query += " WHERE " + condicion;
        return query;
    }

    public string GetInsert()
    {

        if (m_saValores.Count == 0)
            return "";
        string query;

        query = "INSERT INTO " + m_szTabla + " (";
        for (int i = 0; i < m_saCampos.Count; i++)
        {
            if (i != 0)
                query += ",";
            query += m_saCampos[i];
        }
        query += ") VALUES (";
        for (int i = 0; i < m_saValores.Count; i++)
        {
            if (i != 0)
                query += ",";
            query += ((bool)m_baQuoted[i]) ? GetValor(m_saValores[i].ToString()) : m_saValores[i].ToString();
        }
        query += ");";
        return query;
    }

    public string GetSelect(string condicion)
    {
        return GetSelect(condicion, "");
    }

    public string GetSelect(string condicion, string join)
    {

        if (m_saValores.Count == 0)
            return "";

        string query = "SELECT ";

        for (int i = 0; i < m_saCampos.Count; i++)
        {
            if (i != 0)
                query += ",";
            query += m_saValores[i] + " AS '" + m_saCampos[i] + "'";
        }

        query += " FROM " + m_szTabla + " ";
        query += join + " ";
        if (condicion.Length != 0)
            query += " WHERE " + condicion;
        return query;
    }

    public string GetDelete(string condicion)
    {
        return "DELETE FROM " + m_szTabla + condicion;
    }

    public string this[string campo]
    {
        get
        {
            return m_saValores[GetCampo(campo)].ToString();
        }
        set
        {
            m_saValores[GetCampo(campo.Length == 0 ? value : campo)] = value;
        }
    }
}
