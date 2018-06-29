<%@ Page Language="C#" ContentType="text/xml" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>
<asp:Repeater ID="RepeaterRSS" runat="server">
	<HeaderTemplate>
<rss version="2.0" xmlns:content="http://purl.org/rss/1.0/modules/content/" xmlns:atom="http://www.w3.org/2005/Atom" xmlns:slash="http://purl.org/rss/1.0/modules/slash/" xmlns:media="http://search.yahoo.com/mrss/">
	<channel>
		<title>PronosticoExtendido.net</title>
		<link>https://www.pronosticoextendido.net</link>
		<description>El portal del tiempo en Argentina</description>
		<lastBuildDate><%# String.Format("{0:R}", DateTime.Now)%></lastBuildDate>
	</HeaderTemplate>
	<ItemTemplate>
		<item>
			<title><%# RemoveIllegalCharacters(DataBinder.Eval(Container.DataItem, "titulo")) %></title>
			<link>https://www.pronosticoextendido.net/articulo/<%# DataBinder.Eval(Container.DataItem, "url") %>-<%# DataBinder.Eval(Container.DataItem, "idPublicaciones") %>/</link>
			<author><%# RemoveIllegalCharacters(DataBinder.Eval(Container.DataItem, "autor"))%></author>
			<pubDate><%# String.Format("{0:R}", DataBinder.Eval(Container.DataItem, "fecha"))%></pubDate>
			<description><![CDATA[<%# RemoveIllegalCharacters(DataBinder.Eval(Container.DataItem, "subtitulo"))%>]]></description>
			<enclosure url="https://cdn.pronosticoextendido.net<%# DataBinder.Eval(Container.DataItem, "imagenPortada").ToString().Replace(".jpg", "-320.jpg") %>" width="320" height="250" type="image/jpeg" />
			<media:content url="https://cdn.pronosticoextendido.net<%# DataBinder.Eval(Container.DataItem, "imagenPortada").ToString().Replace(".jpg", "-320.jpg") %>" width="320" height="250" type="image/jpeg" />
		</item>
	</ItemTemplate>
	<FooterTemplate>
	</channel>
</rss>  
	</FooterTemplate>
</asp:Repeater>