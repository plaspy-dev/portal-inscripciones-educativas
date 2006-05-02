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
        decimal valorp = 0, valorc = 0;
        int totalp = 0, totalc = 0;
        int benef = 0,aulas=0;
            
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
                valorc += dep.GetValorConvenios();
                valorp += dep.GetValorProyectos();
                aulas += dep.GetAulasNuevas();
                benef += dep.GetEstudiantesBeneficiados();
            }
            xw.WriteStartElement("departamento");
            xw.WriteAttributeString("name", "colombia");
            xw.WriteAttributeString("nombre", "Colombia");

            xw.WriteStartElement("color");
            xw.WriteString("");
            xw.WriteEndElement();

            string comentarios = "";
            if (totalc > 0)
                comentarios += "Convenios\t" + totalc.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorc.ToString()) + "\r";
            if (totalp > 0)
                comentarios += "Proyectos\t" + totalp.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + "\r";
            if (comentarios.Length == 0)
            {
                comentarios = "No hay proyectos";
            }
            else
            {
                comentarios = "\t\t\tNumero\tValor\n" + comentarios;
                comentarios += "Estudiantes Beneficiados\t" + benef.ToString() + "\r";
                comentarios += "Aulas Construidas\t\t\t" + aulas.ToString() + "\r";
            }

            xw.WriteStartElement("comentarios");
            xw.WriteString("Colombia\r" + comentarios);
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
                valorc = dep.GetValorConvenios();
                valorp = dep.GetValorProyectos();
                benef = dep.GetEstudiantesBeneficiados();
                aulas = dep.GetAulasNuevas();

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
                    comentarios += "Estudiantes Beneficiados\t" + benef.ToString() +"\r";
                    comentarios += "Aulas Construidas\t\t\t"+aulas.ToString() + "\r";
                }

                xw.WriteStartElement("comentarios");
                xw.WriteString(dep.m_szNombre + "\r"+  comentarios);
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Close();
    }
}
