using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Text.RegularExpressions;

namespace TSA.Articulo
{

	public static class Funciones
	{
		
		public static string StripTags(string content)
		{
			return MatchReplace(@"< [^>]+>", "", content, true, true, true);
		}
		 
		public static string MatchReplace(string pattern, string match, string content)
		{
			return MatchReplace(pattern, match, content, false, false, false);
		}
		 
		public static string MatchReplace(string pattern, string match, string content, bool multi)
		{
			return MatchReplace(pattern, match, content, multi, false, false);
		}
		 
		public static string MatchReplace(string pattern, string match, string content, bool multi, bool white)
		{
			return MatchReplace(pattern, match, content, multi, white);
		}
		 
		public static string MatchReplace(string pattern, string match, string content, bool multi, bool white, bool cult)
		{
			if (multi && white && cult)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			else if (multi && white)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnoreCase);
			else if (multi && cult)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
			else if (white && cult)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
			else if (multi)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.Multiline);
			else if (white)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			else if (cult)
				return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		 
			// Default
			return Regex.Replace(content, pattern, match, RegexOptions.IgnoreCase);
		}
		
		public static string ArticuloIniciar(string content)
		{
			content = StripTags(content);
			content = MatchReplace(@"\[b\]([^\]]+)\[\/b\]", "<strong>$1</strong>", content);
			content = MatchReplace(@"\[i\]([^\]]+)\[\/i\]", "<em>$1</em>", content);
			content = MatchReplace(@"\[u\]([^\]]+)\[\/u\]", "<span style=\"text-decoration:underline\">$1</span>", content);
			content = MatchReplace(@"\[del\]([^\]]+)\[\/del\]", "<span style=\"text-decoration:line-through\">$1</span>", content);
			content = MatchReplace(@"\[google\]([^\]]+)\[\/google\]", "<a href=\"http://www.google.com/search?q=$1\">$1", content);
			content = MatchReplace(@"\[wikipedia\]([^\]]+)\[\/wikipedia\]", "<a href=\"http://www.wikipedia.org/wiki/$1\">$1</a>", content);
			return content;
		}

		public static string ArticuloImagenDB(string content)
		{
			// Imagen DB
			Regex r = new Regex(@"\[imgdb=([a-z-]+)\](.*?)\[\/imgdb\]", RegexOptions.IgnoreCase);
			Match m = r.Match(content);
			while (m.Success) 
			{
				string estilo = m.Groups[1].Value;
				int id = int.Parse(m.Groups[2].Value);
				string url = "";
				string desc = "";
				ObtenerImagenDB(id, out url, out desc);
				string link = url;
				if (!url.Contains("://"))
				{
					string archivo = HttpContext.Current.Server.MapPath(url);
					int indice = archivo.LastIndexOf(".");
					string thumb = archivo.Substring(0, indice) + "-320." + archivo.Substring(indice + 1);
					if ((estilo != "full") && (System.IO.File.Exists(thumb)))
					{
						indice = url.LastIndexOf(".");
						url = url.Substring(0, indice) + "-320." + url.Substring(indice + 1);
					}
					url = "//cdn.pronosticoextendido.net" + url;
				}
				content = content.Replace(m.Value, "<div class=\"div-pub-img-" + estilo + "\">" + 
				"<a href=\"" + url.Replace("-320.", ".") + "\" data-lightbox=\"articulo\">" +
				"<img class=\"lazy\" data-src=\"" + url + "\" alt=\"" + desc + "\" /></a>" +
				"<p>" + desc + "</p></div>");
				m = m.NextMatch();
			}
			return content;
		}
		
		public static string ArticuloHTML(string content)
		{
			content = ArticuloIniciar(content);
			content = ArticuloImagenDB(content);
			content = ArticuloFinalizar(content);
			return content;
		}
		
		public static string ArticuloFotoHTML(string content)
		{
			content = ArticuloIniciar(content);
			// Imágenes
			Regex r = new Regex(@"\[imgdb=([a-z-]+)\](.*?)\[\/imgdb\]", RegexOptions.IgnoreCase);
			Match m = r.Match(content);
			while (m.Success) 
			{
				content = content.Replace(m.Value, "");
				m = m.NextMatch();
			}
			content = ArticuloFinalizar(content);
			return content;
		}
		
		public static string ArticuloVideoHTML(string content)
		{
			content = ArticuloIniciar(content);
			content = ArticuloImagenDB(content);
			Regex r = new Regex(@"\[youtube\](.*?)\[\/youtube\]", RegexOptions.IgnoreCase);
			Match m = r.Match(content);
			if (m.Success) 
			{
				string url = m.Groups[1].Value;
				content = content.Replace(m.Value, "");
				content = "<div class=\"div-video-spacer\"><iframe src=\"//www.youtube.com/embed/" + url + "?autoplay=1\" " +
					"frameborder=\"0\" allowfullscreen></iframe></div>" + 
					"<div class=\"ssk-group\" style=\"margin-top: 10px\"><a href=\"\" class=\"ssk ssk-facebook\"></a><a href=\"\" class=\"ssk ssk-twitter\"></a>" +
					"<a href=\"\" class=\"ssk ssk-google-plus\"></a><a href=\"\" class=\"ssk ssk-pinterest\"></a><a href=\"\" class=\"ssk ssk-email\"></a></div>" +
					"<div class=\"clear\"></div><span id=\"spnAdVideoChico\"></span>" + content;
			}
			content = ArticuloFinalizar(content);
			return content;
		}
		
		public static string GaleriaHTML(string content)
		{
			string galeria = "";
			Regex r = new Regex(@"\[imgdb=([a-z-]+)\](.*?)\[\/imgdb\]", RegexOptions.IgnoreCase);
			Match m = r.Match(content);
			int matchCount = 0;
			while (m.Success) 
			{
				string estilo = m.Groups[1].Value;
				int id = int.Parse(m.Groups[2].Value);
				string url = "";
				string desc = "";
				ObtenerImagenDB(id, out url, out desc);
				url = "//cdn.pronosticoextendido.net" + url;
				galeria += "<div class=\"galeria-contenido\"><a href=\"" + url + "\" data-lightbox=\"articulo\">";
				galeria += "<img src=\"";
				galeria += url + "\" alt=\"" + desc + "\" /></a><p>" + desc + "</p></div>";
				m = m.NextMatch();
				matchCount++;
			}
			return galeria;
		}
		
		public static void ObtenerImagenDB(int ID, out string URL, out string Descripcion)
		{
			DataRow FRow = TSA.General.Funciones.ConsultarSQL("SELECT * FROM imagenes WHERE idImagenes = " + ID).Rows[0];
			URL = FRow["url"].ToString();
			Descripcion = FRow["descripcion"].ToString();
		}


		public static string NombreCategoria(int ID)
		{
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) 
			return TSA.General.Funciones.ConsultarSQL("SELECT categoria_en FROM categorias WHERE idCategorias = " + 
				ID).Rows[0].ItemArray[0].ToString();
			return TSA.General.Funciones.ConsultarSQL("SELECT categoria FROM categorias WHERE idCategorias = " + 
				ID).Rows[0].ItemArray[0].ToString();
		}
		
		public static DataTable ObtenerArticulos(int Desde, int Cantidad, string Categorias, string Provincias, string Exclusiones)
		{
			string Command = "SELECT p.idPublicaciones, p.url, p.imagenPortada, p.titulo, p.subtitulo, p.fecha, c.idCategorias, c.categoria " +
			"FROM publicaciones p, publicacionescategorias pc, categorias c WHERE " +
			"p.idPublicaciones = pc.idPublicaciones AND pc.idCategorias = c.idCategorias AND ";
			if ((Exclusiones != "0") && (Exclusiones != ""))
				Command += "p.idPublicaciones NOT IN (" + Exclusiones +  ") AND ";
			Command += "DATE(p.fecha) <= DATE(NOW()) AND ((DATE(p.fechaHasta) >= DATE(NOW())) OR (p.fechaHasta IS NULL)) " +
			"GROUP BY idPublicaciones ORDER BY fecha DESC LIMIT " + Desde + "," + Cantidad;
			return TSA.General.Funciones.ConsultarSQL(Command);
		}
		
		// ------------- Obtener la cantidad de artículos para generar la paginación ----------------
		
		public static int ObtenerCantidadArticulos(string Categorias, string Provincias, string Exclusiones)
		{
			string Command = "SELECT COUNT(1) as cantidad " +
			"FROM publicaciones p, publicacionescategorias pc, categorias c WHERE " +
			"p.idPublicaciones = pc.idPublicaciones AND pc.idCategorias = c.idCategorias AND ";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) Command += " p.idIdiomas = 2 AND ";
			else Command += " p.idIdiomas = 1 AND ";
			if ((Categorias != "0") && (Categorias != ""))
				Command += "(c.idCategorias IN (" + Categorias + ") OR c.idCategoriasPadre IN (" + Categorias + ")) AND ";
			if ((Provincias != "0") && (Provincias != ""))
				Command += "EXISTS(SELECT * FROM publicacionesprovincias pp WHERE pp.idPublicaciones = " +
				"p.idPublicaciones AND pp.idProvincias IN (" + Provincias + ")) AND ";
			if ((Exclusiones != "0") && (Exclusiones != ""))
				Command += "p.idPublicaciones NOT IN (" + Exclusiones +  ") AND ";
			Command += "DATE(p.fecha) <= DATE(NOW()) AND ((DATE(p.fechaHasta) >= DATE(NOW())) OR (p.fechaHasta IS NULL)) ";
			return int.Parse(TSA.General.Funciones.ConsultarSQL(Command).Rows[0]["cantidad"].ToString());
		}
		

		

		// ------------- Dibujar el extracto de un artículo en distintos tamaños y diseños ----------------
		
		public static string DibujarExtractoChico(DataRow FRow, int Numero)
		{
			string html = "";
			if (Numero % 4 == 0)
				html += "<div class='div-pub-resumen-chico'><div>";
			else
				html += "<div class='div-pub-resumen-chico'><div>";
			string RutaBase = "/articulo/";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) RutaBase = "/en/article/";
			html += "<a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			html += "<div class='div-pub-resumen-chico-img'>";
			string imagenUrl = FRow["imagenPortada"].ToString();
			string archivo = "C:\\websites\\cdn.pronosticoextendido.net" + imagenUrl;
			int indice = archivo.LastIndexOf(".");
			string thumb = archivo.Substring(0, indice) + "-320." + archivo.Substring(indice + 1);
			if (System.IO.File.Exists(thumb))
			{
				indice = imagenUrl.LastIndexOf(".");
				imagenUrl = imagenUrl.Substring(0, indice) + "-320." + imagenUrl.Substring(indice + 1);
			}
			imagenUrl = "//cdn.pronosticoextendido.net" + imagenUrl;
			html += "<img class='lazy' data-src='" + imagenUrl + "' alt='" + FRow["titulo"] + "' /></div>";
			html += "<h4>" + FRow["titulo"] + "</h4>";
			
			html += "</a></div></div>";
			if (Numero % 4 == 0)
				html += "<div class='clear'></div>";
			return html;
		}
		
		public static string DibujarExtractoMediano(DataRow FRow, int Numero)
		{
			string html = "";
			if (Numero % 2 == 0)
				html += "<div class='div-pub-resumen-medio'><div>";
			else
				html += "<div class='div-pub-resumen-medio'><div>";
			string RutaBase = "/articulo/";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) RutaBase = "/en/article/";
			html += "<a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			html += "<div class='div-pub-resumen-medio-img'>";
			string imagenUrl = "//cdn.pronosticoextendido.net" + FRow["imagenPortada"];
			html += "<img class='lazy' data-src='" + imagenUrl + "' alt='" + FRow["titulo"] + "' /></div>";
			html += "<div class='div-pub-resumen-medio-cont'>";
			html += "<h3>" + FRow["titulo"] + "</h3>";
			
			html += "<span>" + FRow["subtitulo"] + "</span>";
			html += "</div></a></div></div>";
			if (Numero % 2 == 0)
				html += "<div class='clear'></div>";
			return html;
		}
			
		public static string DibujarExtractoGrande(DataRow FRow)
		{
			string html = "";
			html += "<div class='div-pub-resumen-grande'>";
			string RutaBase = "/articulo/";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) RutaBase = "/en/article/";
			html += "<a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			html += "<div class='div-pub-resumen-grande-img'>";
			string imagenUrl = "//cdn.pronosticoextendido.net" + FRow["imagenPortada"];
			html += "<img class='lazy' data-src='" + imagenUrl + "' alt='" + FRow["titulo"] + "' /></div>";
			html += "<div class='div-pub-resumen-grande-cont'>";
			html += "<h3>" + FRow["titulo"] + "</h3>";
			
			html += "<span>" + FRow["subtitulo"] + "</span>";
			html += "</a></div></div>";
			return html;
		}
			
		public static string DibujarExtractoSlider(DataRow FRow, int Numero)
		{
			string html = "";
			html += "<div class='slider-contenido slider" + Numero + "-contenido'>";
			string RutaBase = "/articulo/";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) RutaBase = "/en/article/";
			html += "<a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			string imagenUrl = "//cdn.pronosticoextendido.net" + FRow["imagenPortada"];
			html += "<img src='" + imagenUrl + "' alt='" + FRow["titulo"] + "' /></a>";
			html += "<h3><a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			html += "<span><span>" + FRow["titulo"] + "</span></span></a></h3>";
			html += "</div>";
			return html;
		}
			
		public static string DibujarExtractoTexto(DataRow FRow)
		{
			string html = "";
			html += "<div class='div-pub-resumen-texto'>";
			string RutaBase = "/articulo/";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) RutaBase = "/en/article/";
			html += "<a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			html += "<h4>" + FRow["titulo"] + "</h4></a>";
			html += "<hr /></div>";
			return html;
		}
			
		public static string DibujarExtractoTextoChico(DataRow FRow)
		{
			string html = "";
			html += "<div class='div-pub-resumen-texto'>";
			string RutaBase = "/articulo/";
			if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/en/")) RutaBase = "/en/article/";
			html += "<a href='" + RutaBase + FRow["url"] + "-" + FRow["idPublicaciones"] + "/'>";
			html += "<h4 style='margin: 10px'>" + FRow["titulo"] + "</h4></a>";
			html += "</div>";
			return html;
		}
			
	}

}
