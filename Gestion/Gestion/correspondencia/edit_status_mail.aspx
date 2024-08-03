<%@ Page language="c#" Codebehind="edit_status_mail.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_status_mail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>turnar_correspondencia</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="receive_mail" method="post" runat="server">
			<TABLE class="fullScrollMenu" id="Table1" cellSpacing="1" cellPadding="1" width="100%"
				border="0">
				<TR>
					<TD class="applicationTitle" noWrap>Registro de Estatus&nbsp;</TD>
					<TD noWrap align="right" colSpan="3"><IMG title="Cerrar y regresar a la página principal" onclick="window.close();" src="/Gestion/Images/close_red.gif"
							align="absMiddle">
					</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="4">
						<TABLE border="1" width="100%">
							<TR class="fullScrollMenuHeader">
								<TD class="fullScrollMenuTitle" vAlign="top" noWrap>Actualiza&nbsp; Volante</TD>
								<td noWrap align="center" class="fullScrollMenuTitle">&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnSave" runat="server" Text="Guardar" CssClass="cmdSubmit" Width="103px"></asp:button>&nbsp;</td>
							</TR>
							<TR>
								<TD class="header-gray" noWrap>Volante No.</TD>
								<TD noWrap colSpan="2"><asp:label id="lblVolante" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray" noWrap>Referencia</TD>
								<TD noWrap colSpan="2"><asp:label id="lblReference" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray" noWrap style="HEIGHT: 72px">Asunto</TD>
								<TD noWrap colSpan="2">
									<asp:TextBox id="txtSubject" runat="server" Width="100%" TextMode="MultiLine" Enabled="False"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="header-gray" noWrap>Resumen</TD>
								<TD noWrap colSpan="2">
									<asp:TextBox id="txtSummary" runat="server" Width="100%" TextMode="MultiLine" Enabled="False"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD noWrap class="header-gray">Remitente</TD>
								<TD noWrap colSpan="2"><asp:label id="lblSender" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="header-gray" noWrap>
									Destinatario
								</TD>
								<TD noWrap colSpan="2">
									<asp:Label id="lblAddressee" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" noWrap class="header-gray">Estatus</TD>
								<TD noWrap colSpan="2"><asp:radiobuttonlist id="rblStatus" runat="server" AutoPostBack="True">
										<asp:ListItem Value="Pendiente" Selected="True">Pendiente</asp:ListItem>
										<asp:ListItem Value="Tramite">Tramite</asp:ListItem>
										<asp:ListItem Value="Concluido">Concluido</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="header-gray" style="WIDTH: 150px" noWrap>Fecha</TD>
								<TD noWrap colSpan="2">
									<asp:TextBox id="txtResponseDate" runat="server"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 150px" noWrap class="header-gray">Respuesta</TD>
								<TD noWrap colSpan="2"><asp:textbox id="txtResponse" runat="server" Width="100%" TextMode="MultiLine" MaxLength="4000"
										Rows="13"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
