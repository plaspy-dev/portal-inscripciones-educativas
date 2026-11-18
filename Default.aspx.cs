using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("pragma", "no-cache");
        Response.CacheControl = "Private";
        Response.Expires = 0;
        
        m_SFechaActual.Text = CPageTemplate.GetFechaHora(DateTime.Now);
        m_STituloLargo.Text = "Ingreso";
        m_SUsuarioActual.Text = "";

        if (!CConector.IsLogged(this))
        {
            // sin logear
            if (Request.Form.Count != 0)
            {
                if (Usuario.Text.Length == 0 || Contrasena.Text.Length == 0)
                    return;

                string grupo = "";
                string login = "";
                string vigencias = "";
                string id = "";
                string nombre = "";
                ArrayList vigenciasu = new ArrayList();

                CConector conector = CConector.GetConector();

                SqlDataReader result = conector.Query("SELECT login,clave,grupo,vigencias,id,nombre FROM usuarios2 where login = '" + Request["usuario"] + "' AND estado=1");

                if (result == null)
                {
                    mensaje.Text= conector.GetLastError();
                    if (mensaje.Text.Length == 0)
                    {
                        mensaje.Text = "Usuario o contraseña incorrecto";
                    }
                    System.Threading.Thread.Sleep(3000);
                    return;
                }
                else
                {
                    mensaje.Text = "Usuario o contraseña incorrecto";
                    if (result.Read())
                    {
                        if (result[1].ToString() != Request["contrasena"])
                        {
                            System.Threading.Thread.Sleep(3000);
                            return;
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(3000);
                        return;
                    }

                    grupo = result[2].ToString();
                    login = result[0].ToString();
                    vigencias = result[3].ToString();
                    id = result[4].ToString();
                    nombre = result[5].ToString();

                    int pos = 0;
                    while ((pos = vigencias.IndexOf(",")) != -1)
                    {
                        vigenciasu.Add(vigencias.Substring(0, pos));
                        vigencias = vigencias.Substring(pos + 1);
                    }
                    if (vigencias.Length != 0)
                        vigenciasu.Add(vigencias);
                }

                mensaje.Text = "";
                CUsuarioDatos usuario = new CUsuarioDatos(login, vigencias, id, grupo, nombre);
                usuario.SetVigencias(vigenciasu);
                usuario.SetVigencia();
                Session["usuario"]=usuario;
                conector.QueryMod("UPDATE usuarios2 SET usesion=GETDATE() WHERE id='" + id + "'");

                result = conector.Query("SELECT usuariosentes2.enteterritorial,eterritoriales.nombre,usuariosentes2.vigencia"
                    + ", (SELECT COUNT(*) FROM proyectosi WHERE proyectosi.eterritorial=usuariosentes2.enteterritorial AND proyectosi.vigencia=usuariosentes2.vigencia) AS proyectosi"
                    + ", (SELECT COUNT(*) FROM proyectos WHERE proyectos.eterritorial=usuariosentes2.enteterritorial AND proyectos.vigencia=usuariosentes2.vigencia) AS proyectos"
                    + ", (SELECT COUNT(*) FROM conveniosm WHERE conveniosm.eterritorial=usuariosentes2.enteterritorial AND conveniosm.vigencia=usuariosentes2.vigencia) AS convenios"
                    + " FROM usuariosentes2 LEFT JOIN eterritoriales ON eterritoriales.codigo=usuariosentes2.enteterritorial WHERE usuariosentes2.usuario=" + id + " ORDER BY eterritoriales.nombre");
                if (result != null)
                {
                    while (result.Read())
                    {
                        usuario.AddEnte(result[1].ToString(), Convert.ToInt32(result[0].ToString())
                            , result[2].ToString()
                            ,Convert.ToInt32(result[3].ToString())
                            , Convert.ToInt32(result[4].ToString())
                            , Convert.ToInt32(result[5].ToString())
                            );
                    }
                }
                CConector.SetCookie("usuario",Usuario.Text);
                Response.Redirect("entrada.aspx");
            }
        }
        else
        {
            string end = Request.QueryString["end"];
            if (end == "1")
            {
                Session.RemoveAll();
            }
            else
            {
                Response.Redirect("entrada.aspx");
            }
        }

        Usuario.Focus();
        if (Usuario.Text.Length == 0 || Contrasena.Text.Length == 0)
        {
            Usuario.Text = CConector.GetCookie("usuario");
        }
    }

    protected void bIngreso_Click(object sender, EventArgs e)
    {
    }
}
