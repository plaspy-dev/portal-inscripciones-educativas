using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

public partial class datosdep : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("pragma", " no-cache");
        Response.CacheControl = "Private";
        Response.Expires = 0;

        CMapa mapa = CMapa.GetMapa();
        if (mapa == null)
            return;
        if (Session["departamentomapa"] == null)
            return;
        int tipo = 0;
        if (Session["tipomapa"].ToString() == "convenios")
            tipo = 1;
        else if (Session["tipomapa"].ToString() == "contratos")
            tipo = 2;
        else if (Session["tipomapa"].ToString() == "nuevo")
            tipo = 3;

        Response.ContentType = "text/xml";
        decimal maximo = 0;
        decimal valorp = 0;
        int totalp = 0;

        XmlWriter xw = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
        xw.WriteStartDocument(true);

        xw.WriteStartElement("departamentos");

        CMapaDepartamentos dep = mapa.GetDepartamento(Session["departamentomapa"].ToString());
        if (dep == null)
        {
            ArrayList departamentos=mapa.GetDepartamentos();
            for (int j = 0; j < departamentos.Count; j++)
            {
                dep= (CMapaDepartamentos)departamentos[j];
                totalp += dep.GetTotalProyectos();
                valorp += dep.GetValorProyectos();
            }

            xw.WriteStartElement("departamento");
            xw.WriteAttributeString("name", "colombia");
            xw.WriteAttributeString("nombre", "Colombia");

            xw.WriteStartElement("color");
            xw.WriteString("");
            xw.WriteEndElement();

            string comentarios = "";
            if (totalp > 0)
                comentarios += "$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + " en " + totalp.ToString() + " Proyectos\r";

            if (comentarios.Length == 0)
            {
                switch (tipo)
                {
                    case 1:
                        comentarios = "No hay Informacion de ejecucion";
                        break;
                    case 2:
                        comentarios = "No hay Informacion de estudiantes";
                        break;
                    case 3:
                        comentarios = "No hay Informacion de aulas";
                        break;
                    default:
                        comentarios = "No hay Informacion de valor";
                        break;
                }
            }
            xw.WriteStartElement("comentarios");
            xw.WriteString("Colombia\r" + comentarios);
            xw.WriteEndElement();

            decimal maxtemp;
            xw.WriteStartElement("estadisticas");
            switch (tipo)
            {
                case 1:
                    for (int j = 0; j < departamentos.Count; j++)
                    {
                        dep = (CMapaDepartamentos)departamentos[j];
                        maxtemp = dep.GetMaximoAvance();
                        if (maximo < maxtemp)
                            maximo = maxtemp;
                    }
                    xw.WriteAttributeString("columnas", "Ejecucion " + Convert.ToInt32(maximo).ToString() + "%");
                    break;
                case 2:
                    for (int j = 0; j < departamentos.Count; j++)
                    {
                        dep = (CMapaDepartamentos)departamentos[j];
                        maxtemp = dep.GetMaxEstudiantesB();
                        if (maximo < maxtemp)
                            maximo = maxtemp;
                    }
                    xw.WriteAttributeString("columnas", "Estudiantes Beneficiados " + maximo.ToString());
                    break;
                case 3:
                    for (int j = 0; j < departamentos.Count; j++)
                    {
                        dep = (CMapaDepartamentos)departamentos[j];
                        maxtemp = dep.GetMaxAulas();
                        if (maximo < maxtemp)
                            maximo = maxtemp;
                    }
                    xw.WriteAttributeString("columnas", "Aulas nuevas " + maximo.ToString());
                    break;
                default:
                    for (int j = 0; j < departamentos.Count; j++)
                    {
                        dep = (CMapaDepartamentos)departamentos[j];
                        maxtemp = dep.GetMaximoValorP();
                        if (maximo < maxtemp)
                            maximo = maxtemp;
                    }
                    xw.WriteAttributeString("columnas", "Valor $" + CPageTemplate.GetFormatDouble(maximo.ToString()));
                    break;
            }

            xw.WriteAttributeString("filas", "Proyectos");

            ArrayList proy = null;
            CMapaProyectos proyecto;

            if (totalp == 0)
            {
                xw.WriteStartElement("barra");
                xw.WriteAttributeString("porcentaje", "0");
                xw.WriteAttributeString("texto", "");
                xw.WriteEndElement();
            }

            for (int h = 0; h < departamentos.Count; h++)
            {
                dep = (CMapaDepartamentos)departamentos[h];
                proy = dep.GetProyectos();
                if (proy == null)
                    continue;
                switch (tipo)
                {
                    case 1:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("barra");
                            int por = maximo == 0 ? 0 : Convert.ToInt32(dep.GetAvance(proyecto) * 100 / maximo);
                            xw.WriteAttributeString("porcentaje", por.ToString());
                            xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                            xw.WriteEndElement();
                        }
                        break;
                    case 2:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("barra");
                            int por = maximo == 0 ? 0 : Convert.ToInt32(proyecto.m_nEstudiantesB * 100 / maximo);
                            xw.WriteAttributeString("porcentaje", por.ToString());
                            xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                            xw.WriteEndElement();
                        }
                        break;
                    case 3:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("barra");
                            int por = maximo == 0 ? 0 : Convert.ToInt32(proyecto.m_nAulas * 100 / maximo);
                            xw.WriteAttributeString("porcentaje", por.ToString());
                            xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                            xw.WriteEndElement();
                        }
                        break;
                    default:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("barra");
                            int por = 0;
                            if (proyecto.m_szValor.Length > 0)
                                por = maximo == 0 ? 0 : Convert.ToInt32(Convert.ToDecimal(proyecto.m_szValor) * 100 / maximo);
                            xw.WriteAttributeString("porcentaje", por.ToString());
                            xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                            xw.WriteEndElement();
                        }
                        break;
                }
            }
            xw.WriteEndElement();
            xw.WriteStartElement("descripcion");
            xw.WriteString(comentarios);
            xw.WriteEndElement();

            xw.WriteStartElement("grilla");
            xw.WriteStartElement("titulocolumna");
            xw.WriteStartElement("valor");
            xw.WriteString("Proyecto");
            switch (tipo)
            {
                case 1:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Avance");
                    break;
                case 2:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Estudiantes Beneficiados");
                    break;
                case 3:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Aulas nuevas");
                    break;
                default:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Municipio");
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Institucion");
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Objeto");
                    break;
            }
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("Valor");
            xw.WriteEndElement();
            xw.WriteEndElement();

            xw.WriteStartElement("valorescolumna");
            for (int h = 0; h < departamentos.Count; h++)
            {
                dep = (CMapaDepartamentos)departamentos[h];
                proy = dep.GetProyectos();
                if (proy != null)
                {
                    switch (tipo)
                    {
                        case 1:
                            for (int j = 0; j < proy.Count; j++)
                            {
                                proyecto = (CMapaProyectos)proy[j];
                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szCodigo);
                                xw.WriteEndElement();

                                xw.WriteStartElement("valor");
                                xw.WriteString(dep.GetAvance(proyecto).ToString() + "%");
                                xw.WriteEndElement();
                                xw.WriteStartElement("valor");
                                xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                                xw.WriteEndElement();
                            }
                            break;
                        case 2:
                            for (int j = 0; j < proy.Count; j++)
                            {
                                proyecto = (CMapaProyectos)proy[j];
                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szCodigo);
                                xw.WriteEndElement();

                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_nEstudiantesB.ToString());
                                xw.WriteEndElement();
                                xw.WriteStartElement("valor");
                                xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                                xw.WriteEndElement();
                            }
                            break;
                        case 3:
                            for (int j = 0; j < proy.Count; j++)
                            {
                                proyecto = (CMapaProyectos)proy[j];
                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szCodigo);
                                xw.WriteEndElement();

                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_nAulas.ToString());
                                xw.WriteEndElement();
                                xw.WriteStartElement("valor");
                                xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                                xw.WriteEndElement();
                            }
                            break;
                        default:
                            for (int j = 0; j < proy.Count; j++)
                            {
                                proyecto = (CMapaProyectos)proy[j];
                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szCodigo);
                                xw.WriteEndElement();

                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szMunicipio);
                                xw.WriteEndElement();
                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szInstitucion);
                                xw.WriteEndElement();
                                xw.WriteStartElement("valor");
                                xw.WriteString(proyecto.m_szObjeto);
                                xw.WriteEndElement();
                                xw.WriteStartElement("valor");
                                xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                                xw.WriteEndElement();
                            }
                            break;
                    }
                }
            }
            xw.WriteEndElement();
        }
        else
        {
            totalp = dep.GetTotalProyectos();
            valorp = dep.GetValorProyectos();

            xw.WriteStartElement("departamento");
            xw.WriteAttributeString("name", dep.m_szMapa);
            xw.WriteAttributeString("nombre", dep.m_szNombre);

            xw.WriteStartElement("color");
            xw.WriteString(dep.m_nColor == 0 ? "" : dep.m_nColor.ToString()); // 0x0000FF
            xw.WriteEndElement();

            string comentarios = "";
            if (totalp > 0)
                comentarios += "$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + " en " + totalp.ToString() + " Proyectos\r";
            if (comentarios.Length == 0)
            {
                switch (tipo)
                {
                    case 1:
                        comentarios = "No hay Informacion de ejecucion";
                        break;
                    case 2:
                        comentarios = "No hay Informacion de estudiantes";
                        break;
                    case 3:
                        comentarios = "No hay Informacion de aulas";
                        break;
                    default:
                        comentarios = "No hay Informacion de valor";
                        break;
                }
            }

            xw.WriteStartElement("comentarios");
            xw.WriteString(dep.m_szNombre + "\r" + comentarios);
            xw.WriteEndElement();

            xw.WriteStartElement("estadisticas");
            switch (tipo)
            {
                case 1:
                    maximo = dep.GetMaximoAvance();
                    xw.WriteAttributeString("columnas", "Ejecucion " + Convert.ToInt32(maximo).ToString() + "%");
                    break;
                case 2:
                    maximo = dep.GetMaxEstudiantesB();
                    xw.WriteAttributeString("columnas", "Estudiantes Beneficiados " + maximo.ToString());
                    break;
                case 3:
                    maximo = dep.GetMaxAulas();
                    xw.WriteAttributeString("columnas", "Aulas nuevas " + maximo.ToString());
                    break;
                default:
                    maximo = dep.GetMaximoValorP();
                    xw.WriteAttributeString("columnas", "Valor $" + CPageTemplate.GetFormatDouble(maximo.ToString()));
                    break;
            }
            xw.WriteAttributeString("filas", "Proyectos");

            ArrayList proy = null;
            CMapaProyectos proyecto;
            proy = dep.GetProyectos();
            if (totalp == 0)
            {
                xw.WriteStartElement("barra");
                xw.WriteAttributeString("porcentaje", "0");
                xw.WriteAttributeString("texto", "");
                xw.WriteEndElement();
            }

            switch (tipo)
            {
                case 1:
                    for (int j = 0; j < totalp; j++)
                    {
                        proyecto = (CMapaProyectos)proy[j];
                        xw.WriteStartElement("barra");
                        int por = maximo == 0 ? 0 : Convert.ToInt32(dep.GetAvance(proyecto) * 100 / maximo);
                        xw.WriteAttributeString("porcentaje", por.ToString());
                        xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                        xw.WriteEndElement();
                    }
                    break;
                case 2:
                    for (int j = 0; j < totalp; j++)
                    {
                        proyecto = (CMapaProyectos)proy[j];
                        xw.WriteStartElement("barra");
                        int por = maximo == 0 ? 0 : Convert.ToInt32(proyecto.m_nEstudiantesB * 100 / maximo);
                        xw.WriteAttributeString("porcentaje", por.ToString());
                        xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                        xw.WriteEndElement();
                    }
                    break;
                case 3:
                    for (int j = 0; j < totalp; j++)
                    {
                        proyecto = (CMapaProyectos)proy[j];
                        xw.WriteStartElement("barra");
                        int por = maximo == 0 ? 0 : Convert.ToInt32(proyecto.m_nAulas * 100 / maximo);
                        xw.WriteAttributeString("porcentaje", por.ToString());
                        xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                        xw.WriteEndElement();
                    }
                    break;
                default:
                    for (int j = 0; j < totalp; j++)
                    {
                        proyecto = (CMapaProyectos)proy[j];
                        xw.WriteStartElement("barra");
                        int por = 0;
                        if (proyecto.m_szValor.Length > 0)
                            por = maximo == 0 ? 0 : Convert.ToInt32(Convert.ToDecimal(proyecto.m_szValor) * 100 / maximo);
                        xw.WriteAttributeString("porcentaje", por.ToString());
                        xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                        xw.WriteEndElement();
                    }
                    break;
            }

            xw.WriteEndElement();
            xw.WriteStartElement("descripcion");
            xw.WriteString(comentarios);
            xw.WriteEndElement();

            xw.WriteStartElement("grilla");
            xw.WriteStartElement("titulocolumna");
            xw.WriteStartElement("valor");
            xw.WriteString("Proyecto");
            switch (tipo)
            {
                case 1:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Avance");
                    break;
                case 2:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Estudiantes Beneficiados");
                    break;
                case 3:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Aulas nuevas");
                    break;
                default:
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Municipio");
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Institucion");
                    xw.WriteEndElement();
                    xw.WriteStartElement("valor");
                    xw.WriteString("Objeto");
                    break;
            }
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("Valor");
            xw.WriteEndElement();
            xw.WriteEndElement();

            if (proy != null)
            {
                xw.WriteStartElement("valorescolumna");

                switch (tipo)
                {
                    case 1:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szCodigo);
                            xw.WriteEndElement();

                            xw.WriteStartElement("valor");
                            xw.WriteString(dep.GetAvance(proyecto).ToString() + "%");
                            xw.WriteEndElement();
                            xw.WriteStartElement("valor");
                            xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                            xw.WriteEndElement();
                        }
                        break;
                    case 2:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szCodigo);
                            xw.WriteEndElement();

                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_nEstudiantesB.ToString());
                            xw.WriteEndElement();
                            xw.WriteStartElement("valor");
                            xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                            xw.WriteEndElement();
                        }
                        break;
                    case 3:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szCodigo);
                            xw.WriteEndElement();

                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_nAulas.ToString());
                            xw.WriteEndElement();
                            xw.WriteStartElement("valor");
                            xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                            xw.WriteEndElement();
                        }
                        break;
                    default:
                        for (int j = 0; j < proy.Count; j++)
                        {
                            proyecto = (CMapaProyectos)proy[j];
                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szCodigo);
                            xw.WriteEndElement();

                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szMunicipio);
                            xw.WriteEndElement();
                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szInstitucion);
                            xw.WriteEndElement();
                            xw.WriteStartElement("valor");
                            xw.WriteString(proyecto.m_szObjeto);
                            xw.WriteEndElement();
                            xw.WriteStartElement("valor");
                            xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                            xw.WriteEndElement();
                        }
                        break;
                }
                xw.WriteEndElement();
            }
        }
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Close();
    }
}

