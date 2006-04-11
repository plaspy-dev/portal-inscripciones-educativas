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
        CConector.SetCookie("formatop",tipo.ToString());

        if (tipo==0) // 0 excel, 1 en pantalla
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "inline;filename=" + Title + ".xls");
        }

        tipo = Request.QueryString["campos"] == null ? 0 : Convert.ToInt32(Request.QueryString["campos"]);
        bool[] hidden = (bool []) Session["hiddencols"];
        Session["hiddencolsprint"] = hidden;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CPageTemplate.ParseRow((GridView)sender,CTablaEx.GetTabla(),e,false);
    }
}
