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
        if (!PageLoad("usuarios2", 8))
            return;

        m_saCampos[0] = "id";
        m_saCampos[1] = "login";
        m_saCampos[2] = "grupo";
        m_saCampos[3] = "nombre";
        m_saCampos[4] = "clave";
        m_saCampos[5] = "usesion";
        m_saCampos[6] = "estado";
        m_saCampos[7] = "vigencias";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Usuario";
        m_saTitulos[2] = "Grupo";
        m_saTitulos[3] = "Nombre";
        m_saTitulos[4] = "Clave";
        m_saTitulos[5] = "Ultimo Inicio de sesión";
        m_saTitulos[6] = "Activo";
        m_saTitulos[7] = "Vigencias";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[2] = "varios.nombre";

        m_szCondicion = " usuarios2.estado!=-1";

        m_szJoin = " LEFT JOIN varios ON (varios.id=usuarios2.grupo)";

        m_szIndice = m_saTitulos[0];
        // autofiltro
        if (m_CBGrupo.SelectedIndex > 0)
            m_szCondicionEx += " AND usuarios2.grupo=" + m_CBGrupo.SelectedValue;

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion + m_szCondicionEx);
        FillComboBox(m_CBGrupo, "SELECT id,nombre FROM varios WHERE tipo=1 AND (nReservado!=-1  OR nReservado IS NULL)  ORDER BY nombre");
        FillComboBox(m_CBVigencias, "SELECT nombre,nombre FROM vigencias");

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
        m_baHidden[0] = true;
        m_baHidden[4]=true;

        m_szMensaje = "No se encontraron usuarios";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_EUsuario.UpdateAfterCallBack = true;
        m_CBGrupo.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_EContrasena.UpdateAfterCallBack = true;
        m_EConfirmar.UpdateAfterCallBack = true;
        m_CActivo.UpdateAfterCallBack = true;
        m_LBVigencias.UpdateAfterCallBack = true;
        m_LBEntes.UpdateAfterCallBack = true;
        m_SDVigencias.UpdateAfterCallBack = true;
        m_CBVigencias.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();

        m_EUsuario.Text= row[1].ToString();
        SelectInCombo(m_CBGrupo, row[2].ToString());
        m_ENombre.Text= row[3].ToString();
        m_CActivo.Checked = row[6].ToString() == "1";

        m_SDVigencias.Text = "Sobrescribe los entes de todas las vigencias";
        m_CBVigencias.SelectedIndex = 0;

        Session["idvigencias"] = row[7].ToString();
        m_LBVigencias.DataBind();
        Session["identesvigencia"] = "";
        Session["identes"]= m_nId.Value;
        m_LBEntes.DataBind();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_EUsuario.Text = "";
        m_ENombre.Text = "";
        m_EContrasena.Text = "";
        m_EConfirmar.Text = "";
        m_SDVigencias.Text = "Sobrescribe los entes de todas las vigencias";
        m_CActivo.Checked = true;
        m_CBVigencias.SelectedIndex = 0;
        m_CBGrupo.SelectedIndex = 0;

        Session["idvigencias"] = "";
        m_LBVigencias.DataBind();
        Session["identesvigencia"] = "";
        Session["identes"] = "";
        m_LBEntes.DataBind();

        base.m_BCancelar_Click(sender, e);
    }

    protected bool UpdateEntes()
    {
        if (m_nId.Value.Length == 0)
            return true;
        string[] vigencias;
        if (m_CBVigencias.SelectedIndex > 0)
        {
            vigencias = new string[1];
            vigencias[0] = m_CBVigencias.SelectedValue;
            if (m_pConector.QueryMod("DELETE FROM usuariosentes2 WHERE usuario=" + m_nId.Value + " AND vigencia='" + m_CBVigencias.SelectedValue + "'") == -1)
                return false;
        }
        else
        {
            bool continuar = false;
            for (int i = 0; i < m_LBEntes.Items.Count; i++)
            {
                if (((CheckBox)m_LBEntes.Items[i].Cells[0].Controls[0]).Checked)
                {
                    continuar = true;
                    break;
                }
            }
            if (continuar == false)
                return true;

            vigencias = new string[m_CBVigencias.Items.Count - 1];
            for (int i = 1; i < m_CBVigencias.Items.Count; i++)
            {
                vigencias[i - 1] = m_CBVigencias.Items[i].Value;
            }
            if (m_pConector.QueryMod("DELETE FROM usuariosentes2 WHERE usuario=" + m_nId.Value) == -1)
                return false;
        }
        string[] all = new string[3];
        all[0] = m_nId.Value;

        for (int j = 0; j < vigencias.Length; j++)
        {
            all[2] = vigencias[j];
            for (int i = 0; i < m_LBEntes.Items.Count; i++)
            {
                if (((CheckBox)m_LBEntes.Items[i].Cells[0].Controls[0]).Checked)
                {
                    all[1] = m_LBEntes.Items[i].Cells[1].Text;
                    if (m_pConector.QueryMod(CConector.CrearInsercion("usuariosentes2", all)) == -1)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        m_saCampos[0] = "";
        // insertar
        m_saDatos[1]= m_EUsuario.Text;
        m_saDatos[2] = m_CBGrupo.SelectedValue;
        m_saDatos[3] = m_ENombre.Text;
        m_saDatos[4] = m_EContrasena.Text;
        m_saDatos[5] = "";
        m_saDatos[6] = m_CActivo.Checked ? "1" : "0";

        for( int i=0; i<m_LBVigencias.Items.Count; i++) {
            if (((CheckBox)m_LBVigencias.Items[i].Cells[0].Controls[0]).Checked)
                m_saDatos[7] += m_LBVigencias.Items[i].Cells[1].Text + ",";
        }

        // incializar los datos a insertar
        // m_saDatos[1] = m_ENombre.Text;
        int id=m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos), true);
        if (id== -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            m_nId.Value = id.ToString();
            if (UpdateEntes())
                base.AfterUpdate("Usuario " + m_EUsuario.Text + " Ingresado");
            else
            {
                m_SMensaje.Text = m_pConector.GetLastError();
                m_SMensaje.UpdateAfterCallBack = true;
            }
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[1] = m_EUsuario.Text;
        m_saDatos[2] = m_CBGrupo.SelectedValue;
        m_saDatos[3] = m_ENombre.Text;
        m_saDatos[4] = m_EContrasena.Text;
        m_saDatos[5] = "";
        m_saDatos[6] = m_CActivo.Checked ? "1" : "0";

        for (int i = 0; i < m_LBVigencias.Items.Count; i++)
        {
            if (((CheckBox)m_LBVigencias.Items[i].Cells[0].Controls[0]).Checked)
                m_saDatos[7] += m_LBVigencias.Items[i].Cells[1].Text + ",";
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
            if (UpdateEntes())
                base.AfterUpdate("Usuario " + m_EUsuario.Text + " Modificado");
            else
            {
                m_SMensaje.Text = m_pConector.GetLastError();
                m_SMensaje.UpdateAfterCallBack = true;
            }
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE usuarios2 SET estado=-1 WHERE id=" + m_nId.Value) == -1)
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
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBVigencias_SelectedIndexChanged(object sender, EventArgs e)
    {
        string vigencia = Session["identesvigencia"] == null ? "" : Session["identesvigencia"].ToString();
        if (vigencia.Length > 0)
        {
            int old = m_CBVigencias.SelectedIndex;
            SelectInCombo(m_CBVigencias, vigencia);
            UpdateEntes();
            m_CBVigencias.SelectedIndex = old;
        }
        m_SDVigencias.Text = m_CBVigencias.SelectedIndex>0 ? "" : "Sobrescribe los entes de todas las vigencias";
        m_SDVigencias.UpdateAfterCallBack = true;
        Session["identesvigencia"] = m_CBVigencias.SelectedIndex>0 ? m_CBVigencias.SelectedValue : "";
        m_LBEntes.DataBind();
        m_LBEntes.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            e.Row.Cells[7].Text = e.Row.Cells[7].Text == "1" ? "Si" : "No";
        }
    }
}
