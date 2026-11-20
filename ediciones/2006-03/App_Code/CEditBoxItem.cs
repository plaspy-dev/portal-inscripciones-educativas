using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CEditBoxItem
/// </summary>
public class CEditBoxItem : ITemplate
{
    protected bool m_bBoton=false;
    protected string m_szGrilla;
    protected bool m_bNumerico=false;

	public CEditBoxItem()
	{
	}

    public CEditBoxItem(bool numerico)
    {
        m_bNumerico = numerico;
    }

    public CEditBoxItem(string grilla)
    {
        m_szGrilla = grilla;
        m_bBoton = true;
    }

    void ITemplate.InstantiateIn(Control container)
    {
        TextBox box = new TextBox();
        box.DataBinding += new EventHandler(this.BindData);
        box.AutoPostBack = autoPostBack;
        if( m_bBoton)
            box.Attributes["readonly"] = "true";
        if (m_bNumerico)
        {
            box.CssClass = "inputnumerico";
            box.Attributes["onKeypress"] = "isnb();";
        }
        container.Controls.Add(box);
        if (m_bBoton)
        {
            Button boton = new Button();
            boton.UseSubmitBehavior = false;
            boton.Text = "...";
            container.Controls.Add(boton);
        }
    }

    private string dataField;
    public string DataField
    {
        get
        {
            return dataField;
        }
        set
        {
            dataField = value;
        }
    }
    private bool autoPostBack = false;
    public bool AutoPostBack
    {
        set
        {
            autoPostBack = value;
        }
        get
        {
            return autoPostBack;
        }
    }

    private void BindData(object sender, EventArgs e)
    {
        TextBox box = (TextBox)sender;
        DataGridItem container = (DataGridItem)box.NamingContainer;
        box.Text = ((DataRowView)container.DataItem)[dataField].ToString();
        if (m_bBoton)
        {
            Button boton=(Button) ((DataGridItem)container).Controls[3].Controls[1];
            boton.Attributes["onclick"] = "setFecha('" + m_szGrilla + "$ctl" + ((((DataGridItem)container).ItemIndex + 2) < 10 ? "0" : "")
            + (((DataGridItem)container).ItemIndex + 2) + "$ctl01');";
        }
        //if (m_szOnClick.Length != 0)
        //    box.Attributes.Add("onclick", m_szOnClick + "(this, '" + m_szGrilla + "_ctl" + ((((DataGridItem)container).ItemIndex + 2) < 10 ? "0" : "")
        //        + (((DataGridItem)container).ItemIndex + 2) + "_ctl0')")
    }
}
