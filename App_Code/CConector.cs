using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CConector
/// </summary>
public class CConector
{
    protected string m_szServidor;
    protected string m_szBaseDeDatos;
    protected string m_szUsuario;
    protected string m_szContrasena;
    protected string m_szLastError;
    protected string m_szQuery;

    protected SqlConnection m_pSQLConector;
    protected SqlDataReader m_pSQLReader;

    public CConector()
	{
		// cargamos los valores por defect
		m_szServidor= GetProfileString("Servidor", "localhost");
		m_szBaseDeDatos= GetProfileString("DB", "OrganizationExample22");
		m_szUsuario= GetProfileString("usuario","sa");
		m_szContrasena= GetProfileString("contrasena","");
        m_szQuery = "";
		m_szLastError="";			
		m_pSQLConector= null;
		m_pSQLReader= null;
        QueryMod("SET DATEFORMAT dmy");
	}

    public SqlConnection GetConexion() {
        return m_pSQLConector;
    }

    public static bool IsLogged(Page pagina)
    {
        if (CConector.GetProfileInt("administrador", 0) == 1)
        {
            if (pagina.Session["usuario"]== null)
            {
                CUsuarioDatos usuario = new CUsuarioDatos("Administrador", "2000", "0", "0", "Administrador");
                pagina.Session["usuario"] = usuario;
            }
            return true;
        }
        return pagina.Session["usuario"] != null;
    }

    static public string GetCookie(string cookie)
    {
        return GetCookie(cookie, "");
    }

    static public string GetCookie(string cookie, string defecto)
    {
        string valor = defecto;
        if (HttpContext.Current.Request.Cookies[cookie]!=null)
            valor = HttpContext.Current.Request.Cookies[cookie].Value.Replace("%%"," ");
        return valor;
    }

    static public int GetCookie(string cookie, int defecto)
    {
        return Convert.ToInt32(GetCookie(cookie, defecto.ToString()));
    }

    static public void SetCookie(string cookie, string valor)
    {
        HttpContext.Current.Response.Cookies[cookie].Value = valor.Replace(" ", "%%");
        HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddYears(1);
    }

	    static public int GetProfileInt(string key, int valor) 
		{
			string variable=System.Configuration.ConfigurationManager.AppSettings[key];
			if( variable==null)
				return valor;
			return Convert.ToInt32(variable);
		}

		static public string GetProfileString(string key, string valor) 
		{
            string variable = System.Configuration.ConfigurationManager.AppSettings[key];
			if( variable==null)
				variable=valor;
			return variable;
		}

		public string GetConnectionString() 
		{
			string conn= GetProfileString("conexion", null);
			if( conn==null) 
				conn= "Data Source=" + m_szServidor + ";Initial Catalog=" + m_szBaseDeDatos+ ";User ID=" + m_szUsuario + ";Password=" + m_szContrasena+ ";";
			return conn;
		}

		protected bool IsConect() 
		{
			if( m_pSQLConector==null)  // verificamos que la conexion exista
			{
				// solo por guardar compatibilidad con la version anterior 'coneccion'
				string conn= GetConnectionString();
				try 
				{
					m_pSQLConector= new SqlConnection(conn);
					m_pSQLConector.Open();
				}
				catch (SqlException ex ) 
				{
					m_szLastError= ex.Message;
					return false;
				}
				return true;
			}
			return true;
		}

		public SqlDataAdapter QueryAdaptador(string query) 
		{
			return new SqlDataAdapter(query, GetConnectionString());
		}

		public int QueryMod( string query) 
		{
			return QueryMod(query, false);
		}

