<%@ Page language="c#" Codebehind="edit_mail_receive.aspx.cs" AutoEventWireup="false" Inherits="Gestion.Correspondencia.edit_mail_receive" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>insert_mail_addressee</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/gestion/resources/applications.css" type="text/css" rel="stylesheet">
		<script language="C#" runat="server">

			void Close_Form(object sender, ImageClickEventArgs e) 
			{
				Response.Redirect("receive_mail.aspx");
			}

		</script>
	</HEAD>
	<body>
		<form id="edit_mail_addressee" method="post" runat="server">
			<TABLE class="standardPageTable" width="100%" align="right" border="1">
				<TR>
					<TD colspan="5" style="HEIGHT: 20px" align="right" class="applicationTitle"><asp:imagebutton id="ImageButton1" onclick="Close_Form" runat="server" ImageUrl="/Gestion/Images/close_red.gif"
							ToolTip="Cerrar y regresar a la página principal" CssClass="cmdSubmit"></asp:imagebutton></TD>
				</TR>
				<tr>
					<TD colspan="5" class="applicationTitle" style="HEIGHT: 20px" noWrap>Registro de 
						Correspondencia</TD>
				</tr>
				<tr>
					<TD class="header-gray" style="WIDTH: 120px; HEIGHT: 13px" noWrap>Fecha:
					</TD>
					<TD style="HEIGHT: 13px" noWrap colSpan="3"><asp:textbox id="txtFecha" runat="server" MaxLength="10" CssClass="standard-text"></asp:textbox></TD>
				<TR>
					<td class="header-gray" style="WIDTH: 120px; HEIGHT: 13px" noWrap>Respuesta:
					</td>
					<td style="HEIGHT: 13px" noWrap colSpan="3"><asp:textbox id="txtRespuesta" runat="server" CssClass="standard-text" Width="100%" Height="216px"
							TextMode="MultiLine"></asp:textbox></td>
				</TR>
				<TR>
					<TD noWrap align="right" colSpan="4"><asp:button id="btnSend" runat="server" CssClass="standard-text" Text="Aceptar"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
