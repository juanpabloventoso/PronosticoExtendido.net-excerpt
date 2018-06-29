<%@ Page Language="C#" MasterPageFile="~/base.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" Title="Encuesta | PronosticoExtendido.net" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cabecera" Runat="Server">
	<meta GroupName="robots" content="noindex, nofollow" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contenido" Runat="Server">

<asp:Panel ID="pnlIntro" runat="server" style="padding: 10px">
<h1>Encuesta</h1>
¡Gracias por dedicarnos parte de tu tiempo a participar en esta encuesta! Elaboramos estas preguntas para averiguar cómo podemos mejorar nuestra web 
y los productos y servicios que te ofrecemos. 
Valoraremos mucho tus respuestas y esperamos poder aplicar las recomendaciones próximamente.
<br />
<br />
Tardarás sólo unos 5 a 10 minutos en completar el breve cuestionario. Queremos asegurarte que tus respuestas se tratarán de forma totalmente confidencial, 
lo cual significa que los datos de esta encuesta no se compartirán con terceros ni se utilizarán con otros fines que no sean estadísticos. 
</asp:Panel>
<style>
input[type='radio'] { transform: scale(2); }
label { margin-left: 10px; height: 25px; display: inline-block; }
thead {background: #e6e6e6 }
td { padding: 5px }
.td-tit { width: 10%; text-align: center }
.td-opc { height: 40px; text-align: center }
.td-opc label { display: none }
tr:nth-child(even) { background: #f1f1f1 }
textarea { height: 120px !important }
@media all and (max-width: 768px)
{
	thead { display: none }
	tr:nth-child(even) { background: #fff }
	td { display: block; margin-bottom: 20px }
	.td-opc { height: initial; text-align: left }
	.td-opc label { display: inline-block }
}

/*
input[type=radio] {
    display: none;
}
input[type=radio] + label::before {
    content: '';
    display: inline-block;
    border: 1px solid #456;
    border-radius: 50%;
    margin: 0 0.5em 0;
    width: 1.5em;
    height: 1.5em;	
}
input[type=radio]:checked + label::before {
    background-image: url(//cdn.pronosticoextendido.net/imagenes/iconos/check.png);
}
*/
</style>
<div class="box-login" style="margin-top: 10px">
	<div style="width: 50%">
		<div style="width: 80%; border: solid 1px #bce; min-width: 280px; height: 20px; border-radius: 3px;">
		<asp:Literal ID="lblProgreso" runat="server" /></div>
		<br />
	</div>

	<asp:Panel ID="pnlPaso1" runat="server">
		Aproximadamente, ¿cuántas veces visitaste <b>PronosticoExtendido.net</b> en los últimos 30 días?
		<br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta1" ID="pregunta1resp1" Text="<b>2 a 3</b> veces" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta1" ID="pregunta1resp2" Text="<b>4 a 6</b> veces" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta1" ID="pregunta1resp3" Text="<b>7 a 9</b> veces" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta1" ID="pregunta1resp4" Text="<b>10 veces</b> o mas" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta1" ID="pregunta1resp5" Text="<b>es la primera vez</b> que la visito" /><br /><br />
		<asp:HiddenField ID="width" runat="server" />
		<asp:HiddenField ID="height" runat="server" />
		<script type="text/javascript">
			$(document).ready(function() {
				$("#<%= width.ClientID %>").val($(window).width());
				$("#<%= height.ClientID %>").val($(window).height());    
			});
		</script>
	</asp:Panel>
	<asp:Panel ID="pnlPaso2" runat="server">
		De acuerdo a tu experiencia, ¿creés que volverás a visitar <b>PronosticoExtendido.net</b> en el futuro?
		<br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta2" ID="pregunta2resp1" Text="<b>No</b>, no lo creo" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta2" ID="pregunta2resp2" Text="<b>Sí</b>, seguramente" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta2" ID="pregunta2resp3" Text="<b>No lo sé</b>" /><br /><br />
	</asp:Panel>
	<asp:Panel ID="pnlPaso3" runat="server">
		¿Cual es tu nivel de satisfacción general con <b>PronosticoExtendido.net</b>?
		<br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta3" ID="pregunta3resp1" Text="Muy insatisfecho" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta3" ID="pregunta3resp2" Text="Algo insatisfecho" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta3" ID="pregunta3resp3" Text="Ni satisfecho ni insatisfecho" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta3" ID="pregunta3resp4" Text="Algo satisfecho" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta3" ID="pregunta3resp5" Text="Muy satisfecho" /><br /><br />
		<asp:RadioButton runat="server" GroupName="pregunta3" ID="pregunta3resp6" Text="No lo sé" /><br /><br />
	</asp:Panel>
	<asp:Panel ID="pnlPaso4" runat="server">
		¿Cual es tu nivel de satisfacción con los siguientes aspectos de <b>PronosticoExtendido.net</b>?
		<br /><br />
		<table>
		<thead>
		<tr>
		<td>Aspecto</td>
		<td class="td-tit">Muy insatisfecho</td>
		<td class="td-tit">Algo insatisfecho</td>
		<td class="td-tit">Ni satisfecho ni insatisfecho</td>
		<td class="td-tit">Algo satisfecho</td>
		<td class="td-tit">Muy satisfecho</td>
		<td class="td-tit">No lo sé</td>
		</tr>
		</thead>
		<tbody>
		<tr>
		<td><b>Confiabilidad de los pronósticos</b> (es decir, que se cumplen y anticipan con buen grado de certeza)</td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta4" ID="pregunta4resp1" Text="Muy insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta4" ID="pregunta4resp2" Text="Algo insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta4" ID="pregunta4resp3" Text="Ni satisfecho ni insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta4" ID="pregunta4resp4" Text="Algo satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta4" ID="pregunta4resp5" Text="Muy satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta4" ID="pregunta4resp6" Text="No lo sé" /><br /></td>
		</tr>
		<tr>
		<td><b>RadSat y mapas interactivos</b> (utilidad, facilidad de uso y confiabilidad)</td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta5" ID="pregunta5resp1" Text="Muy insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta5" ID="pregunta5resp2" Text="Algo insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta5" ID="pregunta5resp3" Text="Ni satisfecho ni insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta5" ID="pregunta5resp4" Text="Algo satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta5" ID="pregunta5resp5" Text="Muy satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta5" ID="pregunta5resp6" Text="No lo sé" /><br /></td>
		</tr>
		<tr>
		<td><b>Alertas, avisos e informes</b> (claridad en los contenidos y confiabilidad del pronóstico)</td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta6" ID="pregunta6resp1" Text="Muy insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta6" ID="pregunta6resp2" Text="Algo insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta6" ID="pregunta6resp3" Text="Ni satisfecho ni insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta6" ID="pregunta6resp4" Text="Algo satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta6" ID="pregunta6resp5" Text="Muy satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta6" ID="pregunta6resp6" Text="No lo sé" /><br /></td>
		</tr>
		<tr>
		<td><b>Facilidad de uso</b> (del sitio web en general, secciones distinguibles, acceso móvil)</td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta7" ID="pregunta7resp1" Text="Muy insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta7" ID="pregunta7resp2" Text="Algo insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta7" ID="pregunta7resp3" Text="Ni satisfecho ni insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta7" ID="pregunta7resp4" Text="Algo satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta7" ID="pregunta7resp5" Text="Muy satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta7" ID="pregunta7resp6" Text="No lo sé" /><br /></td>
		</tr>
		<tr>
		<td><b>Diseño atractivo</b> (gráficos, íconos, combinación de colores, diseño de los mapas)</td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta8" ID="pregunta8resp1" Text="Muy insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta8" ID="pregunta8resp2" Text="Algo insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta8" ID="pregunta8resp3" Text="Ni satisfecho ni insatisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta8" ID="pregunta8resp4" Text="Algo satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta8" ID="pregunta8resp5" Text="Muy satisfecho" /></td>
		<td class="td-opc"><asp:RadioButton runat="server" GroupName="pregunta8" ID="pregunta8resp6" Text="No lo sé" /><br /></td>
		</tr>
		</tbody>
		</table>
	</asp:Panel>
	<asp:Panel ID="pnlPaso5" runat="server">
		Comentanos, ¿qué es lo que <b><u>mas</u></b> te gusta de <b>PronosticoExtendido.net</b> en comparación a otros sitios de meteorología y pronóstico del tiempo?
		<br /><br />
		<asp:TextBox runat="server" ID="pregunta9resp1" TextMode="MultiLine"></asp:TextBox>
	</asp:Panel>
	<asp:Panel ID="pnlPaso6" runat="server">
		Comentanos, ¿qué es lo que <b><u>menos</u></b> te gusta de <b>PronosticoExtendido.net</b> en comparación a otros sitios de meteorología y pronóstico del tiempo?
		<br /><br />
		<asp:TextBox runat="server" ID="pregunta10resp1" TextMode="MultiLine"></asp:TextBox>
	</asp:Panel>
	<asp:Panel ID="pnlPaso7" runat="server">
		Comentanos, ¿qué sugerencias, correcciones o mejoras propondrías para que realicemos en <b>PronosticoExtendido.net</b>?
		<br /><br />
		<asp:TextBox runat="server" ID="pregunta11resp1" TextMode="MultiLine"></asp:TextBox>
	</asp:Panel>
	<asp:Panel ID="pnlPaso8" runat="server">
		<b>Finalmente,</b> ¿querés dejarnos tu nombre y una dirección de correo electrónico para que te contactemos en caso de que tengamos en cuenta y apliquemos alguna de tus sugerencias?
		<br /><br />
		Nombre (opcional):<br />
		<asp:TextBox runat="server" ID="txtNombre"></asp:TextBox>
		<br /><br />
		Correo electrónico (opcional):<br />
		<asp:TextBox runat="server" ID="txtCorreo"></asp:TextBox>
	</asp:Panel>
	<asp:Panel ID="pnlGracias" runat="server">
		<h2>¡Muchas gracias!</h2>
		Gracias por haber respondido nuestra encuesta. Estaremos analizando con atención los resultados para mejorar nuestra web y la previsión meteorológica en Argentina y la región.
		<br /><br />
	</asp:Panel>
	
	<div style="border-top: solid 1px #e1e1e1; text-align: right">
	<br />
	<asp:Button ID="btnAnterior" runat="server" Text="Anterior" OnClick="btnAnterior_Click" />
	<asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" />
	</div>
    <br />
    <asp:Label ID="lblEstado" runat="server" style="color: #e30; font-weight: bold; padding: 8px 15px; border: solid 1px #ccc; text-align: left; background: #fdd; width: initial" Visible="false"></asp:Label>
    <asp:Label ID="lblEstadoOK" runat="server" style="color: #070; font-weight: bold; padding: 8px 15px; border: solid 1px #ccc; text-align: left; background: #ded; width: initial" Visible="false" Text="Gracias por tus comentarios. Te escribiremos en breve."></asp:Label>
</div>
<br />
</asp:Content>
