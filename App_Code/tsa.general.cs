using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;

namespace TSA.General
{

	public static class Funciones
	{
	
		public static void LeerCookies()
		{
			if ((HttpContext.Current.Request.Cookies["PELoginUID"] != null) && (HttpContext.Current.Request.Cookies["PELoginSID"] != null))
			{
				string correoElectronico = HttpContext.Current.Request.Cookies["PELoginUID"].Value;
				string password = HttpContext.Current.Request.Cookies["PELoginSID"].Value;
				DataTable tabla = ConsultarSQL("SELECT idUsuarios, nombres, idLocalidades1, idLocalidades2, idTiposAccesos FROM usuarios " +
				"WHERE activo = true AND habilitado = true AND correoElectronico = '" + 
				correoElectronico.Replace("'", "&#39") + "' AND MD5(password) = '" + password.Replace("'", "&#39") + "'");
				if (tabla.Rows.Count == 1)
				{
					HttpContext.Current.Session["idUsuarios"] = tabla.Rows[0].ItemArray[0].ToString();
					HttpContext.Current.Session["usuarioNombre"] = tabla.Rows[0].ItemArray[1].ToString();
					if (tabla.Rows[0].ItemArray[2].ToString() != "")
						HttpContext.Current.Session["usuarioLocalidad1"] = tabla.Rows[0].ItemArray[2].ToString();
					else
						HttpContext.Current.Session.Remove("usuarioLocalidad1");
					if (tabla.Rows[0].ItemArray[3].ToString() != "") 
						HttpContext.Current.Session["usuarioLocalidad2"] = tabla.Rows[0].ItemArray[3].ToString();
					else
						HttpContext.Current.Session.Remove("usuarioLocalidad2");
					HttpContext.Current.Session["usuarioPais1"] = "10";
					HttpContext.Current.Session["usuarioPais1Nombre"] = "Argentina";
					HttpContext.Current.Session["usuarioAcceso"] = tabla.Rows[0].ItemArray[4].ToString();
				}
			}
			if (HttpContext.Current.Request.Cookies["PEConfigMetrico"] != null)
			{
				string metrico = HttpContext.Current.Request.Cookies["PEConfigMetrico"].Value;
				HttpContext.Current.Session["usuarioMetrico"] = metrico;
			}
			HttpContext.Current.Session["CookieLeida"] = "1";
		}
	
		public static bool VerificarSesionIniciada()
		{
			if (HttpContext.Current.Session["idUsuarios"] == null)
			{
				HttpContext.Current.Response.Redirect("/usuarios/login/?ir=" + HttpContext.Current.Request.Url.AbsolutePath.Replace("default.aspx", ""));
				return false;
			}
			return true;
		}
		
		public static string CanonicoActual()
		{
			string Canon = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0].Replace("default.aspx", "");
			return Canon.ToLower().Replace(" ", "-").Replace("http:", "https:");
		}
		
		public static DataTable ConsultarSQL(string SQL)
		{
			SqlDataSource ds = new SqlDataSource();
			ds.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tsa_cam"].ConnectionString;
			ds.ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["tsa_cam"].ProviderName;
			ds.SelectCommand = SQL;
			return ((DataView)ds.Select(DataSourceSelectArguments.Empty)).Table;
		}
		
		public static void EjecutarSQL(string SQL)
		{
			SqlDataSource ds = new SqlDataSource();
			ds.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tsa_cam"].ConnectionString;
			ds.ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["tsa_cam"].ProviderName;
			ds.UpdateCommand = SQL;
			ds.Update();
		}
		
		public static bool GeoLocalizarIP(out string Localidad, out string Provincia, out string Pais)
		{
			Localidad = "";
			Provincia = "";
			Pais = "";
			return false;
			WebClient WC = new WebClient();
			string IP = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? 
				HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
		}
				
		public static string NormalizarTexto(string Texto) 
		{
			string normalizedString = Texto.Normalize(NormalizationForm.FormD);
			StringBuilder builder = new StringBuilder();
			foreach (char c in normalizedString)
			{
				UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
				if (category != UnicodeCategory.NonSpacingMark)
					builder.Append(c);
			}
			return builder.ToString().Normalize(NormalizationForm.FormC);
		}		
	}

}
