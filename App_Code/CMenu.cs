using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CMenu
/// </summary>
public class CMenu
{
    protected string m_szMenu;
    protected CMenuItems m_pItems;

    public CMenu(Page padre)
    {
        m_szMenu = "";

        CConector conector = CConector.GetConector();
        CUsuarioDatos usuario = (CUsuarioDatos)padre.Session["usuario"];

        SqlDataReader result = conector.Query("SELECT * FROM Menus WHERE orden=NULL OR orden!=-2 ORDER BY padre,orden", true);
        m_pItems = new CMenuItems();

        while (result.Read())
        {
            string nombre = result[1].ToString();
            string id = result[0].ToString();
            string descripcion = result[2].ToString();
            string url = result[3].ToString();
            string parent = result[4].ToString();
            string orden = result[5].ToString();
            if (orden.Length == 0)
                orden = "0";
            if (parent.Length == 0)
                parent = "0";
            m_pItems.AddMenu(nombre, Convert.ToInt32(id), descripcion, HttpContext.Current.Request.ApplicationPath + url, Convert.ToInt32(parent),Convert.ToInt32(orden));
        }
        if (CConector.GetProfileInt("administrador", 0) == 1)
        {
            m_pItems.SetPermisoEx(1);
        }
        else if ((result = conector.Query("SELECT menu,permisos FROM gruposupermisos WHERE grupo='" + usuario.GetGrupo() + "'", true)) != null)
        {
            while (result.Read())
            {
                m_pItems.SetPermiso(Convert.ToInt32(result[0].ToString()), Convert.ToInt32(result[1].ToString()));
            }
        }

        m_pItems.AddMenu("Configurar", -5, "Configura la aplicación", "", 0);
        m_pItems.AddMenu("Contraseña", -4, "Cambia la contraseña", HttpContext.Current.Request.ApplicationPath + "/conf/contrasena/contrasena.aspx", -5);
        m_pItems.AddMenu("Sistema", -3, "Cambia la configuración del aplicativo", HttpContext.Current.Request.ApplicationPath + "/conf/app/app.aspx", -5);
        m_pItems.AddMenu("Limpiar", -1, "Cierra las Ventanas", HttpContext.Current.Request.ApplicationPath + "/entrada.aspx", 0);
        m_pItems.AddMenu("Terminar", -2, "Cierra las Sesión Actual", HttpContext.Current.Request.ApplicationPath + "/default.aspx?end=1", 0);
        m_pItems.SetPermiso(-3, 1);
        m_pItems.SetPermiso(-4, 1);
        m_pItems.SetPermiso(-1, 1);
        m_pItems.SetPermiso(-2, 1);
        m_szMenu = m_pItems.GetMenu();
    }

    public int GetPermisos(string url)
    {
        return m_pItems.GetPermisos(url.ToLower());
    }

    public string GetMenu()
    {
        string resultado = "";
        resultado += "<script type=\"text/javascript\">\n";
        resultado += "var menuItems = \n";
        // aqui va el menu
        resultado += m_szMenu + ";\n";
        // listo
        resultado += "cmDraw ('menu', menuItems, 'vbr', cmThemeMiniBlack, 'ThemeMiniBlack');\n";
        resultado += "</script>\n";
        return resultado;
    }
    public string GetFullPath(string url)
    {
        return m_pItems.GetFullPath(url.ToLower());
    }
}
