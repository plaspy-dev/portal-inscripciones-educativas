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

public partial class sistema_vigencias_vigencias : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("vigencias", 4))
            return;

        m_saCampos[0] = "nombre";
        m_saCampos[1] = "maximoproyectos";
        m_saCampos[2] = "presupuesto";
        m_saCampos[3] = "cierre";

        m_saTitulos[0] = "Vigencia";
        m_saTitulos[1] = "Máximo Proyectos";
        m_saTitulos[2] = "Valor Presupuesto Nacional";
        m_saTitulos[3] = "Fecha Cierre Inscripción Proyectos";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_szCondicion = " vigencias.nReservado!=-1 ";

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);

        m_szIndice = m_saTitulos[0];

        m_EPresupuesto.Attributes["onKeypress"] = "isfloat(this);";
        m_EPresupuesto.Attributes["onblur"] = "tcomas(this);";
        m_EPresupuesto.Attributes["onfocus"] = "scomas(this);";

        m_EMaximo.Attributes["onKeypress"] = "isnb();";

        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_EVigencia.UpdateAfterCallBack = true;
        m_EMaximo.UpdateAfterCallBack = true;
        m_EPresupuesto.UpdateAfterCallBack = true;
        m_EFecha.UpdateAfterCallBack = true;
    }

    override protected void ShowControls(bool show)
    {
        m_EVigencia.Visible = show;
        m_EMaximo.Visible = show;
        m_EPresupuesto.Visible = show;
        m_EFecha.Visible = show;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_EVigencia.Text = row[0].ToString();
        m_EMaximo.Text = row[1].ToString();
        m_EPresupuesto.Text = row[2].ToString();
        m_EFecha.Text = row[3].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        m_nId.Value = "";
        m_EVigencia.Text = "";
        m_EMaximo.Text = "";
        m_EPresupuesto.Text = "";
        m_EFecha.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saDatos[0]=m_EVigencia.Text;
        m_saDatos[1]= m_EMaximo.Text;
        m_saDatos[2]= GetDouble(m_EPresupuesto.Text);
        m_saDatos[3]= m_EFecha.Text;

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Vigencia " + m_EVigencia.Text + " Ingresada");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        // campos a actualizar
        m_saDatos[0] = m_EVigencia.Text;
        m_saDatos[1] = m_EMaximo.Text;
        m_saDatos[2] = GetDouble(m_EPresupuesto.Text);
        m_saDatos[3] = m_EFecha.Text;

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Vigencia " + m_EVigencia.Text + " Editada");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM vigencias WHERE nombre='" + m_nId.Value+ "'") == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Vigencia " + m_EVigencia.Text + " Eliminada");
        }
    }
}
