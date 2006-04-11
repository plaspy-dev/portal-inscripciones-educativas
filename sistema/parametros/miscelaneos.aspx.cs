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

public partial class sistema_parametros_miscelaneos : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("varios", 4))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "tipo";
        m_saCampos[2] = "nombre";
        m_saCampos[3] = "descripcion";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Tipo";
        m_saTitulos[2] = "Nombre";
        m_saTitulos[3] = "Descripción";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1] = "misma.nombre";

        m_szIndice = m_saTitulos[0];

        m_szJoin = " LEFT JOIN varios AS misma ON (misma.tipo=0 AND misma.nReservado=varios.tipo) ";
        m_szCondicion = " (varios.nReservado IS NULL OR varios.nReservado!=-1) ";
        // autofiltro
        if (m_CBTipo.SelectedIndex > 0)
            m_szCondicionEx += " AND varios.tipo=" + m_CBTipo.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;

        FillComboBox(m_CBTipo, "SELECT nReservado,nombre FROM varios WHERE tipo=0 ORDER BY nombre");
     
        EndPageLoad();
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_nId.UpdateAfterCallBack = true;
        
        // busqueda en el combobox 
        ListItem item = m_CBTipo.Items.FindByText(row[1].ToString());
        m_CBTipo.SelectedIndex = item == null ? 0 : m_CBTipo.Items.IndexOf(item);
        m_CBTipo.UpdateAfterCallBack = true;

        m_ENombre.Text = row[2].ToString();
        m_ENombre.UpdateAfterCallBack = true;

        m_EDescripcion.Text = row[3].ToString();
        m_EDescripcion.UpdateAfterCallBack = true;

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelarP_Click(object sender, EventArgs e)
    {
        // mostrar los campos los campos
        
        m_CBTipo.Visible = true;
        m_CBTipo.UpdateAfterCallBack = true;
        m_ENombre.Visible = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_EDescripcion.Visible = true;
        m_EDescripcion.UpdateAfterCallBack = true;

        base.m_BCancelarP_Click(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_nId.UpdateAfterCallBack = true;
        m_CBTipo.SelectedIndex = 0;
        m_CBTipo.UpdateAfterCallBack = true;
        m_EDescripcion.Text = "";
        m_EDescripcion.UpdateAfterCallBack = true;
        m_ENombre.Text = "";
        m_ENombre.UpdateAfterCallBack = true;

        base.m_BCancelar_Click(sender, e);
    }

    override protected void m_BImprimir_Click(object sender, EventArgs e)
    {
        // ocultar los campos
        
        m_CBTipo.Visible = false;
        m_CBTipo.UpdateAfterCallBack = true;
        m_ENombre.Visible = false;
        m_ENombre.UpdateAfterCallBack = true;
        m_EDescripcion.Visible = false;
        m_EDescripcion.UpdateAfterCallBack = true;

        base.m_BImprimir_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";

        // incializar los datos a insertar
        m_saDatos[1]= m_CBTipo.SelectedValue;
        m_saDatos[2] = m_ENombre.Text;
        m_saDatos[3] = m_EDescripcion.Text;
        
        int id = m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos), true);
        if (id == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack=true;
            return;
        }
        string texto = "Objeto " + m_ENombre.Text + " Ingresado";
        if (m_saDatos[1] == "0" && id != 0)  // es un tipo de objecto
        {
            SqlDataReader reader = m_pConector.Query("SELECT MAX(nReservado) FROM varios WHERE tipo=0");
            int max = 0;
            if (reader != null && reader.Read())
            {
                max = Convert.ToInt32(reader[0].ToString()) + 1;
                m_pConector.QueryMod(CConector.CreateUpdate(m_szTabla, "nReservado", max.ToString())
                    + CConector.GetCondicion("id", id.ToString()));
                texto += " Con nReservado= " + max.ToString();
            }
        }
        CTablaEx.RebuilTable();
        m_BCancelar_Click(sender, e);
        m_SMensaje.Text = texto;
        m_LCResultados.DataBind();
        m_LCResultados.UpdateAfterCallBack = true;
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[1] = m_CBTipo.SelectedValue;
        m_saDatos[2] = m_ENombre.Text;
        m_saDatos[3] = m_EDescripcion.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            string texto = "Objeto " + m_ENombre.Text + " Editado";
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
        if (m_pConector.QueryMod("UPDATE varios SET nReservado=-1 WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            CTablaEx.RebuilTable();
            string texto = "Objeto " + m_ENombre.Text + " Eliminado";
            m_BCancelar_Click(sender, e);
            m_SMensaje.Text = texto;
            m_LCResultados.DataBind();
            m_LCResultados.UpdateAfterCallBack = true;
        }
    }
    protected void m_CBTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_STipo.Text = "";
        bool visible=true;
        if (m_CBTipo.SelectedIndex > 0)
        {
            SqlDataReader reader = m_pConector.Query("SELECT descripcion FROM varios WHERE tipo=0 AND nReservado=" + m_CBTipo.SelectedValue);
            if (reader != null && reader.Read())
                m_STipo.Text = reader[0].ToString();
            visible=false;
        }
        m_STipo.UpdateAfterCallBack = true;
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[2].Visible=visible;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}

