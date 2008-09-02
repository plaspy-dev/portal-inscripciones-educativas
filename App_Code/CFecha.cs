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
/// Summary description for CFecha
/// </summary>
public class CFecha
{
	public CFecha()
	{
	}

    public static string GetMes(int mes)
    {
        switch (mes)
        {
            case 1:
                return "Enero";
            case 2:
                return "Febrero";
            case 3:
                return "Marzo";
            case 4:
                return "Abril";
            case 5:
                return "Mayo";
            case 6:
                return "Junio";
            case 7:
                return "Julio";
            case 8:
                return "Agosto";
            case 9:
                return "Septiembre";
            case 10:
                return "Octubre";
            case 11:
                return "Noviembre";
            case 12:
                return "Diciembre";
        }
        return "";
    }

    public static void FillMeses(Anthem.DropDownList lista)
    {
        if (lista.Items.Count > 0)
            return;
        lista.Items.Add(new ListItem("(Ninguno)", "-1"));
        for( int i=1; i<13; i++)
            lista.Items.Add(new ListItem(GetMes(i), i.ToString()));
    }
    public static void FillAnos(Anthem.DropDownList lista, int iniciar)
    {
        if (lista.Items.Count > 0)
            return;
        lista.Items.Add(new ListItem("(Ninguno)", "-1"));
        for (int i = iniciar; i < DateTime.Now.Year; i++)
        {
            lista.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }
}
