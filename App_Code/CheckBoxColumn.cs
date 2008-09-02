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
/// Summary description for CheckBoxColumn
/// </summary>
public class CheckBoxColumn : System.Web.UI.WebControls.TemplateColumn
{
	protected string m_szFuncion;

		public CheckBoxColumn(string funcion, string onclick, string grilla)
		{
			// set the view one as readonly
            m_szFuncion = funcion;
            viewItem = new CheckBoxItem(true); // SAW was false
            viewItem.SetOnClick(onclick, grilla);
            m_szGrid = grilla;
			this.ItemTemplate = viewItem as ITemplate;

			// let the edit check box be editable
			editItem = new CheckBoxItem(true);
			this.EditItemTemplate = editItem as ITemplate;
		}

		/// <summary>
		/// Initialise our CheckBoxColumn with an optional ImmediatePostback capability.
		/// </summary>
		/// <param name="ImmediatePostback">If true then each change of state of the CheckBox item 
		/// will cause an event to be fired immediately on the server.</param>
		public CheckBoxColumn(bool ImmediatePostback)
		{
			// set the view one as readonly
			viewItem = new CheckBoxItem(ImmediatePostback);
			this.ItemTemplate = viewItem as ITemplate;

			// let the edit check box be editable
			editItem = new CheckBoxItem(true);
			this.EditItemTemplate = editItem as ITemplate;

			AutoPostBack = ImmediatePostback;
		}

        public override void InitializeCell(TableCell cell,int columnIndex, ListItemType itemType)
        {
            //let the base class initialize the cell
            base.InitializeCell(cell, columnIndex, itemType);

            if (m_bCheckInHeader && itemType == ListItemType.Header)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "_ctl" + m_nControl;
                checkbox.Text = cell.Text;
                checkbox.Attributes.Add("onclick", m_szFuncion+ "('" + m_szGrid + "',this,'_ctl0" + m_nControl + "')");
                cell.Controls.Add(checkbox);
            }
        }

		/// <summary>
		/// Occurs when the value of the Checked property changes between posts to the server. 
		/// </summary>
		/// <remarks>
		/// The <b>CheckedChanged</b> event is raised when the value of the Checked property changes between posts to the server.
		///
		/// <b>Note</b>   This event does not post the page back to the server unless the AutoPostBack property is set to true.
		/// <b>Note</b>   The control must have viewstate enabled for the <b>CheckedChanged</b> event to work correctly.
		/// </remarks>
		public event EventHandler CheckedChanged
		{
			add
			{
				viewItem.CheckedChanged += value;
				editItem.CheckedChanged += value;
			}
			remove
			{
				viewItem.CheckedChanged -= value;
				editItem.CheckedChanged -= value;
			}
		}

		/// <summary>
		/// If true then then each click on a CheckBox will cause an event to be fired on the server.
		/// </summary>
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
        protected bool m_bCheckInHeader = false;
        protected string m_szGrid = "";
        protected int m_nControl=0;

        public void SetCheckInHeader(int control)
        {
            m_bCheckInHeader = true;
            m_nControl = control;
        }

		/// <summary>
		/// The DataField that we wish our control to bind to.
		/// </summary>
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

		/// <summary>
		/// Internal storage of the CheckBoxItem that is to be used for the view state.
		/// </summary>
		private CheckBoxItem viewItem;

		/// <summary>
		/// Internal storage of the CheckBoxItem that is to be used for the edit state.
		/// </summary>
		private CheckBoxItem editItem;
}
