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
/// Summary description for CDateFormat
/// </summary>
public class CDateFormat
{
    protected string m_szCampo1;
    protected string m_szCampo2;
    protected string m_szCampo3;
    protected string m_szCampo4;
    protected string m_szSeparador1;
    protected string m_szSeparador2;
    protected string m_szSeparador3;

	public CDateFormat()
	{
        m_szCampo1 = CConector.GetCookie("fecha1", "MMMM");
        m_szCampo2 = CConector.GetCookie("fecha2", "dd");
        m_szCampo3 = CConector.GetCookie("fecha3", "yyyy");
        m_szCampo4 = CConector.GetCookie("fecha4", "");
        m_szSeparador1 = CConector.GetCookie("separador1", " ");
        m_szSeparador2 = CConector.GetCookie("separador2", " de ");
        m_szSeparador3 = CConector.GetCookie("separador3", "");
	}

    public void Guardar()
    {
        CConector.SetCookie("fecha1",m_szCampo1);
        CConector.SetCookie("fecha2",m_szCampo2);
        CConector.SetCookie("fecha3",m_szCampo3);
        CConector.SetCookie("fecha4",m_szCampo4);
        CConector.SetCookie("separador1",m_szSeparador1);
        CConector.SetCookie("separador2",m_szSeparador2);
        CConector.SetCookie("separador3", m_szSeparador3);
    }

    public void SetCampo(string campo, int index)
    {
        if (campo == "-1")
            campo = "";
        switch (index)
        {
            case 1:
                m_szCampo1 = campo;
                break;
            case 2:
                m_szCampo2 = campo;
                break;
            case 3:
                m_szCampo3 = campo;
                break;
            case 4:
                m_szCampo4 = campo;
                break;
        }
    }
    public void SetSeparador(string separador, int index)
    {
        switch (index)
        {
            case 1:
                m_szSeparador1 = separador;
                break;
            case 2:
                m_szSeparador2 = separador;
                break;
            case 3:
                m_szSeparador3 = separador;
                break;
        }
    }
    public string GetCampo(int index)
    {
        switch (index)
        {
            case 1:
                return m_szCampo1;
            case 2:
                return m_szCampo2;
            case 3:
                return m_szCampo3;
            case 4:
                return m_szCampo4;
        }
        return "";
    }
    public string GetSeparador(int index)
    {
        switch (index)
        {
            case 1:
                return m_szSeparador1;
            case 2:
                return m_szSeparador2;
            case 3:
                return m_szSeparador3;
        }
        return "";
    }
    static public string GetDate(DateTime fecha)
    {
        CDateFormat temp = new CDateFormat();
        return temp.GetFecha(fecha);
    }

    public string GetFecha(DateTime fecha)
    {
        string formato="";
        formato += m_szCampo1;
        formato += "'" + m_szSeparador1 + "'";
        formato += m_szCampo2;
        formato += "'" + m_szSeparador2 + "'";
        formato += m_szCampo3;
        formato += "'" + m_szSeparador3 + "'";
        formato += m_szCampo4;
        return fecha.ToString(formato);
    }
    public static void GetComboBox(DropDownList lista)
    {
        if (lista.Items.Count > 0)
            return;
        lista.Items.Add(new ListItem("(Ninguno)", "-1"));
        lista.Items.Add(new ListItem("Nombre del día completo", "dddd"));
        lista.Items.Add(new ListItem("Nombre del día Abreviado", "ddd"));
        lista.Items.Add(new ListItem("Número de día", "dd"));
        lista.Items.Add(new ListItem("Nombre del mes completo", "MMMM"));
        lista.Items.Add(new ListItem("Nombre del mes abreviado", "MMM"));
        lista.Items.Add(new ListItem("Número de mes", "MM"));
        lista.Items.Add(new ListItem("Año completo", "yyyy"));
        lista.Items.Add(new ListItem("Año abreviado", "yy"));
    }
}
