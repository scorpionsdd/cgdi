<%@ Page language="c#" CodeBehind="Results.aspx.cs" AutoEventWireup="false" Inherits="WebApplication5.Results" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
	<HEAD>
		<title>Results</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="/gestion/hello.js" type=text/javascript></script>
	</HEAD>

	<body>
		<form runat="server" ID="Form1">
			<table cellspacing="1" cellpadding="1" border="0">
				<tr>
					<td bgcolor="navy" colspan="2">
						<font color="white" size="3">Results&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<% = System.DateTime.Now.ToString() %></font></td>
				</tr>
				<% string fieldValue;
				foreach ( string fieldName in Request.QueryString ) {
				if ( Request.QueryString[fieldName] != "" ) fieldValue = Request.QueryString[fieldName] ;
				else fieldValue = "&nbsp;"; 
				%>
				<tr>
					<td align="right" bgcolor="lightskyblue"><b><%= fieldName %></b></td>
					<td bgcolor="lightskyblue"><%= fieldValue %></td>
				</tr>
				<% } %>
			</table>
			<P>
				<a id="myHello" onclick="hello();">Empieza</a></P>
		</form>
	</body>
</HTML>
