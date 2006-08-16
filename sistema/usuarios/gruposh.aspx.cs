using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class sistema_usuarios_gruposh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ayuda_ayuda masterpage = (ayuda_ayuda)Master;

            string clave = Request.QueryString["c"];
            ArrayList values = new ArrayList();
            values.Add(new CHelpData("Nombre", "Campo necesario para identificar el grupo.", clave));
            values.Add(new CHelpData("Descripción", "Descripción del grupo para facilitar su ubicación.", clave));
            values.Add(new CHelpData("Permisos", "Seleccione de la lista los permisos que desea asignar al grupo, basándose en cada menú izquierdo, puede asignar permisos de lectura, escritura, inserción, modificación y eliminación de registros.", clave));
            masterpage.Rep.DataSource = values;
            masterpage.Rep.DataBind();

            masterpage.Titulo = "Grupos";
            masterpage.Descripcion= "Por medio de este menú se crean grupos para así tener control sobre los permisos y privilegios de cada usuario.";
        }
    }
}
