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
        if( Session["tipomapa"].ToString()=="convenios")
            tipo=1;
        else if( Session["tipomapa"].ToString()=="contratos")
            tipo=2;

        CMapaDepartamentos dep = mapa.GetDepartamento(Session["departamentomapa"].ToString());
        if (dep == null)
            return;

        Response.ContentType = "text/xml";
        decimal valorp = 0, valorc = 0, valorcont = 0;
        int totalp = 0, totalc = 0, totalcont = 0;

        XmlWriter xw = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
        xw.WriteStartDocument(true);
        Random rand = new Random();

        xw.WriteStartElement("departamentos");
        totalp = dep.GetTotalProyectos();
        totalc = dep.GetTotalConvenios();
        totalcont = dep.GetTotalContratos();
        valorc = dep.GetValorConvenios();
        valorp = dep.GetValorProyectos();
        valorcont = dep.GetValorContratos();

        xw.WriteStartElement("departamento");
        xw.WriteAttributeString("name", dep.m_szMapa);
        xw.WriteAttributeString("nombre", dep.m_szNombre);

        xw.WriteStartElement("color");
        xw.WriteString(dep.m_nColor == 0 ? "" : dep.m_nColor.ToString()); // 0x0000FF
        xw.WriteEndElement();

        string comentarios = "";
        if (totalp > 0)
            comentarios += "$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + " en " + totalp.ToString() + " Proyectos\r";
        if (totalc > 0)
            comentarios += "$" + CPageTemplate.GetFormatDouble(valorc.ToString()) + " en " + totalc.ToString() + " Convenios\r";
        if (totalcont > 0)
            comentarios += "$" + CPageTemplate.GetFormatDouble(valorcont.ToString()) + " en " + totalcont.ToString() + " Contratos";
        if (comentarios.Length == 0)
        {
            switch (tipo)
            {
                case 1:
                    comentarios = "No hay convenios";
                    break;
                case 2:
                    comentarios = "No hay contratos";
                    break;
                default:
                    comentarios = "No hay proyectos";
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
                xw.WriteAttributeString("columnas", "Convenios");
                break;
            case 2:
                xw.WriteAttributeString("columnas", "Contratos");
                break;
            default:
                xw.WriteAttributeString("columnas", "Proyectos");
                break;
        }
        xw.WriteAttributeString("filas", "Ejecución");

        ArrayList proy = null;
        CMapaProyectos proyecto;
        CMapaContratos contrato;
        CMapaConvenios convenio;

        switch (tipo)
        {
            case 1:
                proy = dep.GetConvenios();
                for (int j = 0; j < totalc; j++)
                {
                    convenio= (CMapaConvenios)proy[j];
                    xw.WriteStartElement("barra");
                    xw.WriteAttributeString("porcentaje", rand.Next(100).ToString());
                    xw.WriteAttributeString("texto", convenio.m_szCodigo);
                    xw.WriteEndElement();
                }
                break;
            case 2:
                proy = dep.GetContratos();
                for (int j = 0; j < totalcont; j++)
                {
                    contrato = (CMapaContratos)proy[j];
                    xw.WriteStartElement("barra");
                    xw.WriteAttributeString("porcentaje", rand.Next(100).ToString());
                    xw.WriteAttributeString("texto", contrato.m_szCodigo);
                    xw.WriteEndElement();
                }
                break;
            default:
                proy = dep.GetProyectos();
                for (int j = 0; j < totalp; j++)
                {
                    proyecto = (CMapaProyectos)proy[j];
                    xw.WriteStartElement("barra");
                    xw.WriteAttributeString("porcentaje", rand.Next(100).ToString());
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
        switch (tipo)
        {
            case 1:
                xw.WriteString("Convenios");
                break;
            case 2:
                xw.WriteString("Contratos");
                break;
            default:
                xw.WriteString("Proyecto");
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
                        convenio = (CMapaConvenios)proy[j];
                        xw.WriteStartElement("valor");
                        xw.WriteString(convenio.m_szCodigo);
                        xw.WriteEndElement();
                        xw.WriteStartElement("valor");
                        xw.WriteString(CPageTemplate.GetFormatDouble(convenio.m_szValor));
                        xw.WriteEndElement();
                    }
                    break;
                case 2:
                    for (int j = 0; j < proy.Count; j++)
                    {
                        contrato = (CMapaContratos)proy[j];
                        xw.WriteStartElement("valor");
                        xw.WriteString(contrato.m_szCodigo);
                        xw.WriteEndElement();
                        xw.WriteStartElement("valor");
                        xw.WriteString(CPageTemplate.GetFormatDouble(contrato.m_szValor));
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
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Close();
    }
}

