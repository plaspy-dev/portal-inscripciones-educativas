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

public partial class print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = Request.QueryString["titulo"];
        
        int tipo= Request.QueryString["modo"]==null ? 0 : Convert.ToInt32(Request.QueryString["modo"]);
        Response.Cookies["formatop"].Value = tipo.ToString();
        Response.Cookies["formatop"].Expires = DateTime.Now.AddDays(30);

        if (tipo==0) // 0 excel, 1 en pantalla
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "inline;filename=" + Title + ".xls");
        }

        tipo = Request.QueryString["campos"] == null ? 0 : Convert.ToInt32(Request.QueryString["campos"]);
        bool[] hidden = (bool []) Session["hiddencols"];
        if (hidden != null)
        {
            int pos = 1;
            for (int i = 0; i < hidden.Length; i++)
            {
                if (!hidden[i])
                {
                    if ((pos&tipo)==0)
                    {
                        hidden[i] = true;
                    }
                    pos *= 2;
                }
            }
        }
        Session["hiddencolsprint"] = hidden;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            //DataTable tabla = CTablaEx.GetTabla();
            //for (int i = 1; i < tabla.Columns.Count; i++)
            //{
            //    if (tabla.Columns[i].DataType == typeof(double) || tabla.Columns[i].DataType == typeof(decimal))
            //    {
            //        e.Row.Cells[i + 1].Text = CPageTemplate.GetFormatDouble(e.Row.Cells[i + 1].Text);
            //        e.Row.Cells[i + 1].CssClass = "inputnumerico";
            //    }
            //    else if (tabla.Columns[i].DataType == typeof(int))
            //    {
            //        e.Row.Cells[i + 1].CssClass = "inputnumerico";
            //    }
            //    else if (tabla.Columns[i].DataType == typeof(DateTime))
            //    {
            //        e.Row.Cells[i + 1].Text = CPageTemplate.GetFechaCorta(e.Row.Cells[i + 1].Text);
            //        e.Row.Cells[i + 1].CssClass = "inputnumerico";
            //    }
            //}
        }
    }
}
