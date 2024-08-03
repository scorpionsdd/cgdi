<%@ Page language="c#" Codebehind="mail_alternate.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.frmAlternate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>turnar_correspondencia</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<LINK href="/gestion/css/worksheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="/gestion/general.js" type="text/javascript"></script>
	</HEAD>
	<body onload="changeFrames();">
		<form id="frmAlternate" method="post" runat="server">
			<TABLE class="standardPageTable" id="Table1" width="100%" border="0">
				<TR>
					<TD class="applicationTitle" noWrap>Correspondencia&nbsp;Recibida</TD>
					<TD align="right" colSpan="3" class="applicationTitle"><IMG title="Cerrar y regresar a la página principal" onclick="parent.frames[1].location.href = '/gestion/portal/main.aspx'; parent.frames[0].location.href = '/gestion/portal/header.aspx';"
							alt="Cerrar" src="../images/close_red.gif" align="absMiddle">
					</TD>
				</TR>
				<TR>
					<TD id="divSearchOptions" colSpan="4">
						<TABLE class="fullScrollMenu" border="0">
							<TR class="fullScrollMenuHeader">
								<TD class="fullScrollMenuTitle">Documentos&nbsp;Recibidos</TD>
								<TD class="fullScrollMenuTitle" vAlign="middle" noWrap align="left" colSpan="2"><asp:linkbutton id="lnkExecute" runat="server" Height="20" CssClass="cmdSubmit">Ejecutar Consulta</asp:linkbutton></TD>
								<TD class="fullScrollMenuTitle" vAlign="middle" noWrap align="left"></TD>
							</TR>
							<TR>
								<TD class="header-gray" vAlign="middle" noWrap>Tipo de Correspondencia</TD>
								<TD noWrap colSpan="2"><asp:dropdownlist id="ddlAlternateCopy" runat="server" Height="25px" CssClass="standard-text">
										<asp:ListItem Value="0" Selected="True">Turnados</asp:ListItem>
										<asp:ListItem Value="1">Con copia</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="header-gray" noWrap colSpan="3">Período:&nbsp;&nbsp; Del &nbsp;
									<asp:textbox id="txtFromDate" runat="server" MaxLength="10" Width="75px"></asp:textbox>&nbsp;&nbsp; 
									Al&nbsp;&nbsp;&nbsp;
									<asp:textbox id="txtToDate" runat="server" MaxLength="20" Width="75px"></asp:textbox>&nbsp;&nbsp; 
									dd/mm/yyyy</TD>
							</TR>
							<TR>
								<TD class="header-gray" vAlign="middle" noWrap>Seleccionar</TD>
								<TD noWrap colSpan="2"><asp:dropdownlist id="dropShow" runat="server" Height="25px" CssClass="standard-text">
										<asp:ListItem Value="0" Selected="True">Pendientes</asp:ListItem>
										<asp:ListItem Value="1">Tr&#225;mite</asp:ListItem>
										<asp:ListItem Value="3">Concluidos</asp:ListItem>
										<asp:ListItem Value="4">Todos</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="header-gray" noWrap></TD>
							</TR>
							<TR>
								<TD class="header-gray" vAlign="middle" noWrap>
								Nivel de Desglose de Area
								<TD noWrap colSpan="2"><asp:dropdownlist id="ddlNivelArea" runat="server">
										<asp:ListItem Value="1">Direcci&#243;n General</asp:ListItem>
										<asp:ListItem Value="2">Direcci&#243;n</asp:ListItem>
										<asp:ListItem Value="3">SubDirecci&#243;n</asp:ListItem>
										<asp:ListItem Value="4">Gerencia</asp:ListItem>
										<asp:ListItem Value="5">SubGerencia</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD noWrap>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap colSpan="4" width="100%" height="100%">
						<TABLE class="dataGrid" border="0" width="100%" height="100%">
							<% if (ddlAlternateCopy.SelectedItem.Value.ToString() == "0") { %>
							<%= gsHeader %>
							<% } else { %>
							<%= gsHeader1 %>
							<% } %>
							<% if (gsBody.Length !=  0) { %>
							<%= gsBody%>
							<% } else { %>
							<TR>
								<TD colSpan="8"><b>No encontré ningún turno con el criterio de búsqueda proporcionado.</b></TD>
							</TR>
							<% } %>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtFromDate"
				ErrorMessage="Fecha Inválida"></asp:requiredfieldvalidator><asp:comparevalidator id="CompareValidator1" runat="server" Display="None" ControlToValidate="txtFromDate"
				ErrorMessage="Debe digitar una fecha válida de inicio" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator><asp:comparevalidator id="CompareValidator2" runat="server" Display="None" ControlToValidate="txtToDate"
				ErrorMessage="Debe digitar una fecha válida final" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>
