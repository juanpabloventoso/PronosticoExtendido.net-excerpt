using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class _default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
	// Traer las últimas publicaciones de una categoría al azar
	int idCategoria = 9;
	while (idCategoria == 9)
		idCategoria = new Random().Next(3, 12); 
	string Categoria = TSA.Articulo.Funciones.NombreCategoria(idCategoria);
	FTable = TSA.Articulo.Funciones.ObtenerArticulosAzar(0, 6, idCategoria);
	lblNotas1Titulo.Text = "Favoritos de " + Categoria.ToLower();
	lblNotas1.Text = "";
	foreach (DataRow row in FTable.Rows)
		lblNotas1.Text += TSA.Articulo.Funciones.DibujarExtractoLista(row);
    }
}
