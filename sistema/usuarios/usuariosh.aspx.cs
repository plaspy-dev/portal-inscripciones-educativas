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

public partial class sistema_usuarios_usuariosh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ayuda_ayuda masterpage = (ayuda_ayuda)Master;

            string clave = Request.QueryString["c"];
            ArrayList values = new ArrayList();
            values.Add(new CHelpData("Usuario", "Nombre del usuario para con el cual va a ingresar al sistema.", clave));
            values.Add(new CHelpData("Grupo", "Grupo al cual va a pertenecer el usuario, para revisar los permisos de los grupos revise el menú sistema/usuarios/grupos.", clave));
            values.Add(new CHelpData("Nombre", "Nombre real del usuario ejemplo.", clave));
            values.Add(new CHelpData("Contraseña", "Contraseña con la cual el usuario va a ingresar al sistema, el usuario la puede cambiar luego por medio del menú, en la sección contraseña, al dejar el campo en blanco la contraseña no se edita en el usuario actual.", clave));
            values.Add(new CHelpData("Confirmar", "Confirmación de la contraseña anterior.", clave));
            values.Add(new CHelpData("Activo", "Activar el usuario para ingresar al sistema.", clave));
            values.Add(new CHelpData("Vigencias", "Define las vigencias a las cuales el usuario tiene permisos.", clave));
            values.Add(new CHelpData("Entes territoriales", "En este campo usted puede definir los entes territoriales por vigencia o realizar una asignación global, seleccionando en “vigencias de los entes” la vigencia o (Todas) para una asignación global.", clave));

            masterpage.Rep.DataSource = values;
            masterpage.Rep.DataBind();

            masterpage.Titulo = "Usuarios";
            masterpage.Descripcion = "Por medio de esta interfaz se crean los diferentes usuarios del sistema, asignándoles un grupo para los privilegios, las vigencias y los entes territoriales.";

        }

    }
}
