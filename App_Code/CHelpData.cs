using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CHelpData
/// </summary>
public class CHelpData
{
    protected string m_szHelp;
    protected string m_szIndex;
    protected bool  m_bVisible;

	public CHelpData(string index, string help, string key)
	{
        m_szIndex = index;
        m_szHelp = help;
        m_bVisible = false;
        if (key == null || key == "")
            m_bVisible = true;
        else if (index.ToLower() == key.ToLower())
            m_bVisible = true;
	}

    public string Help {
        get {
            return m_szHelp;
        }
    }
    public string Index {
        get {
            return m_szIndex;
        }
    }
    public bool Visible {
        get {
            return m_bVisible;
        }
    }
    public string Clase {
        get {
            if( m_bVisible)
                return "";
            return "style='display:none'";
        }
    }
}