		public int QueryMod( string query, bool identity) 
		{
            m_szQuery = query;
            if (query.IndexOf(" WHERE") == 0)
                return 0;
			int result=0;
			if(!IsConect())
				return -1;
			if( m_pSQLReader!=null) 
			{
				m_pSQLReader.Close();
				m_pSQLReader= null;
			}
			try 
			{
				SqlCommand command= m_pSQLConector.CreateCommand();
				command.CommandText = query;
				result= command.ExecuteNonQuery();
			} 
			catch (SqlException ex) 
			{
				result=-1;
				m_szLastError= ex.Message;
			}
			if(identity && result!=-1) 
			{
				result=-1;
				if(Query("select @@identity")!=null) 
				{
					if(m_pSQLReader.Read()) 
					{
						result= Convert.ToInt32(m_pSQLReader[0].ToString());
					}
				}
			}
			return result;
		}

    public string QueryString(string query)
    {
        SqlDataReader data= Query(query);
        string retorno="";
        if (data != null && data.Read())
        {
            retorno = data[0].ToString();
        }
        return retorno;
    }

		public SqlDataReader Query(string query) 
		{
			return Query(query, false);
		}

		public SqlDataReader Query(string query, bool reset) 
		{
			if(!IsConect() || query==null || query.Length==0)
				return null;
            m_szQuery = query;
			if( m_pSQLReader!=null) 
			{
				m_pSQLReader.Close();
				m_pSQLReader= null;
			}
			try 
			{
				if( reset)
					QueryMod("SET ROWCOUNT 0");
				SqlCommand command= m_pSQLConector.CreateCommand();
				command.CommandText = query;
				m_pSQLReader = command.ExecuteReader();
			} 
			catch (SqlException ex) 
			{
				m_pSQLReader= null;
				m_szLastError= ex.Message;
			}
			return m_pSQLReader;
		}

		public int GetColCount() 
		{
			if( m_pSQLReader==null)
				return 0;
			return m_pSQLReader.FieldCount;
		}

		public string GetLastError()
		{
			return m_szLastError+ "<br>"+ m_szQuery;
		}

		public static string GetCondicion(string campo, string dato) 
		{
			return GetCondicion(campo, dato, "=");
		}

		public static string GetCondicion(string campo, string dato, string comparador) 
		{
			return " WHERE " + campo +" " +comparador + " '" + dato + "'";
		}

		public static string GetCondiciones(string []campos, string []datos, string comparador) 
		{
			string []comp= new string[campos.Length];
			for( int i=0; i<comp.Length; i++)
				comp[i]= comparador;
			return GetCondiciones(campos, datos, comp);
		}

		public static string GetCondiciones(string []campos, string []datos, string []comparador) 
		{
			string condicion="";
			for( int i=0; i<campos.Length; i++) 
			{
				if( campos[i].Length==0 || datos[i].Length==0)
					continue;
				if( condicion.Length!=0)
					condicion+=" AND ";
				if( comparador[i]=="LIKE")
					condicion+=campos[i] + " LIKE '%" + datos[i] + "%'";
				else condicion+=campos[i] + comparador[i] +"'" + datos[i] + "'"; 
			}
			if( condicion.Length!=0)
				condicion= " WHERE " + condicion;
			return condicion;
		}

		public static string CrearInsercion(string tabla, string []campos) 
		{
			string query="";
			string valor;
			query= "INSERT INTO " + tabla + " VALUES ( ";
			
			for( int i=0; i<campos.Length; i++) 
			{
				if( campos[i].Length==0)
					valor="NULL";
				else if( campos[i][0]=='%')
					valor=campos[i].Substring(1);
				else valor="'" + campos[i] + "'";
				if( i!=0)
					query+=",";
				query+=valor;
			}
			query+=" )";
			return query;
		}

		public static string CrearInsercion(string tabla, string []campos, string []valores) 
		{
			string query="";
			string valor;
			query= "INSERT INTO " + tabla +"(";
			int total=0;
			for( int i=0; i<campos.Length; i++) 
			{
				if( campos[i].Length==0)
					continue;
				if(total!=0)
					query+=",";
				query+=campos[i];
				total++;
			}
			total=0;
			query+=") VALUES (";
			for( int i=0; i<campos.Length; i++) 
			{
				if( campos[i].Length==0)
					continue;
				if( valores[i].Length==0)
					valor="NULL";
				else if( valores[i][0]=='%')
					valor=valores[i].Substring(1);
				else valor="'" + valores[i] + "'";
				if(total!=0)
					query+=",";
				query+=valor;
				total++;
			}
			query+=" )";
			return query;
		}
		
