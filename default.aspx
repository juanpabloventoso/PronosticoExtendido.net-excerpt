<%@ Page Language="C#" MasterPageFile="~/vertical.master" EnableViewState="false" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" Title="PronosticoExtendido.net - Pronóstico extendido del tiempo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cabecera" Runat="Server">
    <meta name="msvalidate.01" content="CF50E58A6999191DD84E5DA9A3EAE0B7" />
	<meta property="og:title" content="PronosticoExtendido.net" />
	<meta itemprop="name" content="PronosticoExtendido.net - Pronóstico extendido del tiempo" />
	<meta name="description" content="El portal meteorológico mas completo de Argentina, Uruguay, Paraguay, Chile y el sur de Brasil. Pronósticos, alertas, radares y toda la información del tiempo." />
	<meta property="og:description" content="El portal meteorológico mas completo de Argentina, Uruguay, Paraguay, Chile y el sur de Brasil." />
	<meta itemprop="description" content="El portal meteorológico mas completo de Argentina, Uruguay, Paraguay, Chile y el sur de Brasil. Pronósticos, alertas, radares y toda la información del tiempo." />
	<meta name="keywords" content="pronostico extendido, noticias del clima, alerta meteorologico, radar meteorologico, clima argentina, clima uruguay, clima paraguay, clima brasil, clima chile" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="seccion" Runat="Server">
	<div class="div-home">
	<div class="movil-mostrar">
		<span id="spnAhora"></span>
		<br />
		<span id="spnAdChico2"></span>
	</div>
	<div class="div-slider-spacer">
	<div class="div-slider">
		<div id="slider1" class="slider-wrap">
			<asp:Literal ID="lblSlider" runat="server" />
			<div id="slider1-paginacion" class="slider-paginacion"></div>
			<div id="galeriaIzq"></div><div id="galeriaDer"></div>
		</div>
	</div>	
	<div class="clear"></div>
	</div>
	</div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contenido" Runat="Server">

<div class="div-home-left">	

<asp:Literal ID="lblNoticias1" runat="server" />

<div class="clear"></div>


<div class="div-contenido-holder ocultar-movil">
<div class="div-contenido">
<h2 class="titulo-seccion"><a href="/radsat/">RadSat&reg; - Mapa interactivo <span>»</span></a></h2>
	<div id="div-mapa-holder">
		<div id="div-mapa-inset" class="div-mapa-inset"></div>
	</div>
</div>
</div>
		
<br />

<div class="div-contenido-holder ocultar-movil">	
<div class="div-contenido">
	<h2 class="titulo-seccion"><a href="/fotos/">Fotos <span>»</span></a></h2>
	<asp:Literal ID="lblFotos" runat="server" />
	<div class="clear"></div>
</div>	
</div>	


<div class="div-contenido-holder">	
<div class="div-contenido">
	<h2 class="titulo-seccion"><a href="/videos/">Videos <span>»</span></a></h2>
	<asp:Literal ID="lblVideos" runat="server" />
	<div class="clear"></div>
</div>	
</div>	
	


<div class="div-contenido-holder pnl-prono-mapa">	
<div class="div-contenido">
	<h2 class="titulo-seccion"><a href="/prevencion/">Prevención y acción <span>»</span></a></h2>
	<asp:Literal ID="lblPrevencion" runat="server" />
	<div class="clear"></div>
</div>	
</div>

	
</div>
	
