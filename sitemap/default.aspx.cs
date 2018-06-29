using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class _Default: System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int cantidadTotal = int.Parse(TSA.General.Funciones.ConsultarSQL(
			"SELECT COUNT(DISTINCT(CONCAT(l.localidad, pa.pais))) FROM localidades l, paises pa WHERE l.idPaises = pa.idPaises AND l.codigoPron1 IS NOT NULL").Rows[0].ItemArray[0].ToString());
		lblSitemap.Text = "";
		for (int i = 0; i < cantidadTotal / 1000; i++)
			lblSitemap.Text += "<sitemap><loc>https://www.pronosticoextendido.net/sitemap/pronosticos/?pagina=" + (i + 1).ToString() + "</loc></sitemap>";
	}

}