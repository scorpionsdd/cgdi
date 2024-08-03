<%@ Page language="c#" Codebehind="document_display.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.document_display" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreateMail</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="/gestion/scripts/script.js"></script>
	</HEAD>
	<body>
		<form id="CreateMail" method="post" runat="server">
			<TABLE class="standardPageTable" id="Table1">
				<TBODY>
					<TR>
						<TD class="applicationTitle" style="HEIGHT: 20px" noWrap width="80%">Datos del 
							Documento</TD>
						<TD style="HEIGHT: 20px" noWrap align="right" colSpan="3" class="applicationTitle"><asp:button id="btnReturn" runat="server" Text="Regresar"></asp:button>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD noWrap align="left" colSpan="4">
							<TABLE class="fullScrollMenu" border="1">
								<TBODY>
									<TR class="fullScrollMenuHeader">
										<TD class="fullScrollMenuTitle" style="WIDTH: 20%; HEIGHT: 20px" vAlign="top" noWrap>Información 
											General del Documento</TD>
										<td class="fullScrollMenuTitle" style="HEIGHT: 20px" noWrap align="right" width="80%">&nbsp;</td>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 41px" vAlign="middle" noWrap><FONT class="header-gray" size="3">Tipo 
												de Documento</FONT>
										</TD>
										<TD style="HEIGHT: 41px" width="80%" colSpan="2"><asp:label id="lblDocumentType" runat="server" Width="80%" Height="22px"></asp:label></TD>
									<TR>
										<td class="header-gray" style="WIDTH: 20%; HEIGHT: 30px" vAlign="top" noWrap><FONT class="header-gray" size="3">Fecha 
												del Documento</FONT></td>
										<td style="HEIGHT: 30px" width="80%" colSpan="2"><asp:label id="lblDocumentDate" runat="server" Width="80%" Height="22px"></asp:label></td>
									</TR>
									<TR>
										<td class="header-gray" style="WIDTH: 20%; HEIGHT: 39px" vAlign="top" noWrap><FONT class="header-gray" size="3">Referencia</FONT></td>
										<td style="HEIGHT: 39px" width="80%" colSpan="2"><asp:label id="lblReference" runat="server" Width="80%" Height="25px"></asp:label></td>
									</TR>
									<TR>
										<td class="header-gray" style="WIDTH: 20%; HEIGHT: 16px" vAlign="top" noWrap><FONT class="header-gray" size="3">Nombre 
												Proyecto</FONT></td>
										<td style="HEIGHT: 16px" width="80%" colSpan="2"><asp:label id="lblNombreProyecto" runat="server" Width="80%" Height="22px"></asp:label></td>
									</TR>
									<TR>
										<td class="header-gray" style="WIDTH: 20%; HEIGHT: 39px" vAlign="top" noWrap><FONT class="header-gray" size="3">Tipo 
												de Solicitud</FONT></td>
										<td style="HEIGHT: 39px" width="80%" colSpan="2"><asp:label id="lblTipoSolicitud" runat="server" Width="80%" Height="25px"></asp:label></td>
									</TR>
									<TR>
										<td class="header-gray" style="WIDTH: 20%; HEIGHT: 39px" vAlign="top" noWrap><FONT class="header-gray" size="3">Tipo 
												de Atención</FONT>
										</td>
										<td style="HEIGHT: 39px" width="80%" colSpan="2"><asp:label id="lblTipoAtencion" runat="server" Width="80%" Height="25px"></asp:label>
											&nbsp;&nbsp;
											<asp:label id="lblFechaAtencion" runat="server" Width="80%" Height="25px"></asp:label>
										</td>
									</TR>
									<TR>
										<td class="header-gray" style="WIDTH: 20%; HEIGHT: 16px" vAlign="top" noWrap><FONT class="header-gray" size="3">Asunto</FONT></td>
										<td style="HEIGHT: 16px" width="80%" colSpan="2"><asp:label id="lblSubject" runat="server" Width="80%" Height="22px"></asp:label></td>
									</TR>
									<TR>
										<TD class="header-gray" style="WIDTH: 20%; HEIGHT: 20px" vAlign="top" noWrap>Resumen</TD>
										<TD width="80%" colSpan="2" style="HEIGHT: 20px"><asp:label id="lblSummary" runat="server" Width="80%" Height="63px"></asp:label></TD>
									</TR>
									<TR>
										<TD class="header-gray" style="WIDTH: 20%; HEIGHT: 51px" vAlign="middle" noWrap>Remitente
											<FONT class="header-gray" size="3"></FONT>
										</TD>
										<TD style="HEIGHT: 51px" width="80%" colSpan="2">
											<asp:label id="lblFromArea" runat="server" Width="80%" Height="25px"></asp:label><BR>
											<asp:label id="lblFromTitular" runat="server" Width="80%" Height="22px"></asp:label><BR>
											<asp:label id="lblFromJob" runat="server" Width="80%"></asp:label>
										</TD>
									</TR>
									<TR>
										<TD class="header-gray" style="WIDTH: 20%; HEIGHT: 43px" vAlign="middle" noWrap><FONT class="header-gray" size="3">Area 
												que Recibe</FONT></TD>
										<TD style="HEIGHT: 43px" width="80%" colSpan="2">
											<asp:label id="lblToArea" runat="server" Width="80%" Height="24px"></asp:label><BR>
											<asp:label id="lblToTitular" runat="server" Width="80%" Height="22px"></asp:label><BR>
											<asp:label id="lblToJob" runat="server" Width="80%"></asp:label>
										</TD>
									</TR>
									<TR>
										<TD class="header-gray" style="WIDTH: 20%; HEIGHT: 34px"><FONT class="header-gray" size="3">Requiere 
												Trámite?</FONT></TD>
										<TD style="WIDTH: 80%; HEIGHT: 34px" width="88" colSpan="2" height="34"><asp:label id="lblStepRequire" runat="server" Height="25px"></asp:label></TD>
									</TR>
									<TR>
										<TD class="header-gray" style="WIDTH: 20%; HEIGHT: 64px" vAlign="top" noWrap>Anexo</TD>
										<TD style="HEIGHT: 64px" align="left" width="80%" colSpan="2"><asp:label id="lblAnexo" runat="server" Width="80%" Height="52px"></asp:label></TD>
									</TR>
								</TBODY>
							</TABLE>
							<asp:TextBox id="txtDocumentId" runat="server" Visible="False"></asp:TextBox>
							<asp:TextBox id="txtSend" runat="server" Visible="False"></asp:TextBox>
							<asp:TextBox id="txtAction" runat="server" Visible="False"></asp:TextBox>
							<asp:TextBox id="txtMailType" runat="server" Visible="False"></asp:TextBox>
							<asp:TextBox id="txtQueryType" runat="server" Visible="False"></asp:TextBox>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE>
		<P></P>
		</TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
