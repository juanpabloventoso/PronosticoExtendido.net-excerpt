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

namespace TSA.Imagen
{

	public static class Funciones
	{
		
		public static void CrearMiniatura(string Archivo)
		{
			using (System.Drawing.Image img = System.Drawing.Image.FromFile(Archivo))
			{
				float widthRatio = (float)img.Width / (float)320;
				// Resize to the greatest ratio
				int newWidth = Convert.ToInt32(Math.Floor((float)img.Width / widthRatio));
				int newHeight = Convert.ToInt32(Math.Floor((float)img.Height / widthRatio));
				using (System.Drawing.Image thumb = img.GetThumbnailImage(newWidth, newHeight, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailImageAbortCallback), IntPtr.Zero))
				{
					int indice = Archivo.LastIndexOf(".");
					string ArchivoThumb = Archivo.Substring(0, indice) + "-320." + Archivo.Substring(indice + 1);
					thumb.Save(ArchivoThumb, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
			}
		}

		public static bool ThumbnailImageAbortCallback()
		{
			return true;
		}				
			
	}

}
