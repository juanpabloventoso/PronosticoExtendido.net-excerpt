using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TSA.Social
{

	public static class Funciones
	{
	
		public static string DibujarComentar()
		{
			return 	"<div class=\"fb-comments\" data-href=\"" + 
				TSA.General.Funciones.CanonicoActual().Replace("https", "http") + "\" data-width=\"100%\"" +
				"data-numposts=\"5\" data-colorscheme=\"light\"></div>";

		}
		
	}

}
