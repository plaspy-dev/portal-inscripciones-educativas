using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class sistema_parametros_menus : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("menus", 6))
            return;
        m_saCampos[0] = "id";
        m_saCampos[1] = "nombre";
        m_saCampos[2] = "descripcion";
        m_saCampos[3] = "url";
        m_saCampos[4] = "padre";
        m_saCampos[5] = "orden";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Nombre";
        m_saTitulos[2] = "Descripción";
        m_saTitulos[3] = "URL";
        m_saTitulos[4] = "Padre";
        m_saTitulos[5] = "Orden";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[4] = "menuitem.nombre";

        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN menus AS menuitem ON (menuitem.id=menus.padre)";
        m_szCondicion = " (menus.orden!=-2 ) ";
        if (m_CBPadre.SelectedIndex > 0)
        {
            m_szCondicionEx += " AND menus.padre=" + m_CBPadre.SelectedValue;
        }

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;

        FillComboBox(m_CBPadre, "SELECT id,nombre FROM menus WHERE (url='' OR url IS NULL) AND orden!=-2 ORDER BY nombre");
        // m_EOrden.Attributes["onKeypress"] = "isnb();";
        m_szMensaje = "No se encontraron menus";
        EndPageLoad();
    }

    protected void m_CBPadre_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_SPadre.Text = "";
        bool visible = true;
        if (m_CBPadre.SelectedIndex > 0)
        {
            SqlDataReader reader = m_pConector.Query("SELECT descripcion FROM menus WHERE id=" + m_CBPadre.SelectedValue);
            if (reader != null && reader.Read())
                m_SPadre.Text = reader[0].ToString();
            visible = false;
        }
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[5].Visible=visible;
        m_LCResultados.UpdateAfterCallBack = true;
        m_SPadre.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();

        m_nId.UpdateAfterCallBack = true;
        m_ENombre.Text = row[1].ToString();
        m_ENombre.UpdateAfterCallBack = true;
        m_EDescripcion.Text = row[2].ToString(); 
        m_EDescripcion.UpdateAfterCallBack = true;
        m_EURL.Text = row[3].ToString();
        m_EURL.UpdateAfterCallBack = true;
        m_EOrden.Text = row[5].ToString();
        m_EOrden.UpdateAfterCallBack = true;

        ListItem item= m_CBPadre.Items.FindByText(row[4].ToString());
        m_CBPadre.SelectedIndex = item==null ? 0 : m_CBPadre.Items.IndexOf(item);
        m_CBPadre.UpdateAfterCallBack = true;

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelarP_Click(object sender, EventArgs e)
    {
        m_EURL.Visible = true;
        m_EURL.UpdateAfterCallBack = true;
        m_EDescripcion.Visible = true;
        m_EDescripcion.UpdateAfterCallBack = true;
        m_ENombre.Visible = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_CBPadre.Visible = true;
        m_CBPadre.UpdateAfterCallBack = true;
        m_EOrden.Visible = true;
        m_EOrden.UpdateAfterCallBack = true;

        base.m_BCancelarP_Click(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        m_nId.Value = "";
        m_nId.UpdateAfterCallBack = true;
        m_ENombre.Text = "";
        m_ENombre.UpdateAfterCallBack = true;
        m_EDescripcion.Text = "";
        m_EDescripcion.UpdateAfterCallBack = true;
        m_EURL.Text = "";
        m_EURL.UpdateAfterCallBack = true;
        m_CBPadre.SelectedIndex = 0;
        m_EOrden.Text = "";
        m_EOrden.UpdateAfterCallBack = true;
        
        base.m_BCancelar_Click(sender, e);
    }

    override protected void m_BImprimir_Click(object sender, EventArgs e)
    {
        m_EURL.Visible = false;
        m_EURL.UpdateAfterCallBack = true;
        m_EDescripcion.Visible = false;
        m_EDescripcion.UpdateAfterCallBack = true;
        m_ENombre.Visible = false;
        m_ENombre.UpdateAfterCallBack = true;
        m_CBPadre.Visible = false;
        m_CBPadre.UpdateAfterCallBack = true;
        m_EOrden.Visible = false;
        m_EOrden.UpdateAfterCallBack = true;

        base.m_BImprimir_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_EDescripcion.Text;
        m_saDatos[3] = m_EURL.Text;
        if (m_CBPadre.SelectedIndex > 0)
            m_saDatos[4] = m_CBPadre.SelectedValue;
        m_saDatos[5] = m_EOrden.Text;
        if (m_saDatos[5] == "")
            m_saDatos[5] = "1000";

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            string texto = "Menu " + m_ENombre.Text + " Ingresado";
            CTablaEx.RebuilTable();
            m_BCancelar_Click(sender, e);
            m_SMensaje.Text = texto;
            m_LCResultados.DataBind();
            m_LCResultados.UpdateAfterCallBack = true;
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_EDescripcion.Text;
        m_saDatos[3] = m_EURL.Text;
        if (m_saDatos[3].Length == 0)
            m_saDatos[3] = "%''";
        if (m_CBPadre.SelectedIndex > 0)
            m_saDatos[4] = m_CBPadre.SelectedValue;
        else m_saDatos[4] = "%NULL";
        m_saDatos[5] = m_EOrden.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            string texto = "Menu " + m_ENombre.Text + " Editado";
            CTablaEx.RebuilTable();
            m_BCancelar_Click(sender, e);
            m_SMensaje.Text = texto;
            m_LCResultados.DataBind();
            m_LCResultados.UpdateAfterCallBack = true;
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE menus SET orden=-2 WHERE id=" + m_nId.Value)== -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            CTablaEx.RebuilTable();
            string texto = "Menu " + m_ENombre.Text + " Eliminado";
            m_BCancelar_Click(sender, e);
            m_SMensaje.Text = texto;
            m_LCResultados.DataBind();
            m_LCResultados.UpdateAfterCallBack = true;
        }
    }
}
