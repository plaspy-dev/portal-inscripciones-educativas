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
        int benef = 0,aulas=0, dotacion=0, aulasr=0, benefd=0;
            
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
                dotacion += dep.GetDotacion();
                aulasr += dep.GetAulasR();
                benefd += dep.GetEstudiantesBeneficiadosDot();
            }
            xw.WriteStartElement("departamento");
            xw.WriteAttributeString("name", "colombia");
            xw.WriteAttributeString("nombre", "Colombia");

            xw.WriteStartElement("color");
            xw.WriteString("");
            xw.WriteEndElement();

            string comentarios = "";
            if (totalc > 0)
            {
                comentarios += totalc.ToString() + " convenios por valor de $" + CPageTemplate.GetFormatDouble(valorc.ToString()) + "\r";
                // comentarios += "Convenios\t" + totalc.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorc.ToString()) + "\r";
            }
            if (totalp > 0)
            {
                comentarios += totalp.ToString() + " proyectos por valor de $" + CPageTemplate.GetFormatDouble(valorp.ToString()) + "\r";
                // comentarios += "Proyectos\t" + totalp.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + "\r";
            }
            if (comentarios.Length == 0)
            {
                comentarios = "No hay proyectos";
            }
            else
            {
                // comentarios = "\t\t\tNumero\tValor\n" + comentarios;
                if (benef > 0)
                    comentarios += benef.ToString() + " Estudiantes beneficiados por aulas nuevas\r";
                if (benefd > 0)
                    comentarios += benefd.ToString() + " Estudiantes beneficiados por dotaciones\r";
                if (dotacion > 0)
                    comentarios += dotacion.ToString() + " Dotaciones\r";
                if (aulas > 0)
                    comentarios += aulas.ToString() + " Aulas nuevas\r";
                if (aulasr > 0)
                    comentarios += aulasr.ToString() + " Aulas de reposicion\r";
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
                dotacion = dep.GetDotacion();
                aulasr = dep.GetAulasR();
                benefd = dep.GetEstudiantesBeneficiadosDot();

                xw.WriteStartElement("departamento");
                xw.WriteAttributeString("name", dep.m_szMapa);
                xw.WriteAttributeString("nombre", dep.m_szNombre);

                xw.WriteStartElement("color");
                xw.WriteString(dep.m_nColor==0 ? "" : dep.m_nColor.ToString()); // 0x0000FF
                xw.WriteEndElement();

                string comentarios = "";
                if (totalc > 0)
                {
                    comentarios += totalc.ToString() + " convenios por valor de $" + CPageTemplate.GetFormatDouble(valorc.ToString()) + "\r";
                    // comentarios += "Convenios\t" + totalc.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorc.ToString()) + "\r";
                }
                if (totalp > 0)
                {
                    comentarios += totalp.ToString() + " proyectos por valor de $" + CPageTemplate.GetFormatDouble(valorp.ToString()) + "\r";
                    // comentarios += "Proyectos\t" + totalp.ToString() + "\t\t$" + CPageTemplate.GetFormatDouble(valorp.ToString()) + "\r";
                }
                if (comentarios.Length == 0)
                {
                    comentarios = "No hay proyectos";
                }
                else
                {
                    // comentarios = "\t\t\tNumero\tValor\n" + comentarios;
                    if(benef>0)
                        comentarios += benef.ToString() + " Estudiantes beneficiados por aulas nuevas\r";
                    if (benefd > 0)
                        comentarios += benefd.ToString() + " Estudiantes beneficiados por dotaciones\r";
                    if (dotacion > 0)
                        comentarios += dotacion.ToString() + " Dotaciones\r";
                    if (aulas > 0)
                        comentarios += aulas.ToString() + " Aulas nuevas\r";
                    if (aulasr > 0) 
                        comentarios += aulasr.ToString() + " Aulas de reposicion\r";
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
