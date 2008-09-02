using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Collections;

public class CHelpPage : System.Web.UI.Page
{
    public string m_szTitulo;
    public string m_szDescripcion;

	public CHelpPage()
	{
        m_szTitulo = "";
        m_szDescripcion = "";
	}

    public void Cargar(ArrayList values)
    {
        if (!IsPostBack)
        {
            string url = Request.Url.LocalPath;
            url = url.Replace(".aspx", ".xml");
            url = Server.MapPath(url);

            string clave = Request.QueryString["c"];

            XmlTextReader reader = new XmlTextReader(url);

            string titulo = "";
            string descripcion = "";
            string nombre = "";

            while (reader.Read())
            {
                if (!reader.IsEmptyElement)
                {
                    nombre = reader.Name.ToLower();
                    if (nombre == "titulo")
                    {
                        reader.Read();
                        titulo = reader.ReadString();
                    }
                    else if (nombre == "descripcion")
                    {
                        reader.Read();
                        descripcion = reader.ReadString();
                        values.Add(new CHelpData(titulo, descripcion, clave));
                    }
                    else if (nombre == "tayuda")
                    {
                        reader.Read();
                        m_szTitulo = reader.ReadString();
                    }
                    else if (nombre == "dayuda")
                    {
                        reader.Read();
                        m_szDescripcion = reader.ReadString();
                    }
                }
            }
            reader.Close();
        }
    }
}
