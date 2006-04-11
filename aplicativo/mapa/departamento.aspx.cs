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

public partial class aplicativo_mapa_departamento : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("departamentos", 5))
            return;

        m_saCampos[0] = "codigo";
        m_saCampos[1] = "nombre";
        m_saCampos[2] = "mapa";
        m_saCampos[3] = "color";
        m_saCampos[4] = "observaciones";

        m_saTitulos[0] = "Código División política";
        m_saTitulos[1] = "Nombre Departamento";
        m_saTitulos[2] = "Nombre en el Mapa";
        m_saTitulos[3] = "Color en el Mapa";
        m_saTitulos[4] = "Observaciones en el Mapa";

        m_szCondicion = " (color IS NULL OR color!=-1) ";

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCampos, m_saTitulos, " WHERE " + m_szCondicion);

        m_szIndice = m_saTitulos[0];
        m_ECodigo.Attributes["onKeypress"] = "isnb();";
        m_EColor.Attributes["onKeypress"] = "isnb();";

        m_szMensaje = "No se encontraron departamentos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {
        m_nId.UpdateAfterCallBack = true;
        m_ECodigo.UpdateAfterCallBack = true;
        m_ENombre.UpdateAfterCallBack = true;
        m_EMapa.UpdateAfterCallBack = true;
        m_EColor.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;

        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        m_ECodigo.Text = row[0].ToString();
        m_ENombre.Text = row[1].ToString();
        m_EMapa.Text = row[2].ToString();
        m_EColor.Text = row[3].ToString();
        m_EObservaciones.Text = row[4].ToString();

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ECodigo.Text = "";
        m_ENombre.Text = "";
        m_EMapa.Text = "";
        m_EColor.Text = "";
        m_EObservaciones.Text = "";

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        m_saDatos[0]=m_ECodigo.Text;
        m_saDatos[1]=m_ENombre.Text;
        m_saDatos[2]=m_EMapa.Text;
        m_saDatos[3]=m_EColor.Text;
        m_saDatos[4] = GetString(m_EObservaciones.Text);

        if (m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos)) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Departamento " + m_ENombre.Text + " Ingresado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {
        m_saDatos[0] = m_ECodigo.Text;
        m_saDatos[1] = m_ENombre.Text;
        m_saDatos[2] = m_EMapa.Text;
        m_saDatos[3] = m_EColor.Text;
        m_saDatos[4] = GetString(m_EObservaciones.Text);

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Departamento " + m_ENombre.Text + " Editado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("UPDATE departamentos SET color=-1 WHERE codigo=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Departamento " + m_ENombre.Text + " Eliminado");
        }
    }
}