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
        //if (Session["mapav"] == null)
        //    return false;
        //string vigencia= Session["mapav"].ToString();

        /*
            nombre del departamento
            nombre en flash
            color
            entes territoriales cantidad
            numero de convenios
            valor total de convenios
            observaciones en el mapa
         */
        Response.ContentType = "text/xml";
        CConector conector = new CConector();
        string query = "";
        if( Request.QueryString["mapa"]=="1")
            query = "SELECT 'colombia','colombia',0,1,2,4,'mis obse'";
        else
            query = "SELECT nombre,mapa,0,1,2,4,observaciones FROM departamentos";
        SqlDataReader reader= conector.Query(query);
        if (reader != null)
        {
            XmlWriter xw = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
            xw.WriteStartDocument(true);

            xw.WriteStartElement("departamentos");
            Random rand = new Random();

            while (reader.Read())
            {
                xw.WriteStartElement("departamento");
                xw.WriteAttributeString("name", reader[1].ToString());
                xw.WriteAttributeString("nombre", reader[0].ToString());

                xw.WriteStartElement("color");
                if (reader[2].ToString() != "0")
                {
                    xw.WriteString(reader[2].ToString());
                } else xw.WriteString(rand.Next(0xFFFFFF).ToString()); 
                xw.WriteEndElement();

                xw.WriteStartElement("comentarios");
                xw.WriteString(reader[0].ToString() + " :  Entes = " + string.Format("{0:###}", reader[3]) + " -- Convenios = " + string.Format("{0:###}", reader[4]) + " -- Valor Total = " + string.Format("{0:$##,##}", reader[5]));
                xw.WriteEndElement();
                xw.WriteStartElement("estadisticas");
                // if (tipo == 1)
                // {
                    xw.WriteAttributeString("columnas", "Proyectos");
                    xw.WriteAttributeString("filas", "Ejecución");
                // }
                //if (tipo == 2)
                //{
                //    xw.WriteAttributeString("columnas", "Convenios");
                //    xw.WriteAttributeString("filas", "Ejecución");
                //}
                //if (tipo == 3)
                //{
                //    xw.WriteAttributeString("columnas", "Contratos");
                //    xw.WriteAttributeString("filas", "Ejecución");
                //}


                for (int i = 1; i < 5; i++)
                {
                    xw.WriteStartElement("barra");
                    xw.WriteAttributeString("porcentaje", "20");
                    xw.WriteAttributeString("texto", "Numero de Proyecto");
                    xw.WriteEndElement();
                }

                xw.WriteEndElement();
                xw.WriteStartElement("descripcion");
                xw.WriteString(reader[0].ToString() + " : " + reader[6].ToString());
                xw.WriteEndElement();
                xw.WriteStartElement("grilla");
                xw.WriteStartElement("titulocolumna");
                xw.WriteStartElement("valor");
                xw.WriteString("TiTulo 1");
                xw.WriteEndElement();
                xw.WriteStartElement("valor");
                xw.WriteString("TiTulo 2");
                xw.WriteEndElement();
                xw.WriteStartElement("valor");
                xw.WriteString("TiTulo 3");
                xw.WriteEndElement();
                xw.WriteStartElement("valor");
                xw.WriteString("TiTulo 4");
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
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Close();
            reader.Close();
        }
    }
}
