using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;

/// <summary>
/// Summary description for CPageTemplate
/// </summary>
public class CPageTemplate : System.Web.UI.Page
{
    protected CMenu m_pMenu;
    protected CConector m_pConector;
    protected CUsuarioDatos m_pUsuario;
    protected string m_szTitulo;

    protected string m_szTabla;
    protected string m_szQuery;
    protected string m_szIndice;
    protected string[] m_saCampos;
    protected string[] m_saDatos;
    protected string[] m_saTitulos;
    protected string[] m_saCamposAlias;
    protected string m_szJoin;
    protected bool[] m_baHidden;
    protected string m_szCondicion;
    protected string m_szCondicionEx;
    protected string m_szTipoIndice;
    protected int m_nPermisos;
    protected string m_szMensaje;

    public CPageTemplate()
    {
        m_pMenu = null;
        m_pConector = null;
        m_szTitulo = "";
        m_szTipoIndice = "INT";
        m_nPermisos = 0;
        m_szCondicionEx = "";
        m_szMensaje = "No se encontraron resultados";
    }

    public bool PageLoad(string tabla, int campos)
    {
        return PageLoad(tabla, campos, true);
    }

    public bool PageLoad(string tabla, int campos, bool verificar)
    {
        Response.AddHeader("pragma", " no-cache");
        Response.CacheControl = "Private";
        Response.Expires = 0;

        if (!CConector.IsLogged(this))
        {
            Response.Redirect(HttpContext.Current.Request.ApplicationPath + "/default.aspx");
            Response.End();
            return false;
        }
        m_pMenu = (CMenu)Session["menu"];
        if (m_pMenu == null)
        {
            m_pMenu = new CMenu(this);
            Session.Add("menu", m_pMenu);
        }
        m_nPermisos = m_pMenu.GetPermisos(Request.Url.AbsolutePath);
        if (verificar && m_nPermisos < 1)
        {
            Response.Redirect(HttpContext.Current.Request.ApplicationPath + "/Entrada.aspx");
            Response.End();
            return false;
        }

        m_szTitulo = m_pMenu.GetFullPath(Request.Url.AbsolutePath);
        m_pUsuario = (CUsuarioDatos)Session["usuario"];
        m_pConector = CConector.GetConector();

        m_szJoin = "";
        m_szCondicion = "";
        m_szTabla = tabla;
        m_szQuery = "SELECT * FROM " + m_szTabla;

        m_saCampos = new string[campos];
        m_saTitulos = new string[campos];
        m_saCamposAlias = new string[campos];
        m_baHidden = new bool[campos];
        m_saDatos= new string[campos];

        for (int i = 0; i < campos; i++)
        {
            m_saCampos[i] = "";
            m_saTitulos[i] = "";
            m_saCamposAlias[i] = "";
            m_saDatos[i] = "";
            m_baHidden[i] = false;
        }
        DropDownList combo = (DropDownList)FindControl("m_CBVigenciasCombo");
        if( combo!=null)
            m_pUsuario.GetVigencias(combo);
        if (!IsPostBack)
        {
            Label lab = (Label)FindControl("m_STituloLargo");
            if (lab != null)
                lab.Text = m_szTitulo;
            if ((lab = (Label)FindControl("m_SUsuarioActual")) != null)
                lab.Text = m_pUsuario.GetNombre();
            if ((lab = (Label)FindControl("m_SFechaActual")) != null)
                lab.Text = GetFechaHora(DateTime.Now);
        }
        return true;
    }

    public void SetPermisos(int permisos)
    {
        m_nPermisos = permisos;
    }

