<%@ Page Language="C#" MasterPageFile="~/base.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" Title="Contacto | PronosticoExtendido.net" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cabecera" Runat="Server">
	<meta name="robots" content="noindex, nofollow" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contenido" Runat="Server">

<div style="padding: 10px">
	<h1>Contacto</h1>
    Si querés dejarnos un comentario, sugerencia, duda o propuesta, escribinos utilizando el siguiente formulario. Si tenés alguna duda o problema sobre el uso de este sitio, te recomendamos explorar las preguntas frecuentes.
</div>

<div class="box-login" style="margin-top: 10px">
	<h2>Escribinos</h2>
	Completá los siguientes datos y nos estaremos comunicando con mucho gusto.
	<br />
	<br />
	<span>* Nombre:</span>
	<asp:TextBox ID="txtNombre" runat="server" Width="250" />
	<br />
	<br />
	<span>* Correo electrónico:</span>
	<asp:TextBox ID="txtCorreoElectronico" runat="server" Width="250" placeholder="nombre@host.com" />
	<br />
	<br />
	<span style="text-align: left">* Comentarios:</span>
	<asp:TextBox ID="txtComentarios" runat="server" TextMode="Multiline" Height="150" />
	<br />
	<br />
	<asp:Button ID="btnAceptar" runat="server" Text="Enviar" OnClick="btnAceptar_Click" />
    <br />
    <asp:Label ID="lblEstado" runat="server" style="color: #e30; font-weight: bold; padding: 8px 15px; border: solid 1px #ccc; text-align: left; background: #fdd; width: initial" Visible="false"></asp:Label>
    <asp:Label ID="lblEstadoOK" runat="server" style="color: #070; font-weight: bold; padding: 8px 15px; border: solid 1px #ccc; text-align: left; background: #ded; width: initial" Visible="false" Text="Gracias por tus comentarios. Te escribiremos en breve."></asp:Label>
</div>
<br />
</asp:Content>
