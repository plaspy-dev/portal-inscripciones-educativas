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
/// Summary description for CEditBoxCol
/// </summary>
public class CEditBoxCol : System.Web.UI.WebControls.TemplateColumn
{
    private CEditBoxItem viewItem;
    private CEditBoxItem editItem;

	public CEditBoxCol()
	{
        viewItem = new CEditBoxItem();
        editItem = new CEditBoxItem();

        this.ItemTemplate = viewItem as ITemplate;
        this.EditItemTemplate = editItem as ITemplate;
	}

    public CEditBoxCol(bool numerico)
    {
        viewItem = new CEditBoxItem(numerico);
        editItem = new CEditBoxItem(numerico);

        this.ItemTemplate = viewItem as ITemplate;
        this.EditItemTemplate = editItem as ITemplate;
    }

    public CEditBoxCol(string grilla)
    {
        viewItem = new CEditBoxItem(grilla);
        editItem = new CEditBoxItem(grilla);

        this.ItemTemplate = viewItem as ITemplate;
        this.EditItemTemplate = editItem as ITemplate;
    }

    public bool AutoPostBack
    {
        set
        {
            viewItem.AutoPostBack = value;
            editItem.AutoPostBack = value;
        }
        get
        {
            return viewItem.AutoPostBack;
        }
    }
    public string DataField
    {
        get
        {
            return viewItem.DataField;
        }
        set
        {
            viewItem.DataField = value;
            editItem.DataField = value;
        }
    }

}
