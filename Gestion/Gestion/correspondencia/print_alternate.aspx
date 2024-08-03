<%@ Page language="c#" Codebehind="print_alternate.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.print_alternate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Gestión de Correspondencia</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="print_mail" method="post" runat="server">
			<TABLE cellSpacing="11" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td style="WIDTH: 224px; HEIGHT: 81px">&nbsp;
							<asp:image id="Image1" runat="server" Height="40px" Width="141px" ImageUrl="/Gestion/Images/logo.gif"></asp:image></td>
						<td class="applicationTitle" style="WIDTH: 165px" align="center"><asp:label id="lblSystem" runat="server"></asp:label><br>
							<asp:label id="lblArea" Font-Size="XX-Small" Runat="server"></asp:label></td>
						<td noWrap align="right">Autor:&nbsp;
							<asp:label id="lblAutor" runat="server"></asp:label><BR>
							<br>
							Volante Número:&nbsp;
							<asp:label id="lblVolante" runat="server" Font-Size="Larger" Font-Bold="True"></asp:label><BR>
							<BR>
							Fecha de Elaboración:&nbsp;&nbsp;
							<asp:label id="lblElaborationDate" runat="server"></asp:label><BR>
						</td>
					</tr>
				</TBODY>
			</TABLE>
			Datos del Documento
			<TABLE class="tan-border" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR>
						<TD noWrap><STRONG>Referencia</STRONG></TD>
						<TD class="report-text" noWrap><asp:label id="lblReference" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 10px" noWrap width="10%"><STRONG>Fecha del documento</STRONG>
						</TD>
						<TD class="report-text" style="HEIGHT: 10px" noWrap><asp:label id="lblDocumentDate" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top" noWrap><STRONG>Area que recibe</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 17px" vAlign="top" width="70%"><asp:label id="lblToArea" runat="server"></asp:label><br>
							<asp:label id="lblToName" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top" noWrap><STRONG>Remitente</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 30px" vAlign="top" width="70%"><asp:label id="lblFromArea" runat="server"></asp:label><BR>
							<asp:label id="lblFromName" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap><STRONG>Tipo de documento</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 30px" vAlign="top" width="70%"><asp:label id="lblDocumentType" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap><STRONG>Nombre del proyecto</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 19px" vAlign="top" width="70%"><asp:label id="lblNombreProyecto" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap><STRONG>Tipo de solicitud</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 17px" vAlign="top" width="70%"><asp:label id="lblTipoSolicitud" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap><STRONG>Tipo de atención</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 17px" vAlign="top" width="70%"><asp:label id="lblTipoAtencion" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap><STRONG>Asunto</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 15px" width="70%"><asp:label id="lblSubject" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap><STRONG>Anexos</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 15px" width="70%"><asp:label id="lblAttached" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top" noWrap><STRONG>Resumen</STRONG></TD>
						<TD class="report-text" style="HEIGHT: 15px" width="70%"><asp:label id="lblSummary" runat="server"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
			<%= sTurnar %>
			<%= sCCpara %>
			<%= sFirma %>
		</form>
	</body>
</HTML>
