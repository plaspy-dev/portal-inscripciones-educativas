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
using System.IO;
using System.Xml;

public partial class datosmapa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("pragma", " no-cache");
        Response.CacheControl = "Private";
        Response.Expires = 0;
        /*
            nombre del departamento
            nombre en flash
            color
            entes territoriales cantidad
            numero de convenios
            valor total de convenios
            observaciones en el mapa
         */
        CMapa mapa = CMapa.GetMapa();
        if (mapa == null)
            return;
        Response.ContentType = "text/xml";
        
        ArrayList deps = mapa.GetDepartamentos();
        CMapaDepartamentos dep;
        decimal valorp = 0, valorc = 0, valorcont = 0;
        int totalp = 0, totalc = 0, totalcont = 0;
            
        XmlWriter xw = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
        xw.WriteStartDocument(true);
        Random rand = new Random();

        xw.WriteStartElement("departamentos");
        if (Request.QueryString["mapa"] == "1")
        {
            for (int i = 0; i < deps.Count; i++)
            {
                dep = (CMapaDepartamentos)deps[i];
                totalp += dep.GetTotalProyectos();
                totalc += dep.GetTotalConvenios();
                totalcont += dep.GetTotalContratos();
                valorc += dep.GetValorConvenios();
                valorp += dep.GetValorProyectos();
                valorcont += dep.GetValorContratos();
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
            if (totalc > 0)
                comentarios += "$" + CPageTemplate.GetFormatDouble(valorc.ToString()) + " en " + totalc.ToString() + " Convenios\r";
            if (totalcont > 0)
                comentarios += "$" + CPageTemplate.GetFormatDouble(valorcont.ToString()) + " en " + totalcont.ToString() + " Contratos";
            if (comentarios.Length == 0)
                comentarios = "No hay proyectos";

            xw.WriteStartElement("comentarios");
            xw.WriteString("Colombia\r" + comentarios);
            xw.WriteEndElement();

            xw.WriteStartElement("estadisticas");
            xw.WriteAttributeString("columnas", "Proyectos");
            xw.WriteAttributeString("filas", "Ejecución");

            for (int i = 0; i < 5; i++)
            {
                xw.WriteStartElement("barra");
                xw.WriteAttributeString("porcentaje", "20");
                xw.WriteAttributeString("texto", "Numero de Proyecto");
                xw.WriteEndElement();
            }

            xw.WriteEndElement();
            xw.WriteStartElement("descripcion");
            xw.WriteString(comentarios);
            xw.WriteEndElement();
            xw.WriteStartElement("grilla");
            xw.WriteStartElement("titulocolumna");
            xw.WriteStartElement("valor");
            xw.WriteString("Proyecto");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("Valor");
            xw.WriteEndElement();
            xw.WriteEndElement();

            xw.WriteStartElement("valorescolumna");
            xw.WriteStartElement("valor");
            xw.WriteString("valor 1");
            xw.WriteEndElement();

            xw.WriteStartElement("valor");
            xw.WriteString("valor 2");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("valor 3");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("valor 4");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("valor 11");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("valor 12");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("valor 13");
            xw.WriteEndElement();
            xw.WriteStartElement("valor");
            xw.WriteString("valor 14");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
        }
        else
        {
            for (int i = 0; i < deps.Count; i++)
            {
                dep = (CMapaDepartamentos)deps[i];
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
                xw.WriteString(dep.m_nColor==0 ? "" : dep.m_nColor.ToString()); // 0x0000FF
                xw.WriteEndElement();

                string comentarios = "";
                if (totalc > 0)
                    comentarios += "Convenios\t" + totalc.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorc.ToString()) + "\r";
                if(totalp>0)
                    comentarios += "Proyectos\t" + totalp.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + "\r";
                if (comentarios.Length == 0)
                {
                    comentarios = "No hay proyectos";
                }
                else
                {
                    comentarios = "\t\t\tNumero\tValor\n" + comentarios;
                    comentarios += "Estudiantes Beneficiados\t500\r";
                    comentarios += "Aulas Construidas\t\t\t10\r";
                }

                xw.WriteStartElement("comentarios");
                xw.WriteString(dep.m_szNombre + "\r"+  comentarios);
                xw.WriteEndElement();

                xw.WriteStartElement("estadisticas");
                xw.WriteAttributeString("columnas", "Proyectos");
                xw.WriteAttributeString("filas", "Ejecución");

                ArrayList proy = dep.GetProyectos();
                CMapaProyectos proyecto;

                for (int j = 0; j < totalp; j++)
                {
                    proyecto = (CMapaProyectos)proy[j];
                    xw.WriteStartElement("barra");
                    xw.WriteAttributeString("porcentaje", rand.Next(100).ToString());
                    xw.WriteAttributeString("texto", proyecto.m_szCodigo);
                    xw.WriteEndElement();
                }

                xw.WriteEndElement();
                xw.WriteStartElement("descripcion");
                xw.WriteString(comentarios);
                xw.WriteEndElement();
                
                xw.WriteStartElement("grilla");
                    xw.WriteStartElement("titulocolumna");
                        xw.WriteStartElement("valor");
                        xw.WriteString("Proyecto");
                        xw.WriteEndElement();
                        xw.WriteStartElement("valor");
                        xw.WriteString("Valor");
                        xw.WriteEndElement();
                    xw.WriteEndElement();

                if (proy != null)
                {
                    xw.WriteStartElement("valorescolumna");
                    for (int j = 0; j < proy.Count; j++)
                    {
                        proyecto = (CMapaProyectos)proy[j];
                        xw.WriteStartElement("valor");
                        xw.WriteString(proyecto.m_szCodigo);
                        xw.WriteEndElement();
                        xw.WriteStartElement("valor");
                        xw.WriteString(CPageTemplate.GetFormatDouble(proyecto.m_szValor));
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Close();
    }
}
