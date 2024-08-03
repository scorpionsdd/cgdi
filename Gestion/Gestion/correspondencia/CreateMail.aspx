<%@ Page language="c#" Codebehind="CreateMail.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.CreateMail" %>
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
		
			function closeForm()
			{
				var sUrl = '/gestion/portal/main.aspx';
				window.location.href = sUrl;
			}
		</script>
	</HEAD>
	<body>
		<form id="CreateMail" method="post" runat="server">
			<TABLE id="Table1" class="standardPageTable" width="100%">
				<TR>
					<TD class="applicationTitle" noWrap>Registro de Documentos</TD>
					<TD class="applicationTitle" align="center"><asp:label id="lblMsg" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;
						<asp:label id="lblVolante" runat="server"></asp:label></TD>
					<TD style="HEIGHT: 22px" class="applicationTitle" colSpan="2" noWrap align="right"><IMG title="Cerrar y regresar a la página principal" onclick="closeForm();" align="absMiddle"
							src="../Images/close_red.gif">
					</TD>
					</TD></TR>
				<TR>
					<TD width="100%" colSpan="4" noWrap align="right">
						<TABLE class="fullScrollMenu" border="1">
							<TR class="fullScrollMenuHeader">
								<TD style="WIDTH: 238px; HEIGHT: 29px" class="fullScrollMenuTitle" vAlign="top" noWrap>Información 
									General del Documento</TD>
								<td style="HEIGHT: 29px" class="fullScrollMenuTitle" width="100%" noWrap align="right">&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnCreate" tabIndex="99" runat="server" CssClass="cmdSubmit" Text="Crear Documento"></asp:button>&nbsp;</td>
							</TR>
							<TR>
								<TD style="WIDTH: 238px; HEIGHT: 30px" vAlign="top" noWrap><FONT class="header-gray" size="3"><A href="javascript:OpenDocumentType('dropDocumentType', true)">Tipo 
											de Documento</A></FONT><br>
									<asp:textbox id="txtDocumentType" runat="server" Enabled="False"></asp:textbox></TD>
								<TD style="HEIGHT: 30px" colSpan="2"><asp:textbox id="txtTipoDocumento" tabIndex="1" runat="server" Width="225px"></asp:textbox><asp:dropdownlist id="dropDocumentType" tabIndex="1" runat="server" Height="22px" AutoPostBack="True"></asp:dropdownlist></TD>
							<TR>
								<TD style="WIDTH: 20.08%; HEIGHT: 30px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Fecha 
										del Documento</FONT></TD>
								<td style="HEIGHT: 30px" width="80%" colSpan="2"><asp:textbox id="txtDate" tabIndex="2" runat="server" CssClass="standard-text" Height="22px"
										MaxLength="10"></asp:textbox></td>
							</TR>
							<TR>
								<td style="WIDTH: 20.08%; HEIGHT: 26px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Referencia</FONT></td>
								<td style="HEIGHT: 26px" width="80%" colSpan="2"><asp:textbox id="txtReference" tabIndex="2" runat="server" CssClass="standard-text" Width="70%"
										Height="22px" MaxLength="60"></asp:textbox>&nbsp;</td>
							</TR>
							<TR>
								<TD class="header-gray" style="WIDTH: 20%; HEIGHT: 28px" vAlign="middle" noWrap>Nombre 
									del Proyecto</TD>
								<TD style="HEIGHT: 28px" width="80%" colSpan="2"><asp:TextBox id="txtNombreProyecto" runat="server" Width="414px" tabIndex="3"></asp:TextBox>
									<asp:RequiredFieldValidator id="rfvNombreProyecto" runat="server" ErrorMessage="Se requiere el nombre del proyecto"
										ControlToValidate="txtNombreProyecto"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<td style="WIDTH: 20.08%; HEIGHT: 26px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Tipo 
										de Solicitud</FONT></td>
								<td style="HEIGHT: 26px" width="80%" colSpan="2">
									<asp:dropdownlist id="ddlTipoSolicitud" tabIndex="4" runat="server" Height="22px"></asp:dropdownlist>
									&nbsp;&nbsp;<asp:Label ID="lblTipoSolicitud" Runat="server"></asp:Label></td>
							</TR>
							<TR>
								<td style="WIDTH: 20.08%; HEIGHT: 26px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Tipo 
										de Atención</FONT></td>
								<td style="HEIGHT: 26px" width="80%" colSpan="2"><asp:dropdownlist id="ddlTipoAtencion" tabIndex="5" runat="server" Height="22px" AutoPostBack="True"></asp:dropdownlist>
									&nbsp;&nbsp;<asp:Label ID="lblTipoAtencion" Runat="server"></asp:Label>
									<asp:TextBox style="Z-INDEX: 0" id="txtFecha" runat="server" Visible="False"></asp:TextBox></td>
							</TR>
							<TR>
								<td style="WIDTH: 20.08%; HEIGHT: 102px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Asunto</FONT></td>
								<td style="HEIGHT: 102px" width="80%" colSpan="2"><asp:textbox id="txtSubject" tabIndex="6" runat="server" Width="100%" TextMode="MultiLine" Rows="5"
										Columns="1"></asp:textbox><asp:requiredfieldvalidator id="rvSubject" runat="server" Height="17px" ControlToValidate="txtSubject" ErrorMessage="Debe digitar el asunto"
										Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rvDocumentDate" runat="server" ControlToValidate="txtDate" ErrorMessage="Debe digitar una fecha válida"
										Display="None"></asp:requiredfieldvalidator><asp:comparevalidator id="cvDocumentDate" runat="server" ControlToValidate="txtDate" ErrorMessage="Debe digitar una fecha válida"
										Display="None" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator><asp:requiredfieldvalidator id="rvDocumentType" runat="server" ControlToValidate="txtTipoDocumentoId" ErrorMessage="Debe elegir el tipo de documento"
										Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rvFrom" runat="server" ControlToValidate="txtRemAreaId" ErrorMessage="Debe elegir un remitente"
										Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rvFromName" runat="server" ControlToValidate="txtRemitenteId" ErrorMessage="Debe elegir el titular de la dependencia"
										Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rvTo" runat="server" ControlToValidate="txtDestAreaId" ErrorMessage="Debe elegir el area que recibe"
										Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rvToName" runat="server" ControlToValidate="txtDestinatarioId" ErrorMessage="Debe de elegir al titular del área"
										Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rvSign" runat="server" ControlToValidate="txtSignId" ErrorMessage="Debe elegir el firmante"
										Display="None"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD style="WIDTH: 20.08%" class="header-gray" vAlign="top" noWrap>Resumen</TD>
								<TD width="80%" colSpan="2"><asp:textbox id="txtSummary" tabIndex="7" runat="server" Width="100%" Height="82px" MaxLength="4000"
										TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 20.08%; HEIGHT: 84px" class="header-gray" vAlign="top" noWrap>
									<% if (gsMailType == "E") { %>
									<A href="javascript:getSender()">Remitente</A>
									<% } else {%>
									Remitente
									<% } %>
									<br>
									<asp:textbox id="txtRemitenteArea" runat="server" Enabled="False" Width="232px"></asp:textbox><BR>
									<asp:textbox id="txtRemitenteName" runat="server" Enabled="False" Width="232px"></asp:textbox><BR>
									<asp:textbox id="txtRemitenteJob" runat="server" Enabled="False" Width="232px"></asp:textbox></TD>
								<TD style="HEIGHT: 84px" width="80%" colSpan="2">
									<P><asp:dropdownlist id="dropFrom" tabIndex="8" runat="server" Width="75%" Height="30px" AutoPostBack="True"
											Visible="False"></asp:dropdownlist><asp:textbox id="txtFrom" tabIndex="9" runat="server" Width="70%" MaxLength="512"></asp:textbox><asp:textbox id="txtCutDate" tabIndex="99" runat="server" Width="80px" MaxLength="10"></asp:textbox><asp:button id="btnSelCutDate" tabIndex="99" runat="server" Text="Ejecutar" Width="56px" CausesValidation="False"></asp:button></P>
									<P align="left"><asp:dropdownlist id="dropFromName" tabIndex="10" runat="server" Width="75%" Height="30px" AutoPostBack="True"
											Visible="False"></asp:dropdownlist><asp:textbox id="txtFromName" tabIndex="11" runat="server" Width="70%" MaxLength="512"></asp:textbox>&nbsp;&nbsp;</P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 20.08%; HEIGHT: 100px" class="header-gray" vAlign="top" noWrap><FONT class="header-gray" size="3">Area 
										que Recibe<br>
										<asp:textbox id="txtDestinatarioArea" runat="server" Enabled="False" Width="232px"></asp:textbox><br>
										<asp:textbox id="txtDestinatarioName" runat="server" Enabled="False" Width="232px"></asp:textbox><BR>
										<asp:textbox id="txtDestinatarioJob" runat="server" Enabled="False" Width="232px"></asp:textbox></FONT></TD>
								<TD style="HEIGHT: 100px" width="80%" colSpan="2"><p><asp:dropdownlist id="dropTo" tabIndex="12" runat="server" Width="75%" Height="21px" AutoPostBack="True"></asp:dropdownlist></p>
									<p><asp:dropdownlist id="dropToName" tabIndex="13" runat="server" Width="75%" Height="22px" AutoPostBack="True"></asp:dropdownlist></p>
									<P><asp:textbox id="txtAreaId" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtVolante" runat="server" Visible="False"></asp:textbox></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 20.08%; HEIGHT: 40px" class="header-gray" vAlign="top" noWrap>Firma<br>
									<asp:textbox id="txtSign" runat="server" Enabled="False" Width="232px"></asp:textbox></TD>
								<TD style="HEIGHT: 40px" width="80%" colSpan="2"><asp:dropdownlist id="dropFirma" tabIndex="14" runat="server" Width="75%" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 20.08%; HEIGHT: 34px" class="header-gray"><FONT class="header-gray" size="3">Requiere 
										Trámite?</FONT></TD>
								<TD style="WIDTH: 80%; HEIGHT: 34px" height="34" width="88" colSpan="2"><asp:radiobuttonlist id="rblRequire" tabIndex="15" runat="server" CssClass="header-gray" Width="100%"
										RepeatDirection="Horizontal">
										<asp:ListItem Value="Si" Selected="True">Si</asp:ListItem>
										<asp:ListItem Value="No">No</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 20.08%" class="header-gray" vAlign="top" noWrap>Anexo</TD>
								<TD width="80%" colSpan="2" align="left"><asp:textbox id="txtAttached" tabIndex="16" runat="server" Width="100%" Height="67px" MaxLength="1024"
										TextMode="MultiLine"></asp:textbox></TD>
							</TR>
						</TABLE>
						<asp:button id="btnContinue" tabIndex="99" runat="server" CssClass="cmdSubmit" Text="Crear Documento"></asp:button></TD>
					<TD width="100%" noWrap></TD>
				</TR>
			</TABLE>
			<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary><asp:textbox id="tbDocumentTypeId" runat="server" Width="8px" MaxLength="12"></asp:textbox><asp:textbox id="txtDocumentoBisId" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtDocumentoId" runat="server" Width="48px" Visible="False"></asp:textbox><asp:textbox id="txtAction" runat="server" Width="48px" Visible="False"></asp:textbox><asp:textbox id="txtMailType" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtSenderID" runat="server" Width="1px" MaxLength="12"></asp:textbox><asp:textbox id="txtDestinatarioId" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtDestAreaId" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtRemitenteId" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtRemAreaId" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtSignId" runat="server" Width="40px" Visible="False"></asp:textbox><asp:textbox id="txtTipoDocumentoId" runat="server" Width="40px" Visible="False"></asp:textbox></form>
		</TR></TABLE>
		<P></P>
		</TD></TR></TABLE></TD></TR></TABLE></FORM>
	</body>
</HTML>
