<%@ Page language="c#" %>
<!doctype HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Time Tracking Error</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="/gestion/resources/styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="script.js"></script>
	</head>
	<body>
		<form id="Reports" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="989" border="0" style="WIDTH: 989px; HEIGHT: 156px">
				<tr>
					<td class="tab-active" vAlign="top" height="153" style="HEIGHT: 153px">
						<p><img height="15" src="images/spacer.gif" width="15"></p>
						<h4><p align="center">HA OCURRIDO UN ERROR &nbsp;
							<br>
							<br>
							<% if ( Request["errMessage"] == "There is no row at position 0." ) { %>
							El usuario en turno no tiene acceso al Registro de Correspondencia.
							<% } else {%>
							<%= Request["errMessage"] %>
							<% } %>
							<br>
							<br>
							<% if ( Request["errNumber"] == "-2146233033" ) { %>
							Favor de levantar un ticket a Mesa de Servicio.
							<% } else {%>
							Debe comunicarse con el administrador del Sistema a la Extensión 3227.
							<% } %>
							</p></h4>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
