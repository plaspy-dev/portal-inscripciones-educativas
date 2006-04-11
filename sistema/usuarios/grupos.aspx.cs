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

public partial class sistema_usuarios_grupos : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("varios", 3))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "nombre";
        m_saCampos[2] = "descripcion";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Nombre";
        m_saTitulos[2] = "Descripción";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_szIndice = m_saTitulos[0];
        m_szCondicion = " tipo=1 AND (nReservado IS NULL OR nReservado!=-1) ";

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        m_baHidden[0] = true;

        if (m_LBPermisos.Columns.Count == 0)
        {
            CheckBoxColumn checkCol = new CheckBoxColumn("DGSelectOrUnselectAllTodos", "updateOtros", m_LBPermisos.ID);
            checkCol.HeaderText = "Todos";
            checkCol.DataField = "Todos";
            checkCol.SetCheckInHeader(0);
            m_LBPermisos.Columns.Add(checkCol);

            BoundColumn data = new BoundColumn();
            data.HeaderText = "id";
            data.DataField = "id";
            data.Visible = false;
            m_LBPermisos.Columns.Add(data);

            data = new BoundColumn();
            data.HeaderText = "Menu";
            data.DataField = "Menu";
            m_LBPermisos.Columns.Add(data);

            checkCol = new CheckBoxColumn("DGSelectOrUnselectAllOtros", "updateTodos", m_LBPermisos.ID);
            checkCol.HeaderText = "Consulta";
            checkCol.DataField = "Consulta";
            checkCol.SetCheckInHeader(1);
            m_LBPermisos.Columns.Add(checkCol);

            checkCol = new CheckBoxColumn("DGSelectOrUnselectAllOtros", "updateTodos", m_LBPermisos.ID);
            checkCol.HeaderText = "Imprimir";
            checkCol.DataField = "Imprimir";
            checkCol.SetCheckInHeader(2);
            m_LBPermisos.Columns.Add(checkCol);

            checkCol = new CheckBoxColumn("DGSelectOrUnselectAllOtros", "updateTodos", m_LBPermisos.ID);
            checkCol.HeaderText = "Insertar";
            checkCol.DataField = "Insertar";
            checkCol.SetCheckInHeader(3);
            m_LBPermisos.Columns.Add(checkCol);

            checkCol = new CheckBoxColumn("DGSelectOrUnselectAllOtros", "updateTodos", m_LBPermisos.ID);
            checkCol.HeaderText = "Modificar";
            checkCol.DataField = "Modificar";
            checkCol.SetCheckInHeader(4);
            m_LBPermisos.Columns.Add(checkCol);

            checkCol = new CheckBoxColumn("DGSelectOrUnselectAllOtros", "updateTodos", m_LBPermisos.ID);
            checkCol.HeaderText = "Eliminar";
            checkCol.DataField = "Eliminar";
            checkCol.SetCheckInHeader(5);
            m_LBPermisos.Columns.Add(checkCol);

            m_LBPermisos.DataKeyField = "id";
            m_LBPermisos.DataBind();
        }
        m_szMensaje = "No se encontraron grupos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_EDescripcion.UpdateAfterCallBack = true;
        m_LBPermisos.UpdateAfterCallBack = true;
    }

    
    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        Session["idpermisos"] = m_nId.Value;
        m_ENombre.Text = row[1].ToString();
        m_EDescripcion.Text = row[2].ToString();
        m_LBPermisos.DataBind();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ENombre.Text = "";
        m_EDescripcion.Text = "";
        Session["idpermisos"] = "";
        m_LBPermisos.DataBind();

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "tipo";
        m_saDatos[0] = "1";

        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_EDescripcion.Text;

        int id= m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos), true);

        m_saCampos[0] = "";

        if ( id== -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
            return;
        }
        else
        {
            int permisos,iter;
            m_saDatos[0] = id.ToString();
            for (int i = 0; i < m_LBPermisos.Items.Count; i++)
            {
                permisos = 0;
                iter = 1;
                for (int j = 3; j < 8; j++)
                {
                    if (((CheckBox)m_LBPermisos.Items[i].Cells[j].Controls[0]).Checked)
                        permisos+= iter;
                    iter *= 2;
                }
                m_saDatos[1] = m_LBPermisos.Items[i].Cells[1].Text;
                m_saDatos[2] = permisos.ToString();
                if (m_pConector.QueryMod(CConector.CrearInsercion("gruposupermisos", m_saDatos)) == -1)
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                    return ;
                }
            }
            base.AfterUpdate("Grupo " + m_ENombre.Text + " Ingresado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saCampos[0] = "";

        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_EDescripcion.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion("id", m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            SqlDataReader reader = null;

            m_saCampos[0] = "grupo";
            m_saCampos[1] = "menu";

            int permisos, iter;

            m_saDatos[0] = m_nId.Value;
            m_saCampos[2] = "";

            for (int i = 0; i < m_LBPermisos.Items.Count; i++)
            {
                permisos = 0;
                iter = 1;
                for (int j = 3; j < 8; j++)
                {
                    if (((CheckBox)m_LBPermisos.Items[i].Cells[j].Controls[0]).Checked)
                        permisos += iter;
                    iter *= 2;
                }
                m_saDatos[1] = m_LBPermisos.Items[i].Cells[1].Text;
                m_saDatos[2] = permisos.ToString();
                

                if ((reader = m_pConector.Query("SELECT id,permisos FROM gruposupermisos" + CConector.GetCondiciones(m_saCampos, m_saDatos, "="))) != null && reader.Read())
                { // existe
                    if (reader[1].ToString() != m_saDatos[2])
                    {
                        if (m_pConector.QueryMod(CConector.CreateUpdate("gruposupermisos", "permisos", permisos.ToString()) + CConector.GetCondicion("id", reader[0].ToString())) == -1)
                        {
                            m_SMensaje.Text = m_pConector.GetLastError();
                            m_SMensaje.UpdateAfterCallBack = true;
                            return;
                        }
                    }
                }
                else if (m_pConector.QueryMod(CConector.CrearInsercion("gruposupermisos", m_saDatos)) == -1)
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                    return;
                }
            }

            base.AfterUpdate("Grupo " + m_ENombre.Text + " Editado");
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
            base.AfterUpdate("Grupo " + m_ENombre.Text + " Eliminado");
        }
    }
}
