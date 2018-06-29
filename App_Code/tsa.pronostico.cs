using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using System.IO;

namespace TSA.Pronostico
{

	public static class Funciones
	{
	
		public static void URLaLocalidad(string URL, out string Localidad, 
			out string Pais)
		{
			string[] Items = URL.Split('/');
			if (Items.Length > 1) 
			{
				Pais = Items[0].Replace("+", " ").Replace("-", " ").Replace("/", "").Replace("'", "\\'").Split('&')[0];
				Localidad = Items[1].Replace("+", " ").Replace("-", " ").Replace("/", "").Replace("'", "\\'").Split('&')[0];
			}
			else
			{
				Localidad = Items[0].Replace("+", " ").Replace("-", " ").Replace("/", "").Replace("'", "\\'").Split('&')[0];
				Pais = "Argentina";
			}
		}
	
		public static string LocalidadaURL(string Localidad)
		{
			string[] Items = Localidad.Split(',');
			string URL = "";
			if (Items.Length > 2)
			{
				if (Items[2].Trim().ToLower() != "argentina")
					URL = Items[2].Trim() + "/";
			}
			else
				if ((Items.Length > 1) && ((Items[1].Trim().ToLower() != "argentina"))) URL = Items[1].Trim() + "/";
			URL = URL + Items[0].Trim();
			URL = URL.ToLower().Replace("+", "-").Replace(" ", "-").Split('&')[0] + "/";
			return TSA.General.Funciones.NormalizarTexto(URL);
		}
		
		public static string LocalidadActual()
		{
			string localidad = "";
			if (HttpContext.Current.Request["localidad"] != null)
				localidad = HttpContext.Current.Request["localidad"].ToString().ToLower().Trim().Replace(" ", "-").TrimEnd('/');
			else
				if (HttpContext.Current.Request["location"] != null)
					localidad = HttpContext.Current.Request["location"].ToString().ToLower().Trim().Replace(" ", "-").TrimEnd('/');
				else
					if (HttpContext.Current.Session["usuarioLocalidad1Nombre"] != null)
						localidad = HttpContext.Current.Session["usuarioLocalidad1Nombre"].ToString().ToLower().Trim().Replace(" ", "-");
			string[] s = localidad.Split(',');
			if (s.Length > 1) return s[1].Trim(); else return s[0].Trim();
		}
		
		public static string PaisActual()
		{
			string pais = "";
			if (HttpContext.Current.Request["pais"] != null)
				pais = HttpContext.Current.Request["pais"].ToString().ToLower().Trim().Replace("-", " ").TrimEnd('/');
			else
				if (HttpContext.Current.Session["usuarioPais1Nombre"] != null)
					pais = HttpContext.Current.Session["usuarioPais1Nombre"].ToString().Replace("-", " ");
			return pais;
		}
		
		public static string ProvinciaActual()
		{
			string provincia = "";
			if (HttpContext.Current.Request["provincia"] != null)
				provincia = HttpContext.Current.Request["provincia"].ToString().ToLower().Trim().Replace("-", " ").TrimEnd('/');
			else
				if (HttpContext.Current.Session["usuarioProvincia1Nombre"] != null)
					provincia = HttpContext.Current.Session["usuarioProvincia1Nombre"].ToString().Replace("-", " ");
			return provincia;
		}
		
		public static string RegionActual()
		{
			string region = "";
			if (HttpContext.Current.Request["region"] != null)
				region = HttpContext.Current.Request["region"].ToString().ToLower().Trim().Replace("-", " ").TrimEnd('/');
			else
				if (HttpContext.Current.Session["usuarioRegion1Nombre"] != null)
					region = HttpContext.Current.Session["usuarioRegion1Nombre"].ToString().Replace("-", " ");
			return region;
		}
		
		public static string DepartamentoActual()
		{
			string departamento = "";
			if (HttpContext.Current.Request["departamento"] != null)
				departamento = HttpContext.Current.Request["departamento"].ToString().ToLower().Trim().Replace("-", " ").TrimEnd('/');
			else
				if (HttpContext.Current.Session["usuarioDepartamento1Nombre"] != null)
					departamento = HttpContext.Current.Session["usuarioDepartamento1Nombre"].ToString().Replace("-", " ");
			return departamento;
		}
		
	}

}
