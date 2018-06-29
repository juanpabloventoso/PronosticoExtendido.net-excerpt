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
		string Command = "SELECT p.idPublicaciones, p.url, p.imagenPortada, p.titulo, p.subtitulo, p.fecha, CONCAT(u.nombres, ' ', u.apellido) as autor " +
		"FROM publicaciones p INNER JOIN usuarios u ON (p.idUsuarios = u.idUsuarios) WHERE " +
		"p.idIdiomas = 1 AND " +
		"DATE(p.fecha) <= DATE(NOW()) AND ((DATE(p.fechaHasta) >= DATE(NOW())) OR (p.fechaHasta IS NULL)) " +
		"GROUP BY idPublicaciones ORDER BY fecha DESC";
		RepeaterRSS.DataSource = TSA.General.Funciones.ConsultarSQL(Command);
		RepeaterRSS.DataBind();
	}

	protected string RemoveIllegalCharacters(object input)
	{
		string data = input.ToString();
		data = data.Replace("&", "&amp;");
		data = data.Replace("\"", "&quot;");
		data = data.Replace("'", "&apos;");
		data = data.Replace("<", "&lt;");
		data = data.Replace(">", "&gt;");
		return data;
	}
}