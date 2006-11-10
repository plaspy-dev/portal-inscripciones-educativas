using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class fotos_proyectos_galeria : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("", 1))
            return;
        // quitar de aqui
        m_BSubir.Visible = (m_nPermisos & 4) == 4;
        if (!IsPostBack)
        {
            Session["idcontratofoto"] = Request.QueryString["id"];
            CGaleria gal = new CGaleria();
            gal.RebuilTable();
            m_CBVista.Items.Add(new ListItem("Iconos", "0"));
            m_CBVista.Items.Add(new ListItem("Presentación", "1"));
            if (CConector.GetCookie("galeriap", "0") == "0")
                m_CBVista.SelectedIndex = 0;
            else m_CBVista.SelectedIndex = 1;
            MultiView1.ActiveViewIndex = Convert.ToInt32(m_CBVista.SelectedValue);
        }
        EndPageLoad();
        if (!IsPostBack)
        {
            m_STituloLargo.Text = "Registro Fotográfico - Proyecto No." + Request.QueryString["proyecto"];
            m_LCFotos.DataBind();
            m_LCFotos_PageIndexChanged(null, null);
        }
    }

    public string DeleteString()
    {
        return (m_nPermisos&16)==16 ? "Eliminar" : "";
    }

    protected void m_LBFotos_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM fotos WHERE id=" + m_LBFotos.SelectedValue);
        CGaleria gal = new CGaleria();
        gal.RebuilTable();
        m_LBFotos.DataBind();
        m_LCFotos.DataBind();
        m_SMen.Text = "Foto eliminada";
        m_LCFotos_PageIndexChanged(sender, e);
    }
    protected void m_BSubir_Click(object sender, EventArgs e)
    {
        int len = m_ERegistro.PostedFile.ContentLength;
        if (len <= 0)
        {
            m_SMen.Text = "Por favor seleccione un archivo";
            return;
        }
        if (m_ERegistro.PostedFile.ContentType.IndexOf("jpeg") < 0)
        {
            m_SMen.Text = "Por favor seleccione un archivo con formato JPEG";
            return;
        }
        byte[] pic = new byte[len];
        m_ERegistro.PostedFile.InputStream.Read(pic, 0, len);
        CConector conector = new CConector();
        SqlConnection connection = conector.GetConexion();
        string query = "INSERT INTO fotos (fecha,proyecto,foto) VALUES (" + (m_EFechaRegistro.Text.Length > 0 ? ("'" + m_EFechaRegistro.Text + "'") : "GETDATE()") + ",@proyecto,@foto)";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@proyecto", Session["idcontratofoto"].ToString());
        cmd.Parameters.AddWithValue("@foto", pic);
        cmd.ExecuteNonQuery();

        CGaleria gal = new CGaleria();
        gal.RebuilTable();
        m_LBFotos.DataBind();
        m_LCFotos.DataBind();
        m_LCFotos_PageIndexChanged(sender, e);
        m_SMen.Text = "Foto agregada";
    }
    protected void m_CBVista_SelectedIndexChanged(object sender, EventArgs e)
    {
        CConector.SetCookie("galeriap", m_CBVista.SelectedValue);
        MultiView1.ActiveViewIndex = Convert.ToInt32(m_CBVista.SelectedValue);
    }
    protected void m_LCFotos_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_pConector.QueryMod("DELETE FROM fotos WHERE id=" + m_LCFotos.SelectedDataKey.Value);
        CGaleria gal = new CGaleria();
        gal.RebuilTable();
        m_LBFotos.DataBind();
        m_LCFotos.DataBind();
        m_LCFotos.UpdateAfterCallBack = true;
        m_SMen.Text = "Foto eliminada";
        m_SMen.UpdateAfterCallBack = true;
        m_LCFotos_PageIndexChanged(sender, e);
    }

    protected void m_LCFotos_PageIndexChanged(object sender, EventArgs e)
    {
        CGaleria gal= new CGaleria();
        DataTable table = gal.GetTabla();
        int total = table.Rows.Count;
        if (total == 0)
        {
            m_SFotoR.Text = "";
        }
        else
        {
            m_SFotoR.Text = "Foto " + (m_LCFotos.PageIndex + 1).ToString() + " de " + total.ToString() + " Fotos";
        }
        m_SFotoR.UpdateAfterCallBack = true;
    }
}