<div class="div-home-right">

	<span id="spnSidebarAdGrande"></span>

	<div class="div-contenido-holder">
	<div class="div-contenido">
		<h2 class="h-app-sidebar-t1"><a href="/apps/">Anticipá el mal tiempo</a></h2>
		<h3 class="h-app-sidebar-t2"><a href="/apps/">Estés donde estés</a></h3>
		<a href="/apps/"><img class="lazy" style="width: 100%" data-src="//cdn.pronosticoextendido.net/imagenes/banners/app-2016-336.jpg" alt="App para Android" /></a>
	</div>
	</div>
	
	<div class="div-contenido-holder">
	<div class="div-contenido">
		<h2 class="titulo-seccion"><asp:Literal ID="lblNotas1Titulo" runat="server" /></h2>
		<asp:Literal ID="lblNotas1" runat="server" />
		<div class="clear"></div>
	</div>
	</div>
	
	<span id="spnSidebarAdGrande2"></span>
	
	<div class="div-contenido-holder">
	<div class="div-contenido">
	<h3>Argentina hoy</h3>
	<a href="/pronosticos/paises/argentina/">
		<img class="lazy" data-src="http://mapas.meteored.com.ar/America%20Sur_Argentina_1.map_1.jpg" alt="Pronóstico para Argentina" style="width: 100%" />
	</a>
	<p style="text-align: right; color: #888">Fuente: <a href="http://www.meteored.com.ar/" rel="nofollow" target="_blank">MeteoRed</a></p>
	</div>
	</div>
	
	<span id="spnSidebarAdGrande3"></span>
	
	<div class="div-contenido-holder">
	<div class="div-contenido">
	<div style="background: #f1f1f1; margin-bottom: 5px;">
		<div style="float: left; height: 48px; padding: 8px;">
			<img class="img-autor" src="https://lh4.googleusercontent.com/-AYTITfRFQ9Y/VTqeAYtqneI/AAAAAAAAA90/UsPeEMHq7JY/s384-no/perfil11.jpg" alt="Juan Pablo Ventoso" />
		</div>
		<div style="float: left; margin-top: 8px;">
			<a href="https://plus.google.com/118286517153137834447?rel=author" rel="author" title="Juan Pablo Ventoso">Juan Pablo Ventoso</a>
			<br /><b>Creador y administrador</b>
		</div>
		<div class="clear"></div>
	</div>
	Soy Argentino y aficionado a la meteorología desde hace mas de 20 años. Me intereso particularmente en la prevención frente al mal tiempo.
	<br /><br />
	PronosticoExtendido.net busca anticipar y prevenir los eventos de tiempo severo en Argentina y países de la región, incorporando además la información de alertas y avisos oficiales.
	<br /><br />
	Los pronósticos informados en esta web son de carácter no oficial, es decir, no mantiene relación con el Servicio Meteorológico Nacional de Argentina ú otras agencias extranjeras, a excepción de los alertas y avisos vigentes.
	</div>
	</div>
	
</div>

<div class="clear"></div>

    <asp:SqlDataSource ID="dsConsulta" runat="server" 
        ConnectionString="<%$ ConnectionStrings:tsa_cam %>" 
        ProviderName="<%$ ConnectionStrings:tsa_cam.ProviderName %>"></asp:SqlDataSource>



<script type="text/javascript" src="//cdn.pronosticoextendido.net/js/jquery.mobile.custom.min.js"></script>
<script type="text/javascript" src="//cdn.pronosticoextendido.net/js/slider.js?v=4"></script>

<script type="text/javascript">
featuredcontentslider.init({
	id: "slider1", 
	contentsource: ["inline", ""], 
	toc: "#increment", 
	nextprev: ["<", ">"], 
	revealtype: "click",
	enablefade: [false, 0.2], 
	autorotate: [false, 0], 
	onChange:function (previndex, curindex) { 
	}
});
</script>

<asp:Literal ID="lblSliderJS" runat="server" />

<script type="text/javascript">
PE.ads.adSense.crearBannerResponsive("spnAdGrande", "5100684732", "superior", true);
if (PE.ventana.esGrande) {
	PE.ads.adSense.crearBanner("spnSidebarAdGrande", "4133940606", 300, 600, "superior", true);
	PE.ads.adSense.crearBanner("spnSidebarAdGrande2", "3755863750", 300, 250, "inferior", true);
	PE.ads.adSense.crearBanner("spnSidebarAdGrande3", "3755863750", 300, 250, "inferior", true);
}	
function pe_alCargarAsync() {
	if (PE.ventana.esGrande) PE.crearRadSat();
}
function pe_RadSatCargado() {
	PE.RadSat.iniciar("div-mapa-inset", 5); 
	PE.RadSat.mapa.activarZoom(false);
}
if (PE.ventana.esChica) {
	$.ajax({
		url: "/servicios/obtener-ahora.aspx?ancho=" + PE.ventana.ancho,
		dataType: "html",
		success: function(data){
			$("#spnAhora").html(data);
		}
	});
	PE.ads.adSense.crearBannerResponsive("spnAdChico2", "5100684732", "superior", true);
}
</script>

</asp:Content>
