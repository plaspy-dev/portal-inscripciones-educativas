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
        if (!PageLoad("varios", 5))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "tipo";
        m_saCampos[2] = "nombre";
        m_saCampos[3] = "descripcion";
        m_saCampos[4] = "szReservado";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Tipo";
        m_saTitulos[2] = "Nombre";
        m_saTitulos[3] = "Descripción";
        m_saTitulos[4] = "szReservado";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];
        m_saCamposAlias[1] = "misma.nombre";

        m_szIndice = m_saTitulos[0];

        m_szJoin = " LEFT JOIN varios AS misma ON (misma.tipo=0 AND misma.nReservado=varios.tipo) ";
        m_szCondicion = " (varios.nReservado IS NULL OR varios.nReservado!=-1) ";
        // autofiltro
        if (!IsPostBack)
        {
            m_BInsertar.Style["display"] = "none";
            m_nTipo.Value = "-1";
            m_szMensaje = "Por favor seleccione un tipo.";
        }
        m_szCondicion += " AND varios.tipo=" + m_nTipo.Value;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        m_baHidden[0] = true;
        m_baHidden[4] = true;

        if (m_LBTipoProyectos.Columns.Count == 0)
        {
            CheckBoxColumn checkCol = new CheckBoxColumn("DGSelectOrUnselectAllTodos", "updateOtros", m_LBTipoProyectos.ID);
            checkCol.HeaderText = "";
            checkCol.DataField = "szReservado";
            checkCol.SetCheckInHeader(0);
            m_LBTipoProyectos.Columns.Add(checkCol);

            BoundColumn data = new BoundColumn();
            data.HeaderText = "id";
            data.DataField = "id";
            data.Visible = false;
            m_LBTipoProyectos.Columns.Add(data);

            data = new BoundColumn();
            data.HeaderText = "Tipo Proyecto";
            data.DataField = "nombre";
            m_LBTipoProyectos.Columns.Add(data);

            m_LBTipoProyectos.DataKeyField = "id";
            m_LBTipoProyectos.DataBind();
            if(!IsPostBack)
                m_LBTipoProyectos.Visible = false;
        }

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

        m_ENombre.Text = row[2].ToString();
        m_ENombre.UpdateAfterCallBack = true;

        m_EDescripcion.Text = row[3].ToString();
        m_EDescripcion.UpdateAfterCallBack = true;

        CTipoProyecto tipo = new CTipoProyecto();
        tipo.Select(Convert.ToInt32(m_nId.Value));
        m_LBTipoProyectos.DataBind();
        m_LBTipoProyectos.UpdateAfterCallBack = true;

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelarP_Click(object sender, EventArgs e)
    {
        // mostrar los campos los campos

        m_ENombre.Visible = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_EDescripcion.Visible = true;
        m_EDescripcion.UpdateAfterCallBack = true;

        base.m_BCancelarP_Click(sender, e);
        if (m_nTipo.Value == "-1")
        {
            m_BInsertar.Style["display"] = "none";
        }
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_nId.UpdateAfterCallBack = true;
        m_EDescripcion.Text = "";
        m_EDescripcion.UpdateAfterCallBack = true;
        m_ENombre.Text = "";
        m_ENombre.UpdateAfterCallBack = true;

        CTipoProyecto tipo = new CTipoProyecto();
        tipo.Select(0);
        m_LBTipoProyectos.DataBind();
        m_LBTipoProyectos.UpdateAfterCallBack = true;

        base.m_BCancelar_Click(sender, e);
        if (m_nTipo.Value == "-1")
        {
            m_BInsertar.Style["display"] = "none";
        }
    }

    override protected void m_BImprimir_Click(object sender, EventArgs e)
    {
        // ocultar los campos

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
        m_saDatos[1] = m_nTipo.Value;
        m_saDatos[2] = m_ENombre.Text;
        m_saDatos[3] = m_EDescripcion.Text;

        if (m_nTipo.Value == "15")
        {
            for (int i = 0; i < m_LBTipoProyectos.Items.Count; i++)
            {
                if (((CheckBox)m_LBTipoProyectos.Items[i].Cells[0].Controls[0]).Checked)
                    m_saDatos[4] += m_LBTipoProyectos.Items[i].Cells[1].Text + ",";
            }
        }

        int id = m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos), true);
        if (id == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
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
        m_saDatos[1] = m_nTipo.Value;
        m_saDatos[2] = m_ENombre.Text;
        m_saDatos[3] = m_EDescripcion.Text;


        if (m_nTipo.Value == "15")
        {
            for (int i = 0; i < m_LBTipoProyectos.Items.Count; i++)
            {
                if (((CheckBox)m_LBTipoProyectos.Items[i].Cells[0].Controls[0]).Checked)
                    m_saDatos[4] += m_LBTipoProyectos.Items[i].Cells[1].Text + ",";
            }
        }

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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (sender == null)
            return;

        Anthem.LinkButton link = (Anthem.LinkButton)sender;

        m_BCancelar_Click(sender, null);
        m_STitulo.Text = link.Text;

        string tipo = link.ID;
        tipo= tipo.Remove(0,10);
        tipo = tipo.Replace("a", "");
        m_nTipo.Value = tipo;
        m_BInsertar.Style["display"] = "";

        m_LBTipoProyectos.Visible = m_nTipo.Value=="15";
        m_LBTipoProyectos.UpdateAfterCallBack = true;

        m_szCondicion = " (varios.nReservado IS NULL OR varios.nReservado!=-1) ";
        m_szCondicion+= " AND varios.tipo=" + tipo;
        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        Session["query"] = m_szQuery;
        
        m_STitulo.UpdateAfterCallBack = true;
        m_nTipo.UpdateAfterCallBack = true;
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[2].Visible = false;
        m_LCResultados.UpdateAfterCallBack = true;
    }
}

