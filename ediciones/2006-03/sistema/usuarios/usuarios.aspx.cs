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

public partial class sistema_usuarios_usuarios : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("usuarios", 9))
            return;

        m_saCampos[0] = "documento";
        m_saCampos[1] = "login";
        m_saCampos[2] = "grupo";
        m_saCampos[3] = "grupop";
        m_saCampos[4] = "nombre";
        m_saCampos[5] = "clave";
        m_saCampos[6] = "usesion";
        m_saCampos[7] = "estado";
        m_saCampos[8] = "vigencias";

        m_saTitulos[0] = "Identificación";
        m_saTitulos[1] = "Usuario";
        m_saTitulos[2] = "Grupo";
        m_saTitulos[3] = "Grupo P";
        m_saTitulos[4] = "Nombre";
        m_saTitulos[5] = "Clave";
        m_saTitulos[6] = "Ultimo Inicio de sesión";
        m_saTitulos[7] = "Activo";
        m_saTitulos[8] = "Vigencias";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[2] = "varios.nombre";
        m_saCamposAlias[3] = "grupos.nombre";
        m_saCamposAlias[4] = "personas.nombre";

        m_szCondicion = " usuarios.estado!=-1";

        m_szJoin = " LEFT JOIN varios ON (varios.id=usuarios.grupo) LEFT JOIN personas ON (personas.id=usuarios.nombre) "
            + " LEFT JOIN varios AS grupos ON (grupos.id=personas.grupop) ";

        m_szIndice = m_saTitulos[0];
        // autofiltro
        if (m_CBGrupo.SelectedIndex > 0)
            m_szCondicion += " AND usuarios.grupo=" + m_CBGrupo.SelectedValue;
        if (m_CBGrupoP.SelectedIndex > 0)
            m_szCondicion += " AND personas.grupop=" + m_CBGrupoP.SelectedValue;
        if (m_CBNombre.SelectedIndex > 0)
            m_szCondicion += " AND usuarios.nombre=" +  m_CBNombre.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);


        FillComboBox(m_CBGrupo, "SELECT id,nombre FROM varios WHERE tipo=1 AND (nReservado!=-1  OR nReservado IS NULL)  ORDER BY nombre");
        FillComboBox(m_CBGrupoP, "SELECT id,nombre FROM varios WHERE tipo=2 AND (nReservado!=-1  OR nReservado IS NULL) ORDER BY nombre");
        FillComboBox(m_CBNombre, m_CBGrupoP.SelectedIndex> 0 ? ("SELECT id,nombre FROM Personas WHERE grupop=" + m_CBGrupoP.SelectedValue + " ORDER BY nombre") : "", true);

        if (m_LBVigencias.Columns.Count == 0)
        {
            CheckBoxColumn checkCol = new CheckBoxColumn("DGSelectOrUnselectAllTodos", "updateOtros", m_LBVigencias.ID);
            checkCol.HeaderText = "";
            checkCol.DataField = "autorizados";
            checkCol.SetCheckInHeader(0);
            m_LBVigencias.Columns.Add(checkCol);

            BoundColumn data = new BoundColumn();
            data.HeaderText = "Vigencia";
            data.DataField = "Vigencia";
            m_LBVigencias.Columns.Add(data);

            m_LBVigencias.DataKeyField = "Vigencia";
            m_LBVigencias.DataBind();
        }

        if (m_LBEntes.Columns.Count == 0)
        {
            CheckBoxColumn checkCol = new CheckBoxColumn("DGSelectOrUnselectAllTodos", "updateOtros", m_LBEntes.ID);
            checkCol.HeaderText = "";
            checkCol.DataField = "autorizado";
            checkCol.SetCheckInHeader(0);
            m_LBEntes.Columns.Add(checkCol);

            BoundColumn data = new BoundColumn();
            data.HeaderText = "id";
            data.DataField = "id";
            data.Visible = false;
            m_LBEntes.Columns.Add(data);

            data = new BoundColumn();
            data.HeaderText = "Ente";
            data.DataField = "Nombre";
            m_LBEntes.Columns.Add(data);

            m_LBEntes.DataKeyField = "id";
            m_LBEntes.DataBind();
        }
        m_EDocumento.Attributes["onKeypress"] = "isnb();";

        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_EDocumento.UpdateAfterCallBack = true;
        m_EUsuario.UpdateAfterCallBack = true;
        m_CBGrupoP.UpdateAfterCallBack = true;
        m_CBGrupo.UpdateAfterCallBack = true;
        m_CBNombre.UpdateAfterCallBack = true;
        m_EContrasena.UpdateAfterCallBack = true;
        m_CActivo.UpdateAfterCallBack = true;
        m_LBVigencias.UpdateAfterCallBack = true;
        m_LBEntes.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_EDocumento.Visible = show;
        m_EUsuario.Visible = show;
        m_CBGrupoP.Visible = show;
        m_CBGrupo.Visible = show;
        m_CBNombre.Visible = show;
        m_EContrasena.Visible = show;
        m_CActivo.Visible = show;
        m_LBVigencias.Visible = show;
        m_LBEntes.Visible = show;
        Panel1.Visible = show;
        Panel1.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_EDocumento.Text = row[0].ToString();
        m_EUsuario.Text= row[1].ToString();

        ListItem item = m_CBGrupo.Items.FindByText(row[2].ToString());
        m_CBGrupo.SelectedIndex = item == null ? 0 : m_CBGrupo.Items.IndexOf(item);
        item = m_CBGrupoP.Items.FindByText(row[3].ToString());
        m_CBGrupoP.SelectedIndex = item == null ? 0 : m_CBGrupoP.Items.IndexOf(item);
        FillComboBox(m_CBNombre, m_CBGrupoP.SelectedIndex > 0 ? ("SELECT id,nombre FROM Personas WHERE grupop=" + m_CBGrupoP.SelectedValue + " ORDER BY nombre") : "", true);
        item = m_CBNombre.Items.FindByText(row[4].ToString());
        m_CBNombre.SelectedIndex = item == null ? 0 : m_CBNombre.Items.IndexOf(item);
        m_EContrasena.Text = row[5].ToString();
        m_CActivo.Checked = row[7].ToString() == "1";

        Session["idvigencias"] = row[8].ToString();
        m_LBVigencias.DataBind();
        Session["identes"]= m_nId.Value;
        m_LBEntes.DataBind();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_EDocumento.Text = "";
        m_EUsuario.Text = "";

        m_EContrasena.Text = "";
        m_CActivo.Checked = true;

        Session["idvigencias"] = "";
        m_LBVigencias.DataBind();
        Session["identes"] = "";
        m_LBEntes.DataBind();

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saDatos[0]= m_EDocumento.Text;
        m_saDatos[1]= m_EUsuario.Text;
        m_saDatos[2] = m_CBGrupo.SelectedValue;
        m_saCampos[3] = "";
        m_saDatos[4] = m_CBNombre.SelectedValue;
        m_saDatos[5] = m_EContrasena.Text;
        m_saCampos[6] = "";
        m_saDatos[7] = m_CActivo.Enabled ? "1" : "0";
        for( int i=0; i<m_LBVigencias.Items.Count; i++) {
            if (((CheckBox)m_LBVigencias.Items[i].Cells[0].Controls[0]).Checked)
                m_saDatos[8] += m_LBVigencias.Items[i].Cells[1].Text + ",";
        }

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;
        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos))== -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            string[] all = new string[2];
            all[0] = m_saDatos[0];
            for (int i = 0; i < m_LBEntes.Items.Count; i++)
            {
                if (((CheckBox)m_LBEntes.Items[i].Cells[0].Controls[0]).Checked)
                {
                    all[1] = m_LBEntes.Items[i].Cells[1].Text;
                    if (m_pConector.QueryMod(CConector.CrearInsercion("usuariosentes", all)) == -1)
                    {
                        m_SMensaje.Text = m_pConector.GetLastError();
                        m_SMensaje.UpdateAfterCallBack = true;
                        return;
                    }
                }
            }
            base.AfterUpdate("Usuario " + m_EUsuario.Text + " Ingresado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        // m_saDatos[1] = m_ENombre.Text;
        m_saDatos[0] = m_nId.Value;
        m_saDatos[1] = m_EUsuario.Text;
        m_saDatos[2] = m_CBGrupo.SelectedValue;
        m_saCampos[3] = "";
        m_saDatos[4] = m_CBNombre.SelectedValue;
        m_saDatos[5] = m_EContrasena.Text;
        m_saCampos[6] = "";
        m_saDatos[7] = m_CActivo.Enabled ? "1" : "0";
        for (int i = 0; i < m_LBVigencias.Items.Count; i++)
        {
            if (((CheckBox)m_LBVigencias.Items[i].Cells[0].Controls[0]).Checked)
                m_saDatos[8] += m_LBVigencias.Items[i].Cells[1].Text+",";
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
            string[] all = new string[2];
            string[] campos = new string[2];
            bool existe = false;
            SqlDataReader reader;

            campos[0]="documento";
            campos[1]="enteterritorial";
            all[0] = m_saDatos[0];

            for (int i = 0; i < m_LBEntes.Items.Count; i++)
            {
                all[1] = m_LBEntes.Items[i].Cells[1].Text;
                query = "SELECT documento FROM usuariosentes " + CConector.GetCondiciones(campos, all, "=");
                reader = m_pConector.Query(query);
                existe = (reader!=null && reader.Read());
                query = "";
                if (((CheckBox)m_LBEntes.Items[i].Cells[0].Controls[0]).Checked)
                {
                    if(!existe)
                        query = CConector.CrearInsercion("usuariosentes", all);
                }
                else if (existe)
                {
                    query = "DELETE FROM usuariosentes " + CConector.GetCondiciones(campos, all, "=");
                }
                if (query.Length>0 && m_pConector.QueryMod(query) == -1)
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                    return;
                }
            }
            base.AfterUpdate("Usuario " + m_EUsuario.Text + " Modificado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE usuarios SET estado=-1 WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Usuario " + m_EUsuario.Text + " Eliminado");
        }
    }
    protected void m_CBGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        m_LCResultados.Columns[3].Visible = m_CBGrupo.SelectedIndex == 0;
        m_LCResultados.Columns[4].Visible = m_CBGrupoP.SelectedIndex == 0;
        m_LCResultados.Columns[5].Visible = m_CBNombre.SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBGrupoP_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBGrupo_SelectedIndexChanged(sender, e);
        m_CBNombre.UpdateAfterCallBack = true;
    }
    protected void m_CBNombre_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBGrupo_SelectedIndexChanged(sender, e);
    }
}