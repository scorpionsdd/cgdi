<%@ Page language="c#" Codebehind="create_report.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.create_report" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>create_report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<SCRIPT ID="clientEventHandlersJS" LANGUAGE="javascript">
		<!--

			function showRightButtonMsg() {
				var sMsg;
  
				sMsg = "Para obtener una copia del reporte en su equipo, se requiere hacer\n" +
					 "clic con el botón derecho del ratón y seleccionar la opción\n" + 
				"'Guardar destino como...'\n\n" + 
				"Gracias."
				alert(sMsg);	
			}

			function showReportInBrowser(sFileName) {	
					window.open(sFileName, 'dummy', "menubar=yes,toolbar=yes,scrollbars=yes,status=yes,location=no");
				return true;
			}
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<table bgColor="khaki" width="100%" border="0" cellspacing="0" cellpadding="4">
			<tr>
				<td class="applicationTitle"><font size="2"><b>La información solicitada está lista.</b></font></td>
			</tr>
			<tr>
				<td class="applicationTitle"><font size="2"><b>¿Qué desea hacer?</b></font></td>
			</tr>
		</table>
		<br>
		<table width="100%" border="0" cellspacing="0" cellpadding="4">
			<tr>
				<td>
					<a href="<%=sFileName%>" onclick="showRightButtonMsg();return false;"><img src="/gestion/images/download.gif" border="0">
					</a>
				</td>
				<td valign="middle">
					<a href="<%=sFileName%>" onclick="showRightButtonMsg();return false;">Si se desea 
						obtener una copia del reporte en su equipo, haga clic sobre esta liga con el 
						botón derecho del ratón y seleccione la opción 'Guardar destino como...' </a>
					<br>
					<br>
				</td>
			</tr>
			<tr>
				<td>
					<a href="" onclick="showReportInBrowser('<%=sFileName%>');return false;"><img src="/gestion/images/view.jpg" border="0">
					</a>
				</td>
				<td valign="middle">
					<a href="" onclick="showReportInBrowser('<%=sFileName%>');return false;">Ver la 
						información solicitada en una página nueva del navegador. </a>
					<br>
					<br>
				</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<a href="" onclick="window.history.back();return false">Cerrar esta ventana y 
						perder la información obtenida. </a>
					<br>
				</td>
			</tr>
		</table>
	</body>
</HTML>
