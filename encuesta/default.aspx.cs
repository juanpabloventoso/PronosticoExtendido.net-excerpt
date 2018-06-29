using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class _default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (Page.IsPostBack) return;
		ConfigurarPaso(1);
    }
	
    protected void formError(string estado)
    {
		if (estado == "")
			lblEstado.Visible = false;
		else
		{
			lblEstado.Text = estado;
			lblEstado.Visible = true;
		}
    }

	protected void ConfigurarPaso(int Paso)
	{
		Session["EncuestaPaso"] = Paso;
		pnlIntro.Visible = false;
		pnlPaso1.Visible = false;
		pnlPaso2.Visible = false;
		pnlPaso3.Visible = false;
		pnlPaso4.Visible = false;
		pnlPaso5.Visible = false;
		pnlPaso6.Visible = false;
		pnlPaso7.Visible = false;
		pnlPaso8.Visible = false;
		pnlGracias.Visible = false;
		if (Paso == 1) pnlIntro.Visible = true;
		if (Paso == 1) pnlPaso1.Visible = true;
		if (Paso == 2) pnlPaso2.Visible = true;
		if (Paso == 3) pnlPaso3.Visible = true;
		if (Paso == 4) pnlPaso4.Visible = true;
		if (Paso == 5) pnlPaso5.Visible = true;
		if (Paso == 6) pnlPaso6.Visible = true;
		if (Paso == 7) pnlPaso7.Visible = true;
		if (Session["idUsuarios"] == null)
		{
			if (Paso == 8) pnlPaso8.Visible = true;
			if (Paso == 9) pnlGracias.Visible = true;
			btnAnterior.Visible = (Paso > 1) && (Paso != 9);
			if (Paso == 8) btnSiguiente.Text = "Finalizar"; else
			if (Paso == 9) btnSiguiente.Text = "Volver"; else
			btnSiguiente.Text = "Siguiente";
			if (Paso == 9)
				TSA.General.Funciones.EjecutarSQL("UPDATE encuestas SET finalizada = 1 WHERE idEncuestas = " + Session["idEncuesta"].ToString());
			lblProgreso.Text = "<div style='height: 100%; width: " + ((Paso - 1) * 100 / 8).ToString() + "%; background: #def'></div>";
		}
		else
		{
			if (Paso == 8) pnlGracias.Visible = true;
			btnAnterior.Visible = (Paso > 1) && (Paso != 8);
			if (Paso == 7) btnSiguiente.Text = "Finalizar"; else
			if (Paso == 8) btnSiguiente.Text = "Volver"; else
			btnSiguiente.Text = "Siguiente";
			if (Paso == 8)
				TSA.General.Funciones.EjecutarSQL("UPDATE encuestas SET finalizada = 1 WHERE idEncuestas = " + Session["idEncuesta"].ToString());
			lblProgreso.Text = "<div style='height: 100%; width: " + ((Paso - 1) * 100 / 7).ToString() + "%; background: #def'></div>";
		}
	}
		
	protected string GetUserIP()
	{
		string ip = "";
		if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
			ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
		else if (Request.UserHostAddress.Length != 0)
			ip = Request.UserHostAddress;
		return ip;
	}	
	
	protected void VerificarEncuestaCreada()
	{
		if (Session["idEncuesta"] != null) return;
		if (Session["idUsuarios"] != null)
		{
			int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestas WHERE idUsuarios = " + Session["idUsuarios"].ToString()).Rows.Count;
			if (cant > 0)
				Session["idEncuesta"] = TSA.General.Funciones.ConsultarSQL("SELECT idEncuestas FROM encuestas WHERE idUsuarios = " + Session["idUsuarios"].ToString()).Rows[0].ItemArray[0].ToString();
		}
		if (Session["idEncuesta"] == null)
		{
			if (Session["idUsuarios"] != null)
				TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestas (idUsuarios, IP, idLocalidades, fechaHora, navegador, otrosdatos) VALUES " + "(" + 
					Session["idUsuarios"].ToString() + ", '" + GetUserIP() + "', " + Session["usuarioLocalidad1"].ToString() + ", NOW(), '" + Request.UserAgent + "', '" +
					width.Value + " - " + height.Value + "');");
			else
				TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestas (IP, idLocalidades, fechaHora, navegador, otrosdatos) VALUES " + "(" + 
					"'" + GetUserIP() + "', " + Session["usuarioLocalidad1"].ToString() + ", NOW(), '" + Request.UserAgent + "', '" +
					width.Value + " - " + height.Value + "');");
			Session["idEncuesta"] = TSA.General.Funciones.ConsultarSQL("SELECT MAX(idEncuestas) FROM encuestas").Rows[0].ItemArray[0].ToString();
		}
	}
	
	protected bool ValidarPaso1()
	{
		string opcion = "1";
		if (pregunta1resp1.Checked) opcion = "2";
		if (pregunta1resp2.Checked) opcion = "4";
		if (pregunta1resp3.Checked) opcion = "7";
		if (pregunta1resp4.Checked) opcion = "10";
		int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 1").Rows.Count;
		if (cant > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 1;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 1, '" + opcion + "');");
		formError("");
		return true;
	}
	
	protected bool ValidarPaso2()
	{
		string opcion = "-1";
		if (pregunta2resp1.Checked) opcion = "0";
		if (pregunta2resp2.Checked) opcion = "1";
		int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 2").Rows.Count;
		if (cant > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 2;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 2, '" + opcion + "');");
		formError("");
		return true;
	}
	
	protected bool ValidarPaso3()
	{
		string opcion = "-1";
		if (pregunta3resp1.Checked) opcion = "0";
		if (pregunta3resp2.Checked) opcion = "1";
		if (pregunta3resp3.Checked) opcion = "2";
		if (pregunta3resp4.Checked) opcion = "3";
		if (pregunta3resp5.Checked) opcion = "4";
		int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 3").Rows.Count;
		if (cant > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 3;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 3, '" + opcion + "');");
		formError("");
		return true;
	}
	
	protected bool ValidarPaso4()
	{
		string opcion4 = "-1";
		if (pregunta4resp1.Checked) opcion4 = "0";
		if (pregunta4resp2.Checked) opcion4 = "1";
		if (pregunta4resp3.Checked) opcion4 = "2";
		if (pregunta4resp4.Checked) opcion4 = "3";
		if (pregunta4resp5.Checked) opcion4 = "4";
		int cant4 = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 4").Rows.Count;
		if (cant4 > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion4 + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 4;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 4, '" + opcion4 + "');");
		string opcion5 = "-1";
		if (pregunta5resp1.Checked) opcion5 = "0";
		if (pregunta5resp2.Checked) opcion5 = "1";
		if (pregunta5resp3.Checked) opcion5 = "2";
		if (pregunta5resp4.Checked) opcion5 = "3";
		if (pregunta5resp5.Checked) opcion5 = "4";
		int cant5 = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 5").Rows.Count;
		if (cant5 > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion5 + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 5;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 5, '" + opcion5 + "');");
		string opcion6 = "-1";
		if (pregunta6resp1.Checked) opcion6 = "0";
		if (pregunta6resp2.Checked) opcion6 = "1";
		if (pregunta6resp3.Checked) opcion6 = "2";
		if (pregunta6resp4.Checked) opcion6 = "3";
		if (pregunta6resp5.Checked) opcion6 = "4";
		int cant6 = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 6").Rows.Count;
		if (cant6 > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion6 + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 6;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 6, '" + opcion6 + "');");
		string opcion7 = "-1";
		if (pregunta7resp1.Checked) opcion7 = "0";
		if (pregunta7resp2.Checked) opcion7 = "1";
		if (pregunta7resp3.Checked) opcion7 = "2";
		if (pregunta7resp4.Checked) opcion7 = "3";
		if (pregunta7resp5.Checked) opcion7 = "4";
		int cant7 = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 7").Rows.Count;
		if (cant7 > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion7 + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 7;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 7, '" + opcion7 + "');");
		string opcion8 = "-1";
		if (pregunta8resp1.Checked) opcion8 = "0";
		if (pregunta8resp2.Checked) opcion8 = "1";
		if (pregunta8resp3.Checked) opcion8 = "2";
		if (pregunta8resp4.Checked) opcion8 = "3";
		if (pregunta8resp5.Checked) opcion8 = "4";
		int cant8 = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 8").Rows.Count;
		if (cant8 > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion8 + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 8;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 8, '" + opcion8 + "');");
		formError("");
		return true;
	}

	protected bool ValidarPaso5()
	{
		string opcion = pregunta9resp1.Text.Trim();
		int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 9").Rows.Count;
		if (cant > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 9;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 9, '" + opcion + "');");
		formError("");
		return true;
	}
	
	protected bool ValidarPaso6()
	{
		string opcion = pregunta10resp1.Text.Trim();
		int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 10").Rows.Count;
		if (cant > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 10;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 10, '" + opcion + "');");
		formError("");
		return true;
	}
	
	protected bool ValidarPaso7()
	{
		string opcion = pregunta11resp1.Text.Trim();
		int cant = TSA.General.Funciones.ConsultarSQL("SELECT * FROM encuestasrespuestas WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 11").Rows.Count;
		if (cant > 0)
			TSA.General.Funciones.EjecutarSQL("UPDATE encuestasrespuestas SET Respuesta1 = '" + opcion + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString() + " AND idEncuestasPreguntas = 11;");
		else
			TSA.General.Funciones.EjecutarSQL("INSERT INTO encuestasrespuestas (idEncuestas, idEncuestasPreguntas, Respuesta1) VALUES (" + Session["idEncuesta"].ToString() + ", 11, '" + opcion + "');");
		formError("");
		return true;
	}
	
	protected bool ValidarPaso8()
	{
		string opcion1 = txtNombre.Text.Trim();
		string opcion2 = txtCorreo.Text.Trim().ToLower();
		TSA.General.Funciones.EjecutarSQL("UPDATE encuestas SET nombre = '" + opcion1 + "', email = '" + opcion2 + "' WHERE idEncuestas = " + Session["idEncuesta"].ToString());
		formError("");
		return true;
	}
	
	protected bool ValidarPaso(int Paso)
	{
		VerificarEncuestaCreada();
		if (Paso == 1) return ValidarPaso1();
		if (Paso == 2) return ValidarPaso2();
		if (Paso == 3) return ValidarPaso3();
		if (Paso == 4) return ValidarPaso4();
		if (Paso == 5) return ValidarPaso5();
		if (Paso == 6) return ValidarPaso6();
		if (Paso == 7) return ValidarPaso7();
		if (Session["idUsuarios"] != null)
			if (Paso == 8) Response.Redirect("/");
		else
		{
			if (Paso == 8) return ValidarPaso8();
			if (Paso == 9) Response.Redirect("/");
		}
		return true;
	}
	
	protected void btnAnterior_Click(object sender, EventArgs e)
	{
		int Paso = int.Parse(Session["EncuestaPaso"].ToString());
		if (Paso < 1) return;
		if (!ValidarPaso(Paso)) return;
		Paso--;
		ConfigurarPaso(Paso);
	}
		
	protected void btnSiguiente_Click(object sender, EventArgs e)
	{
		int Paso = int.Parse(Session["EncuestaPaso"].ToString());
		if (((Session["idUsuarios"] == null) && (Paso > 9)) ||
		   ((Session["idUsuarios"] != null) && (Paso > 8))) return;
		if (!ValidarPaso(Paso)) return;
		Paso++;
		ConfigurarPaso(Paso);
	}
		
}
