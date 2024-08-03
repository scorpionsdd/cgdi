<%@ Page language="c#" Codebehind="ccpara_reply.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.ccpara_reply" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Editor Respuestas</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function CloseWindow(){
				window.close();
				return false;
			}
		</script>
	</HEAD>
	<body onload="<%= gsClick%>">
		<form id="EditReplyAlternate" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%" border="1">
				<TR>
					<TD class="applicationTitle" style="HEIGHT: 26px" noWrap>Editor de Respuestas</TD>
					<td style="HEIGHT: 26px" align="right" colSpan="2" class="applicationTitle">
						<IMG id="clicker" style="CURSOR: hand" onclick="CloseWindow()" src="/gestion/images/close_red.gif">
					</td>
				</TR>
				<TR>
					<TD noWrap colSpan="4">
						<TABLE class="fullScrollMenu" cellPadding="1" width="100%" border="0">
							<tr>
								<TD class="header-gray">Volante No.</TD>
								<TD colSpan="2"><asp:label id="lblVolante" runat="server" Font-Size="X-Small"></asp:label></TD>
							</tr>
							<TR>
								<TD class="header-gray">Referencia</TD>
								<TD colSpan="2"><asp:label id="lblReference" runat="server" Width="100%"></asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray">Fecha del Documento</TD>
								<TD colSpan="2"><asp:label id="lblDocumentDate" runat="server" Height="21px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray">Fecha de Registro</TD>
								<TD colSpan="2"><asp:label id="lblRegistryDate" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray">Asunto</TD>
								<TD colSpan="2"><asp:textbox id="txtSubject" runat="server" Width="100%" Height="79px" TextMode="MultiLine" Enabled="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="header-gray">Resumen</TD>
								<TD colSpan="2"><asp:textbox id="txtSummary" runat="server" Width="100%" Height="72px" TextMode="MultiLine" Enabled="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="header-gray">Remitente</TD>
								<TD colSpan="2"><asp:label id="lblSender" runat="server" Width="100%"></asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray">Destinatario</TD>
								<TD colSpan="2"><asp:label id="lblAddressee" runat="server" width="100%"></asp:label></TD>
							</TR>
							<tr>
								<TD class="header-gray">Fecha</TD>
								<TD colSpan="2"><asp:textbox id="txtDate" runat="server"></asp:textbox></TD>
							</tr>
							<tr>
								<td class="header-gray">Respuesta</td>
								<td colSpan="2"><asp:textbox id="txtReply" runat="server" Width="100%" Height="178px" TextMode="MultiLine" MaxLength="512"></asp:textbox></td>
							</tr>
							<tr>
								<td align="right" colSpan="4"><asp:button id="btnSave" runat="server" Text="Guardar"></asp:button></td>
							</tr>
						</TABLE>
						<asp:textbox id="txtTurnarId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtState" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtClick" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtDocumentId" runat="server" Visible="False"></asp:textbox></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
