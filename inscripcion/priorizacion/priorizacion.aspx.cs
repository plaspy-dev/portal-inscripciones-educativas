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

public partial class inscripcion_priorizacion_priorizacion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("proyectosi", 2))
            return;

        m_saCampos[0] = "priorizacion";
        m_saCampos[1] = "eterritorial";

        m_pUsuario.FillComboBox(m_EEntes);
        if (!IsPostBack)
        {
            BoundField data = new BoundField();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LCPriorizados.Columns.Add(data);
            m_LCSinPriorizar.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Código";
            m_LCPriorizados.Columns.Add(data);
            m_LCSinPriorizar.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Institución educativa";
            m_LCPriorizados.Columns.Add(data);
            m_LCSinPriorizar.Columns.Add(data);

            data = new BoundField();
            data.DataField = data.HeaderText = "Priorización";
            m_LCPriorizados.Columns.Add(data);

            Session["priorizadostabla"] = null;
            Session["sinpriorizartabla"] = null;
            Session["idente"] = null;
        }
        EndPageLoad();
        if (!IsPostBack)
        {
            m_BAceptar.Style["display"] = "";
            m_BCancelar.Style["display"] = "";
        }
    }

    override protected void UpdateAll()
    {
        m_EEntes.UpdateAfterCallBack = true;
        m_LCPriorizados.UpdateAfterCallBack = true;
        m_LCSinPriorizar.UpdateAfterCallBack = true;
    }

    override protected void ShowPrint(bool show)
    {
        base.ShowPrint(show);
        m_BAceptar.Style["display"] = show ? "none" : "";
        m_BCancelar.Style["display"] = show ? "none" : "";
        m_BAceptar.UpdateAfterCallBack = true;
        m_BCancelar.UpdateAfterCallBack = true;
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        Session["idente"] = 0;
        m_EEntes.SelectedIndex = -1;
        m_LCSinPriorizar.DataBind();
        m_LCPriorizados.DataBind();

        base.m_BCancelar_Click(sender, e);

        m_BAceptar.Style["display"] = "";
        m_BCancelar.Style["display"] = "";
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[0] = "";

        // editar
        m_saDatos[1] = m_EEntes.SelectedValue;
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos[0], m_saDatos[0]) + CConector.GetCondicion(m_saCampos[1], m_saDatos[1])
            + " AND proyectosi.vigencia='" +m_pUsuario.GetVigenciaActual() +"'";
        
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            m_saCampos[1] = "id";
            DataTable tablap = CTablaPriorizados.GetTabla();
            for (int i = 0; i < tablap.Rows.Count; i++)
            {
                m_saDatos[1]= tablap.Rows[i].ItemArray.GetValue(0).ToString();
                m_saDatos[0] = tablap.Rows[i][3].ToString();
                query = CConector.CreateUpdate(m_szTabla, m_saCampos[0], m_saDatos[0]) + CConector.GetCondicion(m_saCampos[1], m_saDatos[1]);
                if (m_pConector.QueryMod(query) == -1)
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                    return;
                }
            }
            base.AfterUpdate("Priorización completada");
            m_BAceptar.Style["display"] = "";
            m_BCancelar.Style["display"] = "";
        }
    }

    protected void Recalcular()
    {
        DataTable tablap = CTablaPriorizados.GetTabla();
        for (int i = 0; i < tablap.Rows.Count; i++)
        {
            tablap.Rows[i][3] = i + 1;
        }
        m_LCSinPriorizar.DataBind();
        m_LCPriorizados.DataBind();
        m_LCPriorizados.UpdateAfterCallBack = true;
        m_LCSinPriorizar.UpdateAfterCallBack = true;
    }
    protected void m_BIzquierda_Click(object sender, EventArgs e)
    {
        if (m_LCPriorizados.SelectedIndex == -1)
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Por favor seleccione un proyecto priorizado');");
            return;
        }
        DataTable tablap = CTablaPriorizados.GetTabla();
        DataTable tablas = CTablaSinPriorizar.GetTabla();
        DataRow row = GetDataRow(m_LCPriorizados, tablap);
        if (row[3].ToString() == "0")
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Este proyecto esta bloqueado intente con otro');");
            return;
        }
        object[] datos = new object[3];
        for (int i = 0; i < 3; i++)
            datos[i] = row.ItemArray.GetValue(i);
        tablap.Rows.Remove(row);
        tablas.Rows.Add(datos);
        Recalcular();
    }

    protected void m_BDerecha_Click(object sender, EventArgs e)
    {
        if (m_LCSinPriorizar.SelectedIndex == -1)
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Por favor seleccione un proyecto sin priorizar');");
            return;
        }
        DataTable tablap = CTablaPriorizados.GetTabla();
        DataTable tablas = CTablaSinPriorizar.GetTabla();
        DataRow row= GetDataRow(m_LCSinPriorizar, tablas);
        object []datos= new object[4];
        for (int i = 0; i < row.ItemArray.Length; i++)
            datos[i] = row.ItemArray.GetValue(i);
        datos[3] = 0;
        tablas.Rows.Remove(row);
        tablap.Rows.Add(datos);
        Recalcular();
    }

    protected void m_EEntes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["idente"] = m_EEntes.SelectedValue;
        m_LCSinPriorizar.DataBind();
        m_LCPriorizados.DataBind();
        m_LCPriorizados.UpdateAfterCallBack = true;
        m_LCSinPriorizar.UpdateAfterCallBack = true;
    }
    protected void m_ESubir_Click(object sender, EventArgs e)
    {
        if (m_LCPriorizados.SelectedIndex == -1)
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Por favor seleccione un proyecto priorizado');");
            return;
        }
        DataTable tablap = CTablaPriorizados.GetTabla();
        DataRow row = GetDataRow(m_LCPriorizados, tablap);
        if (row[3].ToString() == "0")
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Este proyecto esta bloqueado intente con otro');");
            return;
        }
        int index = tablap.Rows.IndexOf(row);
        if (index == 0)
        {
            return;
        }
        if (tablap.Rows[index - 1][3].ToString() == "0")
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Este proyecto no puede subir mas');");
            return;
        }
        DataRow nuevo = tablap.NewRow();
        for (int i = 0; i < row.ItemArray.Length; i++)
            nuevo[i] = row[i];
        
        tablap.Rows.Remove(row);
        tablap.Rows.InsertAt(nuevo, index - 1);

        for (int i = 0; i < tablap.Rows.Count; i++)
        {
            if (tablap.Rows[i][3].ToString() == "0")
                continue;
            tablap.Rows[i][3] = i + 1;
        }
        m_LCPriorizados.SelectedIndex = index - 1;
        m_LCPriorizados.DataBind();
        m_LCPriorizados.UpdateAfterCallBack = true;
    }
    protected void m_EBajar_Click(object sender, EventArgs e)
    {
        if (m_LCPriorizados.SelectedIndex == -1)
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Por favor seleccione un proyecto priorizado');");
            return;
        }
        DataTable tablap = CTablaPriorizados.GetTabla();
        DataRow row = GetDataRow(m_LCPriorizados, tablap);
        if (row[3].ToString() == "0")
        {
            Anthem.Manager.AddScriptForClientSideEval("alert('Este proyecto esta bloqueado intente con otro');");
            return;
        }
        int index= tablap.Rows.IndexOf(row);
        if (index == tablap.Rows.Count-1)
        {
            return;
        }
        DataRow nuevo = tablap.NewRow();
        for (int i = 0; i < row.ItemArray.Length; i++)
            nuevo[i] = row[i];

        tablap.Rows.Remove(row);
        tablap.Rows.InsertAt(nuevo, index + 1);

        for (int i = 0; i < tablap.Rows.Count; i++)
        {
            if (tablap.Rows[i][3].ToString() == "0")
                continue;
            tablap.Rows[i][3] = i + 1;
        }
        m_LCPriorizados.SelectedIndex = index + 1;
        m_LCPriorizados.DataBind();
        m_LCPriorizados.UpdateAfterCallBack = true;
    }
    protected void m_LCPriorizados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "Bloqueado";
                e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
        }
    }
}
