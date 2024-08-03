<%@ Page language="c#" Codebehind="alternate_reply.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.alternate_reply" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Editor Respuestas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="/gestion/resources/applications.css">
		<LINK rel="stylesheet" type="text/css" href="/gestion/css/worksheet.css">
		<script language="javascript">
		
			function viewAttach(sFile){
				window.open(sFile,'_blank');
				return false;
			}
			
			//
			//
			//
			function ValidaRespuesta()
			{
				var lDropdownList = document.getElementById('<%=ddlTipoRespuesta.ClientID %>');
				var SelectedIndex = lDropdownList.selectedIndex;
				if (SelectedIndex == "-1")
				{
					alert("Debe seleccionar el tipo de respuesta");
					return false;
				}
				return true;
			}

			function CloseWindow(){
			
				document.all.clicker.click();
				window.close();
				return false;
			}
		</script>
	</HEAD>
	<body onload="<%= gsClick%>">
		<form id="EditReplyAlternate" method="post" runat="server">
			<TABLE class="standardPageTable" border="1" width="100%">
				<TR>
					<TD style="HEIGHT: 29px" class="applicationTitle" noWrap>Editor de Respuestas</TD>
					<td style="HEIGHT: 29px" class="applicationTitle" colSpan="2" align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<IMG style="CURSOR: hand" id="clicker" onclick="window.close();" src="/gestion/images/close_red.gif">
					</td>
				</TR>
			</TABLE>
			<table class="fullScrollMenu" border="12" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width="8"><IMG src="images/spacer.gif" width="8" height="8"></td>
					<td style="WIDTH: 197px" vAlign="top" width="197">
						<!-- Left Panel -->
						<table border="0" cellSpacing="0" cellPadding="0" width="206">
							<tr>
								<td style="WIDTH: 257px" vAlign="top">
									<table style="WIDTH: 215px; HEIGHT: 47px" border="0" cellSpacing="12" cellPadding="0" width="215">
										<tr>
											<td><span class="header-gray">No. Volante</span></td>
											<td><asp:label id="lblVolante" runat="server"></asp:label></td>
										</tr>
										<tr>
											<td><span class="header-gray">Referencia:</span></td>
											<TD><asp:label id="lblReference" runat="server"></asp:label></TD>
										</tr>
										<tr>
											<td><span class="header-gray">Fecha del Documento:</span></td>
											<TD><asp:label id="lblDocumentDate" runat="server"></asp:label></TD>
										</tr>
										<tr>
											<td><span class="header-gray">Fecha de Registro:</span></td>
											<TD><asp:label id="lblRegistryDate" runat="server"></asp:label></TD>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 257px" height="11" vAlign="top"><IMG src="/gestion/images/spacer.gif" width="1" height="1"></td>
							</tr>
							<tr>
								<td style="WIDTH: 257px" vAlign="top">
									<table style="WIDTH: 215px" border="0" cellSpacing="12" cellPadding="0" width="215">
										<TBODY>
											<tr vAlign="top">
												<td class="header-gray">Remitente<br>
													<asp:textbox id="txtSenderName" runat="server" CssClass="standard-text" Width="200px" ReadOnly="True"
														Enabled="False"></asp:textbox></td>
											</tr>
											<tr vAlign="top">
												<td class="header-gray">Area<br>
													<asp:textbox id="txtSenderArea" runat="server" CssClass="standard-text" Width="200px" Enabled="False"></asp:textbox></td>
											</tr>
											<tr vAlign="top">
												<td class="header-gray">Destinatario<br>
													<asp:textbox id="txtAddresseName" runat="server" CssClass="standard-text" Width="200px" ReadOnly="True"
														Enabled="False"></asp:textbox></td>
											</tr>
											<tr vAlign="top">
												<td class="header-gray">Area<br>
													<asp:textbox id="txtAddresseArea" runat="server" CssClass="standard-text" Width="200px" Enabled="False"></asp:textbox></td>
											</tr>
										</TBODY>
									</table>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 257px" height="11" vAlign="top"><IMG src="images/spacer.gif" width="1" height="1"></td>
							</tr>
						</table>
						<!-- End Left Panel --></td>
					<td width="11"><IMG src="/gestion/images/spacer.gif" width="11" height="11"></td>
					<td vAlign="top">
						<!-- Right Panel -->
						<table style="WIDTH: 658px; HEIGHT: 428px" border="0" cellSpacing="11" cellPadding="0"
							width="658" height="428">
							<tr vAlign="top">
								<td class="header-gray">Asunto:<br>
									<asp:textbox id="txtSubject" runat="server" CssClass="standard-text" Width="100%" Columns="30"
										TextMode="MultiLine" Rows="9" MaxLength="4000"></asp:textbox></td>
								<td class="header-gray">Resumen:<br>
									<asp:textbox id="txtSummary" runat="server" CssClass="standard-text" Width="100%" Columns="30"
										TextMode="MultiLine" Rows="9" MaxLength="4000"></asp:textbox></td>
							</tr>
							<tr vAlign="top">
								<TD style="HEIGHT: 27px" class="header-gray" colSpan="2">Fecha<br>
									<asp:textbox id="txtDate" runat="server"></asp:textbox></TD>
							</tr>
							<tr vAlign="top">
								<TD style="HEIGHT: 27px" class="header-gray" colSpan="2">Tipo de Respuesta<br>
									<asp:dropdownlist id="ddlTipoRespuesta" runat="server" AutoPostBack="True">
										<asp:ListItem Value="-1" Selected="True">--Seleccione--</asp:ListItem>
										<asp:ListItem Value="3">Atendida</asp:ListItem>
										<asp:ListItem Value="2">Nuevo requerimiento</asp:ListItem>
										<asp:ListItem Value="1">Versi&#243;n</asp:ListItem>
										<asp:ListItem Value="0">Rechazada</asp:ListItem>
									</asp:dropdownlist><asp:requiredfieldvalidator id="rfvTipoRespuesta" runat="server" InitialValue="-1" ControlToValidate="ddlTipoRespuesta"
										ErrorMessage="Debe seleccionar un tipo de respuesta"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr vAlign="top">
								<td style="HEIGHT: 142px" class="header-gray" colSpan="2">Respuesta<br>
									<asp:textbox id="txtReply" runat="server" Width="100%" Columns="30" TextMode="MultiLine" Rows="9"
										MaxLength="4000"></asp:textbox></td>
							</tr>
							<tr>
								<td style="HEIGHT: 14px" colSpan="2" align="right"><asp:requiredfieldvalidator id="rfvRespuesta" runat="server" ControlToValidate="txtReply" ErrorMessage="Debe introducir una respuesta">Debe introducir una respuesta</asp:requiredfieldvalidator>
									<asp:button id="btnAttach" runat="server" CssClass="cmdSubmit" Width="110px" Height="23px" Text="Adjuntar Archivos"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button style="Z-INDEX: 0" id="btnSave" runat="server" CssClass="cmdSubmit" Width="57" Height="23"
										Text="Guardar"></asp:button></td>
							</tr>
							<tr vAlign="top">
								<td height="15"></td>
							</tr>
							<%= createAttach() %>
						</table>
						<asp:textbox id="txtTurnarId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtState" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtClick" runat="server" Visible="False"></asp:textbox></td>
					<!-- End Right Panel --> </TD>
					<td width="11"></td>
				</tr>
				<tr>
					<td height="15" vAlign="top" colSpan="5"><IMG src="/gestion/images/spacer.gif" width="15" height="15"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
