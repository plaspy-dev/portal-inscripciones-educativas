using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class importar_archivos_Default : CPageTemplate
{
    protected byte[] contenido;
    protected int   m_nTamano;
    protected int   m_nPosicion;
    protected ArrayList m_pPalabras;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageLoad("", 1))
            return;
        EndPageLoad();
    }
    protected int Read() {

        string cadena = "";
        m_pPalabras.Clear();
        char texto;
        for (; m_nPosicion < m_nTamano; m_nPosicion++)
        {
            texto = (char)contenido[m_nPosicion];
            if(texto=='\t') {
                m_pPalabras.Add(cadena);
                cadena = "";
            } else if(texto=='\r' || texto=='\n') {
                m_pPalabras.Add(cadena);
                m_nPosicion++;
                if (texto == '\r')
                    m_nPosicion++;
                break;
            } else cadena += texto;
        }
        return m_pPalabras.Count;
    }
    protected bool InitFile(FileUpload fileupload) {
        if(!fileupload.HasFile) {
            m_SMensaje.Text = "Por favor seleccione un archivo.";
            return false;
        }
        m_nTamano = fileupload.PostedFile.ContentLength;
        if(m_nTamano>0) {
            m_nPosicion = 0;
            contenido = new byte[m_nTamano];
            fileupload.PostedFile.InputStream.Read(contenido, 0, m_nTamano);
            m_pPalabras = new ArrayList();
            return true;
        }
        return false;
    }
    protected void m_BDepartamentos_Click(object sender, EventArgs e)
    {
        if (!InitFile(m_FUDepartamentos))
            return;
        int nLinea = 0;
        int nuevos = 0;
        int actualizados = 0;
        CQuery query = new CQuery("departamentos");
        CQuery select= new CQuery("departamentos");
        select+="codigo";
        string consulta;

        while(true) {
            nLinea++;
            switch(Read()) {
                case 0:
                    m_SMensaje.Text = "Importación terminada " + nLinea.ToString() + " Lineas. " + actualizados.ToString() + " Registros actualizados. " + nuevos.ToString() + " Registros nuevos.";
                    return;
                case 2:
                    {
                        query["codigo"]= m_pPalabras[0].ToString();
                        query["nombre"] = m_pPalabras[1].ToString();
                        if(m_pConector.QueryString(select.GetSelect("codigo='" + query["codigo"] +"'")).Length==0) {// no existe o hay un error
                            if(m_pConector.GetLastError().Length!=0) {
                                m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                                return;
                            } else {
                                consulta = query.GetInsert();
                                nuevos++;
                            }
                        } else { // si existe
                            consulta = query.GetUpdate("codigo='" + query["codigo"] + "'");
                            actualizados++;
                        }
                        if(m_pConector.QueryMod(consulta)==-1) {
                            m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                            return;
                        }
                    }
                    break;
                default:
                    m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ".se necesitan 2 campos";
                    return;
            }
        }
    }
    protected void m_BETerritoriales_Click(object sender, EventArgs e)
    {
        if (!InitFile(m_FUETerritoriales))
            return;
        int nLinea = 0;
        int nuevos = 0;
        int actualizados = 0;
        CQuery query = new CQuery("eterritoriales");
        CQuery select = new CQuery("eterritoriales");
        select += "codigo";
        string consulta;

        while (true)
        {
            nLinea++;
            switch (Read())
            {
                case 0:
                    m_SMensaje.Text = "Importación terminada " + nLinea.ToString() + " Lineas. " + actualizados.ToString() + " Registros actualizados. " + nuevos.ToString() + " Registros nuevos.";
                    return;
                case 3:
                    {
                        query["codigo"] = m_pPalabras[0].ToString();
                        query["nombre"] = m_pPalabras[1].ToString();
                        if (m_pConector.QueryString(select.GetSelect("codigo='" + query["codigo"] + "'")).Length == 0)
                        {// no existe o hay un error
                            if (m_pConector.GetLastError().Length != 0)
                            {
                                m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                                return;
                            }
                            else
                            {
                                consulta = query.GetInsert();
                                nuevos++;
                            }
                        }
                        else
                        { // si existe
                            consulta = query.GetUpdate("codigo='" + query["codigo"] + "'");
                            actualizados++;
                        }
                        if (m_pConector.QueryMod(consulta) == -1)
                        {
                            m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                            return;
                        }
                    }
                    break;
                default:
                    m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ".se necesitan 3 campos";
                    return;
            }
        }
    }
    protected void m_BMunicipios_Click(object sender, EventArgs e)
    {
        if (!InitFile(m_FUMunicipios))
            return;
        int nLinea = 0;
        int nuevos = 0;
        int actualizados = 0;
        CQuery query = new CQuery("municipios");
        CQuery select = new CQuery("municipios");
        select += "id";
        string consulta;

        while (true)
        {
            nLinea++;
            switch (Read())
            {
                case 0:
                    m_SMensaje.Text = "Importación terminada " + nLinea.ToString() + " Lineas. " + actualizados.ToString() + " Registros actualizados. " + nuevos.ToString() + " Registros nuevos.";
                    return;
                case 3:
                    {
                        query["departamento"] = m_pPalabras[0].ToString();
                        query["codigo"] = m_pPalabras[1].ToString();
                        query["nombre"] = m_pPalabras[2].ToString();

                        if (m_pConector.QueryString(select.GetSelect("codigo='" + query["codigo"] + "' AND departamento='" + query["departamento"] + "'")).Length == 0)
                        {// no existe o hay un error
                            if (m_pConector.GetLastError().Length != 0)
                            {
                                m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                                return;
                            }
                            else
                            {
                                consulta = query.GetInsert();
                                nuevos++;
                            }
                        }
                        else
                        { // si existe
                            consulta = query.GetUpdate("codigo='" + query["codigo"] + "' AND departamento='" + query["departamento"] + "'");
                            actualizados++;
                        }
                        if (m_pConector.QueryMod(consulta) == -1)
                        {
                            m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                            return;
                        }
                    }
                    break;
                default:
                    m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ".se necesitan 3 campos";
                    return;
            }
        }
    }

    protected string UpdateMunicipio(string eterritorial, string departamento, string municipio) {

        CQuery query = new CQuery("municipios");
        CQuery select = new CQuery("municipios");
        select += "id";
        string resultado;

        query["eterritorial"] = eterritorial;

        resultado = m_pConector.QueryString(select.GetSelect("codigo='" + municipio + "' AND departamento='" + departamento + "'"));

        if (resultado.Length == 0)
        {// no existe y hay un error
            if (m_pConector.GetLastError().Length != 0)
            {
                m_SMensaje.Text = m_pConector.GetLastError();
            }
            else
            {
                m_SMensaje.Text = "Municipio no existente.";
            }
        }
        else
        { // si existe
            if (m_pConector.QueryMod(query.GetUpdate("id='" + resultado + "'")) == -1)
            {
                m_SMensaje.Text = m_pConector.GetLastError();
                return "";
            }
        }
        return resultado;
    }
    
    public static string GetCalendario(string tipo) {
        if (tipo.Length == 0)
            return tipo;
        switch(Convert.ToInt32(tipo)) {
            case 1:
                return "573";
            case 2:
                return "574";
            case 3:
                return "575";
        }
        return "";
    }
    public static string GetPoblacion(string tipo)
    {
        if (tipo.Length == 0)
            return tipo;
        switch (Convert.ToInt32(tipo))
        {
            case 1:
                return "169";
            case 2:
                return "170";
            case 3:
                return "171";
        }
        return "";
    }

    public static string GetSector(string tipo)
    {
        if (tipo.Length == 0)
            return tipo;
        switch (Convert.ToInt32(tipo))
        {
            case 1:
                return "146";
            case 2:
                return "147";
        }
        return "";
    }

    public static string GetZona(string tipo)
    {
        if (tipo.Length == 0)
            return tipo;
        switch (Convert.ToInt32(tipo))
        {
            case 1:
                return "177";
            case 2:
                return "176";
            case 3:
                return "178";
        }
        return "";
    }

    public bool UpdateMatriculas(string codigodane, string ano, string matricula) {

        CQuery query= new CQuery("institucionesvariacion");
        ano= ano.Replace(".", "");
        if (matricula.Length == 0)
            matricula = "0";

        SqlDataReader reader= m_pConector.Query("SELECT id FROM insteducativas WHERE codigo='" + codigodane + "'");
        if (reader == null)
        {
            m_SMensaje.Text = m_pConector.GetLastError();
            return false;
        }
        ArrayList list = new ArrayList();
        while(reader.Read()) { // enumeramos todas las instituciones con ese codigo dane.

            list.Add(reader[0].ToString());
        }
        query["anolectivo"] = ano;
        query["alumnos"] = matricula;

        for (int i = 0; i < list.Count; i++)
        {
            query["institucion"] = list[i].ToString();
            string id= m_pConector.QueryString("SELECT id FROM institucionesvariacion WHERE anolectivo='" + ano + "' AND institucion='" + query["institucion"] + "'");
            if(id.Length==0) {
                if(m_pConector.GetLastError().Length!=0) {
                    m_SMensaje.Text = m_pConector.GetLastError();
                    return false;
                }
                id = query.GetInsert();
            } else {
                id = query.GetUpdate("id='" + id + "'");
            }
            if(m_pConector.QueryMod(id)==-1) {
                m_SMensaje.Text = m_pConector.GetLastError();
                return false;
            }
        }
        return true;
    }

    protected void m_BInstituciones_Click(object sender, EventArgs e)
    {
        if (!InitFile(m_FUIEducativas))
            return;
        int nLinea = 0;
        int nuevos = 0;
        int actualizados = 0;
        CQuery query = new CQuery("insteducativas");
        CQuery select = new CQuery("insteducativas");
        select += "id";
        string consulta;

        while (true)
        {
            nLinea++;
            switch (Read())
            {
                case 0:
                    m_SMensaje.Text = "Importación terminada " + nLinea.ToString() + " Lineas. " + actualizados.ToString() + " Registros actualizados. " + nuevos.ToString() + " Registros nuevos.";
                    return;
                case 24:
                case 25:
                    {
                        if (m_pPalabras.Count == 24)
                            m_pPalabras.Add("");
                        // 0 es el año
                        // 24 es la matricula actual.
                        // actualizamos y buscamos el municipio.


                        // 1  es el ente territorial.
                        // 2 es el departamento.
                        // 3 es el municipio.
                        query["municipio"] = UpdateMunicipio(m_pPalabras[1].ToString(),m_pPalabras[2].ToString(),m_pPalabras[3].ToString());
                        if (query["municipio"].Length==0) { // no encontramos al municipio
                            m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_SMensaje.Text;
                            return;
                        }
                        query["codigo"] = m_pPalabras[5].ToString();
                        query["nombre"] = m_pPalabras[6].ToString();

                        // necesario hacer el cambio de estas tres.
                        query["calendario"] = GetCalendario(m_pPalabras[7].ToString());
                        query["tpoblacion"] = GetPoblacion(m_pPalabras[8].ToString());
                        query["sector"] = GetSector(m_pPalabras[9].ToString());                        

                        query["rector"] = m_pPalabras[11].ToString();
                        query["zona"] = GetZona(m_pPalabras[12].ToString());
                        query["barrioinspeccion"] = m_pPalabras[13].ToString();
                        query["direccion"] = m_pPalabras[14].ToString();
                        query["telefonos"] = m_pPalabras[15].ToString();
                        query["fax"] = m_pPalabras[16].ToString();
                        query["web"] = m_pPalabras[17].ToString();
                        query["email"] = m_pPalabras[18].ToString();

                        if (m_pConector.QueryString(select.GetSelect("codigo='" + query["codigo"] + "'")).Length == 0)
                        {// no existe o hay un error
                            if (m_pConector.GetLastError().Length != 0)
                            {
                                m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                                return;
                            }
                            else
                            {
                                consulta = query.GetInsert();
                                nuevos++;
                            }
                        }
                        else
                        { // si existe
                            consulta = query.GetUpdate("codigo='" + query["codigo"] + "'");
                            actualizados++;
                        }
                        if (m_pConector.QueryMod(consulta) == -1)
                        {
                            m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_pConector.GetLastError();
                            return;
                        }
                        if(!UpdateMatriculas(query["codigo"], m_pPalabras[0].ToString(), m_pPalabras[24].ToString())) {
                            m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ". " + m_SMensaje.Text;
                            return;
                        }
                    }
                    break;
                default:
                    m_SMensaje.Text = "Error en la linea " + nLinea.ToString() + ".se necesitan 25 campos";
                    return;
            }
        }
    }
}