		public static string CreateUpdate(string tabla, string campo, string dato) 
		{
            if(dato.Length==0)
                return "UPDATE " + tabla + " SET " + campo + "=NULL";
            else if( dato[0]=='%')
                return "UPDATE " + tabla + " SET " + campo + "=" + dato.Substring(1);
			return "UPDATE " + tabla + " SET " + campo + "='" + dato + "'";
		}

		public static string CreateUpdate(string tabla, string []campos, string []datos) 
		{
			string update="UPDATE " + tabla + " SET ";
			bool primero=false;
			for( int i=0; i<campos.Length; i++) 
			{
				if(campos[i]==null || datos[i]==null || campos[i].Length==0 || datos[i].Length==0)
					continue;
				if( primero)
					update+=" , ";
				else primero=true;
				if( datos[i][0]=='%')
					update+=campos[i] + "=" + datos[i].Substring(1);
				else update+=campos[i] + "='" + datos[i] + "'";
			}
			return primero ? update : "";
		}

		public static string CrearInsercion(string tabla, string []campos, string condicion) 
		{
			string query="";
			string valor;
			query= "INSERT INTO " + tabla + " SELECT ";
			
			for( int i=0; i<campos.Length; i++) 
			{
				if( campos[i].Length==0)
					valor="NULL";
				else if( campos[i][0]=='%')
					valor=campos[i].Substring(1);
				else valor="'" + campos[i] + "'";
				if( i!=0)
					query+=",";
				query+=valor;
			}
			query+=" FROM " + tabla;
			if( condicion.Length!=0)
				query+= " WHERE " + condicion;
			return query;
		}

		public static string CrearSelectT(string tabla, string []campos, string []titulos) 
		{
			return CrearSelectT(tabla, campos, titulos, "");

		}

		public static string CrearSelectT(string tabla, string []campos, string []titulos, string join) 
		{
			string select= "SELECT ";
			bool primero=false;
			for( int i=0; i<campos.Length; i++) 
			{
				if(campos[i]==null || campos[i].Length==0 )
					continue;
				if( primero)
					select+=" , ";
				else primero=true;
				select+=campos[i];
				if( titulos[i]!=null && titulos[i].Length!=0) 
				{
					select+=" AS '" + titulos[i] + "'";
				}
			}
			return select + " FROM " + tabla + " " + join;
		}

		public static string CrearSelectT(string tabla, string []campos, string []datos, string []titulos) 
		{
			return CrearSelectT(tabla, campos, datos, titulos, "") ;
		}

		public static string CrearSelectT(string tabla, string []campos, string []datos, string []titulos, string join) 
		{
			string select= "SELECT ";
			bool primero=false;
			for( int i=0; i<campos.Length; i++) 
			{
				if(campos[i]==null || datos[i]==null || campos[i].Length==0 || datos[i].Length==0)
					continue;
				if( primero)
					select+=" , ";
				else primero=true;
				select+=campos[i];
				if( titulos[i]!=null && titulos[i].Length!=0) 
				{
					select+=" AS '" + titulos[i] + "'";
				}
			}
			return select + " FROM " + tabla + " " + join;
		}

    static public string GetCurrentVersion()
    {
        return "Versión 1.17";
    }

    static public CConector GetConector() {

        if (HttpContext.Current.Session["Conector"] == null)
        {
            CConector conector = new CConector();
            HttpContext.Current.Session["Conector"] = conector;
        }
        return (CConector) HttpContext.Current.Session["Conector"];
    }
}