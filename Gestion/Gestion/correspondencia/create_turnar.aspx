<%@ Page language="c#" Codebehind="create_turnar.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.create_turnar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreateMail</title>
		<META content="text/html; charset=windows-1252" http-equiv="Content-Type">
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="/gestion/resources/applications.css">
		<script language="javascript" src="/gestion/scripts/script.js"></script>
		<script language="javascript">
			
			function call_back() {
				
				window.close();
				window.opener.document.all.lnkExecute.click();
				return false;
	
			
			}
			
		</script>
	</HEAD>
	<body>
		<form id="CreateMail" method="post" runat="server">
			<TABLE id="Table1" class="standardPageTable">
				<TBODY>
					<TR>
						<TD style="HEIGHT: 20px" class="applicationTitle" width="80%" noWrap>Datos del 
							Documento</TD>
						<TD style="HEIGHT: 20px" class="applicationTitle" colSpan="3" noWrap align="right"><a class="cmdSubmit" onclick="window.close();" href="">Regresar</a>
						</TD>
					</TR>
					<TR>
						<TD colSpan="4" noWrap align="right">
							<TABLE class="fullScrollMenu" border="1">
								<TBODY>
									<TR class="fullScrollMenuHeader">
										<TD style="WIDTH: 20%; HEIGHT: 29px" class="fullScrollMenuTitle" vAlign="top" noWrap>Información 
											General del Documento</TD>
										<td style="HEIGHT: 29px" class="fullScrollMenuTitle" width="80%" noWrap align="center"><asp:button id="btnTurnar" runat="server" Text="ReTurnar" CssClass="cmdSubmit" Width="84px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 27px" class="header-gray" vAlign="middle" noWrap>Firma</TD>
										<TD style="HEIGHT: 27px" width="80%" colSpan="2"><asp:dropdownlist id="ddlSignature" runat="server" Width="414px"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 22px" vAlign="middle" noWrap><FONT class="header-gray" size="3">Tipo 
												de Documento</FONT>
										</TD>
										<TD style="HEIGHT: 22px" width="80%" colSpan="2"><asp:label id="lblDocumentType" runat="server" Width="305px" Height="22px"></asp:label></TD>
									<TR>
										<td style="WIDTH: 20%; HEIGHT: 29px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Fecha 
												del Documento</FONT></td>
										<td style="HEIGHT: 29px" width="80%" colSpan="2"><asp:label id="lblDocumentDate" runat="server" Width="136px" Height="22px"></asp:label></td>
									</TR>
									<TR>
										<td style="WIDTH: 20%; HEIGHT: 41px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Referencia</FONT></td>
										<td style="HEIGHT: 41px" width="80%" colSpan="2"><asp:label id="lblReference" runat="server" Width="393px" Height="25px"></asp:label></td>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 28px" class="header-gray" vAlign="middle" noWrap>Nombre 
											Proyecto</TD>
										<TD style="HEIGHT: 28px" width="80%" colSpan="2"><asp:textbox id="txtNombreProyecto" runat="server" Width="414px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 27px" class="header-gray" vAlign="middle" noWrap>Solicitud</TD>
										<TD style="HEIGHT: 27px" width="80%" colSpan="2"><asp:label id="lblTipoSolicitud" runat="server" Width="414px"></asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 28px" class="header-gray" vAlign="middle" noWrap>Atención</TD>
										<TD style="HEIGHT: 28px" width="80%" colSpan="2"><asp:label id="lblTipoAtencion" runat="server" Width="414px"></asp:label>&nbsp;Fecha
											<asp:textbox style="Z-INDEX: 0" id="txtFechaAtencion" runat="server" Visible="False"></asp:textbox></TD>
									</TR>
									<TR>
										<td style="WIDTH: 20%; HEIGHT: 50px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Asunto</FONT></td>
										<td style="HEIGHT: 50px" width="80%" colSpan="2"><asp:label id="lblSubject" runat="server" Width="80%" Height="43px"></asp:label></td>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 63px" class="header-gray" vAlign="top" noWrap>Resumen</TD>
										<TD style="HEIGHT: 63px" width="80%" colSpan="2"><asp:label id="lblSummary" runat="server" Width="80%" Height="82px"></asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 52px" class="header-gray" vAlign="middle" noWrap>Remitente
											<FONT class="header-gray" size="3"></FONT>
										</TD>
										<TD style="HEIGHT: 52px" width="80%" colSpan="2"><asp:label id="lblFromArea" runat="server" Width="80%" Height="25px"></asp:label><BR>
											<asp:label id="lblFromTitular" runat="server" Width="80%" Height="22px"></asp:label><BR>
											<asp:label id="lblFromJob" runat="server" Width="80%"></asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 48px" class="header-gray" vAlign="middle" noWrap><FONT class="header-gray" size="3">Area 
												que Recibe</FONT></TD>
										<TD style="HEIGHT: 48px" width="80%" colSpan="2"><asp:label id="lblToArea" runat="server" Width="80%" Height="24px"></asp:label><BR>
											<asp:label id="lblToTitular" runat="server" Width="80%" Height="22px"></asp:label><BR>
											<asp:label id="lblToJob" runat="server" Width="80%"></asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 17px" class="header-gray"><FONT class="header-gray" size="3">Requiere 
												Trámite?</FONT></TD>
										<TD style="WIDTH: 80%; HEIGHT: 17px" height="17" width="88" colSpan="2"><asp:label id="lblStepRequire" runat="server" Height="25px"></asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 20%; HEIGHT: 51px" class="header-gray" vAlign="top" noWrap>Anexo</TD>
										<TD style="HEIGHT: 51px" width="80%" colSpan="2" align="left"><asp:label id="lblAnexo" runat="server" Width="80%" Height="71px"></asp:label></TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<P><asp:textbox id="txtDocumentTurnarId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtDocumentId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtDocumentBisId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtFromAreaId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtToAreaId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtFromTitularId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtToTitularId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtTipoRemitente" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtTipoDocumentoId" runat="server" Visible="False"></asp:textbox>
				<asp:TextBox style="Z-INDEX: 0" id="txtTipoSolicitudId" runat="server" Visible="False"></asp:TextBox>
				<asp:TextBox style="Z-INDEX: 0" id="txtTipoAtencionId" runat="server" Visible="False"></asp:TextBox></P>
		</form>
		</TR></TBODY></TABLE></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
