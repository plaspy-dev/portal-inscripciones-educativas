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

public partial class aprobacion_convenios_inscripcion : CPageTemplate
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("conveniosm", 17))
            return;
        m_saCampos[0] = "id";
        m_saCampos[1] = "grupo";
        m_saCampos[2] = "eterritorial";
        m_saCampos[3] = "valor";
        m_saCampos[4] = "codigo";
        m_saCampos[5] = "tipo";
        m_saCampos[6] = "objeto";
        m_saCampos[7] = "fechaaprobacion";
        m_saCampos[8] = "vencimiento";
        m_saCampos[9] = "estado";
        m_saCampos[10] = "observaciones";
        m_saCampos[11] = "plazo";
        m_saCampos[13] = "cdp";
        m_saCampos[14] = "rdp";
        m_saCampos[15] = "fechardp";
        m_saCampos[16] = "fechacdp";

        m_saTitulos[0] = "id";
        m_saTitulos[1] = "Grupo";
        m_saTitulos[2] = "Entidad territorial";
        m_saTitulos[3] = "Valor convenio";
        m_saTitulos[4] = "Número convenio";
        m_saTitulos[5] = "Tipo";
        m_saTitulos[6] = "Objeto";
        m_saTitulos[7] = "Fecha aprobación";
        m_saTitulos[8] = "Fecha vencimiento";
        m_saTitulos[9] = "Estado";
        m_saTitulos[10] = "Observaciones";
        m_saTitulos[11] = "Plazo";
        m_saTitulos[12] = "NIT del ente territorial";
        m_saTitulos[13] = "Certificado de disponibilidad presupuestal";
        m_saTitulos[14] = "Registro Presupuestal";
        m_saTitulos[15] = "Fecha rdp";
        m_saTitulos[16] = "Fecha cdp";

        for (int i = 0; i < m_saCampos.Length; i++)
            m_saCamposAlias[i] = m_szTabla + "." + m_saCampos[i];

        m_saCamposAlias[1]="varios.nombre";
        m_saCamposAlias[2]="eterritoriales.nombre";
        m_saCamposAlias[5]="varios1.nombre";
        m_saCamposAlias[8] = "DATEADD(dd, conveniosm.plazo,conveniosm.fechaaprobacion)";
        m_saCamposAlias[9]="varios2.nombre";
        m_saCamposAlias[12] = "eterritoriales.nit";
        
        m_szIndice = m_saTitulos[0];
        m_szJoin = " LEFT JOIN eterritoriales ON (eterritoriales.codigo=conveniosm.eterritorial) "
            +" LEFT JOIN asignacionpresupuestal ON (asignacionpresupuestal.eterritorial=conveniosm.eterritorial) "
            +" LEFT JOIN varios ON (varios.id=asignacionpresupuestal.grupo) "
            +" LEFT JOIN varios AS varios1 ON (varios1.id=conveniosm.tipo) "
            +" LEFT JOIN varios AS varios2 ON (varios2.id=conveniosm.estado) "
            ;
        m_szCondicion = " (conveniosm.vigencia='" +m_pUsuario.GetVigenciaActual() + "') ";
        m_szCondicion += " AND " + m_pUsuario.GetCondicion("conveniosm.eterritorial");
        // autofiltro
        if (m_CBEnte.SelectedIndex > 0)
            m_szCondicionEx += " AND conveniosm.eterritorial=" + m_CBEnte.SelectedValue;
        if (m_CBTipo.SelectedIndex > 0)
            m_szCondicionEx += " AND conveniosm.tipo=" + m_CBTipo.SelectedValue;
        if (m_CBEstado.SelectedIndex > 0)
            m_szCondicionEx += " AND conveniosm.estado=" + m_CBEstado.SelectedValue;

        m_pUsuario.FillComboBox(m_CBEnte);
        FillComboBox(m_CBTipo, "SELECT id,nombre FROM varios WHERE tipo=31 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBEstado, "SELECT id,nombre FROM varios WHERE tipo=44 AND (nReservado IS NULL OR nReservado!=-1)");
        FillComboBox(m_CBTipoGasto, "SELECT id,nombre FROM varios WHERE tipo=46 AND (nReservado IS NULL OR nReservado!=-1)");

        m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion+ m_szCondicionEx);
        m_baHidden[0] = true;
        for( int i=11; i<m_baHidden.Length; i++)
            m_baHidden[i] = true;

        m_ENumero.Attributes["onKeypress"] = "isnb();";
        m_EPlazo.Attributes["onKeypress"] = "isnb();";

        m_EValor.Attributes["onKeypress"] = "isfloat(this);";
        m_EValor.Attributes["onblur"] = "tcomas(this);";
        m_EValor.Attributes["onfocus"] = "scomas(this);";

        if (m_LBDocumentos.Columns.Count==0)
        {
            CheckBoxColumn checkCol = new CheckBoxColumn("DGSelectOrUnselectAllTodos", "updateOtros", m_LBDocumentos.ID);
            checkCol.HeaderText = "";
            checkCol.DataField = "did";
            checkCol.SetCheckInHeader(0);
            m_LBDocumentos.Columns.Add(checkCol);

            BoundColumn data = new BoundColumn();
            data.DataField = data.HeaderText = "id";
            data.Visible = false;
            m_LBDocumentos.Columns.Add(data);

            data = new BoundColumn();
            data.DataField = "Documento";
            data.HeaderText = "Documento";
            m_LBDocumentos.Columns.Add(data);

            CEditBoxCol editcol = new CEditBoxCol(m_LBDocumentos.ID);
            editcol.DataField = "Fecha";
            editcol.HeaderText = "Fecha";
            m_LBDocumentos.Columns.Add(editcol);

            editcol = new CEditBoxCol();
            editcol.DataField = "Número";
            editcol.HeaderText = "Número";
            m_LBDocumentos.Columns.Add(editcol);

            editcol = new CEditBoxCol();
            editcol.DataField = "Observaciones";
            editcol.HeaderText = "Observaciones";
            m_LBDocumentos.Columns.Add(editcol);

            m_LBDocumentos.DataKeyField = "id";
            m_LBDocumentos.DataBind();
        }

        m_szMensaje = "No se encontraron proyectos";
        EndPageLoad();
    }

    override protected void UpdateAll()
    {

        m_STitulo.UpdateAfterCallBack = true;


        m_nId.UpdateAfterCallBack = true;
        m_CBEnte.UpdateAfterCallBack = true;
        m_ENumero.UpdateAfterCallBack = true;
        m_EValor.UpdateAfterCallBack = true;
        m_CBTipo.UpdateAfterCallBack = true;
        m_EPlazo.UpdateAfterCallBack = true;
        m_EFechaAprobacion.UpdateAfterCallBack = true;
        m_SFechaVencimiento.UpdateAfterCallBack = true;
        m_CBEstado.UpdateAfterCallBack = true;
        m_EObjeto.UpdateAfterCallBack = true;
        m_EObservaciones.UpdateAfterCallBack = true;
        // pestaña 2
        m_SNIT.UpdateAfterCallBack = true;
        m_CBTipoGasto.UpdateAfterCallBack = true;
        m_EFechaCDP.UpdateAfterCallBack = true;
        m_EFechaRDP.UpdateAfterCallBack = true;
        m_ERDP.UpdateAfterCallBack = true;
        m_ECDP.UpdateAfterCallBack = true;
        m_LBDocumentos.UpdateAfterCallBack = true;
    }

    override protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Anthem.GridView gv = sender as Anthem.GridView;

        DataRow row = GetDataRow(gv);
        if (row == null)
            return;
        
        
        m_nId.Value = gv.SelectedDataKey.Value.ToString();
        Session["idconvenio"] = m_nId.Value;
        m_LBDocumentos.DataBind();

        SelectInCombo(m_CBEnte, row[2].ToString());
        m_EValor.Text = GetFormatDouble(row[3].ToString());;
        m_ENumero.Text=row[4].ToString();
        SelectInCombo(m_CBTipo, row[5].ToString());
        m_EObjeto.Text = row[6].ToString();
        m_EFechaAprobacion.Text= GetFechaCorta(row[7].ToString());
        m_SFechaVencimiento.Text= GetFechaCorta(row[8].ToString());
        SelectInCombo(m_CBEstado, row[9].ToString());
        m_EObservaciones.Text= row[10].ToString();
        m_EPlazo.Text= row[11].ToString();
        m_SNIT.Text = row[12].ToString();
        m_ECDP.Text = row[13].ToString();
        m_ERDP.Text = row[14].ToString();
        m_EFechaRDP.Text= GetFechaCorta(row[15].ToString());
        m_EFechaCDP.Text = GetFechaCorta(row[16].ToString());

        base.m_LCResultados_SelectedIndexChanged(sender, e);
    }

    override protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        // limpiar los campos
        m_nId.Value = "";
        m_ENumero.Text="";
        m_EValor.Text="";
        m_EPlazo.Text="";
        m_EFechaAprobacion.Text="";
        m_SFechaVencimiento.Text="";
        m_EObjeto.Text = "";
        m_EObservaciones.Text="";
        m_CBEnte.SelectedIndex = 0;
        m_CBTipo.SelectedIndex = 0;
        m_CBEstado.SelectedIndex = 0;
        // pestaña 2
        m_SNIT.Text="";
        m_ERDP.Text = "";
        m_ECDP.Text = "";
        m_EFechaCDP.Text = "";
        m_EFechaRDP.Text = "";

        m_STitulo.Text = "Nuevo convenio";

        Session["idconvenio"] = "";
        m_LBDocumentos.DataBind();

        base.m_BCancelar_Click(sender, e);
    }

    protected void m_BInsertar_Click(object sender, EventArgs e)
    {
        // insertar
        m_saCampos[0] = "";
        m_saCampos[1] = "";

        m_saDatos[2]=m_CBEnte.SelectedValue;
        m_saDatos[3]=GetDouble(m_EValor.Text);
        m_saDatos[4]=m_ENumero.Text;
        m_STitulo.Text = "Convenio No. " + m_ENumero.Text;
        m_saDatos[5]=m_CBTipo.SelectedValue;
        m_saDatos[6]=m_EObjeto.Text;
        m_saDatos[7]=GetFechaCorta(m_EFechaAprobacion.Text);
        m_saCampos[8]="vigencia";
        m_saDatos[8] = m_pUsuario.GetVigenciaActual();
        m_saDatos[9]= GetSeleccion(m_CBEstado.SelectedValue);
        m_saDatos[10]=m_EObservaciones.Text;
        m_saDatos[11] = m_EPlazo.Text;
        m_saCampos[12] = "";
        m_saDatos[13] = m_ECDP.Text;
        m_saDatos[14] = m_ERDP.Text;
        m_saDatos[15] = GetFechaCorta(m_EFechaRDP.Text);
        m_saDatos[16] = GetFechaCorta(m_EFechaCDP.Text);

        int id = m_pConector.QueryMod(CConector.CrearInsercion(m_szTabla, m_saCampos, m_saDatos));

        if (id == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            m_nId.Value = id.ToString();
            DataTable tabla = CDocumentosConvenio.GetTabla();
            for (int i = 0; i < m_saCampos.Length; i++)
            {
                m_saCampos[i] = m_saDatos[i] = "";
            }
            // variosconveniom
            // documentosconveniom
            m_saCampos[0] = "convenio";
            m_saDatos[0] = m_nId.Value;
            m_saCampos[1] = "tipo";

            m_saDatos[1] = "";
            if (m_CBTipo.SelectedIndex > 0)
                m_saDatos[1]= m_CBTipo.SelectedValue;
            m_pConector.QueryMod(CConector.CrearInsercion("variosconveniom",m_saCampos, m_saDatos));

            m_saCampos[1] = "documento";
            m_saCampos[2] = "fecha";
            m_saCampos[3] = "numero";
            m_saCampos[4] = "observaciones";
            string query = "";
            bool check;
            for (int i = 0; i < m_LBDocumentos.Items.Count; i++)
            {
                query = "";
                check = ((CheckBox)m_LBDocumentos.Items[i].Cells[0].Controls[0]).Checked;
                m_saDatos[1] = m_LBDocumentos.Items[i].Cells[1].Text;
                m_saDatos[2] = GetDate(((TextBox)m_LBDocumentos.Items[i].Cells[3].Controls[0]).Text);
                m_saDatos[3] = GetString(((TextBox)m_LBDocumentos.Items[i].Cells[4].Controls[0]).Text);
                m_saDatos[4] = GetString(((TextBox)m_LBDocumentos.Items[i].Cells[5].Controls[0]).Text);

                DataRow row= tabla.Rows.Find(m_saDatos[1]);
                if (row != null && row[1].ToString() != "")
                {
                    if (check) // actualizar
                    {
                        query = CConector.CreateUpdate("documentosconveniom", m_saCampos, m_saDatos) + CConector.GetCondicion("id", row[1].ToString());
                    }
                    else
                    {
                        query = "DELETE FROM documentosconveniom WHERE id=" + row[1].ToString();
                    }
                }
                else if (check)
                {
                    query = CConector.CrearInsercion("documentosconveniom", m_saCampos, m_saDatos);
                }
                if (query.Length > 0 && m_pConector.QueryMod(query) == -1)
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                    return;
                }
            }
            MultiView1.ActiveViewIndex = 0;
            SetView(0);
            base.AfterUpdate("Convenio "  + m_ENumero.Text + " Ingresado");
        }
    }

    protected void m_BAceptar_Click(object sender, EventArgs e)
    {

        m_saDatos[2] = m_CBEnte.SelectedValue;
        m_saDatos[3] = GetDouble(m_EValor.Text);
        m_saDatos[4] = m_ENumero.Text;
        m_saDatos[5] = m_CBTipo.SelectedValue;
        m_saDatos[6] = m_EObjeto.Text;
        m_saDatos[7] = GetFechaCorta(m_EFechaAprobacion.Text);
        m_saCampos[8] = "";
        m_saDatos[9] = GetSeleccion( m_CBEstado.SelectedValue);
        m_saDatos[10] = m_EObservaciones.Text;
        m_saDatos[11] = m_EPlazo.Text;
        m_saCampos[12] = "";
        m_saDatos[13] = m_ECDP.Text;
        m_saDatos[14] = m_ERDP.Text;
        m_saDatos[15] = GetFechaCorta(m_EFechaRDP.Text);
        m_saDatos[16] = GetFechaCorta(m_EFechaCDP.Text);

        // editar
        string query = CConector.CreateUpdate(m_szTabla, m_saCampos, m_saDatos) + CConector.GetCondicion(m_saCampos[0], m_nId.Value);
        if (m_pConector.QueryMod(query) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            DataTable tabla = CDocumentosConvenio.GetTabla();
            for (int i = 0; i < m_saCampos.Length; i++)
            {
                m_saCampos[i] = m_saDatos[i] = "";
            }
            // documentosconveniom
            m_saCampos[0] = "convenio";
            m_saDatos[0] = m_nId.Value;

            m_saDatos[1] = "";
            if (m_CBTipo.SelectedIndex > 0)
                m_saDatos[1] = m_CBTipo.SelectedValue;
            m_pConector.QueryMod(CConector.CreateUpdate("variosconveniom", m_saCampos[1], m_saDatos[1])+ CConector.GetCondicion(m_saCampos[0], m_saDatos[0]));

            m_saCampos[1] = "documento";
            m_saCampos[2] = "fecha";
            m_saCampos[3] = "numero";
            m_saCampos[4] = "observaciones";
            bool check;
            for (int i = 0; i < m_LBDocumentos.Items.Count; i++)
            {
                query = "";
                check = ((CheckBox)m_LBDocumentos.Items[i].Cells[0].Controls[0]).Checked;
                m_saDatos[1] = m_LBDocumentos.Items[i].Cells[1].Text;
                m_saDatos[2] = GetDate(((TextBox)m_LBDocumentos.Items[i].Cells[3].Controls[0]).Text);
                m_saDatos[3] = GetString( ((TextBox)m_LBDocumentos.Items[i].Cells[4].Controls[0]).Text);
                m_saDatos[4] = GetString(((TextBox)m_LBDocumentos.Items[i].Cells[5].Controls[0]).Text);

                DataRow row = tabla.Rows.Find(m_saDatos[1]);
                if (row != null && row[1].ToString() != "")
                {
                    if (check) // actualizar
                    {
                        query = CConector.CreateUpdate("documentosconveniom", m_saCampos, m_saDatos) + CConector.GetCondicion("id", row[1].ToString());
                    }
                    else
                    {
                        query = "DELETE FROM documentosconveniom WHERE id=" + row[1].ToString();
                    }
                }
                else if (check)
                {
                    query = CConector.CrearInsercion("documentosconveniom", m_saCampos, m_saDatos);
                }
                if (query.Length > 0 && m_pConector.QueryMod(query) == -1)
                {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    m_SMensaje.UpdateAfterCallBack = true;
                    return;
                }
            }
            MultiView1.ActiveViewIndex = 0;
            SetView(0);
            base.AfterUpdate("Convenio " + m_ENumero.Text + " Editado");
        }
    }

    protected void m_BEliminar_Click(object sender, EventArgs e)
    {
        // eliminar
        if (m_pConector.QueryMod("DELETE FROM conveniosm WHERE id=" + m_nId.Value) == -1)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            m_SMensaje.UpdateAfterCallBack = true;
        }
        else
        {
            base.AfterUpdate("Convenio " + m_ENumero.Text + " Eliminado");
        }
    }
    protected void m_CBEnte_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_LCResultados.DataBind();
        m_LCResultados_PageIndexChanged(m_LCResultados, e);
        // m_LCResultados.Columns[].Visible = .SelectedIndex == 0;
        m_LCResultados.UpdateAfterCallBack = true;
    }
    protected void m_CBTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBEnte_SelectedIndexChanged(sender, e);
    }
    protected void m_CBEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_CBEnte_SelectedIndexChanged(sender, e);
    }
    protected void m_LBDocumentos_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            TextBox box = (TextBox )e.Item.Cells[3].Controls[0];
            box.Text = GetFechaCorta(box.Text);
        }
    }
    override protected void SetView(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                m_BFinalizarE.Style["display"] = m_BAceptar.Style["display"];
                m_BFinalizar.Style["display"] = m_BAceptar.Style["display"] == "" ? "none" : "";
                break;
        }
        base.SetView(index);
    }
}