    virtual public void CleanQuery()
    {
        if(m_szCondicion.Length>0)
            m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin + " WHERE " + m_szCondicion);
        else m_szQuery = CConector.CrearSelectT(m_szTabla, m_saCamposAlias, m_saTitulos, m_szJoin);
        Session["query"] = m_szQuery;
        Anthem.GridView grid = (Anthem.GridView)FindControl("m_LCResultados");
        if (grid != null)
        {
            grid.DataBind();
            grid.UpdateAfterCallBack = true;
        }
    }

    public virtual void EndPageLoad()
    {

        Session["query"] = m_szQuery;
        Session["indice"] = m_szIndice;
        Session["hiddencols"] = m_baHidden;

        if (!IsPostBack)
        {
            ImageButton boton;
            if ((boton = (ImageButton)FindControl("m_BCancelarP")) != null)
            {
                boton.Style["display"] = "none";
            }
            if ((boton = (ImageButton)FindControl("m_BAceptar")) != null)
            {
                boton.Style["display"] = "none";
            }
            if ((boton = (ImageButton)FindControl("m_BAceptarP")) != null)
            {
                boton.Style["display"] = "none";
            }
            if ((boton = (ImageButton)FindControl("m_BEliminar")) != null)
            {
                boton.Style["display"] = "none";
            }
            if ((m_nPermisos & 4) == 0 && (boton = (ImageButton)FindControl("m_BInsertar")) != null)
            {
                boton.Style["display"]="none";
            }
            Anthem.LinkButton lboton = (Anthem.LinkButton)FindControl("m_LBTodos");
            if (lboton != null)
                lboton.Style["display"] = "none";
            if ((lboton = (Anthem.LinkButton)FindControl("m_LBNinguno")) != null)
                lboton.Style["display"] = "none";
            Anthem.DropDownList lista;
            if ((lista = (Anthem.DropDownList)FindControl("m_CBImprimir")) != null)
            {
                lista.Style["display"] = "none";
                lista.SelectedIndex = Convert.ToInt32(CConector.GetCookie("formatop", "0"));
            }
            Label label = (Label)FindControl("m_SVersion");
            if (label != null)
            {
                label.Text = CConector.GetCurrentVersion();
            }
        }
        Anthem.GridView grid = (Anthem.GridView)FindControl("m_LCResultados");
        if (grid != null)
        {
            if (grid.Columns.Count == 1)
            {
                grid.Columns[0].ItemStyle.Width= new Unit(16, UnitType.Pixel);
                for (int i = 0; i < m_saTitulos.Length; i++)
                {
                    BoundField data = new BoundField();
                    data.HeaderText = m_saTitulos[i];
                    data.Visible = !m_baHidden[i];
                    data.DataField = data.SortExpression = m_saTitulos[i];
                    grid.Columns.Add(data);
                }
                grid.DataKeyNames = new string[] { m_szIndice }; 
            }
            grid.EmptyDataText = m_szMensaje;
            grid.PageSize = CConector.GetCookie("pager", 20);
        }
    }

    public string GetTituloCorto()
    {
        string titulo = m_szTitulo;
        int ultimo = m_szTitulo.LastIndexOf('/');
        if (ultimo != -1)
        {
            titulo = m_szTitulo.Substring(ultimo + 1);
        }
        return titulo;
    }

    public virtual void GetTitulo()
    {
        Response.Write(m_szTitulo);
    }

    public virtual void GetMenu()
    {
        Response.Write(m_pMenu.GetMenu());
    }

    public virtual void EndDocument()
    {
    }

    public virtual void GetVigencia()
    {
        Response.Write(m_pUsuario.GetVigenciaActual());
    }

    public static string GetUnformatMoney(string cadena, string inicio)
    {
        cadena = cadena.Replace(".", "");
        return inicio + cadena.Replace(",", ".");
    }

    public static string GetUnformatMoney(string cadena)
    {
        return GetUnformatMoney(cadena, "%");
    }

    virtual protected void m_BAceptarP_Click(object sender, EventArgs e)
    {
        //imprimir
        Anthem.Manager.AddScriptForClientSideEval("printForm('" + GetTituloCorto() + "');");
    }

    virtual protected void ShowPrint(bool show)
    {
        int check = 1;
        Anthem.CheckBox checkbox;

        while ((checkbox = (Anthem.CheckBox)FindControl("CheckBox" + check)) != null)
        {
            checkbox.Visible = show;
            checkbox.UpdateAfterCallBack = true;
            check++;
        }
        Anthem.GridView grid = (Anthem.GridView)FindControl("m_LCResultados");
        if (grid != null)
        {
            grid.Columns[0].Visible = !show;
            grid.UpdateAfterCallBack = true;
        }

        Anthem.ImageButton boton = (Anthem.ImageButton)FindControl("m_BImprimir");
        if (boton != null)
        {
            boton.Style["display"] = show ? "none" :"";
            boton.UpdateAfterCallBack = true;
        }
        if ((boton = (Anthem.ImageButton)FindControl("m_BCancelar"))!=null)
        {
            boton.Style["display"] = show ? "none" : "";
            boton.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 4) == 4 && (boton = (Anthem.ImageButton)FindControl("m_BInsertar")) != null)
        {
            boton.Style["display"] = show ? "none" : "";
            boton.UpdateAfterCallBack = true;
        }
        if ((boton = (Anthem.ImageButton)FindControl("m_BAceptarP")) != null)
        {
            boton.Style["display"] = show ? "" : "none";
            boton.UpdateAfterCallBack = true;
        }
        if ((boton = (Anthem.ImageButton)FindControl("m_BCancelarP")) != null)
        {
            boton.Style["display"] = show ? "" : "none";
            boton.UpdateAfterCallBack = true;
        }
        Anthem.LinkButton lboton = (Anthem.LinkButton)FindControl("m_LBTodos");
        if (lboton!=null)
        {
            lboton.Style["display"] = show ? "" : "none";
            lboton.UpdateAfterCallBack = true;
        }
        if ((lboton = (Anthem.LinkButton)FindControl("m_LBNinguno")) != null)
        {
            lboton.Style["display"] = show ? "" : "none";
            lboton.UpdateAfterCallBack = true;
        }
        Anthem.Label label = (Anthem.Label)FindControl("m_SMensaje");
        if (label != null)
        {
            label.Text = show ? "Formato" : "";
            label.UpdateAfterCallBack = true;
        }
        Anthem.DropDownList lista;
        if ((lista = (Anthem.DropDownList)FindControl("m_CBImprimir")) != null)
        {
            lista.Style["display"] = show ? "" : "none";
            lista.UpdateAfterCallBack = true;
        }
    }

    virtual protected void m_BImprimir_Click(object sender, EventArgs e)
    {
        ShowControls(false);
        UpdateAll();
        ShowPrint(true);
    }

    virtual protected void m_BCancelarP_Click(object sender, EventArgs e)
    {
        ShowControls(true);
        UpdateAll();
        ShowPrint(false);
    }

    virtual protected void m_LCResultados_PageIndexChanged(object sender, EventArgs e)
    {
        int desde, hasta;
        int total = CTablaEx.GetCount();
        Anthem.Label label = (Anthem.Label) FindControl("m_SResumen");
        Anthem.GridView grid = (Anthem.GridView)sender;
        if (grid == null)
            return;
        if (total == 0)
        {
            if (label != null)
            {
                label.Text = "";
                label.UpdateAfterCallBack = true;
            }
            return;
        }

        desde = grid.PageSize * grid.PageIndex + 1;
        hasta = desde + grid.PageSize-1;
        if (hasta > total)
            hasta = total;

        if (label!=null)
        {
            label.Text = "Resultados del " + desde + " al " + hasta + ", de " + total + " Resultados";
            label.UpdateAfterCallBack = true;
        }
        grid.SelectedIndex = -1;
    }

    virtual public void GetResumen()
    {
        m_LCResultados_PageIndexChanged(FindControl("m_LCResultados"), null);
    }

    static public DataRow GetDataRow(Anthem.GridView grid, DataTable tabla)
    {
        if (tabla == null)
            return null;
        return tabla.Rows.Find(grid.SelectedDataKey.Value);
    }

    static public DataRow GetDataRow(Anthem.GridView grid)
    {
        return GetDataRow(grid, CTablaEx.GetTabla());
    }

    protected void ShowEdicion(bool show,string mensaje)
    {

        Anthem.ImageButton boton;
        if ((boton = (Anthem.ImageButton)FindControl("m_BAceptar")) != null)
        {
            boton.Style["display"] = show ? "" : "none";
            boton.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 16) == 16 && (boton = (Anthem.ImageButton)FindControl("m_BEliminar")) != null)
        {
            boton.Style["display"] = show ? "" : "none";
            boton.UpdateAfterCallBack = true;
        }
        if ((boton = (Anthem.ImageButton)FindControl("m_BImprimir")) != null)
        {
            boton.Style["display"] = show ? "none" : "";
            boton.UpdateAfterCallBack = true;
        }
        if ((m_nPermisos & 4) == 4 && (boton = (Anthem.ImageButton)FindControl("m_BInsertar")) != null)
        {
            boton.Style["display"] = show ? "none" : "";
            boton.UpdateAfterCallBack = true;
        }
        Anthem.Label label = (Anthem.Label)FindControl("m_SMensaje");
        if (label != null)
        {
            label.Text = mensaje;
            label.UpdateAfterCallBack = true;
        }
        if (!show) {
            Anthem.GridView grid = (Anthem.GridView)FindControl("m_LCResultados");
            if (grid != null)
            {
                grid.SelectedIndex = -1;
                grid.UpdateAfterCallBack = true;
            }
        }
    }

    virtual protected void m_LCResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateAll();
        ShowEdicion(true, "Edición");
    }

    virtual protected void m_BCancelar_Click(object sender, EventArgs e)
    {
        UpdateAll();
        ShowEdicion(false, "");
        CleanQuery();
    }

    virtual protected void UpdateAll()
    {
    }

    virtual protected void ShowControls(bool show)
    {
    }

    virtual protected void AfterUpdate(string texto)
    {
        CTablaEx.RebuilTable();
        m_BCancelar_Click(null, null);
        Anthem.Label label = (Anthem.Label)FindControl("m_SMensaje");
        if (label != null)
            label.Text = texto;
        Anthem.GridView grid = (Anthem.GridView)FindControl("m_LCResultados");
        if(grid!=null) {
            grid.DataBind();
            grid.UpdateAfterCallBack = true;
        }
    }
    virtual protected void SetView(int index)
    {
        Button boton;
        int i=1;
        while ((boton =(Button) FindControl("m_BTab" +i.ToString())) != null)
        {
            boton.CssClass = (i - 1) == index ? "cselected" : "cinftab";
            i++;
        }
    }

    virtual protected void OnNext(object sender, EventArgs e)
    {
        MultiView view = (MultiView)FindControl("MultiView1");
        if (view != null)
        {
            view.ActiveViewIndex = view.ActiveViewIndex + 1;
            SetView(view.ActiveViewIndex);
        }
    }

    virtual protected void OnPrev(object sender, EventArgs e)
    {
        MultiView view = (MultiView)FindControl("MultiView1");
        if (view != null)
        {
            view.ActiveViewIndex = view.ActiveViewIndex - 1;
            SetView(view.ActiveViewIndex);
        }
    }

    virtual protected void OnTabsClick(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        MultiView view = (MultiView)FindControl("MultiView1");
        int vista=0;
        if (boton!=null && view!=null)
        {
            vista= Convert.ToInt32(boton.ID[boton.ID.Length-1].ToString());
            vista--;
            view.ActiveViewIndex = vista;
            SetView(vista);
        }
    }
    static protected string GetDouble(string valor)
    {
        if (valor.Length == 0)
            return "";
        valor = valor.Replace(".", "");
        valor= valor.Replace(",", ".");
        return "%" + valor;
    }
    static protected string GetString(string valor)
    {
        if (valor.Length == 0)
            return "%''";
        else return valor;
    }
    static protected string GetSeleccion(string valor)
    {
        if (valor == "-1")
            return "%NULL";
        else return valor;
    }
    static protected string GetDate(string valor)
    {
        if (valor == "")
            return "%NULL";
        else return valor;
    }
    static public string GetFechaCorta(string valor)
    {
        if (valor.Length > 10)
            return valor.Substring(0, 10);
        return valor;
    }

    public void FillComboBox(Anthem.DropDownList lista, string query)
    {
        FillComboBox(lista, query, false);
    }
    static public void SelectInCombo(Anthem.DropDownList lista, string value)
    {
        ListItem item = lista.Items.FindByText(value);
        lista.SelectedIndex = item == null ? 0 : lista.Items.IndexOf(item);
    }
    static public void SelectInComboValue(Anthem.DropDownList lista, string value)
    {
        ListItem item = lista.Items.FindByValue(value);
        lista.SelectedIndex = item == null ? 0 : lista.Items.IndexOf(item);
    }
    public void FillComboBox(Anthem.DropDownList lista, string query, bool force)
    {
        // int selectedindex = lista.SelectedIndex;
        if (lista.Items.Count > 0)
            if (force)
            {
                if (Session[lista.ID] != null && Session[lista.ID].ToString() == query)
                   return;
                lista.Items.Clear();
            } else return;
        lista.Items.Add(new ListItem("(Ninguno)", "-1"));
        if (query == null || query.Length == 0)
            return;
        SqlDataReader reader = m_pConector.Query(query);
        if (reader != null)
        {
            while (reader.Read())
            {
                lista.Items.Add(new ListItem(reader[1].ToString(), reader[0].ToString()));
            }
        }
        if (force)
        {
            Session[lista.ID]=query;
        }
        // lista.SelectedIndex = selectedindex;
    }
    static public string GetFormatDouble(string valor)
    {
        return GetFormatDouble(valor, 2, true);
    }

    static protected string GetFormatDouble(string valor, int decimales, bool showceros)
    {
        if (valor.Length == 0 || valor=="&nbsp;")
            return valor;
        string texto = valor;

        int pos = texto.IndexOf(',');
        string dec = "";
        if (pos != -1)
        {
            dec = texto.Substring(pos + 1);
            texto = texto.Substring(0, pos);
            if (!showceros)
            {
                while (dec.Length>0 && dec[dec.Length - 1] == '0')
                    dec = dec.Remove(dec.Length - 1, 1);
            }
        }
        ArrayList textof = new ArrayList();
        int i = 0;
        for (i = texto.Length - 3; i >= 0; i -= 3)
        {
            textof.Add(texto.Substring(i, 3));
        }
        if ((i = texto.Length % 3) != 0)
        {
            textof.Add(texto.Substring(0, i));
        }
        texto = "";
        for (i = textof.Count - 1; i >= 0; i--)
        {
            texto += textof[i];
            if (i != 0)
                texto += ".";
        }
        if (dec.Length > 1)
        {
            if (dec.Length > decimales)
                dec = dec.Substring(0, decimales);
            texto += "," + dec;
        }
        return texto;
    }

    static public void ParseRow(GridView grid, DataTable tabla, GridViewRowEventArgs e)
    {
        ParseRow(grid, tabla, e, true);
    }

    static public void ParseRow(GridView grid, DataTable tabla, GridViewRowEventArgs e, bool trim)
    {
        if (e.Row.DataItemIndex != -1)
        {
            int realindex = grid.PageIndex * grid.PageSize;
            realindex += e.Row.DataItemIndex;

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].GetType() == typeof(BoundField))
                {
                    Type tipodato = tabla.Rows[realindex][((BoundField)grid.Columns[i]).DataField].GetType();
                    if (tipodato == typeof(double) || tipodato == typeof(decimal) || tipodato == typeof(float))
                    {
                        e.Row.Cells[i].Text = GetFormatDouble(e.Row.Cells[i].Text);
                        e.Row.Cells[i].CssClass = "inputnumerico";
                    }
                    else if (tipodato == typeof(int) || tipodato == typeof(Int32))
                    {
                        e.Row.Cells[i].CssClass = "inputnumerico";
                    }
                    else if (tipodato == typeof(DateTime))
                    {
                        if (e.Row.Cells[i].Text.Length > 10)
                            e.Row.Cells[i].Text = GetFecha(Convert.ToDateTime(e.Row.Cells[i].Text));
                        e.Row.Cells[i].CssClass = "inputnumerico";
                    }
                    e.Row.Cells[i].ToolTip = HttpUtility.HtmlDecode(grid.Columns[i].HeaderText) + " : " + HttpUtility.HtmlDecode(e.Row.Cells[i].Text);
                    if (trim)
                    {
                        string texto = HttpUtility.HtmlDecode(e.Row.Cells[i].Text);
                        int max = CConector.GetCookie("maxcelda", 25);
                        if (max > 3 && texto.Length > max)
                        {
                            texto = texto.Substring(0, max - 3) + "...";
                            e.Row.Cells[i].Text = HttpUtility.HtmlEncode(texto);
                        }
                    }
                }
            }
        }
    }

    virtual protected void m_LCResultados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ParseRow((GridView)sender, CTablaEx.GetTabla(), e);
    }

    virtual protected void m_CBVigenciasCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CUsuarioDatos usuario = (CUsuarioDatos)Session["usuario"];
        if (usuario.SetVigencia(((DropDownList)sender).SelectedValue))
        {
            Response.Redirect(Request.Url.AbsolutePath);
            Response.End();
        }
    }
    virtual protected void timer_Tick(object sender, EventArgs e)
    {
        Anthem.Label lab;
        if ((lab = (Anthem.Label)FindControl("m_SFechaActual")) != null)
        {
            lab.Text = GetFechaHora(DateTime.Now);
            lab.UpdateAfterCallBack = true;
        }
    }

    static public string GetFecha(DateTime tiempo)
    {
        return CDateFormat.GetDate(tiempo);
    }

    static public string GetFechaHora(DateTime tiempo)
    {
        return CDateFormat.GetDate(tiempo)+ ", " + tiempo.ToShortTimeString();
    }
}

